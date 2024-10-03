using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace StardewDruid.Cast.Fates
{
    public class Whisk : EventHandle
    {

        public SpellHandle whiskSpell;

        public List<SpellHandle> warpSpells = new();

        public bool whiskreturn;

        public Vector2 destination;

        public int strikeTimer;

        public int warpTimer;

        public Whisk()
        {

        }

        public override void EventActivate()
        {
            
            base.EventActivate();

            destination = origin;

            EventClicks(actionButtons.action);

            EventClicks(actionButtons.rite);

            WhiskSetup();

        }


        public virtual void WhiskSetup()
        {

            whiskSpell = new(location, origin, Game1.player.Position, 64);

            whiskSpell.type = SpellHandle.spells.missile;

            whiskSpell.missile = IconData.missiles.whisk;

            whiskSpell.indicator = IconData.cursors.fatesCharge;

            whiskSpell.instant = true;

            whiskSpell.projectile = 4;

            whiskSpell.projectileSpeed = 2;

            whiskSpell.projectilePeak = -1;

            whiskSpell.Update();

            Mod.instance.spellRegister.Add(whiskSpell);

        }

        public override bool EventActive()
        {

            if (Game1.player.currentLocation.Name != location.Name)
            {

                eventComplete = true;

            }

            if (eventComplete)
            {

                return false;

            }

            return true;

        }

        public override bool EventPerformAction(SButton Button, actionButtons Action = actionButtons.action)
        {

            if (Action == actionButtons.special)
            {

                return true;

            }

            if (!EventActive())
            {

                return false;

            }

            if (whiskreturn)
            {

                PerformStrike();
   
                return false;

            }

            if (Action == actionButtons.action)
            {

                List<Vector2> whiskTiles = ModUtility.GetTilesBetweenPositions(location, whiskSpell.projectilePosition, Game1.player.Position);

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

            if (PerformStrike())
            {

                return false;

            }

            PerformWarp();

            return false;

        }

        public bool PerformStrike()
        {

            if (!Mod.instance.questHandle.IsGiven(Journal.QuestHandle.fatesTwo))
            {

                return false;

            }

            if (Mod.instance.eventRegister.ContainsKey(Rite.eventTransform))
            {

                return false;

            }

            if (Mod.instance.eventRegister.ContainsKey("curse"))
            {

                if (Mod.instance.eventRegister["curse"] is Cast.Effect.Curse curseEffect)
                {

                    List<StardewValley.Monsters.Monster> validTargets = new();

                    if (curseEffect.victims.Count > 0)
                    {

                        int delay = 0;

                        for (int g = Math.Min(8,curseEffect.victims.Count - 1); g >= 0; g--)
                        {

                            KeyValuePair<StardewValley.Monsters.Monster, CurseTarget> curseTarget = curseEffect.victims.ElementAt(g);

                            if (!Utility.isOnScreen(curseTarget.Key.Position,64))
                            {
                                
                                continue;
                            
                            }

                            int strikes = 1;

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

                            for (int i = 0; i < strikes; i++)
                            {

                                SpellHandle sweep = new(Game1.player, new() { curseTarget.Key }, Mod.instance.CombatDamage());

                                sweep.type = SpellHandle.spells.warpstrike;

                                sweep.counter = 0 - delay;

                                int d = directions[Mod.instance.randomIndex.Next(directions.Count)];

                                directions.Remove(d);

                                sweep.projectile = d;

                                Mod.instance.spellRegister.Add(sweep);

                                warpSpells.Add(sweep);

                                delay += 15;

                            }

                        }

                        eventActive = true;

                        activeLimit = 5;

                        Game1.displayFarmer = false;

                        Game1.player.temporarilyInvincible = true;

                        Game1.player.temporaryInvincibilityTimer = 1;

                        Game1.player.currentTemporaryInvincibilityDuration = 5000;

                        return true;

                    }

                }

            }

            return false;

        }

        public void PerformWarp()
        {

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.fatesOne))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.fatesOne, 1);

            }

            SpellHandle teleport = new(Game1.player.currentLocation, destination, Game1.player.Position);

            teleport.type = SpellHandle.spells.teleport;

            teleport.instant = true;

            teleport.Update();

            Mod.instance.spellRegister.Add(teleport);

            whiskSpell.Shutdown();

            eventComplete = true;

            Mod.instance.rite.castTimer = 10;

        }

        public override void EventDecimal()
        {

            if (!EventActive())
            {

                return;

            }

            if (warpSpells.Count > 0)
            {
                
                foreach(SpellHandle warpSpell in warpSpells)
                {

                    if (!warpSpell.shutdown)
                    {

                        return;

                    }
                
                }

                Game1.displayFarmer = true;

                Game1.player.temporarilyInvincible = false;

                Game1.player.temporaryInvincibilityTimer = 0;

                Game1.player.currentTemporaryInvincibilityDuration = 0;

                Game1.player.stopGlowing();

                PerformWarp();

                eventComplete = true;

            }

            if (!whiskSpell.shutdown)
            {

                return;

            }

            eventComplete = true;

        }

        public override void EventRemove()
        {
            
            base.EventRemove();

            Game1.displayFarmer = true;

        }

    }

}
