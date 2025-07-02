using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewDruid.Handle;

namespace StardewDruid.Dialogue
{
    public static class DialogueIntroduction
    {

        public static string DialogueApproach(CharacterHandle.characters character)
        {

            switch (character)
            {

                // companion characters

                case CharacterHandle.characters.Effigy:

                    if (Mod.instance.save.milestone == QuestHandle.milestones.weald_weapon || Mod.instance.save.milestone == QuestHandle.milestones.mists_weapon)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.40");

                    }

                    if (Game1.player.currentLocation.IsFarm)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.47") +
                            Mod.instance.Helper.Translation.Get("CharacterHandle.48");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.52");

                case CharacterHandle.characters.Revenant:

                    if (Mod.instance.save.milestone == QuestHandle.milestones.stars_weapon)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.59");

                    }
                    return Mod.instance.Helper.Translation.Get("CharacterHandle.62");

                case CharacterHandle.characters.Jester:

                    if (Mod.instance.save.milestone == QuestHandle.milestones.fates_weapon)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.69");

                    }

                    if (Mod.instance.save.milestone == QuestHandle.milestones.fates_enchant)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.76");

                    }

                    if (Game1.player.currentLocation.IsFarm)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.83");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.87");

                case CharacterHandle.characters.Buffin:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.91");

                case CharacterHandle.characters.Shadowtin:

                    if (Game1.player.currentLocation.IsFarm)
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.98") +
                            Mod.instance.Helper.Translation.Get("CharacterHandle.99");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.103");

                case CharacterHandle.characters.Blackfeather:

                    if (Mod.instance.questHandle.IsGiven(QuestHandle.questBlackfeather))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.315.3");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.323.1");


                case CharacterHandle.characters.Marlon:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.343.5");

                case CharacterHandle.characters.Aldebaran:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.343.6");

                case CharacterHandle.characters.recruit_one:
                case CharacterHandle.characters.recruit_two:
                case CharacterHandle.characters.recruit_three:
                case CharacterHandle.characters.recruit_four:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.361.1").Tokens(new { villager = Mod.instance.save.recruits[character].display, title = RecruitHandle.RecruitTitle(Mod.instance.save.recruits[character].name), name = Game1.player.Name, });

                case CharacterHandle.characters.PalBat:
                case CharacterHandle.characters.PalSlime:
                case CharacterHandle.characters.PalSpirit:
                case CharacterHandle.characters.PalGhost:
                case CharacterHandle.characters.PalSerpent:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.386.1").Tokens(new { name = PalHandle.PalName(character), title = PalHandle.PalScheme(character) });

                // other characters

                case CharacterHandle.characters.energies:

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.swordWeald))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.336.1");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.107");

                // ---------------------------------------------------

                case CharacterHandle.characters.attendant:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.310.1");

                case CharacterHandle.characters.spring_bench:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.386.2");

                case CharacterHandle.characters.spring_steephouse:


                    if (Mod.instance.questHandle.IsComplete(QuestHandle.distillery))
                    {

                        CharacterHandle.OpenInventory(CharacterHandle.characters.spring_bench);

                    }
                    else
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueInventory.386.3");

                    }

                    break;

                case CharacterHandle.characters.spring_batchhouse:


                    if (Mod.instance.questHandle.IsComplete(QuestHandle.distillery))
                    {

                        CharacterHandle.OpenInventory(CharacterHandle.characters.spring_vintner);

                    }
                    else
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueInventory.386.4");

                    }

                    break;

                case CharacterHandle.characters.spring_packhouse:


                    if (Mod.instance.questHandle.IsComplete(QuestHandle.distillery))
                    {

                        CharacterHandle.OpenInventory(CharacterHandle.characters.spring_packer);

                    }
                    else
                    {

                        return Mod.instance.Helper.Translation.Get("DialogueInventory.386.5");

                    }

                    break;

                // ---------------------------------------------------

                case CharacterHandle.characters.waves:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.111");

                case CharacterHandle.characters.herbalism:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.115");

                case CharacterHandle.characters.anvil:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.373.1");

                case CharacterHandle.characters.keeper:

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.swordWeald))
                    {

                        return Mod.instance.Helper.Translation.Get("CharacterHandle.337.1");

                    }

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.7");

                // epitaphs

                case CharacterHandle.characters.epitaph_prince:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.8");

                case CharacterHandle.characters.epitaph_isles:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.9");

                case CharacterHandle.characters.epitaph_knoll:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.10");

                case CharacterHandle.characters.epitaph_servants_oak:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.11");

                case CharacterHandle.characters.epitaph_servants_holly:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.26");

                case CharacterHandle.characters.epitaph_kings_oak:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.27");

                case CharacterHandle.characters.epitaph_kings_holly:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.28");

                case CharacterHandle.characters.epitaph_guardian:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.29");

                case CharacterHandle.characters.epitaph_dragon:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.30");

                // engravings

                case CharacterHandle.characters.engraving_left:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.331.1") + Mod.instance.Helper.Translation.Get("CharacterHandle.331.2");

                case CharacterHandle.characters.engraving_right:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.331.3");

                // star chapel

                case CharacterHandle.characters.star_altar:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.340.1");

                case CharacterHandle.characters.star_desk:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.347.1");

                case CharacterHandle.characters.star_bench:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.347.2");

                // clearing

                case CharacterHandle.characters.bearrock:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.372.1");

                // court of fates and chaos

                case CharacterHandle.characters.monument_artisans:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.119");

                case CharacterHandle.characters.monument_priesthood:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.123");

                case CharacterHandle.characters.monument_morticians:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.127");

                case CharacterHandle.characters.monument_chaos:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.131");

                // shrine room

                case CharacterHandle.characters.shrine_engine:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.323.20");

                case CharacterHandle.characters.shrine_forge:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.328.1");

                case CharacterHandle.characters.shrine_desk:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.347.3");

                case CharacterHandle.characters.shrine_locker:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.347.4");

                case CharacterHandle.characters.shrine_shelf:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.347.5");


                // tomb

                case CharacterHandle.characters.dragon_statue:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.340.2");


                // crow gate

                case CharacterHandle.characters.crow_brazier:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.340.3");

                case CharacterHandle.characters.crow_gate:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.347.6");

                // the moors

                case CharacterHandle.characters.cairn_dragon:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.385.1");

                case CharacterHandle.characters.cairn_warrior:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.385.2");

                case CharacterHandle.characters.cairn_witches:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.385.3") + Mod.instance.Helper.Translation.Get("DialogueIntroduction.385.4");


            }

            return null;

        }

        public static string DialogueNevermind(CharacterHandle.characters character)
        {

            switch (character)
            {

                // companion characters

                default:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.143");

                // epitaphs

                case CharacterHandle.characters.epitaph_prince:

                case CharacterHandle.characters.epitaph_isles:

                case CharacterHandle.characters.epitaph_knoll:

                case CharacterHandle.characters.epitaph_servants_oak:

                case CharacterHandle.characters.epitaph_servants_holly:

                case CharacterHandle.characters.epitaph_kings_oak:

                case CharacterHandle.characters.epitaph_kings_holly:

                case CharacterHandle.characters.epitaph_guardian:

                case CharacterHandle.characters.epitaph_dragon:

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.329.31");

                // interesting

                case CharacterHandle.characters.shrine_locker:

                case CharacterHandle.characters.shrine_shelf:

                case CharacterHandle.characters.crow_gate:

                case CharacterHandle.characters.crow_brazier:

                case CharacterHandle.characters.bearrock:

                case CharacterHandle.characters.cairn_dragon:

                case CharacterHandle.characters.cairn_warrior:

                case CharacterHandle.characters.cairn_witches:

                    return Mod.instance.Helper.Translation.Get("DialogueIntroduction.347.7");


            }

        }

    }


}
