using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Journal
{
    public class RelicPage : QuestPage
    {

        public RelicPage(string QuestId, int Record) : base(QuestId, Record) 
        {

        }

        public override void populateInterface()
        {

            parentJournal = journalTypes.relics;

            type = journalTypes.relicPage;

            interfaceComponents = new()
            {

                [201] = addButton(journalButtons.back),

                [301] = addButton(journalButtons.exit),

                [302] = addButton(journalButtons.scrollUp),

                [303] = addButton(journalButtons.scrollBar),

                [304] = addButton(journalButtons.scrollDown),

            };

        }

        public override void populateContent()
        {

            string relicId = journalId;

            // ----------------------------- title

            int textHeight = 48;

            title = Mod.instance.relicHandle.reliquary[relicId].title;

            int start = 0;

            // ----------------------------- description

            contentComponents[start] = new(ContentComponent.contentTypes.text, "description");

            contentComponents[start].text[0] = Mod.instance.relicHandle.reliquary[relicId].description;

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

            textHeight += contentComponents[start++].bounds.Height;


            // ------------------------------ conditional instructions

            for (int i = 0; i < Mod.instance.relicHandle.reliquary[relicId].narrative.Count; i++)
            {

                string detail = Mod.instance.relicHandle.reliquary[relicId].narrative[i];

                contentComponents[start] = new(ContentComponent.contentTypes.text, i.ToString());

                contentComponents[start].text[0] = detail;

                contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkBlue;

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            textHeight += 48;

            contentBox = new(xPositionOnScreen, yPositionOnScreen + 96, width, textHeight);

        }


        public override void activateInterface()
        {

            resetInterface();

            scrolled = 0;

            if (contentBox.Height < 512)
            {

                interfaceComponents[302].active = false;

                interfaceComponents[303].active = false;

                interfaceComponents[304].active = false;

            }
            else
            {

                scrollId = 303;

            }

        }
    }

}
