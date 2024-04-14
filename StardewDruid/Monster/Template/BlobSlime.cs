using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;

using StardewModdingAPI;
using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;

namespace StardewDruid.Monster.Template
{
    public class BlobSlime : StardewDruid.Monster.Template.BigSlime
    {

        public BlobSlime()
        {

        }

        public BlobSlime(Vector2 tile, int combatModifier)
            : base(tile, combatModifier * 10)
        {

        }

        public override void LoadOut()
        {

            hatsTexture = Game1.content.Load<Texture2D>("Characters\\Farmer\\hats");

            hatSourceRect = Game1.getSourceRectForStandardTileSheet(hatsTexture, 200, 20, 20);

            dropHat = true;

            loadedout = true;

            slimeColor = Color.LightBlue * 0.7f;

        }

    }

}
