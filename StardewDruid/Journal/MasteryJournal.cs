using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Events;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Journal
{
    public class MasteryJournal : DruidJournal
    {

        public MasteryDiscipline.disciplines hoverFocus;

        public List<string> hoverData = new();

        public MasteryJournal(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
        {


        }

        public override void prepareContent()
        {

        }

        public override void populateContent()
        {

            type = journalTypes.masteries;

            pagination = 0;

            contentComponents = Mod.instance.masteryHandle.DisciplineComponents();

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                int componentWidth = ((width - 40) / 5);

                component.Value.SetText(width);

                component.Value.bounds = new Rectangle(xPositionOnScreen + 20 + (component.Key % 5) * componentWidth, yPositionOnScreen + 24, componentWidth, height - 40);

            }

        }

        public override void populateInterface()
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

                [201] = addButton(journalButtons.openEffects),
                [202] = addButton(journalButtons.openLore),

                [301] = addButton(journalButtons.exit),

            };

        }

        public override void pressContent()
        {

            openJournal(journalTypes.masteryOverview, contentComponents[focus].id);

        }

        public override void drawContent(SpriteBatch b)
        {

            for (int i = 0; i < contentComponents.Count; i++)
            {

                DrawDiscipline(b, contentComponents[i], i);

            }

        }

        public virtual void DrawDiscipline(SpriteBatch b, ContentComponent contentComponent, int Index)
        {

            IClickableMenu.drawTextureBox(
                b,
                Game1.mouseCursors,
                new Rectangle(384, 396, 15, 15),
                contentComponent.bounds.X,
                contentComponent.bounds.Y,
                contentComponent.bounds.Width,
                contentComponent.bounds.Height,
                (browsing && Index == focus) ? Color.Wheat : Color.White,
                3f,
                false,
                -1f
            );

            // upper image -------------------------------------------

            Vector2 center = contentComponent.bounds.Center.ToVector2();

            Vector2 origin = new Vector2(contentComponent.textureSources[0].Width / 2, contentComponent.textureSources[0].Height / 2);

            Vector2 placement = center - new Vector2(0, origin.Y * 4);

            b.Draw(Mod.instance.iconData.masteryTexture, placement + new Vector2(-1, 3), contentComponent.textureSources[0], Microsoft.Xna.Framework.Color.Black * 0.35f, 0f, origin, 4f, 0, 0.900f);

            b.Draw(Mod.instance.iconData.masteryTexture, placement, contentComponent.textureSources[0], Color.White, 0f, origin, 4f, 0, 0.901f);

            // discipline label -------------------------------------------

            origin = new Vector2(contentComponent.textMeasures[0].X / 2, contentComponent.textMeasures[0].Y / 2);

            placement = center + new Vector2(4, 8);

            b.DrawString(Game1.dialogueFont, contentComponent.textParse[0], placement + new Vector2(-1, 1), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, origin, 0.8f, SpriteEffects.None, 0.900f);

            b.DrawString(Game1.dialogueFont, contentComponent.textParse[0], placement, contentComponent.textColours[0], 0f, origin, 0.8f, SpriteEffects.None, 0.901f);

            if (!contentComponent.textureSources.ContainsKey(1))
            {

                return;

            }

            // path icons -------------------------------------------

            origin = new Vector2(contentComponent.textureSources[1].Width / 2, contentComponent.textureSources[1].Height / 2);

            placement = center + new Vector2(0, (contentComponent.textMeasures[0].Y / 2) + (contentComponent.textureSources[1].Height * 1.5f) + 16);

            for (int i = 1; i < contentComponent.textureSources.Count; i++)
            {

                b.Draw(Mod.instance.iconData.masteryTexture, placement, contentComponent.textureSources[i], Color.White, 0f, origin, 3f, 0, 0.901f);

                placement.Y += (contentComponent.textureSources[1].Height * 3) + 16;

            }

        }

        public override void drawHover(SpriteBatch b)
        {

            base.drawHover(b);

            if (!browsing)
            {

                return;

            }

            string disciplineString = contentComponents[focus].id;

            MasteryDiscipline.disciplines disciplineId = Enum.Parse<MasteryDiscipline.disciplines>(disciplineString);

            MasteryDiscipline disciplineData = Mod.instance.masteryHandle.disciplines[disciplineId];

            float contentHeight = 16;

            // -------------------------------------------------------
            // title

            string titleText = Game1.parseText(disciplineData.name, Game1.dialogueFont, 476);

            Vector2 titleSize = Game1.dialogueFont.MeasureString(titleText);

            contentHeight += 20 + titleSize.Y;

            // -------------------------------------------------------
            // description

            string descriptionText = Game1.parseText(disciplineData.description, Game1.smallFont, 476);

            Vector2 descriptionSize = Game1.smallFont.MeasureString(descriptionText);

            contentHeight += 12 + descriptionSize.Y;

            // -------------------------------------------------------
            // details

            if(hoverFocus != disciplineId || hoverData.Count == 0)
            {

                hoverData = new();

                foreach (MasteryPath.paths path in disciplineData.paths)
                {

                    if (Mod.instance.masteryHandle.PathLocked(path))
                    {

                        continue;

                    }

                    string pathName = Mod.instance.masteryHandle.paths[path].name;

                    pathName += StringData.slash;

                    int pathLevel = Mod.instance.masteryHandle.PathLevel(path);

                    pathName += StringData.Get(StringData.str.level, new { level = pathLevel });

                    hoverData.Add(pathName);

                }

                hoverFocus = disciplineId;

            }

            contentHeight += 12; // bar

            foreach (string detail in hoverData)
            {

                string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                contentHeight += detailSize.Y;

            }

            contentHeight += 12;

            contentHeight += 4; // bottom

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

            // --------------------------------
            // top

            DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

            textPosition += 12;

            // -------------------------------------------------------
            // description

            b.DrawString(Game1.smallFont, descriptionText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, descriptionText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 12 + descriptionSize.Y;

            // -------------------------------------------------------
            // details

            DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

            textPosition += 12;

            foreach (string detail in hoverData)
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
