using Microsoft.Xna.Framework;
using StardewValley;
using System;

namespace StardewDruid.Cast.Fates
{
    public class Gravity : CastHandle
    {

        public int type;

        public Gravity(Vector2 target,  int Type = 0)
            : base(target)
        {

            int castCombat = Game1.player.CombatLevel / 2;

            castCost = Math.Max(6, 12 - castCombat);

            type = Type;

        }

        public override void CastEffect()
        {

            if (!Mod.instance.rite.castTask.ContainsKey("masterGravity"))
            {

                Mod.instance.UpdateTask("lessonGravity", 1);

            }

            GravityEvent gravityEvent = new(targetVector, type, Mod.instance.DamageLevel());

            gravityEvent.EventTrigger();

            castFire = true;

        }

    }

}
