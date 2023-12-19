// Decompiled with JetBrains decompiler
// Type: StardewDruid.Journal.Druid
// Assembly: StardewDruid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 24DA4344-683E-4959-87A6-C0A858BCC7DA
// Assembly location: C:\Users\piers\source\repos\StardewDruid\StardewDruid\bin\Debug\net5.0\StardewDruid.dll

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Map;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#nullable disable
namespace StardewDruid.Journal
{
  internal class Druid : IClickableMenu
  {
    public Texture2D iconTexture;
    public Texture2D targetTexture;
    public Dictionary<string, Rectangle> iconFrames;
    public const int region_forwardButton = 101;
    public const int region_backButton = 102;
    public List<List<Page>> pages;
    public List<ClickableComponent> questLogButtons;
    private int currentPage;
    private int questPage = -1;
    public ClickableTextureComponent forwardButton;
    public ClickableTextureComponent endButton;
    public ClickableTextureComponent backButton;
    public ClickableTextureComponent startButton;
    protected Page _shownPage;
    protected List<string> _objectiveText;
    protected float _contentHeight;
    protected float _scissorRectHeight;
    public float scrollAmount;
    public ClickableTextureComponent upArrow;
    public ClickableTextureComponent downArrow;
    public ClickableTextureComponent scrollBar;
    private bool scrolling;
    public Rectangle scrollBarBounds;
    private string hoverText = "";

    public Druid()
      : base(0, 0, 0, 0, true)
    {
      this.iconTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Icons.png"));
      this.targetTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Target.png"));
      this.iconFrames = new Dictionary<string, Rectangle>()
      {
        ["Effigy"] = new Rectangle(0, 0, 8, 8),
        ["Weald"] = new Rectangle(8, 0, 8, 8),
        ["Mists"] = new Rectangle(16, 0, 8, 8),
        ["Stars"] = new Rectangle(24, 0, 8, 8),
        ["Jester"] = new Rectangle(0, 8, 8, 8),
        ["Fates"] = new Rectangle(8, 8, 8, 8),
        ["Ether"] = new Rectangle(16, 8, 8, 8)
      };
      Game1.playSound("bigSelect");
      this.setupPages();
      this.width = 832;
      this.height = 576;
      if (LocalizedContentManager.CurrentLanguageCode == 9 || LocalizedContentManager.CurrentLanguageCode == 8)
        this.height += 64;
      Vector2 centeringOnScreen = Utility.getTopLeftPositionForCenteringOnScreen(this.width, this.height, 0, 0);
      this.xPositionOnScreen = (int) centeringOnScreen.X;
      this.yPositionOnScreen = (int) centeringOnScreen.Y + 32;
      this.questLogButtons = new List<ClickableComponent>();
      for (int index = 0; index < 6; ++index)
        this.questLogButtons.Add(new ClickableComponent(new Rectangle(this.xPositionOnScreen + 16, this.yPositionOnScreen + 16 + index * ((this.height - 32) / 6), this.width - 32, (this.height - 32) / 6 + 4), index.ToString() ?? "")
        {
          myID = index,
          downNeighborID = -7777,
          upNeighborID = index > 0 ? index - 1 : -1,
          rightNeighborID = -7777,
          leftNeighborID = -7777,
          fullyImmutable = true
        });
      this.upperRightCloseButton = new ClickableTextureComponent(new Rectangle(this.xPositionOnScreen + this.width - 20, this.yPositionOnScreen - 8, 48, 48), Game1.mouseCursors, new Rectangle(337, 494, 12, 12), 4f, false);
      ClickableTextureComponent textureComponent1 = new ClickableTextureComponent(new Rectangle(this.xPositionOnScreen - 64, this.yPositionOnScreen + 8, 48, 44), Game1.mouseCursors, new Rectangle(352, 495, 12, 11), 4f, false);
      ((ClickableComponent) textureComponent1).myID = 102;
      ((ClickableComponent) textureComponent1).rightNeighborID = -7777;
      this.backButton = textureComponent1;
      ClickableTextureComponent textureComponent2 = new ClickableTextureComponent(new Rectangle(this.xPositionOnScreen + this.width + 64 - 48, this.yPositionOnScreen + this.height - 48, 48, 44), Game1.mouseCursors, new Rectangle(365, 495, 12, 11), 4f, false);
      ((ClickableComponent) textureComponent2).myID = 101;
      this.forwardButton = textureComponent2;
      ClickableTextureComponent textureComponent3 = new ClickableTextureComponent(new Rectangle(this.xPositionOnScreen - 64, this.yPositionOnScreen + 72, 48, 44), Game1.mouseCursors, new Rectangle(365, 495, 12, 11), 4f, false);
      ((ClickableComponent) textureComponent3).myID = 103;
      this.startButton = textureComponent3;
      ClickableTextureComponent textureComponent4 = new ClickableTextureComponent(new Rectangle(this.xPositionOnScreen + this.width + 16, this.yPositionOnScreen + this.height - 100, 48, 44), Game1.mouseCursors, new Rectangle(365, 495, 12, 11), 4f, false);
      ((ClickableComponent) textureComponent4).myID = 104;
      this.endButton = textureComponent4;
      int num = this.xPositionOnScreen + this.width + 16;
      this.upArrow = new ClickableTextureComponent(new Rectangle(num, this.yPositionOnScreen + 96, 44, 48), Game1.mouseCursors, new Rectangle(421, 459, 11, 12), 4f, false);
      this.downArrow = new ClickableTextureComponent(new Rectangle(num, this.yPositionOnScreen + this.height - 64, 44, 48), Game1.mouseCursors, new Rectangle(421, 472, 11, 12), 4f, false);
      this.scrollBarBounds = new Rectangle();
      this.scrollBarBounds.X = ((ClickableComponent) this.upArrow).bounds.X + 12;
      this.scrollBarBounds.Width = 24;
      this.scrollBarBounds.Y = ((ClickableComponent) this.upArrow).bounds.Y + ((ClickableComponent) this.upArrow).bounds.Height + 4;
      this.scrollBarBounds.Height = ((ClickableComponent) this.downArrow).bounds.Y - 4 - this.scrollBarBounds.Y;
      this.scrollBar = new ClickableTextureComponent(new Rectangle(this.scrollBarBounds.X, this.scrollBarBounds.Y, 24, 40), Game1.mouseCursors, new Rectangle(435, 463, 6, 10), 4f, false);
      if (!Game1.options.SnappyMenus)
        return;
      this.populateClickableComponentList();
      base.snapToDefaultClickableComponent();
    }

    public void setupPages() => this.pages = QuestData.RetrievePages();

    protected virtual void customSnapBehavior(int direction, int oldRegion, int oldID)
    {
      if (oldID >= 0 && oldID < 6 && this.questPage == -1)
      {
        switch (direction)
        {
          case 1:
            if (this.currentPage < this.pages.Count - 1)
            {
              this.currentlySnappedComponent = this.getComponentWithID(101);
              this.currentlySnappedComponent.leftNeighborID = oldID;
              break;
            }
            break;
          case 2:
            if (oldID < 5 && this.pages[this.currentPage].Count - 1 > oldID)
            {
              this.currentlySnappedComponent = this.getComponentWithID(oldID + 1);
              break;
            }
            break;
          case 3:
            if (this.currentPage > 0)
            {
              this.currentlySnappedComponent = this.getComponentWithID(102);
              this.currentlySnappedComponent.rightNeighborID = oldID;
              break;
            }
            break;
        }
      }
      else if (oldID == 102)
      {
        if (this.questPage != -1)
          return;
        this.currentlySnappedComponent = this.getComponentWithID(0);
      }
      this.snapCursorToCurrentSnappedComponent();
    }

    public virtual void snapToDefaultClickableComponent()
    {
      this.currentlySnappedComponent = this.getComponentWithID(0);
      this.snapCursorToCurrentSnappedComponent();
    }

    public virtual void receiveGamePadButton(Buttons b)
    {
      if (b == 4194304 && this.questPage == -1 && this.currentPage < this.pages.Count - 1)
      {
        this.nonQuestPageForwardButton();
      }
      else
      {
        if (b != 8388608 || this.questPage != -1)
          return;
        if (this.currentPage > 0)
          this.nonQuestPageBackButton();
        else
          this.pageEndButton();
      }
    }

    public bool NeedsScroll()
    {
      return this.questPage != -1 && (double) this._contentHeight > (double) this._scissorRectHeight;
    }

    public virtual void receiveScrollWheelAction(int direction)
    {
      if (this.NeedsScroll())
      {
        float num = this.scrollAmount - (float) (Math.Sign(direction) * 64 / 2);
        if ((double) num < 0.0)
          num = 0.0f;
        if ((double) num > (double) this._contentHeight - (double) this._scissorRectHeight)
          num = this._contentHeight - this._scissorRectHeight;
        if ((double) this.scrollAmount != (double) num)
        {
          this.scrollAmount = num;
          Game1.playSound("shiny4");
          this.SetScrollBarFromAmount();
        }
      }
      base.receiveScrollWheelAction(direction);
    }

    public virtual void receiveRightClick(int x, int y, bool playSound = true)
    {
    }

    public virtual void performHoverAction(int x, int y)
    {
      this.hoverText = "";
      base.performHoverAction(x, y);
      this.forwardButton.tryHover(x, y, 0.2f);
      this.backButton.tryHover(x, y, 0.2f);
      this.endButton.tryHover(x, y, 0.2f);
      this.startButton.tryHover(x, y, 0.2f);
      if (!this.NeedsScroll())
        return;
      this.upArrow.tryHover(x, y, 0.1f);
      this.downArrow.tryHover(x, y, 0.1f);
      this.scrollBar.tryHover(x, y, 0.1f);
      int num = this.scrolling ? 1 : 0;
    }

    public virtual void receiveKeyPress(Keys key)
    {
      if (Game1.isAnyGamePadButtonBeingPressed() && this.questPage != -1 && Game1.options.doesInputListContain(Game1.options.menuButton, key))
        this.exitQuestPage();
      else
        base.receiveKeyPress(key);
      if (Game1.options.doesInputListContain(Game1.options.journalButton, key) && this.readyToClose())
      {
        Game1.exitActiveMenu();
        Game1.playSound("bigDeSelect");
      }
      if (!Mod.instance.RiteButtonPressed())
        return;
      Game1.exitActiveMenu();
    }

    private void nonQuestPageForwardButton()
    {
      ++this.currentPage;
      Game1.playSound("shwip");
      if (!Game1.options.SnappyMenus || this.currentPage != this.pages.Count - 1)
        return;
      this.currentlySnappedComponent = this.getComponentWithID(0);
      this.snapCursorToCurrentSnappedComponent();
    }

    private void nonQuestPageBackButton()
    {
      --this.currentPage;
      Game1.playSound("shwip");
      if (!Game1.options.SnappyMenus || this.currentPage != 0)
        return;
      this.currentlySnappedComponent = this.getComponentWithID(0);
      this.snapCursorToCurrentSnappedComponent();
    }

    private void pageEndButton()
    {
      this.currentPage = this.pages.Count - 1;
      Game1.playSound("shwip");
      if (!Game1.options.SnappyMenus || this.currentPage != 0)
        return;
      this.currentlySnappedComponent = this.getComponentWithID(0);
      this.snapCursorToCurrentSnappedComponent();
    }

    private void pageStartButton()
    {
      this.currentPage = 0;
      Game1.playSound("shwip");
      if (!Game1.options.SnappyMenus || this.currentPage != 0)
        return;
      this.currentlySnappedComponent = this.getComponentWithID(0);
      this.snapCursorToCurrentSnappedComponent();
    }

    public virtual void leftClickHeld(int x, int y)
    {
      if (GameMenu.forcePreventClose)
        return;
      base.leftClickHeld(x, y);
      if (this.scrolling)
        this.SetScrollFromY(y);
    }

    public virtual void releaseLeftClick(int x, int y)
    {
      if (GameMenu.forcePreventClose)
        return;
      base.releaseLeftClick(x, y);
      this.scrolling = false;
    }

    public virtual void SetScrollFromY(int y)
    {
      int y1 = ((ClickableComponent) this.scrollBar).bounds.Y;
      this.scrollAmount = Utility.Clamp((float) (y - this.scrollBarBounds.Y) / (float) (this.scrollBarBounds.Height - ((ClickableComponent) this.scrollBar).bounds.Height), 0.0f, 1f) * (this._contentHeight - this._scissorRectHeight);
      this.SetScrollBarFromAmount();
      if (y1 == ((ClickableComponent) this.scrollBar).bounds.Y)
        return;
      Game1.playSound("shiny4");
    }

    public void UpArrowPressed()
    {
      ((ClickableComponent) this.upArrow).scale = this.upArrow.baseScale;
      this.scrollAmount -= 64f;
      if ((double) this.scrollAmount < 0.0)
        this.scrollAmount = 0.0f;
      this.SetScrollBarFromAmount();
    }

    public void DownArrowPressed()
    {
      ((ClickableComponent) this.downArrow).scale = this.downArrow.baseScale;
      this.scrollAmount += 64f;
      if ((double) this.scrollAmount > (double) this._contentHeight - (double) this._scissorRectHeight)
        this.scrollAmount = this._contentHeight - this._scissorRectHeight;
      this.SetScrollBarFromAmount();
    }

    private void SetScrollBarFromAmount()
    {
      if (!this.NeedsScroll())
      {
        this.scrollAmount = 0.0f;
      }
      else
      {
        if ((double) this.scrollAmount < 8.0)
          this.scrollAmount = 0.0f;
        if ((double) this.scrollAmount > (double) this._contentHeight - (double) this._scissorRectHeight - 8.0)
          this.scrollAmount = this._contentHeight - this._scissorRectHeight;
        ((ClickableComponent) this.scrollBar).bounds.Y = (int) ((double) this.scrollBarBounds.Y + (double) (this.scrollBarBounds.Height - ((ClickableComponent) this.scrollBar).bounds.Height) / (double) Math.Max(1f, this._contentHeight - this._scissorRectHeight) * (double) this.scrollAmount);
      }
    }

    public virtual void applyMovementKey(int direction)
    {
      base.applyMovementKey(direction);
      if (!this.NeedsScroll())
        return;
      switch (direction)
      {
        case 0:
          this.UpArrowPressed();
          break;
        case 2:
          this.DownArrowPressed();
          break;
      }
    }

    public virtual void receiveLeftClick(int x, int y, bool playSound = true)
    {
      base.receiveLeftClick(x, y, playSound);
      if (Game1.activeClickableMenu == null)
        return;
      if (this.questPage == -1)
      {
        for (int index = 0; index < this.questLogButtons.Count; ++index)
        {
          if (this.pages.Count > 0 && this.pages[this.currentPage].Count > index && this.questLogButtons[index].containsPoint(x, y))
          {
            Game1.playSound("smallSelect");
            this.questPage = index;
            this._shownPage = this.pages[this.currentPage][index];
            this._objectiveText = this._shownPage.objectives;
            this.scrollAmount = 0.0f;
            this.SetScrollBarFromAmount();
            if (!Game1.options.SnappyMenus)
              return;
            this.currentlySnappedComponent = this.getComponentWithID(102);
            this.currentlySnappedComponent.rightNeighborID = -7777;
            this.currentlySnappedComponent.downNeighborID = 104;
            this.snapCursorToCurrentSnappedComponent();
            return;
          }
        }
        if (this.currentPage == 0 && ((ClickableComponent) this.backButton).containsPoint(x, y))
          this.exitThisMenu(true);
        else if (this.currentPage < this.pages.Count - 1 && ((ClickableComponent) this.forwardButton).containsPoint(x, y))
          this.nonQuestPageForwardButton();
        else if (this.currentPage > 0 && ((ClickableComponent) this.backButton).containsPoint(x, y))
          this.nonQuestPageBackButton();
        else if (this.currentPage > 0 && ((ClickableComponent) this.startButton).containsPoint(x, y))
          this.pageStartButton();
        else if (this.currentPage < this.pages.Count - 1 && ((ClickableComponent) this.endButton).containsPoint(x, y))
          this.pageEndButton();
        else
          this.exitThisMenu(true);
      }
      else
      {
        if (!this.NeedsScroll() || ((ClickableComponent) this.backButton).containsPoint(x, y))
          this.exitQuestPage();
        if (!this.NeedsScroll())
          return;
        if (((ClickableComponent) this.downArrow).containsPoint(x, y) && (double) this.scrollAmount < (double) this._contentHeight - (double) this._scissorRectHeight)
        {
          this.DownArrowPressed();
          Game1.playSound("shwip");
        }
        else if (((ClickableComponent) this.upArrow).containsPoint(x, y) && (double) this.scrollAmount > 0.0)
        {
          this.UpArrowPressed();
          Game1.playSound("shwip");
        }
        else if (((ClickableComponent) this.scrollBar).containsPoint(x, y))
          this.scrolling = true;
        else if (((Rectangle) ref this.scrollBarBounds).Contains(x, y))
          this.scrolling = true;
        else if (!((ClickableComponent) this.downArrow).containsPoint(x, y) && x > this.xPositionOnScreen + this.width && x < this.xPositionOnScreen + this.width + 128 && y > this.yPositionOnScreen && y < this.yPositionOnScreen + this.height)
        {
          this.scrolling = true;
          base.leftClickHeld(x, y);
          base.releaseLeftClick(x, y);
        }
      }
    }

    public void exitQuestPage()
    {
      this.questPage = -1;
      this.setupPages();
      Game1.playSound("shwip");
      if (!Game1.options.SnappyMenus)
        return;
      base.snapToDefaultClickableComponent();
    }

    public virtual void update(GameTime time) => base.update(time);

    public virtual void draw(SpriteBatch b)
    {
      SpriteBatch spriteBatch1 = b;
      Texture2D fadeToBlackRect = Game1.fadeToBlackRect;
      Viewport viewport = Game1.graphics.GraphicsDevice.Viewport;
      Rectangle bounds = ((Viewport) ref viewport).Bounds;
      Color color = Color.op_Multiply(Color.Black, 0.75f);
      spriteBatch1.Draw(fadeToBlackRect, bounds, color);
      SpriteText.drawStringWithScrollCenteredAt(b, "Stardew Druid", this.xPositionOnScreen + this.width / 2, this.yPositionOnScreen - 64, "", 1f, -1, 0, 0.88f, false);
      IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 373, 18, 18), this.xPositionOnScreen, this.yPositionOnScreen, this.width, this.height, Color.White, 4f, true, -1f);
      if (this.questPage == -1)
      {
        for (int index = 0; index < this.questLogButtons.Count; ++index)
        {
          if (this.pages.Count<List<Page>>() > 0 && this.pages[this.currentPage].Count<Page>() > index)
          {
            IClickableMenu.drawTextureBox(b, Game1.mouseCursors, new Rectangle(384, 396, 15, 15), this.questLogButtons[index].bounds.X, this.questLogButtons[index].bounds.Y, this.questLogButtons[index].bounds.Width, this.questLogButtons[index].bounds.Height, this.questLogButtons[index].containsPoint(Game1.getOldMouseX(), Game1.getOldMouseY()) ? Color.Wheat : Color.White, 4f, false, -1f);
            if (this.pages[this.currentPage][index].active)
              SpriteText.drawString(b, "active", ((Rectangle) ref this.questLogButtons[index].bounds).Right - 160, this.questLogButtons[index].bounds.Y + 40, 999999, -1, 999999, 1f, 0.88f, false, -1, "", -1, (SpriteText.ScrollTextAlignment) 0);
            SpriteText.drawString(b, this.pages[this.currentPage][index].title, this.questLogButtons[index].bounds.X + 100, this.questLogButtons[index].bounds.Y + 24, 999999, -1, 999999, 1f, 0.88f, false, -1, "", -1, (SpriteText.ScrollTextAlignment) 0);
            Utility.drawWithShadow(b, this.iconTexture, new Vector2((float) (this.questLogButtons[index].bounds.X + 32), (float) (this.questLogButtons[index].bounds.Y + 28)), this.iconFrames[this.pages[this.currentPage][index].icon], Color.White, 0.0f, Vector2.Zero, 5f, false, 0.99f, -1, -1, 0.35f);
          }
        }
      }
      else
      {
        SpriteText.drawStringHorizontallyCenteredAt(b, this._shownPage.title, this.xPositionOnScreen + this.width / 2, this.yPositionOnScreen + 32, 999999, -1, 999999, 1f, 0.88f, false, -1, 99999);
        string text1 = Game1.parseText(this._shownPage.description, Game1.dialogueFont, this.width - 128);
        Rectangle scissorRectangle = ((GraphicsResource) b).GraphicsDevice.ScissorRectangle;
        Vector2 vector2 = Game1.dialogueFont.MeasureString(text1);
        Rectangle rectangle = new Rectangle()
        {
          X = this.xPositionOnScreen + 32,
          Y = this.yPositionOnScreen + 96
        };
        rectangle.Height = this.yPositionOnScreen + this.height - 32 - rectangle.Y;
        rectangle.Width = this.width - 64;
        this._scissorRectHeight = (float) rectangle.Height;
        Rectangle screen = Utility.ConstrainScissorRectToScreen(rectangle);
        b.End();
        SpriteBatch spriteBatch2 = b;
        BlendState alphaBlend = BlendState.AlphaBlend;
        SamplerState pointClamp = SamplerState.PointClamp;
        RasterizerState rasterizerState = new RasterizerState();
        rasterizerState.ScissorTestEnable = true;
        Matrix? nullable = new Matrix?();
        spriteBatch2.Begin((SpriteSortMode) 0, alphaBlend, pointClamp, (DepthStencilState) null, rasterizerState, (Effect) null, nullable);
        Game1.graphics.GraphicsDevice.ScissorRectangle = screen;
        Utility.drawTextWithShadow(b, text1, Game1.dialogueFont, new Vector2((float) (this.xPositionOnScreen + 64), (float) ((double) this.yPositionOnScreen - (double) this.scrollAmount + 96.0)), Game1.textColor, 1f, -1f, -1, -1, 1f, 3);
        float num1 = (float) ((double) (this.yPositionOnScreen + 96) + (double) vector2.Y + 32.0) - this.scrollAmount;
        for (int index = 0; index < this._objectiveText.Count; ++index)
        {
          string str = this._objectiveText[index];
          int num2 = this.width - 128;
          SpriteFont dialogueFont = Game1.dialogueFont;
          int num3 = num2;
          string text2 = Game1.parseText(str, dialogueFont, num3);
          Color darkBlue = Color.DarkBlue;
          Utility.drawTextWithShadow(b, text2, Game1.dialogueFont, new Vector2((float) (this.xPositionOnScreen + 64), num1 - 8f), darkBlue, 1f, -1f, -1, -1, 1f, 3);
          num1 += Game1.dialogueFont.MeasureString(text2).Y;
          this._contentHeight = num1 + this.scrollAmount - (float) screen.Y;
        }
        b.End();
        ((GraphicsResource) b).GraphicsDevice.ScissorRectangle = scissorRectangle;
        b.Begin((SpriteSortMode) 0, BlendState.AlphaBlend, SamplerState.PointClamp, (DepthStencilState) null, (RasterizerState) null, (Effect) null, new Matrix?());
        if (this.NeedsScroll())
        {
          if ((double) this.scrollAmount > 0.0)
            b.Draw(Game1.staminaRect, new Rectangle(screen.X, ((Rectangle) ref screen).Top, screen.Width, 4), Color.op_Multiply(Color.Black, 0.15f));
          if ((double) this.scrollAmount < (double) this._contentHeight - (double) this._scissorRectHeight)
            b.Draw(Game1.staminaRect, new Rectangle(screen.X, ((Rectangle) ref screen).Bottom - 4, screen.Width, 4), Color.op_Multiply(Color.Black, 0.15f));
        }
      }
      if (this.NeedsScroll())
      {
        this.upArrow.draw(b);
        this.downArrow.draw(b);
        this.scrollBar.draw(b);
      }
      if (this.currentPage < this.pages.Count - 1 && this.questPage == -1)
      {
        this.forwardButton.draw(b);
        b.Draw(this.targetTexture, new Vector2((float) (((ClickableComponent) this.endButton).bounds.X - 12), (float) (((ClickableComponent) this.endButton).bounds.Y + 48)), new Rectangle?(new Rectangle(0, 0, 64, 64)), (double) ((ClickableComponent) this.endButton).scale > 4.0 ? new Color(0.0f, 1f, 0.0f, 1f) : new Color(0.5f, 1f, 0.5f, 1f), -1.57079637f, Vector2.Zero, 2f, (SpriteEffects) 0, 999f);
      }
      if (this.currentPage > 0)
        b.Draw(this.targetTexture, new Vector2((float) (((ClickableComponent) this.startButton).bounds.X + 60), (float) (((ClickableComponent) this.startButton).bounds.Y - 8)), new Rectangle?(new Rectangle(0, 0, 64, 64)), (double) ((ClickableComponent) this.startButton).scale > 4.0 ? new Color(0.0f, 1f, 0.0f, 1f) : new Color(0.5f, 1f, 0.5f, 1f), 1.57079637f, Vector2.Zero, 2f, (SpriteEffects) 0, 999f);
      this.backButton.draw(b);
      base.draw(b);
      Game1.mouseCursorTransparency = 1f;
      this.drawMouse(b, false, -1);
      if (this.hoverText.Length <= 0)
        return;
      IClickableMenu.drawHoverText(b, this.hoverText, Game1.dialogueFont, 0, 0, -1, (string) null, -1, (string[]) null, (Item) null, 0, -1, -1, -1, -1, 1f, (CraftingRecipe) null, (IList<Item>) null);
    }
  }
}
