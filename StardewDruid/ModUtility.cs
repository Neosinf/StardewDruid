using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;
using static StardewValley.FarmerSprite;
using StardewDruid.Cast;
using StardewValley.TerrainFeatures;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid
{
    static class ModUtility
    {

        public static void AnimateHands(Farmer player, int direction, int timeFrame)
        {

            //List<int> validAnimations = new() { 0, 6, 12 };

            //if (validAnimations.Contains(player.FarmerSprite.CurrentSingleAnimation))
            //{

                player.Halt();

                AnimationFrame carryAnimation;

                switch (direction)
                {

                    case 0: // Up

                        carryAnimation = new(12, timeFrame, true, false); // changes secondaryArm to active

                        break;

                    case 1: // Right

                        carryAnimation = new(6, timeFrame, true, false);

                        break;

                    case 2: // Down

                        carryAnimation = new(0, timeFrame, true, false);

                        break;

                    default: // Left

                        carryAnimation = new(6, timeFrame, true, true); // same as right but flipped

                        break;

                }

                player.FarmerSprite.animateOnce(new AnimationFrame[1] { carryAnimation });
            
           //}

        }

        public static void AnimateGrowth(GameLocation targetLocation, Vector2 targetVector) // DruidCastGrowth
        {

            int animationRow = 10;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

            float animationInterval = 75f;

            int animationLength = 8;

            int animationLoops = 1;

            Microsoft.Xna.Framework.Color animationColor = new(0.8f, 1, 0.8f, 1); // light green

            Vector2 animationPosition = new(targetVector.X * 64 + 8, targetVector.Y * 64 + 8);

            float animationSort = (animationPosition.X * 1000) + animationPosition.Y;

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, false, animationSort, 0f, animationColor, 1f, 0f, 0f, 0f)
            {
                //motion = new Vector2(0f, -0.1f)

            };

            Game1.currentLocation.temporarySprites.Add(newAnimation);

            return;

        }

        public static void AnimateEarth(GameLocation targetLocation, Vector2 targetVector) // DruidGrowthAnimation
        {

            int animationRow = 11;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

            float animationInterval = 75f;

            int animationLength = 8;

            int animationLoops = 1;

            Microsoft.Xna.Framework.Color animationColor = Microsoft.Xna.Framework.Color.White; //new(0.8f, 1, 0.8f, 1); // light green

            Vector2 animationPosition = new(targetVector.X * 64 + 8, targetVector.Y * 64 + 8);

            float animationSort = (animationPosition.X * 1000) + animationPosition.Y + 100;

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, false, animationSort, 0f, animationColor, 0.75f, 0f, 0f, 0f)
            {
                //motion = new Vector2(0f, -0.1f)

            };

            Game1.currentLocation.temporarySprites.Add(newAnimation);

            return;

        }

        public static void AnimateSplash(GameLocation targetLocation, Vector2 targetVector, bool animationFlipped) // DruidCastSplash
        {

            int animationRow = 19;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 128, 128);

            float animationInterval = 150f;

            int animationLength = 4;

            int animationLoops = 1;

            Microsoft.Xna.Framework.Color animationColor = Microsoft.Xna.Framework.Color.White;

            Vector2 animationPosition = new(targetVector.X * 64, targetVector.Y * 64);

            float animationSort = (targetVector.X * 1000) + targetVector.Y;

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, animationFlipped, animationSort, 0f, animationColor, 1f, 0f, 0f, 0f);

            Game1.currentLocation.temporarySprites.Add(newAnimation);

            return;

        }

        public static void AnimateBolt(GameLocation targetLocation, Vector2 targetVector) //DruidLightningBolt
        {
            AnimateBolt(targetLocation, targetVector, "thunder");

        }

        public static void AnimateBolt(GameLocation targetLocation, Vector2 targetVector, string playSound)
        {

            Game1.flashAlpha = (float)(0.5 + Game1.random.NextDouble());

            Game1.playSound(playSound);

            Vector2 targetPosition = new(targetVector.X * 64, targetVector.Y * 64);

            targetLocation.temporarySprites.Add(new TemporaryAnimatedSprite(362, 75f, 6, 1, targetPosition, flicker: false, flipped: false));

            Microsoft.Xna.Framework.Rectangle sourceRect = new Microsoft.Xna.Framework.Rectangle(644, 1078, 37, 57);

            Vector2 position = targetPosition + new Vector2(-5, -sourceRect.Height);

            while (position.Y > (float)(-sourceRect.Height * 2))
            {
                targetLocation.temporarySprites.Add(new TemporaryAnimatedSprite("LooseSprites\\Cursors", sourceRect, 9999f, 1, 999, position, flicker: false, Game1.random.NextDouble() < 0.5, (targetPosition.Y + 32f) / 10000f + 0.001f, 0.025f, Microsoft.Xna.Framework.Color.White, 2f, 0f, 0f, 0f)
                {
                    light = true,
                    lightRadius = 2f,
                    delayBeforeAnimationStart = 200,
                    lightcolor = Microsoft.Xna.Framework.Color.Black
                });
                position.Y -= sourceRect.Height * 2;
            }

            return;

        }

        public static void AnimateRipple(GameLocation targetLocation, Vector2 targetVector) //DruidCastSplosh
        {

            Game1.playSound("yoba");

            int animationRow = 0;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

            float animationInterval = 75f;

            int animationLength = 8;

            int animationLoops = 1;

            Vector2 animationPosition = new((targetVector.X * 64) - 16, (targetVector.Y * 64) - 16);

            float animationSort = (targetVector.X * 1000) + targetVector.Y;

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, false, animationSort, 0f, Microsoft.Xna.Framework.Color.White, 1.5f, 0f, 0f, 0f);

            targetLocation.temporarySprites.Add(newAnimation);

        }

        public static void AnimateMeteor(GameLocation targetLocation, Vector2 targetVector, bool targetDirection)
        {

            Microsoft.Xna.Framework.Rectangle meteorRectangle = new(0, 0, 32, 32);

            Vector2 meteorPosition;

            Vector2 meteorMotion;

            bool meteorRoll;

            switch (targetDirection)
            {

                case true:

                    meteorPosition = new((targetVector.X - 3) * 64, (targetVector.Y - 6) * 64);

                    meteorMotion = new(0.32f, 0.64f);

                    meteorRoll = false;

                    break;

                default:

                    meteorPosition = new((targetVector.X + 3) * 64, (targetVector.Y - 6) * 64);

                    meteorMotion = new(-0.32f, 0.64f);

                    meteorRoll = true;

                    break;

            }

            float meteorInterval = 150f;

            float animationSort = (targetVector.X * 1000) + targetVector.Y + 10;

            TemporaryAnimatedSprite meteorAnimation = new("TileSheets\\Fireball", meteorRectangle, meteorInterval, 4, 0, meteorPosition, flicker: false, meteorRoll, animationSort, 0f, Color.White, 2f, 0f, 0f, 0f)
            {

                motion = meteorMotion,

                timeBasedMotion = true,

            };

            targetLocation.temporarySprites.Add(meteorAnimation);

        }

        public static TemporaryAnimatedSprite AnimateIndicator(GameLocation targetLocation, Vector2 targetVector, Color animationColor)
        {

            int animationRow = 0;

            Microsoft.Xna.Framework.Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

            float animationInterval = 100f;

            int animationLength = 8;

            int animationLoops = 999999;

            Vector2 animationPosition = new((targetVector.X * 64) - 16, (targetVector.Y * 64) - 16);

            float animationSort = (targetVector.X * 1000) + targetVector.Y;

            TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, false, animationSort, 0f, animationColor, 1.5f, 0f, 0f, 0f) {

                dontClearOnAreaEntry = false,

            };

            targetLocation.temporarySprites.Add(newAnimation);

            return (newAnimation);

        }

        public static bool CheckSeed(GameLocation targetLocation, Vector2 targetVector)
        {
            
            bool plantSeed = true;

            List<Vector2> neighbourVectors = GetTilesWithinRadius(targetLocation, targetVector, 1);

            Layer buildingLayer = targetLocation.Map.GetLayer("Buildings");

            foreach (Vector2 neighbourVector in neighbourVectors)
            {

                if (!plantSeed)
                {
                    break;
                }

                Tile buildingTile = buildingLayer.PickTile(new Location((int)neighbourVector.X * 64, (int)neighbourVector.Y * 64), Game1.viewport.Size);

                if (buildingTile != null)
                {

                    if (buildingTile.TileIndexProperties.TryGetValue("Passable", out _) == false)
                    {
                        plantSeed = false;

                    }

                    continue;

                }

                if (targetLocation.terrainFeatures.ContainsKey(neighbourVector))
                {
                    var terrainFeature = targetLocation.terrainFeatures[neighbourVector];

                    switch (terrainFeature.GetType().Name.ToString())
                    {

                        case "Tree":

                            plantSeed = false;

                            break;

                        case "HoeDirt":

                            HoeDirt hoeDirt = terrainFeature as HoeDirt;

                            if (hoeDirt.crop != null)
                            {

                                plantSeed = false;

                            }

                            break;

                        default:

                            break;

                    }
                }

            }

            return plantSeed;

        }

        static List<Vector2> TilesWithinOne(Vector2 center)
        {
            List<Vector2> result = new() {

                center + new Vector2(0, -1),    // N
                center + new Vector2(1, -1),    // NE
                center + new Vector2(1, 0),     // E
                center + new Vector2(1, 1),     // SE
                center + new Vector2(0, 1),     // S
                center + new Vector2(-1, 1),    // SW
                center + new Vector2(-1, 0),    // W
                center + new Vector2(-1, -1),   // NW

            };

            return result;

        }

        static List<Vector2> TilesWithinTwo(Vector2 center)
        {
            List<Vector2> result = new()
            {
               
                center + new Vector2(0,-2), // N
                center + new Vector2(1,-2), // NE

                center + new Vector2(2,-1), // NE
                center + new Vector2(2,0), // E
                center + new Vector2(2,1), // SE

                center + new Vector2(1,2), // SE
                center + new Vector2(0,2), // S
                center + new Vector2(-1,2), // SW

                center + new Vector2(-2,1), // SW
                center + new Vector2(-2,0), // W
                center + new Vector2(-2,-1), // NW

                 center + new Vector2(-1,-2), // NW

            };

            return result;

        }

        static List<Vector2> TilesWithinThree(Vector2 center)
        {
            List<Vector2> result = new() {

                center + new Vector2(0,-3), // N
                center + new Vector2(1,-3),

                center + new Vector2(2,-2), // NE

                center + new Vector2(3,-1), // E
                center + new Vector2(3,0),
                center + new Vector2(3,1),

                center + new Vector2(2,2), // SE

                center + new Vector2(1,3), // S
                center + new Vector2(0,3),
                center + new Vector2(-1,3),

                center + new Vector2(-2,2), // SW

                center + new Vector2(-3,1), // W
                center + new Vector2(-3,0),
                center + new Vector2(-3,-1),

                center + new Vector2(-2,-2), // NW

                center + new Vector2(-1,-3), // NNW
 
            };

            return result;

        }

        static List<Vector2> TilesWithinFour(Vector2 center)
        {
            List<Vector2> result = new() {

                
                center + new Vector2(0,-4), // N
                center + new Vector2(1,-4),

                center + new Vector2(2,-3),
                center + new Vector2(3,-3), // NE
                center + new Vector2(3,-2),

                center + new Vector2(4,-1), // E
                center + new Vector2(4,0),
                center + new Vector2(4,1),

                center + new Vector2(3,2),
                center + new Vector2(3,3), // SE
                center + new Vector2(2,3),

                center + new Vector2(1,4), // S
                center + new Vector2(0,4),
                center + new Vector2(-1,4),

                center + new Vector2(-2,3),
                center + new Vector2(-3,3), // SW
                center + new Vector2(-3,2),

                center + new Vector2(-4,1), // W
                center + new Vector2(-4,0),
                center + new Vector2(-4,-1),

                center + new Vector2(-3,-2),
                center + new Vector2(-3,-3), // NW
                center + new Vector2(-2,-3),

                center + new Vector2(-1,-4), // NNW

            };

            return result;

        }

        static List<Vector2> TilesWithinFive(Vector2 center)
        {
            List<Vector2> result = new() {

                center + new Vector2(0,-5), // N
                center + new Vector2(1,-5),

                center + new Vector2(2,-4), // NE
                center + new Vector2(3,-4),
                center + new Vector2(4,-3),
                center + new Vector2(4,-2),

                center + new Vector2(5,-1), // E
                center + new Vector2(5,0),
                center + new Vector2(5,1),

                center + new Vector2(4,2), // SE
                center + new Vector2(4,3),
                center + new Vector2(3,4),
                center + new Vector2(2,4),
               
                center + new Vector2(1,5), // S
                center + new Vector2(0,5),
                center + new Vector2(-1,5),

                center + new Vector2(-2,4), // SW
                center + new Vector2(-3,4),
                center + new Vector2(-4,3),
                center + new Vector2(-4,2),

                center + new Vector2(-5,1), // W
                center + new Vector2(-5,0),
                center + new Vector2(-5,-1),

                center + new Vector2(-4,-2), // NW
                center + new Vector2(-4,-3),
                center + new Vector2(-3,-4),
                center + new Vector2(-2,-4),

                center + new Vector2(-1,-5), // NNW

            };

            return result;

        }

        static List<Vector2> TilesWithinSix(Vector2 center)
        {
            List<Vector2> result = new() {

                center + new Vector2(0,-6), // N
                center + new Vector2(1,-6),

                center + new Vector2(2,-5),
                center + new Vector2(3,-5),
                center + new Vector2(4,-4), // NE
                center + new Vector2(5,-3),
                center + new Vector2(5,-2),

                center + new Vector2(6,-1),
                center + new Vector2(6,0), // E
                center + new Vector2(6,1),

                center + new Vector2(5,2),
                center + new Vector2(5,3),
                center + new Vector2(4,4), // SE
                center + new Vector2(3,5),
                center + new Vector2(2,5),

                center + new Vector2(1,6),
                center + new Vector2(0,6), // S
                center + new Vector2(-1,6),

                center + new Vector2(-2,5),
                center + new Vector2(-3,5),
                center + new Vector2(-4,4), // SW
                center + new Vector2(-5,3),
                center + new Vector2(-5,2),

                center + new Vector2(-6,-1),
                center + new Vector2(-6,0), // W
                center + new Vector2(-6,1),

                center + new Vector2(-5,-2),
                center + new Vector2(-5,-3),
                center + new Vector2(-4,-4), // NW
                center + new Vector2(-3,-5),
                center + new Vector2(-2,-5),

                center + new Vector2(-1,-6), // NNW

            };

            return result;

        }

        public static List<Vector2> GetTilesWithinRadius(GameLocation playerLocation, Vector2 center, int level)
        {

            List<Vector2> templateList;

            switch (level)
            {
                case 2:
                    templateList = TilesWithinTwo(center);
                    break;
                case 3:
                    templateList = TilesWithinThree(center);
                    break;
                case 4:
                    templateList = TilesWithinFour(center);
                    break;
                case 5:
                    templateList = TilesWithinFive(center);
                    break;
                case 6:
                    templateList = TilesWithinSix(center);
                    break;
                default: // 1
                    templateList = TilesWithinOne(center);
                    break;

            }

            float mapWidth = (playerLocation.Map.DisplayWidth / 16);

            float mapHeight = (playerLocation.Map.DisplayHeight / 16);

            List<Vector2> vectorList = new();

            foreach (Vector2 testVector in templateList)
            {

                if (testVector.X < 0 || testVector.X > mapWidth || testVector.Y < 0 || testVector.Y > mapHeight)
                {

                }
                else
                {

                    vectorList.Add(testVector);

                }

            }

            return vectorList;

        }

    }

}
