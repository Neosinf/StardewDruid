using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Location;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Event.Access
{
    public class AccessHandle : EventHandle
    {

        public Vector2 stair;

        public Vector2 exit;

        public string entrance;

        public string access;

        public bool manualSet;

        public AccessHandle() { }

        public void AccessSetup(string Entrance, string Access, Vector2 Stair, Vector2 Exit)
        {

            triggerEvent = true;

            stair = Stair;

            entrance = Entrance;

            access = Access;

            exit = Exit;

            origin = (stair - new Vector2(6,0)) * 64;

            eventId = entrance + "_" + access;

            Mod.instance.RegisterEvent(this, eventId);

        }

        public override bool TriggerLocation()
        {
 
            if (Game1.player.currentLocation.Name == entrance)
            {

                return true;

            }

            return false;

        }

        public override bool TriggerCheck()
        {

            if (Vector2.Distance(Game1.player.Position, origin) > (3 * 64))
            {

                return false;

            }

            if (Vector2.Distance(Game1.player.Position, stair * 64) < 192)
            {

                return false;

            }

            if (!CheckStair())
            {

                ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 600);

                Mod.instance.iconData.ImpactIndicator(location, stair * 64 + new Vector2(64, 0), IconData.impacts.impact, 6, new());

                location.playSound("boulderBreak");

                AccessStair();

                AccessWarps();

            }

            return true;

        }

        public virtual bool CheckStair()
        {

            foreach (Warp warp in location.warps)
            {

                if (warp.X == stair.X)
                {

                    return true;

                }

            }

            return false;

        }

        public override void TriggerInterval()
        {

            if (CheckStair()) {  
                
                return; 
            
            }

            triggerCounter++;

            TemporaryAnimatedSprite targetAnimation = Mod.instance.iconData.AnimateTarget(location, origin, IconData.schemes.grannysmith, triggerCounter % 6);

            animations.Add(targetAnimation);

            return;

        }

        public void AccessStair()
        {

            TileSheet tileSheet = new(
                location.map,
                IconData.chapel_assetName,
                new(
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.chapel].Width / 16,
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.chapel].Height / 16
                ),
                new(16, 16)
            );

            location.map.AddTileSheet(tileSheet);

            location.map.LoadTileSheets(Game1.mapDisplayDevice);

            int tilex = (int)stair.X;

            int tiley = (int)stair.Y;

            Layer buildings = location.map.GetLayer("Buildings");

            buildings.Tiles[tilex, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 56);

            buildings.Tiles[tilex, tiley].TileIndexProperties.Add("Passable", new(true));

            buildings.Tiles[tilex + 1, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 57);

            buildings.Tiles[tilex + 2, tiley] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 58);

            buildings.Tiles[tilex, tiley + 1] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 70);

            buildings.Tiles[tilex, tiley + 1].TileIndexProperties.Add("Passable", new(true));

            buildings.Tiles[tilex + 1, tiley + 1] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 71);

            buildings.Tiles[tilex + 2, tiley + 1] = new StaticTile(buildings, tileSheet, BlendMode.Alpha, 72);


        }

        public void AccessWarps()
        {

            int tilex = (int)stair.X;

            int tiley = (int)stair.Y;

            location.warps.Add(new Warp(tilex, tiley, access, (int)exit.X, (int)exit.Y, flipFarmer: false));

            location.warps.Add(new Warp(tilex, tiley + 1, access, (int)exit.X, (int)exit.Y, flipFarmer: false));

        }

    }

}
