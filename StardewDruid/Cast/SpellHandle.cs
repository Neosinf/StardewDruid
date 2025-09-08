using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Fates;
using StardewDruid.Cast.Mists;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.GameData.WorldMaps;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Projectiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Claims;
using xTile.Layers;
using xTile.Tiles;
using static StardewDruid.Data.IconData;
using static System.Net.Mime.MediaTypeNames;



namespace StardewDruid.Cast
{

    public class SpellHandle
    {

        // specified

        public GameLocation location;

        public Vector2 destination;

        public Vector2 origin;

        public int counter;

        public IconData.impacts display = IconData.impacts.none;

        public int displayRadius;

        public int displayFactor;

        public bool displayFlip;

        public TargetTypes targetType = TargetTypes.none;

        public int damageRadius;

        public float damageFarmers;

        public float damageMonsters;

        public float critical;

        public float criticalModifier;

        public List<Effects> added = new();

        public int effectRadius;

        public string effectId;

        public IconData.ritecircles circle = ritecircles.none;

        // derived

        public List<TemporaryAnimatedSprite> animations = new();

        public TemporaryAnimatedSprite cursor;

        public MissileHandle missileHandle;

        public Vector2 impact;

        public Monster.Boss boss;

        public List<StardewValley.Monsters.Monster> monsters = new();

        public List<StardewValley.Monsters.Monster> affected = new();

        public List<StardewValley.Monsters.Monster> defeated = new();

        public bool queried;

        public bool external;

        public bool local;

        public bool instant;

        public bool shutdown;

        public bool invisibility;

        public enum Spells
        {

            effect,
            swipe,
            explode,

            missile,
            blackhole,
            deathwind,
            crate,
            teleport,
            warpstrike,
            honourstrike,
            levitate,
            trick,
            warp,
            warpout,
            warpin,
            grasp,

            bolt,
            quickbolt,
            greatbolt,
            judgement,

            beam,
            lightning,
            lightbeam,
            artemis,

            echo,
            wave,

            ritecircle,

        }

        public Spells type;

        public IconData.cursors indicator = IconData.cursors.none;

        public IconData.schemes scheme = IconData.schemes.none;

        public MissileHandle.missiles missile = MissileHandle.missiles.none;

        public enum TargetTypes
        {
            none,
            radial,
            conic,
            target,
        }

        public enum Effects
        {

            none,

            sunder,
            swipe,
            breaker,
            toolbreak,
            reave,
            douse,
            hack,
            toolhack,
            explode,

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
            glare,
            crate,
            teleport,
            stomping,
            charm,
            bore,
            jump,
            blackhole,
            wisp,
            snare,
            chain,
            binds,
            bigcrits,
            holy,
            capture,
            detonate,
            omen,

        }

        public enum Sounds
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
            crit,
            stumpCrack,
            treethud,

        }

        public Sounds sound = Sounds.none;

        public SoundHandle.SoundCue soundTrigger = SoundHandle.SoundCue.None;

        public SoundHandle.SoundCue soundImpact = SoundHandle.SoundCue.None;

        public SpellHandle(Farmer farmer, List<StardewValley.Monsters.Monster> Monsters, float damage)
        {

            location = farmer.currentLocation;

            origin = farmer.Position + new Vector2(32);

            destination = Monsters.First().Position + new Vector2(32);

            damageRadius = 128;

            displayRadius = 2;

            displayFactor = 2;

            effectRadius = displayRadius;

            damageFarmers = -1f;

            damageMonsters = damage;

            impact = destination;

            type = Spells.explode;

            monsters = Monsters;

        }

        public SpellHandle(Farmer farmer, Vector2 Destination, int Radius, float damage)
        {

            location = farmer.currentLocation;

            origin = farmer.Position;

            destination = Destination;

            damageRadius = Radius;

            displayFactor = 2;

            damageFarmers = -1f;

            damageMonsters = damage;

            impact = destination;

            type = Spells.explode;

            displayRadius = (int)((damageRadius - 32) / 64);

            effectRadius = displayRadius;

        }

        public SpellHandle(GameLocation Location, Vector2 Destination, Vector2 Origin, int Radius = 128, float vsFarmers = -1f, float vsMonsters = -1f)
        {

            location = Location;

            origin = Origin;

            destination = Destination;

            damageRadius = Radius;

            displayFactor = 2;

            damageFarmers = vsFarmers;

            damageMonsters = vsMonsters;

            impact = Destination;

            type = Spells.explode;

            displayRadius = (int)((damageRadius - 32 ) / 64);

            effectRadius = displayRadius;

        }

        public SpellHandle(Vector2 Destination, int Radius, IconData.impacts Display, List<Effects> Added)
        {

            location = Game1.player.currentLocation;

            origin = Destination;

            destination = Destination;

            damageRadius = Radius;

            displayFactor = 2;

            damageFarmers = -1f;

            damageMonsters = -1f;

            impact = Destination;

            type = Spells.effect;

            display = Display;

            displayRadius = (int)((damageRadius - 32) / 64);

            effectRadius = displayRadius;

            added = Added;

        }

        public SpellHandle(Farmer farmer, IconData.ritecircles circleId)
        {

            location = farmer.currentLocation;

            origin = farmer.Position + new Vector2(32);

            destination = origin;

            impact = destination;

            type = Spells.ritecircle;

            circle = circleId;

        }

        public void SpellQuery()
        {

            List<int> array = new()
            {
                (int)destination.X,
                (int)destination.Y,
                (int)origin.X,
                (int)origin.Y,
                damageRadius,
                Convert.ToInt32(type),
                Convert.ToInt32(scheme),
                Convert.ToInt32(indicator),
                Convert.ToInt32(display),
                displayFactor,
                Convert.ToInt32(missile),
                instant ? 1 : 0,
                displayRadius,
                Convert.ToInt32(circle),
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

            damageRadius = spellData[4];

            impact = destination;

            damageFarmers = -1;

            damageMonsters = -1;

            type = (Spells)spellData[5];

            scheme = (IconData.schemes)spellData[6];

            indicator = (IconData.cursors)spellData[7];

            display = (IconData.impacts)spellData[8];

            sound = Sounds.none;

            displayFactor = spellData[9];

            missile = (MissileHandle.missiles)spellData[10];

            instant = (spellData[11] == 1);

            displayRadius = spellData[12];

            circle = (IconData.ritecircles)spellData[13];

        }

        public void Register()
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

                if (!external && !local && type != Spells.effect)
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

                case Spells.effect:

                    if (counter == 1)
                    {

                        RadialDisplay();

                        ApplyEffects();

                    }

                    if (counter == 120)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case Spells.swipe:

                    if (counter == 1)
                    {

                        ApplyDamage();

                        RadialDisplay();

                        ApplyEffects();

                    }

                    if (counter == 60)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case Spells.explode:

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

                        ApplyDamage();

                        RadialDisplay();

                        ApplyEnvironment();

                        ApplyEffects();

                        ClearCursor();

                    }

                    if (counter == 120)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case Spells.bolt:
                case Spells.greatbolt:
                case Spells.judgement:

                    if (counter == 1)
                    {

                        BoltHandle bolt = new();

                        switch (type)
                        {

                            case Spells.bolt:

                                if (sound == Sounds.none)
                                {

                                    soundTrigger = SoundHandle.SoundCue.CastBolt;

                                }

                                bolt.Setup(location, origin, destination, MissileHandle.missiles.bolt, scheme, displayFactor);
                                
                                break;
                             
                            case Spells.greatbolt:

                                if (sound == Sounds.none)
                                {

                                    soundTrigger = SoundHandle.SoundCue.CastBeam;

                                }

                                bolt.Setup(location, origin, destination, MissileHandle.missiles.greatbolt, scheme, displayFactor);
                                
                                break;

                            case Spells.judgement:

                                if (sound == Sounds.none)
                                {

                                    soundTrigger = SoundHandle.SoundCue.CastBeam;

                                }

                                bolt.Setup(location, origin, destination, MissileHandle.missiles.judgement, scheme, displayFactor);

                                break;

                        }

                        TriggerSound();

                        missileHandle = bolt;

                        LightRadius(bolt.origin);

                    }

                    if(counter == 15)
                    {

                        RadialDisplay();

                        ApplyDamage();

                        ApplyEnvironment();

                        ApplyEffects();

                    }

                    missileHandle.Update();

                    if (missileHandle.shutdown)
                    {

                        Shutdown();

                    }

                    return true;

                case Spells.quickbolt:

                    if (counter == 1)
                    {

                        BoltHandle bolt = new();

                        bolt.Setup(location, origin, destination, MissileHandle.missiles.quickbolt, scheme, displayFactor);

                        missileHandle = bolt;

                        if (sound == Sounds.none)
                        {

                            soundTrigger = SoundHandle.SoundCue.CastMists;

                        }

                        TriggerSound();

                        ApplyDamage();

                        ApplyEnvironment();

                        ApplyEffects();

                    }

                    if (counter == 5)
                    {

                        RadialDisplay();

                    }

                    missileHandle.Update();

                    if (missileHandle.shutdown)
                    {

                        Shutdown();

                    }

                    break;

                case Spells.missile:

                    if (counter == 1)
                    {

                        if (added.Contains(Effects.aiming))
                        {

                            if(monsters.Count > 0)
                            {

                                destination = monsters.First().Position;

                            }

                        }

                        missileHandle = new();

                        missileHandle.Setup(location, origin, destination, missile, scheme, displayFactor);

                        TargetCursor();

                        TriggerSound();

                    }

                    missileHandle.Update();

                    impact = missileHandle.projectilePosition;

                    if (missileHandle.shutdown)
                    {

                        ApplyDamage();

                        RadialDisplay();

                        ApplyEnvironment();

                        ApplyEffects();

                        ClearCursor();

                        Shutdown();

                        return false;

                    }

                    return true;


                case Spells.beam:
                case Spells.lightning:
                case Spells.lightbeam:
                case Spells.artemis:

                    if (counter == 1)
                    {

                        targetType = TargetTypes.conic;

                        LaunchBeam();

                        LightRadius(origin);

                    }

                    if(counter == 32)
                    {

                        ApplyEnvironment();

                    }

                    if (counter == 48)
                    {

                        ApplyDamage();

                        RadialDisplay();

                        ApplyEffects();

                    }

                    if (counter == 90)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case Spells.echo:

                    if (counter == 30)
                    {

                        targetType = TargetTypes.conic;

                        LaunchEcho();

                        LightRadius(origin);

                    }

                    if(counter == 45)
                    {

                        ApplyEnvironment();

                    }

                    if (counter == 80)
                    {

                        ApplyDamage();

                        RadialDisplay();

                        ApplyEffects();

                    }

                    if (counter == 150)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;


                case Spells.wave:

                    if (counter == 1)
                    {

                        targetType = TargetTypes.conic;

                        if (instant)
                        {

                            counter = 30;

                        }

                    }

                    if (counter == 30)
                    {

                        LaunchWave();

                        TriggerSound();

                        ApplyEnvironment();

                    }

                    if (counter == 50)
                    {

                        ApplyDamage();

                        RadialDisplay();

                        ApplyEffects();

                    }

                    if (counter == 90)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case Spells.blackhole:

                    if (counter == 1)
                    {

                        LaunchBlackhole();

                        ApplyEffects();

                    }

                    if (counter == 240)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case Spells.deathwind:

                    if (counter == 1)
                    {

                        LaunchDeathWind();

                        ApplyEffects();

                    }

                    if (counter == 15)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case Spells.teleport:

                    if(counter == 1)
                    {

                        TeleportStart();

                        if (!instant && displayFactor > 1)
                        {

                            InvisibilityHide();

                        }

                    }

                    if (counter >= displayFactor)
                    {

                        TeleportEffect();

                        TeleportEnd();

                        Shutdown();

                        return false;

                    }

                    return true;

                case Spells.warpstrike:
                case Spells.honourstrike:

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

                        CharacterHandle.characters warphero = CharacterHandle.characters.Thanatoshi;

                        if(scheme == IconData.schemes.death)
                        {

                            warpstrike.LoadWeapon(WeaponRender.weapons.scythetwo);

                        }

                        if (scheme == IconData.schemes.darkgray)
                        {

                            warphero = CharacterHandle.characters.Astarion;

                            warpstrike.LoadWeapon(WeaponRender.weapons.estoc);

                        }

                        if (type == Spells.honourstrike)
                        {

                            switch (displayFactor)
                            {
                                default:
                                    warphero = CharacterHandle.characters.HonourCaptain;
                                    break;

                                case 2:
                                case 3:
                                    warphero = CharacterHandle.characters.HonourGuard;
                                    break;

                                case 4:
                                case 5:
                                    warphero = CharacterHandle.characters.HonourKnight;
                                    break;

                            }

                            warpstrike.LoadWeapon(WeaponRender.weapons.starsword);

                        }

                        animations = warpstrike.AnimateWarpStrike(location, warphero, impact, displayFactor);

                    }

                    if (counter == 18)
                    {

                        RadialDisplay();

                        ApplyDamage();

                    }

                    if(counter == 60 || animations[0].timer >= (animations[0].interval * animations[0].animationLength))
                    {

                        Shutdown();

                    }

                    return true;

                case Spells.trick:

                    TrickDisplay();

                    Shutdown();

                    return false;

                case Spells.warp:
                    
                    if (external)
                    {

                        Shutdown();

                        return false;

                    }

                    if(counter == 1)
                    {

                        SpellHandle warpOut = new(Game1.player.currentLocation, Vector2.Zero, origin)
                        {
                            type = Spells.warpout,

                            scheme = scheme,

                            displayFactor = (int)IconData.warps.circle,

                        };

                        Mod.instance.spellRegister.Add(warpOut);

                        if(sound != Sounds.none)
                        {

                            Game1.player.currentLocation.playSound(sound.ToString());

                        }

                    }

                    if (counter == 15)
                    {

                        WarpEffect();

                        Vector2 warpRender = destination * 64;

                        SpellHandle warpIn = new(location, Vector2.Zero, warpRender)
                        {
                            type = Spells.warpin,

                            scheme = scheme,

                            displayFactor = (int)IconData.warps.circle,

                        };

                        Mod.instance.spellRegister.Add(warpIn);

                    }

                    return true;

                case Spells.warpout:
                case Spells.warpin:

                    if (scheme != IconData.schemes.none)
                    {

                        Mod.instance.iconData.WarpIndicator(location, origin, type == Spells.warpout, IconData.warps.circle, scheme);

                    }
                    else
                    {

                        Mod.instance.iconData.WarpIndicator(location, origin, type == Spells.warpout,(IconData.warps)displayFactor);

                    }

                    return false;

                case Spells.grasp:

                    if(counter == 1)
                    {

                        LaunchGrasp();

                    }

                    if (counter == 60)
                    {

                        ApplyDamage();

                        RadialDisplay();

                        ApplyEffects();

                    }

                    if (counter == 120)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case Spells.crate:

                    return false;

                case Spells.ritecircle:

                    TriggerSound();

                    Mod.instance.iconData.RiteCircle(location, origin, circle, 4f, new() { interval = 2000, loops = 1, fade = 0.0005f });

                    Shutdown();

                    return false;

            }

            

            return true;
        }

        public void Shutdown()
        {

            shutdown = true;

            RemoveAnimations();

            if(cursor != null)
            {

                location.temporarySprites.Remove(cursor);

                cursor = null;

            }

            missileHandle?.Shutdown();

            if(invisibility)
            {

                InvisibilityReveal();

            }

        }

        public void RemoveAnimations()
        {


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

                case Spells.lightbeam:

                    texture = Mod.instance.iconData.judgementTexture;

                    break;

                case Spells.lightning:

                    texture = Mod.instance.iconData.lightningTexture;

                    break;


                case Spells.artemis:

                    texture = Mod.instance.iconData.artemisTexture;

                    break;

                default:
                case Spells.beam:

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

        }

        public void LaunchEcho()
        {

            Vector2 diff = destination - origin;

            Vector2 point = diff / Vector2.Distance(origin, destination);

            float rotate = (float)Math.Atan2(diff.Y, diff.X);

            float source = 0f;

            float interval = 125f;

            float startScale = 1.5f;

            float changeScale = 0.5f;

            int changeDistance = 48;

            float impactDistance = 4.5f;

            float impactAlpha = 0.75f;

            float alphaFade = 0.0005f;

            int loops = 2;

            //bool move = false;

            switch (missile)
            {

                case MissileHandle.missiles.deathecho:

                    source = 64f;

                    break;

                case MissileHandle.missiles.bubbleecho:

                    source = 128f;

                    interval = 175f;

                    loops = 1;

                    break;

                case MissileHandle.missiles.fireecho:

                    source = 192f;

                    //move = true;

                    break;

                case MissileHandle.missiles.curseecho:

                    source = 256f;

                    interval = 175f;

                    loops = 1;

                    //move = true;

                    break;

                /*case MissileHandle.missiles.murderecho:

                    source = 320f;

                    rotate = 0f;

                    startScale = 3f;

                    changeScale = 0.75f;

                    changeDistance = 80;

                    impactDistance = 4f;

                    impactAlpha = 1f;

                    //move = true;

                    break;*/

                case MissileHandle.missiles.windecho:

                    source = 320f;

                    impactAlpha = 0.75f;

                    //move = true;

                    break;
            }

            List<Vector2> points = new()
            {
                origin + (point * displayFactor * changeDistance),
                origin + (point * displayFactor * changeDistance * 2),
                origin + (point * displayFactor * changeDistance * 3),
                origin + (point * displayFactor * changeDistance * 4),
                origin + (point * displayFactor * changeDistance * 5),
            };

            impact = origin + (point * displayFactor * changeDistance * impactDistance);

            int timespan = (int)interval * (int)loops * 5;

            Vector2 movement = (points[4] - points[0]) / timespan;

            float change = (changeScale * 4) / timespan;

            for (int i = 0; i < points.Count; i++)
            {

                TemporaryAnimatedSprite part = new(0, interval, 5, loops, points[0], false, false)
                {
                    sourceRect = new(0, (int)source, 64, 64),
                    sourceRectStartingPos = new Vector2(0.0f, source),
                    texture = Mod.instance.iconData.echoTexture,
                    scale = startScale,
                    timeBasedMotion = true,
                    layerDepth = 999f,
                    rotation = rotate,
                    alpha = impactAlpha,
                    alphaFade = alphaFade,
                    scaleChange = change,
                    motion = new Vector2(-0.032f, -0.032f) + movement,
                    delayBeforeAnimationStart = 250 * i,
                };

                location.temporarySprites.Add(part);

                animations.Add(part);

            }

        }
        
        public void LaunchWave()
        {

            Vector2 diff = destination - origin;

            Vector2 point = diff / Vector2.Distance(origin, destination);

            float rotate = (float)Math.Atan2(diff.Y, diff.X);

            float source = 0;

            bool flip = false;

            switch (Mod.instance.randomIndex.Next(3))
            {
                case 0:
                    source = 64;
                    break;

                case 1:
                    flip = true;
                    break;

            }

            Microsoft.Xna.Framework.Color waveColor = Mod.instance.iconData.SchemeColour(scheme);

            float interval = 67.5f;

            float impactAlpha = 0.5f;

            float alphaFade = 0.0005f;

            int timespan = (int)interval * 8;

            Vector2 movement = diff / timespan * 0.75f;

            Vector2 scaleMovement = new(-0.0032f);

            TemporaryAnimatedSprite part = new(0, interval, 8, 1, origin + (diff / 2) - new Vector2(96), false, false)
            {
                sourceRect = new(0, (int)source, 64, 64),
                sourceRectStartingPos = new Vector2(0.0f, source),
                texture = Mod.instance.iconData.impactsTextureFive,
                scale = 3.5f,
                timeBasedMotion = true,
                layerDepth = 999f,
                rotation = rotate,
                alpha = impactAlpha,
                alphaFade = alphaFade,
                verticalFlipped = flip,
                color = waveColor,
                motion = movement + scaleMovement,
                scaleChange = 0.001f,

            };

            location.temporarySprites.Add(part);

            animations.Add(part);

        }

        public void LaunchBlackhole()
        {

            TemporaryAnimatedSprite staticAnimation = new(0, 9999f, 1, 1, impact - new Vector2(64, 64), false, false)
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
            
            Souls windsNew = new();

            windsNew.EventSetup(Game1.player,origin,Rite.eventDeathwinds);

            windsNew.EventActivate();

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

        public void LaunchGrasp()
        {

            int direction = ModUtility.DirectionToTarget(origin, destination)[2];

            Vector2 realVector = ModUtility.DirectionAsVectorCircular(direction);

            int upper = (direction + 6) % 8;

            Vector2 upperVector = ModUtility.DirectionAsVectorCircular(upper);

            int lower = (direction + 2) % 8;

            Vector2 lowerVector = ModUtility.DirectionAsVectorCircular(lower);

            float rotate = ModUtility.DirectionAsRadian(upper);

            ImpactAdditional additional = new()
            {
                rotation = rotate
            };

            int middle = Mod.instance.randomIndex.Next(3);

            int top = Mod.instance.randomIndex.Next(3);

            int bottom = Mod.instance.randomIndex.Next(3);

            // ----------------------------------

            Vector2 preDestination = destination - (realVector * 192);

            animations.Add(Mod.instance.iconData.CreateGrasp(location, preDestination, middle, 3f, additional));

            Vector2 closerPreDestination = destination - (realVector * 160);

            Vector2 preUpperDestination = closerPreDestination + (upperVector * 48);

            animations.Add(Mod.instance.iconData.CreateGrasp(location, preUpperDestination, top, 2f, additional));

            Vector2 preLowerDestination = closerPreDestination + (lowerVector * 48);

            animations.Add(Mod.instance.iconData.CreateGrasp(location, preLowerDestination, bottom, 2f, additional));

            // ----------------------------------

            additional.delay = 500;

            animations.Add(Mod.instance.iconData.CreateGrasp(location, destination, middle, 4f, additional));

            Vector2 upperDestination = destination + (upperVector * 64);

            animations.Add(Mod.instance.iconData.CreateGrasp(location, upperDestination, top, 3f, additional));

            Vector2 lowerDestination = destination + (lowerVector * 64);

            animations.Add(Mod.instance.iconData.CreateGrasp(location, lowerDestination, bottom, 3f, additional));

            // ----------------------------------

            additional.delay = 1000;

            Vector2 postDestination = destination + (realVector * 192);

            animations.Add(Mod.instance.iconData.CreateGrasp(location, postDestination, middle, 3f, additional));

            Vector2 closerPostDestination = destination + (realVector * 160);

            Vector2 postUpperDestination = closerPostDestination + (upperVector * 48);

            animations.Add(Mod.instance.iconData.CreateGrasp(location, postUpperDestination, top, 2f, additional));

            Vector2 postLowerDestination = closerPostDestination + (lowerVector * 48);

            animations.Add(Mod.instance.iconData.CreateGrasp(location, postLowerDestination, bottom, 2f, additional));

        }

        // ========================================= cosmetic
        public void TargetCursor()
        {

            if (indicator == IconData.cursors.none)
            {
                return;
            }

            CursorAdditional addEffects = new() { interval = 10000, scale = displayFactor, scheme = scheme, alpha = 0.4f, };

            cursor = Mod.instance.iconData.CursorIndicator(location, impact + new Vector2(32), indicator, addEffects);
        
        }

        public void ClearCursor()
        {

            location.temporarySprites.Remove(cursor);

            cursor = null;

        }

        public void TriggerSound()
        {

            if(soundTrigger != SoundHandle.SoundCue.None)
            {

                Mod.instance.sounds.PlayCue(soundTrigger);

            }

        }

        public void RadialDisplay()
        {

            //LightRadius(impact);

            if (display != IconData.impacts.none)
            {

                float size = displayRadius == 0 ? 4f : (float)displayRadius;

                Mod.instance.iconData.ImpactIndicator(location, impact, display, size, new() { scheme = scheme, flip = displayFlip});

            }

            if (sound != Sounds.none && sound != Sounds.silent)
            {

                Game1.currentLocation.playSound(sound.ToString());

            }

            if (soundImpact != SoundHandle.SoundCue.None)
            {

                Mod.instance.sounds.PlayCue(soundImpact);

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

            switch (displayFactor)
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

                switch (displayFactor)
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

        public void TeleportStart()
        {

            if (!added.Contains(Effects.teleport))
            {

                added.Add(Effects.teleport);

            }

            Mod.instance.iconData.AnimateQuickWarp(Game1.player.currentLocation, origin, true);

        }

        public void TeleportCloser()
        {

            float distance = Vector2.Distance(Game1.player.Position, destination);

            if(distance <= 32)
            {

                return;

            }

            if(displayFactor <= 0)
            {

                return;

            }

            Vector2 move = (destination - origin) * (counter / displayFactor);

            Game1.panScreen((int)move.X, (int)move.Y);

            //Game1.player.Position = origin + );

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

            Game1.player.currentTemporaryInvincibilityDuration = displayFactor * 1000;

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

        public void ApplyDamage()
        {

            if (external)
            {

                return;

            }

            if (damageFarmers > 0 && boss != null)
            {

                List<Farmer> farmers;

                if (targetType == TargetTypes.conic)
                {

                    farmers = ModUtility.FarmerConic(location, origin, impact, effectRadius, true);

                }
                else
                {
                    farmers = ModUtility.FarmerProximity(location, impact, damageRadius + 32, true);

                }

                ModUtility.DamageFarmers(farmers, (int)damageFarmers, boss);

            }

            if (damageMonsters <= 0)
            {

                return;

            }

            List<StardewValley.Monsters.Monster> monsterTargets = new(monsters);

            if (monsterTargets.Count == 0)
            {

                if(targetType == TargetTypes.conic)
                {

                    monsterTargets = ModUtility.MonsterConic(location, origin, impact, effectRadius, true);

                }
                else
                {
                    monsterTargets = ModUtility.MonsterProximity(location, impact, damageRadius + 32, true);

                }

            }

            if (monsterTargets.Count == 0)
            {

                return;

            }

            bool push = added.Contains(Effects.push);

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

            ModUtility.DamageMonsters(monsterTargets, (int)damageMonsters, critical, criticalModifier, push);

            affected = monsterTargets;

            foreach(StardewValley.Monsters.Monster individual in monsterTargets)
            {

                if (!ModUtility.MonsterVitals(individual, location))
                {

                    defeated.Add(individual);

                    ApothecaryHandle.RandomTrophy(individual.Position + new Vector2(32),20);

                }

            }

        }

        public List<StardewValley.Monsters.Monster> MonsterVictims()
        {

            return affected;

        }

        // ========================================= LOCAL ONLY EFFECTS

        public void ApplyEnvironment()
        {


            if (external)
            {

                return;

            }

            foreach (Effects effect in added)
            {

                switch (effect)
                {

                    case Effects.breaker:
                    case Effects.toolbreak:
                    case Effects.hack:
                    case Effects.toolhack:
                    case Effects.reave:
                    case Effects.douse:
                    case Effects.sunder:
                    case Effects.swipe:

                        ExplosiveEffect(effect);

                        break;

                    case Effects.explode:

                        ExplosiveEffect(Effects.swipe);

                        ExplosiveEffect(Effects.breaker);

                        ExplosiveEffect(Effects.hack);

                        ExplosiveEffect(Effects.reave);

                        break;

                }

            }

        }

        public void ApplyEffects()
        {

            if (external)
            {

                return;

            }

            foreach (Effects effect in added)
            {

                switch (effect)
                {

                    case Effects.drain:

                        DrainEffect();

                        break;

                    case Effects.stone:

                        StoneEffect();

                        break;

                    case Effects.knock:
                    case Effects.morph:
                    case Effects.mug:
                    case Effects.daze:
                    case Effects.doom:
                    case Effects.immolate:
                    case Effects.glare:
                    case Effects.holy:
                    case Effects.omen:

                        CurseEffect(effect);

                        break;

                    case Effects.embers:

                        EmberEffect();

                        break;

                    case Effects.gravity:

                        GravityEffect();

                        break;

                    case Effects.harvest:

                        HarvestEffect();

                        break;

                    case Effects.tornado:

                        TornadoEffect();

                        break;

                    case Effects.teleport:

                        TeleportEffect();

                        break;

                    case Effects.stomping:

                        StompEffect();

                        break;

                    case Effects.charm:

                        CharmEffect();

                        CurseEffect(Effects.charm);

                        break;

                    case Effects.bore:

                        BoreEffect();

                        break;

                    case Effects.jump:

                        JumpEffect();

                        break;

                    case Effects.blackhole:

                        BlackHoleEffect();

                        break;

                    case Effects.wisp:

                        FreezeEffect();

                        break;

                    case Effects.snare:
                    case Effects.chain:
                    case Effects.binds:

                        if(boss != null)
                        {

                            ChainEffect(effect);

                            break;

                        }

                        SnareEffect(effect);

                        break;

                    case Effects.bigcrits:

                        BigCritsEffect();

                        break;

                    case Effects.capture:

                        CaptureEffect();

                        break;

                    case Effects.detonate:

                        DetonateEffect();

                        break;

                }

            }

        }

        public void ExplosiveEffect(Effects effect = Effects.explode)
        {

            if(effectId == null)
            {

                effectId = Guid.NewGuid().ToString();

            }

            Explosion explosiveEvent;

            if (!Mod.instance.eventRegister.ContainsKey(effectId))
            {

                explosiveEvent = new()
                {
                    eventId = effectId,

                };

                explosiveEvent.EventActivate();

                if(targetType == TargetTypes.conic)
                {

                    explosiveEvent.ConicSetup(location, origin, impact, effectRadius);

                }
                else
                {

                    explosiveEvent.RadialSetup(location, impact, effectRadius);

                }

            }
            else
            {

                explosiveEvent = Mod.instance.eventRegister[effectId] as Explosion;

            }

            if (explosiveEvent.blastEffects.ContainsKey(effect))
            {

                explosiveEvent.blastEffects[effect] = true;


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

                    int debris = SpawnData.RockFall(location, 20 - Mod.instance.PowerLevel * 2)[2];

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

            Mod.instance.GiveExperience(3,1);

        }

        public void DrainEffect()
        {
            
            if (external)
            {

                return;

            }

            float impes = 1;

            if (Mod.instance.apothecaryHandle.buff.applied.ContainsKey(BuffHandle.buffTypes.vigor))
            {

                impes += (Mod.instance.apothecaryHandle.buff.applied[BuffHandle.buffTypes.vigor].level * 0.25f);

            }

            float drain = Math.Max(Mod.instance.PowerLevel, Mod.instance.PowerLevel * MonsterVictims().Count) * impes;

            int num = Math.Min((int)drain, Game1.player.maxHealth - Game1.player.health);

            if (num > 0)
            {

                Game1.player.health += num;

                Rectangle healthBox = Game1.player.GetBoundingBox();

                Game1.player.currentLocation.debris.Add(
                    new Debris(
                        num,
                        new Vector2(healthBox.Center.X + 16, healthBox.Center.Y),
                        Mod.instance.iconData.SchemeColour(IconData.schemes.herbal_celeri),
                        1f,
                        Game1.player
                    )
                );

            }

            float leech = drain * 1.5f;

            int stamina = Math.Min((int)leech, Game1.player.MaxStamina - (int)Game1.player.stamina);

            if (stamina > 0)
            {

                Game1.player.stamina += stamina;

                Rectangle healthBox = Game1.player.GetBoundingBox();

                location.debris.Add(
                    new Debris(
                        stamina,
                        new Vector2(healthBox.Center.X + 16, healthBox.Center.Y),
                        Mod.instance.iconData.SchemeColour(IconData.schemes.herbal_ligna),
                        0.75f,
                        Game1.player
                    )
                );

            }

        }

        public void CurseEffect(Effects effect = Effects.knock)
        {
            
            if (external)
            {

                return;

            }

            Curse curseEffect;

            if (!Mod.instance.eventRegister.ContainsKey(Rite.eventCurse))
            {

                curseEffect = new()
                {
                    eventId = Rite.eventCurse
                };

                curseEffect.EventActivate();

            }
            else
            {

                curseEffect = Mod.instance.eventRegister[Rite.eventCurse] as Curse;

            }

            List< StardewValley.Monsters.Monster> monsterTargets = MonsterVictims();

            foreach (StardewValley.Monsters.Monster monster in monsterTargets)
            {

                curseEffect.AddTarget(location, monster, effect);

            }

        }

        public void EmberEffect()
        {
            
            if (external)
            {

                return;

            }

            Ember ember;

            if (!Mod.instance.eventRegister.ContainsKey("emberEffect"))
            {

                ember = new()
                {
                    eventId = "emberEffect"
                };

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
                ModUtility.PositionToTile(impact),
                vsFarmers,
                vsMonsters,
                scheme,
                (int)(damageRadius/64)
            );

        }

        public void ChainEffect(Effects effect = Effects.chain)
        {

            Snare snareEffect;

            if (!Mod.instance.eventRegister.ContainsKey(Rite.eventSnare))
            {

                snareEffect = new()
                {
                    eventId = Rite.eventSnare
                };

                snareEffect.EventActivate();

            }
            else
            {

                snareEffect = Mod.instance.eventRegister[Rite.eventSnare] as Snare;

            }

            int threshold = damageRadius + 32;

            int difficulty = 40;

            switch (Mod.instance.Config.modDifficulty)
            {

                case "kiwi":

                    difficulty = 100;

                    break;

                case "hard":

                    difficulty = 80;

                    break;

                case "medium":

                    difficulty = 60;

                    break;

            }


            if (Context.IsMainPlayer)
            {

                foreach (KeyValuePair<CharacterHandle.characters,TrackHandle> tracker in Mod.instance.trackers)
                {

                    if (!Mod.instance.characters[tracker.Key].ChangeBehaviour(true))
                    {

                        continue;

                    }

                    if (Vector2.Distance(Mod.instance.characters[tracker.Key].Position, impact) <= threshold)
                    {

                        snareEffect.AddVictim(Mod.instance.characters[tracker.Key], difficulty, effect);

                    }

                }

            }

            if (Vector2.Distance(Game1.player.Position, impact) <= threshold)
            {

                snareEffect.TargetPlayer(difficulty, effect);

            }

        }

        public void SnareEffect(Effects effect = Effects.snare)
        {

            if (external)
            {

                return;

            }

            Snare snareEffect;

            if (!Mod.instance.eventRegister.ContainsKey(Rite.eventSnare))
            {

                snareEffect = new()
                {
                    eventId = Rite.eventSnare
                };

                snareEffect.EventActivate();

            }
            else
            {

                snareEffect = Mod.instance.eventRegister[Rite.eventSnare] as Snare;

            }


            List<StardewValley.Monsters.Monster> monsterTargets = MonsterVictims();

            foreach (var monster in monsterTargets)
            {

                snareEffect.AddMonster(monster, 40, effect);

            }

        }

        // ========================================= gravity well

        public void BlackHoleEffect()
        {

            SpellHandle hole = new(Game1.player, impact, 5 * 64, 0)
            {
                type = SpellHandle.Spells.blackhole,

                added = new() { SpellHandle.Effects.gravity, }
            };

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
                gravity = new()
                {
                    eventId = "gravityEffect"
                };

                gravity.EventActivate();

            }
            else
            {

                gravity = Mod.instance.eventRegister["gravityEffect"] as Gravity;
            }

            gravity.AddTarget(location, ModUtility.PositionToTile(impact), 4, damageRadius * 2);

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

                harvest = new()
                {
                    eventId = "harvestEffect"
                };

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

                tornado = new()
                {
                    eventId = "tornadoEffect"
                };

                tornado.EventActivate();

            }
            else
            {

                tornado = Mod.instance.eventRegister["tornadoEffect"] as Whirlpool;
            }

            tornado.AddTarget(location, ModUtility.PositionToTile(impact));

        }

        // ========================================= other effect

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

            int direction = (int)displayFactor;

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

            stompEvent.EventSetup(Game1.player,origin, "stomp" + origin.ToString());

            stompEvent.EventActivate();

        }

        public void BigCritsEffect()
        {

            if(monsters.Count > 0)
            {

                List<Microsoft.Xna.Framework.Color> colours = Mod.instance.iconData.gradientColours[IconData.schemes.stars];

                location.debris.Add(new Debris(9999, origin + new Vector2(Mod.instance.randomIndex.Next(32), Mod.instance.randomIndex.Next(32)), colours[Mod.instance.randomIndex.Next(colours.Count)], 4f, null));

            }

        }

        public void DetonateEffect()
        {

            if (external)
            {

                return;

            }

            if(boss == null)
            {

                return;

            }

            boss.Health = 1;

            ModUtility.HitMonster(boss, 9999, true);

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

                if (Vector2.Distance(witness.Position, destination) >= damageRadius)
                {

                    continue;

                }

                witness.faceTowardFarmerForPeriod(3000, 4, false, Game1.player);

                Game1.player.friendshipData[witness.Name].TalkedToToday = true;

                int friendship = displayFactor * 15;

                ModUtility.ChangeFriendship(witness, friendship);

                ReactionData.ReactTo(witness, ReactionData.reactions.lovebomb, friendship, new());

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

            List<Vector2> bores;

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

            if(displayFactor <= 1)
            {

                return;

            }

            Vector2 bounce = destination + ((destination - origin) * 0.5f);

            SpellHandle clone = new(location, bounce, destination, damageRadius - 64, damageFarmers, damageMonsters)
            {

                type = type,

                targetType = targetType,

                missile = missile,

                display = display,

                displayFactor = displayFactor - 1,

                sound = sound,

                added = added,

                effectRadius = effectRadius - 1,

            };

            Mod.instance.spellRegister.Add(clone);

        }

        public void FreezeEffect()
        {

            if (Mod.instance.eventRegister.ContainsKey(Rite.eventWisps))
            {

                if (Mod.instance.eventRegister[Rite.eventWisps] is Wisps wispEvent)
                {

                    wispEvent.AddSingle(destination);

                    return;

                }

            }

            Wisps wispNew = new();

            wispNew.EventSetup(Game1.player,Game1.player.Position, Rite.eventWisps);

            wispNew.EventActivate();

            wispNew.AddSingle(destination);

        }

        public void CaptureEffect()
        {
            
            if (external)
            {

                return;

            }

            int captureChance = 99;

            if (Mod.instance.apothecaryHandle.buff.applied.ContainsKey(BuffHandle.buffTypes.capture))
            {

                switch (Mod.instance.apothecaryHandle.buff.applied[BuffHandle.buffTypes.capture].level)
                {

                    case 1:
                        captureChance = 40; break;
                    case 2:
                        captureChance = 60; break;
                    case 3:
                        captureChance = 85; break;

                }

            }

            foreach (var monster in defeated)
            {

                if (!ModUtility.MonsterVitals(monster, monster.currentLocation))
                {

                    if(Mod.instance.randomIndex.Next(100) >= captureChance)
                    {

                        return;

                    }

                    switch (monster)
                    {

                        case StardewValley.Monsters.Bat:
                        case StardewDruid.Monster.ShadowBat:

                            PalHandle.Capture(CharacterHandle.characters.PalBat, monster.Position);

                            break;

                        case StardewValley.Monsters.GreenSlime:
                        case StardewValley.Monsters.BigSlime:
                        case StardewDruid.Monster.Jellyfiend:

                            PalHandle.Capture(CharacterHandle.characters.PalSlime, monster.Position);

                            break;
                        case StardewValley.Monsters.DustSpirit:
                        case StardewDruid.Monster.Dustfiend:

                            PalHandle.Capture(CharacterHandle.characters.PalSpirit, monster.Position);

                            break;

                        case StardewValley.Monsters.Ghost:
                        case StardewDruid.Monster.Spectre:

                            PalHandle.Capture(CharacterHandle.characters.PalGhost, monster.Position);

                            break;

                        case StardewValley.Monsters.Serpent:
                        case StardewDruid.Monster.ShadowSerpent:

                            PalHandle.Capture(CharacterHandle.characters.PalSerpent, monster.Position);

                            break;


                    }

                    continue;

                }

            }

        }

    }

}
