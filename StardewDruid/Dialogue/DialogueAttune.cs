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
using StardewValley.Tools;
using System.Reflection;
using StardewDruid.Handle;
using StardewDruid.Cast.Ether;

namespace StardewDruid.Dialogue
{
    public static class DialogueAttune
    {

        public static string DialogueOption(CharacterHandle.characters character)
        {

            if (Mod.instance.Config.slotAttune)
            {

                return null;

            }

            switch (character)
            {

                case CharacterHandle.characters.energies:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.swordWeald))
                    {

                        return AttunementIntro(Rite.Rites.weald);



                    }
                    return null;

                case CharacterHandle.characters.waves:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.swordMists))
                    {
                        return AttunementIntro(Rite.Rites.mists);


                    }

                    return null;

                case CharacterHandle.characters.star_altar:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.swordStars))
                    {

                        return AttunementIntro(Rite.Rites.stars);

                    }

                    return null;

                case CharacterHandle.characters.monument_priesthood:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.swordFates))
                    {

                        return AttunementIntro(Rite.Rites.fates);

                    }

                    return null;

                case CharacterHandle.characters.dragon_statue:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.swordEther))
                    {

                        return AttunementIntro(Rite.Rites.ether);

                    }

                    return null;

                case CharacterHandle.characters.crow_brazier:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.questBlackfeather))
                    {

                        return AttunementIntro(Rite.Rites.bones);

                    }

                    return null;

            }

            return null;

        }

        public static DialogueSpecial DialogueGenerate(CharacterHandle.characters character, int index = 0 , int answer = 0)
        {

            if (Mod.instance.Config.slotAttune)
            {

                return null;

            }

            DialogueSpecial generate = new();

            int toolIndex = Mod.instance.AttuneableWeapon();

            int attuneUpdate;

            switch (character)
            {

                case CharacterHandle.characters.energies:

                    attuneUpdate = AttunementUpdate(Rite.Rites.weald);

                    switch (attuneUpdate)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1239").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1246").Tokens(new { tool = Game1.player.CurrentTool.Name, }) +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.1249");

                            break;

                        case 2:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1255").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 3:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1263").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                    }

                    return generate;

                case CharacterHandle.characters.waves:

                    attuneUpdate = AttunementUpdate(Rite.Rites.mists);

                    switch (attuneUpdate)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1239").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1290").Tokens(new { tool = Game1.player.CurrentTool.Name, }) +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.1293");


                            break;

                        case 2:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1300").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 3:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1308").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                    }

                    return generate;

                case CharacterHandle.characters.star_altar:

                    attuneUpdate = AttunementUpdate(Rite.Rites.stars);

                    switch (attuneUpdate)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1239").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1334").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 2:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1341").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 3:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1348").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                    }

                    return generate;

                case CharacterHandle.characters.monument_priesthood:

                    attuneUpdate = AttunementUpdate(Rite.Rites.fates);

                    switch (attuneUpdate)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1239").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1373").Tokens(new { tool = Game1.player.CurrentTool.Name, }) +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.1375") +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.1376");

                            break;

                        case 2:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1382").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 3:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1389").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                    }

                    return generate;

                case CharacterHandle.characters.dragon_statue:

                    attuneUpdate = AttunementUpdate(Rite.Rites.ether);

                    switch (attuneUpdate)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1239").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1414") +
                                            Mod.instance.Helper.Translation.Get("CharacterHandle.1415");

                            break;

                        case 2:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1421").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 3:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1428").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                    }

                    return generate;

                case CharacterHandle.characters.crow_brazier:

                    attuneUpdate = AttunementUpdate(Rite.Rites.bones);

                    switch (attuneUpdate)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1239").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.315.4").Tokens(new { tool = Game1.player.CurrentTool.Name, }) + Mod.instance.Helper.Translation.Get("CharacterHandle.315.5");

                            break;

                        case 2:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.315.6").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 3:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.315.7").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                    }

                    return generate;



            }
            return generate;

        }

        public static string AttunementIntro(Rite.Rites compare)
        {

            int toolIndex = Mod.instance.AttuneableWeapon();

            if (toolIndex == -1 || toolIndex == 999 || Game1.player.CurrentTool == null)
            {

                return null;

            }

            Dictionary<int, Rite.Rites> comparison = SpawnData.WeaponAttunement(true);

            if (comparison.ContainsKey(toolIndex))
            {

                if (comparison[toolIndex] == compare)
                {


                    return Mod.instance.Helper.Translation.Get("CharacterHandle.1557").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                }
                else
                {

                    return null;

                }

            }

            if (Mod.instance.save.attunement.ContainsKey(toolIndex))
            {

                if (Mod.instance.save.attunement[toolIndex] == compare)
                {


                    return Mod.instance.Helper.Translation.Get("CharacterHandle.1577").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                }

            }

            return Mod.instance.Helper.Translation.Get("CharacterHandle.1584").Tokens(new { tool = Game1.player.CurrentTool.Name, rite = StringData.RiteNames(compare), });

        }

        public static int AttunementUpdate(Rite.Rites compare)
        {

            int toolIndex = Mod.instance.AttuneableWeapon();

            if (toolIndex == -1 || toolIndex == 999)
            {

                return 0;

            }

            Dictionary<int, Rite.Rites> comparison = SpawnData.WeaponAttunement(true);

            if (comparison.ContainsKey(toolIndex))
            {

                if (comparison[toolIndex] == compare)
                {

                    return 1;

                }
                else
                {

                    return 0;

                }

            }

            if (Mod.instance.save.attunement.ContainsKey(toolIndex))
            {

                if (Mod.instance.save.attunement[toolIndex] == compare)
                {

                    Mod.instance.iconData.ImpactIndicator(Game1.player.currentLocation, Game1.player.Position, IconData.impacts.supree, 6f, new());

                    Game1.player.currentLocation.playSound(SpellHandle.Sounds.yoba.ToString());

                    Mod.instance.DetuneWeapon();

                    return 2;

                }

                //return 0;

            }

            Mod.instance.iconData.ImpactIndicator(Game1.player.currentLocation, Game1.player.Position, IconData.impacts.supree, 6f, new());

            Game1.player.currentLocation.playSound(SpellHandle.Sounds.yoba.ToString());

            Mod.instance.AttuneWeapon(compare);

            return 3;

        }


    }


}
