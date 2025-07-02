using StardewDruid.Character;
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

    public static class DialogueTalk
    {

        public static string DialogueOption(CharacterHandle.characters character)
        {

            switch (character)
            {

                case CharacterHandle.characters.recruit_one:
                case CharacterHandle.characters.recruit_two:
                case CharacterHandle.characters.recruit_three:
                case CharacterHandle.characters.recruit_four:

                    NPC villager = (Mod.instance.characters[character] as Recruit).villager;

                    if (villager.canTalk() && villager.CurrentDialogue.Count > 0)
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueLore.361.1");

                    }

                    break;

            }

            return null;

        }
        
        public static void DialogueGenerate(CharacterHandle.characters character)
        {

            NPC villager = (Mod.instance.characters[character] as Recruit).villager;

            if (villager.canTalk() && villager.CurrentDialogue.Count > 0)
            {

                Game1.drawDialogue(villager);

            }

        }

    }

}
