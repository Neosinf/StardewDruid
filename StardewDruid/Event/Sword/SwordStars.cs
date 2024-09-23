
using Microsoft.Xna.Framework;
using StardewDruid.Character;
using StardewModdingAPI;
using StardewValley;


namespace StardewDruid.Event.Sword
{
    internal class SwordStars : EventHandle
    {

        public SwordStars()
        {

            activeLimit = -1;

        }

        public override void SetupCompanion()
        {

            if (!Mod.instance.characters.ContainsKey(CharacterHandle.characters.Revenant))
            {

                CharacterHandle.CharacterLoad(CharacterHandle.characters.Revenant, Character.Character.mode.home);

            }

            companions[0] = Mod.instance.characters[CharacterHandle.characters.Revenant];

            companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

            companions[0].Position = new Vector2(26, 19) * 64;

            companions[0].ResetActives();

            companions[0].netIdle.Set((int)Character.Character.idles.standby);

            companions[0].eventName = eventId;

            voices[0] = companions[0];

            DialogueLoad(companions[0], 1);

        }

        public override bool TriggerActive()
        {

            if (TriggerLocation())
            {

                EventActivate();

            }

            return false;

        }

        public override bool AttemptReset()
        {

            if (!eventActive)
            {

                return true;

            }

            eventActive = false;

            EventRemove();

            triggerEvent = true;

            SetupCompanion();

            return true;

        }

        public override void EventRemove()
        {

            if (activeCounter >= 100 && eventActive)
            {

                eventComplete = true;

            }

            base.EventRemove();
        }

        public override void EventInterval()
        {
            activeCounter++;

            switch (activeCounter)
            {

                case 4:

                    DialogueCue(1);

                    break;

                case 7:

                    DialogueCue(2);

                    break;

                case 10:

                    DialogueCue(3);

                    break;

                case 13:


                    DialogueCue(4);

                    break;

                case 16:

                    DialogueCue(5);

                    break;

                case 19:

                    DialogueCue(6);

                    break;

                case 22:

                    DialogueCue(7);

                    activeCounter = 0;

                    break;

                case 101:

                    companions[0].ResetActives();

                    companions[0].LookAtTarget(Game1.player.Position);

                    DialogueSetups(companions[0], 2);

                    break;

                case 105:

                    activeCounter = 200;

                    break;

                case 201:

                    DialogueSetups(companions[0], 3);

                    break;

                case 205:

                    activeCounter = 300;

                    break;

                case 301:

                    companions[0].SwitchToMode(Character.Character.mode.random, Game1.player);

                    DialogueCue(8);

                    companions[0].LookAtTarget(Game1.player.Position);

                    companions[0].TargetIdle(6000);

                    eventComplete = true;

                    break;

            }

        }

    }

}