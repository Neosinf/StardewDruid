using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using StardewDruid.Render;
using StardewDruid.Handle;

namespace StardewDruid.Character
{
    public class Bat : StardewDruid.Character.Character
    {

        public BatRender hoverRender;

        public Bat()
        {
        }

        public Bat(CharacterHandle.characters type = CharacterHandle.characters.Bat)
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

            hoverRender = new(characterType);

            LoadIntervals();

            setScale = 4f;

            gait = 1.8f;

            modeActive = mode.random;

            idleFrames[idles.idle] = hoverRender.walkFrames;

            idleFrames[idles.rest] = hoverRender.restFrames;

            walkFrames = hoverRender.walkFrames;

            specialFrames[specials.special] = hoverRender.specialFrames[specials.special];

            specialFrames[specials.sweep] = hoverRender.specialFrames[specials.special];

            dashFrames[dashes.dash] = hoverRender.dashFrames[dashes.dash];

            dashFrames[dashes.smash] = hoverRender.dashFrames[dashes.dash];

            loadedOut = true;

        }

        public override void draw(SpriteBatch b, float alpha = -1f)
        {
            
        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128) || characterTexture == null)
            {
                return;
            }

            DrawEmote(b);

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            Vector2 usePosition = SpritePosition(localPosition);

            DrawCharacter(b, usePosition);

        }

        public override void DrawCharacter(SpriteBatch b, Vector2 spritePosition)
        {

            BatRenderAdditional hoverAdditional = new()
            {
                scale = setScale,

                layer = (float)StandingPixel.Y / 10000f + 0.001f,

                flip = SpriteFlip(),

                fade = fadeOut,

                direction = netDirection.Value,

                series = BatRenderAdditional.batseries.none,

                position = spritePosition,

            };

            if (netDash.Value != 0)
            {

                hoverAdditional.direction = netDirection.Value + (netDashProgress.Value * 4);

                hoverAdditional.frame = Math.Min(dashFrame, (dashFrames[(dashes)netDash.Value][hoverAdditional.direction].Count - 1));

                hoverAdditional.series = BatRenderAdditional.batseries.dash;

            }
            else if (netSpecial.Value != 0)
            {

                hoverAdditional.series = BatRenderAdditional.batseries.special;

            }
            else if (netIdle.Value == (int)idles.rest)
            {

                hoverAdditional.series = BatRenderAdditional.batseries.rest;

                hoverAdditional.frame = IdleFrame(idles.rest);

            }

            hoverRender.DrawNormal(b,hoverAdditional);

        }


        public override void normalUpdate(GameTime time, GameLocation location)
        {

            base.normalUpdate(time, location);

            hoverRender.Update(pathActive == pathing.none);

        }

        public override bool SweepAttack(StardewValley.Monsters.Monster monster)
        {

            return SmashAttack(monster);

        }
        public override Microsoft.Xna.Framework.Rectangle OverheadPortrait()
        {

            return new Rectangle(14, 5, 16, 16);

        }

    }

}
