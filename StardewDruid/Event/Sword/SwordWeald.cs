
using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewValley;
using System.Collections.Generic;


namespace StardewDruid.Event.Sword
{
    internal class SwordWeald : EventHandle
    {
        public Dictionary<int, Vector2> eventVectors = new()
        {

            // ENTER
            // Sighs location
            [1] = new Vector2(19, 7),
            // Rustling location
            [2] = new Vector2(14, 8),
            // Whistling location
            [3] = new Vector2(24, 8),


        };

        public SwordWeald()
        {

        }

        public override void EventActivate()
        {

            base.EventActivate();

            Mod.instance.spellRegister.Add(new(Game1.player.Position, 256, IconData.impacts.supree, new()) { sound = SpellHandle.Sounds.getNewSpecialItem, scheme = IconData.schemes.weald, });

            Mod.instance.iconData.DecorativeIndicator(location, Game1.player.Position, IconData.decorations.weald, 4f, new());

            AddActor(0, eventVectors[1] * 64);
            
            AddActor(1, eventVectors[2] * 64);
            
            AddActor(2, eventVectors[3] * 64);

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

                    Mod.instance.spellRegister.Add(new(origin, 256, IconData.impacts.supree, new()) { sound = SpellHandle.Sounds.discoverMineral, scheme = IconData.schemes.weald, });

                    break;

            }

        }

    }

}