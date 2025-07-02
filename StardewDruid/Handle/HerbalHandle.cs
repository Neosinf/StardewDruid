
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Dialogue;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using StardewDruid.Journal;
using StardewValley.Menus;
using StardewDruid.Location.Druid;
using StardewValley.Objects;
using StardewDruid.Data;

namespace StardewDruid.Handle
{
    public class HerbalHandle
    {

        public enum herbals
        {

            none,

            // potions

            ligna,
            melius_ligna,
            satius_ligna,
            magnus_ligna,
            optimus_ligna,
            impes,
            melius_impes,
            satius_impes,
            magnus_impes,
            optimus_impes,
            celeri,
            melius_celeri,
            satius_celeri,
            magnus_celeri,
            optimus_celeri,

            // special
            faeth,
            aether,

            // powders
            coruscant,

            imbus,
            amori,
            donis,
            rapidus,

            // powders 2
            voil,

            concutere,
            jumere,
            felis,
            sanctus,

            // balls
            captis,
            ferrum_captis,
            aurum_captis,
            diamas_captis,
            capesso,

            // omens
            omen_feather,
            omen_tuft,
            omen_shell,
            omen_tusk,
            omen_nest,
            omen_glass,
            omen_down,
            omen_coral,
            omen_bloom,

            // trophies
            trophy_shroom,
            trophy_eye,
            trophy_pumpkin,
            trophy_pearl,
            trophy_tooth,
            trophy_spiral,
            trophy_spike,
            trophy_seed,
            trophy_dragon

        }

        public Dictionary<string, Herbal> herbalism = new();

        public Dictionary<herbals, int> orders = new();

        public HerbalBuff buff = new();

        public Dictionary<herbals, List<string>> titles = new()
        {
            [herbals.ligna] = new() {
                Mod.instance.Helper.Translation.Get("HerbalData.1"),
                Mod.instance.Helper.Translation.Get("HerbalData.2"),
            },
            [herbals.impes] = new() {
                Mod.instance.Helper.Translation.Get("HerbalData.3"),
                Mod.instance.Helper.Translation.Get("HerbalData.4"),
            },
            [herbals.celeri] = new() {
                Mod.instance.Helper.Translation.Get("HerbalData.5"),
                Mod.instance.Helper.Translation.Get("HerbalData.6"),
            },
            [herbals.faeth] = new() {
                Mod.instance.Helper.Translation.Get("HerbalData.7"),
                Mod.instance.Helper.Translation.Get("HerbalData.8"),
            },
        };

        public Dictionary<herbals, IconData.schemes> schemes = new()
        {
            [herbals.ligna] = IconData.schemes.herbal_ligna,
            [herbals.impes] = IconData.schemes.herbal_impes,
            [herbals.celeri] = IconData.schemes.herbal_celeri,
            [herbals.faeth] = IconData.schemes.herbal_faeth,
            [herbals.aether] = IconData.schemes.herbal_aether,
            [herbals.voil] = IconData.schemes.herbal_voil,

        };

        public Dictionary<herbals, List<herbals>> lines = new()
        {
            [herbals.ligna] = new() {
                herbals.ligna,
                herbals.melius_ligna,
                herbals.satius_ligna,
                herbals.magnus_ligna,
                herbals.optimus_ligna,
            },
            [herbals.impes] = new() {
                herbals.impes,
                herbals.melius_impes,
                herbals.satius_impes,
                herbals.magnus_impes,
                herbals.optimus_impes,
            },
            [herbals.celeri] = new() {
                herbals.celeri,
                herbals.melius_celeri,
                herbals.satius_celeri,
                herbals.magnus_celeri,
                herbals.optimus_celeri,
            },

        };

        public List<herbals> herbalLayout = new()
        {

            herbals.ligna,
            herbals.melius_ligna,
            herbals.satius_ligna,
            herbals.magnus_ligna,
            herbals.optimus_ligna,

            herbals.faeth,

            herbals.impes,
            herbals.melius_impes,
            herbals.satius_impes,
            herbals.magnus_impes,
            herbals.optimus_impes,

            herbals.aether,

            herbals.celeri,
            herbals.melius_celeri,
            herbals.satius_celeri,
            herbals.magnus_celeri,
            herbals.optimus_celeri,

            herbals.none,

        };

        public Dictionary<herbals, List<herbals>> bomblines = new()
        {
            [herbals.coruscant] = new() {
                herbals.imbus,
                herbals.amori,
                herbals.donis,
                herbals.rapidus,
            },
            [herbals.voil] = new() {
                herbals.concutere,
                herbals.jumere,
                herbals.felis,
                herbals.sanctus,
            },
            [herbals.captis] = new() {
                herbals.captis,
                herbals.ferrum_captis,
                herbals.aurum_captis,
                herbals.diamas_captis,
                herbals.capesso,
            },

        };

        public List<herbals> bombLayout = new()
        {

            herbals.imbus,
            herbals.amori,
            herbals.donis,
            herbals.rapidus,
            herbals.none,
            herbals.coruscant,

            herbals.concutere,
            herbals.jumere,
            herbals.felis,
            herbals.sanctus,
            herbals.none,
            herbals.voil,

            herbals.captis,
            herbals.ferrum_captis,
            herbals.aurum_captis,
            herbals.diamas_captis,
            herbals.capesso,
            herbals.none,

        };

        public Dictionary<herbals, List<herbals>> omenlines = new()
        {
            [herbals.omen_feather] = new() {
                herbals.omen_feather,
                herbals.omen_tuft,
                herbals.omen_shell,
                herbals.omen_tusk,
                herbals.omen_nest,
                herbals.omen_glass,
                herbals.omen_down,
                herbals.omen_coral,
                herbals.omen_bloom,
            },
            [herbals.trophy_shroom] = new() {
                herbals.trophy_shroom,
                herbals.trophy_eye,
                herbals.trophy_pumpkin,
                herbals.trophy_pearl,
                herbals.trophy_tooth,
                herbals.trophy_spiral,
                herbals.trophy_spike,
                herbals.trophy_seed,
                herbals.trophy_dragon
            },

        };

        public List<herbals> omenLayout = new()
        {

            // omens
            herbals.omen_feather,
            herbals.omen_tuft,
            herbals.omen_shell,
            herbals.omen_tusk,
            herbals.omen_nest,
            herbals.omen_glass,
            herbals.omen_down,
            herbals.omen_coral,
            herbals.omen_bloom,

            // trophies
            herbals.trophy_shroom,
            herbals.trophy_eye,
            herbals.trophy_pumpkin,
            herbals.trophy_pearl,
            herbals.trophy_tooth,
            herbals.trophy_spiral,
            herbals.trophy_spike,
            herbals.trophy_seed,
            herbals.trophy_dragon

        };

        public double consumeBuffer;

        public HerbalHandle()
        {

        }

        public void LoadHerbals()
        {

            herbalism = HerbalData.HerbalList();

        }

        public int MaxHerbal()
        {

            if (RelicData.HasRelic(IconData.relics.herbalism_gauge))
            {

                return 4;

            }
            if (RelicData.HasRelic(IconData.relics.herbalism_still))
            {

                return 3;

            }
            if (RelicData.HasRelic(IconData.relics.herbalism_pan))
            {

                return 2;

            }
            if (RelicData.HasRelic(IconData.relics.herbalism_mortar))
            {

                return 1;

            }

            return -1;

        }

        public Dictionary<int, ContentComponent> JournalHerbals()
        {

            Dictionary<int, ContentComponent> journal = new();

            int max = MaxHerbal();

            int start = 0;

            foreach (herbals herbal in herbalLayout)
            {

                string key = herbal.ToString();

                bool empty = false;

                if(herbal == herbals.none)
                {

                    empty = true;

                }
                else
                if (herbalism[key].type == Herbal.herbalType.resource)
                {

                    switch (herbal)
                    {

                        case herbals.aether:

                            if (!RelicData.HasRelic(IconData.relics.herbalism_gauge))
                            {

                                empty = true;

                            }

                            break;

                        case herbals.faeth:

                            if (!RelicData.HasRelic(IconData.relics.herbalism_crucible))
                            {

                                empty = true;

                            }

                            break;

                        case herbals.voil:

                            if (!RelicData.HasRelic(IconData.relics.herbalism_crucible))
                            {

                                empty = true;

                            }

                            break;


                    }

                }
                else if (herbalism[key].level > max)
                {

                    empty = true;

                }

                if (empty)
                {

                    ContentComponent blank = new(ContentComponent.contentTypes.potion, key, false);

                    journal[start++] = blank;

                    ContentComponent blankToggle = new(ContentComponent.contentTypes.toggleleft, key, false);

                    journal[start++] = blankToggle;

                    ContentComponent blankToggleTwo = new(ContentComponent.contentTypes.toggleright, "none", false);

                    journal[start++] = blankToggleTwo;

                    continue;
                }

                herbalism[key].craftable = -1;

                herbalism[key].amounts.Clear();

                herbalism[key].potions.Clear();

                if (herbalism[key].stamina > 0)
                {

                    float difficulty = 1.6f - (Mod.instance.ModDifficulty() * 0.1f);

                    int staminaGain = (int)(herbalism[key].stamina * difficulty);

                    int healthGain = (int)(herbalism[key].health * difficulty);

                    herbalism[key].staminaReadout = Mod.instance.Helper.Translation.Get("HerbalData.327.1").Tokens(new { stamina = staminaGain.ToString(), health = healthGain.ToString() });

                }

                // =============================================================== potion button

                ContentComponent content = new(ContentComponent.contentTypes.potion, key);

                int amount = GetHerbalism(herbal);

                string amountString = amount.ToString();

                content.text[0] = amount.ToString();

                if (amount == 0)
                {

                    content.textureSources[0] = IconData.PotionRectangles(herbalism[key].grayed);

                }
                else
                {

                    content.textureSources[0] = IconData.PotionRectangles(herbalism[key].display);

                }

                switch (BuffStatus(herbalism[key]))
                {

                    case 2:

                        content.textureSources[1] = new();

                        break;

                }

                journal[start++] = content;

                // =============================================================== active button

                ContentComponent toggle = new(ContentComponent.contentTypes.toggleleft, key);

                IconData.displays flag = IconData.displays.complete;

                StringData.stringkeys hovertext = StringData.stringkeys.acEnabled;

                if (Mod.instance.save.potions.ContainsKey(herbal))
                {

                    switch (Mod.instance.save.potions[herbal])
                    {

                        case 0: // Consumed Disabled

                            flag = IconData.displays.exit;

                            hovertext = StringData.stringkeys.acDisabled;

                            break;

                        case 1: // Normal

                            flag = IconData.displays.complete;

                            break;

                        case 2: // Priority

                            flag = IconData.displays.flag;

                            hovertext = StringData.stringkeys.acPriority;

                            break;

                        case 3: // Brew Disabled

                            flag = IconData.displays.active;

                            hovertext = StringData.stringkeys.acIgnored;

                            break;

                        case 4: // All Disabled

                            flag = IconData.displays.skull;

                            hovertext = StringData.stringkeys.acRestricted;

                            break;

                    }
                }

                toggle.icons[0] = flag;

                toggle.text[0] = StringData.Strings(hovertext);

                journal[start++] = toggle;

                // =============================================================== ship button

                ContentComponent toggleTwo = new(ContentComponent.contentTypes.toggleright, key);

                if (!Mod.instance.magic && Mod.instance.herbalData.herbalism[key].units > 0)
                {

                    toggleTwo.icons[0] = IconData.displays.goods;

                    toggleTwo.text[0] = StringData.Strings(StringData.stringkeys.sendToGoods);

                }
                else
                {

                    toggleTwo.active = false;

                }

                journal[start++] = toggleTwo;

            }

            return journal;

        }

        public Dictionary<int, ContentComponent> JournalBombs()
        {

            Dictionary<int, ContentComponent> journal = new();

            int start = 0;

            if (!RelicData.HasRelic(IconData.relics.herbalism_mortar))
            {

                return journal;

            }

            foreach (herbals herbal in bombLayout)
            {

                bool blankSpace = false;

                if (!RelicData.HasRelic(IconData.relics.herbalism_still))
                {

                    if (bomblines[herbals.voil].Contains(herbal) || herbal == herbals.voil)
                    {

                        blankSpace = true;

                    }

                }

                if (!RelicData.HasRelic(IconData.relics.monsterbadge) || Mod.instance.magic)
                {

                    if (bomblines[herbals.captis].Contains(herbal))
                    {

                        blankSpace = true;

                    }

                }
                
                if (herbal == herbals.none)
                {

                    blankSpace = true;

                }

                if (blankSpace)
                {

                    ContentComponent blank = new(ContentComponent.contentTypes.potion, "none", false);

                    journal[start++] = blank;

                    ContentComponent blankToggle = new(ContentComponent.contentTypes.toggleleft, "none", false);

                    journal[start++] = blankToggle;

                    ContentComponent blankToggleTwo = new(ContentComponent.contentTypes.toggleright, "none", false);

                    journal[start++] = blankToggleTwo;

                    continue;

                }

                string key = herbal.ToString();

                herbalism[key].craftable = -1;

                herbalism[key].amounts.Clear();

                herbalism[key].potions.Clear();

                // =============================================================== potion button

                ContentComponent content = new(ContentComponent.contentTypes.potion, key);

                int amount = GetHerbalism(herbal);

                string amountString = amount.ToString();

                content.text[0] = amount.ToString();

                if (amount == 0)
                {

                    content.textureSources[0] = IconData.PotionRectangles(herbalism[key].grayed);

                }
                else
                {

                    content.textureSources[0] = IconData.PotionRectangles(herbalism[key].display);

                }

                switch (BuffStatus(herbalism[key]))
                {

                    case 2:

                        content.textureSources[1] = new();

                        break;

                }

                journal[start++] = content;

                // =============================================================== active button

                ContentComponent toggle = new(ContentComponent.contentTypes.toggleleft, key);

                IconData.displays flag = IconData.displays.complete;

                StringData.stringkeys hovertext = StringData.stringkeys.acEnabled;

                if (Mod.instance.save.potions.ContainsKey(herbal))
                {

                    switch (Mod.instance.save.potions[herbal])
                    {

                        case 0:

                            flag = IconData.displays.exit;

                            hovertext = StringData.stringkeys.acDisabled;

                            break;

                        case 1:

                            flag = IconData.displays.complete;

                            break;

                        case 2:

                            flag = IconData.displays.flag;

                            hovertext = StringData.stringkeys.acPriority;

                            break;

                        case 3:

                            flag = IconData.displays.active;

                            hovertext = StringData.stringkeys.acIgnored;

                            break;

                        case 4:

                            flag = IconData.displays.skull;

                            hovertext = StringData.stringkeys.acRestricted;

                            break;

                    }
                }

                toggle.icons[0] = flag;

                toggle.text[0] = StringData.Strings(hovertext);

                journal[start++] = toggle;

                // =============================================================== ship button

                ContentComponent toggleTwo = new(ContentComponent.contentTypes.toggleright, key);

                if (!Mod.instance.magic && Mod.instance.herbalData.herbalism[key].units > 0)
                {

                    toggleTwo.icons[0] = IconData.displays.goods;

                    toggleTwo.text[0] = StringData.Strings(StringData.stringkeys.sendToGoods);

                }
                else
                {

                    toggleTwo.active = false;

                }

                journal[start++] = toggleTwo;

            }

            return journal;

        }

        public Dictionary<int, ContentComponent> JournalOmens()
        {

            Dictionary<int, ContentComponent> journal = new();

            int start = 0;

            foreach (herbals herbal in omenLayout)
            {

                string key = herbal.ToString();

                herbalism[key].craftable = -1;

                herbalism[key].amounts.Clear();

                herbalism[key].potions.Clear();

                // =============================================================== potion button

                ContentComponent content = new(ContentComponent.contentTypes.potion, key);

                int amount = GetHerbalism(herbal);

                string amountString = amount.ToString();

                content.text[0] = amount.ToString();

                if (amount == 0)
                {

                    content.textureSources[0] = IconData.PotionRectangles(herbalism[key].grayed);

                }
                else
                {

                    content.textureSources[0] = IconData.PotionRectangles(herbalism[key].display);

                }

                journal[start++] = content;

                // =============================================================== ship button

                ContentComponent toggleTwo = new(ContentComponent.contentTypes.toggle, key);

                if (!Mod.instance.magic)
                {

                    toggleTwo.icons[0] = IconData.displays.goods;

                    toggleTwo.text[0] = StringData.Strings(StringData.stringkeys.sendToGoods);

                }
                else
                {

                    toggleTwo.active = false;

                }

                journal[start++] = toggleTwo;

            }

            return journal;

        }

        public static int GetCraftable(Herbal herbal)
        {

            if(herbal.craftable != -1)
            {

                return herbal.craftable;

            }
            switch (herbal.type)
            {

                default:

                    int existing = GetHerbalism(herbal.herbal);

                    if(existing == 999)
                    {

                        return 1000;

                    }

                    int craftable = 999 - existing;

                    if (herbal.bases.Count > 0)
                    {

                        int baseAmount = herbal.potions.Values.Min();

                        craftable = Math.Min(craftable, baseAmount);

                    }

                    if (herbal.ingredients.Count > 0)
                    {

                        int baseIngredient = herbal.amounts.Values.Sum();

                        craftable = Math.Min(craftable, baseIngredient);

                    }

                    herbal.craftable = craftable;

                    return craftable;

                case Herbal.herbalType.omen:
                case Herbal.herbalType.trophy:

                    return 0;

            }

        }

        public static Dictionary<string, int> GetItems(Herbal herbal)
        {

            if (herbal.amounts.Count != 0)
            {

                return herbal.amounts;

            }

            switch (herbal.type)
            {
                default:

                    if(herbal.ingredients.Count == 0)
                    {

                        return new();

                    }

                    foreach (string ingredient in herbal.ingredients)
                    {

                        herbal.amounts.Add(ingredient, Game1.player.Items.CountId(ingredient));

                    }
                        
                    return herbal.amounts;

                case Herbal.herbalType.omen:

                    if (GetHerbalism(herbal.herbal) == 0)
                    {

                        return new();

                    }

                    int level = Mod.instance.herbalData.MaxHerbal();

                    switch (herbal.herbal)
                    {

                        case herbals.omen_tusk:

                            if(Game1.player.Items.CountId(HerbalData.stone) <= 3)
                            {

                                return new();

                            }

                            herbal.amounts.Add(HerbalData.omnigeode, 3);

                            return herbal.amounts;

                        case herbals.omen_nest:

                            if (Game1.player.Items.CountId(HerbalData.wood) <= 5)
                            {

                                return new();

                            }

                            herbal.amounts.Add(HerbalData.hardwood, 5);

                            return herbal.amounts;

                        case herbals.omen_glass:

                            if (Game1.player.Items.CountId(HerbalData.emerald) <= 0)
                            {

                                return new();

                            }
                            if (Game1.player.Items.CountId(HerbalData.aquamarine) <= 0)
                            {

                                return new();

                            }
                            if (Game1.player.Items.CountId(HerbalData.ruby) <= 0)
                            {

                                return new();

                            }

                            herbal.amounts.Add(HerbalData.prismatic, 1);

                            return herbal.amounts;

                        case herbals.omen_coral:

                            if (Game1.player.CurrentItem != null)
                            {

                                if (Game1.player.CurrentItem is StardewValley.Object obj)
                                {

                                    if (obj.QualifiedItemId == HerbalData.sap && obj.Stack >= 5)
                                    {

                                        List<string> slimeSyrups = new()
                                        {
                                            "772","773","879","724","725","726","184",
                                        };

                                        foreach (string syrup in slimeSyrups)
                                        {

                                            herbal.amounts.Add(syrup, 1);

                                        }

                                    }

                                }

                            }

                            return herbal.amounts;

                    }

                    return new();

                case Herbal.herbalType.trophy:

                    if (GetHerbalism(herbal.herbal) == 0)
                    {

                        return new();

                    }

                    switch (herbal.herbal)
                    {

                        case herbals.trophy_pumpkin:

                            if(Game1.player.CurrentItem != null)
                            {

                                if(Game1.player.CurrentItem is StardewValley.Object obj)
                                {

                                    if(obj.Category == StardewValley.Object.CookingCategory && obj.Quality < 4)
                                    {

                                        herbal.amounts.Add(obj.QualifiedItemId, 1);

                                    }

                                }

                            }

                            return herbal.amounts;

                        case herbals.trophy_pearl:

                            if (Game1.player.CurrentItem != null)
                            {

                                if (Game1.player.CurrentItem is StardewValley.Object obj)
                                {

                                    if (obj.maximumStackSize() > 1)
                                    {

                                        herbal.amounts.Add(obj.QualifiedItemId, 1);

                                    }

                                }

                            }

                            return herbal.amounts;

                        case herbals.trophy_tooth:

                            if (Game1.player.CurrentItem != null)
                            {

                                if (Game1.player.CurrentItem is StardewValley.Object obj)
                                {

                                    if ((obj.Category == StardewValley.Object.VegetableCategory
                                        || obj.Category == StardewValley.Object.FruitsCategory
                                        || obj.Category == StardewValley.Object.flowersCategory)
                                        && obj.Quality < 4)
                                    {

                                        herbal.amounts.Add(obj.QualifiedItemId, 1);

                                    }

                                }

                            }

                            return herbal.amounts;

                        case herbals.trophy_spiral:

                            if (Game1.player.CurrentItem != null)
                            {

                                if (Game1.player.CurrentItem is StardewValley.Object obj)
                                {

                                    if (obj.Category == StardewValley.Object.artisanGoodsCategory && obj.Quality < 4)
                                    {

                                        herbal.amounts.Add(obj.QualifiedItemId, 1);

                                    }

                                }

                            }

                            return herbal.amounts;


                        case herbals.trophy_spike:

                            if (Game1.player.CurrentItem != null)
                            {

                                if (Game1.player.CurrentItem is StardewValley.Object obj)
                                {

                                    if (obj.QualifiedItemId == HerbalData.cherrybomb && obj.Stack >= 5)
                                    {

                                        herbal.amounts.Add(HerbalData.megabomb, 5);

                                    }

                                    if (obj.QualifiedItemId == HerbalData.bomb && obj.Stack >= 5)
                                    {

                                        herbal.amounts.Add(HerbalData.megabomb, 5);

                                    }

                                }

                            }

                            return herbal.amounts;

                    }

                    return new();

            }

        }

        public static Dictionary<herbals, int> GetPotions(Herbal herbal)
        {

            if (herbal.potions.Count != 0)
            {

                return herbal.potions;

            }

            switch (herbal.type)
            {
                default:

                    if (herbal.bases.Count == 0)
                    {

                        return new();

                    }

                    foreach (herbals basePotion in herbal.bases)
                    {

                        herbal.potions.Add(basePotion, GetHerbalism(basePotion));

                    }

                    return herbal.potions;

                case Herbal.herbalType.omen:

                    if (GetHerbalism(herbal.herbal) == 0)
                    {

                        return new();

                    }

                    int level = Mod.instance.herbalData.MaxHerbal();

                    switch (herbal.herbal)
                    {

                        case herbals.omen_feather:

                            for (int i = Mod.instance.herbalData.lines[herbals.ligna].Count - 1; i >= 0; i--)
                            {

                                herbals herbalName = Mod.instance.herbalData.lines[herbals.ligna][i];

                                if (Mod.instance.herbalData.herbalism[herbalName.ToString()].level <= level && GetHerbalism(herbalName) < 999)
                                {

                                    herbal.potions.Add(herbalName, 1);

                                    return herbal.potions;

                                }

                            }

                            break;

                        case herbals.omen_tuft:

                            for (int i = Mod.instance.herbalData.lines[herbals.impes].Count - 1; i >= 0; i--)
                            {

                                herbals herbalName = Mod.instance.herbalData.lines[herbals.impes][i];

                                if (Mod.instance.herbalData.herbalism[herbalName.ToString()].level <= level && GetHerbalism(herbalName) < 999)
                                {

                                    herbal.potions.Add(herbalName, 1);

                                    return herbal.potions;

                                }

                            }
                            break;

                        case herbals.omen_shell:

                            for (int i = Mod.instance.herbalData.lines[herbals.celeri].Count - 1; i >= 0; i--)
                            {

                                herbals herbalName = Mod.instance.herbalData.lines[herbals.celeri][i];

                                if (Mod.instance.herbalData.herbalism[herbalName.ToString()].level <= level && GetHerbalism(herbalName) < 999)
                                {

                                    herbal.potions.Add(herbalName, 1);

                                    return herbal.potions;

                                }

                            }
                            break;


                    }
             
                    return new();

            }

        }

        public static int UpdateHerbalism(herbals herbal, int amount = 0)
        {

            if (amount >= 0)
            {

                if (Mod.instance.save.herbalism.ContainsKey(herbal))
                {

                    if (amount != 0)
                    {

                        int addition = Math.Min(amount, 999 - Mod.instance.save.herbalism[herbal]);

                        Mod.instance.save.herbalism[herbal] += amount;

                    }

                }
                else
                {

                    Mod.instance.save.herbalism[herbal] = amount;

                    GetDefaults(herbal);

                }

            }
            else
            {

                if (Mod.instance.save.herbalism.ContainsKey(herbal))
                {

                    int subtraction = Mod.instance.save.herbalism[herbal] - Math.Abs(amount);

                    if (subtraction < 0)
                    {

                        Mod.instance.save.herbalism[herbal] = 0;

                    }
                    else
                    {

                        Mod.instance.save.herbalism[herbal] -= Math.Abs(amount);

                    }

                }
                else
                {

                    Mod.instance.save.herbalism[herbal] = 0;

                    GetDefaults(herbal);

                }

            }

            if (amount != 0)
            {

                Mod.instance.SyncPreferences();

            }

            return Mod.instance.save.herbalism[herbal];

        }

        public static int GetHerbalism(herbals herbal)
        {

            if (!Mod.instance.save.herbalism.ContainsKey(herbal))
            {

                Mod.instance.save.herbalism[herbal] = 0;

                GetDefaults(herbal);

            }

            return Mod.instance.save.herbalism[herbal];

        }

        public static void GetDefaults(herbals herbal)
        {

            if (!Mod.instance.save.potions.ContainsKey(herbal))
            {

                ModData.potionDefaults potionDefault = Enum.Parse<ModData.potionDefaults>(Mod.instance.Config.potionDefault);

                switch (potionDefault)
                {
                    case ModData.potionDefaults.noconsume:

                        Mod.instance.save.potions.Add(herbal, 0);

                        break;

                    case ModData.potionDefaults.nobrew:

                        Mod.instance.save.potions.Add(herbal, 3);

                        break;

                    case ModData.potionDefaults.disabled:

                        Mod.instance.save.potions.Add(herbal, 4);

                        break;

                    default:

                        Mod.instance.save.potions.Add(herbal, 1);

                        break;

                }

            }

        }

        public void PotionBehaviour(string id)
        {

            Herbal herbal = herbalism[id];

            GetDefaults(herbal.herbal);

            switch (Mod.instance.save.potions[herbal.herbal])
            {

                case 0:
                    Mod.instance.save.potions[herbal.herbal] = 1;
                    break;
                case 1:
                    Mod.instance.save.potions[herbal.herbal] = 2;
                    break;
                case 2:
                    Mod.instance.save.potions[herbal.herbal] = 3;
                    break;
                case 3:
                    Mod.instance.save.potions[herbal.herbal] = 4;
                    break;
                default:
                case 4:
                    Mod.instance.save.potions[herbal.herbal] = 0;
                    break;
            }

        }

        public void ConvertToGoods(string id, int amount)
        {

            Herbal herbal = herbalism[id];

            if(herbal.units <= 0)
            {

                return;

            }

            if (!Mod.instance.save.herbalism.ContainsKey(herbal.herbal))
            {

                return;

            }

            int convert = Math.Min(amount,Mod.instance.save.herbalism[herbal.herbal]);

            Mod.instance.save.herbalism[herbal.herbal] -= convert;

            int goods = convert * herbal.units;

            Mod.instance.exportHandle.AddExport(herbal.export, goods);

            Game1.playSound(SpellHandle.Sounds.Ship.ToString());

        }

        public herbals BestHerbal(herbals line)
        {

            if (!lines.ContainsKey(line))
            {

                return line;

            }

            for(int i = lines[line].Count - 1; i >= 0; i--)
            {

                herbals potion = lines[line][i];

                if (Mod.instance.save.herbalism.ContainsKey(potion))
                {

                    if(Mod.instance.save.herbalism[potion] > 0)
                    {

                        return potion;

                    }

                }

            }

            return herbals.none;

        }

        public void MassBrew(bool bench = false)
        {

            CharacterHandle.RetrieveInventory(CharacterHandle.characters.herbalism);

            int max = MaxHerbal();

            if (RelicData.HasRelic(IconData.relics.herbalism_gauge))
            {

                BrewHerbal(herbals.aether.ToString(), 50, bench);

            }

            if (RelicData.HasRelic(IconData.relics.herbalism_crucible))
            {

                BrewHerbal(herbals.faeth.ToString(), 50, bench);

            }

            foreach (KeyValuePair<herbals, List<herbals>> line in lines)
            {

                foreach (herbals herbal in line.Value)
                {

                    string key = herbal.ToString();

                    if (herbalism[key].level > max)
                    {

                        continue;

                    }

                    BrewHerbal(key, 50, bench);

                }

            }

        }

        public void MassGrind(bool bench = false)
        {

            CharacterHandle.RetrieveInventory(CharacterHandle.characters.herbalism);

            foreach (KeyValuePair<herbals, List<herbals>> bombline in bomblines)
            {

                switch (bombline.Key)
                {
                    case herbals.coruscant:

                        if (!RelicData.HasRelic(IconData.relics.herbalism_mortar))
                        {

                            continue;

                        }

                        BrewHerbal(herbals.coruscant.ToString(), 10, bench);

                        break;

                    case herbals.voil:

                        if (!RelicData.HasRelic(IconData.relics.herbalism_still))
                        {

                            continue;

                        }

                        BrewHerbal(herbals.voil.ToString(), 10, bench);

                        break;

                    case herbals.captis:

                        if (!RelicData.HasRelic(IconData.relics.monsterbadge) || Mod.instance.magic)
                        {

                            continue;

                        }

                        break;


                }

                foreach (herbals herbal in bombline.Value)
                {

                    BrewHerbal(herbal.ToString(), 10, bench);

                }

            }

        }

        public void BrewHerbal(string id, int draught, bool bench = false, bool force = false)
        {

            Herbal herbal = herbalism[id];

            if (Mod.instance.save.potions.ContainsKey(herbal.herbal) && !force)
            {

                if (Mod.instance.save.potions[herbal.herbal] == 3 || Mod.instance.save.potions[herbal.herbal] == 4)
                {

                    return;

                }

            }

            int brewable = Math.Min(999 - GetHerbalism(herbal.herbal), draught);

            if (brewable == 0)
            {

                return;

            }

            if (herbal.bases.Count > 0)
            {

                foreach (herbals required in herbal.bases)
                {

                    int base_ready = GetHerbalism(required);

                    if (base_ready == 0)
                    {

                        return;

                    }

                    brewable = Math.Min(brewable, base_ready);

                }

            }

            int brewed = brewable;

            if (herbal.ingredients.Count > 0)
            {

                brewed = 0;

                if (bench)
                {

                    CharacterHandle.RetrieveInventory(CharacterHandle.characters.herbalism);

                    Chest herbalBench = Mod.instance.chests[CharacterHandle.characters.herbalism];

                    for (int i = 0; i < herbalBench.Items.Count; i++)
                    {

                        if (brewable <= 0)
                        {

                            break;

                        }

                        Item checkSlot = herbalBench.Items.ElementAt(i);

                        if (checkSlot == null)
                        {

                            continue;

                        }

                        Item checkItem = checkSlot.getOne();

                        if (herbal.ingredients.Contains(@checkItem.QualifiedItemId))
                        {

                            int ready_ingredient = herbalBench.Items.ElementAt(i).Stack;

                            if (ready_ingredient > 0)
                            {

                                int ready_brew = Math.Min(ready_ingredient, brewable);

                                herbalBench.Items.ElementAt(i).Stack -= ready_brew;

                                brewed += ready_brew;

                                brewable -= ready_brew;

                            }

                        }

                    }

                    CharacterHandle.CleanInventory(CharacterHandle.characters.herbalism);

                }

                foreach (string ingredient in herbal.ingredients)
                {

                    if (brewable <= 0)
                    {

                        break;

                    }

                    int ready_ingredient = Game1.player.Items.CountId(ingredient);

                    if (ready_ingredient > 0)
                    {

                        int ready_brew = Math.Min(ready_ingredient, brewable);

                        Game1.player.Items.ReduceId(ingredient, ready_brew);

                        brewed += ready_brew;

                        brewable -= ready_brew;

                    }

                }

            }

            if (herbal.bases.Count > 0)
            {

                foreach (herbals required in herbal.bases)
                {

                    UpdateHerbalism(required, 0-brewed);

                }

            }

            Game1.player.currentLocation.playSound(SpellHandle.Sounds.bubbles.ToString());

            UpdateHerbalism(herbal.herbal, brewed);

        }

        public void ConsumeHerbal(string id, bool force = false)
        {

            Herbal herbal = herbalism[id];

            if (Mod.instance.save.potions.ContainsKey(herbal.herbal) && !force)
            {

                if (Mod.instance.save.potions[herbal.herbal] == 0 || Mod.instance.save.potions[herbal.herbal] == 4)
                {

                    return;

                }
            
            }

            float difficulty = 1.6f - Mod.instance.ModDifficulty() * 0.1f;

            int staminaGain = (int)(herbal.stamina * difficulty);

            int healthGain = (int)(herbal.health * difficulty);

            Game1.player.Stamina = Math.Min(Game1.player.MaxStamina, Game1.player.Stamina + staminaGain);

            Game1.player.health = Math.Min(Game1.player.maxHealth, Game1.player.health + healthGain);

            Rectangle healthBox = Game1.player.GetBoundingBox();

            if (Game1.currentGameTime.TotalGameTime.TotalSeconds > consumeBuffer)
            {

                consumeBuffer = Game1.currentGameTime.TotalGameTime.TotalSeconds + 5;

                DisplayPotion hudmessage = new(Mod.instance.Helper.Translation.Get("HerbalData.1116").Tokens(new { potion = herbal.title, }), herbal);

                Game1.addHUDMessage(hudmessage);

            }

            if (herbal.buff != HerbalBuff.herbalbuffs.none)
            {

                buff.apply(herbal.buff, herbal.level, herbal.duration);

            }

            UpdateHerbalism(herbal.herbal, 0 - 1);

            Mod.instance.SyncPreferences();

        }

        public int BuffStatus(Herbal herbal)
        {

            if(herbal.buff == HerbalBuff.herbalbuffs.none)
            {

                return 0;

            }

            if (buff.applied.ContainsKey(herbal.buff))
            {

                return 2;

            }

            return 1;


        }

        public void CheckBuff()
        {

            buff.check();

            if(buff.applied.Count <= 0)
            {

                if (Game1.player.buffs.IsApplied(184652.ToString()))
                {

                    ClearBuff();

                }

                return;

            }

            if (Game1.player.buffs.IsApplied(184652.ToString()))
            {

                return;

            }

            Game1.player.buffs.Apply(buff);

        }

        public void ClearBuff()
        {

            Game1.player.buffs.Remove(184652.ToString());

        }

        public void RemoveBuffs()
        {

            Game1.player.buffs.Remove(184652.ToString());

            buff.applied.Clear();

        }

        public void HoverBuff(SpriteBatch b)
        {

            if (Game1.buffsDisplay.hoverText.Contains(StringData.Strings(StringData.stringkeys.herbalBuffDescription)))
            {

                if(Game1.buffsDisplay.isWithinBounds(Game1.getOldMouseX(), Game1.getOldMouseY()))
                {

                    buff.draw(b);

                    return;

                }

            }

        }

        public void ConvertGeodes()
        {

            if (Context.IsMainPlayer)
            {

                CharacterHandle.RetrieveInventory(CharacterHandle.characters.anvil);

            }

            Dictionary<string, Item> extracts = new();

            for (int i = 0; i < Game1.player.Items.Count; i++)
            {

                Item checkSlot = Game1.player.Items[i];

                if (checkSlot == null)
                {

                    continue;

                }

                Item checkItem = checkSlot.getOne();

                if (Utility.IsGeode(checkItem))
                {

                    for (int e = Game1.player.Items[i].Stack - 1; e >= 0; e--)
                    {

                        Item extraction = Utility.getTreasureFromGeode(checkItem);

                        if (checkItem.QualifiedItemId.Contains("MysteryBox"))
                        {

                            Game1.stats.Increment("MysteryBoxesOpened", 1);

                        }
                        else
                        {

                            Game1.stats.GeodesCracked++;

                        }

                        if (extracts.ContainsKey(extraction.QualifiedItemId))
                        {

                            extracts[extraction.QualifiedItemId].Stack += extraction.Stack;

                        }
                        else
                        {

                            extracts[extraction.QualifiedItemId] = extraction;

                        }

                        Game1.player.Items[i] = null;

                    }

                }

            }

            Vector2 origin = new(0);

            if (Game1.currentLocation is Grove grove)
            {

                origin = grove.anvilPosition;

            }
            else
            {

                return;

            }

            foreach (KeyValuePair<string, Item> extract in extracts)
            {

                if (!Context.IsMainPlayer)
                {

                    ThrowHandle throwExtract = new(Game1.player, origin, extract.Value);

                    throwExtract.register();

                    continue;

                }

                if (Mod.instance.chests[CharacterHandle.characters.anvil].addItem(extract.Value) != null)
                {
                    ThrowHandle throwExtract = new(Game1.player, origin, extract.Value);

                    throwExtract.register();

                }

            }

            Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.Spells.greatbolt, factor = 3, sound = SpellHandle.Sounds.thunder, });

            Rectangle relicRect = IconData.RelicRectangles(IconData.relics.crow_hammer);

            TemporaryAnimatedSprite animation = new(0, 1500, 1, 1, origin, false, false)
            {
                sourceRect = relicRect,
                sourceRectStartingPos = new(relicRect.X, relicRect.Y),
                texture = Mod.instance.iconData.relicsTexture,
                layerDepth = 900f,
                rotation = -0.76f,
                scale = 4f,
            };

            Game1.player.currentLocation.TemporarySprites.Add(animation);

            Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.supree, new()) { type = SpellHandle.Spells.effect, counter = -15, sound = SpellHandle.Sounds.yoba, displayRadius = 2, });

            Mod.instance.spellRegister.Add(new(origin, 256, IconData.impacts.supree, new()) { type = SpellHandle.Spells.effect, counter = -60, instant = true, displayRadius = 3, scheme = IconData.schemes.mists, sound = SpellHandle.Sounds.secret1 });

        }

        public static string ForgeCheck()
        {

            if (Game1.player.CurrentTool is Tool currentTool)
            {

                Dictionary<string, string> toolUpgrades = new()
                {
                    // Axe
                    ["(T)Axe"] = "(T)CopperAxe",
                    ["(T)CopperAxe"] = "(T)SteelAxe",
                    ["(T)SteelAxe"] = "(T)GoldAxe",
                    ["(T)GoldAxe"] = "(T)IridiumAxe",
                    // Pickaxe
                    ["(T)Pickaxe"] = "(T)CopperPickaxe",
                    ["(T)CopperPickaxe"] = "(T)SteelPickaxe",
                    ["(T)SteelPickaxe"] = "(T)GoldPickaxe",
                    ["(T)GoldPickaxe"] = "(T)IridiumPickaxe",
                    // Hoe
                    ["(T)WateringCan"] = "(T)CopperWateringCan",
                    ["(T)CopperWateringCan"] = "(T)SteelWateringCan",
                    ["(T)SteelWateringCan"] = "(T)GoldWateringCan",
                    ["(T)GoldWateringCan"] = "(T)IridiumWateringCan",
                    // Can
                    ["(T)Hoe"] = "(T)CopperHoe",
                    ["(T)CopperHoe"] = "(T)SteelHoe",
                    ["(T)SteelHoe"] = "(T)GoldHoe",
                    ["(T)GoldHoe"] = "(T)IridiumHoe",

                };

                if (toolUpgrades.ContainsKey(currentTool.QualifiedItemId))
                {

                    return toolUpgrades[currentTool.QualifiedItemId];

                }

            }

            return string.Empty;

        }

        public static int ForgeRequirement(string toolId)
        {

            Dictionary<string, int> toolRequirements = new()
            {
                // Axe
                ["(T)CopperAxe"] = 4,
                ["(T)SteelAxe"] = 6,
                ["(T)GoldAxe"] = 9,
                ["(T)IridiumAxe"] = 12,
                // Pickaxe
                ["(T)CopperPickaxe"] = 4,
                ["(T)SteelPickaxe"] = 6,
                ["(T)GoldPickaxe"] = 9,
                ["(T)IridiumPickaxe"] = 12,
                // Hoe
                ["(T)CopperWateringCan"] = 4,
                ["(T)SteelWateringCan"] = 6,
                ["(T)GoldWateringCan"] = 9,
                ["(T)IridiumWateringCan"] = 12,
                // Can
                ["(T)CopperHoe"] = 4,
                ["(T)SteelHoe"] = 6,
                ["(T)GoldHoe"] = 9,
                ["(T)IridiumHoe"] = 12,

            };

            return toolRequirements[toolId];


        }

        public static void ForgeUpgrade()
        {

            if (Game1.player.CurrentTool is Tool currentTool)
            {

                Dictionary<string, string> toolUpgrades = new()
                {
                    // Axe
                    ["(T)Axe"] = "(T)CopperAxe",
                    ["(T)CopperAxe"] = "(T)SteelAxe",
                    ["(T)SteelAxe"] = "(T)GoldAxe",
                    ["(T)GoldAxe"] = "(T)IridiumAxe",
                    // Pickaxe
                    ["(T)Pickaxe"] = "(T)CopperPickaxe",
                    ["(T)CopperPickaxe"] = "(T)SteelPickaxe",
                    ["(T)SteelPickaxe"] = "(T)GoldPickaxe",
                    ["(T)GoldPickaxe"] = "(T)IridiumPickaxe",
                    // Hoe
                    ["(T)WateringCan"] = "(T)CopperWateringCan",
                    ["(T)CopperWateringCan"] = "(T)SteelWateringCan",
                    ["(T)SteelWateringCan"] = "(T)GoldWateringCan",
                    ["(T)GoldWateringCan"] = "(T)IridiumWateringCan",
                    // Can
                    ["(T)Hoe"] = "(T)CopperHoe",
                    ["(T)CopperHoe"] = "(T)SteelHoe",
                    ["(T)SteelHoe"] = "(T)GoldHoe",
                    ["(T)GoldHoe"] = "(T)IridiumHoe",

                };

                if (toolUpgrades.ContainsKey(currentTool.QualifiedItemId))
                {

                    Tool tooling = (Tool)ItemRegistry.Create(toolUpgrades[currentTool.QualifiedItemId]);

                    tooling.UpgradeFrom(currentTool);

                    Game1.player.removeItemFromInventory(currentTool);

                    Vector2 origin = new Vector2(1280, 1048);

                    new ThrowHandle(Game1.player, origin, tooling) { delay = 60 }.register();

                    Mod.instance.spellRegister.Add(new(origin, 192, IconData.impacts.none, new()) { type = SpellHandle.Spells.greatbolt, factor = 3, sound = SpellHandle.Sounds.thunder, });

                    Rectangle relicRect = IconData.RelicRectangles(IconData.relics.crow_hammer);

                    TemporaryAnimatedSprite animation = new(0, 1500, 1, 1, origin, false, false)
                    {
                        sourceRect = relicRect,
                        sourceRectStartingPos = new(relicRect.X, relicRect.Y),
                        texture = Mod.instance.iconData.relicsTexture,
                        layerDepth = 900f,
                        rotation = -0.76f,
                        scale = 4f,
                    };

                    Game1.player.currentLocation.TemporarySprites.Add(animation);

                    Mod.instance.spellRegister.Add(new(origin, 256, IconData.impacts.supree, new()) { type = SpellHandle.Spells.effect, displayRadius = 3, counter = -15, sound = SpellHandle.Sounds.yoba });

                    Mod.instance.spellRegister.Add(new(origin, 320, IconData.impacts.supree, new()) { type = SpellHandle.Spells.effect, displayRadius = 4, counter = -60, instant = true, scheme = IconData.schemes.mists, sound = SpellHandle.Sounds.secret1 });

                }

            }

        }

        public static bool RandomStudy()
        {

            Dictionary<string, string> recipes = new();

            foreach (KeyValuePair<string, string> allRecipes in CraftingRecipe.cookingRecipes)
            {

                if (!Game1.player.cookingRecipes.ContainsKey(allRecipes.Key))
                {

                    recipes[allRecipes.Key] = allRecipes.Value;

                }

            }

            if ((recipes.Count == 0 || Mod.instance.randomIndex.Next(3) == 0) && !Mod.instance.Config.disableShopdata)
            {
                List<string> books = new()
                {
                    "Book_Trash",
                    "Book_Crabbing",
                    "Book_Bombs",
                    "Book_Roe",
                    "Book_WildSeeds",
                    "Book_Woodcutting",
                    "Book_Defense",
                    "Book_Friendship",
                    "Book_Void",
                    "Book_Speed",
                    "Book_Marlon",
                    "Book_QueenOfSauce",
                    "Book_Diamonds",
                    "Book_Mystery",
                    "Book_Speed2",
                    "Book_Artifact",
                    "Book_Horse",
                    "Book_Grass",
                };

                List<string> bookCandidates = new();

                foreach (string book in books)
                {

                    if (Game1.player.stats.Get(book) != 0)
                    {

                        continue;

                    }

                    bookCandidates.Add(book);

                }

                if (bookCandidates.Count > 0)
                {

                    Item newBook = ItemRegistry.Create(books[Mod.instance.randomIndex.Next(bookCandidates.Count)], 1);

                    new ThrowHandle(Game1.player, Game1.player.Position + new Vector2(64, 128), newBook) { delay = 10, holdup = true }.register();

                    return true;

                }

            }

            if (recipes.Count == 0)
            {

                return false;

            }

            KeyValuePair<string, string> craftingRecipe = recipes.ElementAt(Mod.instance.randomIndex.Next(recipes.Count));

            Game1.player.cookingRecipes.Add(craftingRecipe.Key, 0);

            CraftingRecipe newThing = new(craftingRecipe.Key);

            ThrowHandle.AnimateHoldup();

            Rectangle relicRect = IconData.RelicRectangles(IconData.relics.book_letters);

            TemporaryAnimatedSprite animation = new(0, 2000, 1, 1, Game1.player.Position + new Vector2(2, -124f), false, false)
            {
                sourceRect = relicRect,
                sourceRectStartingPos = new(relicRect.X, relicRect.Y),
                texture = Mod.instance.iconData.relicsTexture,
                layerDepth = 900f,
                delayBeforeAnimationStart = 175,
                scale = 3f,

            };

            Game1.player.currentLocation.TemporarySprites.Add(animation);

            string text = Mod.instance.Helper.Translation.Get("HerbalData.361.35") + newThing.DisplayName;

            Game1.drawObjectDialogue(text);

            return true;

        }

        public static bool RandomTinker()
        {

            List<string> list = new()
            {
                "(BC)10",
                "(BC)12",
                "(BC)13",
                "(BC)16",
                "(BC)19",
                "(BC)21",
                "(BC)25",
                "(BC)156",
                "(BC)158",
                "(BC)165",
                "(BC)182",
                "(BC)208",
                "(BC)211",
                "(BC)216",
                "(BC)239",
                "(BC)246",
                "(BC)265",
                "(BC)272",
                "(BC)275",
                //"(BC)MushroomLog",
                "(BC)BaitMaker",
                "(BC)Dehydrator",
                "(BC)FishSmoker",

            };

            Item newMachine = ItemRegistry.Create(list[Mod.instance.randomIndex.Next(list.Count)], 1);

            new ThrowHandle(Game1.player, Game1.player.Position + new Vector2(128, 0), newMachine) { delay = 10, holdup = true }.register();

            return true;

        }

        public void ConsumeOmen(string id, int amount)
        {

            Herbal herbal = herbalism[id];

            int available = GetHerbalism(herbal.herbal);

            if (available == 0)
            {

                Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                return;

            }

            int level = Mod.instance.herbalData.MaxHerbal();

            switch (herbal.herbal)
            {

                case herbals.omen_feather:

                    for (int i = Mod.instance.herbalData.lines[herbals.ligna].Count - 1; i >= 0; i--)
                    {

                        herbals herbalName = Mod.instance.herbalData.lines[herbals.ligna][i];

                        int potionExist = GetHerbalism(herbalName);

                        if (Mod.instance.herbalData.herbalism[herbalName.ToString()].level <= level && potionExist < 999)
                        {

                            List<int> potionLimits = new()
                            {
                                999 - potionExist,

                                available,

                                amount,

                            };

                            int potionLimit = potionLimits.Min();

                            UpdateHerbalism(herbalName, potionLimit);

                            UpdateHerbalism(herbal.herbal, 0 - potionLimit);

                            Game1.playSound(SpellHandle.Sounds.bubbles.ToString());

                            return;

                        }

                    }

                    break;

                case herbals.omen_tuft:

                    for (int i = Mod.instance.herbalData.lines[herbals.impes].Count - 1; i >= 0; i--)
                    {

                        herbals herbalName = Mod.instance.herbalData.lines[herbals.impes][i];

                        int potionExist = GetHerbalism(herbalName);

                        if (Mod.instance.herbalData.herbalism[herbalName.ToString()].level <= level && potionExist < 999)
                        {

                            List<int> potionLimits = new()
                            {
                                999 - potionExist,

                                available,

                                amount,

                            };

                            int potionLimit = potionLimits.Min();

                            UpdateHerbalism(herbalName, potionLimit);

                            UpdateHerbalism(herbal.herbal, 0 - potionLimit);

                            Game1.playSound(SpellHandle.Sounds.bubbles.ToString());

                            return;

                        }

                    }
                    break;

                case herbals.omen_shell:

                    for (int i = Mod.instance.herbalData.lines[herbals.celeri].Count - 1; i >= 0; i--)
                    {

                        herbals herbalName = Mod.instance.herbalData.lines[herbals.celeri][i];

                        int potionExist = GetHerbalism(herbalName);

                        if (Mod.instance.herbalData.herbalism[herbalName.ToString()].level <= level && potionExist < 999)
                        {

                            List<int> potionLimits = new()
                            {
                                999 - potionExist,

                                available,

                                amount,

                            };

                            int potionLimit = potionLimits.Min();

                            UpdateHerbalism(herbalName, potionLimit);

                            UpdateHerbalism(herbal.herbal, 0 - potionLimit);

                            Game1.playSound(SpellHandle.Sounds.bubbles.ToString());

                            return;

                        }

                    }
                    break;

                case herbals.omen_tusk:

                    int stoneCount = Game1.player.Items.CountId(HerbalData.stone);

                    List<int> omniLimits = new()
                    {
                        (int)(stoneCount / 3),

                        available,

                        amount,

                    };

                    int omniLimit = omniLimits.Min();

                    int omniYield = omniLimit * 3;

                    if (omniYield <= 0)
                    {

                        break;

                    }

                    Game1.player.Items.ReduceId(HerbalData.stone, omniYield);

                    UpdateHerbalism(herbal.herbal, 0 - omniLimit);

                    StardewValley.Object omni = new StardewValley.Object(HerbalData.omnigeode.Replace("(O)", ""), omniYield);

                    if (!Game1.player.addItemToInventoryBool(omni))
                    {

                        Game1.player.dropItem(omni);

                    }

                    Game1.playSound(SpellHandle.Sounds.bubbles.ToString());

                    return;

                case herbals.omen_nest:

                    int woodCount = Game1.player.Items.CountId(HerbalData.wood);

                    List<int> hardLimits = new()
                    {
                        (int)(woodCount / 5),

                        available,

                        amount,

                    };

                    int hardLimit = hardLimits.Min();

                    if (hardLimit <= 0)
                    {

                        break;

                    }

                    int hardYield = hardLimit * 5;

                    Game1.player.Items.ReduceId(HerbalData.wood, hardYield);

                    UpdateHerbalism(herbal.herbal, 0 - hardLimit);

                    StardewValley.Object hard = new StardewValley.Object(HerbalData.hardwood.Replace("(O)", ""), hardYield);

                    if (!Game1.player.addItemToInventoryBool(hard))
                    {

                        Game1.player.dropItem(hard);

                    }

                    Game1.playSound(SpellHandle.Sounds.bubbles.ToString());

                    return;

                case herbals.omen_glass:

                    List<int> prismLimits = new()
                    {

                        Game1.player.Items.CountId(HerbalData.emerald),

                        Game1.player.Items.CountId(HerbalData.aquamarine),

                        Game1.player.Items.CountId(HerbalData.ruby),

                        available,

                        amount,

                    };

                    int prismYield = prismLimits.Min();

                    if (prismYield <= 0)
                    {

                        break;

                    }

                    Game1.player.Items.ReduceId(HerbalData.emerald, prismYield);

                    Game1.player.Items.ReduceId(HerbalData.aquamarine, prismYield);

                    Game1.player.Items.ReduceId(HerbalData.ruby, prismYield);

                    UpdateHerbalism(herbal.herbal, 0 - prismYield);

                    StardewValley.Object prism = new StardewValley.Object(HerbalData.prismatic.Replace("(O)", ""), prismYield);

                    if (!Game1.player.addItemToInventoryBool(prism))
                    {

                        Game1.player.dropItem(prism);

                    }

                    Game1.playSound(SpellHandle.Sounds.bubbles.ToString());

                    return;

                case herbals.omen_down:

                    List<int> powderLimits = new()
                    {

                        available,

                        amount,

                    };

                    int powderYield = powderLimits.Min();

                    if (powderYield <= 0)
                    {

                        break;

                    }

                    for (int i = 0; i < powderYield; i++)
                    {

                        HerbalHandle.herbals randomPowder = HerbalHandle.RandomPowder();

                        UpdateHerbalism(randomPowder, 1);

                    }

                    UpdateHerbalism(herbal.herbal, 0 - powderYield);

                    Game1.playSound(SpellHandle.Sounds.bubbles.ToString());

                    return;

                case herbals.omen_coral:

                    List<int> syrupLimits = new()
                    {

                        (int)(Game1.player.Items.CountId(HerbalData.sap) / 5),

                        available,

                        amount,

                    };

                    int syrupYield = syrupLimits.Min();

                    if (syrupYield <= 0)
                    {

                        break;

                    }

                    Game1.player.Items.ReduceId(HerbalData.sap, syrupYield * 5);

                    List<string> slimeSyrups = new()
                    {
                        "772","773","879","724","725","726","184",
                    };

                    string syrup = slimeSyrups[Mod.instance.randomIndex.Next(slimeSyrups.Count)];

                    StardewValley.Object addSyrup = new StardewValley.Object(syrup, syrupYield);

                    if (!Game1.player.addItemToInventoryBool(addSyrup))
                    {

                        Game1.player.dropItem(addSyrup);

                    }

                    UpdateHerbalism(herbal.herbal, 0 - syrupYield);

                    Game1.playSound(SpellHandle.Sounds.bubbles.ToString());

                    return;

                case herbals.trophy_pumpkin:

                    if (Game1.player.CurrentItem != null)
                    {

                        if (Game1.player.CurrentItem is StardewValley.Object obj)
                        {

                            if (obj.Category == StardewValley.Object.CookingCategory && obj.Quality < 4)
                            {

                                StardewValley.Item pumped = obj.getOne();

                                List<int> pumpStacks = new()
                                {

                                    obj.Stack,
                                    available,
                                    amount,

                                };

                                int pumpStack = pumpStacks.Min();

                                pumped.Stack = pumpStack;

                                pumped.Quality = 4;

                                if (!Game1.player.addItemToInventoryBool(pumped))
                                {

                                    Game1.player.dropItem(pumped);

                                }

                                Game1.player.Items.ReduceId(obj.QualifiedItemId, 1);

                                UpdateHerbalism(herbal.herbal, 0 - pumpStack);

                                Game1.playSound(SpellHandle.Sounds.bubbles.ToString());

                                return;

                            }

                        }

                    }

                    break;

                case herbals.trophy_pearl:

                    if (Game1.player.CurrentItem != null)
                    {

                        if (Game1.player.CurrentItem is StardewValley.Object obj)
                        {

                            if (obj.maximumStackSize() > 1)
                            {

                                StardewValley.Item pumped = obj.getOne();

                                List<int> pumpStacks = new()
                                {

                                    available,
                                    amount,

                                };

                                int pumpStack = pumpStacks.Min();

                                pumped.Stack = pumpStack;

                                if (!Game1.player.addItemToInventoryBool(pumped))
                                {

                                    Game1.player.dropItem(pumped);


                                }

                                UpdateHerbalism(herbal.herbal, 0 - pumpStack);

                                Game1.playSound(SpellHandle.Sounds.bubbles.ToString());

                                return;
                            }

                        }

                    }

                    break;

                case herbals.trophy_tooth:

                    if (Game1.player.CurrentItem != null)
                    {

                        if (Game1.player.CurrentItem is StardewValley.Object obj)
                        {

                            if ((obj.Category == StardewValley.Object.VegetableCategory
                                || obj.Category == StardewValley.Object.FruitsCategory
                                || obj.Category == StardewValley.Object.flowersCategory)
                                && obj.Quality < 4)
                            {

                                StardewValley.Item pumped = obj.getOne();

                                List<int> pumpStacks = new()
                                {

                                    obj.Stack,
                                    available,
                                    amount,

                                };

                                int pumpStack = pumpStacks.Min();

                                pumped.Stack = pumpStack;

                                pumped.Quality = 4;

                                if (!Game1.player.addItemToInventoryBool(pumped))
                                {

                                    Game1.player.dropItem(pumped);

                                }

                                Game1.player.Items.ReduceId(obj.QualifiedItemId, 1);

                                UpdateHerbalism(herbal.herbal, 0 - pumpStack);

                                Game1.playSound(SpellHandle.Sounds.bubbles.ToString());

                                return;


                            }

                        }

                    }

                    break;

                case herbals.trophy_spiral:

                    if (Game1.player.CurrentItem != null)
                    {

                        if (Game1.player.CurrentItem is StardewValley.Object obj)
                        {

                            if (obj.Category == StardewValley.Object.artisanGoodsCategory && obj.Quality < 4)
                            {
                                StardewValley.Item pumped = obj.getOne();

                                List<int> pumpStacks = new()
                                {

                                    obj.Stack,
                                    available,
                                    amount,

                                };

                                int pumpStack = pumpStacks.Min();

                                pumped.Stack = pumpStack;

                                pumped.Quality = 4;

                                if (!Game1.player.addItemToInventoryBool(pumped))
                                {

                                    Game1.player.dropItem(pumped);

                                }

                                Game1.player.Items.ReduceId(obj.QualifiedItemId, 1);

                                UpdateHerbalism(herbal.herbal, 0 - pumpStack);

                                Game1.playSound(SpellHandle.Sounds.bubbles.ToString());

                                return;

                            }

                        }

                    }

                    break;

                case herbals.trophy_spike:

                    if (Game1.player.CurrentItem == null)
                    {

                        Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                        return;

                    }

                    if (Game1.player.CurrentItem is not StardewValley.Object)
                    {

                        Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                        return;

                    }

                    StardewValley.Object bombHeld = (Game1.player.CurrentItem as StardewValley.Object);

                    List<int> bombStacks = new()
                    {
                        available,
                        amount,

                    };

                    string reduceId = HerbalData.cherrybomb;

                    if (bombHeld.QualifiedItemId == HerbalData.cherrybomb && bombHeld.Stack >= 5)
                    {

                        bombStacks.Add(Game1.player.Items.CountId(HerbalData.cherrybomb) / 5);

                    }
                    else
                    if (bombHeld.QualifiedItemId == HerbalData.bomb && bombHeld.Stack >= 5)
                    {

                        bombStacks.Add(Game1.player.Items.CountId(HerbalData.bomb) / 5);

                        reduceId = HerbalData.bomb;

                    }
                    else
                    {

                        return;

                    }

                    int bombStack = bombStacks.Min();

                    Game1.player.Items.ReduceId(reduceId, bombStack * 5);

                    StardewValley.Object addMega = new StardewValley.Object(HerbalData.megabomb, bombStack * 5);

                    if (!Game1.player.addItemToInventoryBool(addMega))
                    {

                        Game1.player.dropItem(addMega);

                    }

                    UpdateHerbalism(herbal.herbal, 0 - bombStack);

                    Game1.playSound(SpellHandle.Sounds.bubbles.ToString());

                    return;


                case herbals.trophy_seed:

                    List<int> seedStacks = new()
                    {
                        available,
                        amount,

                    };

                    int seedStack = seedStacks.Min();

                    GameLocation seedFarm = Game1.getFarm();

                    List<string> cropList = SpawnData.CropList(seedFarm);

                    List<string> shopList = SpawnData.ShopList(seedFarm);

                    foreach(string crop in shopList)
                    {

                        if (cropList.Contains(crop))
                        {

                            continue;

                        }

                        cropList.Add(crop);

                    }

                    for (int s = 0; s < 3 * seedStack; s++)
                    {

                        StardewValley.Object seedPack = new(cropList[Mod.instance.randomIndex.Next(cropList.Count)],1);

                        if (!Game1.player.addItemToInventoryBool(seedPack))
                        {

                            Game1.player.dropItem(seedPack);

                        }

                    }

                    UpdateHerbalism(herbal.herbal, 0 - seedStack);

                    Game1.playSound(SpellHandle.Sounds.bubbles.ToString());

                    return;

            }

            Game1.playSound(SpellHandle.Sounds.ghost.ToString());

            return;

        }

        public static void RandomOmen(Vector2 position, int chance = 8)
        {

            if (Mod.instance.randomIndex.Next(chance) != 0)
            {

                return;

            }

            herbals omen = (herbals)((int)herbals.omen_feather + Mod.instance.randomIndex.Next(9));
        
            new ThrowHandle(Game1.player, position, omen, 1).register();

        }
        
        public static void RandomTrophy(Vector2 position, int chance = 8)
        {

            if (Mod.instance.randomIndex.Next(chance) != 0)
            {

                return;

            }

            herbals trophy = (herbals)((int)herbals.trophy_shroom + Mod.instance.randomIndex.Next(9));

            new ThrowHandle(Game1.player, position, trophy, 1).register();

        }        
        
        public static herbals RandomHerbal(Vector2 position)
        {

            herbals loot = (herbals)((int)herbals.omen_feather + Mod.instance.randomIndex.Next(18));

            /*int number = 1;

            switch (Mod.instance.randomIndex.Next(12))
            {

                case 1:
                    loot = herbals.satius_ligna;
                    number = 2;
                    break;
                case 2:
                    loot = herbals.satius_impes;
                    number = 2;
                    break;
                case 3:
                    loot = herbals.satius_celeri;
                    number = 2;
                    break;
                case 4:
                case 5:
                case 6:
                    loot = (herbals)((int)herbals.coruscant + Mod.instance.randomIndex.Next(10));
                    break;

            }*/

            new ThrowHandle(Game1.player, position, loot, 1).register();

            return loot;

        }

        public static herbals RandomPowder()
        {

            herbals powder;

            int tier = 0;

            if (RelicData.HasRelic(IconData.relics.herbalism_still))
            {

                tier = Mod.instance.randomIndex.Next(2);

            }

            if (RelicData.HasRelic(IconData.relics.monsterbadge))
            {

                tier = Mod.instance.randomIndex.Next(3);

            }

            switch (tier)
            {
                default:
                case 0:

                    powder = HerbalHandle.herbals.imbus + Mod.instance.randomIndex.Next(4);

                    break;

                case 1:

                    powder = HerbalHandle.herbals.concutere + Mod.instance.randomIndex.Next(4);

                    break;

                case 2:

                    if (!Mod.instance.magic)
                    {

                        powder = HerbalHandle.herbals.captis + Mod.instance.randomIndex.Next(5);

                        break;

                    }

                    powder = HerbalHandle.herbals.imbus + Mod.instance.randomIndex.Next(5);

                    break;

            }

            return powder;

        }

    }

    public class Herbal
    {

        public HerbalHandle.herbals herbal = HerbalHandle.herbals.none;

        public IconData.potions display = IconData.potions.ligna;

        public IconData.potions grayed = IconData.potions.lignaGray;

        public string title;

        public string description;

        public List<string> ingredients = new();

        public List<HerbalHandle.herbals> bases = new();

        public List<string> details = new();

        public Dictionary<HerbalHandle.herbals, int> potions = new();

        public List<string> potionrequirements = new();

        public Dictionary<string, int> amounts = new();

        public List<string> itemrequirements = new();

        public int level;

        public int duration;

        public HerbalBuff.herbalbuffs buff = HerbalBuff.herbalbuffs.none;

        public int health;

        public int stamina;

        public string staminaReadout;

        public int price;

        public ExportHandle.exports export = ExportHandle.exports.potions;

        public int units;

        public int craftable;

        public enum herbalType
        {
            resource,
            potion,
            powder,
            ball,
            omen,
            trophy
        }

        public herbalType type = herbalType.potion;

    }

    public class HerbalBuff : Buff
    {

        public enum herbalbuffs
        {
            none,
            alignment,
            vigor,
            celerity,
            imbuement,
            amorous,
            donor,
            rapidfire,
            concussion,
            jumper,
            feline,
            sanctified,
            capture,
            spellcatch,
        }

        public Dictionary<herbalbuffs, HerbalApplied> applied = new();

        public HerbalBuff()
        : base(
                184652.ToString(),
                source: Mod.instance.Helper.Translation.Get("HerbalData.1313"),
                displaySource: Mod.instance.Helper.Translation.Get("HerbalData.1314"),
                duration: ENDLESS,
                iconTexture: Mod.instance.iconData.displayTexture,
                iconSheetIndex: 5,
                displayName: StringData.Strings(StringData.stringkeys.herbalBuffDescription)
                )
        {


        }

        public void apply(herbalbuffs herbal, int level, int duration)
        {

            if (!applied.ContainsKey(herbal))
            {

                applied[herbal] = new HerbalApplied();

            }

            applied[herbal].counter += duration;

            if (applied[herbal].level >= level)
            {

                return;

            }

            applied[herbal].level = level;

            switch (herbal)
            {

                case herbalbuffs.alignment:

                    effects.MagneticRadius.Set(level * 32);

                    Game1.player.buffs.Dirty = true;

                    break;

                case herbalbuffs.celerity:

                    effects.Speed.Set(0.25f * level);

                    Game1.player.buffs.Dirty = true;

                    break;
                    
            }

        }

        public void dismiss(herbalbuffs herbal)
        {

            switch (herbal)
            {

                case herbalbuffs.alignment:

                    effects.MagneticRadius.Set(0f);

                    Game1.player.buffs.Dirty = true;

                    break;

                case herbalbuffs.celerity:

                    effects.Speed.Set(0f);

                    Game1.player.buffs.Dirty = true;

                    break;

            }


        }

        public void check()
        {

            for (int i = applied.Count - 1; i >= 0; i--)
            {

                KeyValuePair<herbalbuffs, HerbalApplied> herbBuff = applied.ElementAt(i);

                applied[herbBuff.Key].counter -= 1;

                if (applied[herbBuff.Key].counter <= 0)
                {

                    dismiss(herbBuff.Key);

                    applied.Remove(herbBuff.Key);

                }

            }

        }

        public void draw(SpriteBatch b)
        {

            List<string> description = new();

            for (int i = 0; i < applied.Count; i++)
            {

                KeyValuePair<herbalbuffs, HerbalApplied> herbBuff = applied.ElementAt(i);

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

                switch (herbBuff.Key)
                {

                    case herbalbuffs.alignment:

                        description.Add( Mod.instance.Helper.Translation.Get("HerbalData.1266") + level  + expire);

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.2"));

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.374.1"));

                        break;

                    case herbalbuffs.vigor:

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.1274") + level + expire);

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.4"));

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.374.2"));

                        break;

                    case herbalbuffs.celerity:

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.1282") + level + expire);

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.6"));

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.374.3"));

                        break;

                    case herbalbuffs.imbuement:

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.361.29") + level + expire);

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.373.1"));

                        break;

                    case herbalbuffs.amorous:

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.361.30") + level + expire);

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.373.2"));

                        break;

                    case herbalbuffs.donor:

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.361.31") + level + expire);

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.373.3"));

                        break;

                    case herbalbuffs.rapidfire:

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.386.29") + level + expire);

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.386.30"));

                        break;

                    case herbalbuffs.concussion:

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.361.32") + level + expire);

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.373.4"));

                        break;

                    case herbalbuffs.jumper:

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.361.33") + level + expire);

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.373.5"));

                        break;

                    case herbalbuffs.feline:

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.361.34") + level + expire);

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.373.6"));

                        break;

                    case herbalbuffs.sanctified:

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.386.31") + level + expire);

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.386.32"));

                        break;

                    case herbalbuffs.capture:

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.386.33") + level + expire);

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.386.34"));

                        break;

                    case herbalbuffs.spellcatch:

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.399.5") + level + expire);

                        description.Add(Mod.instance.Helper.Translation.Get("HerbalData.399.6"));

                        break;
                }

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

            Color outerTop = new(167, 81, 37);

            Color outerBot = new(139, 58, 29);

            Color inner = new(246, 146, 30);

            //textPosition += 12;

            // -------------------------------------------------------
            // details

            if (description.Count > 0)
            {

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition, 488, 2), outerTop);

                b.Draw(Game1.staminaRect, new Rectangle((int)textMargin - 4, (int)textPosition + 2, 488, 3), inner);

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


    }

    public class HerbalApplied
    {

        public HerbalBuff.herbalbuffs buff = HerbalBuff.herbalbuffs.none;

        public int counter;

        public int level;

    }


}
