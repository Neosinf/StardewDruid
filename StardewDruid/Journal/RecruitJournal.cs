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
using StardewDruid.Character;
using StardewDruid.Cast;
using StardewValley.Minigames;

namespace StardewDruid.Journal
{

    public class RecruitJournal : DruidJournal
    {

        public RecruitJournal(string QuestId, int Record) : base(QuestId, Record)
        {


        }

        public override void populateContent()
        {

            type = journalTypes.recruits;

            title = StringData.Strings(StringData.stringkeys.recruits);

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

                    Journal.ContentComponent content = new(ContentComponent.contentTypes.herolist, hero.ToString());

                    content.text[0] = Mod.instance.save.recruits[hero].display;

                    content.textures[0] = CharacterHandle.FindVillager(Mod.instance.save.recruits[hero].name).Sprite.Texture;

                    if (CharacterHandle.RecruitHero(Mod.instance.save.recruits[hero].name))
                    {

                        content.text[0] = CharacterHandle.RecruitTitle(Mod.instance.save.recruits[hero].name, Mod.instance.save.recruits[hero].display);

                        content.icons[0] = IconData.displays.heroes;

                    }

                    contentComponents[start++] = content;

                    continue;

                }

                Journal.ContentComponent blank = new(ContentComponent.contentTypes.herolist, hero.ToString());

                blank.active = false;

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

                [101] = addButton(journalButtons.quests),
                [102] = addButton(journalButtons.effects),
                [103] = addButton(journalButtons.relics),
                [104] = addButton(journalButtons.herbalism),
                [105] = addButton(journalButtons.lore),
                [106] = addButton(journalButtons.transform),
                [107] = addButton(journalButtons.recruits),

                [201] = addButton(journalButtons.clearOne),
                [202] = addButton(journalButtons.clearTwo),
                [203] = addButton(journalButtons.clearThree),
                [204] = addButton(journalButtons.clearFour),

                [301] = addButton(journalButtons.exit),

            };

        }

        public override void activateInterface()
        {

            resetInterface();

            fadeMenu();

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

                if (!Mod.instance.save.recruits.ContainsKey(hero))
                {

                    interfaceComponents[201 + i].active = false;

                }

            }

        }

        public override void pressButton(journalButtons button)
        {

            switch (button)
            {

                case journalButtons.clearOne:

                    ClearHero(CharacterHandle.characters.recruit_one);

                    return;

                case journalButtons.clearTwo:

                    ClearHero(CharacterHandle.characters.recruit_two);

                    return;

                case journalButtons.clearThree:

                    ClearHero(CharacterHandle.characters.recruit_three);

                    return;

                case journalButtons.clearFour:

                    ClearHero(CharacterHandle.characters.recruit_four);

                    return;

                default:

                    base.pressButton(button);

                    break;

            }

        }


        public override void pressContent()
        {

            CharacterHandle.characters type = Enum.Parse<CharacterHandle.characters>(contentComponents[focus].id);

            if (Mod.instance.trackers.ContainsKey(type))
            {

                Game1.playSound(SpellHandle.sounds.ghost.ToString());

                return;

            }

            CharacterHandle.RecruitLoad(type);

            Mod.instance.CastMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.361.3").Tokens(new { name = (Mod.instance.characters[type] as Recruit).villager.Name, }), 0, true);

            exitThisMenu();

        }

        public override void pressCancel()
        {

            CharacterHandle.characters type = Enum.Parse<CharacterHandle.characters>(contentComponents[focus].id);

            if (!Mod.instance.characters.ContainsKey(type))
            {

                Game1.playSound(SpellHandle.sounds.ghost.ToString());

                return;

            }

            if (Mod.instance.characters[type].currentLocation == null)
            {

                Game1.playSound(SpellHandle.sounds.ghost.ToString());

                return;

            }

            RecruitData hero = Mod.instance.save.recruits[type];

            Mod.instance.CastMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.361.4").Tokens(new { name = (Mod.instance.characters[type] as Recruit).villager.Name, }), 0, true);

            CharacterHandle.RecruitRemove(type);

            exitThisMenu();

        }

        public virtual void ClearHero(CharacterHandle.characters type)
        {

            CharacterHandle.RecruitRemove(type);

            populateContent();

            activateInterface();

        }

    }

}
