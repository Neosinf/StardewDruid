using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Journal
{
    public class EffectJournal : DruidJournal
    {

        public EffectJournal(string QuestId, int Record) : base(QuestId, Record)
        {


        }
        public override void populateInterface()
        {

            type = journalTypes.effects;

            title = DialogueData.Strings(DialogueData.stringkeys.grimoire);

            interfaceComponents = new()
            {

                [101] = addButton(journalButtons.quests),
                [102] = addButton(journalButtons.effects),
                [103] = addButton(journalButtons.relics),
                [104] = addButton(journalButtons.herbalism),
                [105] = addButton(journalButtons.lore),
                [106] = addButton(journalButtons.transform),

                [108] = addButton(journalButtons.active),
                [109] = addButton(journalButtons.reverse),

                [201] = addButton(journalButtons.back),
                [202] = addButton(journalButtons.start),

                [301] = addButton(journalButtons.exit),

                [305] = addButton(journalButtons.end),
                [306] = addButton(journalButtons.forward),

            };

        }

        public override void populateContent()
        {

            pagination = 6;

            contentComponents = Mod.instance.questHandle.JournalEffects();

            if (record >= contentComponents.Count)
            {

                record = 0;

            }

            foreach (KeyValuePair<int, ContentComponent> component in contentComponents)
            {

                component.Value.setBounds(component.Key % pagination, xPositionOnScreen, yPositionOnScreen, width, height);

            }

        }

        public override void pressContent()
        {

            openJournal(journalTypes.effectPage, contentComponents[focus].id, focus);

        }

    }

}
