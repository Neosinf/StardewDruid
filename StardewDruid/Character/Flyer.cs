
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using StardewValley.Minigames;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewValley.Objects;
using StardewDruid.Journal;
using StardewValley.Monsters;
using System.Linq;
using System.Runtime.Intrinsics;
using System.Threading;
using System.Security.Cryptography.X509Certificates;


namespace StardewDruid.Character
{
    public class Flyer : StardewDruid.Character.Character
    {

        public enum flyerFrames
        {
            
            downIdle,
            downPeck,
            downLift,
            downFullOut,
            downHalfOut,
            downGlide,
            downHalfIn,
            downFullIn,

            rightIdle,
            rightPeck,
            rightLift,
            rightFullOut,
            rightHalfOut,
            rightGlide,
            rightHalfIn,
            rightFullIn,

            upIdle,
            upPeck,
            upLift,
            upFullOut,
            upHalfOut,
            upGlide,
            upHalfIn,
            upFullIn,

        }

        public bool liftOff;

        public bool circling;

        public List<StardewValley.Item> carryItems = new();

        public Flyer()
        {
        }

        public Flyer(CharacterHandle.characters type = CharacterHandle.characters.ShadowCrow)
          : base(type)
        {

        }

        public Rectangle FlyerRectangle(flyerFrames frame)
        {
            return new((int)frame % 8 * 48, (int)frame / 8 * 48, 48, 48);
        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = Enum.Parse<CharacterHandle.characters>(Name);

            }

            characterTexture = CharacterHandle.CharacterTexture(characterType);

            LoadIntervals();

            warpDisplay = IconData.warps.smoke;

            moveInterval = 12;

            dashInterval = 9;

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

            alertFrames = new(haltFrames);

            idleFrames = new()
            {

                [0] = new() {
                    FlyerRectangle(flyerFrames.upIdle),
                    FlyerRectangle(flyerFrames.upPeck),
                    FlyerRectangle(flyerFrames.upIdle),
                    FlyerRectangle(flyerFrames.upPeck),
                    FlyerRectangle(flyerFrames.upIdle),
                    FlyerRectangle(flyerFrames.upIdle),
                },

                [1] = new() {
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightPeck),
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightPeck),
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightIdle),
                },

                [2] = new() {
                    FlyerRectangle(flyerFrames.downIdle),
                    FlyerRectangle(flyerFrames.downPeck),
                    FlyerRectangle(flyerFrames.downIdle),
                    FlyerRectangle(flyerFrames.downPeck),
                    FlyerRectangle(flyerFrames.downIdle),
                    FlyerRectangle(flyerFrames.downIdle),
                },

                [3] = new() {
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightPeck),
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightPeck),
                    FlyerRectangle(flyerFrames.rightIdle),
                    FlyerRectangle(flyerFrames.rightIdle),
                },

            };
            specialInterval = 10;

            specialCeiling = 5;

            specialFloor = 0;

            specialFrames = new(idleFrames);

            workFrames = new(specialFrames);

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

            if (netDashActive.Value || netSmashActive.Value || netSceneActive.Value)
            {

                return;

            }

            drawActual(b);

        }
        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {

            base.drawAboveAlwaysFrontLayer(b);

            if (netDashActive.Value || netSmashActive.Value || netSceneActive.Value)
            {

                drawActual(b);

            }

        }

        public virtual void drawActual(SpriteBatch b)
        {

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

            DrawShadow(b, spritePosition, drawLayer);

        }


        public override void DrawStandby(SpriteBatch b, Vector2 spritePosition, float drawLayer)
        {

            int chooseFrame = IdleFrame();

            float fade = fadeOut == 0 ? 1f : fadeOut;

            b.Draw(
                characterTexture,
                spritePosition,
                idleFrames[netDirection.Value][chooseFrame],
                Color.White * fade,
                0f,
                new Vector2(24),
                setScale,
                netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer
            );

            return;

        }

        public override void DrawShadow(SpriteBatch b, Vector2 shadowPosition, float drawLayer)
        {

            shadowPosition.Y += 24;

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
                new Vector2(24), 
                setScale * 0.8f, 
                0, 
                drawLayer - 0.0001f
            );

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

        public override bool TrackToClose(int close = 128)
        {

            return false;

        }

        public override bool TrackToFar(int limit = 960, int nodeLimit = 20)
        {
            return false;

        }

        public override bool PathPlayer()
        {

            Vector2 target = Mod.instance.trackers[characterType].TrackPosition();

            if(Mod.instance.trackers[characterType].linger > 0 && Vector2.Distance(target,Position) >= 128)
            {

                Vector2 offset = ModUtility.DirectionAsVector(trackQuadrant) * 128;

                Vector2 tryPosition = target + offset;

                List<Vector2> open = ModUtility.GetOccupiableTilesNearby(currentLocation, ModUtility.PositionToTile(tryPosition), -1, 2, 0);

                // can land at site
                /*if (PathTarget(tryPosition, 1, 2))
                {

                    // dont need the tracked path now

                    pathActive = pathing.running;

                    Mod.instance.trackers[characterType].nodes.Clear();

                    followTimer = 180 + (60 * Mod.instance.randomIndex.Next(5));

                    return true;

                }*/

                if (open.Count > 0)
                {

                    traversal.Add(open[Mod.instance.randomIndex.Next(open.Count)], 1);

                    destination = traversal.Keys.Last();

                    pathActive = pathing.running;

                    Mod.instance.trackers[characterType].nodes.Clear();

                    followTimer = 180 + (60 * Mod.instance.randomIndex.Next(5));

                    return true;

                }

            }

            return CircleAround(target);

        }

        public bool CircleAround(Vector2 target)
        {

            traversal.Clear();

            List<Vector2> circleArounds = ModUtility.GetTilesWithinRadius(currentLocation, ModUtility.PositionToTile(target), 6 + Mod.instance.randomIndex.Next(3), false);

            List<Vector2> circleBacks = new();

            int quadrant = ModUtility.DirectionToTarget(Position, target)[2];

            int segment = (circleArounds.Count / 8);

            int upperLevel = segment * quadrant;

            int lowerLevel = segment * ((quadrant + 7) % 8);

            for (int i = 0; i < circleArounds.Count; i += 2)
            {
                    
                if (i > upperLevel && i < upperLevel + 6)
                {

                    continue;

                }

                if (i > lowerLevel && i < lowerLevel + 6)
                {

                    continue;

                }
                
                if (i < lowerLevel)
                {

                    circleBacks.Add(circleArounds[i]);
                    
                    continue;
                
                }

                traversal.Add(circleArounds[i], 0);

            }

            for(int i = 0;i < circleBacks.Count; i++)
            {

                traversal.Add(circleBacks[i], 0);

            }

            if (traversal.Count > 0)
            {
                
                pathActive = pathing.circling;

                destination = traversal.Keys.Last();

                return true;

            }

            return false;

        }

        public override bool PathTrack()
        {

            if (base.PathTrack())
            {

                pathActive = pathing.running;

                return true;

            }

            return false;

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {
            
            return base.SmashAttack(monster);

        }

        public override void ConnectSweep()
        {

            SpellHandle swipeEffect = new(Game1.player, Position, 192, Mod.instance.CombatDamage() / 2);

            swipeEffect.instant = true;

            swipeEffect.added = new() { SpellHandle.effects.mug, };

            swipeEffect.sound = SpellHandle.sounds.crow;

            //swipeEffect.display = IconData.impacts.puff;

            Mod.instance.spellRegister.Add(swipeEffect);

        }


        public override bool TargetWork()
        {

            if (carryItems.Count > 0)
            {
                
                ResetActives();

                if (PathTarget(Game1.player.Position, 2, 2))
                {

                    pathActive = pathing.player;

                    SetDash(Game1.player.Position, false);

                    Mod.instance.trackers[characterType].linger = 0;

                    cooldownTimer = cooldownInterval / 2;

                }

                return true;
            
            }

            foreach (NPC witness in ModUtility.GetFriendsInLocation(Game1.player.currentLocation, true))
            {

                float distance = Vector2.Distance(witness.Position, Position);

                if (distance < 256f)
                {

                    if (Mod.instance.Witnessed(ReactionData.reactions.corvid, witness))
                    {
                        
                        continue;
                    
                    }
                    
                    workVector = ModUtility.PositionToTile(witness.Position);

                    netWorkActive.Set(true);

                    netSpecialActive.Set(true);

                    specialTimer = 60;

                    if (distance > 80)
                    {
                        Vector2 offset = ModUtility.DirectionAsVector(trackQuadrant) * 64;

                        if (PathTarget(witness.Position + offset, 2, 1))
                        {

                            SetDash(destination*64, false);

                            specialTimer += 30;

                        }

                    }

                    return true;

                }

            }

            List<Vector2> objectVectors = new List<Vector2>();

            for (int i = 0; i < 4; i++)
            {

                if (currentLocation.objects.Count() == 0)
                {
                    break;

                }

                objectVectors = ModUtility.GetTilesWithinRadius(currentLocation, occupied, i); ;

                foreach (Vector2 objectVector in objectVectors)
                {

                    if (currentLocation.objects.ContainsKey(objectVector))
                    {

                        if (ValidWorkTarget(currentLocation.objects[objectVector]))
                        {

                            LookAtTarget(objectVector * 64, true);

                            workVector = objectVector;

                            netWorkActive.Set(true);

                            netSpecialActive.Set(true);

                            specialTimer = 60;

                            Vector2 workPosition = workVector * 64;

                            if (Vector2.Distance(workPosition, Position) > 80)
                            {
                                
                                if (PathTarget(workPosition, 2, 1))
                                {

                                    SetDash(workPosition, false);

                                    specialTimer += 30;
                                
                                }
  
                            }

                            return true;

                        }

                    }

                }

            }

            foreach(Debris debris in currentLocation.debris)
            {

                if (debris.Chunks.Count > 0)
                {
                    
                    if (Vector2.Distance(debris.Chunks.First().position.Value,Position) <= 192)
                    {

                        switch (debris.debrisType.Value)
                        {

                            case Debris.DebrisType.ARCHAEOLOGY:
                            case Debris.DebrisType.OBJECT:
                            case Debris.DebrisType.RESOURCE:

                                workVector = ModUtility.PositionToTile(debris.Chunks.First().position.Value);

                                netWorkActive.Set(true);

                                netSpecialActive.Set(true);

                                specialTimer = 30;

                                return true;

                        }

                    }

                }

            }

            return false;

        }

        public bool ValidWorkTarget(StardewValley.Object targetObject)
        {

            if (
                targetObject.IsBreakableStone() ||
                targetObject.IsTwig() || 
                targetObject.QualifiedItemId == "(O)169" ||
                targetObject.IsWeeds() ||
                targetObject.Name.Contains("SupplyCrate") ||
                targetObject is BreakableContainer breakableContainer ||
                targetObject.QualifiedItemId == "(O)590" ||
                targetObject.QualifiedItemId == "(O)SeedSpot" ||
                targetObject.isForage()
            )
            {

                return true;

            }

            return false;

        }


        public override void PerformWork()
        {

            if (specialTimer == 30)
            {

                MidWorkFunction();
            
            }

            if(specialTimer == 5)
            {
                
                foreach (Debris debris in currentLocation.debris)
                {

                    if (debris.Chunks.Count > 0)
                    {
                        
                        if (Vector2.Distance(debris.Chunks.First().position.Value, Position) <= 192)
                        {
                            
                            switch (debris.debrisType.Value)
                            {

                                case Debris.DebrisType.ARCHAEOLOGY:
                                case Debris.DebrisType.OBJECT:
                                case Debris.DebrisType.RESOURCE:

                                    string debrisId = debris.itemId.Value;

                                    if (debris.item != null)
                                    {

                                        debrisId = debris.item.QualifiedItemId;

                                    }

                                    Item carryDebris = ItemRegistry.Create(debrisId);

                                    carryItems.Add(carryDebris);

                                    debris.item = null;

                                    debris.Chunks.Clear();

                                    break;

                            }

                        }

                    }

                }

            }

        }

        public virtual void MidWorkFunction()
        {


            foreach (NPC witness in ModUtility.GetFriendsInLocation(Game1.player.currentLocation, true))
            {

                float distance = Vector2.Distance(witness.Position, Position);

                if (distance < 256f)
                {

                    if (Mod.instance.Witnessed(ReactionData.reactions.corvid, witness))
                    {

                        continue;

                    }

                    int friendship = 15;

                    switch (Mod.instance.randomIndex.Next(3))
                    {

                        case 0:

                            friendship = 0;

                            if (Game1.NPCGiftTastes.TryGetValue(witness.Name, out var giftValue))
                            {

                                string[] giftArray = giftValue.Split('/');

                                List<string> giftList = new();

                                string[] lovedGifts = ArgUtility.SplitBySpace(giftArray[1]);

                                for (int j = 0; j < lovedGifts.Length; j++)
                                {

                                    if (lovedGifts[j].Length > 0 && !lovedGifts[j].StartsWith('-'))
                                    {

                                        giftList.Add(lovedGifts[j]);

                                    }

                                }

                                string[] likedGifts = ArgUtility.SplitBySpace(giftArray[3]);

                                for (int j = 0; j < likedGifts.Length; j++)
                                {

                                    if (likedGifts[j].Length > 0 && !likedGifts[j].StartsWith('-'))
                                    {

                                        giftList.Add(likedGifts[j]);

                                    }

                                }

                                if (giftList.Count > 0)
                                {

                                    Item stealGift = ItemRegistry.Create(giftList[Mod.instance.randomIndex.Next(giftList.Count)],1,2,true);

                                    if (stealGift != null)
                                    {

                                        carryItems.Add(stealGift);

                                    }

                                }


                            }

                            break;

                        case 1:

                            friendship = 25;

                            break;

                    }

                    Game1.player.changeFriendship(friendship, witness);

                    ReactionData.ReactTo(witness, ReactionData.reactions.corvid, friendship);

                    return;

                }

            }

            if (currentLocation.objects.ContainsKey(workVector))
            {

                if (SpawnData.ForageCheck(currentLocation.objects[workVector]))
                {

                    StardewValley.Item objectInstance = ModUtility.ExtractForage(currentLocation, workVector, false);

                    currentLocation.objects.Remove(workVector);

                    carryItems.Add(objectInstance);

                    return;

                }

                SpellHandle explode = new(Game1.player, workVector * 64, 128, -1);

                explode.type = SpellHandle.spells.explode;

                //explode.display = IconData.impacts.flashbang;

                explode.indicator = IconData.cursors.none;

                if (currentLocation.objects[workVector].IsBreakableStone())
                {

                    explode.sound = SpellHandle.sounds.hammer;

                }

                explode.power = 2;

                explode.terrain = 2;

                Mod.instance.spellRegister.Add(explode);

            }

        }

        public override void DashAscension()
        {

            if(pathProgress > 1 && carryItems.Count > 0 && pathActive == pathing.player)
            {

                foreach(Item item in carryItems)
                {
                    
                    ThrowHandle throwItem = new(Game1.player, Position, item);

                    Mod.instance.throwRegister.Add(throwItem);

                }

                carryItems.Clear();

            }

            base.DashAscension();


        }

    }

}
