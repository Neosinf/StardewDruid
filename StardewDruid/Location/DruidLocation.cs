﻿using StardewDruid.Character;
using Microsoft.Xna.Framework;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;

namespace StardewDruid.Location
{
    public class DruidLocation : GameLocation
    {

        public List<Location.WarpTile> warpSets = new();

        public List<Location.TerrainTile> terrainTiles = new();

        public Dictionary<Vector2, CharacterHandle.characters> dialogueTiles = new();

        public List<Location.LightField> lightFields = new();

        public Dictionary<Vector2, Location.CrateTile> crateTiles = new();

        public bool internalDarkness;

        public bool ambientDarkness;

        public DruidLocation()
        {

        }

        public DruidLocation(string name)
            : base("Maps\\Shed",name)
        {


        }
        protected override void _updateAmbientLighting()
        {

            if (internalDarkness)
            {

                Game1.ambientLight = new(64, 64, 32);

                return;

            }

            if (ambientDarkness)
            {

                if (Game1.player.currentLocation.Name != Name)
                {

                    ambientDarkness = false;

                }
                else
                {

                    Game1.ambientLight = new(192, 192, 160);

                }

                return;

            }

            base._updateAmbientLighting();

        }

        public override void draw(SpriteBatch b)
        {
            
            base.draw(b);

            foreach (TerrainTile tile in terrainTiles)
            {

                tile.draw(b, this);

            }

            foreach (Location.LightField light in lightFields)
            {

                light.draw(b);

            }

            foreach (KeyValuePair<Vector2,Location.CrateTile> crate in crateTiles)
            {

                crate.Value.draw(b);

            }

        }

        // Overridden methods

        public override bool CanItemBePlacedHere(Vector2 tile, bool itemIsPassable = false, CollisionMask collisionMask = CollisionMask.All, CollisionMask ignorePassables = ~CollisionMask.Objects, bool useFarmerTile = false, bool ignorePassablesExactly = false)
        {

            return false;

        }

        public override bool isActionableTile(int xTile, int yTile, Farmer who)
        {

            Vector2 actionTile = new(xTile, yTile);

            if (dialogueTiles.ContainsKey(actionTile) && Mod.instance.activeEvent.Count == 0)
            {

                return true;

            }

            if (crateTiles.ContainsKey(actionTile))
            {

                if (!crateTiles[actionTile].empty)
                {

                    return true;

                }

            }

            return base.isActionableTile(xTile, yTile, who);

        }

        public override bool checkAction(xTile.Dimensions.Location tileLocation, xTile.Dimensions.Rectangle viewport, Farmer who)
        {

            Vector2 actionTile = new(tileLocation.X, tileLocation.Y);

            if (dialogueTiles.ContainsKey(actionTile))
            {

                CharacterHandle.characters characterType = dialogueTiles[actionTile];

                if (!Mod.instance.dialogue.ContainsKey(characterType))
                {

                    Mod.instance.dialogue[characterType] = new(characterType);

                }

                Mod.instance.dialogue[characterType].DialogueApproach();

                return true;

            }

            if (crateTiles.ContainsKey(actionTile))
            {
                
                if (crateTiles[actionTile].empty)
                {

                    return false;

                }

                crateTiles[actionTile].open(this);

                return true;

            }

            return base.checkAction(tileLocation, viewport, who);

        }

        public virtual void addDialogue()
        {


        }

    }

}
