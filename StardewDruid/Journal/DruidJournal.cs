using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Data;
using StardewDruid.Event.Scene;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection;
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
            questPage,
            effects,
            relics,
            herbalism,

        }

        public journalTypes type = journalTypes.quests;

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

            associateQuest,
            associateEffect,
            replayQuest,

            exit,

            back,
            start,
            backto,

            scrollUp,
            scrollBar,
            scrollDown,
            forward,
            end,

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

        public static void openJournal(journalTypes Type, string Id = null, int Record = 0)
        {

            Game1.activeClickableMenu.exitThisMenu(false);

            switch (Type)
            {

                default:
                case journalTypes.quests:

                    Game1.activeClickableMenu = new DruidJournal(Id, Record);

                    break;

                case journalTypes.questPage:

                    Game1.activeClickableMenu = new QuestPage(Id, Record);

                    break;

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

                [105] = addButton(journalButtons.active),
                [106] = addButton(journalButtons.reverse),

                [201] = addButton(journalButtons.back),
                [202] = addButton(journalButtons.start),

                [301] = addButton(journalButtons.exit),

                [305] = addButton(journalButtons.forward),
                [306] = addButton(journalButtons.end),

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

        public virtual void activateInterface()
        {

            resetInterface();

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

            if (!Mod.instance.Config.activeJournal)
            {

                interfaceComponents[105].fade = 0.8f;

            }

            if (!Mod.instance.Config.reverseJournal)
            {

                interfaceComponents[106].fade = 0.8f;

            }

            int top = record - (record % pagination);

            int page = top == 0 ? 0 : top / pagination;

            int upper = contentComponents.Count % pagination;

            int next = contentComponents.Count - upper;

            int pages = next == 0 ? 0 : next / pagination;

            if (page == 0)
            {
                
                // back
                interfaceComponents[201].active = false;

                // start
                interfaceComponents[202].active = false;

            }

            if (pages == page)
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

                case journalButtons.associateQuest:

                    return new JournalComponent(Button, new Vector2(xR - (4 + 64 + 4 + 32), yT), IconData.displays.quest, new());

                case journalButtons.associateEffect:

                    return new JournalComponent(Button, new Vector2(xR - (4 + 64 + 4 + 32), yT), IconData.displays.effect, new());

                case journalButtons.replayQuest:

                    return new JournalComponent(Button, new Vector2(xR - (4 + 64 + 4 + 32), yT), IconData.displays.replay, new());

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


            }

        }

        public virtual void pressButton(journalButtons button)
        {

            switch (button)
            {

                default:
                case journalButtons.quests:

                    break;

                case journalButtons.effects:

                    break;

                case journalButtons.relics:

                    break;

                case journalButtons.herbalism:

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

                case journalButtons.associateQuest:

                    break;

                case journalButtons.associateEffect:

                    break;

                case journalButtons.replayQuest:

                    break;

                case journalButtons.exit:

                    exitThisMenu();

                    break;

                case journalButtons.back:

                    int back = record - (record % pagination);

                    record = Math.Max(0, back - pagination);

                    activateInterface();
                    
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

                    int forward = record - (record % pagination);

                    record = Math.Min(contentComponents.Count, forward + pagination);

                    activateInterface();

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

        public virtual void scrollAmount(int direction)
        {

            scrolled += (int)(direction * 32);

            int limit = contentBox.Height - 512;

            if (scrolled > limit)
            {

                scrolled = limit;

                interfaceComponents[scrollId].position.Y = scrollBox.Bottom - 32;

            }
            else if (scrolled < 0)
            {

                scrolled = 0;

                interfaceComponents[scrollId].position.Y = scrollBox.Top + 32;

            }
            else
            {

                float ratio = (float)Math.Abs(scrolled) / (float)limit;

                interfaceComponents[scrollId].position.Y = scrollBox.Top + 32 + (int)((float)(scrollBox.Height-64) * ratio);

            }

            interfaceComponents[scrollId].setBounds();

        }

        public virtual void scrollWithin(int Y)
        {

            interfaceComponents[scrollId].position.Y = Math.Max(scrollBox.Top + 32, Math.Min(scrollBox.Bottom-32, Y ));

            float diff = interfaceComponents[scrollId].position.Y - (float)(scrollBox.Top + 32);

            float ratio = diff / (float)(scrollBox.Height - 64);

            int limit = contentBox.Height - 512;

            scrolled = (int)((float)ratio * (float)limit);

            interfaceComponents[scrollId].setBounds();

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

            int top = record - (record % pagination);

            //int upper = contentComponents.Count % pagination;

            for (int i = 0; i < pagination; i++)
            {

                int index = top + i;

                if(contentComponents.Count <= index)
                {
                    break;
                }

                ContentComponent component = contentComponents[index];

                component.draw(b, Vector2.Zero, (browsing && index == focus));

                IconData.displays questIcon;

                switch (Mod.instance.save.progress[component.id].status)
                {

                    default:
                    case 1:

                        questIcon = IconData.displays.active;

                        break;
                    case 2:
                    case 3:

                        questIcon = IconData.displays.complete;

                        break;
                    case 4:

                        questIcon = IconData.displays.replay;

                        break;

                }

                b.Draw(
                    Mod.instance.iconData.displayTexture,
                    new Vector2(component.bounds.Right - 20f - 32f + 1f, component.bounds.Center.Y + 3f),
                    IconData.DisplayRectangle(questIcon),
                    Microsoft.Xna.Framework.Color.Black * 0.35f,
                    0f,
                    new Vector2(8),
                    4f,
                    0,
                    0.900f
                );

                b.Draw(
                    Mod.instance.iconData.displayTexture,
                    new Vector2(component.bounds.Right - 20f - 32f, component.bounds.Center.Y),
                    IconData.DisplayRectangle(questIcon),
                    Microsoft.Xna.Framework.Color.White,
                    0f,
                    new Vector2(8),
                    4f,
                    0,
                    0.901f
                );


            }

        }

        public virtual void drawHover(SpriteBatch b)
        {

            if (interfacing)
            {

                if (interfaceComponents[focus].text != null)
                {

                    Vector2 textOffset = Game1.smallFont.MeasureString(interfaceComponents[focus].text) * 1.25f;

                    Microsoft.Xna.Framework.Rectangle hoverbox = drawHoverBox(b,(int)textOffset.X+32, (int)textOffset.Y+32);

                    b.DrawString(
                        Game1.smallFont, 
                        interfaceComponents[focus].text, 
                        new Vector2(hoverbox.Center.X-(textOffset.X/2), 
                        hoverbox.Center.Y - (textOffset.Y / 2)), 
                        Game1.textColor, 
                        0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1f);

                    b.DrawString(
                        Game1.smallFont, 
                        interfaceComponents[focus].text, 
                        new Vector2(hoverbox.Center.X - (textOffset.X / 2) - 1f, 
                        hoverbox.Center.Y - (textOffset.Y / 2) + 1.5f), 
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

            if (scrollId != 0 && scrollBox.Contains(x, y))
            {

                scrolling = true;

                return;

            }

            foreach (KeyValuePair<int, JournalComponent> component in interfaceComponents)
            {

                if (!component.Value.active)
                {
                    
                    continue;

                }

                if (component.Value.bounds.Contains(x,y))
                {

                    focus = component.Key;

                    interfacing = true;

                    if(component.Value.hover < component.Value.spec.hoverLimit)
                    {

                        component.Value.hover++;

                    }

                }
                else if(component.Value.hover > 0)
                {

                    component.Value.hover--;

                }

            }

            if (interfacing)
            {

                return;

            }

            if(pagination == 0)
            {

                return;

            }

            int top = record - (record % pagination);

            //int upper = contentComponents.Count % pagination;

            for (int i = 0; i < pagination; i++)
            {

                int index = top + i;

                if (contentComponents.Count <= index)
                {
                    break;
                }

                ContentComponent component = contentComponents[index];

                if (component.bounds.Contains(x, y))
                {

                    browsing = true;

                    focus = index;

                    return;

                }

            }

        }

        public override void receiveKeyPress(Keys key)
        {
        }

        public override void applyMovementKey(int direction)
        {
        }

        public override void receiveLeftClick(int x, int y, bool playSound = true)
        {

            if (scrolling)
            {

                scrollWithin(y);

                return;
            
            }

            if (interfacing)
            {

                pressButton(interfaceComponents[focus].button);

                return;

            }

            if(browsing)
            {

                pressContent();

            }

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
        }

    }

    public class JournalComponent
    {
        
        public DruidJournal.journalButtons button;

        public Microsoft.Xna.Framework.Vector2 position;

        public IconData.displays display;

        public Microsoft.Xna.Framework.Rectangle bounds;

        public Microsoft.Xna.Framework.Rectangle source;

        public int hover;

        public string text;

        public bool active;

        public float fade = 1f;

        public JournalAdditional spec = new();

        public JournalComponent(DruidJournal.journalButtons Button, Vector2 Position, IconData.displays Display, JournalAdditional additional)
        {

            position = Position;

            button = Button;

            display = Display;

            spec = additional;

            setBounds();

        }
        
        public void setBounds()
        {

            source = IconData.DisplayRectangle(display);

            bounds = new((int)position.X - (int)(8f * spec.scale), (int)position.Y - (int)(8f * spec.scale), (int)(16f * spec.scale), (int)(16f * spec.scale));

            text = DialogueData.ButtonStrings(button);

        }

        public void draw(SpriteBatch b)
        {

            b.Draw(
                Mod.instance.iconData.displayTexture, 
                position, 
                source,
                Color.White * fade,
                0f, 
                new Vector2(8), 
                spec.scale + (0.05f * hover), 
                spec.flip ? SpriteEffects.FlipHorizontally : 0, 
                999f
            );

        }

    }

    public class JournalAdditional
    {

        public float scale = 3.5f;

        public bool flip;

        public float hoverLimit = 5;

    }

    public class ContentComponent
    {

        public enum contentTypes
        {
            list,
            gallery,
            title,
            text

        }

        public contentTypes type;

        public string id;

        public int context;

        public Microsoft.Xna.Framework.Rectangle bounds;

        // display content

        public string text;

        public Vector2 measure;

        public Microsoft.Xna.Framework.Color color;

        public IconData.displays icon;

        public Microsoft.Xna.Framework.Rectangle iconSource;

        public ContentComponent(contentTypes Type, string ID)
        {

            type = Type;

            id = ID;

            color = new Color(86, 22, 12);

        }

        public void setBounds(int index, int xP, int yP, int width, int height)
        {

            switch (type)
            {

                case contentTypes.list:

                    bounds = new Rectangle(xP + 16, yP + 16 + index * ((height - 32) / 6), width - 32, (height - 32) / 6 + 4);

                    if(icon != IconData.displays.none)
                    {

                        iconSource = IconData.DisplayRectangle(icon);

                    }

                    return;

                case contentTypes.title:

                    text = Game1.parseText(id, Game1.dialogueFont, width);

                    measure = Game1.dialogueFont.MeasureString(text);

                    bounds = new Rectangle(xP, yP, width, 80);

                    return;

                case contentTypes.text:

                    // width should be journal.width - 128

                    text = Game1.parseText(id, Game1.dialogueFont, width);

                    measure = Game1.dialogueFont.MeasureString(text);

                    bounds = new Rectangle(xP, yP, (int)measure.X, (int)measure.Y+16);

                    return;

            }

        }

        public void draw(SpriteBatch b, Vector2 offset, bool focus = false)
        {

            switch (type)
            {

                case contentTypes.list:

                    IClickableMenu.drawTextureBox(
                        b,
                        Game1.mouseCursors,
                        new Rectangle(384, 396, 15, 15),
                        bounds.X,
                        bounds.Y,
                        bounds.Width,
                        bounds.Height,
                        focus ? Color.Wheat : Color.White,
                        4f,
                        false,
                        -1f
                    );

                    // icon

                    b.Draw(
                        Mod.instance.iconData.displayTexture,
                        new Vector2(bounds.Left + 20f + 32f - 1.5f,  bounds.Center.Y + 3f),
                        iconSource,
                        Microsoft.Xna.Framework.Color.Black * 0.35f,
                        0f,
                        new Vector2(8),
                        4f, 
                        0, 
                        0.900f
                    );

                    b.Draw(
                        Mod.instance.iconData.displayTexture,
                        new Vector2(bounds.Left + 20f + 32f, bounds.Center.Y),
                        iconSource,
                        Microsoft.Xna.Framework.Color.White,
                        0f,
                        new Vector2(8),
                        4f,
                        0,
                        0.901f
                    );

                    // title

                    b.DrawString(
                        Game1.dialogueFont,
                        text,
                        new Vector2(bounds.Left + 20f + 64 + 16f - 1.5f, bounds.Center.Y - 20 + 1.5f),
                        Microsoft.Xna.Framework.Color.Brown * 0.35f,
                        0f,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        0.900f
                    );

                    b.DrawString(
                        Game1.dialogueFont,
                        text,
                        new Vector2(bounds.Left + 20f + 64 + 16f, bounds.Center.Y - 20),
                        color,
                        0f,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        0.901f
                    );

                    return;

                case contentTypes.title:

                    b.DrawString(
                        Game1.dialogueFont,
                        text,
                        new Vector2(bounds.Center.X - (measure.X / 2f * 1.1f) - 1.5f, offset.Y + bounds.Center.Y - (measure.Y / 2 * 1.1f) + 1.5f),
                        Microsoft.Xna.Framework.Color.Brown * 0.35f,
                        0f,
                        Vector2.Zero,
                        1.1f,
                        SpriteEffects.None,
                        0.900f
                    );

                    b.DrawString(
                        Game1.dialogueFont,
                        text,
                        new Vector2(bounds.Center.X - (measure.X / 2f * 1.1f), offset.Y + bounds.Center.Y - (measure.Y / 2 * 1.1f)),
                        color,
                        0f,
                        Vector2.Zero,
                        1.1f,
                        SpriteEffects.None,
                        0.901f
                    );

                    return;

                case contentTypes.text:

                    b.DrawString(
                        Game1.dialogueFont,
                        text,
                        new Vector2(bounds.Left - 1.5f, offset.Y + bounds.Top + 1.5f),
                        Microsoft.Xna.Framework.Color.Brown * 0.35f,
                        0f,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        0.900f
                    );

                    b.DrawString(
                        Game1.dialogueFont,
                        text,
                        new Vector2(bounds.Left, offset.Y + bounds.Top),
                        color,
                        0f,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        0.901f
                    );
                    return;

            }

        }

    }

}
