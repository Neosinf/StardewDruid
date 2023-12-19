using Microsoft.Xna.Framework;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.IO;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

#nullable disable
namespace StardewDruid.Event.Challenge
{
    public class Tyrannus : ChallengeHandle
    {
        public bool modifiedSandDragon;
        public Reaper bossMonster;
        public Vector2 bossTile;
        public bool adjustWarp;

        public Tyrannus(Vector2 target, Rite rite, Quest quest)
          : base(target, rite, quest)
        {
            this.targetVector = target;
            this.voicePosition = Vector2.op_Addition(Vector2.op_Multiply(this.targetVector, 64f), new Vector2(0.0f, -32f));
            this.expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 120.0;
        }

        public override void EventTrigger()
        {
            ModUtility.AnimateRadiusDecoration(this.targetLocation, this.targetVector, "Weald", 1f, 1f);
            ModUtility.AnimateRockfalls(this.targetLocation, ((Character)this.targetPlayer).getTileLocation());
            // ISSUE: method pointer
            DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object)this, __methodptr(RockfallSounds)), 575);
            // ISSUE: method pointer
            DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object)this, __methodptr(RockfallSounds)), 675);
            // ISSUE: method pointer
            DelayedAction.functionAfterDelay(new DelayedAction.delayedBehavior((object)this, __methodptr(RockfallSounds)), 775);
            Mod.instance.RegisterEvent((EventHandle)this, "active");
        }

        public void RockfallSounds()
        {
            this.targetLocation.playSoundPitched(new Random().Next(2) == 0 ? "boulderBreak" : "boulderCrack", 800, (NetAudio.SoundContext)0);
        }

        public override bool EventActive()
        {
            if (((Character)this.targetPlayer).currentLocation == this.targetLocation && !this.eventAbort)
            {
                double totalSeconds = Game1.currentGameTime.TotalGameTime.TotalSeconds;
                if (this.expireTime < totalSeconds || this.expireEarly)
                    return this.EventExpire();
                int num = (int)Math.Round(this.expireTime - totalSeconds);
                if (this.activeCounter != 0 && num % 10 == 0 && num != 0)
                    Game1.addHUDMessage(new HUDMessage(string.Format("{0} more minutes left!", (object)num), "2"));
                return true;
            }
            this.EventAbort();
            return false;
        }

        public override void RemoveMonsters()
        {
            if (this.bossMonster != null)
            {
                this.targetLocation.characters.Remove((NPC)this.bossMonster);
                this.bossMonster = (Reaper)null;
            }
            base.RemoveMonsters();
        }

        public override void EventRemove()
        {
            base.EventRemove();
            if (!(this.targetLocation is MineShaft))
                return;
            Vector2 bossTile = this.bossTile;
            for (int index1 = 0; index1 < this.targetLocation.map.GetLayer("Buildings").LayerHeight; ++index1)
            {
                for (int index2 = 0; index2 < this.targetLocation.map.GetLayer("Buildings").LayerWidth; ++index2)
                {
                    if (this.targetLocation.map.GetLayer("Buildings").Tiles[index2, index1] != null && this.targetLocation.map.GetLayer("Buildings").Tiles[index2, index1].TileIndex == 115)
                    {
                        // ISSUE: explicit constructor call
                        ((Vector2)ref bossTile).\u002Ector((float)(index2 + 1), (float)(index1 + 1));
                    }
                }
            }
            Layer layer = this.targetLocation.map.GetLayer("Buildings");
            layer.Tiles[(int)bossTile.X, (int)bossTile.Y] = (Tile)new StaticTile(layer, this.targetLocation.map.TileSheets[0], (BlendMode)0, 174);
            Game1.player.TemporaryPassableTiles.Add(new Rectangle((int)bossTile.X * 64, (int)bossTile.Y * 64, 64, 64));
            Mod.instance.CastMessage("A way down has appeared");
        }

        public override bool EventExpire()
        {
            if (this.eventLinger == -1)
            {
                this.RemoveMonsters();
                this.eventLinger = 4;
                return true;
            }
            if (this.eventLinger == 3)
            {
                if (this.expireEarly)
                {
                    if (!this.questData.name.Contains("Two"))
                        new Throw().ThrowSword(Game1.player, 57, this.bossTile, 500);
                    Mod.instance.CompleteQuest(this.questData.name);
                    if (((Character)Mod.instance.characters["Jester"]).currentLocation.Name == this.targetLocation.Name)
                        Mod.instance.dialogue["Jester"].specialDialogue.Add("quests", new List<string>()
            {
              "Jester of Fate:^Thank you for helping me put Thanatoshi to rest.",
              "I'm sorry about your kinsman.",
              "I think this cutlass is to blame"
            });
                }
                else
                {
                    Mod.instance.CastMessage("Try again tomorrow");
                    Mod.instance.characters["Jester"].showTextAboveHead("Thanatoshi... why...", -1, 2, 3000, 0);
                }
            }
            return base.EventExpire();
        }

        public override void EventInterval()
        {
            ++this.activeCounter;
            if (this.eventLinger != -1 || this.activeCounter == 1)
                return;
            if (this.activeCounter == 2)
            {
                this.AddTomb();
                this.targetVector = new Vector2(13f, 18f);
                Game1.inMine = true;
                Game1.warpFarmer("UndergroundMine145", 13, 19, 2);
                Game1.xLocationAfterWarp = 13;
                Game1.yLocationAfterWarp = 19;
                this.voicePosition = Vector2.op_Addition(Vector2.op_Multiply(this.targetVector, 64f), new Vector2(0.0f, -32f));
            }
            else if (this.activeCounter == 3)
            {
                ((Character)this.targetPlayer).Position = Vector2.op_Multiply(this.targetVector, 64f);
                this.bossMonster = MonsterData.CreateMonster(17, new Vector2(13f, 9f), this.riteData.combatModifier) as Reaper;
                if (this.questData.name.Contains("Two"))
                    this.bossMonster.HardMode();
                this.targetLocation.characters.Add((NPC)this.bossMonster);
                ((Character)this.bossMonster).currentLocation = this.riteData.castLocation;
                ((Character)this.bossMonster).update(Game1.currentGameTime, this.riteData.castLocation);
                this.SetTrack("LavaMine");
                this.bossTile = new Vector2(13f, 9f);
            }
            else
            {
                if (this.activeCounter == 5 && ((Character)Mod.instance.characters["Jester"]).currentLocation.Name == this.targetLocation.Name)
                    Mod.instance.characters["Jester"].showTextAboveHead("What a moment...", -1, 2, 3000, 0);
                if (this.activeCounter == 10 && ((Character)Mod.instance.characters["Jester"]).currentLocation.Name == this.targetLocation.Name)
                    Mod.instance.characters["Jester"].showTextAboveHead("Thanatoshi?", -1, 3, 3000, 0);
                if (this.activeCounter == 15 && ((Character)Mod.instance.characters["Jester"]).currentLocation.Name == this.targetLocation.Name)
                    Mod.instance.characters["Jester"].showTextAboveHead("The Reaper of Fate", -1, 3, 3000, 0);
                if (this.activeCounter == 20 && ((Character)Mod.instance.characters["Jester"]).currentLocation.Name == this.targetLocation.Name)
                    Mod.instance.characters["Jester"].showTextAboveHead("Stop Thanatoshi!", -1, 3, 3000, 0);
                if (this.activeCounter == 25 && ((Character)Mod.instance.characters["Jester"]).currentLocation.Name == this.targetLocation.Name)
                    Mod.instance.characters["Jester"].showTextAboveHead("I am the Jester of Fate, your kin", -1, 2, 3000, 0);
                if (this.activeCounter == 30 && ((Character)Mod.instance.characters["Jester"]).currentLocation.Name == this.targetLocation.Name)
                    Mod.instance.characters["Jester"].showTextAboveHead("It's no use, he's insane", -1, 2, 3000, 0);
                if (this.activeCounter == 35 && ((Character)Mod.instance.characters["Jester"]).currentLocation.Name == this.targetLocation.Name)
                    Mod.instance.characters["Jester"].showTextAboveHead("That's... a cutlass... on the shaft", -1, 2, 3000, 0);
                if (this.activeCounter == 40 && ((Character)Mod.instance.characters["Jester"]).currentLocation.Name == this.targetLocation.Name)
                    Mod.instance.characters["Jester"].showTextAboveHead("What has he done to himself?", -1, 2, 3000, 0);
                if (this.activeCounter == 50 && ((Character)Mod.instance.characters["Jester"]).currentLocation.Name == this.targetLocation.Name)
                    Mod.instance.characters["Jester"].showTextAboveHead("For Fate and Fortune!", -1, 3, 3000, 0);
                if (this.bossMonster.defeated || this.bossMonster.Health <= 0 || this.bossMonster == null || !this.targetLocation.characters.Contains((NPC)this.bossMonster))
                    this.expireEarly = true;
                else
                    this.bossTile = ((Character)this.bossMonster).getTileLocation();
            }
        }

        public void AddTomb()
        {
            MineShaft mineShaft = new MineShaft(145);
            MineShaft.activeMines.Clear();
            MineShaft.activeMines.Add(mineShaft);
            ((NetFieldBase<string, NetString>)((GameLocation)mineShaft).mapPath).Value = "Maps\\Mines\\33";
            mineShaft.loadedMapNumber = 33;
            ((GameLocation)mineShaft).updateMap();
            ((NetFieldBase<string, NetString>)mineShaft.mapImageSource).Value = "Maps\\Mines\\mine_desert_dark_dangerous";
            ((GameLocation)mineShaft).Map.TileSheets[0].ImageSource = "Maps\\Mines\\mine_desert_dark_dangerous";
            ((GameLocation)mineShaft).Map.LoadTileSheets(Game1.mapDisplayDevice);
            mineShaft.mineLevel = 100;
            mineShaft.chooseLevelType();
            mineShaft.mineLevel = 145;
            mineShaft.findLadder();
            this.targetLocation = Game1.getLocationFromName("UndergroundMine145");
            Layer layer1 = this.targetLocation.map.GetLayer("Back");
            Layer layer2 = this.targetLocation.map.GetLayer("Buildings");
            Layer layer3 = this.targetLocation.map.GetLayer("Front");
            TileSheet tileSheet1 = new TileSheet("zestfordragontiles99999999", this.targetLocation.map, Path.Combine("Maps", "DesertTiles"), new Size(16, 23), new Size(1, 1));
            this.targetLocation.map.AddTileSheet(tileSheet1);
            TileSheet tileSheet2 = this.targetLocation.map.TileSheets[0];
            layer1.Tiles[15, 11] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 166);
            layer1.Tiles[16, 11] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 167);
            layer1.Tiles[17, 11] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 167);
            layer1.Tiles[18, 11] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 167);
            layer1.Tiles[19, 11] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 167);
            layer1.Tiles[20, 11] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 168);
            layer1.Tiles[11, 12] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 166);
            layer1.Tiles[12, 12] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 167);
            layer1.Tiles[13, 12] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 167);
            layer1.Tiles[14, 12] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 167);
            layer1.Tiles[15, 12] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 152);
            layer1.Tiles[16, 12] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[17, 12] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[18, 12] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[19, 12] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[20, 12] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 184);
            layer1.Tiles[8, 13] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 166);
            layer1.Tiles[9, 13] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 167);
            layer1.Tiles[10, 13] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 167);
            layer1.Tiles[11, 13] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 152);
            layer1.Tiles[12, 13] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[13, 13] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[14, 13] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[15, 13] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[16, 13] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[17, 13] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[18, 13] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 181);
            layer1.Tiles[19, 13] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[20, 13] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 184);
            layer1.Tiles[8, 14] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 182);
            layer1.Tiles[9, 14] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[10, 14] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[11, 14] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[12, 14] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[13, 14] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[14, 14] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[15, 14] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[16, 14] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[17, 14] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[18, 14] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[19, 14] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[20, 14] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 184);
            layer1.Tiles[8, 15] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 182);
            layer1.Tiles[9, 15] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[10, 15] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[11, 15] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[12, 15] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[13, 15] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[14, 15] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[15, 15] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[16, 15] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[17, 15] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[18, 15] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[19, 15] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[20, 15] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 184);
            layer1.Tiles[8, 16] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 182);
            layer1.Tiles[9, 16] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 181);
            layer1.Tiles[10, 16] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[11, 16] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[12, 16] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 181);
            layer1.Tiles[13, 16] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[14, 16] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[15, 16] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[16, 16] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 150);
            layer1.Tiles[17, 16] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 199);
            layer1.Tiles[18, 16] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 199);
            layer1.Tiles[19, 16] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 199);
            layer1.Tiles[20, 16] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 200);
            layer1.Tiles[8, 17] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 182);
            layer1.Tiles[9, 17] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[10, 17] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[11, 17] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 165);
            layer1.Tiles[12, 17] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 150);
            layer1.Tiles[13, 17] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 199);
            layer1.Tiles[14, 17] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 199);
            layer1.Tiles[15, 17] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 199);
            layer1.Tiles[16, 17] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 200);
            layer1.Tiles[8, 18] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 198);
            layer1.Tiles[9, 18] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 199);
            layer1.Tiles[10, 18] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 199);
            layer1.Tiles[11, 18] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 199);
            layer1.Tiles[12, 18] = (Tile)new StaticTile(layer1, tileSheet2, (BlendMode)0, 200);
            layer3.Tiles[17, 13] = (Tile)new StaticTile(layer3, tileSheet1, (BlendMode)0, 3);
            layer3.Tiles[18, 13] = (Tile)new StaticTile(layer3, tileSheet1, (BlendMode)0, 4);
            layer2.Tiles[17, 14] = (Tile)new StaticTile(layer2, tileSheet1, (BlendMode)0, 19);
            layer2.Tiles[18, 14] = (Tile)new StaticTile(layer2, tileSheet1, (BlendMode)0, 20);
            layer3.Tiles[15, 13] = (Tile)new StaticTile(layer3, tileSheet1, (BlendMode)0, 5);
            layer3.Tiles[15, 14] = (Tile)new StaticTile(layer3, tileSheet1, (BlendMode)0, 21);
            layer2.Tiles[15, 15] = (Tile)new StaticTile(layer2, tileSheet1, (BlendMode)0, 37);
            layer3.Tiles[14, 13] = (Tile)new StaticTile(layer3, tileSheet1, (BlendMode)0, 5);
            layer3.Tiles[14, 14] = (Tile)new StaticTile(layer3, tileSheet1, (BlendMode)0, 21);
            layer2.Tiles[14, 15] = (Tile)new StaticTile(layer2, tileSheet1, (BlendMode)0, 37);
            layer3.Tiles[13, 13] = (Tile)new StaticTile(layer3, tileSheet1, (BlendMode)0, 5);
            layer3.Tiles[13, 14] = (Tile)new StaticTile(layer3, tileSheet1, (BlendMode)0, 21);
            layer2.Tiles[13, 15] = (Tile)new StaticTile(layer2, tileSheet1, (BlendMode)0, 37);
        }
    }
}