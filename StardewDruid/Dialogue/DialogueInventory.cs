using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

                case CharacterHandle.characters.anvil:

                    if (RelicHandle.HasRelic(IconData.relics.druid_hammer))
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
                case CharacterHandle.characters.anvil:

                    ChestHandle.OpenInventory(ChestHandle.chests.Anvil);

                    break;

                case CharacterHandle.characters.spring_bench:

                    switch (index)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("DialogueInventory.386.2");

                            generate.responses.Add(1,Mod.instance.Helper.Translation.Get("DialogueInventory.386.3"));

                            generate.leads.Add(1, 1);

                            generate.responses.Add(2,Mod.instance.Helper.Translation.Get("DialogueInventory.386.4"));

                            generate.leads.Add(2, 2);

                            generate.responses.Add(3,Mod.instance.Helper.Translation.Get("DialogueInventory.386.5"));

                            generate.leads.Add(3, 3);

                            return generate;

                        case 1:

                            ChestHandle.OpenInventory(ChestHandle.chests.Distillery);

                            break;

                    }

                    break;

                default:

                    ChestHandle.OpenInventory(ChestHandle.chests.Companions);

                    break;

            };

            return null;

        }

    }


}
