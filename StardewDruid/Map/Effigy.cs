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

                    default: // case "swordEarth":

                        effigyChoices.Add(new Response("journey", "The tree gave me a branch shaped like a sword."));

                        break;

                }

            }
            else
            {

                effigyQuestion = "Forgotten Effigy: ^Successor.";

                effigyChoices.Add(new Response("journey", "I wish to continue my journey"));

            }

            if (blessingList.ContainsKey("water"))
            {

                effigyChoices.Add(new Response("blessing", "I seek another's favour"));

            }

            effigyChoices.Add(new Response("none", "(say nothing)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(DialogueApproach);

            targetPlayer.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

            return;

        }

        public void DialogueQuery()
        {

            mod.UpdateQuest("approachEffigy", true);

            List<Response> effigyChoices = new();

            string effigyQuestion = "Forgotten Effigy: ^I was crafted by the first farmer. ^If you intend to succeed him, your journey begins with me.";

            effigyChoices.Add(new Response("journey", "Where will this journey take me?"));

            effigyChoices.Add(new Response("none", "(say nothing)"));

            GameLocation.afterQuestionBehavior effigyBehaviour = new(DialogueApproach);

            targetPlayer.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices.ToArray(), effigyBehaviour);

        }

        public void DialogueApproach(Farmer effigyVisitor, string effigyAnswer)
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


        public void DialogueJourney()
        {

            Dictionary<string, int> blessingsReceived = mod.BlessingList();

            string effigyReply = "";

            string hudPrompt = "";

            if (lessonGiven)
            {
                effigyReply = "Forgotten Effigy: ^Hmm... return tomorrow after I have consulted the Others.";

            }
            else if (!mod.QuestComplete("swordEarth"))
            {
                effigyReply = "Forgotten Effigy: " +
                    "^Well.. your first adventure... will be to honor the Two Kings." +
                    $"^The first farmer would perform a rite with his hands raised. He called it the {mod.CastControl()} move." +
                    "^Seek out the giant malus of the southern forest." +
                    "^Perform the rite below the tree's boughs, and return hence with what you receive there.";

                mod.UpdateBlessing("earth");

                EarthDecoration();

                mod.NewQuest("swordEarth");

                //hudPrompt = $"{mod.CastControl()} with a weapon or scythe in hand to perform rite of the earth";

            }
            else if (!mod.QuestComplete("challengeEarth"))
            {

                switch (blessingsReceived["earth"])
                {

                    case 1: // bush, water, grass, stump, boulder

                        effigyReply = "Forgotten Effigy: ^Go now, sample the valley's hidden bounty. ^Remember to return tomorrow for another lesson.";

                        hudPrompt = $"{mod.CastControl()} extract foragables from large bushes, trees, grass and bodies of water";

                        break;

                    case 2: // lawn, dirt, trees

                        effigyReply = "Forgotten Effigy: ^Years of stagnation have starved the valley is starved of it's wilderness. ^Go now, and recolour the barren spaces.";

                        hudPrompt = $"{mod.CastControl()} sprout trees, seasonal forage and flowers in empty spaces";

                        break;

                    case 3: // hoed

                        effigyReply = "Forgotten Effigy: ^Your connection to the earth deepens. You may channel the power of the Two Kings for your own purposes.";

                        hudPrompt = $"{mod.CastControl()} cultivate new crops in tilled soil and fertilise budding growth.";

                        break;

                    case 4: // rockfall

                        effigyReply = "Forgotten Effigy: ^Be careful in the mines. The deep earth answers your call, both above and below you.";

                        hudPrompt = $"{mod.CastControl()} shake loose rocks free from the ceilings of mine shafts";

                        break;

                    case 5:

                        effigyReply = "Forgotten Effigy: ^A trial presents itself. Foulness seeps from the mountain. ^Cleanse it with the King's blessing.";

                        mod.NewQuest("challengeEarth");

                        break;

                    default: // explode weeds

                        effigyReply = "Forgotten Effigy: ^Good. You are now a subject of the two kingdoms, and bear authority over the weed and the twig. ^Use this new power to drive out decay and detritus. ^Then return tomorrow for another lesson.";

                        hudPrompt = $"{mod.CastControl()} explode weeds and twigs";

                        break;

                }

                mod.LevelBlessing("earth");

                ModUtility.AnimateEarth(Game1.player.currentLocation, Game1.player.getTileLocation() + new Vector2(0, -2));

                lessonGiven = true;

            }
            else if (!mod.QuestComplete("swordWater"))
            {
                effigyReply = "Forgotten Effigy: ^The Voice Beyond the Shore harkens to you now. ^Perform this rite from the furthest pier, and return hence with her offering.";

                mod.UpdateBlessing("water");

                WaterDecoration();

                mod.NewQuest("swordWater");

                hudPrompt = $"{mod.CastControl()} with a weapon or scythe in hand to perform rite of the water";

            }
            else if (!mod.QuestComplete("challengeWater"))
            {

                switch (blessingsReceived["water"])
                {

                    case 1: // scarecrow, rod, craftable

                        effigyReply = "Forgotten Effigy: ^The Lady is fascinated by the industriousness of humanity. ^Combine your artifice with her blessing.";

                        hudPrompt = $"{mod.CastControl()} strike scarecrows, rods and other farm equipment to expedite function";

                        break;

                    case 2: // fishspot

                        effigyReply = "Forgotten Effigy: ^The denizens of the deep water serve the Lady. ^Go now, and test your skill against them.";

                        hudPrompt = $"{mod.CastControl()} strike deep water to produce a fishing-spot that yields rare species of fish";

                        break;

                    case 3: // stump, boulder, enemy

                        effigyReply = "Forgotten Effigy: ^Your connection to the plane beyond broadens. ^Invoke lightning to destroy your foes.";

                        hudPrompt = $"{mod.CastControl()} expend high amounts of stamina to instantly destroy enemies and obstacles";

                        break;

                    case 4: // portal, campfire

                        effigyReply = "Forgotten Effigy: ^Are you yet a master of the veil between worlds? ^Focus your will on a single point to breach the divide.";

                        hudPrompt = $"{mod.CastControl()} strike candle torches and campfires to bolster their energy";

                        break;

                    case 5:

                        effigyReply = "Forgotten Effigy: A new trial presents itself. Creatures of shadow linger in the hollowed grounds of the village. ^Smite them with the Lady's blessing.";

                        mod.NewQuest("challengeWater");

                        break;

                    default: // totem

                        effigyReply = "Forgotten Effigy: ^Find the shrines to the patrons of the Valley. ^Strike them with this blessing to draw an ounce of their power.";

                        hudPrompt = $"{mod.CastControl()} strike warp shrines to extract totems";

                        break;

                }

                mod.LevelBlessing("water");

                ModUtility.AnimateBolt(Game1.player.currentLocation, Game1.player.getTileLocation() + new Vector2(0, -2));

                lessonGiven = true;

            }
            else if (!mod.QuestComplete("swordStars"))
            {
                effigyReply = "Forgotten Effigy: ^Your name is known within the celestial plane. ^Return molten fire to the lake of flames and return forthwith its power.";

                mod.UpdateBlessing("stars");

                StarsDecoration();

                mod.NewQuest("swordStars");

                hudPrompt = $"{mod.CastControl()} with a weapon or scythe in hand to perform rite of the stars";

            }
            else if (!mod.QuestComplete("challengeStars"))
            {
                
                effigyReply = "Forgotten Effigy: Your last trial awaits. The southern forest reeks of the first farmer's mortal enemy. ^Rain judgement upon the slime with the blessing of the Stars.";

                mod.NewQuest("challengeStars");

                mod.LevelBlessing("stars");

                ModUtility.AnimateMeteor(Game1.player.currentLocation, Game1.player.getTileLocation() + new Vector2(0, -2),true);

                lessonGiven = true;
                
            }
            else
            {
                
                effigyReply = "Satisfied Effigy: Your power rivals that of the first farmer. You have nothing further to prove. ^When the seasons change, the valley may call upon your aid again.";

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

            //string effigyChoice = mod.ActiveBlessing();

            //string correctAnswer = effigyAnswer;

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

            //if (correctAnswer != effigyChoice)
            //{
            //    mod.UpdateBlessing(correctAnswer);

            //}

            mod.UpdateBlessing(effigyAnswer);

            Game1.activeClickableMenu = new DialogueBox(effigyReply);

        }

        /*public void DialogueWeapon()
        {

            string effigyQuestion = "Forgotten Effigy: ^Some weapons lie beneath the surface of the material plane. ^The forest, the shore, the mountains... all hold such secrets.";

            Response[] effigyChoices = new Response[5]
            {
                 new Response("forest","Where in the forest?"),
                 new Response("tides","Where by the shore?"),
                 new Response("mountains","Where in the mountains?"),
                 new Response("normal","Anything powerful and stylish?"),
                 new Response("none", "(say nothing)")
            };

            GameLocation.afterQuestionBehavior effigyBehaviour = new(DialogueWeaponAnswer);

            targetPlayer.currentLocation.createQuestionDialogue(effigyQuestion, effigyChoices, effigyBehaviour);

            return;

        }

        public void DialogueWeaponAnswer(Farmer effigyVisitor, string effigyAnswer)
        {

            string effigyReply;

            switch (effigyAnswer)
            {
                case "forest":

                    effigyReply = "Forgotten Effigy: ";

                    break;

                case "tides":

                    effigyReply = "Forgotten Effigy: ^Strike with storm the swells south of the stars in the shallows.";

                    break;

                case "mountains":

                    effigyReply = "Forgotten Effigy: ^";

                    break;

                case "normal":

                    effigyReply = "Forgotten Effigy: ^You can channel through a material weapon... ^But know that only those blades forged in the otherworld possess innate power.";

                    break;

                default: //"none"

                    effigyReply = "...";

                    break;

            }

            Game1.activeClickableMenu = new DialogueBox(effigyReply);

            return;

        }

        public void DialogueTrial()
        {

            List<string> challengesCompleted = mod.ChallengesCompleted();

            List<string> challengesReceived = mod.ChallengesReceived();

            List<string> blessingsReceived = mod.BlessingsReceived();

            string effigyReply = "I will offer you a new trial when you are ready.";

            if (!challengesReceived.Contains("challengeEarth") && blessingsReceived.Contains("earth"))
            {

                mod.ReceiveChallenge("challengeEarth");

                effigyReply = "Foulness seeps from the mountain. ^Cleanse it with the King's blessing.";

            }

            if (challengesCompleted.Contains("challengeEarth") && !challengesReceived.Contains("challengeWater") && blessingsReceived.Contains("water"))
            {

                mod.ReceiveChallenge("challengeWater");

                effigyReply = "";

            }

            if (challengesCompleted.Contains("challengeWater") && !challengesReceived.Contains("challengeStars") && blessingsReceived.Contains("stars"))
            {

                mod.ReceiveChallenge("challengeStars");

                effigyReply = ;

            }

            if (challengesCompleted.Contains("challengeEarth") && challengesCompleted.Contains("challengeWater") && challengesCompleted.Contains("challengeStars"))
            {

               
            
            }


            Game1.activeClickableMenu = new DialogueBox(effigyReply);

        }*/

    }

}
