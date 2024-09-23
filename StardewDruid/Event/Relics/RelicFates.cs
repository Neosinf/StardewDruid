
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
    internal class RelicFates: EventHandle
    {

        public RelicFates()
        {

        }

        public override bool TriggerAbort()
        {

            if ((Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[5])
            {

                Mod.instance.questHandle.CompleteQuest(eventId);

                TriggerRemove();

                triggerAbort = true;

            }

            return triggerAbort;

        }

        public override bool TriggerCheck()
        {

            if ((Game1.getLocationFromName("CommunityCenter") as CommunityCenter).numberOfCompleteBundles() < 3)
            {

                Mod.instance.CastDisplay(DialogueData.Strings(DialogueData.stringkeys.noInstructions));

                return false;

            }

            return base.TriggerCheck();
        
        }

        public override void EventActivate()
        {

            base.EventActivate();

            ModUtility.AnimateHands(Game1.player,Game1.player.FacingDirection,600);

            Mod.instance.iconData.DecorativeIndicator(location, Game1.player.Position, IconData.decorations.fates, 4f, new());

            location.playSound(SpellHandle.sounds.discoverMineral.ToString());

        }

        public override void EventInterval()
        {

            activeCounter++;

            if(activeCounter == 1)
            {

                foreach (KeyValuePair<Vector2,IconData.relics> relic in new Dictionary<Vector2,IconData.relics>()
                {

                    [origin + new Vector2(-64,0)] = IconData.relics.box_measurer,
                    [origin + new Vector2(0, -64)] = IconData.relics.box_mortician,
                    [origin + new Vector2(64, 0)] = IconData.relics.box_artisan,
                    [origin + new Vector2(0, 64)] = IconData.relics.box_chaos,

                })
                {

                    Microsoft.Xna.Framework.Rectangle relicRect = IconData.RelicRectangles(relic.Value);

                    TemporaryAnimatedSprite animation = new(0, 3000, 1, 1, relic.Key, false, false)
                    {
                        sourceRect = relicRect,
                        sourceRectStartingPos = new(relicRect.X, relicRect.Y),
                        texture = Mod.instance.iconData.relicsTexture,
                        layerDepth = (Game1.player.Position.Y + 65) / 10000,
                        scale = 3f,
                    };

                    location.TemporarySprites.Add(animation);

                }

            }

            if(activeCounter == 3)
            {

                CommunityCenter communityCenter = location as CommunityCenter;

                Dictionary<string, string> bundleData = Game1.netWorldState.Value.BundleData;

                string areaNameFromNumber = CommunityCenter.getAreaNameFromNumber(5);

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

                communityCenter.markAreaAsComplete(5);

                communityCenter.restoreAreaCutscene(5);

                communityCenter.areaCompleteReward(5);

                eventComplete = true;

            }

        }

    }

}