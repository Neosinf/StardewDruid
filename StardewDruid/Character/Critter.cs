
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

        public Critter(CharacterHandle.characters type = CharacterHandle.characters.BlackCat)
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

            LoadIntervals();

            overhead = 112;

            setScale = 3.75f;

            gait = 2.5f;

            modeActive = mode.random;

            idleFrames[idles.standby] = FrameSeries(32, 32, 0, 0, 1);

            idleFrames[idles.alert] = FrameSeries(32, 32, 0, 0, 1);

            walkFrames = FrameSeries(32, 32, 0, 0, 7);

            runningFrames = FrameSeries(32, 32, 0, 128, 6, FrameSeries(32, 32, 0, 0, 1));

            specialFrames[specials.invoke] = new()
            {

                [0] = new() { new(128, 192, 32, 32), },

                [1] = new() { new(128, 160, 32, 32), },

                [2] = new() { new(128, 128, 32, 32), },

                [3] = new() { new(128, 224, 32, 32), },

            };

            specialIntervals[specials.invoke] = 90;
            specialCeilings[specials.invoke] = 0;
            specialFloors[specials.invoke] = 0;

            specialFrames[specials.sweep] = FrameSeries(32, 32, 0, 128, 3);

            dashFrames[dashes.dash] = new(specialFrames[specials.sweep]);

            dashFrames[dashes.dash][4] = new() { new(64, 192, 32, 32), };
            dashFrames[dashes.dash][5] = new() { new(64, 160, 32, 32), };
            dashFrames[dashes.dash][6] = new() { new(64, 128, 32, 32), };
            dashFrames[dashes.dash][7] = new() { new(64, 192, 32, 32), };

            dashFrames[dashes.dash][8] = new() { new(96, 192, 32, 32), new(128, 192, 32, 32), new(160, 192, 32, 32), };
            dashFrames[dashes.dash][9] = new() { new(96, 160, 32, 32), new(128, 160, 32, 32), new(160, 160, 32, 32), };
            dashFrames[dashes.dash][10] = new() { new(96, 128, 32, 32), new(128, 128, 32, 32), new(160, 128, 32, 32), };
            dashFrames[dashes.dash][11] = new() { new(96, 192, 32, 32), new(128, 192, 32, 32), new(160, 192, 32, 32), };

            dashFrames[dashes.smash] = new(dashFrames[dashes.dash]);

            loadedOut = true;
        }

        public override void DrawStandby(SpriteBatch b, Vector2 localPosition, float drawLayer, float fade)
        {
            Vector2 spritePosition = localPosition + new Vector2(32, 64) - (new Vector2(16, 32) * setScale);

            int idleFrame = IdleFrame(idles.standby);

            b.Draw(
                characterTexture,
                spritePosition,
                idleFrames[idles.standby][0][idleFrame],
                Color.White * fade,
                0f,
                Vector2.Zero,
                setScale,
                (netDirection.Value % 2 == 0 && netAlternative.Value == 3) || netDirection.Value == 3 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer
            );

            DrawShadow(b, localPosition, drawLayer);

        }

        public override void DrawWalk(SpriteBatch b, Vector2 localPosition, float drawLayer, float fade)
        {
            Vector2 spritePosition = localPosition + new Vector2(32, 64) - (new Vector2(16, 32) * setScale);

            if (netMovement.Value == (int)movements.run)
            {
                b.Draw(
                    characterTexture,
                    spritePosition,
                    runningFrames[netDirection.Value][moveFrame],
                    Color.White * fade,
                    0f,
                    Vector2.Zero,
                    setScale,
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
            DrawShadow(b, localPosition, drawLayer);
        }

        public override void DrawShadow(SpriteBatch b, Vector2 localPosition, float drawLayer)
        {

            float fade = fadeOut == 0 ? 0.35f : fadeOut * 0.35f;

            Vector2 shadowPosition = localPosition + new Vector2(32,56);

            if (netDirection.Value % 2 == 1)
            {
                shadowPosition.Y += 4;
            }

            b.Draw(Mod.instance.iconData.cursorTexture, shadowPosition, Mod.instance.iconData.shadowRectangle, Color.White * fade, 0.0f, new Vector2(24), setScale/2, 0, drawLayer - 0.0001f);

        }

        public override Rectangle GetBoundingBox()
        {

            if (netDirection.Value % 2 == 0)
            {

                return new Rectangle((int)Position.X + 8, (int)Position.Y + 8, 48, 48);

            }

            return new Rectangle((int)Position.X - 16, (int)Position.Y + 8, 96, 48);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {
            
            return base.SmashAttack(monster);

        }


    }

}
