using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Network;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;

namespace StardewDruid.Journal
{
    public class GuildPage : DruidJournal
    {

        public GuildPage(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
        {

        }

        public override void populateInterface()
        {

            parent = journalTypes.guilds;

            type = journalTypes.guildPage;

            interfaceComponents = new()
            {
                // ------------------------------------------
                [101] = addButton(journalButtons.openOrders),

                [201] = addButton(journalButtons.previous),

                [301] = addButton(journalButtons.exit),

                [302] = addButton(journalButtons.scrollUp),

                [303] = addButton(journalButtons.scrollBar),

                [304] = addButton(journalButtons.scrollDown),

            };

        }

        public override void populateContent()
        {

            ExportGuild.guilds guildId = Enum.Parse<ExportGuild.guilds>(parameters[0]);

            ExportGuild guild = Mod.instance.exportHandle.guilds[guildId];

            GuildRecord record = Mod.instance.save.guilds[guildId];

            int Level = ExportHandle.GuildLevel(guildId);

            // ----------------------------- title

            int textHeight = 48;

            title = guild.name;

            int start = 0;

            // ----------------------------- intro

            contentComponents[start] = new(ContentComponent.contentTypes.text, "introduction");

            contentComponents[start].text[0] = guild.intro;

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

            textHeight += contentComponents[start++].bounds.Height;

            // ------------------------------ instructions

            contentComponents[start] = new(ContentComponent.contentTypes.text, "level");

            int nextlevel = ExportHandle.GuildNext(guildId);

            contentComponents[start].text[0] = StringData.Get(StringData.str.level, new { level = Level });

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

            textHeight += contentComponents[start++].bounds.Height;

            // ------------------------------ relationship

            contentComponents[start] = new(ContentComponent.contentTypes.text, "relationship");

            if (nextlevel == -1)
            {

                contentComponents[start].text[0] = StringData.Get(StringData.str.maxLevel);

            }
            else
            {

                contentComponents[start].text[0] = StringData.Get(StringData.str.relationship) + record.experience + StringData.slash + nextlevel;

            }

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

            textHeight += contentComponents[start++].bounds.Height;

            // ----------------------------- description

            contentComponents[start] = new(ContentComponent.contentTypes.text, "description");

            contentComponents[start].text[0] = guild.description;

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

            textHeight += contentComponents[start++].bounds.Height;

            // ------------------------------ conditional instructions

            for (int i = 0; i < guild.benefits.Count; i++)
            {

                KeyValuePair<int,string> benefit = guild.benefits.ElementAt(i);

                contentComponents[start] = new(ContentComponent.contentTypes.text, i.ToString());

                contentComponents[start].text[0] = StringData.Get(StringData.str.level, new { level = benefit.Key }) + StringData.colon + benefit.Value;

                Microsoft.Xna.Framework.Color benefitColour = Microsoft.Xna.Framework.Color.DarkBlue;

                if (Level < benefit.Key)
                {

                    benefitColour = Microsoft.Xna.Framework.Color.DarkBlue * 0.5f;

                }

                contentComponents[start].textColours[0] = benefitColour;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            textHeight += 48;

            contentBox = new(xPositionOnScreen, yPositionOnScreen + 96, width, textHeight);

        }


        public override void drawContent(SpriteBatch b)
        {

            // preserve current batch rectangle

            Rectangle preserve = b.GraphicsDevice.ScissorRectangle;

            b.End();

            // create new batch for scroll rectangle

            SpriteBatch b2 = b;

            b2.Begin(0, BlendState.AlphaBlend, SamplerState.PointClamp, null, new RasterizerState() { ScissorTestEnable = true }, null, new Matrix?());

            // determine inframe

            Rectangle inframe = new(xPositionOnScreen + 32, yPositionOnScreen + 48, width - 64, height - 96);

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

    }

}
