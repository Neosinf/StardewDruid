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
using StardewValley.Locations;
using Microsoft.Xna.Framework;
using StardewDruid.Event;

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

                    int runestones = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.runestones);

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

                case CharacterHandle.characters.attendant:

                    int tactical = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.tactical);

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

                    int avalant = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.avalant);

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

                    int books = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.books);

                    if (books == -1)
                    {

                        return null;

                    }

                    if (books == 4)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.262");

                    }
                    else if (books >= 1)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.268");

                    }

                    return null;

                case CharacterHandle.characters.Buffin:

                    int boxes = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.boxes);

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

                    int restores = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.restore);

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

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.302");

                case CharacterHandle.characters.monument_priesthood:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.306");

                case CharacterHandle.characters.monument_morticians:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.310");

                case CharacterHandle.characters.monument_chaos:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.314");
            }

            return null;

        }

        public static DialogueSpecial DialogueGenerate(CharacterHandle.characters character, int index = 0, int answer = 0)
        {

            DialogueSpecial generate = new();

            switch (character)
            {

                case CharacterHandle.characters.energies:

                    int runestones = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.runestones);

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

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.559"));

                            generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.561"));

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

                case CharacterHandle.characters.attendant:

                    int tactical = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.tactical);

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

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.310.6"));

                            generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.310.7"));
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

                    int avalant = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.avalant);

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

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.604"));

                            generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.606") +
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

                    int restoration = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.restore);

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

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.329.17"));

                            generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.329.18"));

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

                    int books = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.books);

                    if (books == -1)
                    {

                        return generate;

                    }

                    if (books == 4)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.650") + Mod.instance.Helper.Translation.Get("CharacterHandle.664") +
                        Mod.instance.Helper.Translation.Get("CharacterHandle.665");

                        if (CommunityCheck(0))
                        {

                            Mod.instance.questHandle.AssignQuest(QuestHandle.relicEther);

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.652"));

                            generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.654"));

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

                    int booksMarlon = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.books);

                    if (booksMarlon == -1)
                    {

                        return generate;

                    }

                    if (booksMarlon == 4)
                    {

                        generate.intro = string.Empty;

                        generate.intro += Mod.instance.Helper.Translation.Get("CharacterHandle.343.19");

                        if (CommunityCheck(0))
                        {

                            Mod.instance.questHandle.AssignQuest(QuestHandle.relicEther);

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.652"));

                            generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.343.20"));

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

                    int boxes = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.boxes);

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

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.699") +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.700"));

                            generate.answers.Add(Mod.instance.Helper.Translation.Get("CharacterHandle.702") +
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

                    switch (Mod.instance.relicsData.ArtisanRelicQuest())
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

                    int priestProgress = Mod.instance.relicsData.ProgressRelicQuest(RelicData.relicsets.boxes);

                    if (priestProgress >= 0)
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.794") +
                            Mod.instance.Helper.Translation.Get("CharacterHandle.795");

                    }
                    else
                    {

                        generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.801");

                    }

                    return generate;


                case CharacterHandle.characters.monument_morticians:

                    switch (Mod.instance.relicsData.MorticianRelicQuest())
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

                    switch (Mod.instance.relicsData.ChaosRelicQuest())
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
