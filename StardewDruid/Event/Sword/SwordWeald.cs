
using Microsoft.Xna.Framework;
using StardewDruid.Data;
using StardewValley;


namespace StardewDruid.Event.Sword
{
    internal class SwordWeald : EventHandle
    {

        public SwordWeald()
        {

        }

        public override void EventActivate()
        {

            base.EventActivate();

            ModUtility.AnimateHands(Game1.player,Game1.player.FacingDirection,600);

            Mod.instance.iconData.DecorativeIndicator(location, Game1.player.Position, IconData.decorations.weald, 4f, new());

            location.playSound("discoverMineral");

            AddActor(0, origin + new Vector2(-192, -32));
            AddActor(1, origin + new Vector2(128, -96));
            AddActor(2, origin + new Vector2(-64, -180));
        }

        public override void EventInterval()
        {

            activeCounter++;

            switch (activeCounter)
            {

                case 1:

                    DialogueCue(0);

                    break;

                case 4:

                    DialogueCue(1);

                    break;

                case 7:

                    DialogueCue(2);

                    break;

                case 10:

                    DialogueSetups(null, 1);

                    break;

                case 101:

                    DialogueSetups(null, 2);

                    break;

                case 201:

                    DialogueSetups(null, 3);

                    break;

                case 301:

                    eventComplete = true;

                    DialogueCue(4);

                    //---------------------- throw Forest Sword

                    Mod.instance.iconData.ImpactIndicator(location, origin, IconData.impacts.nature, 6f, new());

                    location.playSound("discoverMineral");

                    break;

            }

        }

    }

}