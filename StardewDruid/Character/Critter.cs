
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;

namespace StardewDruid.Character
{
    public class Critter : StardewDruid.Character.Character
    {


        public Dictionary<int, List<Rectangle>> runningFrames;

        public Critter()
        {
        }

        public Critter(CharacterHandle.characters type = CharacterHandle.characters.Shadowcat)
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

            overhead = 112;

            setScale = 3.75f;

            gait = 1.8f;

            modeActive = mode.random;

            haltFrames = FrameSeries(32, 32, 0, 0, 1);

            idleFrames = new(haltFrames);

            alertFrames = new(haltFrames);

            walkFrames = FrameSeries(32, 32, 0, 0, 7);

            runningFrames = FrameSeries(32, 32, 0, 128, 6, FrameSeries(32, 32, 0, 0, 1));

            specialFrames = new()
            {

                [0] = new() { new(128, 192, 32, 32), },

                [1] = new() { new(128, 160, 32, 32), },

                [2] = new() { new(128, 128, 32, 32), },

                [3] = new() { new(128, 224, 32, 32), },

            };

            workFrames = new(specialFrames);

            sweepFrames = FrameSeries(32, 32, 0, 128, 3);

            dashFrames = new(sweepFrames);

            dashFrames[4] = new() { new(64, 192, 32, 32), };
            dashFrames[5] = new() { new(64, 160, 32, 32), };
            dashFrames[6] = new() { new(64, 128, 32, 32), };
            dashFrames[7] = new() { new(64, 192, 32, 32), };

            dashFrames[8] = new() { new(96, 192, 32, 32), new(128, 192, 32, 32), new(160, 192, 32, 32), };
            dashFrames[9] = new() { new(96, 160, 32, 32), new(128, 160, 32, 32), new(160, 160, 32, 32), };
            dashFrames[10] = new() { new(96, 128, 32, 32), new(128, 128, 32, 32), new(160, 128, 32, 32), };
            dashFrames[11] = new() { new(96, 192, 32, 32), new(128, 192, 32, 32), new(160, 192, 32, 32), };

            smashFrames = new(dashFrames);

            loadedOut = true;
        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            Vector2 spritePosition = localPosition + new Vector2(32, 64) - (new Vector2(16, 32) * setScale);

            float drawLayer = (float)StandingPixel.Y / 10000f + 0.001f;

            float fade = fadeOut == 0 ? 1f : fadeOut;

            DrawEmote(b);

            if (netStandbyActive.Value)
            {

                DrawStandby(b, spritePosition, drawLayer);

                DrawShadow(b, localPosition, drawLayer);

                return;

            }
            else if (netHaltActive.Value)
            {

                b.Draw(
                    characterTexture,
                    spritePosition,
                    haltFrames[netDirection.Value][0],
                    Color.White * fade,
                    0f,
                    Vector2.Zero,
                    setScale,
                    (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

            }
            else if (netSweepActive.Value)
            {

                b.Draw(
                    characterTexture,
                    spritePosition,
                    sweepFrames[netDirection.Value][sweepFrame],
                    Color.White * fade,
                    0f,
                    Vector2.Zero,
                    setScale,
                    (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

            }
            else if (netDashActive.Value)
            {

                int dashSeries = netDirection.Value + (netDashProgress.Value * 4);

                int dashSetto = Math.Min(dashFrame, (dashFrames[dashSeries].Count - 1));

                b.Draw(
                    characterTexture,
                    spritePosition - new Vector2(0,dashHeight),
                    dashFrames[dashSeries][dashSetto],
                    Color.White * fade,
                    0f,
                    Vector2.Zero,
                    setScale,
                    (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

            }
            else if (netSmashActive.Value)
            {

                int smashSeries = netDirection.Value + (netDashProgress.Value * 4);

                int smashSetto = Math.Min(dashFrame, (smashFrames[smashSeries].Count - 1));

                b.Draw(
                    characterTexture,
                    spritePosition - new Vector2(0, dashHeight),
                    smashFrames[smashSeries][smashSetto],
                    Color.White * fade,
                    0f,
                    Vector2.Zero,
                    setScale,
                    (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

            }
            else if (netWorkActive.Value)
            {

                b.Draw(
                    characterTexture,
                    spritePosition,
                    workFrames[0][specialFrame],
                    Color.White * fade,
                    0.0f,
                    Vector2.Zero,
                    setScale,
                    SpriteEffects.None,
                    drawLayer
                );

            }
            else if (netSpecialActive.Value)
            {

                b.Draw(
                    characterTexture,
                    spritePosition,
                    specialFrames[netDirection.Value][0],
                    Color.White * fade,
                    0.0f,
                    Vector2.Zero,
                    setScale,
                    (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? (SpriteEffects)1 : 0,
                    drawLayer
                );

            }
            else
            {

                if (TightPosition() && idleTimer > 0 && !netSceneActive.Value)
                {

                    DrawStandby(b, spritePosition, drawLayer);

                }
                else
                if (pathActive == pathing.running)
                {
                    b.Draw(
                        characterTexture,
                        spritePosition,
                        runningFrames[netDirection.Value][moveFrame],
                        Color.White * fade,
                        0f,
                        Vector2.Zero,
                        3.75f,
                        (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                        drawLayer
                    );
                }
                else
                {
                    b.Draw(
                        characterTexture,
                        spritePosition,
                        walkFrames[netDirection.Value][moveFrame],
                        Color.White * fade,
                        0f,
                        Vector2.Zero,
                        setScale,
                        (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                        drawLayer
                    );

                }


            }

            DrawShadow(b, localPosition, drawLayer);

        }

        public override void DrawStandby(SpriteBatch b, Vector2 spritePosition, float drawLayer)
        {

            int chooseFrame = IdleFrame();

            float fade = fadeOut == 0 ? 1f : fadeOut;

            b.Draw(
                characterTexture,
                spritePosition,
                idleFrames[0][chooseFrame],
                Color.White * fade,
                0f,
                Vector2.Zero,
                setScale,
                netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer
            );

            return;

        }

        public override Rectangle GetBoundingBox()
        {

            if (netDirection.Value % 2 == 0)
            {

                return new Rectangle((int)Position.X + 8, (int)Position.Y + 8, 48, 48);

            }

            return new Rectangle((int)Position.X - 16, (int)Position.Y + 8, 96, 48);

        }

    }

}
