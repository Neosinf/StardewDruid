using Microsoft.Xna.Framework;
using StardewValley;
using System.Collections.Generic;


namespace StardewDruid.Cast.Weald
{
    internal class Dirt : CastHandle
    {

        public Dirt(Vector2 target)
            : base(target)
        {

            castCost = 6;

            if (Game1.player.ForagingLevel >= 8)
            {

                castCost = 4;

            }

        }

        public override void CastEffect()
        {

            int probability = randomIndex.Next(3);

            if (probability == 0)
            {

                int procChance = 65 - Mod.instance.CurrentProgress;

                if (randomIndex.Next(procChance) == 0 && Mod.instance.rite.spawnIndex["artifact"])
                {

                    int tileX = (int)targetVector.X;
                    int tileY = (int)targetVector.Y;

                    if (targetLocation.getTileIndexAt(tileX, tileY, "AlwaysFront") == -1 &&
                        targetLocation.getTileIndexAt(tileX, tileY, "Front") == -1 &&
                        !targetLocation.isBehindBush(targetVector) &&
                        targetLocation.doesTileHaveProperty(tileX, tileY, "Diggable", "Back") != null
                    )
                    {

                        targetLocation.objects.Add(targetVector, new StardewValley.Object("590", 1));

                        castFire = true;

                    }

                }

            }
            else if (Mod.instance.rite.spawnIndex["trees"] && !Mod.instance.EffectDisabled("Trees")) // 1/10 tree
            {

                bool treeSpawn  = false;

                switch (targetPlayer.FacingDirection)
                {
                    case 0:

                        if(Mod.instance.rite.castVector.Y < targetVector.Y)
                        {
                            treeSpawn = ModUtility.RandomTree(targetLocation, targetVector);

                        }        

                        break;

                    case 1:

                        if (Mod.instance.rite.castVector.X > targetVector.X)
                        {
                            treeSpawn = ModUtility.RandomTree(targetLocation, targetVector);

                        }
                        break;
                    
                    case 2:

                        if (Mod.instance.rite.castVector.Y > targetVector.Y)
                        {
                            treeSpawn = ModUtility.RandomTree(targetLocation, targetVector);

                        }
                        break;

                    default:

                        if (Mod.instance.rite.castVector.X < targetVector.X)
                        {
                            treeSpawn = ModUtility.RandomTree(targetLocation, targetVector);

                        }
                        break;

                }

                if (treeSpawn)
                {

                    castFire = true;

                }

            }

            return;

        }

    }
}
