using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
    public class Effigy : StardewDruid.Character.Character
    {
        public List<Vector2> ritesDone;
        public int riteIcon;
        public bool showIcon;

        public Effigy()
        {
        }

        public Effigy(Vector2 position, string map)
          : base(position, map, nameof(Effigy))
        {
            ritesDone = new List<Vector2>();
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

            if (timers.ContainsKey("idle"))
            {
                int num = timers["idle"] / 200 % 2 == 0 ? 0 : 32;

                b.Draw(
                    Sprite.Texture,
                    localPosition + new Vector2(32f, 16f),
                    new Rectangle(num, 160, 32, 32),
                    Color.White,
                    0f,
                    new Vector2(Sprite.SpriteWidth / 2, Sprite.SpriteHeight * 3f / 4f),
                    Math.Max(0.2f, scale) * 4f,
                    flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    Math.Max(0f, drawOnTop ? 0.991f : ((float)getStandingY() / 10000f))
                );

                return;

            }

            if (timers.ContainsKey("beam"))
            {

                DrawBeamEffect(b);

                return;

            }

            Rectangle sourceRectangle = Sprite.SourceRect;

            if (timers.ContainsKey("cast"))
            {
                switch (moveDirection)
                {
                    case 0:
                        sourceRectangle = new(32, 128, 16, 32);
                        break;
                    case 1:
                        sourceRectangle = new(16, 128, 16, 32);
                        break;
                    case 2:
                        sourceRectangle = new(0, 128, 16, 32);
                        break;
                    default:
                        sourceRectangle = new(48, 128, 16, 32);
                        break;

                }

            }

            b.Draw(
                Sprite.Texture,
                localPosition + new Vector2(32f, 16f),
                sourceRectangle,
                Color.White,
                0f,
                new Vector2(Sprite.SpriteWidth / 2, Sprite.SpriteHeight * 3f / 4f),
                Math.Max(0.2f, scale) * 4f,
                flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawOnTop ? 0.991f : ((float)getStandingY() / 10000f)
                );

            b.Draw(
                Game1.shadowTexture,
                localPosition + new Vector2(32f, 40f),
                Game1.shadowTexture.Bounds,
                Color.White * 0.65f,
                0f,
                new Vector2(Game1.shadowTexture.Bounds.Center.X, Game1.shadowTexture.Bounds.Center.Y),
                4f,
                SpriteEffects.None,
                Math.Max(0.0f, (getStandingY() / 10000f) - 0.0001f)
                );

            if (showIcon)
            {
                b.Draw(
                    Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Icons.png")),
                    localPosition + new Vector2(16,-16),
                    new Rectangle((riteIcon % 4)*8, (riteIcon / 4)*8, 8, 8),
                    Color.White,
                    0f,
                    new Vector2(0, 0),
                    4f,
                    SpriteEffects.None,
                    drawOnTop ? 0.992f : ((float)getStandingY() / 10000f) + 0.001f
                );

            }



        }

        public void DrawBeamEffect(SpriteBatch b)
        {


            Vector2 localPosition = getLocalPosition(Game1.viewport);

            if (timers["beam"] >= 48)
            {
                b.Draw(
                    Sprite.Texture,
                    localPosition + new Vector2(0, 16f),
                    new Rectangle(0, 192, 32, 32),
                    Color.White,
                    0f,
                    new Vector2(Sprite.SpriteWidth / 2, Sprite.SpriteHeight * 3f / 4f),
                    4f,
                    moveDirection == 3 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawOnTop ? 0.991f : ((float)getStandingY() / 10000f)
                    );

                b.Draw(
                    Game1.shadowTexture,
                    localPosition + new Vector2(0, 40f),
                    Game1.shadowTexture.Bounds,
                    Color.White * 0.65f,
                    0f,
                    new Vector2(Game1.shadowTexture.Bounds.Center.X, Game1.shadowTexture.Bounds.Center.Y),
                    4f,
                    SpriteEffects.None,
                    Math.Max(0.0f, (getStandingY() / 10000f) - 0.0001f)
                    );

                Rectangle beamRect = new Rectangle(0, 0, 64, 64);

                if (timers["beam"] <= 51)
                {
                    beamRect.X += 192;
                }
                else if (timers["beam"] <= 54)
                {
                    beamRect.X += 128;
                }
                else if (timers["beam"] <= 57)
                {
                    beamRect.X += 64;
                }

                b.Draw(
                    Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "EnergyBomb.png")),
                    localPosition - new Vector2(96f, 64f),
                    beamRect,
                    Color.White,
                    0f,
                    new Vector2(0,0),
                    1f,
                    SpriteEffects.None,
                    Math.Max(0.0f, (getStandingY() / 10000f) +0.0001f)
                    );

                return;

            }

            Rectangle box = GetBoundingBox();
            float depth = 999f;
            float rotation = 0f;
            float fade = 1f;

            Vector2 position = Vector2.Zero;

            Rectangle rectangle = new(0, 128, 160, 32);

            if (timers["beam"] >= 42)
            {
                rectangle.Y = 0;
            }
            else if (timers["beam"] >= 36)
            {
                rectangle.Y = 32;
            }
            else if (timers["beam"] >= 30)
            {
                rectangle.Y = 64;
            }
            else if (timers["beam"] >= 24)
            {
                rectangle.Y = 96;
            }
            else
            {

                fade = timers["beam"] / 240;

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
                    position = new(box.Right - (float)Game1.viewport.X - 32f, box.Center.Y - (float)Game1.viewport.Y - 96f);
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

            b.Draw(
                Sprite.Texture,
                localPosition + new Vector2(0, 16f),
                new Rectangle(32,192,32,32),
                Color.White,
                0f,
                new Vector2(Sprite.SpriteWidth / 2, Sprite.SpriteHeight * 3f / 4f),
                4f,
                moveDirection == 3 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawOnTop ? 0.991f : ((float)getStandingY() / 10000f)
                );

            b.Draw(
                Game1.shadowTexture,
                localPosition + new Vector2(0, 40f),
                Game1.shadowTexture.Bounds,
                Color.White * 0.65f,
                0f,
                new Vector2(Game1.shadowTexture.Bounds.Center.X, Game1.shadowTexture.Bounds.Center.Y),
                4f,
                SpriteEffects.None,
                Math.Max(0.0f, (getStandingY() / 10000f) - 0.0001f)
                );

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {
            base.checkAction(who, l);
            if (Context.IsMainPlayer)
            {
                AdjustJacket();
            }
                
            if (!Mod.instance.dialogue.ContainsKey(nameof(Effigy)))
            {
                Dictionary<string, StardewDruid.Dialogue.Dialogue> dialogue = Mod.instance.dialogue;
                StardewDruid.Dialogue.Effigy effigy = new StardewDruid.Dialogue.Effigy();
                effigy.npc = this;
                dialogue[nameof(Effigy)] = effigy;
            }
            Mod.instance.dialogue[nameof(Effigy)].DialogueApproach();
            return true;
        }

        public override List<Vector2> RoamAnalysis()
        {
            
            List<Vector2> vector2List = new List<Vector2>();

            int takeABreak = 0;

            foreach (Dictionary<Vector2, StardewValley.Object> dictionary in currentLocation.Objects)
            {
                
                foreach (KeyValuePair<Vector2, StardewValley.Object> keyValuePair in dictionary)
                {
                    
                    if (keyValuePair.Value.IsScarecrow())
                    {

                        Vector2 scareVector = new(keyValuePair.Key.X * 64f, keyValuePair.Key.Y * 64f);

                        vector2List.Add(scareVector);

                        takeABreak++;

                    }

                    if (takeABreak >= 4)
                    {

                        vector2List.Add(new Vector2(-1f));

                        takeABreak = 0;

                    }

                }

                if (vector2List.Count >= 30)
                {

                    break;

                }
                    
            }
           
            List<Vector2> collection = base.RoamAnalysis();
            
            vector2List.AddRange(collection);
            
            return vector2List;
        
        }

        public override void AnimateMovement(GameTime time)
        {

            moveDown = false;
            moveLeft = false;
            moveRight = false;
            moveUp = false;
            FacingDirection = moveDirection;
            showIcon = false;
            switch (moveDirection)
            {
                case 0:
                    Sprite.AnimateUp(time, 0, "");
                    moveUp = true;
                    AdjustJacket();
                    break;
                case 1:
                    Sprite.AnimateRight(time, 0, "");
                    moveRight = true;
                    break;
                case 2:
                    Sprite.AnimateDown(time, 0, "");
                    moveDown = true;
                    break;
                default:
                    Sprite.AnimateLeft(time, 0, "");
                    moveLeft = true;
                    break;
            }

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

                if ((double)Math.Abs(vector2.Y) <= 72.0)
                {

                    Halt();

                    moveDirection = 1;

                    if(vector2.X < 0.001)
                    {
                        moveDirection = 3;

                    }

                    AnimateMovement(Game1.currentGameTime);

                    timers["stop"] = 48;

                    timers["beam"] = 60;

                    timers["cooldown"] = 240;

                    timers["busy"] = 300;

                }
                else
                {

                    Halt();

                    AnimateMovement(Game1.currentGameTime);

                    timers["stop"] = 72;

                    timers["cast"] = 30;

                    timers["cooldown"] = 240;

                    timers["busy"] = 300;

                    List<int> diff = ModUtility.CalculatePush(currentLocation, targetMonster, Position, 64);

                    ModUtility.HitMonster(currentLocation, Game1.player, targetMonster, Mod.instance.DamageLevel() / 2, false, diffX: diff[0], diffY: diff[1]);

                    ModUtility.AnimateBolt(currentLocation, new Vector2(targetMonster.getTileLocation().X, targetMonster.getTileLocation().Y - 1), 1200);
 
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
                        base.DealDamageToMonster(character, true, -1, false);

                    }

                }

            }

            currentLocation.playSoundPitched("flameSpellHit", 1200, 0);

        }

        public void AdjustJacket()
        {
            if (moveDirection != 0 || Sprite.currentFrame < 8 || Sprite.currentFrame >= 12)
            {
                return;
            }

            showIcon = true;

            switch (Mod.instance.CurrentBlessing())
            {

                case "mists":
                    //Sprite.currentFrame += 12;
                    riteIcon = 2;
                    break;

                case "stars":
                    //Sprite.currentFrame += 20;
                    riteIcon = 3;
                    break;

                case "fates":
                    //Sprite.currentFrame += 28;
                    riteIcon = 4;
                    break;

                case "ether":
                    //Sprite.currentFrame += 28;
                    riteIcon = 6;
                    break;

                default: // weald
                    riteIcon = 1;
                    break;

            }
            //Sprite.UpdateSourceRect();
        }

        public override void HitMonster(StardewValley.Monsters.Monster monsterCharacter)
        {

            return;

        }

        public override void ReachedRoamPosition()
        {
            Vector2 vector2 = new(roamVectors[roamIndex].X / 64f, roamVectors[roamIndex].Y / 64f);//Vector2.op_Division(roamVectors[roamIndex], 64f);
            if (ritesDone.Contains(vector2) || !currentLocation.Objects.ContainsKey(vector2) || !currentLocation.Objects[vector2].IsScarecrow())
                return;
            Halt();
            timers["cast"] = 30;
            Rite rite = Mod.instance.NewRite(false);
            bool Reseed = !Mod.instance.EffectDisabled("Seeds");
            for (int level = 1; level < (Mod.instance.PowerLevel()+1); level++)
            {
                foreach (Vector2 tilesWithinRadius in ModUtility.GetTilesWithinRadius(currentLocation, vector2, level))
                {
                    if (currentLocation.terrainFeatures.ContainsKey(tilesWithinRadius) && currentLocation.terrainFeatures[tilesWithinRadius].GetType().Name.ToString() == "HoeDirt")
                        rite.effectCasts[tilesWithinRadius] = new StardewDruid.Cast.Weald.Crop(tilesWithinRadius, rite, Reseed, true);
                }
            }
            if (currentLocation.Name == Game1.player.currentLocation.Name && Utility.isOnScreen(Position, 128))
            {
                ModUtility.AnimateRadiusDecoration(currentLocation, vector2, "Weald", 1f, 1f, 1500f);
                Game1.player.currentLocation.playSoundPitched("discoverMineral", 1000, 0);
            }
            rite.CastEffect(false);
            ritesDone.Add(vector2);
        }

    }

}
