using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Location;
using StardewValley.Locations;
using Microsoft.Xna.Framework;
using StardewValley.Tools;
using System.Reflection;
using StardewDruid.Journal;
using StardewDruid.Handle;

namespace StardewDruid.Dialogue
{
    public static class DialogueAdventure
    {

        public static string DialogueOption(CharacterHandle.characters character)
        {

            switch (character)
            {

                case CharacterHandle.characters.energies:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.swordMists))
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueAdventure.343.4");

                    }

                    return null;

                case CharacterHandle.characters.waves:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.411");

                case CharacterHandle.characters.monument_chaos:

                    return Mod.instance.Helper.Translation.Get("DialogueAdventure.379.7");

                case CharacterHandle.characters.star_altar:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.419") +
                        Mod.instance.Helper.Translation.Get("CharacterHandle.420");

                case CharacterHandle.characters.anvil:

                    if (RelicHandle.HasRelic(IconData.relics.druid_hammer))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.427");

                    }

                    return null;

                case CharacterHandle.characters.dragon_statue:

                    return Mod.instance.Helper.Translation.Get("DialogueAdventure.343.1");

                case CharacterHandle.characters.shrine_engine:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.323.17");

                case CharacterHandle.characters.PalBat:
                case CharacterHandle.characters.PalSlime:
                case CharacterHandle.characters.PalSpirit:
                case CharacterHandle.characters.PalGhost:
                case CharacterHandle.characters.PalSerpent:

                    return Mod.instance.Helper.Translation.Get("DialogueAdventure.386.1").Tokens(new { name = PalHandle.PalName(character), title = PalHandle.PalScheme(character) });


            }

            if (!Context.IsMainPlayer)
            {

                return null;

            }

            switch (character)
            {

                case CharacterHandle.characters.Effigy:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.questEffigy))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.381");

                    }

                    break;

                case CharacterHandle.characters.Jester:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.392");

                case CharacterHandle.characters.Buffin:

                    if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Buffin))
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueAdventure.379.3");

                    }

                    if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Jester))
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueAdventure.379.5");

                    }

                    break;

                case CharacterHandle.characters.Shadowtin:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.403");

                case CharacterHandle.characters.Blackfeather:

                    if (Mod.instance.questHandle.IsGiven(QuestHandle.questBlackfeather))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.323.3");

                    }

                    break;

                case CharacterHandle.characters.Aldebaran:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.343.8");

                case CharacterHandle.characters.recruit_one:
                case CharacterHandle.characters.recruit_two:
                case CharacterHandle.characters.recruit_three:
                case CharacterHandle.characters.recruit_four:

                    return Mod.instance.Helper.Translation.Get("DialogueAdventure.361.1");

                case CharacterHandle.characters.Marlon:

                    if (RelicHandle.HasRelic(IconData.relics.druid_hieress))
                    {

                        /*foreach (KeyValuePair<CharacterHandle.characters, RecruitHandle> recruitData in Mod.instance.save.recruits)
                        {

                            if (recruitData.Value.name == "Marlon" || recruitData.Value.name == "MarlonFay")
                            {

                                break;

                            }

                        }*/

                        return Mod.instance.Helper.Translation.Get("DialogueAdventure.377.1");

                    }

                    break;

                case CharacterHandle.characters.star_bench:

                    return Mod.instance.Helper.Translation.Get("DialogueAdventure.386.12");
            }

            return null;

        }

        public static DialogueSpecial DialogueGenerate(CharacterHandle.characters character, int index = 0 , int answer = 0)
        {

            switch (index)
            {

                default:
                case 0:

                    return DialogueStart(character, index, answer);

                case 1:

                    return DialogueNext(character, index, answer);

            }

        }

        public static DialogueSpecial DialogueStart(CharacterHandle.characters character, int index = 0, int answer = 0)
        {

            DialogueSpecial generate = new();

            switch (character)
            {

                case CharacterHandle.characters.Effigy:


                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.questEffigy))
                    {

                        break;

                    }

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.896");

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.track)
                    {

                        generate.responses.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.901"));

                        generate.leads.Add(1, 1);

                    }

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.roam)
                    {

                        generate.responses.Add(2, Mod.instance.Helper.Translation.Get("CharacterHandle.909"));

                        generate.leads.Add(2, 2);

                    }

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.home
                        && Mod.instance.characters[character].currentLocation.Name != CharacterHandle.CharacterLocation(CharacterHandle.locations.grove))
                    {

                        generate.responses.Add(3, Mod.instance.Helper.Translation.Get("CharacterHandle.919"));

                        generate.leads.Add(3, 3);

                    }

                    return generate;

                case CharacterHandle.characters.Jester:

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.approachJester))
                    {

                        break;

                    }

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.939"); ;

                    StardewDruid.Character.Character Jester = Mod.instance.characters[character];

                    if (Jester.modeActive != StardewDruid.Character.Character.mode.track)
                    {

                        generate.responses.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.944"));

                        generate.leads.Add(1, 1);

                    }

                    if (Jester.modeActive != StardewDruid.Character.Character.mode.roam)
                    {

                        generate.responses.Add(2, Mod.instance.Helper.Translation.Get("CharacterHandle.953"));

                        generate.leads.Add(2, 2);

                    }

                    if (Jester.modeActive != StardewDruid.Character.Character.mode.home
                        && Jester.currentLocation.Name != CharacterHandle.CharacterLocation(CharacterHandle.locations.grove))
                    {

                        generate.responses.Add(3, Mod.instance.Helper.Translation.Get("CharacterHandle.963"));

                        generate.leads.Add(3, 3);

                    }

                    if (Jester.modeActive == StardewDruid.Character.Character.mode.track && Mod.instance.questHandle.IsComplete(QuestHandle.questJester))
                    {

                        generate.responses.Add(4, Mod.instance.Helper.Translation.Get("DialogueAdventure.379.1"));

                        generate.leads.Add(4, 4);

                    }

                    return generate;

                case CharacterHandle.characters.Shadowtin:

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeFates))
                    {

                        break;

                    }

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.982");

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.track)
                    {

                        generate.responses.Add(1,Mod.instance.Helper.Translation.Get("CharacterHandle.987"));

                        generate.leads.Add(1, 1);

                    }

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.roam)
                    {

                        generate.responses.Add(2,Mod.instance.Helper.Translation.Get("CharacterHandle.996"));

                        generate.leads.Add(2, 2);

                    }

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.home
                        && Mod.instance.characters[character].currentLocation.Name != CharacterHandle.CharacterLocation(CharacterHandle.locations.grove))
                    {

                        generate.responses.Add(3,Mod.instance.Helper.Translation.Get("CharacterHandle.1006"));

                        generate.leads.Add(3, 3);

                    }

                    return generate;

                case CharacterHandle.characters.Blackfeather:

                    if (!Mod.instance.questHandle.IsGiven(QuestHandle.questBlackfeather))
                    {

                        break;

                    }

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.4");

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.track)
                    {

                        generate.responses.Add(1,Mod.instance.Helper.Translation.Get("CharacterHandle.323.5"));

                        generate.leads.Add(1, 1);

                    }

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.roam)
                    {

                        generate.responses.Add(2,Mod.instance.Helper.Translation.Get("CharacterHandle.323.6"));

                        generate.leads.Add(2, 2);

                    }

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.home)
                    {

                        generate.responses.Add(3,Mod.instance.Helper.Translation.Get("CharacterHandle.323.7"));

                        generate.leads.Add(3, 3);

                    }

                    return generate;


                case CharacterHandle.characters.Aldebaran:

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.343.9");

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.track)
                    {

                        generate.responses.Add(1,Mod.instance.Helper.Translation.Get("CharacterHandle.343.10"));

                        generate.leads.Add(1, 1);

                    }

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.roam)
                    {

                        generate.responses.Add(2,Mod.instance.Helper.Translation.Get("CharacterHandle.343.11"));

                        generate.leads.Add(2, 2);

                    }

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.home)
                    {

                        generate.responses.Add(3,Mod.instance.Helper.Translation.Get("CharacterHandle.343.12"));

                        generate.leads.Add(3, 3);

                    }

                    return generate;


                case CharacterHandle.characters.recruit_one:
                case CharacterHandle.characters.recruit_two:
                case CharacterHandle.characters.recruit_three:
                case CharacterHandle.characters.recruit_four:

                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.361.2").Tokens(new { name = Game1.player.Name, });

                    Mod.instance.characters[character].SwitchToMode(Character.Character.mode.limbo, Game1.player);

                    return generate;

                case CharacterHandle.characters.Marlon:


                    string whichMarlon = "Marlon";

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("FlashShifter.SVECode"))
                    {
                        whichMarlon = "MarlonFay";
                    }

                    StardewValley.NPC realMarlon = CharacterHandle.FindVillager(whichMarlon);

                    if (RecruitHandle.RecruitWitness(realMarlon))
                    {

                        Mod.instance.AddWitness(ReactionData.reactions.mists, whichMarlon);

                        generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.377.2");

                        break;

                    }

                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.377.3");

                    break;

                case CharacterHandle.characters.PalBat:
                case CharacterHandle.characters.PalSlime:
                case CharacterHandle.characters.PalSpirit:
                case CharacterHandle.characters.PalGhost:
                case CharacterHandle.characters.PalSerpent:

                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.386.2").Tokens(new { name = PalHandle.PalName(character), title = PalHandle.PalScheme(character) });

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.track)
                    {

                        generate.responses.Add(1,Mod.instance.Helper.Translation.Get("DialogueAdventure.386.3"));

                        generate.leads.Add(1, 1);

                    }

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.roam)
                    {

                        generate.responses.Add(2,Mod.instance.Helper.Translation.Get("DialogueAdventure.386.4"));

                        generate.leads.Add(2, 2);

                    }

                    if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.home)
                    {

                        generate.responses.Add(3,Mod.instance.Helper.Translation.Get("DialogueAdventure.386.5"));

                        generate.leads.Add(3, 3);

                    }

                    generate.responses.Add(4, Mod.instance.Helper.Translation.Get("DialogueAdventure.386.6"));

                    generate.leads.Add(4, 4);

                    return generate;

                case CharacterHandle.characters.energies:

                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.343.5");

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeWeald))
                    {

                        generate.responses.Add(1,Mod.instance.Helper.Translation.Get("DialogueAdventure.348.1"));

                        generate.leads.Add(1, 11);

                    }

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.swordMists))
                    {

                        generate.responses.Add(2,Mod.instance.Helper.Translation.Get("DialogueAdventure.343.6"));

                        generate.leads.Add(2, 12);

                    }

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeMists))
                    {

                        generate.responses.Add(3, Mod.instance.Helper.Translation.Get("DialogueAdventure.343.7"));

                        generate.leads.Add(3, 13);


                    }

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.swordStars))
                    {

                        generate.responses.Add(4, Mod.instance.Helper.Translation.Get("DialogueAdventure.343.8"));

                        generate.leads.Add(4, 14);

                    }

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.swordFates))
                    {

                        generate.responses.Add(5, Mod.instance.Helper.Translation.Get("DialogueAdventure.343.9"));

                        generate.leads.Add(5, 15);

                    }

                    return generate;

                case CharacterHandle.characters.waves:

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1018");

                    generate.responses.Add(1,Mod.instance.Helper.Translation.Get("CharacterHandle.1020"));

                    generate.leads.Add(1, 10);

                    return generate;

                case CharacterHandle.characters.Buffin:

                    if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Buffin))
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.379.4");

                        Mod.instance.characters[character].SwitchToMode(Character.Character.mode.home, Game1.player);

                        return generate;

                    }

                    if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Jester))
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.379.6");

                        Mod.instance.characters[character].SwitchToMode(Character.Character.mode.track, Game1.player);

                        return generate;

                    }

                    return generate;

                case CharacterHandle.characters.star_altar:

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1043") +
                        Mod.instance.Helper.Translation.Get("CharacterHandle.1044");

                    generate.responses.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.1046"));

                    generate.leads.Add(1, 10);

                    return generate;

                case CharacterHandle.characters.monument_chaos:

                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.379.8");

                    generate.responses.Add(1,Mod.instance.Helper.Translation.Get("DialogueAdventure.379.9"));

                    generate.leads.Add(1, 10);

                    return generate;

                case CharacterHandle.characters.dragon_statue:

                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.343.2");

                    generate.responses.Add(1, Mod.instance.Helper.Translation.Get("DialogueAdventure.343.3"));

                    generate.leads.Add(1, 10);

                    return generate;

                case CharacterHandle.characters.shrine_engine:

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.18");

                    generate.responses.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.323.19"));

                    generate.leads.Add(1, 10);

                    return generate;

                case CharacterHandle.characters.anvil:

                    Mod.instance.herbalHandle.ConvertGeodes();

                    return null;

                case CharacterHandle.characters.star_bench:

                    DruidJournal.openJournal(DruidJournal.journalTypes.goods);

                    return null;
            }

            return generate;

        }

        public static DialogueSpecial DialogueNext(CharacterHandle.characters character, int index = 0, int answer = 0)
        {

            DialogueSpecial generate = new();

            Vector2 groveWarp = new Vector2(19, 15) * 64;

            switch (answer)
            {

                case 1: // Follow player

                    switch (character)
                    {
                        default:
                        case CharacterHandle.characters.Effigy:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1076");

                            break;

                        case CharacterHandle.characters.Jester:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1082");

                            break;

                        case CharacterHandle.characters.Shadowtin:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1088");

                            break;

                        case CharacterHandle.characters.Blackfeather:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.8");

                            break;
                        case CharacterHandle.characters.Aldebaran:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.343.13");

                            break;

                        case CharacterHandle.characters.PalBat:
                        case CharacterHandle.characters.PalSlime:
                        case CharacterHandle.characters.PalSpirit:
                        case CharacterHandle.characters.PalSerpent:

                            generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.386.7").Tokens(new { name = PalHandle.PalName(character), title = PalHandle.PalScheme(character) });

                            if (Context.IsMainPlayer)
                            {

                                Mod.instance.characters[character].SwitchToMode(Character.Character.mode.track, Game1.player);

                            }
                            else
                            {

                                Mod.instance.dopplegangers[character].SwitchToMode(Character.Character.mode.track, Game1.player);

                            }

                            return generate;

                    }

                    Mod.instance.characters[character].SwitchToMode(Character.Character.mode.track, Game1.player);

                    return generate;

                case 2: // Work on farm

                    switch (character)
                    {
                        default:
                        case CharacterHandle.characters.Effigy:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1105");

                            break;

                        case CharacterHandle.characters.Jester:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1111");

                            if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Buffin))
                            {

                                Mod.instance.characters[CharacterHandle.characters.Buffin].SwitchToMode(Character.Character.mode.home, Game1.player);

                            }

                            break;

                        case CharacterHandle.characters.Shadowtin:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1117");

                            break;

                        case CharacterHandle.characters.Blackfeather:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.9");

                            break;

                        case CharacterHandle.characters.Aldebaran:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.343.14");

                            break;

                        case CharacterHandle.characters.PalBat:
                        case CharacterHandle.characters.PalSlime:
                        case CharacterHandle.characters.PalSpirit:
                        case CharacterHandle.characters.PalSerpent:

                            generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.386.8").Tokens(new { name = PalHandle.PalName(character), title = PalHandle.PalScheme(character) });

                            if (Context.IsMainPlayer)
                            {

                                Mod.instance.characters[character].SwitchToMode(Character.Character.mode.roam, Game1.player);

                            }
                            else
                            {

                                Mod.instance.dopplegangers[character].SwitchToMode(Character.Character.mode.roam, Game1.player);

                            }

                            return generate;

                    }

                    Mod.instance.characters[character].SwitchToMode(Character.Character.mode.roam, Game1.player);

                    return generate;

                case 3: // Return to grove

                    switch (character)
                    {
                        default:
                        case CharacterHandle.characters.Effigy:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1133");

                            break;

                        case CharacterHandle.characters.Jester:

                            if (Mod.instance.trackers.ContainsKey(CharacterHandle.characters.Buffin))
                            {

                                Mod.instance.characters[CharacterHandle.characters.Buffin].SwitchToMode(Character.Character.mode.home, Game1.player);

                            }

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1139");

                            break;

                        case CharacterHandle.characters.Shadowtin:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1145");

                            break;

                        case CharacterHandle.characters.Blackfeather:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.10");

                            break;

                        case CharacterHandle.characters.Aldebaran:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.343.15");

                            break;

                        case CharacterHandle.characters.PalBat:
                        case CharacterHandle.characters.PalSlime:
                        case CharacterHandle.characters.PalSpirit:
                        case CharacterHandle.characters.PalSerpent:

                            generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.386.9").Tokens(new { name = PalHandle.PalName(character), title = PalHandle.PalScheme(character) });

                            if (Context.IsMainPlayer)
                            {

                                Mod.instance.characters[character].SwitchToMode(Character.Character.mode.home, Game1.player);

                            }
                            else
                            {

                                Mod.instance.dopplegangers[character].SwitchToMode(Character.Character.mode.home, Game1.player);

                            }

                            return generate;

                    }

                    Mod.instance.characters[character].SwitchToMode(Character.Character.mode.home, Game1.player);

                    return generate;

                case 4:

                    switch (character)
                    {
                        // Summon Buffin
                        case CharacterHandle.characters.Jester:

                            generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.379.2");

                            if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Buffin))
                            {

                                Mod.instance.characters[CharacterHandle.characters.Buffin].SwitchToMode(Character.Character.mode.track, Game1.player);

                            }

                            break;

                        // Dismiss Pal
                        case CharacterHandle.characters.PalBat:
                        case CharacterHandle.characters.PalSlime:
                        case CharacterHandle.characters.PalSpirit:
                        case CharacterHandle.characters.PalGhost:
                        case CharacterHandle.characters.PalSerpent:

                            generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.386.10").Tokens(new { name = PalHandle.PalName(character), title = PalHandle.PalScheme(character) });

                            if (Context.IsMainPlayer)
                            {

                                Mod.instance.characters[character].SwitchToMode(Character.Character.mode.limbo, Game1.player);

                            }
                            else
                            {

                                Mod.instance.dopplegangers[character].SwitchToMode(Character.Character.mode.limbo, Game1.player);

                            }

                            return generate;

                    }

                    return generate;

                case 10:

                    Wand wand = new()
                    {
                        lastUser = Game1.player
                    };

                    wand.DoFunction(Game1.player.currentLocation, 0, 0, 0, Game1.player);

                    return generate;

                // Sacred Spring
                case 11:

                    SpellHandle springWarp = new(Mod.instance.locations[LocationHandle.druid_spring_name], new(27, 18), groveWarp) { type = SpellHandle.Spells.warp, scheme = IconData.schemes.white, displayFactor = 1 };

                    Mod.instance.spellRegister.Add(springWarp);

                    return generate;

                // Secluded Atoll
                case 12:

                    SpellHandle atollWarp = new(Mod.instance.locations[LocationHandle.druid_atoll_name], new(30, 21), groveWarp) { type = SpellHandle.Spells.warp, scheme = IconData.schemes.white, displayFactor = 1 };

                    Mod.instance.spellRegister.Add(atollWarp);

                    return generate;

                // Elder Graves
                case 13:

                    SpellHandle gravesWarp = new(Mod.instance.locations[LocationHandle.druid_graveyard_name], new Vector2(27, 17), groveWarp) { type = SpellHandle.Spells.warp, scheme = IconData.schemes.white, displayFactor = 0 };

                    Mod.instance.spellRegister.Add(gravesWarp);

                    return generate;

                // Chapel of the Stars
                case 14:

                    SpellHandle chapelWarp = new(Mod.instance.locations[LocationHandle.druid_chapel_name], new(26, 23), groveWarp) { type = SpellHandle.Spells.warp, scheme = IconData.schemes.white, displayFactor = 0 };

                    Mod.instance.spellRegister.Add(chapelWarp);

                    return generate;

                // Court of Fates and Chaos
                case 15:

                    SpellHandle courtWarp = new(Mod.instance.locations[LocationHandle.druid_court_name], new Vector2(34, 18), groveWarp) { type = SpellHandle.Spells.warp, scheme = IconData.schemes.white, displayFactor = 0 };

                    Mod.instance.spellRegister.Add(courtWarp);

                    return generate;

            }


            return generate;

        }

    }


}
