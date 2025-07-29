using StardewDruid.Cast;
using StardewDruid.Handle;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Data
{

    public class MasterySection
    {

        public enum sectionTypes
        {
            alchemy, // potions, powders, omens, trophies, drop rate etc
            industry, // rite effects that target farm features and environment,  guilds and production, rewards from quests
            druidry, // power, radius, crit chance, cast cost
            community, // companions, monsters, recruits, effects that target villagers
            curiosity, // boosts to various contextual rite effects
        }

        public sectionTypes type;

        public string name;

        public string description;

    }

    public class MasteryNode
    {

        public enum masteries
        {
            
            //alchemy
            potions, // duration, restoration
            powders, // duration
            alchemy,
            witch,

            //industry
            economy,
            experience,
            voide,
            weald,

            //druidry
            power,
            reach,
            winds,
            stars,

            //community
            friendship,
            incentive,
            creatures,
            fates,

            //curiosity
            critical,
            boundless,
            mists,
            ether,

        }

        public masteries mastery;

        public Rite.Rites rite = Rite.Rites.none;

        public string name;

        public string description;

        public List<string> levels;

        public static Microsoft.Xna.Framework.Rectangle MasteryRectangles(masteries mastery)
        {

            int slot = (int)mastery;

            return new(slot % 10 * 16, slot == 0 ? 64 : 64 + (slot / 10 * 16), 16, 16);

        }

    }

    public class MasteryData
    {

        public static Dictionary<MasterySection.sectionTypes, MasterySection> SectionList()
        {

            Dictionary<MasterySection.sectionTypes, MasterySection> sections = new();


            sections[MasterySection.sectionTypes.alchemy] = new()
            {

                type = MasterySection.sectionTypes.alchemy,

                name = "Alchemy",

                description = "Mastery of alchemical techniques, with benefits to stamina cost, buff duration, effect chance, production costs and drop rates for apothecary and alchemy items."

            };

            sections[MasterySection.sectionTypes.industry] = new()
            {

                type = MasterySection.sectionTypes.industry,

                name = "Industry",

                description = "Mastery of industrial and agricultural techniques, with benefits to the cultivation and harvesting of crops, the production of machines and the yield of various rite effects."

            };

            sections[MasterySection.sectionTypes.druidry] = new()
            {

                type = MasterySection.sectionTypes.druidry,

                name = "Druidry",

                description = "Mastery of druidic rites, with benefits to the power and effect of all rites."

            };

            sections[MasterySection.sectionTypes.community] = new()
            {

                type = MasterySection.sectionTypes.community,

                name = "Community",

                description = "Mastery of community based rite effects, with benefits to friendship gain with villagers, creature training, and the power and ability of all companions."

            };

            sections[MasterySection.sectionTypes.curiosity] = new()
            {

                type = MasterySection.sectionTypes.curiosity,

                name = "Curiosity",

                description = "Mastery of bespoke rite effects and possibilities, with benefits to critical hits and the special qualities of rite effects."

            };

            return new();

        }

        public static Dictionary<MasteryNode.masteries, MasteryNode> NodeList()
        {

            Dictionary<MasteryNode.masteries, MasteryNode> nodes = new();

            // ------------------------------------------------------

            nodes[MasteryNode.masteries.potions] = new()
            {

                mastery = MasteryNode.masteries.potions,

                name = "Potioncraft",

                description = "Enhance the effectiveness of potions and their production.",

                levels = new()
                {
                    "After gaining a potion or powder buff, the next channelled rite effect does not consume stamina.",
                    "Two potions or powders (coruscant and captis line) can be combined in alchemy to produce a higher grade product.",
                    "Consumption of Viscosa will extend the duration of active liquor and food buffs by 1 hour.",
                    "Twice as much Faeth, Aether, Coruscant and Voil is produced from the same resources.",
                },

            };

            nodes[MasteryNode.masteries.powders] = new()
            {

                mastery = MasteryNode.masteries.powders,

                name = "Powdercraft",

                description = "Enhance the effectiveness of powders.",

                levels = new()
                {
                    "Potion and powder buff durations increased by 25%.",
                    "20% chance that a throw, toss or windslash will not consume items or stamina.",
                    "Energy costs of rite casts are decreased by 5% for each powder buff active.",
                    "Damage output from druid sources is increased by 5% for each powder buff active.",
                },

            };

            nodes[MasteryNode.masteries.alchemy] = new()
            {

                mastery = MasteryNode.masteries.alchemy,

                name = "Alchemy",

                description = "Improve the sourcing and use of apothecary and alchemy items.",

                levels = new()
                {
                    "Provides a 10% chance that the result of an alchemical process will be doubled (with some exceptions).",
                    "Increases drop rate of omens and trophies by 20%, with an increased chance the drop will be of an item not currently held.",
                    "Provides a 20% chance for a random amount of byproduct to be produced from an alchemical process.",
                    "Uncover a new list of forbidden alchemical techniques."
                },

            };

            nodes[MasteryNode.masteries.witch] = new()
            {

                mastery = MasteryNode.masteries.witch,

                name = "Witchery",

                rite = Rite.Rites.witch,

                description = "Master the use of rituals and familiars (requires Rite of the Witch).",

                levels = new()
                {
                    "Familiars acquire lethality against flying foes.",
                    "Familiars are more effective at completing targetted tasks such as picking, pecking and pickpocketing.",
                    "Rituals have a much greater chance of producing an intended outcome.",
                    "The Gentle Touch effect in locations blessed by a Ritual has a chance to summon a Spirit Animal.",
                },

            };

            // ------------------------------------------------------

            nodes[MasteryNode.masteries.economy] = new()
            {

                mastery = MasteryNode.masteries.economy,

                name = "Economics",

                description = "Improve the Circle of Druid's standing with various guilds operating within the valley (some masteries require later content).",

                levels = new()
                {
                    "Average yield from all druid sources, including gardens and rite effects, increased by 20%.",
                    "Shipping an amount of one good temporarily increases demand (and unit price) for an alternative good by proportion.",
                    "Offsets the impact of seasonal changes on the demand (and subsequently unit price) for goods by 20%.",
                    "Provides a 25% chance that double the amount of goods crates are created when using 'send to goods' option in the apothecary and alchemy menus.",                    
                },

            };

            nodes[MasteryNode.masteries.experience] = new()
            {

                mastery = MasteryNode.masteries.experience,

                name = "Practice",

                description = "Gain insights into adjacent disciplines by practicing druidry.",

                levels = new()
                {
                    "Completion of a quest or lesson milestone restores all health and stamina.",
                    "Experience gained in base skills as a result of rite effects is increased by 25%.",
                    "Experience gained towards druid masteries is increased by 25%.",
                    "Gain a buff that provides +1 to a base skill for every twenty mastery points currently possessed.",
                },

            };

            nodes[MasteryNode.masteries.weald] = new()
            {

                mastery = MasteryNode.masteries.weald,

                name = "Wealdwight",

                rite = Rite.Rites.weald,

                description = "Develop the capacity to channel increasingly large amounts of wild energy (requires Rite of the Weald).",

                levels = new()
                {
                    "The Wilderness effect has a chance to spawn seasonal flowers and rare tree specimens.",
                    "The Cultivate effect applies better fertiliser to crops.",
                    "The Rockfall effect can spawn gem or ore nodes that can be targetted by Resonance.",
                    "The Cultivate effect boosts the growth of trees and crops from seeds by two stages (with some exceptions).",
                },

            };

            nodes[MasteryNode.masteries.voide] = new()
            {

                mastery = MasteryNode.masteries.voide,

                name = "Voidesight",

                rite = Rite.Rites.voide,

                description = "Deepen the connection to the great expanse and its boundless possibilities (requires Rite of the Voide).",

                levels = new()
                {
                    "Opponents in proximity to the exit point of a successful warp are dazed and damaged.",
                    "Continuous casts or channelling of Rite of the Voide produces an energy shield.",
                    "Gain the ability to instantly banish invaders and guardians through the sacrifice of an omen or trophy.",
                    "The channelled Gravity Well effect drains essences from monsters and the environment.",
                },

            };

            // ------------------------------------------------------

            nodes[MasteryNode.masteries.power] = new()
            {

                mastery = MasteryNode.masteries.power,

                name = "Potency",

                description = "Increase the power of druidic rites.",

                levels = new()
                {
                    "Damage output from druid sources increased by 25%.",
                    "Each level in the base skills provides 2% increase in damage output from druid sources.",
                    "Damage output from druid sources increased by a further 25% (stacks with level one).",
                    "The damage stat from the currently equipped weapon is added to the gross damage output from druid sources.",
                },

            };

            nodes[MasteryNode.masteries.reach] = new()
            {

                mastery = MasteryNode.masteries.reach,

                name = "Reach",

                description = "Increase the effective range and extent of druidic rites.",

                levels = new()
                {
                    "Increases the targetting range of all targetted rite effects by 30%.",
                    "Casting cooldowns for rite effects are decreased by 20%.",
                    "Once per day limits on all effects are all reset at 1600hours (4pm) every day.",
                    "Increases the tile radius of all rite effects by 1 (where possible).",

                },

            };

            nodes[MasteryNode.masteries.winds] = new()
            {

                mastery = MasteryNode.masteries.winds,

                name = "Windforce",

                rite = Rite.Rites.winds,

                description = "Develop the impact and utility of the windslash effect.",

                levels = new()
                {
                    "Windslash power level increased to 2 (consistent with copper tools and level 3 weapons).",
                    "Windslash power level increased to 3 (consistent with steel tools and level 6 weapons).",
                    "Windslash power level increased to 4 (consistent with gold tools and level 9 weapons).",
                    "Windslash power level increased to 5 (consistent with iridium tools and level 12 weapons).",
                },

            };

            nodes[MasteryNode.masteries.stars] = new()
            {

                mastery = MasteryNode.masteries.stars,

                name = "Starcalls",

                rite = Rite.Rites.stars,

                description = "Master the ability to draw destructive power from the celestial realm. (requires Rite of the Stars).",

                levels = new()
                {
                    "Meteorites will prioritise stone nodes and monster positions in the casting area.",
                    "Reduces the stamina cost of casts of meteor rain by 30%.",
                    "Chance for an additional meteorite be generated to strike a random target in the casting area.",
                    "A barrage of smaller meteorites will be generated by the channelled Comet effect (in addition to the comet).",
                },

            };

            // ------------------------------------------------------


            nodes[MasteryNode.masteries.friendship] = new()
            {

                mastery = MasteryNode.masteries.friendship,

                name = "Friendliness",

                description = "Improve the receptiveness of villagers to effects from druid sources.",

                levels = new()
                {
                    "Completing quests at the beach, in town, and the mountain, in the forest will provide friendship boosts with an associated set of villagers.",
                    "Chance for all druid companions to obtain and offer a gift when accompanying the player over a long duration.",
                    "Increases the amount of friendship gained with villagers from all druid sources by 50%.",
                    "The base level of all new druid companions, villager recruits and creatures is increased from 1 to 3, with the equivalent amount of experience provided to existing companions.",
                },

            };

            nodes[MasteryNode.masteries.incentive] = new()
            {

                mastery = MasteryNode.masteries.incentive,

                name = "Incentive",

                description = "Increases the rewards provided by all druid sources.",

                levels = new()
                {
                    "10% chance that extra items will be included in the yields from druid sources.",
                    "The monetary rewards from quests, battles and other druid sources is increased by 20%.",
                    "Chance for rare, iridium and difficult-to-obtain items to be included in the yield from druid sources.",
                    "Chance for a random bookseller book, library book or cooking recipe to be included in the yields from druid sources.",

                },
               
            };

            nodes[MasteryNode.masteries.creatures] = new()
            {

                mastery = MasteryNode.masteries.creatures,

                name = "Beasttaming",

                description = "Accelerate the development and ability of tamed creatures.",

                levels = new()
                {
                    "The captis line of powders gains a 20% base chance to capture, and frequency of creature encounters from Gentle Touch effect is increased.",
                    "Creatures tasked to rest, roam or wander will search for a random apothecary or alchemy item once per day.",
                    "Experience gained by all druid companions is increased by 30%.",
                    "All battle ready creatures gain a 20% boost to their stats.",
                },

            };

            nodes[MasteryNode.masteries.fates] = new()
            {

                mastery = MasteryNode.masteries.fates,

                name = "Fatemaker",

                rite = Rite.Rites.fates,

                description = "Increase the ability to enact the will of the High Court of the Fates and Chaos (requires Rite of the Fates).",

                levels = new()
                {
                    "Decreases the time required for a target to recover between applications of curses.",
                    "Unlocks additional Tricks and prevents friendship lost from unliked Tricks.",
                    "Faeth consumption is reduced enchantments that trigger on features adjacent to the initial target.",
                    "Increase the amount of strikes against targets of the Warpstrike effect by at least one per target.",
                },

            };

            // ------------------------------------------------------

            nodes[MasteryNode.masteries.critical] = new()
            {

                mastery = MasteryNode.masteries.critical,

                name = "Lethality",

                description = "Increase the occurance and potency of critical hits.",

                levels = new()
                {
                    "The critical chance and hit stats from the equipped tool or weapon are accounted for in druid damage calculations.",
                    "Base critical hit chance for druid damage sources increased to 15%.",
                    "Critical hit power is boosted by the player's combat professions and tool enchantments.",
                    "Critical hits from druid sources provide a blessing of health, stamina and gold proportional to the total damage.",
                },

            };

            nodes[MasteryNode.masteries.boundless] = new()
            {

                mastery = MasteryNode.masteries.boundless,

                name = "Boundlessness",

                description = "Garner special boosts to the effects of various rites.",

                levels = new()
                {
                    "Casting while amidst grass, crops or next to trees provides a temporary speed boost.",
                    "While on the farm, stamina costs for casting and channelling any effect are greatly reduced.",
                    "Damage against enemies with debuffs applied by druid sources is increased by 25%.",
                    "A killing spree counter is gained from consecutive and recurring acts of devastation. A massive buff is provided at 50 counts."
                },

            };

            nodes[MasteryNode.masteries.mists] = new()
            {

                mastery = MasteryNode.masteries.mists,

                name = "Mistwalker",

                rite = Rite.Rites.mists,

                description = "Intensify the mystical properties of the mists (requires Rite of Mists).",

                levels = new()
                {
                    "Sunder and lethal hits from Smite provide a temporary speed boost.",
                    "The spawn and jump rate of fish creatures effected by Rite of the Mists is increased by 50%.",
                    "The Smite effect gains an additional 20% critical hit chance.",
                    "Effectiveness of the shock debuff is increased and applied more regularly by Rite Of The Mists effects.",
                },

            };

            nodes[MasteryNode.masteries.ether] = new()
            {

                mastery = MasteryNode.masteries.ether,

                name = "Etherealism",

                rite = Rite.Rites.ether,

                description = "Become a powerful conduit for the great streams of ether (requires Rite of Ether).",

                levels = new()
                {
                    "Dragon form provides a +5 defense buff.",
                    "Gain a 20% boost to base stats for battles in dragon form.",
                    "Dragon form melee attacks do not consume stamina, and can be combined with dragonfire to produce a sweeping blast effect.",
                    "Dragon flight gains momentum over extended flight paths. Momentum increases the damage output of landing strikes.",
                },

            };

            return new();

        }

    }



}
