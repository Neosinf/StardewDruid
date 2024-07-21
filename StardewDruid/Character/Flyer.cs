
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using StardewValley.Minigames;

namespace StardewDruid.Character
{
    public class Flyer : StardewDruid.Character.Character
    {

        public enum flyerFrames
        {
            
            downIdle,
            downLift,
            downFullOut,
            downHalfOut,
            downGlide,
            downHalfIn,
            downFullIn,

            rightIdle,
            rightLift,
            rightFullOut,
            rightHalfOut,
            rightGlide,
            rightHalfIn,
            rightFullIn,

            upIdle,
            upLift,
            upFullOut,
            upHalfOut,
            upGlide,
            upHalfIn,
            upFullIn,

        }

        public bool liftOff;

        public Flyer()
        {
        }

        public Flyer(CharacterHandle.characters type = CharacterHandle.characters.Crow)
          : base(type)
        {

        }

        public Rectangle FlyerRectangle(flyerFrames frame)
        {
            return new((int)frame % 7 * 48, (int)frame / 7 * 48, 48, 48);
        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = Enum.Parse<CharacterHandle.characters>(Name);

            }

            characterTexture = CharacterHandle.CharacterTexture(characterType);

            LoadIntervals();

            moveInterval = 9;

            dashInterval = 7;

            sweepInterval = 7;

            overhead = 112;

            setScale = 3.75f;

            gait = 1.8f;

            modeActive = mode.random;

            haltFrames = new()
            {

                [0] = new() {
                    FlyerRectangle(flyerFrames.upIdle), 
                },

                [1] = new() {
                    FlyerRectangle(flyerFrames.rightIdle),
                },

                [2] = new() {
                    FlyerRectangle(flyerFrames.downIdle),
                },

                [3] = new() {
                    FlyerRectangle(flyerFrames.rightIdle),
                },

            };

            idleFrames = new(haltFrames);

            alertFrames = new(haltFrames);

            specialFrames = new(haltFrames);

            workFrames = new(haltFrames);

            walkFrames = new()
            {

                [0] = new() {
                    FlyerRectangle(flyerFrames.upIdle),
                    FlyerRectangle(flyerFrames.upLift),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upFullOut),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upHalfIn),
                    FlyerRectangle(flyerFrames.upFullIn),
                    FlyerRectangle(flyerFrames.upHalfIn),
                },

                [1] = new() {
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightLift),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                    FlyerRectangle(flyerFrames.rightFullIn),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                },

                [2] = new() {
                    FlyerRectangle(flyerFrames.downIdle),
                    FlyerRectangle(flyerFrames.downLift),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downFullOut),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downHalfIn),
                    FlyerRectangle(flyerFrames.downFullIn),
                    FlyerRectangle(flyerFrames.downHalfIn),
                },

                [3] = new() {
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightLift),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                    FlyerRectangle(flyerFrames.rightFullIn),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                },

            };

            sweepFrames = new(walkFrames);

            dashFrames = new()
            {

                [0] = new() {
                    FlyerRectangle(flyerFrames.upLift),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upFullOut),
                    FlyerRectangle(flyerFrames.upHalfOut),
                },
                [1] = new() {
                    FlyerRectangle(flyerFrames.rightLift),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                },
                [2] = new() {
                    FlyerRectangle(flyerFrames.downLift),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downFullOut),
                    FlyerRectangle(flyerFrames.downHalfOut),
                },
                [3] = new() {
                    FlyerRectangle(flyerFrames.rightLift),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                },
                [4] = new() {
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upFullOut),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upHalfIn),
                    FlyerRectangle(flyerFrames.upFullIn),
                    FlyerRectangle(flyerFrames.upHalfIn),
                    FlyerRectangle(flyerFrames.upGlide),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upFullOut),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upGlide),
                },
                [5] = new() {
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                    FlyerRectangle(flyerFrames.rightFullIn),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightGlide),
                },
                [6] = new() {
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downFullOut),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downHalfIn),
                    FlyerRectangle(flyerFrames.downFullIn),
                    FlyerRectangle(flyerFrames.downHalfIn),
                    FlyerRectangle(flyerFrames.downGlide),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downFullOut),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downGlide),
                },
                [7] = new() {
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                    FlyerRectangle(flyerFrames.rightFullIn),
                    FlyerRectangle(flyerFrames.rightHalfIn),
                    FlyerRectangle(flyerFrames.rightGlide),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightGlide),
                },
                [8] = new() {
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upFullOut),
                    FlyerRectangle(flyerFrames.upHalfOut),
                    FlyerRectangle(flyerFrames.upLift),
                },
                [9] = new() {
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightLift),
                },
                [10] = new() {
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downFullOut),
                    FlyerRectangle(flyerFrames.downHalfOut),
                    FlyerRectangle(flyerFrames.downLift),
                },
                [11] = new() {
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightFullOut),
                    FlyerRectangle(flyerFrames.rightHalfOut),
                    FlyerRectangle(flyerFrames.rightLift),
                },
            };

            smashFrames = new(dashFrames);

            loadedOut = true;
        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {

            if (netDashActive.Value || netSmashActive.Value)
            {
                if(netDashProgress.Value == 1)
                {
                    return;
                }

            }
                
            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            Vector2 spritePosition = localPosition + new Vector2(32);

            float drawLayer = (float)StandingPixel.Y / 10000f + 0.001f;

            float fade = fadeOut == 0 ? 1f : fadeOut;

            bool flippity = netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3);

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
                    new Vector2(24),
                    setScale,
                    flippity ? (SpriteEffects)1 : 0,
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
                    new Vector2(24),
                    setScale,
                    flippity ? (SpriteEffects)1 : 0,
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
                    new Vector2(24),
                    setScale,
                    flippity ? (SpriteEffects)1 : 0,
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
                    new Vector2(24),
                    setScale,
                    flippity ? (SpriteEffects)1 : 0,
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
                    new Vector2(24),
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
                    specialFrames[netDirection.Value][0],
                    Color.White * fade,
                    0.0f,
                    new Vector2(24),
                    setScale,
                    flippity ? (SpriteEffects)1 : 0,
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
                {

                    int useFrame = moveFrame;

                    if(liftOff && moveFrame == 1)
                    {

                        useFrame = 5;

                    }

                    b.Draw(
                        characterTexture,
                        spritePosition - new Vector2(0, setScale * (liftOff ? 24f : moveFrame * 12f)),
                        walkFrames[netDirection.Value][moveFrame],
                        Color.White * fade,
                        0f,
                        new Vector2(24),
                        setScale,
                        flippity ? (SpriteEffects)1 : 0,
                        drawLayer
                    );

                    if(moveFrame == 2)
                    {

                        liftOff = true;

                    }
                    else if(moveFrame == 0)
                    {

                        liftOff = false;

                    }

                }


            }

            DrawShadow(b, localPosition, drawLayer);

        }
        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {

            base.drawAboveAlwaysFrontLayer(b);

            if (netDashActive.Value || netSmashActive.Value)
            {
                if (netDashProgress.Value != 1)
                {
                    return;
                }

            }
            else
            {

                return;

            }

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            Vector2 spritePosition = localPosition + new Vector2(32);

            float drawLayer = (float)StandingPixel.Y / 10000f + 0.001f;

            float fade = fadeOut == 0 ? 1f : fadeOut;

            if (netDashActive.Value)
            {

                int dashSeries = netDirection.Value + (netDashProgress.Value * 4);

                int dashSetto = Math.Min(dashFrame, (dashFrames[dashSeries].Count - 1));

                b.Draw(
                    characterTexture,
                    spritePosition - new Vector2(0, dashHeight),
                    dashFrames[dashSeries][dashSetto],
                    Color.White * fade,
                    0f,
                    new Vector2(24),
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
                    new Vector2(24),
                    setScale,
                    (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

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

        public override void DrawShadow(SpriteBatch b, Vector2 localPosition, float drawLayer)
        {

            Vector2 shadowPosition = localPosition + new Vector2(32, 56);

            if (netDirection.Value % 2 == 1)
            {
                shadowPosition.Y += 4;
            }

            b.Draw(Mod.instance.iconData.cursorTexture, shadowPosition, Mod.instance.iconData.shadowRectangle, Color.White * 0.35f, 0.0f, new Vector2(24), setScale * 0.8f, 0, drawLayer - 0.0001f);

        }

        public override Rectangle GetBoundingBox()
        {

            if (netDirection.Value % 2 == 0)
            {

                return new Rectangle((int)Position.X + 8, (int)Position.Y + 8, 48, 48);

            }

            return new Rectangle((int)Position.X - 16, (int)Position.Y + 8, 96, 48);

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {
            return false;
        }

    }

}
