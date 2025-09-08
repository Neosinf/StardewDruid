using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Cast.Ether;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static StardewDruid.Cast.Rite;
using static StardewValley.Minigames.BoatJourney;

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

                        return AttunementIntro(Rite.Rites.witch);

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

            string toolName = Rite.ToolName();

            int attuneUpdate;

            switch (character)
            {

                case CharacterHandle.characters.energies:

                    attuneUpdate = AttunementCompare(Rite.Rites.weald);

                    switch (attuneUpdate)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1239").Tokens(new { tool = Game1.player.CurrentItem.Name, });

                            break;

                        /*case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1246").Tokens(new { tool = Game1.player.CurrentTool.Name, }) +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.1249");

                            break;*/

                        case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1255").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 2:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1263").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                    }

                    return generate;

                case CharacterHandle.characters.waves:

                    attuneUpdate = AttunementCompare(Rite.Rites.mists);

                    switch (attuneUpdate)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1239").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        /*case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1290").Tokens(new { tool = Game1.player.CurrentTool.Name, }) +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.1293");


                            break;*/

                        case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1300").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 2:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1308").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                    }

                    return generate;

                case CharacterHandle.characters.star_altar:

                    attuneUpdate = AttunementCompare(Rite.Rites.stars);

                    switch (attuneUpdate)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1239").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        /*case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1334").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;*/

                        case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1341").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 2:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1348").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                    }

                    return generate;

                case CharacterHandle.characters.monument_priesthood:

                    attuneUpdate = AttunementCompare(Rite.Rites.fates);

                    switch (attuneUpdate)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1239").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        /*case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1373").Tokens(new { tool = Game1.player.CurrentTool.Name, }) +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.1375") +
                                Mod.instance.Helper.Translation.Get("CharacterHandle.1376");

                            break;*/

                        case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1382").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 2:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1389").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                    }

                    return generate;

                case CharacterHandle.characters.dragon_statue:

                    attuneUpdate = AttunementCompare(Rite.Rites.ether);

                    switch (attuneUpdate)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1239").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        /*case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1414") +
                                            Mod.instance.Helper.Translation.Get("CharacterHandle.1415");

                            break;*/

                        case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1421").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 2:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1428").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                    }

                    return generate;

                case CharacterHandle.characters.crow_brazier:

                    attuneUpdate = AttunementCompare(Rite.Rites.witch);

                    switch (attuneUpdate)
                    {

                        case 0:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.1239").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        /*case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.315.4").Tokens(new { tool = Game1.player.CurrentTool.Name, }) + Mod.instance.Helper.Translation.Get("CharacterHandle.315.5");

                            break;*/

                        case 1:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.315.6").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                        case 2:

                            generate.intro = Mod.instance.Helper.Translation.Get("CharacterHandle.315.7").Tokens(new { tool = Game1.player.CurrentTool.Name, });

                            break;

                    }

                    return generate;



            }
            return generate;

        }

        public static string AttunementIntro(Rite.Rites compare)
        {

            switch (AttunementCompare(compare))
            {
                //return Mod.instance.Helper.Translation.Get("CharacterHandle.1557").Tokens(new { tool = Rite.ToolName(), });
                case 1:
                    return Mod.instance.Helper.Translation.Get("CharacterHandle.1577").Tokens(new { tool = Rite.ToolName(), });

                case 2:
                    return Mod.instance.Helper.Translation.Get("CharacterHandle.1584").Tokens(new { tool = Rite.ToolName(), rite = RiteData.RiteNames(compare), });

            }

            return null;

        }

        public static int AttunementCompare(Rite.Rites compare)
        {

            Rite.ritetools toolType = Rite.ToolType();

            switch (toolType)
            {

                case Rite.ritetools.none:
                case Rite.ritetools.megabomb:
                case Rite.ritetools.bomb:
                case Rite.ritetools.cherry:
                case Rite.ritetools.dragon:
                case Rite.ritetools.stone:

                    return 0;

            }

            string toolName = Rite.ToolName();

            if (Mod.instance.save.attunement.ContainsKey(toolName))
            {

                if (Mod.instance.save.attunement[toolName] == compare)
                {

                    return 1;


                }

            }

            return 2;

        }

        public static void AttunementUpdate(Rite.Rites compare)
        {

            switch (AttunementCompare(compare))
            {

                case 1:

                    Mod.instance.iconData.ImpactIndicator(Game1.player.currentLocation, Game1.player.Position, IconData.impacts.supree, 6f, new());

                    Game1.player.currentLocation.playSound(SpellHandle.Sounds.yoba.ToString());

                    Mod.instance.save.attunement.Remove(Rite.ToolName());

                    Mod.instance.rite.Shutdown();

                    Mod.instance.SyncPreferences();

                    break;

                case 2:


                    Mod.instance.iconData.ImpactIndicator(Game1.player.currentLocation, Game1.player.Position, IconData.impacts.supree, 6f, new());

                    Game1.player.currentLocation.playSound(SpellHandle.Sounds.yoba.ToString());

                    Mod.instance.save.attunement[Rite.ToolName()] = compare;

                    Mod.instance.rite.Shutdown();

                    Mod.instance.SyncPreferences();

                    break;

            }


        }

    }

}
