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
using System.Reflection.Metadata;
using System.Linq;
using StardewValley.ItemTypeDefinitions;


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

        public bool projectileStatic;

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
            slimeballtwo,
            rockfall,
            whisk,
            warpball,
            shuriken,
            knife,
            axe,
            hammer,
            bomb,
            cat,
            rocket,
            firefall,
            frostball,
            deathfall,
            bats,
            boulder,
            holygrenade,

            // waves
            slash,
            wave,
            
            // bolt handle
            bolt,
            quickbolt,
            greatbolt,
            judgement,

            // echo
            echo,
            deathecho,
            bubbleecho,
            fireecho,
            curseecho,
            windecho,

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
            slimeball,
            slimeball2,
            slimeball3,

            fireball,
            warpball,
            whisk,
            frostball,

            rocket,
            slimeball4,
            slimeball5,
            slimeball6,

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

            cat1,
            cat2,
            cat3,
            cat4,

            swipe,
            slosh,
            spark1,
            spark2,

            hammer,
            hammer2,
            holygrenade,
            blank2,

            batdown1,
            batdown2,
            batdown3,
            batdown4,

            batup1,
            batup2,
            batup3,
            batup4,

            batright1,
            batright2,
            batright3,
            batright4,

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

                    Game1.player.currentLocation.playSound(SpellHandle.Sounds.fireball.ToString());

                    break;

                case missiles.slimeball:
                case missiles.slimeballtwo:

                    projectileTrajectory = missiletrajectories.ballistic;

                    projectileSpeed = 0.5f;

                    projectilePeak = 320;

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

                    projectileSpeed = 1.2f;

                    projectileIncrements = 4;

                    break;

                case missiles.rockfall:

                    projectileTrajectory = missiletrajectories.orbital;

                    projectileSpeed = 1f;

                    projectileIncrements = Mod.instance.randomIndex.Next(3, 6);

                    break;

                case missiles.bomb:
                case missiles.cat:
                case missiles.holygrenade:

                    projectileTrajectory = missiletrajectories.ballistic;

                    //factor = 2;

                    float bombDistance = Vector2.Distance(origin, destination);

                    if(bombDistance > 640)
                    {

                        projectilePeak = 150;

                        projectileIncrements = 4;

                        projectileSpeed = 1.25f;

                    }
                    else if(bombDistance > 320)
                    {
                        projectilePeak = 250;

                        projectileIncrements = 3;

                        projectileSpeed = 0.75f;

                    }
                    else
                    {
                        projectilePeak = 350;

                        projectileIncrements = 2;

                        projectileSpeed = 0.25f;

                    }

                    break;

                case missiles.cannonball:

                    projectileTrajectory = missiletrajectories.ballistic;

                    projectilePeak = 350;

                    projectileIncrements = 4;

                    projectileSpeed = 0.5f;

                    break;

                case missiles.boulder:
                case missiles.knife:
                case missiles.shuriken:
                case missiles.hammer:
                case missiles.axe:

                    projectileSpeed = 0.75f;

                    break;

                case missiles.bats:

                    projectileStatic = true;

                    projectileSpeed = 0.75f;

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

            CursorAdditional addEffects = new() { interval = duration * 1000, scale = projectile, alpha = 0.35f, };

            switch (lighting)
            {

                case lightEffects.shadow:

                    shadow = Mod.instance.iconData.CursorIndicator(location, destination + new Vector2(32), IconData.cursors.shadow, addEffects);

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

            Vector2 newPosition = projectilePosition + new Vector2(32, 32) - (new Vector2(construct.First().sourceRect.Width / 2, construct.First().sourceRect.Height / 2) * construct.First().scale);

            Vector2 diff = newPosition - construct.First().position;

            float rotate = (float)Math.Atan2(diff.Y, diff.X);

            for (int i = construct.Count - 1; i >= 0; i--)
            {

                newPosition = appear + new Vector2(32, 32) - (new Vector2(construct[i].sourceRect.Width / 2, construct[i].sourceRect.Height / 2) * construct[i].scale);

                construct[i].position = newPosition;

                if (projectileStatic)
                {

                    if (origin.X > destination.X)
                    {

                        construct[i].flipped = true;

                    }

                }
                else
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

                        if (offset < 0f)
                        {
                            break;

                        }

                        float offsetRatio = offset / 256;

                        shadow.scale = Math.Max(1f, factor- offsetRatio);

                        shadow.Position = destination - shadowing + new Vector2(32, 32) - new Vector2(24 * shadow.scale, 24* shadow.scale);

                        break;

                    default:
                    case missiletrajectories.sustained:

                        shadow.Position = destination - shadowing + new Vector2(32, 32) - new Vector2(24 * shadow.scale, 24 * shadow.scale) + new Vector2(0,96);

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

            Vector2 setat = origin + new Vector2(32, 32);

            int loops = (int)Math.Ceiling(increments * 0.5);

            int frames = 4;

            int interval = (increments * 250) / (loops * frames);

            Microsoft.Xna.Framework.Color coreColour = Microsoft.Xna.Framework.Color.White;

            switch (missile)
            {

                case missiles.fireball:
                case missiles.firefall:

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, coreColour, 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][0], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][1], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][2], 0.75f));

                    TemporaryAnimatedSprite fireball = MissileAnimation(location, missileIndexes.fireball, setat, scale * 0.75f, interval * frames * loops, 1, 1, depth + 0.0001f, coreColour, 1f);
                    fireball.rotationChange = (float)Math.PI / 60;
                    missileAnimations.Add(fireball);

                    break;

                case missiles.rocket:

                    Mod.instance.iconData.CreateImpact(location, origin - new Vector2(0, scale * 32), IconData.impacts.smoke, scale, new() { layer = depth - 0.0001f, });

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale * 0.75f, interval, frames, loops, depth, coreColour, 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale * 0.75f, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][0], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale * 0.75f, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][1], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale * 0.75f, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][2], 0.75f));

                    TemporaryAnimatedSprite rocket = MissileAnimation(location, missileIndexes.rocket, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, coreColour, 1f);

                    missileAnimations.Add(rocket);

                    break;

                case missiles.warpball:

                    TemporaryAnimatedSprite warpball = MissileAnimation(location, missileIndexes.warpball, setat, scale * 1.25f, interval * frames * loops, 1, 1, depth + 0.0001f, coreColour, 1f);

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

                    IconData.schemes meteorScheme = IconData.schemes.rock;

                    meteorScheme = (IconData.schemes)((int)meteorScheme + Mod.instance.randomIndex.Next(3));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, coreColour, 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][0], 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][1], 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][2], 1f));

                    missileIndexes meteorite = missileIndexes.meteor1;

                    meteorite = (missileIndexes)((int)meteorite + Mod.instance.randomIndex.Next(6));

                    TemporaryAnimatedSprite meteor = MissileAnimation(location, meteorite, setat, scale * 0.75f, interval * frames * loops, 1, 1, depth + 0.0001f, Mod.instance.iconData.SchemeColour(meteorScheme), 1f);

                    meteor.rotation = (float)Math.PI * 0.5f * Mod.instance.randomIndex.Next(4);

                    meteor.rotationChange = (float)Math.PI / 60;

                    missileAnimations.Add(meteor);

                    break;

                case missiles.rockfall:

                    IconData.schemes rockScheme = IconData.schemes.rock;

                    rockScheme = (IconData.schemes)((int)rockScheme + Mod.instance.randomIndex.Next(3));

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

                    missileIndexes slimeball = (missileIndexes)((int)missileIndexes.slimeball + Mod.instance.randomIndex.Next(3));

                    TemporaryAnimatedSprite slimeAnimation = MissileAnimation(location, slimeball, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, coreColour, 0.75f);

                    missileAnimations.Add(slimeAnimation);

                    break;

                case missiles.slimeballtwo:

                    missileIndexes slimeballtwo = (missileIndexes)((int)missileIndexes.slimeball4 + Mod.instance.randomIndex.Next(3));

                    TemporaryAnimatedSprite slimeAnimationtwo = MissileAnimation(location, slimeballtwo, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, coreColour, 0.75f);

                    missileAnimations.Add(slimeAnimationtwo);

                    break;
                case missiles.cannonball:

                    Mod.instance.iconData.CreateImpact(location, setat - new Vector2(0,scale*32), IconData.impacts.smoke, scale, new() { layer = depth - 0.0001f,});

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, coreColour, 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][0], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][1], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][2], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.cannonball, setat, (int)(scale * 0.75f), interval * frames * loops, 1, 1, depth + 0.0001f, Mod.instance.iconData.SchemeColour(IconData.schemes.bomb_two), 0.75f));

                    break;

                case missiles.bomb:

                    depth = 999f;

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, coreColour, 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][0], 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][1], 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][2], 1f));

                    TemporaryAnimatedSprite bombAnimation = MissileAnimation(location, missileIndexes.bomb, setat, scale * 0.75f, interval * frames * loops, 1, 1, depth + 0.0001f, Microsoft.Xna.Framework.Color.BurlyWood, 1f);

                    bombAnimation.rotationChange = 0.0001f;

                    missileAnimations.Add(bombAnimation);

                    TemporaryAnimatedSprite bombAnimation2 = MissileAnimation(location, missileIndexes.bomb2, setat, scale * 0.75f, interval * frames * loops, 1, 1, depth + 0.0002f, Mod.instance.iconData.SchemeColour(scheme), 1f);

                    bombAnimation2.rotationChange = 0.0001f;

                    missileAnimations.Add(bombAnimation2);

                    break;

                case missiles.cat:

                    depth = 999f;
                    
                    TemporaryAnimatedSprite catAnimation3 = MissileAnimationTwo(location, missileIndexesTwo.cat1, setat, scale, interval, frames, loops, depth + 0.0003f, coreColour, 1f);

                    switch (ModUtility.DirectionToTarget(origin, destination)[2])
                    {

                        case 5:
                        case 6:
                        case 7:

                            catAnimation3.verticalFlipped = true;

                            break;

                    }

                    missileAnimations.Add(catAnimation3);

                    break;

                case missiles.death:
                case missiles.deathfall:

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.death][2], 0.9f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.death][1], 0.9f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.death][0], 0.9f));

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

                            thrownWeapon2 = MissileAnimation(location, missileIndexes.shuriken2, setat, scale, interval * frames * loops, 1, 1, depth + 0.0002f, Mod.instance.iconData.SchemeColour(scheme), 1f);

                            break;

                        case missiles.knife:

                            thrownWeapon = MissileAnimation(location, missileIndexes.knife, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, Microsoft.Xna.Framework.Color.SteelBlue, 1f);

                            thrownWeapon2 = MissileAnimation(location, missileIndexes.knife2, setat, scale, interval * frames * loops, 1, 1, depth + 0.0002f, Mod.instance.iconData.SchemeColour(scheme), 1f);

                            break;

                        case missiles.axe:

                            thrownWeapon = MissileAnimation(location, missileIndexes.axe, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, Microsoft.Xna.Framework.Color.BurlyWood, 1f);

                            thrownWeapon2 = MissileAnimation(location, missileIndexes.axe2, setat, scale, interval * frames * loops, 1, 1, depth + 0.0002f, Mod.instance.iconData.SchemeColour(scheme), 1f);

                            break;

                        case missiles.hammer:

                            thrownWeapon = MissileAnimationTwo(location, missileIndexesTwo.hammer, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, Microsoft.Xna.Framework.Color.BurlyWood, 1f);

                            thrownWeapon2 = MissileAnimationTwo(location, missileIndexesTwo.hammer2, setat, scale, interval * frames * loops, 1, 1, depth + 0.0002f, Mod.instance.iconData.SchemeColour(scheme), 1f);

                            break;


                    }

                    switch (ModUtility.DirectionToTarget(origin, destination)[2])
                    {
                        case 0:
                        case 4:
 
                            break;

                        case 1:
                        case 2:
                        case 3:

                            thrownWeapon.rotationChange = (float)Math.PI / 30;

                            thrownWeapon2.rotationChange = (float)Math.PI / 30;

                            break;

                        case 5:
                        case 6:
                        case 7:

                            thrownWeapon.rotationChange = 0f - (float)Math.PI / 30;

                            thrownWeapon2.rotationChange = 0f - (float)Math.PI / 30;

                            break;

                    }

                    missileAnimations.Add(thrownWeapon);

                    missileAnimations.Add(thrownWeapon2);

                    break;

                case missiles.bats:

                    TemporaryAnimatedSprite missileAnimation;

                    switch (ModUtility.DirectionToTarget(origin, destination)[2])
                    {
                        case 0:
                        case 1:
                        case 7:

                            missileAnimation = MissileAnimationTwo(location, missileIndexesTwo.batup1, setat, 2f, interval, frames, loops, depth, coreColour, 1f);

                            missileAnimation.timeBasedMotion = true;

                            missileAnimation.scaleChange = scale / 4000f;

                            missileAnimations.Add(missileAnimation);

                            break;

                        case 2:
                        case 6:

                            missileAnimation = MissileAnimationTwo(location, missileIndexesTwo.batright1, setat, 2f, interval, frames, loops, depth, coreColour, 1f);

                            missileAnimation.timeBasedMotion = true;

                            missileAnimation.scaleChange = scale / 4000f;

                            missileAnimations.Add(missileAnimation);

                            break;

                        case 3:
                        case 4:
                        case 5:

                            missileAnimation = MissileAnimationTwo(location, missileIndexesTwo.batdown1, setat, 2f, interval, frames, loops, depth, coreColour, 1f);

                            missileAnimation.timeBasedMotion = true;

                            missileAnimation.scaleChange = scale / 4000f;

                            missileAnimations.Add(missileAnimation);

                            break;

                    }

                    break;

                case missiles.boulder:

                    IconData.schemes boulderScheme = IconData.schemes.rock;

                    boulderScheme = (IconData.schemes)((int)boulderScheme + Mod.instance.randomIndex.Next(3));

                    missileIndexes boulderfalling = missileIndexes.rock1;

                    boulderfalling = (missileIndexes)((int)boulderfalling + Mod.instance.randomIndex.Next(3));

                    TemporaryAnimatedSprite boulderCore = MissileAnimation(location, boulderfalling, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, Mod.instance.iconData.SchemeColour(boulderScheme), 1f);

                    boulderCore.rotation = 0.0001f;

                    missileAnimations.Add(boulderCore);

                    break;

                case missiles.holygrenade:

                    depth = 999f;

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, coreColour, 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][0], 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][1], 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, Mod.instance.iconData.gradientColours[IconData.schemes.stars][2], 1f));

                    TemporaryAnimatedSprite holyGrenade = MissileAnimationTwo(location, missileIndexesTwo.holygrenade, setat, (int)(scale * 0.75f), interval * frames * loops, 1, 1, depth + 0.0001f, Color.White, 1f);

                    holyGrenade.rotationChange = 0.0001f;

                    missileAnimations.Add(holyGrenade);

                    break;


            }

            construct = missileAnimations;

        }

        public TemporaryAnimatedSprite MissileAnimation(GameLocation location, missileIndexes missile, Vector2 origin, float scale, int interval, int frames, int loops, float depth, Microsoft.Xna.Framework.Color color, float alpha)
        {

            Microsoft.Xna.Framework.Rectangle rect = new((int)missile % 4 * 96, (int)((int)missile / 4) * 96, 96, 96);

            Vector2 setat = origin - (new Vector2(48, 48) * scale);

            TemporaryAnimatedSprite missileAnimation = new(0, interval, frames, loops, setat, false, false)
            {

                sourceRect = rect,

                sourceRectStartingPos = new Vector2(rect.X, rect.Y),

                texture = Mod.instance.iconData.missileTexture,

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

            Vector2 setat = origin - (new Vector2(48, 48) * scale);

            TemporaryAnimatedSprite missileAnimation = new(0, interval, frames, loops, setat, false, false)
            {

                sourceRect = rect,

                sourceRectStartingPos = new Vector2(rect.X, rect.Y),

                texture = Mod.instance.iconData.missileTextureTwo,

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
