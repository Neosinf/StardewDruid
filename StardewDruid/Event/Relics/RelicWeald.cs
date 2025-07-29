
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
    internal class RelicWeald: EventHandle
    {

        public RelicWeald()
        {

        }

        public override bool TriggerUpdate()
        {

            if ((Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[1])
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

            if (!Game1.player.hasOrWillReceiveMail("canReadJunimoText"))
            {

                Mod.instance.RegisterDisplay(StringData.Strings(StringData.stringkeys.noJunimo));

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

                foreach (KeyValuePair<Vector2,IconData.relics> relic in new Dictionary<Vector2,IconData.relics>()
                {

                    [origin + new Vector2(0,-384)] = IconData.relics.runestones_spring,
                    [origin + new Vector2(384, 0)] = IconData.relics.runestones_farm,
                    [origin + new Vector2(0, 384)] = IconData.relics.runestones_moon,
                    [origin + new Vector2(-384, 0)] = IconData.relics.runestones_cat,

                })
                {
                    ThrowHandle throwRelic = new(Game1.player.Position, relic.Key, relic.Value)
                    {
                        delay = Mod.instance.randomIndex.Next(5) * 10,

                        impact = IconData.impacts.puff
                    };

                    throwRelic.register();

                }

            }

            if(activeCounter == 3)
            {

                CompleteArea(location);

                CommunityCenter communityCenter = location as CommunityCenter;

                communityCenter.markAreaAsComplete(1);

                communityCenter.restoreAreaCutscene(1);

                communityCenter.areaCompleteReward(1);

                eventComplete = true;

            }

        }

        public static void CompleteArea(GameLocation location)
        {
            CommunityCenter communityCenter = location as CommunityCenter;

            Dictionary<string, string> bundleData = Game1.netWorldState.Value.BundleData;

            string areaNameFromNumber = CommunityCenter.getAreaNameFromNumber(1);

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

        }

    }

}