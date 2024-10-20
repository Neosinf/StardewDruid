
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using StardewDruid.Render;

namespace StardewDruid.Character
{
    public class Barker : StardewDruid.Character.Character
    {

        public Barker()
        {
        }

        public Barker(CharacterHandle.characters type = CharacterHandle.characters.GreyWolf)
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

            walkFrames = WolfRender.WalkFrames();

            dashFrames = new()
            {
                [dashes.dash] = WolfRender.DashFrames(),
                [dashes.smash] = WolfRender.DashFrames(),
            };

            specialFrames = new()
            {
                [specials.special] = WolfRender.SpecialFrames(),
                [specials.sweep] = WolfRender.SweepFrames(),
            };

            specialIntervals = new()
            {
                [specials.special] = 12,
                [specials.sweep] = 9,
            };

            specialFloors = new()
            {
                [specials.special] = 0,
                [specials.sweep] = 0,
            };

            specialCeilings = new()
            {
                [specials.special] = 0,
                [specials.sweep] = 5,
            };

            loadedOut = true;

        }

        public override bool SpriteAngle()
        {

            return SpriteFlip();

        }

        public override void DrawShadow(SpriteBatch b, Vector2 spritePosition, float drawLayer)
        {

            Vector2 shadowPosition = spritePosition + new Vector2(0, setScale * 24);

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
                (setScale * 0.8f) + (Math.Abs(0 - (walkFrames[0].Count() / 2) + moveFrame) * 0.05f),
                0,
                drawLayer - 0.0001f
            );

        }

    }

}
