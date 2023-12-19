using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Map;
using StardewValley;
using System.Collections.Generic;

namespace StardewDruid.Event.Scene
{
    public class Scythe : EventHandle
    {
        private Quest questData;

        public Scythe(Vector2 target, Rite rite, Quest quest)
          : base(target, rite)
        {
            this.questData = quest;
        }

        public override void EventTrigger()
        {
            Mod.instance.CompleteQuest("swordFates");
            Jester character = Mod.instance.characters["Jester"] as Jester;
            Mod.instance.RegisterEvent((EventHandle)this, "scene");
            this.expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 20.0;
        }

        public override void EventRemove()
        {
            Jester character = Mod.instance.characters["Jester"] as Jester;
            ModUtility.AnimateQuickWarp(character.currentLocation, character.Position - new Vector2(0.0f, 32f), "Solar");
            character.SwitchDefaultMode();
            character.WarpToDefault();
            base.EventRemove();
        }

        public override bool EventActive()
        {
            return targetPlayer.currentLocation == this.targetLocation && this.expireTime >= Game1.currentGameTime.TotalGameTime.TotalSeconds;
        }

        public override void EventInterval()
        {
            activeCounter++;
            Jester character = Mod.instance.characters["Jester"] as Jester;
            if (this.activeCounter == 1)
            {
                if (character.currentLocation.Name != this.riteData.castLocation.Name)
                {
                    character.Halt();
                    character.currentLocation.characters.Remove((NPC)character);
                    character.currentLocation = this.riteData.castLocation;
                    character.currentLocation.characters.Add((NPC)character);
                }
                character.Position = (questData.triggerVector* 64f)*new Vector2(-128f, 128f);
                ModUtility.AnimateQuickWarp(this.riteData.castLocation,character.Position - new Vector2(0.0f, 32f), "Solar");
                character.SwitchEventMode();
                character.eventVectors.Add(character.Position+ new Vector2(0.0f,-128f));
                character.timers["busy"] = 1000;
                Mod.instance.dialogue["Jester"].specialDialogue.Add("Thanatoshi", new List<string>()
                {
                  "I hope he found peace...",
                  "Grim. Dark. I Love this place.",
                  "Do you know this figure?",
                  "I have a strange foreboding about this."
                });
            }
            if (activeCounter != 2)
            {

                return;
            }
            character.showTextAboveHead("so Thanatoshi was here.", -1, 2, 3000, 0);
        }
    }
}