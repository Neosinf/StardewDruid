using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Location;
using StardewDruid.Location.Druid;
using StardewModdingAPI;
using StardewValley;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Locations;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static StardewDruid.Character.CharacterHandle;

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
            FirstFarmer,
            LadyBeyond,
            Dwarf,
            Dragon,
            Crowmother,
            Justiciar,
            Thanatoshi,
            Seafarer,
            DarkShooter,
            DarkRogue,
            DarkGoblin,
            Doja,
            Carnivellion,
            Macarbi,
            CultWitch,

            // NPC companion
            recruit_one,
            recruit_two,
            recruit_three,
            recruit_four,
            Wizard,
            Witch,
            Linus,
            Caroline,
            Clint,

            // Honour guard
            HonourGuard,
            HonourCaptain,
            HonourKnight,

            // event monsters
            Spectre,

            // animals
            Bat,
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
            YellowBear,
            GreyWolf,
            BlackWolf,
            BrownOwl,
            GreyOwl,

            Serpent,
            RiverSerpent,
            LavaSerpent,
            NightSerpent,

            // other stuff
            exit_moors,


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

            /*Dictionary<string, characters> types = new()
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
            }*/

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

        public static bool CharacterSave(characters character)
        {

            switch (character)
            {

                case characters.Effigy:
                case characters.Shadowtin:
                case characters.Jester:
                case characters.Blackfeather:
                case characters.Aldebaran:
                case characters.recruit_one:
                case characters.recruit_two:
                case characters.recruit_three:
                case characters.recruit_four:

                    return true;

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

                            return new Vector2(43, 18) * 64;

                        case characters.Shadowtin:

                            return new Vector2(43, 17) * 64;

                        case characters.Jester:

                            return new Vector2(40, 18) * 64;

                        default:

                            return new Vector2(40, 17) * 64;


                    }

                case locations.rest:

                    switch (entity)
                    {

                        case characters.Blackfeather:

                            return new Vector2(36, 15) * 64;

                        case characters.Shadowtin:

                            return new Vector2(1696, 896);

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
                        case characters.Blackfeather:

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

                    if (tryVectors.Count > 0)
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

                    return LocationHandle.druid_court_name;

                case locations.grove:

                    return LocationHandle.druid_grove_name;

                case locations.chapel:

                    return LocationHandle.druid_chapel_name;

                case locations.gate:

                    return LocationHandle.druid_gate_name;

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

            if (location is Gate)
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

            Mod.instance.dialogue[character] = new(character);

            Mod.instance.characters[character].NewDay();

            Mod.instance.characters[character].SwitchToMode(mode, Game1.player);

        }

        public static void RecruitLoad(characters type, bool suspend = false)
        {

            if (!Context.IsMainPlayer)
            {

                return;

            }

            RecruitData hero = Mod.instance.save.recruits[type];

            NPC witness;

            if (!Mod.instance.characters.ContainsKey(type))
            {

                witness = FindVillager(hero.name);

                Mod.instance.save.recruits[type].display = witness.displayName;

                switch (hero.name)
                {

                    case "Wizard":

                        if (Mod.instance.Helper.ModRegistry.IsLoaded("Nom0ri.RomRas"))
                        {

                            Mod.instance.characters[type] = new Witch(type, witness);

                        }
                        else
                        {

                            Mod.instance.characters[type] = new Wizard(type, witness);

                        }
                        break;

                    case "Linus":

                        Mod.instance.characters[type] = new Linus(type, witness);

                        break;

                    case "Caroline":

                        Mod.instance.characters[type] = new Caroline(type, witness);

                        break;

                    case "Clint":

                        Mod.instance.characters[type] = new Clint(type, witness);

                        break;

                    default:

                        Mod.instance.characters[type] = new Recruit(type, witness);

                        break;

                }

                Mod.instance.characters[type].NewDay();

            }
            else
            {

                witness = (Mod.instance.characters[type] as Recruit).villager;

            }

            if (!RecruitValid(witness))
            {

                RecruitRemove(type);

                return;

            }

            Mod.instance.dialogue[type] = new(type, witness);

            Mod.instance.characters[type].SwitchToMode(StardewDruid.Character.Character.mode.track, Game1.player);

            Mod.instance.trackers[type].suspended = suspend;

        }

        public static void RecruitRemove(characters type)
        {

            if (Mod.instance.characters.ContainsKey(type))
            {

                StardewDruid.Character.Character entity = Mod.instance.characters[type];

                entity.SwitchToMode(StardewDruid.Character.Character.mode.random, Game1.player);

                if (entity.currentLocation != null)
                {

                    entity.currentLocation.characters.Remove(entity);

                }

            }

            Mod.instance.characters.Remove(type);

            Mod.instance.save.characters.Remove(type);

            Mod.instance.save.recruits.Remove(type);

            Mod.instance.dialogue.Remove(type);

            Mod.instance.trackers.Remove(type);

        }

        public static bool RecruitValid(NPC villager)
        {

            if(villager.Schedule != null)
            {

                return true;

            }

            return RecruitHero(villager.Name);

        }

        public static bool RecruitHero(string name)
        {

            switch (name)
            {

                case "Wizard":
                case "Linus":
                case "Caroline":
                case "Clint":

                    return true;

            }

            return false;

        }

        public static string RecruitTitle(string name, string display)
        {

            switch (name)
            {

                case "Wizard":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.363.3").Tokens(new { name = display, }); //"Successor of Elements";

                case "Linus":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.363.4").Tokens(new { name = display, }); //"Successor of Wilds";

                case "Caroline":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.363.5").Tokens(new { name = display, });// "Frost Witch";

                case "Clint":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.363.6").Tokens(new { name = display, }); //"Hammer Lord";

            }

            return String.Empty;

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

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "DarkRogue.png"));

                // crows
                case characters.Rook:
                case characters.Crow:
                case characters.Raven:
                case characters.Magpie:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Corvid" + character.ToString() + ".png"));

                case characters.Jester:

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("Neosinf.BlackJester"))
                    {

                        return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", character.ToString() + "Two.png"));

                    }

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", character.ToString() + ".png"));

                case characters.recruit_one:
                case characters.recruit_two:
                case characters.recruit_three:
                case characters.recruit_four:

                    return Mod.instance.Helper.GameContent.Load<Texture2D>(Path.Combine("Characters", "Dwarf"));

                default:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", character.ToString() + ".png"));

                case characters.Dwarf:

                    return FindVillager("Dwarf").Sprite.Texture;

            }

        }

        public static Texture2D CharacterPortrait(characters character)
        {

            switch (character)
            {

                case characters.Revenant:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "RevenantPortrait.png"));

                case characters.Jester:

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("Neosinf.BlackJester"))
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

                case characters.Dwarf:
                case characters.Linus:
                case characters.Marlon:

                    return FindVillager(character.ToString()).Portrait;

                case characters.Wizard:
                case characters.Witch:

                    return FindVillager("Wizard").Portrait;

                default:

                    return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "EffigyPortrait.png"));

            }

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

        public static bool CharacterDialogue(StardewDruid.Character.Character npc, Farmer farmer)
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
                // Heroes
                case characters.recruit_one:
                case characters.recruit_two:
                case characters.recruit_three:
                case characters.recruit_four:

                    if (!Mod.instance.dialogue.ContainsKey(npc.characterType))
                    {

                        Mod.instance.dialogue[npc.characterType] = new(npc.characterType);

                        Mod.instance.dialogue[npc.characterType].npc = npc;

                    }

                    npc.Halt();

                    Mod.instance.dialogue[npc.characterType].DialogueApproach();

                    return true;

            }

            return false;

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

                            Item oldItem = new StardewValley.Object(item.id, item.stack, quality: item.quality);

                            if (oldItem != null)
                            {

                                newChest.Items.Add(oldItem);

                            }

                            continue;

                        }

                        ParsedItemData parsedItemData = ItemRegistry.GetData(item.id);

                        if (System.Object.ReferenceEquals(parsedItemData, null))
                        {

                            continue;

                        }

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
