using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Network;
using StardewValley.Quests;
using System;
using System.Collections.Generic;

namespace StardewDruid.Journal
{
    public class RecruitPage : DruidJournal
    {

        public Texture2D portrait;

        public RecruitPage(string RecruitId, int Record) : base(RecruitId, Record) 
        {

        }

        public override void populateInterface()
        {

            parentJournal = journalTypes.ledger;

            type = journalTypes.companion;

            interfaceComponents = new()
            {

                // ------------------------------------------

                [111] = addButton(journalButtons.summonCompanion),

                [112] = addButton(journalButtons.dismissCompanion),

                //-------------------------------------------

                [201] = addButton(journalButtons.back),

                [301] = addButton(journalButtons.exit),

            };

        }

        public override void populateContent()
        {

            CharacterHandle.characters character = Enum.Parse<CharacterHandle.characters>(journalId);

            RecruitHandle recruit = Mod.instance.save.recruits[character];

            int start = 0;

            title = recruit.display;

            // ===============================================================================================

            NPC villager = CharacterHandle.FindVillager(recruit.name);

            portrait = villager.Portrait;

            contentComponents = new();

            ContentComponent recruitPortrait = new(ContentComponent.contentTypes.portrait, "portrait")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 352, yPositionOnScreen + 48, 320, 320)
            };

            recruitPortrait.textures[0] = portrait;

            if (portrait.Width > 64)
            {

                recruitPortrait.textureSources[0] = new Rectangle(0, 0, portrait.Width / 2, portrait.Width / 2);

            }
            else
            {

                recruitPortrait.textureSources[0] = new Rectangle(0, 0, 64, 64);

            }

            contentComponents[start++] = recruitPortrait;

            // ===============================================================================================

            int textStart = 48;

            ContentComponent recruitTitle = new(ContentComponent.contentTypes.text, "title")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2), yPositionOnScreen + textStart, 320, 64)
            };

            recruitTitle.text[0] = RecruitHandle.RecruitTitle(recruit.name);

            recruitTitle.SetText(320);

            contentComponents[start++] = recruitTitle;

            textStart += (int)recruitTitle.textMeasures[0].Y + 24;

            // -----------------------------------------------------

            ContentComponent recruitLevel = new(ContentComponent.contentTypes.text, "level")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2), yPositionOnScreen + textStart, 320, 64)
            };

            recruitLevel.text[0] = RecruitHandle.RecruitLevel(recruit.level);

            recruitLevel.SetText(320);

            contentComponents[start++] = recruitLevel;

            textStart += (int)recruitLevel.textMeasures[0].Y + 24;

            // -----------------------------------------------------

            ContentComponent recruitExperience = new(ContentComponent.contentTypes.text, "experience")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2), yPositionOnScreen + textStart, 320, 64)
            };

            int nextlevel = RecruitHandle.NextLevel(recruit.level);

            if(nextlevel == -1)
            {

                recruitExperience.text[0] = StringData.Strings(StringData.stringkeys.maxLevel);
            
            }
            else
            {

                recruitExperience.text[0] = StringData.Strings(StringData.stringkeys.experience) + recruit.level + StringData.slash + nextlevel;

            }

            recruitExperience.SetText(320);

            contentComponents[start++] = recruitExperience;

            textStart += (int)recruitExperience.textMeasures[0].Y + 24;

            // -----------------------------------------------------

            ContentComponent recruitFriendship = new(ContentComponent.contentTypes.text, "friendship")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2), yPositionOnScreen + textStart, 320, 64)
            };

            int friendship = 0;

            if (Game1.player.friendshipData.ContainsKey(villager.Name))
            {

                friendship = Game1.player.friendshipData[villager.Name].Points;

            }

            recruitFriendship.text[0] = StringData.Strings(StringData.stringkeys.friendship) + friendship.ToString();

            recruitFriendship.SetText(320);

            contentComponents[start++] = recruitFriendship;

            textStart += (int)recruitFriendship.textMeasures[0].Y + 24;

            // -----------------------------------------------------

            textStart += 32;

            ContentComponent recruitSpecial = new(ContentComponent.contentTypes.text, "special")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 352, yPositionOnScreen + textStart, 640, 64)
            };

            recruitSpecial.text[0] = RecruitHandle.RecruitSpecial(recruit.name);

            recruitSpecial.SetText(704);

            contentComponents[start++] = recruitSpecial;

        }


        public override void activateInterface()
        {

            resetInterface();

        }

        public override void pressButton(journalButtons button)
        {

            CharacterHandle.characters type = Enum.Parse<CharacterHandle.characters>(journalId);

            switch (button)
            {

                case journalButtons.summonCompanion:

                    if (Mod.instance.trackers.ContainsKey(type))
                    {

                        Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                        return;

                    }

                    if (RecruitHandle.RecruitLoad(type))
                    {

                        Mod.instance.RegisterMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.361.3").Tokens(new { name = Mod.instance.save.recruits[type].display, }), 0, true);

                    }
                    else
                    {

                        Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                        return;

                    }

                    exitThisMenu();

                    return;

                case journalButtons.dismissCompanion:

                    if (!Mod.instance.characters.ContainsKey(type))
                    {

                        Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                        return;

                    }

                    if (Mod.instance.characters[type].currentLocation == null)
                    {

                        Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                        return;

                    }

                    RecruitHandle hero = Mod.instance.save.recruits[type];

                    Mod.instance.RegisterMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.361.4").Tokens(new { name = Mod.instance.save.recruits[type].display, }), 0, true);

                    RecruitHandle.RecruitRemove(type);

                    exitThisMenu();

                    return;

                case journalButtons.back:

                    DruidJournal.openJournal(parentJournal, null, record);

                    break;

                default:

                    base.pressButton(button);

                    break;

            }

        }

    }

}
