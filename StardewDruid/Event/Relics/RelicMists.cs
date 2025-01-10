
using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Minigames;
using System;
using System.Collections.Generic;

namespace StardewDruid.Event.Relics
{
    internal class RelicMists : EventHandle
    {

        public RelicMists()
        {

        }

        public override bool TriggerAbort()
        {

            if ((Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[2])
            {

                Mod.instance.questHandle.CompleteQuest(eventId);

                TriggerRemove();

                triggerAbort = true;

            }

            return triggerAbort;

        }

        public override bool TriggerCheck()
        {

            if ((Game1.getLocationFromName("CommunityCenter") as CommunityCenter).numberOfCompleteBundles() == 0)
            {
                
                Mod.instance.CastDisplay(StringData.Strings(StringData.stringkeys.noInstructions));

                return false;

            }

            return base.TriggerCheck();
        
        }

        public override void EventActivate()
        {

            base.EventActivate();

            Mod.instance.spellRegister.Add(new(Game1.player.Position, 288, IconData.impacts.supree, new()) { sound = SpellHandle.sounds.thunder_small, });

        }

        public override void EventInterval()
        {

            activeCounter++;

            if(activeCounter == 1)
            {

                foreach (IconData.relics relic in new List<IconData.relics>()
                {

                    IconData.relics.avalant_disc,
                    IconData.relics.avalant_chassis,
                    IconData.relics.avalant_gears,
                    IconData.relics.avalant_casing,
                    IconData.relics.avalant_needle,
                    IconData.relics.avalant_measure,

                })
                {
                    ThrowHandle throwRelic = new(Game1.player.Position, origin + new Vector2((Mod.instance.randomIndex.Next(5) * 32) - 64, -128), relic);

                    throwRelic.delay = Mod.instance.randomIndex.Next(5) * 10;

                    throwRelic.impact = IconData.impacts.splash;

                    throwRelic.register();

                }

            }

            if(activeCounter == 3)
            {

                CommunityCenter communityCenter = location as CommunityCenter;

                Dictionary<string, string> bundleData = Game1.netWorldState.Value.BundleData;

                string areaNameFromNumber = CommunityCenter.getAreaNameFromNumber(2);

                foreach (string key in bundleData.Keys)
                {
                    
                    if (key.Contains(areaNameFromNumber))
                    {
                        
                        int bundleId = Convert.ToInt32(key.Split('/')[1]);

                        communityCenter.bundleRewards[bundleId] = true;

                        for (int i = 0; i < communityCenter.bundles[bundleId].Length; i++)
                        {

                            communityCenter.bundles.FieldDict[bundleId][i] = true;

                        }
                    }

                }

                communityCenter.markAreaAsComplete(2);

                communityCenter.restoreAreaCutscene(2);

                communityCenter.areaCompleteReward(2);

                eventComplete = true;

            }

        }

    }

}