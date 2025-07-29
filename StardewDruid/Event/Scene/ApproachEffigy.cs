using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewDruid.Journal;
using StardewDruid.Location.Druid;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;


namespace StardewDruid.Event.Scene
{
    public class ApproachEffigy : EventHandle
    {

        public Dictionary<int, Vector2> eventVectors = new();

        public ApproachEffigy()
        {

            eventVectors[0] = (Mod.instance.locations[Location.LocationHandle.druid_cavern_name] as Cavern).middlePosition;

            eventVectors[1] = new Vector2(Cavern.groveExitX, Cavern.groveExitY) * 64;

            origin = eventVectors[0];

            sceneLimit = 3;

        }

        public override bool EventPerformAction(actionButtons Action = actionButtons.action)
        {

            if(Action != actionButtons.rite)
            {

                return false;

            }

            if (activeScene != 0)
            {

                return false;

            }

            Mod.instance.spellRegister.Add(new(Game1.player, IconData.ritecircles.druid) { soundTrigger = SoundHandle.SoundCue.DruidHorn });

            ExitPreamble();

            return true;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            // add false trigger

            EventClicks(actionButtons.rite);

            EventRender triggerField = new(eventId, location.Name, origin, IconData.displays.druid);

            eventRenders.Add("trigger",triggerField);

            // add voice

            AddActor(0, origin - new Vector2(24, 80));

        }

        public override void EventInterval()
        {

            activeCounter++;

            switch (activeScene)
            {

                case 0:

                    ScenePreamble();

                    break;

                case 1:

                    SceneEffigyEnter();

                    break;

                case 2:

                    SceneEffigy();
                    
                    break;

                case 3:

                    SceneEffigyTwo();

                    break;

                case 4:

                    SceneExit();

                    break;

            }

        }

        public void ScenePreamble()
        {

            switch (activeCounter)
            {


                case 1:

                    DialogueCue(1);

                    break;

                case 4:

                    DialogueCue(2);

                    break;

                case 7:

                    DialogueCue(3);

                    break;

                case 10:

                    DialogueCue(4);

                    break;

                case 13:

                    DialogueCue(5);

                    break;

                case 16:

                    DialogueCue(900);

                    break;

                case 19:

                    activeCounter = 0;

                    break;

            }

        }

        public void ExitPreamble()
        {

            activeScene = 1;

            activeCounter = 0;

            eventRenders.Remove("trigger");

            RemoveClicks();

        }

        public void SceneEffigyEnter()
        {

            switch (activeCounter)
            {

                case 1:

                RemoveActors();

                SceneBar(eventTitle, 0);

                Vector2 EffigyPosition = origin + new Vector2(512, -512);

                LoadCompanion(CharacterHandle.characters.Effigy, EffigyPosition, 0, 1, false);

                companions[0].TargetEvent(100, Game1.player.Position + new Vector2(128, 0), true);

                companions[0].SetDash(Game1.player.Position + new Vector2(128, 0));

                companions[0].netLayer.Set(10000);

                DialogueCue(6);

                break;

            }

        }

        public void SceneEffigy()
        {

            switch (activeCounter)
            {
                case 1:

                    DialogueCueWithFeeling(7, 0, Character.Character.specials.gesture);

                    break;

                case 4:

                    DialogueLoad(0, 1);

                    break;

                case 6:

                    DialogueNext(companions[0]);

                    break;

                case 10:

                    NextScene();

                    break;

            }

        }

        public void SceneEffigyTwo()
        {

            switch (activeCounter)
            {

                case 1:

                    DialogueLoad(0, 2);

                    break;

                case 6:

                    DialogueNext(companions[0]);

                    break;

                case 10:

                    NextScene();

                    break;

            }

        }

        public void SceneExit()
        {

            switch (activeCounter)
            {

                case 1:

                    companions[0].TargetEvent(0, eventVectors[1]);

                    DialogueCue(8);

                    break;

                case 3:

                    eventComplete = true;

                    break;

            }

        }

        public override void SkipEvent(int index = -1)
        {

            switch (activeScene)
            {
                case 0:

                    ExitPreamble();

                    break;

                case 1:
                case 2:
                case 3:


                    SetDialogueChoices(1, 2);

                    Vector2 warpScene3 = ModUtility.PositionToTile(origin);

                    Mod.instance.WarpAllFarmers(location.Name, (int)warpScene3.X - 1, (int)warpScene3.Y, 1);
                    
                    LoadCompanion(CharacterHandle.characters.Effigy, origin + new Vector2(64, 0), 0, 1, false);

                    activeScene = 3;

                    activeCounter = 0;

                    break;

                case 4:

                    eventComplete = true;

                    break;

            }

        }

        public override void EventScene(int index)
        {
            switch (index)
            {
                case 100:

                    companions[0].LookAtTarget(Game1.player.Position,true);

                    companions[0].netSpecial.Set((int)Character.Character.specials.pickup);

                    companions[0].specialTimer = 30;

                    companions[0].netLayer.Set(0);

                    SpellHandle explosion = new(companions[0].Position, 256, IconData.impacts.impact, new());

                    Mod.instance.spellRegister.Add(explosion);

                    NextScene();

                    break;


            }
        }

    }

}