
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
    internal class RelicEther: EventHandle
    {

        public RelicEther()
        {

        }

        public override bool TriggerAbort()
        {

            if ((Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[0])
            {

                Mod.instance.questHandle.CompleteQuest(eventId);

                TriggerRemove();

                triggerAbort = true;

            }

            return triggerAbort;

        }

        public override bool TriggerCheck()
        {

            if (!Game1.player.hasOrWillReceiveMail("canReadJunimoText"))
            {
                
                Mod.instance.CastDisplay(DialogueData.Strings(DialogueData.stringkeys.noJunimo));

                return false;

            }

            return base.TriggerCheck();
        
        }

        public override void EventActivate()
        {

            base.EventActivate();

            ModUtility.AnimateHands(Game1.player,Game1.player.FacingDirection,600);

            Mod.instance.iconData.DecorativeIndicator(location, Game1.player.Position, IconData.decorations.ether, 4f, new());

            location.playSound(SpellHandle.sounds.discoverMineral.ToString());
            location.playSound(SpellHandle.sounds.discoverMineral.ToString());
            location.playSound(SpellHandle.sounds.discoverMineral.ToString());

        }

        public override void EventInterval()
        {

            activeCounter++;

            if(activeCounter == 1)
            {

                ThrowHandle throwRelic = new(Game1.player.Position, origin + new Vector2(0, -256), IconData.relics.book_druid);

                throwRelic.delay = Mod.instance.randomIndex.Next(5) * 10;

                throwRelic.impact = IconData.impacts.puff;

                throwRelic.register();

            }

            if(activeCounter == 3)
            {

                CommunityCenter communityCenter = location as CommunityCenter;

                Dictionary<string, string> bundleData = Game1.netWorldState.Value.BundleData;

                string areaNameFromNumber = CommunityCenter.getAreaNameFromNumber(0);

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

                communityCenter.markAreaAsComplete(0);

                communityCenter.restoreAreaCutscene(0);

                communityCenter.areaCompleteReward(0);

                eventComplete = true;

            }

        }

    }

}