using StardewDruid.Journal;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewDruid.Character;
using StardewModdingAPI;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Location;
using StardewValley.Locations;
using Microsoft.Xna.Framework;
using StardewValley.Tools;
using System.Reflection;

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

                case CharacterHandle.characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.415");

                //case CharacterHandle.characters.Revenant:
                //case CharacterHandle.characters.Marlon:
                case CharacterHandle.characters.star_altar:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.419") +
                        Mod.instance.Helper.Translation.Get("CharacterHandle.420");

                case CharacterHandle.characters.herbalism:

                    if (Journal.RelicData.HasRelic(IconData.relics.crow_hammer))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.427");

                    }

                    return null;

                case CharacterHandle.characters.dragon_statue:

                    return Mod.instance.Helper.Translation.Get("DialogueAdventure.343.1");

                case CharacterHandle.characters.shrine_engine:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.323.17");

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


            }

            return null;

        }

        public static DialogueSpecial DialogueGenerate(CharacterHandle.characters character, int index = 0 , int answer = 0)
        {

            DialogueSpecial generate = new();

            switch (index)
            {

                case 0:

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

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.901"));

                                generate.answers.Add(1.ToString());
                            }

                            if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.roam)
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.909"));

                                generate.answers.Add(2.ToString());

                            }

                            if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.home
                                && Mod.instance.characters[character].currentLocation.Name != CharacterHandle.CharacterLocation(CharacterHandle.locations.grove))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.919"));

                                generate.answers.Add(3.ToString());

                            }

                            generate.lead = true;

                            return generate;

                        case CharacterHandle.characters.Jester:

                            if (!Mod.instance.questHandle.IsComplete(QuestHandle.approachJester))
                            {

                                break;

                            }

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.939"); ;

                            if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.track)
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.944"));

                                generate.answers.Add(1.ToString());

                            }

                            if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.roam)
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.953"));

                                generate.answers.Add(2.ToString());

                            }

                            if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.home
                                && Mod.instance.characters[character].currentLocation.Name != CharacterHandle.CharacterLocation(CharacterHandle.locations.grove))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.963"));

                                generate.answers.Add(3.ToString());

                            }

                            generate.lead = true;

                            return generate;

                        case CharacterHandle.characters.Shadowtin:

                            if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeFates))
                            {

                                break;

                            }

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.982"); ;

                            if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.track)
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.987"));

                                generate.answers.Add(1.ToString());

                            }

                            if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.roam)
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.996"));

                                generate.answers.Add(2.ToString());

                            }

                            if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.home
                                && Mod.instance.characters[character].currentLocation.Name != CharacterHandle.CharacterLocation(CharacterHandle.locations.grove))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.1006"));

                                generate.answers.Add(3.ToString());

                            }

                            generate.lead = true;

                            return generate;

                        case CharacterHandle.characters.Blackfeather:

                            if (!Mod.instance.questHandle.IsGiven(QuestHandle.questBlackfeather))
                            {

                                break;

                            }

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.4"); ;

                            if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.track)
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.323.5"));

                                generate.answers.Add(1.ToString());

                            }

                            if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.roam)
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.323.6"));

                                generate.answers.Add(2.ToString());

                            }

                            if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.home
                                && Mod.instance.characters[character].currentLocation.Name != CharacterHandle.CharacterLocation(CharacterHandle.locations.gate))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.323.7"));

                                generate.answers.Add(3.ToString());

                            }

                            generate.lead = true;

                            return generate;


                        case CharacterHandle.characters.Aldebaran:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.343.9"); ;

                            if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.track)
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.343.10"));

                                generate.answers.Add(1.ToString());

                            }

                            if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.roam)
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.343.11"));

                                generate.answers.Add(2.ToString());

                            }

                            if (Mod.instance.characters[character].modeActive != StardewDruid.Character.Character.mode.home
                                && Mod.instance.characters[character].currentLocation.Name != CharacterHandle.CharacterLocation(CharacterHandle.locations.gate))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.343.12"));

                                generate.answers.Add(3.ToString());

                            }

                            generate.lead = true;

                            return generate;


                        case CharacterHandle.characters.energies:

                            generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.343.5");

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeWeald))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueAdventure.348.1"));

                                generate.answers.Add(11.ToString());

                            }

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.swordMists))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueAdventure.343.6"));

                                generate.answers.Add(12.ToString());

                            }

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeMists))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueAdventure.343.7"));

                                generate.answers.Add(13.ToString());

                            }

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.swordStars))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueAdventure.343.8"));

                                generate.answers.Add(14.ToString());

                            }

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.swordFates))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueAdventure.343.9"));

                                generate.answers.Add(15.ToString());

                            }

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.swordEther))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueAdventure.343.10"));

                                generate.answers.Add(16.ToString());

                            }

                            if (Mod.instance.questHandle.IsComplete(QuestHandle.questShadowtin))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueAdventure.343.11"));

                                generate.answers.Add(17.ToString());

                            }

                            generate.lead = true;

                            return generate;

                        case CharacterHandle.characters.waves:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1018");

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.1020"));

                            generate.answers.Add(10.ToString());

                            generate.lead = true;

                            return generate;

                        case CharacterHandle.characters.Buffin:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1031");

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.1033"));

                            generate.answers.Add(10.ToString());

                            generate.lead = true;

                            return generate;

                        case CharacterHandle.characters.star_altar:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1043") +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.1044");

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.1046"));

                            generate.answers.Add(10.ToString());

                            generate.lead = true;

                            return generate;

                        /*case CharacterHandle.characters.star_altar:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.343.16") +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.343.17");

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.343.18"));

                            generate.answers.Add(10.ToString());

                            generate.lead = true;

                            return generate;*/

                        case CharacterHandle.characters.dragon_statue:

                            generate.intro = Mod.instance.Helper.Translation.Get("DialogueAdventure.343.2");

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueAdventure.343.3"));

                            generate.answers.Add(10.ToString());

                            generate.lead = true;

                            return generate;

                        case CharacterHandle.characters.shrine_engine:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.18");

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.323.19"));

                            generate.answers.Add(10.ToString());

                            generate.lead = true;

                            return generate;

                        case CharacterHandle.characters.herbalism:

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

                            }

                            Mod.instance.characters[character].SwitchToMode(Character.Character.mode.home, Game1.player);

                            return generate;

                        case 10:

                            Wand wand = new();

                            wand.lastUser = Game1.player;

                            wand.DoFunction(Game1.player.currentLocation, 0, 0, 0, Game1.player);

                            return generate;

                        // Sacred Spring
                        case 11:

                            SpellHandle springWarp = new(Mod.instance.locations[LocationData.druid_spring_name], new(27,18), new Vector2(1312, 960)) { type = SpellHandle.spells.warp, scheme = IconData.schemes.white, projectile = 1 };

                            Mod.instance.spellRegister.Add(springWarp);

                            return generate;

                        // Secluded Atoll
                        case 12:

                            SpellHandle atollWarp = new(Mod.instance.locations[LocationData.druid_atoll_name], new(30, 21), new Vector2(1312,960)) { type = SpellHandle.spells.warp, scheme = IconData.schemes.white, projectile = 1 };

                            Mod.instance.spellRegister.Add(atollWarp);

                            return generate;

                        // Elder Graves
                        case 13:

                            SpellHandle gravesWarp = new(Mod.instance.locations[LocationData.druid_graveyard_name], new Vector2(27, 17), new Vector2(1312, 960)) { type = SpellHandle.spells.warp, scheme = IconData.schemes.white, projectile = 0 };

                            Mod.instance.spellRegister.Add(gravesWarp);

                            return generate;

                        // Chapel of the Stars
                        case 14:

                            SpellHandle chapelWarp = new(Mod.instance.locations[LocationData.druid_chapel_name], new(26, 23), new Vector2(1312, 960)) { type = SpellHandle.spells.warp, scheme = IconData.schemes.white, projectile = 0 };

                            Mod.instance.spellRegister.Add(chapelWarp);

                            return generate;

                        // Court of Fates and Chaos
                        case 15:

                            SpellHandle courtWarp = new(Mod.instance.locations[LocationData.druid_court_name], new Vector2(34, 18), new Vector2(1312, 960)) { type = SpellHandle.spells.warp, scheme = IconData.schemes.white, projectile = 0};

                            Mod.instance.spellRegister.Add(courtWarp);

                            return generate;

                        // Tomb of Tyrannus
                        case 16:

                            SpellHandle tombWarp = new(Mod.instance.locations[LocationData.druid_tomb_name], new Vector2(26, 17), new Vector2(1312, 960)) { type = SpellHandle.spells.warp, scheme = IconData.schemes.white, projectile = 0 }; ;

                            Mod.instance.spellRegister.Add(tombWarp);

                            return generate;

                        // Shrine Engine Room
                        case 17:

                            SpellHandle shrineWarp = new(Mod.instance.locations[LocationData.druid_engineum_name], new Vector2(27,19), new Vector2(1312, 960)) { type = SpellHandle.spells.warp, scheme = IconData.schemes.white, projectile = 0 }; ;

                            Mod.instance.spellRegister.Add(shrineWarp);

                            return generate;



                    }

                    break;
            }

            return generate;

        }

    }


}
