using Microsoft.Xna.Framework;
using System.Collections.Generic;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Xml.Linq;

namespace StardewDruid.Cast
{
    internal class GreetVillager : Cast
    {

        private readonly StardewValley.NPC targetVillager;

        public GreetVillager(Mod mod, Vector2 target, Rite rite, NPC villager)
            : base(mod, target, rite)
        {

            targetVillager = villager;

            castCost = 4;

        }

        public override void CastEarth()
        {

            targetVillager.faceTowardFarmerForPeriod(3000, 4, false, targetPlayer);

            if(targetPlayer.hasPlayerTalkedToNPC(targetVillager.Name))
            {
                
                return;

            }

            int emoteIndex = 8;

            if (targetPlayer.friendshipData[targetVillager.Name].Points >= 500)
            {

                emoteIndex = 32;

            }

            if (targetPlayer.friendshipData[targetVillager.Name].Points >= 1000)
            {

                emoteIndex = 20;

            }

            targetVillager.doEmote(emoteIndex);

            targetPlayer.friendshipData[targetVillager.Name].TalkedToToday = true;

            targetPlayer.changeFriendship(25, targetVillager);

            castFire = true;

        }

    }

}
