using Microsoft.Xna.Framework;
using StardewValley.BellsAndWhistles;
using StardewValley.Locations;
using StardewValley;
using System;
using System.Collections.Generic;
using StardewDruid.Data;
using StardewDruid.Journal;

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

            SpellHandle explode = new(Game1.player, target * 64, radius * 64, damageLevel);

            explode.type = SpellHandle.spells.explode;

            if (sound)
            {
                
                explode.sound = SpellHandle.sounds.flameSpellHit;
            
            }

            explode.display = IconData.impacts.puff;

            explode.indicator = IconData.cursors.weald;

            explode.projectile = 3;

            explode.power = 2;

            explode.explosion = radius;

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.wealdOne))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.wealdOne, 1);

            }

            Mod.instance.spellRegister.Add(explode);

            Mod.instance.rite.castCost += 1;

        }

    }

}
