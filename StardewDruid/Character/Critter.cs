
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using StardewDruid.Render;
using StardewDruid.Handle;

namespace StardewDruid.Character
{
    public class Critter : StardewDruid.Character.Character
    {

        CritterRender critterRender;

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

            critterRender = new(characterType.ToString());

            LoadIntervals();

            setScale = 3.75f;

            gait = 2.5f;

            modeActive = mode.random;

            idleFrames[idles.standby] = critterRender.idleFrames;

            idleFrames[idles.alert] = critterRender.idleFrames;

            walkFrames = critterRender.walkFrames;

            runningFrames = critterRender.runningFrames;

            dashFrames[dashes.dash] = critterRender.dashFrames;

            dashFrames[dashes.smash] = critterRender.dashFrames;

            specialFrames[specials.special] = critterRender.specialFrames;

            specialIntervals[specials.special] = 90;
            specialCeilings[specials.special] = 0;
            specialFloors[specials.special] = 0;

            specialFrames[specials.sweep] = critterRender.sweepFrames;

            specialIntervals[specials.sweep] = 15;
            specialCeilings[specials.sweep] = 3;
            specialFloors[specials.sweep] = 0;

            specialFrames[specials.invoke] = critterRender.specialFrames;

            specialIntervals[specials.invoke] = 90;
            specialCeilings[specials.invoke] = 0;
            specialFloors[specials.invoke] = 0;

            specialFrames[specials.greet] = critterRender.greetFrames;

            specialIntervals[specials.greet] = 30;
            specialCeilings[specials.greet] = 3;
            specialFloors[specials.greet] = 1;

            idleFrames[idles.standby] = critterRender.sitFrames;

            idleFrames[idles.rest] = critterRender.sleepFrames;

            loadedOut = true;
        
        }
        public override Vector2 SpritePosition(Vector2 localPosition)
        {

            float spriteScale = GetScale();

            int width = GetWidth();

            int height = GetHeight();

            Vector2 spritePosition = localPosition + new Vector2(32, 128) - new Vector2(0, height / 2 * spriteScale);

            if (netDash.Value > 0)
            {

                spritePosition.Y -= dashHeight;

            }

            return spritePosition;

        }

        public override void DrawWalk(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            if (netMovement.Value == (int)movements.run)
            {

                DrawRun(b, spritePosition, drawLayer, fade);

                return;

            }

            base.DrawWalk(b, spritePosition, drawLayer, fade);

        }

        public virtual void DrawRun(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            b.Draw(
                characterTexture,
                spritePosition,
                runningFrames[netDirection.Value][moveFrame],
                Color.White * fade,
                0f,
                new Vector2(GetWidth(),GetHeight()),
                setScale,
                (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer
            );

            DrawShadow(b, spritePosition, drawLayer, fade);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {
            
            return base.SmashAttack(monster);

        }

        public override Microsoft.Xna.Framework.Rectangle OverheadPortrait()
        {

            return new Rectangle(21, 13, 16, 16);

        }

    }

}
