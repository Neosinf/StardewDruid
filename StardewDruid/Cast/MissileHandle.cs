using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using System.Collections.Generic;
using System;
using System.IO;
using System.Diagnostics.Contracts;
using xTile.Dimensions;
using static StardewDruid.Data.IconData;
using static StardewDruid.Cast.SpellHandle;
using System.Reflection.Metadata;
using System.Linq;
using static StardewDruid.Character.Character;


namespace StardewDruid.Cast
{
    public class MissileHandle
    {

        public GameLocation location;

        public Vector2 origin;

        public Vector2 destination;

        public IconData.schemes scheme;

        public missiles missile;

        public int factor;

        public Texture2D missileTexture;

        public Texture2D missileTextureTwo;

        public int projectile;

        public int projectileIncrements;

        public int projectileTrack;

        public float projectileSpeed;

        public int projectileOffset;

        public bool projectileDirect;

        public Vector2 projectilePosition;

        public int projectileTotal;

        public int projectilePeak;

        public float projectileDepth;

        public int counter;

        public bool impacted;

        public bool shutdown;

        public enum missiles
        {
            none,
            fireball,
            cannonball,
            meteor,
            death,
            slimeball,
            rockfall,
            whisk,
            warpball,
            shuriken,
            knife,
            axe,
            hammer,
            bomb,
            cat,
            bolt,
            zap,
            rocket,
            firefall,
            frostball,
            deathfall,
        }

        public enum missiletrajectories
        {
            none,
            orbital,
            ballistic,
            sustained,
        }

        public missiletrajectories projectileTrajectory;

        public enum missileIndexes
        {
            blazeCore1,
            blazeCore2,
            blazeCore3,
            blazeCore4,

            blazeInner1,
            blazeInner2,
            blazeInner3,
            blazeInner4,

            blazeOuter1,
            blazeOuter2,
            blazeOuter3,
            blazeOuter4,

            blazeOutline1,
            blazeOutline2,
            blazeOutline3,
            blazeOutline4,

            scatter1,
            slimetrail1,
            slimetrail2,
            slimetrial3,

            fireball,
            warpball,
            whisk,
            frostball,

            rocket,
            slimeball,
            slimeball2,
            slimeball3,

            meteor1,
            meteor2,
            meteor3,
            rock1,

            rock2,
            rock3,
            death,
            cannonball,

            shuriken,
            knife,
            axe,
            bomb,

            shuriken2,
            knife2,
            axe2,
            bomb2,

        }

        public enum missileIndexesTwo
        {

            rainbow1,
            rainbow2,
            rainbow3,
            rainbow4,

            cat1,
            cat2,
            cat3,
            cat4,

            hammer,
            hammer2

        }


        public List<TemporaryAnimatedSprite> construct = new();

        public TemporaryAnimatedSprite shadow;

        public enum lightEffects
        {
            none,
            shadow,
            blaze,
            incoming,

        }

        public lightEffects lighting = lightEffects.none;


        public MissileHandle()
        {

            missileTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Missiles.png"));

            missileTextureTwo = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "MissilesTwo.png"));
        
        }


        public virtual void Setup(
            GameLocation Location,
            Vector2 Origin,
            Vector2 Destination,
            missiles Missile,
            IconData.schemes Scheme = IconData.schemes.none,
            int Factor = 2)
        {

            location = Location;

            origin = Origin;

            destination = Destination;

            missile = Missile;

            scheme = Scheme;

            factor = Factor;

            counter = 0;

            projectileSpeed = 1;

            projectileDepth = location.IsOutdoors ? destination.Y / 10000 + 0.001f : 999f;

            lighting = lightEffects.shadow;

            switch (Missile)
            {

                case missiles.rocket:
                case missiles.deathfall:

                    projectileTrajectory = missiletrajectories.ballistic;

                    projectileSpeed = 0.5f;

                    projectilePeak = 640;

                    counter = -30;

                    break;

                case missiles.fireball:
                case missiles.death:

                    projectileSpeed = 0.75f;

                    Game1.player.currentLocation.playSound(sounds.fireball.ToString());

                    break;

                case missiles.whisk:

                    projectileSpeed = 1.5f;

                    break;

                case missiles.meteor:

                    projectileTrajectory = missiletrajectories.orbital;

                    projectileIncrements = 4;

                    projectileSpeed = 1.5f;

                    projectileOffset = -60 + (30 * Mod.instance.randomIndex.Next(6));

                    break;

                case missiles.firefall:

                    projectileTrajectory = missiletrajectories.orbital;

                    projectileSpeed = 1.5f;

                    projectileIncrements = 4;

                    break;

                case missiles.rockfall:

                    projectileTrajectory = missiletrajectories.orbital;

                    projectileSpeed = 1f;

                    projectileIncrements = Mod.instance.randomIndex.Next(3, 6);

                    break;
                
                case missiles.bomb:
                case missiles.cat:

                    projectileTrajectory = missiletrajectories.ballistic;

                    factor = 2;
                    
                    projectilePeak = 150;
                    
                    projectileIncrements = 3;

                    projectileSpeed = 1.5f;
                    
                    break;

            }

            switch (projectileTrajectory)
            {


                case missiletrajectories.orbital:

                    float netSpeed = 160 * projectileSpeed;

                    origin = destination - new Vector2(projectileOffset * projectileIncrements, netSpeed * projectileIncrements);

                    Lighting();

                    break;

                default:

                    Increments();

                    Lighting();

                    break;

            }

            projectileTotal = (projectileIncrements * 15);

            projectileTrack = 1;

        }

        public void Shutdown()
        {

            if (construct.Count > 0)
            {

                foreach (TemporaryAnimatedSprite animatedSprite in construct)
                {
                    if (animatedSprite.lightId != null)
                    {

                        Utility.removeLightSource(animatedSprite.lightId);
                    }

                    location.temporarySprites.Remove(animatedSprite);

                }

                construct.Clear();

            }

            if(shadow != null)
            {

                location.temporarySprites.Remove(shadow);

            }

            shutdown = true;

        }

        public virtual void Update()
        {

            counter++;

            if (counter == 1)
            {

                ConstructMissile();

            }

            if (counter < projectileTotal)
            {

                AdjustTrajectory(projectileTotal - counter);

            }

            if (counter % 30 == 0)
            {

                projectileTrack++;

            }

            if (counter == projectileTotal)
            {

                Shutdown();

                return;

            }

        }


        // ========================================= missile effects


        public void Increments()
        {

            float range = Vector2.Distance(origin, destination);

            float netSpeed = 160 * projectileSpeed;

            if (range < netSpeed)
            {

                Vector2 diff = (destination - origin) / Vector2.Distance(origin, destination) * netSpeed;

                destination = origin + diff;

                projectileIncrements = 1;

            }
            else if (range > (netSpeed * 8))
            {

                Vector2 diff = (destination - origin) / Vector2.Distance(origin, destination) * (netSpeed * 8);

                destination = origin + diff;

                projectileIncrements = 8;

            }
            else
            {

                projectileIncrements = (int)(range / netSpeed);

            }

        }

        public void Lighting()
        {

            if (lighting == lightEffects.none)
            {
                return;
            }

            float duration = projectileIncrements * 0.25f;

            CursorAdditional addEffects = new() { interval = duration * 1000, scale = projectile, alpha = 0.35f, rotation = 0f, };

            switch (lighting)
            {
                case lightEffects.shadow:

                    shadow = Mod.instance.iconData.CursorIndicator(location, destination, IconData.cursors.shadow, addEffects);

                    shadow.rotationChange = 0f;
                    break;

                case lightEffects.blaze:

                    break;


            }

        }

        public void AdjustTrajectory(int projectileProgress = 0)
        {

            if (construct.Count == 0)
            {

                return;

            }

            Vector2 from = origin;

            Vector2 appear =  destination;

            if (projectileProgress < projectileTotal)
            {

                Vector2 shift = (destination - from) * (projectileTotal - projectileProgress) / projectileTotal;

                appear = from + shift;

                if (projectilePeak > 0)
                {

                    float distance = Vector2.Distance(from, destination);

                    float length = distance / 2;

                    float lengthSq = (length * length);

                    float heightFr = 4 * projectilePeak;

                    float coefficient = lengthSq / heightFr;

                    int midpoint = (projectileTotal / 2);

                    float newHeight = 0;

                    if (projectileProgress != midpoint)
                    {

                        float newLength;

                        if (projectileProgress < midpoint)
                        {

                            newLength = length * (midpoint - projectileProgress) / midpoint;

                        }
                        else
                        {

                            newLength = (length * (projectileProgress - midpoint) / midpoint);

                        }

                        float newLengthSq = newLength * newLength;

                        float coefficientFr = (4 * coefficient);

                        newHeight = newLengthSq / coefficientFr;
                    }

                    appear -= new Vector2(0, projectilePeak - newHeight);
                }

            }

            float targetDepth = location.IsOutdoors ? appear.Y / 10000 + 0.001f : 999f;

            projectilePosition = appear;

            Vector2 newPosition = projectilePosition + new Vector2(32, 32) - (new Vector2(48, 48) * construct.First().scale);

            Vector2 diff = newPosition - construct.First().position;

            float rotate = (float)Math.Atan2(diff.Y, diff.X);

            for (int i = construct.Count - 1; i >= 0; i--)
            {

                newPosition = appear - (new Vector2(48, 48) * construct[i].scale) + new Vector2(32, 32);

                construct[i].position = newPosition;

                if (construct[i].rotationChange == 0f)
                {

                    construct[i].rotation = rotate;

                }
                else
                {

                    if (origin.X > destination.X)
                    {

                        construct[i].flipped = true;

                        construct[i].verticalFlipped = true;

                    }

                }

                if (projectileTrajectory != missiletrajectories.orbital)
                {

                    if (projectileDepth < 0.001f)
                    {

                        construct[i].layerDepth = targetDepth + (i * 0.0001f);

                    }

                }

            }

            if (shadow != null)
            {

                Vector2 shadowing = new Vector2(projectileOffset * projectileIncrements, 0) * projectileProgress / projectileTotal;

                switch (projectileTrajectory)
                {

                    case missiletrajectories.orbital:
                    case missiletrajectories.ballistic:

                        float offset = (destination.Y - newPosition.Y);

                        float offsetRatio = offset / 256;

                        shadow.scale = Math.Max(1f, 4f - offset);

                        shadow.Position = destination - shadowing + new Vector2(32, 32) - new Vector2(16 * shadow.scale, 16 * shadow.scale);

                        break;

                    default:
                    case missiletrajectories.sustained:

                        shadow.Position = destination - shadowing + new Vector2(32, 32) - new Vector2(16 * shadow.scale, 16 * shadow.scale) + new Vector2(0,96);

                        break;

                }



            }

        }

        // ========================================= missile constructs

        public void ConstructMissile()
        {

            int increments = projectileIncrements;

            int scale = factor;

            float depth = projectileDepth;

            List<TemporaryAnimatedSprite> missileAnimations = new();

            Microsoft.Xna.Framework.Rectangle rect = new(0, ((int)missile - 1) * 96, 96, 96);

            Vector2 setat = origin - (new Vector2(48, 48) * scale) + new Vector2(32, 32);

            int loops = (int)Math.Ceiling(increments * 0.5);

            int frames = 4;

            int interval = (increments * 250) / (loops * frames);

            Microsoft.Xna.Framework.Color coreColour = Microsoft.Xna.Framework.Color.White;

            switch (missile)
            {

                case missiles.fireball:
                case missiles.firefall:

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, coreColour, 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][0], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][1], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][2], 0.75f));

                    Vector2 fireCoreSet = origin - (new Vector2(48, 48) * (scale * 0.75f)) + new Vector2(32, 32);

                    TemporaryAnimatedSprite fireball = MissileAnimation(location, missileIndexes.fireball, fireCoreSet, scale * 0.75f, interval * frames * loops, 1, 1, depth + 0.0001f, coreColour, 1f);
                    fireball.rotationChange = (float)Math.PI / 60;
                    missileAnimations.Add(fireball);

                    break;

                case missiles.rocket:

                    Vector2 etherCoreSet = origin - (new Vector2(48, 48) * (scale * 0.75f)) + new Vector2(32, 32);

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, etherCoreSet, scale * 0.75f, interval, frames, loops, depth, coreColour, 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, etherCoreSet, scale * 0.75f, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][0], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, etherCoreSet, scale * 0.75f, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][1], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, etherCoreSet, scale * 0.75f, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][2], 0.75f));

                    TemporaryAnimatedSprite rocket = MissileAnimation(location, missileIndexes.rocket, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, coreColour, 1f);

                    missileAnimations.Add(rocket);

                    break;

                case missiles.warpball:

                    Vector2 warpCoreSet = origin - (new Vector2(48, 48) * (scale * 1.25f)) + new Vector2(32, 32);

                    TemporaryAnimatedSprite warpball = MissileAnimation(location, missileIndexes.warpball, warpCoreSet, scale * 1.25f, interval * frames * loops, 1, 1, depth + 0.0001f, coreColour, 1f);

                    warpball.rotationChange = (float)Math.PI / 60;

                    missileAnimations.Add(warpball);

                    break;

                case missiles.frostball:

                    Vector2 frostCoreSet = origin - (new Vector2(48, 48) * (scale * 1.25f)) + new Vector2(32, 32);

                    TemporaryAnimatedSprite frostball = MissileAnimation(location, missileIndexes.frostball, frostCoreSet, scale * 1.25f, interval * frames * loops, 1, 1, depth + 0.0001f, coreColour, 1f);

                    frostball.rotationChange = (float)Math.PI / 60;

                    missileAnimations.Add(frostball);

                    break;
                case missiles.meteor:

                    schemes meteorScheme = schemes.rock;

                    meteorScheme = (schemes)((int)meteorScheme + Mod.instance.randomIndex.Next(3));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, coreColour, 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][0], 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][1], 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][2], 1f));

                    missileIndexes meteorite = missileIndexes.meteor1;

                    meteorite = (missileIndexes)((int)meteorite + Mod.instance.randomIndex.Next(6));

                    Vector2 coreSet = origin - (new Vector2(48, 48) * (scale * 0.75f)) + new Vector2(32, 32);

                    TemporaryAnimatedSprite meteor = MissileAnimation(location, meteorite, coreSet, scale * 0.75f, interval * frames * loops, 1, 1, depth + 0.0001f, Mod.instance.iconData.SchemeColour(meteorScheme), 1f);

                    meteor.rotation = (float)Math.PI * 0.5f * Mod.instance.randomIndex.Next(4);

                    meteor.rotationChange = (float)Math.PI / 60;

                    missileAnimations.Add(meteor);

                    break;

                case missiles.rockfall:

                    schemes rockScheme = schemes.rock;

                    rockScheme = (schemes)((int)rockScheme + Mod.instance.randomIndex.Next(3));

                    missileIndexes rockfalling = missileIndexes.rock1;

                    rockfalling = (missileIndexes)((int)rockfalling + Mod.instance.randomIndex.Next(3));

                    TemporaryAnimatedSprite rockScatter = MissileAnimation(location, missileIndexes.scatter1, setat, scale, interval * frames * loops, 1, 1, depth, Mod.instance.iconData.SchemeColour(rockScheme), 1f);

                    switch (Mod.instance.randomIndex.Next(2))
                    {

                        case 1: rockScatter.verticalFlipped = true; break;

                    }

                    missileAnimations.Add(rockScatter);

                    TemporaryAnimatedSprite rockCore = MissileAnimation(location, rockfalling, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, Mod.instance.iconData.SchemeColour(rockScheme), 1f);

                    rockCore.rotation = (float)Math.PI * 0.5f * Mod.instance.randomIndex.Next(4);

                    missileAnimations.Add(rockCore);

                    break;

                case missiles.slimeball:

                    schemes slimeScheme = schemes.slime_one;

                    slimeScheme = (schemes)((int)slimeScheme + Mod.instance.randomIndex.Next(3));

                    Microsoft.Xna.Framework.Color slimeColour = Mod.instance.iconData.SchemeColour(slimeScheme);

                    missileIndexes slimetrail = (missileIndexes)((int)missileIndexes.slimetrail1 + Mod.instance.randomIndex.Next(3));

                    missileAnimations.Add(MissileAnimation(location, slimetrail, setat, scale, interval, frames, loops, depth, coreColour, 0.75f));

                    missileIndexes slimeball = (missileIndexes)((int)missileIndexes.slimeball + Mod.instance.randomIndex.Next(3));

                    missileAnimations.Add(MissileAnimation(location, slimeball, setat, (int)(scale * 0.75f), interval * frames * loops, 1, 1, depth + 0.0001f, slimeColour, 0.75f));

                    break;

                case missiles.cannonball:

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, coreColour, 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][0], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][1], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][2], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.cannonball, setat, (int)(scale * 0.75f), interval * frames * loops, 1, 1, depth + 0.0001f, Mod.instance.iconData.SchemeColour(schemes.death), 0.75f));

                    break;

                case missiles.bomb:

                    depth = 999f;

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, coreColour, 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][0], 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][1], 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.stars][2], 1f));

                    TemporaryAnimatedSprite bombAnimation = MissileAnimation(location, missileIndexes.bomb, setat, (int)(scale * 0.75f), interval * frames * loops, 1, 1, depth + 0.0001f, Microsoft.Xna.Framework.Color.BurlyWood, 1f);

                    bombAnimation.rotationChange = 0.0001f;

                    missileAnimations.Add(bombAnimation);

                    TemporaryAnimatedSprite bombAnimation2 = MissileAnimation(location, missileIndexes.bomb2, setat, (int)(scale * 0.75f), interval * frames * loops, 1, 1, depth + 0.0002f, Mod.instance.iconData.SchemeColour(scheme), 1f);

                    bombAnimation2.rotationChange = 0.0001f;

                    missileAnimations.Add(bombAnimation2);

                    break;

                case missiles.cat:

                    depth = 999f;

                    missileAnimations.Add(MissileAnimationTwo(location, missileIndexesTwo.rainbow1, setat, scale, interval, frames, loops, depth, coreColour, 1f));

                    TemporaryAnimatedSprite catAnimation = MissileAnimation(location, missileIndexes.bomb, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, Microsoft.Xna.Framework.Color.BurlyWood, 1f);

                    catAnimation.rotationChange = 0.0001f;

                    missileAnimations.Add(catAnimation);

                    TemporaryAnimatedSprite catAnimation2 = MissileAnimation(location, missileIndexes.bomb2, setat, scale, interval * frames * loops, 1, 1, depth + 0.0002f, Mod.instance.iconData.SchemeColour(scheme), 1f);

                    catAnimation2.rotationChange = 0.0001f;

                    missileAnimations.Add(catAnimation2);

                    missileAnimations.Add(MissileAnimationTwo(location, missileIndexesTwo.cat1, setat, scale, interval, frames, loops, depth + 0.0003f, coreColour, 1f));

                    break;

                case missiles.death:
                case missiles.deathfall:

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.death][2], 0.9f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.death][1], 0.9f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[schemes.death][0], 0.9f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, coreColour, 0.9f));

                    TemporaryAnimatedSprite deathAnimation = MissileAnimation(location, missileIndexes.death, setat, (int)(scale * 0.9f), interval * frames * loops, 1, 1, depth + 0.0001f, coreColour, 0.9f);

                    deathAnimation.rotationChange = 0.0001f;

                    missileAnimations.Add(deathAnimation);

                    break;

                case missiles.whisk:

                    TemporaryAnimatedSprite whisk1 = MissileAnimation(location, missileIndexes.whisk, setat, scale, interval * frames * loops, 1, 1, depth, coreColour, 0.8f);

                    whisk1.rotationChange = (float)(Math.PI / 60);

                    missileAnimations.Add(whisk1);

                    break;

                case missiles.shuriken:

                case missiles.knife:

                case missiles.axe:

                case missiles.hammer:

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, coreColour, 0.15f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, coreColour, 0.15f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, coreColour, 0.15f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.SchemeColour(scheme), 0.15f));

                    TemporaryAnimatedSprite thrownWeapon;

                    TemporaryAnimatedSprite thrownWeapon2;

                    switch (missile)
                    {
                        default:
                        case missiles.shuriken:

                            thrownWeapon = MissileAnimation(location, missileIndexes.shuriken, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, Microsoft.Xna.Framework.Color.LightSteelBlue, 1f);

                            thrownWeapon.rotationChange = 0f - (float)Math.PI / 30;

                            missileAnimations.Add(thrownWeapon);

                            thrownWeapon2 = MissileAnimation(location, missileIndexes.shuriken2, setat, scale, interval * frames * loops, 1, 1, depth + 0.0002f, Mod.instance.iconData.SchemeColour(scheme), 1f);

                            thrownWeapon2.rotationChange = 0f - (float)Math.PI / 30;

                            missileAnimations.Add(thrownWeapon2);

                            break;

                        case missiles.knife:
                            thrownWeapon = MissileAnimation(location, missileIndexes.knife, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, Microsoft.Xna.Framework.Color.SteelBlue, 1f);

                            thrownWeapon.rotationChange = 0f - (float)Math.PI / 30;

                            missileAnimations.Add(thrownWeapon);

                            thrownWeapon2 = MissileAnimation(location, missileIndexes.knife2, setat, scale, interval * frames * loops, 1, 1, depth + 0.0002f, Mod.instance.iconData.SchemeColour(scheme), 1f);

                            thrownWeapon2.rotationChange = 0f - (float)Math.PI / 30;

                            missileAnimations.Add(thrownWeapon2);

                            break;

                        case missiles.axe:
                            thrownWeapon = MissileAnimation(location, missileIndexes.axe, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, Microsoft.Xna.Framework.Color.BurlyWood, 1f);

                            thrownWeapon.rotationChange = 0f - (float)Math.PI / 30;

                            missileAnimations.Add(thrownWeapon);

                            thrownWeapon2 = MissileAnimation(location, missileIndexes.axe2, setat, scale, interval * frames * loops, 1, 1, depth + 0.0002f, Mod.instance.iconData.SchemeColour(scheme), 1f);

                            thrownWeapon2.rotationChange = 0f - (float)Math.PI / 30;

                            missileAnimations.Add(thrownWeapon2);

                            break;

                        case missiles.hammer:

                            thrownWeapon = MissileAnimationTwo(location, missileIndexesTwo.hammer, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, Microsoft.Xna.Framework.Color.BurlyWood, 1f);

                            thrownWeapon.rotationChange = 0f - (float)Math.PI / 30;

                            missileAnimations.Add(thrownWeapon);

                            thrownWeapon2 = MissileAnimationTwo(location, missileIndexesTwo.hammer2, setat, scale, interval * frames * loops, 1, 1, depth + 0.0002f, Mod.instance.iconData.SchemeColour(scheme), 1f);

                            thrownWeapon2.rotationChange = 0f - (float)Math.PI / 30;

                            missileAnimations.Add(thrownWeapon2);

                            break;


                    }

                    break;

            }

            construct = missileAnimations;

        }

        public TemporaryAnimatedSprite MissileAnimation(GameLocation location, missileIndexes missile, Vector2 origin, float scale, int interval, int frames, int loops, float depth, Microsoft.Xna.Framework.Color color, float alpha)
        {

            Microsoft.Xna.Framework.Rectangle rect = new((int)missile % 4 * 96, (int)((int)missile / 4) * 96, 96, 96);

            TemporaryAnimatedSprite missileAnimation = new(0, interval, frames, loops, origin, false, false)
            {

                sourceRect = rect,

                sourceRectStartingPos = new Vector2(rect.X, rect.Y),

                texture = missileTexture,

                scale = scale,

                layerDepth = depth,

                alpha = alpha,

                color = color,

            };

            location.temporarySprites.Add(missileAnimation);

            return missileAnimation;

        }

        public TemporaryAnimatedSprite MissileAnimationTwo(GameLocation location, missileIndexesTwo missile, Vector2 origin, float scale, int interval, int frames, int loops, float depth, Microsoft.Xna.Framework.Color color, float alpha)
        {

            Microsoft.Xna.Framework.Rectangle rect = new((int)missile % 4 * 96, (int)((int)missile / 4) * 96, 96, 96);

            TemporaryAnimatedSprite missileAnimation = new(0, interval, frames, loops, origin, false, false)
            {

                sourceRect = rect,

                sourceRectStartingPos = new Vector2(rect.X, rect.Y),

                texture = missileTextureTwo,

                scale = scale,

                layerDepth = depth,

                alpha = alpha,

                color = color,

            };

            location.temporarySprites.Add(missileAnimation);

            return missileAnimation;

        }

    }

}
