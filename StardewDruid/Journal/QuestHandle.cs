using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Event;
using StardewDruid.Event.Challenge;
using StardewDruid.Event.Scene;
using StardewDruid.Event.Sword;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Constants;
using StardewValley.GameData.Pets;
using StardewValley.Locations;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using xTile.Dimensions;
using static StardewValley.Menus.CharacterCustomization;


namespace StardewDruid.Journal
{
    public class QuestHandle
    {

        public Dictionary<string, Quest> quests = new();

        public Dictionary<string, List<Effect>> effects = new();

        public Dictionary<LoreData.stories, Lorestory> lores = new();

        public Dictionary<string, List<LoreData.stories>> loresets = new();

        public string lorekey;

        public Dictionary<CharacterHandle.characters, List<string>> prompts = new();

        public enum milestones
        {
            none,
            effigy,
            weald_weapon,
            weald_lessons,
            weald_challenge,
            mists_weapon,
            mists_lessons,
            quest_effigy,
            mists_challenge,
            stars_weapon,
            stars_lessons,
            stars_challenge,
            stars_threats,
            jester,
            fates_weapon,
            fates_lessons,
            quest_jester,
            fates_enchant,
            fates_challenge,
            ether_weapon,
            ether_lessons,
            quest_shadowtin,
            ether_treasure,
            ether_challenge,

        }

        public static Dictionary<milestones, List<string>> milestoneQuests = new()
        {

            [milestones.effigy] = new() { approachEffigy, },
            [milestones.weald_weapon] = new() { swordWeald, },
            [milestones.weald_lessons] = new() { herbalism, wealdOne, wealdTwo, wealdThree, wealdFour, wealdFive, },
            [milestones.weald_challenge] = new() { challengeWeald, },
            [milestones.mists_weapon] = new() { swordMists },
            [milestones.mists_lessons] = new() { mistsOne, mistsTwo, mistsThree, mistsFour, },
            [milestones.quest_effigy] = new() { questEffigy, },
            [milestones.mists_challenge] = new() { challengeMists, },
            [milestones.stars_weapon] = new() { swordStars, },
            [milestones.stars_lessons] = new() { starsOne, starsTwo, },
            [milestones.stars_challenge] = new() { challengeStars, },
            [milestones.stars_threats] = new() { challengeAtoll, challengeDragon, },
            [milestones.jester] = new() { approachJester, },
            [milestones.fates_weapon] = new() { swordFates, },
            [milestones.fates_lessons] = new() { fatesOne, fatesTwo, fatesThree, },
            [milestones.quest_jester] = new() { questJester, },
            [milestones.fates_enchant] = new() { fatesFour,  },
            [milestones.fates_challenge] = new() { challengeFates, },
            [milestones.ether_weapon] = new() { swordEther, },
            [milestones.ether_lessons] = new() { etherOne, etherTwo, etherThree,  },
            [milestones.quest_shadowtin] = new() { questShadowtin, },
            [milestones.ether_treasure] = new() { etherFour, },
            [milestones.ether_challenge] = new() { challengeEther, },

        };

        public const string approachEffigy = "approachEffigy";

        public const string swordWeald = "swordWeald";

        public const string herbalism = "herbalism";

        public const string wealdOne = "clearance";

        public const string wealdTwo = "wildbounty";

        public const string wealdThree = "wildgrowth";

        public const string wealdFour = "cultivate";

        public const string wealdFive = "rockfall";

        public const string challengeWeald = "aquifer";

        public const string swordMists = "swordMists";

        public const string mistsOne = "sunder";

        public const string mistsTwo = "artifice";

        public const string mistsThree = "fishing";

        public const string mistsFour = "smite";

        public const string questEffigy = "questEffigy";

        public const string challengeMists = "graveyard";

        public const string swordStars = "swordStars";

        public const string starsOne = "meteor";

        public const string starsTwo = "gravity";

        public const string challengeStars = "infestation";

        public const string challengeAtoll = "seafarers";

        public const string challengeDragon = "dragon";

        public const string approachJester = "approachJester";

        public const string swordFates = "swordFates";

        public const string fatesOne = "whisk";

        public const string fatesTwo = "warpstrike";

        public const string fatesThree = "tricks";

        public const string questJester = "questJester";

        public const string fatesFour = "enchant";

        public const string challengeFates = "rogues";

        public const string swordEther = "swordEther";

        public const string etherOne = "transform";

        public const string etherTwo = "breath";

        public const string etherThree = "dive";

        public const string questShadowtin = "questShadowtin";

        public const string etherFour = "treasure";

        public const string challengeEther = "dustfiends";


        // optional

        public const string relicWeald = "relicWeald";

        public const string relicMists = "relicMists";

        public const string relicFates = "relicFates";

        public const string relicEther = "relicEther";


        public QuestHandle()
        {

            quests = QuestData.QuestList();

            effects = EffectsData.EffectList();

            loresets = LoreData.StorySets();

            lores = LoreData.LoreList();

        }

        // ----------------------------------------------------------------------

        /*public void FixQuests(string questId)
        {

            QuestProgress progress = Mod.instance.save.progress[questId];

            switch (questId)
            {
                case "wealdChallenge":
                case "challengeWeald":

                    Mod.instance.save.progress[challengeWeald] = new(progress.status,progress.delay,progress.progress,progress.replay);

                    break;

                case "challengeMists":

                    Mod.instance.save.progress[challengeMists] = new(progress.status, progress.delay, progress.progress, progress.replay);

                    break;

            }

            Mod.instance.save.progress.Remove(questId);

        }*/

        public List<List<string>> OrganiseQuests(bool active = false, bool reverse = false)
        {

            List<List<string>> source = new();

            List<string> pageList = new();

            List<string> activeList = new();

            foreach(string id in quests.Keys)
            {

                if (!Mod.instance.save.progress.ContainsKey(id))
                {
                    continue;
                }

                QuestProgress progress = Mod.instance.save.progress[id];

                if (progress.status == 1 || progress.status == 4)
                {
                    if (active)
                    {

                        activeList.Add(id);

                    }
                    else
                    {

                        pageList.Add(id);

                    }

                }
                else
                if (progress.status >= 1)
                {

                    pageList.Add(id);

                }
            
            }

            if (reverse)
            {

                pageList.Reverse();

                activeList.Reverse();
            }
            
            if (active)
            {

                activeList.AddRange(pageList);

                pageList = activeList;
            }

            foreach (string page in pageList)
            {

                if (source.Count == 0 || source.Last().Count() == 6)
                {
                    source.Add(new List<string>());
                }

                source.Last().Add(page);

            }

            return source;

        }

        public Dictionary<int,Journal.ContentComponent> JournalQuests()
        {

            Dictionary<int, Journal.ContentComponent> journal = new();

            List<string> pageList = new();

            List<string> activeList = new();

            foreach (string id in quests.Keys)
            {

                if (!Mod.instance.save.progress.ContainsKey(id))
                {
                    continue;
                }

                QuestProgress progress = Mod.instance.save.progress[id];

                if (progress.status == 1 || progress.status == 4)
                {
                    if (Mod.instance.Config.activeJournal)
                    {

                        activeList.Add(id);

                    }
                    else
                    {

                        pageList.Add(id);

                    }

                }
                else
                if (progress.status >= 1)
                {

                    pageList.Add(id);

                }

            }

            if (Mod.instance.Config.reverseJournal)
            {

                pageList.Reverse();

                activeList.Reverse();
            }

            if (Mod.instance.Config.activeJournal)
            {

                activeList.AddRange(pageList);

                pageList = activeList;

            }

            int start = 0;

            foreach (string page in pageList)
            {

                Journal.ContentComponent content = new(ContentComponent.contentTypes.list, page);

                content.text = quests[page].title;

                content.icon = quests[page].icon;

                journal[start++] = content;

            }

            return journal;

        }

        public List<List<string>> OrganiseEffects(bool reverse = false)
        {

            List<List<string>> source = new();

            List<string> pageList = new();

            foreach (KeyValuePair<string, QuestProgress> pair in Mod.instance.save.progress)
            {

                string id = pair.Key;

                QuestProgress progress = pair.Value;

                if (!quests.ContainsKey(id))
                {

                    continue;

                }

                int requirement = quests[id].type == Quest.questTypes.lesson ? 1 : 2;

                if (progress.status >= requirement)
                {

                    if (effects.ContainsKey(id))
                    {

                        for(int i = 0; i < effects[id].Count; i++)
                        {

                            pageList.Add(id+"|"+i.ToString());

                        }

                    }

                }

            }

            if (reverse)
            {

                pageList.Reverse();

            }

            foreach (string page in pageList)
            {

                if (source.Count == 0 || source.Last().Count() == 6)
                {
                    source.Add(new List<string>());
                }

                source.Last().Add(page);

            }

            return source;

        }

        // ----------------------------------------------------------------------

        public void Ready()
        {

            if (!Context.IsMainPlayer)
            {

                Farmhand();

                return;

            }

            List<string> keys = new(Mod.instance.save.progress.Keys);

            /*foreach (string key in keys)
            {
                
                if (!quests.ContainsKey(key))
                {

                    FixQuests(key);

                }

            }*/

            keys = new(Mod.instance.save.progress.Keys);

            foreach(string key in keys)
            {

                if (Mod.instance.save.progress[key].delay > 0)
                {

                    Mod.instance.save.progress[key].delay -= 1;

                }

                if (!quests.ContainsKey(key))
                {

                    continue;

                }

                Quest quest = quests[key];

                if (Mod.instance.save.progress[key].status == 0 && Mod.instance.save.progress[key].delay <= 0)
                {

                    if (quest.give != Quest.questGivers.dialogue || Mod.instance.Config.autoProgress)
                    {

                        Mod.instance.save.progress[key].status = 1;

                        Initialise(key);

                    }

                    DialogueBefore(key);

                }
                else
                if (Mod.instance.save.progress[key].status == 1)
                {

                    Initialise(key);

                }
                else
                if (Mod.instance.save.progress[key].status >= 2)
                {

                    if(loresets.ContainsKey(key))
                    {

                        lorekey = key;

                    }

                    Implement(key);

                }

                if (Mod.instance.save.progress[key].status == 3)
                {

                    Mod.instance.save.progress[key].status = 2;

                }

            }

            // Sync changes
            Mod.instance.SyncMultiplayer();


        }

        public void Farmhand()
        {
            List<string> keys = new(Mod.instance.save.progress.Keys);

            foreach (string key in keys)
            {

                if (!quests.ContainsKey(key))
                {

                    continue;

                }

                Quest quest = quests[key];

                if (Mod.instance.save.progress[key].status == 1)
                {

                    Localise(key);

                }
                else
                if (Mod.instance.save.progress[key].status >= 2)
                {

                    if (loresets.ContainsKey(key))
                    {

                        lorekey = key;

                    }

                    Fulfillment(key);

                }

            }

        }

        public void Promote(milestones milestone)
        {

            if (!Context.IsMainPlayer)
            {

                return;

            }

            foreach(KeyValuePair<milestones,List<string>> mile in milestoneQuests)
            {
                
                if (mile.Key == milestone)
                {
                    foreach (string questId in mile.Value)
                    {

                        if (quests[questId].give == Quest.questGivers.dialogue)
                        {

                            Mod.instance.save.progress[questId] = new();
                        }
                        else
                        {

                            Mod.instance.save.progress[questId] = new(1);

                        }

                    }

                    break;

                }

                foreach (string questId in mile.Value)
                {

                    Mod.instance.save.progress[questId] = new(2);

                }

                Mod.instance.save.milestone = mile.Key;

            }

        }

        public void AssignQuest(string questId)
        {

            if (!Context.IsMainPlayer)
            {

                return;

            }

            if (!quests.ContainsKey(questId))
            {

                return;

            }

            if (quests[questId].give == Quest.questGivers.dialogue)
            {

                Mod.instance.save.progress[questId] = new();

                DialogueBefore(questId);

            }
            else
            {

                Mod.instance.save.progress[questId] = new(1);

                Initialise(questId);

                DialogueBefore(questId);

                // Sync changes
                Mod.instance.SyncMultiplayer();

            }

        }

        public void RevisitQuest(string questId)
        {

            if (!Context.IsMainPlayer)
            {

                return;

            }

            Initialise(questId);

            Mod.instance.save.progress[questId].status = 4;

            // Sync changes
            Mod.instance.SyncMultiplayer();

        }

        public void CompleteQuest(string questId,int questRating = 0)
        {

            if (!Context.IsMainPlayer)
            {
                return;

            }
                
            if (!Mod.instance.save.progress.ContainsKey(questId))
            {

                Mod.instance.save.progress[questId] = new();

            }

            if (Mod.instance.save.progress[questId].status <= 1)
            {

                if (loresets.ContainsKey(questId))
                {

                    lorekey = questId;

                }

                OnComplete(questId,questRating);

                Implement(questId);

                DialogueAfter(questId);

            }
            else
            {

                OnReplayComplete(questId, questRating);

            }

            Mod.instance.save.progress[questId].status = 3;

            Mod.instance.CastMessage(quests[questId].title + " " + DialogueData.Strings(DialogueData.stringkeys.questComplete), 1, true);

            if (quests[questId].reward > 0)
            {
                float adjustReward = 1.2f - ((float)Mod.instance.ModDifficulty() * 0.1f);

                Game1.player.Money += (int)((float)quests[questId].reward * adjustReward);

            }

            Game1.playSound("yoba");

            // Sync changes
            Mod.instance.SyncMultiplayer();

        }

        public int UpdateTask(string quest, int update)
        {

            if (!Mod.instance.save.progress.ContainsKey(quest))
            {
                return -1;

            }

            if (Mod.instance.save.progress[quest].status != 1)
            {

                return -1;

            }

            int progress = Mod.instance.save.progress[quest].progress + update;

            if (!Context.IsMainPlayer)
            {

                QueryData queryData = new()
                {
                    name =quest,
                    value = update.ToString(),
                };

                Mod.instance.EventQuery(queryData, QueryData.queries.QuestUpdate);

                return progress;

            }

            int limit = quests[quest].requirement;

            Mod.instance.save.progress[quest].progress = Math.Min(progress,limit);

            if(progress >= limit)
            {

                CompleteQuest(quest);

                return progress;

            }

            int portion = (limit / 2);

            if (portion != 0)
            {

                if (progress % portion == 0)
                {

                    Mod.instance.CastMessage(quests[quest].title + " " + ((progress * 100) / limit).ToString() + " " + DialogueData.Strings(DialogueData.stringkeys.questComplete), 2, true);
                }

            }

            return progress;

        }

        public void TaskSet(string quest, int set)
        {
            
            if (Mod.instance.save.progress.ContainsKey(quest))
            {

                Mod.instance.save.progress[quest].progress = set;

            }

        }

        public bool IsComplete(string quest)
        {

            if (Mod.instance.save.progress.ContainsKey(quest))
            {

                return (Mod.instance.save.progress[quest].status >= 2);

            }

            return false;

        }

        public bool IsOpen(string quest)
        {

            if (Mod.instance.save.progress.ContainsKey(quest))
            {

                return (Mod.instance.save.progress[quest].status <= 1);

            }

            return false;

        }

        public bool IsGiven(string quest)
        {

            if (Mod.instance.save.progress.ContainsKey(quest))
            {

                return (Mod.instance.save.progress[quest].status >= 1);

            }

            return false;

        }

        public bool IsReplayable(string quest)
        {

            switch (quests[quest].type)
            {

                case Quest.questTypes.sword:
                case Quest.questTypes.heart:
                case Quest.questTypes.challenge:

                        return true;

            }

            return false;

        }

        public bool IsQuestGiver(CharacterHandle.characters character)
        {

            if (prompts.ContainsKey(character))
            {

                List<string> loadout = new(prompts[character]);

                for (int q = loadout.Count - 1; q >= 0; q--)
                {

                    if (IsComplete(loadout[q]))
                    {

                        prompts[character].RemoveAt(q);

                    }

                }

                if (prompts[character].Count > 0)
                {

                    return true;

                }

            }

            return false;

        }

        // ----------------------------------------------------------------------

        public void Localise(string questId)
        {
            switch (questId)
            {

                case approachEffigy:

                    LocationData.DruidLocations(LocationData.druid_grove_name);

                return;

                case herbalism:

                    (Mod.instance.locations[LocationData.druid_grove_name] as Grove).HerbalTiles(true);

                return;

                case wealdFive:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.crow_hammer.ToString());
                    return;

                case swordMists:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.wayfinder_censer.ToString());

                    LocationData.DruidLocations(LocationData.druid_atoll_name);

                return;

                case mistsTwo:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.herbalism_pan.ToString());

                return;

                case swordStars:

                    LocationData.DruidLocations(LocationData.druid_chapel_name);

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.wayfinder_lantern.ToString());

                return;

                case starsOne:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.herbalism_still.ToString());

                return;

                case challengeDragon:

                    LocationData.DruidLocations(LocationData.druid_vault_name);

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.wayfinder_water.ToString());

                return;

                case swordFates:

                    LocationData.DruidLocations(LocationData.druid_court_name);

                return;

                case questJester:

                    LocationData.DruidLocations(LocationData.druid_archaeum_name);

                return;

                case fatesFour:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.herbalism_crucible.ToString());

                return;

                case swordEther:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.wayfinder_ceremonial.ToString());

                return;

                case etherOne:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.dragon_form.ToString());

                return;

                case questShadowtin:

                    LocationData.DruidLocations(LocationData.druid_engineum_name);

                    return;

                case etherFour:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.herbalism_gauge.ToString());

                    return;

                case challengeEther:

                    LocationData.DruidLocations(LocationData.druid_gate_name);

                    return;

            }

        }

        public void Initialise(string questId)
        {

            Localise(questId);

            switch (questId)
            {
                
                case approachEffigy:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Scene.ApproachEffigy().EventSetup(questId);

                    }

                    return;

                case swordWeald:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Sword.SwordWeald().EventSetup(questId);

                    }

                    return;

                case wealdOne:

                    CheckAssignment(wealdTwo,1);

                    return;

                case wealdTwo:

                    CheckAssignment(wealdThree,1);

                    return;

                case wealdThree:

                    CheckAssignment(wealdFour,1);

                    return;

                case wealdFour:

                    CheckAssignment(wealdFive,1);

                    return;

                case wealdFive:

                    CheckAssignment(challengeWeald,1);

                    return;

                case challengeWeald:

                    new Event.Challenge.ChallengeWeald().EventSetup(questId);

                    return;

                case swordMists:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Sword.SwordMists().EventSetup(questId);
                    
                    }

                    return;

                case mistsOne:

                    CheckAssignment(mistsTwo,1);

                    return;

                case mistsTwo:

                    CheckAssignment(mistsThree,1);

                    return;

                case mistsThree:

                    CheckAssignment(mistsFour,1);

                    return;

                case mistsFour:

                    CheckAssignment(questEffigy,1);

                    return;

                case questEffigy:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Scene.QuestEffigy().EventSetup(questId);

                    }

                    return;

                case challengeMists:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Challenge.ChallengeMists().EventSetup(questId);

                    }

                    return;

                case swordStars:

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Revenant, Character.Character.mode.home);

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Sword.SwordStars().EventSetup(questId);

                    }

                    return;

                case starsOne:

                    CheckAssignment(starsTwo, 1);

                    return;

                case starsTwo:

                    CheckAssignment(challengeStars, 1);

                    CheckAssignment(challengeAtoll, 1);

                    CheckAssignment(challengeDragon, 1);

                    return;

                case challengeStars:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Challenge.ChallengeStars().EventSetup(questId);

                    }

                    return;

                case challengeAtoll:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Challenge.ChallengeAtoll().EventSetup(questId);

                    }

                    return;

                case challengeDragon:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Challenge.ChallengeDragon().EventSetup(questId);

                    }
                    return;

                case approachJester:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Scene.ApproachJester().EventSetup(questId);

                    }

                    return;

                case swordFates:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Sword.SwordFates().EventSetup(questId);

                    }

                    return;

                case fatesOne:

                    CheckAssignment(fatesTwo, 1);

                    return;

                case fatesTwo:

                    CheckAssignment(fatesThree, 1);

                    return;

                case fatesThree:

                    CheckAssignment(questJester, 1);

                    return;

                case questJester:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Scene.QuestJester().EventSetup(questId);

                    }

                    return;

                case fatesFour:

                    CheckAssignment(challengeFates, 1);

                    return;

                case challengeFates:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Challenge.ChallengeFates().EventSetup(questId);

                    }

                    return;

                case swordEther:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Sword.SwordEther().EventSetup(questId);

                    }

                    return;

                case etherOne:

                    CheckAssignment(etherTwo, 1);

                    return;

                case etherTwo:

                    CheckAssignment(etherThree, 1);

                    return;

                case etherThree:

                    CheckAssignment(questShadowtin, 1);

                    return;

                case questShadowtin:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Scene.QuestShadowtin().EventSetup(questId);

                    }

                    return;

                case etherFour:

                    CheckAssignment(challengeEther, 1);

                    return;

                case challengeEther:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Challenge.ChallengeEther().EventSetup(questId);

                    }

                    return;

                // =====================================================================

                case relicWeald:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Relics.RelicWeald().EventSetup(questId);

                    }

                    return;

                case relicMists:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Relics.RelicMists().EventSetup(questId);

                    }

                    return;

                case relicFates:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Relics.RelicFates().EventSetup(questId);

                    }

                    return;

                case relicEther:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Relics.RelicEther().EventSetup(questId);

                    }

                    return;


            }

        }

        public void Fulfillment(string questId)
        {

            switch (questId)
            {

                case approachEffigy:

                    LocationData.DruidLocations(LocationData.druid_grove_name);

                    return;

                case swordWeald:

                    (Mod.instance.locations[LocationData.druid_grove_name] as Grove).AddDialogueTiles();

                    return;

                case herbalism:

                    (Mod.instance.locations[LocationData.druid_grove_name] as Grove).HerbalTiles();

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.herbalism_mortar.ToString());

                    return;

                case wealdFive:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.crow_hammer.ToString());

                    return;

                case challengeWeald:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.runestones_spring.ToString());

                    return;

                case swordMists:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.wayfinder_censer.ToString());

                    LocationData.DruidLocations(LocationData.druid_atoll_name);

                    (Mod.instance.locations[LocationData.druid_atoll_name] as Atoll).AddDialogueTiles();

                    return;

                case mistsTwo:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.herbalism_pan.ToString());

                    return;

                case questEffigy:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.effigy_crest.ToString());

                    return;

                case swordStars:

                    LocationData.DruidLocations(LocationData.druid_chapel_name);

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.wayfinder_lantern.ToString());

                    break;

                case starsOne:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.herbalism_still.ToString());

                    return;

                case challengeStars:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.runestones_moon.ToString());

                    return;

                case challengeAtoll:
                case challengeDragon:

                    if (questId == challengeAtoll)
                    {
                        Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.runestones_farm.ToString());
                    }

                    if (questId == challengeDragon)
                    {

                        Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.wayfinder_water.ToString());

                        Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.runestones_cat.ToString());
                    }

                    return;

                case approachJester:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.jester_dice.ToString());

                    break;

                case swordFates:

                    LocationData.DruidLocations(LocationData.druid_court_name);

                    break;

                case fatesOne:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.wayfinder_eye.ToString());

                    return;

                case fatesFour:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.herbalism_crucible.ToString());

                    return;

                case challengeFates:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.book_wyrven.ToString());

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.shadowtin_tome.ToString());

                    LocationData.DruidLocations(LocationData.druid_tomb_name);

                    return;

                case swordEther:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.wayfinder_ceremonial.ToString());

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.box_measurer.ToString());

                    (Mod.instance.locations[LocationData.druid_court_name] as Court).AddDialogueTiles();

                    return;

                case etherOne:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.dragon_form.ToString());

                    return;

                case questShadowtin:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.wayfinder_dwarf.ToString());

                    LocationData.DruidLocations(LocationData.druid_engineum_name);

                    return;

                case etherFour:

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.herbalism_gauge.ToString());

                    return;

                case challengeEther:

                    LocationData.DruidLocations(LocationData.druid_gate_name);

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.stardew_druid.ToString());

                    return;

            }

        }

        public void Implement(string questId)
        {

            Fulfillment(questId);

            switch (questId)
            {

                case approachEffigy:

                    Character.Character.mode effigyMode = 
                        Mod.instance.save.characters.ContainsKey(CharacterHandle.characters.Effigy) ?
                        Mod.instance.save.characters[CharacterHandle.characters.Effigy] : 
                        Character.Character.mode.home;

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Effigy, effigyMode);

                    CheckAssignment(swordWeald, 0);

                    Milecrossed(milestones.effigy);

                    return;

                case swordWeald:

                    Milecrossed(milestones.weald_weapon);

                    CheckAssignment(wealdOne, 0);

                    CheckAssignment(herbalism, 0);

                    return;

                case wealdOne:

                    CheckAssignment(wealdTwo, 0);

                    return;

                case wealdTwo:

                    CheckAssignment(wealdThree, 0);

                    return;

                case wealdThree:

                    CheckAssignment(wealdFour, 0);

                    return;

                case wealdFour:

                    CheckAssignment(wealdFive, 0);

                    return;

                case wealdFive:

                    CheckAssignment(challengeWeald, 0);

                    if (!IsComplete(wealdOne)) { return; }
                    if (!IsComplete(wealdTwo)) { return; }
                    if (!IsComplete(wealdThree)) { return; }
                    if (!IsComplete(wealdFour)) { return; }

                    Milecrossed(milestones.weald_lessons);

                    return;

                case challengeWeald:

                    Milecrossed(milestones.weald_challenge);

                    CheckAssignment(swordMists, 1);

                    return;

                case swordMists:

                    Milecrossed(milestones.mists_weapon);

                    CheckAssignment(mistsOne, 0);

                    return;

                case mistsOne:

                    CheckAssignment(mistsTwo, 0);

                    return;

                case mistsTwo:

                    CheckAssignment(mistsThree, 0);

                    return;

                case mistsThree:

                    CheckAssignment(mistsFour, 0);

                    return;

                case mistsFour:

                    CheckAssignment(questEffigy, 0);

                    if (!IsComplete(mistsOne)) { return; }
                    if (!IsComplete(mistsTwo)) { return; }
                    if (!IsComplete(mistsThree)) { return; }

                    Milecrossed(milestones.mists_lessons);

                    return;

                case questEffigy:

                    Milecrossed(milestones.quest_effigy);

                    CheckAssignment(challengeMists, 1);

                    return;

                case challengeMists:

                    Milecrossed(milestones.mists_challenge);

                    CheckAssignment(swordStars, 1);

                    return;

                case swordStars:

                    Milecrossed(milestones.stars_weapon);

                    CheckAssignment(starsOne, 0);

                    CheckAssignment(starsTwo, 1);

                    if (!Mod.instance.characters.ContainsKey(CharacterHandle.characters.Revenant))
                    {

                        CharacterHandle.CharacterLoad(CharacterHandle.characters.Revenant, Character.Character.mode.home);

                    }

                    break;

                case starsOne:

                    CheckAssignment(starsTwo, 0);

                    return;

                case starsTwo:

                    CheckAssignment(challengeStars, 0);

                    if (!IsComplete(starsOne)) { return; }

                    Milecrossed(milestones.stars_lessons);

                    return;

                case challengeStars:

                    CheckAssignment(challengeAtoll, 0);

                    CheckAssignment(challengeDragon, 0);

                    Milecrossed(milestones.stars_challenge);

                    return;

                case challengeAtoll:
                case challengeDragon:

                    if (!IsComplete(challengeAtoll)) { return; }

                    if (!IsComplete(challengeDragon)) { return; }

                    Milecrossed(milestones.stars_threats);

                    CheckAssignment(approachJester, 1);

                    return;

                case approachJester:

                    Character.Character.mode jesterMode = 
                        Mod.instance.save.characters.ContainsKey(CharacterHandle.characters.Jester) ? 
                        Mod.instance.save.characters[CharacterHandle.characters.Jester] : 
                        Character.Character.mode.home;

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Jester, jesterMode);

                    CheckAssignment(swordFates, 0);

                    Milecrossed(milestones.jester);

                    break;

                case swordFates:

                    Milecrossed(milestones.fates_weapon);

                    CheckAssignment(fatesOne, 0);

                    CheckAssignment(fatesTwo, 1);

                    break;

                case fatesOne:

                    CheckAssignment(fatesTwo, 0);

                    return;

                case fatesTwo:

                    CheckAssignment(fatesThree, 0);

                    return;

                case fatesThree:

                    CheckAssignment(questJester, 0);

                    if (!IsComplete(fatesOne)) { return; }
                    if (!IsComplete(fatesTwo)) { return; }

                    Milecrossed(milestones.fates_lessons);

                    return;

                case questJester:

                    CheckAssignment(fatesFour, 1);

                    Milecrossed(milestones.quest_jester);

                    if (!Mod.instance.characters.ContainsKey(CharacterHandle.characters.Buffin))
                    {

                        CharacterHandle.CharacterLoad(CharacterHandle.characters.Buffin, Character.Character.mode.home);

                    }

                    return;

                case fatesFour:

                    CheckAssignment(challengeFates, 1);

                    Milecrossed(milestones.fates_enchant);

                    return;

                case challengeFates:

                    if (!Mod.instance.characters.ContainsKey(CharacterHandle.characters.Shadowtin))
                    {

                        Character.Character.mode shadowtinMode =
                            Mod.instance.save.characters.ContainsKey(CharacterHandle.characters.Shadowtin) ?
                            Mod.instance.save.characters[CharacterHandle.characters.Shadowtin] :
                            Character.Character.mode.home;

                        CharacterHandle.CharacterLoad(CharacterHandle.characters.Shadowtin, shadowtinMode);

                    }

                    CheckAssignment(swordEther,0);

                    Milecrossed(milestones.fates_challenge);

                    return;

                case swordEther:

                    CheckAssignment(etherOne, 0);

                    CheckAssignment(etherTwo, 1);

                    Milecrossed(milestones.ether_weapon);

                    return;

                case etherOne:

                    CheckAssignment(etherTwo, 1);

                    return;

                case etherTwo:

                    CheckAssignment(etherThree, 1);

                    return;

                case etherThree:

                    CheckAssignment(questShadowtin, 1);

                    if (!IsComplete(etherOne)) { return; }
                    if (!IsComplete(etherTwo)) { return; }

                    Milecrossed(milestones.ether_lessons);

                    return;

                case questShadowtin:

                    CheckAssignment(etherFour, 1);

                    Milecrossed(milestones.quest_shadowtin);

                    return;

                case etherFour:

                    CheckAssignment(challengeEther, 1);

                    Milecrossed(milestones.ether_treasure);

                    return;

                case challengeEther:

                    Milecrossed(milestones.ether_challenge);

                    return;

            }

        }

        public void OnAccept(string questId)
        {

            switch (questId)
            {

                case mistsTwo:

                    ThrowHandle throwPan = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Effigy].Position, IconData.relics.herbalism_pan);

                    throwPan.register();

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.herbalism_pan.ToString());

                    Mod.instance.save.herbalism[HerbalData.herbals.melius_ligna] = 3;

                    Mod.instance.save.herbalism[HerbalData.herbals.melius_impes] = 3;

                    Mod.instance.save.herbalism[HerbalData.herbals.melius_celeri] = 3;

                    break;

                case wealdFive:

                    ThrowHandle throwHammer = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Effigy].Position, IconData.relics.crow_hammer);

                    throwHammer.register();

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.crow_hammer.ToString());

                    break;

                case swordMists:

                    ThrowHandle throwCenser = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Effigy].Position, IconData.relics.wayfinder_censer);

                    throwCenser.register();

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.wayfinder_censer.ToString());

                    break;

                case swordStars:

                    ThrowHandle throwLantern = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Effigy].Position, IconData.relics.wayfinder_lantern);

                    throwLantern.register();

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.wayfinder_lantern.ToString());

                    break;

                case starsOne:

                    ThrowHandle throwStill = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Revenant].Position, IconData.relics.herbalism_still);

                    throwStill.register();

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.herbalism_still.ToString());

                    Mod.instance.save.herbalism[HerbalData.herbals.satius_ligna] = 3;

                    Mod.instance.save.herbalism[HerbalData.herbals.satius_impes] = 3;

                    Mod.instance.save.herbalism[HerbalData.herbals.satius_celeri] = 3;

                    break;

                case fatesOne:

                    ThrowHandle throwEye = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Jester].Position, IconData.relics.wayfinder_eye);

                    throwEye.register();

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.wayfinder_eye.ToString());

                    break;

                case fatesFour:

                    ThrowHandle throwCup = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Buffin].Position, IconData.relics.herbalism_crucible);

                    throwCup.register();

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.herbalism_crucible.ToString());

                    Mod.instance.save.herbalism[HerbalData.herbals.magnus_ligna] = 3;

                    Mod.instance.save.herbalism[HerbalData.herbals.magnus_impes] = 3;

                    Mod.instance.save.herbalism[HerbalData.herbals.magnus_celeri] = 3;

                    Mod.instance.save.herbalism[HerbalData.herbals.faeth] = 5;

                    break;

                case swordEther:

                    ThrowHandle throwCeremonial = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Revenant].Position, IconData.relics.wayfinder_ceremonial);

                    throwCeremonial.register();

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.wayfinder_ceremonial.ToString());

                    break;

                case etherOne:

                    ThrowHandle throwDragononomicon = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Shadowtin].Position, IconData.relics.dragon_form);

                    throwDragononomicon.register();

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.dragon_form.ToString());

                    break;

                case etherFour:

                    ThrowHandle throwGauge = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Shadowtin].Position, IconData.relics.herbalism_gauge);

                    throwGauge.register();

                    Mod.instance.relicsData.ReliquaryUpdate(IconData.relics.herbalism_gauge.ToString());

                    Mod.instance.save.herbalism[HerbalData.herbals.optimus_ligna] = 3;

                    Mod.instance.save.herbalism[HerbalData.herbals.optimus_impes] = 3;

                    Mod.instance.save.herbalism[HerbalData.herbals.optimus_celeri] = 3;

                    Mod.instance.save.herbalism[HerbalData.herbals.aether] = 5;

                    break;

            }

            return;

        }

        public void OnComplete(string questId, int questRating)
        {
            
            ThrowHandle swordThrow;

            ThrowHandle throwRelic;

            int friendship;

            switch (questId)
            {

                case swordWeald:

                    Mod.instance.save.rite = Rite.rites.weald;

                    swordThrow = new(Game1.player, quests[questId].origin - new Vector2(64, 320), SpawnData.swords.forest);

                    swordThrow.delay = 40;

                    swordThrow.register();

                    break;

                case herbalism:

                    throwRelic = new(Game1.player, new Vector2(21,24)*64 + new Vector2(32), IconData.relics.herbalism_mortar);

                    throwRelic.register();

                    Mod.instance.save.herbalism[HerbalData.herbals.ligna] = 3;

                    Mod.instance.save.herbalism[HerbalData.herbals.impes] = 3;

                    Mod.instance.save.herbalism[HerbalData.herbals.celeri] = 3;

                    break;

                case challengeWeald:

                    friendship = 100;

                    friendship += questRating * 8;

                    VillagerData.CommunityFriendship(VillagerData.villagerLocales.mountain, friendship, questRating);

                    throwRelic = new(Game1.player, Game1.player.Position + new Vector2(192, -64), IconData.relics.runestones_spring);

                    throwRelic.register();

                    break;

                case swordMists:

                    Mod.instance.save.rite = Rite.rites.mists;

                    swordThrow = new(Game1.player, quests[questId].origin + new Vector2(64, 320), SpawnData.swords.neptune);

                    swordThrow.register();

                    Mod.instance.iconData.ImpactIndicator(Game1.player.currentLocation, quests[questId].origin + new Vector2(64, 320), IconData.impacts.fish, 4f, new());

                    break;

                case mistsTwo:

                    SpawnData.LearnRecipe();

                    break;

                case challengeMists:

                    friendship = 125;

                    friendship += questRating * 25;

                    VillagerData.CommunityFriendship(VillagerData.villagerLocales.town, friendship, questRating);

                    break;

                case swordStars:

                    Mod.instance.save.rite = Rite.rites.stars;

                    swordThrow = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Revenant].Position, SpawnData.swords.holy);

                    swordThrow.register();

                    break;

                case challengeAtoll:

                    if (!Mod.instance.save.reliquary.ContainsKey(IconData.relics.runestones_farm.ToString()))
                    {
                        throwRelic = new(Game1.player, Game1.player.Position + new Vector2(192, -64), IconData.relics.runestones_farm);

                        throwRelic.register();

                    }

                    break;

                case challengeDragon:

                    if (!Mod.instance.save.reliquary.ContainsKey(IconData.relics.runestones_cat.ToString()))
                    {
                        throwRelic = new(Game1.player, Game1.player.Position + new Vector2(192, -64), IconData.relics.runestones_cat);

                        throwRelic.register();

                    }

                    if(Game1.player.currentLocation is Vault vault)
                    {
                        
                        vault.AddCrateTile(24, 10, 1);

                        vault.AddCrateTile(28, 10, 2);

                        vault.AddCrateTile(32, 10, 3);

                    }

                    break;

                case challengeStars:

                    friendship = 100;

                    friendship += questRating * 8;

                    VillagerData.CommunityFriendship(VillagerData.villagerLocales.forest, friendship, questRating);

                    throwRelic = new(Game1.player, Game1.player.Position + new Vector2(192, -64), IconData.relics.runestones_moon);

                    throwRelic.register();

                    break;

                case approachJester:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Jester].Position, IconData.relics.jester_dice);

                    throwRelic.register();

                    break;

                case swordFates:

                    Mod.instance.save.rite = Rite.rites.fates;

                    break;

                case challengeFates:

                    if (!Mod.instance.save.reliquary.ContainsKey(IconData.relics.shadowtin_tome.ToString()))
                    {
                        throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Shadowtin].Position, IconData.relics.shadowtin_tome);

                        throwRelic.register();

                    }

                    break;

                case swordEther:

                    Mod.instance.save.rite = Rite.rites.ether;

                    swordThrow = new(Game1.player, Game1.player.Position + new Vector2(192, -64), SpawnData.swords.cutlass);

                    swordThrow.register();

                    ThrowHandle throwNotes = new(Game1.player, Game1.player.Position + new Vector2(192, -64), IconData.relics.box_measurer);

                    throwNotes.delay = 120;

                    throwNotes.register();

                    break;

                case challengeEther:

                    if (!Mod.instance.save.reliquary.ContainsKey(IconData.relics.stardew_druid.ToString()))
                    {
                        throwRelic = new(Game1.player, Game1.player.Position + new Vector2(192, -64), IconData.relics.stardew_druid);

                        throwRelic.register();

                    }

                    break;

            }

            return;

        }

        public void OnReplayComplete(string questId, int questRating)
        {

            int friendship = 0;

            ThrowHandle throwItem;

            switch (questId)
            {

                case challengeWeald:

                    friendship += questRating * 8;

                    VillagerData.CommunityFriendship(VillagerData.villagerLocales.mountain, friendship, questRating);

                    break;

                case challengeMists:

                    friendship += questRating * 25;

                    VillagerData.CommunityFriendship(VillagerData.villagerLocales.town, friendship, questRating);

                    break;

                case challengeStars:

                    friendship += questRating * 8;

                    VillagerData.CommunityFriendship(VillagerData.villagerLocales.forest, friendship, questRating);

                    break;

                case challengeAtoll:

                    // rain totem (O)681
                    throwItem = new(Game1.player, Game1.player.Position + new Vector2(128, -64), new StardewValley.Object("681",1));

                    throwItem.register();
                    
                    break;

                case challengeDragon:

                    // prismatic shard (O)74
                    throwItem = new(Game1.player, Game1.player.Position + new Vector2(128, -64), new StardewValley.Object("74", 1));

                    throwItem.register();

                    break;

                case challengeFates:

                    // iridium sprinkler (O)645
                    throwItem = new(Game1.player, Game1.player.Position + new Vector2(128, -64), new StardewValley.Object("645", 1));

                    throwItem.register();

                    break;

                case swordEther:

                    // faeth *20

                    Herbal Faeth = Mod.instance.herbalData.herbalism[HerbalData.herbals.faeth.ToString()];

                    DisplayPotion hudmessage = new("+20 " + Faeth.title, Faeth);

                    Game1.addHUDMessage(hudmessage);

                    if (Mod.instance.save.herbalism.ContainsKey(HerbalData.herbals.faeth))
                    {

                        Mod.instance.save.herbalism[HerbalData.herbals.faeth] += Math.Min(20, 999 - Mod.instance.save.herbalism[HerbalData.herbals.faeth]);

                    }
                    else
                    {

                        Mod.instance.save.herbalism[HerbalData.herbals.faeth] = 20;

                    }

                    break;

                case challengeEther:

                    // rare seed (O)347 x2
                    throwItem = new(Game1.player, Game1.player.Position + new Vector2(128, -64), new StardewValley.Object("347", 2));

                    throwItem.register();

                    break;

            }

            return;
        }

        public void OnCancel(string questId)
        {

            if (Mod.instance.eventRegister.ContainsKey(questId))
            {

                Mod.instance.eventRegister[questId].EventRemove();

                Mod.instance.eventRegister.Remove(questId);

            }

            Mod.instance.save.progress[questId].status = 3;

        }

        public void Milecrossed(milestones milestone)
        {

            if(milestone > Mod.instance.save.milestone)
            {

                Mod.instance.save.milestone = milestone;

            }

        }

        public void CheckAssignment(string id, int delay)
        {

            if (!Mod.instance.save.progress.ContainsKey(id))
            {

                switch (delay)
                {
                    
                    case 0:

                        AssignQuest(id);

                        break;

                    default:

                        Mod.instance.save.progress[id] = new(0, delay);

                        break;


                }

            }

        }

        // ----------------------------------------------------------------------

        public void DialogueBefore(string questId, CharacterHandle.characters character = CharacterHandle.characters.none)
        {
            
            if (quests[questId].before.Count > 0)
            {

                foreach(KeyValuePair<CharacterHandle.characters,DialogueSpecial> special in quests[questId].before)
                {

                    if(character != CharacterHandle.characters.none)
                    {
                        
                        if (special.Key != character)
                        {

                            continue;

                        }

                    }

                    if (!Mod.instance.characters.ContainsKey(special.Key))
                    {

                        return;

                    }

                    if (!Mod.instance.dialogue.ContainsKey(special.Key))
                    {

                        Mod.instance.dialogue[special.Key] = new(special.Key);

                    }

                    special.Value.questId = questId;

                    Mod.instance.dialogue[special.Key].AddSpecialDialogue(questId, special.Value);

                    if (!prompts.ContainsKey(special.Key))
                    {

                        prompts[special.Key] = new();

                    }

                    if (!prompts[special.Key].Contains(questId))
                    {

                        prompts[special.Key].Add(questId);

                    }

                }

            }

        }

        public void DialogueAfter(string questId)
        {

            if (quests[questId].before.Count > 0)
            {

                foreach (KeyValuePair<CharacterHandle.characters, DialogueSpecial> special in quests[questId].before)
                {

                    if (Mod.instance.dialogue.ContainsKey(special.Key))
                    {

                        Mod.instance.dialogue[special.Key].RemoveSpecialDialogue(questId);

                    }

                }

            }

            if (quests[questId].after.Count > 0)
            {

                foreach (KeyValuePair<CharacterHandle.characters, DialogueSpecial> special in quests[questId].after)
                {

                    if (Mod.instance.dialogue.ContainsKey(special.Key))
                    {

                        special.Value.questId = questId;

                        Mod.instance.dialogue[special.Key].AddSpecialDialogue(questId, special.Value);

                    }

                }

            }

        }

        public void DialogueCheck(string questId, int context, CharacterHandle.characters characterType, int answer = 0)
        {

            if (Mod.instance.save.progress.ContainsKey(questId))
            {

                if(Mod.instance.save.progress[questId].status == 0 && context == 0)
                {

                    Mod.instance.save.progress[questId].status = 1;

                    Mod.instance.save.progress[questId].delay = 0;

                    OnAccept(questId);

                    Initialise(questId);

                    foreach (KeyValuePair<CharacterHandle.characters,Dialogue.Dialogue> dialogues in Mod.instance.dialogue)
                    {

                        dialogues.Value.RemoveSpecialDialogue(questId);

                    }

                    Mod.instance.SyncMultiplayer();

                }

            }

        }

        public void DialogueReload(CharacterHandle.characters character)
        {
            
            if (prompts.ContainsKey(character))
            {

                List<string> loadout = new(prompts[character]);
                
                for (int q = loadout.Count - 1; q >= 0; q--)
                {
                    
                    if (IsComplete(loadout[q]))
                    {

                        prompts[character].RemoveAt(q);

                        continue;

                    }

                    DialogueBefore(loadout[q],character);

                }

            }

        }

    }

}
