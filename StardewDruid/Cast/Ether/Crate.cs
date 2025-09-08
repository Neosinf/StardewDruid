using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Event;
using StardewDruid.Handle;
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

        public bool skipBattle;

        public bool challengeEvent;

        public bool contestEvent;

        public List<StardewValley.Object> treasures = new();

        public Crate()
        {

            

            mainEvent = true;

        }

        public override void TriggerCreate()
        {

            triggerActive = true;

            Mod.instance.triggerRegister[eventId] = true;

            EventRender triggerField = new(eventId, location.Name, origin, IconData.ritecircles.ether);

            eventRenders.Add("crate",triggerField);

            location = Game1.player.currentLocation;
            
        }

        public override bool TriggerCheck()
        {

            return false;

        }

        public override void EventActivate()
        {

            if(Mod.instance.activeEvent.Count > 0)
            {

                return;

            }

            if (!crateThief)
            {

                if (triggerActive)
                {

                    TriggerRemove();

                }

                location = Game1.player.currentLocation;

                ReleaseReward();

                return;

            }

            base.EventActivate();

            LoadBoss();

        }

        public void LoadBoss()
        {
            
            StardewDruid.Monster.Boss thief;

            switch (crateTerrain)
            {

                default:

                    switch (Mod.instance.randomIndex.Next(3))
                    {
                        default:
                        case 0:

                            thief = new StardewDruid.Monster.Dark(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());
                            
                            thief.displayName = Mod.instance.Helper.Translation.Get("NarratorData.361.23");

                            break;

                        case 1:

                            thief = new StardewDruid.Monster.DarkShooter(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());

                            thief.displayName = Mod.instance.Helper.Translation.Get("NarratorData.361.11");

                            break;

                        case 2:

                            thief = new StardewDruid.Monster.DarkGoblin(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());

                            thief.displayName = Mod.instance.Helper.Translation.Get("NarratorData.361.22");

                            break;

                    }

                    break;

                case 2:

                    int forest = SpawnData.ForestWaterCheck(location);

                    if (location is Caldera || location.Name == "UndergroundMine100")
                    {
                        
                        thief = new StardewDruid.Monster.ShadowSerpent(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty(),"LavaSerpent");

                        thief.displayName = Mod.instance.Helper.Translation.Get("NarratorData.391.1");

                        thief.netScheme.Set(2);

                        break;

                    }
                    else if (location is Beach || location.Name.Contains("Beach") || location is IslandLocation || location is Atoll || forest == 3)
                    {

                        if (Mod.instance.randomIndex.Next(3) == 0)
                        {

                            thief = new StardewDruid.Monster.ShadowSerpent(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty(), "NightSerpent");

                            thief.displayName = Mod.instance.Helper.Translation.Get("NarratorData.391.2");

                            thief.netScheme.Set(4);

                            break;

                        }

                        thief = new StardewDruid.Monster.ShadowSerpent(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());

                        thief.displayName = Mod.instance.Helper.Translation.Get("NarratorData.391.3");

                        break;

                    }

                    if (Mod.instance.randomIndex.Next(3) == 0)
                    {

                        thief = new StardewDruid.Monster.ShadowSerpent(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty(), "NightSerpent");

                        thief.displayName = Mod.instance.Helper.Translation.Get("NarratorData.391.2");

                        thief.netScheme.Set(4);

                        break;

                    }

                    thief = new StardewDruid.Monster.ShadowSerpent(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty(), "RiverSerpent");

                    thief.displayName = Mod.instance.Helper.Translation.Get("NarratorData.391.4");

                    thief.netScheme.Set(1);

                    break;

            }

            thief.currentLocation = location;

            thief.tether = origin;

            thief.tetherLimit = 1920;

            location.characters.Add(thief);

            if (crateTreasure)
            {

                thief.basePulp = 15;

                thief.SetMode(2);

                thief.tempermentActive = Monster.Boss.temperment.coward;

                narrators = NarratorData.DialogueNarrators(QuestHandle.treasureChase);

                cues = DialogueData.DialogueScene(QuestHandle.treasureChase);

            }

            if (heldTreasure)
            {

                thief.SetMode(Math.Max(2,treasures.Count));

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

            if (skipBattle)
            {

                InitiateContest();

                return;


            }

            bosses[0].netPosturing.Set(true);

            challengeEvent = true;

        }

        public void InitiateContest()
        {

            challengeEvent = false;

            if (!ContestInterval())
            {

                return;

            }

            activeLimit = 60;

            switch (crateTerrain)
            {

                default:

                    ProgressBar(StringData.Get(StringData.str.treasureHunt), 0);

                    break;

                case 2:

                    ProgressBar(StringData.Get(StringData.str.treasureGuardian), 0);

                    break;

            }

            bosses[0].netPosturing.Set(false);

            contestEvent = true;

        }

        public override void EventInterval()
        {

            activeCounter++;

            if (challengeEvent)
            {

                ChallengeInterval();

            }

            if (contestEvent)
            {

                ContestInterval();

                return;

            }

        }

        public void ChallengeInterval()
        {

            if (activeCounter == 1)
            {

                bosses[0].LookAtTarget(Game1.player.Position);

                bosses[0].doEmote(16);

                return;

            }

            if (activeCounter == 2)
            {

                BattleHandle battleHandle = new();

                battleHandle.opposition = bosses[0];

                battleHandle.ThiefEngage(eventId);

                return;

            }

            if (Mod.instance.battleRegister.Count == 0)
            {

                InitiateContest();

            }

        }

        public bool ContestInterval()
        {

            if(activeCounter >= activeLimit)
            {

                eventComplete = true;

                return false;

            }

            if (bosses.Count == 0)
            {

                eventComplete = true;

                return false;

            }

            origin = bosses[0].Position;

            if (!ModUtility.MonsterVitals(bosses[0], location) || bosses[0].netWoundedActive.Value)
            {

                bosses[0].deathIsNoEscape();

                eventComplete = true;

                ReleaseReward();

                return false;

            }

            return true;

        }

        public void ReleaseReward() 
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

            IconData.relics fishRelic = Mod.instance.relicHandle.RelicMistsLocations();

            if (fishRelic != IconData.relics.none)
            {

                if (!RelicHandle.HasRelic(fishRelic))
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

            List<Vector2> tryGround = ModUtility.GetOccupiableTilesNearby(location, treasureVector, -1, 0, 2);

            if(tryGround.Count > 0)
            {

                Mod.instance.rite.specialCasts[location.Name].Add("crate");

                Vector2 tryBore = tryGround.First();

                layer.Tiles[(int)tryBore.X, (int)tryBore.Y] = new StaticTile(layer, location.map.TileSheets[0], 0, (location as MineShaft).mineLevel > 120 ? 174 : 173);

                Game1.player.TemporaryPassableTiles.Add(new Microsoft.Xna.Framework.Rectangle((int)treasureVector.X * 64, (int)treasureVector.Y * 64, 64, 64));

                DialogueCue(991);

            }

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

            // crate display

            SpellHandle crate = new(location, new(1), origin)
            {
                type = SpellHandle.Spells.crate,

                counter = 60
            };

            crate.added.Add(SpellHandle.Effects.crate);

            crate.Update();

            Mod.instance.spellRegister.Add(crate);

            // aether

            int aether = Mod.instance.randomIndex.Next(1, 4);

            ThrowHandle throwAether = new(Game1.player, origin, ApothecaryHandle.items.aether, aether);

            throwAether.register();

            // relics

            IconData.relics bookRelic = Mod.instance.relicHandle.RelicBooksLocations();

            if (bookRelic != IconData.relics.none)
            {

                if (!RelicHandle.HasRelic(bookRelic))
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

                ThrowHandle throwObject = new(Game1.player, offset, treasure)
                {
                    delay = Mod.instance.randomIndex.Next(5) * 10
                };

                throwObject.register();

                if(treasure.Category == StardewValley.Object.FishCategory)
                {

                    Mod.instance.GiveExperience(1, treasure.Quality * 12); // gain fishing experience

                    Game1.player.caughtFish(treasure.ItemId, 1, false, 1);

                }

            }

        }

    }

}
