
using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Minigames;
using System;
using System.Collections.Generic;

namespace StardewDruid.Event.Relics
{
    internal class RelicAlchemistry: EventHandle
    {

        public RelicAlchemistry()
        {

        }

        public override void EventInterval()
        {

            activeCounter++;

            switch (activeCounter)
            {

                case 1:
            
                    companions[0] = new Flyer(CharacterHandle.characters.Macarbi);

                    CharacterMover.Warp(Game1.player.currentLocation, companions[0], new Vector2(1280, 0), false);

                    companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

                    companions[0].eventName = eventId;

                    companions[0].TargetEvent(100, Game1.player.Position - new Vector2(128,0), true);

                    activeCounter++;

                    break;

                case 99:

                    ThrowHandle dropRune = new(Game1.player, Game1.player.Position - new Vector2(64,480), IconData.relics.runestones_alchemistry);

                    dropRune.register();

                    eventComplete = true;

                    break;

                case 101:

                    Game1.player.completelyStopAnimatingOrDoingAction();

                    Game1.player.freezePause = 500;

                    Game1.player.doEmote(8);

                    Game1.player.faceDirection(3);

                    location.playSound(SpellHandle.Sounds.owl.ToString());

                    companions[0].ResetActives();

                    companions[0].LookAtTarget(Game1.player.Position, true);

                    companions[0].netIdle.Set((int)Character.Character.idles.standby);

                    break;

                case 102:

                    EventRender runestone = new("runestones_alchemistry", location.Name, companions[0].Position + new Vector2(64, -32), IconData.relics.runestones_alchemistry)
                    {
                        scale = 3f
                    };

                    runestone.layer += 0.0064f;

                    eventRenders.Add("runestones_alchemistry", runestone);

                    break;

                case 104:

                    companions[0].doEmote(32);

                    eventRenders.Clear();

                    ThrowHandle giveRune = new(Game1.player, companions[0].Position, IconData.relics.runestones_alchemistry);

                    giveRune.Inventorise();

                    break;

                case 105:

                    companions[0].ResetActives();

                    companions[0].TargetEvent(200, new Vector2(1280, 0), true);

                    break;

                case 115:

                    eventComplete = true;

                    break;

            }

        }

        public override void EventScene(int index)
        {
            switch (index)
            {
                case 100:

                    if (Vector2.Distance(Game1.player.Position,companions[0].Position) <= 192)
                    {

                        activeCounter = 100;

                        break;

                    }

                    companions[0].TargetEvent(100, Game1.player.Position - new Vector2(128, 0), true);

                    break;

                case 200:

                    RemoveActors();

                    break;

            }

        }

    }

}