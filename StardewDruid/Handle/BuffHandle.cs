using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using StardewValley.Menus;
using StardewDruid.Data;
using StardewValley.ItemTypeDefinitions;

namespace StardewDruid.Handle
{
    public class BuffHandle : Buff
    {

        public enum buffTypes
        {
            none,
            alignment,
            vigor,
            celerity,

            imbuement,
            amorous,
            macerari,
            rapidfire,

            concussion,
            jumper,
            feline,
            sanctified,

            capture,
            spellcatch,

        }

        public Dictionary<buffTypes, BuffDetail> details = new();

        public Dictionary<buffTypes, BuffApplied> applied = new();

        public BuffHandle()
        : base(
                184652.ToString(),
                source: Mod.instance.Helper.Translation.Get("HerbalData.1313"),
                displaySource: Mod.instance.Helper.Translation.Get("HerbalData.1314"),
                duration: ENDLESS,
                iconTexture: Mod.instance.iconData.displayTexture,
                iconSheetIndex: 5,
                displayName: StringData.Get(StringData.str.herbalBuffDescription)
                )
        {

            details = BuffData.BuffList();

        }

        public bool ApplyBuff(ApothecaryItem itemData)
        {

            buffTypes buffing = itemData.buff;

            if (!applied.ContainsKey(buffing))
            {

                applied[buffing] = new BuffApplied();

            }
            else
            if (applied[buffing].level > itemData.level)
            {

                return false;

            }

            applied[buffing].counter += itemData.duration;

            applied[buffing].level = itemData.level;

            int skillLevel = (int)(itemData.level / 2);

            switch (buffing)
            {

                case buffTypes.alignment:

                    effects.MagneticRadius.Set(itemData.level * 32);

                    effects.FarmingLevel.Set(skillLevel);

                    effects.ForagingLevel.Set(skillLevel);

                    Game1.player.buffs.Dirty = true;

                    break;

                case buffTypes.vigor:

                    effects.MiningLevel.Set(skillLevel);

                    effects.CombatLevel.Set(skillLevel);

                    Game1.player.buffs.Dirty = true;

                    break;

                case buffTypes.celerity:

                    effects.Speed.Set(0.25f * itemData.level);

                    effects.FishingLevel.Set(skillLevel);

                    Game1.player.buffs.Dirty = true;

                    break;
                    
            }

            return true;

        }

        public void CheckBuff()
        {

            for (int i = applied.Count - 1; i >= 0; i--)
            {

                KeyValuePair<buffTypes, BuffApplied> herbBuff = applied.ElementAt(i);

                applied[herbBuff.Key].counter -= 1;

                if (applied[herbBuff.Key].counter <= 0)
                {

                    DismissBuff(herbBuff.Key);

                    applied.Remove(herbBuff.Key);

                }

            }

            if (applied.Count <= 0)
            {

                if (Game1.player.buffs.IsApplied(Cast.Rite.buffIdHerbal))
                {

                    ClearBuff();

                }

                return;

            }

            if (Game1.player.buffs.IsApplied(Cast.Rite.buffIdHerbal))
            {

                return;

            }

            Game1.player.buffs.Apply(this);

        }

        public void DismissBuff(buffTypes herbal)
        {

            switch (herbal)
            {


                case buffTypes.alignment:

                    effects.MagneticRadius.Set(0f);

                    effects.FarmingLevel.Set(0f);

                    effects.ForagingLevel.Set(0f);

                    Game1.player.buffs.Dirty = true;

                    break;

                case buffTypes.vigor:

                    effects.MiningLevel.Set(0f);

                    effects.CombatLevel.Set(0f);

                    Game1.player.buffs.Dirty = true;

                    break;

                case buffTypes.celerity:

                    effects.Speed.Set(0f);

                    effects.FishingLevel.Set(0f);

                    Game1.player.buffs.Dirty = true;

                    break;

            }

        }

        public void draw(SpriteBatch b)
        {

            List<string> description = new();

            for (int i = 0; i < applied.Count; i++)
            {

                KeyValuePair<buffTypes, BuffApplied> herbBuff = applied.ElementAt(i);

                string level = StringData.colon;

                if (herbBuff.Value.level > 1)
                {

                    switch (herbBuff.Value.level)
                    {

                        case 2:
                            level = " " + Mod.instance.Helper.Translation.Get("CharacterHandle.377.7") + StringData.colon;
                                break;

                        case 3:
                            level = " " + Mod.instance.Helper.Translation.Get("CharacterHandle.377.8") + StringData.colon;
                            break;

                        case 4:
                            level = " " + Mod.instance.Helper.Translation.Get("CharacterHandle.377.9") + StringData.colon;
                            break;

                        case 5:
                            level = " " + Mod.instance.Helper.Translation.Get("CharacterHandle.377.10") + StringData.colon;
                            break;


                    }

                }

                string expire = Math.Round(herbBuff.Value.counter * 0.01,2).ToString("F2");

                BuffDetail buffData = details[herbBuff.Key];

                description.Add(buffData.name + level + expire);

                description.Add(buffData.description);

            }

            float contentHeight = 16;

            // -------------------------------------------------------
            // title

            string titleText = Game1.parseText(Mod.instance.Helper.Translation.Get("HerbalData.373.7"), Game1.smallFont, 476);

            Vector2 titleSize = Game1.smallFont.MeasureString(titleText) * 1.25f;

            contentHeight += 24 + titleSize.Y;
            
            if (description.Count > 0)
            {

                foreach (string detail in description)
                {

                    string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                    Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                    contentHeight += detailSize.Y;

                }

                contentHeight += 24;

            }

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

            IClickableMenu.drawTextureBox(b, Game1.menuTexture, new Rectangle(0, 256, 60, 60), (int)corner.X, (int)corner.Y, 512, (int)contentHeight, Color.White, 1f, true, -1f);

            float textPosition = corner.Y + 16;

            float textMargin = corner.X + 16;

            // -------------------------------------------------------
            // title

            b.DrawString(Game1.smallFont, titleText, new Vector2(textMargin, textPosition), Game1.textColor, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1f);

            b.DrawString(Game1.smallFont, titleText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Color.Brown * 0.35f, 0f, Vector2.Zero, 1.25f, SpriteEffects.None, -1.1f);

            textPosition += 8 + titleSize.Y;

            //textPosition += 12;

            // -------------------------------------------------------
            // details

            if (description.Count > 0)
            {

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 488, 2), new Microsoft.Xna.Framework.Color(167, 81, 37));

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 488, 3), new Microsoft.Xna.Framework.Color(246, 146, 30));

                textPosition += 12;

                foreach (string detail in description)
                {

                    string detailText = Game1.parseText(detail, Game1.smallFont, 476);

                    Vector2 detailSize = Game1.smallFont.MeasureString(detailText);

                    b.DrawString(Game1.smallFont, detailText, new Vector2(textMargin, textPosition), Game1.textColor * 0.9f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1f);

                    b.DrawString(Game1.smallFont, detailText, new Vector2(textMargin - 1.5f, textPosition + 1.5f), Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, -1.1f);

                    textPosition += detailSize.Y;

                }

                textPosition += 12;

            }

        }

        public void ClearBuff()
        {

            Game1.player.buffs.Remove(Cast.Rite.buffIdHerbal);

        }

        public void RemoveBuffs()
        {

            Game1.player.buffs.Remove(Cast.Rite.buffIdHerbal);

            applied.Clear();

        }

        public void HoverBuff(SpriteBatch b)
        {

            if (Game1.buffsDisplay.hoverText.Contains(StringData.Get(StringData.str.herbalBuffDescription)))
            {

                if (Game1.buffsDisplay.isWithinBounds(Game1.getOldMouseX(), Game1.getOldMouseY()))
                {

                    draw(b);

                    return;

                }

            }

        }

    }

    public class BuffApplied
    {

        public BuffHandle.buffTypes buff = BuffHandle.buffTypes.none;

        public int counter = 0;

        public int level = 0;

    }

}
