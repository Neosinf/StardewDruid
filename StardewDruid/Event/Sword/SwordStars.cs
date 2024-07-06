
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

        }

        public override void SetupCompanion()
        {

            if (!Mod.instance.characters.ContainsKey(CharacterHandle.characters.Revenant))
            {

                CharacterHandle.CharacterLoad(CharacterHandle.characters.Revenant, Character.Character.mode.home);

            }

            Mod.instance.characters[CharacterHandle.characters.Revenant].SwitchToMode(Character.Character.mode.random, Game1.player);

            companions[0] = Mod.instance.characters[CharacterHandle.characters.Revenant];

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

                case 1:

                    if(Mod.instance.characters[CharacterHandle.characters.Revenant].modeActive != Character.Character.mode.scene)
                    {
                        
                        Mod.instance.characters[CharacterHandle.characters.Revenant].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    }

                    if (Vector2.Distance(origin, Mod.instance.characters[CharacterHandle.characters.Revenant].Position) >= 64)
                    {

                        Mod.instance.characters[CharacterHandle.characters.Revenant].TargetEvent(0, new Vector2(27, 16) * 64, true);

                    }
                    else
                    {

                        DialogueLoad(0, 1);

                        Mod.instance.characters[CharacterHandle.characters.Revenant].TargetIdle(2000);

                        Mod.instance.characters[CharacterHandle.characters.Revenant].netStandbyActive.Set(true);

                        switch ((int)Game1.currentGameTime.TotalGameTime.TotalSeconds % 30)
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

                                break;

                        }

                    }

                    activeCounter = 0;

                    break;

                case 101:

                    companions[0].TargetIdle(6000);

                    companions[0].LookAtTarget(Game1.player.Position);

                    companions[0].clearTextAboveHead();

                    DialogueLoad(0, 2);

                    break;

                case 105:

                    DialogueNext(companions[0]);

                    break;

                case 110:

                    activeCounter = 200;

                    break;

                case 201:

                    DialogueLoad(0, 3);

                    break;

                case 205:

                    DialogueNext(companions[0]);

                    break;

                case 210:

                    activeCounter = 300;

                    break;

                case 301:

                    Mod.instance.characters[CharacterHandle.characters.Revenant].SwitchToMode(Character.Character.mode.random, Game1.player);

                    eventComplete = true;

                    break;

            }

        }

    }

}