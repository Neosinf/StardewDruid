using Microsoft.Xna.Framework;
using StardewDruid.Dialogue;
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

            if (!Mod.instance.rite.castTask.ContainsKey("masterVillager"))
            {

                Mod.instance.UpdateTask("lessonVillager", 1);

            }
            else
            {

                friendship = 25;

            }

            if (targetPlayer.friendshipData.TryGetValue(riteWitness.Name, out var _))
            {

                ModUtility.GreetVillager(targetPlayer, riteWitness, friendship);

            }

            Reaction.ReactTo(riteWitness, "Weald", friendship);

        }

    }

}
