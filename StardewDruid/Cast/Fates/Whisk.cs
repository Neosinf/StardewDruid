using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Monsters;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.ConstrainedExecution;

namespace StardewDruid.Cast.Fates
{
    public class Whisk : EventHandle
    {

        public SpellHandle whiskSpell;

        public List<SpellHandle> warpSpells = new();

        public bool whiskreturn;

        public Vector2 destination;

        public int warpTimer;

        public int warpStrikes;

        public Whisk()
        {

        }

        public override void EventActivate()
        {
            
            base.EventActivate();

            destination = origin;

            EventClicks(actionButtons.action);

            EventClicks(actionButtons.rite);

            whiskSpell = new(location, origin, Game1.player.Position, 64)
            {
                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.whisk,

                displayFactor = 3,

                indicator = IconData.cursors.whisk,

                instant = true
            };

            whiskSpell.Update();

            Mod.instance.spellRegister.Add(whiskSpell);

        }

        public override bool EventPerformAction(actionButtons Action = actionButtons.action)
        {

            if (!EventActive())
            {

                return false;

            }

            if (Action == actionButtons.rite)
            {

                Mod.instance.RiteButtonSuppress();

            }

            if (Action == actionButtons.action)
            {

                List<Vector2> whiskTiles = ModUtility.GetTilesBetweenPositions(location, whiskSpell.impact, Game1.player.Position);

                for (int i = whiskTiles.Count - 1; i >= 0 ; i--)
                {
                    
                    if (ModUtility.TileAccessibility(location, whiskTiles[i]) != 0)
                    {

                        continue;

                    }

                    destination = whiskTiles[i] * 64;

                    break;

                }

            }

            PerformStrike();

            PerformWarp();

            costCounter = 12 + (warpStrikes * 6);

            Rite.ApplyCost(costCounter);

            return false;

        }

        public void PerformStrike()
        {

            if (!Mod.instance.questHandle.IsGiven(QuestHandle.fatesTwo))
            {

                return;

            }

            if (Mod.instance.eventRegister.ContainsKey(Rite.eventTransform))
            {

                return;

            }

            if (whiskSpell.shutdown)
            {

                return;

            }

            if (!Mod.instance.eventRegister.ContainsKey(Rite.eventCurse))
            {

                return;

            }

            if (Mod.instance.eventRegister[Rite.eventCurse] is not Curse)
            {

                return;

            }

            Curse curseEffect = Mod.instance.eventRegister[Rite.eventCurse] as Curse;

            if (curseEffect.victims.Count == 0)
            {

                return;

            }

            Dictionary <StardewValley.Monsters.Monster,float> validTargets = new();

            List <StardewValley.Monsters.Monster> orderedTargets = new();

            foreach (KeyValuePair<StardewValley.Monsters.Monster, CurseTarget> curseVictim in curseEffect.victims)
            {

                if (!Utility.isOnScreen(curseVictim.Key.Position, 64))
                {

                    continue;

                }

                validTargets.Add(curseVictim.Key, Vector2.Distance(curseVictim.Key.Position, Game1.player.Position));

            }

            foreach(KeyValuePair<StardewValley.Monsters.Monster, float> kvp in validTargets.OrderBy(key => key.Value))
            {

                orderedTargets.Add(kvp.Key);

            }

            int delay = 0;

            int manyStrikes = Math.Min(8, orderedTargets.Count);

            float baseDamage = Mod.instance.CombatDamage();

            for (int g = 0; g < manyStrikes; g++)
            {

                StardewValley.Monsters.Monster warpTarget = orderedTargets.ElementAt(g);

                int strikes = Math.Min(4,(int)(8/manyStrikes));

                if (Mod.instance.questHandle.IsComplete(QuestHandle.fatesTwo))
                {

                    List<float> critical = Mod.instance.CombatCritical();

                    if (Mod.instance.randomIndex.NextDouble() <= (double)critical[0])
                    {

                        strikes = 2 + (int)critical[1];

                    }

                }

                List<int> directions = new() { 0, 1, 2, 3, 4, 5, 6, 7, };

                int s = Mod.instance.randomIndex.Next(3);

                for (int i = strikes -1; i >= 0; i--)
                {

                    SpellHandle sweep = new(Game1.player, new() { warpTarget }, -1f)
                    {
                        type = SpellHandle.Spells.warpstrike,

                        counter = 0 - delay
                    };

                    if (i == 0)
                    {

                        sweep.damageMonsters = strikes * baseDamage;

                        if (Mod.instance.herbalHandle.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.spellcatch))
                        {

                            if (Mod.instance.herbalHandle.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.capture))
                            {

                                sweep.added.Add(SpellHandle.Effects.capture);

                            }

                        }

                    }

                    int d = directions[Mod.instance.randomIndex.Next(directions.Count)];

                    directions.Remove(d);

                    sweep.displayFactor = d;

                    sweep.damageRadius = 128;

                    sweep.display = IconData.impacts.flashbang;

                    sweep.sound = SpellHandle.Sounds.swordswipe;

                    Mod.instance.spellRegister.Add(sweep);

                    warpSpells.Add(sweep);

                    warpStrikes++;

                    delay += 18;

                }

            }

            warpTimer = delay;

        }

        public void PerformWarp()
        {

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.fatesOne))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.fatesOne, 1);

            }

            SpellHandle teleport = new(Game1.player.currentLocation, destination, Game1.player.Position)
            {
                type = SpellHandle.Spells.teleport,

                displayFactor = warpTimer
            };

            teleport.Update();

            Mod.instance.spellRegister.Add(teleport);

            // shut down

            whiskSpell.Shutdown();

            Mod.instance.rite.castTimer = warpTimer;

            Mod.instance.rite.castLevel = 1;

            eventComplete = true;

        }


        public override void EventDecimal()
        {

            if (whiskSpell.shutdown)
            {

                eventComplete = true;

            }

        }

    }

}
