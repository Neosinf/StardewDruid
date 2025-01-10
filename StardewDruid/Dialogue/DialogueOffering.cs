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
using StardewDruid.Event;

namespace StardewDruid.Dialogue
{
    public static class DialogueOffering
    {

        public static string DialogueOption(CharacterHandle.characters character)
        {

            switch (character)
            {

                case CharacterHandle.characters.energies:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.swordEther))
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueOffering.363.1");

                    }

                    return null;

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

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_court_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationHandle.druid_court_name].Contains("FaethBlessing"))
                        {

                            return null;

                        }

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.323.11");

                case CharacterHandle.characters.star_desk:

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_chapel_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationHandle.druid_chapel_name].Contains("DeskBlessing"))
                        {

                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_chapel_name] = new();

                    }

                    return Mod.instance.Helper.Translation.Get("DialogueOffering.347.1");

                case CharacterHandle.characters.star_bench:

                    return Mod.instance.Helper.Translation.Get("DialogueOffering.347.2");

                case CharacterHandle.characters.shrine_engine:

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_engineum_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name].Contains("AetherBlessing"))
                        {

                            return null;

                        }

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.323.13");

                case CharacterHandle.characters.shrine_forge:

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_engineum_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name].Contains("ForgeBlessing"))
                        {

                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name] = new();

                    }

                    string upgradeOption = HerbalData.ForgeCheck();

                    if (upgradeOption == String.Empty)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.328.2");

                    }

                    Tool tooling = (Tool)ItemRegistry.Create(upgradeOption);

                    string upgradeName = tooling.DisplayName;

                    int upgradeRequirement = HerbalData.ForgeRequirement(upgradeOption);

                    if (HerbalData.UpdateHerbalism(HerbalData.herbals.aether) < upgradeRequirement)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.328.3").Tokens(new { tool = Game1.player.CurrentTool.Name, upgrade = upgradeName, aether = upgradeRequirement, });

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.328.4").Tokens(new { tool = Game1.player.CurrentTool.Name, upgrade = upgradeName, aether = upgradeRequirement, });
                
                case CharacterHandle.characters.shrine_desk:

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_engineum_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name].Contains("DeskBlessing"))
                        {

                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name] = new();

                    }

                    return Mod.instance.Helper.Translation.Get("DialogueOffering.347.4");


                case CharacterHandle.characters.keeper:

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeMists))
                    {
                        return null;
                    }

                    if (LocationHandle.GetRestoration(LocationHandle.druid_graveyard_name) >= 4)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.329.20");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.19");

                case CharacterHandle.characters.attendant:

                    if (LocationHandle.GetRestoration(LocationHandle.druid_spring_name) >= 5)
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

                case CharacterHandle.characters.energies:

                    switch (index)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.363.2");

                            // Tomb of Tyrannus
                            if (Mod.instance.questHandle.IsComplete(QuestHandle.swordEther))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueAdventure.343.10"));

                                generate.answers.Add(16.ToString());

                            }

                            // Shrine Engine Room
                            if (Mod.instance.questHandle.IsComplete(QuestHandle.questShadowtin))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueAdventure.343.11"));

                                generate.answers.Add(17.ToString());

                            }

                            // Moors
                            if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeMoors))
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueAdventure.363.1"));

                                generate.answers.Add(18.ToString());

                            }

                            generate.lead = true;

                            return generate;

                        case 1:

                            switch (answer)
                            {
                                // Tomb of Tyrannus
                                case 16:

                                    SpellHandle tombWarp = new(Mod.instance.locations[LocationHandle.druid_tomb_name], new Vector2(26, 17), new Vector2(1312, 960)) { type = SpellHandle.spells.warp, scheme = IconData.schemes.white, factor = 0 }; ;

                                    Mod.instance.spellRegister.Add(tombWarp);

                                    return generate;

                                // Shrine Engine Room
                                case 17:

                                    SpellHandle shrineWarp = new(Mod.instance.locations[LocationHandle.druid_engineum_name], new Vector2(27, 19), new Vector2(1312, 960)) { type = SpellHandle.spells.warp, scheme = IconData.schemes.white, factor = 0 }; ;

                                    Mod.instance.spellRegister.Add(shrineWarp);

                                    return generate;

                                // Moors
                                case 18:

                                    SpellHandle moorsWarp = new(Mod.instance.locations[LocationHandle.druid_moors_name], new Vector2(27, 27), new Vector2(1760, 1760)) { type = SpellHandle.spells.warp, scheme = IconData.schemes.white, factor = 2 }; ;

                                    Mod.instance.spellRegister.Add(moorsWarp);

                                    return generate;
                                    

                            }

                            break;

                    }

                    break;


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

                    bool noInk = true;

                    foreach (string paper in paperList)
                    {

                        if(Game1.player.Items.CountId(paper) != 0)
                        {

                            noPaper = false;

                            break;
                        
                        };

                    }

                    if (Mod.instance.save.herbalism[HerbalData.herbals.voil] > 0)
                    {

                        noInk = false;

                    }
                    else if (Game1.player.Items.CountId("814") > 0)
                    {

                        noInk = false;

                    }

                    if (noPaper || noInk)
                    {

                        Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

                        return null;

                    }

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_chapel_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationHandle.druid_chapel_name].Contains("DeskBlessing"))
                        {

                            Game1.player.currentLocation.playSound(SpellHandle.sounds.batScreech.ToString());

                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_chapel_name] = new();

                    }

                    if (HerbalData.RandomStudy())
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_chapel_name].Add("DeskBlessing");

                        foreach (string paper in paperList)
                        {

                            if (Game1.player.Items.CountId(paper) != 0)
                            {

                                Game1.player.Items.ReduceId(paper, 1);
                                break;

                            };

                        }

                        if (Mod.instance.save.herbalism[HerbalData.herbals.voil] > 0)
                        {

                            Mod.instance.save.herbalism[HerbalData.herbals.voil] -= 1;

                        }
                        else
                        {

                            Game1.player.Items.ReduceId("814", 1);

                        }

                        return null;

                    };

                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.347.3");

                    return generate;

                case CharacterHandle.characters.star_bench:

                    DruidJournal.openJournal(DruidJournal.journalTypes.herbalTrade);

                    return null;

                case CharacterHandle.characters.monument_priesthood:

                    if (!Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_court_name))
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_court_name] = new() { "FaethBlessing", };

                        int faethBlessing = Mod.instance.randomIndex.Next(3, 6);

                        Mod.instance.CastMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.780").Tokens(new { faeth = faethBlessing.ToString(), }));

                        HerbalData.UpdateHerbalism(HerbalData.herbals.faeth, faethBlessing);

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.785");

                        return generate;

                    }

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.12");

                    return generate;

                case CharacterHandle.characters.shrine_engine:

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_engineum_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name].Contains("AetherBlessing"))
                        {

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.16");

                            return generate;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name] = new();

                    }

                    Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name].Add("AetherBlessing");

                    int aetherBlessing = Mod.instance.randomIndex.Next(2, 5);

                    Mod.instance.CastMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.323.14").Tokens(new { aether = aetherBlessing.ToString(), }));

                    HerbalData.UpdateHerbalism(HerbalData.herbals.aether, aetherBlessing);

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.15");

                    return generate;

                case CharacterHandle.characters.shrine_forge:

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_engineum_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name].Contains("ForgeBlessing"))
                        {

                            Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name] = new();

                    }

                    string upgradeOption = HerbalData.ForgeCheck();

                    if (upgradeOption == String.Empty)
                    {

                        Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

                        return null;

                    }

                    int upgradeRequirement = HerbalData.ForgeRequirement(upgradeOption);

                    if (HerbalData.UpdateHerbalism(HerbalData.herbals.aether) < upgradeRequirement)
                    {

                        Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

                        return null;

                    }

                    Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name].Add("ForgeBlessing");

                    HerbalData.UpdateHerbalism(HerbalData.herbals.aether,0 - upgradeRequirement);

                    DisplayPotion shrineForgeMessage = new(
                        Mod.instance.Helper.Translation.Get("HerbalData.1116").Tokens(new { potion = Mod.instance.herbalData.herbalism[HerbalData.herbals.aether.ToString()].title, }) 
                        + " " + StringData.Strings(StringData.stringkeys.multiplier) + upgradeRequirement.ToString(),
                        Mod.instance.herbalData.herbalism[HerbalData.herbals.aether.ToString()]
                    );

                    Game1.addHUDMessage(shrineForgeMessage);

                    HerbalData.ForgeUpgrade();

                    return null;

                case CharacterHandle.characters.shrine_desk:

                    bool lowAether = false;

                    if(HerbalData.UpdateHerbalism(HerbalData.herbals.aether) < 3)
                    {

                        lowAether = true;

                    }

                    if (Game1.player.Items.CountId("388") < 25 || Game1.player.Items.CountId("390") < 25 || lowAether)
                    {

                        Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

                        return null;

                    }

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_engineum_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name].Contains("DeskBlessing"))
                        {

                            Game1.player.currentLocation.playSound(SpellHandle.sounds.ghost.ToString());

                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name] = new();

                    }

                    if (HerbalData.RandomTinker())
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name].Add("DeskBlessing");

                        Game1.player.Items.ReduceId("388", 25);
                        Game1.player.Items.ReduceId("390", 25);

                        DisplayPotion shrineDeskMessage = new(
                            Mod.instance.Helper.Translation.Get("HerbalData.1116").Tokens(new { potion = Mod.instance.herbalData.herbalism[HerbalData.herbals.aether.ToString()].title, }) + " " + StringData.Strings(StringData.stringkeys.multiplier) + "3", 
                            Mod.instance.herbalData.herbalism[HerbalData.herbals.aether.ToString()]
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

                    if (LocationHandle.GetRestoration(LocationHandle.druid_graveyard_name) < 2)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.329.21");

                    }
                    else if (LocationHandle.GetRestoration(LocationHandle.druid_graveyard_name) >= 4)
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

                    if (LocationHandle.GetRestoration(LocationHandle.druid_spring_name) < 2)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.329.3");

                    }
                    else if (LocationHandle.GetRestoration(LocationHandle.druid_spring_name) >= 3)
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
