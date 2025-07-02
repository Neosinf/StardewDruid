using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Battle;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Locations;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

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

        public string dragonTitle;

        public List<string> dragonAbilities = new();

        public int dragonVitality;

        public int dragonAttack;

        public int dragonSpeed;

        public int dragonResist;

        public DragonPage(string QuestId, int Record) : base(QuestId, Record) 
        {

        }

        public override void populateInterface()
        {

            type = journalTypes.dragonPage;

            parentJournal = journalTypes.relics;

            title = StringData.Strings(StringData.stringkeys.dragonomicon);

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.quests),
                [102] = addButton(journalButtons.effects),
                [103] = addButton(journalButtons.relics),
                [104] = addButton(journalButtons.herbalism),
                [105] = addButton(journalButtons.lore),
                [106] = addButton(journalButtons.transform),
                [107] = addButton(journalButtons.recruits),

                [109] = addButton(journalButtons.dragonReset),
                [110] = addButton(journalButtons.dragonSave),

                [301] = addButton(journalButtons.exit),

            };

            /*if (!RelicData.HasRelic(StardewDruid.Data.IconData.relics.heiress_gift))
            {

                interfaceComponents[107] = addButton(journalButtons.pals);

            }*/

        }

        public void FixSchemes()
        {



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

            ContentComponent portrait = new(ContentComponent.contentTypes.custom, "portrait")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) - 352, yPositionOnScreen + 48, 320, 320)
            };

            contentComponents[start++] = portrait;

            // =========================================== colours

            int colourArrayX = xPositionOnScreen + (width / 2) - 352;

            int colourArrayY = yPositionOnScreen + 372 + 40;

            int colourCount = Enum.GetValues<DragonRender.dragonSchemes>().Count();

            for (int s = 0; s < colourCount; s++)
            {

                //KeyValuePair<string, IconData.schemes> scheme = Mod.instance.iconData.DragonSchemery.ElementAt(s);

                ContentComponent chooseDragon = new(ContentComponent.contentTypes.custom, "dragon");

                chooseDragon.text[0] = DragonRender.SchemeDescriptions(s);

                chooseDragon.serial = s;

                chooseDragon.bounds = new Rectangle(colourArrayX + (s * 96), colourArrayY, 64, 64);

                contentComponents[start++] = chooseDragon;

            }

            // =========================================== breath

            colourArrayY = yPositionOnScreen + 480 + 40;

            colourCount = Enum.GetValues<DragonRender.breathSchemes>().Count();

            for (int s = 0; s < colourCount; s++)
            {

                //KeyValuePair<string, IconData.schemes> scheme = Mod.instance.iconData.BreathSchemery.ElementAt(s);

                ContentComponent chooseBreath = new(ContentComponent.contentTypes.custom, "breath");

                chooseBreath.text[0] = DragonRender.BreathDescriptions(s);

                chooseBreath.serial = s;

                chooseBreath.bounds = new Rectangle(colourArrayX + (s * 96), colourArrayY, 64, 64);

                contentComponents[start++] = chooseBreath;

            }

            // =========================================== sizes

            int sizeArrayX = xPositionOnScreen + width - 160;

            int sizeArrayY = yPositionOnScreen + 32;

            for (int s = 1; s < 6; s++)
            {

                ContentComponent chooseSize = new(ContentComponent.contentTypes.custom, "size");

                chooseSize.text[0] = DragonRender.SizeDescriptions(s);

                chooseSize.serial = s;

                chooseSize.bounds = new Rectangle(sizeArrayX, sizeArrayY + (s * 96), 64, 64);

                contentComponents[start++] = chooseSize;

            }

            // ===========================================

            dragonPrimary = new(StringData.Strings(StringData.stringkeys.primaryColour), xPositionOnScreen + (width / 2) + 32, yPositionOnScreen + 96);

            dragonPrimary.setColor(dragonRender.primary);

            ContentComponent choosePrimary = new(ContentComponent.contentTypes.custom, "primary")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) + 32, yPositionOnScreen + 96, SliderBar.defaultWidth, 60)
            };

            contentComponents[start++] = choosePrimary;

            // ===========================================

            dragonSecondary = new(StringData.Strings(StringData.stringkeys.secondaryColour), xPositionOnScreen + (width / 2) + 32, yPositionOnScreen + 200);

            dragonSecondary.setColor(dragonRender.secondary);

            ContentComponent chooseSecondary = new(ContentComponent.contentTypes.custom, "secondary")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) + 32, yPositionOnScreen + 200, SliderBar.defaultWidth, 60)
            };

            contentComponents[start++] = chooseSecondary;

            // ===========================================

            dragonTertiary = new(StringData.Strings(StringData.stringkeys.tertiaryColour), xPositionOnScreen + (width / 2) + 32, yPositionOnScreen + 304);

            dragonTertiary.setColor(dragonRender.tertiary);

            ContentComponent chooseTertiary = new(ContentComponent.contentTypes.custom, "tertiary")
            {
                bounds = new Rectangle(xPositionOnScreen + (width / 2) + 32, yPositionOnScreen + 304, SliderBar.defaultWidth, 60)
            };

            contentComponents[start++] = chooseTertiary;


            dragonTitle = BattleDragon.DragonTitle();

            dragonAbilities.Clear();

            Dictionary<BattleCombatant.battleoptions, BattleAbility.battleabilities> specials = BattleDragon.BattleMoveset(new());

            foreach (KeyValuePair<BattleCombatant.battleoptions, BattleAbility.battleabilities> abilities in specials)
            {

                BattleAbility ability = new(abilities.Value);

                dragonAbilities.Add(ability.title);

            }

            dragonVitality = BattleDragon.BattleVitality(1);

            dragonAttack = BattleDragon.BattleAttack(1);

            dragonSpeed = BattleDragon.BattleSpeed(1);

            dragonResist = BattleDragon.BattleResist(1);

        }

        public override void activateInterface()
        {

            resetInterface();

            fadeMenu();

            if (Mod.instance.magic)
            {

                interfaceComponents[101].active = false;

                interfaceComponents[103].fade = 1f;

                interfaceComponents[103].text = StringData.Strings(StringData.stringkeys.dragonomicon);

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

                    if (dragonScheme != 0)
                    {

                        dragonPrimary.setColor(dragonRender.gradientColours[(DragonRender.dragonSchemes)dragonScheme][0]);

                        dragonSecondary.setColor(dragonRender.gradientColours[(DragonRender.dragonSchemes)dragonScheme][1]);

                        dragonTertiary.setColor(dragonRender.gradientColours[(DragonRender.dragonSchemes)dragonScheme][2]);

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

                    dragonScheme = contentComponents[focus].serial;

                    Mod.instance.Config.dragonScheme = dragonScheme;

                    dragonRender.LoadColourScheme((DragonRender.dragonSchemes)Mod.instance.Config.dragonScheme);

                    Game1.playSound("shwip");

                    populateContent();

                    break;

                case "breath":

                    breathScheme = contentComponents[focus].serial;

                    Mod.instance.Config.dragonBreath = breathScheme;

                    dragonRender.LoadBreathScheme((DragonRender.breathSchemes)Mod.instance.Config.dragonBreath);

                    Game1.playSound("shwip");

                    populateContent();

                    break;

                case "size":

                    Mod.instance.Config.dragonScale = contentComponents[focus].serial;

                    dragonScale = 2f + (0.5f * Mod.instance.Config.dragonScale);

                    Game1.playSound("shwip");

                    populateContent();

                    break;

            }

            return;

        }

        public virtual void SwitchToCustomScheme()
        {

            dragonScheme = 0;

            Mod.instance.Config.dragonScheme = 0;

            dragonRender.LoadColourScheme(DragonRender.dragonSchemes.dragon_custom);

        }

        public override void pressEnter(int x, int y)
        {

            if (dragonPrimary.containsPoint(x, y))
            {

                dragonPrimary.click(x, y);

                DragonUpdateColour();

                SwitchToCustomScheme();

                return;

            }
            else if (dragonSecondary.containsPoint(x, y))
            {

                dragonSecondary.click(x, y);

                DragonUpdateColour();

                SwitchToCustomScheme();

                return;

            }
            else if (dragonTertiary.containsPoint(x, y))
            {

                dragonTertiary.click(x, y);

                DragonUpdateColour();

                SwitchToCustomScheme();

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

            dragonRender.LoadCustomColour();


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

                DragonUpdateColour();

                SwitchToCustomScheme();


            }
            else if (dragonSecondary.containsPoint(x, y))
            {

                dragonSecondary.releaseClick();

                DragonUpdateColour();

                SwitchToCustomScheme();

            }
            else if (dragonTertiary.containsPoint(x, y))
            {

                dragonTertiary.releaseClick();

                DragonUpdateColour();

                SwitchToCustomScheme();

            }

        }

        public override void drawContent(SpriteBatch b)
        {

            int center = xPositionOnScreen + (width / 2);

            Color highlight = Color.Wheat;

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

                        List<Color> embers = dragonRender.breathGradientColours[(DragonRender.breathSchemes)breathScheme];

                        for (int e = 0; e < 4; e++)
                        {

                            Vector2 emberVector = new Vector2(component.Value.bounds.Left, component.Value.bounds.Bottom) + new Vector2(16 + 64 * e, -96);

                            b.Draw(Mod.instance.iconData.emberTexture, emberVector, new(32 * e, 0, 32, 28), embers[2] * 0.65f, 0f, Vector2.Zero, 3f, 0, -1.1f);

                            b.Draw(Mod.instance.iconData.emberTexture, emberVector, new(32 * e, 32, 32, 28), embers[1] * 0.65f, 0f, Vector2.Zero, 3f, 0, -1.1f);

                            b.Draw(Mod.instance.iconData.emberTexture, emberVector, new(32 * e, 64, 32, 28), embers[0] * 0.65f, 0f, Vector2.Zero, 3f, 0, -1.1f);

                            b.Draw(Mod.instance.iconData.emberTexture, emberVector, new(32 * e, 96, 32, 28), Color.White * 0.65f, 0f, Vector2.Zero, 3f, 0, -1.1f);

                        }

                        dragonRender.drawWalk(b, new(component.Value.bounds.X + 160 - (32 * dragonScale), component.Value.bounds.Y + 304 - (64 * dragonScale)), new() { direction = dragonDirection, frame = 0, scale = dragonScale, layer = -1f, flip = dragonDirection == 3, shadow = false, });

                        b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.dragonScheme), new Vector2(component.Value.bounds.X - 1.5f, yPositionOnScreen + 372 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1.1f);

                        b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.dragonScheme), new Vector2(component.Value.bounds.X, yPositionOnScreen + 372), Game1.textColor, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1f);

                        b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.breathScheme), new Vector2(component.Value.bounds.X - 1.5f, yPositionOnScreen + 480 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1.1f);

                        b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.breathScheme), new Vector2(component.Value.bounds.X, yPositionOnScreen + 480), Game1.textColor, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1f);

                        b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.dragonSize), new Vector2(xPositionOnScreen + width - 160f - 1.5f, yPositionOnScreen + 64f + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1.1f);

                        b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.dragonSize), new Vector2(xPositionOnScreen + width - 160f, yPositionOnScreen + 64), Game1.textColor, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1f);

                        break;

                    case "dragon":

                        int checkScheme = component.Value.serial;

                        if (Mod.instance.Config.dragonScheme == checkScheme)
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
                                dragonRender.gradientColours[(DragonRender.dragonSchemes)checkScheme][0], 
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
                                dragonRender.gradientColours[(DragonRender.dragonSchemes)checkScheme][0], 
                                0f, 
                                Vector2.Zero, 
                                1f, 
                                0, 
                                -1f
                             );

                        }

                        break;

                    case "breath":

                        int checkBreathScheme = component.Value.serial;

                        if (Mod.instance.Config.dragonBreath == checkBreathScheme)
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

                            b.Draw(Game1.staminaRect, stamina, new Rectangle((int)stamina.X, (int)stamina.Y, component.Value.bounds.Width -24, component.Value.bounds.Height - 24), dragonRender.breathGradientColours[(DragonRender.breathSchemes)checkBreathScheme][1], 0f, Vector2.Zero, 1f, 0, -1f);

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

                            b.Draw(Game1.staminaRect, stamina, new Rectangle((int)stamina.X, (int)stamina.Y, component.Value.bounds.Width - 24, component.Value.bounds.Height - 24), dragonRender.breathGradientColours[(DragonRender.breathSchemes)checkBreathScheme][1], 0f, Vector2.Zero, 1f, 0, -1f);

                        }

                        break;

                    case "size":

                        //int sizeComponent = Convert.ToInt32(component.Value.text[0];

                        int sizeComponent = component.Value.serial;

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

                            b.Draw(Game1.staminaRect, stamina, new Rectangle((int)stamina.X, (int)stamina.Y, 20 + 4 * sizeComponent, 20 + 4 * sizeComponent), dragonRender.gradientColours[(DragonRender.dragonSchemes)dragonScheme][0], 0f, Vector2.Zero, 1f, 0, -1f);

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

                            b.Draw(Game1.staminaRect, stamina, new Rectangle((int)stamina.X, (int)stamina.Y, 16 + 4 * sizeComponent, 16 + 4 * sizeComponent), dragonRender.gradientColours[(DragonRender.dragonSchemes)dragonScheme][0], 0f, Vector2.Zero, 1f, 0, -1f);

                        }

                        break;

                    case "primary":

                        b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.primaryColour), new Vector2(center + 32, yPositionOnScreen + 64), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

                        b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.primaryColour), new Vector2(center + 32 - 1.5f, yPositionOnScreen + 64 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

                        dragonPrimary.draw(b);

                        break;

                    case "secondary":

                        b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.secondaryColour), new Vector2(center + 32, yPositionOnScreen + 168), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

                        b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.secondaryColour), new Vector2(center + 32 - 1.5f, yPositionOnScreen + 168 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

                        dragonSecondary.draw(b);

                        break;

                    case "tertiary":

                        b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.tertiaryColour), new Vector2(center + 32, yPositionOnScreen + 272), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

                        b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.tertiaryColour), new Vector2(center + 32 - 1.5f, yPositionOnScreen + 272 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

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
                    case "size":
                        drawHoverText(b, contentComponents[focus].text[0]);

                        break;

                    case "portrait":

                        DrawBattleReadout(b);

                        break;

                }

            }

        }

        public virtual void DrawBattleReadout(SpriteBatch b)
        {
            
            float contentHeight = 16;

            // -------------------------------------------------------
            // title

            string titleText = Game1.parseText(dragonTitle, Game1.dialogueFont, 476);

            Vector2 titleSize = Game1.dialogueFont.MeasureString(titleText);

            contentHeight += 24 + titleSize.Y;

            // -------------------------------------------------------
            // description

            string descriptionText = Game1.parseText(StringData.Strings(StringData.stringkeys.dragonStats), Game1.smallFont, 476);

            Vector2 descriptionSize = Game1.smallFont.MeasureString(descriptionText);

            contentHeight += 12 + descriptionSize.Y;
            
            // -------------------------------------------------------
            // attack

            string attackText = Game1.parseText(StringData.Strings(StringData.stringkeys.attackLevel) + dragonAttack, Game1.smallFont, 476);

            Vector2 attackSize = Game1.smallFont.MeasureString(attackText);

            contentHeight += 12 + attackSize.Y;

            // -------------------------------------------------------
            // health

            string healthText = Game1.parseText(StringData.Strings(StringData.stringkeys.healthLevel) + dragonVitality, Game1.smallFont, 476);

            Vector2 healthSize = Game1.smallFont.MeasureString(healthText);

            contentHeight += 12 + healthSize.Y;

            // -------------------------------------------------------
            // speed

            string speedText = Game1.parseText(StringData.Strings(StringData.stringkeys.speedLevel) + dragonSpeed, Game1.smallFont, 476);

            Vector2 speedSize = Game1.smallFont.MeasureString(speedText);

            contentHeight += 12 + speedSize.Y;

            // -------------------------------------------------------
            // resist

            string resistText = Game1.parseText(StringData.Strings(StringData.stringkeys.resistLevel) + dragonResist, Game1.smallFont, 476);

            Vector2 resistSize = Game1.smallFont.MeasureString(resistText);

            contentHeight += 12 + resistSize.Y;


            if (dragonAbilities.Count > 0)
            {

                contentHeight += 12;

                foreach (string detail in dragonAbilities)
                {

                    string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                    Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                    contentHeight += detailSize.Y;

                }

                contentHeight += 12;

            }


            // -------------------------------------------------------
            // texturebox

            int cornerX = Game1.getMouseX() + 32;

            int cornerY = Game1.getMouseY() + 32;

            if (cornerX > Game1.graphics.GraphicsDevice.Viewport.Width - 512)
            {

                int tryCorner = cornerX - 576;

                cornerX = tryCorner < 0 ? 0 : tryCorner;

            }

            if (cornerY > Game1.graphics.GraphicsDevice.Viewport.Height - contentHeight - 48)
            {

                int tryCorner = cornerY - (int)(contentHeight + 64f);

                cornerY = tryCorner < 0 ? 0 : tryCorner;

            }

            Vector2 corner = new(cornerX, cornerY);

            IClickableMenu.drawTextureBox(b, Game1.menuTexture, new Rectangle(0, 256, 60, 60), (int)corner.X, (int)corner.Y, 512, (int)(contentHeight), Color.White, 1f, true, -1f);

            float textPosition = corner.Y + 16;

            float textMargin = corner.X + 16;

            // -------------------------------------------------------
            // title

            b.DrawString(Game1.dialogueFont, titleText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.dialogueFont, titleText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 8 + titleSize.Y;

            Color outerTop = new(167, 81, 37);

            Color outerBot = new(139, 58, 29);

            Color inner = new(246, 146, 30);

            // -------------------------------------------------------
            // description

            b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 488, 2), outerTop);

            b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 488, 3), inner);

            textPosition += 12;

            b.DrawString(Game1.smallFont, descriptionText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, descriptionText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 12 + descriptionSize.Y;


            // -------------------------------------------------------
            // attack

            b.DrawString(Game1.smallFont, attackText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, attackText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 12 + attackSize.Y;

            // -------------------------------------------------------
            // health

            b.DrawString(Game1.smallFont, healthText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, healthText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 12 + healthSize.Y;

            // -------------------------------------------------------
            // speed

            b.DrawString(Game1.smallFont, speedText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, speedText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 12 + speedSize.Y;

            // -------------------------------------------------------
            // resist

            b.DrawString(Game1.smallFont, resistText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, resistText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 12 + resistSize.Y;


            // -------------------------------------------------------
            // details

            if (dragonAbilities.Count > 0)
            {

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 488, 2), outerTop);

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 488, 3), inner);

                textPosition += 12;

                foreach (string detail in dragonAbilities)
                {

                    string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                    Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                    b.DrawString(Game1.smallFont, detailText, new Vector2(textMargin, textPosition), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, detailText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    textPosition += detailSize.Y;

                }

                textPosition += 12;

            }

        }

    }

}
