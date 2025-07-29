using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Event.Challenge;
using StardewDruid.Event.Scene;
using StardewDruid.Event.Sword;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Location.Druid;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Constants;
using StardewValley.Delegates;
using StardewValley.GameData.Pets;
using StardewValley.Locations;
using StardewValley.Projectiles;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using xTile;

namespace StardewDruid.Handle
{
    public class QuestHandle
    {

        public Dictionary<string, Data.Quest> quests = new();

        public Dictionary<LoreStory.stories, LoreStory> lores = new();

        public Dictionary<LoreSet.loresets, LoreSet> loresets = new();

        public Dictionary<CharacterHandle.characters, List<string>> prompts = new();

        public enum milestones
        {
            none,
            effigy,
            sworn_weapon,
            sworn_lessons,
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
            witch_weapon,
            witch_lessons,
            quest_buffin,
            quest_revenant,
            witch_challenge,
            heirs_weapon,
            moors_challenge,
            heirs_lessons,
            end,

        }

        public static Dictionary<milestones, List<string>> milestoneQuests = new()
        {

            [milestones.effigy] = new() { approachEffigy, },
            [milestones.sworn_weapon] = new() { squireWinds, },
            [milestones.sworn_lessons] = new() { windsOne, windsTwo, windsThree,},
            [milestones.weald_weapon] = new() { swordWeald, },
            [milestones.weald_lessons] = new() { herbalism, wealdOne, wealdTwo, wealdThree, wealdFour, chargeUps, bombs, wealdFive, },
            [milestones.weald_challenge] = new() { challengeWeald, },
            [milestones.mists_weapon] = new() { swordMists },
            [milestones.mists_lessons] = new() { mistsOne, mistsTwo, mistsThree, mistsFour, },
            [milestones.quest_effigy] = new() { questEffigy, },
            [milestones.mists_challenge] = new() { challengeMists, },
            [milestones.stars_weapon] = new() { swordStars, },
            [milestones.stars_lessons] = new() { orders, starsOne, captures, starsTwo, },
            [milestones.stars_challenge] = new() { challengeStars, },
            [milestones.stars_threats] = new() { challengeAtoll, challengeDragon, },
            [milestones.jester] = new() { distillery, approachJester,  },
            [milestones.fates_weapon] = new() { swordFates, },
            [milestones.fates_lessons] = new() { fatesOne, fatesTwo, fatesThree, },
            [milestones.quest_jester] = new() { questJester, },
            [milestones.fates_enchant] = new() { fatesFour, },
            [milestones.fates_challenge] = new() { challengeFates, },
            [milestones.ether_weapon] = new() { swordEther, },
            [milestones.ether_lessons] = new() { etherOne, etherTwo, etherThree, },
            [milestones.quest_shadowtin] = new() { questShadowtin, },
            [milestones.ether_treasure] = new() { etherFour, },
            [milestones.ether_challenge] = new() { challengeEther, },
            [milestones.witch_weapon] = new() { questBlackfeather, },
            [milestones.witch_lessons] = new() { witchOne, witchTwo, witchThree, },
            [milestones.quest_buffin] = new() { questBuffin, },
            [milestones.quest_revenant] = new() { questRevenant, },
            [milestones.witch_challenge] = new() { challengeBones, },
            [milestones.heirs_weapon] = new() { swordHeirs, },
            [milestones.moors_challenge] = new() { challengeMoors, },
            [milestones.heirs_lessons] = new() { heirsOne, heirsTwo, },
            [milestones.end] = new() { },

        };

        public const string approachEffigy = "approachEffigy";

        public const string squireWinds = "squireWinds";

        public const string windsOne = "castlesson";

        public const string windsTwo = "alchemylesson";

        public const string windsThree = "bomblesson";

        public const string swordWeald = "swordWeald";

        public const string herbalism = "herbalism";

        public const string wealdOne = "clearance";

        public const string wealdTwo = "wildbounty";

        public const string wealdThree = "wildgrowth";

        public const string wealdFour = "cultivate";

        public const string wealdFive = "rockfall";

        public const string bombs = "bombs";

        public const string chargeUps = "chargeUps";

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

        public const string orders = "orders";

        public const string starsTwo = "gravity";

        public const string captures = "levelMonsters";

        public const string challengeStars = "infestation";

        public const string challengeAtoll = "seafarers";

        public const string challengeDragon = "dragon";

        public const string approachJester = "approachJester";

        public const string distillery = "distillery";

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

        public const string questBlackfeather = "questBlackfeather";

        public const string witchOne = "familiars";

        public const string witchTwo = "retrievers";

        public const string witchThree = "opportunists";

        public const string questBuffin = "questBuffin";

        public const string questRevenant = "questRevenant";

        public const string challengeBones = "bonefire";

        public const string swordHeirs = "swordHeirs";

        public const string threatMoors = "threatMoors";

        public const string challengeMoors = "challengeMoors";

        public const string heirsOne = "levelHeroes";

        public const string heirsTwo = "levelGuilds";
        // ------------------------------------------------------------------ optional

        public const string relicTactical = "relicTactical";

        public const string relicWeald = "relicWeald";

        public const string relicMists = "relicMists";

        public const string relicFates = "relicFates";

        public const string relicEther = "relicEther";

        public const string relicRestore = "relicRestore";

        public const string treasureChase = "treasureChase";

        public const string treasureGuardian = "treasureGuardian";

        public QuestHandle()
        {

        }

        public void LoadQuests()
        {

            if (Mod.instance.magic)
            {

                return;

            }

            quests = QuestData.QuestList();

            loresets = LoreData.LoreSets();

            lores = LoreData.LoreList();

        }

        public Dictionary<int, ContentComponent> JournalQuests()
        {

            Dictionary<int, ContentComponent> journal = new();

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

                ContentComponent content = new(ContentComponent.contentTypes.list, page);

                content.text[0] = quests[page].title;

                content.icons[0] = quests[page].icon;

                IconData.displays questIcon;

                switch (Mod.instance.save.progress[page].status)
                {

                    default:
                    case 1:

                        questIcon = IconData.displays.active;

                        break;
                    case 2:
                    case 3:

                        questIcon = IconData.displays.complete;

                        break;
                    case 4:

                        questIcon = IconData.displays.replay;

                        break;

                }

                content.icons[1] = questIcon;

                journal[start++] = content;

            }

            return journal;

        }

        public KeyValuePair<string, int> questEffects(string questId)
        {

            if (quests[questId].effect == EffectsData.EffectPage.none)
            {

                return new(null, 0);

            }

            string effectPage = quests[questId].effect.ToString();

            Dictionary<int, ContentComponent> effectComponents = JournalEffects();

            foreach (KeyValuePair<int, ContentComponent> effectComponent in effectComponents)
            {

                string[] effectParts = effectComponent.Value.id.Split(".");

                string effectId = effectParts[0];

                if (effectId == effectPage)
                {

                    return new(effectComponent.Value.id, effectComponent.Key);

                }

            }

            return new(null, 0);

        }

        public Dictionary<int, ContentComponent> JournalEffects()
        {

            Dictionary<string, List<EffectsData.EffectPage>> effects = EffectsData.RetrieveList();

            Dictionary<int, ContentComponent> journal = new();

            List<string> pageList = new();

            if (Mod.instance.magic)
            {

                pageList = effects.Keys.ToList();

            }
            else
            {

                foreach (KeyValuePair<string, QuestProgress> pair in Mod.instance.save.progress)
                {

                    string id = pair.Key;

                    QuestProgress progress = pair.Value;

                    if (!quests.ContainsKey(id))
                    {

                        continue;

                    }

                    int requirement = quests[id].type == Data.Quest.questTypes.lesson ? 1 : 2;

                    if (progress.status >= requirement)
                    {

                        if (effects.ContainsKey(id))
                        {

                            pageList.Add(id);

                        }

                    }

                }

            }

            if (Mod.instance.Config.reverseJournal)
            {

                pageList.Reverse();

            }

            int start = 0;

            foreach (string id in pageList)
            {

                for (int i = 0; i < effects[id].Count; i++)
                {

                    Effect effect = EffectsData.RetrieveEffect(effects[id][i]);

                    ContentComponent content = new(ContentComponent.contentTypes.list, effects[id][i].ToString() + "." + i.ToString());

                    content.text[0] = effect.title;

                    content.icons[0] = effect.icon;

                    journal[start++] = content;

                }

            }

            return journal;

        }

        public KeyValuePair<string, int> effectQuests(string combinedId)
        {

            string[] effectParts = combinedId.Split(".");

            string effectId = effectParts[0];

            EffectsData.EffectPage effectPage = Enum.Parse<EffectsData.EffectPage>(effectId);

            Dictionary<int, ContentComponent> questComponents = JournalQuests();

            foreach (KeyValuePair<int, ContentComponent> questComponent in questComponents)
            {

                if (quests[questComponent.Value.id].effect == effectPage)
                {

                    return new(questComponent.Value.id, questComponent.Key);

                }

            }

            return new(null, 0);

        }

        public Dictionary<int, ContentComponent> JournalLore()
        {

            Dictionary<int, ContentComponent> journal = new();

            List<LoreSet.loresets> pageList = new();

            foreach (KeyValuePair<LoreSet.loresets, LoreSet> pair in loresets)
            {

                if (!quests.ContainsKey(pair.Value.quest))
                {

                    continue;

                }

                if (IsComplete(pair.Value.quest))
                {

                    pageList.Add(pair.Key);

                }

            }

            if (Mod.instance.Config.reverseJournal)
            {

                pageList.Reverse();

            }

            int start = 0;

            foreach (LoreSet.loresets id in pageList)
            {

                ContentComponent content = new(ContentComponent.contentTypes.list, id.ToString());

                switch (loresets[id].settype)
                {
                    case LoreSet.settypes.character:
                    case LoreSet.settypes.location:

                        content.text[0] = loresets[id].title;

                        break;

                    case LoreSet.settypes.transcript:

                        content.text[0] = loresets[id].title + StringData.Strings(StringData.stringkeys.transcript);

                        break;
                }

                content.icons[0] = loresets[id].display;

                journal[start++] = content;

            }

            return journal;

        }

        // ----------------------------------------------------------------------

        public void Ready()
        {

            if (Mod.instance.magic)
            {

                return;

            }

            if (!Context.IsMainPlayer)
            {

                Farmhand();

                return;

            }

            List<string> keys = new(Mod.instance.save.progress.Keys);

            keys = new(Mod.instance.save.progress.Keys);

            foreach (string key in keys)
            {

                if (Mod.instance.save.progress[key].delay > 0)
                {

                    Mod.instance.save.progress[key].delay -= 1;

                }

                if (!quests.ContainsKey(key))
                {

                    continue;

                }

                Data.Quest quest = quests[key];

                if (Mod.instance.save.progress[key].status == 0 && Mod.instance.save.progress[key].delay <= 0)
                {

                    if (quest.give != Data.Quest.questGivers.dialogue || Mod.instance.Config.autoProgress)
                    {

                        Mod.instance.save.progress[key].status = 1;

                        OnAccept(key);

                    }

                    DialogueBefore(key);

                }

                if (Mod.instance.save.progress[key].status == 1 || Mod.instance.save.progress[key].status == 4)
                {

                    Initialise(key);

                }

                if (Mod.instance.save.progress[key].status >= 2)
                {

                    Implement(key);

                }

                if (Mod.instance.save.progress[key].status == 3)
                {

                    Mod.instance.save.progress[key].status = 2;

                }

            }

            // Sync changes
            Mod.instance.SyncProgress();

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

                Data.Quest quest = quests[key];

                if (Mod.instance.save.progress[key].status == 1)
                {

                    Localise(key);

                }
                else
                if (Mod.instance.save.progress[key].status >= 2)
                {

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

            foreach (KeyValuePair<milestones, List<string>> mile in milestoneQuests)
            {

                if (mile.Key == milestone)
                {
                    foreach (string questId in mile.Value)
                    {

                        if (quests[questId].give == Data.Quest.questGivers.dialogue)
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

            if (quests[questId].give == Data.Quest.questGivers.dialogue && !Mod.instance.Config.autoProgress)
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
                Mod.instance.SyncProgress();

            }

        }

        public void RevisitQuest(string questId)
        {

            if (Mod.instance.magic)
            {

                return;

            }

            if (!Context.IsMainPlayer)
            {

                return;

            }

            Initialise(questId);

            Mod.instance.save.progress[questId].status = 4;

            // Sync changes
            Mod.instance.SyncProgress();

        }

        public void CompleteQuest(string questId, int questRating = 0)
        {

            if (Mod.instance.magic)
            {

                return;

            }

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

                Mod.instance.save.progress[questId].status = 3;

                OnComplete(questId, questRating);

                Implement(questId);

                DialogueAfter(questId);

            }
            else
            {

                Mod.instance.save.progress[questId].status = 3;

                OnReplayComplete(questId, questRating);

            }

            Mod.instance.RegisterMessage(quests[questId].title + " " + StringData.Strings(StringData.stringkeys.questComplete), 1, true);

            if (quests[questId].reward > 0)
            {

                float adjustReward = 1.2f - Mod.instance.ModDifficulty() * 0.1f;

                int gimmeMoney = (int)(quests[questId].reward * adjustReward);

                Game1.player.Money += gimmeMoney;

                QueryData moneyQuery = new()
                {
                    name = quests[questId].title,
                    value = gimmeMoney.ToString(),
                };

                Mod.instance.EventQuery(moneyQuery, QueryData.queries.GimmeMoney);

            }

            Game1.playSound("yoba");

            // Sync changes
            Mod.instance.SyncProgress();

        }

        public int UpdateTask(string quest, int update)
        {

            if (Mod.instance.magic)
            {

                return -1;

            }

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
                    name = quest,
                    value = update.ToString(),
                };

                Mod.instance.EventQuery(queryData, QueryData.queries.QuestUpdate);

                return progress;

            }

            int limit = quests[quest].requirement;

            Mod.instance.save.progress[quest].progress = Math.Min(progress, limit);

            if (progress >= limit)
            {

                CompleteQuest(quest);

                return progress;

            }

            int portion = limit / 2;

            if (portion != 0)
            {

                if (progress % portion == 0)
                {

                    Mod.instance.RegisterMessage(quests[quest].title + " " + (progress * 100 / limit).ToString() + " " + StringData.Strings(StringData.stringkeys.percentComplete), 2, true);
                }

            }

            return progress;

        }

        public void TaskSet(string quest, int set)
        {

            if (Mod.instance.magic)
            {

                return;

            }

            if (Mod.instance.save.progress.ContainsKey(quest))
            {

                Mod.instance.save.progress[quest].progress = set;

            }

        }

        public bool IsComplete(string quest)
        {

            if (Mod.instance.magic)
            {

                return true;

            }

            if (Mod.instance.save.progress.ContainsKey(quest))
            {

                return Mod.instance.save.progress[quest].status >= 2;

            }

            return false;

        }

        public bool IsGiven(string quest)
        {

            if (Mod.instance.magic)
            {

                return true;

            }

            if (Mod.instance.save.progress.ContainsKey(quest))
            {

                return Mod.instance.save.progress[quest].status >= 1;

            }

            return false;

        }

        public bool IsReplayable(string quest)
        {

            if (!Mod.instance.save.progress.ContainsKey(quest))
            {

                return false;

            }

            if (Mod.instance.save.progress[quest].status == 3)
            {

                return false;

            }

            switch (quests[quest].type)
            {

                case Data.Quest.questTypes.sword:
                case Data.Quest.questTypes.heart:
                case Data.Quest.questTypes.challenge:

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

                    LocationHandle.DruidLocations(LocationHandle.druid_grove_name);

                    LocationHandle.DruidLocations(LocationHandle.druid_cavern_name);

                    //LocationHandle.DruidLocations(LocationHandle.druid_temple_name);

                    //LocationHandle.DruidLocations(LocationHandle.druid_sanctuary_name);

                    return;

                case wealdFive:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.druid_hammer.ToString());

                    return;

                case challengeWeald:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_pot.ToString());

                    LocationHandle.DruidLocations(LocationHandle.druid_spring_name);

                    LocationHandle.GetRestoration(LocationHandle.druid_spring_name);

                    LocationHandle.DruidLocations(LocationHandle.druid_distillery_name);

                    return;

                case swordMists:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_censer.ToString());

                    LocationHandle.DruidLocations(LocationHandle.druid_atoll_name);

                    return;

                case mistsTwo:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.herbalism_pan.ToString());

                    return;

                case challengeMists:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.wayfinder_key.ToString());

                    LocationHandle.DruidLocations(LocationHandle.druid_graveyard_name);

                    LocationHandle.GetRestoration(LocationHandle.druid_graveyard_name);

                    return;

                case swordStars:

                    LocationHandle.DruidLocations(LocationHandle.druid_chapel_name);

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_guardian.ToString());

                    return;

                case starsOne:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.herbalism_still.ToString());

                    return;

                case captures:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.companion_badge.ToString());

                    foreach (KeyValuePair<CharacterHandle.characters, PalData> pal in Mod.instance.save.pals)
                    {

                        pal.Value.Initiate();

                    }

                    return;
                
                case challengeStars:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.wayfinder_glove.ToString());

                    LocationHandle.DruidLocations(LocationHandle.druid_clearing_name);

                    LocationHandle.GetRestoration(LocationHandle.druid_clearing_name);

                    return;

                case challengeDragon:

                    LocationHandle.DruidLocations(LocationHandle.druid_lair_name);

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_water.ToString());

                    return;

                case swordFates:

                    LocationHandle.DruidLocations(LocationHandle.druid_tunnel_name);

                    LocationHandle.DruidLocations(LocationHandle.druid_court_name);

                    return;

                case questJester:

                    LocationHandle.DruidLocations(LocationHandle.druid_archaeum_name);

                    return;

                case fatesFour:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.herbalism_crucible.ToString());

                    return;

                case swordEther:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_ceremony.ToString());

                    return;

                case etherOne:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.druid_dragonomicon.ToString());

                    return;

                case questShadowtin:

                    LocationHandle.DruidLocations(LocationHandle.druid_engineum_name);

                    return;

                case etherFour:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.herbalism_gauge.ToString());

                    return;

                case questBlackfeather:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.companion_glove.ToString());

                    return;

            }

        }

        public void Initialise(string questId)
        {

            Localise(questId);

            switch (questId)
            {

                case approachEffigy:

                    if (!RelicHandle.HasRelic(IconData.relics.wayfinder_stone))
                    {

                        ThrowHandle throwRelic = new(Game1.player, Game1.player.Position, IconData.relics.wayfinder_stone);

                        throwRelic.delay = 300;

                        throwRelic.register();

                    }

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new ApproachEffigy().EventSetup(questId);

                    }

                    return;

                case squireWinds:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new SquireWinds().EventSetup(questId);

                    }

                    return;

                case herbalism:

                    (Mod.instance.locations[LocationHandle.druid_grove_name] as Grove).ToggleBowl();

                    return;

                case swordWeald:

                    return;

                case wealdOne:

                    CheckAssignment(wealdTwo, 1);

                    return;

                case wealdTwo:

                    CheckAssignment(wealdThree, 1);

                    return;

                case wealdThree:

                    CheckAssignment(wealdFour, 1);

                    return;

                case wealdFour:

                    CheckAssignment(wealdFive, 1);

                    CheckAssignment(chargeUps, 1);

                    CheckAssignment(bombs, 1);

                    return;

                case wealdFive:

                    CheckAssignment(challengeWeald, 1);

                    return;

                case challengeWeald:

                    new ChallengeWeald().EventSetup(questId);

                    return;

                case swordMists:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new SwordMists().EventSetup(questId);

                    }

                    return;

                case mistsOne:

                    CheckAssignment(mistsTwo, 1);

                    return;

                case mistsTwo:

                    CheckAssignment(mistsThree, 1);

                    return;

                case mistsThree:

                    CheckAssignment(mistsFour, 1);

                    return;

                case mistsFour:

                    CheckAssignment(questEffigy, 1);

                    return;

                case questEffigy:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new QuestEffigy().EventSetup(questId);

                    }

                    return;

                case challengeMists:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new ChallengeMists().EventSetup(questId);

                    }

                    return;

                case swordStars:

                    if (!Mod.instance.characters.ContainsKey(CharacterHandle.characters.Revenant))
                    {

                        CharacterHandle.CharacterLoad(CharacterHandle.characters.Revenant, Character.Character.mode.home);

                    }

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new SwordStars().EventSetup(questId);

                    }

                    return;

                case starsOne:

                    CheckAssignment(starsTwo, 1);

                    CheckAssignment(captures, 1);

                    return;

                case starsTwo:

                    CheckAssignment(challengeStars, 1);

                    CheckAssignment(challengeAtoll, 1);

                    CheckAssignment(challengeDragon, 1);

                    return;

                case challengeStars:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new ChallengeStars().EventSetup(questId);

                    }

                    return;

                case challengeAtoll:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new ChallengeAtoll().EventSetup(questId);

                    }

                    return;

                case challengeDragon:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new ChallengeDragon().EventSetup(questId);

                    }
                    return;

                case approachJester:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new ApproachJester().EventSetup(questId);

                    }

                    return;

                case swordFates:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new SwordFates().EventSetup(questId);

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

                        new QuestJester().EventSetup(questId);

                    }

                    return;

                case fatesFour:

                    CheckAssignment(challengeFates, 1);

                    return;

                case challengeFates:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new ChallengeFates().EventSetup(questId);

                    }

                    return;

                case swordEther:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new SwordEther().EventSetup(questId);

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

                        new QuestShadowtin().EventSetup(questId);

                    }

                    return;

                case etherFour:

                    CheckAssignment(challengeEther, 1);

                    return;

                case challengeEther:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new ChallengeEther().EventSetup(questId);

                    }

                    return;

                case questBlackfeather:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new QuestBlackfeather().EventSetup(questId);

                    }

                    return;

                case witchOne:

                    CheckAssignment(witchTwo, 1);

                    return;

                case witchTwo:

                    CheckAssignment(witchThree, 1);

                    return;

                case witchThree:

                    CheckAssignment(questBuffin, 1);

                    return;

                case questBuffin:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new QuestBuffin().EventSetup(questId);

                    }

                    return;

                case questRevenant:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new QuestRevenant().EventSetup(questId);

                    }

                    return;

                case challengeBones:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new ChallengeBones().EventSetup(questId);

                    }

                    return;

                case swordHeirs:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new SwordHeirs().EventSetup(questId);

                    }

                    return;

                case challengeMoors:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new ChallengeMoors().EventSetup(questId);

                    }

                    return;

                case heirsOne:

                    CheckAssignment(heirsTwo, 1);

                    return;

                // =====================================================================
                // RELICS

                case relicTactical:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Relics.RelicTactical().EventSetup(questId);

                    }

                    return;

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

                case relicRestore:

                    if (!Mod.instance.eventRegister.ContainsKey(questId))
                    {

                        new Event.Relics.RelicRestore().EventSetup(questId);

                    }

                    return;
            }

        }

        public void Fulfillment(string questId)
        {

            switch (questId)
            {

                case approachEffigy:

                    LocationHandle.DruidLocations(LocationHandle.druid_grove_name);

                    LocationHandle.DruidLocations(LocationHandle.druid_cavern_name);

                    //LocationHandle.DruidLocations(LocationHandle.druid_temple_name);

                    //LocationHandle.DruidLocations(LocationHandle.druid_sanctuary_name);

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.wayfinder_stone.ToString());

                    return;

                case herbalism:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.herbalism_mortar.ToString());

                    return;

                case wealdFive:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.druid_hammer.ToString());

                    return;

                case challengeWeald:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_pot.ToString());

                    LocationHandle.DruidLocations(LocationHandle.druid_spring_name);

                    LocationHandle.GetRestoration(LocationHandle.druid_spring_name);

                    LocationHandle.DruidLocations(LocationHandle.druid_distillery_name);

                    return;

                case swordMists:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_censer.ToString());

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.tactical_discombobulator.ToString());

                    LocationHandle.DruidLocations(LocationHandle.druid_atoll_name);

                    return;

                case mistsTwo:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.herbalism_pan.ToString());

                    return;

                case questEffigy:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.companion_crest.ToString());

                    return;

                case challengeMists:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.wayfinder_key.ToString());

                    LocationHandle.DruidLocations(LocationHandle.druid_graveyard_name);

                    LocationHandle.GetRestoration(LocationHandle.druid_graveyard_name);

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.runestones_spring.ToString());

                    return;

                case swordStars:

                    LocationHandle.DruidLocations(LocationHandle.druid_chapel_name);

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_guardian.ToString());

                    break;

                case starsOne:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.herbalism_still.ToString());

                    return;

                case orders:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.crest_church.ToString());

                    return;

                case captures:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.companion_badge.ToString());

                    foreach (KeyValuePair<CharacterHandle.characters, PalData> pal in Mod.instance.save.pals)
                    {

                        pal.Value.Initiate();

                    }

                    return;

                case challengeStars:

                    LocationHandle.DruidLocations(LocationHandle.druid_clearing_name);

                    LocationHandle.GetRestoration(LocationHandle.druid_clearing_name);

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.wayfinder_glove.ToString());

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.runestones_moon.ToString());

                    return;

                case challengeAtoll:
                case challengeDragon:

                    if (questId == challengeAtoll)
                    {
                        Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.runestones_farm.ToString());
                    }

                    if (questId == challengeDragon)
                    {

                        Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_water.ToString());

                        Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.runestones_cat.ToString());

                        LocationHandle.DruidLocations(LocationHandle.druid_lair_name);

                    }

                    return;

                case approachJester:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.companion_dice.ToString());

                    break;

                case distillery:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.crest_dwarf.ToString());

                    return;

                case swordFates:

                    LocationHandle.DruidLocations(LocationHandle.druid_tunnel_name);

                    LocationHandle.DruidLocations(LocationHandle.druid_court_name);

                    break;

                case fatesOne:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.wayfinder_eye.ToString());

                    return;

                case questJester:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.skull_saurus.ToString());

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.crest_associate.ToString());

                    return;

                case fatesFour:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.herbalism_crucible.ToString());

                    return;

                case challengeFates:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.book_wyrven.ToString());

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.companion_tome.ToString());

                    LocationHandle.DruidLocations(LocationHandle.druid_tomb_name);

                    return;

                case swordEther:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_ceremony.ToString());

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.box_measurer.ToString());

                    return;

                case etherOne:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.druid_dragonomicon.ToString());

                    return;

                case questShadowtin:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.shadowtin_cell.ToString());

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.skull_gelatin.ToString());

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.skull_cannoli.ToString());

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.crest_smuggler.ToString());

                    LocationHandle.DruidLocations(LocationHandle.druid_engineum_name);

                    if (!(Mod.instance.locations[LocationHandle.druid_clearing_name] as Clearing).accessOpen)
                    {

                        (Mod.instance.locations[LocationHandle.druid_clearing_name] as Clearing).OpenAccess();

                    }

                    return;

                case etherFour:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.herbalism_gauge.ToString());

                    return;

                case questBlackfeather:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.companion_glove.ToString());

                    return;

                case questBuffin:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.skull_fox.ToString());

                    return;

                case questRevenant:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.book_knight.ToString());

                    return;

                case challengeBones:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.golden_core.ToString());

                    return;

                case swordHeirs:

                    LocationHandle.DruidLocations(LocationHandle.druid_moors_name);

                    (Mod.instance.locations[LocationHandle.druid_sanctuary_name] as Sanctuary).OpenGate();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.druid_hieress.ToString());

                    return;

                case heirsTwo:

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.stardew_druid.ToString());

                    return;
            }

        }

        public void Implement(string questId)
        {

            Fulfillment(questId);

            switch (questId)
            {

                case approachEffigy:

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Effigy, CharacterHandle.CharacterSaveMode(CharacterHandle.characters.Effigy));

                    CheckAssignment(squireWinds, 0);

                    return;

                case swordWeald:

                    CheckAssignment(herbalism, 0);

                    CheckAssignment(wealdOne, 0);

                    return;

                case herbalism:

                    (Mod.instance.locations[LocationHandle.druid_grove_name] as Grove).ToggleBowl(true);

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

                    CheckAssignment(chargeUps, 0);

                    CheckAssignment(bombs, 0);

                    return;

                case wealdFive:

                    CheckAssignment(challengeWeald, 0);

                    return;

                case challengeWeald:

                    CheckAssignment(swordMists, 1);

                    return;

                case swordMists:

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

                    return;

                case questEffigy:

                    CheckAssignment(challengeMists, 1);

                    return;

                case challengeMists:

                    CheckAssignment(swordStars, 1);

                    return;

                case swordStars:

                    CheckAssignment(orders, 0);

                    CheckAssignment(starsOne, 0);

                    CheckAssignment(captures, 1);

                    CheckAssignment(starsTwo, 1);

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Revenant, CharacterHandle.CharacterSaveMode(CharacterHandle.characters.Revenant));

                    break;

                case starsOne:

                    CheckAssignment(captures, 0);

                    CheckAssignment(starsTwo, 0);

                    return;

                case starsTwo:

                    CheckAssignment(challengeStars, 0);

                    if (!IsComplete(starsOne)) { return; }

                    return;

                case challengeStars:

                    CheckAssignment(challengeAtoll, 0);

                    CheckAssignment(challengeDragon, 0);

                    return;

                case challengeAtoll:
                case challengeDragon:

                    if (!IsComplete(challengeAtoll)) { return; }

                    if (!IsComplete(challengeDragon)) { return; }

                    CheckAssignment(distillery, 0);

                    CheckAssignment(approachJester, 1);

                    return;

                case approachJester:

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Jester, CharacterHandle.CharacterSaveMode(CharacterHandle.characters.Jester));

                    CheckAssignment(swordFates, 0);

                    break;

                case swordFates:

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

                    return;

                case questJester:

                    CheckAssignment(fatesFour, 1);

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Buffin, CharacterHandle.CharacterSaveMode(CharacterHandle.characters.Buffin));

                    return;

                case fatesFour:

                    CheckAssignment(challengeFates, 1);

                    return;

                case challengeFates:

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Shadowtin, CharacterHandle.CharacterSaveMode(CharacterHandle.characters.Shadowtin));

                    CheckAssignment(swordEther, 1);

                    return;

                case swordEther:

                    CheckAssignment(etherOne, 0);

                    CheckAssignment(etherTwo, 1);

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

                    CheckAssignment(etherFour, 1);

                    return;

                case etherFour:

                    CheckAssignment(challengeEther, 1);

                    return;

                case challengeEther:

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Blackfeather, CharacterHandle.CharacterSaveMode(CharacterHandle.characters.Blackfeather));

                    CheckAssignment(questBlackfeather, 1);

                    return;

                case questBlackfeather:

                    CheckAssignment(witchOne, 0);

                    return;

                case witchOne:

                    CheckAssignment(witchTwo, 1);

                    return;

                case witchTwo:

                    CheckAssignment(witchThree, 1);

                    return;

                case witchThree:

                    CheckAssignment(questBuffin, 1);

                    return;

                case questBuffin:

                    CheckAssignment(questRevenant, 1);

                    return;

                case questRevenant:

                    CheckAssignment(challengeBones, 1);

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Marlon, CharacterHandle.CharacterSaveMode(CharacterHandle.characters.Marlon));

                    return;

                case challengeBones:

                    CheckAssignment(swordHeirs, 1);

                    CharacterHandle.CharacterLoad(CharacterHandle.characters.Aldebaran, CharacterHandle.CharacterSaveMode(CharacterHandle.characters.Aldebaran));

                    return;

                case swordHeirs:

                    CheckAssignment(challengeMoors, 0);

                    /*if (!Mod.instance.eventRegister.ContainsKey(threatMoors))
                    {

                        new Threat.ThreatMoors().EventSetup(Vector2.Zero, threatMoors, true);

                    }*/

                    List<CharacterHandle.characters> recruits = new()
                    {
                        CharacterHandle.characters.recruit_one,
                        CharacterHandle.characters.recruit_two,
                        CharacterHandle.characters.recruit_three,
                        CharacterHandle.characters.recruit_four,

                    };

                    foreach (CharacterHandle.characters recruit in recruits)
                    {

                        if (!Mod.instance.save.characters.ContainsKey(recruit))
                        {

                            continue;

                        }

                        if (Mod.instance.save.characters[recruit] != Character.Character.mode.recruit)
                        {

                            continue;

                        }

                        RecruitHandle.RecruitLoad(recruit);

                        if (Mod.instance.trackers.ContainsKey(recruit))
                        {

                            Mod.instance.trackers[recruit].suspended = true;

                        }

                    }

                    return;

                case challengeMoors:

                    CheckAssignment(heirsOne, 1);

                    return;

                case heirsOne:

                    CheckAssignment(heirsTwo, 1);

                    return;

            }

        }

        public void OnAccept(string questId)
        {

            StringData.stringkeys messageKey = StringData.stringkeys.questReceived;

            switch (quests[questId].type)
            {
                case Data.Quest.questTypes.lesson:

                    break;

                case Data.Quest.questTypes.challenge:

                    break;

            }

            string acceptMessage = quests[questId].title + StringData.Strings(messageKey);

            DisplayMessage displayMessage = new(acceptMessage, quests[questId].icon);

            Game1.addHUDMessage(displayMessage);

            // --------------------------------------------------------

            ThrowHandle throwRelic;

            switch (questId)
            {

                case mistsTwo:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Effigy].Position, IconData.relics.herbalism_pan);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.herbalism_pan.ToString());

                    List<HerbalHandle.herbals> satius = new()
                    {
                        HerbalHandle.herbals.satius_ligna,

                        HerbalHandle.herbals.satius_impes,

                        HerbalHandle.herbals.satius_celeri,

                    };

                    foreach (HerbalHandle.herbals sat in satius)
                    {

                        HerbalHandle.UpdateHerbalism(sat, 3);

                    }

                    break;

                case wealdFive:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Effigy].Position, IconData.relics.druid_hammer);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.druid_hammer.ToString());

                    break;

                case challengeWeald:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Effigy].Position, IconData.relics.lantern_pot);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_pot.ToString());

                    break;

                case swordMists:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Effigy].Position, IconData.relics.lantern_censer);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_censer.ToString());

                    break;

                case challengeMists:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Effigy].Position, IconData.relics.wayfinder_key);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.wayfinder_key.ToString());

                    break;

                case swordStars:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Effigy].Position, IconData.relics.lantern_guardian);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_guardian.ToString());

                    break;

                case starsOne:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Revenant].Position, IconData.relics.herbalism_still);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.herbalism_still.ToString());

                    List<HerbalHandle.herbals> magnus = new()
                    {
                        HerbalHandle.herbals.magnus_ligna,

                        HerbalHandle.herbals.magnus_impes,

                        HerbalHandle.herbals.magnus_celeri,

                    };

                    foreach (HerbalHandle.herbals mag in magnus)
                    {

                        HerbalHandle.UpdateHerbalism(mag, 3);

                    }

                    break;

                case captures:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Revenant].Position, IconData.relics.companion_badge);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.companion_badge.ToString());

                    break;

                case challengeStars:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Effigy].Position, IconData.relics.wayfinder_glove);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.wayfinder_glove.ToString());

                    break;

                case challengeDragon:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Revenant].Position, IconData.relics.lantern_water);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_water.ToString());

                    break;

                case fatesOne:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Jester].Position, IconData.relics.wayfinder_eye);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.wayfinder_eye.ToString());

                    break;

                case fatesFour:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Buffin].Position, IconData.relics.herbalism_crucible);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.herbalism_crucible.ToString());

                    HerbalHandle.UpdateHerbalism(HerbalHandle.herbals.faeth, 5);

                    break;

                case swordEther:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Revenant].Position, IconData.relics.lantern_ceremony);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.lantern_ceremony.ToString());

                    break;

                case etherOne:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Shadowtin].Position, IconData.relics.druid_dragonomicon);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.druid_dragonomicon.ToString());

                    break;

                case etherFour:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Shadowtin].Position, IconData.relics.herbalism_gauge);

                    throwRelic.register();

                    List<HerbalHandle.herbals> optimus = new()
                    {
                        HerbalHandle.herbals.optimus_ligna,

                        HerbalHandle.herbals.optimus_impes,

                        HerbalHandle.herbals.optimus_celeri,

                        HerbalHandle.herbals.aether,

                    };

                    foreach (HerbalHandle.herbals opt in optimus)
                    {

                        HerbalHandle.UpdateHerbalism(opt, 3);

                    }

                    break;

                case questShadowtin:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Shadowtin].Position, IconData.relics.crest_smuggler);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.crest_smuggler.ToString());

                    break;

                case questBlackfeather:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Blackfeather].Position, IconData.relics.companion_glove);

                    throwRelic.register();

                    Mod.instance.relicHandle.ReliquaryUpdate(IconData.relics.companion_glove.ToString());

                    break;

            }

            return;

        }

        public void OnComplete(string questId, int questRating)
        {

            ThrowHandle swordThrow;

            ThrowHandle throwRelic;

            Vector2 throwPosition;

            int friendship;

            switch (questId)
            {

                case squireWinds:

                    throwPosition = Game1.player.Position + new Vector2(64);

                    if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Effigy))
                    {

                        if (Mod.instance.characters[CharacterHandle.characters.Effigy] is Effigy effigy)
                        {

                            if(effigy.currentLocation.Name == Game1.player.currentLocation.Name)
                            {

                                if(Vector2.Distance(effigy.Position,Game1.player.Position) <= 320)
                                {

                                    throwPosition = effigy.Position;

                                }

                            }

                        }

                    }

                    Mod.instance.spellRegister.Add(new(Game1.player, IconData.ritecircles.winds) { soundTrigger = Handle.SoundHandle.SoundCue.RisingWind, });

                    throwRelic = new(Game1.player, throwPosition, IconData.relics.druid_grimoire);

                    throwRelic.register();

                    break;

                case swordWeald:

                    Mod.instance.save.rite = Rite.Rites.weald;

                    swordThrow = new(Game1.player, quests[questId].origin - new Vector2(64, 320), SpawnData.Swords.forest)
                    {
                        delay = 40
                    };

                    swordThrow.register();

                    break;

                case herbalism:

                    throwRelic = new(Game1.player, Game1.player.Position + new Vector2(64), IconData.relics.herbalism_mortar);

                    throwRelic.register();

                    List<HerbalHandle.herbals> melius = new()
                    {
                        HerbalHandle.herbals.ligna,

                        HerbalHandle.herbals.impes,

                        HerbalHandle.herbals.celeri,

                        HerbalHandle.herbals.melius_ligna,

                        HerbalHandle.herbals.melius_impes,

                        HerbalHandle.herbals.melius_celeri,

                    };

                    foreach (HerbalHandle.herbals mel in melius)
                    {

                        HerbalHandle.UpdateHerbalism(mel, 3);

                    }

                    break;

                case challengeWeald:

                    friendship = 100;

                    friendship += questRating * 8;

                    VillagerData.CommunityFriendship(VillagerData.villagerLocales.mountain, friendship, questRating);

                    throwRelic = new(Game1.player, Game1.player.Position + new Vector2(192, -64), IconData.relics.tactical_discombobulator);

                    throwRelic.register();

                    break;

                case swordMists:

                    Mod.instance.save.rite = Rite.Rites.mists;

                    swordThrow = new(Game1.player, quests[questId].origin + new Vector2(64, 320), SpawnData.Swords.neptune);

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

                    throwRelic = new(Game1.player, Game1.player.Position + new Vector2(192, -64), IconData.relics.runestones_spring);

                    throwRelic.register();

                    break;

                case swordStars:

                    Mod.instance.save.rite = Rite.Rites.stars;

                    swordThrow = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Revenant].Position, SpawnData.Swords.holy);

                    swordThrow.register();

                    break;

                case orders:

                    throwRelic = new(Game1.player, Game1.player.Position + new Vector2(64), IconData.relics.crest_church);

                    throwRelic.register();

                    Mod.instance.exportHandle.Orders();

                    break;

                case challengeAtoll:

                    if (!RelicHandle.HasRelic(IconData.relics.runestones_farm))
                    {
                        throwRelic = new(Game1.player, Game1.player.Position + new Vector2(192, -64), IconData.relics.runestones_farm);

                        throwRelic.register();

                    }

                    break;

                case challengeDragon:

                    if (!RelicHandle.HasRelic(IconData.relics.runestones_cat))
                    {
                        throwRelic = new(Game1.player, Game1.player.Position + new Vector2(192, -64), IconData.relics.runestones_cat);

                        throwRelic.register();

                    }

                    if (Game1.player.currentLocation is Lair vault)
                    {

                        vault.AddCrateField(new(24, 12), SpawnData.Swords.lava);

                        vault.AddCrateField(new(28, 12), new StardewValley.Object("336", 5));

                        vault.AddCrateField(new(32, 12), new StardewValley.Object("74", 1));

                    }

                    break;

                case challengeStars:

                    friendship = 100;

                    friendship += questRating * 20;

                    VillagerData.CommunityFriendship(VillagerData.villagerLocales.forest, friendship, questRating);

                    throwRelic = new(Game1.player, Game1.player.Position + new Vector2(192, -64), IconData.relics.runestones_moon);

                    throwRelic.register();

                    break;

                case distillery:

                    throwRelic = new(Game1.player, Game1.player.Position + new Vector2(64), IconData.relics.crest_dwarf);

                    throwRelic.register();

                    List<ExportHandle.exports> machines = new()
                    {
                        ExportHandle.exports.crushers,
                        ExportHandle.exports.press,
                        ExportHandle.exports.kiln,
                        ExportHandle.exports.mashtun,
                        ExportHandle.exports.fermentation,
                        ExportHandle.exports.distillery,
                        ExportHandle.exports.barrel,
                        ExportHandle.exports.packer,

                    };

                    foreach (ExportHandle.exports machine in machines)
                    {

                        Mod.instance.exportHandle.AddExport(machine, 1);

                    }

                    break;

                case approachJester:

                    throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Jester].Position, IconData.relics.companion_dice);

                    throwRelic.register();

                    break;

                case swordFates:

                    Mod.instance.save.rite = Rite.Rites.fates;

                    break;

                case challengeFates:

                    if (!RelicHandle.HasRelic(IconData.relics.companion_tome))
                    {
                        throwRelic = new(Game1.player, Mod.instance.characters[CharacterHandle.characters.Shadowtin].Position, IconData.relics.companion_tome);

                        throwRelic.register();

                    }

                    break;

                case swordEther:

                    Mod.instance.save.rite = Rite.Rites.ether;

                    swordThrow = new(Game1.player, Game1.player.Position + new Vector2(192, -64), SpawnData.Swords.cutlass);

                    swordThrow.register();

                    ThrowHandle throwNotes = new(Game1.player, Game1.player.Position + new Vector2(192, -64), IconData.relics.box_measurer)
                    {
                        delay = 120
                    };

                    throwNotes.register();

                    break;

                case questBlackfeather:

                    Mod.instance.save.rite = Rite.Rites.witch;

                    swordThrow = new(Game1.player, Game1.player.Position + new Vector2(192, -64), SpawnData.Swords.knife);

                    swordThrow.register();

                    break;

                case questRevenant:

                    // Remove Revenant from game

                    Mod.instance.characters[CharacterHandle.characters.Revenant].SwitchToMode(Character.Character.mode.home, Game1.player);

                    ThrowHandle throwBook = new(Game1.player, Game1.player.Position + new Vector2(192, -64), IconData.relics.book_knight);

                    throwBook.register();

                    break;

                case challengeBones:

                    // Remove Effigy from game

                    Mod.instance.characters[CharacterHandle.characters.Effigy].SwitchToMode(Character.Character.mode.home, Game1.player);

                    break;

                case swordHeirs:

                    // Gift of the Heiress

                    if (!RelicHandle.HasRelic(IconData.relics.druid_hieress))
                    {

                        throwRelic = new(Game1.player, new Vector2(27, 5) * 64, IconData.relics.druid_hieress);

                        throwRelic.register();

                    }

                    break;

                case heirsTwo:

                    if (!RelicHandle.HasRelic(IconData.relics.stardew_druid))
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

            Vector2 throwPosition = Game1.player.Position + new Vector2(128, -64);

            ThrowHandle throwItem;

            switch (questId)
            {

                case challengeWeald:

                    friendship += questRating * 4;

                    VillagerData.CommunityFriendship(VillagerData.villagerLocales.mountain, friendship, questRating);

                    HerbalHandle.RandomHerbal(throwPosition);

                    break;

                case challengeMists:

                    friendship += questRating * 10;

                    VillagerData.CommunityFriendship(VillagerData.villagerLocales.town, friendship, questRating);

                    HerbalHandle.RandomHerbal(throwPosition);

                    break;

                case challengeStars:

                    friendship += questRating * 8;

                    VillagerData.CommunityFriendship(VillagerData.villagerLocales.forest, friendship, questRating);

                    HerbalHandle.RandomHerbal(throwPosition);

                    break;

                case challengeAtoll:

                    // rain totem (O)681
                    throwItem = new(Game1.player, throwPosition, new StardewValley.Object("681", 1));

                    throwItem.register();

                    HerbalHandle.RandomHerbal(throwPosition);

                    break;

                case challengeDragon:

                    // prismatic shard (O)74
                    throwItem = new(Game1.player, throwPosition, new StardewValley.Object("74", 1));

                    throwItem.register();

                    HerbalHandle.RandomHerbal(throwPosition);

                    break;

                case challengeFates:

                    // iridium sprinkler (O)645
                    throwItem = new(Game1.player, throwPosition, new StardewValley.Object("645", 1));

                    throwItem.register();

                    HerbalHandle.RandomHerbal(throwPosition);

                    break;

                case swordEther:

                    // faeth *20
                    Herbal Faeth = Mod.instance.herbalHandle.herbalism[HerbalHandle.herbals.faeth.ToString()];

                    DisplayMessage hudmessage = new("+20 " + Faeth.title, Faeth);

                    Game1.addHUDMessage(hudmessage);

                    HerbalHandle.UpdateHerbalism(HerbalHandle.herbals.faeth, 20);

                    HerbalHandle.RandomHerbal(throwPosition);

                    break;

                case challengeEther:

                    // rare seed (O)347 x2
                    throwItem = new(Game1.player, throwPosition, new StardewValley.Object("347", 2));

                    throwItem.register();

                    HerbalHandle.RandomHerbal(throwPosition);

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


                        int useDelay = Mod.instance.Config.paceProgress;

                        Mod.instance.save.progress[id] = new(0, useDelay);

                        break;


                }

            }

        }

        // ----------------------------------------------------------------------

        public void DialogueBefore(string questId, CharacterHandle.characters character = CharacterHandle.characters.none, bool force = false)
        {

            if (quests[questId].before.Count == 0)
            {

                return;

            }

            foreach (KeyValuePair<CharacterHandle.characters, DialogueSpecial> special in quests[questId].before)
            {

                if (character != CharacterHandle.characters.none)
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

                if (CharacterHandle.CharacterGone(special.Key))
                {

                    if(special.Value.questContext == 0)
                    {

                        Mod.instance.save.progress[questId].status = 1;

                    }

                    return;

                }

                if (!prompts.ContainsKey(special.Key))
                {

                    prompts[special.Key] = new();

                }

                if (!prompts[special.Key].Contains(questId))
                {

                    prompts[special.Key].Add(questId);

                }

                if (!force)
                {

                    if (Mod.instance.save.progress[questId].given == 2)
                    {

                        continue;

                    }

                    if (special.Value.questContext != 0 && Mod.instance.save.progress[questId].given == 1)
                    {

                        continue;

                    }

                }

                if (!Mod.instance.dialogue.ContainsKey(special.Key))
                {

                    Mod.instance.dialogue[special.Key] = new(special.Key);

                }

                special.Value.questId = questId;

                Mod.instance.dialogue[special.Key].AddSpecialDialogue(questId, special.Value);

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

            if (!Mod.instance.save.progress.ContainsKey(questId))
            {

                return;

            }

            if (Mod.instance.save.progress[questId].status != 0)
            {

                return;

            }

            if (context != 0)
            {

                if (Mod.instance.save.progress[questId].given < 2)
                {

                    Mod.instance.save.progress[questId].given = 1;

                }

                return;

            }

            Mod.instance.save.progress[questId].status = 1;

            Mod.instance.save.progress[questId].delay = 0;

            OnAccept(questId);

            Initialise(questId);

            foreach (KeyValuePair<CharacterHandle.characters, Dialogue.Dialogue> dialogues in Mod.instance.dialogue)
            {

                dialogues.Value.RemoveSpecialDialogue(questId);

            }

            Mod.instance.SyncProgress();

            Mod.instance.save.progress[questId].given = 2;

            return;

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

                    DialogueBefore(loadout[q], character, true);

                }

            }

        }

    }

}
