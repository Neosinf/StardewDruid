
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
    internal class RelicTactical: EventHandle
    {

        public RelicTactical()
        {

        }

        public override bool TriggerUpdate()
        {

            if ((Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[3])
            {

                Mod.instance.questHandle.CompleteQuest(eventId);

                if (triggerActive)
                {

                    TriggerRemove();

                }

                return false;

            }

            return base.TriggerUpdate();

        }

        public override bool TriggerCheck()
        {

            if ((Game1.getLocationFromName("CommunityCenter") as CommunityCenter).numberOfCompleteBundles() < 2)
            {

                Mod.instance.RegisterDisplay(StringData.Get(StringData.str.noInstructions));

                return false;

            }

            return base.TriggerCheck();

        }

        public override void EventActivate()
        {

            base.EventActivate();

            Mod.instance.spellRegister.Add(new(Game1.player.Position, 288, IconData.impacts.supree, new()) { sound = SpellHandle.Sounds.getNewSpecialItem, });


        }

        public override void EventInterval()
        {

            activeCounter++;

            if(activeCounter == 1)
            {

                foreach (IconData.relics relic in new List<IconData.relics>()
                {

                    IconData.relics.tactical_discombobulator,
                    IconData.relics.tactical_mask,
                    IconData.relics.tactical_lunchbox,
                    IconData.relics.tactical_cell,

                })
                {
                    ThrowHandle throwRelic = new(Game1.player.Position, origin + new Vector2((Mod.instance.randomIndex.Next(5) * 8) - 40, -256), relic)
                    {
                        delay = Mod.instance.randomIndex.Next(5) * 10,

                        impact = IconData.impacts.flasher

                    };

                    throwRelic.register();

                }

            }

            if(activeCounter == 3)
            {

                CommunityCenter communityCenter = location as CommunityCenter;

                Dictionary<string, string> bundleData = Game1.netWorldState.Value.BundleData;

                string areaNameFromNumber = CommunityCenter.getAreaNameFromNumber(3);

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

                communityCenter.markAreaAsComplete(3);

                communityCenter.restoreAreaCutscene(3);

                communityCenter.areaCompleteReward(3);

                eventComplete = true;

            }

        }

    }

}