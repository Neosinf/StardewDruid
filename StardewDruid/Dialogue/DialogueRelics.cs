using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewModdingAPI;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewValley.Locations;
using Microsoft.Xna.Framework;
using StardewDruid.Journal;
using StardewDruid.Handle;

namespace StardewDruid.Dialogue
{
    public static class DialogueRelics
    {

        public static string DialogueOption(CharacterHandle.characters character)
        {

            if (!Context.IsMainPlayer)
            {
                return null;

            }
            switch (character)
            {

                case CharacterHandle.characters.energies:

                    int runestones = Mod.instance.relicHandle.ProgressRelicQuest(RelicHandle.relicsets.runestones);

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

                case CharacterHandle.characters.herbalism:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.herbalism))
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueAdventure.373.1").Tokens(new { keybind = Mod.instance.Config.herbalismButtons.ToString(), });

                    }

                    return null;

                case CharacterHandle.characters.anvil:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.herbalism))
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueAdventure.373.2").Tokens(new { keybind = Mod.instance.Config.herbalismButtons.ToString(), });

                    }

                    return null;

                case CharacterHandle.characters.attendant:

                    int tactical = Mod.instance.relicHandle.ProgressRelicQuest(RelicHandle.relicsets.tactical);

                    if (tactical == -1)
                    {

                        return null;

                    }

                    if (tactical == 5)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.310.2");

                    }
                    else if (tactical >= 1)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.310.3");

                    }

                    return null;

                case CharacterHandle.characters.waves:

                    int avalant = Mod.instance.relicHandle.ProgressRelicQuest(RelicHandle.relicsets.avalant);

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

                case CharacterHandle.characters.Revenant:
                case CharacterHandle.characters.Marlon:

                    int books = Mod.instance.relicHandle.ProgressRelicQuest(RelicHandle.relicsets.books);

                    if (books == -1)
                    {

                        return null;

                    }

                    if (books == 5)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.262");

                    }
                    else if (books >= 1)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.268");

                    }

                    return null;

                case CharacterHandle.characters.Buffin:

                    int boxes = Mod.instance.relicHandle.ProgressRelicQuest(RelicHandle.relicsets.boxes);

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

                case CharacterHandle.characters.keeper:

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeMists))
                    {
                        return null;
                    }

                    int restores = Mod.instance.relicHandle.ProgressRelicQuest(RelicHandle.relicsets.restore);

                    if (restores == -1)
                    {

                        return null;

                    }

                    if (restores == 3)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.329.12");

                    }
                    else if (restores >= 1)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.329.13");

                    }

                    return null;

                case CharacterHandle.characters.monument_artisans:

                    if (RelicHandle.HasRelic(IconData.relics.box_measurer))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.302");

                    }

                    break;

                case CharacterHandle.characters.monument_priesthood:

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.306");

                case CharacterHandle.characters.monument_morticians:

                    if (RelicHandle.HasRelic(IconData.relics.box_measurer))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.310");

                    }

                    break;

                case CharacterHandle.characters.monument_chaos:

                    if (RelicHandle.HasRelic(IconData.relics.box_measurer))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.314");

                    }

                    break;

                case CharacterHandle.characters.PalBat:
                case CharacterHandle.characters.PalSlime:
                case CharacterHandle.characters.PalSpirit:
                case CharacterHandle.characters.PalGhost:
                case CharacterHandle.characters.PalSerpent:

                    return Mod.instance.Helper.Translation.Get("DialogueRelics.398.1");

            }

            return null;

        }

        public static DialogueSpecial DialogueGenerate(CharacterHandle.characters character, int index = 0, int answer = 0)
        {

            DialogueSpecial generate = new();

            switch (character)
            {

                case CharacterHandle.characters.energies:

                    int runestones = Mod.instance.relicHandle.ProgressRelicQuest(RelicHandle.relicsets.runestones);

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

                            generate.responses.Add(1,Mod.instance.Helper.Translation.Get("CharacterHandle.559"));

                            generate.answers.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.561"));

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

                case CharacterHandle.characters.herbalism:

                    Game1.exitActiveMenu();

                    DruidJournal.openJournal(DruidJournal.journalTypes.potions);

                    return null;

                case CharacterHandle.characters.anvil:

                    Game1.exitActiveMenu();

                    DruidJournal.openJournal(DruidJournal.journalTypes.powders);

                    return null;

                case CharacterHandle.characters.attendant:

                    int tactical = Mod.instance.relicHandle.ProgressRelicQuest(RelicHandle.relicsets.tactical);

                    if (tactical == -1)
                    {

                        return null;

                    }

                    if (tactical == 5)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.310.4") +
                            Mod.instance.Helper.Translation.Get("CharacterHandle.310.5");

                        if (CommunityCheck(3))
                        {
                            Mod.instance.questHandle.AssignQuest(QuestHandle.relicTactical);

                            generate.responses.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.310.6"));

                            generate.answers.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.310.7"));
                        }
                        else
                        {

                            Mod.instance.questHandle.CompleteQuest(QuestHandle.relicTactical);

                        }

                    }
                    else if (tactical >= 1)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.310.8") +
                            Mod.instance.Helper.Translation.Get("CharacterHandle.310.9") +
                            Mod.instance.Helper.Translation.Get("CharacterHandle.310.10") +
                            Mod.instance.Helper.Translation.Get("CharacterHandle.310.11");

                    }

                    return generate;

                case CharacterHandle.characters.waves:

                    int avalant = Mod.instance.relicHandle.ProgressRelicQuest(RelicHandle.relicsets.avalant);

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

                            generate.responses.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.604"));

                            generate.answers.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.606") +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.607"));
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

                case CharacterHandle.characters.keeper:

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeMists))
                    {
                        return null;
                    }

                    int restoration = Mod.instance.relicHandle.ProgressRelicQuest(RelicHandle.relicsets.restore);

                    if (restoration == -1)
                    {

                        return null;

                    }

                    if (restoration == 3)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.329.16");

                        if (CommunityCheck(4))
                        {
                            Mod.instance.questHandle.AssignQuest(QuestHandle.relicRestore);

                            generate.responses.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.329.17"));

                            generate.answers.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.329.18"));

                        }
                        else
                        {

                            Mod.instance.questHandle.CompleteQuest(QuestHandle.relicRestore);

                        }

                    }
                    else if (restoration >= 1)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.329.14") +
                            Mod.instance.Helper.Translation.Get("CharacterHandle.329.15");

                    }

                    return generate;

                case CharacterHandle.characters.Revenant:

                    int books = Mod.instance.relicHandle.ProgressRelicQuest(RelicHandle.relicsets.books);

                    if (books == -1)
                    {

                        return generate;

                    }

                    if (books == 5)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.650") + Mod.instance.Helper.Translation.Get("CharacterHandle.664") +
                        Mod.instance.Helper.Translation.Get("CharacterHandle.665");

                        if (CommunityCheck(0))
                        {

                            Mod.instance.questHandle.AssignQuest(QuestHandle.relicEther);

                            generate.responses.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.652"));

                            generate.answers.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.654"));

                        }
                        else
                        {

                            Mod.instance.questHandle.CompleteQuest(QuestHandle.relicEther);

                        }


                    }
                    else if (books >= 1)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.671");

                    }

                    return generate;

                case CharacterHandle.characters.Marlon:

                    int booksMarlon = Mod.instance.relicHandle.ProgressRelicQuest(RelicHandle.relicsets.books);

                    if (booksMarlon == -1)
                    {

                        return generate;

                    }

                    if (booksMarlon == 5)
                    {

                        generate.intro = string.Empty;

                        generate.intro += Mod.instance.Helper.Translation.Get("CharacterHandle.343.19");

                        if (CommunityCheck(0))
                        {

                            Mod.instance.questHandle.AssignQuest(QuestHandle.relicEther);

                            generate.responses.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.652"));

                            generate.answers.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.343.20"));

                        }
                        else
                        {

                            Mod.instance.questHandle.CompleteQuest(QuestHandle.relicEther);

                        }


                    }
                    else if (booksMarlon >= 1)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.343.21");

                    }

                    return generate;

                case CharacterHandle.characters.Buffin:

                    int boxes = Mod.instance.relicHandle.ProgressRelicQuest(RelicHandle.relicsets.boxes);

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

                            generate.responses.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.699") +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.700"));

                            generate.answers.Add(1, Mod.instance.Helper.Translation.Get("CharacterHandle.702") +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.703") +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.704"));

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


                case CharacterHandle.characters.monument_artisans:

                    switch (Mod.instance.relicHandle.ArtisanRelicQuest())
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

                case CharacterHandle.characters.monument_priesthood:

                    int priestProgress = Mod.instance.relicHandle.ProgressRelicQuest(RelicHandle.relicsets.boxes);

                    if (priestProgress >= 0)
                    {

                        if (Mod.instance.questHandle.IsComplete(QuestHandle.swordEther))
                        {

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.794") +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.795") +
                                Mod.instance.Helper.Translation.Get("DialogueRelics.375.1");


                        }
                        else
                        {

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.794") +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.795");


                        }

                    }
                    else
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.801");

                    }

                    return generate;


                case CharacterHandle.characters.monument_morticians:

                    switch (Mod.instance.relicHandle.MorticianRelicQuest())
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

                case CharacterHandle.characters.monument_chaos:

                    switch (Mod.instance.relicHandle.ChaosRelicQuest())
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


                case CharacterHandle.characters.PalBat:

                    RelicFunction.ClickFunction(IconData.relics.monster_bat.ToString());

                    return null;

                case CharacterHandle.characters.PalSlime:

                    RelicFunction.ClickFunction(IconData.relics.monster_slime.ToString());

                    return null;

                case CharacterHandle.characters.PalSpirit:

                    RelicFunction.ClickFunction(IconData.relics.monster_spirit.ToString());

                    return null;

                case CharacterHandle.characters.PalGhost:

                    RelicFunction.ClickFunction(IconData.relics.monster_ghost.ToString());

                    return null;

                case CharacterHandle.characters.PalSerpent:

                    RelicFunction.ClickFunction(IconData.relics.monster_serpent.ToString());

                    return null;


            }

            return generate;

        }

        public static bool CommunityCheck(int area)
        {

            if (
                !Game1.MasterPlayer.mailReceived.Contains("JojaMember") &&
                !(Game1.getLocationFromName("CommunityCenter") as CommunityCenter).areasComplete[area]
            )
            {

                return true;

            }

            return false;

        }


    }


}
