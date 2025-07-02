using Microsoft.Xna.Framework;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewModdingAPI;
using StardewDruid.Event.Sword;
using StardewDruid.Journal;
using StardewValley.Locations;
using xTile.Tiles;
using xTile;
using System.IO;
using StardewDruid.Handle;

namespace StardewDruid.Location
{
    public class DruidLocation : GameLocation
    {

        public List<Location.TerrainField> terrainFields = new();

        public List<Location.TerrainField> grassFields = new();

        public List<Location.TerrainField> floorFields = new();

        public List<Location.LightField> lightFields = new();

        public Dictionary<Vector2, CharacterHandle.characters> dialogueTiles = new();

        public bool internalDarkness;

        public bool ambientDarkness;

        public int ambientFactor;

        public bool summonActive;

        public int restoration;

        public bool seasonalGround;

        public const int barrierIndex = 360;

        public const int cavernIndex = 120;

        public DruidLocation()
        {

        }

        public DruidLocation(string name)
            : base("Maps\\Shed",name)
        {

        }

        public DruidLocation(string name, string baseName)
            : base(baseName, name)
        {

        }

        public void mapReset()
        {

            terrainFields = new();

            grassFields = new();

            lightFields = new();

            dialogueTiles = new();

        }

        public virtual void RestoreTo(int restore)
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

                    ambientFactor = 0;

                }
                else
                if (ambientFactor < 192)
                {

                    ambientFactor++;

                }

            }
            else
            {

                if (ambientFactor > 0)
                {

                    ambientFactor--;

                }

            }

            base._updateAmbientLighting();

            if (ambientFactor > 0)
            {

                int factor = (int)ambientFactor / 6;

                Color ambientColour = Game1.ambientLight;

                int R = Math.Min(ambientColour.R, 5 * factor);

                int G = Math.Min(ambientColour.G, 5 * factor);

                int B = Math.Min(ambientColour.B, 4 * factor);

                Game1.ambientLight = new(R, G, B);

            }

            return;

        }

        public override void draw(SpriteBatch b)
        {
            
            base.draw(b);

            foreach (TerrainField tile in terrainFields)
            {

                tile.draw(b, this);

            }

            foreach (TerrainField tile in grassFields)
            {

                tile.draw(b, this);

            }

        }

        public override void drawFloorDecorations(SpriteBatch b)
        {

            foreach (TerrainField tile in floorFields)
            {

                tile.draw(b, this);

            }

            base.drawFloorDecorations(b);

        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {

            foreach (TerrainField tile in terrainFields)
            {

                tile.drawFront(b, this);

            }

            base.drawAboveAlwaysFrontLayer(b);

        }

        // Overridden methods

        public override bool CanItemBePlacedHere(Vector2 tile, bool itemIsPassable = false, CollisionMask collisionMask = CollisionMask.All, CollisionMask ignorePassables = ~CollisionMask.Objects, bool useFarmerTile = false, bool ignorePassablesExactly = false)
        {

            if(Name == LocationHandle.druid_grove_name)
            {
                
                if (Mod.instance.Config.decorateGrove)
                {

                    return base.CanItemBePlacedHere(tile, itemIsPassable, collisionMask, ignorePassables, useFarmerTile, ignorePassablesExactly);

                }

            }

            return false;

        }

        public override bool isActionableTile(int xTile, int yTile, Farmer who)
        {

            Vector2 actionTile = new(xTile, yTile);

            if (dialogueTiles.ContainsKey(actionTile) && Mod.instance.activeEvent.Count == 0)
            {

                return readyDialogue();

            }

            return base.isActionableTile(xTile, yTile, who);

        }

        public virtual bool readyDialogue()
        {

            return true;

        }

        public virtual void addDialogue()
        {


        }

        public override void tryToAddCritters(bool onlyIfOnScreen = false)
        {


        }

        public override bool checkAction(xTile.Dimensions.Location tileLocation, xTile.Dimensions.Rectangle viewport, Farmer who)
        {

            Vector2 actionTile = new(tileLocation.X, tileLocation.Y);

            if (readyDialogue())
            {

                if (dialogueTiles.ContainsKey(actionTile) && Mod.instance.activeEvent.Count == 0)
                {

                    CharacterHandle.characters characterType = dialogueTiles[actionTile];

                    if (!Mod.instance.dialogue.ContainsKey(characterType))
                    {

                        Mod.instance.dialogue[characterType] = new(characterType);

                    }

                    Mod.instance.dialogue[characterType].DialogueApproach();

                    return true;

                }

            }

            return base.checkAction(tileLocation, viewport, who);

        }

        public override bool isTileFishable(int tileX, int tileY)
        {

            if (Mod.instance.eventRegister.ContainsKey(Rite.eventFishspot))
            {

                if (Mod.instance.eventRegister[Rite.eventFishspot].EventActive())
                {

                    return ModUtility.WaterCheck(this, new(tileX, tileY), 1);

                }

            }

            if (Mod.instance.Helper.ModRegistry.IsLoaded("shekurika.WaterFish"))
            {

                return false;

            }

            return ModUtility.WaterCheck(this, new(tileX, tileY), 1);

        }

        public override void updateSeasonalTileSheets(Map map = null)
        {

            if (!seasonalGround)
            {

                base.updateSeasonalTileSheets(map);

                return;

            }

            if (map == null)
            {

                map = Map;

            }

            map.DisposeTileSheets(Game1.mapDisplayDevice);

            foreach (TileSheet tileSheet in map.TileSheets)
            {
                
                string imageSource = tileSheet.ImageSource;

                switch (imageSource)
                {

                    case "StardewDruid.Tilesheets.groundspring":
                    case "StardewDruid.Tilesheets.groundsummer":
                    case "StardewDruid.Tilesheets.groundautumn":
                    case "StardewDruid.Tilesheets.groundwinter":

                        switch (GetSeason())
                        {

                            default:

                                tileSheet.ImageSource = "StardewDruid.Tilesheets.groundspring";

                                break;

                            case Season.Summer:

                                tileSheet.ImageSource = "StardewDruid.Tilesheets.groundsummer";

                                break;

                            case Season.Fall:

                                tileSheet.ImageSource = "StardewDruid.Tilesheets.groundautumn";

                                break;

                            case Season.Winter:

                                tileSheet.ImageSource = "StardewDruid.Tilesheets.groundwinter";

                                break;

                        }

                        try
                        {

                            Game1.mapDisplayDevice.LoadTileSheet(tileSheet);

                        }
                        catch (Exception exception)
                        {

                            Mod.instance.Monitor.Log(exception.Message);

                            tileSheet.ImageSource = imageSource;

                        }

                        break;

                }

            }

            map.LoadTileSheets(Game1.mapDisplayDevice);

        }

    }

}
