using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast.Mists;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Journal;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Constants;
using StardewValley.Locations;
using StardewValley.Network;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.Intrinsics.X86;
using System.Threading;
using System.Timers;
using xTile.Layers;

namespace StardewDruid.Cast
{

    public class WispHandle
    {

        public GameLocation location;

        public Vector2 tile;

        public Vector2 position;

        public Vector2 origin;

        public LightSource light;

        public int body;

        public int eye;

        public int frame;

        public int interval;

        public int cooldown;

        public int gaze;

        public Vector2 movement;

        public Vector2 previous;

        public Vector2 destination;

        public List<Vector2> traverse = new();

        public int tier;

        public float rotate;

        public float scale;

        public float fade;

        public float fadeTo;

        public float damageFarmers;

        public float damageMonsters;

        public StardewDruid.Monster.Boss bossMonster;

        public enum wisptypes
        {
            mists,
            winds,
            death,
        }

        public wisptypes type;

        public WispHandle(GameLocation Location, Vector2 Tile)
        {

            type = wisptypes.mists;

            location = Location;

            tile = Tile;

            position = (tile * 64) + new Vector2(32);

            origin = position;

            scale = 3.2f;

            fade = 0.3f;

            fadeTo = 0.6f;

            body = Mod.instance.randomIndex.Next(3);

            eye = Mod.instance.randomIndex.Next(3);

            frame = Mod.instance.randomIndex.Next(16);

            cooldown = Mod.instance.randomIndex.Next(12);

            damageMonsters = Mod.instance.CombatDamage();

            Behaviour();

            Light();

        }

        public WispHandle(GameLocation Location, Vector2 Tile, wisptypes Type, Vector2 Position, float Fade)
        {

            type = Type;

            location = Location;

            tile = Tile;

            origin = (tile * 64) + new Vector2(32);

            tier = (int)Vector2.Distance(origin,Position) / 64;

            position = Position;

            body = Mod.instance.randomIndex.Next(3);

            eye = 0;

            frame = Mod.instance.randomIndex.Next(16);

            cooldown = Mod.instance.randomIndex.Next(5);

            fade = 0.05f;

            fadeTo = Fade;

            scale = 2.5f + (0.25f * tier);

            Light();

        }

        public void Light()
        {

            if(fadeTo <= 0.5)
            {

                return;

            }

            string id = "18465_" + (position.X * 10000 + position.Y).ToString();

            if (Game1.currentLightSources.ContainsKey(id))
            {

                return;

            }

            light = new LightSource(id, LightSource.lantern, position + new Vector2(32), 4f, Microsoft.Xna.Framework.Color.Black * 0.75f);

            Game1.currentLightSources.Add(id, light);

        }


        public void draw(SpriteBatch b)
        {

            if (!Utility.isOnScreen(position, 128) || Game1.player.currentLocation.Name != location.Name)
            {

                return;
            
            }

            // -----------------------------------------------------------

            Vector2 localPosition = Game1.GlobalToLocal(position);

            Texture2D wispTexture;

            switch (type)
            {
                default:
                case wisptypes.mists:
                    wispTexture = Mod.instance.iconData.wispTexture;
                    break;
                case wisptypes.winds:
                    wispTexture = Mod.instance.iconData.windwispTexture;
                    break;
                case wisptypes.death:
                    wispTexture = Mod.instance.iconData.deathwispTexture;
                    break;

            }

            float layer = position.Y / 10000 + 0.0128f;

            int useFrame = frame;

            SpriteEffects flip = SpriteEffects.None;

            if (frame > 11)
            {

                useFrame = 15 - frame;

            }
            else if (frame > 7)
            {
                useFrame = frame - 8;

                flip = SpriteEffects.FlipHorizontally;

            }
            else if (frame > 3)
            {

                useFrame = 7 - frame;

                flip = SpriteEffects.FlipHorizontally;

            }

            int gazing = 0;

            SpriteEffects backwards = SpriteEffects.None;

            switch (gaze)
            {
                case 1: gazing = 1; break;
                case 2: gazing = 2; break;
                case 3: gazing = 3; break;
                case 5: gazing = 3; backwards = SpriteEffects.FlipHorizontally; break;
                case 6: gazing = 2; backwards = SpriteEffects.FlipHorizontally; break;
                case 7: gazing = 1; backwards = SpriteEffects.FlipHorizontally; break;
            }

            Microsoft.Xna.Framework.Rectangle hoverSource = new(useFrame * 32, body * 64, 32, 64);

            Microsoft.Xna.Framework.Rectangle eyeSource = new(gazing * 32, 192 + (eye * 64), 32, 64);

            b.Draw(
                wispTexture,
                localPosition,
                hoverSource,
                Microsoft.Xna.Framework.Color.White * fade,
                rotate,
                new Vector2(16,32),
                scale,
                flip,
                layer
            );

            b.Draw(
                wispTexture,
                localPosition,
                eyeSource,
                Microsoft.Xna.Framework.Color.White * fade * 0.5f,
                rotate,
                new Vector2(16,32),
                scale,
                backwards,
                layer + 0.0001f
            );

            b.Draw(
                Mod.instance.iconData.shadowTexture,
                localPosition + new Vector2(0,32*scale),
                Mod.instance.iconData.shadowRectangle,
                Microsoft.Xna.Framework.Color.White * fade,
                0f,
                new Vector2(24),
                scale/2,
                0,
                layer - 0.0001f
            );

        }

        public void shutdown()
        {

            if (light != null)
            {

                Game1.currentLightSources.Remove(light.Id);

            }

            if (damageMonsters > 0)
            {

                Mod.instance.iconData.AnimateQuickWarp(location, position, false, IconData.warps.mist);

            }

        }

        public void Movement()
        {

            position += movement;

            switch (type)
            {

                case wisptypes.winds:

                    Vector2 diff = origin - position;

                    rotate = (float)Math.Atan2(diff.Y, diff.X);

                    Vector2 angle = new Vector2(diff.Y, diff.X * -1);

                    movement = ModUtility.PathFactor(Vector2.Zero, angle) * (tier * 3);

                    if (Vector2.Distance(origin, position) > tier * 64)
                    {

                        movement += ModUtility.PathFactor(position, origin);

                    }

                    break;

                case wisptypes.death:

                    Vector2 deathdiff = origin - position;

                    rotate = (float)Math.Atan2(deathdiff.Y, deathdiff.X);

                    Vector2 deathangle = new Vector2(deathdiff.Y, deathdiff.X * -1);

                    movement = ModUtility.PathFactor(Vector2.Zero, deathangle) * (tier * 3);

                    movement += ModUtility.PathFactor(origin, position) * tier;

                    break;

            }

            if (light != null)
            {

                light.position.Set(position + new Vector2(32));

            }

        }

        public bool Behaviour()
        {

            frame++;

            if (frame > 15)
            {

                frame = 0;

            }

            // Movement ---------------------------------------------------------

            switch (type)
            {

                case wisptypes.mists:

                    gaze = ModUtility.DirectionToTarget(position, Game1.player.Position)[2];

                    float roam = Vector2.Distance(origin, position);

                    if(roam >= 192)
                    {

                        int ret = ModUtility.DirectionToTarget(position, origin)[2];

                        movement = ModUtility.DirectionAsVector(ret) * 3;
                    
                    }
                    else if(roam <= 32)
                    {
    
                        movement = ModUtility.DirectionAsVector(Mod.instance.randomIndex.Next(8)) * 3;

                    }

                    break;

                case wisptypes.winds:
                case wisptypes.death:

                    if (fade < fadeTo)
                    {

                        fade += 0.025f;

                    }
                    else
                    if (fade > (fadeTo + 0.5f))
                    {

                        fade -= 0.025f;

                    }

                    break;

            }

            // Effects ---------------------------------------------------------

            switch (type)
            {

                case wisptypes.mists:

                    cooldown++;

                    if (cooldown > 18) // ~9-10 seconds
                    {

                        cooldown = 10;

                        List<StardewValley.Monsters.Monster> mistVictims = ModUtility.MonsterProximity(Game1.player.currentLocation, position, 192, true);

                        if (mistVictims.Count > 0)
                        {

                            cooldown = 0;

                            SpellHandle bolt = new(Game1.player, position - new Vector2(32), 192, damageMonsters)
                            {
                                type = SpellHandle.Spells.explode,

                                instant = true,

                                sound = SpellHandle.Sounds.thunder_small,

                                display = IconData.impacts.boltnode
                            };

                            //bolt.added = new() { SpellHandle.effects.push, };

                            Mod.instance.spellRegister.Add(bolt);

                        }
                        else
                        if (Vector2.Distance(position, Game1.player.Position) <= 256)
                        {

                            cooldown = 0;

                            SpellHandle mists = new(position - new Vector2(32), 192, IconData.impacts.mists, new() { SpellHandle.Effects.drain, }) { displayRadius = 3, };

                            Mod.instance.spellRegister.Add(mists);

                        }

                    }

                    break;

                case wisptypes.winds:

                    if(damageMonsters == -1)
                    {

                        break;

                    }

                    cooldown++;

                    if (cooldown > 5)
                    {

                        cooldown = 0;

                        List<StardewValley.Monsters.Monster> windVictims = ModUtility.MonsterProximity(Game1.player.currentLocation,position, 192, true);

                        if (windVictims.Count > 0)
                        {

                            SpellHandle spell = new(Game1.player, windVictims.First().Position, 256, damageMonsters)
                            {
                                type = SpellHandle.Spells.explode,

                                instant = true,

                                scheme = IconData.schemes.fates,

                                display = IconData.impacts.glare,

                                displayRadius = 3
                            };

                            spell.added = new() { Rite.FatesCurse() };

                            Mod.instance.spellRegister.Add(spell);

                        }

                    }

                    break;

                case wisptypes.death:

                    if (damageFarmers == -1)
                    {

                        break;

                    }

                    if (!ModUtility.MonsterVitals(bossMonster, location))
                    {

                        break;

                    }

                    cooldown++;

                    if (cooldown > 9)
                    {

                        cooldown = 5;

                        if (Vector2.Distance(position, Game1.player.Position) <= 256)
                        {

                            cooldown = 0;

                            SpellHandle spell = new(Game1.player.currentLocation, Game1.player.Position, position, 192, damageFarmers, damageMonsters)
                            {
                                type = SpellHandle.Spells.explode,

                                instant = true,

                                scheme = IconData.schemes.death,

                                display = IconData.impacts.skull,

                                boss = bossMonster
                            };

                            Mod.instance.spellRegister.Add(spell);

                        }

                    }

                    break;

            }

            return true;

        }

    }

}
