using StardewDruid.Cast.Fates;
using StardewDruid.Cast.Mists;
using StardewDruid.Character;
using StardewDruid.Journal;
using StardewDruid.Handle;
using StardewDruid.Location.Druid;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Data
{
    public static class JournalData
    {

        public static string JournalTitle(DruidJournal.journalTypes menu)
        {

            switch (menu)
            {

                // main
                case DruidJournal.journalTypes.quests: 

                    return "Quest Journal";

                case DruidJournal.journalTypes.masteries: 

                    return "Disciplines";

                case DruidJournal.journalTypes.relics: 

                    return "Reliquary";

                case DruidJournal.journalTypes.alchemy: 

                    return "Runeboard";

                case DruidJournal.journalTypes.potions: 

                    return "Apothecary";

                case DruidJournal.journalTypes.ledger: 

                    return "Ledger";

                case DruidJournal.journalTypes.dragon: 

                    return "Dragonomicon";

                // quests
                //case DruidJournal.journalTypes.questPage: 

                case DruidJournal.journalTypes.questionPage: 
                    return "Current Quest";

                // skills
                //case DruidJournal.journalTypes.skillPage: 

                case DruidJournal.journalTypes.effects: 
                    return "Grimoire";

                //case DruidJournal.journalTypes.effectPage: 

                case DruidJournal.journalTypes.lore: 
                    return "Chronicle";
                //case DruidJournal.journalTypes.lorePage:

                // relics
                //case DruidJournal.journalTypes.relicPage: 

                // alchemy
                case DruidJournal.journalTypes.omens: 
                    return "Omens Satchel";

                case DruidJournal.journalTypes.trophies: 
                    return "Trophy Case";

                // herbalism
                case DruidJournal.journalTypes.powders: 
                    return "Powderbox";

                case DruidJournal.journalTypes.goods: 
                    return "Goods Store";

                // recruits
                //case DruidJournal.journalTypes.recruitPage: 

                //case DruidJournal.journalTypes.palPage: 

                // other
                case DruidJournal.journalTypes.orders: 

                    return "Guild Orders";

                case DruidJournal.journalTypes.guilds:

                    return "Guild Details";

                case DruidJournal.journalTypes.distillery: 

                    return "Distillery";

                case DruidJournal.journalTypes.distilleryEstimated: 

                    return "Estimated Production";

                case DruidJournal.journalTypes.distilleryRecent: 

                    return "Recent Production";

                //case DruidJournal.journalTypes.guildPage: 
  
                case DruidJournal.journalTypes.battle: 

                    return "Battle!";

                default:

                    return null;
            }
        
        }

        public static string ButtonStrings(DruidJournal.journalButtons button)
        {

            switch (button)
            {

                case DruidJournal.journalButtons.back:

                    return "Go Back";

                case DruidJournal.journalButtons.start:

                    return "Return to Start";

                case DruidJournal.journalButtons.forward:

                    return "Go Forward";

                case DruidJournal.journalButtons.end:

                    return "Skip to End";

                case DruidJournal.journalButtons.exit:

                    return "Exit";

                case DruidJournal.journalButtons.openQuests:

                    return "View quests";

                case DruidJournal.journalButtons.openMasteries:

                    return "View masteries";

                case DruidJournal.journalButtons.openRelics:

                    return "View relics";

                case DruidJournal.journalButtons.openPotions:

                    return "View potions";

                case DruidJournal.journalButtons.openAlchemy:

                    return "Open alchemy menu";

                case DruidJournal.journalButtons.openEffects:

                    return "Review rite effects";

                case DruidJournal.journalButtons.openLore:

                    return "Review druid lore";

                case DruidJournal.journalButtons.openDragonomicon:

                    return "Configure dragon form";

                case DruidJournal.journalButtons.getHint:

                    return "Get quest hint";

                case DruidJournal.journalButtons.openPowders:

                    return "View powders";

                case DruidJournal.journalButtons.openCompanions:

                    return "View companion";

                case DruidJournal.journalButtons.openGoods:
                case DruidJournal.journalButtons.openGoodsDistillery:
  
                    return "View goods";

                case DruidJournal.journalButtons.openOmens:

                    return "View collected omens";

                case DruidJournal.journalButtons.openTrophies:

                    return "View collected trophies";


                case DruidJournal.journalButtons.openOrders:

                    return "View guild orders";

                case DruidJournal.journalButtons.openGuilds:

                    return "View guild details";

                case DruidJournal.journalButtons.openDistillery:

                    return "View distillery machines";

                case DruidJournal.journalButtons.openDistilleryInventory:

                    return "View distillery inventory";

                case DruidJournal.journalButtons.openProductionEstimated:

                    return "View estimated distillery production";

                case DruidJournal.journalButtons.openProductionRecent:

                    return "View recent distillery production";


                case DruidJournal.journalButtons.active:

                    return "Sort by active entries";

                case DruidJournal.journalButtons.reverse:

                    return "Reverse order of entries";

                case DruidJournal.journalButtons.refresh:

                    return "Refresh all";

                case DruidJournal.journalButtons.skipQuest:

                    return "Skip quest";

                case DruidJournal.journalButtons.replayQuest:

                    return "Replay quest";

                case DruidJournal.journalButtons.replayTomorrow:

                    return "Quest can be replayed tomorrow";

                case DruidJournal.journalButtons.cancelReplay:

                    return "Cancel quest replay";

                case DruidJournal.journalButtons.viewEffect:

                    return "View related effect";

                case DruidJournal.journalButtons.viewQuest:

                    return "View related quest";

                case DruidJournal.journalButtons.dragonCopy:

                    return "Copy preset configuration to custom slot";

                case DruidJournal.journalButtons.dragonSave:

                    return "Save configuration";

                case DruidJournal.journalButtons.clearBuffs:

                    return "Clear applied potion and powder buffs";

                case DruidJournal.journalButtons.summonCompanion:

                    return "Summon companion";

                case DruidJournal.journalButtons.dismissCompanion:

                    return "Dismiss companion";

                case DruidJournal.journalButtons.summonPal:

                    return "Summon creature";

                case DruidJournal.journalButtons.dismissPal:

                    return "Dismiss creature";

                case DruidJournal.journalButtons.schemePal:

                    return "Change creature scheme";

                case DruidJournal.journalButtons.renamePal:

                    return "Rename creature";

                case DruidJournal.journalButtons.rewildPal:

                    return "Allow creature to roam wilderness";

                case DruidJournal.journalButtons.HP:

                    return StringData.Get(StardewDruid.Data.StringData.str.HP);

                case DruidJournal.journalButtons.STM:

                    return StringData.Get(StardewDruid.Data.StringData.str.STM);

                case DruidJournal.journalButtons.previous:

                    return "Previous Menu";

                case DruidJournal.journalButtons.levelUp:

                    return "Level Up";

                default:

                    return null;
            }

        }

    }

}
