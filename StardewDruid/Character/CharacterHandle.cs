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

        public static string CharacterTitle(characters character)
        {
            switch (character)
            {
                case characters.Shadowtin:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.CharacterTitle.Shadowtin");

                case characters.Jester:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.CharacterTitle.Jester");

                case characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.CharacterTitle.Buffin");

                case characters.Revenant:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.CharacterTitle.Revenant");

                default:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.CharacterTitle.Effigy");

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

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.Effigy.0");

                    }

                    if (Mod.instance.characters[characters.Effigy].currentLocation.IsFarm)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.Effigy.1");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.Effigy.2");

                case characters.Revenant:

                    if (Mod.instance.save.milestone == QuestHandle.milestones.stars_weapon)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.Revenant.0");

                    }
                    return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.Revenant.1");

                case characters.Jester:

                    if (Mod.instance.save.milestone == QuestHandle.milestones.fates_weapon)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.Jester.0");

                    }

                    if (Mod.instance.save.milestone == QuestHandle.milestones.fates_enchant)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.Jester.1");

                    }

                    if (Mod.instance.characters[characters.Jester].currentLocation.IsFarm)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.Jester.2");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.Jester.3");

                case characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.Buffin.0");

                case characters.Shadowtin:

                    if (Mod.instance.characters[characters.Jester].currentLocation.IsFarm)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.Shadowtin.0");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.Shadowtin.1");

                case characters.energies:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.energies.0");

                case characters.waves:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.waves.0");

                case characters.herbalism:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.herbalism.0");

                case characters.monument_artisans:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.monument_artisans.0");

                case characters.monument_priesthood:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.monument_priesthood.0");

                case characters.monument_morticians:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.monument_morticians.0");

                case characters.monument_chaos:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueApproach.monument_chaos.0");

            }

            return null;

        }

        public static string DialogueNevermind(characters character)
        {


            return Mod.instance.Helper.Translation.Get("nevermind");


        }

        public static string DialogueOption(characters character, subjects subject)
        {

            switch (subject)
            {

                case subjects.quests:

                    if (Mod.instance.questHandle.IsQuestGiver(character))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.quests.0");

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

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.relics.energies.4");

                            }
                            else if (runestones >= 1)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.relics.energies.1");

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

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.relics.waves.6");

                            }
                            else if (avalant >= 1)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.relics.waves.1");

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

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.relics.Revenant.4");

                            }
                            else if (books >= 1)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.relics.Revenant.1");

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

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.relics.Buffin.4");

                            }
                            else if (boxes >= 1)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.relics.Buffin.1");

                            }

                            return null;

                        case characters.monument_artisans:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.relics.monument_artisans");

                        case characters.monument_priesthood:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.relics.monument_priesthood");

                        case characters.monument_morticians:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.relics.monument_morticians");

                        case characters.monument_chaos:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.relics.monument_chaos");
                    }

                    break;

                case subjects.inventory:

                    switch (character)
                    {

                        case characters.Effigy:

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.questEffigy) && Context.IsMainPlayer)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.inventory.Effigy");

                            }

                            break;

                        case characters.Jester:

                            if (Context.IsMainPlayer)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.inventory.Jester");

                            }

                            break;

                        case characters.Shadowtin:

                            if (Context.IsMainPlayer)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.inventory.Shadowtin");

                            }

                            break;

                        case characters.herbalism:

                            if (Context.IsMainPlayer)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.inventory.herbalism");

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

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.adventure.Effigy");

                            }

                            break;

                        case characters.Jester:

                            if (Context.IsMainPlayer)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.adventure.Jester");

                            }

                            break;                        
                        
                        case characters.Shadowtin:
                            
                            if (Context.IsMainPlayer)
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.adventure.Shadowtin");

                            }

                            break;

                        case characters.waves:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.adventure.waves");

                        case characters.Buffin:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.adventure.Buffin");

                        case characters.Revenant:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.adventure.Revenant");
                        case characters.herbalism:

                            if (Mod.instance.save.reliquary.ContainsKey(IconData.relics.crow_hammer.ToString()))
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.adventure.herbalism");

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

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.attune.herbalism.0");

                            }
                            if (Mod.instance.questHandle.IsGiven(QuestHandle.herbalism))
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueOption.attune.herbalism.1");

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

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.energies.4.intro");
                                if (!Game1.MasterPlayer.mailReceived.Contains("JojaMember") && !(Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[1])
                                {

                                    Mod.instance.questHandle.AssignQuest(QuestHandle.relicWeald);

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.energies.4.responses"));

                                    generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.energies.4.answers"));

                                }
                                else
                                {

                                    Mod.instance.questHandle.CompleteQuest(QuestHandle.relicWeald);

                                }

                            }
                            else if (runestones >= 1)
                            {

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.energies.1.intro");

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

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.waves.6.intro");

                                if (!Game1.MasterPlayer.mailReceived.Contains("JojaMember") && !(Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[2])
                                {
                                    Mod.instance.questHandle.AssignQuest(QuestHandle.relicMists);

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.waves.6.responses"));

                                    generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.waves.6.answers"));
                                }
                                else
                                {

                                    Mod.instance.questHandle.CompleteQuest(QuestHandle.relicMists);

                                }

                            }
                            else if (avalant >= 1)
                            {

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.waves.1.intro");

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

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.Revenant.4.intro.0");


                                if (!Game1.MasterPlayer.mailReceived.Contains("JojaMember") && !(Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[0])
                                {
                                    
                                    Mod.instance.questHandle.AssignQuest(QuestHandle.relicEther);

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.Revenant.4.intro.1");
                                    
                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.Revenant.4.responses"));

                                    generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.Revenant.4.answers"));

                                }
                                else
                                {

                                    Mod.instance.questHandle.CompleteQuest(QuestHandle.relicEther);

                                }

                                generate.intro += Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.Revenant.4.intro.2");

                            }
                            else if (books >= 1)
                            {

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.Revenant.1.intro");

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
                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.Buffin.4.intro.0");

                                if (!Game1.MasterPlayer.mailReceived.Contains("JojaMember") && !(Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[5])
                                {

                                    Mod.instance.questHandle.AssignQuest(QuestHandle.relicFates);

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.Buffin.4.intro.1");

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.Buffin.4.responses"));

                                    generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.Buffin.4.answers"));

                                }
                                else
                                {

                                    Mod.instance.questHandle.CompleteQuest(QuestHandle.relicFates);

                                }

                                generate.intro += Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.Buffin.4.intro.2");

                            }
                            else if (boxes >= 1)
                            {

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.Buffin.1.intro");

                            }

                            return generate;


                        case characters.monument_artisans:
                            
                            switch (Mod.instance.relicsData.ArtisanRelicQuest())
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.monument_artisans.0.intro");

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.monument_artisans.1.intro");

                                    ThrowHandle throwNotes = new(Game1.player, Game1.player.Position + new Vector2(-64, -256), IconData.relics.box_artisan);

                                    throwNotes.register();

                                    break;

                                default:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.monument_artisans.-1.intro");

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

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.monument_priesthood.intro");

                                return generate;

                            }

                            if (priestProgress >= 0)
                            {

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.monument_priesthood.0.intro");

                            }
                            else
                            {

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.monument_priesthood.-1.intro");

                            }

                            return generate;


                        case characters.monument_morticians:

                            switch (Mod.instance.relicsData.MorticianRelicQuest())
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.monument_morticians.0.intro");

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.monument_morticians.1.intro");

                                    ThrowHandle throwNotes = new(Game1.player, Game1.player.Position + new Vector2(64, -256), IconData.relics.box_mortician);

                                    throwNotes.register();
                                    break;
                                default:
                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.monument_morticians.-1.intro");

                                    break;
                            }

                            return generate;

                        case characters.monument_chaos:

                            switch (Mod.instance.relicsData.ChaosRelicQuest())
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.monument_chaos.0.intro");

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.monument_chaos.1.intro");

                                    ThrowHandle throwNotes = new(Game1.player, Game1.player.Position + new Vector2(64,-256), IconData.relics.box_chaos);

                                    throwNotes.register();

                                    break;

                                default:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.relics.monument_chaos.-1.intro");

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

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Effigy.intro");

                                    if (Mod.instance.characters[character].modeActive != Character.mode.track)
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Effigy.responses.0"));

                                        generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Effigy.answers.0"));
                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.roam)
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Effigy.responses.1"));

                                        generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Effigy.answers.1"));

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.home
                                        && Mod.instance.characters[character].currentLocation.Name != CharacterLocation(locations.grove))
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Effigy.responses.2"));

                                        generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Effigy.answers.2"));

                                    }

                                    generate.lead = true;

                                    return generate;                                
                                
                                
                                case characters.Jester:

                                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.approachJester))
                                    {

                                        break;

                                    }

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Jester.intro"); ;

                                    if (Mod.instance.characters[character].modeActive != Character.mode.track)
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Jester.responses.0"));

                                        generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Jester.answers.0"));

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.roam)
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Jester.responses.1"));

                                        generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Jester.answers.1"));

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.home
                                        && Mod.instance.characters[character].currentLocation.Name != CharacterLocation(locations.grove))
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Jester.responses.2"));

                                        generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Jester.answers.2"));

                                    }

                                    generate.lead = true;

                                    return generate;

                                case characters.Shadowtin:

                                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeFates))
                                    {

                                        break;

                                    }

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Shadowtin.intro"); ;

                                    if (Mod.instance.characters[character].modeActive != Character.mode.track)
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Shadowtin.responses.0"));

                                        generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Shadowtin.answers.0"));

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.roam)
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Shadowtin.responses.1"));

                                        generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Shadowtin.answers.1"));

                                    }

                                    if (Mod.instance.characters[character].modeActive != Character.mode.home
                                        && Mod.instance.characters[character].currentLocation.Name != CharacterLocation(locations.grove))
                                    {

                                        generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Shadowtin.responses.2"));

                                        generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Shadowtin.answers.2"));

                                    }

                                    generate.lead = true;

                                    return generate;

                                case characters.waves:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.waves.intro");

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.waves.responses"));

                                    generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.waves.answers"));

                                    generate.lead = true;

                                    return generate;


                                case characters.Buffin:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Buffin.intro");

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Buffin.responses"));

                                    generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Buffin.answers"));

                                    generate.lead = true;

                                    return generate;

                                case characters.Revenant:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Revenant.intro");

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Revenant.responses"));

                                    generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.0.Revenant.answers"));

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

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.1.Effigy.intro");

                                            break;

                                        case characters.Jester:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.1.Jester.intro");

                                            break;

                                        case characters.Shadowtin:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.1.Shadowtin.intro");

                                            break;

                                    }

                                    Mod.instance.characters[character].SwitchToMode(Character.mode.track, Game1.player);

                                    return generate;

                                case 2: // Work on farm

                                    switch (character)
                                    {
                                        default:
                                        case characters.Effigy:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.2.Effigy.intro");

                                            break;

                                        case characters.Jester:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.2.Jester.intro");

                                            break;

                                        case characters.Shadowtin:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.2.Shadowtin.intro");

                                            break;
                                    }

                                    Mod.instance.characters[character].SwitchToMode(Character.mode.roam, Game1.player);

                                    return generate;

                                case 3: // Return to grove

                                    switch (character)
                                    {
                                        default:
                                        case characters.Effigy:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.3.Effigy.intro");

                                            break;

                                        case characters.Jester:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.3.Jester.intro");

                                            break;

                                        case characters.Shadowtin:

                                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.adventure.3.Shadowtin.intro");

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

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.energies.0.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.energies.1.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                                case 2:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.energies.2.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                                case 3:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.energies.3.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                            }

                            return generate;

                        case characters.waves:

                            attuneUpdate = AttunementUpdate(Rite.rites.mists);

                            switch (attuneUpdate)
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.waves.0.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.waves.1.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});


                                    break;

                                case 2:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.waves.2.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                                case 3:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.waves.3.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                            }

                            return generate;

                        case characters.Revenant:

                            attuneUpdate = AttunementUpdate(Rite.rites.stars);

                            switch (attuneUpdate)
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.Revenant.0.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.Revenant.1.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                                case 2:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.Revenant.2.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                                case 3:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.Revenant.3.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                            }

                            return generate;

                        case characters.Jester:

                            attuneUpdate = AttunementUpdate(Rite.rites.fates);

                            switch (attuneUpdate)
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.Jester.0.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.Jester.1.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                                case 2:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.Jester.2.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                                case 3:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.Jester.3.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                            }

                            return generate;

                        case characters.Shadowtin:

                            attuneUpdate = AttunementUpdate(Rite.rites.ether);

                            switch (attuneUpdate)
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.Shadowtin.0.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.Shadowtin.1.intro");

                                    break;

                                case 2:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.Shadowtin.2.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                                case 3:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.Shadowtin.3.intro").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                                    break;

                            }

                            return generate;


                        case characters.herbalism:

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.herbalism))
                            {

                                generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.herbalism.intro");

                                Mod.instance.herbalData.MassBrew();

                                return generate;

                            }

                            switch (index)
                            {

                                case 0:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.herbalism.0.intro");

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.herbalism.0.responses.0"));

                                    generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.herbalism.0.answers.0"));

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.herbalism.0.responses.1"));

                                    generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.herbalism.0.answers.1"));

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.herbalism.0.responses.2"));

                                    generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.herbalism.0.answers.2"));

                                    generate.lead = true;

                                    break;

                                case 1:

                                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.DialogueGenerator.attune.herbalism.1.intro");

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


                    return Mod.instance.Helper.Translation.Get("CharacterHandle.AttunementIntro.0").Tokens(new{toolName=Game1.player.CurrentTool.Name});

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


                    return Mod.instance.Helper.Translation.Get("CharacterHandle.AttunementIntro.1").Tokens(new{toolName=Game1.player.CurrentTool.Name});

                }

            }

            return Mod.instance.Helper.Translation.Get("CharacterHandle.AttunementIntro.2").Tokens(new{compare=compare.ToString(),toolName=Game1.player.CurrentTool.Name});

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
