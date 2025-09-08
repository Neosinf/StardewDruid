using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace StardewDruid.Journal
{
    public class MasteryOverview : DruidJournal
    {

        public MasteryPath.paths hoverFocus;

        public List<string> hoverData = new();

        public MasteryDiscipline.disciplines discipline;

        public MasteryOverview(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
        {

        }

        public override void populateInterface()
        {

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.openMasteries),


                [201] = addButton(journalButtons.back),

                [202] = addButton(journalButtons.previous),


                [301] = addButton(journalButtons.exit),

                [302] = addButton(journalButtons.forward),

            };

        }

        public override void prepareContent()
        {

            discipline = MasteryDiscipline.disciplines.alchemy;

            if (parameters.Count > 0)
            {

                discipline = Enum.Parse<MasteryDiscipline.disciplines>(parameters[0]);

            }

        }

        public override void populateContent()
        {

            MasteryDiscipline disciplineData = Mod.instance.masteryHandle.disciplines[discipline];

            title = disciplineData.name;

            contentColumns = 4;

            pagination = 0;

            contentComponents = Mod.instance.masteryHandle.NodeComponents(discipline);

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                int componentWidth = (width - 40) / 2;

                component.Value.SetText(componentWidth - 24);

                int componentHeight = (height - 40) / 2;

                component.Value.bounds = new Rectangle(xPositionOnScreen + 20 + (component.Key % 2) * componentWidth, yPositionOnScreen + 24 + ((int)(component.Key / 2) * componentHeight), componentWidth, componentHeight);

            }

        }

        public override void pressButton(journalButtons button)
        {

            int disciplineId = (int)discipline;

            int disciplineMax = Enum.GetValues<MasteryDiscipline.disciplines>().Count() - 1;

            switch (button)
            {
                case journalButtons.previous:

                    openJournal(journalTypes.masteries);

                    return;

                case journalButtons.forward:

                    if(disciplineMax >= (disciplineId + 1))
                    {

                        discipline = (MasteryDiscipline.disciplines)(disciplineId + 1);

                    }
                    else
                    {

                        discipline = 0;

                    }

                    populateContent();

                    return;

                case journalButtons.back:

                    if((disciplineId-1) < 0)
                    {

                        discipline = (MasteryDiscipline.disciplines)disciplineMax;

                    }
                    else
                    {

                        discipline = (MasteryDiscipline.disciplines)(disciplineId - 1);

                    }

                    populateContent();

                    return;
            
            }

            base.pressButton(button);

        }

        public override void pressContent()
        {

            openJournal(journalTypes.masteryPage, contentComponents[focus].id);

            return;

        }

        public override void pressCancel()
        {

            return;

        }

        public override void drawContent(SpriteBatch b)
        {

            for (int i = 0; i < contentComponents.Count; i++)
            {

                DrawPath(b, contentComponents[i], i);

            }

        }

        public virtual void DrawPath(SpriteBatch b, ContentComponent contentComponent, int Index)
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

            Vector2 placement = new Vector2(contentComponent.bounds.Left + 24, contentComponent.bounds.Top + 20);

            b.DrawString(Game1.dialogueFont, contentComponent.textParse[0], placement + new Vector2(-1, 1), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.900f);

            b.DrawString(Game1.dialogueFont, contentComponent.textParse[0],placement, contentComponent.textColours[0], 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.901f);

            placement = placement + new Vector2(0, contentComponent.textMeasures[0].Y + 16);

            b.DrawString(Game1.dialogueFont, contentComponent.textParse[1], placement + new Vector2(-1, 1), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, contentComponent.textScales[1] - 0.05f, SpriteEffects.None, 0.900f);

            b.DrawString(Game1.dialogueFont, contentComponent.textParse[1], placement, contentComponent.textColours[1], 0f, Vector2.Zero, contentComponent.textScales[1] - 0.05f, SpriteEffects.None, 0.901f);

            placement = new Vector2(contentComponent.bounds.Left + 24, contentComponent.bounds.Bottom - 20 - 60);

            for (int i = 0; i < contentComponent.textureSources.Count; i++)
            {

                b.Draw(Mod.instance.iconData.masteryTexture, placement, contentComponent.textureSources[i], Color.White, 0f, Vector2.Zero, 3f, 0, 0.901f);

                placement.X += (contentComponent.textureSources[i].Height * 3) + 16;

            }

        }

        public override void drawHover(SpriteBatch b)
        {

            base.drawHover(b);

            if (!browsing)
            {

                return;

            }

            string pathString = contentComponents[focus].id;

            MasteryPath.paths pathId = Enum.Parse<MasteryPath.paths>(pathString);

            MasteryPath pathData = Mod.instance.masteryHandle.paths[pathId];

            int pathLevel = Mod.instance.masteryHandle.PathLevel(pathId);

            float contentHeight = 16;

            // -------------------------------------------------------
            // title

            string titleText = Game1.parseText(pathData.name, Game1.dialogueFont, 604);

            Vector2 titleSize = Game1.dialogueFont.MeasureString(titleText);

            contentHeight += 20 + titleSize.Y * 0.8f;

            // -------------------------------------------------------
            // details

            if (hoverFocus != pathId || hoverData.Count == 0)
            {

                hoverData = new();

                foreach (KeyValuePair<int,MasteryNode.nodes> node in pathData.nodes)
                {

                    if (node.Key > pathLevel)
                    {

                        break;

                    }

                    if (node.Key == pathLevel)
                    {

                        hoverData.Add(StringData.Get(StringData.str.nextLevel) + StringData.colon + " " + Mod.instance.masteryHandle.nodes[node.Value].name);

                        hoverData.Add(Mod.instance.masteryHandle.nodes[node.Value].description);

                        break;

                    }

                    hoverData.Add(StringData.Get(StringData.str.level, new { level = node.Key +1 }) + " " + Mod.instance.masteryHandle.nodes[node.Value].name);

                    hoverData.Add(Mod.instance.masteryHandle.nodes[node.Value].description);

                }

                hoverFocus = pathId;

            }

            contentHeight += 12; // bar

            List<string> detailParse = new();

            List<Vector2> detailSizing = new();

            foreach (string detail in hoverData)
            {

                string detailText = Game1.parseText(detail, Game1.smallFont, 604);

                detailParse.Add(detailText);

                Vector2 detailSize = Game1.smallFont.MeasureString(detailText);
                
                detailSizing.Add(detailSize);

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

            IClickableMenu.drawTextureBox(b, Game1.menuTexture, new Rectangle(0, 256, 60, 60), (int)corner.X, (int)corner.Y, 640, (int)(contentHeight), Color.White, 1f, true, -1f);

            float textPosition = corner.Y + 16;

            float textMargin = corner.X + 16;

            // -------------------------------------------------------
            // title

            b.DrawString(Game1.dialogueFont, titleText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, -1f);

            b.DrawString(Game1.dialogueFont, titleText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, -1.1f);

            textPosition += 8 + titleSize.Y * 0.8f;

            // -------------------------------------------------------
            // details

            DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

            textPosition += 12;

            int d = 0;

            foreach (string detail in hoverData)
            {

                string detailText = detailParse[d];

                Vector2 detailSize = detailSizing[d++];

                b.DrawString(Game1.smallFont, detailText, new Vector2(textMargin, textPosition), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                b.DrawString(Game1.smallFont, detailText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                textPosition += detailSize.Y;

            }

            textPosition += 12;

        }

    }

}
