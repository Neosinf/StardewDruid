using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
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

            

            int tryCost = 18 - Game1.player.CombatLevel;

            costCounter = tryCost < 8 ? 8 : tryCost;

        }

        public override void EventDecimal()
        {
            
            if (!Mod.instance.RiteButtonHeld())
            {

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

            if(Game1.player.Stamina <= (costCounter * 2))
            {

                eventComplete = true;

                return;

            }

            if(decimalCounter % 2 == 0)
            {

                Vector2 origin = Rite.GetTargetCursor(Game1.player.FacingDirection, 1280, -1);

                SpellHandle bolt = new(origin, 64 * Mod.instance.randomIndex.Next(2, 5), IconData.impacts.none, new())
                {
                    type = SpellHandle.Spells.quickbolt,

                    displayFactor = 3,

                    sound = SpellHandle.Sounds.silent,

                    display = IconData.impacts.sparknode,

                    damageMonsters = Mod.instance.CombatDamage() / 2
                };

                List<float> crits = Mod.instance.CombatCritical();

                bolt.critical = crits[0] + 0.2f;

                bolt.criticalModifier = crits[1];

                if (decimalCounter % 6 == 0)
                {

                    bolt.displayFactor =4;

                    bolt.sound = SpellHandle.Sounds.flameSpellHit;

                    bolt.damageMonsters *= 3;

                }

                if (decimalCounter % 12 == 0)
                {

                    bolt.sound = SpellHandle.Sounds.flameSpellHit;

                }


                if (Mod.instance.apothecaryHandle.buff.applied.ContainsKey(BuffHandle.buffTypes.spellcatch))
                {

                    if (Mod.instance.apothecaryHandle.buff.applied.ContainsKey(BuffHandle.buffTypes.capture))
                    {

                        bolt.added.Add(SpellHandle.Effects.capture);

                    }

                }

                Mod.instance.spellRegister.Add(bolt);

                Rite.ApplyCost(costCounter);

            }

        }

    }

}
