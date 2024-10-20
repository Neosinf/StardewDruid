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
using StardewValley.Characters;
using StardewValley.Delegates;
using StardewValley.GameData.Minecarts;
using StardewValley.ItemTypeDefinitions;
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
using System.Xml.Linq;
using xTile.Dimensions;
using xTile.Tiles;

namespace StardewDruid.Character
{

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
            gate,
            rest,
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
            Blackfeather,
            Aldebaran,

            // crows
            Rook,
            Crow,
            Raven,
            Magpie,

            // map interaction
            energies,
            herbalism,

            attendant,
            
            waves,

            keeper,
            epitaph_prince,
            epitaph_isles,
            epitaph_knoll,
            epitaph_servants_oak,
            epitaph_servants_holly,
            epitaph_kings_oak,
            epitaph_kings_holly,
            epitaph_guardian,
            epitaph_dragon,

            star_altar,
            star_desk,
            star_bench,

            monument_artisans,
            monument_priesthood,
            monument_morticians,
            monument_chaos,

            engraving_left,
            engraving_right,
            dragon_statue,

            shrine_engine,
            shrine_forge,
            shrine_desk,
            shrine_locker,
            shrine_shelf,

            crow_brazier,
            crow_gate,

            // event
            disembodied,
            Marlon,
            Gunther,
            Wizard,
            Witch,
            FirstFarmer,
            LadyBeyond,
            Dwarf,
            Dragon,
            Crowmother,
            Paladin,
            Justiciar,
            Thanatoshi,
            Seafarer,
            Linus,
            DarkShooter,
            DarkRogue,
            DarkGoblin,
            Doja,
            Carnivellion,
            Macarbi,

            // event monsters
            Spectre,

            // animals
            ShadowBat,
            ShadowRook,
            ShadowCrow,
            ShadowRaven,
            ShadowMagpie,

            BlackCat,
            GingerCat,
            TabbyCat,
            RedFox,
            YellowFox,
            BlackBear,
            BrownBear,
            GreyWolf,
            BlackWolf,
            BrownOwl,
            GreyOwl,


        }

        public enum subjects
        {
            quests,
            lore,
            relics,
            inventory,
            adventure,
            warp,
            attune,
            offering,
        }

        public static string CharacterName(characters entity)
        {

            switch (entity)
            {

                case characters.Effigy:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.311.1");

                case characters.Revenant:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.311.2");

                case characters.Jester:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.311.3");

                case characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.311.4");

                case characters.Shadowtin:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.311.5");

                case characters.Blackfeather:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.315.1");

                case characters.Marlon:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.343.1");

                case characters.Aldebaran:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.343.2");

                default:

                    return entity.ToString();

            }

        }

        public static characters CharacterType(string name)
        {

            Dictionary<string, characters> types = new()
            {
                [Mod.instance.Helper.Translation.Get("CharacterHandle.311.1")] = characters.Effigy,
                [Mod.instance.Helper.Translation.Get("CharacterHandle.311.2")] = characters.Revenant,
                [Mod.instance.Helper.Translation.Get("CharacterHandle.311.3")] = characters.Jester,
                [Mod.instance.Helper.Translation.Get("CharacterHandle.311.4")] = characters.Buffin,
                [Mod.instance.Helper.Translation.Get("CharacterHandle.311.5")] = characters.Shadowtin,
                [Mod.instance.Helper.Translation.Get("CharacterHandle.315.1")] = characters.Blackfeather,
                [Mod.instance.Helper.Translation.Get("CharacterHandle.343.1")] = characters.Marlon,
                [Mod.instance.Helper.Translation.Get("CharacterHandle.343.2")] = characters.Aldebaran,
            };

            if (types.ContainsKey(name))
            { 
                
                return types[name];
            }

            return Enum.Parse<CharacterHandle.characters>(name);

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

                case characters.Blackfeather:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.315.2");
                
                case characters.keeper:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.25");

                case characters.Marlon:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.343.3");

                case characters.Aldebaran:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.343.4");

                default:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.24");

            }

        }

        public static Vector2 CharacterStart(locations location, characters entity = characters.none)
        {

            switch (location)
            {
                case locations.court:

                    return new Vector2(18, 18) * 64;

                case locations.archaeum:

                    return new Vector2(26, 15) * 64;

                case locations.grove:

                    switch (entity)
                    {

                        case characters.Blackfeather:

                            return new Vector2(43, 17) * 64;

                        case characters.Shadowtin:

                            return new Vector2(43, 15) * 64;

                        case characters.Jester:

                            return new Vector2(40, 17) * 64;

                        default:

                            return new Vector2(40, 15) * 64;


                    }

                case locations.rest:

                    switch (entity)
                    {

                        case characters.Blackfeather:

                            return new Vector2(36, 15) * 64;

                        case characters.Shadowtin:

                            return new Vector2(1696,896);

                        case characters.Jester:

                            return new Vector2(36, 16) * 64;

                        case characters.Effigy:

                            return new Vector2(31, 15) * 64;

                        case characters.Revenant:
                        case characters.Marlon:

                            return new Vector2(20, 13) * 64;

                        case characters.Buffin:

                            return new Vector2(17, 16) * 64;

                        case characters.Aldebaran:

                            return new Vector2(25, 14) * 64;

                        default:

                            return Vector2.Zero;

                    }

                case locations.chapel:

                    return new Vector2(27, 21) * 64;

                case locations.gate:

                    switch (entity)
                    {
                        case characters.Buffin:

                            return new Vector2(27, 17) * 64;

                        case characters.Aldebaran:

                            return new Vector2(29, 17) * 64;

                    }

                    break;

                case locations.farm:

                    Vector2 farmTry;

                    GameLocation farm = Game1.getFarm();

                    FarmHouse homeOfFarmer = Utility.getHomeOfFarmer(Game1.player);

                    if (homeOfFarmer != null)
                    {
                        
                        Point frontDoorSpot = homeOfFarmer.getFrontDoorSpot();

                        farmTry = frontDoorSpot.ToVector2() + new Vector2(0, 2);

                    } 
                    else
                    {

                        farmTry = WarpData.WarpTiles(farm);

                    }

                    List<Vector2> tryVectors = ModUtility.GetOccupiableTilesNearby(farm, farmTry, -1, 0, 2);

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

                case locations.gate:

                    return LocationData.druid_gate_name;

                case locations.farm:

                    return Game1.getFarm().Name;

            }

            return null;

        }

        public static Vector2 RoamTether(GameLocation location)
        {

            if(location is null)
            {

                return Vector2.Zero;

            }

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

            if(location is Gate)
            {

                return CharacterStart(locations.gate);

            }

            return new(location.map.Layers[0].LayerWidth / 2, location.map.Layers[0].LayerHeight / 2);

        }

        public static locations CharacterHome(characters entity)
        {

            if (Mod.instance.magic)
            {

                return locations.farm;

            }

            switch (entity)
            {

                case characters.Buffin:

                    return locations.court;

                case characters.Revenant:
                case characters.Marlon:

                    return locations.chapel;

                case characters.Blackfeather:
                case characters.Aldebaran:

                    return locations.gate;

                default:

                    return locations.grove;

            }

        }
        
        public static locations CharacterRest(characters entity)
        {

            if (Mod.instance.magic)
            {

                return locations.farm;

            }

            switch (entity)
            {

                case characters.Buffin:

                    return locations.court;

                case characters.Revenant:
                case characters.Marlon:

                    return locations.chapel;

                case characters.Aldebaran:

                    return locations.gate;

                default:

                    return locations.grove;

            }

        }
        
        public static void CharacterWarp(Character entity, locations destination, bool instant = false)
        {

            string destiny = CharacterLocation(destination);

            Vector2 position = CharacterStart(destination, entity.characterType);

            CharacterMover mover = new(entity, destiny, position, true);

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

                case characters.Blackfeather:

                    Mod.instance.characters[character] = new Blackfeather(character);

                    break;

                case characters.Marlon:

                    Mod.instance.characters[character] = new Marlon(character);

                    break;

                case characters.Aldebaran:

                    Mod.instance.characters[character] = new Aldebaran(character);

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
                case characters.attendant:
                case characters.waves:
                case characters.herbalism:
                case characters.monument_artisans:
                case characters.monument_priesthood:
                case characters.monument_morticians:
                case characters.monument_chaos:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images","DarkRogue.png"));

                case characters.Dwarf:

                    return Mod.instance.Helper.GameContent.Load<Texture2D>(Path.Combine("Characters", "Dwarf"));

                case characters.ShadowBat:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Batwing.png"));

                // crows
                case characters.ShadowRook:
                case characters.ShadowCrow:
                case characters.ShadowRaven:
                case characters.ShadowMagpie:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Corvid" + character.ToString().Replace("Shadow","") + ".png"));

                // crows
                case characters.Rook:
                case characters.Crow:
                case characters.Raven:
                case characters.Magpie:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Corvid" + character.ToString() + ".png"));

                default:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", character.ToString() + ".png"));

            }

        }

        public static Texture2D CharacterPortrait(characters character)
        {

            switch (character)
            {

                case characters.Revenant:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "RevenantPortrait.png"));

                case characters.Jester:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "JesterPortrait.png"));

                case characters.Buffin:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "BuffinPortrait.png"));

                case characters.Shadowtin:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "ShadowtinPortrait.png"));

                case characters.Blackfeather:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "BlackfeatherPortrait.png"));

                case characters.Aldebaran:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "AldebaranPortrait.png"));

                case characters.Dwarf:

                    return Mod.instance.Helper.GameContent.Load<Texture2D>(Path.Combine("Portraits", "Dwarf"));
                    
                case characters.Linus:

                    return Mod.instance.Helper.GameContent.Load<Texture2D>(Path.Combine("Portraits", "Linus"));

                case characters.Marlon:

                    return Mod.instance.Helper.GameContent.Load<Texture2D>(Path.Combine("Portraits", "Marlon"));

                case characters.Wizard:
                case characters.Witch:
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

            if (character == characters.Blackfeather)
            {

                disposition.id += 5;
                disposition.Birthday_Season = "fall";
                disposition.Birthday_Day = 15;

            }

            if (character == characters.Marlon)
            {

                disposition.id += 6;
                disposition.Birthday_Day = 15;

            }

            if (character == characters.Aldebaran)
            {

                disposition.id += 7;

            }


            return disposition;

        }

        public static string DialogueOption(characters character, subjects subject)
        {


            switch (subject)
            {

                case subjects.quests:

                    if (!Context.IsMainPlayer)
                    {
                        return null;

                    }

                    if (Mod.instance.questHandle.IsQuestGiver(character))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.159");

                    }

                    return null;

                case subjects.lore:

                    return LoreData.LoreOption(character);
                
                case subjects.inventory:

                    if (!Context.IsMainPlayer)
                    {
                        return null;

                    }
                    switch (character)
                    {

                        case characters.Effigy:

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.questEffigy))
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.329");

                            }

                            break;

                        case characters.Jester:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.340");

                        case characters.Shadowtin:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.351");


                        case characters.Blackfeather:

                            if (Mod.instance.questHandle.IsGiven(QuestHandle.questBlackfeather))
                            {

                                return Mod.instance.Helper.Translation.Get("CharacterHandle.323.2");

                            }

                            break;

                        case characters.herbalism:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.362");


                        case characters.Aldebaran:

                            return Mod.instance.Helper.Translation.Get("CharacterHandle.343.7");


                    }

                    break;

                case subjects.relics:

                    return Dialogue.DialogueRelics.DialogueOption(character);

                case subjects.adventure:

                    return DialogueAdventure.DialogueOption(character);

                case subjects.attune:

                    return DialogueAttune.DialogueOption(character);

                case subjects.offering:

                    return DialogueOffering.DialogueOption(character);

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

                    List<LoreStory> stories = LoreData.RetrieveLore(character);

                    foreach (LoreStory story in stories)
                    {
                        generate.intro = LoreData.LoreIntro(character);

                        generate.responses.Add(story.question);

                        generate.answers.Add(story.answer);

                    }

                    break;
                
                case subjects.inventory:

                    switch (character)
                    {

                        case characters.Aldebaran:

                            OpenInventory(characters.Effigy);

                            break;

                        default:

                            OpenInventory(character);

                            break;

                    };

                    return null;

                case subjects.relics:

                    return Dialogue.DialogueRelics.DialogueGenerate(character, index, answer);

                case subjects.adventure:

                    return Dialogue.DialogueAdventure.DialogueGenerate(character, index, answer);

                case subjects.attune:

                    return Dialogue.DialogueAttune.DialogueGenerate(character, index, answer);

                case subjects.offering:

                    return Dialogue.DialogueOffering.DialogueGenerate(character, index, answer);

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

                        string QualifiedId = ItemRegistry.QualifyItemId(item.id);

                        if (QualifiedId == null)
                        {
                            
                            newChest.Items.Add(new StardewValley.Object(item.id, item.stack, quality: item.quality));

                            continue;

                        }

                        ParsedItemData parsedItemData = ItemRegistry.GetData(item.id);

                        if (parsedItemData.ItemType is ToolDataDefinition toolDefinition)
                        {

                            continue;

                        }

                        newChest.Items.Add(ItemRegistry.Create(QualifiedId, amount: item.stack, quality: item.quality));

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


    }

}
