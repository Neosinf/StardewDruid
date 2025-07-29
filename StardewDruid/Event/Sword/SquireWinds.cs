
using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Location;
using StardewDruid.Location.Druid;
using StardewValley;
using System.Collections.Generic;


namespace StardewDruid.Event.Sword
{
    internal class SquireWinds : EventHandle
    {

        public SquireWinds()
        {

            origin = (Mod.instance.locations[LocationHandle.druid_grove_name] as Grove).circlePosition - new Vector2(0, 64);

            sceneLimit = 4;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            Mod.instance.spellRegister.Add(new(Game1.player, IconData.ritecircles.winds){ soundTrigger = Handle.SoundHandle.SoundCue.RisingWind, });

            AddActor(0, origin - new Vector2(0, 128));
            
            AddActor(1, origin - new Vector2(128, 0));

            AddActor(2, origin + new Vector2(128, 0));

        }

        public override void EventInterval()
        {

            activeCounter++;

            switch (activeScene)
            {

                case 0:

                    SceneOne();

                    break;

                case 1:

                    SceneTwo();

                    break;

                case 2:

                    SceneThree();

                    break;

                case 3:

                    SceneFour();

                    break;

                case 4:

                    SceneFive();

                    break;

            }

        }

        public void SceneOne()
        {

            switch (activeCounter)
            {

                case 1:

                    SceneBar(eventTitle, 0);

                    DialogueCue(1);

                    break;

                case 4:

                    DialogueCue(2);

                    break;

                case 7:

                    DialogueCue(3);

                    break;

                case 10:

                    DialogueSetups(null, 0);

                    break;

                case 13:

                    NextScene();

                    break;

            }

        }

        public void SceneTwo()
        {

            switch (activeCounter)
            {

                case 1:

                    DialogueSetups(null, 1);

                    break;

                case 4:

                    NextScene();

                    break;

            }

        }

        public void SceneThree()
        {

            switch (activeCounter)
            {

                case 1:

                    DialogueSetups(null, 2);

                    break;

                case 4:

                    NextScene();

                    break;

            }

        }

        public void SceneFour()
        {

            switch (activeCounter)
            {

                case 1:

                    Vector2 effigyEnter = (location as Grove).benchPosition + new Vector2(352, -64);

                    LoadCompanion(CharacterHandle.characters.Effigy, effigyEnter, 0, 3, true);

                    companions[0].TargetEvent(0, Game1.player.Position + new Vector2(96, 0));

                    DialogueCue(4);

                    break;

                case 5:

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    DialogueSetups(companions[0], 3);

                    break;

                case 8:

                    NextScene();

                    break;

            }

        }

        public void SceneFive()
        {

            switch (activeCounter)
            {

                case 1:

                    DialogueCue(5);

                    break;

                case 4:

                    DialogueCue(6);

                    break;

                case 7:

                    eventComplete = true;

                    break;

            }

        }

        public override void SkipEvent(int index = -1)
        {

            switch (activeScene)
            {

                case 0:
                case 1:
                case 2:

                    Vector2 originTile2 = ModUtility.PositionToTile(origin);

                    Mod.instance.WarpAllFarmers(location.Name, (int)originTile2.X, (int)originTile2.Y, 2);

                    SetDialogueChoices(0, 2);

                    activeScene = 3;

                    activeCounter = 0;

                    break;

                case 3:
                case 4:

                    SetDialogueChoices(3, 3);

                    eventComplete = true;

                    break;

            }

        }

    }

}