using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Constants;
using StardewValley.Locations;
using StardewValley.Network;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Timers;

namespace StardewDruid.Cast.Mists
{
    public class Wrath : EventHandle
    {

        public Wrath()
            : base()
        {

            activeLimit = -1;

        }

        public override bool EventActive()
        {

            if (!Mod.instance.RiteButtonHeld())
            {

                return false;

            }

            return base.EventActive();

        }

        public override void EventDecimal()
        {

            if (!EventActive())
            {

                RemoveAnimations();

                eventComplete = true;

                return;

            }

            decimalCounter++;

            if (decimalCounter < 7)
            {

                return;

            }

            if (decimalCounter == 7)
            {

                Game1.flashAlpha = 1f;

                location.playSound("thunder");

            }

            if(decimalCounter % 2 == 0)
            {

                Vector2 origin = Rite.GetTargetCursor(Game1.player.FacingDirection, 1280, -1);

                SpellHandle bolt = new(origin, 64 * Mod.instance.randomIndex.Next(2,5), IconData.impacts.none, new());

                bolt.type = SpellHandle.spells.bolt;

                bolt.factor =3;

                bolt.sound = SpellHandle.sounds.silent;

                bolt.display = IconData.impacts.sparknode;

                bolt.damageMonsters = Mod.instance.CombatDamage() /2;

                List<float> crits = Mod.instance.CombatCritical();

                bolt.critical = crits[0] + 0.2f;

                bolt.criticalModifier = crits[1];

                if (decimalCounter % 6 == 0)
                {

                    bolt.factor =4;

                    bolt.sound = SpellHandle.sounds.flameSpellHit;

                    bolt.damageMonsters *= 3;

                }

                if (decimalCounter % 12 == 0)
                {

                    bolt.sound = SpellHandle.sounds.flameSpellHit;

                }

                Mod.instance.spellRegister.Add(bolt);

                int tryCost = 18 - Game1.player.CombatLevel;

                Mod.instance.rite.castCost += tryCost < 8 ? 8 : tryCost;

                Mod.instance.rite.ApplyCost();

            }

        }

    }

}
