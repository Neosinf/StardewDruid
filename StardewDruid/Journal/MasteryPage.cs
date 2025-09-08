using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Journal
{
    public class MasteryPage : DruidJournal
    {

        public MasteryPath.paths path;

        public MasteryDiscipline.disciplines discipline;

        public bool maxLevel;

        public int requiredExp;

        public int availableExp;

        public int usedExp;

        public MasteryPage(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
        {

        }

        public override void populateInterface()
        {

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.openMasteries),

                [102] = addButton(journalButtons.levelUp),


                [201] = addButton(journalButtons.previous),


                [301] = addButton(journalButtons.exit),

                [302] = addButton(journalButtons.scrollUp),

                [303] = addButton(journalButtons.scrollBar),

                [304] = addButton(journalButtons.scrollDown),

            };

        }

        public override void activateInterface()
        {
            
            base.activateInterface();

            if (requiredExp > availableExp || maxLevel)
            {

                interfaceComponents[interfaceRegistry[journalButtons.levelUp]].fade = 0.7f;

            }

        }

        public override void prepareContent()
        {

            path = MasteryPath.paths.alchemy;

            if (parameters.Count > 0)
            {

                path = Enum.Parse<MasteryPath.paths>(parameters[0]);

            }

        }

        public override void populateContent()
        {

            scrollHeight = 468;

            usedExp = Mod.instance.save.masteries.Values.Sum();

            availableExp = Mod.instance.save.experience - usedExp;

            int pathLevel = Mod.instance.masteryHandle.PathLevel(path);

            // ----------------------------- title

            int textHeight = 48;

            int start = 0;

            MasteryPath pathData = Mod.instance.masteryHandle.paths[path];

            discipline = pathData.discipline;

            title = pathData.name;

            // ----------------------------- description

            contentComponents[start] = new(ContentComponent.contentTypes.text, "description");

            contentComponents[start].text[0] = pathData.description;

            contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width-128, 0);

            textHeight += contentComponents[start++].bounds.Height;

            // ------------------------------ details

            maxLevel = true;

            foreach (KeyValuePair<int, MasteryNode.nodes> node in pathData.nodes)
            {

                MasteryNode nodeData = Mod.instance.masteryHandle.nodes[node.Value];

                string nodeTitle = StringData.Get(StringData.str.level, new { level = node.Key+1 }) + StringData.colon + nodeData.name;

                if (node.Key == pathLevel)
                {

                    nodeTitle = StringData.Get(StringData.str.nextLevel) + StringData.colon + nodeData.name;

                }
                
                // node title

                contentComponents[start] = new(ContentComponent.contentTypes.text, node.Value.ToString());

                contentComponents[start].text[0] = nodeTitle;

                contentComponents[start].textScales[0] = 0.9f;

                if (node.Key == pathLevel)
                {

                    contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkSlateBlue;

                    maxLevel = false;

                    requiredExp = Mod.instance.masteryHandle.RequiredExperience(path, node.Key + 1);

                }
                else if (node.Key > pathLevel)
                {

                    contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.SlateGray;

                }

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

                // node description

                contentComponents[start] = new(ContentComponent.contentTypes.text, node.Value.ToString() + "_description");

                contentComponents[start].text[0] = nodeData.description;
                
                contentComponents[start].textScales[0] = 0.9f;

                if (node.Key == pathLevel)
                {

                    contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.DarkSlateBlue;

                }
                else if (node.Key > pathLevel)
                {

                    contentComponents[start].textColours[0] = Microsoft.Xna.Framework.Color.SlateGray;

                }

                contentComponents[start].setBounds(0, xPositionOnScreen + 64, yPositionOnScreen + textHeight, width - 128, 0);

                textHeight += contentComponents[start++].bounds.Height;

            }

            textHeight += 48;

            contentBox = new(xPositionOnScreen, yPositionOnScreen + 96, width, textHeight);

        }


        public override void pressButton(journalButtons button)
        {

            switch (button)
            {
                
                case journalButtons.previous:

                    openJournal(journalTypes.masteryOverview, contentComponents[focus].id);

                    return;

                case journalButtons.levelUp:

                    if (requiredExp > availableExp || maxLevel)
                    {

                        Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                        break;

                    }

                    Mod.instance.masteryHandle.LevelUp(path);

                    populateContent();

                    activateInterface();

                    return;

                default:

                    base.pressButton(button);

                    break;

            }

        }

        public override void drawInterface(SpriteBatch b)
        {
            
            base.drawInterface(b);

            // -----------------------------------

            string requiredLabel = StringData.Get(StringData.str.maxLevel);

            if (!maxLevel)
            {

                requiredLabel = StringData.Get(StringData.str.nextLevel) + StringData.colon + " " + Math.Min(availableExp, requiredExp) + StringData.slash + requiredExp;

            }

            Microsoft.Xna.Framework.Color requiredColour = ContentComponent.defaultColour;

            if (requiredExp > availableExp)
            {

                requiredColour = Color.DarkRed;

            }

            Vector2 requiredPosition = new Vector2(xPositionOnScreen + 64, yPositionOnScreen + height - 72);

            b.DrawString(Game1.dialogueFont, requiredLabel, requiredPosition + new Vector2(-1f, 2f), Color.Brown * 0.25f, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.88f);

            b.DrawString(Game1.dialogueFont, requiredLabel, requiredPosition, requiredColour, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.88f);

            // -----------------------------------

            string totalLabel = StringData.Get(StringData.str.experience) + usedExp + StringData.slash + Mod.instance.save.experience;

            Vector2 totalPosition = new Vector2(xPositionOnScreen + (width / 2) + 80, yPositionOnScreen + height - 72);

            b.DrawString(Game1.dialogueFont, totalLabel, totalPosition + new Vector2(-1f, 2f), Color.Brown * 0.25f, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.88f);

            b.DrawString(Game1.dialogueFont, totalLabel, totalPosition, ContentComponent.defaultColour, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.88f);


        }

    }

}
