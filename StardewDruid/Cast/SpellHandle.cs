using Microsoft.Xna.Framework;
using StardewValley;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using StardewModdingAPI;
using StardewDruid.Data;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Fates;
using System.Security.Claims;
using static StardewDruid.Character.Character;
using StardewValley.Locations;
using xTile.Layers;
using xTile.Tiles;
using StardewDruid.Event;
using StardewValley.Projectiles;
using StardewDruid.Render;
using StardewValley.Monsters;
using StardewValley.Objects.Trinkets;
using System.Threading;
using StardewValley.Constants;
using StardewDruid.Location.Druid;


namespace StardewDruid.Cast
{
    public class SpellHandle
    {

        // specified

        public GameLocation location;

        public Vector2 destination;

        public Vector2 origin;

        public int counter;

        public int radius;

        public int factor;

        public float damageFarmers;

        public float damageMonsters;

        public float critical;

        public float criticalModifier;

        public int power;

        public int explosion;

        public int terrain;

        // derived

        public List<TemporaryAnimatedSprite> animations = new();

        public TemporaryAnimatedSprite cursor;

        public MissileHandle missileHandle;

        public Vector2 impact;

        public Monster.Boss boss;

        public List<StardewValley.Monsters.Monster> monsters = new();

        public bool queried;

        public bool external;

        public bool local;

        public bool instant;

        public bool shutdown;

        public bool invisibility;

        public enum spells
        {
            effect,
            swipe,
            explode,
            beam,
            lightning,
            missile,
            bolt,
            blackhole,
            deathwind,
            echo,
            deathecho,
            bubbleecho,
            fireecho,
            crate,
            teleport,
            warpstrike,
            honourstrike,
            levitate,
            trick,
            warp,
            warpout,
            warpin,

        }

        public spells type;

        public IconData.cursors indicator = IconData.cursors.none;

        public IconData.schemes scheme = IconData.schemes.none;

        public MissileHandle.missiles missile = MissileHandle.missiles.none;

        public enum effects
        {
            none,
            sap,
            drain,
            stone,
            embers,
            homing,
            aiming,
            push,
            gravity,
            harvest,
            tornado,
            knock,
            morph,
            mug,
            daze,
            doom,
            immolate,
            capture,
            //shock,
            glare,
            crate,
            teleport,
            stomping,
            charm,
            bore,
            jump,
            blackhole,
            freeze,

        }

        public List<effects> added = new();

        public IconData.impacts display = IconData.impacts.none;

        public enum sounds
        {
            none,
            silent,
            explosion,
            flameSpellHit,
            shadowDie,
            boulderBreak,
            dropItemInWater,
            discoverMineral,
            swordswipe,
            thunder_small,
            thunder,
            dustMeep,
            yoba,
            secret1,
            wand,
            doorCreak,
            fireball,
            bubbles,
            batScreech,
            batFlap,
            Ship,
            hammer,
            leafrustle,
            slime,
            ghost,
            owl,
            crow,
            cat,
            dog_bark,
            getNewSpecialItem,
            warrior,
            pullItemFromWater,
            openChest,
        }

        public sounds sound = sounds.none;

        public SpellHandle(Farmer farmer, List<StardewValley.Monsters.Monster> Monsters, float damage)
        {

            location = farmer.currentLocation;

            origin = farmer.Position + new Vector2(32);

            destination = Monsters.First().Position + new Vector2(32);

            radius = 128;

            factor = 2;

            damageFarmers = -1f;

            damageMonsters = damage;

            impact = destination;

            type = spells.explode;

            monsters = Monsters;

        }

        public SpellHandle(Farmer farmer, Vector2 Destination, int Radius, float damage)
        {

            location = farmer.currentLocation;

            origin = farmer.Position;

            destination = Destination;

            radius = Radius;

            factor = 2;

            damageFarmers = -1f;

            damageMonsters = damage;

            impact = destination;

            type = spells.explode;


        }

        public SpellHandle(GameLocation Location, Vector2 Destination, Vector2 Origin, int Radius = 128, float vsFarmers = -1f, float vsMonsters = -1f)
        {

            location = Location;

            origin = Origin;

            destination = Destination;

            radius = Radius;

            factor = 2;

            damageFarmers = vsFarmers;

            damageMonsters = vsMonsters;

            impact = Destination;

            type = spells.explode;

        }

        public SpellHandle(Vector2 Destination, int Radius, IconData.impacts Display, List<effects> Added)
        {

            location = Game1.player.currentLocation;

            origin = Destination;

            destination = Destination;

            radius = Radius;

            factor = 2;

            damageFarmers = -1f;

            damageMonsters = -1f;

            impact = Destination;

            type = spells.effect;

            display = Display;

            sound = sounds.none;

            added = Added;

        }

        public void SpellQuery()
        {

            List<int> array = new()
            {
                (int)destination.X,
                (int)destination.Y,
                (int)origin.X,
                (int)origin.Y,
                radius,
                Convert.ToInt32(type),
                Convert.ToInt32(scheme),
                Convert.ToInt32(indicator),
                Convert.ToInt32(display),
                factor,
                Convert.ToInt32(missile),
                instant ? 1 : 0,
            };

            QueryData query = new()
            {
                name = type.ToString(),

                value = System.Text.Json.JsonSerializer.Serialize(array),

                location = location.Name,

            };

            Mod.instance.EventQuery(query, QueryData.queries.SpellHandle);

        }

        public SpellHandle(GameLocation Location, List<int> spellData)
        {

            external = true;

            location = Location;

            destination = new Vector2(spellData[0], spellData[1]);

            origin = new Vector2(spellData[2], spellData[3]);

            radius = spellData[4];

            impact = destination;

            damageFarmers = -1;

            damageMonsters = -1;

            type = (spells)spellData[5];

            scheme = (IconData.schemes)spellData[6];

            indicator = (IconData.cursors)spellData[7];

            display = (IconData.impacts)spellData[8];

            sound = sounds.none;

            factor = spellData[9];

            missile = (MissileHandle.missiles)spellData[10];

            instant = (spellData[11] == 1);

        }

        public void register()
        {

            Mod.instance.spellRegister.Add(this);

        }

        public bool Update()
        {

            counter++;

            if (counter <= 0)
            {

                return true;

            }

            if(location == null)
            {

                return false;

            }

            if (Context.IsMultiplayer && !queried)
            {

                if (!external && !local && type != spells.effect)
                {

                    SpellQuery();

                }

                queried = true;

            }

            if (shutdown)
            {

                return false;

            }

            if (counter % 10 == 0)
            {

                if (boss != null)
                {

                    if (!ModUtility.MonsterVitals(boss, location))
                    {

                        Shutdown();

                        return false;

                    }

                }

                if (monsters.Count > 0)
                {

                    for (int m = monsters.Count - 1; m >= 0; m--)
                    {

                        if (!ModUtility.MonsterVitals(monsters[m], location))
                        {

                            monsters.Remove(monsters[m]);

                        }

                    }

                }

            }

            switch (type)
            {

                case spells.effect:

                    if (counter == 1)
                    {

                        RadialDisplay();

                        ApplyEffects(impact);

                    }

                    if (counter == 120)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case spells.swipe:

                    if (counter == 1)
                    {

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, new());

                        RadialDisplay();

                        ApplyEffects(impact);

                    }

                    if (counter == 60)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case spells.explode:

                    if (counter == 1)
                    {
                        if (instant)
                        {

                            counter = 15;

                        }
                        else
                        {

                            TargetCursor();

                        }

                    }

                    if (counter == 15)
                    {

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, new());

                        RadialExplode();

                        RadialDisplay();

                        ApplyEffects(impact);

                        ClearCursor();

                    }

                    if (counter == 120)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case spells.bolt:

                    if (counter == 1)
                    {
                        BoltHandle bolt = new();

                        bolt.Setup(location, origin, destination, MissileHandle.missiles.bolt, scheme, factor);

                        missileHandle = bolt;

                        if (sound == sounds.none)
                        {

                            sound = sounds.flameSpellHit;

                        }

                        RadialDisplay();

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, monsters);

                        ApplyEffects(impact);

                    }

                    missileHandle.Update();

                    if (missileHandle.shutdown)
                    {

                        Shutdown();

                    }

                    return true;

                case spells.missile:

                    if (counter == 1)
                    {

                        if (added.Contains(effects.aiming))
                        {

                            if(monsters.Count > 0)
                            {

                                destination = monsters.First().Position;

                            }

                        }

                        missileHandle = new();

                        missileHandle.Setup(location, origin, destination, missile, scheme, factor);

                        TargetCursor();

                    }

                    missileHandle.Update();

                    impact = missileHandle.projectilePosition;

                    if (missileHandle.shutdown)
                    {

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, new());

                        RadialExplode();

                        RadialDisplay();

                        ApplyEffects(impact);

                        ClearCursor();

                        Shutdown();

                        return false;

                    }

                    return true;


                case spells.beam:
                case spells.lightning:

                    if (counter == 1)
                    {

                        LaunchBeam();

                        LightRadius(origin);

                    }

                    if (counter == 24)
                    {

                        GrazeDamage(1, 3, 3, true);

                    }

                    if (counter == 30)
                    {

                        GrazeDamage(2, 3, 3, true);

                    }

                    if (counter == 48)
                    {

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, new());

                        RadialDisplay();

                        ApplyEffects(impact);

                    }

                    if (counter == 90)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;


                case spells.echo:
                case spells.deathecho:
                case spells.bubbleecho:
                case spells.fireecho:

                    if (counter == 30)
                    {

                        LaunchEcho();

                        LightRadius(origin);

                    }

                    if (counter == 40)
                    {

                        GrazeDamage(1, 2, 4, true);

                    }

                    if (counter == 80)
                    {

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, new());

                        RadialDisplay();

                        ApplyEffects(impact);

                    }

                    if (counter == 150)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case spells.blackhole:

                    if (counter == 1)
                    {

                        LaunchBlackhole();

                        ApplyEffects(impact);

                    }

                    if (counter == 240)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case spells.deathwind:

                    if (counter == 1)
                    {

                        LaunchDeathWind();

                        ApplyEffects(impact);

                    }

                    if (counter == 15)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case spells.crate:

                    if (counter == 1)
                    {

                        CrateCreate();

                    }

                    if (counter == 61)
                    {

                        Shutdown();

                        CrateOpen();

                        location.playSound(sounds.doorCreak.ToString());

                    }

                    if (counter == 91)
                    {

                        Shutdown();

                        CrateRelease();

                        ApplyEffects(destination);

                        location.playSound(sounds.yoba.ToString());

                    }

                    if(counter >= 300)
                    {
                        Shutdown();

                        return false;

                    }

                    return true;

                case spells.teleport:

                    if(counter == 1)
                    {

                        TeleportStart();

                        if (!instant && factor > 1)
                        {

                            InvisibilityHide();

                        }

                    }

                    if(factor > 1)
                    {

                        TeleportCloser();

                    }

                    if (counter >= factor)
                    {

                        TeleportEffect();

                        TeleportEnd();

                        ApplyEffects(destination);

                        Shutdown();

                        return false;

                    }

                    return true;

                case spells.warpstrike:
                case spells.honourstrike:

                    if (counter == 1)
                    {

                        if(monsters.Count > 0)
                        {

                            if (ModUtility.MonsterVitals(monsters.First(), location))
                            {

                                impact = monsters.First().Position + new Vector2(32);

                            }
                            else
                            {

                                Shutdown();

                                return false;

                            }

                        }

                        WeaponRender warpstrike = new();

                        Character.CharacterHandle.characters warphero = Character.CharacterHandle.characters.Thanatoshi;

                        if (type == spells.honourstrike)
                        {

                            switch (factor)
                            {
                                default:
                                    warphero = Character.CharacterHandle.characters.HonourCaptain;
                                    break;

                                case 2:
                                case 3:
                                    warphero = Character.CharacterHandle.characters.HonourGuard;
                                    break;

                                case 4:
                                case 5:
                                    warphero = Character.CharacterHandle.characters.HonourKnight;
                                    break;

                            }

                            warpstrike.swordScheme = WeaponRender.swordSchemes.sword_stars;

                        }

                        animations = warpstrike.AnimateWarpStrike(location, warphero, impact, factor);

                    }

                    if (counter == 18)
                    {

                        RadialDisplay();

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, monsters);

                    }

                    if(counter == 30)
                    {

                        Shutdown();

                    }

                    return true;

                case spells.trick:

                    TrickDisplay();

                    Shutdown();

                    return false;

                case spells.warp:
                    
                    if (external)
                    {

                        Shutdown();

                        return false;

                    }

                    if(counter == 1)
                    {

                        SpellHandle warpOut = new(Game1.player.currentLocation, Vector2.Zero, origin);

                        warpOut.type = spells.warpout;

                        warpOut.scheme = scheme;

                        Mod.instance.spellRegister.Add(warpOut);

                        if(sound != sounds.none)
                        {

                            Game1.player.currentLocation.playSound(sound.ToString());

                        }

                    }

                    if (counter == 15)
                    {

                        WarpEffect();

                        Vector2 warpRender = destination * 64;

                        if(location is Grove)
                        {

                            warpRender.X += 32;

                        }

                        SpellHandle warpIn = new(location, Vector2.Zero, warpRender);

                        warpIn.type = spells.warpin;

                        warpIn.scheme = scheme;

                        Mod.instance.spellRegister.Add(warpIn);

                    }

                    return true;

                case spells.warpout:
                case spells.warpin:

                    if (counter == 1)
                    {

                        if (scheme != IconData.schemes.none)
                        {

                            Mod.instance.iconData.AnimateQuickWarp(location, origin, false, IconData.warps.circle, scheme);

                        }
                        else
                        {

                            Mod.instance.iconData.AnimateQuickWarp(location, origin, type == spells.warpout);

                        }
                    }

                    if (counter == 15)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

            }

            

            return true;
        }

        public void Shutdown()
        {

            shutdown = true;

            if (animations.Count > 0)
            {

                foreach (TemporaryAnimatedSprite animatedSprite in animations)
                {
                    if (animatedSprite.lightId != null)
                    {

                        Utility.removeLightSource(animatedSprite.lightId);

                    }

                    location.temporarySprites.Remove(animatedSprite);

                }

                animations.Clear();

            }

            if(cursor != null)
            {

                location.temporarySprites.Remove(cursor);

                cursor = null;

            }

            if (missileHandle != null)
            {

                missileHandle.Shutdown();

            }

            if(invisibility)
            {

                InvisibilityReveal();

            }


        }

        // ========================================= other combat effects

        public void LaunchBeam()
        {

            Vector2 diff = (destination - origin) / Vector2.Distance(origin, destination);

            //Vector2 diff = ModUtility.PathFactor(origin, destination);

            Vector2 middle = diff * 384f;

            impact = origin + diff * 688f;

            float rotate = (float)Math.Atan2(diff.Y, diff.X);

            Vector2 setPosition = origin + middle - new Vector2(384f, 96f);

            Texture2D texture;

            switch (type)
            {
                case spells.lightning:

                    texture = Mod.instance.iconData.lightningTexture;

                    break;

                default:
                case spells.beam:

                    texture = Mod.instance.iconData.laserTexture;

                    break;

            }

            TemporaryAnimatedSprite beam = new(0, 100f, 12, 1, setPosition, false, false)
            {
                sourceRect = new(0, 0, 256, 64),
                sourceRectStartingPos = new Vector2(0.0f, 0.0f),
                texture = texture,
                scale = 3f,
                timeBasedMotion = true,
                layerDepth = 999f,
                rotation = rotate,
                alpha = 0.9f,
                //color = Mod.instance.iconData.SchemeColour(scheme),
            };

            location.temporarySprites.Add(beam);

            animations.Add(beam);

            /*TemporaryAnimatedSprite beamInner = new(0, 100f, 12, 1, setPosition, false, false)
            {
                sourceRect = new(0, 384, 160, 32),
                sourceRectStartingPos = new Vector2(0.0f, 384.0f),
                texture = Mod.instance.iconData.laserTexture,
                scale = 4f,
                timeBasedMotion = true,
                layerDepth = 999f,
                rotation = rotate,
                alpha = 0.9f,
            };

            location.temporarySprites.Add(beamInner);

            animations.Add(beam);*/

        }

        public void LaunchEcho()
        {

            Vector2 diff = destination - origin;

            Vector2 point = diff / Vector2.Distance(origin, destination);

            float rotate = (float)Math.Atan2(diff.Y, diff.X);

            float source = 0f;

            float interval = 125f;

            int loops = 2;

            switch (type)
            {

                case spells.deathecho:

                    source = 64f;

                    break;

                case spells.bubbleecho:

                    source = 128f;

                    interval = 175f;

                    loops = 1;

                    break;

                case spells.fireecho:

                    source = 192f;

                    break;

            }

            List<Vector2> points = new()
            {
                origin + (point * factor * 48),
                origin + (point * factor * 96),
                origin + (point * factor * 144),
                origin + (point * factor * 192),
                origin + (point * factor * 240),
            };

            impact = origin + (point * factor * 216);

            for (int i = 0; i < points.Count; i++)
            {

                float echoScale = 1.5f + (0.5f * i);

                TemporaryAnimatedSprite part = new(0, interval, 5, loops, points[i] - new Vector2(32* echoScale, 32f*echoScale), false, false)
                {
                    sourceRect = new(0, (int)source, 64, 64),
                    sourceRectStartingPos = new Vector2(0.0f, source),
                    texture = Mod.instance.iconData.echoTexture,
                    scale = echoScale,
                    timeBasedMotion = true,
                    layerDepth = 999f,
                    rotation = rotate,
                    alpha = 0.75f,
                    alphaFade = 0.0005f,
                    scaleChange = 0.0005f,
                    motion = new Vector2(-0.032f,-0.032f),
                    delayBeforeAnimationStart = 250 * i,

                };

                location.temporarySprites.Add(part);

                animations.Add(part);

            }

        }

        public void LaunchBlackhole()
        {

            TemporaryAnimatedSprite staticAnimation = new(0, 99999f, 1, 1, impact - new Vector2(64, 64), false, false)
            {

                sourceRect = new(0, 0, 64, 64),

                sourceRectStartingPos = new Vector2(0, 0),

                texture = Mod.instance.iconData.gravityTexture,

                scale = 3f,

                layerDepth = location.IsOutdoors ? impact.Y / 10000 + 0.001f : 990f,

                rotationChange = -0.06f,

                timeBasedMotion = true,

                alpha = 0.75f,

            };

            location.temporarySprites.Add(staticAnimation);

            animations.Add(staticAnimation);

            TemporaryAnimatedSprite bandAnimation = new(0, 9999f, 1, 1, impact - new Vector2(64, 64), false, false)
            {

                sourceRect = new(64, 0, 64, 64),

                sourceRectStartingPos = new Vector2(64, 0),

                texture = Mod.instance.iconData.gravityTexture,

                scale = 3f,

                layerDepth = location.IsOutdoors ? impact.Y / 10000 + 0.002f : 991f,

                timeBasedMotion = true,

                alpha = 0.75f,

            };

            location.temporarySprites.Add(bandAnimation);

            animations.Add(bandAnimation);

            TemporaryAnimatedSprite cloudAnimation = Mod.instance.iconData.CreateSwirl(location, impact, 5f, new() { interval = 150, frame = 1, frames = 5, loops = 10, flip = true, alpha = 0.05f, layer = location.IsOutdoors ? impact.Y / 10000 : 990f });

            animations.Add(cloudAnimation);

        }
       
        public void LaunchDeathWind()
        {
            
            Winds windsNew = new();

            windsNew.EventSetup(origin,Rite.eventDeathwinds);

            windsNew.EventActivate();

            windsNew.eventLocked = true;

            if (external)
            {

                windsNew.WindArray(new(), WispHandle.wisptypes.death, 9);

            }
            else
            {

                windsNew.damageFarmers = damageFarmers;

                windsNew.damageMonsters = damageMonsters;

                windsNew.WindArray(new() { boss, }, WispHandle.wisptypes.death, 9);

            }

        }

        // ========================================= cosmetic
        public void TargetCursor()
        {

            if (indicator == IconData.cursors.none)
            {
                return;
            }

            CursorAdditional addEffects = new() { interval = 10000, scale = factor, scheme = scheme, alpha = 0.4f, };

            cursor = Mod.instance.iconData.CursorIndicator(location, impact, indicator, addEffects);
        
        }

        public void ClearCursor()
        {

            location.temporarySprites.Remove(cursor);

            cursor = null;

        }

        public void RadialDisplay()
        {

            LightRadius(impact);

            if (display != IconData.impacts.none)
            {

                float size = (radius - 32f) / 64f;

                size = Math.Min(4f, size);

                Mod.instance.iconData.ImpactIndicator(location, impact, display, size, new() { scheme = scheme,});

            }

            if (sound != sounds.none && sound != sounds.silent)
            {

                Game1.currentLocation.playSound(sound.ToString());

            }

        }

        public void LightRadius(Vector2 source)
        {

            string lightid = "18465_" + (source.X * 10000 + source.Y).ToString();

            TemporaryAnimatedSprite lightCircle = new(23, 200f, 6, 1, source, false, Game1.random.NextDouble() < 0.5)
            {
                texture = Game1.mouseCursors,
                lightId = lightid,
                lightRadius = 3f,
                lightcolor = Color.Black,
                alphaFade = 0.03f,
                Parent = location,
            };

            location.temporarySprites.Add(lightCircle);

            animations.Add(lightCircle);

        }

        public void TrickDisplay()
        {

            switch (factor)
            {

                case 0:

                    ModUtility.AnimateRandomFish(location, ModUtility.PositionToTile(impact));

                    return;

                case 1:

                    ModUtility.AnimateButterflySpray(location, ModUtility.PositionToTile(impact));

                    return;

            }

            List<NPC> villagers = ModUtility.GetFriendsInLocation(location, true);

            foreach (NPC witness in villagers)
            {

                if (Vector2.Distance(witness.Position, impact) >= 64)
                {

                    continue;

                }

                bool invis = true;

                if (external)
                {
                    
                    if (!witness.IsInvisible)
                    {

                        continue;

                    }

                    invis = false;

                }
                else if(witness.IsInvisible)
                {

                    continue;

                }

                switch (factor)
                {

                    default:
                    case 2:

                        Slimification slimification = new(witness, invis);

                        slimification.EventActivate();

                        break;

                    case 3:

                        Polymorph polymorph = new(witness, invis);

                        polymorph.EventActivate();

                        Mod.instance.iconData.ImpactIndicator(location, witness.Position - new Vector2(0,60), IconData.impacts.smoke, 5, new() { layer = 1f,});

                        break;

                    case 4:

                        Levitate levitation = new(witness, invis);

                        levitation.EventActivate();

                        break;

                }

                return;

            }

        }

        public void CrateCreate()
        {

            TemporaryAnimatedSprite crate = new(0, 1000, 1, 1, origin + new Vector2(16, 0), false, false)
            {

                sourceRect = new(0, 0, 32, 64),

                sourceRectStartingPos = new Vector2(0, 0),

                texture = Mod.instance.iconData.crateTexture,

                scale = 1f, //* size,

                layerDepth = origin.Y / 10000,

                scaleChange = 0.002f,

                motion = new Vector2(-0.032f, -0.096f),

                timeBasedMotion = true,

            };

            location.temporarySprites.Add(crate);

            animations.Add(crate);

        }

        public void CrateOpen()
        {

            TemporaryAnimatedSprite crateOpen = new(0, 167, 3, 1, origin - new Vector2(16, 96), false, false)
            {

                sourceRect = new(0, 0, 32, 64),

                sourceRectStartingPos = new Vector2(64, 0),

                texture = Mod.instance.iconData.crateTexture,

                scale = 3f,

                layerDepth = (origin.Y+64) / 10000,

            };

            location.temporarySprites.Add(crateOpen);

            animations.Add(crateOpen);

            location.playSound(SpellHandle.sounds.openChest.ToString());

            List<Vector2> sparkles = new()
            {
                new Vector2(64,-128),
                new Vector2(-64,0),
                new Vector2(48,-16),
                new Vector2(-48,-112),

            };

            for (int i = 0; i < sparkles.Count; i++)
            {

                Vector2 sparkleVector = origin + sparkles[i];

                Mod.instance.iconData.ImpactIndicator(location, sparkleVector, IconData.impacts.glare, 4, new() { scheme = scheme, });

            }


        }

        public void CrateRelease()
        {

            TemporaryAnimatedSprite crateOpen = new(0, 3000, 1, 1, origin - new Vector2(16, 96), false, false)
            {

                sourceRect = new(64, 0, 32, 64),

                sourceRectStartingPos = new Vector2(64, 0),

                texture = Mod.instance.iconData.crateTexture,

                scale =3f,

                layerDepth = (origin.Y + 64) / 10000,

            };

            location.temporarySprites.Add(crateOpen);

            animations.Add(crateOpen);

            Mod.instance.iconData.ImpactIndicator(location, origin - new Vector2(0, 32), IconData.impacts.sparkle, 2, new() { layer = origin.Y / 10000 + 0.001f, });

        }

        public void TeleportStart()
        {

            if (!added.Contains(effects.teleport))
            {

                added.Add(effects.teleport);

            }

            Mod.instance.iconData.AnimateQuickWarp(Game1.player.currentLocation, origin, true);

        }

        public void TeleportCloser()
        {

            float distance = Vector2.Distance(origin, destination);

            if(distance <= 32)
            {

                return;

            }

            if(factor <= 0)
            {

                return;

            }

            Game1.player.Position = origin + ((destination - origin) * (counter / factor));

        }

        public void TeleportEnd()
        {

            Mod.instance.iconData.AnimateQuickWarp(Game1.player.currentLocation, destination);

        }

        public void InvisibilityHide()
        {

            if (external)
            {

                return;

            }

            if (Mod.instance.eventRegister.ContainsKey(Rite.eventTransform))
            {
                return;

            }

            invisibility = true;

            Game1.displayFarmer = false;

            Game1.player.temporarilyInvincible = true;

            Game1.player.temporaryInvincibilityTimer = 1;

            Game1.player.currentTemporaryInvincibilityDuration = factor * 1000;

        }

        public void InvisibilityReveal()
        {

            if (external)
            {

                return;

            }

            if (Mod.instance.eventRegister.ContainsKey(Rite.eventTransform))
            {
                return;

            }

            invisibility = false;

            Game1.displayFarmer = true;

            Game1.player.temporarilyInvincible = false;

            Game1.player.temporaryInvincibilityTimer = 0;

            Game1.player.currentTemporaryInvincibilityDuration = 0;

            Game1.player.stopGlowing();

        }

        // ========================================= CONSEQUENCES

        public void GrazeDamage(int piece, int division, float reach = -1, bool effects = false)
        {

            if (external)
            {

                return;

            }

            if (reach == -1)
            {

                reach = factor;

            }

            Vector2 diff = (impact - origin) / division * piece;

            Vector2 current = origin + diff;

            ApplyDamage(current, reach * 32, (int)(damageFarmers / 2), (int)(damageMonsters / 2), new());

            if (effects)
            {
                
                ApplyEffects(current);

            }

        }

        public void ApplyDamage(Vector2 position, float reach, float hitfarmers, float hitmonsters, List<StardewValley.Monsters.Monster> individuals)
        {

            if (external)
            {

                return;

            }

            if (hitfarmers > 0 && boss != null)
            {

                List<Farmer> farmers = ModUtility.FarmerProximity(location, new() { position }, reach + 32, true);

                ModUtility.DamageFarmers(farmers, (int)hitfarmers, boss);

            }

            if (hitmonsters > 0)
            {

                if (individuals.Count == 0)
                {

                    individuals = ModUtility.MonsterProximity(location, new() { position }, reach + 32, true);

                }

                if (individuals.Count == 0)
                {

                    return;

                }

                bool push = false;

                foreach (effects effect in added)
                {

                    switch (effect)
                    {

                        case effects.push:

                            push = true;

                            break;

                    }

                }

                if (critical == 0f)
                {

                    List<float> criticals = Mod.instance.CombatCritical();

                    critical = criticals[0];

                    if (criticalModifier == 0f)
                    {

                        criticalModifier = criticals[1];

                    }

                }

                if (criticalModifier == 0f)
                {

                    List<float> criticals = Mod.instance.CombatCritical();

                    criticalModifier = criticals[1];

                }

                ModUtility.DamageMonsters(individuals, Game1.player, (int)hitmonsters, critical, criticalModifier, push);

            }

        }

        public void RadialExplode()
        {

            if (external)
            {

                return;

            }
            
            if (power > 0)
            {

                if (explosion == 0)
                {

                    explosion = 3;

                }

                ModUtility.Explode(location, ModUtility.PositionToTile(impact), Game1.player, explosion, power);

            }

            if (terrain > 0)
            {

                ModUtility.Reave(location, ModUtility.PositionToTile(impact), Game1.player, terrain);

            }

        }

        // ========================================= LOCAL ONLY EFFECTS

        public void ApplyEffects(Vector2 zone)
        {

            if (external)
            {

                return;

            }

            foreach (effects effect in added)
            {

                switch (effect)
                {
                    case effects.drain:

                        DrainEffect();

                        break;

                    case effects.sap:

                        SapEffect();

                        break;

                    case effects.stone:

                        StoneEffect();

                        break;

                    case effects.knock:
                    case effects.morph:
                    case effects.mug:
                    case effects.daze:
                    case effects.doom:
                    case effects.immolate:
                    case effects.glare:

                        CurseEffect(effect);

                        break;

                    case effects.embers:

                        EmberEffect(zone);

                        break;

                    case effects.gravity:

                        GravityEffect();

                        break;

                    case effects.harvest:

                        HarvestEffect();

                        break;

                    case effects.tornado:

                        TornadoEffect();

                        break;

                    case effects.capture:

                        CaptureEffect();

                        break;

                    /*case effects.shock:

                        ShockEffect();

                        break;*/

                    case effects.teleport:

                        TeleportEffect();

                        break;

                    case effects.crate:

                        CrateEffect();

                        break;

                    case effects.stomping:

                        StompEffect();

                        break;

                    case effects.charm:

                        CharmEffect();

                        break;

                    case effects.bore:

                        BoreEffect();

                        break;

                    case effects.jump:

                        JumpEffect();

                        break;

                    case effects.blackhole:

                        BlackHoleEffect();

                        break;

                    case effects.freeze:

                        FreezeEffect();

                        break;


                }

            }

        }

        public void StoneEffect()
        {

            if (external)
            {

                return;

            }

            Random randomIndex = new();

            int rockCut = randomIndex.Next(2);

            int generateAmt = Math.Max(1, randomIndex.Next(Mod.instance.PowerLevel));

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.wealdFive))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.wealdFive, generateAmt);

            }

            Vector2 targetVector = destination / 64;

            for (int i = 0; i < generateAmt; i++)
            {
                
                if (i == 0)
                {

                    int debris = SpawnData.RockFall(location, Game1.player, 20 - Mod.instance.PowerLevel * 2)[2];

                    if (Game1.player.professions.Contains(21) && rockCut == 0)
                    {

                        Game1.createObjectDebris("382", (int)targetVector.X, (int)targetVector.Y);

                    }
                    else if (Game1.player.professions.Contains(19) && rockCut == 0)
                    {

                        Game1.createObjectDebris(debris.ToString(), (int)targetVector.X, (int)targetVector.Y);

                    }

                    Game1.createObjectDebris(debris.ToString(), (int)targetVector.X, (int)targetVector.Y);

                }
                else
                {

                    Game1.createObjectDebris("390", (int)targetVector.X, (int)targetVector.Y);

                }

            }

            Game1.player.gainExperience(3,1);

        }

        public void SapEffect()
        {
            
            if (external)
            {

                return;

            }

            int leech = 0;

            float impes = 0.05f;

            if (Mod.instance.herbalData.applied.ContainsKey(HerbalData.herbalbuffs.vigor))
            {

                impes = 0.05f * Mod.instance.herbalData.applied[HerbalData.herbalbuffs.vigor].level;

            }

            int drain = Math.Max(4,(int)(Mod.instance.CombatDamage() * impes));

            if (monsters.Count == 0)
            {

                monsters = ModUtility.MonsterProximity(location, new() { impact }, radius + 32, true);

            }

            foreach (var monster in monsters)
            {

                if (!ModUtility.MonsterVitals(monster, monster.currentLocation))
                {
                    continue;
                }

                Rectangle boundingBox = monster.GetBoundingBox();

                Color color = Color.DarkGreen;

                Mod.instance.iconData.ImpactIndicator(location, monster.Position, IconData.impacts.sparkle, 3f, new() { color = Color.Teal, });

                location.debris.Add(new Debris(drain, new Vector2(boundingBox.Center.X + 16, boundingBox.Center.Y), color, 1f, monster));

                leech += drain;

            }

            int num = Math.Min(leech, Game1.player.MaxStamina - (int)Game1.player.stamina);

            if (num > 0)
            {

                Game1.player.stamina += num;

                Rectangle healthBox = Game1.player.GetBoundingBox();

                location.debris.Add(
                    new Debris(
                        num,
                        new Vector2(healthBox.Center.X + 16, healthBox.Center.Y),
                        Color.Teal,
                        0.75f,
                        Game1.player
                    )
                );

            }

        }

        public void DrainEffect()
        {
            
            if (external)
            {

                return;

            }

            float impes = 0.1f;

            if (Mod.instance.herbalData.applied.ContainsKey(HerbalData.herbalbuffs.vigor))
            {

                impes = 0.1f * Mod.instance.herbalData.applied[HerbalData.herbalbuffs.vigor].level;

            }

            int drain = Math.Max(Mod.instance.PowerLevel*5, (int)(damageMonsters * impes));

            int num = Math.Min(drain, Game1.player.maxHealth - Game1.player.health);

            if (num > 0)
            {

                Game1.player.health += num;

                Rectangle healthBox = Game1.player.GetBoundingBox();

                Game1.player.currentLocation.debris.Add(
                    new Debris(
                        num,
                        new Vector2(healthBox.Center.X + 16, healthBox.Center.Y),
                        Color.Green,
                        1f,
                        Game1.player
                    )
                );

            }

        }

        public void CurseEffect(effects effect = effects.knock)
        {
            
            if (external)
            {

                return;

            }

            Curse curseEffect;

            if (!Mod.instance.eventRegister.ContainsKey("curse"))
            {

                curseEffect = new();

                curseEffect.eventId = "curse";

                curseEffect.EventActivate();

            }
            else
            {

                curseEffect = Mod.instance.eventRegister["curse"] as Curse;

            }

            if (monsters.Count == 0)
            {

                List<StardewValley.Monsters.Monster> monsterTargets = ModUtility.MonsterProximity(location, new() { impact }, radius + 32, true);

                foreach (var monster in monsterTargets)
                {

                    curseEffect.AddTarget(location, monster, effect);

                }

                return;

            }

            foreach (var monster in monsters)
            {

                curseEffect.AddTarget(location, monster, effect);

            }

        }

        public void EmberEffect(Vector2 zone)
        {
            
            if (external)
            {

                return;

            }

            Ember ember;

            if (!Mod.instance.eventRegister.ContainsKey("emberEffect"))
            {

                ember = new();

                ember.eventId = "emberEffect";

                ember.EventActivate();

            }
            else
            {

                ember = Mod.instance.eventRegister["emberEffect"] as Ember;

            }

            int vsFarmers = 0;

            if (damageFarmers > 0)
            {

                vsFarmers = (int)(damageFarmers / 5);

            }

            int vsMonsters = 0;

            if (damageMonsters > 0)
            {

                vsMonsters = (int)(damageMonsters / 5);

            }

            ember.RadialTarget(
                location,
                ModUtility.PositionToTile(zone),
                vsFarmers,
                vsMonsters,
                scheme,
                (int)(radius/64)
            );

        }

        public void CaptureEffect()
        {
            
            if (!Context.IsMainPlayer)
            {

                return;

            }

            foreach (StardewDruid.Character.Character character in ModUtility.GetFriendsInLocation(location))
            {

                if (!character.ChangeBehaviour(true))
                {

                    continue;

                }

                if(Vector2.Distance(character.Position,impact) <= radius + 32)
                {

                    character.ResetActives();

                    character.LookAtTarget(origin);

                    character.netIdle.Set((int)Character.Character.idles.daze);

                    character.idleTimer = radius * 2;

                }

            }

        }

        /*public void ShockEffect()
        {
            
            if (external)
            {

                return;

            }

            Vector2 shockOrigin = impact;

            int shockSize = 6;

            int shockDirection = -1;

            int shockCounter = 1;

            List<StardewValley.Monsters.Monster> victims = ModUtility.MonsterProximity(location, new() { shockOrigin }, 640, true);

            foreach(StardewValley.Monsters.Monster victim in victims)
            {

                if(shockSize == 0)
                {

                    break;

                }

                if(monsters.Count > 0)
                {

                    if(victim == monsters.First())
                    {

                        continue;

                    }

                }

                Vector2 victimPosition = victim.Position + new Vector2(32, 0);

                float shockDistance = Vector2.Distance(shockOrigin, victimPosition);

                if(shockDistance > (56f * shockSize))
                { 
                
                    continue;
                
                }

                int thisDirection = ModUtility.DirectionToTarget(victimPosition, shockOrigin)[2];

                int upDirection = (thisDirection + 1) % 8;

                int downDirection = (thisDirection + 7) % 8;

                if (shockDirection == -1)
                {

                    shockDirection = thisDirection;

                }

                List<int> checkDirections = new() { thisDirection, upDirection, downDirection };

                if (!checkDirections.Contains(shockDirection))
                {

                    continue;

                }

                float shockDamage = damageMonsters * (shockSize / 6);

                SpellHandle bolt = new(location, victimPosition, shockOrigin, 56, -1, shockDamage);

                bolt.factor = shockSize;

                bolt.display = IconData.impacts.sparkbang;

                bolt.counter = -10 * shockCounter;

                bolt.monsters = new() { victim, };

                bolt.scheme = scheme;

                //bolt.type = SpellHandle.spells.zap;

                bolt.critical = critical;

                bolt.criticalModifier = criticalModifier;

                Mod.instance.spellRegister.Add(bolt);

                shockOrigin = victimPosition;

                shockSize -= 1;

                shockDirection = thisDirection;

                shockCounter++;

            }

        }*/

        // ========================================= gravity well

        public void BlackHoleEffect()
        {

            SpellHandle hole = new(Game1.player, impact, 5 * 64, 0);

            hole.type = SpellHandle.spells.blackhole;

            hole.added = new() { SpellHandle.effects.gravity, };

            Mod.instance.spellRegister.Add(hole);

        }

        public void GravityEffect()
        {
            
            if (external)
            {

                return;

            }

            Gravity gravity;

            if (!Mod.instance.eventRegister.ContainsKey("gravityEffect"))
            {
                gravity = new();

                gravity.eventId = "gravityEffect";

                gravity.EventActivate();

            }
            else
            {

                gravity = Mod.instance.eventRegister["gravityEffect"] as Gravity;
            }

            gravity.AddTarget(location, ModUtility.PositionToTile(impact), 4, radius * 2);

        }

        public void HarvestEffect()
        {
            
            if (external)
            {

                return;

            }

            Harvest harvest;

            if (!Mod.instance.eventRegister.ContainsKey("harvestEffect"))
            {

                harvest = new();

                harvest.eventId = "harvestEffect";

                harvest.EventActivate();

            }
            else
            {

                harvest = Mod.instance.eventRegister["harvestEffect"] as Harvest;
            }

            harvest.AddTarget(location, ModUtility.PositionToTile(impact));

        }

        public void TornadoEffect()
        {
            
            if (external)
            {

                return;

            }

            Whirlpool tornado;

            if (!Mod.instance.eventRegister.ContainsKey("tornadoEffect"))
            {

                tornado = new();

                tornado.eventId = "tornadoEffect";

                tornado.EventActivate();

            }
            else
            {

                tornado = Mod.instance.eventRegister["tornadoEffect"] as Whirlpool;
            }

            tornado.AddTarget(location, ModUtility.PositionToTile(impact));

        }

        // ========================================= other effect

        public void CrateEffect()
        {
            
            if (external)
            {

                return;

            }

            StardewValley.Object treasure = SpawnData.RandomTreasure();

            new ThrowHandle(Game1.player, origin, treasure).register();

        }

        public void TeleportEffect()
        {
            if (external)
            {

                return;

            }

            Game1.player.Position = destination;
        
        }

        public void WarpEffect()
        {

            int direction = (int)factor;

            if (direction < 0 || direction > 3)
            {

                direction = 0;

            }

            Game1.warpFarmer(location.Name, (int)destination.X, (int)destination.Y, direction);

            Game1.xLocationAfterWarp = (int)destination.X;

            Game1.yLocationAfterWarp = (int)destination.Y;

        }
        
        public void StompEffect()
        {

            if (external)
            {

                return;

            }

            Cast.Ether.Stomping stompEvent = new();

            stompEvent.EventSetup(origin, "stomp" + origin.ToString());

            stompEvent.EventActivate();

        }

        // ========================================= bomb effect

        public void CharmEffect()
        {

            List<NPC> villagers = ModUtility.GetFriendsInLocation(location, true);

            foreach (NPC witness in villagers)
            {

                if (Mod.instance.Witnessed(ReactionData.reactions.lovebomb, witness))
                {

                    continue;

                }

                if (Vector2.Distance(witness.Position, destination) >= radius)
                {

                    continue;

                }

                witness.faceTowardFarmerForPeriod(3000, 4, false, Game1.player);

                Game1.player.friendshipData[witness.Name].TalkedToToday = true;

                ModUtility.ChangeFriendship(Game1.player, witness, 100);

                ReactionData.ReactTo(witness, ReactionData.reactions.lovebomb, 100);

            }

        }

        public void BoreEffect()
        {

            if (
                location is MineShaft mineShaft
                && mineShaft.mineLevel != MineShaft.bottomOfMineLevel
                && mineShaft.mineLevel != MineShaft.quarryMineShaft)
            {

            }
            else
            {

                return;

            }

            Layer layer = location.map.GetLayer("Buildings");

            Vector2 boreVectorZero = ModUtility.PositionToTile(destination);

            List<Vector2> bores = new();

            for (int i = 0; i < 4; i++)
            { 
            
                if(i == 0) { 
                    
                    bores = new() { boreVectorZero, };

                }
                else
                {

                    bores = ModUtility.GetTilesWithinRadius(location, boreVectorZero, i);

                }

                foreach(Vector2 boreVector in bores)
                {

                    if(ModUtility.GroundCheck(location,boreVector) == "ground")
                    {
                        
                        layer.Tiles[(int)boreVector.X, (int)boreVector.Y] = new StaticTile(layer, location.map.TileSheets[0], 0, mineShaft.mineLevel > 120 ? 174 : 173);

                        Game1.player.TemporaryPassableTiles.Add(new Microsoft.Xna.Framework.Rectangle((int)boreVector.X * 64, (int)boreVector.Y * 64, 64, 64));

                        return;

                    }

                }

            }

        }

        public void JumpEffect()
        {

            if(factor <= 1)
            {

                return;

            }

            Vector2 bounce = destination + ((destination - origin) * 0.5f);

            SpellHandle clone = new(location, bounce, destination, radius - 64, damageFarmers, damageMonsters);

            clone.type = type;

            clone.missile = missile;

            clone.display = display;

            clone.factor = factor - 1;

            clone.power = Math.Max(1, power -1);

            clone.explosion = Math.Max(1, explosion -1);

            clone.terrain = Math.Max(1, terrain -1);

            clone.sound = sound;

            clone.added = added;

            Mod.instance.spellRegister.Add(clone);

        }

        public void FreezeEffect()
        {
            if (external)
            {

                return;

            }

            if (monsters.Count == 0)
            {

                monsters = ModUtility.MonsterProximity(location, new() { impact }, radius + 32, true);
            }

            Vector2 velocityTowardPoint = Utility.getVelocityTowardPoint(origin,destination, 5f);

            DebuffingProjectile debuffingProjectile = new DebuffingProjectile("frozen", 17, 0, 0, 0f, velocityTowardPoint.X, velocityTowardPoint.Y, impact, location, Game1.player, hitsMonsters: true, playDefaultSoundOnFire: false);
            debuffingProjectile.wavyMotion.Value = false;
            debuffingProjectile.piercesLeft.Value = 99999;
            debuffingProjectile.maxTravelDistance.Value = 3000;
            debuffingProjectile.IgnoreLocationCollision = true;
            debuffingProjectile.ignoreObjectCollisions.Value = true;
            debuffingProjectile.maxVelocity.Value = 12f;
            debuffingProjectile.projectileID.Value = 15;
            debuffingProjectile.alpha.Value = 0.001f;
            debuffingProjectile.alphaChange.Value = 0.05f;
            debuffingProjectile.light.Value = true;
            debuffingProjectile.debuffIntensity.Value = 4;
            debuffingProjectile.boundingBoxWidth.Value = 32;

            foreach (var monster in monsters)
            {

                if (!ModUtility.MonsterVitals(monster, monster.currentLocation))
                {

                    continue;

                }

                debuffingProjectile.behaviorOnCollisionWithMonster(monster, monster.currentLocation);

            }

        }

    }

}
