
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using StardewDruid.Render;

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

            overhead = 112;

            setScale = 3.75f;

            gait = 2.5f;

            modeActive = mode.random;

            idleFrames[idles.standby] = critterRender.idleFrames;

            idleFrames[idles.alert] = critterRender.idleFrames;

            walkFrames = critterRender.walkFrames;

            runningFrames = critterRender.runningFrames;

            specialFrames[specials.special] = critterRender.specialFrames;

            specialFrames[specials.invoke] = critterRender.specialFrames;

            specialFrames[specials.sweep] = critterRender.sweepFrames;

            dashFrames[dashes.dash] = critterRender.dashFrames;

            dashFrames[dashes.smash] = critterRender.dashFrames;

            specialIntervals[specials.invoke] = 90;
            specialCeilings[specials.invoke] = 0;
            specialFloors[specials.invoke] = 0;

            specialIntervals[specials.special] = 30;
            specialCeilings[specials.special] = 1;
            specialFloors[specials.special] = 1;

            specialIntervals[specials.special] = 15;
            specialCeilings[specials.special] = 2;
            specialFloors[specials.special] = 0;

            loadedOut = true;
        
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
                new Vector2(16),
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


    }

}
