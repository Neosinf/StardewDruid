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
    public class Serpent: StardewDruid.Character.Character
    {

        public SerpentRender serpentRender;

        public Serpent()
        {
        }

        public Serpent(CharacterHandle.characters type = CharacterHandle.characters.Serpent)
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

            serpentRender = new(characterType);

            LoadIntervals();

            setScale = 4f;

            gait = 1.8f;

            modeActive = mode.random;

            idleFrames[idles.idle] = serpentRender.walkFrames;

            walkFrames = serpentRender.walkFrames;

            specialFrames[specials.special] = serpentRender.specialFrames[specials.special];

            specialFrames[specials.sweep] = serpentRender.specialFrames[specials.special];

            dashFrames[dashes.dash] = serpentRender.dashFrames[dashes.dash];

            dashFrames[dashes.smash] = serpentRender.dashFrames[dashes.dash];

            loadedOut = true;

        }

        public override void draw(SpriteBatch b, float alpha = -1f)
        {
            
        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128) || characterTexture == null)
            {
                return;
            }

            DrawEmote(b);

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            Vector2 usePosition = SpritePosition(localPosition);

            DrawCharacter(b, usePosition);

        }

        public override void DrawCharacter(SpriteBatch b, Vector2 usePosition)
        {

            SerpentRenderAdditional hoverAdditional = new()
            {
                scale = setScale,

                position = usePosition,

                layer = (float)StandingPixel.Y / 10000f + 0.001f,

                flip = SpriteFlip(),

                fade = fadeOut,

                direction = netDirection.Value,

                series = SerpentRenderAdditional.serpentseries.none,
            };

            if (netDash.Value != 0)
            {

                hoverAdditional.direction = netDirection.Value + (netDashProgress.Value * 4);

                hoverAdditional.frame = Math.Min(dashFrame, (dashFrames[(dashes)netDash.Value][hoverAdditional.direction].Count - 1));

                hoverAdditional.series = SerpentRenderAdditional.serpentseries.dash;

            }
            else if (netSpecial.Value != 0)
            {

                hoverAdditional.frame = specialFrame;

                hoverAdditional.series = SerpentRenderAdditional.serpentseries.special;

            }
            else
            if ((movements)netMovement.Value == movements.run)
            {

                hoverAdditional.series = SerpentRenderAdditional.serpentseries.tackle;

                hoverAdditional.frame = (int)((Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 2000) / 500);

            }

            serpentRender.DrawNormal(b,hoverAdditional);

        }

        public override int GetWidth()
        {

            return 64;

        }

        public override void normalUpdate(GameTime time, GameLocation location)
        {

            base.normalUpdate(time, location);

            serpentRender.Update(pathActive == pathing.none);

        }

        public override bool SweepAttack(StardewValley.Monsters.Monster monster)
        {

            return SmashAttack(monster);

        }

        public override Microsoft.Xna.Framework.Rectangle OverheadPortrait()
        {

            return new Rectangle(48, 16, 16, 16);

        }

    }

}
