
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using StardewDruid.Render;
using StardewDruid.Data;

namespace StardewDruid.Character
{
    public class Wolf : StardewDruid.Character.Character
    {

        public WolfRender wolfRender;

        public Wolf()
        {
        }

        public Wolf(CharacterHandle.characters type = CharacterHandle.characters.GreyWolf)
          : base(type)
        {

        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.CharacterType(Name);

            }

            characterTexture = CharacterHandle.CharacterTexture(characterType);

            wolfRender = new(characterType.ToString());

            LoadIntervals();

            overhead = 112;

            setScale = 3.75f;

            gait = 2.5f;

            modeActive = mode.random;

            idleFrames = new()
            {
                [idles.idle] = wolfRender.idleFrames,
            }; 

            walkFrames = wolfRender.walkFrames;

            dashFrames = new()
            {
                [dashes.dash] = wolfRender.dashFrames,
                [dashes.smash] = wolfRender.dashFrames,
            };

            specialFrames = new()
            {
                [specials.special] = wolfRender.sweepFrames,
                [specials.sweep] = wolfRender.sweepFrames,
            };

            specialIntervals = new()
            {
                [specials.special] = 12,
                [specials.sweep] = 9,
            };

            specialFloors = new()
            {
                [specials.special] = 0,
                [specials.sweep] = 0,
            };

            specialCeilings = new()
            {
                [specials.special] = 0,
                [specials.sweep] = 5,
            };

            loadedOut = true;

        }

        public override bool SpriteAngle()
        {

            return SpriteFlip();

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128) || characterTexture == null)
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            WolfRenderAdditional additional = new();

            additional.layer = ((float)StandingPixel.Y + (float)LayerOffset()) / 10000f;

            additional.scale = setScale;

            additional.position = SpritePosition(localPosition);

            additional.flip = netDirection.Value == 3 || netDirection.Value % 2 == 0 && netAlternative.Value == 3;

            additional.fade = fadeOut == 0 ? 1f : fadeOut;

            DrawEmote(b);

            if (netSpecial.Value != 0)
            {

                specials useSpecial = (specials)netSpecial.Value;

                if (!specialFrames.ContainsKey(useSpecial))
                {

                    useSpecial = specials.none;

                }

                additional.direction = netDirection.Value;

                additional.frame = specialFrame;

                additional.mode = WolfRenderAdditional.wolfmode.growl;

                switch (useSpecial)
                {

                    case specials.none:

                        break;

                    case specials.sweep:

                        additional.series = WolfRenderAdditional.wolfseries.sweep;

                        wolfRender.DrawNormal(b, additional);

                        return;

                    default:

                        additional.series = WolfRenderAdditional.wolfseries.special;

                        wolfRender.DrawNormal(b, additional);

                        return;

                }

            }

            if (netDash.Value != 0)
            {

                int dashSeries = netDirection.Value + (netDashProgress.Value * 4);

                int dashSetto = Math.Min(dashFrame, (dashFrames[(dashes)netDash.Value][dashSeries].Count - 1));

                additional.series = WolfRenderAdditional.wolfseries.dash;

                additional.direction = dashSeries;

                additional.frame = dashSetto;

                wolfRender.DrawNormal(b, additional);

                return;

            }

            if((idles)netIdle.Value == idles.daze)
            {
                b.Draw(
                    Mod.instance.iconData.displayTexture,
                    additional.position - new Vector2(0, overhead == 0 ? 144 : overhead),
                    IconData.DisplayRectangle(IconData.displays.glare),
                    Color.White,
                    0f,
                    new Vector2(8),
                    additional.scale,
                    SpriteEffects.None,
                    additional.layer
                );

            }

            additional.direction = netDirection.Value;

            additional.frame = moveFrame;

            wolfRender.DrawNormal(b, additional);


        }

    }

}
