using StardewValley;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;
using StardewDruid.Cast;
using StardewDruid.Data;

namespace StardewDruid.Location
{
    public class Tunnel : DruidLocation
    {

        public Tunnel() { }

        public Tunnel(string Name)
            : base(Name,Path.Combine("Maps","Mines","77377"))
        {

        }

        protected override void _updateAmbientLighting()
        {

            Game1.ambientLight = new(48, 48, 24);

        }

        public override void updateMap()
        {

            Microsoft.Xna.Framework.Vector2 statueVector = new Vector2(30, 6) * 64;

            for (int x = 28; x < 32; x++)
            {

                for (int y = 4; y < 8; y++)
                {

                    if (isActionableTile(x, y, Game1.player))
                    {

                        foreach (Layer layer in map.Layers)
                        {

                            if (layer.Tiles[x, y] != null)
                            {

                                layer.Tiles[x, y] = new StaticTile(layer, layer.Tiles[x, y].TileSheet, BlendMode.Alpha, layer.Tiles[x, y].TileIndex);

                            }

                        }

                    };

                }

            }

            if (Game1.player.mailReceived.Contains("gotGoldenScythe"))
            {

                setMapTileIndex(29, 4, 245, "Front");

                setMapTileIndex(30, 4, 246, "Front");

                setMapTileIndex(29, 5, 261, "Front");

                setMapTileIndex(30, 5, 262, "Front");

                setMapTileIndex(29, 6, 277, "Buildings");

                setMapTileIndex(30, 56, 278, "Buildings");

            }

            /*Layer layer = map.RequireLayer("Buildings");

            for (int i = 0; i < layer.LayerHeight; i++)
            {
                for (int j = 0; j < layer.LayerWidth; j++)
                {
                    int tileIndexAt = layer.GetTileIndexAt(j, i);

                    if (tileIndexAt != -1)
                    {

                        // Add lights
                        if (tileIndexAt == 97 || tileIndexAt == 113 || tileIndexAt == 65 || tileIndexAt == 66 || tileIndexAt == 81 || tileIndexAt == 82 || tileIndexAt == 48)
                        {
                            
                            sharedLights[j + i * 1000] = new LightSource(4, new Vector2(j, i) * 64f, 2.5f, new Color(0, 50, 100), j + i * 1000, LightSource.LightContext.None, 0L);
                            
                            switch (tileIndexAt)
                            {
                                case 66:
                                    lightGlows.Add(new Vector2(j, i) * 64f + new Vector2(0f, 64f));
                                    break;
                                case 97:
                                case 113:
                                    lightGlows.Add(new Vector2(j, i) * 64f + new Vector2(32f, 32f));
                                    break;
                            
                            }
                        
                        }
                    
                    }

                    bool flag = false;

                    // Remove action tiles
                    string[] array = ArgUtility.SplitBySpace(doesTileHaveProperty(j, i, "Action", "Buildings"));

                    if (!ShouldIgnoreAction(array, Game1.player, new xTile.Dimensions.Location(j, i)))
                    {
                        switch (array[0])
                        {
                            case "Dialogue":
                            case "Message":
                            case "MessageOnce":
                            case "NPCMessage":
                                flag = true;
                                break;
                            case "MessageSpeech":
                                flag = true;
                                break;
                            default:
                                flag = true;
                                break;
                        }
                    }

                    if (flag)
                    {

                        if (layer.Tiles[j, i] != null)
                        {

                            layer.Tiles[j, i] = new StaticTile(layer, layer.Tiles[j, i].TileSheet, BlendMode.Alpha, layer.Tiles[j, i].TileIndex);

                        }

                    }

                }

            }*/
        }

    }

}
