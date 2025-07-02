using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Data;
using StardewDruid.Event.Scene;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Menus;
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

            quests,
            effects,
            relics,
            herbalism,
            lore,

            questPage,
            effectPage,
            relicPage,
            lorePage,
            dragonPage,

            questionPage,

            bombs,
            omens,
            goods,

            orders,

            distillery,
            distilleryEstimated,
            distilleryRecent,

            recruits,
            recruitPage,

            palPage,

            guildPage,

            battle,
            
        }

        public journalTypes type = journalTypes.quests;

        public journalTypes parentJournal = journalTypes.quests;

        public string title = StringData.Strings(StringData.stringkeys.stardewDruid);

        public enum journalButtons
        {

            quests,
            effects,
            relics,
            herbalism,
            lore,
            transform,
            bombs,
            recruits,
            omens,
            goods,

            active,
            reverse,
            refresh,

            viewQuest,
            viewEffect,
            question,
            skipQuest,
            replayTomorrow,
            replayQuest,
            cancelReplay,
            clearBuffs,

            clearOne,
            clearTwo,
            clearThree,
            clearFour,

            exit,

            back,
            start,
            backto,

            scrollUp,
            scrollBar,
            scrollDown,
            forward,
            end,

            dragonReset,
            dragonSave,

            summonRecruit,
            dismissRecruit,
            clearRecruit,

            summonPal,
            dismissPal,
            schemePal,
            renamePal,
            removePal,

            distilleryEstimated,
            distilleryRecent,

        }

        public string journalId;

        public int focus;

        public bool scrolling;

        public bool interfacing;

        public bool browsing;

        public int record;

        public int pagination;

        public bool detail;

        public int scrolled;

        public Microsoft.Xna.Framework.Rectangle scrollBox;

        public int scrollId;

        public Microsoft.Xna.Framework.Rectangle contentBox = new();

        public Dictionary<int, JournalComponent> interfaceComponents = new();

        public Dictionary<int, ContentComponent> contentComponents = new();

        public Dictionary<int, ContentComponent> otherComponents = new();

        public int contentColumns = 1;

        public DruidJournal()
         : base(0, 0, 0, 0, true)
        {

        }

        public DruidJournal(string JournalId = null, int Record = 0)
          : base(0, 0, 0, 0, true)
        {

            journalId = JournalId;

            record = Record;

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
            if (Mod.instance.Config.effectsButtons.GetState() == SButtonState.Pressed)
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

                return journalTypes.herbalism;

            }

            return journalTypes.none;

        }

        public static void openJournal(journalTypes Type, string Id = null, int Record = 0)
        {
            
            if(Game1.activeClickableMenu != null)
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
                case journalTypes.quests:

                    if (Mod.instance.magic)
                    {

                        Game1.activeClickableMenu = new EffectJournal(Id, Record);

                        break;

                    }

                    Game1.activeClickableMenu = new DruidJournal(Id, Record);

                    break;

                case journalTypes.questPage:

                    Game1.activeClickableMenu = new QuestPage(Id, Record);

                    break;

                case journalTypes.effects:

                    Game1.activeClickableMenu = new EffectJournal(Id, Record);

                    break;

                case journalTypes.effectPage:

                    Game1.activeClickableMenu = new EffectPage(Id, Record);

                    break;

                case journalTypes.relics:

                    if (Mod.instance.magic)
                    {

                        Game1.activeClickableMenu = new DragonPage(Id, Record);

                        break;

                    }

                    Game1.activeClickableMenu = new RelicJournal(Id, Record);

                    break;

                case journalTypes.relicPage:

                    Game1.activeClickableMenu = new RelicPage(Id, Record);

                    break;

                case journalTypes.lore:

                    if (Mod.instance.magic)
                    {

                        Game1.activeClickableMenu = new EffectJournal(Id, Record);

                        break;

                    }

                    Game1.activeClickableMenu = new LoreJournal(Id, Record);

                    break;

                case journalTypes.lorePage:

                    Game1.activeClickableMenu = new LorePage(Id, Record);

                    break;

                case journalTypes.herbalism:

                    Game1.activeClickableMenu = new HerbalJournal(Id, Record);

                    break;

                case journalTypes.dragonPage:

                    Game1.activeClickableMenu = new DragonPage(Id, Record);

                    break;

                case journalTypes.questionPage:

                    Game1.activeClickableMenu = new QuestionPage(Id, Record);

                    break;

                case journalTypes.orders:

                    Game1.activeClickableMenu = new OrdersJournal(Id, Record);

                    break;

                case journalTypes.omens:

                    Game1.activeClickableMenu = new OmenJournal(Id, Record);

                    break;

                case journalTypes.goods:

                    Game1.activeClickableMenu = new GoodsJournal(Id, Record);

                    break;

                case journalTypes.distillery:

                    Game1.activeClickableMenu = new DistilleryJournal(Id, Record);

                    break;

                case journalTypes.distilleryEstimated:

                    Game1.activeClickableMenu = new DistilleryEstimated(Id, Record);

                    break;

                case journalTypes.distilleryRecent:

                    Game1.activeClickableMenu = new DistilleryRecent(Id, Record);

                    break;

                case journalTypes.bombs:

                    Game1.activeClickableMenu = new BombJournal(Id, Record);

                    break;

                case journalTypes.recruits:

                    Game1.activeClickableMenu = new RecruitJournal(Id, Record);

                    break;

                case journalTypes.recruitPage:

                    Game1.activeClickableMenu = new RecruitPage(Id, Record);

                    break;

                case journalTypes.palPage:

                    Game1.activeClickableMenu = new PalPage(Id, Record);

                    break;

                case journalTypes.guildPage:

                    Game1.activeClickableMenu = new GuildPage(Id, Record);

                    break;
            }


        }

        public virtual void populateContent()
        {

            pagination = 6;

            contentComponents = Mod.instance.questHandle.JournalQuests();

            if(record >= contentComponents.Count)
            {

                record = 0;

            }

            foreach(KeyValuePair<int,ContentComponent> component in contentComponents)
            {

                component.Value.setBounds(component.Key % pagination, xPositionOnScreen, yPositionOnScreen, width, height);

            }

        }

        public virtual void populateInterface()
        {

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.quests),
                [102] = addButton(journalButtons.effects),
                [103] = addButton(journalButtons.relics),
                [104] = addButton(journalButtons.herbalism),
                [105] = addButton(journalButtons.lore),
                [106] = addButton(journalButtons.transform),
                [107] = addButton(journalButtons.recruits),

                [201] = addButton(journalButtons.back),
                [202] = addButton(journalButtons.start),
                [203] = addButton(journalButtons.question),
                [204] = addButton(journalButtons.active),
                [205] = addButton(journalButtons.reverse),

                [301] = addButton(journalButtons.exit),

                [305] = addButton(journalButtons.end),
                [306] = addButton(journalButtons.forward),

            };

        }

        public virtual void activateInterface()
        {

            resetInterface();

            fadeMenu();

            if (type != journalTypes.quests)
            {

                interfaceComponents[204].active = false;

            }
            else
            if (!Mod.instance.Config.activeJournal)
            {

                interfaceComponents[204].fade = 0.8f;

            }

            if (!Mod.instance.Config.reverseJournal)
            {

                interfaceComponents[205].fade = 0.8f;

            }

            int firstOnThisPage = record - (record % pagination);

            int thispage = firstOnThisPage == 0 ? 0 : firstOnThisPage / pagination;

            int last = contentComponents.Count - 1;

            int firstOnLastPage = last - (last % pagination);

            int lastpage = firstOnLastPage == 0 ? 0 : firstOnLastPage / pagination;

            if (thispage == 0)
            {

                // back
                interfaceComponents[201].active = false;

                // start
                interfaceComponents[202].active = false;

            }

            if (lastpage == thispage)
            {

                // forward
                interfaceComponents[305].active = false;

                // end
                interfaceComponents[306].active = false;

            }

        }

        public virtual void resetInterface()
        {

            interfacing = false;

            browsing = false;

            foreach (KeyValuePair<int, JournalComponent> component in interfaceComponents)
            {

                component.Value.active = true;

                component.Value.hover = 0;

                component.Value.fade = 1f;

            }

        }

        public virtual void fadeMenu()
        {

            if (type != journalTypes.quests)
            {

                interfaceComponents[101].fade = 0.8f;

            }

            if (type != journalTypes.effects)
            {

                interfaceComponents[102].fade = 0.8f;

            }

            if (type != journalTypes.relics)
            {

                interfaceComponents[103].fade = 0.8f;

            }

            if (type != journalTypes.herbalism)
            {

                interfaceComponents[104].fade = 0.8f;

            }

            if (type != journalTypes.lore)
            {

                interfaceComponents[105].fade = 0.8f;

            }

            if (!RelicData.HasRelic(StardewDruid.Data.IconData.relics.dragon_form))
            {

                interfaceComponents[106].active = false;

            }
            else if (type != journalTypes.dragonPage)
            {

                interfaceComponents[106].fade = 0.8f;

            }

            if (!RelicData.HasRelic(StardewDruid.Data.IconData.relics.heiress_gift))
            {

                interfaceComponents[107].active = false;

            }
            else if (type != journalTypes.recruits)
            {

                interfaceComponents[107].fade = 0.8f;

            }

            /*if (interfaceComponents[107].button == journalButtons.recruits)
            {

                if(!RelicData.HasRelic(StardewDruid.Data.IconData.relics.heiress_gift))
                {

                    interfaceComponents[107].active = false;

                }
                else if (type != journalTypes.recruits)
                {

                    interfaceComponents[107].fade = 0.8f;

                }

            }
            else
            {
                if (!RelicData.HasRelic(StardewDruid.Data.IconData.relics.monsterbadge))
                {

                    interfaceComponents[107].active = false;

                }
                else if (type != journalTypes.pals)
                {

                    interfaceComponents[107].fade = 0.8f;

                }

            }*/

            if (Mod.instance.magic)
            {

                interfaceComponents[101].active = false;

                interfaceComponents[103].active = false;

                interfaceComponents[105].active = false;

                //interfaceComponents[107].active = false;

            }

        }

        public JournalComponent addButton(journalButtons Button)
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

                    return new JournalComponent(Button, new Vector2(xP - 36, yP + 36), IconData.displays.forward, new() { flip = true, });

                case journalButtons.start:

                    return new JournalComponent(Button, new Vector2(xP - 36, yP + 68 + 36), IconData.displays.end, new() { flip = true, });

                case journalButtons.active:

                    return new JournalComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 36), IconData.displays.active, new());

                case journalButtons.reverse:

                    return new JournalComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 68 + 36), IconData.displays.reverse, new());

                case journalButtons.question:

                    return new JournalComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 36), IconData.displays.question, new());

                case journalButtons.refresh:

                    return new JournalComponent(Button, new Vector2(xP - 36, yP + 68  + 36), IconData.displays.knock, new());

                case journalButtons.clearBuffs:

                    return new JournalComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 36), IconData.displays.active, new());

                case journalButtons.bombs:

                    return new JournalComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 36), IconData.displays.powderbox, new());

                case journalButtons.omens:

                    return new JournalComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 68 + 36), IconData.displays.omens, new());

                case journalButtons.goods:

                    return new JournalComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 68 + 68 + 68 + 36), IconData.displays.goods, new());

                // ====================================== replay buttons

                case journalButtons.skipQuest:

                    return new JournalComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 36), IconData.displays.skip, new());

                case journalButtons.replayQuest:

                    return new JournalComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 36), IconData.displays.replay, new());

                case journalButtons.replayTomorrow:

                    return new JournalComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 36), IconData.displays.flag, new());

                case journalButtons.cancelReplay:

                    return new JournalComponent(Button, new Vector2(xP - 36, yP + 68 + 68 + 36), IconData.displays.active, new());

                // ====================================== top bar

                default:
                case journalButtons.quests:

                    return new JournalComponent(Button, new Vector2(xP + 36, yT), IconData.displays.quest, new());

                case journalButtons.effects:

                    return new JournalComponent(Button, new Vector2(xP + 68 + 36, yT), IconData.displays.effect, new());

                case journalButtons.relics:

                    return new JournalComponent(Button, new Vector2(xP + 68 + 68 + 36, yT), IconData.displays.relic, new());

                case journalButtons.herbalism:

                    return new JournalComponent(Button, new Vector2(xP + 68 + 68 + 68 + 36, yT), IconData.displays.herbalism, new());

                case journalButtons.lore:

                    return new JournalComponent(Button, new Vector2(xR - 68 - 68 - 68 - 36, yT), IconData.displays.lore, new());

                case journalButtons.transform:

                    return new JournalComponent(Button, new Vector2(xR - 68 - 68 - 36, yT), IconData.displays.transform, new());

                case journalButtons.recruits:

                    return new JournalComponent(Button, new Vector2(xR - 68 - 36, yT), IconData.displays.heroes, new());

                // ====================================== variant top buttons

                case journalButtons.viewQuest:

                    return new JournalComponent(Button, new Vector2(xR - 36, yT), IconData.displays.quest, new());

                case journalButtons.viewEffect:

                    return new JournalComponent(Button, new Vector2(xR - 68 - 36, yT), IconData.displays.effect, new());

                // dragon menu

                case journalButtons.dragonReset:

                    return new JournalComponent(Button, new Vector2(xR - 400, yB - 72), IconData.displays.replay, new());

                case journalButtons.dragonSave:

                    return new JournalComponent(Button, new Vector2(xR - 320, yB - 72), IconData.displays.save, new());

                // recruit menu

                case journalButtons.summonRecruit:

                    return new JournalComponent(Button, new Vector2(xR - 160, yB - 72), IconData.displays.complete, new());

                case journalButtons.dismissRecruit:

                    return new JournalComponent(Button, new Vector2(xR - 80, yB - 72), IconData.displays.exit, new());

                case journalButtons.clearRecruit:

                    return new JournalComponent(Button, new Vector2(xP + 80, yB - 72), IconData.displays.active, new());

                // pal menu

                case journalButtons.summonPal:

                    return new JournalComponent(Button, new Vector2(xR - 160, yB - 72), IconData.displays.complete, new());

                case journalButtons.dismissPal:

                    return new JournalComponent(Button, new Vector2(xR - 80, yB - 72), IconData.displays.exit, new());

                case journalButtons.renamePal:

                    return new JournalComponent(Button, new Vector2(xP + 80, yP + 80), IconData.displays.scroll, new());

                case journalButtons.schemePal:

                    return new JournalComponent(Button, new Vector2(xP + 80, yP + 160), IconData.displays.replay, new());

                case journalButtons.removePal:

                    return new JournalComponent(Button, new Vector2(xP + 80, yP + 240), IconData.displays.tree, new());

                // ======================================  right side

                case journalButtons.exit:

                    return new JournalComponent(Button, new Vector2(xR + 36, yP + 36), IconData.displays.exit, new() { scale = 4f, });

                case journalButtons.scrollUp:

                    return new JournalComponent(Button, new Vector2(xR + 36, yP + 68 + 68 + 36), IconData.displays.up, new() { flip = true, });

                case journalButtons.scrollBar:

                    int scrollUpper = yP + 68 + 68 + 68 + 4;

                    int scrollLower = yB - 68 - 68 - 68 - 4;

                    scrollBox = new(xR + 36, scrollUpper, 64, scrollLower - scrollUpper);

                    return new JournalComponent(Button, new Vector2(xR + 36, yP + 68 + 68 + 68 + 36), IconData.displays.scroll, new() { flip = true, });

                case journalButtons.scrollDown:

                    return new JournalComponent(Button, new Vector2(xR + 36, yB - 68 - 68 - 36), IconData.displays.down, new() { flip = true, });

                case journalButtons.forward:

                    return new JournalComponent(Button, new Vector2(xR + 36, yB - 36), IconData.displays.forward, new());

                case journalButtons.end:

                    return new JournalComponent(Button, new Vector2(xR + 36, yB - 68 - 36), IconData.displays.end, new());


                // ======================================  production

                case journalButtons.distilleryRecent:

                    return new JournalComponent(Button, new Vector2(xP + 36, yT), IconData.displays.lore, new());

                case journalButtons.distilleryEstimated:

                    return new JournalComponent(Button, new Vector2(xP + 68 + 36, yT), IconData.displays.effect, new());
                
            }

        }

        public virtual void pressButton(journalButtons button)
        {

            switch (button)
            {

                default:
                case journalButtons.quests:

                    DruidJournal.openJournal(journalTypes.quests);

                    break;

                case journalButtons.effects:

                    DruidJournal.openJournal(journalTypes.effects);

                    break;

                case journalButtons.relics:

                    DruidJournal.openJournal(journalTypes.relics);

                    break;

                case journalButtons.herbalism:

                    DruidJournal.openJournal(journalTypes.herbalism);

                    break;

                case journalButtons.lore:

                    DruidJournal.openJournal(journalTypes.lore);

                    break;

                case journalButtons.transform:

                    DruidJournal.openJournal(journalTypes.dragonPage);

                    break;

                case journalButtons.recruits:

                    DruidJournal.openJournal(journalTypes.recruits);

                    break;

                case journalButtons.bombs:

                    DruidJournal.openJournal(journalTypes.bombs);

                    break;

                case journalButtons.omens:

                    DruidJournal.openJournal(journalTypes.omens);

                    break;

                case journalButtons.goods:

                    DruidJournal.openJournal(journalTypes.goods);

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

                case journalButtons.question:

                    DruidJournal.openJournal(journalTypes.questionPage);

                    break;



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

        public virtual void pressContent()
        {
            
            if (type == journalTypes.quests)
            {
                
                openJournal(journalTypes.questPage, contentComponents[focus].id, focus);

            }

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

            int limit = contentBox.Height - 512;

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

            int limit = contentBox.Height - 512;

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

            foreach(KeyValuePair<int,JournalComponent> component in interfaceComponents)
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

            foreach (KeyValuePair<int, JournalComponent> component in interfaceComponents)
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

            SpriteText.drawStringWithScrollCenteredAt(b, title, xPositionOnScreen + width / 2, yPositionOnScreen - 64);

            // Mainbox

            IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 373, 18, 18), xPositionOnScreen, yPositionOnScreen, width, height, Color.White, 4f, true, -1f);

            // interface

            drawInterface(b);

            // content

            drawContent(b);

            // mouse

            Game1.mouseCursorTransparency = 1f;

            drawMouse(b, false, -1);

            // hoverdetail

            drawHover(b);

        }

        public virtual void drawInterface(SpriteBatch b)
        {

            foreach(KeyValuePair<int,JournalComponent> component in interfaceComponents)
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

            Microsoft.Xna.Framework.Rectangle hoverbox = drawHoverBox(b, (int)textOffset.X + 64, (int)textOffset.Y + 32);

            b.DrawString(
                Game1.smallFont,
                t,
                new Vector2(hoverbox.Center.X - (textOffset.X / 2),
                hoverbox.Center.Y - (textOffset.Y / 2) + 2f),
                Game1.textColor,
                0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1f);

            b.DrawString(
                Game1.smallFont,
                t,
                new Vector2(hoverbox.Center.X - (textOffset.X / 2) - 1f,
                hoverbox.Center.Y - (textOffset.Y / 2) + 2f + 1.5f),
                Microsoft.Xna.Framework.Color.Brown * 0.35f,
                0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1f);

        }

        public Microsoft.Xna.Framework.Rectangle drawHoverBox(SpriteBatch b, int bWidth = 512, int bHeight = 64)
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
                4f,
                true,
                -1f
            );

            return bounds;

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
