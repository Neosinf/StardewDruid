﻿using Microsoft.Xna.Framework;
using StardewValley;
using System.Collections.Generic;

namespace StardewDruid.Cast.Weald
{
    internal class Grass : CastHandle
    {

        public Grass(Vector2 target)
            : base(target)
        {

            castCost = 0;

        }

        public override void CastEffect()
        {

            if (!targetLocation.terrainFeatures.ContainsKey(targetVector))
            {

                return;

            }

            if (targetLocation.terrainFeatures[targetVector] is not StardewValley.TerrainFeatures.Grass)
            {

                return;

            }

            int probability = randomIndex.Next(2);

            List<Throw> throwList = new();

            if (randomIndex.Next(100) == 0) // 1:1000 chance
            {

                throwList.Add(new(targetPlayer, targetVector * 64, 114, 0));

            }

            if (probability == 0 && !Mod.instance.EffectDisabled("Seeds") && Mod.instance.rite.castTask.ContainsKey("masterCreature"))
            {

                int wildSeed;

                switch (Game1.currentSeason)
                {

                    case "spring":

                        wildSeed = 495;
                        break;

                    case "summer":

                        wildSeed = 496;
                        break;

                    case "fall":

                        wildSeed = 497;
                        break;

                    default:

                        wildSeed = 498;

                        break;

                }

                for (int i = 0; i < randomIndex.Next(4); i++)
                {

                    throwList.Add(new(targetPlayer, targetVector * 64, wildSeed, 0));

                }

            }
            else
            {

                for (int i = 0; i < randomIndex.Next(4); i++)
                {

                    throwList.Add(new(targetPlayer, targetVector * 64, 771, 0));

                }

            }

            for (int i = 0; i < throwList.Count; i++)
            {

                throwList[i].ThrowObject();

            }

        }

    }
}
