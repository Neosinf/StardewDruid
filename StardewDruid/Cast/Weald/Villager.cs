using Microsoft.Xna.Framework;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewValley;
using System.Xml.Linq;

namespace StardewDruid.Cast.Weald
{
    internal class Villager : CastHandle
    {

        NPC riteWitness;

        public Villager(Vector2 target,  NPC witness)
            : base(target)
        {

            riteWitness = witness;

        }

        public override void CastEffect()
        {

            int friendship = 0;

            if (Mod.instance.questHandle.IsComplete(QuestHandle.clearLesson))
            {
                
                friendship = 25;

            }

            ModUtility.GreetVillager(targetPlayer, riteWitness, friendship);

            ReactionData.ReactTo(riteWitness, "Weald", friendship);

        }

    }

}
