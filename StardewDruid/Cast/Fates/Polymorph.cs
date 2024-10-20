using Microsoft.Xna.Framework;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Data;
using StardewDruid.Event;
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

                    companions[0] = new StardewDruid.Character.Growler(Character.CharacterHandle.characters.BrownBear);

                    location.playSound("BearGrowl");

                    break;               
                
                case 1:

                    companions[0] = new StardewDruid.Character.Growler(Character.CharacterHandle.characters.BlackBear);

                    location.playSound("BearGrowlTwo");

                    break;

                case 2:

                    companions[0] = new StardewDruid.Character.Critter(Character.CharacterHandle.characters.BlackCat);

                    location.playSound(SpellHandle.sounds.cat.ToString());

                    break;

                case 3:

                    companions[0] = new StardewDruid.Character.Critter(Character.CharacterHandle.characters.TabbyCat);

                    location.playSound(SpellHandle.sounds.cat.ToString());

                    break;

                case 4:

                    companions[0] = new StardewDruid.Character.Critter(Character.CharacterHandle.characters.RedFox);

                    location.playSound(SpellHandle.sounds.dog_bark.ToString());

                    break;

                case 5:

                    companions[0] = new StardewDruid.Character.Barker(Character.CharacterHandle.characters.GreyWolf);

                    location.playSound(SpellHandle.sounds.dog_bark.ToString());

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
