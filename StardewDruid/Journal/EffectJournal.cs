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


namespace StardewDruid.Journal
{
    public class EffectJournal : DruidJournal
    {

        public int damageLevel;

        public int druidPower;

        public EffectJournal(string QuestId, int Record) : base(QuestId, Record)
        {


        }

        public override void populateContent()
        {

            damageLevel = Mod.instance.CombatDamage();

            druidPower = Mod.instance.PowerLevel;

            type = journalTypes.effects;

            title = StringData.Strings(StringData.stringkeys.grimoire);

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

        public override void drawContent(SpriteBatch b)
        {

            base.drawContent(b);

            b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.druidPower) + druidPower.ToString(), new Vector2(xPositionOnScreen + width - 336 - 1f, yPositionOnScreen + 32 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1.15f, SpriteEffects.None, 0.900f);

            b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.druidPower) + druidPower.ToString(), new Vector2(xPositionOnScreen + width - 336, yPositionOnScreen + 32), new Color(86, 22, 12), 0f, Vector2.Zero, 1.15f, SpriteEffects.None, 0.901f);

            b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.damageLevel) + damageLevel.ToString(), new Vector2(xPositionOnScreen + width - 336 - 1f, yPositionOnScreen + 68 + 1.5f), Microsoft.Xna.Framework.Color.Brown * 0.35f, 0f, Vector2.Zero, 1.15f, SpriteEffects.None, 0.900f);

            b.DrawString(Game1.smallFont, StringData.Strings(StringData.stringkeys.damageLevel) + damageLevel.ToString(), new Vector2(xPositionOnScreen + width - 336, yPositionOnScreen + 68), new Color(86, 22, 12), 0f, Vector2.Zero, 1.15f, SpriteEffects.None, 0.901f);

        }

    }

}
