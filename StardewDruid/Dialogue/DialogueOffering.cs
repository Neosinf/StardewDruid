using StardewDruid.Journal;
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
using StardewDruid.Location.Druid;
using StardewDruid.Location.Terrain;
using StardewDruid.Handle;

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

                case CharacterHandle.characters.attendant:

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeWeald))
                    {
                        return null;
                    }

                    if (LocationHandle.GetRestoration(LocationHandle.druid_spring_name) >= 3)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.329.2");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.1");

                case CharacterHandle.characters.spring_bench:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.distillery))
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueOffering.386.17");

                    }

                    if (Mod.instance.questHandle.IsGiven(QuestHandle.distillery))
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueOffering.386.10");

                    }

                    return null;

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


                    if (Mod.instance.questHandle.IsComplete(QuestHandle.orders))
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueOffering.347.2");

                    }

                    if (Mod.instance.questHandle.IsGiven(QuestHandle.orders))
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueOffering.386.12");

                    }

                    return null;

                case CharacterHandle.characters.bearrock:

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeStars))
                    {
                        return null;
                    }

                    if (LocationHandle.GetRestoration(LocationHandle.druid_clearing_name) >= 3)
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueOffering.372.2");

                    }

                    return Mod.instance.Helper.Translation.Get("DialogueOffering.372.1");

                case CharacterHandle.characters.monument_priesthood:

                    if (!Mod.instance.questHandle.IsGiven(QuestHandle.fatesFour))
                    {

                        return null;

                    }

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_court_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationHandle.druid_court_name].Contains("FaethBlessing"))
                        {

                            return null;

                        }

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.323.11");

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

                    string upgradeOption = HerbalHandle.ForgeCheck();

                    if (upgradeOption == String.Empty)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.328.2");

                    }

                    Tool tooling = (Tool)ItemRegistry.Create(upgradeOption);

                    string upgradeName = tooling.DisplayName;

                    int upgradeRequirement = HerbalHandle.ForgeRequirement(upgradeOption);

                    if (HerbalHandle.GetHerbalism(HerbalHandle.herbals.aether) < upgradeRequirement)
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

                case CharacterHandle.characters.PalBat:
                case CharacterHandle.characters.PalSlime:
                case CharacterHandle.characters.PalSpirit:
                case CharacterHandle.characters.PalGhost:
                case CharacterHandle.characters.PalSerpent:

                    if (!Mod.instance.save.pals.ContainsKey(character))
                    {
                        return null;
                    }

                    if (Mod.instance.save.pals[character].fedtoday)
                    {
                        return null;
                    }

                    return Mod.instance.Helper.Translation.Get("DialogueOffering.386.1");

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

                            Vector2 groveWarp = new Vector2(19, 15) * 64;

                            switch (answer)
                            {
                                // Tomb of Tyrannus
                                case 16:

                                    SpellHandle tombWarp = new(Mod.instance.locations[LocationHandle.druid_tomb_name], new Vector2(26, 17), groveWarp) { type = SpellHandle.Spells.warp, scheme = IconData.schemes.white, factor = 0 }; ;

                                    Mod.instance.spellRegister.Add(tombWarp);

                                    return generate;

                                // Shrine Engine Room
                                case 17:

                                    SpellHandle shrineWarp = new(Mod.instance.locations[LocationHandle.druid_engineum_name], new Vector2(27, 19), groveWarp) { type = SpellHandle.Spells.warp, scheme = IconData.schemes.white, factor = 0 }; ;

                                    Mod.instance.spellRegister.Add(shrineWarp);

                                    return generate;

                                // Moors
                                case 18:

                                    SpellHandle moorsWarp = new(Mod.instance.locations[LocationHandle.druid_moors_name], new Vector2(27, 28), groveWarp) { type = SpellHandle.Spells.warp, scheme = IconData.schemes.white, factor = 2 }; ;

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

                case CharacterHandle.characters.spring_bench:


                    if (Mod.instance.questHandle.IsComplete(QuestHandle.distillery))
                    {

                        DruidJournal.openJournal(DruidJournal.journalTypes.distillery);

                        return null;

                    }

                    /*if (Game1.player.Items.CountId("388") >= 500 && Game1.player.Items.CountId("390") >= 300 && Game1.player.Money >= 10000)
                    {

                        Game1.player.Items.ReduceId("388",500);

                        Game1.player.Items.ReduceId("390", 300);

                        Game1.player.Money -= 10000;

                        generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.386.11");

                        Mod.instance.questHandle.CompleteQuest(QuestHandle.distillery);

                    }
                    else
                    {

                        Game1.player.currentLocation.playSound(SpellHandle.Sounds.ghost.ToString());

                        generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.386.15") +
                            Mod.instance.Helper.Translation.Get("DialogueOffering.386.16");

                    }*/

                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.386.11");

                    Mod.instance.questHandle.CompleteQuest(QuestHandle.distillery);

                    return generate;

                case CharacterHandle.characters.star_desk:

                    List<string> paperList = new()
                    {
                        "96",
                        "97",
                        "98",
                        "99",
                        "814",
                    };

                    string usePaper = String.Empty;

                    foreach (string paper in paperList)
                    {

                        if (Game1.player.Items.CountId(paper) != 0)
                        {

                            usePaper = paper;

                            break;

                        }
                        
                    }

                    if(usePaper == String.Empty)
                    {

                        Game1.player.currentLocation.playSound(SpellHandle.Sounds.ghost.ToString());

                        generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.398.1");

                        return generate;

                    }

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_chapel_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationHandle.druid_chapel_name].Contains("DeskBlessing"))
                        {

                           // Game1.player.currentLocation.playSound(SpellHandle.Sounds.batScreech.ToString());
                            Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.BatScreech);
                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_chapel_name] = new();

                    }

                    if (HerbalHandle.RandomStudy())
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_chapel_name].Add("DeskBlessing");

                        Game1.player.Items.ReduceId(usePaper, 1);

                        return null;

                    }

                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.347.3");

                    return generate;

                case CharacterHandle.characters.star_bench:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.orders))
                    {

                        DruidJournal.openJournal(DruidJournal.journalTypes.orders);

                        return null;

                    }

                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.386.13") +
                        Mod.instance.Helper.Translation.Get("DialogueOffering.386.14");

                    Mod.instance.questHandle.CompleteQuest(QuestHandle.orders);

                    return generate;

                case CharacterHandle.characters.monument_priesthood:

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_court_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationHandle.druid_court_name].Contains("FaethBlessing"))
                        {

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.12");

                            return generate;

                        }

                    }

                    Mod.instance.rite.specialCasts[LocationHandle.druid_court_name] = new() { "FaethBlessing", };

                    int faethBlessing = Mod.instance.randomIndex.Next(3, 6);

                    Mod.instance.CastMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.780").Tokens(new { faeth = faethBlessing.ToString(), }));

                    HerbalHandle.UpdateHerbalism(HerbalHandle.herbals.faeth, faethBlessing);

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.785");

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

                    if(Game1.player.currentLocation is Engineum engineum)
                    {

                        foreach (TerrainField engineField in engineum.terrainFields)
                        {

                            if(engineField is EngineShrine engineShrine)
                            {

                                engineShrine.opened = true;

                            }

                        }

                    }

                    int aetherBlessing = Mod.instance.randomIndex.Next(2, 5);

                    Mod.instance.CastMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.323.14").Tokens(new { aether = aetherBlessing.ToString(), }));

                    HerbalHandle.UpdateHerbalism(HerbalHandle.herbals.aether, aetherBlessing);

                    generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.323.15");

                    return generate;

                case CharacterHandle.characters.shrine_forge:

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_engineum_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name].Contains("ForgeBlessing"))
                        {

                            Game1.player.currentLocation.playSound(SpellHandle.Sounds.ghost.ToString());

                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name] = new();

                    }

                    string upgradeOption = HerbalHandle.ForgeCheck();

                    if (upgradeOption == String.Empty)
                    {

                        Game1.player.currentLocation.playSound(SpellHandle.Sounds.ghost.ToString());

                        return null;

                    }

                    int upgradeRequirement = HerbalHandle.ForgeRequirement(upgradeOption);

                    if (HerbalHandle.GetHerbalism(HerbalHandle.herbals.aether) < upgradeRequirement)
                    {

                        Game1.player.currentLocation.playSound(SpellHandle.Sounds.ghost.ToString());

                        return null;

                    }

                    Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name].Add("ForgeBlessing");

                    HerbalHandle.UpdateHerbalism(HerbalHandle.herbals.aether,0 - upgradeRequirement);

                    DisplayPotion shrineForgeMessage = new(
                        Mod.instance.Helper.Translation.Get("HerbalData.1116").Tokens(new { potion = Mod.instance.herbalData.herbalism[HerbalHandle.herbals.aether.ToString()].title, }) 
                        + " " + StringData.Strings(StringData.stringkeys.multiplier) + upgradeRequirement.ToString(),
                        Mod.instance.herbalData.herbalism[HerbalHandle.herbals.aether.ToString()]
                    );

                    Game1.addHUDMessage(shrineForgeMessage);

                    HerbalHandle.ForgeUpgrade();

                    return null;

                case CharacterHandle.characters.shrine_desk:

                    bool lowAether = false;

                    if(HerbalHandle.GetHerbalism(HerbalHandle.herbals.aether) < 3)
                    {

                        lowAether = true;

                    }

                    if (Game1.player.Items.CountId("388") < 25 || Game1.player.Items.CountId("390") < 25 || lowAether)
                    {

                        Game1.player.currentLocation.playSound(SpellHandle.Sounds.ghost.ToString());

                        return null;

                    }

                    if (Mod.instance.rite.specialCasts.ContainsKey(LocationHandle.druid_engineum_name))
                    {

                        if (Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name].Contains("DeskBlessing"))
                        {

                            Game1.player.currentLocation.playSound(SpellHandle.Sounds.ghost.ToString());

                            return null;

                        }

                    }
                    else
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name] = new();

                    }

                    if (HerbalHandle.RandomTinker())
                    {

                        Mod.instance.rite.specialCasts[LocationHandle.druid_engineum_name].Add("DeskBlessing");

                        Game1.player.Items.ReduceId("388", 25);
                        Game1.player.Items.ReduceId("390", 25);

                        DisplayPotion shrineDeskMessage = new(
                            Mod.instance.Helper.Translation.Get("HerbalData.1116").Tokens(new { potion = Mod.instance.herbalData.herbalism[HerbalHandle.herbals.aether.ToString()].title, }) + " " + StringData.Strings(StringData.stringkeys.multiplier) + "3", 
                            Mod.instance.herbalData.herbalism[HerbalHandle.herbals.aether.ToString()]
                        );

                        Game1.addHUDMessage(shrineDeskMessage);

                        return null;

                    };

                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.347.5");

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

                case CharacterHandle.characters.bearrock:

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeStars))
                    {

                        return null;

                    }

                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.372.3");

                    if (LocationHandle.GetRestoration(LocationHandle.druid_clearing_name) >= 3)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.372.4");

                        if (!RelicData.HasRelic(IconData.relics.restore_cloth))
                        {

                            generate.intro += Mod.instance.Helper.Translation.Get("DialogueOffering.372.5");

                            ThrowHandle throwOffering = new(Game1.player, Game1.player.Position + new Vector2(64, -192), IconData.relics.restore_cloth);

                            throwOffering.register();

                        }

                    }

                    return generate;

                case CharacterHandle.characters.PalBat:
                case CharacterHandle.characters.PalSlime:
                case CharacterHandle.characters.PalSpirit:
                case CharacterHandle.characters.PalGhost:
                case CharacterHandle.characters.PalSerpent:

                    int statBoost;

                    switch (index)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.386.2").Tokens(new { name = PalHandle.PalName(character), title = PalHandle.PalScheme(character) });

                            if (Mod.instance.herbalData.BestHerbal(HerbalHandle.herbals.ligna) != HerbalHandle.herbals.none)
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueOffering.386.3"));

                                generate.answers.Add(1.ToString());

                            }

                            if (Mod.instance.herbalData.BestHerbal(HerbalHandle.herbals.impes) != HerbalHandle.herbals.none)
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueOffering.386.4"));

                                generate.answers.Add(2.ToString());

                            }

                            if (Mod.instance.herbalData.BestHerbal(HerbalHandle.herbals.celeri) != HerbalHandle.herbals.none)
                            {

                                generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueOffering.386.5"));

                                generate.answers.Add(3.ToString());
                            }

                            if (Mod.instance.save.herbalism.ContainsKey(HerbalHandle.herbals.faeth))
                            {

                                if (Mod.instance.save.herbalism[HerbalHandle.herbals.faeth] > 0)
                                {

                                    generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueOffering.390.1"));

                                    generate.answers.Add(4.ToString());

                                }

                            }

                            generate.lead = true;

                            break;

                        case 1:

                            switch (answer)
                            {

                                case 1:


                                    statBoost = Mod.instance.save.pals[character].BoostStat(HerbalHandle.herbals.ligna);

                                    if (statBoost > 0)
                                    {

                                        generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.386.6").Tokens(new { name = PalHandle.PalName(character), title = PalHandle.PalScheme(character), amount = statBoost });

                                        break;

                                    }

                                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.386.9");

                                    break;

                                case 2:

                                    statBoost = Mod.instance.save.pals[character].BoostStat(HerbalHandle.herbals.impes);

                                    if (statBoost > 0)
                                    {

                                        generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.386.7").Tokens(new { name = PalHandle.PalName(character), title = PalHandle.PalScheme(character), amount = statBoost });

                                        break;

                                    }

                                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.386.9");

                                    break;

                                case 3:

                                    statBoost = Mod.instance.save.pals[character].BoostStat(HerbalHandle.herbals.celeri);

                                    if (statBoost > 0)
                                    {

                                        generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.386.8").Tokens(new { name = PalHandle.PalName(character), title = PalHandle.PalScheme(character), amount = statBoost });

                                        break;

                                    }

                                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.386.9");

                                    break;

                                case 4:

                                    statBoost = Mod.instance.save.pals[character].BoostStat(HerbalHandle.herbals.faeth);

                                    if (statBoost > 0)
                                    {

                                        generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.390.2").Tokens(new { name = PalHandle.PalName(character), title = PalHandle.PalScheme(character), amount = statBoost });

                                        break;

                                    }

                                    generate.intro = Mod.instance.Helper.Translation.Get("DialogueOffering.386.9");

                                    break;


                            }

                            break;


                    }

                    break;


            }

            return generate;

        }

    }


}
