using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Event
{
    internal class EventHandle
    {

        private Mod mod;

        public EventHandle(Mod Mod)
        {

            mod = Mod;

        }

        public void RunEvent(Vector2 vector, Quest questData, Rite rite)
        {

            ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 500);

            Game1.player.currentLocation.playSound("yoba");

            Cast.CastHandle castHandle;

            switch (questData.triggerType)
            {

                case "figure":

                    castHandle = new StardewDruid.Event.Figure(mod, vector, rite, questData);

                    break;

                case "sword":

                    castHandle = new StardewDruid.Event.Sword(mod, vector, rite, questData);

                    break;

                default: // challenge

                    castHandle = new StardewDruid.Event.Challenge(mod, vector, rite, questData);

                    break;

            }

            castHandle.CastQuest();

        }

    }

}
