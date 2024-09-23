
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Character
{
    public class Growler : StardewDruid.Character.Character
    {

        public Growler()
        {
        }

        public Growler(CharacterHandle.characters type = CharacterHandle.characters.Shadowbear)
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

            walkFrames = FrameSeries(64, 64, 0, 0, 7);

            walkFrames[3] = new(walkFrames[1]);

            loadedOut = true;

        }

        public override bool SpriteAngle()
        {

            return SpriteFlip();

        }

        public override void DrawShadow(SpriteBatch b, Vector2 localPosition, float drawLayer)
        {

            float fade = fadeOut == 0 ? 0.35f : fadeOut * 0.35f;

            Vector2 shadowPosition = localPosition + new Vector2(32, 96);

            float offset = setScale + (Math.Abs(0 - (walkFrames[0].Count() / 2) + moveFrame) * 0.1f);

            if (netDirection.Value % 2 == 1)
            {

                shadowPosition.Y += 4;

            }

            b.Draw(Mod.instance.iconData.cursorTexture, shadowPosition, Mod.instance.iconData.shadowRectangle, Color.White*fade, 0.0f, new Vector2(24), 3f, 0, drawLayer - 0.0001f);

        }

        public override Rectangle GetBoundingBox()
        {

            if (netDirection.Value % 2 == 0)
            {

                return new Rectangle((int)Position.X -16, (int)Position.Y - 16, 96, 80);

            }

            return new Rectangle((int)Position.X - 24, (int)Position.Y - 16, 112, 80);

        }

    }

}
