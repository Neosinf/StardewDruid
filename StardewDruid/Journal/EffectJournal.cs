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

        public override void populateContent()
        {

            type = journalTypes.effects;

            title = DialogueData.Strings(DialogueData.stringkeys.grimoire);

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

        public override void activateInterface()
        {
            base.activateInterface();

            interfaceComponents[105].active = false;

        }

        public override void pressContent()
        {

            openJournal(journalTypes.effectPage, contentComponents[focus].id, focus);

        }

    }

}
