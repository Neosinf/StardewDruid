﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StardewDruid.Data.DialogueData;
using static StardewDruid.Data.IconData;

namespace StardewDruid.Journal
{
    public class DragonPage : DruidJournal
    {
        public DragonRender dragonRender;

        public ColorPicker dragonPrimary;

        public ColorPicker dragonSecondary;

        public ColorPicker dragonTertiary;

        public int dragonDirection;

        public float dragonScale;

        public int dragonScheme;

        public int breathScheme;

        public DragonPage(string QuestId, int Record) : base(QuestId, Record) 
        {

        }

        public override void populateInterface()
        {

            type = journalTypes.dragonPage;

            parentJournal = journalTypes.relics;

            title = DialogueData.Strings(DialogueData.stringkeys.dragonomicon);

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.quests),
                [102] = addButton(journalButtons.effects),
                [103] = addButton(journalButtons.relics),
                [104] = addButton(journalButtons.herbalism),
                [105] = addButton(journalButtons.lore),
                [106] = addButton(journalButtons.transform),

                [107] = addButton(journalButtons.dragonReset),
                [108] = addButton(journalButtons.dragonSave),

                [301] = addButton(journalButtons.exit),

            };

        }

        public override void populateContent()
        {

            dragonRender = new DragonRender();

            dragonRender.LoadConfigScheme();

            dragonDirection = 1;

            dragonScale = 2f + (0.5f * Mod.instance.Config.dragonScale);

            dragonScheme = Mod.instance.Config.dragonScheme;

            breathScheme = Mod.instance.Config.dragonBreath;

            // ===========================================

            int start = 0;

            contentComponents = new();

            ContentComponent portrait = new(ContentComponent.contentTypes.custom, "portrait");

            portrait.bounds = new Rectangle(xPositionOnScreen + (width / 2) - 352, yPositionOnScreen + 48, 320, 320);

            contentComponents[start++] = portrait;

            // =========================================== colours

            int colourArrayX = xPositionOnScreen + (width / 2) - 352;

            int colourArrayY = yPositionOnScreen + 372 + 40;

            int colourCount = Mod.instance.iconData.DragonOptions.Count;

            for (int s = 0; s < colourCount; s++)
            {

                KeyValuePair<string, IconData.schemes> scheme = Mod.instance.iconData.DragonSchemery.ElementAt(s);

                ContentComponent chooseDragon = new(ContentComponent.contentTypes.custom, "dragon");

                chooseDragon.text[0] = scheme.Key;

                chooseDragon.bounds = new Rectangle(colourArrayX + (s * 96), colourArrayY, 64, 64);

                contentComponents[start++] = chooseDragon;

            }

            // =========================================== breath

            colourArrayY = yPositionOnScreen + 480 + 40;

            colourCount = Mod.instance.iconData.BreathOptions.Count;

            for (int s = 0; s < colourCount; s++)
            {

                KeyValuePair<string, IconData.schemes> scheme = Mod.instance.iconData.BreathSchemery.ElementAt(s);

                ContentComponent chooseBreath = new(ContentComponent.contentTypes.custom, "breath");

                chooseBreath.text[0] = scheme.Key;

                chooseBreath.bounds = new Rectangle(colourArrayX + (s * 96), colourArrayY, 64, 64);

                contentComponents[start++] = chooseBreath;

            }

            // =========================================== sizes

            int sizeArrayX = xPositionOnScreen + width - 160;

            int sizeArrayY = yPositionOnScreen + 32;

            for (int s = 1; s < 6; s++)
            {

                ContentComponent chooseSize = new(ContentComponent.contentTypes.custom, "size");

                chooseSize.text[0] = s.ToString();

                chooseSize.bounds = new Rectangle(sizeArrayX, sizeArrayY + (s * 96), 64, 64);

                contentComponents[start++] = chooseSize;

            }

            // ===========================================

            dragonPrimary = new(DialogueData.Strings(stringkeys.primaryColour), xPositionOnScreen + (width / 2) + 32, yPositionOnScreen + 96);

            dragonPrimary.setColor(Mod.instance.iconData.gradientColours[IconData.schemes.dragon_custom][0]);

            ContentComponent choosePrimary = new(ContentComponent.contentTypes.custom, "primary");

            choosePrimary.bounds = new Rectangle(xPositionOnScreen + (width / 2) + 32, yPositionOnScreen + 96, SliderBar.defaultWidth, 60);

            contentComponents[start++] = choosePrimary;

            // ===========================================

            dragonSecondary = new(DialogueData.Strings(stringkeys.secondaryColour), xPositionOnScreen + (width / 2) + 32, yPositionOnScreen + 200);

            dragonSecondary.setColor(Mod.instance.iconData.gradientColours[IconData.schemes.dragon_custom][1]);

            ContentComponent chooseSecondary = new(ContentComponent.contentTypes.custom, "secondary");

            chooseSecondary.bounds = new Rectangle(xPositionOnScreen + (width / 2) + 32, yPositionOnScreen + 200, SliderBar.defaultWidth, 60);

            contentComponents[start++] = chooseSecondary;

            // ===========================================

            dragonTertiary = new(DialogueData.Strings(stringkeys.tertiaryColour), xPositionOnScreen + (width / 2) + 32, yPositionOnScreen + 304);

            dragonTertiary.setColor(Mod.instance.iconData.gradientColours[IconData.schemes.dragon_custom][2]);

            ContentComponent chooseTertiary = new(ContentComponent.contentTypes.custom, "tertiary");

            chooseTertiary.bounds = new Rectangle(xPositionOnScreen + (width / 2) + 32, yPositionOnScreen + 304, SliderBar.defaultWidth, 60);

            contentComponents[start++] = chooseTertiary;

        }

        public override void activateInterface()
        {

            resetInterface();

            fadeMenu();

            if (Mod.instance.magic)
            {

                interfaceComponents[101].active = false;

                interfaceComponents[103].fade = 1f;

                interfaceComponents[103].text = DialogueData.Strings(stringkeys.dragonomicon);

            }

        }

        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.back:

                    DruidJournal.openJournal(parentJournal, null, record);

                    break;

                case journalButtons.dragonReset:

                    IconData.schemes newScheme = (IconData.schemes)dragonScheme;

                    if (newScheme != schemes.dragon_custom)
                    {

                        dragonPrimary.setColor(Mod.instance.iconData.gradientColours[newScheme][0]);

                        dragonSecondary.setColor(Mod.instance.iconData.gradientColours[newScheme][1]);

                        dragonTertiary.setColor(Mod.instance.iconData.gradientColours[newScheme][2]);

                        DragonUpdateColour();

                    }

                    return;

                case journalButtons.dragonSave:

                    Mod.instance.SaveConfig();

                    exitThisMenu();

                    return;

                default:

                    base.pressButton(button);

                    break;

            }

        }

        public override void pressContent()
        {

            switch (contentComponents[focus].id)
            {

                case "portrait":

                    dragonDirection = (dragonDirection + 1) % 4;

                    break;

                case "dragon":

                    dragonScheme = (int)Mod.instance.iconData.DragonSchemery[contentComponents[focus].text[0]];

                    Mod.instance.Config.dragonScheme = dragonScheme;

                    dragonRender.LoadConfigScheme();

                    Game1.playSound("shwip");

                    break;

                case "breath":

                    breathScheme = (int)Mod.instance.iconData.BreathSchemery[contentComponents[focus].text[0]];

                    Mod.instance.Config.dragonBreath = breathScheme;

                    dragonRender.LoadConfigScheme();

                    Game1.playSound("shwip");

                    break;

                case "size":

                    Mod.instance.Config.dragonScale = Convert.ToInt32(contentComponents[focus].text[0]);

                    dragonScale = 2f + (0.5f * Mod.instance.Config.dragonScale);

                    Game1.playSound("shwip");

                    break;

            }

            return;

        }

        public virtual void SwitchToCustomScheme()
        {

            dragonScheme = (int)IconData.schemes.dragon_custom;

            Mod.instance.Config.dragonScheme = (int)IconData.schemes.dragon_custom;

        }

        public override void pressEnter(int x, int y)
        {

            if (dragonPrimary.containsPoint(x, y))
            {

                dragonPrimary.click(x, y);

                SwitchToCustomScheme();

                DragonUpdateColour();

                return;

            }
            else if (dragonSecondary.containsPoint(x, y))
            {

                dragonSecondary.click(x, y);

                SwitchToCustomScheme();

                DragonUpdateColour();

                return;

            }
            else if (dragonTertiary.containsPoint(x, y))
            {

                dragonTertiary.click(x, y);

                SwitchToCustomScheme();

                DragonUpdateColour();

                return;

            }

            base.pressEnter(x, y);

        }

        public void DragonUpdateColour()
        {

            Microsoft.Xna.Framework.Color primary = dragonPrimary.getSelectedColor();

            Mod.instance.Config.dragonPrimaryR = primary.R;
            Mod.instance.Config.dragonPrimaryG = primary.G;
            Mod.instance.Config.dragonPrimaryB = primary.B;

            Microsoft.Xna.Framework.Color secondary = dragonSecondary.getSelectedColor();

            Mod.instance.Config.dragonSecondaryR = secondary.R;
            Mod.instance.Config.dragonSecondaryG = secondary.G;
            Mod.instance.Config.dragonSecondaryB = secondary.B;

            Microsoft.Xna.Framework.Color tertiary = dragonTertiary.getSelectedColor();

            Mod.instance.Config.dragonTertiaryR = tertiary.R;
            Mod.instance.Config.dragonTertiaryG = tertiary.G;
            Mod.instance.Config.dragonTertiaryB = tertiary.B;

            Mod.instance.iconData.CustomDragonScheme();

            if (dragonScheme == (int)IconData.schemes.dragon_custom)
            {

                dragonRender.LoadConfigScheme();

            }

        }


        public override void leftClickHeld(int x, int y)
        {

            if (dragonPrimary.containsPoint(x, y))
            {

                dragonPrimary.clickHeld(x, y);

            }
            else if (dragonSecondary.containsPoint(x, y))
            {

                dragonSecondary.clickHeld(x, y);

            }
            else if (dragonTertiary.containsPoint(x, y))
            {

                dragonTertiary.clickHeld(x, y);

            }

        }

        public override void releaseLeftClick(int x, int y)
        {

            if (dragonPrimary.containsPoint(x, y))
            {

                dragonPrimary.releaseClick();

                SwitchToCustomScheme();

                DragonUpdateColour();

            }
            else if (dragonSecondary.containsPoint(x, y))
            {

                dragonSecondary.releaseClick();

                SwitchToCustomScheme();

                DragonUpdateColour();

            }
            else if (dragonTertiary.containsPoint(x, y))
            {

                dragonTertiary.releaseClick();

                SwitchToCustomScheme();

                DragonUpdateColour();

            }

        }

        public override void drawContent(SpriteBatch b)
        {

            int center = xPositionOnScreen + (width / 2);

            Color highlight = Color.Wheat;

            IconData.schemes scheme;

            Vector2 stamina;

            foreach (KeyValuePair<int,ContentComponent> component in contentComponents)
            {
                
                highlight = new(233,212,171);

                switch (component.Value.id)
                {

                    case "portrait":

                        IClickableMenu.drawTextureBox(
                            b,
                            Game1.mouseCursors,
                            new Rectangle(384, 396, 15, 15),
                            component.Value.bounds.X,
                            component.Value.bounds.Y,
                            component.Value.bounds.Width,
                            component.Value.bounds.Height,
                            Color.Wheat,
                            4f,
                            false,
                            -1.2f
                        );

                        List<Color> embers = Mod.instance.iconData.gradientColours[(IconData.schemes)breathScheme];

                        for (int e = 0; e < 4; e++)
                        {

                            Vector2 emberVector = new Vector2(component.Value.bounds.Left, component.Value.bounds.Bottom) + new Vector2(16 + 64 * e, -96);

                            b.Draw(Mod.instance.iconData.emberTexture, emberVector, new(32 * e, 0, 32, 28), embers[2] * 0.65f, 0f, Vector2.Zero, 3f, 0, -1.1f);

                            b.Draw(Mod.instance.iconData.emberTexture, emberVector, new(32 * e, 32, 32, 28), embers[1] * 0.65f, 0f, Vector2.Zero, 3f, 0, -1.1f);

                            b.Draw(Mod.instance.iconData.emberTexture, emberVector, new(32 * e, 64, 32, 28), embers[0] * 0.65f, 0f, Vector2.Zero, 3f, 0, -1.1f);

                            b.Draw(Mod.instance.iconData.emberTexture, emberVector, new(32 * e, 96, 32, 28), Color.White * 0.65f, 0f, Vector2.Zero, 3f, 0, -1.1f);

                        }

                        dragonRender.drawWalk(b, new(component.Value.bounds.X + 128, component.Value.bounds.Y + 240), new() { direction = dragonDirection, frame = 0, scale = dragonScale, layer = -1f, flip = dragonDirection == 3, shadow = false, });

                        b.DrawString(Game1.smallFont, DialogueData.Strings(stringkeys.dragonScheme), new Vector2(component.Value.bounds.X - 1.5f, yPositionOnScreen + 372 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1.1f);

                        b.DrawString(Game1.smallFont, DialogueData.Strings(stringkeys.dragonScheme), new Vector2(component.Value.bounds.X, yPositionOnScreen + 372), Game1.textColor, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1f);

                        b.DrawString(Game1.smallFont, DialogueData.Strings(stringkeys.breathScheme), new Vector2(component.Value.bounds.X - 1.5f, yPositionOnScreen + 480 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1.1f);

                        b.DrawString(Game1.smallFont, DialogueData.Strings(stringkeys.breathScheme), new Vector2(component.Value.bounds.X, yPositionOnScreen + 480), Game1.textColor, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1f);

                        b.DrawString(Game1.smallFont, DialogueData.Strings(stringkeys.dragonSize), new Vector2(xPositionOnScreen + width - 160f - 1.5f, yPositionOnScreen + 64f + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1.1f);

                        b.DrawString(Game1.smallFont, DialogueData.Strings(stringkeys.dragonSize), new Vector2(xPositionOnScreen + width - 160f, yPositionOnScreen + 64), Game1.textColor, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1f);

                        break;

                    case "dragon":

                        scheme = Mod.instance.iconData.DragonSchemery[component.Value.text[0]];

                        if (Mod.instance.Config.dragonScheme == (int)scheme)
                        {

                            highlight = Color.White;

                        }

                        if (browsing && focus == component.Key)
                        {
                            IClickableMenu.drawTextureBox(
                                b,
                                Game1.mouseCursors,
                                new Rectangle(384, 396, 15, 15),
                                (int)(component.Value.bounds.Center.X - component.Value.bounds.Width * 0.55f),
                                (int)(component.Value.bounds.Center.Y - component.Value.bounds.Height * 0.55f),
                                (int)(component.Value.bounds.Width * 1.1f),
                                (int)(component.Value.bounds.Height * 1.1f),
                                highlight,
                                2.2f,
                                false,
                                -1.1f
                            );

                            stamina = new Vector2(component.Value.bounds.X + 11, component.Value.bounds.Y + 11);

                            b.Draw(
                                Game1.staminaRect, 
                                stamina, 
                                new Rectangle((int)stamina.X, (int)stamina.Y, component.Value.bounds.Width - 24, component.Value.bounds.Height - 24), 
                                Mod.instance.iconData.gradientColours[scheme][0], 
                                0f, 
                                Vector2.Zero, 
                                1f, 
                                0, 
                                -1f);

                        }
                        else
                        {

                            IClickableMenu.drawTextureBox(
                                 b,
                                 Game1.mouseCursors,
                                 new Rectangle(384, 396, 15, 15),
                                 component.Value.bounds.X,
                                 component.Value.bounds.Y,
                                 component.Value.bounds.Width,
                                 component.Value.bounds.Height,
                                 highlight,
                                 2f,
                                 false,
                                 -1.1f
                             );

                            stamina = new Vector2(component.Value.bounds.X + 12, component.Value.bounds.Y + 12);

                            b.Draw(
                                Game1.staminaRect, 
                                stamina, 
                                new Rectangle((int)stamina.X, (int)stamina.Y, component.Value.bounds.Width - 24, component.Value.bounds.Height - 24), 
                                Mod.instance.iconData.gradientColours[scheme][0], 
                                0f, 
                                Vector2.Zero, 
                                1f, 
                                0, 
                                -1f
                             );

                        }

                        break;

                    case "breath":

                        scheme = Mod.instance.iconData.BreathSchemery[component.Value.text[0]];

                        if (Mod.instance.Config.dragonBreath == (int)scheme)
                        {

                            highlight = Color.White;

                        }

                        if (browsing && focus == component.Key)
                        {
                            IClickableMenu.drawTextureBox(
                                b,
                                Game1.mouseCursors,
                                new Rectangle(384, 396, 15, 15),
                                (int)(component.Value.bounds.Center.X - component.Value.bounds.Width * 0.55f),
                                (int)(component.Value.bounds.Center.Y - component.Value.bounds.Height * 0.55f),
                                (int)(component.Value.bounds.Width * 1.1f),
                                (int)(component.Value.bounds.Height * 1.1f),
                                highlight,
                                2.2f,
                                false,
                                -1.1f
                            );

                            stamina = new Vector2(component.Value.bounds.X + 11, component.Value.bounds.Y + 11);

                            b.Draw(Game1.staminaRect, stamina, new Rectangle((int)stamina.X, (int)stamina.Y, component.Value.bounds.Width -24, component.Value.bounds.Height - 24), Mod.instance.iconData.gradientColours[scheme][1], 0f, Vector2.Zero, 1f, 0, -1f);

                        }
                        else {

                            IClickableMenu.drawTextureBox(
                                 b,
                                 Game1.mouseCursors,
                                 new Rectangle(384, 396, 15, 15),
                                 component.Value.bounds.X,
                                 component.Value.bounds.Y,
                                 component.Value.bounds.Width,
                                 component.Value.bounds.Height,
                                 highlight,
                                 2f,
                                 false,
                                 -1.1f
                             );

                            stamina = new Vector2(component.Value.bounds.X + 12, component.Value.bounds.Y + 12);

                            b.Draw(Game1.staminaRect, stamina, new Rectangle((int)stamina.X, (int)stamina.Y, component.Value.bounds.Width - 24, component.Value.bounds.Height - 24), Mod.instance.iconData.gradientColours[scheme][1], 0f, Vector2.Zero, 1f, 0, -1f);

                        }

                        break;

                    case "size":

                        int sizeComponent = Convert.ToInt32(component.Value.text[0]);

                        if (dragonScale == (2f + (0.5f*sizeComponent)))
                        {

                            highlight = Color.White;

                        }

                        if (browsing && focus == component.Key)
                        {
                            IClickableMenu.drawTextureBox(
                                b,
                                Game1.mouseCursors,
                                new Rectangle(384, 396, 15, 15),
                                (int)(component.Value.bounds.Center.X - component.Value.bounds.Width * 0.55f),
                                (int)(component.Value.bounds.Center.Y - component.Value.bounds.Height * 0.55f),
                                (int)(component.Value.bounds.Width * 1.1f),
                                (int)(component.Value.bounds.Height * 1.1f),
                                highlight,
                                2.2f,
                                false,
                                -1.1f
                            );

                            stamina = new Vector2(component.Value.bounds.Center.X - 10 - (2 * sizeComponent), component.Value.bounds.Center.Y - 10 - (2 * sizeComponent));

                            b.Draw(Game1.staminaRect, stamina, new Rectangle((int)stamina.X, (int)stamina.Y, 20 + 4 * sizeComponent, 20 + 4 * sizeComponent), Mod.instance.iconData.gradientColours[(IconData.schemes)dragonScheme][0], 0f, Vector2.Zero, 1f, 0, -1f);

                        }
                        else
                        {

                            IClickableMenu.drawTextureBox(
                                 b,
                                 Game1.mouseCursors,
                                 new Rectangle(384, 396, 15, 15),
                                 component.Value.bounds.X,
                                 component.Value.bounds.Y,
                                 component.Value.bounds.Width,
                                 component.Value.bounds.Height,
                                 highlight,
                                 2f,
                                 false,
                                 -1.1f
                             );

                            stamina = new Vector2(component.Value.bounds.Center.X - 8 - (2 * sizeComponent), component.Value.bounds.Center.Y - 8 - (2 * sizeComponent));

                            b.Draw(Game1.staminaRect, stamina, new Rectangle((int)stamina.X, (int)stamina.Y, 16 + 4 * sizeComponent, 16 + 4 * sizeComponent), Mod.instance.iconData.gradientColours[(IconData.schemes)dragonScheme][0], 0f, Vector2.Zero, 1f, 0, -1f);

                        }

                        break;

                    case "primary":

                        b.DrawString(Game1.smallFont, DialogueData.Strings(stringkeys.primaryColour), new Vector2(center + 32, yPositionOnScreen + 64), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

                        b.DrawString(Game1.smallFont, DialogueData.Strings(stringkeys.primaryColour), new Vector2(center + 32 - 1.5f, yPositionOnScreen + 64 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

                        dragonPrimary.draw(b);

                        break;

                    case "secondary":

                        b.DrawString(Game1.smallFont, DialogueData.Strings(stringkeys.secondaryColour), new Vector2(center + 32, yPositionOnScreen + 168), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

                        b.DrawString(Game1.smallFont, DialogueData.Strings(stringkeys.secondaryColour), new Vector2(center + 32 - 1.5f, yPositionOnScreen + 168 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

                        dragonSecondary.draw(b);

                        break;

                    case "tertiary":

                        b.DrawString(Game1.smallFont, DialogueData.Strings(stringkeys.tertiaryColour), new Vector2(center + 32, yPositionOnScreen + 272), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

                        b.DrawString(Game1.smallFont, DialogueData.Strings(stringkeys.tertiaryColour), new Vector2(center + 32 - 1.5f, yPositionOnScreen + 272 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

                        dragonTertiary.draw(b);

                        break;


                }

            }

        }


        public override void drawHover(SpriteBatch b)
        {
            
            base.drawHover(b);

            if (browsing)
            {

                switch (contentComponents[focus].id)
                {

                    case "dragon":
                    case "breath":

                        drawHoverText(b, contentComponents[focus].text[0]);

                        break;

                }

            }

        }

    }

}
