using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewValley.Buffs;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Cast.Effect
{
    public class Shield : EventHandle
    {

        public Shield()
            : base()
        {

        }

        public override void EventActivate()
        {
            base.EventActivate();

            BuffEffects buffEffect = new();

            buffEffect.Defense.Set(99);

            Buff shieldBuff = new(
                Rite.buffIdShield,
                source: StringData.Strings(StringData.stringkeys.druidShield),
                displaySource: StringData.RiteNames(Rite.Rites.stars),
                duration: 3000,
                displayName: StringData.Strings(StringData.stringkeys.druidShield),
                description: StringData.Strings(StringData.stringkeys.defenseIncrease),
                effects: buffEffect);

            Game1.player.buffs.Apply(shieldBuff);

        }

        public override void EventDraw(SpriteBatch b)
        {

            if (!Game1.player.hasBuff(Rite.buffIdShield))
            {

                return;

            }

            Vector2 spritePosition = Game1.GlobalToLocal(Game1.player.Position);

            float drawLayer = Game1.player.StandingPixel.Y / 10000f;

            b.Draw(
                Mod.instance.iconData.shieldTexture,
                spritePosition + new Vector2(32,0),
                new Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48),
                Color.LightGoldenrodYellow * 0.2f,
                0f,
                new Vector2(24),
                5f,
                0,
                drawLayer + 0.0004f
            );

            int sparkle = (int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 1000) / 200;

            b.Draw(
                Mod.instance.iconData.shieldTexture,
                spritePosition + new Vector2(32, 0),
                new Microsoft.Xna.Framework.Rectangle(48 + (48 * sparkle), 0, 48, 48),
                Color.White * 0.6f,
                0f,
                new Vector2(24),
                4f,
                0,
                drawLayer + 0.0005f
            );

        }

        public override void EventRemove()
        {
            
            base.EventRemove();

            Game1.player.buffs.Remove(Rite.buffIdShield);

        }

        public override void EventInterval()
        {

            activeCounter++;

            if(activeCounter == 8)
            {

                eventComplete = true;

            }

        }

    }

}

