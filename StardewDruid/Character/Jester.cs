using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Event.World;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
    public class Jester : StardewDruid.Character.Character
    {
        public int xOffset;
        //public Vector2 facePosition;
        //public Vector2 beamPosition;
        //public Vector2 beamTargetOne;
        //public Vector2 beamTargetTwo;
        //public float beamRotation;
        //public bool flipBeam;
        //public bool flipFace;
        //public bool flipVert;

        //public bool beamActive;
        //public int beamTimer;
        //public int beamMoment;

        public Jester()
        {
        }

        public Jester(Vector2 position, string map)
          : base(position, map, nameof(Jester))
        {
            HideShadow = true;
        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {
            if (!Context.IsMainPlayer)
            {
                base.draw(b, alpha);

                return;
            }

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            if (base.IsEmoting && !Game1.eventUp)
            {
                Vector2 localPosition2 = getLocalPosition(Game1.viewport);
                localPosition2.Y -= 32 + Sprite.SpriteHeight * 4;
                b.Draw(Game1.emoteSpriteSheet, localPosition2, new Microsoft.Xna.Framework.Rectangle(base.CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, base.CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16), Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, getStandingY() / 10000f);
            }

            Vector2 localPosition = getLocalPosition(Game1.viewport);

            b.Draw(
                Game1.shadowTexture,
                localPosition + new Vector2(32f, 40f),
                Game1.shadowTexture.Bounds,
                Color.White * alpha, 0f,
                new Vector2(Game1.shadowTexture.Bounds.Center.X, Game1.shadowTexture.Bounds.Center.Y),
                4f,
                SpriteEffects.None,
                Math.Max(0.0f, getStandingY() / 10000f) - 0.0001f
                );


            if (timers.ContainsKey("idle"))
            {
                int num3 = timers["idle"] / 80 % 4 * 32;

                b.Draw(
                    Sprite.Texture,
                    localPosition + new Vector2(64, 16f),
                    new Rectangle(num3, 256, 32, 32),
                    Color.White,
                    0f,
                    new Vector2(Sprite.SpriteWidth / 2, Sprite.SpriteHeight * 3f / 4f),
                    4f,
                    flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    Math.Max(0f, drawOnTop ? 0.991f : (getStandingY() / 10000f))
                );

            }
            else
            {
                b.Draw(
                    Sprite.Texture,
                    localPosition + new Vector2(64f + xOffset, 16f),
                    Sprite.SourceRect,
                    Color.White,
                    0f,
                    new Vector2(Sprite.SpriteWidth / 2, Sprite.SpriteHeight * 3f / 4f),
                    4f,
                    flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    Math.Max(0f, drawOnTop ? 0.991f : (getStandingY() / 10000f))
                );

            }

            if (timers.ContainsKey("beam"))
            {

                DrawBeamEffect(b);

            }

        }

        public void DrawBeamEffect(SpriteBatch b)
        {

            Rectangle box = GetBoundingBox();
            float depth = 999f;
            float rotation = 0f;
            float fade = 1f;

            Vector2 position = Vector2.Zero;

            Rectangle rectangle = new(0, 128, 160, 32);

            if (timers["beam"] >= 54)
            {
                rectangle.Y = 0;
            }
            else if (timers["beam"] >= 48)
            {
                rectangle.Y = 32;
            }
            else if (timers["beam"] >= 42)
            {
                rectangle.Y = 64;
            }
            else if (timers["beam"] >= 36)
            {
                rectangle.Y = 96;
            }
            else
            {

                fade = timers["beam"] / 360;

            }

            switch (moveDirection)
            {
                case 0:
                    //position = new(box.Center.X - 48f - (float)Game1.viewport.X, box.Top - 496f - (float)Game1.viewport.Y);
                    position = new(box.Center.X - (float)Game1.viewport.X, box.Top - (float)Game1.viewport.Y);
                    depth = 0.0001f;
                    rotation = ((float)Math.PI / 2) + (float)Math.PI;
                    break;
                case 1:
                    position = new(box.Right - (float)Game1.viewport.X - 32f, box.Center.Y - (float)Game1.viewport.Y - 64f);
                    break;
                case 2:
                    position = new(box.Center.X - (float)Game1.viewport.X + 32f, box.Center.Y - (float)Game1.viewport.Y);
                    rotation = (float)Math.PI / 2;
                    break;
                default:
                    //position = new(box.Left - 496f - (float)Game1.viewport.X, box.Center.Y - 80f - (float)Game1.viewport.Y);
                    position = new(box.Left - (float)Game1.viewport.X - 32f, box.Center.Y - (float)Game1.viewport.Y);
                    rotation = (float)Math.PI;
                    break;

            }

            b.Draw(
                Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "EnergyBeam.png")),
                position,
                rectangle,
                Color.White * fade,
                rotation,
                Vector2.Zero,
                3f,
                SpriteEffects.None,
                depth
            );

        }

        public override Rectangle GetBoundingBox()
        {

            if(moveDirection % 2 == 0)
            {

                return new Rectangle ((int)Position.X + 8, (int)Position.Y - 16, 48, 76);

            }

            return new Rectangle ((int)Position.X + 16, (int)Position.Y + 8, 96, 48);

        }

        public override Rectangle GetHitBox()
        {
            return moveDirection % 2 == 0 ? new Rectangle((int)Position.X + 8, (int)Position.Y - 16, 48, 64) : new Rectangle((int)Position.X - 16, (int)Position.Y + 16, 96, 32);
        }

        public override void AnimateMovement(GameTime time)
        {
            if (timers.ContainsKey("attack") && targetOpponents.Count > 0 && (double)Vector2.Distance(Position, targetOpponents.First().Position) <= 96.0 && Sprite.CurrentFrame % 6 == 2 && Sprite.CurrentFrame >= 24)
            {
                return;
            }

            flip = false;
            moveDown = false;
            moveLeft = false;
            moveRight = false;
            moveUp = false;
            FacingDirection = moveDirection;
            Sprite.interval = 175f;
            xOffset = 0;
            switch (moveDirection)
            {
                case 0:
                    Sprite.AnimateUp(time, 0, "");
                    moveUp = true;
                    if (altDirection == 3)
                    {
                        flip = true;
                        xOffset = -56;
                        break;
                    }
                    break;
                case 1:
                    Sprite.AnimateRight(time, 0, "");
                    moveRight = true;
                    xOffset = -32;
                    break;
                case 2:
                    Sprite.AnimateDown(time, 0, "");
                    moveDown = true;
                    if (altDirection == 3)
                    {
                        flip = true;
                        xOffset = -56;
                        break;
                    }
                    break;
                default:
                    moveLeft = true;
                    Sprite.AnimateLeft(time, 0, "");
                    xOffset = -32;
                    break;
            }
            if (!timers.ContainsKey("sprint") && !timers.ContainsKey("attack"))
            {
                return;
            }
            if (Sprite.CurrentFrame < 24)
            {
                Sprite.CurrentFrame += 24;
                Sprite.UpdateSourceRect();
            }
            Sprite.interval = 125f;
        }

        public override bool TargetOpponent()
        {
            if (!base.TargetOpponent())
            {

                return false;
            
            }
                
            float distance = Vector2.Distance(Position, targetOpponents.First().Position);

            if (distance >= 64f && distance <= 480f)
            {

                StardewValley.Monsters.Monster targetMonster = targetOpponents.First();

                Vector2 vector2 = new(targetMonster.Position.X - Position.X - 32f, targetMonster.Position.Y - Position.Y);//Vector2.op_Subtraction(((StardewValley.Character)targetOpponents.First<Monster>()).Position, Vector2.op_Addition(Position, new Vector2(32f, 0.0f)));
                
                if ((double)Math.Abs(vector2.Y) <= 64.0 || (double)Math.Abs(vector2.X) <= 64.0)
                {

                    Halt();

                    AnimateMovement(Game1.currentGameTime);

                    timers["stop"] = 48;

                    timers["beam"] = 60;

                    timers["cooldown"] = 240;

                    timers["busy"] = 300;

                }

            }
            
            return true;

        }

        public override void update(GameTime time, GameLocation location)
        {

            base.update(time, location);

            if (timers.ContainsKey("beam"))
            {
                if (timers["beam"] == 30)
                {

                    BeamAttack();

                }

            }
        
        }

        public void BeamAttack()
        {

            Vector2 beamPoint = Vector2.Zero;

            Rectangle beamBox = GetBoundingBox();

            switch (moveDirection)
            {
                case 0:

                    beamPoint = new(beamBox.Center.X, beamBox.Top - 240);

                    break;

                case 1:

                    beamPoint = new(beamBox.Right + 240, beamBox.Center.Y);

                    break;

                case 2:

                    beamPoint = new(beamBox.Center.X, beamBox.Bottom + 240);

                    break;

                default:

                    beamPoint = new(beamBox.Left - 240, beamBox.Center.Y);

                    break;

            }

            for (int index = currentLocation.characters.Count - 1; index >= 0; index--)
            {

                if (currentLocation.characters[index] is StardewValley.Monsters.Monster character)
                {

                    if (Vector2.Distance(character.Position, beamPoint) < 240f)
                    {
                        base.DealDamageToMonster(character, true, Mod.instance.DamageLevel() / 2, false);

                    }

                }

            }

            currentLocation.playSoundPitched("flameSpellHit", 1200, 0);

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {
            if (!base.checkAction(who, l))
                return false;
            if (!Mod.instance.dialogue.ContainsKey(nameof(Jester)))
            {
                Dictionary<string, StardewDruid.Dialogue.Dialogue> dialogue = Mod.instance.dialogue;
                StardewDruid.Dialogue.Jester jester = new StardewDruid.Dialogue.Jester();
                jester.npc = this;
                dialogue[nameof(Jester)] = jester;
            }
            Mod.instance.dialogue[nameof(Jester)].DialogueApproach();
            return true;
        }

        public override void HitMonster(StardewValley.Monsters.Monster monsterCharacter)
        {

            DealDamageToMonster(monsterCharacter,true, Mod.instance.DamageLevel());

        }

        public override void DealDamageToMonster(StardewValley.Monsters.Monster monster, bool kill = false, int damage = -1, bool push = true)
        {
            
            base.DealDamageToMonster(monster, true, damage, push);
            
            if (Mod.instance.CurrentProgress() < 25)
            {
            
                return;
            
            }
                
            ApplyDazeEffect(monster);

        }

        public void ApplyDazeEffect(StardewValley.Monsters.Monster monster)
        {
            if (Mod.instance.eventRegister.ContainsKey("Gravity"))
                return;
            List<int> source = new List<int>();
            for (int index = 0; index < 5; ++index)
            {
                string key = "daze" + index.ToString();
                if (!Mod.instance.eventRegister.ContainsKey(key))
                    source.Add(index);
                else if ((Mod.instance.eventRegister[key] as Daze).victim == monster)
                    return;
            }
            if (source.Count <= 0)
                return;
            Rite rite = Mod.instance.NewRite(false);
            Daze daze = new Daze(getTileLocation(), rite, monster, source.First<int>(), 1);
            if (!MonsterData.CustomMonsters().Contains(monster.GetType()))
            {
                monster.Halt();
                monster.stunTime = 4000;
            }
            daze.EventTrigger();
        }

        public override void SwitchFollowMode()
        {
            base.SwitchFollowMode();
            Buff buff = new Buff("Fortune's Favour", 999999, nameof(Jester), 4);
            buff.buffAttributes[4] = 2;
            buff.which = 184654;
            if (Game1.buffsDisplay.hasBuff(184654))
                return;
            Game1.buffsDisplay.addOtherBuff(buff);
        }

        public override void SwitchDefaultMode()
        {
            base.SwitchDefaultMode();
            foreach (Buff otherBuff in Game1.buffsDisplay.otherBuffs)
            {
                if (otherBuff.which == 184654)
                    otherBuff.removeBuff();
            }
        }
    }
}
