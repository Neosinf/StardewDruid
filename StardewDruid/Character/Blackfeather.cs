using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.FruitTrees;
using StardewValley.Internal;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
    public class Blackfeather : StardewDruid.Character.Character
    {

        public Blackfeather()
        {
        }

        public Blackfeather(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Blackfeather;

            }

            LoadOutLady();

            LoadOutKick();

            cooldownInterval = 90;

            restSet = true;

        }

        public override void DrawLaunch(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {
            
            Rectangle useFrame = specialFrames[specials.launch][netDirection.Value][specialFrame];

            int specialHover = 0;

            switch (specialFrame)
            {
                case 0:
                case 3:

                    specialHover = 16;

                    break;

                case 1:
                case 2:

                    specialHover = 32;

                    break;

            }

            b.Draw(
                characterTexture,
                spritePosition - new Vector2(0,specialHover),
                useFrame,
                Color.White * fade,
                0.0f,
                new Vector2(useFrame.Width / 2, useFrame.Height / 2),
                setScale,
                SpriteAngle() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawShadow(b, spritePosition, drawLayer, fade);

            int rate = Math.Abs((int)(Game1.currentGameTime.TotalGameTime.TotalSeconds % 12) - 6);

            Color circleColour = new Color(256 - (rate * 21), 256 - (rate * 21), 256 - rate);

            b.Draw(
                 Mod.instance.iconData.sheetTextures[IconData.tilesheets.ritual],
                 spritePosition + new Vector2(0, 14 * setScale),
                 new Rectangle(0, 96, 64, 48),
                 Color.White * fade,
                 0f,
                 new Vector2(32, 24),
                 setScale,
                 0,
                 drawLayer - 0.0005f
             );

            b.Draw(
                 Mod.instance.iconData.sheetTextures[IconData.tilesheets.ritual],
                 spritePosition + new Vector2(0, 14 * setScale),
                 new Rectangle(64, 96, 64, 48),
                 circleColour * fade,
                 0f,
                 new Vector2(32, 24),
                 setScale,
                 0,
                 drawLayer - 0.0006f
             );
        }

        public override List<Vector2> RoamAnalysis()
        {

            List<Vector2> collection = base.RoamAnalysis();

            if (Game1.currentSeason == "winter")
            {

                return collection;

            }

            List<Vector2> scarelist = new List<Vector2>();

            int takeABreak = 0;

            foreach (Dictionary<Vector2, StardewValley.Object> dictionary in currentLocation.Objects)
            {

                foreach (KeyValuePair<Vector2, StardewValley.Object> keyValuePair in dictionary)
                {

                    if (keyValuePair.Value.IsScarecrow())
                    {

                        Vector2 scareVector = new(keyValuePair.Key.X * 64f, keyValuePair.Key.Y * 64f);

                        scarelist.Add(scareVector);

                        takeABreak++;

                    }

                    if (takeABreak >= 4)
                    {

                        scarelist.Add(new Vector2(-1f));

                        takeABreak = 0;

                    }

                }

                if (scarelist.Count >= 30)
                {

                    break;

                }

            }

            scarelist.AddRange(collection);

            return scarelist;

        }

        public override bool TargetWork()
        {

            if (new SpawnIndex(currentLocation).cultivate == false)
            {

                return false;

            }

            if (currentLocation.objects.Count() <= 0)
            {

                return false;

            }

            List<Vector2> tileVectors;

            for (int i = 0; i < 5; i++)
            {

                tileVectors = ModUtility.GetTilesWithinRadius(currentLocation, occupied, i);

                foreach (Vector2 scareVector in tileVectors)
                {

                    if (currentLocation.objects.ContainsKey(scareVector))
                    {

                        if (currentLocation.Objects[scareVector].IsScarecrow())
                        {

                            string workString = Game1.season.ToString() + Game1.dayOfMonth.ToString() + "_" + currentLocation.Name + "_" + scareVector.X.ToString() + "_" + scareVector.Y.ToString();

                            if (workRegister.Contains(workString))
                            {

                                continue;

                            }

                            ResetActives();

                            LookAtTarget(scareVector * 64, true);

                            netSpecial.Set((int)specials.invoke);

                            specialTimer = 90;

                            workVector = scareVector;

                            return true;

                        }

                    }

                }

            }

            return false;

        }

        public override void PerformWork()
        {

            if (specialTimer == 80)
            {

                if (currentLocation.Name == Game1.player.currentLocation.Name && Utility.isOnScreen(Position, 128))
                {

                    Mod.instance.iconData.DecorativeIndicator(currentLocation, Position, IconData.decorations.weald, 3f, new());

                    TemporaryAnimatedSprite skyAnimation = Mod.instance.iconData.SkyIndicator(currentLocation, Position, IconData.skies.valley, 1f, new() { interval = 1000, });

                    skyAnimation.scaleChange = 0.002f;

                    skyAnimation.motion = new(-0.064f, -0.064f);

                    skyAnimation.timeBasedMotion = true;

                    Game1.player.currentLocation.playSound("discoverMineral", null, 1000);

                }

            }

            if (specialTimer == 50)
            {

                CharacterHandle.RetrieveInventory(CharacterHandle.characters.Blackfeather);

                if (Mod.instance.chests[CharacterHandle.characters.Blackfeather].Items.Count > 0)
                {

                    Cultivate cultivateEvent = new();

                    cultivateEvent.EventSetup(currentLocation, workVector * 64, "blackfeather_cultivate_" + workVector.ToString());

                    cultivateEvent.inventory = Mod.instance.chests[CharacterHandle.characters.Blackfeather].Items;

                    cultivateEvent.EventActivate();

                }

                string workString = Game1.season.ToString() + Game1.dayOfMonth.ToString() + "_" + currentLocation.Name + "_" + workVector.X.ToString() + "_" + workVector.Y.ToString();

                if (!workRegister.Contains(workString))
                {

                    workRegister.Add(workString);

                }

            }

            if (specialTimer == 20 && !Game1.IsRainingHere(currentLocation) && Game1.currentSeason != "winter")
            {

                Artifice artificeHandle = new();

                artificeHandle.ArtificeScarecrow(currentLocation, workVector,true);

            }

        }

        /*public override bool SmashAttack(StardewValley.Monsters.Monster monster)
        {
            
            return SpecialAttack(monster);

        }*/

        public override void ConnectSweep()
        {

            SpellHandle swipeEffect = new(Game1.player, Position, 256, Mod.instance.CombatDamage() / 2)
            {
                type = SpellHandle.Spells.explode,

                display = IconData.impacts.boltnode,

                instant = true,

                added = new() { SpellHandle.Effects.push, }
            };

            if (Mod.instance.activeEvent.Count == 0)
            {

                swipeEffect.sound = SpellHandle.Sounds.thunder_small;

            }

            Mod.instance.spellRegister.Add(swipeEffect);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.launch);

            specialTimer = 60;

            SetCooldown(40, 0.5f);

            LookAtTarget(monster.GetBoundingBox().Center.ToVector2(), true);

            SpellHandle special = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, Mod.instance.CombatDamage())
            {
                type = SpellHandle.Spells.lightning,

                factor = 6,

                counter = -12
            };

            if (Mod.instance.activeEvent.Count == 0)
            {
                
                special.sound = SpellHandle.Sounds.thunder_small;

            }

            //special.added = new() { SpellHandle.effects.shock, };

            Mod.instance.spellRegister.Add(special);

            return true;

        }


    }

}
