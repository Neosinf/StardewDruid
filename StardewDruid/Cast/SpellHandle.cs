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
using StardewValley.Locations;
using xTile.Layers;
using xTile.Tiles;
using StardewValley.Projectiles;
using StardewDruid.Render;
using StardewValley.GameData.WorldMaps;
using System.ComponentModel;
using StardewDruid.Handle;
using StardewDruid.Cast.Mists;



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

        }

        public Spells type;

        public IconData.cursors indicator = IconData.cursors.none;

        public IconData.schemes scheme = IconData.schemes.none;

        public MissileHandle.missiles missile = MissileHandle.missiles.none;

        public enum Effects
        {
            none,
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
            freeze,
            douse,
            snare,
            chain,
            binds,
            bigcrits,
            holy,
            capture,
            detonate,
            omen,
        }

        public List<Effects> added = new();

        public IconData.impacts display = IconData.impacts.none;

        public int displayRadius;

        public bool displayFlip;

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

            radius = 128;

            displayRadius = 2;

            factor = 2;

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

            radius = Radius;

            factor = 2;

            damageFarmers = -1f;

            damageMonsters = damage;

            impact = destination;

            type = Spells.explode;

            displayRadius = (int)((radius - 32) / 64);

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

            type = Spells.explode;

            displayRadius = (int)((radius - 32 ) / 64);

        }

        public SpellHandle(Vector2 Destination, int Radius, IconData.impacts Display, List<Effects> Added)
        {

            location = Game1.player.currentLocation;

            origin = Destination;

            destination = Destination;

            radius = Radius;

            factor = 2;

            damageFarmers = -1f;

            damageMonsters = -1f;

            impact = Destination;

            type = Spells.effect;

            display = Display;

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
                displayRadius,
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

            type = (Spells)spellData[5];

            scheme = (IconData.schemes)spellData[6];

            indicator = (IconData.cursors)spellData[7];

            display = (IconData.impacts)spellData[8];

            sound = Sounds.none;

            factor = spellData[9];

            missile = (MissileHandle.missiles)spellData[10];

            instant = (spellData[11] == 1);

            displayRadius = spellData[12];

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

                        ApplyEffects(impact);

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

                                bolt.Setup(location, origin, destination, MissileHandle.missiles.bolt, scheme, factor);
                                
                                break;
                             
                            case Spells.greatbolt:

                                if (sound == Sounds.none)
                                {

                                    soundTrigger = SoundHandle.SoundCue.CastBeam;

                                }

                                bolt.Setup(location, origin, destination, MissileHandle.missiles.greatbolt, scheme, factor);
                                
                                break;

                            case Spells.judgement:

                                if (sound == Sounds.none)
                                {

                                    soundTrigger = SoundHandle.SoundCue.CastBeam;

                                }

                                bolt.Setup(location, origin, destination, MissileHandle.missiles.judgement, scheme, factor);

                                break;

                        }

                        TriggerDisplay();

                        missileHandle = bolt;

                        LightRadius(bolt.origin);

                    }

                    if(counter == 15)
                    {

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

                case Spells.quickbolt:

                    if (counter == 1)
                    {

                        BoltHandle bolt = new();

                        bolt.Setup(location, origin, destination, MissileHandle.missiles.quickbolt, scheme, factor);

                        missileHandle = bolt;

                        if (sound == Sounds.none)
                        {

                            soundTrigger = SoundHandle.SoundCue.CastMists;

                        }

                        TriggerDisplay();

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, monsters);

                        ApplyEffects(impact);

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

                        missileHandle.Setup(location, origin, destination, missile, scheme, factor);

                        TargetCursor();

                        TriggerDisplay();

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


                case Spells.beam:
                case Spells.lightning:
                case Spells.lightbeam:
                case Spells.artemis:

                    if (counter == 1)
                    {

                        LaunchBeam();

                        LightRadius(origin);

                    }

                    if (counter == 30)
                    {

                        GrazeDamage(1, 4, 3, true);

                    }

                    if (counter == 36)
                    {

                        GrazeDamage(2, 4, 3, true);

                    }

                    if (counter == 42)
                    {

                        GrazeDamage(3, 4, 3, true);

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


                case Spells.echo:

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

                case Spells.blackhole:

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

                case Spells.deathwind:

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

                case Spells.crate:

                    if (counter == 1)
                    {

                        CrateCreate();

                    }

                    if (counter == 61)
                    {

                        RemoveAnimations();

                        CrateOpen();

                        location.playSound(Sounds.doorCreak.ToString());

                    }

                    if (counter == 91)
                    {

                        RemoveAnimations();

                        CrateRelease();

                        ApplyEffects(destination);

                        location.playSound(Sounds.yoba.ToString());

                    }

                    if(counter >= 300)
                    {
                        Shutdown();

                        return false;

                    }

                    return true;

                case Spells.teleport:

                    if(counter == 1)
                    {

                        TeleportStart();

                        if (!instant && factor > 1)
                        {

                            InvisibilityHide();

                        }

                    }

                    /*if(factor > 1)
                    {

                        TeleportCloser();

                    }*/

                    if (counter >= factor)
                    {

                        TeleportEffect();

                        TeleportEnd();

                        ApplyEffects(destination);

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

                            switch (factor)
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

                        animations = warpstrike.AnimateWarpStrike(location, warphero, impact, factor);

                    }

                    if (counter == 18)
                    {

                        RadialDisplay();

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, monsters);

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

                            factor = (int)IconData.warps.circle,

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

                            factor = (int)IconData.warps.circle,

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

                        Mod.instance.iconData.WarpIndicator(location, origin, type == Spells.warpout,(IconData.warps)factor);

                    }

                    return false;

                case Spells.grasp:

                    if(counter == 1)
                    {

                        LaunchGrasp();

                    }

                    if (counter == 60)
                    {

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, new());

                        RadialDisplay();

                        ApplyEffects(impact);

                    }

                    if (counter == 120)
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
                origin + (point * factor * changeDistance),
                origin + (point * factor * changeDistance * 2),
                origin + (point * factor * changeDistance * 3),
                origin + (point * factor * changeDistance * 4),
                origin + (point * factor * changeDistance * 5),
            };

            impact = origin + (point * factor * changeDistance * impactDistance);

            /*if (move)
            {*/

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


            /*}
            else
            {

                for (int i = 0; i < points.Count; i++)
                {

                    float echoScale = startScale + (changeScale * i);

                    TemporaryAnimatedSprite part = new(0, interval, 5, loops, points[i] - new Vector2(32 * echoScale, 32f * echoScale), false, false)
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
                        motion = new Vector2(-0.032f, -0.032f),
                        delayBeforeAnimationStart = 250 * i,

                    };

                    location.temporarySprites.Add(part);

                    animations.Add(part);

                }

            }*/

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

            CursorAdditional addEffects = new() { interval = 10000, scale = factor, scheme = scheme, alpha = 0.4f, };

            cursor = Mod.instance.iconData.CursorIndicator(location, impact, indicator, addEffects);
        
        }

        public void ClearCursor()
        {

            location.temporarySprites.Remove(cursor);

            cursor = null;

        }

        public void TriggerDisplay()
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

            location.playSound(SpellHandle.Sounds.openChest.ToString());

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

            if(factor <= 0)
            {

                return;

            }

            Vector2 move = (destination - origin) * (counter / factor);

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

            Vector2 diff = Vector2.Zero;

            if (piece > 0)
            {
            
                diff = (impact - origin) / division * piece;
            
            };

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

                foreach (Effects effect in added)
                {

                    switch (effect)
                    {

                        case Effects.push:

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

                ModUtility.DamageMonsters(individuals, (int)hitmonsters, critical, criticalModifier, push);

                foreach(StardewValley.Monsters.Monster individual in individuals)
                {

                    if (!ModUtility.MonsterVitals(individual, location))
                    {

                        defeated.Add(individual);

                        HerbalHandle.RandomTrophy(individual.Position + new Vector2(32),20);

                    }

                }

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

                ModUtility.Explode(location, ModUtility.PositionToTile(impact), explosion, power);

            }

            if (terrain > 0)
            {

                ModUtility.Reave(location, ModUtility.PositionToTile(impact), terrain, true);

            }

        }

        // ========================================= LOCAL ONLY EFFECTS

        public void ApplyEffects(Vector2 zone)
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

                        EmberEffect(zone);

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

                    case Effects.crate:

                        CrateEffect();

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

                    case Effects.freeze:

                        FreezeEffect();

                        break;

                    case Effects.douse:

                        DouseEffect();

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

        public void SapEffect()
        {
            
            if (external)
            {

                return;

            }

            int leech = 0;

            float impes = 0.05f;

            if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.vigor))
            {

                impes = 0.05f * Mod.instance.herbalData.buff.applied[HerbalBuff.herbalbuffs.vigor].level;

            }

            int drain = Math.Min(12,(int)(Mod.instance.CombatDamage() * impes));

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

        }

        public void DrainEffect()
        {
            
            if (external)
            {

                return;

            }

            float impes = 0.15f;

            if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.vigor))
            {

                impes += (Mod.instance.herbalData.buff.applied[HerbalBuff.herbalbuffs.vigor].level * 0.05f);

            }

            int drain = Math.Max(Mod.instance.PowerLevel*2, (int)(damageMonsters * impes));

            int num = Math.Min(drain, Game1.player.maxHealth - Game1.player.health);

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

            int leech = Math.Max(Mod.instance.PowerLevel * 4, (int)(damageMonsters * impes * 1.5f));

            int health = Math.Min(leech, Game1.player.MaxStamina - (int)Game1.player.stamina);

            if (health > 0)
            {

                Game1.player.stamina += health;

                Rectangle healthBox = Game1.player.GetBoundingBox();

                location.debris.Add(
                    new Debris(
                        health,
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

            if (monsters.Count == 0)
            {

                List<StardewValley.Monsters.Monster> monsterTargets = ModUtility.MonsterProximity(location, new() { impact }, radius + 32, true);

                foreach (StardewValley.Monsters.Monster monster in monsterTargets)
                {

                    curseEffect.AddTarget(location, monster, effect);

                }

                return;

            }

            foreach (StardewValley.Monsters.Monster monster in monsters)
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
                ModUtility.PositionToTile(zone),
                vsFarmers,
                vsMonsters,
                scheme,
                (int)(radius/64)
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

            int threshold = radius + 32;

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

            List<StardewValley.Monsters.Monster> monsterTargets = ModUtility.MonsterProximity(location, new() { impact }, radius + 128, true);

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

        public void DouseEffect()
        {

            if (external)
            {

                return;

            }

            ModUtility.WaterRadius(location, ModUtility.PositionToTile(destination), explosion, true);

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

                if (Vector2.Distance(witness.Position, destination) >= radius)
                {

                    continue;

                }

                witness.faceTowardFarmerForPeriod(3000, 4, false, Game1.player);

                Game1.player.friendshipData[witness.Name].TalkedToToday = true;

                int friendship = factor * 15;

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

            if(factor <= 1)
            {

                return;

            }

            Vector2 bounce = destination + ((destination - origin) * 0.5f);

            SpellHandle clone = new(location, bounce, destination, radius - 64, damageFarmers, damageMonsters)
            {
                type = type,

                missile = missile,

                display = display,

                factor = factor - 1,

                power = Math.Max(1, power - 1),

                explosion = Math.Max(1, explosion - 1),

                terrain = Math.Max(1, terrain - 1),

                sound = sound,

                added = added
            };

            Mod.instance.spellRegister.Add(clone);

        }

        public void FreezeEffect()
        {

            if (Mod.instance.eventRegister.ContainsKey(Rite.eventWisps))
            {

                if (Mod.instance.eventRegister[Rite.eventWisps] is Wisps wispEvent)
                {

                    if (wispEvent.eventLocked)
                    {

                        wispEvent.AddSingle(destination);

                    }

                    return;

                }

            }

            Wisps wispNew = new();

            wispNew.EventSetup(Game1.player.Position, Rite.eventWisps);

            wispNew.EventActivate();

            wispNew.activeLimit = 120;

            wispNew.eventCounter = 0;

            wispNew.eventLocked = true;

            wispNew.AddSingle(destination);

        }

        public void CaptureEffect()
        {
            
            if (external)
            {

                return;

            }

            int captureChance = 99;

            if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.capture))
            {

                switch (Mod.instance.herbalData.buff.applied[HerbalBuff.herbalbuffs.capture].level)
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
