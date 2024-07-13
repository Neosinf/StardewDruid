
using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewValley;
using StardewValley.GameData.Characters;
using StardewValley.Objects;
using StardewValley.Tools;
using System;
using System.Collections.Generic;


namespace StardewDruid.Journal
{
    public static class EffectsData
    {

        public static Dictionary<string,List<Effect>> EffectList()
        {

            Dictionary<string, List<Effect>> effects = new();

            // ====================================================================
            // Weald effects

            Effect ritesOfTheDruids = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.ritesOfTheDruids.title"),
                icon = IconData.displays.weald,
                description = Mod.instance.Helper.Translation.Get("Effect.ritesOfTheDruids.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.ritesOfTheDruids.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.ritesOfTheDruids.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.ritesOfTheDruids.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.ritesOfTheDruids.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.ritesOfTheDruids.details.3")
                    //"Hold: You can run and ride a horse while holding the rite button.",
                    //"Grass: Performing a rite provides faster movement through grass.",
                }
            };

            Effect herbalism = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.herbalism.title"),
                icon = IconData.displays.herbalism,
                description = Mod.instance.Helper.Translation.Get("Effect.herbalism.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.herbalism.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.herbalism.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.herbalism.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.herbalism.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.herbalism.details.3")
                }
            };

            Effect clear = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.clear.title"),
                icon = IconData.displays.weald,
                description = Mod.instance.Helper.Translation.Get("Effect.clear.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.clear.instruction"),
                details = new()
                {   
                    Mod.instance.Helper.Translation.Get("Effect.clear.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.clear.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.clear.details.2"),
                    
                }
            };

            Effect attunement = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.attunement.title"),
                icon = IconData.displays.weald,
                description = Mod.instance.Helper.Translation.Get("Effect.attunement.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.attunement.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.attunement.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.attunement.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.attunement.details.2")

                }
            };

            effects[QuestHandle.wealdOne] = new() { ritesOfTheDruids, herbalism,  clear, attunement, };

            /*Effect wildbounty = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.wildbounty.title"),
                icon = IconData.displays.weald,
                description = Mod.instance.Helper.Translation.Get("Effect.wildbounty.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.wildbounty.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.wildbounty.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.wildbounty.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.wildbounty.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.wildbounty.details.3"),
                    Mod.instance.Helper.Translation.Get("Effect.wildbounty.details.4"),
                    Mod.instance.Helper.Translation.Get("Effect.wildbounty.details.5"),
                    Mod.instance.Helper.Translation.Get("Effect.wildbounty.details.6"),
                    Mod.instance.Helper.Translation.Get("Effect.wildbounty.details.7"),
                    Mod.instance.Helper.Translation.Get("Effect.wildbounty.details.8"),
                    Mod.instance.Helper.Translation.Get("Effect.wildbounty.details.9"),
                    
                }

            };

            Effect community = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.community.title"),
                icon = IconData.displays.weald,
                description = Mod.instance.Helper.Translation.Get("Effect.community.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.community.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.community.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.community.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.community.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.community.details.3"),
                }
            };*/

            Effect caress = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.caress.title"),
                icon = IconData.displays.weald,
                description = Mod.instance.Helper.Translation.Get("Effect.caress.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.caress.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.caress.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.caress.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.caress.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.caress.details.3"),
                    Mod.instance.Helper.Translation.Get("Effect.caress.details.4"),

                }

            };

            effects[QuestHandle.wealdTwo] = new() {caress, };

            Effect wildgrowth = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.wildgrowth.title"),
                icon = IconData.displays.weald,
                description = Mod.instance.Helper.Translation.Get("Effect.wildgrowth.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.wildgrowth.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.wildgrowth.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.wildgrowth.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.wildgrowth.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.wildgrowth.details.3"),
                    Mod.instance.Helper.Translation.Get("Effect.wildgrowth.details.4"),
                    
                }
            };

            effects[QuestHandle.wealdThree] = new() { wildgrowth };

            Effect cultivate = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.cultivate.title"),
                icon = IconData.displays.weald,
                description = Mod.instance.Helper.Translation.Get("Effect.cultivate.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.cultivate.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.cultivate.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.cultivate.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.cultivate.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.cultivate.details.3"),
                    Mod.instance.Helper.Translation.Get("Effect.cultivate.details.4"),
                    Mod.instance.Helper.Translation.Get("Effect.cultivate.details.5"),
                    Mod.instance.Helper.Translation.Get("Effect.cultivate.details.6"),
                    Mod.instance.Helper.Translation.Get("Effect.cultivate.details.7"),
                    Mod.instance.Helper.Translation.Get("Effect.cultivate.details.8"),
                    Mod.instance.Helper.Translation.Get("Effect.cultivate.details.9"),
                    Mod.instance.Helper.Translation.Get("Effect.cultivate.details.10"),
                   
                }

            };

            effects[QuestHandle.wealdFour] = new() { cultivate };

            Effect rockfall = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.rockfall.title"),
                icon = IconData.displays.weald,
                description = Mod.instance.Helper.Translation.Get("Effect.rockfall.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.rockfall.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.rockfall.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.rockfall.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.rockfall.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.rockfall.details.3"),
                    
                }

            };

            Effect sap = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.sap.title"),
                icon = IconData.displays.weald,
                description = Mod.instance.Helper.Translation.Get("Effect.sap.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.sap.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.sap.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.sap.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.sap.details.2")
                }

            };

            Effect crowhammer = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.crowhammer.title"),
                icon = IconData.displays.chaos,
                description = Mod.instance.Helper.Translation.Get("Effect.crowhammer.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.crowhammer.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.crowhammer.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.crowhammer.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.crowhammer.details.2"),
                }

            };

            effects[QuestHandle.wealdFive] = new() { rockfall, sap, crowhammer,};

            // ====================================================================
            // Runestones

            Effect relicsets = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.relicsets.title"),
                icon = IconData.displays.relic,
                description = Mod.instance.Helper.Translation.Get("Effect.relicsets.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.relicsets.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.relicsets.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.relicsets.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.relicsets.details.2"),
                }
            };

            effects[QuestHandle.challengeWeald] = new() { relicsets };

            // ====================================================================
            // Mists effects

            Effect cursorTargetting = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.cursorTargetting.title"),
                icon = IconData.displays.mists,
                description = Mod.instance.Helper.Translation.Get("Effect.cursorTargetting.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.cursorTargetting.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.cursorTargetting.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.cursorTargetting.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.cursorTargetting.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.cursorTargetting.details.3")
                }

            };

            Effect sunder = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.sunder.title"),
                icon = IconData.displays.mists,
                description = Mod.instance.Helper.Translation.Get("Effect.sunder.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.sunder.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.sunder.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.sunder.details.1"),
                }
            };

            effects[QuestHandle.mistsOne] = new() { cursorTargetting,sunder };

            Effect campfire = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.campfire.title"),
                icon = IconData.displays.mists,
                description = Mod.instance.Helper.Translation.Get("Effect.campfire.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.campfire.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.campfire.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.campfire.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.campfire.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.campfire.details.3"),
                }
            };

            Effect totemShrines = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.totemShrines.title"),
                icon = IconData.displays.mists,
                description = Mod.instance.Helper.Translation.Get("Effect.totemShrines.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.totemShrines.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.totemShrines.details.0"),
                }
            };


            Effect artifice = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.artifice.title"),
                icon = IconData.displays.mists,
                description = Mod.instance.Helper.Translation.Get("Effect.artifice.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.artifice.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.artifice.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.artifice.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.artifice.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.artifice.details.3"),

                }
            };

            effects[QuestHandle.mistsTwo] = new() { campfire, totemShrines, artifice,};

            Effect rodMaster = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.rodMaster.title"),
                icon = IconData.displays.mists,
                description = Mod.instance.Helper.Translation.Get("Effect.rodMaster.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.rodMaster.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.rodMaster.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.rodMaster.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.rodMaster.details.2")
                }
            };

            effects[QuestHandle.mistsThree] = new() { rodMaster, };

            Effect smite = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.smite.title"),
                icon = IconData.displays.mists,
                description = Mod.instance.Helper.Translation.Get("Effect.smite.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.smite.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.smite.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.smite.details.1"),
                }
            };

            Effect veilCharge = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.veilCharge.title"),
                icon = IconData.displays.mists,
                description = Mod.instance.Helper.Translation.Get("Effect.veilCharge.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.veilCharge.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.veilCharge.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.veilCharge.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.veilCharge.details.2"),
                }
            };

            effects[QuestHandle.mistsFour] = new() { smite, veilCharge, };

            Effect summonWisps = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.summonWisps.title"),
                icon = IconData.displays.mists,
                description = Mod.instance.Helper.Translation.Get("Effect.summonWisps.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.summonWisps.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.summonWisps.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.summonWisps.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.summonWisps.details.2")
                    
                }
            };

            Effect summonEffigy = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.summonEffigy.title"),
                icon = IconData.displays.effigy,
                description = Mod.instance.Helper.Translation.Get("Effect.summonEffigy.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.summonEffigy.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.summonEffigy.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.summonEffigy.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.summonEffigy.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.summonEffigy.details.3"),
                }
            };

            // ====================================================================
            // Stars Effects

            effects[QuestHandle.questEffigy] = new() { summonWisps, summonEffigy };

            Effect meteorRain = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.meteorRain.title"),
                icon = IconData.displays.stars,
                description = Mod.instance.Helper.Translation.Get("Effect.meteorRain.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.meteorRain.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.meteorRain.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.meteorRain.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.meteorRain.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.meteorRain.details.3"),
                    Mod.instance.Helper.Translation.Get("Effect.meteorRain.details.4"),
                    Mod.instance.Helper.Translation.Get("Effect.meteorRain.details.5"),
                    Mod.instance.Helper.Translation.Get("Effect.meteorRain.details.6"),
                    Mod.instance.Helper.Translation.Get("Effect.meteorRain.details.7"),
                    Mod.instance.Helper.Translation.Get("Effect.meteorRain.details.8"),
                    Mod.instance.Helper.Translation.Get("Effect.meteorRain.details.9"),
                    Mod.instance.Helper.Translation.Get("Effect.meteorRain.details.10"),
                }
            };

            Effect starBurst = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.starBurst.title"),
                icon = IconData.displays.stars,
                description = Mod.instance.Helper.Translation.Get("Effect.starBurst.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.starBurst.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.starBurst.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.starBurst.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.starBurst.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.starBurst.details.3"),
                }
            };

            effects[QuestHandle.starsOne] = new() { meteorRain, starBurst, };

            Effect gravityWell = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.gravityWell.title"),
                icon = IconData.displays.stars,
                description = Mod.instance.Helper.Translation.Get("Effect.gravityWell.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.gravityWell.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.gravityWell.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.gravityWell.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.gravityWell.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.gravityWell.details.3"),
                    Mod.instance.Helper.Translation.Get("Effect.gravityWell.details.4"),
                }
            };

            effects[QuestHandle.starsTwo] = new() { gravityWell, };

            /*Effect ritualSummon = new()
            {
                title = "Ritual of Summoning",
                icon = IconData.displays.mists,
                description = "The druids would attempt to commune with spirits at times when the barrier between the material and ethereal world had waned. The Lady's power can punch right through the veil.",
                instruction = "Perform a ritual of summoning by channeling (press and hold) Rite of the Mists in specific locations. Fight off the monsters that step through the veil to receive a reward.",
                details = new()
                {
                    "Locations: Druid Grove, Druid Atoll (far eastern beach), and other Druid specific sites.",
                    "Trigger: Stand within the summoning circle and hold Rite of the Mists to cast",
                    "Difficulty: The longer the summoning rite is held (up to five levels, capped by Druid level) the stronger the summoning. " +
                    "Rounds: The number of rounds is determined by summoning strength and Druid level up to ten",
                }
            };*/

            //effects[QuestHandle.questEffigy] = new() { wispSummon, ritualSummon, };

            // ====================================================================
            // Stars Effects

            Effect summonJester = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.summonJester.title"),
                icon = IconData.displays.jester,
                description = Mod.instance.Helper.Translation.Get("Effect.summonJester.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.summonJester.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.summonJester.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.summonJester.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.summonJester.details.2"),
                }
            };

            effects[QuestHandle.approachJester] = new() { summonJester, };            

            Effect whisk = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.whisk.title"),
                icon = IconData.displays.fates,
                description = Mod.instance.Helper.Translation.Get("Effect.whisk.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.whisk.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.whisk.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.whisk.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.whisk.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.whisk.details.3"),
                    Mod.instance.Helper.Translation.Get("Effect.whisk.details.4"),
                }
            };

            effects[QuestHandle.fatesOne] = new() { whisk, };

            Effect warpstrike = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.warpstrike.title"),
                icon = IconData.displays.fates,
                description = Mod.instance.Helper.Translation.Get("Effect.warpstrike.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.warpstrike.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.warpstrike.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.warpstrike.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.warpstrike.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.warpstrike.details.3"),
                }
            };

            Effect curses = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.curses.title"),
                icon = IconData.displays.fates,
                description = Mod.instance.Helper.Translation.Get("Effect.curses.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.curses.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.curses.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.curses.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.curses.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.curses.details.3"),
                    Mod.instance.Helper.Translation.Get("Effect.curses.details.4"),
                }
            };

            effects[QuestHandle.fatesTwo] = new() { warpstrike, curses, };

            Effect tricks = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.tricks.title"),
                icon = IconData.displays.fates,
                description = Mod.instance.Helper.Translation.Get("Effect.tricks.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.tricks.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.tricks.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.tricks.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.tricks.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.tricks.details.3"),
                    Mod.instance.Helper.Translation.Get("Effect.tricks.details.4"),
                }
            };

            effects[QuestHandle.fatesThree] = new() { tricks, };

            Effect enchant = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.enchant.title"),
                icon = IconData.displays.fates,
                description = Mod.instance.Helper.Translation.Get("Effect.enchant.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.enchant.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.enchant.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.enchant.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.enchant.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.enchant.details.3"),
                    Mod.instance.Helper.Translation.Get("Effect.enchant.details.4"),
                    Mod.instance.Helper.Translation.Get("Effect.enchant.details.5"),
                }
            };

            effects[QuestHandle.fatesFour] = new() { enchant, };

           /* Effect revisit = new()
            {
                title = "Revisit",
                icon = IconData.displays.fates,
                description = "The Fates can inhabit moments in time, and though the outcome has been determined in the original moment, a lesson or answer can be gained from witnessing events from a fresh perspective.",
                instruction = "The runestones in the Relics menu can now be used to revisit previous challenges at a higher difficulty level.",
                details = new()
                {
                    "Limit: Each challenge can be revisited once a day",
                    "Each runestone is associated with a specific event and its tooltip contains the instructions to trigger the event" +
                    "The event plays out as it previously did, except without the usual quest prompts or rewards",

                }
            };

            effects[QuestHandle.fatesFive] = new() {revisit, };*/

            Effect summonShadowtin = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.summonShadowtin.title"),
                icon = IconData.displays.shadowtin,
                description = Mod.instance.Helper.Translation.Get("Effect.summonShadowtin.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.summonShadowtin.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.summonShadowtin.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.summonShadowtin.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.summonShadowtin.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.summonShadowtin.details.3"),
                    Mod.instance.Helper.Translation.Get("Effect.summonShadowtin.details.4")
                }
            };

            effects[QuestHandle.challengeFates] = new() { summonShadowtin, };

            // ====================================================
            // Dragon Effects

            Effect dragonForm = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.dragonForm.title"),
                icon = IconData.displays.ether,
                description = Mod.instance.Helper.Translation.Get("Effect.dragonForm.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.dragonForm.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.dragonForm.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.dragonForm.details.1"),
                }
            };

            Effect dragonFlight = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.dragonFlight.title"),
                icon = IconData.displays.ether,
                description = Mod.instance.Helper.Translation.Get("Effect.dragonFlight.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.dragonFlight.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.dragonFlight.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.dragonFlight.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.dragonFlight.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.dragonFlight.details.3"),
                }
            };

            effects[QuestHandle.etherOne] = new() { dragonForm, dragonFlight, };

            Effect dragonBreath = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.dragonBreath.title"),
                icon = IconData.displays.ether,
                description = Mod.instance.Helper.Translation.Get("Effect.dragonBreath.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.dragonBreath.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.dragonBreath.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.dragonBreath.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.dragonBreath.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.dragonBreath.details.3")
                }
            };

            effects[QuestHandle.etherTwo] = new() { dragonBreath, };

            Effect dragonDive = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.dragonDive.title"),
                icon = IconData.displays.ether,
                description = Mod.instance.Helper.Translation.Get("Effect.dragonDive.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.dragonDive.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.dragonDive.details.0") ,
                    Mod.instance.Helper.Translation.Get("Effect.dragonDive.details.1"),
                }
            };

            effects[QuestHandle.etherThree] = new() { dragonDive, };

            Effect dragonTreasure = new()
            {
                title = Mod.instance.Helper.Translation.Get("Effect.dragonTreasure.title"),
                icon = IconData.displays.ether,
                description = Mod.instance.Helper.Translation.Get("Effect.dragonTreasure.description"),
                instruction = Mod.instance.Helper.Translation.Get("Effect.dragonTreasure.instruction"),
                details = new()
                {
                    Mod.instance.Helper.Translation.Get("Effect.dragonTreasure.details.0"),
                    Mod.instance.Helper.Translation.Get("Effect.dragonTreasure.details.1"),
                    Mod.instance.Helper.Translation.Get("Effect.dragonTreasure.details.2"),
                    Mod.instance.Helper.Translation.Get("Effect.dragonTreasure.details.3"),
                }
            };

            effects[QuestHandle.etherFour] = new() { dragonTreasure, };

            /*Effect recollect = new()
            {
                title = "Recollect",
                icon = IconData.displays.ether,
                description = "Ether transcends time and the boundaries of the material realm, yet carries with it the traumas and triumphs of moments it flows through.",
                instruction = "Revisiting past events with Fates: Perception while transformed with Rite of the Ether will alter the event.",
                details = new()
                {
                    "Limit: Triggering the alternate version of the revisited quest still counts to the daily limit",
                    "The event will play out differently, with alternate challenges, bosses and narrative structure",
                }
            };

            effects[QuestHandle.etherFive] = new() { recollect, };*/

            return effects;

        }

    }
    public class Effect
    {

        // -----------------------------------------------
        // journal

        public string title;

        public IconData.displays icon = IconData.displays.none;

        public string description;

        public string instruction;

        public List<string> details;


    }

}
