using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;


namespace StardewDruid.Event.Scene
{
    public class ApproachEffigy : EventHandle
    {

        public ApproachEffigy()
        {

        }

        public override void TriggerInterval()
        {

            base.TriggerInterval();

            int actionCycle = triggerCounter % 20;

            if (actors.Count == 0)
            {

                AddActor(0, origin - new Vector2(48, 48));

            }

            switch (actionCycle)
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

            }

        }

        public override void EventActivate()
        {

            TriggerRemove();

            eventActive = true;

            locales = new() { Location.LocationHandle.druid_grove_name, Game1.player.currentLocation.Name, };

            location = Game1.player.currentLocation;

            Mod.instance.RegisterEvent(this, eventId, true);

            activeLimit = eventCounter + 302;

            Mod.instance.spellRegister.Add(new(Game1.player.Position, 384, IconData.impacts.supree, new()) { sound = SpellHandle.sounds.discoverMineral, });

            Mod.instance.rite.CastRockfall(true);
            Mod.instance.rite.CastRockfall(true);
            Mod.instance.rite.CastRockfall(true);
            Mod.instance.rite.CastRockfall(true);
            Mod.instance.rite.CastRockfall(true);
            Mod.instance.rite.CastRockfall(true);

        }

        public override void EventInterval()
        {

            if(Game1.activeClickableMenu != null)
            {

                activeLimit += 1;

                return;

            }

            activeCounter++;

            switch (activeCounter)
            {

                // ------------------------------------------
                // 1 Beginning
                // ------------------------------------------

                case 1:

                    Vector2 EffigyPosition = origin + new Vector2(128, -640);

                    TemporaryAnimatedSprite EffigyAnimation = new(0, 2000f, 1, 1, EffigyPosition - new Vector2(36, 72), false, false)
                    {

                        sourceRect = new(0, 0, 32, 32),

                        texture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Effigy),

                        scale = 4.25f,

                        motion = new Vector2(0, 0.32f),

                        rotationChange = 0.1f,

                        timeBasedMotion = true,

                    };

                    location.temporarySprites.Add(EffigyAnimation);

                    break;

                case 3:

                    RemoveActors();

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Effigy, StardewDruid.Character.Character.mode.scene);

                    companions.Add(0,Mod.instance.characters[CharacterHandle.characters.Effigy]);

                    companions[0].ResetActives();

                    companions[0].currentLocation = location;

                    companions[0].Position = origin + new Vector2(64,0);

                    companions[0].currentLocation.characters.Add(companions[0]);

                    companions[0].LookAtTarget(Game1.player.Position);

                    companions[0].eventName = eventId;

                    voices[1] = companions[0];

                    DialogueCue(6);

                    Mod.instance.iconData.ImpactIndicator(location, origin + new Vector2(64, 0), IconData.impacts.impact, 6f, new());

                    location.playSound("explosion");

                    break;

                case 4:

                    DialogueLoad(0, 1);

                    break;

                case 6:

                    DialogueNext(companions[0]);

                    break;

                case 10:

                    activeCounter = 100;

                    break;

                case 101:

                    DialogueLoad(0, 2);

                    break;

                case 106:

                    DialogueNext(companions[0]);

                    break;

                case 110:

                    activeCounter = 200;

                    break;

                case 201:

                    companions[0].TargetEvent(0, companions[0].Position + new Vector2(0, 192));

                    break;

                case 203:

                    eventComplete = true;

                    break;

            }

        }

    }

}