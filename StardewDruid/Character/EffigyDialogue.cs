using Microsoft.VisualBasic;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Character
{
    public class EffigyDialogue
    {

        public Effigy effigy;

        public bool lessonGiven;

        public string questFeedback;

        private string returnFrom;

        //public int caveChoice;

        public Dictionary<string, List<string>> specialDialogue = new();

        public EffigyDialogue()
        {

            //caveChoice = Game1.player.caveChoice.Value;

        }

        public void DialogueApproach()
        {

            if(specialDialogue.Count > 0)
            {

                DialogueSpecial();

                return;

            }

            string effigyQuestion = "Forgotten Effigy: ^Ah... the successor appears.";

            List<Response> effigyChoices = new();

            if (Map.QuestData.ChallengeCompleted())
            {

                effigyChoices.Add(new Response("threats", "What challenges may I undertake for the Circle?"));

            }
            else if (Map.QuestData.JourneyCompleted())
            {

                effigyChoices.Add(new Response("challenge", "Does the valley have need of me?"));

            }
            else
            {
                
                effigyChoices.Add(new Response("journey", "I've come for a lesson"));

            }

            if (Mod.instance.HasBlessing("earth"))
            {
                
                effigyChoices.Add(new Response("business", "I have some questions"));

            }
           
            effigyChoices.Add(new Response("none", "(say nothing)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(ApproachAnswer);

            returnFrom = null;

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour, effigy);

            return;

        }

        public void DialogueSpecial()
        {

            KeyValuePair<string,List<string>> special = specialDialogue.First();

            string effigyQuestion = special.Value[0];

            List<Response> effigyChoices = new()
            {
                new Response(special.Key, special.Value[1]),

                new Response("none", "(say nothing)")
            };

            specialDialogue.Remove(special.Key);

            GameLocation.afterQuestionBehavior effigyBehaviour = new(ApproachAnswer);

            returnFrom = null;

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour, effigy);

            return;

        }

        public void ApproachAnswer(Farmer effigyVisitor, string effigyAnswer)
        {

            switch (effigyAnswer)
            {

                case "journey":

                    DelayedAction.functionAfterDelay(ReplyJourney, 100);

                    break;

                case "challenge":

                    DelayedAction.functionAfterDelay(ReplyChallenge, 100);

                    break;

                case "threats":

                    DelayedAction.functionAfterDelay(ReplyThreats, 100);

                    break;

                case "query":

                    DelayedAction.functionAfterDelay(DialogueQuery, 100);

                    break;

                case "ancestor":

                    DelayedAction.functionAfterDelay(ReplyAncestor, 100);

                    break;

                case "demetrius":

                    DelayedAction.functionAfterDelay(DialogueDemetrius, 100);

                    break;

                case "business":

                    DelayedAction.functionAfterDelay(DialogueBusiness, 100);

                    break;
            }

            return;

        }

        // =============================================================
        // Challenge
        // =============================================================

        public void DialogueQuery()
        {

            Mod.instance.CompleteQuest("approachEffigy");

            List<Response> effigyChoices = new();

            string effigyQuestion = "Forgotten Effigy: ^I was crafted by the first farmer of the valley, a powerful friend of the otherworld." +
                "If you intend to succeed him, you will need to learn many lessons.";

            effigyChoices.Add(new Response("journey", "What is the first lesson?"));

            if (Game1.year >= 2)
            {

                effigyChoices.Add(new Response("ancestor", "The first farmer was my ancestor, and my family has practiced his craft for generations"));

            };

            effigyChoices.Add(new Response("none", "(say nothing)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(ApproachAnswer);

            returnFrom = null;

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour, effigy);

        }

        public void ReplyJourney()
        {

            Dictionary<string, int> blessingList = Mod.instance.BlessingList();

            string effigyReply;

            if (lessonGiven)
            {
                //effigyReply = "Forgotten Effigy: ^Hmm... return tomorrow after I have consulted the Others.";
                effigyReply = "Hmm... return tomorrow after I have consulted the Others.";
            }
            else if (!Mod.instance.QuestComplete("swordEarth"))
            {

                if (Mod.instance.QuestGiven("swordEarth"))
                {
                    //effigyReply = "Forgotten Effigy: ^Go to the forest. Find the giant malus. That is all for now.";
                    effigyReply = "Go to the forest. Find the giant malus. That is all for now.";
                }
                else
                {

                    //effigyReply = "Forgotten Effigy: " +
                    //"^Seek the patronage of the two Kings. Find the giant malus of the southern forest and perform a rite below it's boughs." +
                    //"..." +
                    //$"^({Mod.instance.CastControl()} with a melee weapon or scythe in hand to perform a rite. Hold the button to increase the range of the effect.)";

                    effigyReply = "Seek the patronage of the two Kings. Find the giant malus of the southern forest and perform a rite below it's boughs. " +
                    $"({Mod.instance.CastControl()} with a melee weapon or scythe in hand to perform a rite. Hold the button to increase the range of the effect.)";


                    Mod.instance.NewQuest("swordEarth");

                    Game1.currentLocation.playSoundPitched("discoverMineral", 600);

                }

            }
            else if (!Mod.instance.QuestComplete("challengeEarth"))
            {


                if (!blessingList.ContainsKey("earth"))
                {

                    Mod.instance.UpdateBlessing("earth");

                    Mod.instance.ChangeBlessing("earth");
                    
                    //Mod.instance.DecorateCave();

                }

                switch (blessingList["earth"])
                {

                    case 0: // explode weeds

                        effigyReply = "Good. You are now a subject of the two kingdoms, and bear authority over the weed and the twig. " +
                            "Use this new power to drive out decay and detritus. Return tomorrow for another lesson. " +
                            $"({Mod.instance.CastControl()}: explode weeds and twigs. Remotely greet animals, pets and villagers once a day. Hold the button to increase the range of the effect)";

                        Mod.instance.NewQuest("lessonVillager");

                        break;

                    case 1: // bush, water, grass, stump, boulder

                        effigyReply = "The valley springs into new life. Go now, sample its hidden bounty, and prepare to face those who guard its secrets. " +
                            $"({Mod.instance.CastControl()}: extract foragables from large bushes, wood from trees, fibre and seeds from grass and small fish from water. Might spawn monsters)";

                        Mod.instance.NewQuest("lessonCreature");

                        break;

                    case 2: // lawn, dirt, trees

                        effigyReply = "Years of stagnation have starved the valley of it's wilderness. Go now, and recolour the barren spaces. " +
                            $"({Mod.instance.CastControl()}: sprout trees, grass, seasonal forage and flowers in empty spaces)";

                        Mod.instance.NewQuest("lessonForage");

                        break;

                    case 3: // hoed

                        effigyReply = "Your connection to the earth deepens. You may channel the power of the Two Kings for your own purposes. " +
                            $"({Mod.instance.CastControl()}: increase the growth rate and quality of growing crops. Convert planted wild seeds into random cultivations. Fertilise trees and uptick the growth rate of fruittrees)";

                        Mod.instance.NewQuest("lessonCrop");

                        break;

                    case 4: // rockfall

                        effigyReply = "Be careful in the mines. The deep earth answers your call, both above and below you. " +
                            $"({Mod.instance.CastControl()}: shake loose rocks free from the ceilings of mine shafts. Explode gem ores)";

                        Mod.instance.NewQuest("lessonRockfall");

                        break;

                    default: // quest

                        if (Mod.instance.QuestGiven("challengeEarth"))
                        {
                            effigyReply = "Stop dallying. Return when the mountain is cleansed.";
                        }
                        else
                        {
                            effigyReply = "A trial presents itself. Foulness seeps from the mountain springs. Cleanse the source with the King's blessing. " +
                            $"(You have received a new quest)";

                            Mod.instance.NewQuest("challengeEarth");

                        }

                        break;

                }

                if (blessingList["earth"] <= 4)
                {

                    lessonGiven = true;

                    Mod.instance.LevelBlessing("earth");

                    Game1.currentLocation.playSoundPitched("discoverMineral", 600);

                }

            }
            else if (!Mod.instance.QuestComplete("swordWater"))
            {

                if (Mod.instance.QuestGiven("swordWater"))
                {
                    effigyReply = "Seek the furthest pier.";

                }
                else
                {
                    effigyReply = "The Voice Beyond the Shore harkens to you now. ^Perform a rite at the furthest pier, and behold her power. " +
                    $"({Mod.instance.CastControl()} with a weapon or scythe in hand to perform a rite. Hold the button to increase the range of the effect)";

                    Mod.instance.NewQuest("swordWater");

                    Game1.currentLocation.playSoundPitched("thunder_small", 1200);

                }

            }
            else if (!Mod.instance.QuestComplete("challengeWater"))
            {

                if (!blessingList.ContainsKey("water"))
                {

                    Mod.instance.UpdateBlessing("water");

                    Mod.instance.ChangeBlessing("water");
                    
                    //Mod.instance.DecorateCave();

                }

                switch (blessingList["water"])
                {

                    case 0: // warp totems

                        effigyReply = "Good. The Lady Beyond the Shore has answered your call. Find the shrines to the patrons of the Valley, and strike them to draw out a portion of their essence. Do the same to any obstacle in your way. " +
                        $"({Mod.instance.CastControl()}: strike warp shrines once a day to extract totems, and boulders and stumps to extract resources)";

                        //Mod.instance.LevelBlessing("special");

                        Mod.instance.NewQuest("lessonTotem");

                        break;

                    case 1: // scarecrow, rod, craftable, campfire

                        effigyReply = "The Lady is fascinated by the industriousness of humanity. Combine your artifice with her blessing and reap the rewards. " +
                            $"({Mod.instance.CastControl()}: strike scarecrows, campfires and lightning rods to activate special functions. Villager firepits will work too)";

                        Mod.instance.NewQuest("lessonCookout");

                        break;

                    case 2: // fishspot

                        effigyReply = "The denizens of the deep water serve the Lady. Go now, and test your skill against them. " +
                            $"({Mod.instance.CastControl()}: strike deep water to produce a fishing-spot that yields rare species of fish)";

                        Mod.instance.NewQuest("lessonFishspot");

                        break;

                    case 3: // stump, boulder, enemy

                        effigyReply = "Your connection to the plane beyond broadens. Call upon the Lady's Voice to destroy your foes. " +
                            $"({Mod.instance.CastControl()}: expend high amounts of stamina to instantly destroy enemies)";

                        Mod.instance.NewQuest("lessonSmite");

                        break;

                    case 4: // portal

                        effigyReply = "Are you yet a master of the veil between worlds? Focus your will to breach the divide. " +
                            $"({Mod.instance.CastControl()}: strike candle torches to create monster portals. Every candle included in the rite increases the challenge. Only works in remote outdoor locations like the backwoods)";

                        Mod.instance.NewQuest("lessonPortal");

                        break;

                    default: // quest

                        if (Mod.instance.QuestGiven("challengeWater"))
                        {
                            effigyReply = "Deal with the shadows first.";

                        }
                        else
                        {

                            effigyReply = "A new trial presents itself. Creatures of shadow linger in the hollowed grounds of the village. Smite them with the Lady's blessing. " +
                            $"(You have received a new quest)";

                            Mod.instance.NewQuest("challengeWater");

                        }

                        break;

                }

                if (blessingList["water"] <= 4)
                {

                    lessonGiven = true;

                    Mod.instance.LevelBlessing("water");

                    Game1.currentLocation.playSoundPitched("thunder_small", 1200);

                }

            }
            else if (!Mod.instance.QuestComplete("swordStars"))
            {

                if (Mod.instance.QuestGiven("swordStars"))
                {
                    effigyReply = "Find the lake of flames.";

                }
                else
                {

                    effigyReply = "Your name is known within the celestial plane. Travel to the lake of flames. Retrieve the final vestige of the first farmer.";

                    Mod.instance.NewQuest("swordStars");

                }

            }
            else if (!Mod.instance.QuestComplete("challengeStars"))
            {


                if (!blessingList.ContainsKey("stars"))
                {

                    Mod.instance.UpdateBlessing("stars");

                    Mod.instance.ChangeBlessing("stars");

                    //Mod.instance.DecorateCave();

                }

                switch (blessingList["stars"])
                {

                    case 0: // warp totems

                        effigyReply = "Excellent. The Stars Beyond the Expanse have chosen a new champion. Shatter the thickened wild so that new life may find root there. " +
                        $"({Mod.instance.CastControl()}: call down a shower of fireballs that increase in number and range as the cast is held)";

                        //Mod.instance.LevelBlessing("special");

                        Mod.instance.NewQuest("lessonMeteor");

                        Mod.instance.LevelBlessing("stars");

                        lessonGiven = true;

                        Game1.currentLocation.playSoundPitched("Meteorite", 1200);

                        break;

                    default:

                        if (Mod.instance.QuestGiven("challengeStars"))
                        {
                            effigyReply = "Only you can deal with the threat to the forest";

                        }
                        else
                        {
                            effigyReply = "Your last trial awaits. The southern forest reeks of our mortal enemy. Rain judgement upon the slime with the blessing of the Stars. " +
                                    "(You have received a new quest)";

                            Mod.instance.NewQuest("challengeStars");

                            //ModUtility.AnimateMeteor(Game1.player.currentLocation, Game1.player.getTileLocation() + new Vector2(0, -2), true);

                            Game1.currentLocation.playSoundPitched("Meteorite", 1200);

                        }

                        break;
                }

            }
            else
            {

                effigyReply = "Your power rivals that of the first farmer. I have nothing further to teach you. When the seasons change, the valley may call upon your aid once again. " +
                "(Thank you for playing with StardewDruid. Credits: Neosinf/StardewDruid, PathosChild/SMAPI, ConcernedApe/StardewValley)";

                Game1.currentLocation.playSound("yoba");

            }

            if (effigyReply.Length > 0)
            {

                Game1.drawDialogue(effigy, effigyReply);
               // Game1.activeClickableMenu = new DialogueBox(effigyReply);

            }

        }

        public void ReplyChallenge()
        {
            
            string effigyReply;

            List<string> challengeQuests = Map.QuestData.ChallengeQuests();

            List<string> addQuests = new();

            foreach (string questName in challengeQuests)
            {

                if (!Mod.instance.QuestGiven(questName))
                {

                    addQuests.Add(questName);

                }

            }

            foreach (string questName in addQuests)
            {

                Mod.instance.NewQuest(questName);

            }

            effigyReply = "Those with a twisted connection to the otherworld may remain tethered to the Valley long after their mortal vessel wastes away. " +
                "Strike them with bolt and flame to draw out and disperse their corrupted energies. " +
                "(You have received new quests)";

            Game1.currentLocation.playSound("yoba");

            lessonGiven = true;

            Game1.drawDialogue(effigy, effigyReply);

            return;

        }

        public void ReplyThreats()
        {

            string effigyReply;

            List<Response> effigyChoices = new();

            if (lessonGiven)
            {

                effigyReply = "I will scry through the endless chitter of those who watch from the otherworld. Return to me tomorrow for more tidings.";

                Game1.drawDialogue(effigy, effigyReply);

                return;

            }

            List<string> activeThreats = Map.QuestData.ActiveSeconds();

            if (activeThreats.Count > 0)
            {

                effigyReply = "Have you dealt with the threat before you?";

                effigyChoices.Add(new Response("abort", "I have been unable to proceed against this threat for now. Is there something else?"));

                effigyChoices.Add(new Response("cancel", "(oh that's right)"));

            }
            else
            {
                effigyReply = "An old threat has re-emerged. Be careful, they may have increased in power since your last confrontation.";

                effigyChoices.Add(new Response("accept", "(accept) I am ready to face the renewed threat"));

                effigyChoices.Add(new Response("refuse", "(refuse) The valley must endure without my help"));

                effigyChoices.Add(new Response("cancel", "(not now) I'll need some time to prepare"));

            }

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerThreats);

            Game1.player.currentLocation.createQuestionDialogue(effigyReply, effigyChoices.ToArray(), effigyBehaviour, effigy);

        }

        public void AnswerThreats(Farmer effigyVisitor, string effigyAnswer)
        {
            string effigyReply;

            switch (effigyAnswer)
            {

                case "accept":

                    Dictionary<string, string> challengeList = Map.QuestData.SecondQuests();

                    List<string> enterList = new();

                    List<string> rotateList = new();

                    effigyReply = "May you be successful against the shadows of the otherworld. (You have received a new quest)";

                    foreach (KeyValuePair<string, string> challenge in challengeList)
                    {

                        string questName = challenge.Key + "Two";

                        if (!Mod.instance.QuestGiven(questName))
                        {

                            enterList.Add(questName);

                        }

                        rotateList.Add(questName);

                    }

                    if (enterList.Count == 0)
                    {

                        enterList = rotateList;

                    }

                    string enterChallenge = enterList[Game1.random.Next(enterList.Count)];

                    Mod.instance.NewQuest(enterChallenge);

                    Game1.currentLocation.playSound("yoba");

                    lessonGiven = true;

                    Game1.drawDialogue(effigy, effigyReply);

                    break;

                case "refuse":

                    lessonGiven = true;

                    effigyReply = "The valley will withstand the threat as it can, as it always has.";

                    Game1.drawDialogue(effigy, effigyReply);

                    break;

                case "abort":

                    List<string> activeThreats = Map.QuestData.ActiveSeconds();

                    Mod.instance.RemoveQuest(activeThreats[0]);

                    effigyReply = "The valley will withstand the threat as it can, as it always has.";

                    Game1.drawDialogue(effigy, effigyReply);

                    break;

                case "cancel":

                    returnFrom = "threats";

                    DelayedAction.functionAfterDelay(DialogueApproach, 100);

                    break;

            }

            return;

        }

        public void ReplyAncestor()
        {
            string effigyReply = "Indeed, I hear the wisdom of the old valley in each word spoken. " +
            "(All rites unlocked at maximum level)";

            Game1.currentLocation.playSound("yoba");

            Mod.instance.UnlockAll();

            //Mod.instance.DecorateCave();

            Game1.drawDialogue(effigy, effigyReply);
            //Game1.activeClickableMenu = new DialogueBox(effigyReply);

        }


        public void DialogueDemetrius()
        {

            string effigyQuestion = "Forgotten Effigy: ^I concealed myself for a time, then I spoke to him in the old tongue of the Calico shamans.";

            List<Response> effigyChoices = new()
            {
                new Response("descended", "Do you think Demetrius is descended from the shaman tradition?!"),

                new Response("offended", "Wow, he must have been offended. Demetrius is a man of modern science and sensibilities."),

                new Response("return", "Nope, not going to engage with this.")
            };

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerDemetrius);

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour, effigy);

            return;

        }

        public void AnswerDemetrius(Farmer effigyVisitor, string effigyAnswer)
        {

            string effigyReply;

            switch (effigyAnswer)
            {
                case "return":

                    returnFrom = "demetrius";

                    DelayedAction.functionAfterDelay(DialogueApproach, 100);

                    return;

                default: //

                    if (Game1.player.caveChoice.Value == 1)
                    {

                        effigyReply = "... ... He came in with a feathered mask on, invoked a rite of summoning, threw Bat feed everywhere, then ran off singing \"Old man in a frog pond\".";

                    }
                    else
                    {

                        effigyReply = "I can smell the crisp, sandy scent of the Calico variety of mushroom. The shamans would eat them to... enter a trance-like state.";

                    }

                    break;

            }
            Game1.drawDialogue(effigy, effigyReply);
            //Game1.activeClickableMenu = new DialogueBox(effigyReply);



        }

        // =============================================================
        // Business
        // =============================================================

        public void DialogueBusiness()
        {

            string effigyQuestion = "Forgotten Effigy: ^The traditions live on.";

            List<Response> effigyChoices = new();

            Dictionary<string, int> blessingList = Mod.instance.BlessingList();

            string blessing = Mod.instance.ActiveBlessing();

            effigyChoices.Add(new Response("effects", "I'd like to review my training (manage rite effects)"));

            if (blessingList.ContainsKey("water"))
            //if (Mod.instance.HasBlessing("water"))
            {

                effigyChoices.Add(new Response("blessing", "I want to change my patron (change rite)"));

            }

            if (blessingList.ContainsKey("earth") && Context.IsMultiplayer && Context.IsMainPlayer)
            //if (Mod.instance.HasBlessing("earth") && Context.IsMultiplayer && Context.IsMainPlayer)
            {

                effigyChoices.Add(new Response("farmhands", "I want to share what I've learned with others (train farmhands)"));

            }

            int toolIndex = Mod.instance.AttuneableWeapon();

            if (toolIndex != -1)
            {

                effigyChoices.Add(new Response("attune", $"I want to dedicate this {Game1.player.CurrentTool.Name} (manage attunement)"));


            }

            effigyChoices.Add(new Response("none", "(nevermind)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerBusiness);

            returnFrom = null;

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour, effigy);

            return;
        }

        public void AnswerBusiness(Farmer effigyVisitor, string effigyAnswer)
        {

            switch (effigyAnswer)
            {
                case "blessing":

                    DelayedAction.functionAfterDelay(DialogueBlessing, 100);

                    break;

                case "effects":

                    DelayedAction.functionAfterDelay(DialogueEffects, 100);

                    break;

                case "farmhands":

                    DelayedAction.functionAfterDelay(DialogueFarmhands, 100);

                    break;

                case "attune":

                    DelayedAction.functionAfterDelay(DialogueAttune, 100);

                    break;

            }

            return;

        }

        public void DialogueBlessing()
        {

            string effigyQuestion = "Forgotten Effigy: ^The Kings, the Lady, the Stars, I may entreat them all.";

            List<Response> effigyChoices = new();

            if (Mod.instance.ActiveBlessing() != "earth")
            {

                effigyChoices.Add(new Response("earth", "Seek the Two Kings for me"));

            }

            if (Mod.instance.BlessingList().ContainsKey("water") && Mod.instance.ActiveBlessing() != "water")
            {

                effigyChoices.Add(new Response("water", "Call out to the Lady Beyond The Shore"));

            }

            if (Mod.instance.BlessingList().ContainsKey("stars") && Mod.instance.ActiveBlessing() != "stars")
            {

                effigyChoices.Add(new Response("stars", "Look to the Stars for me"));

            }

            if (Mod.instance.ActiveBlessing() != "none")
            {

                effigyChoices.Add(new Response("none", "I don't want anyone's favour (disables all effects)"));

            }

            effigyChoices.Add(new Response("cancel", "(say nothing)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerBlessing);

            returnFrom = null;

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour,effigy);

            return;

        }

        public void AnswerBlessing(Farmer effigyVisitor, string effigyAnswer)
        {

            string effigyReply;

            switch (effigyAnswer)
            {
                case "earth":

                    Game1.addHUDMessage(new HUDMessage($"{Mod.instance.CastControl()} to perform rite of the earth", ""));

                    effigyReply = "The Kings of Oak and Holly come again.";

                    Game1.currentLocation.playSound("discoverMineral");

                    break;

                case "water":

                    Game1.addHUDMessage(new HUDMessage($"{Mod.instance.CastControl()} to perform rite of the water", ""));

                    effigyReply = "The Voice Beyond the Shore echoes around us.";

                    Game1.currentLocation.playSound("thunder_small");

                    break;

                case "stars":

                    Game1.addHUDMessage(new HUDMessage($"{Mod.instance.CastControl()} to perform rite of the stars", ""));

                    effigyReply = "Life to ashes. Ashes to dust.";

                    Game1.currentLocation.playSound("Meteorite");

                    break;

                case "none":

                    Game1.addHUDMessage(new HUDMessage($"{Mod.instance.CastControl()} will do nothing", ""));

                    effigyReply = "The light fades away.";

                    Game1.currentLocation.playSound("ghost");

                    break;

                default: // "cancel"

                    effigyReply = "(says nothing back).";

                    break;

            }

            if (effigyAnswer != "cancel")
            {

                Mod.instance.ChangeBlessing(effigyAnswer);

            }

            //Mod.instance.DecorateCave();

            Game1.drawDialogue(effigy, effigyReply);
            //Game1.activeClickableMenu = new DialogueBox(effigyReply);

        }

        public void DialogueEffects()
        {

            Dictionary<string, int> blessingList = Mod.instance.BlessingList();

            Dictionary<string, int> toggleList = Mod.instance.ToggleList();

            string effigyQuestion = "Forgotten Effigy: ^Our traditions are etched into the bedrock of the valley.";

            if (returnFrom == "forget")
            {

                effigyQuestion = "Forgotten Effigy: ^The druid's life is... full of random surprises... but may you not suffer any more of this kind.";


            }

            if (returnFrom == "remember")
            {

                effigyQuestion = "Forgotten Effigy: ^Let the essence of life itself enrich your world.";

            }

            List<Response> effigyChoices = new()
            {
                new Response("earth", "What role do the Two Kings play?")
            };

            if (blessingList.ContainsKey("water"))
            {

                effigyChoices.Add(new Response("water", "Who is the Voice Beyond the Shore?"));

            }

            if (blessingList.ContainsKey("stars"))
            {

                effigyChoices.Add(new Response("stars", "Do the Stars have names?"));

            }

            if (blessingList["earth"] >= 2)
            {
                if (toggleList.Count < 4)
                {
                    effigyChoices.Add(new Response("disable", "I'd rather forget something that happened (disable effects)"));

                }
                if (toggleList.Count > 0)
                {

                    effigyChoices.Add(new Response("enable", "I want to relearn something (enable effects)"));

                }

            }

            effigyChoices.Add(new Response("return", "(nevermind)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerEffects);

            returnFrom = null;

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour,effigy);

            return;

        }

        public void AnswerEffects(Farmer effigyVisitor, string effigyAnswer)
        {

            switch (effigyAnswer)
            {

                case "earth":

                    DelayedAction.functionAfterDelay(EffectsEarth, 100);

                    break;

                case "water":

                    DelayedAction.functionAfterDelay(EffectsWater, 100);

                    break;

                case "stars":

                    DelayedAction.functionAfterDelay(EffectsStars, 100);

                    break;

                case "disable":

                    DelayedAction.functionAfterDelay(DialogueDisable, 100);

                    break;

                case "enable":

                    DelayedAction.functionAfterDelay(DialogueEnable, 100);

                    break;

                case "return":

                    returnFrom = "effects";

                    DelayedAction.functionAfterDelay(DialogueApproach, 100);

                    break;

            }

            return;

        }

        public void EffectsEarth()
        {

            Dictionary<string, int> blessingList = Mod.instance.BlessingList();

            string effigyQuestion = "Forgotten Effigy: ^The King of Oaks and the King of Holly war upon the Equinox. One will rule with winter, one with summer.";

            if (blessingList["earth"] >= 1)
            {

                effigyQuestion += "^Lesson 1. Explode weeds and twigs. Greet Villagers, Pets and Animals once a day.";

            }

            if (blessingList["earth"] >= 2)
            {

                effigyQuestion += "^Lesson 2. Extract foragables from the landscape. Might attract monsters.";

            }

            effigyQuestion += "^ ";

            List<Response> effigyChoices = new();

            GameLocation.afterQuestionBehavior effigyBehaviour;

            if (blessingList["earth"] >= 3)
            {

                effigyChoices.Add(new Response("next", "Next ->"));

                effigyBehaviour = new(EffectsEarthTwo);

            }
            else
            {

                effigyChoices.Add(new Response("return", "It's all clear now"));

                effigyBehaviour = new(ReturnEffects);

            }

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour, effigy);

            return;

        }

        public void EffectsEarthTwo(Farmer effigyVisitor, string effigyAnswer)
        {

            Dictionary<string, int> blessingList = Mod.instance.BlessingList();

            string effigyQuestion = "Forgotten Effigy: ^Lesson 3. Sprout trees, grass, seasonal forage and flowers in empty spaces.";

            if (blessingList["earth"] >= 4)
            {

                effigyQuestion += "^Lesson 4. Increase the growth rate and quality of growing crops. Convert planted wild seeds into random cultivations.";

            }

            if (blessingList["earth"] >= 5)
            {

                effigyQuestion += "^Lesson 5. Shake loose rocks free from the ceilings of mine shafts. Explode gem ores.";

            }

            effigyQuestion += "^ ";

            List<Response> effigyChoices = new()
            {
                new Response("return", "It's all clear now")
            };

            GameLocation.afterQuestionBehavior effigyBehaviour = new(ReturnEffects);

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour,effigy);

            return;

        }

        public void EffectsWater()
        {

            Dictionary<string, int> blessingList = Mod.instance.BlessingList();

            string effigyQuestion = "Forgotten Effigy: ^The Voice is that of the Lady of the Isle of Mists. She is as ancient and powerful as the sunset on the Gem Sea.";

            if (blessingList["water"] >= 1)
            {

                effigyQuestion += "^Lesson 1. Strike warp shrines, stumps, logs and boulders to extract resources.";

            }

            if (blessingList["water"] >= 2)
            {

                effigyQuestion += "^Lesson 2. Strike scarecrows, campfires and lightning rods to activate special functions.";

            }

            effigyQuestion += "^ ";

            List<Response> effigyChoices = new();

            GameLocation.afterQuestionBehavior effigyBehaviour;

            if (blessingList["water"] >= 3)
            {

                effigyChoices.Add(new Response("next", "Next ->"));

                effigyBehaviour = new(EffectsWaterTwo);

            }
            else
            {

                effigyChoices.Add(new Response("return", "It's all clear now"));

                effigyBehaviour = new(ReturnEffects);

            }

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour, effigy);

            return;

        }

        public void EffectsWaterTwo(Farmer effigyVisitor, string effigyAnswer)
        {

            Dictionary<string, int> blessingList = Mod.instance.BlessingList();

            string effigyQuestion = "Forgotten Effigy: ^Lesson 3. Strike deep water to produce a fishing-spot that yields rare species of fish.";


            if (blessingList["water"] >= 4)
            {

                effigyQuestion += "^Lesson 4. Expend high amounts of stamina to smite enemies with bolts of power.";

            }

            if (blessingList["water"] >= 5)
            {

                effigyQuestion += "^Lesson 5. Strike candle torches placed in remote outdoor locations to produce monster portals.";

            }

            effigyQuestion += "^ ";

            List<Response> effigyChoices = new()
            {
                new Response("return", "It's all clear now")
            };

            GameLocation.afterQuestionBehavior effigyBehaviour = new(ReturnEffects);

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour, effigy);

            return;

        }

        public void EffectsStars()
        {

            Dictionary<string, int> blessingList = Mod.instance.BlessingList();

            string effigyQuestion = "Forgotten Effigy: ^The Stars have no names that can be uttered by earthly dwellers. They exist high above, and beyond, and care not for the life of our world, though their light sustains much of it. ^Yet... there is one star... a fallen star. That has a name. A name that we dread to speak.";

            List<Response> effigyChoices = new()
            {
                new Response("return", "It's all clear now")
            };

            GameLocation.afterQuestionBehavior effigyBehaviour = new(ReturnEffects);

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour, effigy);

            return;

        }

        public void ReturnEffects(Farmer effigyVisitor, string effigyAnswer)
        {

            DelayedAction.functionAfterDelay(DialogueEffects, 100);

            return;

        }

        public void DialogueDisable()
        {

            Dictionary<string, int> blessingList = Mod.instance.BlessingList();

            Dictionary<string, int> toggleList = Mod.instance.ToggleList();

            string effigyQuestion = "Forgotten Effigy: ^Is there a lesson you'd rather forget.";

            List<Response> effigyChoices = new();

            if (!toggleList.ContainsKey("forgetSeeds") && blessingList["earth"] >= 3)
            {

                effigyChoices.Add(new Response("forgetSeeds", "I end up with seeds in my boots everytime I run through the meadow. IT'S ANNOYING."));

            }

            if (!toggleList.ContainsKey("forgetFish"))
            {

                effigyChoices.Add(new Response("forgetFish", "I got slapped in the face by a flying fish today."));

            }

            if (!toggleList.ContainsKey("forgetCritters"))
            {

                effigyChoices.Add(new Response("forgetCritters", "Why does a bat sleep in every damn tree on this farm. Can't they live in this cave instead?"));

            }

            if (!toggleList.ContainsKey("forgetTrees") && blessingList["earth"] >= 3)
            {

                effigyChoices.Add(new Response("forgetTrees", "Just about inside Clint's by 3:50pm when a tree sprouted in front of me. Now my crotch is sore AND I don't have a Copper Axe."));

            }

            if (effigyChoices.Count == 0)
            {

                effigyQuestion = "Forgotten Effigy: ^Your mind is already completely empty.";

                effigyChoices.Add(new Response("return", "(sure...)"));

            }
            else
            {

                effigyChoices.Add(new Response("return", "(nevermind)"));

            }

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerDisable);

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour, effigy);

            return;

        }

        public void AnswerDisable(Farmer effigyVisitor, string effigyAnswer)
        {

            switch (effigyAnswer)
            {
                case "return":

                    returnFrom = "nevermind";

                    break;

                default: //

                    Mod.instance.ToggleEffect(effigyAnswer);

                    returnFrom = "forget";

                    break;

            }

            DelayedAction.functionAfterDelay(DialogueEffects, 100);

        }

        public void DialogueEnable()
        {

            Dictionary<string, int> blessingList = Mod.instance.BlessingList();

            Dictionary<string, int> toggleList = Mod.instance.ToggleList();

            string effigyQuestion = "Forgotten Effigy: ^The mind is open.";

            List<Response> effigyChoices = new();

            if (toggleList.ContainsKey("forgetSeeds"))
            {

                effigyChoices.Add(new Response("forgetSeeds", "There's a time to reap and a time to sow. I want to reap seeds from wild grass, sell them, and buy a Sow."));

            }

            if (toggleList.ContainsKey("forgetFish"))
            {

                effigyChoices.Add(new Response("forgetFish", "I miss the way the fish dance to the rhythm of the rite"));

            }

            if (toggleList.ContainsKey("forgetCritters"))
            {

                effigyChoices.Add(new Response("forgetCritters", "I miss the feeling of being watched from every bush."));

            }

            if (toggleList.ContainsKey("forgetTrees"))
            {

                effigyChoices.Add(new Response("forgetTrees", "Stuff Clint. I want to impress Emily with the magic sprout trick."));

            }

            if (effigyChoices.Count == 0)
            {

                effigyQuestion = "Forgotten Effigy: ^You already remember everything I taught you.";

                effigyChoices.Add(new Response("return", "(sure...)"));

            }
            else
            {

                effigyChoices.Add(new Response("return", "(nevermind)"));

            }

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerEnable);

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour, effigy);

            return;

        }

        public void AnswerEnable(Farmer effigyVisitor, string effigyAnswer)
        {

            switch (effigyAnswer)
            {
                case "return":

                    returnFrom = "nevermind";

                    break;

                default: //

                    Mod.instance.ToggleEffect(effigyAnswer);

                    returnFrom = "remember";

                    break;

            }

            DelayedAction.functionAfterDelay(DialogueEffects, 100);

        }

        public void DialogueFarmhands()
        {

            string effigyReply = "Teach them to embrace the source, or seize it.";

            Mod.instance.TrainFarmhands();

            Game1.drawDialogue(effigy, effigyReply);
            //Game1.activeClickableMenu = new DialogueBox(effigyReply);

            return;

        }

        public void DialogueAttune()
        {

            string effigyQuestion;

            List<Response> effigyChoices = new();

            int toolIndex = Mod.instance.AttuneableWeapon();

            string attunement = Mod.instance.AttunedWeapon(toolIndex);

            Dictionary<string, int> blessingList = Mod.instance.BlessingList();

            effigyQuestion = $"Forgotten Effigy: ^To whom should this {Game1.player.CurrentTool.Name} be dedicated to?^";

            if (attunement != "reserved")
            {

                if (attunement != "earth" && blessingList.ContainsKey("earth"))
                {

                    effigyChoices.Add(new Response("earth", $"To the Two Kings"));


                }

                if (attunement != "water" && blessingList.ContainsKey("water"))
                {

                    effigyChoices.Add(new Response("water", $"To the Lady Beyond the Shore"));


                }

                if (attunement != "stars" && blessingList.ContainsKey("stars"))
                {

                    effigyChoices.Add(new Response("stars", $"To the Stars Themselves"));


                }

                if (attunement != "none")
                {

                    effigyChoices.Add(new Response("none", "I want to reclaim it for myself (removes attunement)"));


                }

            }
            else
            {

                effigyQuestion = $"Forgotten Effigy: ^This {Game1.player.CurrentTool.Name} will not attune to anything else.";

            }

            effigyChoices.Add(new Response("return", "(nevermind)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerAttune);

            returnFrom = null;

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour, effigy);

        }

        public void AnswerAttune(Farmer effigyVisitor, string effigyAnswer)
        {

            //string effigyReply = $"Forgotten Effigy: ^Done. Now this {Game1.player.CurrentTool.Name} will serve ";
            string effigyReply = $"This {Game1.player.CurrentTool.Name} will serve ";

            switch (effigyAnswer)
            {
                case "return":

                    returnFrom = "attune";

                    DelayedAction.functionAfterDelay(DialogueApproach, 100);

                    return;

                case "none":

                    //effigyReply = $"Forgotten Effigy: ^This {Game1.player.CurrentTool.Name} will no longer serve.";
                    effigyReply = $"This {Game1.player.CurrentTool.Name} will no longer serve.";

                    Mod.instance.DetuneWeapon();
                    
                    Game1.drawDialogue(effigy, effigyReply);
                    //Game1.activeClickableMenu = new DialogueBox(effigyReply);

                    return;

                case "stars": effigyReply += "the very Stars Themselves"; Game1.currentLocation.playSound("Meteorite"); break;

                case "water": effigyReply += "the Lady Beyond the Shore"; Game1.currentLocation.playSound("thunder_small"); break;

                default: effigyReply += "the Two Kings"; Game1.currentLocation.playSound("discoverMineral"); break; //earth

            }

            Mod.instance.AttuneWeapon(effigyAnswer);

            Game1.drawDialogue(effigy, effigyReply);
            //Game1.activeClickableMenu = new DialogueBox(effigyReply);

            return;

        }


    }
}
