using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Event;
using StardewDruid.Location.Druid;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;

namespace StardewDruid.Cast.Ether
{

    public class Crate : EventHandle
    {

        public int crateTerrain;

        public bool crateThief;

        public bool crateTreasure;

        public bool heldTreasure;

        public List<StardewValley.Object> treasures = new();

        public Crate()
        {

        }

        public override void TriggerField()
        {

            eventRenders.Add(new(eventId, location.Name, origin, IconData.decorations.ether));

        }

        public override bool TriggerCheck()
        {

            return false;

        }

        public override void EventActivate()
        {
            
            base.EventActivate();

            if(!crateThief)
            {

                eventComplete = true;

            }

        }

        public override void EventInterval()
        {

            activeCounter++;

            StardewDruid.Monster.Boss thief;

            if (bosses.Count > 0)
            {

                thief = bosses.First().Value;

                origin = thief.Position;

                if (!ModUtility.MonsterVitals(thief, location) || thief.netWoundedActive.Value)
                {

                    thief.deathIsNoEscape();

                    eventComplete = true;

                    return;

                }

                return;

            }

            activeLimit = 60;

            EventBar(StringData.Strings(StringData.stringkeys.treasureGuardian), 0);

            switch (crateTerrain)
            {

                default:

                    switch (Mod.instance.randomIndex.Next(3))
                    {
                        default:
                        case 0:
                            thief = new StardewDruid.Monster.Dark(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());
                            break;
                        case 1:
                            thief = new StardewDruid.Monster.DarkShooter(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());
                            break;
                        case 2:
                            thief = new StardewDruid.Monster.DarkGoblin(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());
                            break;

                    }

                    break;

                case 2:

                    int forest = SpawnData.ForestWaterCheck(location);

                    if (location is Caldera || location.Name == "UndergroundMine100")
                    {
                        thief = new StardewDruid.Monster.ShadowSerpent(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty(),"LavaSerpent");

                        thief.netScheme.Set(2);

                        break;

                    }
                    else if (location is Beach || location.Name.Contains("Beach") || location is IslandLocation || location is Atoll || forest == 3)
                    {

                        if (Mod.instance.randomIndex.Next(3) == 0)
                        {

                            thief = new StardewDruid.Monster.ShadowSerpent(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty(), "NightSerpent");

                            thief.netScheme.Set(4);

                            break;

                        }

                        thief = new StardewDruid.Monster.ShadowSerpent(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());

                        break;

                    }

                    if (Mod.instance.randomIndex.Next(3) == 0)
                    {

                        thief = new StardewDruid.Monster.ShadowSerpent(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty(), "NightSerpent");

                        thief.netScheme.Set(4);

                        break;

                    }

                    thief = new StardewDruid.Monster.ShadowSerpent(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty(), "RiverSerpent");

                    thief.netScheme.Set(1);

                    break;

            }

            thief.currentLocation = location;

            location.characters.Add(thief);

            if (crateTreasure)
            {

                thief.tempermentActive = Monster.Boss.temperment.coward;

                thief.basePulp = 15;

                thief.SetMode(2);

                narrators = NarratorData.DialogueNarrators(QuestHandle.treasureChase);

                cues = DialogueData.DialogueScene(QuestHandle.treasureChase);

            }

            if (heldTreasure)
            {

                thief.tempermentActive = Monster.Boss.temperment.aggressive;

                thief.SetMode(treasures.Count);

                narrators = NarratorData.DialogueNarrators(QuestHandle.treasureGuardian);

                cues = DialogueData.DialogueScene(QuestHandle.treasureGuardian);

            }

            thief.netHaltActive.Set(true);

            thief.idleTimer = 60;

            thief.LookAtTarget(Game1.player.Position);

            thief.setWounded = true;

            thief.update(Game1.currentGameTime, location);

            bosses[0] = thief;

            BossBar(0, 0);

            DialogueCue(990);

            Mod.instance.iconData.AnimateQuickWarp(location, origin);

        }

        public override void EventCompleted()
        {
            
            if (
                crateTerrain != 2
                && location is MineShaft mineShaft 
                && mineShaft.mineLevel != MineShaft.bottomOfMineLevel 
                && mineShaft.mineLevel != MineShaft.quarryMineShaft)
            {

                OpenHole();

                return;

            }

            if(crateTerrain == 2)
            {

                FishRelic();

            }

            if (crateTreasure)
            {

                OpenTreasure();

            }

            if (heldTreasure)
            {

                ReleaseTreasure();

            }

        }

        public void FishRelic()
        {

            IconData.relics fishRelic = Mod.instance.relicsData.RelicMistsLocations();

            if (fishRelic != IconData.relics.none)
            {

                if (!RelicData.HasRelic(fishRelic))
                {

                    ThrowHandle throwRelic = new(Game1.player, origin, fishRelic);

                    throwRelic.register();

                }

            }

        }

        public void OpenHole()
        {

            Layer layer = location.map.GetLayer("Buildings");

            Vector2 treasureVector = ModUtility.PositionToTile(origin);

            layer.Tiles[(int)treasureVector.X, (int)treasureVector.Y] = new StaticTile(layer, location.map.TileSheets[0], 0, (location as MineShaft).mineLevel > 120 ? 174 : 173);

            Game1.player.TemporaryPassableTiles.Add(new Microsoft.Xna.Framework.Rectangle((int)treasureVector.X * 64, (int)treasureVector.Y * 64, 64, 64));

            DialogueCue(991);

        }

        public void OpenTreasure()
        {

            if (!Mod.instance.rite.specialCasts.ContainsKey(location.Name))
            {

                Mod.instance.rite.specialCasts[location.Name] = new();

            }

            Mod.instance.rite.specialCasts[location.Name].Add("crate");

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.etherFour))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.etherFour, 1);

            }

            int aether = Mod.instance.randomIndex.Next(1, 4);

            Herbal Aether = Mod.instance.herbalData.herbalism[HerbalData.herbals.aether.ToString()];

            DisplayPotion hudmessage = new("+" + aether.ToString() + " " + Aether.title, Aether);

            Game1.addHUDMessage(hudmessage);

            HerbalData.UpdateHerbalism(HerbalData.herbals.aether, aether);

            SpellHandle crate = new(location, new(1), origin);

            crate.type = SpellHandle.spells.crate;

            crate.counter = 60;

            crate.added.Add(SpellHandle.effects.crate);

            crate.Update();

            Mod.instance.spellRegister.Add(crate);

            IconData.relics bookRelic = Mod.instance.relicsData.RelicBooksLocations();

            if (bookRelic != IconData.relics.none)
            {

                if (!RelicData.HasRelic(bookRelic))
                {

                    ThrowHandle throwRelic = new(Game1.player, origin, bookRelic);

                    throwRelic.register();

                }

            }

        }

        public void ReleaseTreasure()
        {

            foreach(StardewValley.Object treasure in treasures)
            {

                Vector2 offset = origin + new Vector2(Mod.instance.randomIndex.Next(4) * 16, Mod.instance.randomIndex.Next(4) * 16);

                ThrowHandle throwObject = new(Game1.player, offset, treasure);

                throwObject.delay = Mod.instance.randomIndex.Next(5) * 10;

                throwObject.register();

                Game1.player.gainExperience(1, treasure.Quality * 12); // gain fishing experience

                Game1.player.caughtFish(treasure.ItemId, 1, false, 1);

            }

        }

    }

}
