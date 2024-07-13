using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Delegates;
using StardewValley.GameData.Minecarts;
using StardewValley.Locations;
using StardewValley.Minigames;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Objects;
using StardewValley.Quests;
using StardewValley.TerrainFeatures;
using StardewValley.TokenizableStrings;
using StardewValley.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using xTile.Dimensions;
using xTile.Tiles;
using static StardewDruid.Character.Character;
using static StardewDruid.Character.CharacterHandle;

namespace StardewDruid.Character
{
    public static class CharacterHandle
    {

        public enum locations
        {
            grove,
            farm,
            chapel,
            vault,
            court,
            archaeum,
        }

        public enum characters
        {
            none,

            // NPCS
            Effigy,
            Jester,
            Revenant,
            Buffin,
            Shadowtin,

            // map interaction
            disembodied,
            energies,
            waves,
            herbalism,
            monument_artisans,
            monument_priesthood,
            monument_morticians,
            monument_chaos,

            // event
            Marlon,
            Gunther,
            Wizard,
            FirstFarmer,
            LadyBeyond,
            Dwarf,
            Dragon,

            // animals
            Shadowcat,
            Shadowfox,
            Shadowbat,

        }

        public enum subjects
        {
            quests,
            lore,
            relics,
            inventory,
            adventure,
            attune,
        }

        public static string CharacterTitle(characters character)
        {
            switch (character)
            {
                case characters.Shadowtin:

                    return "Shadowtin, Treasure Hunter";

                case characters.Jester:

                    return "Jester, Envoy of the Fates";

                case characters.Buffin:

                    return "Buffin, Agent of Chaos";

                case characters.Revenant:

                    return "Revenant, Guardian of the Star";

                default:

                    return "Effigy, Last of the Circle";

            }

        }

        public static Vector2 CharacterStart(locations location)
        {

            switch (location)
            {
                case locations.court:

                    return new Vector2(17, 17) * 64;

                case locations.archaeum:

                    return new Vector2(26, 15) * 64;

                case locations.grove:

                    return new Vector2(39, 15) * 64;

                case locations.chapel:

                    return new Vector2(27, 19) * 64;

                case locations.farm:

                    Vector2 farmTry;

                    GameLocation farm = Game1.getFarm();

                    FarmHouse homeOfFarmer = Utility.getHomeOfFarmer(Game1.player);

                    if (homeOfFarmer != null)
                    {
                        Point frontDoorSpot = homeOfFarmer.getFrontDoorSpot();

                        farmTry = frontDoorSpot.ToVector2() + new Vector2(0, 128);

                    } 
                    else
                    {

                        farmTry = WarpData.WarpTiles(farm);

                    }

                    List<Vector2> tryVectors = ModUtility.GetOccupiableTilesNearby(farm, ModUtility.PositionToTile(farmTry), -1, 0, 2);

                    if(tryVectors.Count > 0)
                    {

                        return tryVectors[Mod.instance.randomIndex.Next(tryVectors.Count)] * 64;

                    }

                    break;



            }

            return Vector2.Zero;

        }

        public static string CharacterLocation(locations location)
        {

            switch (location)
            {

                case locations.court:

                    return LocationData.druid_court_name;

                case locations.grove:

                    return LocationData.druid_grove_name;

                case locations.chapel:

                    return LocationData.druid_chapel_name;

                case locations.farm:

                    return Game1.getFarm().Name;

            }

            return null;

        }

        public static Vector2 RoamTether(GameLocation location)
        {

            if (location is Farm)
            {

                return CharacterStart(locations.farm);

            }

            if (location is Grove)
            {

                return CharacterStart(locations.grove) + new Vector2(0, Mod.instance.randomIndex.Next(3) * 64);

            }

            if (location is Chapel)
            {

                return CharacterStart(locations.chapel);

            }

            if (location is Court)
            {

                return CharacterStart(locations.court);

            }

            return new(location.map.Layers[0].LayerWidth / 2, location.map.Layers[0].LayerHeight / 2);

        }

        public static locations CharacterHome(characters character)
        {
            switch (character)
            {

                case characters.Buffin:

                    return locations.court;

                case characters.Revenant:

                    return locations.chapel;

                default:

                    return locations.grove;

            }

        }

        public static void CharacterWarp(Character entity, locations destination, bool instant = false)
        {

            string destiny = CharacterLocation(destination);

            Vector2 position = CharacterStart(destination);

            CharacterMover mover = new(entity.characterType);

            mover.WarpSet(destiny, position, true);

            if (instant)
            {

                mover.Update();

                return;

            }

            Mod.instance.movers[entity.characterType] = mover;

        }

        public static void CharacterLoad(characters character, Character.mode mode)
        {

            if (!Context.IsMainPlayer)
            {

                return;

            }

            if (Mod.instance.characters.ContainsKey(character))
            {

                if(Mod.instance.characters[character].modeActive != mode)
                {

                    Mod.instance.characters[character].SwitchToMode(mode, Game1.player);

                }

                return;

            }

            switch (character)
            {

                case characters.Revenant:

                    Mod.instance.characters[character] = new Revenant(character);

                    break;

                case characters.Jester:

                    Mod.instance.characters[character] = new Jester(character);

                    break;

                case characters.Buffin:


                    Mod.instance.characters[character] = new Buffin(character);

                    break;

                case characters.Shadowtin:

                    Mod.instance.characters[character] = new Shadowtin(character);

                    break;

                default:

                    character = characters.Effigy;

                    Mod.instance.characters[character] = new Effigy(character);

                    break;
            
            }

            Mod.instance.dialogue[character] = new(character);

            Mod.instance.characters[character].NewDay();

            Mod.instance.characters[character].SwitchToMode(mode, Game1.player);

        }

        public static Texture2D CharacterTexture(characters character)
        {

            switch (character)
            {
                case characters.disembodied:
                case characters.energies:
                case characters.waves:
                case characters.herbalism:
                case characters.monument_artisans:
                case characters.monument_priesthood:
                case characters.monument_morticians:
                case characters.monument_chaos:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images","DarkRogue.png"));

                case characters.Dwarf:

                    return Mod.instance.Helper.GameContent.Load<Texture2D>(Path.Combine("Characters", "Dwarf"));

                case characters.Shadowbat:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Batwing.png"));

                default:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", character.ToString() + ".png"));

            }

        }

        public static Texture2D CharacterPortrait(characters character)
        {

            switch (character)
            {
                /*case characters.jester:
                case characters.shadowtin:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", CharacterNames()[character] + "Portrait.png"));*/
                case characters.Revenant:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "RevenantPortrait.png"));

                case characters.Jester:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "JesterPortrait.png"));

                case characters.Buffin:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "BuffinPortrait.png"));

                case characters.Shadowtin:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "ShadowtinPortrait.png"));

                case characters.Dwarf:

                    return Mod.instance.Helper.GameContent.Load<Texture2D>(Path.Combine("Portraits", "Dwarf"));

                case characters.Wizard:

                    return Mod.instance.Helper.GameContent.Load<Texture2D>(Path.Combine("Portraits", "Wizard"));

                default:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "EffigyPortrait.png"));

            }

        }

        public static CharacterDisposition CharacterDisposition(characters character)
        {

            CharacterDisposition disposition = new()
            {
                Age = 30,
                Manners = 2,
                SocialAnxiety = 1,
                Optimism = 0,
                Gender = 0,
                datable = false,
                Birthday_Season = "summer",
                Birthday_Day = 27,
                id = 18465001,
                speed = 2,

            };

            if (character == characters.Revenant)
            {

                disposition.id += 1;
                disposition.Birthday_Day = 15;

            }

            if (character == characters.Jester)
            {

                disposition.id += 2;
                disposition.Birthday_Season = "fall";

            }

            if (character == characters.Buffin)
            {

                disposition.id += 3;
                disposition.Birthday_Season = "spring";

            }

            if (character == characters.Shadowtin)
            {

                disposition.id += 4;
                disposition.Birthday_Season = "winter";

            }

            return disposition;

        }

        public static string DialogueApproach(characters character)
        {

            switch (character)
            {

                case characters.Effigy:

                    if (Mod.instance.save.milestone == QuestHandle.milestones.weald_weapon || Mod.instance.save.milestone == QuestHandle.milestones.mists_weapon)
                    {

                        return "The Effigy: Successor, remember to retain discipline in your training, and visit me tomorrow for new instruction";

                    }

                    if (Mod.instance.characters[characters.Effigy].currentLocation.IsFarm)
                    {

                        return "The Effigy: Provide me with some seeds, and I will sow them as I attend to the crops. " +
                            "Remember to place plenty of scarecrows about. I like to talk to them.";

                    }

                    return "The Effigy: Greetings, successor";

                case characters.Revenant:

                    if (Mod.instance.save.milestone == QuestHandle.milestones.stars_weapon)
                    {

                        return "The Revenant: Fortumei bless you, warrior. Come see me again tomorrow, I might have more to teach you.";

                    }
                    return "The Revenant: Hail, adventurer.";

                case characters.Jester:

                    if (Mod.instance.save.milestone == QuestHandle.milestones.fates_weapon)
                    {

                        return "The Jester of Fate: That's all for today friend. Now what can I find to rub up against...";

                    }

                    if (Mod.instance.save.milestone == QuestHandle.milestones.fates_enchant)
                    {

                        return "The Jester of Fate: Did you go see Buffin at the court of Fates and Chaos? She has the best stuff.";

                    }

                    if (Mod.instance.characters[characters.Jester].currentLocation.IsFarm)
                    {

                        return "The Jester of Fate: I think this place could do with some animals. I enjoy milking.";

                    }

                    return "The Jester of Fate: Hey friend.";

                case characters.Buffin:

                    return "You would make a great servant of chaos, Farmer.";

                case characters.Shadowtin:

                    if (Mod.instance.characters[characters.Jester].currentLocation.IsFarm)
                    {

                        return "Shadowtin Bear: I am fascinated by the industry of the organic world, " +
                            "especially the substances you refer to as maple syrup and honey.";

                    }

                    return "(Shadowtin's ethereal eyes shine through a cold metal mask)";

                case characters.energies:

                    return "Energies of the Weald: Squire (squire) (squire) (squire)";

                case characters.waves:

                    return "Murmurs of the Waves: Yes, friend";

                case characters.herbalism:

                    return "An old stone bench used by the old Druids for crafting herbal remedies";

                case characters.monument_artisans:

                    return "The beak has not been worn down with the passage of time.";

                case characters.monument_priesthood:

                    return "The cat presides over the court";

                case characters.monument_morticians:

                    return "The giant owl appears to watch the entrance to the cavern, as if waiting for something to arrive, or watching something leave.";

                case characters.monument_chaos:

                    return "It's unnerving to see a canine so large sit so still.";

            }

            return null;

        }

        public static string DialogueNevermind(characters character)
        {


            return "(nevermind)";


        }

        public static string DialogueOption(characters character, subjects subject)
        {

            switch (subject)
            {

                case subjects.quests:

                    if (Mod.instance.questHandle.IsQuestGiver(character))
                    {

                        return "(quests) Can you please repeat the instructions?";

                    }

                    return null;

                case subjects.lore:

                    if(Mod.instance.questHandle.lorekey != null)
                    {

                        foreach (LoreData.stories story in Mod.instance.questHandle.loresets[Mod.instance.questHandle.lorekey])
                        {

                            if (Mod.instance.questHandle.lores.ContainsKey(story))
                            {

                                if (Mod.instance.questHandle.lores[story].character == character)
                                {

                                    return LoreData.RequestLore(character);

                                }

                            }

                        }

                    }

                    break;

                case subjects.relics:

                    switch (character)
                    {

                        case characters.energies:

                            int runestones = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.runestones);

                            if (runestones == -1)
                            {

                                return null;

                            }

                            if (runestones == 4)
                            {

                                return "(relics) I've recovered all the missing runestones from the founding of the circle. How do I access their latent powers?";

                            }
                            else if (runestones >= 1)
                            {

                                return "(relics) I think I've found an artifact from the time of the circle's founding";

                            }

                            return null;

                        case characters.waves:

                            int avalant = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.avalant);

                            if (avalant == -1)
                            {

                                return null;

                            }

                            if (avalant == 6)
                            {

                                return "(relics) I've found all the components of the ancient Avalant. Will this enable me to chart a course to the isle of mists?";

                            }
                            else if (avalant >= 1)
                            {

                                return "(relics) I fished out a fragment of a strange device. Is it familiar to you?";

                            }

                            return null;

                        case characters.Revenant:

                            int books = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.books);

                            if (books == -1)
                            {

                                return null;

                            }

                            if (books == 4)
                            {

                                return "(relics) I've managed to find three manuscripts hidden by the first farmer before he disappeared.";

                            }
                            else if (books >= 1)
                            {

                                return "(relics) How many records from the founding of the circle are missing?";

                            }

                            return null;

                        case characters.Buffin:

                            int boxes = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.boxes);

                            if (boxes == -1)
                            {

                                return null;

                            }

                            if (boxes == 4)
                            {

                                return "(relics) These ornament boxes, they're all empty";

                            }
                            else if (boxes >= 1)
                            {

                                return "(relics) The Reaper of Fate kept this small box on him. Is it of value to the Fates?";

                            }

                            return null;

                        case characters.monument_artisans:

                            return "You take out your strongest tool, and strike as hard as you can.";

                        case characters.monument_priesthood:

                            return "You raise your hand towards the feline's gentle features.";

                        case characters.monument_morticians:

                            return "You marvel at the colouring and lustre of the owl's feathers. You reach out, expecting a surface as cool and smooth as ivory.";

                        case characters.monument_chaos:

                            return "You lift the back of your hand towards the enlarged snout.";
                    }

                    break;

                case subjects.inventory:

                    switch (character)
                    {

                        case characters.Effigy:

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.questEffigy) && Context.IsMainPlayer)
                            {

                                return "(inventory) I have some seeds for you to sow.";

                            }

                            break;

                        case characters.Jester:

                            if (Context.IsMainPlayer)
                            {

                                return "Jester... what's in your mouth. Come on. Show me.";

                            }

                            break;

                        case characters.Shadowtin:

                            if (Context.IsMainPlayer)
                            {

                                return "(inventory) Let's take stock of our supplies.";

                            }

                            break;

                        case characters.herbalism:

                            if (Context.IsMainPlayer)
                            {

                                return "(inventory) Store and review potion ingredients.";

                            }

                            break;
                    }

                    break;

                case subjects.adventure:

                    switch (character)
                    {

                        case characters.Effigy:

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.questEffigy) && Context.IsMainPlayer)
                            {

                                return "(adventure) I have a task for you.";

                            }

                            break;

                        case characters.Jester:

                            if (Context.IsMainPlayer)
                            {

                                return "(adventure) Let's talk adventure.";

                            }

                            break;                        
                        
                        case characters.Shadowtin:
                            
                            if (Context.IsMainPlayer)
                            {

                                return "(adventure) Lets talk about our partnership.";

                            }

                            break;

                        case characters.waves:

                            return "(warp) Why is the monument to the lady all the way out here?";

                        case characters.Buffin:

                            return "(warp) What is it like to be a servant of Chaos, Buffin? I imagine your duties take you to all sorts of random places.";

                        case characters.Revenant:

                            return "(warp) It was worth the treacherous ascent to witness the unfurling of the curtain of the sky. " +
                                "The stage is set with the heroes of the constellations, and their radiance inspires me. Can I sleep here?";

                        case characters.herbalism:

                            if (Mod.instance.save.reliquary.ContainsKey(IconData.relics.crow_hammer.ToString()))
                            {

                                return "(hammer) Break and store the contents of all geodes in inventory.";

                            }

                            break;
                    }

                    break;

                case subjects.attune:

                    switch (character)
                    {

                        case characters.energies:

                            return AttunementIntro(Rite.rites.weald);

                        case characters.waves:

                            return AttunementIntro(Rite.rites.mists);

                        case characters.Revenant:

                            return AttunementIntro(Rite.rites.stars);

                        case characters.Jester:

                            return AttunementIntro(Rite.rites.fates);

                        case characters.Shadowtin:

                            return AttunementIntro(Rite.rites.ether);

                        case characters.herbalism:

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.herbalism))
                            {

                                return "(herbalism) Replenish potions";

                            }
                            if (Mod.instance.questHandle.IsGiven(QuestHandle.herbalism))
                            {

                                return "The bowl beckons. You reach out, tentatively, and touch its rim with your fingertip.";

                            }

                            return null;

                    }

                    break;

            }

            return null;

        }

        public static DialogueSpecial DialogueGenerator(characters character, subjects subject, int index = 0, int answer = 0)
        {

            DialogueSpecial generate = new();

            switch (subject)
            {

                case subjects.quests:

                    Mod.instance.questHandle.DialogueReload(character);

                    return null;

                case subjects.lore:

                    if (Mod.instance.questHandle.lorekey != null)
                    {
                        foreach (LoreData.stories story in Mod.instance.questHandle.loresets[Mod.instance.questHandle.lorekey])
                        {

                            if (Mod.instance.questHandle.lores.ContainsKey(story))
                            {

                                if (Mod.instance.questHandle.lores[story].character == character)
                                {

                                    generate.intro = LoreData.CharacterLore(character);

                                    generate.responses.Add(Mod.instance.questHandle.lores[story].question);

                                    generate.answers.Add(Mod.instance.questHandle.lores[story].answer);

                                }

                            }

                        }

                    }


                    break;

                case subjects.relics:

                    switch (character)
                    {

                        case characters.energies:

                            int runestones = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.runestones);

                            if (runestones == -1)
                            {

                                return generate;

                            }

                            if (runestones == 4)
                            {

                                generate.intro = "Whispering on the wind: (laughs) You already possess conduits for the energies represented in the runes. Well, I can't remember what the cat means. " +
                                    "Sighs of the Earth: The calico shamans served the ancient ones, the dragons. Their wild shape-shifting has been forgotten to time and ruin. The runestones are useless to you now.";

                                if (!Game1.MasterPlayer.mailReceived.Contains("JojaMember") && !(Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[1])
                                {

                                    Mod.instance.questHandle.AssignQuest(QuestHandle.relicWeald);

                                    generate.responses.Add("Oh. Well the craftsmanship is rather neat. I think I'll use them to redecorate the craftroom at the community center.");

                                    generate.answers.Add("Rustling in the woodland: When sourcing materials for your redecoration project, consider nearby sources and support your local woodlands! We have the best timber products on the market. " +
                                        "(New quest recieved)");

                                }
                                else
                                {

                                    Mod.instance.questHandle.CompleteQuest(QuestHandle.relicWeald);

                                }

                            }
                            else if (runestones >= 1)
                            {

                                generate.intro = "Rustling in the woodland: It is one of the runestones gifted to your forebearers, those tasked to govern in the Kings' stead. " +
                                    "The other stones belong to creatures that do not care for the old ways. To gather all four would be a boon to the Weald.";

                            }

                            return generate;

                        case characters.waves:

                            int avalant = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.avalant);

                            if (avalant == -1)
                            {

                                return null;

                            }

                            if (avalant == 6)
                            {

                                generate.intro = "Murmurs of the Waves: Indeed. The finned faithful will be very pleased... to be rid of the ruinous device! " +
                                    "You see, it points towards the broken forgehall of the drowned city, deep within the abyssal trench, where mortals cannot tread. It is useless to you.";

                                if (!Game1.MasterPlayer.mailReceived.Contains("JojaMember") && !(Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[2])
                                {
                                    Mod.instance.questHandle.AssignQuest(QuestHandle.relicMists);

                                    generate.responses.Add("The parts appear to be quite sea-resistant. Perhaps I can use them to repair the community center fishtank.");

                                    generate.answers.Add("Fish... Tank? We weren't aware the little creatures had armed themselves with war machines. " +
                                        "We will warn the guardians of the depths to prepare for a fish uprising! But do as you will with the Avalant. " +
                                        "(New Quest Received)");
                                }
                                else
                                {

                                    Mod.instance.questHandle.CompleteQuest(QuestHandle.relicMists);

                                }

                            }
                            else if (avalant >= 1)
                            {

                                generate.intro = "Murmurs of the Waves: The Avalant, to guide the blessed pilgrim through the judgements of the mists. " +
                                    "It was broken, but can be restored. The finned faithful carry the pieces from the sea to the sacred spring. " +
                                    "Continue to use the power of the lady to fish the waters, and you might collect all the scattered pieces.";

                            }

                            return generate;

                        case characters.Revenant:

                            int books = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.books);

                            if (books == -1)
                            {

                                return generate;

                            }

                            if (books == 4)
                            {

                                generate.intro = "";


                                if (!Game1.MasterPlayer.mailReceived.Contains("JojaMember") && !(Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[0])
                                {
                                    
                                    Mod.instance.questHandle.AssignQuest(QuestHandle.relicEther);

                                    generate.intro = "The Revenant: ";
                                    
                                    generate.responses.Add("I guess there's no great mystery to glean from these texts, but at least I can add the recipes to the digests in the community pantry.");

                                    generate.answers.Add("Even the one for Fish Stew? Some of the ideas of my day are best left in the past. (New Quest Received)");

                                }
                                else
                                {

                                    Mod.instance.questHandle.CompleteQuest(QuestHandle.relicEther);

                                }

                                generate.intro += "I think the Lady Beyond must have stashed these texts, farmer. The letters were penned by the prince of the sunken city, heir to the Isle of Mists, and the one who seduced our fair Starborn. " +
                                    "I have nothing to say about the tome. The green book might have been too personal to leave intact, as it's been scrubbed down to just the boring parts. Recipes and potting tips. ";

                            }
                            else if (books >= 1)
                            {

                                generate.intro = "All the annals are accounted for, but if you find any manuscripts that weren't profaned by those crazies, bring them to me, might be good for a memory or a laugh.";

                            }

                            return generate;

                        case characters.Buffin:

                            int boxes = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.boxes);

                            if (boxes == -1)
                            {

                                return generate;

                            }

                            if (boxes == 4)
                            {
                                generate.intro = "";

                                if (!Game1.MasterPlayer.mailReceived.Contains("JojaMember") && !(Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[5])
                                {

                                    Mod.instance.questHandle.AssignQuest(QuestHandle.relicFates);

                                    generate.intro = "Buffin, Agent of Chaos: ";

                                    generate.responses.Add("I guess I understand. There's a bulletin board layered with all kinds of moments like that, only perceptable, put on paper and print stock. I think these boxes would be perfect to put the memories of the community in.");

                                    generate.answers.Add("You make me smile, farmer. There's so much goodwill the Fates have for the people of this valley, though we are limited in how we can share it, and the desperate, cruel legacy of Thanatoshi hasn't helped. I count you as a friend to the Fae and blessed by Yoba. " +
                                        "(New quest recieved)");

                                }
                                else
                                {

                                    Mod.instance.questHandle.CompleteQuest(QuestHandle.relicFates);

                                }

                                generate.intro += "Nonsense, farmer, they're full. Full of imperceptible wonders. Good will, happy memories and lessons learned by the Fates that experienced life on this realm.";

                            }
                            else if (boxes >= 1)
                            {

                                generate.intro = "Very valuable, farmer, for the Fate that possessed it. These trinkets are keepsakes of home, to comfort those of us on assignment to the realms. " +
                                    "This belonged to a kindred spirit of Thanatoshi's. Masayoshi, the Justiciar of Fate at a moment in the history of this world. " +
                                    "Imagine the bond they shared, to exchange boxes with one another. (Buffin looks towards the monuments) " +
                                    "You'll have to convince the stone guardians to relinquish their secrets if you want the complete set. The Artisans are fond of tools, the Morticians, bones, probably, and Chaos... animals, of course!";

                            }

                            return generate;


                        case characters.monument_artisans:
                            
                            switch (Mod.instance.relicsData.ArtisanRelicQuest())
                            {

                                case 0:

                                    generate.intro = "The tool almost flies out of your hands with the rebound. It didn't even impact the surface of the monument. You may need an implement with a lustre that delights the Artisans. (Come back with an iridium tool) ";

                                    break;

                                case 1:

                                    generate.intro = "Your strike lands and a marvelous clang echoes through the cavern, singing the praises of a beautiful implement.";

                                    ThrowHandle throwNotes = new(Game1.player, Game1.player.Position + new Vector2(-64, -256), IconData.relics.box_artisan);

                                    throwNotes.register();

                                    break;

                                default:

                                    generate.intro = "Verse 1 of 4. Cracks have appeared in the stonework of the court monuments.";

                                    break;

                            }

                            return generate;

                        case characters.monument_priesthood:

                            int priestProgress = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.boxes);

                            if (!Mod.instance.save.herbalism.ContainsKey(HerbalData.herbals.faeth))
                            {

                                Mod.instance.save.herbalism[HerbalData.herbals.faeth] = 0;

                            }

                            if (Mod.instance.save.herbalism[HerbalData.herbals.faeth] == 0)
                            {

                                int faethBlessing = Mod.instance.randomIndex.Next(1, 4);

                                Mod.instance.CastMessage("You have received " + faethBlessing.ToString() + " faeth");

                                Mod.instance.save.herbalism[HerbalData.herbals.faeth] = faethBlessing;

                                generate.intro = "The Priestess bestows you with a small gift of Faeth";

                                return generate;

                            }

                            if (priestProgress >= 0)
                            {

                                generate.intro = "The shadow cast by the cat shifts over your hands, as if reading your fortune from the lines in your palms. " +
                                    "You now know that you are to find one that is lost, where three have stayed one has strayed far away.";

                            }
                            else
                            {

                                generate.intro = "Verse 2 of 4. The one that was lost has returned, and their silent vigil has concluded.";

                            }

                            return generate;


                        case characters.monument_morticians:

                            switch (Mod.instance.relicsData.MorticianRelicQuest())
                            {

                                case 0:

                                    generate.intro = "You do not recognise the material of the owl's feathers. Perhaps you do not know the Mortician's art. (Come back with a 'Prehistoric' or 'Fossilised' bone item) ";

                                    break;

                                case 1:

                                    generate.intro = "Your hands have honoured the bones of those that have passed on. ";

                                    ThrowHandle throwNotes = new(Game1.player, Game1.player.Position + new Vector2(64, -256), IconData.relics.box_mortician);

                                    throwNotes.register();
                                    break;
                                default:
                                    generate.intro = "Verse 3 of 4. Their eyes now shut to the cares of this world.";

                                    break;
                            }

                            return generate;

                        case characters.monument_chaos:

                            switch (Mod.instance.relicsData.ChaosRelicQuest())
                            {

                                case 0:

                                    generate.intro = "Your hands do not smell sweet to the hungry fox. (Come back when you have built a Deluxe Barn or Deluxe Coop) ";

                                    break;

                                case 1:

                                    generate.intro = "The fox's grin appears to widen. Your hands have touched the splendid creatures. ";

                                    ThrowHandle throwNotes = new(Game1.player, Game1.player.Position + new Vector2(64,-256), IconData.relics.box_chaos);

                                    throwNotes.register();

                                    break;

                                default:

                                    generate.intro = "Verse 4 of 4. They are free to crumble into the unburdened nothing of dust.";

                                    break;
                            }

                            return generate;

                    }

                    break;

                case subjects.inventory:

                    OpenInventory(character);

                    return null;

                case subjects.adventure:

                    switch (index)
                    {

                        case 0:


                            switch (character)
                            {

                                case characters.Effigy:


                                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.questEffigy))
                                    {

                                        break;

                                    }

                                    generate.intro = "The Effigy: It is time to roam the wilds once more.";

                                    if (Mod.instance.characters[character].modeActive != Character.mode.track)
                                    {

                                        generate.responses.Add("(follow) Come explore the valley with me.");

                                        generate.answers.Add("1");
                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.roam)
                                    {

                                        generate.responses.Add("(work) My farm would benefit from your gentle stewardship.");

                                        generate.answers.Add("2");

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.home
                                        && Mod.instance.characters[character].currentLocation.Name != CharacterLocation(locations.grove))
                                    {

                                        generate.responses.Add("(rest) Thank you for everything you do for the circle.");

                                        generate.answers.Add("3");

                                    }

                                    generate.lead = true;

                                    return generate;                                
                                
                                
                                case characters.Jester:

                                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.approachJester))
                                    {

                                        break;

                                    }

                                    generate.intro = "The Jester of Fate: I'm ready to explore the world."; ;

                                    if (Mod.instance.characters[character].modeActive != Character.mode.track)
                                    {

                                        generate.responses.Add("(follow) Let us continue our quest.");

                                        generate.answers.Add("1");

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.roam)
                                    {

                                        generate.responses.Add("(work) There's plenty going on on the farm.");

                                        generate.answers.Add("2");

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.home
                                        && Mod.instance.characters[character].currentLocation.Name != CharacterLocation(locations.grove))
                                    {

                                        generate.responses.Add("(rest) The Druid's grove is where it's all happening.");

                                        generate.answers.Add("3");

                                    }

                                    generate.lead = true;

                                    return generate;

                                case characters.Shadowtin:

                                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeFates))
                                    {

                                        break;

                                    }

                                    generate.intro = "Shadowtin Bear: What do you propose?"; ;

                                    if (Mod.instance.characters[character].modeActive != Character.mode.track)
                                    {

                                        generate.responses.Add("(follow) Let us continue our investigation.");

                                        generate.answers.Add("1");

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.roam)
                                    {

                                        generate.responses.Add("(work) There's plenty of research material on the farm.");

                                        generate.answers.Add("2");

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.home
                                        && Mod.instance.characters[character].currentLocation.Name != CharacterLocation(locations.grove))
                                    {

                                        generate.responses.Add("(rest) That's enough treasure hunting for now.");

                                        generate.answers.Add("3");

                                    }

                                    generate.lead = true;

                                    return generate;

                                case characters.waves:

                                    generate.intro = "Murmurs of the waves: Still yourself, and let the rolling motions of the mists carry you home.";

                                    generate.responses.Add("I feel... wet? Wait what... !");

                                    generate.answers.Add("10");

                                    generate.lead = true;

                                    return generate;


                                case characters.Buffin:

                                    generate.intro = "Buffoonette of Chaos: Indeed they do, and I'm glad you asked me to demonstrate. Now, close your eyes. Prepare for your mind to be swept along the Great Stream.";

                                    generate.responses.Add("Sorry, Buffin, what did you just say?");

                                    generate.answers.Add("11");

                                    generate.lead = true;

                                    return generate;

                                case characters.Revenant:

                                    generate.intro = "The Revenant: Best you be off home farmer, you don't want to be around when the bats have their rave. " +
                                        "There's a cart-line that extends from here all the way to the main network. It was working well the last time I used it, but tell me if it requires any repairs.";

                                    generate.responses.Add("Has it been inspected recent- (Revenant pushes you into the cart bed and disengages the brake-lever)");

                                    generate.answers.Add("12");

                                    generate.lead = true;

                                    return generate;

                                case characters.herbalism:

                                    Mod.instance.herbalData.ConvertGeodes();

                                    return null;

                            }

                            break;

                        case 1:

                            switch (answer)
                            {

                                case 1: // Follow player

                                    switch (character)
                                    {
                                        default:
                                        case characters.Effigy:

                                            generate.intro = "I will see how you exercise the authority of the sleeping kings.";

                                            break;

                                        case characters.Jester:

                                            generate.intro = "Lead the way, fateful one.";

                                            break;

                                        case characters.Shadowtin:

                                            generate.intro = "Indeed. How about we split the spoils fifty fifty.";

                                            break;

                                    }

                                    Mod.instance.characters[character].SwitchToMode(Character.mode.track, Game1.player);

                                    return generate;

                                case 2: // Work on farm

                                    switch (character)
                                    {
                                        default:
                                        case characters.Effigy:

                                            generate.intro = "I will take my place amongst the posts and furrows of my old master's home.";

                                            break;

                                        case characters.Jester:

                                            generate.intro = "Let's see who's around to bother.";

                                            break;

                                        case characters.Shadowtin:

                                            generate.intro = "Lets see how profitable this agricultural venture is.";

                                            break;
                                    }

                                    Mod.instance.characters[character].SwitchToMode(Character.mode.roam, Game1.player);

                                    return generate;

                                case 3: // Return to grove

                                    switch (character)
                                    {
                                        default:
                                        case characters.Effigy:

                                            generate.intro = "I will return to where I may hear the rumbling energies of the valley's leylines.";

                                            break;

                                        case characters.Jester:

                                            generate.intro = "(Jester grins) That's my favourite place to nap!";

                                            break;

                                        case characters.Shadowtin:

                                            generate.intro = "Good idea. I require a quiet, shaded place to ruminate.";

                                            break;
                                    }

                                    Mod.instance.characters[character].SwitchToMode(Character.mode.home, Game1.player);

                                    return generate;

                                case 10:
                                case 11:
                                case 12:

                                    if (index == 12)
                                    {

                                        Dictionary<string, MinecartNetworkData> dictionary = DataLoader.Minecarts(Game1.content);

                                        if (dictionary.TryGetValue("Default", out var network))
                                        {

                                            MinecartNetworkData minecartNetworkData = network;

                                            if (minecartNetworkData != null && minecartNetworkData.Destinations?.Count > 0)
                                            {

                                                foreach (MinecartDestinationData destination in network.Destinations)
                                                {

                                                    if (destination.Id.Contains("Bus"))
                                                    {

                                                        Game1.player.currentLocation.MinecartWarp(destination);
                                                        return generate;

                                                    }

                                                }

                                            }

                                        }

                                    }


                                    if(index == 11 && Mod.instance.randomIndex.Next(2) == 0)
                                    {

                                        for(int i = 0; i < 4; i++)
                                        {

                                            GameLocation location = Game1.locations.ElementAt(Mod.instance.randomIndex.Next(Game1.locations.Count()));

                                            Vector2 tile = location.getRandomTile();

                                            if(ModUtility.TileAccessibility(location,tile) != 0)
                                            {

                                                continue;

                                            }

                                            Mod.instance.iconData.ImpactIndicator(Game1.player.currentLocation, Game1.player.Position, IconData.impacts.bomb, 4, new());

                                            Game1.player.playNearbySoundAll("wand");

                                            Game1.warpFarmer(location.Name, (int)tile.X, (int)tile.Y, 2);

                                            Game1.xLocationAfterWarp = (int)tile.X;

                                            Game1.yLocationAfterWarp = (int)tile.Y;

                                            return generate;

                                        }

                                    }

                                    Wand wand = new();

                                    wand.lastUser = Game1.player;

                                    wand.DoFunction(Game1.player.currentLocation, 0, 0, 0, Game1.player);

                                    return generate;


                            }

                            break;
                    }

                    break;

                case subjects.attune:

                    int toolIndex = Mod.instance.AttuneableWeapon();

                    int attuneUpdate;

                    switch (character)
                    {

                        case characters.energies:

                            attuneUpdate = AttunementUpdate(Rite.rites.weald);

                            switch (attuneUpdate)
                            {

                                case 0:

                                    generate.intro = "This " + Game1.player.CurrentTool.Name + " resists attunement";

                                    break;

                                case 1:

                                    generate.intro = "Sighs of the Earth: The " + Game1.player.CurrentTool.Name + " was crafted from materials blessed by the Lords of the Weald. " +
                                    "It will serve your purposes as squire just fine, but its allegiance will always be to the Two Kings.";

                                    break;

                                case 2:

                                    generate.intro = "Sighs of the Earth: This " + Game1.player.CurrentTool.Name + " will no longer serve the Two Kings";

                                    break;

                                case 3:

                                    generate.intro = "Sighs of the Earth: This " + Game1.player.CurrentTool.Name + " will serve the Two Kings";

                                    break;

                            }

                            return generate;

                        case characters.waves:

                            attuneUpdate = AttunementUpdate(Rite.rites.mists);

                            switch (attuneUpdate)
                            {

                                case 0:

                                    generate.intro = "This " + Game1.player.CurrentTool.Name + " resists attunement";

                                    break;

                                case 1:

                                    generate.intro = "Murmurs of the Waves: The " + Game1.player.CurrentTool.Name + " is from a time before the Lady, " +
                                        "before the mists swirled around the isle, before the city it was forged in was lost to the storm.";


                                    break;

                                case 2:

                                    generate.intro = "Murmurs of the Waves: This " + Game1.player.CurrentTool.Name + " will no longer serve the Lady Beyond the Shore";

                                    break;

                                case 3:

                                    generate.intro = "Murmurs of the Waves: This " + Game1.player.CurrentTool.Name + " will serve the Lady Beyond the Shore";

                                    break;

                            }

                            return generate;

                        case characters.Revenant:

                            attuneUpdate = AttunementUpdate(Rite.rites.stars);

                            switch (attuneUpdate)
                            {

                                case 0:

                                    generate.intro = "This " + Game1.player.CurrentTool.Name + " resists attunement";

                                    break;

                                case 1:

                                    generate.intro = "There used to be a lot of warriors in our order. Now all that remains of them are the " + Game1.player.CurrentTool.Name + "'s that never tarnish.";

                                    break;

                                case 2:

                                    generate.intro = "This " + Game1.player.CurrentTool.Name + " will no longer serve the Lights of the Great Expanse";

                                    break;

                                case 3:

                                    generate.intro = "This " + Game1.player.CurrentTool.Name + " will serve the Lights of the Great Expanse";

                                    break;

                            }

                            return generate;

                        case characters.Jester:

                            attuneUpdate = AttunementUpdate(Rite.rites.fates);

                            switch (attuneUpdate)
                            {

                                case 0:

                                    generate.intro = "This " + Game1.player.CurrentTool.Name + " resists attunement";

                                    break;

                                case 1:

                                    generate.intro = "I don't think that " + Game1.player.CurrentTool.Name + " is of this world, Farmer. " +
                                        "I think it was made by one of the Morticians to reap the wayward souls of mortals. " +
                                        "Or maybe they wanted to cut the heads off of flowers. Morticians are melancholic.";

                                    break;

                                case 2:

                                    generate.intro = "This " + Game1.player.CurrentTool.Name + " means nothing to the Fates now, Farmer";

                                    break;

                                case 3:

                                    generate.intro = "This " + Game1.player.CurrentTool.Name + " will please the High Priestess";

                                    break;

                            }

                            return generate;

                        case characters.Shadowtin:

                            attuneUpdate = AttunementUpdate(Rite.rites.ether);

                            switch (attuneUpdate)
                            {

                                case 0:

                                    generate.intro = "This " + Game1.player.CurrentTool.Name + " resists attunement";

                                    break;

                                case 1:

                                    generate.intro = "I suspect that the instrument you acquired from the Reaper of Fate carries the ether-bound memories of the Tyrant of Calico and all the shapeshifters who served under him. " +
                                                    "Thus, the guise of an ancient one seems a natural result from it's use. That's my theory anyway.";

                                    break;

                                case 2:

                                    generate.intro = "This " + Game1.player.CurrentTool.Name + " does not touch the Ether now, Farmer";

                                    break;

                                case 3:

                                    generate.intro = "This " + Game1.player.CurrentTool.Name + " will now whistle through the streams of Ether";

                                    break;

                            }

                            return generate;


                        case characters.herbalism:

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.herbalism))
                            {

                                generate.intro = "Potion stock has been refilled based on available ingredients.";

                                Mod.instance.herbalData.MassBrew();

                                return generate;

                            }

                            switch (index)
                            {

                                case 0:

                                    generate.intro = "Your fingertip traces the smoothened edge. The bowl seems pleased, as if the face reflected in the shiny inner surface is happier than your own.";

                                    generate.responses.Add("You rub your palms around the curvature of the bowl.");

                                    generate.answers.Add("0");

                                    generate.responses.Add("You use a firm grip to grasp the mortar. It's so strong.");

                                    generate.answers.Add("1");

                                    generate.responses.Add("Your face goes into the bowl.");

                                    generate.answers.Add("2");

                                    generate.lead = true;

                                    break;

                                case 1:

                                    generate.intro = "A soft sound of contentment fills your ear. You're startled. You look back and forth but find no other creature but yourself at the stone table. " +
                                        "Even stranger, you have acquired the forgotten knowledge of herbalism, with all the craft secrets of the druids that stood before this bowl in ages past. " +
                                        "With a bit of amateur effort, three stoppered flasks, each with a small amount of herbal remedy, dangle from your belt. " +
                                        "The bowl is pleased. (Herbalism journal unlocked)";

                                    Mod.instance.questHandle.CompleteQuest(QuestHandle.herbalism);

                                    break;

                            }

                            return generate;

                    }

                    break;

            }

            return generate;

        }

        public static void RetrieveInventory(characters character)
        {

            if (!Mod.instance.chests.ContainsKey(character))
            {

                Chest newChest = new();

                //newChest.SpecialChestType = Chest.SpecialChestTypes.BigChest;

                if (Mod.instance.save.chests.ContainsKey(character))
                {

                    foreach (ItemData item in Mod.instance.save.chests[character])
                    {

                        newChest.Items.Add(new StardewValley.Object(item.id, item.stack, quality: item.quality));

                    }

                }

                Mod.instance.chests[character] = newChest;

            }

        }

        public static void OpenInventory(characters character)
        {

            RetrieveInventory(character);

            Mod.instance.chests[character].ShowMenu();

        }

        public static string AttunementIntro(Rite.rites compare)
        {

            int toolIndex = Mod.instance.AttuneableWeapon();

            if (toolIndex == -1 || toolIndex == 999)
            {

                return null;

            }

            Dictionary<int, Rite.rites> comparison = SpawnData.WeaponAttunement(true);

            if (comparison.ContainsKey(toolIndex))
            {

                if (comparison[toolIndex] == compare)
                {


                    return "Is there a special quality to this " + Game1.player.CurrentTool.Name + "?";

                }
                else
                {

                    return null;

                }

            }

            if (Mod.instance.save.attunement.ContainsKey(toolIndex))
            {

                if (Mod.instance.save.attunement[toolIndex] == compare)
                {


                    return "(detune) Can I reclaim this " + Game1.player.CurrentTool.Name + "?";

                }

            }

            return "(attune) I want to dedicate this " + Game1.player.CurrentTool.Name + " to the "+compare.ToString();

        }

        public static int AttunementUpdate(Rite.rites compare)
        {
            
            int toolIndex = Mod.instance.AttuneableWeapon();

            if (toolIndex == -1 || toolIndex == 999)
            {

                return 0;

            }

            Dictionary<int, Rite.rites> comparison = SpawnData.WeaponAttunement(true);

            if (comparison.ContainsKey(toolIndex))
            {

                if (comparison[toolIndex] == compare)
                {

                    return 1;

                }
                else
                {

                    return 0;

                }

            }

            if (Mod.instance.save.attunement.ContainsKey(toolIndex))
            {

                if (Mod.instance.save.attunement[toolIndex] == compare)
                {

                    Mod.instance.iconData.ImpactIndicator(Game1.player.currentLocation, Game1.player.Position, IconData.impacts.nature, 6f, new());

                    Game1.player.currentLocation.playSound("yoba");

                    Mod.instance.DetuneWeapon();

                    return 2;

                }

                //return 0;

            }

            Mod.instance.iconData.ImpactIndicator(Game1.player.currentLocation, Game1.player.Position, IconData.impacts.nature, 6f, new());

            Game1.player.currentLocation.playSound("yoba");

            Mod.instance.AttuneWeapon(compare);

            return 3;

        }

    }

    public class CharacterMover
    {

        public CharacterHandle.characters character;

        public enum moveType
        {

            from,
            to,
            remove,

        }

        public moveType type;

        public string locale;

        public Vector2 position;

        public bool animate;

        public CharacterMover(CharacterHandle.characters CharacterType)
        {

            character = CharacterType;

        }

        public void Update()
        {

            Character entity = Mod.instance.characters[character];

            GameLocation target;

            if (Mod.instance.locations.ContainsKey(locale))
            {

                target = Mod.instance.locations[locale];

            }
            else
            {

                target = Game1.getLocationFromName(locale);

            }

            switch (type)
            {

                case moveType.from:

                    target.characters.Remove(entity);

                    break;

                case moveType.to:

                    Warp(target, entity, position);

                    break;

                case moveType.remove:

                    RemoveAll(entity);

                    break;

            }


        }

        public void WarpSet(string Target, Vector2 Position, bool Animate = true)
        {

            type = moveType.to;

            locale = Target;

            position = Position;

            animate = Animate;

        }

        public static void Warp(GameLocation target, Character entity, Vector2 position, bool animate = true)
        {

            if (entity.currentLocation != null)
            {

                if (animate)
                {

                    Mod.instance.iconData.AnimateQuickWarp(entity.currentLocation, entity.Position, true);

                }

                entity.currentLocation.characters.Remove(entity);

            }

            entity.ResetActives(true);

            target.characters.Add(entity);

            entity.currentLocation = target;

            entity.Position = position;

            entity.SettleOccupied();

            if (animate)
            {

                Mod.instance.iconData.AnimateQuickWarp(entity.currentLocation, entity.Position);

            }

        }

        public void RemovalSet(string From)
        {

            type = moveType.from;

            locale = From;

        }

        public void RemoveAll(Character entity)
        {

            foreach (GameLocation location in (IEnumerable<GameLocation>)Game1.locations)
            {

                if (location.characters.Count > 0)
                {

                    if (location.characters.Contains(entity))
                    {

                        location.characters.Remove(entity);

                    }

                }

            }

            if (!Context.IsMainPlayer)
            {

                Mod.instance.characters.Remove(character);

            }

        }

    }

    public class CharacterDisposition
    {
        public int Age;
        public int Manners;
        public int SocialAnxiety;
        public int Optimism;
        public Gender Gender;
        public bool datable;
        public string Birthday_Season;
        public int Birthday_Day;
        public int id;
        public int speed;
    }

}
