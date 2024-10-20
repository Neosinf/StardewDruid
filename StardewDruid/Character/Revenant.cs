using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.FruitTrees;
using StardewValley.Internal;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
    public class Revenant : Effigy
    {

        public Revenant()
        {


        }

        public Revenant(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {
            base.LoadOut();

            WeaponLoadout();

            //weaponRender.swordScheme = IconData.schemes.stars;

            idleFrames[idles.standby] = new()
            {
                [0] = new(){ new(192, 0, 32, 32), },
                [1] = new() { new(192, 0, 32, 32), },
                [2] = new() { new(192, 0, 32, 32), },
                [3] = new() { new(192, 0, 32, 32), },
            };

            restSet = true;

            idleFrames[idles.rest] = new()
            {
                [0] = new() { new(0, 64, 32, 32), },

            };


        }

        public override void DrawLaunch(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {
            Rectangle useFrame = specialFrames[specials.launch][netDirection.Value][specialFrame];

            b.Draw(
                characterTexture,
                spritePosition,
                useFrame,
                Color.White * fade,
                0.0f,
                new Vector2(useFrame.Width / 2, useFrame.Height / 2),
                setScale,
                SpriteFlip() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawShadow(b, spritePosition, drawLayer);

        }

        public override void DrawDash(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            if (netDash.Value != (int)dashes.smash)
            {

                base.DrawDash(b, spritePosition, drawLayer, fade);

                return;

            }

            int dashSeries = netDirection.Value + (netDashProgress.Value * 4);

            int dashSetto = Math.Min(dashFrame, (dashFrames[(dashes)netDash.Value][dashSeries].Count - 1));

            Vector2 dashVector = spritePosition - new Vector2(0, dashHeight);

            Rectangle dashTangle = dashFrames[(dashes)netDash.Value][dashSeries][dashSetto];

            b.Draw(
                characterTexture,
                dashVector,
                dashTangle,
                Color.White * fade,
                0f,
                new Vector2(16),
                setScale,
                SpriteFlip() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawShadow(b, spritePosition, drawLayer);

            weaponRender.DrawWeapon(b, dashVector - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = dashTangle, flipped = SpriteFlip() });

            if (netDashProgress.Value >= 2)
            {

                weaponRender.DrawSwipe(b, dashVector - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = dashTangle, flipped = SpriteFlip() });

            }

        }

        public override void DrawSweep(SpriteBatch b, Vector2 sweepVector, float drawLayer, float fade)
        {

            Rectangle sweepFrame = specialFrames[(specials)netSpecial.Value][netDirection.Value][specialFrame];

            b.Draw(
                characterTexture,
                sweepVector,
                sweepFrame,
                Color.White * fade,
                0.0f,
                new Vector2(16),
                setScale,
                0,
                drawLayer
            );

            DrawShadow(b, sweepVector, drawLayer);

            weaponRender.DrawWeapon(b, sweepVector - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = sweepFrame, });

            weaponRender.DrawSwipe(b, sweepVector - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = sweepFrame, });

        }

        public override void DrawAlert(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            Rectangle alertFrame = idleFrames[idles.alert][netDirection.Value][0];

            b.Draw(
                 characterTexture,
                 spritePosition,
                 alertFrame,
                 Color.White * fade,
                 0f,
                 new Vector2(16),
                 setScale,
                 SpriteAngle() ? (SpriteEffects)1 : 0,
                 drawLayer
             );

            DrawShadow(b, spritePosition, drawLayer);

            weaponRender.DrawWeapon(b, spritePosition - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = alertFrame, flipped = SpriteAngle() });

        }

        public override mode SpecialMode(mode modechoice)
        {

            switch (modechoice)
            {

                case mode.home:

                case mode.random:

                case mode.roam:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.questRevenant))
                    {

                        return mode.limbo;

                    }

                    break;

            }

            return modechoice;

        }

    }

}
