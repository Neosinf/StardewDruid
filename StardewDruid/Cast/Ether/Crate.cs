using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Event;
using StardewDruid.Journal;
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

                if (!ModUtility.MonsterVitals(thief, location) || thief.netWoundedActive.Value)
                {

                    origin = thief.Position;

                    eventComplete = true;

                    return;

                }

                return;

            }

            activeLimit = 60;

            EventBar(DialogueData.Strings(DialogueData.stringkeys.treasureHunt), 0);

            switch (Mod.instance.randomIndex.Next(3))
            {
                default:
                case 0:
                    thief = new StardewDruid.Monster.DarkRogue(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());
                    break;
                case 1:
                    thief = new StardewDruid.Monster.DarkShooter(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());
                    break;
                case 2:
                    thief = new StardewDruid.Monster.DarkGoblin(ModUtility.PositionToTile(origin), Mod.instance.CombatDifficulty());
                    break;

            }

            thief.currentLocation = location;

            location.characters.Add(thief);

            thief.baseJuice = 1;

            thief.SetMode(2);

            thief.tempermentActive = Monster.Boss.temperment.coward;

            thief.netHaltActive.Set(true);

            thief.idleTimer = 60;

            thief.SetDirection(Game1.player.Position);

            thief.setWounded = true;

            thief.update(Game1.currentGameTime, location);

            narrators = DialogueData.DialogueNarrators("treasureChase");

            cues = DialogueData.DialogueScene("treasureChase");

            bosses[0] = thief;

            BossBar(0, 0);

            DialogueCue(990);

        }

        public override void EventCompleted()
        {
            
            if (!Mod.instance.questHandle.IsComplete(Journal.QuestHandle.etherFour))
            {

                Mod.instance.questHandle.UpdateTask(Journal.QuestHandle.etherFour, 1);

            }

            if (!Mod.instance.rite.specialCasts.ContainsKey(location.Name))
            {

                Mod.instance.rite.specialCasts[location.Name] = new();

            }

            Mod.instance.rite.specialCasts[location.Name].Add("crate");

            if (
                location is MineShaft mineShaft 
                && mineShaft.mineLevel != MineShaft.bottomOfMineLevel 
                && mineShaft.mineLevel != MineShaft.quarryMineShaft)
            {

                Layer layer = location.map.GetLayer("Buildings");

                Vector2 treasureVector = ModUtility.PositionToTile(origin);

                layer.Tiles[(int)treasureVector.X, (int)treasureVector.Y] = new StaticTile(layer, location.map.TileSheets[0], 0, mineShaft.mineLevel > 120 ? 174 : 173);

                Game1.player.TemporaryPassableTiles.Add(new Microsoft.Xna.Framework.Rectangle((int)treasureVector.X * 64, (int)treasureVector.Y * 64, 64, 64));


                DialogueCue(991);

                return;

            }

            int aether = Mod.instance.randomIndex.Next(1,4);

            Herbal Aether = Mod.instance.herbalData.herbalism[HerbalData.herbals.aether.ToString()];

            DisplayPotion hudmessage = new("+" + aether.ToString() + " " + Aether.title, Aether);
                
            Game1.addHUDMessage(hudmessage);

            if (Mod.instance.save.herbalism.ContainsKey(HerbalData.herbals.aether))
            {

                Mod.instance.save.herbalism[HerbalData.herbals.aether] += Math.Min(aether,999-Mod.instance.save.herbalism[HerbalData.herbals.aether]);

            }
            else
            {

                Mod.instance.save.herbalism[HerbalData.herbals.aether] = aether;

            }

            SpellHandle crate = new(location, new(1), origin);

            crate.type = SpellHandle.spells.crate;

            crate.counter = 60;

            crate.added.Add(SpellHandle.effects.crate);

            crate.Update();

            Mod.instance.spellRegister.Add(crate);

            if(crateTerrain == 2)
            {

                IconData.relics fishRelic = Mod.instance.relicsData.RelicMistsLocations();

                if (fishRelic != IconData.relics.none)
                {

                    if (!Mod.instance.save.reliquary.ContainsKey(fishRelic.ToString()))
                    {

                        ThrowHandle throwRelic = new(Game1.player, origin, fishRelic);

                        throwRelic.register();

                        return;

                    }

                }

            }

            IconData.relics bookRelic = Mod.instance.relicsData.RelicBooksLocations();

            if (bookRelic != IconData.relics.none)
            {

                if (!Mod.instance.save.reliquary.ContainsKey(bookRelic.ToString()))
                {

                    ThrowHandle throwRelic = new(Game1.player, origin, bookRelic);

                    throwRelic.register();

                }

            }

        }

    }

}
