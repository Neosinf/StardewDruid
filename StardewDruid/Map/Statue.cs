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

namespace StardewDruid.Map
{
    internal class Statue
    {

        private readonly Mod mod;

        private Farmer targetPlayer;

        public Statue(Mod Mod)
        {

            mod = Mod;

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

            //------------------------ add statue

            string witchTiles = Path.Combine("Maps", "WitchHutTiles");

            if (File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "Content", witchTiles + ".xnb")))
            {

                TileSheet witchSheet = new("witchHutTiles", farmCave.map, witchTiles, new xTile.Dimensions.Size(8, 16), new xTile.Dimensions.Size(1, 1));

                farmCave.map.AddTileSheet(witchSheet);

                buildingsLayer.Tiles[6, 1] = new StaticTile(buildingsLayer, witchSheet, BlendMode.Alpha, 108);

                buildingsLayer.Tiles[5, 2] = new StaticTile(buildingsLayer, witchSheet, BlendMode.Alpha, 115);

                StaticTile statueBody = new(buildingsLayer, witchSheet, BlendMode.Alpha, 116);

                statueBody.TileIndexProperties.Add("Action", "FarmCaveDruidStatue");

                buildingsLayer.Tiles[6, 2] = statueBody;

                buildingsLayer.Tiles[7, 2] = new StaticTile(buildingsLayer, witchSheet, BlendMode.Alpha, 117);

                StaticTile statueFeet = new(buildingsLayer, witchSheet, BlendMode.Alpha, 124);

                statueFeet.TileIndexProperties.Add("Action", "FarmCaveDruidStatue");

                buildingsLayer.Tiles[6, 3] = statueFeet;

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

            switch (mod.StatueChoice())
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

            string statueQuestion = "Forgotten Effigy: ^What do you seek?";

            Response[] statueChoices = new Response[5]
            {
                 new Response("blessing","A blessing"),
                 new Response("weapon","A weapon"),
                 new Response("trial","A chance to prove myself"),
                 new Response("query","What are you?"),
                 new Response("none", "(say nothing)")
            };

            GameLocation.afterQuestionBehavior statueBehaviour = new(DialogueApproach);

            targetPlayer.currentLocation.createQuestionDialogue(statueQuestion, statueChoices, statueBehaviour);

            return;

        }

        public void DialogueApproach(Farmer statueVisitor, string statueAnswer)
        {

            switch (statueAnswer)
            {
                case "blessing":

                    DelayedAction.functionAfterDelay(DialogueBlessing, 100);

                    break;

                case "weapon":

                    DelayedAction.functionAfterDelay(DialogueWeapon, 100);

                    break;

                case "trial":

                    DelayedAction.functionAfterDelay(DialogueTrial, 100);

                    break;

                case "query":

                    DelayedAction.functionAfterDelay(DialogueQuery, 100);

                    break;

            }

            return;

        }

        public void DialogueBlessing()
        {

            string statueQuestion = "Forgotten Effigy: ^The Kings, the Lady, the Stars, I speak on behalf of them all. ^Who do you entreat for a blessing?";

            Response[] statueChoices = new Response[4]
            {
                 new Response("earth","The Two Kings"),
                 new Response("water","The Lady Beyond The Shore"),
                 new Response("stars","The Stars Themselves"),
                 new Response("none", "(say nothing)")
            };

            GameLocation.afterQuestionBehavior statueBehaviour = new(DialogueBlessingAnswer);

            targetPlayer.currentLocation.createQuestionDialogue(statueQuestion, statueChoices, statueBehaviour);

            return;

        }

        public void DialogueBlessingAnswer(Farmer statueVisitor, string statueAnswer)
        {

            string statueChoice = mod.StatueChoice();

            string correctAnswer = statueAnswer;

            string statueReply;

            switch (statueAnswer)
            {
                case "earth":

                    statueReply = "...";

                    if (statueAnswer != statueChoice)
                    {

                        Game1.addHUDMessage(new HUDMessage($"{mod.CastControl()} to perform rite of the earth", ""));

                        statueReply = "Forgotten Effigy: ^The Kings of Oak and Holly come again.";

                        Game1.currentLocation.playSound("discoverMineral");

                        EarthDecoration();

                    }

                    break;

                case "water":

                    statueReply = "...";

                    if (statueVisitor.fishingLevel.Value <= 4)
                    {

                        correctAnswer = statueChoice;

                        statueReply = "Forgotten Effigy: ^The Voice Beyond the Shore is silent. ^Return when you have mastery over the water.";

                        Game1.currentLocation.playSound("ghost");

                    }
                    else
                    {

                        if (statueAnswer != statueChoice)
                        {

                            Game1.addHUDMessage(new HUDMessage($"{mod.CastControl()} to perform rite of the water", ""));

                            statueReply = "Forgotten Effigy: ^The Voice Beyond the Shore echoes around us.";

                            Game1.currentLocation.playSound("thunder_small");

                            WaterDecoration();

                        }
                    }

                    break;

                case "stars":

                    statueReply = "...";

                    if (statueVisitor.combatLevel.Value <= 5)
                    {

                        correctAnswer = statueChoice;

                        statueReply = "Forgotten Effigy: ^Your name is not known in the celestial plane. ^Return when you have bested many foes.";

                        Game1.currentLocation.playSound("ghost");

                    }
                    else
                    {

                        if (statueAnswer != statueChoice)
                        {

                            Game1.addHUDMessage(new HUDMessage($"{mod.CastControl()} to perform rite of the stars", ""));

                            statueReply = "Forgotten Effigy: ^Life to ashes. Life to dust.";

                            Game1.currentLocation.playSound("Meteorite");

                            StarsDecoration();

                        }

                    }

                    break;

                default: // "none"

                    statueReply = "...";

                    if (statueAnswer != statueChoice)
                    {

                        statueReply = "Forgotten Effigy: ^The light fades away.";

                        NoDecoration();

                    }

                    break;

            }

            if (correctAnswer != statueChoice)
            {
                mod.UpdateChoice(correctAnswer);

            }
            Game1.activeClickableMenu = new DialogueBox(statueReply);

        }

        public void DialogueWeapon()
        {

            string statueQuestion = "Forgotten Effigy: ^Some weapons lie beneath the surface of the material plane. ^The forest, the shore, the mountains... all hold such secrets.";

            Response[] statueChoices = new Response[5]
            {
                 new Response("forest","Where in the forest?"),
                 new Response("tides","Where by the shore?"),
                 new Response("mountains","Where in the mountains?"),
                 new Response("normal","Anything powerful and stylish?"),
                 new Response("none", "(say nothing)")
            };

            GameLocation.afterQuestionBehavior statueBehaviour = new(DialogueWeaponAnswer);

            targetPlayer.currentLocation.createQuestionDialogue(statueQuestion, statueChoices, statueBehaviour);

            return;

        }

        public void DialogueWeaponAnswer(Farmer statueVisitor, string statueAnswer)
        {

            string statueReply;

            switch (statueAnswer)
            {
                case "forest":

                    statueReply = "Forgotten Effigy: ^Seek out the giant, the lonely one, the malus of the forest.";

                    break;

                case "tides":

                    statueReply = "Forgotten Effigy: ^Strike with storm the swells south of the stars in the shallows.";

                    break;

                case "mountains":

                    statueReply = "Forgotten Effigy: ^Return molten fire to the lake of flames.";

                    break;

                case "normal":

                    statueReply = "Forgotten Effigy: ^You can channel through a material weapon... ^But know that only those blades forged in the otherworld possess innate power.";

                    break;

                default: //"none"

                    statueReply = "...";

                    break;

            }

            Game1.activeClickableMenu = new DialogueBox(statueReply);

            return;

        }

        public void DialogueTrial()
        {

            List<string> challengesCompleted = mod.ChallengesCompleted();

            List<string> challengesReceived = mod.ChallengesReceived();

            List<string> blessingsReceived = mod.BlessingsReceived();

            string statueReply = "I will offer you a new trial when you are ready.";

            if (!challengesReceived.Contains("challengeEarth") && blessingsReceived.Contains("earth"))
            {

                mod.ReceiveChallenge("challengeEarth");

                statueReply = "Foulness seeps from the mountain. ^Cleanse it with the King's blessing.";

            }

            if (challengesCompleted.Contains("challengeEarth") && !challengesReceived.Contains("challengeWater") && blessingsReceived.Contains("water"))
            {

                mod.ReceiveChallenge("challengeWater");

                statueReply = "Creatures of shadow linger in the hollowed grounds of the village. ^Smite them with the Lady's blessing.";

            }

            if (challengesCompleted.Contains("challengeWater") && !challengesReceived.Contains("challengeStars") && blessingsReceived.Contains("stars"))
            {

                mod.ReceiveChallenge("challengeStars");

                statueReply = "The southern forest reeks of the pernicious slime. ^Rain judgement upon them with the blessing of the Stars.";

            }

            if (challengesCompleted.Contains("challengeEarth") && challengesCompleted.Contains("challengeWater") && challengesCompleted.Contains("challengeStars"))
            {

                statueReply = "Your power rivals that of the first farmer. You have nothing further to prove. ^When the seasons change, the valley may call upon your aid again.";
            
            }


            Game1.activeClickableMenu = new DialogueBox(statueReply);

        }

        public void DialogueQuery()
        {

            string statueReply = "I was crafted by the first farmer, to save and share his knowledge of the otherworld.";

            Game1.activeClickableMenu = new DialogueBox(statueReply);

        }


    }

}
