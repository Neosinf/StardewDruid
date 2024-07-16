using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;

namespace StardewDruid.Character
{
    public class Hoverer : StardewDruid.Character.Character
    {

        public int hoverHeight;

        public int hoverInterval;

        public int hoverIncrements;

        public float hoverElevate;

        public int hoverFrame;

        public Hoverer()
        {
        }

        public Hoverer(CharacterHandle.characters type = CharacterHandle.characters.Shadowbat)
          : base(type)
        {

        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = Enum.Parse<CharacterHandle.characters>(Name);

            }
            characterTexture = CharacterHandle.CharacterTexture(characterType);

            LoadIntervals();

            setScale = 2f;

            overhead = 112;

            gait = 1.8f;

            hoverInterval = 12;

            hoverIncrements = 2;

            hoverElevate = 1f;

            modeActive = mode.random;

            haltFrames = new()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 64, 32, 32),
                    new Rectangle(32, 64, 32, 32),
                    new Rectangle(64, 64, 32, 32),
                    new Rectangle(32, 64, 32, 32),
                },

                [1] = new List<Rectangle>()
                {
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                    new Rectangle(64, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),
                    new Rectangle(64, 0, 32, 32),
                    new Rectangle(32, 0, 32, 32),

                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(0, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),
                    new Rectangle(64, 32, 32, 32),
                    new Rectangle(32, 32, 32, 32),

                }
            };

            idleFrames = new(haltFrames);

            walkFrames = new(haltFrames);

            alertFrames = new(haltFrames);

            specialFrames = new()
            {

                [8] = new List<Rectangle>()
                {
                    new Rectangle(0, 160, 32, 32),
                    new Rectangle(32, 160, 32, 32),
                    new Rectangle(64, 160, 32, 32),
                    new Rectangle(32, 160, 32, 32),
                },
                [9] = new List<Rectangle>()
                {
                    new Rectangle(0, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),
                    new Rectangle(64, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),

                },
                [10] = new List<Rectangle>()
                {
                    new Rectangle(0, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),
                    new Rectangle(64, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),

                },
                [11] = new List<Rectangle>()
                {
                    new Rectangle(0, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),
                    new Rectangle(64, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),

                }

            };

            workFrames = new(specialFrames);

            sweepFrames = new(specialFrames);

            dashFrames = new Dictionary<int, List<Rectangle>>()
            {
                [0] = new List<Rectangle>()
                {
                    new Rectangle(0, 160, 32, 32),
                    new Rectangle(32, 160, 32, 32),
                    new Rectangle(64, 160, 32, 32),

                },

                [1] = new List<Rectangle>()
                {
                    new Rectangle(0, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),
                    new Rectangle(64, 128, 32, 32),

                },
                [2] = new List<Rectangle>()
                {
                    new Rectangle(0, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),
                    new Rectangle(64, 96, 32, 32),

                },
                [3] = new List<Rectangle>()
                {
                    new Rectangle(0, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),
                    new Rectangle(64, 128, 32, 32),

                },
                [4] = new List<Rectangle>()
                {
                    new Rectangle(32, 160, 32, 32),
                },
                [5] = new List<Rectangle>()
                {
                    new Rectangle(32, 128, 32, 32),
                },
                [6] = new List<Rectangle>()
                {
                    new Rectangle(32, 96, 32, 32),
                },
                [7] = new List<Rectangle>()
                {
                    new Rectangle(32, 128, 32, 32),
                },
                [8] = new List<Rectangle>()
                {
                    new Rectangle(0, 160, 32, 32),
                    new Rectangle(32, 160, 32, 32),
                    new Rectangle(64, 160, 32, 32),
                    new Rectangle(32, 160, 32, 32),
                },
                [9] = new List<Rectangle>()
                {
                    new Rectangle(0, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),
                    new Rectangle(64, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),

                },
                [10] = new List<Rectangle>()
                {
                    new Rectangle(0, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),
                    new Rectangle(64, 96, 32, 32),
                    new Rectangle(32, 96, 32, 32),

                },
                [11] = new List<Rectangle>()
                {
                    new Rectangle(0, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),
                    new Rectangle(64, 128, 32, 32),
                    new Rectangle(32, 128, 32, 32),

                }
            };

            smashFrames = new(dashFrames);

            loadedOut = true;

        }

        public override void draw(SpriteBatch b, float alpha = -1f)
        {
            
        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {

            base.drawAboveAlwaysFrontLayer(b);

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            Vector2 spritePosition = GetPosition(localPosition, setScale);

            float drawLayer = (float)StandingPixel.Y / 10000f + 0.001f;

            bool flippity = netDirection.Value == 3 || netDirection.Value % 2 == 0 && netAlternative.Value == 3;

            float fade = fadeOut == 0 ? 1f : fadeOut;

            DrawEmote(b);

            if (netDashActive.Value)
            {

                int setFlightSeries = netDirection.Value + (netDashProgress.Value * 4);

                int setFlightFrame = Math.Min(dashFrame, (dashFrames[setFlightSeries].Count - 1));

                b.Draw(
                    characterTexture,
                    spritePosition,
                    dashFrames[setFlightSeries][setFlightFrame],
                    Color.White * fade,
                    0,
                    Vector2.Zero,
                    setScale,
                    flippity ? (SpriteEffects)1 : 0,
                    drawLayer
                );

            }
            else if (netSpecialActive.Value)
            {

                b.Draw(
                    characterTexture,
                    spritePosition,
                    specialFrames[netDirection.Value][specialFrame],
                    Color.White * fade,
                    0,
                    Vector2.Zero,
                    setScale,
                    flippity ? (SpriteEffects)1 : 0,
                    drawLayer);

            }
            else
            {

                b.Draw(
                    characterTexture,
                    spritePosition,
                    idleFrames[netDirection.Value][hoverFrame],
                    Color.White * fade,
                    0,
                    Vector2.Zero,
                    setScale,
                    flippity ? (SpriteEffects)1 : 0,
                    drawLayer);

            }

            DrawShadow(b, localPosition, drawLayer);

        }

        public virtual Vector2 GetPosition(Vector2 localPosition, float spriteScale = -1f, bool shadow = false)
        {

            if (spriteScale == -1f)
            {
                spriteScale = setScale;

            }

            int width = idleFrames[0][0].Width;

            int height = idleFrames[0][0].Height;

            Vector2 spritePosition = localPosition + new Vector2(width, width * 2) - new Vector2(width / 2 * spriteScale, height * spriteScale);

            if (shadow)
            {

                spritePosition.Y += 6 * spriteScale;

                return spritePosition;

            }

            if (netDashActive.Value || netSmashActive.Value)
            {

                spritePosition.Y -= dashHeight;

            }
            else
            if (hoverInterval > 0)
            {

                spritePosition.Y -= (float)Math.Abs(hoverHeight) * hoverElevate;

            }

            return spritePosition;

        }

        public override void DrawShadow(SpriteBatch b, Vector2 localPosition, float drawLayer)
        {

            int offset = hoverHeight;

            Vector2 shadowPosition = new(localPosition.X - 10 + (offset), localPosition.Y + 14 + (offset));

            if (netDirection.Value % 2 == 1)
            {
                
                shadowPosition.Y += 4;
            
            }

            b.Draw(
                Mod.instance.iconData.cursorTexture, 
                shadowPosition, 
                Mod.instance.iconData.shadowRectangle, 
                Color.White * 0.35f, 
                0.0f, 
                Vector2.Zero, 
                (1.75f - (offset * 0.025f)) * (setScale / 4f), 
                0, 
                drawLayer - 1E-06f
            );

        }

        public override Rectangle GetBoundingBox()
        {

            Vector2 spritePosition = GetPosition(Position);

            float spriteScale = setScale;

            int width = idleFrames[0][0].Width;

            int height = idleFrames[0][0].Height;

            Rectangle box = new(
                (int)(spritePosition.X + (spriteScale * 2)),
                (int)(spritePosition.Y + (spriteScale * 4)),
                (int)(spriteScale * (width - 4)),
                (int)(spriteScale * (height - 4))
            );

            return box;

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

                if (hoverFrame >= idleFrames.Count)
                {

                    hoverFrame = 0;

                }

            }

        }

    }

}
