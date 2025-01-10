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

namespace StardewDruid.Location.Druid
{
    public class Tunnel : DruidLocation
    {

        public Tunnel() { }

        public Tunnel(string Name)
            : base(Name, Path.Combine("Maps", "Mines", "77377"))
        {

        }

        protected override void _updateAmbientLighting()
        {

            Game1.ambientLight = new(48, 48, 24);

        }

        public override void updateMap()
        {

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

                setMapTile(29, 4, 245, "Front", "mine");
                setMapTile(30, 4, 246, "Front", "mine");
                setMapTile(29, 5, 261, "Front", "mine");
                setMapTile(30, 5, 262, "Front", "mine");
                setMapTile(29, 6, 277, "Buildings", "mine");
                setMapTile(30, 56, 278, "Buildings", "mine");

            }

        }

    }

}
