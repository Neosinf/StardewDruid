using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Journal
{
    public class DistilleryJournal : GoodsJournal
    {

        public DistilleryJournal(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
        {


        }

        public override void prepareContent()
        {

            type = journalTypes.distillery;

            parent = journalTypes.orders;

        }

        public override void populateInterface()
        {

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.openOrders),

                [201] = addButton(journalButtons.previous),

                [202] = addButton(journalButtons.openGuilds),
                [203] = addButton(journalButtons.openGoodsDistillery),
                [204] = addButton(journalButtons.openDistilleryInventory),
                [205] = addButton(journalButtons.openProductionRecent),
                [206] = addButton(journalButtons.openProductionEstimated),

                [207] = addButton(journalButtons.refresh),

                [301] = addButton(journalButtons.exit),

            };

        }

        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.refresh:

                    populateContent();

                    return;

                default:

                    base.pressButton(button);

                    break;

            }

        }
        public override void pressContent()
        {

            if (contentComponents[focus].serial != 2)
            {

                return;

            }

            ExportMachine.machines machineId = Enum.Parse<ExportMachine.machines>(contentComponents[focus].id);

            if (Mod.instance.exportHandle.UpdateMachine(machineId))
            {

                populateContent();

                return;

            }
            else
            {

                Game1.playSound(SpellHandle.Sounds.ghost.ToString());

            }

        }

        public override void drawHover(SpriteBatch b)
        {

            if (interfacing)
            {

                if (interfaceComponents[focus].text != null)
                {

                    drawHoverText(b, interfaceComponents[focus].text);

                }

                return;

            }

            if (!browsing)
            {

                hoverFocus = -1;

                return;

            }

            switch (contentComponents[focus].serial)
            {

                case 1:

                    DrawExportHover(b);

                    break;

                case 2:

                    DrawUpgradeHover(b);

                    break;

                case 3:

                    DrawResourceHover(b);

                    break;

            }

        }

        public override void PrepareExportHover()
        {

            HoverReset();

            string machineId = contentComponents[focus].id;

            ExportMachine.machines export = Enum.Parse<ExportMachine.machines>(machineId);

            ExportMachine machine = Mod.instance.exportHandle.machines[export];

            float contentHeight = 20;

            // -------------------------------------------------------
            // title

            string titleText = Game1.parseText(machine.name, Game1.dialogueFont, 476);

            Vector2 titleSize = Game1.dialogueFont.MeasureString(titleText);

            hoverTitle = titleText;

            hoverTitleY = titleSize.Y;

            contentHeight += 4 + titleSize.Y;

            // -------------------------------------------------------
            // description

            string descriptionText = Game1.parseText(machine.description, Game1.smallFont, 476);

            Vector2 descriptionSize = Game1.smallFont.MeasureString(descriptionText);

            hoverDescription = descriptionText;

            hoverDescriptionSize = descriptionSize.Y;

            contentHeight += 12 + descriptionSize.Y;

            // -------------------------------------------------------
            // machine level

            contentHeight += 12; // bar

            int Level = 1;

            if (Mod.instance.save.machines.ContainsKey(export))
            {

                Level = Mod.instance.save.machines[export].level;

            }

            string levelText = StringData.Get(StringData.str.level, new { level = Level });

            string levelParse = Game1.parseText(levelText, Game1.smallFont, 476);

            Vector2 levelSize = Game1.smallFont.MeasureString(levelParse);

            contentHeight += levelSize.Y;

            hoverDetails.Add(levelParse);

            contentHeight += 12;

            // -------------------------------------------------------
            // machine stat

            int machineStat = Mod.instance.exportHandle.MachineStat(export);

            string statText = machineStat + machine.technical;

            string statParse = Game1.parseText(statText, Game1.smallFont, 476);

            Vector2 statSize = Game1.smallFont.MeasureString(statParse);

            contentHeight += statSize.Y;

            hoverDetails.Add(statParse);

            contentHeight += 12;

            // -------------------------------------------------------

            contentHeight += 20; // bottom

            hoverHeight = (int)contentHeight;

        }

        public void PrepareUpgradeHover()
        {

            HoverReset();

            string machineId = contentComponents[focus].id;

            ExportMachine.machines export = Enum.Parse<ExportMachine.machines>(machineId);

            ExportMachine machine = Mod.instance.exportHandle.machines[export];

            float contentHeight = 20;

            // -------------------------------------------------------
            // title

            int machineLevel = 1;

            if (Mod.instance.save.machines.ContainsKey(export))
            {

                machineLevel = Mod.instance.save.machines[export].level;

            }

            string titleText = StringData.Get(StringData.str.upgradeLevel, new { value = machine.name, level = machineLevel + 1 });

            string titleParse = Game1.parseText(titleText, Game1.smallFont, 476);

            Vector2 titleSize = Game1.smallFont.MeasureString(titleParse);

            hoverTitle = titleParse;

            hoverTitleY = titleSize.Y;

            contentHeight += 4 + titleSize.Y;

            hoverDetails.Clear();

            hoverSources.Clear();

            // -------------------------------------------------------
            // labour

            int existingLabour = 0;

            ExportResource.resources labourType = machine.labour;

            if (Mod.instance.save.resources.ContainsKey(labourType))
            {

                existingLabour = Mod.instance.save.resources[labourType];

            }

            int requiredLabour = machine.hireage[machineLevel];

            hoverDetails.Add(Mod.instance.exportHandle.resources[labourType].name);

            hoverDetails.Add(existingLabour + " (" + requiredLabour + ")");

            hoverSources[0] = IconData.DisplayRectangle(ExportResource.ResourceDisplay(labourType));

            contentHeight += 56; // resource 1

            // -------------------------------------------------------
            // material

            int existingMaterial = 0;

            if (Mod.instance.save.resources.ContainsKey(ExportResource.resources.materials))
            {

                existingMaterial = Mod.instance.save.resources[ExportResource.resources.materials];

            }

            int requiredMaterial = machine.materials[machineLevel];

            hoverDetails.Add(Mod.instance.exportHandle.resources[ExportResource.resources.materials].name);

            hoverDetails.Add(existingMaterial + " (" + requiredMaterial + ")");

            hoverSources[1] = IconData.DisplayRectangle(ExportResource.ResourceDisplay(ExportResource.resources.materials));

            contentHeight += 56; // resource 2

            // -------------------------------------------------------

            contentHeight += 20; // bottom

            hoverHeight = (int)contentHeight;

        }

        public void DrawUpgradeHover(SpriteBatch b)
        {

            // --------------------------------------------------------
            // Upgrade Button

            if (hoverFocus != focus)
            {

                PrepareUpgradeHover();

            }

            // -------------------------------------------------------
            // texturebox

            Rectangle boxBounds = drawHoverBox(b, 512, hoverHeight);

            float textPosition = boxBounds.Y + 20;

            float textMargin = boxBounds.X + 16;

            // -------------------------------------------------------
            // title

            b.DrawString(Game1.smallFont, hoverTitle, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, hoverTitle, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1, SpriteEffects.None, -1.1f);

            textPosition += 4 + hoverTitleY;

            // -------------------------------------------------------

            DrawSeparator(b, (int)textMargin - 4, (int)textPosition);

            textPosition += 12;

            // -------------------------------------------------------
            // labour

            b.Draw(Mod.instance.iconData.displayTexture, new Vector2(textMargin, textPosition), hoverSources[0], Color.White, 0f, Vector2.Zero, 3f, 0, 1f);

            b.DrawString(Game1.smallFont, hoverDetails[0], new Vector2(textMargin + 56, textPosition + 8), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, hoverDetails[0], new Vector2(textMargin + 56 - 1.5f, textPosition + 8 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

            Vector2 labourParse = Game1.smallFont.MeasureString(hoverDetails[1]);

            b.DrawString(Game1.smallFont, hoverDetails[1], new Vector2(textMargin + 476 - labourParse.X, textPosition + 8), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, hoverDetails[1], new Vector2(textMargin + 476 - labourParse.X - 1.5f, textPosition + 8 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

            textPosition += 56;

            // -------------------------------------------------------
            // material

            b.Draw(Mod.instance.iconData.displayTexture, new Vector2(textMargin, textPosition), hoverSources[1], Color.White, 0f, Vector2.Zero, 3f, 0, 1f);

            b.DrawString(Game1.smallFont, hoverDetails[2], new Vector2(textMargin + 56, textPosition + 8), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, hoverDetails[2], new Vector2(textMargin + 56 - 1.5f, textPosition + 8 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

            Vector2 materialParse = Game1.smallFont.MeasureString(hoverDetails[3]);

            b.DrawString(Game1.smallFont, hoverDetails[3], new Vector2(textMargin + 476 - materialParse.X, textPosition + 8), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, hoverDetails[3], new Vector2(textMargin + 476 - materialParse.X - 1.5f, textPosition + 8 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

        }


    }

}
