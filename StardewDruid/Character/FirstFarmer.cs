using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Render;
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
using System.Reflection.Metadata.Ecma335;

namespace StardewDruid.Character
{
    public class FirstFarmer : StardewDruid.Character.Character
    {
        
        public Texture2D hatsTexture;

        public int hatsIndex;

        public FirstFarmer()
        {
        }

        public FirstFarmer(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {
            
            base.LoadOut();

            hatsTexture = Game1.content.Load<Texture2D>("Characters\\Farmer\\hats");

            hatsIndex = 55;

            WeaponLoadout();

            weaponRender.swordScheme = WeaponRender.swordSchemes.sword_warp;

        }

        public override void DrawHat(SpriteBatch b, Vector2 spritePosition,  float drawLayer, float fade)
        {


            int UseIndex = hatsIndex + 0;

            int hatOffset = 0;

            switch (netDirection.Value)
            {
                case 0:

                    UseIndex += 36;

                    break;

                case 1:

                    UseIndex += 12;

                    break;

                case 3:

                    UseIndex += 24;

                    hatOffset -= (int)(1 * setScale);

                    break;

            }

            if (netIdle.Value == (int)Character.idles.kneel)
            {

                b.Draw(
                    hatsTexture,
                    spritePosition - new Vector2(hatOffset, 6*setScale),
                    Game1.getSourceRectForStandardTileSheet(hatsTexture, UseIndex, 20, 20),
                    Color.White * fade,
                    0.0f,
                    new Vector2(10),
                    setScale,
                    SpriteAngle() ? (SpriteEffects)1 : 0,
                    drawLayer + 0.0001f
                );

            }
            else
            {

                b.Draw(
                    hatsTexture,
                    spritePosition - new Vector2(hatOffset, 13 * setScale),
                    Game1.getSourceRectForStandardTileSheet(hatsTexture, UseIndex, 20, 20),
                    Color.White * fade,
                    0.0f,
                    new Vector2(10),
                    setScale,
                    SpriteAngle() ? (SpriteEffects)1 : 0,
                    drawLayer + 0.0001f
                );

            }

        }

    }

}
