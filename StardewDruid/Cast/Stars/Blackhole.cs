using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.Minecarts;
using StardewValley.Locations;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using xTile.Tiles;
using static StardewDruid.Data.IconData;

namespace StardewDruid.Cast.Stars
{
    public class Blackhole : EventHandle
    {

        public Vector2 target;

        public bool blackhole;

        public bool meteor;

        public bool water;

        public Blackhole()
        {
            
        }

        public override bool EventActive()
        {

            if (!inabsentia && !eventLocked)
            {

                if (Mod.instance.Config.riteButtons.GetState() != SButtonState.Held)
                {

                    return false;

                }

                if (Vector2.Distance(origin, Game1.player.Position) > 32)
                {

                    return false;

                }

            }
            
            return base.EventActive();

        }

        public override void EventDecimal()
        {

            if (!EventActive())
            {

                eventComplete = true;

                return;

            }

            decimalCounter++;

            if (!inabsentia && !eventLocked)
            {

                if (decimalCounter == 3)
                {

                    Mod.instance.rite.channel(IconData.skies.night,60);

                    channel = IconData.skies.night;

                }

                if (decimalCounter == 11)
                {
                    
                    decimalCounter = 0;

                    eventLocked = true;

                    if(ModUtility.GroundCheck(location, ModUtility.PositionToTile(target)) == "water"){

                        water = true;
                        meteor = true;
                    }

                }

                return;

            }

            if(decimalCounter == 1)
            {

                GravityWell();

            }

            if(decimalCounter == 30)
            {

                eventComplete = true;

            }

        }

        public void GravityWell()
        {

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.starsTwo))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.starsTwo, 1);
            }

            SpellHandle hole = new(Game1.player, target, 5 * 64, Mod.instance.CombatDamage() * 4);

            hole.type = SpellHandle.spells.blackhole;

            if (water)
            {

                hole.added = new() { SpellHandle.effects.tornado, };

            }
            else if (Game1.player.CurrentTool is not MeleeWeapon || Game1.player.CurrentTool.isScythe())
            {

                hole.added = new() { SpellHandle.effects.harvest, };

            }
            else
            {

                hole.added = new() { SpellHandle.effects.gravity, };

            }

            Mod.instance.spellRegister.Add(hole);

            blackhole = true;

            Mod.instance.rite.castCost = 48;

            Mod.instance.rite.ApplyCost();

        }

    }

}
