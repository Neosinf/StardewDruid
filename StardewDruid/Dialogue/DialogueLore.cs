using StardewDruid.Data;
using StardewDruid.Handle;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Dialogue
{

    public static class DialogueLore
    {

        public static string DialogueOption(CharacterHandle.characters character)
        {

            switch (character)
            {

                case CharacterHandle.characters.Effigy:

                    return Mod.instance.Helper.Translation.Get("LoreData.10");

                case CharacterHandle.characters.Jester:

                    return Mod.instance.Helper.Translation.Get("LoreData.14");

                case CharacterHandle.characters.Revenant:

                    return Mod.instance.Helper.Translation.Get("LoreData.18");

                case CharacterHandle.characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("LoreData.22");

                case CharacterHandle.characters.Shadowtin:

                    return Mod.instance.Helper.Translation.Get("LoreData.26");

                case CharacterHandle.characters.Blackfeather:

                    return Mod.instance.Helper.Translation.Get("LoreData.320.1");

                case CharacterHandle.characters.keeper:

                    return Mod.instance.Helper.Translation.Get("LoreData.329.1");

                case CharacterHandle.characters.bearrock:

                    return Mod.instance.Helper.Translation.Get("LoreData.372.13");

                case CharacterHandle.characters.Aldebaran:

                    return Mod.instance.Helper.Translation.Get("LoreData.343.2");

                case CharacterHandle.characters.recruit_one:
                case CharacterHandle.characters.recruit_two:
                case CharacterHandle.characters.recruit_three:
                case CharacterHandle.characters.recruit_four:

                    return RecruitHandle.LoreOption(character);
            }

            return null;

        }

        public static DialogueSpecial DialogueGenerate(CharacterHandle.characters character, int index = 0, int answer = 0)
        {

            DialogueSpecial generate = new();

            List<LoreStory> stories = RetrieveLore(character);

            foreach (LoreStory story in stories)
            {
                generate.intro = DialogueIntro(character);

                generate.responses.Add(story.question);

                generate.answers.Add(story.answer);

            }

            return generate;

        }

        public static string DialogueIntro(CharacterHandle.characters character)
        {

            switch (character)
            {

                default:
                case CharacterHandle.characters.Effigy:

                    return Mod.instance.Helper.Translation.Get("LoreData.41");

                case CharacterHandle.characters.Jester:

                    return Mod.instance.Helper.Translation.Get("LoreData.45");

                case CharacterHandle.characters.Revenant:

                    return Mod.instance.Helper.Translation.Get("LoreData.49");

                case CharacterHandle.characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("LoreData.53");

                case CharacterHandle.characters.Shadowtin:

                    return Mod.instance.Helper.Translation.Get("LoreData.57");

                case CharacterHandle.characters.Blackfeather:

                    return Mod.instance.Helper.Translation.Get("LoreData.320.2");

                case CharacterHandle.characters.keeper:

                    return Mod.instance.Helper.Translation.Get("LoreData.329.2");

                case CharacterHandle.characters.bearrock:

                    return Mod.instance.Helper.Translation.Get("LoreData.372.14");

                case CharacterHandle.characters.Aldebaran:

                    return Mod.instance.Helper.Translation.Get("LoreData.343.3");

                case CharacterHandle.characters.recruit_one:
                case CharacterHandle.characters.recruit_two:
                case CharacterHandle.characters.recruit_three:
                case CharacterHandle.characters.recruit_four:

                    return RecruitHandle.LoreIntro(character);

            }

        }

        public static List<LoreStory> RetrieveLore(CharacterHandle.characters character)
        {

            switch (character)
            {

                default:
                    
                    List<LoreStory> list = new();

                    int limit = 0;

                    for (int i = Mod.instance.questHandle.lores.Count - 1; i >= 0; i--)
                    {

                        LoreStory story = Mod.instance.questHandle.lores.ElementAt(i).Value;

                        if (story.loretype != LoreStory.loretypes.story)
                        {

                            continue;

                        }

                        if (story.character != character)
                        {

                            continue;

                        }

                        if (!story.anytime && !Mod.instance.questHandle.IsComplete(story.quest))
                        {

                            continue;

                        }

                        list.Add(story);

                        limit++;

                        if (limit == 3)
                        {

                            break;

                        }

                    }

                    return list;

                case CharacterHandle.characters.recruit_one:
                case CharacterHandle.characters.recruit_two:
                case CharacterHandle.characters.recruit_three:
                case CharacterHandle.characters.recruit_four:

                    return RecruitHandle.LoreStories(character);

            }

        }

    }
}
