using Microsoft.Xna.Framework;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using static StardewDruid.Cast.SpellHandle;
using static StardewDruid.Data.IconData;
using static StardewValley.Minigames.TargetGame;

namespace StardewDruid.Cast.Fates
{
    public class Polymorph : EventHandle
    {
        public NPC npc;

        public bool invis;

        public Polymorph(NPC witness, bool Invis)
        {

            npc = witness;

            location = npc.currentLocation;

            eventId = "polymorph_" + witness.Name;

            invis = Invis;

            if (Invis)
            {

                npc.IsInvisible = true;

            }

            activeLimit = 5;

            ChooseCreature();

        }

        public void ChooseCreature()
        {

            switch (Mod.instance.randomIndex.Next(6))
            {

                case 0:

                    companions[0] = new StardewDruid.Character.Bear(CharacterHandle.characters.BrownBear);

                    Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.BearGrowl);

                    break;               
                
                case 1:

                    companions[0] = new StardewDruid.Character.Bear(CharacterHandle.characters.BlackBear);

                    Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.BearRoar);

                    break;

                case 2:

                    companions[0] = new StardewDruid.Character.Critter(CharacterHandle.characters.BlackCat);

                    location.playSound(SpellHandle.Sounds.cat.ToString());

                    break;

                case 3:

                    companions[0] = new StardewDruid.Character.Critter(CharacterHandle.characters.TabbyCat);

                    location.playSound(SpellHandle.Sounds.cat.ToString());

                    break;

                case 4:

                    companions[0] = new StardewDruid.Character.Critter(CharacterHandle.characters.RedFox);

                    location.playSound(SpellHandle.Sounds.dog_bark.ToString());

                    break;

                case 5:

                    companions[0] = new StardewDruid.Character.Wolf(CharacterHandle.characters.BrownWolf);

                    location.playSound(SpellHandle.Sounds.dog_bark.ToString());

                    break;


            }

            companions[0].currentLocation = location;

            companions[0].Position = npc.Position;

            location.characters.Add(companions[0]);

            companions[0].SwitchToMode(Character.Character.mode.scene, Game1.player);

            companions[0].eventName = eventId;

            companions[0].LookAtTarget(Game1.player.Position,true);

        }

        public override void EventRemove()
        {

            if (invis)
            {

                npc.IsInvisible = false;

            }

            base.EventRemove();

        }

        public override void EventDecimal()
        {

            if (!EventActive())
            {

                return;

            }

            decimalCounter++;

            switch (decimalCounter)
            {

                case 10:

                    companions[0].netDirection.Set((companions[0].netDirection.Value + 2) % 4);

                    companions[0].netAlternative.Set((companions[0].netAlternative.Value + 2) % 4);

                    break;

                case 20:
                    companions[0].netDirection.Set((companions[0].netDirection.Value + 1) % 4);

                    companions[0].netAlternative.Set((companions[0].netAlternative.Value + 2) % 4);

                    break;

                case 30:

                    eventComplete = true;

                    break;

            }

        }

    }

}
