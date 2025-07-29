using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Battle;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Network;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using static StardewDruid.Character.Character;

namespace StardewDruid.Journal
{
    public class PalPage : DruidJournal
    {

        public PalPage(string RecruitId, int Record) : base(RecruitId, Record) 
        {

        }

        public override void populateInterface()
        {

            parentJournal = journalTypes.relics;

            type = journalTypes.palPage;

            interfaceComponents = new()
            {
                // ------------------------------------------

                [110] = addButton(journalButtons.renamePal),

                [111] = addButton(journalButtons.schemePal),

                [112] = addButton(journalButtons.summonPal),

                [113] = addButton(journalButtons.dismissPal),

                [114] = addButton(journalButtons.rewildPal),

                [201] = addButton(journalButtons.back),

                [301] = addButton(journalButtons.exit),

            };

        }
        public override void activateInterface()
        {

            resetInterface();

        }

        public override void populateContent()
        {

            CharacterHandle.characters character = Enum.Parse<CharacterHandle.characters>(journalId);

            PalData data = Mod.instance.save.pals[character];

            int level = PalHandle.UnitLevel(data.experience);

            // ----------------------------- title

            int start = 0;

            title = PalHandle.PalScheme(data.type);

            // ===============================================================================================


            contentComponents = new();

            ContentComponent recruitPortrait = new(ContentComponent.contentTypes.battleportrait, "portrait")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 336, yPositionOnScreen + 48, 256, 256)
            };
            CharacterHandle.characters spriteType = PalHandle.CharacterType(character, data.scheme);

            recruitPortrait.textures[0] = CharacterHandle.CharacterTexture(spriteType);

            recruitPortrait.textureSources[0] = PalHandle.CharacterSource(character);

            contentComponents[start++] = recruitPortrait;

            // ===============================================================================================

            int textStart = 48;

            ContentComponent recruitTitle = new(ContentComponent.contentTypes.text, "title")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + textStart, 320, 64)
            };

            recruitTitle.text[0] = data.name;

            recruitTitle.SetText(416);

            contentComponents[start++] = recruitTitle;

            textStart += (int)recruitTitle.textMeasures[0].Y + 16;

            // -----------------------------------------------------

            ContentComponent recruitLevel = new(ContentComponent.contentTypes.textsmall, "level")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + textStart, 320, 64)
            };

            recruitLevel.text[0] = StringData.LevelStrings(level);

            recruitLevel.SetText(416);

            contentComponents[start++] = recruitLevel;

            textStart += (int)recruitLevel.textMeasures[0].Y + 12;

            // -----------------------------------------------------

            ContentComponent recruitExperience = new(ContentComponent.contentTypes.textsmall, "experience")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + textStart, 320, 64)
            };

            int nextlevel = PalHandle.NextLevel(data.experience);

            if (nextlevel == -1)
            {

                recruitExperience.text[0] = StringData.Strings(StringData.stringkeys.maxLevel);

            }
            else
            {

                recruitExperience.text[0] = StringData.Strings(StringData.stringkeys.experience) + data.experience + StringData.slash + nextlevel;

            }

            recruitExperience.SetText(416);

            contentComponents[start++] = recruitExperience;

            textStart += (int)recruitExperience.textMeasures[0].Y + 12;

            // STATS ===============================================================================================

            int statStart = textStart;

            ContentComponent caught = new(ContentComponent.contentTypes.textsmall, "hired")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + statStart, 320, 64)
            };

            caught.text[0] = StringData.Strings(StringData.stringkeys.numberHired) + data.hired.ToString() + StringData.slash + data.caught.ToString();

            caught.SetText(416);

            contentComponents[start++] = caught;

            statStart += (int)caught.textMeasures[0].Y + 12;

            // ----------------------------------------------------------------

            ContentComponent health = new(ContentComponent.contentTypes.textsmall, "health")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + statStart, 320, 56)
            };

            health.text[0] = StringData.Strings(StringData.stringkeys.healthLevel) + PalHandle.HealthLevel(data.type,level,data.health);

            health.SetText(416);

            contentComponents[start++] = health;

            statStart += (int)health.textMeasures[0].Y + 12;

            // ----------------------------------------------------------------

            int specialStart = statStart;

            ContentComponent attack = new(ContentComponent.contentTypes.textsmall, "attack")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + statStart, 320, 56)
            };

            attack.text[0] = StringData.Strings(StringData.stringkeys.attackLevel) + PalHandle.AttackLevel(data.type, level, data.attack);

            attack.SetText(416);

            contentComponents[start++] = attack;

            statStart += (int)attack.textMeasures[0].Y + 12;

            // ----------------------------------------------------------------

            ContentComponent speed = new(ContentComponent.contentTypes.textsmall, "speed")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + statStart, 320, 56)
            };

            speed.text[0] = StringData.Strings(StringData.stringkeys.speedLevel) + PalHandle.SpeedLevel(data.type, level, data.speed);

            speed.SetText(416);

            contentComponents[start++] = speed;

            statStart += (int)speed.textMeasures[0].Y + 12;

            // ----------------------------------------------------------------

            ContentComponent resist = new(ContentComponent.contentTypes.textsmall, "resist")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + statStart, 320, 56)
            };

            resist.text[0] = StringData.Strings(StringData.stringkeys.resistLevel) + PalHandle.ResistLevel(data.type, level, data.speed);

            resist.SetText(416);

            contentComponents[start++] = resist;

            statStart += (int)resist.textMeasures[0].Y + 12;

            // ----------------------------------------------------------------

            ContentComponent wins = new(ContentComponent.contentTypes.textsmall, "wins")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + statStart, 320, 56)
            };

            wins.text[0] = StringData.Strings(StringData.stringkeys.winsAmount) + PalHandle.BattleWins(data.type);

            wins.SetText(320);

            contentComponents[start++] = wins;


            // SPECIALS ===============================================================================================

            ContentComponent specialAbilities = new(ContentComponent.contentTypes.textsmall, "abilities")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 416, yPositionOnScreen + specialStart, 320, 56)
            };

            specialAbilities.text[0] = "Special Abilities:";

            specialAbilities.SetText(352);

            contentComponents[start++] = specialAbilities;

            specialStart += (int)specialAbilities.textMeasures[0].Y + 16;

            // ----------------------------------------------------------------

            Dictionary<BattleCombatant.battleoptions, BattleAbility.battleabilities> specials = PalHandle.SpecialMoves(new(), data.type, level);

            foreach(KeyValuePair<BattleCombatant.battleoptions, BattleAbility.battleabilities> abilities in specials)
            {

                BattleAbility ability = new(abilities.Value);

                ContentComponent special = new(ContentComponent.contentTypes.textsmall, ability.button)
                {
                    bounds = new Rectangle(xPositionOnScreen + (width / 2) - 416, yPositionOnScreen + specialStart, 320, 64)
                };

                special.text[0] = ability.title;

                special.SetText(352);

                contentComponents[start++] = special;

                specialStart += (int)special.textMeasures[0].Y + 12;

            }

            int love = PalHandle.LoveLevel(data.love);

            ContentComponent lovebar = new(ContentComponent.contentTypes.lovebar, "love")
            {
            
                bounds = new Rectangle(xPositionOnScreen + (width / 2) + 256, yPositionOnScreen + 48, 320, 64)
            
            };

            lovebar.text[0] = love.ToString();

            contentComponents[start++] = lovebar;

        }

        public virtual void RenamePal(string nameInput)
        {

            CharacterHandle.characters entity = Enum.Parse<CharacterHandle.characters>(journalId);

            Mod.instance.save.pals[entity].name = Utility.FilterDirtyWords(nameInput);

            Game1.activeClickableMenu.exitThisMenu(false);

            populateContent();

            activateInterface();

            Game1.activeClickableMenu = this;

            Mod.instance.SyncPreferences();

        }

        public override void pressButton(journalButtons button)
        {

            CharacterHandle.characters entity = Enum.Parse<CharacterHandle.characters>(journalId);

            switch (button)
            {

                case journalButtons.summonPal:

                    if (!Mod.instance.save.pals.ContainsKey(entity))
                    {

                        Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                        return;

                    }

                    Mod.instance.save.pals[entity].PalLoad(Character.Character.mode.track);

                    Mod.instance.RegisterMessage(Mod.instance.save.pals[entity].name + StringData.Strings(StringData.stringkeys.joinedPlayer), 0, true);

                    exitThisMenu();

                    Mod.instance.SyncPreferences();

                    return;

                case journalButtons.dismissPal:

                    if (!Mod.instance.save.pals.ContainsKey(entity))
                    {

                        Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                        return;

                    }

                    Mod.instance.save.pals[entity].PalLoad(Character.Character.mode.home);

                    Mod.instance.RegisterMessage(Mod.instance.save.pals[entity].name + StringData.Strings(StringData.stringkeys.returnedHome), 0, true);

                    exitThisMenu();

                    Mod.instance.SyncPreferences();

                    return;

                case journalButtons.rewildPal:

                    if (!Mod.instance.save.pals.ContainsKey(entity))
                    {

                        Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                        return;

                    }

                    Mod.instance.save.pals[entity].PalLoad(Character.Character.mode.limbo);

                    Mod.instance.RegisterMessage(Mod.instance.save.pals[entity].name + StringData.Strings(StringData.stringkeys.wandering), 0, true);

                    exitThisMenu();

                    Mod.instance.SyncPreferences();

                    return;

                case journalButtons.schemePal:

                    Mod.instance.save.pals[entity].PalScheme();

                    populateContent();

                    activateInterface();

                    Mod.instance.SyncPreferences();

                    return;

                case journalButtons.renamePal:

                    NamingMenu namingMenu = new(RenamePal, PalHandle.PalScheme(Mod.instance.save.pals[entity].type), Mod.instance.save.pals[entity].name);

                    Game1.activeClickableMenu.exitThisMenu(false);

                    Game1.activeClickableMenu = namingMenu;

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
