using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast
{
    internal class Scarecrow : CastHandle
    {

        public Scarecrow(Mod mod, Vector2 target, Rite rite)
            : base(mod, target, rite)
        {

            castCost = Math.Max(12, 36 - (rite.caster.FarmingLevel * 2));

        }

        public override void CastWater()
        {

            int animationRow = 51;

            Rectangle animationRectangle = new(0, animationRow * 64, 64, 64);

            float animationInterval = 75f;

            int animationLength = 8;

            int animationLoops = 1;

            Color animationColor = new(0.8f, 0.8f, 1f, 1f);

            Vector2 animationPosition;

            float animationSort;

            for(int i = 1; i < 4; i++)
            { 

                List<Vector2> hoeVectors = ModUtility.GetTilesWithinRadius(targetLocation, targetVector, i);

                foreach (Vector2 hoeVector in hoeVectors)
                {

                    if (targetLocation.terrainFeatures.ContainsKey(hoeVector))
                    {

                        var terrainFeature = targetLocation.terrainFeatures[hoeVector];

                        if (terrainFeature is HoeDirt)
                        {

                            HoeDirt hoeDirt = terrainFeature as HoeDirt;

                            if (hoeDirt.state.Value == 0)
                            {

                                hoeDirt.state.Value = 1;

                                animationPosition = new((hoeVector.X * 64) + 10, (hoeVector.Y * 64) + 10);

                                animationSort = (hoeVector.X * 1000) + hoeVector.Y;

                                TemporaryAnimatedSprite newAnimation = new("TileSheets\\animations", animationRectangle, animationInterval, animationLength, animationLoops, animationPosition, false, false, animationSort, 0f, animationColor, 0.7f, 0f, 0f, 0f)
                                {
                                    //motion = new Vector2(0f, -0.1f),
                                    delayBeforeAnimationStart = i*200,

                                };

                                Game1.currentLocation.temporarySprites.Add(newAnimation);

                            }

                        }

                    }

                    continue;

                }

            }

            castFire = true;

            //castLimit = true;

            ModUtility.AnimateBolt(targetLocation, targetVector);

            //ModUtility.AnimateRipple(targetLocation, targetVector);

            Utility.addSprinklesToLocation(targetLocation, (int)targetVector.X - 1, (int)targetVector.Y - 1, 3, 3, 999, 333, Color.White);

            return;

        }

    }

}
