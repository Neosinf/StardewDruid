using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using StardewDruid.Render;

namespace StardewDruid.Character
{
    public class Serpent: StardewDruid.Character.Character
    {

        public int hoverHeight;

        public int hoverInterval;

        public int hoverIncrements;

        public float hoverElevate;

        public int hoverFrame;

        public SerpentRender serpentRender;

        public Serpent()
        {
        }

        public Serpent(CharacterHandle.characters type = CharacterHandle.characters.Serpent)
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

            serpentRender = new(characterType.ToString());

            LoadIntervals();

            setScale = 4f;

            overhead = 112;

            gait = 1.8f;

            hoverInterval = 24;

            hoverIncrements = 2;

            hoverElevate = 0.75f;

            modeActive = mode.random;

            idleFrames[idles.idle] = serpentRender.walkFrames;

            walkFrames = serpentRender.walkFrames;

            specialFrames[specials.special] = serpentRender.specialFrames[specials.special];

            specialFrames[specials.sweep] = serpentRender.specialFrames[specials.special];

            dashFrames[dashes.dash] = serpentRender.dashFrames[dashes.dash];

            dashFrames[dashes.smash] = serpentRender.dashFrames[dashes.dash];

            loadedOut = true;

        }

        public override void draw(SpriteBatch b, float alpha = -1f)
        {
            
        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {

            base.drawAboveAlwaysFrontLayer(b);

            if (IsInvisible || !Utility.isOnScreen(Position, 128) || characterTexture == null)
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            SerpentRenderAdditional hoverAdditional = new();

            hoverAdditional.scale = setScale;

            hoverAdditional.position = SpritePosition(localPosition);

            hoverAdditional.layer = (float)StandingPixel.Y / 10000f + 0.001f;

            hoverAdditional.flip = SpriteFlip();

            hoverAdditional.fade = fadeOut == 0 ? 1f : fadeOut;

            hoverAdditional.direction = netDirection.Value;

            hoverAdditional.frame = hoverFrame;

            hoverAdditional.series = SerpentRenderAdditional.serpentseries.hover;

            DrawEmote(b);

            if (netDash.Value != 0)
            {

                hoverAdditional.direction = netDirection.Value + (netDashProgress.Value * 4);

                hoverAdditional.frame = Math.Min(dashFrame, (dashFrames[(dashes)netDash.Value][hoverAdditional.direction].Count - 1));

                hoverAdditional.series = SerpentRenderAdditional.serpentseries.dash;

            }
            else if (netSpecial.Value != 0)
            {

                hoverAdditional.frame = specialFrame;

                hoverAdditional.series = SerpentRenderAdditional.serpentseries.special;

            }

            if((movements)netMovement.Value == movements.run)
            {

                hoverAdditional.series = SerpentRenderAdditional.serpentseries.dash;

                if(hoverAdditional.frame == 3)
                {

                    hoverAdditional.frame = 1;

                }

            }

            serpentRender.DrawNormal(b,hoverAdditional);


        }

        public override int GetWidth()
        {

            return 64;

        }


        public override Vector2 SpritePosition(Vector2 localPosition)
        {
            
            Vector2 spritePosition = base.SpritePosition(localPosition);

            if (hoverInterval > 0)
            {

                spritePosition.Y -= (float)Math.Abs(hoverHeight) * hoverElevate;

            }

            return spritePosition;


        }

        public override void update(GameTime time, GameLocation location)
        {
            
            base.update(time, location);

            hoverHeight++;

            int heightLimit = (hoverIncrements * hoverInterval);

            if (hoverHeight > heightLimit)
            {
                hoverHeight -= (heightLimit * 2);
            }

            if (Math.Abs(hoverHeight) % hoverInterval == 0)
            {

                hoverFrame++;

                if (hoverFrame >= walkFrames[0].Count)
                {

                    hoverFrame = 0;

                }

            }

        }

        public override bool SweepAttack(StardewValley.Monsters.Monster monster)
        {

            return SmashAttack(monster);

        }

    }

}
