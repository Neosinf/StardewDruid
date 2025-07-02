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
using StardewDruid.Handle;

namespace StardewDruid.Dialogue
{
    public static class DialogueInventory
    {

        public static string DialogueOption(CharacterHandle.characters character)
        {

            if (!Context.IsMainPlayer)
            {
                return null;

            }

            switch (character)
            {

                case CharacterHandle.characters.Effigy:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.questEffigy))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.329");

                    }

                    break;

                case CharacterHandle.characters.Jester:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.340");

                case CharacterHandle.characters.Shadowtin:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.351");


                case CharacterHandle.characters.Blackfeather:

                    if (Mod.instance.questHandle.IsGiven(QuestHandle.questBlackfeather))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.323.2");

                    }

                    break;

                case CharacterHandle.characters.herbalism:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.herbalism))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.362");

                    }

                    break;

                case CharacterHandle.characters.anvil:

                    if (RelicData.HasRelic(IconData.relics.crow_hammer))
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueInventory.373.1");

                    }

                    break;

                case CharacterHandle.characters.Aldebaran:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.343.7");

                case CharacterHandle.characters.spring_bench:


                    if (Mod.instance.questHandle.IsComplete(QuestHandle.distillery))
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueInventory.386.1");

                    }

                    break;

            }

            return null;

        }

        public static DialogueSpecial DialogueGenerate(CharacterHandle.characters character, int index = 0 , int answer = 0)
        {

            DialogueSpecial generate = new();

            switch (character)
            {

                case CharacterHandle.characters.Aldebaran:

                    CharacterHandle.OpenInventory(CharacterHandle.characters.Effigy);

                    break;

                case CharacterHandle.characters.spring_bench:

                    switch (index)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("DialogueInventory.386.2");

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueInventory.386.3"));

                            generate.answers.Add(1.ToString());

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueInventory.386.4"));

                            generate.answers.Add(2.ToString());

                            generate.responses.Add(Mod.instance.Helper.Translation.Get("DialogueInventory.386.5"));

                            generate.answers.Add(3.ToString());

                            generate.lead = true;

                            return generate;

                        case 1:

                            switch (answer)
                            {
                                case 1:

                                    CharacterHandle.OpenInventory(CharacterHandle.characters.spring_bench);

                                    break;

                                case 2:

                                    CharacterHandle.OpenInventory(CharacterHandle.characters.spring_vintner);

                                    break;

                                case 3:

                                    CharacterHandle.OpenInventory(CharacterHandle.characters.spring_packer);

                                    break;
                            }

                            break;

                    }

                    break;

                default:

                    CharacterHandle.OpenInventory(character);

                    break;

            };

            return null;

        }

    }


}
