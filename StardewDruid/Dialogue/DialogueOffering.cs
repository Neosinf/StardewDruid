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
using static StardewDruid.Journal.DruidJournal;
using static StardewValley.Menus.CharacterCustomization;
using static StardewDruid.Journal.HerbalData;

namespace StardewDruid.Dialogue
{
    public static class DialogueOffering
    {

        public static string DialogueOption(CharacterHandle.characters character)
        {

            switch (character)
            {

                case CharacterHandle.characters.herbalism:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.herbalism))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.466");

                    }
                    if (Mod.instance.questHandle.IsGiven(QuestHandle.herbalism))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.472");

                    }

                    return null;

                case CharacterHandle.characters.monument_priesthood:

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationData.druid_court_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationData.druid_court_name].Contains("FaethBlessing"))
                        {

                            return null;

                        }

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.323.11");

                case CharacterHandle.characters.star_desk:

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationData.druid_chapel_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationData.druid_chapel_name].Contains("DeskBlessing"))
                        {

                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationData.druid_chapel_name] = new();

                    }

                    return Mod.instance.Helper.Translation.Get("DialogueOffering.347.1");

                case CharacterHandle.characters.star_bench:

                    return Mod.instance.Helper.Translation.Get("DialogueOffering.347.2");

                case CharacterHandle.characters.shrine_engine:

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationData.druid_engineum_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationData.druid_engineum_name].Contains("AetherBlessing"))
                        {

                            return null;

                        }

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.323.13");

                case CharacterHandle.characters.shrine_forge:

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationData.druid_engineum_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationData.druid_engineum_name].Contains("ForgeBlessing"))
                        {

                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationData.druid_engineum_name] = new();

                    }

                    string upgradeOption = HerbalData.ForgeCheck();

                    if (upgradeOption == String.Empty)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.328.2");

                    }

                    Tool tooling = (Tool)ItemRegistry.Create(upgradeOption);

                    string upgradeName = tooling.DisplayName;

                    int upgradeRequirement = HerbalData.ForgeRequirement(upgradeOption);

                    if (!Mod.instance.save.herbalism.ContainsKey(HerbalData.herbals.aether))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.328.3").Tokens(new { tool = Game1.player.CurrentTool.Name, upgrade = upgradeName, aether = upgradeRequirement, });

                    }

                    if (Mod.instance.save.herbalism[HerbalData.herbals.aether] < upgradeRequirement)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.328.3").Tokens(new { tool = Game1.player.CurrentTool.Name, upgrade = upgradeName, aether = upgradeRequirement, });

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.328.4").Tokens(new { tool = Game1.player.CurrentTool.Name, upgrade = upgradeName, aether = upgradeRequirement, });
                
                case CharacterHandle.characters.shrine_desk:

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationData.druid_engineum_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationData.druid_engineum_name].Contains("DeskBlessing"))
                        {

                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationData.druid_engineum_name] = new();

                    }

                    return Mod.instance.Helper.Translation.Get("DialogueOffering.347.4");


                case CharacterHandle.characters.keeper:

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeMists))
                    {
                        return null;
                    }

                    if (Mod.instance.save.restoration[LocationData.druid_graveyard_name] >= 4)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.329.20");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.19");

                case CharacterHandle.characters.attendant:

                    if (Mod.instance.save.restoration[LocationData.druid_spring_name] >= 5)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.329.2");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.1");

            }

            return null;

        }

        public static DialogueSpecial DialogueGenerate(CharacterHandle.characters character, int index = 0 , int answer = 0)
        {

            DialogueSpecial generate = new();

            switch (character)
            {

                case CharacterHandle.characters.herbalism:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.herbalism))
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1443");

                        Mod.instance.herbalData.MassBrew(true);

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

                case CharacterHandle.characters.star_desk:

                    List<string> paperList = new()
                    {
                        "96",
                        "97",
                        "98",
                        "99",
                        "428",
                    };

                    bool noPaper = true;

                    foreach (string paper in paperList)
                    {

                        if(Game1.player.Items.CountId(paper) != 0)
                        {

                            noPaper = false;

                            break;
                        
                        };

                    }

                    if (noPaper || Game1.player.Items.CountId("814") == 0)
                    {
                        Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

                        return null;

                    }

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationData.druid_chapel_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationData.druid_chapel_name].Contains("DeskBlessing"))
                        {

                            Game1.player.currentLocation.playSound(SpellHandle.sounds.batScreech.ToString());

                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationData.druid_chapel_name] = new();

                    }

                    if (HerbalData.RandomStudy())
                    {

                        Mod.instance.rite.specialCasts[LocationData.druid_chapel_name].Add("DeskBlessing");

                        foreach (string paper in paperList)
                        {

                            if (Game1.player.Items.CountId(paper) != 0)
                            {

                                Game1.player.Items.ReduceId(paper, 1);
                                break;

                            };

                        }

                        Game1.player.Items.ReduceId("814", 1);

                        return null;

                    };

                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.347.3");

                    return generate;

                case CharacterHandle.characters.star_bench:

                    DruidJournal.openJournal(journalTypes.herbalTrade);

                    return null;

                case CharacterHandle.characters.monument_priesthood:

                    if (!Mod.instance.save.herbalism.ContainsKey(HerbalData.herbals.faeth))
                    {

                        Mod.instance.save.herbalism[HerbalData.herbals.faeth] = 0;

                    }

                    if (!Mod.instance.rite.specialCasts.ContainsKey(LocationData.druid_court_name))
                    {

                        Mod.instance.rite.specialCasts[LocationData.druid_court_name] = new() { "FaethBlessing", };

                        int faethBlessing = Mod.instance.randomIndex.Next(3, 6);

                        Mod.instance.CastMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.780").Tokens(new { faeth = faethBlessing.ToString(), }));

                        Mod.instance.save.herbalism[HerbalData.herbals.faeth] += faethBlessing;

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.785");

                        return generate;

                    }

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.12");

                    return generate;

                case CharacterHandle.characters.shrine_engine:

                    if (!Mod.instance.save.herbalism.ContainsKey(HerbalData.herbals.aether))
                    {

                        Mod.instance.save.herbalism[HerbalData.herbals.aether] = 0;

                    }

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationData.druid_engineum_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationData.druid_engineum_name].Contains("AetherBlessing"))
                        {

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.16");

                            return generate;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationData.druid_engineum_name] = new();

                    }

                    Mod.instance.rite.specialCasts[LocationData.druid_engineum_name].Add("AetherBlessing");

                    int aetherBlessing = Mod.instance.randomIndex.Next(4, 6);

                    Mod.instance.CastMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.323.14").Tokens(new { aether = aetherBlessing.ToString(), }));

                    Mod.instance.save.herbalism[HerbalData.herbals.aether] += aetherBlessing;

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.15");

                    return generate;

                case CharacterHandle.characters.shrine_forge:

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationData.druid_engineum_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationData.druid_engineum_name].Contains("ForgeBlessing"))
                        {

                            Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationData.druid_engineum_name] = new();

                    }

                    string upgradeOption = HerbalData.ForgeCheck();

                    if (upgradeOption == String.Empty)
                    {

                        Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

                        return null;

                    }

                    int upgradeRequirement = HerbalData.ForgeRequirement(upgradeOption);

                    if (!Mod.instance.save.herbalism.ContainsKey(HerbalData.herbals.aether))
                    {

                        Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

                        return null;

                    }

                    if (Mod.instance.save.herbalism[HerbalData.herbals.aether] < upgradeRequirement)
                    {

                        Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

                        return null;

                    }

                    Mod.instance.rite.specialCasts[LocationData.druid_engineum_name].Add("ForgeBlessing");

                    Mod.instance.save.herbalism[HerbalData.herbals.aether] -= upgradeRequirement;

                    DisplayPotion shrineForgeMessage = new(
                        Mod.instance.Helper.Translation.Get("HerbalData.1116").Tokens(new { potion = Mod.instance.herbalData.herbalism[herbals.aether.ToString()].title, }) 
                        + " " + DialogueData.Strings(DialogueData.stringkeys.multiplier) + upgradeRequirement.ToString(),
                        Mod.instance.herbalData.herbalism[herbals.aether.ToString()]
                    );

                    Game1.addHUDMessage(shrineForgeMessage);

                    HerbalData.ForgeUpgrade();

                    return null;

                case CharacterHandle.characters.shrine_desk:

                    bool lowAether = false;

                    if (!Mod.instance.save.herbalism.ContainsKey(HerbalData.herbals.aether))
                    {

                        lowAether = true;

                    }
                    else if(Mod.instance.save.herbalism[HerbalData.herbals.aether] < 3)
                    {

                        lowAether = true;

                    }

                    if (Game1.player.Items.CountId("388") < 25 || Game1.player.Items.CountId("390") < 25 || lowAether)
                    {

                        Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

                        return null;

                    }

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationData.druid_engineum_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationData.druid_engineum_name].Contains("DeskBlessing"))
                        {

                            Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationData.druid_engineum_name] = new();

                    }

                    if (HerbalData.RandomTinker())
                    {

                        Mod.instance.rite.specialCasts[LocationData.druid_engineum_name].Add("DeskBlessing");

                        Game1.player.Items.ReduceId("388", 25);
                        Game1.player.Items.ReduceId("390", 25);

                        DisplayPotion shrineDeskMessage = new(
                            Mod.instance.Helper.Translation.Get("HerbalData.1116").Tokens(new { potion = Mod.instance.herbalData.herbalism[herbals.aether.ToString()].title, }) + " " + DialogueData.Strings(DialogueData.stringkeys.multiplier) + "3", 
                            Mod.instance.herbalData.herbalism[herbals.aether.ToString()]
                        );

                        Game1.addHUDMessage(shrineDeskMessage);

                        return null;

                    };

                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.347.5");

                    return generate;

                case CharacterHandle.characters.keeper:

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeMists))
                    {
                        return null;
                    }

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.329.22");

                    if (Mod.instance.save.restoration[LocationData.druid_graveyard_name] < 2)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.329.21");

                    }
                    else if (Mod.instance.save.restoration[LocationData.druid_graveyard_name] >= 4)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.329.23");

                        if (!RelicData.HasRelic(IconData.relics.restore_offering))
                        {

                            generate.intro += Mod.instance.Helper.Translation.Get("CharacterHandle.329.24");

                            ThrowHandle throwOffering = new(Game1.player, Game1.player.Position + new Vector2(64, -192), IconData.relics.restore_offering);

                            throwOffering.register();

                        }

                    }

                    return generate;

                case CharacterHandle.characters.attendant:


                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.329.4");

                    if (Mod.instance.save.restoration[LocationData.druid_spring_name] < 2)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.329.3");

                    }
                    else if (Mod.instance.save.restoration[LocationData.druid_spring_name] >= 3)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.329.5");

                        if (!RelicData.HasRelic(IconData.relics.restore_goshuin))
                        {

                            generate.intro += Mod.instance.Helper.Translation.Get("CharacterHandle.329.6");

                            ThrowHandle throwGoshuin = new(Game1.player, Game1.player.Position + new Vector2(64, -192), IconData.relics.restore_goshuin);

                            throwGoshuin.register();

                        }

                    }

                    return generate;

            }

            return generate;

        }

    }


}
