using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Location;
using StardewDruid.Location.Druid;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.Characters;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Locations;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace StardewDruid.Handle
{

    public class CompanionData
    {

        public CharacterHandle.characters character;

        public Character.Character.mode mode;

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

    public static class CharacterHandle
    {

        public enum locations
        {
            grove,
            farm,
            temple,
            sanctuary,
            chapel,
            vault,
            court,
            archaeum,
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
            anvil,

            attendant,
            spring_bench,
            spring_vintner,
            spring_packer,
            spring_steephouse,
            spring_batchhouse,
            spring_packhouse,

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

            bearrock,

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

            cairn_dragon,
            cairn_warrior,
            cairn_witches,

            // event
            disembodied,
            FirstFarmer,
            LadyBeyond,
            Marlon,
            Dwarf,
            Gunther,
            Dragon,
            Crowmother,
            Justiciar,
            Thanatoshi,
            Seafarer,
            SeafarerCaptain,
            SeafarerMate,
            DarkShooter,
            DarkRogue,
            DarkGoblin,
            Doja,
            Macarbi,
            CultWitch,
            Wizard,
            Witch,
            Linus,

            // NPC companion
            recruit_one,
            recruit_two,
            recruit_three,
            recruit_four,

            Caroline,
            Clint,
            Krobus,

            Veteran,
            Archaeologist,
            Bombmaker,
            Elementalist,
            Shapeshifter,
            Sorceress,
            Astarion,
            Sevinae,
            Lance,
            Sophia,
            Witcher,

            // Pals

            PalBat,
            PalSlime,
            PalSpirit,
            PalGhost,
            PalSerpent,

            // Honour guard
            HonourGuard,
            HonourCaptain,
            HonourKnight,

            // event monsters
            Spectre,
            WhiteSpectre,
            BoneWitch,
            PeatWitch,
            MoorWitch,
            Ghostking,

            Jellyfiend,
            PinkJellyfiend,
            BlueJellyfiend,
            Jellyking,

            Dustfiend,
            Firefiend,
            Etherfiend,

            // creatures
            Bat,
            BrownBat,
            RedBat,
            Batking,
            Batcleric,

            CorvidRook,
            CorvidCrow,
            CorvidRaven,
            CorvidMagpie,
            SeaGull,

            BlackCat,
            GingerCat,
            TabbyCat,
            
            RedFox,
            YellowFox,
            
            BlackBear,
            BrownBear,
            GreyBear,
            
            RedPanda,
            GoldPanda,
            TrashPanda,

            BrownWolf,
            BlueWolf,
            BlackWolf,
            RedWolf,

            BrownOwl,
            GreyOwl,

            Serpent,
            RiverSerpent,
            LavaSerpent,
            NightSerpent,
            Serpentking,


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

                // Pal titles

                case characters.PalBat:
                case characters.PalSlime:
                case characters.PalSpirit:
                case characters.PalSerpent:
                case characters.PalGhost:

                    return PalHandle.PalName(entity);

                default:

                    return entity.ToString();

            }

        }

        public static characters CharacterType(string name)
        {

            return Enum.Parse<characters>(name);

        }

        public static string CharacterTitle(characters entity)
        {
            switch (entity)
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

                // Pal titles

                case characters.PalBat:
                case characters.PalSlime:
                case characters.PalSpirit:
                case characters.PalGhost:
                case characters.PalSerpent:

                    return PalHandle.PalScheme(entity);

                default:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.24");

            }

        }

        public static bool CharacterSave(characters character)
        {

            switch (character)
            {

                case characters.Effigy:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.approachEffigy))
                    {
                        return true;
                    }

                    return false;

                case characters.Jester:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.approachJester))
                    {
                        return true;
                    }

                    return false;

                case characters.Shadowtin:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeFates))
                    {
                        return true;
                    }

                    return false;

                case characters.Blackfeather:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeEther))
                    {
                        return true;
                    }

                    return false;

                case characters.Aldebaran:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeBones))
                    {
                        return true;
                    }

                    return false;

                case characters.Revenant:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.swordStars))
                    {
                        return true;
                    }

                    return false;

                case characters.Buffin:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.questJester))
                    {

                        return true;

                    }

                    return false;

                case characters.Marlon:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.questRevenant))
                    {

                        return true;

                    }

                    return false;

                case characters.recruit_one:
                case characters.recruit_two:
                case characters.recruit_three:
                case characters.recruit_four:

                    if (Mod.instance.characters[character].modeActive == Character.Character.mode.recruit)
                    {

                        return true;

                    }

                    return false;

                case characters.PalBat:
                case characters.PalSlime:
                case characters.PalSpirit:
                case characters.PalSerpent:
                case characters.PalGhost:

                    if (Mod.instance.questHandle.IsGiven(QuestHandle.captures))
                    {

                        return true;

                    }

                    return false;


            }

            return false;

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

                            return new Vector2(40, 18) * 64;

                        case characters.Shadowtin:

                            return new Vector2(40, 17) * 64;

                        case characters.Jester:

                            return new Vector2(37, 18) * 64;

                        default:

                            return new Vector2(33 + Mod.instance.randomIndex.Next(10), 19 + Mod.instance.randomIndex.Next(5))*64;


                    }

                case locations.rest:

                    switch (entity)
                    {

                        case characters.Blackfeather:

                            return new Vector2(41, 12) * 64;

                        case characters.Shadowtin:

                            return new Vector2(38.5f, 14.5f) * 64;

                        case characters.Jester:

                            return new Vector2(41, 13) * 64;

                        case characters.Effigy:

                            return new Vector2(34, 13) * 64;

                        case characters.Revenant:
                        case characters.Marlon:

                            return new Vector2(20, 13) * 64;

                        case characters.Buffin:

                            return new Vector2(17, 16) * 64;

                        case characters.Aldebaran:

                            return new Vector2(24, 14) * 64;

                        case characters.PalBat:

                            return new Vector2(39f, 11.5f) * 64;

                        default:

                            return Vector2.Zero;

                    }

                case locations.chapel:

                    return new Vector2(27, 21) * 64;

                case locations.sanctuary:

                    switch (entity)
                    {

                        case characters.PalBat:

                            return new Vector2(12, 12) * 64;

                        case characters.PalSlime:

                            return new Vector2(12, 18) * 64;

                        case characters.PalSerpent:

                            return new Vector2(12, 24) * 64;

                        case characters.PalSpirit:

                            return new Vector2(40, 12) * 64;

                        case characters.PalGhost:

                            return new Vector2(40, 18) * 64;

                        default:

                            return new Vector2(28, 24) * 64;

                    }

                case locations.temple:

                    return new Vector2(26, 28) * 64;

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

                    if (tryVectors.Count > 0)
                    {

                        return tryVectors[Mod.instance.randomIndex.Next(tryVectors.Count)] * 64;

                    }

                    break;

            }

            return Vector2.Zero;

        }

        public static Character.Character.mode CharacterSaveMode(characters entity)
        {

            Character.Character.mode savemode =
                Mod.instance.save.companions.ContainsKey(entity.ToString()) ?
                Mod.instance.save.companions[entity.ToString()].mode :
                Character.Character.mode.home;

            return savemode;

        }

        public static string CharacterLocation(locations location)
        {

            switch (location)
            {

                case locations.court:

                    return LocationHandle.druid_court_name;

                case locations.grove:

                    return LocationHandle.druid_grove_name;

                case locations.chapel:

                    return LocationHandle.druid_chapel_name;

                case locations.sanctuary:

                    return LocationHandle.druid_sanctuary_name;

                case locations.farm:

                    return Game1.getFarm().Name;

            }

            return null;

        }

        public static Vector2 RoamTether(GameLocation location)
        {

            if (location is null)
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

            if (location is Sanctuary)
            {

                return CharacterStart(locations.sanctuary);

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

                    return locations.temple;

                case characters.Aldebaran:
                case characters.PalBat:
                case characters.PalSlime:
                case characters.PalSpirit:
                case characters.PalSerpent:
                case characters.PalGhost:

                    return locations.sanctuary;

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

                    return locations.sanctuary;

                default:

                    return locations.grove;

            }

        }

        public static void CharacterWarp(Character.Character entity, locations destination, bool instant = false)
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

        public static void CharacterLoad(characters character, Character.Character.mode mode)
        {

            if (!Context.IsMainPlayer)
            {

                return;

            }

            if (Mod.instance.characters.ContainsKey(character))
            {

                if (Mod.instance.characters[character].modeActive != mode)
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

                case characters.recruit_one:
                case characters.recruit_two:
                case characters.recruit_three:
                case characters.recruit_four:

                    return;

                default:

                    character = characters.Effigy;

                    Mod.instance.characters[character] = new Effigy(character);

                    break;

            }

            if (Mod.instance.characters[character] is Recruit recruit)
            {

                Mod.instance.dialogue[character] = new(character, recruit.villager);

            }
            else
            {
                
                Mod.instance.dialogue[character] = new(character);

            }

            Mod.instance.characters[character].NewDay();

            Mod.instance.characters[character].SwitchToMode(mode, Game1.player);

        }


        public static Texture2D CharacterTexture(characters character)
        {

            switch (character)
            {
                case characters.disembodied:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "DarkRogue.png"));

                // crows
                case characters.Rook:
                case characters.Crow:
                case characters.Raven:
                case characters.Magpie:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Corvid" + character.ToString() + ".png"));

                case characters.Jester:

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("neosinf.BlackJester") || Mod.instance.Config.enableGothic)
                    {

                        return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", character.ToString() + "Two.png"));

                    }

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", character.ToString() + ".png"));

                case characters.PalBat:
                case characters.PalSlime:
                case characters.PalSpirit:
                case characters.PalGhost:
                case characters.PalSerpent:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Bat.png"));

                case characters.Marlon:

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("neosinf.WitcherMarlon") || Mod.instance.Config.enableGothic)
                    {

                        return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Witcher.png"));

                    }

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", character.ToString() + ".png"));

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

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("Neosinf.BlackJester") || Mod.instance.Config.enableGothic)
                    {

                        return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "JesterPortraitTwo.png"));

                    }

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "JesterPortrait.png"));

                case characters.Buffin:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "BuffinPortrait.png"));

                case characters.Shadowtin:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "ShadowtinPortrait.png"));

                case characters.Blackfeather:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "BlackfeatherPortrait.png"));

                case characters.Aldebaran:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "AldebaranPortrait.png"));

                case characters.Gunther:
                case characters.Dwarf:
                case characters.Linus:
                case characters.Marlon:

                    return FindVillager(character.ToString()).Portrait;

                case characters.Wizard:
                case characters.Witch:

                    return FindVillager("Wizard").Portrait;

                case characters.PalBat:
                case characters.PalSlime:
                case characters.PalSpirit:
                case characters.PalGhost:
                case characters.PalSerpent:

                    return null;

                default:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "EffigyPortrait.png"));

            }

        }

        public static bool CharacterGone(characters character)
        {

            switch (character)
            {

                case characters.Effigy:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeBones))
                    {
                        return true;
                    }

                    break;

                case characters.Revenant:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.questRevenant))
                    {
                        return true;
                    }

                    break;

            }

            return false;

        }

        public static NPC FindVillager(string name)
        {

            foreach (GameLocation location in (IEnumerable<GameLocation>)Game1.locations)
            {

                if (location.characters.Count > 0)
                {

                    for (int c = location.characters.Count - 1; c >= 0; c--)
                    {

                        NPC npc = location.characters.ElementAt(c);

                        if (npc.Name == name)
                        {

                            return npc;

                        }

                    }

                }

            }

            return null;

        }

        public static CharacterDisposition CharacterDisposition(characters character)
        {

            CharacterDisposition disposition = new()
            {
                Age = 0,
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

            switch (character)
            {

                case characters.Revenant:
                    disposition.id += 1;
                    disposition.Birthday_Day = 15;
                    break;

                case characters.Jester:

                    disposition.id += 2;
                    disposition.Birthday_Season = "fall";
                    break;

                case characters.Buffin:
                    disposition.id += 3;
                    disposition.Birthday_Season = "spring";
                    break;

                case characters.Shadowtin:
                    disposition.id += 4;
                    disposition.Birthday_Season = "winter";

                    break;

                case characters.Blackfeather:

                    disposition.id += 5;
                    disposition.Birthday_Season = "fall";
                    disposition.Birthday_Day = 15;
                    break;

                case characters.Aldebaran:

                    disposition.id += 7;
                    break;

                case characters.Marlon:
                    disposition.id += 6;
                    disposition.Birthday_Day = 15;
                    break;

            }

            return disposition;

        }

        public static bool CharacterDialogue(Character.Character npc, Farmer farmer)
        {
            switch (npc.characterType)
            {
                
                // Companions
                case characters.Effigy:
                case characters.Revenant:
                case characters.Jester:
                case characters.Buffin:
                case characters.Shadowtin:
                case characters.Blackfeather:
                case characters.Aldebaran:
                case characters.Marlon:

                    if (!Mod.instance.dialogue.ContainsKey(npc.characterType))
                    {

                        Mod.instance.dialogue[npc.characterType] = new(npc.characterType)
                        {
                            npc = npc
                        };

                    }

                    npc.Halt();

                    Mod.instance.dialogue[npc.characterType].DialogueApproach();

                    return true;
                
                // Heroes
                case characters.recruit_one:
                case characters.recruit_two:
                case characters.recruit_three:
                case characters.recruit_four:
                // Pals
                case characters.PalBat:
                case characters.PalSlime:
                case characters.PalSpirit:
                case characters.PalGhost:
                case characters.PalSerpent:


                    if (!npc.localised)
                    {

                        return false;

                    }
                    else
                    if (!Mod.instance.dialogue.ContainsKey(npc.characterType))
                    {

                        Mod.instance.dialogue[npc.characterType] = new(npc.characterType)
                        {
                            npc = npc
                        };

                    }

                    npc.Halt();

                    Mod.instance.dialogue[npc.characterType].DialogueApproach();

                    return true;

            }

            return false;

        }

    }

}
