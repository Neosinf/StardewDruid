using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Cast.Ether;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Event.Scene;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Menus;
using StardewValley.Network;
using StardewValley.Objects;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Schema;

namespace StardewDruid.Journal
{
    public class DruidJournal : IClickableMenu
    {

        public enum journalTypes
        {
            none,

            // main
            quests,
            masteries,
            relics,
            alchemy,
            potions,
            orders,
            ledger,
            dragon,

            // quests
            questPage,
            questionPage,

            // masteries
            masteryOverview,
            masteryPage,
            effects,
            effectPage,
            lore,
            lorePage,

            // relics
            relicPage,

            // alchemy
            omens,
            trophies,

            // apothecary
            powders,
            goods,

            // companions
            companion,
            palPage,

            // other
            guilds,
            guildPage,
            goodsDistillery,
            distillery,
            distilleryInventory,
            distilleryEstimated,
            distilleryRecent,

            battle,
            
        }

        public journalTypes type = journalTypes.quests;

        public journalTypes parent = journalTypes.quests;

        public int record = 0;

        public List<string> parameters = new();

        public string title = StringData.Get(StringData.str.stardewDruid);

        public enum journalButtons
        {

            openQuests,
            openMasteries,
            openRelics,
            openAlchemy,
            openPotions,
            openOmens,
            openOrders,
            openCompanions,
            openDragonomicon,

            refresh,
            openEffects,
            openLore,
            openPowders,
            openTrophies,
            openGoods,
            getHint,
            skipQuest,
            replayTomorrow,
            replayQuest,
            cancelReplay,
            clearBuffs,
            
            openGuilds,
            openGoodsDistillery,
            openDistillery,
            openDistilleryInventory,
            openProductionEstimated,
            openProductionRecent,

            exit,

            back,
            start,
            previous,

            scrollUp,
            scrollBar,
            scrollDown,
            forward,
            end,
            active,
            reverse,

            viewEffect,
            viewQuest,
            dragonCopy,
            dragonSave,

            summonCompanion,
            dismissCompanion,

            summonPal,
            dismissPal,
            schemePal,
            renamePal,
            rewildPal,

            HP,
            STM,

            levelUp,

        }

        public int focus;

        public bool scrolling;

        public bool interfacing;

        public bool browsing;

        public int pagination;

        public bool detail;

        public int scrolled;

        public Microsoft.Xna.Framework.Rectangle scrollBox;

        public int scrollId;

        public int scrollHeight = 512;

        public Microsoft.Xna.Framework.Rectangle contentBox = new();

        public Microsoft.Xna.Framework.Vector2 titlePosition = new();

        public Dictionary<int, InterfaceComponent> interfaceComponents = new();

        public Dictionary<journalButtons, int> interfaceRegistry = new();

        public Dictionary<int, ContentComponent> contentComponents = new();

        public Dictionary<int, ContentComponent> otherComponents = new();

        public int contentColumns = 1;

        public DruidJournal()
         : base(0, 0, 0, 0, true)
        {

        }

        public DruidJournal(journalTypes Type, List<string> Parameters)
          : base(0, 0, 0, 0, true)
        {

            type = Type;

            title = JournalData.JournalTitle(type);

            parameters = Parameters;

            width = 960;

            height = 640;

            Vector2 centeringOnScreen = Utility.getTopLeftPositionForCenteringOnScreen(width, height, 0, 0);

            xPositionOnScreen = (int)centeringOnScreen.X;

            yPositionOnScreen = (int)centeringOnScreen.Y + 32;

            prepareContent();

            populateContent();

            populateInterface();

            activateInterface();

        }

        public virtual void prepareContent()
        {

        }

        public static journalTypes JournalButtonPressed()
        {

            if (Mod.instance.Config.journalButtons.GetState() == SButtonState.Pressed)
            {

                return journalTypes.quests;

            }
            else
            if (Mod.instance.Config.skillsButtons.GetState() == SButtonState.Pressed)
            {

                return journalTypes.effects;

            }
            else
            if (Mod.instance.Config.relicsButtons.GetState() == SButtonState.Pressed)
            {

                return journalTypes.relics;

            }
            else
            if (Mod.instance.Config.herbalismButtons.GetState() == SButtonState.Pressed && Mod.instance.questHandle.IsComplete(QuestHandle.herbalism))
            {

                return journalTypes.potions;

            }

            return journalTypes.none;

        }

        public static journalTypes JournalUnlocked(journalTypes Type)
        {

            if (Mod.instance.magic)
            {

                switch (Type)
                {

                    case journalTypes.dragon:
                    case journalTypes.masteries:
                    case journalTypes.masteryOverview:
                    case journalTypes.masteryPage:
                    case journalTypes.effects:
                    case journalTypes.effectPage:
                    case journalTypes.potions:
                    case journalTypes.powders:
                    case journalTypes.goods:
                    case journalTypes.alchemy:
                    case journalTypes.omens:
                    case journalTypes.trophies:
                    case journalTypes.ledger:
                    case journalTypes.companion:

                        return Type;

                    case journalTypes.quests:

                        return journalTypes.masteries;

                    case journalTypes.relics:

                        return journalTypes.dragon;

                    case journalTypes.lore:

                        return journalTypes.effects;

                }

                return journalTypes.none;

            }

            switch (Type)
            {

                default:
                case journalTypes.quests:
                case journalTypes.questPage:
                case journalTypes.questionPage:
                case journalTypes.relics:
                case journalTypes.relicPage:

                    return Type;

                // --------------------------------

                case journalTypes.masteries:
                case journalTypes.masteryOverview:
                case journalTypes.masteryPage:
                case journalTypes.effects:
                case journalTypes.effectPage:
                case journalTypes.lore:
                case journalTypes.lorePage:
                    
                    if (RelicHandle.HasRelic(IconData.relics.druid_grimoire))
                    {

                        return Type;


                    }

                    break;

                // --------------------------------

                case journalTypes.potions:
                case journalTypes.powders:
                case journalTypes.goods:

                    if (RelicHandle.HasRelic(IconData.relics.druid_apothecary))
                    {

                        return Type;


                    }

                    break;

                // --------------------------------

                case journalTypes.alchemy:
                case journalTypes.omens:
                case journalTypes.trophies:

                    if (RelicHandle.HasRelic(IconData.relics.druid_runeboard))
                    {

                        return Type;

                    }

                    break;

                // --------------------------------

                case journalTypes.dragon:

                    if (RelicHandle.HasRelic(IconData.relics.druid_dragonomicon))
                    {

                        return Type;


                    }
                    break;

                // --------------------------------

                case journalTypes.ledger:
                case journalTypes.companion:
                case journalTypes.palPage:

                    if (RelicHandle.HasRelic(IconData.relics.companion_crest))
                    {

                        return Type;


                    }
                    break;


                // --------------------------------

                case journalTypes.orders:
                case journalTypes.guilds:
                case journalTypes.guildPage:

                    if (RelicHandle.HasRelic(IconData.relics.crest_church))
                    {

                        return Type;


                    }

                    break;

                // --------------------------------

                case journalTypes.distillery:
                case journalTypes.distilleryInventory:
                case journalTypes.distilleryEstimated:
                case journalTypes.distilleryRecent:

                    if (RelicHandle.HasRelic(IconData.relics.crest_dwarf))
                    {

                        return Type;


                    }

                    break;

            }

            return journalTypes.none;

        }

        public static void openJournal(journalTypes Type)
        {

            openJournal(Type,new List<string>());

        }

        public static void openJournal(journalTypes Type, string parameter)
        {

            openJournal(Type, new List<string>(){ parameter});

        }

        public static void openJournal(journalTypes Type, List<string> Parameters)
        {

            Type = JournalUnlocked(Type);

            if(Type == journalTypes.none)
            {

                Game1.playSound("ghost");

            }

            if (Game1.activeClickableMenu != null)
            {

                Game1.activeClickableMenu.exitThisMenu(false);

                Game1.playSound("shwip");

            } else
            {

                Game1.playSound("bigSelect");

            }

            switch (Type)
            {

                default:

                    return;

                case journalTypes.quests:

                    Game1.activeClickableMenu = new QuestJournal(Type, Parameters);

                    break;

                case journalTypes.questPage:

                    Game1.activeClickableMenu = new QuestPage(Type, Parameters);

                    break;

                case journalTypes.questionPage:

                    Game1.activeClickableMenu = new QuestionPage(Type, Parameters);

                    break;

                // --------------------------------

                case journalTypes.masteries:

                    Game1.activeClickableMenu = new MasteryJournal(Type, Parameters);

                    break;

                case journalTypes.masteryOverview:

                    Game1.activeClickableMenu = new MasteryOverview(Type, Parameters);

                    break;

                case journalTypes.masteryPage:

                    Game1.activeClickableMenu = new MasteryPage(Type, Parameters);

                    break;

                // --------------------------------

                case journalTypes.relics:

                    Game1.activeClickableMenu = new RelicJournal(Type, Parameters);

                    break;

                case journalTypes.relicPage:

                    Game1.activeClickableMenu = new RelicPage(Type, Parameters);

                    break;

                // --------------------------------

                case journalTypes.effects:

                    Game1.activeClickableMenu = new EffectJournal(Type, Parameters);

                    break;

                case journalTypes.effectPage:

                    Game1.activeClickableMenu = new EffectPage(Type, Parameters);

                    break;

                case journalTypes.lore:

                    Game1.activeClickableMenu = new LoreJournal(Type, Parameters);

                    break;

                case journalTypes.lorePage:

                    Game1.activeClickableMenu = new LorePage(Type, Parameters);

                    break;

                // --------------------------------

                case journalTypes.alchemy:

                    Game1.activeClickableMenu = new AlchemyJournal(Type, Parameters);

                    break;

                // --------------------------------

                case journalTypes.potions:

                    Game1.activeClickableMenu = new PotionJournal(Type, Parameters);

                    break;

                case journalTypes.powders:

                    Game1.activeClickableMenu = new PowderJournal(Type, Parameters);

                    break;

                case journalTypes.goods:
                case journalTypes.goodsDistillery:

                    Game1.activeClickableMenu = new GoodsJournal(Type, Parameters);

                    break;

                // --------------------------------

                case journalTypes.omens:

                    Game1.activeClickableMenu = new OmenJournal(Type, Parameters);

                    break;

                case journalTypes.trophies:

                    Game1.activeClickableMenu = new TrophyJournal(Type, Parameters);

                    break;

                // --------------------------------

                case journalTypes.orders:

                    Game1.activeClickableMenu = new OrdersJournal(Type, Parameters);

                    break;

                case journalTypes.guilds:

                    Game1.activeClickableMenu = new GuildJournal(Type, Parameters);

                    break;

                case journalTypes.guildPage:

                    Game1.activeClickableMenu = new GuildPage(Type, Parameters);

                    break;

                case journalTypes.distillery:

                    Game1.activeClickableMenu = new DistilleryJournal(Type, Parameters);

                    break;

                case journalTypes.distilleryInventory:

                    ChestHandle.OpenInventory(ChestHandle.chests.Distillery, journalTypes.distillery);
                    
                    break;

                case journalTypes.distilleryEstimated:

                    Game1.activeClickableMenu = new DistilleryEstimated(Type, Parameters);

                    break;

                case journalTypes.distilleryRecent:

                    Game1.activeClickableMenu = new DistilleryRecent(Type, Parameters);

                    break;

                // --------------------------------

                case journalTypes.dragon:

                    Game1.activeClickableMenu = new DragonJournal(Type, Parameters);

                    break;

                // --------------------------------

                case journalTypes.ledger:

                    Game1.activeClickableMenu = new RecruitJournal(Type, Parameters);

                    break;

                case journalTypes.companion:

                    Game1.activeClickableMenu = new RecruitPage(Type, Parameters);

                    break;

                case journalTypes.palPage:

                    Game1.activeClickableMenu = new PalPage(Type, Parameters);

                    break;

            }

        }

        public virtual void populateContent()
        {


        }

        public virtual void ParameterRecord()
        {

            if (parameters.Count == 0)
            {
                return;

            }

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                if (component.Value.id == parameters[0])
                {

                    record = component.Key;

                }

            }

        }

        public virtual void populateInterface()
        {

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.openQuests),
                [102] = addButton(journalButtons.openMasteries),
                [103] = addButton(journalButtons.openRelics),
                [104] = addButton(journalButtons.openAlchemy),
                [105] = addButton(journalButtons.openPotions),
                [106] = addButton(journalButtons.openCompanions),
                [107] = addButton(journalButtons.openOrders),
                [108] = addButton(journalButtons.openDragonomicon),

                [301] = addButton(journalButtons.exit),

            };

        }

        public virtual void activateInterface()
        {

            resetInterface();

            reviseInterface();

        }

        public virtual void resetInterface()
        {

            Vector2 titleSize = Game1.dialogueFont.MeasureString(title);

            titlePosition = new Vector2(xPositionOnScreen + width - (int)titleSize.X - 16, yPositionOnScreen - (int)titleSize.Y - 4);

            interfacing = false;

            browsing = false;

            interfaceRegistry = new();

            foreach (KeyValuePair<int, InterfaceComponent> component in interfaceComponents)
            {

                component.Value.active = true;

                component.Value.hover = 0;

                component.Value.fade = 1f;

                interfaceRegistry[component.Value.button] = component.Key;

            }

        }

        public InterfaceComponent addButton(journalButtons Button)
        {

            int xP = xPositionOnScreen;

            int yP = yPositionOnScreen;

            //int xH = xP - 68 - 68;

            int xR = xP + width;

            int yT = yP - 36;

            int yB = yP + height;

            switch (Button)
            {

                // ======================================  left side

                case journalButtons.back:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 36), IconData.displays.forward, new() { flip = true, });

                case journalButtons.start:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 36), IconData.displays.end, new() { flip = true, });

                case journalButtons.previous:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 36), IconData.displays.previous, new() );

                // quest

                case journalButtons.getHint:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 36), IconData.displays.question, new());

                case journalButtons.viewQuest:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 36), IconData.displays.quest, new());

                case journalButtons.viewEffect:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 36), IconData.displays.skills, new());

                case journalButtons.skipQuest:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 68 + 36), IconData.displays.skip, new());

                case journalButtons.replayQuest:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 68 + 36), IconData.displays.replay, new());

                case journalButtons.replayTomorrow:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 68 + 36), IconData.displays.flag, new());

                case journalButtons.cancelReplay:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 68 + 36), IconData.displays.active, new());

                // skills

                case journalButtons.openEffects:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 36), IconData.displays.effects, new());

                case journalButtons.openLore:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 68 + 36), IconData.displays.lore, new());


                // apothecary

                case journalButtons.openPowders:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 36), IconData.displays.powderbox, new());

                case journalButtons.openGoods:
                case journalButtons.openGoodsDistillery:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 36), IconData.displays.goods, new());


                // alchemy

                case journalButtons.openOmens:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 36), IconData.displays.omens, new());

                case journalButtons.openTrophies:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 36), IconData.displays.trophies, new());


                // distillery

                case journalButtons.openGuilds:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 36), IconData.displays.guilds, new());

                case journalButtons.openDistillery:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 68 + 36), IconData.displays.malt, new());

                case journalButtons.openDistilleryInventory:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 68 + 68 + 36), IconData.displays.inventory, new());

                case journalButtons.openProductionRecent:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 68 + 68 + 68 + 36), IconData.displays.lore, new());

                case journalButtons.openProductionEstimated:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 68 + 68 + 68 + 68 + 36), IconData.displays.skills, new());


                // refresh

                case journalButtons.refresh:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yB - 36), IconData.displays.knock, new());

                case journalButtons.clearBuffs:

                    return new InterfaceComponent(Button, new Vector2(xP - 36, yB - 68 - 36), IconData.displays.active, new());


                // ====================================== top bar

                default:
                case journalButtons.openQuests:

                    return new InterfaceComponent(Button, new Vector2(xP + 36, yT), IconData.displays.quest, new());

                case journalButtons.openMasteries:

                    return new InterfaceComponent(Button, new Vector2(xP + 68 + 36, yT), IconData.displays.skills, new());

                case journalButtons.openRelics:

                    return new InterfaceComponent(Button, new Vector2(xP + 68 + 68 + 36, yT), IconData.displays.relic, new());

                case journalButtons.openAlchemy:

                    return new InterfaceComponent(Button, new Vector2(xP + 68 + 68 + 68 + 36, yT), IconData.displays.alchemy, new());

                case journalButtons.openPotions:

                    return new InterfaceComponent(Button, new Vector2(xP + 68 + 68 + 68 + 68 + 36, yT), IconData.displays.herbalism, new());

                case journalButtons.openCompanions:

                    return new InterfaceComponent(Button, new Vector2(xP + 68 + 68 + 68 + 68 + 68 + 36, yT), IconData.displays.heroes, new());

                case journalButtons.openOrders:

                    return new InterfaceComponent(Button, new Vector2(xP + 68 + 68 + 68 + 68 + 68 + 68 + 36, yT), IconData.displays.orders, new());

                case journalButtons.openDragonomicon:

                    return new InterfaceComponent(Button, new Vector2(xP + 68 + 68 + 68 + 68 + 68 + 68 + 68 + 36, yT), IconData.displays.transform, new());

                // ====================================== variant top buttons displayed within content

                // mastery menu

                case journalButtons.levelUp:

                    return new InterfaceComponent(Button, new Vector2(xP + (width / 2), yB - 56), IconData.displays.levelup, new());

                // dragon menu

                case journalButtons.dragonCopy:

                    return new InterfaceComponent(Button, new Vector2(xR - 400, yB - 72), IconData.displays.replay, new());

                case journalButtons.dragonSave:

                    return new InterfaceComponent(Button, new Vector2(xR - 320, yB - 72), IconData.displays.save, new());

                // recruit menu

                case journalButtons.summonCompanion:

                    return new InterfaceComponent(Button, new Vector2(xR - 160, yB - 72), IconData.displays.complete, new());

                case journalButtons.dismissCompanion:

                    return new InterfaceComponent(Button, new Vector2(xR - 80, yB - 72), IconData.displays.exit, new());

                // pal menu

                case journalButtons.summonPal:

                    return new InterfaceComponent(Button, new Vector2(xR - 160, yB - 72), IconData.displays.complete, new());

                case journalButtons.dismissPal:

                    return new InterfaceComponent(Button, new Vector2(xR - 80, yB - 72), IconData.displays.exit, new());

                case journalButtons.renamePal:

                    return new InterfaceComponent(Button, new Vector2(xP + 80, yP + 80), IconData.displays.scroll, new());

                case journalButtons.schemePal:

                    return new InterfaceComponent(Button, new Vector2(xP + 80, yP + 160), IconData.displays.replay, new());

                case journalButtons.rewildPal:

                    return new InterfaceComponent(Button, new Vector2(xP + 80, yP + 240), IconData.displays.tree, new());


                // ======================================  right side

                case journalButtons.exit:

                    return new InterfaceComponent(Button, new Vector2(xR + 36, yP + 36), IconData.displays.exit, new() { scale = 4f, });

                case journalButtons.scrollUp:

                    return new InterfaceComponent(Button, new Vector2(xR + 36, yP + 68 + 36), IconData.displays.up, new() { flip = true, });

                case journalButtons.scrollBar:

                    int scrollUpper = yP + 68 + 68 + 4;

                    int scrollLower = yB - 68 - 68 - 68 - 68 - 4;

                    scrollBox = new(xR + 36, scrollUpper, 64, scrollLower - scrollUpper);

                    return new InterfaceComponent(Button, new Vector2(xR + 36, yP + 68 + 68 + 36), IconData.displays.scroll, new() { flip = true, });

                case journalButtons.scrollDown:

                    return new InterfaceComponent(Button, new Vector2(xR + 36, yB - 68 - 68 - 68 - 36), IconData.displays.down, new() { flip = true, });

                case journalButtons.forward:

                    return new InterfaceComponent(Button, new Vector2(xR + 36, yB - 36), IconData.displays.forward, new());

                case journalButtons.end:

                    return new InterfaceComponent(Button, new Vector2(xR + 36, yB - 68 - 36), IconData.displays.end, new());

                // ---------------------------------------  other right side

                case journalButtons.active:

                    return new InterfaceComponent(Button, new Vector2(xR + 36, yP + 68 + 68 + 36), IconData.displays.active, new());

                case journalButtons.reverse:

                    return new InterfaceComponent(Button, new Vector2(xR + 36, yP + 68 + 68 + 68 + 36), IconData.displays.reverse, new());

                case journalButtons.HP:

                    return new InterfaceComponent(Button, new Vector2(xR + 36, yP + 68 + 36 + 36), IconData.displays.none, new());

                case journalButtons.STM:

                    return new InterfaceComponent(Button, new Vector2(xR + 36, yP + 68 + 68 + 36 + 36), IconData.displays.none, new());

            }

        }

        public virtual void reviseInterface()
        {

            foreach (KeyValuePair<journalButtons,int> button in interfaceRegistry)
            {

                switch (button.Key)
                {

                    case journalButtons.openQuests:

                        if (Mod.instance.magic)
                        {

                            interfaceComponents[button.Value].active = false;

                        }

                        break;

                    case journalButtons.openMasteries:

                        if (JournalUnlocked(journalTypes.masteries) == journalTypes.none)
                        {

                            interfaceComponents[button.Value].active = false;

                        }

                        break;

                    case journalButtons.openRelics:

                        if (Mod.instance.magic)
                        {

                            interfaceComponents[button.Value].active = false;

                        }

                        if (JournalUnlocked(journalTypes.relics) == journalTypes.none)
                        {

                            interfaceComponents[button.Value].active = false;

                        }

                        break;

                    case journalButtons.openAlchemy:

                        if(JournalUnlocked(journalTypes.alchemy) == journalTypes.none)
                        {

                            interfaceComponents[button.Value].active = false;

                        }

                        break;

                    case journalButtons.openPotions:

                        if (JournalUnlocked(journalTypes.potions) == journalTypes.none)
                        {

                            interfaceComponents[button.Value].active = false;

                        }

                        break;

                    case journalButtons.openCompanions:

                        if (JournalUnlocked(journalTypes.companion) == journalTypes.none)
                        {

                            interfaceComponents[button.Value].active = false;

                        }

                        break;

                    case journalButtons.openOrders:

                        if (JournalUnlocked(journalTypes.orders) == journalTypes.none)
                        {

                            interfaceComponents[button.Value].active = false;

                        }

                        break;

                    case journalButtons.openDragonomicon:


                        if (JournalUnlocked(journalTypes.dragon) == journalTypes.none)
                        {

                            interfaceComponents[button.Value].active = false;

                        }

                        break;

                    case journalButtons.active:

                        if (!Mod.instance.Config.activeJournal)
                        {

                            interfaceComponents[button.Value].fade = 0.8f;

                        }

                        break;

                    case journalButtons.reverse:

                        if (!Mod.instance.Config.reverseJournal)
                        {

                            interfaceComponents[button.Value].fade = 0.8f;

                        }

                        break;

                    case journalButtons.back:

                        if(pagination == 0)
                        {

                            break;

                        }

                        int firstOnThisPage = record - (record % pagination);

                        int thispage = firstOnThisPage == 0 ? 0 : firstOnThisPage / pagination;

                        int last = contentComponents.Count - 1;

                        int firstOnLastPage = last - (last % pagination);

                        int lastpage = firstOnLastPage == 0 ? 0 : firstOnLastPage / pagination;

                        if (thispage == 0)
                        {
                            // back
                            interfaceComponents[button.Value].active = false;

                            if (interfaceRegistry.ContainsKey(journalButtons.start))
                            {
                                // start
                                interfaceComponents[interfaceRegistry[journalButtons.start]].active = false;

                            }

                        }

                        if (lastpage == thispage)
                        {

                            // forward
                            if (interfaceRegistry.ContainsKey(journalButtons.forward))
                            {
                                // start
                                interfaceComponents[interfaceRegistry[journalButtons.forward]].active = false;

                            }

                            // end
                            if (interfaceRegistry.ContainsKey(journalButtons.end))
                            {
                                // start
                                interfaceComponents[interfaceRegistry[journalButtons.end]].active = false;

                            }

                        }

                        break;


                    case journalButtons.scrollBar:

                        scrolled = 0;

                        if (contentBox.Height < scrollHeight)
                        {

                            interfaceComponents[button.Value].active = false;

                            interfaceComponents[interfaceRegistry[journalButtons.scrollUp]].active = false;

                            interfaceComponents[interfaceRegistry[journalButtons.scrollDown]].active = false;

                        }
                        else
                        {

                            scrollId = button.Value;

                        }

                        break;


                }

            }

        }

        public virtual void pressButton(journalButtons button)
        {

            switch (button)
            {

                default:
                case journalButtons.openQuests:

                    openJournal(journalTypes.quests);

                    break;

                case journalButtons.openMasteries:

                    openJournal(journalTypes.masteries);

                    break;

                case journalButtons.openAlchemy:

                    openJournal(journalTypes.alchemy);

                    break;

                case journalButtons.openRelics:

                    openJournal(journalTypes.relics);

                    break;

                case journalButtons.openPotions:

                    openJournal(journalTypes.potions);

                    break;

                case journalButtons.openLore:

                    openJournal(journalTypes.lore);

                    break;

                case journalButtons.openEffects:

                    openJournal(journalTypes.effects);

                    break;

                case journalButtons.openDragonomicon:

                    openJournal(journalTypes.dragon);

                    break;

                case journalButtons.openCompanions:

                    openJournal(journalTypes.ledger);

                    break;

                case journalButtons.openPowders:

                    openJournal(journalTypes.powders);

                    break;

                case journalButtons.openOmens:

                    openJournal(journalTypes.omens);

                    break;

                case journalButtons.openTrophies:

                    openJournal(journalTypes.trophies);

                    break;

                case journalButtons.openOrders:

                    openJournal(journalTypes.orders);

                    break;

                case journalButtons.openGuilds:

                    openJournal(journalTypes.guilds);

                    break;

                case journalButtons.openGoods:

                    openJournal(journalTypes.goods);

                    break;

                case journalButtons.openGoodsDistillery:

                    openJournal(journalTypes.goodsDistillery);

                    break;

                case journalButtons.active:

                    Mod.instance.Config.activeJournal = !Mod.instance.Config.activeJournal;

                    populateContent();

                    activateInterface();

                    break;

                case journalButtons.reverse:

                    Mod.instance.Config.reverseJournal = !Mod.instance.Config.reverseJournal;

                    populateContent();

                    activateInterface();

                    break;

                case journalButtons.exit:

                    exitThisMenu();

                    Game1.playSound("bigDeSelect");

                    break;

                case journalButtons.back:

                    if (pagination != 0)
                    {

                        int back = record - (record % pagination);

                        record = Math.Max(0, back - pagination);

                        activateInterface();

                    }
                    
                    break;

                case journalButtons.start:

                    record = 0;

                    activateInterface();
                    
                    break;

                case journalButtons.previous:

                    openJournal(parent);

                    break;

                case journalButtons.scrollUp:

                    scrollAmount(-1);

                    scrollAmount(-1);

                    break;

                case journalButtons.scrollBar:

                    break;

                case journalButtons.scrollDown:

                    scrollAmount(1);

                    scrollAmount(1);

                    break;

                case journalButtons.forward:

                    if (pagination != 0)
                    {

                        int forward = record - (record % pagination);

                        record = Math.Min(contentComponents.Count-1, forward + pagination);

                        activateInterface();
                    
                    }

                    break;

                case journalButtons.end:

                    record = (contentComponents.Count-1) - ((contentComponents.Count-1) % pagination);

                    activateInterface();

                    break;

                case journalButtons.getHint:

                    openJournal(journalTypes.questionPage);

                    break;

                case journalButtons.openDistillery:

                    DruidJournal.openJournal(journalTypes.distillery);

                    return;

                case journalButtons.openDistilleryInventory:

                    DruidJournal.openJournal(journalTypes.distilleryInventory);

                    return;

                case journalButtons.openProductionEstimated:

                    DruidJournal.openJournal(journalTypes.distilleryEstimated);

                    return;

                case journalButtons.openProductionRecent:

                    if (Mod.instance.exportHandle.recentProduction.Count == 0)
                    {

                        Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                        return;

                    }

                    DruidJournal.openJournal(journalTypes.distilleryRecent);

                    return;

            }

        }

        public virtual void heldButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.scrollUp:

                    scrollAmount(-1);

                    break;

                case journalButtons.scrollBar:

                    break;

                case journalButtons.scrollDown:

                    scrollAmount(1);

                    break;

            }

        }

        public virtual int ShiftPressed()
        {

            int amount = 1;

            if (Mod.instance.Helper.Input.GetState(SButton.LeftShift) == SButtonState.Held)
            {
                amount *= 5;
            }

            if (Mod.instance.Helper.Input.GetState(SButton.RightShift) == SButtonState.Held)
            {
                amount *= 5;
            }
            
            if (Mod.instance.Helper.Input.GetState(SButton.LeftControl) == SButtonState.Held)
            {
                amount *= 10;
            }
            
            if (Mod.instance.Helper.Input.GetState(SButton.RightControl) == SButtonState.Held)
            {
                amount *= 10;
            }

            return amount;

        }

        public virtual void pressContent()
        {

        }

        public virtual void pressCancel()
        {

        }

        public virtual void pressEnter(int X, int Y)
        {

            if (scrolling)
            {

                scrollWithin(Y);

                return;

            }

            if (interfacing)
            {

                if (interfaceComponents[focus].active)
                {

                    pressButton(interfaceComponents[focus].button);

                }

            }

            if (browsing)
            {

                pressContent();

            }

        }

        public void pressBackspace(int X, int Y)
        {

            if (browsing)
            {

                pressCancel();

            }
            else
            {

                pressButton(journalButtons.back);

            }

        }

        // ===================================================== 

        public virtual bool scrollAmount(int direction)
        {

            scrolled += (int)(direction * 32);

            int limit = contentBox.Height - scrollHeight;

            if (scrolled > limit)
            {

                scrolled = limit;

                interfaceComponents[scrollId].setBounds();

                interfaceComponents[scrollId].position.Y = scrollBox.Bottom - 32;

                Game1.playSound("shiny4");

                return false;

            }
            else if (scrolled < 0)
            {

                scrolled = 0;

                interfaceComponents[scrollId].setBounds();

                interfaceComponents[scrollId].position.Y = scrollBox.Top + 32;

                Game1.playSound("shiny4");

                return false;

            }

            float ratio = (float)Math.Abs(scrolled) / (float)limit;

            interfaceComponents[scrollId].position.Y = scrollBox.Top + 32 + (int)((float)(scrollBox.Height - 64) * ratio);

            interfaceComponents[scrollId].setBounds();

            return true;

        }

        public virtual void scrollWithin(int Y)
        {

            interfaceComponents[scrollId].position.Y = Math.Max(scrollBox.Top + 32, Math.Min(scrollBox.Bottom - 32, Y));

            float diff = interfaceComponents[scrollId].position.Y - (float)(scrollBox.Top + 32);

            float ratio = diff / (float)(scrollBox.Height - 64);

            int limit = contentBox.Height - scrollHeight;

            scrolled = (int)((float)ratio * (float)limit);

            interfaceComponents[scrollId].setBounds();

            Game1.playSound("shiny4");

        }

        public void shiftFocus(int direction)
        {

            if (scrolling)
            {

                movementScrolling(direction);

                return;

            }

            if (interfacing)
            {

                movementInterface(direction);

                return;
            }

            if(pagination == 0)
            {

                if (scrollId != 0)
                {

                    pageScrolling(direction);

                }

                return;

            }

            shiftContents(direction);

        }

        public virtual void movementScrolling(int direction)
        {

            switch (direction)
            {

                case 0:

                    if (scrollAmount(-1))
                    {

                        Mouse.SetPosition((int)interfaceComponents[scrollId].position.X, (int)interfaceComponents[scrollId].position.Y);

                        return;

                    }

                    break;

                case 1:

                    shiftInterface(scrollId, 1);

                    break;


                case 2:

                    if (scrollAmount(1))
                    {

                        Mouse.SetPosition((int)interfaceComponents[scrollId].position.X, (int)interfaceComponents[scrollId].position.Y);

                        return;

                    }

                    break;


                case 3:

                    focusContents();

                    break;

            }

        }

        public virtual void pageScrolling(int direction)
        {
            
            switch (direction)
            {

                case 0:

                    if (!scrollAmount(-1))
                    {

                        shiftInterface(100, 1);

                        return;

                    }

                    break;

                case 1:

                    shiftInterface(300, 1);

                    break;

                case 2:

                    if (!scrollAmount(1))
                    {

                        shiftInterface(100, 1);

                        return;

                    }

                    break;


                case 3:

                    shiftInterface(200, 1);

                    break;

            }

        }

        public virtual void movementInterface(int direction)
        {

            // Top
            if (focus < 200)
            {
                switch (direction)
                {

                    case 0:

                        focusContents(false);

                        break;

                    case 1:

                        shiftInterface(focus, 1);

                        break;


                    case 2:

                        focusContents();

                        break;


                    case 3:

                        shiftInterface(focus, -1);

                        break;

                }

            }
            // Left side
            else if (focus < 300)
            {
                switch (direction)
                {

                    case 0:

                        shiftInterface(focus, -1);

                        break;

                    case 1:

                        focusContents();

                        break;


                    case 2:

                        shiftInterface(focus, 1);

                        break;


                    case 3:

                        shiftInterface(300, 1);

                        break;

                }

            }
            // Right side
            else if (focus < 400)
            {
                switch (direction)
                {

                    case 0:

                        shiftInterface(focus, -1);

                        break;

                    case 1:

                        shiftInterface(200, 1);

                        break;

                    case 2:

                        shiftInterface(focus, 1);

                        break;

                    case 3:

                        focusContents();

                        break;

                }

            }

        }

        public void shiftInterface(int previous, int increment = 1)
        {

            List<int> interfaceables = new();

            foreach(KeyValuePair<int,InterfaceComponent> component in interfaceComponents)
            {
                if (component.Value.active)
                {

                    interfaceables.Add(component.Key);

                }

            }

            interfaceables.Sort();

            for(int i = 0; i < interfaceables.Count; i++)
            {

                int key = interfaceables[i];

                if(increment < 0)
                {

                    if(key >= previous)
                    {

                        if (i == 0)
                        {

                            focusInterface(interfaceables.Last());
                            
                            return;

                        }
                        else
                        {

                            focusInterface(interfaceables[i - 1]);

                            return;

                        }

                    }
                    else if (i == interfaceables.Count-1)
                    {

                        focusInterface(key);

                        return;

                    }

                }
                else if (increment > 0)
                {

                    if (key == previous)
                    {

                        int next = i + 1;

                        if (next > interfaceables.Count - 1)
                        {

                            focusInterface(interfaceables.First());

                            return;

                        }
                        else
                        {

                            focusInterface(interfaceables[next]);

                            return;

                        }

                    }
                    else if (key > previous)
                    {

                        focusInterface(interfaceables[i]);

                        return;

                    }
                    else if (i == interfaceables.Count - 1)
                    {

                        focusInterface(interfaceables.First());

                        return;

                    }

                }

            }

            focusContents();

        }

        public void focusInterface(int key)
        {

            focus = key;

            scrolling = false;

            browsing = false;

            interfacing = true;

            Mouse.SetPosition(interfaceComponents[focus].bounds.Center.X, interfaceComponents[focus].bounds.Center.Y);

        }

        public List<int> AvailableComponents()
        {

            List<int> available = new();

            int top = 0;

            int total = contentComponents.Count;

            if (pagination > 0)
            {

                top = record - (record % pagination);

                total = pagination;

            }

            for (int i = 0; i < total; i++)
            {

                int index = top + i;

                if (contentComponents.Count <= index)
                {

                    break;

                }

                ContentComponent component = contentComponents[index];

                if (contentComponents[index].active)
                {

                    available.Add(index);

                }

            }

            return available;

        }

        public void shiftContents(int direction)
        {

            List<int> available = AvailableComponents();

            switch (direction)
            {

                case 0:
                case 3:

                    if (direction == 3 && focus % contentColumns == 0)
                    {

                        shiftInterface(200, 1);

                        return;

                    }

                    if (focus == available.First())
                    {

                        shiftInterface(100, 1);

                        return;

                    }

                    for (int i = available.Count - 1; i >= 0; i--)
                    {

                        if (focus == available[i])
                        {

                            browsing = true;

                            focus = available[i - 1];

                            Mouse.SetPosition(contentComponents[focus].bounds.Center.X, contentComponents[focus].bounds.Center.Y);

                            return;

                        }


                    }

                    break;

                case 1:
                case 2:


                    if (direction == 1 && focus % contentColumns == (contentColumns - 1))
                    {

                        shiftInterface(300, 1);

                        return;

                    }

                    if (focus == available.Last())
                    {

                        shiftInterface(100, 1);

                        return;

                    }

                    for (int i = 0; i < available.Count; i++)
                    {

                        if (focus == available[i])
                        {

                            browsing = true;

                            focus = available[i + 1];

                            Mouse.SetPosition(contentComponents[focus].bounds.Center.X, contentComponents[focus].bounds.Center.Y);

                            return;

                        }


                    }

                    break;

            }

        }

        public void focusContents(bool first = true)
        {

            scrolling = false;

            interfacing = false;

            browsing = false;

            if(contentComponents.Count > 0)
            {

                List<int> available = AvailableComponents();

                if (available.Count > 0)
                {

                    browsing = true;

                    if (first)
                    {

                        focus = available.First();

                        Mouse.SetPosition(contentComponents[focus].bounds.Center.X, contentComponents[focus].bounds.Center.Y);

                    }
                    else
                    {

                        focus = available.Last();

                        Mouse.SetPosition(contentComponents[focus].bounds.Center.X, contentComponents[focus].bounds.Center.Y);

                    }

                    return;

                }

            }
            
            if (interfaceComponents.Count > 0)
            {

                interfacing = true;

                focus = interfaceComponents.First().Key;

                Mouse.SetPosition(interfaceComponents[focus].bounds.Center.X, interfaceComponents[focus].bounds.Center.Y);

                return;

            }

        }

        public void hoverInterface(int x, int y)
        {

            foreach (KeyValuePair<int, InterfaceComponent> component in interfaceComponents)
            {

                if (!component.Value.active)
                {

                    continue;

                }

                if (component.Value.bounds.Contains(x, y))
                {

                    focus = component.Key;

                    interfacing = true;

                    if (component.Value.hover < component.Value.spec.hoverLimit)
                    {

                        component.Value.hover++;

                    }

                }
                else if (component.Value.hover > 0)
                {

                    component.Value.hover--;

                }

            }

        }

        public void hoverContent(int x, int y)
        {

            int top = 0;

            int total = contentComponents.Count;

            if (pagination > 0)
            {

                top = record - (record % pagination);

                total = pagination;

            }

            for (int i = 0; i < total; i++)
            {

                int index = top + i;

                if (contentComponents.Count <= index)
                {
                    
                    break;
                
                }

                ContentComponent component = contentComponents[index];

                if (!component.active)
                {

                    continue;

                }

                if (component.bounds.Contains(x, y))
                {

                    browsing = true;

                    focus = index;

                    return;

                }

            }

        }

        // ===================================================== 

        public override void draw(SpriteBatch b)
        {

            // Background

            b.Draw(Game1.fadeToBlackRect, Game1.graphics.GraphicsDevice.Viewport.Bounds, Color.Black * 0.75f);

            // Title

            drawTitle(b);

            // Mainbox

            IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 373, 18, 18), xPositionOnScreen, yPositionOnScreen, width, height, Color.White, 4f, true, -1f);

            // interface

            drawInterface(b);

            // content

            if (scrollId != 0)
            {

                drawScroll(b);

            }
            else
            {

                drawContent(b);

            }

            // mouse

            Game1.mouseCursorTransparency = 1f;

            drawMouse(b, false, -1);

            // hoverdetail

            drawHover(b);

        }

        public virtual void drawTitle(SpriteBatch b)
        {

            b.DrawString(
                Game1.dialogueFont,
                title,
                titlePosition + new Vector2(-1, 1.5f),
                Microsoft.Xna.Framework.Color.Brown * 0.35f,
                0f, Vector2.Zero, 1f, SpriteEffects.None, 0.900f);

            b.DrawString(
                Game1.dialogueFont,
                title,
                titlePosition,
                Microsoft.Xna.Framework.Color.Wheat*0.9f,
                0f, Vector2.Zero, 1f, SpriteEffects.None, 0.901f);

        }

        public virtual void drawInterface(SpriteBatch b)
        {

            foreach(KeyValuePair<int,InterfaceComponent> component in interfaceComponents)
            {

                if (!component.Value.active)
                {

                    continue;

                }

                component.Value.draw(b);

            }

        }

        public virtual void drawContent(SpriteBatch b)
        {

            int top = 0;

            int total = contentComponents.Count;

            if (pagination > 0)
            {

                top = record - (record % pagination);

                total = pagination;

            }

            for (int i = 0; i < total; i++)
            {

                int index = top + i;

                if(contentComponents.Count <= index)
                {
                    
                    break;
                
                }

                if (!contentComponents[index].active)
                {

                    continue;

                }

                contentComponents[index].draw(b, Vector2.Zero, (browsing && index == focus));

            }

        }

        public virtual void drawScroll(SpriteBatch b)
        {

            // preserve current batch rectangle

            Rectangle preserve = b.GraphicsDevice.ScissorRectangle;

            b.End();

            // create new batch for scroll rectangle

            SpriteBatch b2 = b;

            b2.Begin(0, BlendState.AlphaBlend, SamplerState.PointClamp, null, new RasterizerState() { ScissorTestEnable = true }, null, new Matrix?());

            // determine inframe

            Rectangle inframe = new(xPositionOnScreen + 32, yPositionOnScreen + 48, width - 64, scrollHeight + 32);

            Rectangle screen = Utility.ConstrainScissorRectToScreen(inframe);

            Game1.graphics.GraphicsDevice.ScissorRectangle = screen;

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                component.Value.draw(b, new Vector2(0, -scrolled));

            }

            // leave inframe

            b.End();

            b.GraphicsDevice.ScissorRectangle = preserve;

            b.Begin(0, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null, new Matrix?());

        }

        public virtual void drawHover(SpriteBatch b)
        {

            if (interfacing)
            {

                if (interfaceComponents[focus].text != null)
                {

                    drawHoverText(b, interfaceComponents[focus].text);

                }

            }

        }

        public virtual void drawHoverText(SpriteBatch b, string t)
        {

            Vector2 textOffset = Game1.smallFont.MeasureString(t) * 1.25f;

            Microsoft.Xna.Framework.Rectangle hoverbox = drawHoverBox(b, (int)textOffset.X + 64, (int)textOffset.Y + 32, 3f);

            Vector2 hoverPosition = new Vector2(hoverbox.Center.X - (textOffset.X / 2),hoverbox.Center.Y - (textOffset.Y / 2) + 2f);

            b.DrawString(
                Game1.smallFont,
                t,
                hoverPosition + new Vector2(-1,1.5f),
                Microsoft.Xna.Framework.Color.Brown * 0.35f,
                0f, Vector2.Zero, 1.25f, SpriteEffects.None, 0.900f);

            b.DrawString(
                Game1.smallFont,
                t,
                hoverPosition,
                Game1.textColor,
                0f, Vector2.Zero, 1.25f, SpriteEffects.None, 0.901f);

        }

        public Microsoft.Xna.Framework.Rectangle drawHoverBox(SpriteBatch b, int bWidth = 512, int bHeight = 64, float border = 4f)
        {

            // -------------------------------------------------------
            // texturebox

            int cornerX = Game1.getMouseX() + 32;

            int cornerY = Game1.getMouseY() + 32;

            if (cornerX > Game1.graphics.GraphicsDevice.Viewport.Width - bWidth)
            {

                int tryCorner = cornerX - bWidth - 32;

                cornerX = tryCorner < 0 ? 0 : tryCorner;

            }

            if (cornerY > Game1.graphics.GraphicsDevice.Viewport.Height - bHeight - 48)
            {

                int tryCorner = cornerY - bHeight - 32;

                cornerY = tryCorner < 0 ? 0 : tryCorner;

            }

            Vector2 corner = new(cornerX, cornerY);

            Microsoft.Xna.Framework.Rectangle bounds = new((int)corner.X, (int)corner.Y, bWidth, bHeight);

            IClickableMenu.drawTextureBox(
                b,
                Game1.mouseCursors,
                new Rectangle(384, 396, 15, 15),
                bounds.X,
                bounds.Y,
                bounds.Width,
                bounds.Height,
                Color.White,
                border,
                true,
                -1f
            );

            return bounds;

        }

        public virtual void DrawSeparator(SpriteBatch b, int x, int y, float width = 488f)
        {

            b.Draw(Game1.staminaRect, new Rectangle(x, y, 488, 2), new Microsoft.Xna.Framework.Color(167, 81, 37));

            b.Draw(Game1.staminaRect, new Rectangle(x, y + 2, 488, 3), new Microsoft.Xna.Framework.Color(246, 146, 30));

        }

        // ===================================================== 

        public override bool overrideSnappyMenuCursorMovementBan()
        {

            return true;

        }

        public override void receiveGamePadButton(Buttons b)
        {

            switch(b)
            {

                case Buttons.RightTrigger:

                    pressButton(journalButtons.back);

                    break;

                case Buttons.LeftTrigger:

                    pressButton(journalButtons.forward);

                    break;

            }

        }

        public override void receiveKeyPress(Keys key)
        {

            if (key == 0)
            {

                return;

            }

            if(Game1.options.doesInputListContain(Game1.options.menuButton, key))
            {

                pressButton(journalButtons.exit);

                return;

            }

            if (Game1.options.doesInputListContain(Game1.options.moveUpButton, key))
            {

                shiftFocus(0);

                return;

            }

            if (Game1.options.doesInputListContain(Game1.options.moveRightButton, key))
            {

                shiftFocus(1);

                return;

            }

            if (Game1.options.doesInputListContain(Game1.options.moveDownButton, key))
            {

                shiftFocus(2);

                return;

            }

            if (Game1.options.doesInputListContain(Game1.options.moveLeftButton, key))
            {

                shiftFocus(3);

                return;

            }

            /*if (Game1.options.snappyMenus && Game1.options.gamepadControls)
            {

                if (Game1.options.doesInputListContain(Game1.options., key))
                {

                    return;

                }

                applyMovementKey(key);

                return;

            }*/

            switch (key)
            {

                case Keys.Escape:

                    pressButton(journalButtons.exit);

                    break;

                case Keys.Back:

                    pressBackspace(Mouse.GetState().X, Mouse.GetState().Y);

                    break;

                case Keys.Enter:

                    pressEnter(Mouse.GetState().X, Mouse.GetState().Y);

                    break;

                case Keys.Up:

                    shiftFocus(0);

                    break;

                case Keys.Right:

                    shiftFocus(1);
                    break;

                case Keys.Down:

                    shiftFocus(2);

                    break;

                case Keys.Left:

                    shiftFocus(3);

                    break;

            }

        }

        public override void receiveScrollWheelAction(int direction)
        {
            
            if (scrollId != 0)
            {
                
                if(direction > 0)
                {

                    scrollAmount(-1);

                }
                else
                {

                    scrollAmount(1);
                
                }
                
            }

        }

        public override void performHoverAction(int x, int y)
        {

            interfacing = false;

            browsing = false;

            scrolling = false;

            focus = 0;

            if (scrollId != 0 && scrollBox.Contains(x, y))
            {

                scrolling = true;

                focus = scrollId;

                return;

            }

            hoverInterface(x, y);

            if (interfacing)
            {

                return;

            }

            hoverContent(x,y);

        }

        public override void applyMovementKey(int direction)
        {

            shiftFocus(direction);

        }

        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {

            pressEnter(x,y);

        }

        public override void leftClickHeld(int x, int y)
        {

            if (scrolling)
            {

                scrollWithin(y);

                return;

            }

            if (interfacing)
            {

                heldButton(interfaceComponents[focus].button);

                return;

            }

        }

        public override void releaseLeftClick(int x, int y)
        {

        }

        public override void receiveRightClick(int x, int y, bool playSound = true)
        {

            pressBackspace(x,y);

        }

    }

}
