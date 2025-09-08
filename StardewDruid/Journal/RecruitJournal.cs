using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley.Menus;
using System.Diagnostics.Metrics;
using StardewDruid.Cast;
using StardewValley.Minigames;
using StardewDruid.Handle;

namespace StardewDruid.Journal
{

    public class RecruitJournal : DruidJournal
    {

        public RecruitJournal(journalTypes Type, List<string> Parameters) : base(Type, Parameters)
        {


        }

        public override void populateContent()
        {

            type = journalTypes.ledger;

            pagination = 4;

            contentComponents = new();

            int start = 0;

            List<CharacterHandle.characters> slots = new()
            {

                CharacterHandle.characters.recruit_one,
                CharacterHandle.characters.recruit_two,
                CharacterHandle.characters.recruit_three,
                CharacterHandle.characters.recruit_four,

            };

            for (int i = 0; i < slots.Count; i++)
            {

                CharacterHandle.characters hero = slots[i];

                if (Mod.instance.save.recruits.ContainsKey(hero))
                {

                    NPC original = CharacterHandle.FindVillager(Mod.instance.save.recruits[hero].name);

                    Journal.ContentComponent content = new(ContentComponent.contentTypes.herolist, hero.ToString());

                    content.text[0] = Mod.instance.save.recruits[hero].display;

                    if (original != null)
                    {

                        content.textures[0] = original.Sprite.Texture;

                        content.textureSources[0] = new Rectangle(0, 0, 16, 24);

                    }

                    if (RecruitHandle.RecruitHero(Mod.instance.save.recruits[hero].name))
                    {
                        string name = Mod.instance.save.recruits[hero].name;

                        content.text[0] = Mod.instance.save.recruits[hero].display;

                        content.text[1] = RecruitHandle.RecruitTitle(name);

                        content.textures[0] = Mod.instance.iconData.displayTexture;

                        content.textureSources[0] = IconData.DisplayRectangle(IconData.displays.heroes);

                        content.text[2] = RecruitHandle.RecruitLevel(Mod.instance.save.recruits[hero].level);

                    }

                    contentComponents[start++] = content;

                    continue;

                }

                Journal.ContentComponent blank = new(ContentComponent.contentTypes.herolist, hero.ToString())
                {
                    active = false
                };

                contentComponents[start++] = blank;

            }

            if (record >= contentComponents.Count)
            {

                record = 0;

            }

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                component.Value.setBounds(component.Key % pagination, xPositionOnScreen, yPositionOnScreen, width, height);

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

                [301] = addButton(journalButtons.exit),

            };

        }

        public override void pressContent()
        {

            openJournal(journalTypes.companion,contentComponents[focus].id);

        }

    }

}
