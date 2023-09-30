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

        private Farmer targetPlayer;

        public bool lessonGiven;

        public string questCompleted;

        public Effigy(Mod Mod)
        {

            mod = Mod;

            lessonGiven = false;

        }

        public void ModifyCave()
        {

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            Layer backLayer = farmCave.map.GetLayer("Back");

            Layer buildingsLayer = farmCave.map.GetLayer("Buildings");

            //------------------------ rearrange existing tiles

            backLayer.Tiles[6, 1] = buildingsLayer.Tiles[6, 1];

            backLayer.Tiles[5, 2] = buildingsLayer.Tiles[5, 2];

            backLayer.Tiles[6, 2] = buildingsLayer.Tiles[6, 3];

            backLayer.Tiles[7, 2] = buildingsLayer.Tiles[7, 2];

            buildingsLayer.Tiles[5, 3] = buildingsLayer.Tiles[3, 4];

            buildingsLayer.Tiles[7, 3] = buildingsLayer.Tiles[9, 4];

            //------------------------ add effigy

            string witchTiles = Path.Combine("Maps", "WitchHutTiles");

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Content", witchTiles + ".xnb")))
            {

                TileSheet witchSheet = new("witchHutTiles", farmCave.map, witchTiles, new xTile.Dimensions.Size(8, 16), new xTile.Dimensions.Size(1, 1));

                farmCave.map.AddTileSheet(witchSheet);

                buildingsLayer.Tiles[6, 1] = new StaticTile(buildingsLayer, witchSheet, BlendMode.Alpha, 108);

                buildingsLayer.Tiles[5, 2] = new StaticTile(buildingsLayer, witchSheet, BlendMode.Alpha, 115);

                StaticTile effigyBody = new(buildingsLayer, witchSheet, BlendMode.Alpha, 116);

                effigyBody.TileIndexProperties.Add("Action", "FarmCaveDruidEffigy");

                buildingsLayer.Tiles[6, 2] = effigyBody;

                buildingsLayer.Tiles[7, 2] = new StaticTile(buildingsLayer, witchSheet, BlendMode.Alpha, 117);

                StaticTile effigyFeet = new(buildingsLayer, witchSheet, BlendMode.Alpha, 124);

                effigyFeet.TileIndexProperties.Add("Action", "FarmCaveDruidEffigy");

                buildingsLayer.Tiles[6, 3] = effigyFeet;

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

        private static void NoDecoration()
        {

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            TileSheet craftableSheet = farmCave.map.GetTileSheet("craftableTiles");

            if (craftableSheet != null)
            {

                Layer frontLayer = farmCave.map.GetLayer("Front");


                frontLayer.Tiles[5, 0] = null;

                frontLayer.Tiles[5, 1] = null;

                frontLayer.Tiles[7, 0] = null;

                frontLayer.Tiles[7, 1] = null;

            }

            return;

        }

        private static void EarthDecoration()
        {

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            TileSheet craftableSheet = farmCave.map.GetTileSheet("craftableTiles");

            if (craftableSheet != null)
            {

                Layer frontLayer = farmCave.map.GetLayer("Front");

                frontLayer.Tiles[5, 0] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 372);

                frontLayer.Tiles[5, 1] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 380);

                frontLayer.Tiles[7, 0] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 372);

                frontLayer.Tiles[7, 1] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 380);

            }

            return;

        }

        private static void WaterDecoration()
        {

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            TileSheet craftableSheet = farmCave.map.GetTileSheet("craftableTiles");

            if (craftableSheet != null)
            {

                Layer frontLayer = farmCave.map.GetLayer("Front");

                frontLayer.Tiles[5, 0] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 373);

                frontLayer.Tiles[5, 1] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 381);

                frontLayer.Tiles[7, 0] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 373);

                frontLayer.Tiles[7, 1] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 381);

            }

            return;

        }

        private static void StarsDecoration()
        {

            GameLocation farmCave = Game1.getLocationFromName("FarmCave");

            TileSheet craftableSheet = farmCave.map.GetTileSheet("craftableTiles");

            if (craftableSheet != null)
            {

                Layer frontLayer = farmCave.map.GetLayer("Front");

                frontLayer.Tiles[5, 0] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 374);

                frontLayer.Tiles[5, 1] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 382);

                frontLayer.Tiles[7, 0] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 374);

                frontLayer.Tiles[7, 1] = new StaticTile(frontLayer, craftableSheet, BlendMode.Alpha, 382);

            }

            return;

        }

        public void Approach(Farmer player)
        {

            targetPlayer = player;

            string effigyQuestion;

            List<Response> effigyChoices = new();

            Dictionary<string, int> blessingList = mod.BlessingList();

            if (!mod.QuestComplete("approachEffigy"))
            {
                
                effigyQuestion = "Forgotten Effigy: ^Ah... the successor appears.";

                effigyChoices.Add(new Response("query", "What are you?"));

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
            else
            {

                effigyQuestion = "Forgotten Effigy: ^Successor.";

                effigyChoices.Add(new Response("journey", "I've come for a lesson"));

            }

            if (blessingList.ContainsKey("water"))
            {

                effigyChoices.Add(new Response("blessing", "I seek another's favour"));

            }

            effigyChoices.Add(new Response("none", "(say nothing)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerApproach);

            targetPlayer.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

            return;

        }

        public void AnswerApproach(Farmer effigyVisitor, string effigyAnswer)
        {

            switch (effigyAnswer)
            {
                case "blessing":

                    DelayedAction.functionAfterDelay(DialogueBlessing, 100);

                    break;

                case "journey":

                    DelayedAction.functionAfterDelay(DialogueJourney, 100);

                    break;

                case "query":

                    DelayedAction.functionAfterDelay(DialogueQuery, 100);

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

            effigyChoices.Add(new Response("none", "(say nothing)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(AnswerApproach);

            targetPlayer.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

        }


        public void DialogueJourney()
        {

            Dictionary<string, int> blessingList = mod.BlessingList();

            string effigyReply;

            string hudPrompt = "";

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
                    $"^({mod.CastControl()} to perform a rite)";

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
                            $"^({mod.CastControl()}: explode weeds and twigs)";

                        break;

                    case 1: // bush, water, grass, stump, boulder

                        effigyReply = "Forgotten Effigy: ^The valley springs into new life. Go now, sample its hidden bounty, and prepare to face those who guard its secrets." +
                            "^..." +
                            $"^({mod.CastControl()}: extract foragables from large bushes, wood from trees, fibre and seeds from grass and small fish from water. Might spawn monsters.)";

                        break;

                    case 2: // lawn, dirt, trees

                        effigyReply = "Forgotten Effigy: ^Years of stagnation have starved the valley of it's wilderness. Go now, and recolour the barren spaces." +
                            "^..." +
                            $"^({mod.CastControl()}: sprout trees, grass, seasonal forage and flowers in empty spaces)";

                        break;

                    case 3: // hoed

                        effigyReply = "Forgotten Effigy: ^Your connection to the earth deepens. You may channel the power of the Two Kings for your own purposes." +
                            "^..." +
                            $"^({mod.CastControl()}: increase the growth rate and quality of growing crops. Convert planted wild seeds into random cultivations. Stamina cost increases for each stage of growth.)";

                        break;

                    case 4: // rockfall

                        effigyReply = "Forgotten Effigy: ^Be careful in the mines. The deep earth answers your call, both above and below you." +
                            "^..." +
                            $"^({mod.CastControl()}: shake loose rocks free from the ceilings of mine shafts. Explode gem ores." +
                            $")";

                        break;

                    default: // quest

                        if (mod.QuestGiven("challengeEarth"))
                        {
                            effigyReply = "Forgotten Effigy: Stop dallying. Return when the mountain is cleansed.";

                        }
                        else
                        {
                            effigyReply = "Forgotten Effigy: ^A trial presents itself. Foulness seeps from the mountain springs. Cleanse the source with the King's blessing.";

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
                    $"^({mod.CastControl()} with a weapon or scythe in hand to perform a rite)";

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
                            $"^({mod.CastControl()}:strike scarecrows, campfires and lightning rods to activate special functions.)";

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

                            effigyReply = "Forgotten Effigy: ^A new trial presents itself. Creatures of shadow linger in the hollowed grounds of the village. Smite them with the Lady's blessing.";

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
                    effigyReply = "Forgotten Effigy: ^Your last trial awaits. The southern forest reeks of our mortal enemy. Rain judgement upon the slime with the blessing of the Stars.";

                    mod.NewQuest("challengeStars");

                    mod.LevelBlessing("stars");

                    ModUtility.AnimateMeteor(Game1.player.currentLocation, Game1.player.getTileLocation() + new Vector2(0, -2), true);

                    Game1.currentLocation.playSoundPitched("Meteorite", 1200);

                }

            }
            else
            {
                
                effigyReply = "Satisfied Effigy: ^Your power rivals that of the first farmer. I have nothing further to teach you. When the seasons change, the valley may call upon your aid once again."+
                "^..." +
                "^(Thank you for playing with StardewDruid." +
                "^Credits: Neosinf/StardewDruid, PathosChild/SMAPI, ConcernedApe/StardewValley)";

                Game1.currentLocation.playSound("Yoba");

            }

            if (hudPrompt.Length > 0)
            {
                Game1.addHUDMessage(new HUDMessage(hudPrompt, ""));

            }

            if (effigyReply.Length > 0)
            {

                Game1.activeClickableMenu = new DialogueBox(effigyReply);

            }

        }

        public void DialogueBlessing()
        {

            string effigyQuestion = "Forgotten Effigy: ^The Kings, the Lady, the Stars, I may entreat them all.";

            List<Response> effigyChoices = new();

            if (mod.ActiveBlessing() != "earth")
            {

                effigyChoices.Add(new Response("earth", "Seek the Two Kings"));

            }

            if (mod.BlessingList().ContainsKey("water") && mod.ActiveBlessing() != "water")
            {

                effigyChoices.Add(new Response("water", "Call out to the Lady Beyond The Shore"));

            }

            if (mod.BlessingList().ContainsKey("stars") && mod.ActiveBlessing() != "stars")
            {

                effigyChoices.Add(new Response("stars", "Look to the Stars"));

            }

            effigyChoices.Add(new Response("none", "(say nothing)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(DialogueBlessingAnswer);

            targetPlayer.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

            return;

        }

        public void DialogueBlessingAnswer(Farmer effigyVisitor, string effigyAnswer)
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

                default: // "none"

                    effigyReply = "Forgotten Effigy: ^The light fades away.";

                    NoDecoration();

                    break;

            }

            mod.UpdateBlessing(effigyAnswer);

            Game1.activeClickableMenu = new DialogueBox(effigyReply);

        }

    }

}
