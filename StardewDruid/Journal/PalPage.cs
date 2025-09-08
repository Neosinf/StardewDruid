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
using System.Reflection.Emit;

namespace StardewDruid.Journal
{
    public class PalPage : DruidJournal
    {

        public PalPage(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
        {

        }

        public override void populateInterface()
        {

            parent = journalTypes.relics;

            type = journalTypes.palPage;

            interfaceComponents = new()
            {
                // ------------------------------------------

                [110] = addButton(journalButtons.renamePal),

                [111] = addButton(journalButtons.schemePal),

                [112] = addButton(journalButtons.summonPal),

                [113] = addButton(journalButtons.dismissPal),

                [114] = addButton(journalButtons.rewildPal),

                [201] = addButton(journalButtons.previous),

                [301] = addButton(journalButtons.exit),

            };

        }

        public override void populateContent()
        {

            CharacterHandle.characters character = Enum.Parse<CharacterHandle.characters>(parameters[0]);

            PalData data = Mod.instance.save.pals[character];

            int Level = PalHandle.UnitLevel(data.experience);

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

            recruitTitle.textScales[0] = 0.8f;

            recruitTitle.SetText(416);

            contentComponents[start++] = recruitTitle;

            textStart += (int)recruitTitle.textMeasures[0].Y + 16;

            // -----------------------------------------------------

            ContentComponent recruitLevel = new(ContentComponent.contentTypes.text, "level")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + textStart, 320, 64)
            };

            recruitLevel.text[0] = StringData.Get(StringData.str.level, new { level = Level });

            recruitLevel.textScales[0] = 0.8f;

            recruitLevel.SetText(416);

            contentComponents[start++] = recruitLevel;

            textStart += (int)recruitLevel.textMeasures[0].Y + 12;

            // -----------------------------------------------------

            ContentComponent recruitExperience = new(ContentComponent.contentTypes.text, "experience")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + textStart, 320, 64)
            };

            int nextlevel = PalHandle.NextLevel(data.experience);

            if (nextlevel == -1)
            {

                recruitExperience.text[0] = StringData.Get(StringData.str.maxLevel);

            }
            else
            {

                recruitExperience.text[0] = StringData.Get(StringData.str.experience) + data.experience + StringData.slash + nextlevel;

            }

            recruitLevel.textScales[0] = 0.8f;

            contentComponents[start++] = recruitExperience;

            textStart += (int)recruitExperience.textMeasures[0].Y + 12;

            // STATS ===============================================================================================

            int statStart = textStart;

            ContentComponent caught = new(ContentComponent.contentTypes.text, "hired")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + statStart, 320, 64)
            };

            caught.text[0] = StringData.Get(StringData.str.numberHired) + data.hired.ToString() + StringData.slash + data.caught.ToString();

            caught.textScales[0] = 0.8f;

            caught.SetText(416);

            contentComponents[start++] = caught;

            statStart += (int)caught.textMeasures[0].Y + 12;

            // ----------------------------------------------------------------

            ContentComponent health = new(ContentComponent.contentTypes.text, "health")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + statStart, 320, 56)
            };

            health.text[0] = StringData.Get(StringData.str.healthLevel) + PalHandle.HealthLevel(data.type,Level,data.health);

            health.textScales[0] = 0.8f;

            health.SetText(416);

            contentComponents[start++] = health;

            statStart += (int)health.textMeasures[0].Y + 12;

            // ----------------------------------------------------------------

            int specialStart = statStart;

            ContentComponent attack = new(ContentComponent.contentTypes.text, "attack")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + statStart, 320, 56)
            };

            attack.text[0] = StringData.Get(StringData.str.attackLevel) + PalHandle.AttackLevel(data.type, Level, data.attack);

            attack.textScales[0] = 0.8f;

            attack.SetText(416);

            contentComponents[start++] = attack;

            statStart += (int)attack.textMeasures[0].Y + 12;

            // ----------------------------------------------------------------

            ContentComponent speed = new(ContentComponent.contentTypes.text, "speed")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + statStart, 320, 56)
            };

            speed.text[0] = StringData.Get(StringData.str.speedLevel) + PalHandle.SpeedLevel(data.type, Level, data.speed);

            speed.textScales[0] = 0.8f;

            speed.SetText(416);

            contentComponents[start++] = speed;

            statStart += (int)speed.textMeasures[0].Y + 12;

            // ----------------------------------------------------------------

            ContentComponent resist = new(ContentComponent.contentTypes.text, "resist")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + statStart, 320, 56)
            };

            resist.text[0] = StringData.Get(StringData.str.resistLevel) + PalHandle.ResistLevel(data.type, Level, data.speed);

            resist.textScales[0] = 0.8f;

            resist.SetText(416);

            contentComponents[start++] = resist;

            statStart += (int)resist.textMeasures[0].Y + 12;

            // ----------------------------------------------------------------

            ContentComponent wins = new(ContentComponent.contentTypes.text, "wins")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 32, yPositionOnScreen + statStart, 320, 56)
            };

            wins.text[0] = StringData.Get(StringData.str.winsAmount) + PalHandle.BattleWins(data.type);

            wins.SetText(320);

            contentComponents[start++] = wins;


            // SPECIALS ===============================================================================================

            ContentComponent specialAbilities = new(ContentComponent.contentTypes.text, "abilities")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 416, yPositionOnScreen + specialStart, 320, 56)
            };

            specialAbilities.text[0] = "Special Abilities:";

            specialAbilities.SetText(352);

            contentComponents[start++] = specialAbilities;

            specialStart += (int)specialAbilities.textMeasures[0].Y + 16;

            // ----------------------------------------------------------------

            Dictionary<BattleCombatant.battleoptions, BattleAbility.battleabilities> specials = PalHandle.SpecialMoves(new(), data.type, Level);

            foreach(KeyValuePair<BattleCombatant.battleoptions, BattleAbility.battleabilities> abilities in specials)
            {

                BattleAbility ability = new(abilities.Value);

                ContentComponent special = new(ContentComponent.contentTypes.text, ability.button)
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

            CharacterHandle.characters entity = Enum.Parse<CharacterHandle.characters>(parameters[0]);

            Mod.instance.save.pals[entity].name = Utility.FilterDirtyWords(nameInput);

            Game1.activeClickableMenu.exitThisMenu(false);

            populateContent();

            activateInterface();

            Game1.activeClickableMenu = this;

            Mod.instance.SyncPreferences();

        }

        public override void pressButton(journalButtons button)
        {

            CharacterHandle.characters entity = Enum.Parse<CharacterHandle.characters>(parameters[0]);

            switch (button)
            {

                case journalButtons.summonPal:

                    if (!Mod.instance.save.pals.ContainsKey(entity))
                    {

                        Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                        return;

                    }

                    Mod.instance.save.pals[entity].PalLoad(Character.Character.mode.track);

                    Mod.instance.RegisterMessage(Mod.instance.save.pals[entity].name + StringData.Get(StringData.str.joinedPlayer), 0, true);

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

                    Mod.instance.RegisterMessage(Mod.instance.save.pals[entity].name + StringData.Get(StringData.str.returnedHome), 0, true);

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

                    Mod.instance.RegisterMessage(Mod.instance.save.pals[entity].name + StringData.Get(StringData.str.wandering), 0, true);

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

                default:

                    base.pressButton(button);

                    break;

            }

        }


    }

}
