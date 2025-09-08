using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Mists;
using StardewDruid.Handle;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Data
{

    public class MasteryDiscipline
    {

        public enum disciplines
        {
            alchemy, // potions, powders, omens, trophies, drop rate etc
            industry, // rite effects that target farm features and environment,  guilds and production, rewards from quests
            druidry, // power, radius, crit chance, cast cost
            community, // companions, monsters, recruits, effects that target villagers
            curiosity, // boosts to various contextual rite effects
        }

        public disciplines discipline;

        public string name;

        public string description;

        public List<MasteryPath.paths> paths;

        public string technical;

        public static Microsoft.Xna.Framework.Rectangle DisciplineRectangles(disciplines discipline)
        {

            return new((int)discipline * 32, 0, 32, 64);

        }

    }

    public class MasteryPath
    {

        public enum paths
        {
            
            //alchemy
            potions, // duration, restoration
            powders, // duration
            alchemy,
            witch,

            //industry
            experience,
            weald,
            economy,
            voide,

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

        public enum icons
        {
            cross,
            knot,
            vortex,
            yoba,
            sword,
            potion,
            flame,
            book,
            token,
            bomb,
            tree,
            heart,
            winds,
            weald,
            mists,
            stars,
            voide,
            fates,
            ether,
            witch,
        }

        public paths path;

        public icons icon;

        public MasteryDiscipline.disciplines discipline;

        public Rite.Rites rite = Rite.Rites.none;

        public string name;

        public string description;

        public Dictionary<int,MasteryNode.nodes> nodes;

        public static Microsoft.Xna.Framework.Rectangle PathRectangles(icons icon, int level)
        {

            int slot = (int)icon * 16;

            return new(level * 16, slot + 64, 16, 16);

        }

    }

    public class MasteryNode
    {

        public enum nodes
        {

            // potions
            potion_free, 
            potion_doubling, 
            potion_excess, 
            potion_bolster,

            // powders
            powder_packing,
            powder_freeshot,
            powder_synergy,
            powder_stacking,

            // alchemy
            alchemy_gathering,
            alchemy_separate,
            alchemy_byproduct,
            alchemy_enchant,

            // witchery
            witch_predators,
            witch_familiars,
            witch_intercessions,
            witch_spirits,

            // experience
            experience_oldschool,
            experience_discipline,
            experience_training,
            experience_demonstration,

            // weald
            weald_wildgrowth,
            weald_touch,
            weald_rockfall,
            weald_cultivate,

            // economy
            economy_overhead,
            economy_demand,
            economy_flexibility,
            economy_production,

            // voide
            voide_whisk,
            voide_shield,
            voide_banishment,
            voide_drain,

            // power
            power_ascension,
            power_fitness,
            power_weapon,
            power_relics,

            // reach
            reach_range,
            reach_cooldowns,
            reach_reset,
            reach_radius,

            // winds
            winds_copper,
            winds_steel,
            winds_gold,
            winds_iridium,

            // stars
            stars_priority,
            stars_capacity,
            stars_wild,
            stars_calamity,
            
            // friendship
            friendship_communities,
            friendship_gifts,
            friendship_gains,
            friendship_readiness,

            // incentive
            incentive_value,
            incentive_bonus,
            incentive_shinies,
            incentive_quality,

            // creatures
            creatures_touch,
            creatures_productive,
            creatures_learners,
            creatures_battleready,

            // fates
            fates_linger,
            fates_trickster,
            fates_domination,
            fates_warpstriker,

            // critical
            critical_daggercraft,
            critical_precision,
            critical_combatant,
            critical_looter,

            // boundless
            boundless_freneticism,
            boundless_home,
            boundless_cook,
            boundless_glory,

            // mists
            mists_buzz,
            mists_ripples,
            mists_smite,
            mists_shock,

            // ether
            ether_scales,
            ether_ferocity,
            ether_firesweep,
            ether_momentum,

        }

        public nodes mastery;

        public MasteryPath.paths path;

        public string name;

        public string description;

        public int cost = -1;

    }


    public class MasteryData
    {

        public static Dictionary<MasteryDiscipline.disciplines, MasteryDiscipline> DisciplineList()
        {

            Dictionary<MasteryDiscipline.disciplines, MasteryDiscipline> sections = new();


            sections[MasteryDiscipline.disciplines.alchemy] = new()
            {

                discipline = MasteryDiscipline.disciplines.alchemy,

                name = "Alchemy",

                description = "Mastery of alchemical techniques, with benefits to stamina cost, buff duration, effect chance, production costs and drop rates for apothecary and alchemy items.",

                technical = "Replenishment from potions is increased with each level in this discipline",

                paths = new()
                {
                    MasteryPath.paths.potions, // duration, restoration
                    MasteryPath.paths.powders, // duration
                    MasteryPath.paths.alchemy,
                    MasteryPath.paths.witch,
                }

            };

            sections[MasteryDiscipline.disciplines.industry] = new()
            {

                discipline = MasteryDiscipline.disciplines.industry,

                name = "Industry",

                description = "Mastery of industrial and agricultural techniques, with benefits to the cultivation and harvesting of crops, the production of machines and the yield of various rite effects.",

                technical = "Stamina costs for Druidic Rites are lessened with each level in this discipline",

                paths = new()
                {
 
                    MasteryPath.paths.experience,
                    MasteryPath.paths.weald,
                    MasteryPath.paths.economy,
                    MasteryPath.paths.voide,

                }

            };

            sections[MasteryDiscipline.disciplines.druidry] = new()
            {

                discipline = MasteryDiscipline.disciplines.druidry,

                name = "Druidry",

                description = "Mastery of druidic rites, with benefits to the power and effect of all rites.",

                technical = "Base damage from Druidic sources is increased with each level in this discipline",

                paths = new()
                {
                    MasteryPath.paths.power,
                    MasteryPath.paths.reach,
                    MasteryPath.paths.winds,
                    MasteryPath.paths.stars,
                }

            };

            sections[MasteryDiscipline.disciplines.community] = new()
            {

                discipline = MasteryDiscipline.disciplines.community,

                name = "Community",

                description = "Mastery of community based rite effects, with benefits to friendship gain with villagers, creature training, and the power and ability of all companions.",

                technical = "Defense against attacks from enemies of the Circle is increased with each level in this discipline",

                paths = new()
                {
                    MasteryPath.paths.friendship,
                    MasteryPath.paths.incentive,
                    MasteryPath.paths.creatures,
                    MasteryPath.paths.fates,
                }

            };

            sections[MasteryDiscipline.disciplines.curiosity] = new()
            {

                discipline = MasteryDiscipline.disciplines.curiosity,

                name = "Curiosity",

                description = "Mastery of bespoke rite effects and possibilities, with benefits to critical hits and the special qualities of rite effects.",

                technical = "Critical Hit Power is increased with each level in this discipline",

                paths = new()
                {
                    MasteryPath.paths.critical,
                    MasteryPath.paths.boundless,
                    MasteryPath.paths.mists,
                    MasteryPath.paths.ether,
                }

            };

            return sections;

        }

        public static Dictionary<MasteryPath.paths, MasteryPath> PathList()
        {

            Dictionary<MasteryPath.paths, MasteryPath> paths = new();

            // ------------------------------------------------------

            paths[MasteryPath.paths.potions] = new()
            {

                icon = MasteryPath.icons.potion,

                path = MasteryPath.paths.potions,

                discipline = MasteryDiscipline.disciplines.alchemy,

                name = "Potioncraft",

                description = "Enhance the effectiveness of potions and their production.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.potion_free,
                    [1] = MasteryNode.nodes.potion_doubling,
                    [2] = MasteryNode.nodes.potion_excess,
                    [3] = MasteryNode.nodes.potion_bolster,
                }

            };

            paths[MasteryPath.paths.powders] = new()
            {

                icon = MasteryPath.icons.bomb,

                path = MasteryPath.paths.powders,

                discipline = MasteryDiscipline.disciplines.alchemy,

                name = "Powdercraft",

                description = "Enhance the effectiveness of powders and the buffs they provide.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.powder_packing,
                    [1] = MasteryNode.nodes.powder_freeshot,
                    [2] = MasteryNode.nodes.powder_synergy,
                    [3] = MasteryNode.nodes.powder_stacking,
                }

            };

            paths[MasteryPath.paths.alchemy] = new()
            {

                icon = MasteryPath.icons.book,

                path = MasteryPath.paths.alchemy,

                discipline = MasteryDiscipline.disciplines.alchemy,

                name = "Alchemy",

                description = "Improve the sourcing and use of apothecary and alchemy items.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.alchemy_gathering,
                    [1] = MasteryNode.nodes.alchemy_separate,
                    [2] = MasteryNode.nodes.alchemy_byproduct,
                    [3] = MasteryNode.nodes.alchemy_enchant,
                }

            };

            paths[MasteryPath.paths.witch] = new()
            {

                icon = MasteryPath.icons.witch,

                path = MasteryPath.paths.witch,

                discipline = MasteryDiscipline.disciplines.alchemy,

                name = "Witchery",

                rite = Rite.Rites.witch,

                description = "Master the use of intercessions and familiars.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.witch_predators,
                    [1] = MasteryNode.nodes.witch_familiars,
                    [2] = MasteryNode.nodes.witch_intercessions,
                    [3] = MasteryNode.nodes.witch_spirits,
                }

            };

            // ------------------------------------------------------

            paths[MasteryPath.paths.experience] = new()
            {

                icon = MasteryPath.icons.cross,

                path = MasteryPath.paths.experience,

                discipline = MasteryDiscipline.disciplines.industry,

                name = "Practice",

                description = "Gain insights into adjacent disciplines by practicing druidry.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.experience_oldschool,
                    [1] = MasteryNode.nodes.experience_discipline,
                    [2] = MasteryNode.nodes.experience_training,
                    [3] = MasteryNode.nodes.experience_demonstration,
                }

            };

            paths[MasteryPath.paths.weald] = new()
            {

                icon = MasteryPath.icons.weald,

                path = MasteryPath.paths.weald,

                discipline = MasteryDiscipline.disciplines.industry,

                name = "Wealdwight",

                rite = Rite.Rites.weald,

                description = "Develop the capacity to channel increasingly large amounts of wild energy.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.weald_wildgrowth,
                    [1] = MasteryNode.nodes.weald_touch,
                    [2] = MasteryNode.nodes.weald_rockfall,
                    [3] = MasteryNode.nodes.weald_cultivate,
                }

            };

            paths[MasteryPath.paths.economy] = new()
            {

                icon = MasteryPath.icons.token,

                path = MasteryPath.paths.economy,

                discipline = MasteryDiscipline.disciplines.industry,

                name = "Economics",

                rite = Rite.Rites.voide,

                description = "Improve the Circle of Druid's ability to provision the valley guilds.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.economy_production,
                    [1] = MasteryNode.nodes.economy_demand,
                    [2] = MasteryNode.nodes.economy_overhead,
                    [3] = MasteryNode.nodes.economy_flexibility,
                }

            };

            paths[MasteryPath.paths.voide] = new()
            {

                icon = MasteryPath.icons.voide,

                path = MasteryPath.paths.voide,

                discipline = MasteryDiscipline.disciplines.industry,

                name = "Voidesight",

                rite = Rite.Rites.voide,

                description = "Deepen the connection to the great expanse and its boundless possibilities.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.voide_whisk,
                    [1] = MasteryNode.nodes.voide_shield,
                    [2] = MasteryNode.nodes.voide_banishment,
                    [3] = MasteryNode.nodes.voide_drain,
                }

            };

            // ------------------------------------------------------

            paths[MasteryPath.paths.power] = new()
            {

                icon = MasteryPath.icons.sword,

                path = MasteryPath.paths.power,

                discipline = MasteryDiscipline.disciplines.druidry,

                name = "Potency",

                description = "Increase the power of druidic rites.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.power_ascension,
                    [1] = MasteryNode.nodes.power_fitness,
                    [2] = MasteryNode.nodes.power_weapon,
                    [3] = MasteryNode.nodes.power_relics,
                }

            };

            paths[MasteryPath.paths.reach] = new()
            {

                icon = MasteryPath.icons.vortex,

                path = MasteryPath.paths.reach,

                discipline = MasteryDiscipline.disciplines.druidry,

                name = "Reach",

                description = "Increase the effective range and extent of druidic rites.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.reach_range,
                    [1] = MasteryNode.nodes.reach_cooldowns,
                    [2] = MasteryNode.nodes.reach_reset,
                    [3] = MasteryNode.nodes.reach_radius,
                }

            };

            paths[MasteryPath.paths.winds] = new()
            {

                icon = MasteryPath.icons.winds,

                path = MasteryPath.paths.winds,

                discipline = MasteryDiscipline.disciplines.druidry,

                name = "Windforce",

                rite = Rite.Rites.winds,

                description = "Develop the impact and utility of the windslash effect.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.winds_copper,
                    [1] = MasteryNode.nodes.winds_steel,
                    [2] = MasteryNode.nodes.winds_gold,
                    [3] = MasteryNode.nodes.winds_iridium,
                }

            };

            paths[MasteryPath.paths.stars] = new()
            {

                icon = MasteryPath.icons.stars,

                path = MasteryPath.paths.stars,

                discipline = MasteryDiscipline.disciplines.druidry,

                name = "Starcalls",

                rite = Rite.Rites.stars,

                description = "Master the ability to draw destructive power from the celestial realm.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.stars_priority,
                    [1] = MasteryNode.nodes.stars_capacity,
                    [2] = MasteryNode.nodes.stars_wild,
                    [3] = MasteryNode.nodes.stars_calamity,
                }

            };

            // ------------------------------------------------------


            paths[MasteryPath.paths.friendship] = new()
            {

                icon = MasteryPath.icons.heart,

                path = MasteryPath.paths.friendship,

                discipline = MasteryDiscipline.disciplines.community,

                name = "Friendliness",

                description = "Improve the receptiveness of villagers to effects from druid sources.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.friendship_communities,
                    [1] = MasteryNode.nodes.friendship_gifts,
                    [2] = MasteryNode.nodes.friendship_gains,
                    [3] = MasteryNode.nodes.friendship_readiness,
                }

            };

            paths[MasteryPath.paths.incentive] = new()
            {

                icon = MasteryPath.icons.tree,

                path = MasteryPath.paths.incentive,

                discipline = MasteryDiscipline.disciplines.community,

                name = "Incentive",

                description = "Increases the rewards provided by all druid sources.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.incentive_value,
                    [1] = MasteryNode.nodes.incentive_bonus,
                    [2] = MasteryNode.nodes.incentive_shinies,
                    [3] = MasteryNode.nodes.incentive_quality,
                }

            };

            paths[MasteryPath.paths.creatures] = new()
            {

                icon = MasteryPath.icons.yoba,

                path = MasteryPath.paths.creatures,

                discipline = MasteryDiscipline.disciplines.community,

                name = "Beasttaming",

                description = "Accelerate the development and ability of tamed creatures.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.creatures_touch,
                    [1] = MasteryNode.nodes.creatures_productive,
                    [2] = MasteryNode.nodes.creatures_learners,
                    [3] = MasteryNode.nodes.creatures_battleready,
                }

            };

            paths[MasteryPath.paths.fates] = new()
            {

                icon = MasteryPath.icons.fates,

                path = MasteryPath.paths.fates,

                discipline = MasteryDiscipline.disciplines.community,

                name = "Fatemaker",

                rite = Rite.Rites.fates,

                description = "Increase the ability to enact the will of the High Court of the Fates and Chaos.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.fates_linger,
                    [1] = MasteryNode.nodes.fates_trickster,
                    [2] = MasteryNode.nodes.fates_domination,
                    [3] = MasteryNode.nodes.fates_warpstriker,
                }

            };

            // ------------------------------------------------------

            paths[MasteryPath.paths.critical] = new()
            {

                icon = MasteryPath.icons.flame,

                path = MasteryPath.paths.critical,

                discipline = MasteryDiscipline.disciplines.curiosity,

                name = "Lethality",

                description = "Increase the occurance and potency of critical hits.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.critical_daggercraft,
                    [1] = MasteryNode.nodes.critical_precision,
                    [2] = MasteryNode.nodes.critical_combatant,
                    [3] = MasteryNode.nodes.critical_looter,
                }

            };

            paths[MasteryPath.paths.boundless] = new()
            {

                icon = MasteryPath.icons.knot,

                path = MasteryPath.paths.boundless,

                discipline = MasteryDiscipline.disciplines.curiosity,

                name = "Boundlessness",

                description = "Garner special boosts to the effects of various rites.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.boundless_freneticism,
                    [1] = MasteryNode.nodes.boundless_home,
                    [2] = MasteryNode.nodes.boundless_cook,
                    [3] = MasteryNode.nodes.boundless_glory,
                }

            };

            paths[MasteryPath.paths.mists] = new()
            {

                icon = MasteryPath.icons.mists,

                path = MasteryPath.paths.mists,

                discipline = MasteryDiscipline.disciplines.curiosity,

                name = "Mistwalker",

                rite = Rite.Rites.mists,

                description = "Intensify the mystical properties of the mists.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.mists_buzz,
                    [1] = MasteryNode.nodes.mists_ripples,
                    [2] = MasteryNode.nodes.mists_smite,
                    [3] = MasteryNode.nodes.mists_shock,
                }

            };

            paths[MasteryPath.paths.ether] = new()
            {

                icon = MasteryPath.icons.ether,

                path = MasteryPath.paths.ether,

                discipline = MasteryDiscipline.disciplines.curiosity,

                name = "Etherealism",

                rite = Rite.Rites.ether,

                description = "Become a powerful conduit for the great streams of ether.",

                nodes = new()
                {
                    [0] = MasteryNode.nodes.ether_scales,
                    [1] = MasteryNode.nodes.ether_ferocity,
                    [2] = MasteryNode.nodes.ether_firesweep,
                    [3] = MasteryNode.nodes.ether_momentum,
                }

            };

            return paths;

        }

        public static Dictionary<MasteryNode.nodes, MasteryNode> NodeList()
        {

            Dictionary<MasteryNode.nodes, MasteryNode> nodes = new();

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.potion_free] = new()
            {

                mastery = MasteryNode.nodes.potion_free,

                path = MasteryPath.paths.potions,

                name = "Preparation",

                description = "After gaining a potion or powder buff, the next channelled rite effect does not consume stamina.",

            };

            nodes[MasteryNode.nodes.potion_doubling] = new()
            {

                mastery = MasteryNode.nodes.potion_doubling,

                path = MasteryPath.paths.potions,

                name = "Doubling",

                description = "There is a 10% chance that the result of an alchemical process will be doubled (with some exceptions).",

            };

            nodes[MasteryNode.nodes.potion_excess] = new()
            {

                mastery = MasteryNode.nodes.potion_excess,

                path = MasteryPath.paths.potions,

                name = "Excess",

                description = "Twice as much Faeth, Aether, Coruscant and Voil is produced from the same resources.",

            };

            nodes[MasteryNode.nodes.potion_bolster] = new()
            {

                mastery = MasteryNode.nodes.potion_bolster,

                path = MasteryPath.paths.potions,

                name = "Bolster",

                description = "Viscosa consumption extends the duration of active liquor and food buffs.",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.powder_packing] = new()
            {

                mastery = MasteryNode.nodes.powder_packing,

                path = MasteryPath.paths.powders,

                name = "Packing",

                description = "Potion and powder buff durations increased by 25%.",

            };

            nodes[MasteryNode.nodes.powder_freeshot] = new()
            {

                mastery = MasteryNode.nodes.powder_freeshot,

                path = MasteryPath.paths.powders,

                name = "Free Shot",

                description = "20% chance that a throw, toss or windslash will not consume items or stamina.",

            };

            nodes[MasteryNode.nodes.powder_synergy] = new()
            {

                mastery = MasteryNode.nodes.powder_synergy,

                path = MasteryPath.paths.powders,

                name = "Rite Synergy",

                description = "Energy costs of rite casts are decreased by 5% for each powder buff active.",

            };


            nodes[MasteryNode.nodes.powder_stacking] = new()
            {

                mastery = MasteryNode.nodes.powder_stacking,

                path = MasteryPath.paths.powders,

                name = "Stacking",

                description = "Damage output from druid sources is increased by 5% for each powder buff active.",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.alchemy_gathering] = new()
            {

                mastery = MasteryNode.nodes.alchemy_gathering,

                path = MasteryPath.paths.alchemy,

                name = "Gathering",

                description = "Drop rates of omens, trophies and inventory items from all druid sources is increased, with an increased chance an omen or trophy drop will be of an item not currently held.",

            };

            nodes[MasteryNode.nodes.alchemy_separate] = new()
            {

                mastery = MasteryNode.nodes.alchemy_separate,

                path = MasteryPath.paths.alchemy,

                name = "Separate",

                description = "Unlocks the 'separate' feature of the alchemy runeboard, which can be used to crush geodes, open mystery boxes and deconstruct most items.",

            };

            nodes[MasteryNode.nodes.alchemy_byproduct] = new()
            {

                mastery = MasteryNode.nodes.alchemy_byproduct,

                path = MasteryPath.paths.alchemy,

                name = "Byproduct",

                description = "There is a 10% chance for a random amount of resource byproduct to be produced from an alchemical or distillery process.",

            };

            nodes[MasteryNode.nodes.alchemy_enchant] = new()
            {

                mastery = MasteryNode.nodes.alchemy_enchant,

                path = MasteryPath.paths.alchemy,

                name = "Enchant",

                description = "Unlock the 'enchant' feature of the Alchemy runeboard, enabling processes that allow potion remixing and weapon enchanting.",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.witch_predators] = new()
            {

                mastery = MasteryNode.nodes.witch_predators,

                path = MasteryPath.paths.witch,

                name = "Predators",

                description = "Familiars acquire lethality against flying foes.",

            };

            nodes[MasteryNode.nodes.witch_familiars] = new()
            {

                mastery = MasteryNode.nodes.witch_familiars,

                path = MasteryPath.paths.witch,

                name = "Familiars",

                description = "Familiars are more effective at completing targetted tasks such as picking, pecking and pickpocketing.",

            };

            nodes[MasteryNode.nodes.witch_intercessions] = new()
            {

                mastery = MasteryNode.nodes.witch_intercessions,

                path = MasteryPath.paths.witch,

                name = "Intercessions",

                description = "Intercessions have a much greater chance of producing an intended outcome.",

            };


            nodes[MasteryNode.nodes.witch_spirits] = new()
            {

                mastery = MasteryNode.nodes.witch_spirits,

                path = MasteryPath.paths.witch,

                name = "Great Spirits",

                description = "The Gentle Touch effect in locations blessed by an Intercession has a chance to summon a Greater Spirit.",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.experience_oldschool] = new()
            {

                mastery = MasteryNode.nodes.experience_oldschool,

                path = MasteryPath.paths.experience,

                name = "Old School",

                description =  "Completion of a quest or lesson milestone restores all health and stamina.",

            };

            nodes[MasteryNode.nodes.experience_discipline] = new()
            {

                mastery = MasteryNode.nodes.experience_discipline,

                path = MasteryPath.paths.experience,

                name = "Cross Discipline",

                description = "Experience gained in base skills as a result of rite effects is increased by 25%.",

            };

            nodes[MasteryNode.nodes.experience_training] = new()
            {

                mastery = MasteryNode.nodes.experience_training,

                path = MasteryPath.paths.experience,

                name = "Training",

                description = "Mastery experience gained is increased by 25%.",

            };


            nodes[MasteryNode.nodes.experience_demonstration] = new()
            {

                mastery = MasteryNode.nodes.experience_demonstration,

                path = MasteryPath.paths.experience,

                name = "Demonstration",

                description = "Gain a buff that provides +1 to a base skill for every twenty mastery points currently possessed.",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.weald_wildgrowth] = new()
            {

                mastery = MasteryNode.nodes.weald_wildgrowth,

                path = MasteryPath.paths.weald,

                name = "Specimens",

                description = "The Wild Growth effect has a chance to spawn seasonal flowers and rare tree specimens.",

            };

            nodes[MasteryNode.nodes.weald_touch] = new()
            {

                mastery = MasteryNode.nodes.weald_touch,

                path = MasteryPath.paths.weald,

                name = "Horticulture",

                description = "The Gentle Touch effect provides foraging experience when it touches trees, bushes and other natural features. It will add a day of growth to grass and trees (including fruit trees)",

            };

            nodes[MasteryNode.nodes.weald_rockfall] = new()
            {

                mastery = MasteryNode.nodes.weald_rockfall,

                path = MasteryPath.paths.weald,

                name = "Seam Finding",

                description = "The Rockfall effect can spawn gem or ore nodes that can be targetted by Resonance.",

            };


            nodes[MasteryNode.nodes.weald_cultivate] = new()
            {

                mastery = MasteryNode.nodes.weald_cultivate,

                path = MasteryPath.paths.weald,

                name = "Cultivate",

                description = "The Cultivate effect boosts the growth of crops from seeds by two stages (with some exceptions).",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.economy_production] = new()
            {

                mastery = MasteryNode.nodes.economy_production,

                path = MasteryPath.paths.economy,

                name = "Production",

                description = "Provides a 15% chance that double the amount of resources or goods are created when using the dispatch option in the apothecary and alchemy menus.",

            };

            nodes[MasteryNode.nodes.economy_demand] = new()
            {

                mastery = MasteryNode.nodes.economy_demand,

                path = MasteryPath.paths.economy,

                name = "Demand",

                description = "Shipping an amount of one good temporarily increases demand (and unit price) for an alternative good by proportion.",

            };

            nodes[MasteryNode.nodes.economy_overhead] = new()
            {

                mastery = MasteryNode.nodes.economy_overhead,

                path = MasteryPath.paths.economy,

                name = "Overhead",

                description = "Distillery machines are easier to maintain and require less labour and materials to upgrade.",

            };

            nodes[MasteryNode.nodes.economy_flexibility] = new()
            {

                mastery = MasteryNode.nodes.economy_flexibility,

                path = MasteryPath.paths.economy,

                name = "Flexibility",

                description = "Offsets the impact of seasonal changes on the demand (and subsequently unit price) for goods by 25%.",

            };



            // ------------------------------------------------------

            nodes[MasteryNode.nodes.voide_whisk] = new()
            {

                mastery = MasteryNode.nodes.voide_whisk,

                path = MasteryPath.paths.voide,

                name = "Dazing",

                description = "Opponents in proximity to the exit point of a successful warp are dazed and damaged.",

            };

            nodes[MasteryNode.nodes.voide_shield] = new()
            {

                mastery = MasteryNode.nodes.voide_shield,

                path = MasteryPath.paths.voide,

                name = "Shielding",

                description = "Continuous casts or channelling of Rite of the Voide produces an energy shield.",

            };

            nodes[MasteryNode.nodes.voide_banishment] = new()
            {

                mastery = MasteryNode.nodes.voide_banishment,

                path = MasteryPath.paths.voide,

                name = "Banishment",

                description = "Gain the ability to instantly banish invaders and guardians through the sacrifice of an omen or trophy.",

            };


            nodes[MasteryNode.nodes.voide_drain] = new()
            {

                mastery = MasteryNode.nodes.voide_drain,

                path = MasteryPath.paths.voide,

                name = "Draining",

                description = "The channelled Gravity Well effect drains essences from monsters and the environment.",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.power_ascension] = new()
            {

                mastery = MasteryNode.nodes.power_ascension,

                path = MasteryPath.paths.power,

                name = "Ascension",

                description = "Each rite unlocked provides 5% increase in damage output from druid sources.",

            };

            nodes[MasteryNode.nodes.power_fitness] = new()
            {

                mastery = MasteryNode.nodes.power_fitness,

                path = MasteryPath.paths.power,

                name = "Fitness",

                description = "Each level in the base skills provides 2% increase in damage output from druid sources.",

            };

            nodes[MasteryNode.nodes.power_weapon] = new()
            {

                mastery = MasteryNode.nodes.power_weapon,

                path = MasteryPath.paths.power,

                name = "Weaponmaster",

                description = "The damage stat from the currently equipped weapon is added to the gross damage output from druid sources.",

            };

            nodes[MasteryNode.nodes.power_relics] = new()
            {

                mastery = MasteryNode.nodes.power_relics,

                path = MasteryPath.paths.power,

                name = "Talismans",

                description = "Each relic in the reliquary provides 1% increase in damage output from druid sources.",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.reach_range] = new()
            {

                mastery = MasteryNode.nodes.reach_range,

                path = MasteryPath.paths.reach,

                name = "Range",

                description = "Increases the targetting range of all targetted rite effects by 30%.",

            };

            nodes[MasteryNode.nodes.reach_cooldowns] = new()
            {

                mastery = MasteryNode.nodes.reach_cooldowns,

                path = MasteryPath.paths.reach,

                name = "Cooldowns",

                description = "Casting cooldowns for rite effects are decreased by 20%.",

            };

            nodes[MasteryNode.nodes.reach_reset] = new()
            {

                mastery = MasteryNode.nodes.reach_reset,

                path = MasteryPath.paths.reach,

                name = "Reset",

                description = "Once per day limits on all effects are reset at 1600hours (4pm) every day.",

            };


            nodes[MasteryNode.nodes.reach_radius] = new()
            {

                mastery = MasteryNode.nodes.reach_radius,

                path = MasteryPath.paths.winds,

                name = "Radius",

                description = "Increases the tile radius of all rite effects by 1 (where possible).",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.winds_copper] = new()
            {

                mastery = MasteryNode.nodes.winds_copper,

                path = MasteryPath.paths.winds,

                name = "Windforce 2",

                description = "Windslash power level increased to 2 (consistent with copper tools and level 3 weapons).",

            };

            nodes[MasteryNode.nodes.winds_steel] = new()
            {

                mastery = MasteryNode.nodes.winds_steel,

                path = MasteryPath.paths.winds,

                name = "Windforce 3",

                description = "Windslash power level increased to 3 (consistent with steel tools and level 6 weapons).",

            };

            nodes[MasteryNode.nodes.winds_gold] = new()
            {

                mastery = MasteryNode.nodes.winds_gold,

                path = MasteryPath.paths.winds,

                name = "Windforce 4",

                description = "Windslash power level increased to 4 (consistent with gold tools and level 9 weapons).",

            };


            nodes[MasteryNode.nodes.winds_iridium] = new()
            {

                mastery = MasteryNode.nodes.winds_iridium,

                path = MasteryPath.paths.winds,

                name = "Windforce 5",

                description = "Windslash power level increased to 5 (consistent with iridium tools and level 12 weapons).",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.stars_priority] = new()
            {

                mastery = MasteryNode.nodes.stars_priority,

                path = MasteryPath.paths.stars,

                name = "Priority",

                description = "Meteorites will prioritise stone nodes and monster positions in the casting area.",

            };

            nodes[MasteryNode.nodes.stars_capacity] = new()
            {

                mastery = MasteryNode.nodes.stars_capacity,

                path = MasteryPath.paths.stars,

                name = "Capacity",

                description = "Reduces the stamina cost of casts of meteor rain by 30%.",

            };

            nodes[MasteryNode.nodes.stars_wild] = new()
            {

                mastery = MasteryNode.nodes.stars_wild,

                path = MasteryPath.paths.stars,

                name = "Wild",

                description = "Chance for an additional meteorite be generated to strike a random target in the casting area.",

            };


            nodes[MasteryNode.nodes.stars_calamity] = new()
            {

                mastery = MasteryNode.nodes.stars_calamity,

                path = MasteryPath.paths.stars,

                name = "Calamity",

                description = "A barrage of smaller meteorites will be generated by the channelled Comet effect (in addition to the comet).",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.friendship_communities] = new()
            {

                mastery = MasteryNode.nodes.friendship_communities,

                path = MasteryPath.paths.friendship,

                name = "Communities",

                description = "Completing quests in or near a specific location (beach, town, mountain or forest) will provide friendship boosts with a set of villagers associated with that location.",

            };

            nodes[MasteryNode.nodes.friendship_gifts] = new()
            {

                mastery = MasteryNode.nodes.friendship_gifts,

                path = MasteryPath.paths.friendship,

                name = "Gifts",

                description = "Chance for all druid companions to obtain and offer a gift when accompanying the player over a long duration.",

            };

            nodes[MasteryNode.nodes.friendship_gains] = new()
            {

                mastery = MasteryNode.nodes.friendship_gains,

                path = MasteryPath.paths.friendship,

                name = "Sweet Gains",

                description = "Increases the amount of friendship gained with villagers from all druid sources by 50%.",

            };


            nodes[MasteryNode.nodes.friendship_readiness] = new()
            {

                mastery = MasteryNode.nodes.friendship_readiness,

                path = MasteryPath.paths.friendship,

                name = "Readiness",

                description = "The base level of all new druid companions, villager recruits and creatures is increased from 1 to 3, with the equivalent amount of experience provided to existing companions.",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.incentive_value] = new()
            {

                mastery = MasteryNode.nodes.incentive_value,

                path = MasteryPath.paths.incentive,

                name = "Value",

                description = "10% chance that extra items will be included in the yields from druid sources.",
                

            };

            nodes[MasteryNode.nodes.incentive_bonus] = new()
            {

                mastery = MasteryNode.nodes.incentive_bonus,

                path = MasteryPath.paths.incentive,

                name = "Bonus",

                description = "The monetary rewards from quests, battles and other druid sources is increased by 20%.",
                

            };

            nodes[MasteryNode.nodes.incentive_shinies] = new()
            {

                mastery = MasteryNode.nodes.incentive_shinies,

                path = MasteryPath.paths.incentive,

                name = "Shinies",

                description = "Chance for rare and difficult-to-obtain items to be included in the yield from druid sources.",
                

            };


            nodes[MasteryNode.nodes.incentive_quality] = new()
            {

                mastery = MasteryNode.nodes.incentive_quality,

                path = MasteryPath.paths.incentive,

                name = "Quality",

                description = "Where eligible, items yielded from druid sources will always be highest quality",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.creatures_touch] = new()
            {

                mastery = MasteryNode.nodes.creatures_touch,

                path = MasteryPath.paths.creatures,

                name = "Creature Touch",

                description = "The captis line of powders gains a 20% base chance to capture, and frequency of creature encounters from Gentle Touch effect is increased.",

            };

            nodes[MasteryNode.nodes.creatures_productive] = new()
            {

                mastery = MasteryNode.nodes.creatures_productive,

                path = MasteryPath.paths.creatures,

                name = "Productivity",

                description = "All companions gain a boost to productivity for duties they are assigned to.",

            };

            nodes[MasteryNode.nodes.creatures_learners] = new()
            {

                mastery = MasteryNode.nodes.creatures_learners,

                path = MasteryPath.paths.creatures,

                name = "Quick Learners",

                description = "Experience gained by all druid companions is increased by 30%.",

            };


            nodes[MasteryNode.nodes.creatures_battleready] = new()
            {

                mastery = MasteryNode.nodes.creatures_battleready,

                path = MasteryPath.paths.creatures,

                name = "Battleready",

                description = "All battle ready creatures gain a 20% boost to their stats.",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.fates_linger] = new()
            {

                mastery = MasteryNode.nodes.fates_linger,

                path = MasteryPath.paths.fates,

                name = "Lingering Spirits",

                description = "Trickster spirits may linger, haunting nearby monsters and enchanting additional targets for free.",

            };

            nodes[MasteryNode.nodes.fates_trickster] = new()
            {

                mastery = MasteryNode.nodes.fates_trickster,

                path = MasteryPath.paths.fates,

                name = "Trickster",

                description = "Unlocks additional Tricks and prevents friendship lost from unliked Tricks.",


            };

            nodes[MasteryNode.nodes.fates_domination] = new()
            {

                mastery = MasteryNode.nodes.fates_domination,

                path = MasteryPath.paths.fates,

                name = "Domination",

                description = "Damage is increased by 5% for each nearby enemy with a debuff from druid sources.",


            };


            nodes[MasteryNode.nodes.fates_warpstriker] = new()
            {

                mastery = MasteryNode.nodes.fates_warpstriker,

                path = MasteryPath.paths.fates,

                name = "Warpstriker",

                description = "Increase the amount of strikes against targets of the Warpstrike effect by at least one per target.",

            };

            // ------------------------------------------------------
            
            nodes[MasteryNode.nodes.critical_daggercraft] = new()
            {

                mastery = MasteryNode.nodes.critical_daggercraft,

                path = MasteryPath.paths.critical,

                name = "Daggercraft",

                description = "The critical chance and hit stats from the equipped tool or weapon are accounted for in druid damage calculations.",

            };

            nodes[MasteryNode.nodes.critical_precision] = new()
            {

                mastery = MasteryNode.nodes.critical_precision,

                path = MasteryPath.paths.critical,

                name = "Precision",

                description = "Base critical hit chance for druid damage sources increased to 10%.",

            };

            nodes[MasteryNode.nodes.critical_combatant] = new()
            {

                mastery = MasteryNode.nodes.critical_combatant,

                path = MasteryPath.paths.critical,

                name = "Combatant",

                description = "Critical hit power is boosted by the player's combat professions and tool enchantments.",

            };


            nodes[MasteryNode.nodes.critical_looter] = new()
            {

                mastery = MasteryNode.nodes.critical_looter,

                path = MasteryPath.paths.critical,

                name = "Looter",

                description = "Critical hits from druid sources provide a blessing of health, stamina and gold proportional to the total damage.",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.boundless_freneticism] = new()
            {

                mastery = MasteryNode.nodes.boundless_freneticism,

                path = MasteryPath.paths.boundless,

                name = "Freneticism",

                description = "Casting while amidst grass, crops or next to trees provides a temporary speed boost.",

            };

            nodes[MasteryNode.nodes.boundless_home] = new()
            {

                mastery = MasteryNode.nodes.boundless_home,

                path = MasteryPath.paths.boundless,

                name = "Home",

                description = "While on the farm, stamina costs for casting and channelling any effect are greatly reduced.",

            };

            nodes[MasteryNode.nodes.boundless_cook] = new()
            {

                mastery = MasteryNode.nodes.boundless_cook,

                path = MasteryPath.paths.boundless,

                name = "Cook",

                description = "Chance for enemies slain by debuff-triggered effects such as shock and burn to drop a cooking item and related recipe",

            };

            nodes[MasteryNode.nodes.boundless_glory] = new()
            {

                mastery = MasteryNode.nodes.boundless_glory,

                path = MasteryPath.paths.boundless,

                name = "Glory",

                description = "A unique counter is gained from consecutive and recurring acts of devastation. A massive buff is provided at 50 counts.",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.mists_buzz] = new()
            {

                mastery = MasteryNode.nodes.mists_buzz,

                path = MasteryPath.paths.mists,

                name = "Buzz",

                description = "Sunder and lethal hits from Smite provide a temporary speed boost.",
                

            };

            nodes[MasteryNode.nodes.mists_ripples] = new()
            {

                mastery = MasteryNode.nodes.mists_ripples,

                path = MasteryPath.paths.mists,

                name = "Ripples",

                description = "The spawn and jump rate of fish creatures effected by Rite of the Mists is increased by 50%.",
                

            };

            nodes[MasteryNode.nodes.mists_smite] = new()
            {

                mastery = MasteryNode.nodes.mists_smite,

                path = MasteryPath.paths.mists,

                name = "Smite",

                description = "The Smite effect gains an additional 20% critical hit chance.",
                

            };

            nodes[MasteryNode.nodes.mists_shock] = new()
            {

                mastery = MasteryNode.nodes.mists_shock,

                path = MasteryPath.paths.mists,

                name = "Shocking",

                description = "Effectiveness of the shock debuff is increased and applied more regularly by Rite Of The Mists effects.",

            };

            // ------------------------------------------------------

            nodes[MasteryNode.nodes.ether_scales] = new()
            {

                mastery = MasteryNode.nodes.ether_scales,

                path = MasteryPath.paths.ether,

                name = "Scales",

                description = "Dragon form provides a +5 defense buff.",


            };

            nodes[MasteryNode.nodes.ether_ferocity] = new()
            {

                mastery = MasteryNode.nodes.ether_ferocity,

                path = MasteryPath.paths.ether,

                name = "Ferocity",

                description = "Gain a 20% boost to base stats for battles in dragon form.",


            };

            nodes[MasteryNode.nodes.ether_firesweep] = new()
            {

                mastery = MasteryNode.nodes.ether_firesweep,

                path = MasteryPath.paths.ether,

                name = "Firesweep",

                description = "Dragon form melee attacks do not consume stamina, and can be combined with dragonfire to produce a sweeping blast effect.",


            };

            nodes[MasteryNode.nodes.ether_momentum] = new()
            {

                mastery = MasteryNode.nodes.ether_momentum,

                path = MasteryPath.paths.ether,

                name = "Momentum",

                description = "Dragon flight gains momentum over extended flight paths. Momentum increases the damage output of landing strikes.",

            };

            return nodes;

        }

    }



}
