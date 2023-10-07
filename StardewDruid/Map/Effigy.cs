using Microsoft.Xna.Framework;
using StardewValley.Menus;
using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xTile.Layers;
using xTile.Tiles;
using System.Runtime.CompilerServices;
using StardewValley.Locations;
using System.ComponentModel.Design;

namespace StardewDruid.Map
{
    internal class Effigy
    {

        private readonly Mod mod;

        public bool lessonGiven;

        public string questCompleted;

        private int X;

        private int Y;

        private bool hideStatue;

        private bool makeSpace;

        private string returnFrom;

        public int caveChoice;

        public Effigy(Mod Mod,int statueX, int statueY, bool HideStatue, bool MakeSpace)
        {

            mod = Mod;

            lessonGiven = false;

            X = statueX; // 6

            Y = statueY; // 3

            hideStatue = HideStatue;

            makeSpace = MakeSpace;

            caveChoice = Game1.player.caveChoice.Value;

        }

        public void ModifyCave()
        {

            if(hideStatue) { return; }

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            Layer backLayer = farmCave.map.GetLayer("Back");

            Layer buildingsLayer = farmCave.map.GetLayer("Buildings");

            //------------------------ rearrange existing tiles

            if (makeSpace) {


                backLayer.Tiles[6, 1] = buildingsLayer.Tiles[6, 1];

                backLayer.Tiles[5, 2] = buildingsLayer.Tiles[5, 2];

                backLayer.Tiles[6, 2] = buildingsLayer.Tiles[6, 3];

                backLayer.Tiles[7, 2] = buildingsLayer.Tiles[7, 2];

                buildingsLayer.Tiles[5, 3] = buildingsLayer.Tiles[3, 4];

                buildingsLayer.Tiles[7, 3] = buildingsLayer.Tiles[9, 4];


            }


            //------------------------ add effigy

            string witchTiles = Path.Combine("Maps", "WitchHutTiles");

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Content", witchTiles + ".xnb")))
            {

                TileSheet witchSheet = new("witchHutTiles", farmCave.map, witchTiles, new xTile.Dimensions.Size(8, 16), new xTile.Dimensions.Size(1, 1));

                farmCave.map.AddTileSheet(witchSheet);

                buildingsLayer.Tiles[X, Y-2] = new StaticTile(buildingsLayer, witchSheet, BlendMode.Alpha, 108);

                buildingsLayer.Tiles[X-1, Y-1] = new StaticTile(buildingsLayer, witchSheet, BlendMode.Alpha, 115);

                StaticTile effigyBody = new(buildingsLayer, witchSheet, BlendMode.Alpha, 116);

                effigyBody.TileIndexProperties.Add("Action", "FarmCaveDruidEffigy");

                buildingsLayer.Tiles[X, Y-1] = effigyBody;

                buildingsLayer.Tiles[X+1, Y-1] = new StaticTile(buildingsLayer, witchSheet, BlendMode.Alpha, 117);

                StaticTile effigyFeet = new(buildingsLayer, witchSheet, BlendMode.Alpha, 124);

                effigyFeet.TileIndexProperties.Add("Action", "FarmCaveDruidEffigy");

                buildingsLayer.Tiles[X, Y] = effigyFeet;

            }

            //------------------------ add decoration

            string craftableTiles = Path.Combine("TileSheets", "Craftables"); // preloading tilesheet

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Content", craftableTiles + ".xnb")))
            {

                TileSheet craftableSheet = new("craftableTiles", farmCave.map, craftableTiles, new xTile.Dimensions.Size(8, 72), new xTile.Dimensions.Size(1, 1));

                farmCave.map.AddTileSheet(craftableSheet);


            }
            /*
            string fireballTiles = Path.Combine("TileSheets", "Fireball"); // preloading tilesheet

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Content", fireballTiles + ".xnb")))
            {

                TileSheet fireballSheet = new("fireballTiles", farmCave.map, fireballTiles, new xTile.Dimensions.Size(4, 4), new xTile.Dimensions.Size(2, 2));

                farmCave.map.AddTileSheet(fireballSheet);


            }

            string animationTiles = Path.Combine("TileSheets", "animations"); // preloading tilesheet

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Content", animationTiles + ".xnb")))
            {

                TileSheet animationSheet = new("animationTiles", farmCave.map, animationTiles, new xTile.Dimensions.Size(10, 52), new xTile.Dimensions.Size(1, 1));

                farmCave.map.AddTileSheet(animationSheet);


            }*/

            switch (mod.ActiveBlessing())
            {
                case "earth":

                    EarthDecoration();

                    break;

                case "water":

                    WaterDecoration();

                    break;

                case "stars":

                    StarsDecoration();

                    break;

                default: // "none"

                    break;

            }

            return;

        }

        public void NoDecoration()
        {

            if (hideStatue) { return; }

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            TileSheet craftableSheet = farmCave.map.GetTileSheet("craftableTiles");

            if (craftableSheet != null)
            {

                Layer frontLayer = farmCave.map.GetLayer("Front");


                frontLayer.Tiles[X-1, Y-3] = null;

                frontLayer.Tiles[X-1, Y-2] = null;

                frontLayer.Tiles[X+1, Y-3] = null;

                frontLayer.Tiles[X+1, Y-2] = null;

            }

            return;

        }

        public void EarthDecoration()
        {

            if (hideStatue) { return; }

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            TileSheet craftableSheet = farmCave.map.GetTileSheet("craftableTiles");

            if (craftableSheet != null)
            {

                Layer frontLayer = farmCave.map.GetLayer("Front");

                frontLayer.Tiles[X - 1, Y - 3] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 372);

                frontLayer.Tiles[X - 1, Y - 2] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 380);

                frontLayer.Tiles[X + 1, Y - 3] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 372);

                frontLayer.Tiles[X + 1, Y - 2] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 380);
            }

            return;

        }

        public void WaterDecoration()
        {

            if (hideStatue) { return; }

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            TileSheet craftableSheet = farmCave.map.GetTileSheet("craftableTiles");

            if (craftableSheet != null)
            {

                Layer frontLayer = farmCave.map.GetLayer("Front");

                frontLayer.Tiles[X - 1, Y - 3] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 373);

                frontLayer.Tiles[X - 1, Y - 2] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 381);

                frontLayer.Tiles[X + 1, Y - 3] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 373);

                frontLayer.Tiles[X + 1, Y - 2] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 381);
            }

            return;

        }

        public void StarsDecoration()
        {

            if (hideStatue) { return; }

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            TileSheet craftableSheet = farmCave.map.GetTileSheet("craftableTiles");

            if (craftableSheet != null)
            {

                Layer frontLayer = farmCave.map.GetLayer("Front");

                frontLayer.Tiles[X - 1, Y - 3] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 374);

                frontLayer.Tiles[X - 1, Y - 2] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 382);

                frontLayer.Tiles[X + 1, Y - 3] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 374);

                frontLayer.Tiles[X + 1, Y - 2] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 382);

            }

            return;

        }

        public void DialogueApproach()
        {

            string effigyQuestion;

            List<Response> effigyChoices = new();

            Dictionary<string, int> blessingList = mod.BlessingList();

            if (!mod.QuestComplete("approachEffigy"))
            {
                
                effigyQuestion = "Forgotten Effigy: ^Ah... the successor appears.";

                effigyChoices.Add(new Response("query", "What are you?"));

            }
            else if(Game1.player.caveChoice.Value != caveChoice)
            {

                effigyQuestion = "Forgotten Effigy: ^I had a visitor today.";

                effigyChoices.Add(new Response("demetrius", "Did you meet Demetrius?"));

                caveChoice = Game1.player.caveChoice.Value;

            }
            else if(questCompleted != null)
            {
                effigyQuestion = "Forgotten Effigy: ^I sense a change.";

                switch (questCompleted)
                {
                    
                    case "challengeEarth":

                        effigyChoices.Add(new Response("journey", "The rite disturbed the bats. Like ALL the bats."));

                        break;

                    case "swordWater":

                        effigyChoices.Add(new Response("journey", "I went to the pier and... was that a bolt of lightning?"));

                        break;

                    case "challengeWater":

                        effigyChoices.Add(new Response("journey", "The graveyard has a few less shadows."));

                        break;

                    case "swordStars":

                        effigyChoices.Add(new Response("journey", "I found the lake of flames."));

                        break;

                    case "challengeStars":

                        effigyChoices.Add(new Response("journey", "That was like making popcorn. Pop pop pop pop. Bits everywhere."));

                        break;

                    default: // case "swordEarth":

                        effigyChoices.Add(new Response("journey", "The tree gave me a branch shaped like a sword."));

                        break;

                }

                questCompleted = null;

            }
            else if (blessingList.ContainsKey("stars"))
            {

                effigyQuestion = "Satisfied Effigy: ^Successor.";

                effigyChoices.Add(new Response("journey", "Does the valley have need of me?"));

            }
            else
            {

                effigyQuestion = "Forgotten Effigy: ^Successor.";

                effigyChoices.Add(new Response("journey", "I've come for a lesson"));

            }

            if (blessingList.ContainsKey("earth"))
            {

                effigyChoices.Add(new Response("effects", "I'd like to review my training (manage rite effects)"));

            }

            if (blessingList.ContainsKey("water"))
            {

                effigyChoices.Add(new Response("blessing", "I want to change my patron (change rite)"));

            }

            effigyChoices.Add(new Response("none", "(say nothing)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(ApproachAnswer);

            returnFrom = null;

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

            return;

        }

        public void ApproachAnswer(Farmer effigyVisitor, string effigyAnswer)
        {

            switch (effigyAnswer)
            {
                case "blessing":

                    DelayedAction.functionAfterDelay(DialogueBlessing, 100);

                    break;

                case "journey":

                    DelayedAction.functionAfterDelay(DialogueJourney, 100);

                    break;

                case "effects":

                    DelayedAction.functionAfterDelay(DialogueEffects, 100);

                    break;

                case "query":

                    DelayedAction.functionAfterDelay(DialogueQuery, 100);

                    break;

                case "ancestor":

                    DelayedAction.functionAfterDelay(DialogueAncestor, 100);

                    break;

                case "demetrius":

                    DelayedAction.functionAfterDelay(DialogueDemetrius, 100);

                    break;

            }

            return;

        }

        public void DialogueQuery()
        {

            mod.UpdateQuest("approachEffigy", true);

            List<Response> effigyChoices = new();

            string effigyQuestion = "Forgotten Effigy: " +
                "^I was crafted by the first farmer of the valley, a powerful friend of the otherworld." +
                "^If you intend to succeed him, you will need to learn many lessons.";

            effigyChoices.Add(new Response("journey", "What is the first lesson?"));

            if(Game1.year >= 2)
            {

                effigyChoices.Add(new Response("ancestor", "The first farmer was my ancestor, and my family has practiced his craft for generations"));

            };

            effigyChoices.Add(new Response("none", "(say nothing)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(ApproachAnswer);

            returnFrom = null;

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

        }

        public void DialogueJourney()
        {

            Dictionary<string, int> blessingList = mod.BlessingList();

            string effigyReply;

            if (lessonGiven)
            {
                effigyReply = "Forgotten Effigy: ^Hmm... return tomorrow after I have consulted the Others.";

            }
            else if (!mod.QuestComplete("swordEarth"))
            {

                if (mod.QuestGiven("swordEarth"))
                {
                    effigyReply = "Forgotten Effigy: ^Go to the forest. Find the giant malus. That is all for now.";

                }
                else
                {

                    effigyReply = "Forgotten Effigy: " +
                    "^Seek the patronage of the two Kings. Find the giant malus of the southern forest and perform a rite below it's boughs." +
                    "..."+
                    $"^({mod.CastControl()} with a melee weapon or scythe in hand to perform a rite. Hold the button to increase the range of the effect.)";

                    mod.NewQuest("swordEarth");

                    Game1.currentLocation.playSoundPitched("discoverMineral", 600);

                }

            }
            else if (!mod.QuestComplete("challengeEarth"))
            {


                if (!blessingList.ContainsKey("earth"))
                {

                    mod.UpdateBlessing("earth");

                    EarthDecoration();

                }

                switch (blessingList["earth"])
                {

                    case 0: // explode weeds

                        effigyReply = "Forgotten Effigy: ^Good. You are now a subject of the two kingdoms, and bear authority over the weed and the twig. " +
                            "Use this new power to drive out decay and detritus. Return tomorrow for another lesson." +
                            "^..." +
                            $"^({mod.CastControl()}: explode weeds and twigs. Remotely greet animals, pets and villagers once a day. Hold the button to increase the range of the effect.)";

                        break;

                    case 1: // bush, water, grass, stump, boulder

                        effigyReply = "Forgotten Effigy: ^The valley springs into new life. Go now, sample its hidden bounty, and prepare to face those who guard its secrets." +
                            "^..." +
                            $"^({mod.CastControl()}: extract foragables from large bushes, wood from trees, fibre and seeds from grass and small fish from water. Might spawn monsters.)";

                        break;

                    case 2: // lawn, dirt, trees

                        effigyReply = "Forgotten Effigy: ^Years of stagnation have starved the valley of it's wilderness. Go now, and recolour the barren spaces." +
                            "^..." +
                            $"^({mod.CastControl()}: sprout trees, grass, seasonal forage and flowers in empty spaces.)";

                        break;

                    case 3: // hoed

                        effigyReply = "Forgotten Effigy: ^Your connection to the earth deepens. You may channel the power of the Two Kings for your own purposes." +
                            "^..." +
                            $"^({mod.CastControl()}: increase the growth rate and quality of growing crops. Convert planted wild seeds into random cultivations. Fertilise trees and uptick the growth rate of fruittrees.)";

                        break;

                    case 4: // rockfall

                        effigyReply = "Forgotten Effigy: ^Be careful in the mines. The deep earth answers your call, both above and below you." +
                            "^..." +
                            $"^({mod.CastControl()}: shake loose rocks free from the ceilings of mine shafts. Explode gem ores.)";

                        break;

                    default: // quest

                        if (mod.QuestGiven("challengeEarth"))
                        {
                            effigyReply = "Forgotten Effigy: Stop dallying. Return when the mountain is cleansed.";
                        }
                        else
                        {
                            effigyReply = "Forgotten Effigy: ^A trial presents itself. Foulness seeps from the mountain springs. Cleanse the source with the King's blessing."+
                            "^..." +
                            $"^(You have received a new quest.)";

                            mod.NewQuest("challengeEarth");

                        }

                        break;

                }

                if (blessingList["earth"] <= 4)
                {

                    lessonGiven = true;

                    mod.LevelBlessing("earth");

                    Game1.currentLocation.playSoundPitched("discoverMineral", 600);

                }

            }
            else if (!mod.QuestComplete("swordWater"))
            {

                if (mod.QuestGiven("swordWater"))
                {
                    effigyReply = "Forgotten Effigy: ^Seek the furthest pier.";

                }
                else
                {
                    effigyReply = "Forgotten Effigy: ^The Voice Beyond the Shore harkens to you now. ^Perform a rite at the furthest pier, and behold her power." +
                    "^..." +
                    $"^({mod.CastControl()} with a weapon or scythe in hand to perform a rite. Hold the button to increase the range of the effect.)";

                    mod.NewQuest("swordWater");

                    Game1.currentLocation.playSoundPitched("thunder_small", 1200);

                }

            }
            else if (!mod.QuestComplete("challengeWater"))
            {

                if (!blessingList.ContainsKey("water"))
                {

                    mod.UpdateBlessing("water");

                    WaterDecoration();

                }

                switch (blessingList["water"])
                {

                    case 0: // warp totems

                        effigyReply = "Forgotten Effigy: ^Good. The Lady Beyond the Shore has answered your call. Find the shrines to the patrons of the Valley, and strike them to draw out a portion of their essence. Do the same to any obstacle in your way." +
                        "^..." +
                        $"^({mod.CastControl()}: strike warp shrines once a day to extract totems, and boulders and stumps to extract resources)";

                        //mod.LevelBlessing("special");

                        break;

                    case 1: // scarecrow, rod, craftable, campfire

                        effigyReply = "Forgotten Effigy: ^The Lady is fascinated by the industriousness of humanity. Combine your artifice with her blessing and reap the rewards." +
                            "^..." +
                            $"^({mod.CastControl()}: strike scarecrows, campfires and lightning rods to activate special functions. Villager firepits will work too.)";

                        break;

                    case 2: // fishspot

                        effigyReply = "Forgotten Effigy: ^The denizens of the deep water serve the Lady. Go now, and test your skill against them." +
                            "^..." +
                            $"^({mod.CastControl()}: strike deep water to produce a fishing-spot that yields rare species of fish)";

                        break;

                    case 3: // stump, boulder, enemy

                        effigyReply = "Forgotten Effigy: ^Your connection to the plane beyond broadens. ^Call upon the Lady's Voice to destroy your foes." +
                            "^..." +
                            $"^({mod.CastControl()}: expend high amounts of stamina to instantly destroy enemies)";

                        break;

                    case 4: // portal

                        effigyReply = "Forgotten Effigy: ^Are you yet a master of the veil between worlds? Focus your will to breach the divide." +
                            "^..." +
                            $"^({mod.CastControl()}: strike candle torches to create monster portals. Only works in remote outdoor locations.)";

                        break;

                    default: // quest

                        if (mod.QuestGiven("challengeWater"))
                        {
                            effigyReply = "Forgotten Effigy: ^Deal with the shadows first.";

                        }
                        else
                        {

                            effigyReply = "Forgotten Effigy: ^A new trial presents itself. Creatures of shadow linger in the hollowed grounds of the village. Smite them with the Lady's blessing." +
                            "^..." +
                            $"^(You have received a new quest.)";

                            mod.NewQuest("challengeWater");

                        }

                        break;


                }

                if (blessingList["water"] <= 4)
                {

                    lessonGiven = true;

                    mod.LevelBlessing("water");
                    
                    Game1.currentLocation.playSoundPitched("thunder_small", 1200);

                }

            }
            else if (!mod.QuestComplete("swordStars"))
            {

                if (mod.QuestGiven("swordStars"))
                {
                    effigyReply = "Forgotten Effigy: ^Find the lake of flames.";

                }
                else
                {

                    effigyReply = "Forgotten Effigy: ^Your name is known within the celestial plane. ^Travel to the lake of flames. Retrieve the final vestige of the first farmer.";

                    mod.NewQuest("swordStars");

                    Game1.currentLocation.playSoundPitched("Meteorite", 1200);

                }

            }
            else if (!mod.QuestComplete("challengeStars"))
            {


                if (!blessingList.ContainsKey("stars"))
                {

                    mod.UpdateBlessing("stars");

                    StarsDecoration();

                }

                if (mod.QuestGiven("challengeStars"))
                {
                    effigyReply = "Forgotten Effigy: ^Only you can deal with the threat to the forest";

                }
                else
                {
                    effigyReply = "Forgotten Effigy: ^Your last trial awaits. The southern forest reeks of our mortal enemy. Rain judgement upon the slime with the blessing of the Stars." +
                            "^..." +
                            $"^(You have received a new quest.)";

                    mod.NewQuest("challengeStars");

                    mod.LevelBlessing("stars");

                    //ModUtility.AnimateMeteor(Game1.player.currentLocation, Game1.player.getTileLocation() + new Vector2(0, -2), true);

                    Game1.currentLocation.playSoundPitched("Meteorite", 1200);

                }

            }
            else
            {
                
                effigyReply = "Satisfied Effigy: ^Your power rivals that of the first farmer. I have nothing further to teach you. When the seasons change, the valley may call upon your aid once again."+
                "^..." +
                "^(Thank you for playing with StardewDruid." +
                "^Credits: Neosinf/StardewDruid, PathosChild/SMAPI, ConcernedApe/StardewValley)";

                Game1.currentLocation.playSound("yoba");

            }

            if (effigyReply.Length > 0)
            {

                Game1.activeClickableMenu = new DialogueBox(effigyReply);

            }

        }

        public void DialogueAncestor()
        {
            string effigyReply = "Satisfied Effigy: ^Indeed, I hear the wisdom of the old valley in each word spoken. I am blessed to serve you, and know, there is much to be done." +
            "^..." +
            "^( All rites unlocked at maximum level )";

            Game1.currentLocation.playSound("yoba");

            mod.UnlockAll();

            Game1.activeClickableMenu = new DialogueBox(effigyReply);

        }

        public void DialogueBlessing()
        {

            string effigyQuestion = "Forgotten Effigy: ^The Kings, the Lady, the Stars, I may entreat them all.";

            List<Response> effigyChoices = new();

            if (mod.ActiveBlessing() != "earth")
            {

                effigyChoices.Add(new Response("earth", "Seek the Two Kings for me"));

            }

            if (mod.BlessingList().ContainsKey("water") && mod.ActiveBlessing() != "water")
            {

                effigyChoices.Add(new Response("water", "Call out to the Lady Beyond The Shore"));

            }

            if (mod.BlessingList().ContainsKey("stars") && mod.ActiveBlessing() != "stars")
            {

                effigyChoices.Add(new Response("stars", "Look to the Stars for me"));

            }

            if (mod.ActiveBlessing() != "none")
            {

                effigyChoices.Add(new Response("none", "I don't want anyone's favour (disables all effects)"));

            }

            effigyChoices.Add(new Response("cancel", "(say nothing)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerBlessing);

            returnFrom = null;

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

            return;

        }

        public void AnswerBlessing(Farmer effigyVisitor, string effigyAnswer)
        {

            string effigyReply;

            switch (effigyAnswer)
            {
                case "earth":
                    
                    Game1.addHUDMessage(new HUDMessage($"{mod.CastControl()} to perform rite of the earth", ""));

                    effigyReply = "Forgotten Effigy: ^The Kings of Oak and Holly come again.";

                    Game1.currentLocation.playSound("discoverMineral");

                    EarthDecoration();

                    break;

                case "water":

                    Game1.addHUDMessage(new HUDMessage($"{mod.CastControl()} to perform rite of the water", ""));

                    effigyReply = "Forgotten Effigy: ^The Voice Beyond the Shore echoes around us.";

                    Game1.currentLocation.playSound("thunder_small");

                    WaterDecoration();

                    break;

                case "stars":

                    Game1.addHUDMessage(new HUDMessage($"{mod.CastControl()} to perform rite of the stars", ""));

                    effigyReply = "Forgotten Effigy: ^Life to ashes. Ashes to dust.";

                    Game1.currentLocation.playSound("Meteorite");

                    StarsDecoration();

                    break;

                case "none":

                    Game1.addHUDMessage(new HUDMessage($"{mod.CastControl()} will do nothing", ""));

                    effigyReply = "Forgotten Effigy: ^The light fades away.";

                    Game1.currentLocation.playSound("ghost");

                    NoDecoration();

                    break;

                default: // "cancel"

                    effigyReply = "Forgotten Effigy: ^(says nothing back).";

                    break;

            }

            if(effigyAnswer != "cancel")
            {

                mod.UpdateBlessing(effigyAnswer);

            }

            Game1.activeClickableMenu = new DialogueBox(effigyReply);

        }

        public void DialogueEffects()
        {

            Dictionary<string, int> blessingList = mod.BlessingList();

            string effigyQuestion = "Forgotten Effigy: ^Our traditions are etched into the bedrock of the valley.";

            if (returnFrom == "forget")
            {

                effigyQuestion = "Forgotten Effigy: ... you've forgotten what you wanted to forget, haven't you.";

            }

            if (returnFrom == "relearn")
            {

                effigyQuestion = "Forgotten Effigy: ... so you can't remember what you wanted to remember.";

            }

            List<Response> effigyChoices = new();

            effigyChoices.Add(new Response("earth", "What role do the Two Kings play?"));

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
                effigyChoices.Add(new Response("disable", "I'd rather forget something that happened (disable effects)"));

                effigyChoices.Add(new Response("enable", "I want to relearn something (enable effects)"));

            }

            effigyChoices.Add(new Response("return", "(nevermind)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerEffects);

            returnFrom = null;

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

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

            Dictionary<string, int> blessingList = mod.BlessingList();

            string effigyQuestion = "Forgotten Effigy: ^The King of Oaks and the King of Holly war upon the Equinox. One will rule with winter, one with summer.";

            if (blessingList["earth"] >= 1)
            {

                effigyQuestion += "^Lesson 1. Explode weeds and twigs. Greet Villagers, Pets and Animals once a day.";

            }

            if (blessingList["earth"] >= 2)
            {

                effigyQuestion += "^Lesson 2. Extract foragables from the landscape. Might attract monsters.^ ";
            
            }

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

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

            return;

        }

        public void EffectsEarthTwo(Farmer effigyVisitor, string effigyAnswer)
        {

            Dictionary<string, int> blessingList = mod.BlessingList();

            string effigyQuestion = "Forgotten Effigy: ^Lesson 3. Sprout trees, grass, seasonal forage and flowers in empty spaces.";

            if (blessingList["earth"] >= 4)
            {

                effigyQuestion += "^Lesson 4. Increase the growth rate and quality of growing crops. Convert planted wild seeds into random cultivations.";

            }

            if (blessingList["earth"] >= 5)
            {

                effigyQuestion += "^Lesson 5. Shake loose rocks free from the ceilings of mine shafts. Explode gem ores.^ ";

            }

            List<Response> effigyChoices = new();

            effigyChoices.Add(new Response("return", "It's all clear now"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(ReturnEffects);

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

            return;

        }

        public void EffectsWater()
        {

            Dictionary<string, int> blessingList = mod.BlessingList();

            string effigyQuestion = "Forgotten Effigy: ^The Voice is that of the Lady of the Isle of Mists. She is as ancient and powerful as the sunset on the Gem Sea.";

            if (blessingList["water"] >= 1)
            {

                effigyQuestion += "^Lesson 1. Strike warp shrines, stumps, logs and boulders to extract resources.";

            }

            if (blessingList["water"] >= 2)
            {

                effigyQuestion += "^Lesson 2. Strike scarecrows, campfires and lightning rods to activate special functions.^ ";

            }

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

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

            return;

        }

        public void EffectsWaterTwo(Farmer effigyVisitor, string effigyAnswer)
        {

            Dictionary<string, int> blessingList = mod.BlessingList();

            string effigyQuestion = "Forgotten Effigy: ^Lesson 3. Strike deep water to produce a fishing-spot that yields rare species of fish.";


            if (blessingList["water"] >= 4)
            {

                effigyQuestion += "^Lesson 4. Expend high amounts of stamina to instantly destroy enemies.";

            }

            if (blessingList["water"] >= 5)
            {

                effigyQuestion += "^Lesson 5. Strike candle torches placed in remote outdoor locations to produce monster portals.^ ";

            }

            List<Response> effigyChoices = new();

            effigyChoices.Add(new Response("return", "It's all clear now"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(ReturnEffects);

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

            return;

        }

        public void EffectsStars()
        {

            Dictionary<string, int> blessingList = mod.BlessingList();

            string effigyQuestion = "Forgotten Effigy: ^The Stars have no names that can be uttered by earthly dwellers. They exist high above, and beyond, and care not for the life of our world, though their light sustains much of it. Yet... there is one star... a fallen star. He has a name. A name that we dread to speak.";

            List<Response> effigyChoices = new();

            effigyChoices.Add(new Response("return", "It's all clear now"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(ReturnEffects);

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

            return;

        }

        public void ReturnEffects(Farmer effigyVisitor, string effigyAnswer)
        {

            DelayedAction.functionAfterDelay(DialogueEffects, 100);

            return;

        }

        public void DialogueDisable()
        {

            Dictionary<string, int> blessingList = mod.BlessingList();

            string effigyQuestion = "Forgotten Effigy: ^Is there a lesson you'd rather forget.";

            List<Response> effigyChoices = new();

            if (!blessingList.ContainsKey("forgetSeeds") && blessingList["earth"] >= 3)
            {

                effigyChoices.Add(new Response("forgetSeeds", "I end up with seeds in my boots everytime I run through the meadow. IT'S ANNOYING."));

            }

            if (!blessingList.ContainsKey("forgetFish"))
            {

                effigyChoices.Add(new Response("forgetFish", "I got slapped in the face by a flying fish today."));

            }

            if (!blessingList.ContainsKey("forgetCritters"))
            {

                effigyChoices.Add(new Response("forgetCritters", "Why does a bat sleep in every damn tree on this farm. Can't they live in this cave instead?"));

            }

            if (!blessingList.ContainsKey("forgetTrees") && blessingList["earth"] >= 3)
            {

                effigyChoices.Add(new Response("forgetTrees", "Just about inside Clint's by 3:50pm when a tree sprouted in front of me. Now my crotch is sore AND I don't have a Copper Axe."));

            }

            effigyChoices.Add(new Response("return", "(nevermind)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerDisable);

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

            return;

        }

        public void AnswerDisable(Farmer effigyVisitor, string effigyAnswer)
        {

            string effigyReply = "Forgotten Effigy: ^The druid's life is... full of random surprises... but may you not suffer any more of this kind.";

            switch (effigyAnswer)
            {
                case "return":

                    returnFrom = "forget";

                    DelayedAction.functionAfterDelay(DialogueEffects, 100);

                    return;

                default: //

                    mod.ToggleEffect(effigyAnswer);

                    break;

            }

            Game1.activeClickableMenu = new DialogueBox(effigyReply);

        }

        public void DialogueEnable()
        {

            Dictionary<string, int> blessingList = mod.BlessingList();

            string effigyQuestion = "Forgotten Effigy: ^The mind is open.";

            List<Response> effigyChoices = new();

            if (blessingList.ContainsKey("forgetSeeds"))
            {

                effigyChoices.Add(new Response("forgetSeeds", "There's a time to reap and a time to sow. I want to reap seeds from wild grass, sell them, and buy a Sow."));

            }

            if (blessingList.ContainsKey("forgetFish"))
            {

                effigyChoices.Add(new Response("forgetFish", "I miss the way the fish dance to the rhythm of the rite"));

            }

            if (blessingList.ContainsKey("forgetCritters"))
            {

                effigyChoices.Add(new Response("forgetCritters", "I miss the feeling of being watched from every bush."));

            }

            if (blessingList.ContainsKey("forgetTrees"))
            {

                effigyChoices.Add(new Response("forgetTrees", "Stuff Clint. I want to impress Emily with the magic sprout trick."));

            }

            effigyChoices.Add(new Response("return", "(nevermind)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerEnable);

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

            return;

        }

        public void AnswerEnable(Farmer effigyVisitor, string effigyAnswer)
        {

            string effigyReply = "Forgotten Effigy: ^Let the essence of life itself enrich your world.";

            switch (effigyAnswer)
            {
                case "return":

                    returnFrom = "relearn";

                    DelayedAction.functionAfterDelay(DialogueEffects, 100);

                    return;

                default: //

                    mod.ToggleEffect(effigyAnswer);

                    break;

            }

            Game1.activeClickableMenu = new DialogueBox(effigyReply);

        }

        public void DialogueDemetrius()
        {

            string effigyQuestion = "Forgotten Effigy: ^I concealed myself for a time, then I spoke to him in the old tongue of the Calico shamans.";

            List<Response> effigyChoices = new();

            effigyChoices.Add(new Response("descended", "Do you think Demetrius is descended from the shaman tradition?!"));

            effigyChoices.Add(new Response("offended", "Wow, he must have been offended. Demetrius is a man of modern science and sensibilities."));

            effigyChoices.Add(new Response("return", "Nope, not going to engage with this."));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerDemetrius);

            Game1.player.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

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

                    if(caveChoice == 1)
                    {

                        effigyReply = "Forgotten Effigy: ^... ^... ^He came in with a feathered mask on, invoked a rite of summoning, threw Bat feed everywhere, then ran off singing \"Old man in a frog pond\".";

                    }
                    else
                    {

                        effigyReply = "Forgotten Effigy: ^I can smell the crisp, sandy scent of the Calico variety of mushroom. The shamans would eat them to... enter a trance-like state.";

                    }

                    break;

            }

            Game1.activeClickableMenu = new DialogueBox(effigyReply);

        }

    }

}
