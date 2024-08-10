using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Projectiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Layers;
using xTile.Tiles;
using static StardewDruid.Cast.SpellHandle;
using static StardewDruid.Data.IconData;

namespace StardewDruid.Event.Access
{
    public class AccessDoor : AccessHandle
    {

        public TerrainTile terrainTile;

        public AccessDoor() { }

        public override void AccessQuery()
        {

            queryHandle = QueryData.queries.AccessDoor;

            base.AccessQuery();

        }

        public override void AccessClear()
        {

            int tilex = (int)start.X;

            int tiley = (int)start.Y;

            Layer buildings = location.map.GetLayer("Buildings");

            buildings.Tiles[tilex, tiley] = null;

            buildings.Tiles[tilex + 1, tiley] = null;

            buildings.Tiles[tilex + 2, tiley] = null;

            buildings.Tiles[tilex, tiley -1] = null;

            buildings.Tiles[tilex + 1, tiley -1] = null;

            buildings.Tiles[tilex + 2, tiley -1] = null;

            for(int w = location.warps.Count - 1; w >= 0; w--)
            {

                Warp warp = location.warps.ElementAt(w);

                if (warp.X == start.X && (warp.Y == start.Y || warp.Y == start.Y + 1))
                {

                    location.warps.RemoveAt(w);

                    continue;

                }

            }

            if (Utility.isOnScreen(start * 64, 64))
            {

                location.playSound(SpellHandle.sounds.secret1.ToString());

            }

        }

        public override void AccessStart(bool animate = true)
        {

            terrainTile = new(IconData.tilesheets.engineum,3,start*64);

            if (Context.IsMultiplayer && Context.IsMainPlayer)
            {

                AccessQuery();

            }

            if (Utility.isOnScreen(start * 64, 64) && animate)
            {

                ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 600);

                location.playSound(SpellHandle.sounds.secret1.ToString());

            }

        }

        public override void AccessWarps()
        {

            int tilex = (int)start.X;

            int tiley = (int)start.Y;

            location.warps.Add(new Warp(tilex, tiley, access, (int)exit.X, (int)exit.Y, flipFarmer: false));

            location.warps.Add(new Warp(tilex + 1, tiley , access, (int)exit.X, (int)exit.Y, flipFarmer: false));

            location.warps.Add(new Warp(tilex + 2, tiley, access, (int)exit.X, (int)exit.Y, flipFarmer: false));

        }

    }

}
