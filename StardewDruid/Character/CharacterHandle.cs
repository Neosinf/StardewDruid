using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Cast.Ether;
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

        public static bool TryWarpBusStop()
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

                            return true;

                        }

                    }

                }

            }

            return false;

        }

        public static bool CommunityCheck(int area)
        {

            if(
                !Game1.MasterPlayer.mailReceived.Contains("JojaMember") && 
                !(Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[area]
            )
            {

                return true;

            }

            return false;

        }

        public static string CharacterTitle(characters character)
        {
            switch (character)
            {
                case characters.Shadowtin:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.8");

                case characters.Jester:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.12");

                case characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.16");

                case characters.Revenant:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.20");

                default:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.24");

            }

        }
        public static string DialogueApproach(characters character)
        {

            switch (character)
            {

                case characters.Effigy:

                    if (Mod.instance.save.milestone == QuestHandle.milestones.weald_weapon || Mod.instance.save.milestone == QuestHandle.milestones.mists_weapon)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.40");

                    }

                    if (Mod.instance.characters[characters.Effigy].currentLocation.IsFarm)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.47") +
                            Mod.instance.Helper.Translation.Get("CharacterHandle.48");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.52");

                case characters.Revenant:

                    if (Mod.instance.save.milestone == QuestHandle.milestones.stars_weapon)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.59");

                    }
                    return Mod.instance.Helper.Translation.Get("CharacterHandle.62");

                case characters.Jester:

                    if (Mod.instance.save.milestone == QuestHandle.milestones.fates_weapon)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.69");

                    }

                    if (Mod.instance.save.milestone == QuestHandle.milestones.fates_enchant)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.76");

                    }

                    if (Mod.instance.characters[characters.Jester].currentLocation.IsFarm)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.83");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.87");

                case characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.91");

                case characters.Shadowtin:

                    if (Mod.instance.characters[characters.Jester].currentLocation.IsFarm)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.98") +
                            Mod.instance.Helper.Translation.Get("CharacterHandle.99");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.103");

                case characters.energies:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.107");

                case characters.waves:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.111");

                case characters.herbalism:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.115");

                case characters.monument_artisans:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.119");

                case characters.monument_priesthood:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.123");

                case characters.monument_morticians:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.127");

                case characters.monument_chaos:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.131");

            }

            return null;

        }

        public static string DialogueNevermind(characters character)
        {


            return Mod.instance.Helper.Translation.Get("CharacterHandle.143");


        }

        public static string DialogueOption(characters character, subjects subject)
        {

            switch (subject)
            {

                case subjects.quests:

                    if (Mod.instance.questHandle.IsQuestGiver(character))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.159");

                    }

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

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.210");

                            }
                            else if (runestones >= 1)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.216");

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

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.236");

                            }
                            else if (avalant >= 1)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.242");

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

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.262");

                            }
                            else if (books >= 1)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.268");

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

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.288");

                            }
                            else if (boxes >= 1)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.294");

                            }

                            return null;

                        case characters.monument_artisans:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.302");

                        case characters.monument_priesthood:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.306");

                        case characters.monument_morticians:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.310");

                        case characters.monument_chaos:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.314");
                    }

                    break;

                case subjects.inventory:

                    switch (character)
                    {

                        case characters.Effigy:

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.questEffigy) && Context.IsMainPlayer)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.329");

                            }

                            break;

                        case characters.Jester:

                            if (Context.IsMainPlayer)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.340");

                            }

                            break;

                        case characters.Shadowtin:

                            if (Context.IsMainPlayer)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.351");

                            }

                            break;

                        case characters.herbalism:

                            if (Context.IsMainPlayer)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.362");

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

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.381");

                            }

                            break;

                        case characters.Jester:

                            if (Context.IsMainPlayer)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.392");

                            }

                            break;

                        case characters.Shadowtin:

                            if (Context.IsMainPlayer)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.403");

                            }

                            break;

                        case characters.waves:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.411");

                        case characters.Buffin:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.415");

                        case characters.Revenant:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.419") +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.420");

                        case characters.herbalism:

                            if (Mod.instance.save.reliquary.ContainsKey(IconData.relics.crow_hammer.ToString()))
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.427");

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

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.466");

                            }
                            if (Mod.instance.questHandle.IsGiven(QuestHandle.herbalism))
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.472");

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

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.551") +
                                    Mod.instance.Helper.Translation.Get("CharacterHandle.552");

                                if (CommunityCheck(1))
                                {

                                    Mod.instance.questHandle.AssignQuest(QuestHandle.relicWeald);

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.559"));

                                    generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.561") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.562"));

                                }
                                else
                                {

                                    Mod.instance.questHandle.CompleteQuest(QuestHandle.relicWeald);

                                }

                            }
                            else if (runestones >= 1)
                            {

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.576") +
                                    Mod.instance.Helper.Translation.Get("CharacterHandle.577");

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

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.597") +
                                    Mod.instance.Helper.Translation.Get("CharacterHandle.598");

                                if (CommunityCheck(2))
                                {
                                    Mod.instance.questHandle.AssignQuest(QuestHandle.relicMists);

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.604"));

                                    generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.606") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.607") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.608"));
                                }
                                else
                                {

                                    Mod.instance.questHandle.CompleteQuest(QuestHandle.relicMists);

                                }

                            }
                            else if (avalant >= 1)
                            {

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.621") +
                                    Mod.instance.Helper.Translation.Get("CharacterHandle.622") +
                                    Mod.instance.Helper.Translation.Get("CharacterHandle.623");

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

                                generate.intro = string.Empty;

                                if (CommunityCheck(0))
                                {

                                    Mod.instance.questHandle.AssignQuest(QuestHandle.relicEther);

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.650");

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.652"));

                                    generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.654"));

                                }
                                else
                                {

                                    Mod.instance.questHandle.CompleteQuest(QuestHandle.relicEther);

                                }

                                generate.intro += Mod.instance.Helper.Translation.Get("CharacterHandle.664") +
                                    Mod.instance.Helper.Translation.Get("CharacterHandle.665");

                            }
                            else if (books >= 1)
                            {

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.671");

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
                                generate.intro = string.Empty;

                                if (CommunityCheck(5))
                                {

                                    Mod.instance.questHandle.AssignQuest(QuestHandle.relicFates);

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.697");

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.699") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.700"));

                                    generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.702") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.703") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.704") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.705"));

                                }
                                else
                                {

                                    Mod.instance.questHandle.CompleteQuest(QuestHandle.relicFates);

                                }

                                generate.intro += Mod.instance.Helper.Translation.Get("CharacterHandle.715");

                            }
                            else if (boxes >= 1)
                            {

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.721") +
                                    Mod.instance.Helper.Translation.Get("CharacterHandle.722") +
                                    Mod.instance.Helper.Translation.Get("CharacterHandle.723") +
                                    Mod.instance.Helper.Translation.Get("CharacterHandle.724");

                            }

                            return generate;


                        case characters.monument_artisans:

                            switch (Mod.instance.relicsData.ArtisanRelicQuest())
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.738") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.739");

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.745");

                                    ThrowHandle throwNotes = new(Game1.player, Game1.player.Position + new Vector2(-64, -256), IconData.relics.box_artisan);

                                    throwNotes.register();

                                    break;

                                default:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.755");

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

                                Mod.instance.CastMessage(
                                    Mod.instance.Helper.Translation.Get("CharacterHandle.780")
                                    + faethBlessing.ToString() + Mod.instance.Helper.Translation.Get("CharacterHandle.781"));

                                Mod.instance.save.herbalism[HerbalData.herbals.faeth] = faethBlessing;

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.785");

                                return generate;

                            }

                            if (priestProgress >= 0)
                            {

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.794") +
                                    Mod.instance.Helper.Translation.Get("CharacterHandle.795");

                            }
                            else
                            {

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.801");

                            }

                            return generate;


                        case characters.monument_morticians:

                            switch (Mod.instance.relicsData.MorticianRelicQuest())
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.815");

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.821");

                                    ThrowHandle throwNotes = new(Game1.player, Game1.player.Position + new Vector2(64, -256), IconData.relics.box_mortician);

                                    throwNotes.register();
                                    break;
                                default:
                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.828");

                                    break;
                            }

                            return generate;

                        case characters.monument_chaos:

                            switch (Mod.instance.relicsData.ChaosRelicQuest())
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.842");

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.848");

                                    ThrowHandle throwNotes = new(Game1.player, Game1.player.Position + new Vector2(64, -256), IconData.relics.box_chaos);

                                    throwNotes.register();

                                    break;

                                default:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.858");

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

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.896");

                                    if (Mod.instance.characters[character].modeActive != Character.mode.track)
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.901"));

                                        generate.answers.Add(1.ToString());
                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.roam)
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.909"));

                                        generate.answers.Add(2.ToString());

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.home
                                        && Mod.instance.characters[character].currentLocation.Name != CharacterLocation(locations.grove))
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.919"));

                                        generate.answers.Add(3.ToString());

                                    }

                                    generate.lead = true;

                                    return generate;


                                case characters.Jester:

                                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.approachJester))
                                    {

                                        break;

                                    }

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.939"); ;

                                    if (Mod.instance.characters[character].modeActive != Character.mode.track)
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.944"));

                                        generate.answers.Add(1.ToString());

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.roam)
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.953"));

                                        generate.answers.Add(2.ToString());

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.home
                                        && Mod.instance.characters[character].currentLocation.Name != CharacterLocation(locations.grove))
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.963"));

                                        generate.answers.Add(3.ToString());

                                    }

                                    generate.lead = true;

                                    return generate;

                                case characters.Shadowtin:

                                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeFates))
                                    {

                                        break;

                                    }

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.982"); ;

                                    if (Mod.instance.characters[character].modeActive != Character.mode.track)
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.987"));

                                        generate.answers.Add(1.ToString());

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.roam)
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.996"));

                                        generate.answers.Add(2.ToString());

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.home
                                        && Mod.instance.characters[character].currentLocation.Name != CharacterLocation(locations.grove))
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.1006"));

                                        generate.answers.Add(3.ToString());

                                    }

                                    generate.lead = true;

                                    return generate;

                                case characters.waves:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1018");

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.1020"));

                                    generate.answers.Add(10.ToString());

                                    generate.lead = true;

                                    return generate;


                                case characters.Buffin:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1031");

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.1033"));

                                    generate.answers.Add(11.ToString());

                                    generate.lead = true;

                                    return generate;

                                case characters.Revenant:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1043") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1044");

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.1046"));

                                    generate.answers.Add(12.ToString());

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

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1076");

                                            break;

                                        case characters.Jester:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1082");

                                            break;

                                        case characters.Shadowtin:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1088");

                                            break;

                                    }

                                    Mod.instance.characters[character].SwitchToMode(Character.mode.track, Game1.player);

                                    return generate;

                                case 2: // Work on farm

                                    switch (character)
                                    {
                                        default:
                                        case characters.Effigy:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1105");

                                            break;

                                        case characters.Jester:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1111");

                                            break;

                                        case characters.Shadowtin:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1117");

                                            break;
                                    }

                                    Mod.instance.characters[character].SwitchToMode(Character.mode.roam, Game1.player);

                                    return generate;

                                case 3: // Return to grove

                                    switch (character)
                                    {
                                        default:
                                        case characters.Effigy:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1133");

                                            break;

                                        case characters.Jester:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1139");

                                            break;

                                        case characters.Shadowtin:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1145");

                                            break;
                                    }

                                    Mod.instance.characters[character].SwitchToMode(Character.mode.home, Game1.player);

                                    return generate;

                                case 10:
                                case 11:
                                case 12:

                                    if (index == 12)
                                    {

                                        if (TryWarpBusStop())
                                        {

                                            return generate;

                                        }


                                    }


                                    if (index == 11 && Mod.instance.randomIndex.Next(2) == 0)
                                    {

                                        for (int i = 0; i < 4; i++)
                                        {

                                            GameLocation location = Game1.locations.ElementAt(Mod.instance.randomIndex.Next(Game1.locations.Count()));

                                            Vector2 tile = location.getRandomTile();

                                            if (ModUtility.TileAccessibility(location, tile) != 0)
                                            {

                                                continue;

                                            }

                                            Mod.instance.iconData.ImpactIndicator(Game1.player.currentLocation, Game1.player.Position, IconData.impacts.bomb, 4, new());

                                            Game1.player.playNearbySoundAll(SpellHandle.sounds.wand.ToString());

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

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1239")
                                        + Game1.player.CurrentTool.Name + Mod.instance.Helper.Translation.Get("CharacterHandle.1240");

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1246")
                                        + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1248") +
                                    Mod.instance.Helper.Translation.Get("CharacterHandle.1249");

                                    break;

                                case 2:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1255")
                                        + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1257");

                                    break;

                                case 3:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1263")
                                        + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1265");

                                    break;

                            }

                            return generate;

                        case characters.waves:

                            attuneUpdate = AttunementUpdate(Rite.rites.mists);

                            switch (attuneUpdate)
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1282")
                                        + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1284");

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1290")
                                        + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1292") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1293");


                                    break;

                                case 2:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1300")
                                        + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1302");

                                    break;

                                case 3:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1308")
                                        + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1310");

                                    break;

                            }

                            return generate;

                        case characters.Revenant:

                            attuneUpdate = AttunementUpdate(Rite.rites.stars);

                            switch (attuneUpdate)
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1327") + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1328");

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1334") + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1335");

                                    break;

                                case 2:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1341") + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1342");

                                    break;

                                case 3:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1348") + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1349");

                                    break;

                            }

                            return generate;

                        case characters.Jester:

                            attuneUpdate = AttunementUpdate(Rite.rites.fates);

                            switch (attuneUpdate)
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1366") + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1367");

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1373") + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1374") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1375") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1376");

                                    break;

                                case 2:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1382") + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1383");

                                    break;

                                case 3:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1389") + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1390");

                                    break;

                            }

                            return generate;

                        case characters.Shadowtin:

                            attuneUpdate = AttunementUpdate(Rite.rites.ether);

                            switch (attuneUpdate)
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1407") + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1408");

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1414") +
                                                    Mod.instance.Helper.Translation.Get("CharacterHandle.1415");

                                    break;

                                case 2:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1421") + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1422");

                                    break;

                                case 3:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1428") + Game1.player.CurrentTool.Name +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1429");

                                    break;

                            }

                            return generate;


                        case characters.herbalism:

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.herbalism))
                            {

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1443");

                                Mod.instance.herbalData.MassBrew();

                                return generate;

                            }

                            switch (index)
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1456");

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.1458"));

                                    generate.answers.Add(0.ToString());

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.1462"));

                                    generate.answers.Add(1.ToString());

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.1466"));

                                    generate.answers.Add(2.ToString());

                                    generate.lead = true;

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1476") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1477") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1478") +
                                        Mod.instance.Helper.Translation.Get("CharacterHandle.1479");

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


                    return Mod.instance.Helper.Translation.Get("CharacterHandle.1557") +
                        Game1.player.CurrentTool.Name + Mod.instance.Helper.Translation.Get("CharacterHandle.1558");

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


                    return Mod.instance.Helper.Translation.Get("CharacterHandle.1577") +
                        Game1.player.CurrentTool.Name + Mod.instance.Helper.Translation.Get("CharacterHandle.1578");

                }

            }

            return Mod.instance.Helper.Translation.Get("CharacterHandle.1584") +
                Game1.player.CurrentTool.Name + Mod.instance.Helper.Translation.Get("CharacterHandle.1585") + compare.ToString();

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

                    Game1.player.currentLocation.playSound(SpellHandle.sounds.yoba.ToString());

                    Mod.instance.DetuneWeapon();

                    return 2;

                }

                //return 0;

            }

            Mod.instance.iconData.ImpactIndicator(Game1.player.currentLocation, Game1.player.Position, IconData.impacts.nature, 6f, new());

            Game1.player.currentLocation.playSound(SpellHandle.sounds.yoba.ToString());

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

            if(character == CharacterHandle.characters.Dragon)
            {

                RemoveDragons();

                return;

            }

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

        public void RemoveDragons(bool avatars = true)
        {

            List<Dragon> dragons = new();

            foreach (GameLocation location in (IEnumerable<GameLocation>)Game1.locations)
            {

                if (location.characters.Count > 0)
                {

                    for(int c = location.characters.Count - 1; c>=0;c--)
                    {

                        if(location.characters[c] is Dragon dragonCharacter)
                        {

                            if (avatars)
                            {
                                
                                if (!dragonCharacter.avatar)
                                {
                                    
                                    continue;
                                
                                }
                            
                            }

                            location.characters.Remove(dragonCharacter);

                        }

                    }

                }

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
