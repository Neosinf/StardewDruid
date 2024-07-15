using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Event.Scene;
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
using static StardewDruid.Journal.HerbalData;
using static System.Net.Mime.MediaTypeNames;

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

            questPage,
            effectPage,
            relicPage,
            dragonPage,

        }

        public journalTypes type = journalTypes.quests;

        public journalTypes parentJournal = journalTypes.quests;

        public string title = DialogueData.Strings(DialogueData.stringkeys.stardewDruid);

        public enum journalButtons
        {

            quests,
            effects,
            relics,
            herbalism,

            active,
            reverse,
            refresh,

            viewQuest,
            viewEffect,
            skipQuest,
            replayTomorrow,
            replayQuest,
            cancelReplay,

            exit,

            back,
            start,
            backto,

            scrollUp,
            scrollBar,
            scrollDown,
            forward,
            end,

            headerOne, 
            headerTwo, 
            headerThree,

            reset,
            save,

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

            populateContent();

            populateInterface();

            activateInterface();

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

            }

            switch (Type)
            {

                default:
                case journalTypes.quests:

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

                    Game1.activeClickableMenu = new RelicJournal(Id, Record);

                    break;

                case journalTypes.relicPage:

                    Game1.activeClickableMenu = new RelicPage(Id, Record);

                    break;

                case journalTypes.herbalism:

                    Game1.activeClickableMenu = new HerbalJournal(Id, Record);

                    break;

                case journalTypes.dragonPage:

                    Game1.activeClickableMenu = new DragonPage(Id, Record);

                    break;

            }


        }

        public static journalTypes journalTrigger()
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

        public virtual void populateInterface()
        {

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.quests),
                [102] = addButton(journalButtons.effects),
                [103] = addButton(journalButtons.relics),
                [104] = addButton(journalButtons.herbalism),

                [105] = addButton(journalButtons.active),
                [106] = addButton(journalButtons.reverse),

                [201] = addButton(journalButtons.back),
                [202] = addButton(journalButtons.start),

                [301] = addButton(journalButtons.exit),

                [305] = addButton(journalButtons.end),
                [306] = addButton(journalButtons.forward),

            };

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

        }

        public virtual void activateInterface()
        {

            resetInterface();

            fadeMenu();

            if (!Mod.instance.Config.activeJournal)
            {

                interfaceComponents[105].fade = 0.8f;

            }

            if (!Mod.instance.Config.reverseJournal)
            {

                interfaceComponents[106].fade = 0.8f;

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

        public JournalComponent addButton(journalButtons Button)
        {

            int xP = xPositionOnScreen;

            int yP = yPositionOnScreen;

            int xR = xP + width;

            int yT = yP - (4 + 32);

            int yB = yP + height;

            switch (Button)
            {

                // ======================================  left side

                case journalButtons.back:

                    return new JournalComponent(Button, new Vector2(xP - (4 + 32), yP +4 + 32), IconData.displays.forward, new() { flip = true, });

                case journalButtons.start:

                    return new JournalComponent(Button, new Vector2(xP - (4 + 32), yP + 4 + 64 + 4 + 32), IconData.displays.end, new() { flip = true, });

                case journalButtons.headerOne:

                    return new JournalComponent(Button, new Vector2(xP + 48, yP + 17 + 28), IconData.displays.flag, new() { scale = 3f });

                case journalButtons.headerTwo:

                    return new JournalComponent(Button, new Vector2(xP + 48, yP + 17 + 202 + 28), IconData.displays.flag, new() { scale = 3f });

                case journalButtons.headerThree:

                    return new JournalComponent(Button, new Vector2(xP + 48, yP + 17 + 404 + 28), IconData.displays.flag, new() { scale = 3f });


                // ====================================== top bar

                default:
                case journalButtons.quests:

                    return new JournalComponent(Button, new Vector2(xP + 4 + 32, yT), IconData.displays.quest, new());

                case journalButtons.effects:

                    return new JournalComponent(Button, new Vector2(xP + 4 + 64 + 4 + 32, yT), IconData.displays.effect, new());

                case journalButtons.relics:

                    return new JournalComponent(Button, new Vector2(xP + 4 + 64 + 4 + 64 + 4 + 32, yT), IconData.displays.relic, new());

                case journalButtons.herbalism:

                    return new JournalComponent(Button, new Vector2(xP + 4 + 64 + 4 + 64 + 4 + 64 + 4 + 32, yT), IconData.displays.herbalism, new());

                case journalButtons.active:

                    return new JournalComponent(Button, new Vector2(xR - (4 + 64 + 4 + 32), yT), IconData.displays.active, new());

                case journalButtons.reverse:

                    return new JournalComponent(Button, new Vector2(xR - (4 + 32), yT), IconData.displays.reverse, new());

                case journalButtons.refresh:

                    return new JournalComponent(Button, new Vector2(xR - (4 + 32), yT), IconData.displays.knock, new());

                case journalButtons.viewQuest:

                    return new JournalComponent(Button, new Vector2(xR - (4 + 32), yT), IconData.displays.quest, new());

                case journalButtons.viewEffect:

                    return new JournalComponent(Button, new Vector2(xR - (4 + 64 + 4 + 32), yT), IconData.displays.effect, new());

                case journalButtons.skipQuest:

                    return new JournalComponent(Button, new Vector2(xR - (4 + 32), yT), IconData.displays.end, new());

                case journalButtons.replayQuest:

                    return new JournalComponent(Button, new Vector2(xR - (4 + 32), yT), IconData.displays.replay, new());

                case journalButtons.replayTomorrow:

                    return new JournalComponent(Button, new Vector2(xR - (4 + 32), yT), IconData.displays.flag, new());

                case journalButtons.cancelReplay:

                    return new JournalComponent(Button, new Vector2(xR - (4 + 32), yT), IconData.displays.active, new());

                // ======================================  right side

                case journalButtons.exit:

                    return new JournalComponent(Button, new Vector2(xR + 4 + 32, yP + 4 + 32), IconData.displays.exit, new() { scale = 4f, });

                case journalButtons.scrollUp:

                    return new JournalComponent(Button, new Vector2(xR + 4 + 32, yP + (4 + 64 + 4 + 64 + 4 + 32)), IconData.displays.up, new() { flip = true, });

                case journalButtons.scrollBar:

                    int scrollUpper = yP + (4 + 64 + 4 + 64 + 4 + 64 + 4);

                    int scrollLower = yB - (4 + 64 + 4 + 64 + 4 + 64 + 4);

                    scrollBox = new(xR + 4 + 32, scrollUpper, 64, scrollLower - scrollUpper);

                    return new JournalComponent(Button, new Vector2(xR + 4 + 32, yP + (4 + 64 + 4 + 64 + 4 + 64 + 4 + 32)), IconData.displays.scroll, new() { flip = true, });

                case journalButtons.scrollDown:

                    return new JournalComponent(Button, new Vector2(xR + 4 + 32, yB - (4 + 64 + 4 + 64 + 4 + 32)), IconData.displays.down, new() { flip = true, });

                case journalButtons.forward:

                    return new JournalComponent(Button, new Vector2(xR + 4 + 32, yB - (4 + 32)), IconData.displays.forward, new());

                case journalButtons.end:

                    return new JournalComponent(Button, new Vector2(xR + 4 + 32, yB - (4 + 64 + 4 + 32)), IconData.displays.end, new());

                case journalButtons.reset:

                    return new JournalComponent(Button, new Vector2(xR - 160, yB - 240), IconData.displays.knock, new());

                case journalButtons.save:

                    return new JournalComponent(Button, new Vector2(xR - 160, yB - 160), IconData.displays.complete, new());

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

                case journalButtons.skipQuest:

                    Game1.playSound("ghost");

                    Mod.instance.questHandle.CompleteQuest(journalId);

                    Mod.instance.questHandle.OnCancel(journalId);

                    break;

                case journalButtons.replayQuest:

                    if (Mod.instance.questHandle.IsReplayable(journalId))
                    {

                        Game1.playSound("yoba");

                        Mod.instance.questHandle.RevisitQuest(journalId);

                    }

                    break;

                case journalButtons.replayTomorrow:

                    break;

                case journalButtons.cancelReplay:

                    Game1.playSound("ghost");

                    Mod.instance.questHandle.OnCancel(journalId);

                    Mod.instance.SyncMultiplayer();

                    break;

                case journalButtons.exit:

                    exitThisMenu();

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

                    record = contentComponents.Count - (contentComponents.Count % pagination);

                    activateInterface();

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

            openJournal(journalTypes.questPage, contentComponents[focus].id, focus );
            
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

                return false;

            }
            else if (scrolled < 0)
            {

                scrolled = 0;

                interfaceComponents[scrollId].setBounds();

                interfaceComponents[scrollId].position.Y = scrollBox.Top + 32;

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

            if (focus < 200)
            {
                switch (direction)
                {

                    case 0:
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

        public void shiftContents(int direction)
        {

            int start = focus - (focus % pagination);

            int end = Math.Min(contentComponents.Count-1, start + pagination - 1);

            bool active = false;

            switch (direction)
            {

                case 0:

                    while (!active)
                    {
                        
                        if (focus == start)
                        {

                            shiftInterface(100, 1);

                            return;

                        }

                        focus--;

                        if (contentComponents[focus].active)
                        {

                            active = true;

                        }

                    }

                    break;

                case 1:

                    shiftInterface(200, 1);

                    return;

                case 2:
                    while (!active)
                    {

                        if (focus == end)
                        {

                            shiftInterface(100, 1);

                            return;

                        }

                        focus++;

                        if (contentComponents[focus].active)
                        {

                            active = true;

                        }

                    }

                    break;

                case 3:

                    shiftInterface(300, 1);

                    return;

            }

            browsing = true;

            Mouse.SetPosition(contentComponents[focus].bounds.Center.X, contentComponents[focus].bounds.Center.Y);

        }

        public void focusContents()
        {

            scrolling = false;

            interfacing = false;

            browsing = false;

            if(contentComponents.Count > 0)
            {
                
                browsing = true;

                focus = contentComponents.First().Key;

                Mouse.SetPosition(contentComponents[focus].bounds.Center.X, contentComponents[focus].bounds.Center.Y);

            }
            else
            {

                interfacing = true;

                focus = interfaceComponents.First().Key;

                Mouse.SetPosition(interfaceComponents[focus].bounds.Center.X, interfaceComponents[focus].bounds.Center.Y);

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

            //int upper = contentComponents.Count % pagination;

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

                    Vector2 textOffset = Game1.smallFont.MeasureString(interfaceComponents[focus].text) * 1.25f;

                    Microsoft.Xna.Framework.Rectangle hoverbox = drawHoverBox(b,(int)textOffset.X+64, (int)textOffset.Y+32);

                    b.DrawString(
                        Game1.smallFont, 
                        interfaceComponents[focus].text, 
                        new Vector2(hoverbox.Center.X-(textOffset.X/2), 
                        hoverbox.Center.Y - (textOffset.Y / 2) + 2f), 
                        Game1.textColor, 
                        0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1f);

                    b.DrawString(
                        Game1.smallFont, 
                        interfaceComponents[focus].text, 
                        new Vector2(hoverbox.Center.X - (textOffset.X / 2) - 1f, 
                        hoverbox.Center.Y - (textOffset.Y / 2) + 2f + 1.5f), 
                        Microsoft.Xna.Framework.Color.Brown * 0.35f, 
                        0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1f);


                }

            }

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

            /*IClickableMenu.drawTextureBox(
                b, 
                Game1.menuTexture, 
                new Rectangle(0, 256, 60, 60), 
                bounds.X, 
                bounds.Y, 
                bounds.Width, 
                bounds.Height, 
                Color.White, 
                1f, 
                true, 
                -1f
            );*/

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

            if (Game1.options.doesInputListContain(Game1.options.moveLeftButton, key))
            {

                shiftFocus(2);

                return;

            }

            if (Game1.options.doesInputListContain(Game1.options.moveDownButton, key))
            {

                shiftFocus(3);

                return;

            }

            if (Game1.options.snappyMenus && Game1.options.gamepadControls)
            {

                applyMovementKey(key);

                return;

            }

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
