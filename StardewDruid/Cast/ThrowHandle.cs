using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.Minecarts;
using StardewValley.Inventories;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Tools;
using System;
using System.Diagnostics.Metrics;
using System.Reflection;
using xTile.Dimensions;
using static System.Net.Mime.MediaTypeNames;

namespace StardewDruid.Cast
{
    public class ThrowHandle
    {

        public StardewValley.Item item;

        public StardewValley.Tool tooling;

        public string localisation;

        public TemporaryAnimatedSprite animation;

        public int counter;

        public int quality;

        public int delay;

        public Vector2 origin;

        public Vector2 destination;

        public float scale = 3f;

        public float fade;

        public float rotation;

        public bool track;

        public bool queried;

        public enum throwing
        {
            item,
            sword,
            relic,
        }

        public throwing thrown;

        public int timeframe = 60;

        public int height = 192;

        public int index;

        public bool pocket;

        public bool complete;

        public IconData.impacts impact = IconData.impacts.none;

        public ThrowHandle()
        {

        }

        public ThrowHandle(Farmer Player, Vector2 Origin, int index, int Quality = 0)
        {

            localisation = Player.currentLocation.Name;

            thrown = throwing.item;

            setQuality(Quality);

            item = new StardewValley.Object(index.ToString(), 1, false, -1, quality);

            origin = Origin;

            destination = Game1.player.Position + new Vector2(32, 0);

            pocket = true;

            track = true;

        }

        public ThrowHandle(Vector2 Origin, Vector2 Destination, int index, int Quality = 0)
        {
            
            localisation = Game1.player.currentLocation.Name;

            thrown = throwing.item;

            setQuality(Quality);

            item = new StardewValley.Object(index.ToString(), 1, false, -1, quality);

            origin = Origin;

            destination = Destination;

        }

        public ThrowHandle(Farmer Player, Vector2 Origin, StardewValley.Item Item)
        {

            localisation = Player.currentLocation.Name;

            item = Item;

            thrown = throwing.item;

            destination = Game1.player.Position + new Vector2(32, 0);

            origin = Origin;

            pocket = true;

            track = true;

        }

        public ThrowHandle(Vector2 Origin, Vector2 Destination, StardewValley.Item Item)
        {

            localisation = Game1.player.currentLocation.Name;

            item = Item;

            thrown = throwing.item;

            origin = Origin;

            destination = Destination;

        }

        public ThrowHandle(Farmer Player, Vector2 Origin, IconData.relics relic)
        {

            localisation = Player.currentLocation.Name;

            thrown = throwing.relic;

            origin = Origin;

            destination = Game1.player.Position + new Vector2(32, 0);

            index = (int)relic;

            pocket = true;

            Mod.instance.relicsData.ReliquaryUpdate(relic.ToString());

            track = true;

        }

        public ThrowHandle(Vector2 Origin, Vector2 Destination, IconData.relics relic)
        {

            localisation = Game1.player.currentLocation.Name;

            thrown = throwing.relic;

            origin = Origin;

            destination = Destination;

            index = (int)relic;

        }

        public ThrowHandle(Farmer Player, Vector2 Origin, SpawnData.swords sword)
        {

            localisation = Player.currentLocation.Name;

            index = (int)sword;

            tooling = SpawnData.SpawnSword(sword);

            thrown = throwing.sword;

            destination = Game1.player.Position + new Vector2(32, 0);

            origin = Origin;

            pocket = true;

            track = true;

        }

        public void register()
        {

            if(thrown == throwing.sword)
            {

                if(tooling == null)
                {

                    return;

                }

            }

            Mod.instance.throwRegister.Add(this);

        }


        public void setQuality(int Quality)
        {

            quality = Quality;

            if (quality == 3)
            {

                quality = 2;

            }
            else if (quality > 4)
            {

                quality = 4;

            }
            else if (quality < 0)
            {

                quality = 0;

            }

        }

        public void offset()
        {
            /* 
             * animation = drawn object (thrown)
             * counter = ticks
             * timeframe = total ticks
             * height = peak
             * origin = coordinates pixels start
             * destination = coordinates pixels end - player position at tick - can change from acceleration/momentum from gameworld actions
             */

            float distance = Vector2.Distance(origin, destination);

            float length =  distance / 2;

            float lengthSq = (length * length);

            float heightFr = 4 * height;

            float coefficient =  lengthSq / heightFr;

            int midpoint = (timeframe / 2);

            float newHeight = 0;

            if (counter != midpoint)
            {

                float newLength;

                if (counter < midpoint)
                {

                    newLength = length * (midpoint - counter) / midpoint;

                }
                else
                {

                    newLength = (length * (counter- midpoint) / midpoint);

                }

                float newLengthSq = newLength * newLength;

                float coefficientFr = (4 * coefficient);

                newHeight = newLengthSq / coefficientFr;

            }

            Vector2 shift = (destination - origin) * counter / timeframe;

            animation.position = origin + shift - new Vector2(0, height-newHeight);
        
        }

        public void ThrowQuery()
        {
            queried = true;

            if(thrown == throwing.item)
            {

                return;

            }

            if (!Context.IsMultiplayer)
            {

                return;

            }

            if (!Context.IsMainPlayer)
            {

                return;

            }

            if (!pocket)
            {

                return;

            }

            QueryData query;
            
            switch (thrown)
            {

                case throwing.sword:

                    query = new()
                    {
                        name = index.ToString(),

                        location = localisation,

                    };

                    Mod.instance.EventQuery(query, QueryData.queries.ThrowSword);

                    break;

                case throwing.relic:

                    query = new()
                    {
                        name = index.ToString(),

                        location = localisation,

                    };

                    Mod.instance.EventQuery(query, QueryData.queries.ThrowRelic);

                    break;


            }


        }

        public bool update()
        {

            if (!queried)
            {

                ThrowQuery();

            }

            if (complete)
            {

                return false;

            }

            if(delay > 0)
            {

                delay--;

                return true;

            }

            if(localisation != Game1.player.currentLocation.Name)
            {

                if (pocket)
                {
                    
                    pocket = false;

                    inventorise();

                }

                complete = true;

                return false;

            }

            if (counter == 0)
            {

                switch (thrown)
                {

                    case throwing.item:

                        ParsedItemData dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(item.QualifiedItemId);

                        Microsoft.Xna.Framework.Rectangle itemRect = dataOrErrorItem.GetSourceRect(0, item.ParentSheetIndex);

                        animation = new(0, timeframe * 16.66f, 1, 1, origin+new Vector2(8,8), false, false)
                        {
                            sourceRect = itemRect,
                            sourceRectStartingPos = new(itemRect.X, itemRect.Y),
                            texture = dataOrErrorItem.GetTexture(),
                            layerDepth = origin.Y / 10000f,
                            alphaFade = fade,
                            rotationChange = rotation,
                            scale = 3.5f,

                        };

                        Game1.player.currentLocation.TemporarySprites.Add(animation);

                        break;

                    case throwing.relic:

                        Microsoft.Xna.Framework.Rectangle relicRect = IconData.RelicRectangles((IconData.relics)index);

                        animation = new(0, timeframe * 16.66f, 1, 1, origin + new Vector2(2,2), false, false)
                        {
                            sourceRect = relicRect,
                            sourceRectStartingPos = new(relicRect.X,relicRect.Y),
                            texture = Mod.instance.iconData.relicsTexture,
                            layerDepth = 900f,
                            rotationChange = rotation,
                            scale = 3f,

                        };

                        Game1.player.currentLocation.TemporarySprites.Add(animation);

                        break;

                    case throwing.sword:

                        dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(tooling.QualifiedItemId);
    
                        itemRect = dataOrErrorItem.GetSourceRect();

                        animation = new(0, timeframe * 16.66f, 1, 1, origin + new Vector2(8, 8), false, false)
                        {
                            sourceRect = itemRect,
                            sourceRectStartingPos = new(itemRect.X, itemRect.Y),
                            texture = dataOrErrorItem.GetTexture(),
                            layerDepth = origin.Y / 10000f,
                            alphaFade = fade,
                            rotationChange = 0.2f,
                            scale = 4f,

                        };

                        Game1.player.currentLocation.TemporarySprites.Add(animation);

                        break;


                }

            }

            if(track)
            {

                destination = Game1.player.Position + new Vector2(32, -32);

            }

            counter++;

            offset();

            if (counter == timeframe)
            {

                if (pocket)
                {

                    inventorise();

                }

                if(impact != IconData.impacts.none)
                {

                    Mod.instance.iconData.ImpactIndicator(Game1.player.currentLocation, destination, impact, 0.5f * scale, new());

                }

                return false;

            }

            return true;

        }


        public void inventorise()
        {

            switch (thrown)
            {

                case throwing.item:

                    if (track)
                    {
                        if (Game1.player.addItemToInventoryBool(item))
                        {

                            break;

                        }

                        // if unable to add to inventory spawn as debris
                        if (item is StardewValley.Object)
                        {

                            Game1.createItemDebris(item, Game1.player.Position, 2, Game1.player.currentLocation, -1);

                        }
                        else
                        {

                            Game1.player.dropItem(item);

                        }

                        break;

                    }

                    Game1.createItemDebris(item, destination, 2, Game1.player.currentLocation, -1);

                    break;

                case throwing.sword:

                    Game1.player.addItemByMenuIfNecessaryElseHoldUp(tooling, null);

                    break;

                case throwing.relic:

                    AnimateHoldup();

                    Microsoft.Xna.Framework.Rectangle relicRect = IconData.RelicRectangles((IconData.relics)index);

                    animation = new(0, 2500, 1, 1, Game1.player.Position + new Vector2(2, -124f), false, false)
                    {
                        sourceRect = relicRect,
                        sourceRectStartingPos = new(relicRect.X, relicRect.Y),
                        texture = Mod.instance.iconData.relicsTexture,
                        layerDepth = 900f,
                        delayBeforeAnimationStart = 175,
                        scale = 3f,

                    };

                    Game1.player.currentLocation.TemporarySprites.Add(animation);

                    string text = Mod.instance.relicsData.reliquary[((IconData.relics)index).ToString()].heldup;

                    Game1.drawObjectDialogue(text);

                    break;


            }

        }

        public void AnimateHoldup()
        {

            if (Mod.instance.Config.disableHands)
            {

                return;

            }

            if (Mod.instance.Helper.ModRegistry.IsLoaded("PeacefulEnd.FashionSense"))
            {

                return;

            }

            Game1.player.completelyStopAnimatingOrDoingAction();

            Game1.MusicDuckTimer = 2000f;

            DelayedAction.playSoundAfterDelay("getNewSpecialItem", 750);

            Game1.player.faceDirection(2);

            Game1.player.freezePause = 3000;

            Game1.player.FarmerSprite.animateOnce(
                    new FarmerSprite.AnimationFrame[3]{
                                new FarmerSprite.AnimationFrame(57, 0),
                                new FarmerSprite.AnimationFrame(57, 2500, secondaryArm: false, flip: false ),
                                new FarmerSprite.AnimationFrame((short)Game1.player.FarmerSprite.CurrentFrame, 500, secondaryArm: false, flip: false)
                    }
                );

        }


    }

}
