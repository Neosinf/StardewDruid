using Microsoft.Xna.Framework;
using StardewValley.BellsAndWhistles;
using StardewValley.Locations;
using StardewValley;
using System;
using System.Collections.Generic;
using StardewDruid.Data;
using System.Diagnostics.Metrics;
using StardewDruid.Handle;

namespace StardewDruid.Cast.Weald
{
    public class Clearance
    {

        float damageLevel;

        int powerLevel;

        public Clearance()
        {
            
            damageLevel = (int)(Mod.instance.CombatDamage() * 0.1);

            powerLevel = Mod.instance.PowerLevel;

        }

        public void CastActivate(Vector2 target, bool sound = true)
        {

            int radius = 2 + powerLevel;

            SpellHandle explode = new(Game1.player, target * 64, radius * 64, damageLevel)
            {
                type = SpellHandle.Spells.explode
            };

            if (sound)
            {
                
                explode.sound = SpellHandle.Sounds.flameSpellHit;
            
            }

            explode.display = IconData.impacts.puff;

            explode.displayRadius = 2;

            explode.indicator = IconData.cursors.weald;

            explode.factor = 3;

            explode.power = 2;

            explode.explosion = radius;

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.wealdOne))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.wealdOne, 1);

            }

            Mod.instance.spellRegister.Add(explode);

        }

    }

}
