using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Data
{
    public static class ApothecaryData
    {
        public static Dictionary<ApothecaryHandle.items, ApothecaryItem> ItemList()
        {

            Dictionary<ApothecaryHandle.items, ApothecaryItem> potions = new();

            // ====================================================================
            // Viscosa

            potions[ApothecaryHandle.items.viscosa] = new()
            {

                item = ApothecaryHandle.items.viscosa,

                title = "Viscosa",

                description = "A gloop of unlikely organic compounds stabilised by otherworldly essence",

                recipe = AlchemyRecipe.recipes.viscosa,

                details = new()
                {

                    "Provides the base ingredient for potions",

                },

                type = ApothecaryItem.herbalType.resource,

            };

            // ====================================================================
            // Ligna line

            potions[ApothecaryHandle.items.ligna] = new()
            {

                buff = BuffHandle.buffTypes.alignment,

                item = ApothecaryHandle.items.ligna,

                relic = IconData.relics.herbalism_mortar,

                level = 1,

                duration = 180,

                title = "Ligna",

                description = "Like bark-root tea, with more bark, and root to boot.",

                recipe = AlchemyRecipe.recipes.ligna,

                health = 8,

                stamina = 30,

            }; 

            potions[ApothecaryHandle.items.satius_ligna] = new()
            {

                buff = BuffHandle.buffTypes.alignment,

                item = ApothecaryHandle.items.satius_ligna,

                relic = IconData.relics.herbalism_pan,

                level = 2,

                duration = 240,

                title = "Satius Ligna",

                description = "Potent oils enhance the rich, nutty flavour profile of the base mixture.",

                recipe = AlchemyRecipe.recipes.satius_ligna,

                health = 20,

                stamina = 80,

                convert = 2,

                export = ExportGood.goods.potions,

                units = 1,

            };


            potions[ApothecaryHandle.items.magnus_ligna] = new()
            {

                buff = BuffHandle.buffTypes.alignment,

                item = ApothecaryHandle.items.magnus_ligna,

                relic = IconData.relics.herbalism_still,

                level = 3,

                duration = 360,

                title = "Magnus Ligna",

                description = "Infused with the leaves and petals of weeds. Toxic to dogs.",

                recipe = AlchemyRecipe.recipes.magnus_ligna,

                health = 50,

                stamina = 200,

                convert = 2,

                export = ExportGood.goods.potions,

                units = 2,

            };

            potions[ApothecaryHandle.items.optimus_ligna] = new()
            {

                buff = BuffHandle.buffTypes.alignment,

                item = ApothecaryHandle.items.optimus_ligna,

                relic = IconData.relics.herbalism_gauge,

                level = 4,

                duration = 480,

                title = "Optimus Ligna",

                description = "Brims with the vibrant colours of creation",

                recipe = AlchemyRecipe.recipes.optimus_ligna,

                health = 100,

                stamina = 400,

                convert = 2,

                export = ExportGood.goods.potions,

                units = 4,

            };

            // ====================================================================

            potions[ApothecaryHandle.items.vigores] = new()
            {

                buff = BuffHandle.buffTypes.vigor,

                item = ApothecaryHandle.items.vigores,

                relic = IconData.relics.herbalism_mortar,

                level = 1,

                duration = 180,

                title = "Vigores",

                description = "Contains the essence of cave.",

                recipe = AlchemyRecipe.recipes.vigores,

                health = 30,

                stamina = 80,


            };


            potions[ApothecaryHandle.items.satius_vigores] = new()
            {

                buff = BuffHandle.buffTypes.vigor,

                item = ApothecaryHandle.items.satius_vigores,

                relic = IconData.relics.herbalism_pan,

                level = 2,

                duration = 240,

                title = "Satius Vigores",

                description = "Gently raise to boiling temperature then leave to simmer.",

                recipe = AlchemyRecipe.recipes.satius_vigores,

                health = 45,

                stamina = 160,

                convert = 2,

                export = ExportGood.goods.potions,

                units = 1,

            };


            potions[ApothecaryHandle.items.magnus_vigores] = new()
            {

                buff = BuffHandle.buffTypes.vigor,

                item = ApothecaryHandle.items.magnus_vigores,

                relic = IconData.relics.herbalism_still,

                level = 3,

                duration = 360,

                title = "Magnus Vigores",

                description = "The spicy tang that enflames the regions.",

                recipe = AlchemyRecipe.recipes.magnus_vigores,

                health = 70,

                stamina = 320,

                convert = 2,

                export = ExportGood.goods.potions,

                units = 2,

            };

            potions[ApothecaryHandle.items.optimus_vigores] = new()
            {

                buff = BuffHandle.buffTypes.vigor,

                item = ApothecaryHandle.items.optimus_vigores,

                relic = IconData.relics.herbalism_gauge,

                level = 4,

                duration = 480,

                title = "Optimus Vigores",

                description = "It burns, but it stays down.",

                recipe = AlchemyRecipe.recipes.optimus_vigores,

                health = 180,

                stamina = 560,

                convert = 2,

                export = ExportGood.goods.potions,

                units = 4,

            };

            // ====================================================================
            // Celeri series

            potions[ApothecaryHandle.items.celeri] = new()
            {

                buff = BuffHandle.buffTypes.celerity,

                item = ApothecaryHandle.items.celeri,

                relic = IconData.relics.herbalism_mortar,

                level = 1,

                duration = 180,

                title = "Celeri",

                description = "With mineral extracts for revitalised skin and circulation.",

                recipe = AlchemyRecipe.recipes.celeri,

                health = 20,

                stamina = 60,


            };


            potions[ApothecaryHandle.items.satius_celeri] = new()
            {

                buff = BuffHandle.buffTypes.celerity,

                item = ApothecaryHandle.items.satius_celeri,

                relic = IconData.relics.herbalism_pan,

                level = 2,

                duration = 240,

                title = "Satius Celeri",

                description = "Good for your joints and memory retention.",

                recipe = AlchemyRecipe.recipes.satius_celeri,

                health = 40,

                stamina = 120,

                convert = 2,

                export = ExportGood.goods.potions,

                units = 1,

            };


            potions[ApothecaryHandle.items.magnus_celeri] = new()
            {

                buff = BuffHandle.buffTypes.celerity,

                item = ApothecaryHandle.items.magnus_celeri,

                relic = IconData.relics.herbalism_still,

                level = 3,

                duration = 360,

                title = "Magnus Celeri",

                description = "Now with organically sourced protein for heightened muscle recovery.",

                recipe = AlchemyRecipe.recipes.magnus_celeri,

                health = 80,

                stamina = 240,

                convert = 2,

                export = ExportGood.goods.potions,

                units = 2,

            };

            potions[ApothecaryHandle.items.optimus_celeri] = new()
            {

                buff = BuffHandle.buffTypes.celerity,

                item = ApothecaryHandle.items.optimus_celeri,

                relic = IconData.relics.herbalism_gauge,

                level = 4,

                duration = 480,

                title = "Optimus Celeri",

                description = "Now with fluff and sprinkles for added bliss.",

                recipe = AlchemyRecipe.recipes.optimus_celeri,

                health = 120,

                stamina = 480,

                convert = 2,

                export = ExportGood.goods.potions,

                units = 4,

            };

            // ====================================================================
            // Faeth

            potions[ApothecaryHandle.items.faeth] = new()
            {

                buff = BuffHandle.buffTypes.none,

                item = ApothecaryHandle.items.faeth,

                relic = IconData.relics.herbalism_crucible,

                title = "Faeth",

                description = "The delicacy of the Fates, used in enchantments.",

                recipe = AlchemyRecipe.recipes.faeth,

                type = ApothecaryItem.herbalType.resource,

                convert = 2,

                export = ExportGood.goods.potions,

                units = 1,

            };

            // ====================================================================
            // Ether

            potions[ApothecaryHandle.items.aether] = new()
            {

                buff = BuffHandle.buffTypes.none,

                item = ApothecaryHandle.items.aether,

                relic = IconData.relics.herbalism_gauge,

                title = "Aether",

                description = "The essence of creation as wielded by the Ancient Ones.",

                recipe = AlchemyRecipe.recipes.aether,

                type = ApothecaryItem.herbalType.resource,

                convert = 2,

                export = ExportGood.goods.potions,

                units = 2,

            };


            // ====================================================================
            // Powders

            potions[ApothecaryHandle.items.coruscant] = new()
            {

                buff = BuffHandle.buffTypes.none,

                item = ApothecaryHandle.items.coruscant,

                title = "Coruscant",

                description = "Before every great spark is a little sparkle",

                recipe = AlchemyRecipe.recipes.coruscant,

                type = ApothecaryItem.herbalType.resource,

            };

            potions[ApothecaryHandle.items.imbus] = new()
            {

                buff = BuffHandle.buffTypes.imbuement,

                item = ApothecaryHandle.items.imbus,

                title = "Imbus",

                description = "It's certified organic and tastes like spirulina",

                duration = 600,

                recipe = AlchemyRecipe.recipes.imbus,

                type = ApothecaryItem.herbalType.powder,

                convert = 2,

                export = ExportGood.goods.powders,

                units = 2,

            };

            potions[ApothecaryHandle.items.amori] = new()
            {

                buff = BuffHandle.buffTypes.amorous,

                item = ApothecaryHandle.items.amori,

                title = "Amori",

                description = "Crafted with love and irony",

                recipe = AlchemyRecipe.recipes.amori,

                duration = 600,

                type = ApothecaryItem.herbalType.powder,

                convert = 2,

                export = ExportGood.goods.powders,

                units = 2,

            };

            potions[ApothecaryHandle.items.macerari] = new()
            {

                buff = BuffHandle.buffTypes.macerari,

                item = ApothecaryHandle.items.macerari,

                title = "Macerari",

                description = "It will leak all over your refridgerator shelf",

                recipe = AlchemyRecipe.recipes.macerari,

                duration = 600,

                type = ApothecaryItem.herbalType.powder,

                convert = 2,

                export = ExportGood.goods.powders,

                units = 2,

            };

            potions[ApothecaryHandle.items.rapidus] = new()
            {

                buff = BuffHandle.buffTypes.rapidfire,

                item = ApothecaryHandle.items.rapidus,

                title = "Rapidus",

                description = "Enough sport supplement to turn your throwing arm into an automated weapon",

                recipe = AlchemyRecipe.recipes.rapidus,

                duration = 600,

                type = ApothecaryItem.herbalType.powder,

                convert = 2,

                export = ExportGood.goods.powders,

                units = 2,

            };


            // ====================================================================
            // Powders (Stronger)

            potions[ApothecaryHandle.items.voil] = new()
            {

                buff = BuffHandle.buffTypes.none,

                item = ApothecaryHandle.items.voil,

                relic = IconData.relics.herbalism_pan,

                title = "Voil",

                description = "The gruelled essence of an ancient race of lizards",

                recipe = AlchemyRecipe.recipes.voil,

                type = ApothecaryItem.herbalType.resource,

            };

            potions[ApothecaryHandle.items.concutere] = new()
            {

                buff = BuffHandle.buffTypes.concussion,

                item = ApothecaryHandle.items.concutere,

                relic = IconData.relics.herbalism_pan,

                title = "Concutere",

                description = "Keep in a cool, dry, airtight container away from open flames",

                recipe = AlchemyRecipe.recipes.concutere,

                duration = 600,

                type = ApothecaryItem.herbalType.powder,

                convert = 2,

                export = ExportGood.goods.powders,

                units = 3,

            };

            potions[ApothecaryHandle.items.jumere] = new()
            {

                buff = BuffHandle.buffTypes.jumper,

                item = ApothecaryHandle.items.jumere,

                relic = IconData.relics.herbalism_pan,

                title = "Jumere",

                description = "Sprinkle a little over breakfast to give it an extra kick",

                recipe = AlchemyRecipe.recipes.jumere,

                duration = 600,

                type = ApothecaryItem.herbalType.powder,

                convert = 2,

                export = ExportGood.goods.powders,

                units = 3,

            };

            potions[ApothecaryHandle.items.felis] = new()
            {

                buff = BuffHandle.buffTypes.feline,

                item = ApothecaryHandle.items.felis,

                relic = IconData.relics.herbalism_pan,

                title = "Felis",

                description = "What you get when you crush your old cat's medication",

                recipe = AlchemyRecipe.recipes.felis,

                duration = 600,

                type = ApothecaryItem.herbalType.powder,

                convert = 2,

                export = ExportGood.goods.powders,

                units = 3,

            };

            potions[ApothecaryHandle.items.sanctus] = new()
            {

                buff = BuffHandle.buffTypes.sanctified,

                item = ApothecaryHandle.items.sanctus,

                relic = IconData.relics.herbalism_pan,

                title = "Sanctus",

                description = "O Yoba, bless this thy hand grenade, that with it thou mayst blow thine enemies to tiny bits, in thy mercy.",

                recipe = AlchemyRecipe.recipes.sanctus,

                duration = 600,

                type = ApothecaryItem.herbalType.powder,

                convert = 2,

                export = ExportGood.goods.powders,

                units = 3,

            };

            // ====================================================================
            // Capture Balls

            potions[ApothecaryHandle.items.captis] = new()
            {

                buff = BuffHandle.buffTypes.capture,

                item = ApothecaryHandle.items.captis,

                relic = IconData.relics.companion_badge,

                level = 1,

                title = "Captis",

                description = "A rudimentary construction, crude, perhaps enough to scare a timid beast",

                recipe = AlchemyRecipe.recipes.captis,

                duration = 600,

                type = ApothecaryItem.herbalType.ball,

            };

            potions[ApothecaryHandle.items.ferrum_captis] = new()
            {

                buff = BuffHandle.buffTypes.capture,

                item = ApothecaryHandle.items.ferrum_captis,

                relic = IconData.relics.companion_badge,

                level = 2,

                title = "Ferrum Captis",

                description = "A reinforced construction, amenable, strong enough to hold a beast",

                recipe = AlchemyRecipe.recipes.ferrum_captis,

                duration = 600,

                type = ApothecaryItem.herbalType.ball,

                convert = 2,

                export = ExportGood.goods.powders,

                units = 2,

            };

            potions[ApothecaryHandle.items.aurum_captis] = new()
            {

                buff = BuffHandle.buffTypes.capture,

                item = ApothecaryHandle.items.aurum_captis,

                relic = IconData.relics.herbalism_crucible,

                level = 3,

                title = "Aurum Captis",

                description = "A careful construction, splendid, almost too good to throw",

                recipe = AlchemyRecipe.recipes.aurum_captis,

                duration = 600,

                type = ApothecaryItem.herbalType.ball,

                convert = 2,

                export = ExportGood.goods.powders,

                units = 3,

            };

            potions[ApothecaryHandle.items.diamas_captis] = new()
            {

                buff = BuffHandle.buffTypes.capture,

                item = ApothecaryHandle.items.diamas_captis,

                relic = IconData.relics.herbalism_gauge,

                level = 4,

                title = "Diamas Captis",

                description = "A wondrous ball, exquisite, vain creatures will supplicate before you to be captured by such magnificence",

                recipe = AlchemyRecipe.recipes.diamas_captis,

                duration = 600,

                type = ApothecaryItem.herbalType.ball,

                convert = 2,

                export = ExportGood.goods.powders,

                units = 5,

            };

            potions[ApothecaryHandle.items.capesso] = new()
            {

                buff = BuffHandle.buffTypes.spellcatch,

                item = ApothecaryHandle.items.capesso,

                relic = IconData.relics.companion_badge,

                title = "Capesso",

                description = "Come here, come on, oh good boy, good boy! Now get in the cage. It's nice and cosy!",

                recipe = AlchemyRecipe.recipes.capesso,

                duration = 600,

                type = ApothecaryItem.herbalType.powder,

                convert = 2,

                export = ExportGood.goods.powders,

                units = 3,

            };

            // ====================================================================
            // Omens

            potions[ApothecaryHandle.items.omen_feather] = new()
            {

                item = ApothecaryHandle.items.omen_feather,

                title = "Gale Feather",

                description = "A feather from a member of a grand old family of owls, recognised by all the wild creatures of the valley.",

                details = new()
                {
                    "Wild omen that is a common drop from flying creatures",
                },

                type = ApothecaryItem.herbalType.omen,

                recipe = AlchemyRecipe.recipes.best_ligna,

                convert = 2,

                export = ExportGood.goods.omens,

                units = 1,

            };

            potions[ApothecaryHandle.items.omen_tuft] = new()
            {

                item = ApothecaryHandle.items.omen_tuft,

                title = "Tuft of Fluff",

                description = "A toy for young wolves and foxes, gathered and bound by clever crows.",

                details = new()
                {
                    "Wild omen that is a common drop from small four legged critters",
                },

                type = ApothecaryItem.herbalType.omen,

                recipe = AlchemyRecipe.recipes.best_vigores,

                convert = 2,

                export = ExportGood.goods.omens,

                units = 1,

            };

            potions[ApothecaryHandle.items.omen_shell] = new()
            {

                item = ApothecaryHandle.items.omen_shell,

                title = "Gleaming Shell",

                description = "The shell is large and opalescent. The mollusc that owned it must have lived a happy life.",

                details = new()
                {
                    "Wild omen that is a common drop from creatures found at the beach",
                },

                type = ApothecaryItem.herbalType.omen,

                recipe = AlchemyRecipe.recipes.best_celeri,

                convert = 2,

                export = ExportGood.goods.omens,

                units = 1,

            };

            potions[ApothecaryHandle.items.omen_nest] = new()
            {

                item = ApothecaryHandle.items.omen_nest,

                title = "Nest of Secrets",

                description = "An animal gathered and set the twigs and lichens, then poured all its hopes and secrets into it.",

                details = new()
                {
                    "Wild omen that is a common drop from corvidae",
                },

                type = ApothecaryItem.herbalType.omen,

                recipe = AlchemyRecipe.recipes.convert_treeseed,

                convert = 2,

                export = ExportGood.goods.omens,

                units = 1,

            };

            potions[ApothecaryHandle.items.omen_glass] = new()
            {

                item = ApothecaryHandle.items.omen_glass,

                title = "Glass Ingot",

                description = "An opaque piece of natural glass, easy to carry, good for scouring dirt.",

                details = new()
                {
                    "Wild omen that is a common drop from serpents",
                },

                type = ApothecaryItem.herbalType.omen,

                recipe = AlchemyRecipe.recipes.convert_gems,

                convert = 2,

                export = ExportGood.goods.omens,

                units = 1,

            };

            potions[ApothecaryHandle.items.omen_down] = new()
            {

                item = ApothecaryHandle.items.omen_down,

                title = "Grey Down",

                description = "It springs back after being pressed tight in the palm of your hand. Then it tries to fly away.",

                details = new()
                {
                    "Wild omen that is a rare drop from flying creatures touched by the Weald.",
                },

                type = ApothecaryItem.herbalType.omen,

                recipe = AlchemyRecipe.recipes.random_powder,

                convert = 2,

                export = ExportGood.goods.omens,

                units = 1,

            };

            potions[ApothecaryHandle.items.omen_coral] = new()
            {

                item = ApothecaryHandle.items.omen_coral,

                title = "Fossilised Coral",

                description = "The coral that inhabitated this sediment layer is long gone, but the minerals that settled in its place have a remarkable hue.",

                details = new()
                {
                    "Wild omen that is a rare drop from serpents touched by the Weald.",
                },

                type = ApothecaryItem.herbalType.omen,

                recipe = AlchemyRecipe.recipes.convert_liquid,

                convert = 2,

                export = ExportGood.goods.omens,

                units = 1,

            };

            potions[ApothecaryHandle.items.omen_berry] = new()
            {

                item = ApothecaryHandle.items.omen_berry,

                title = "Interesting Fruit",

                description = "It is very interesting. It is indeed something of note. It must be studied, and protected. It is precious.",

                details = new()
                {
                    "Rare drop from creatures.",
                },

                type = ApothecaryItem.herbalType.omen,

                convert = 1,

                resource = ExportResource.resources.must,

                units = 20,

            };

            potions[ApothecaryHandle.items.omen_courtseed] = new()
            {

                item = ApothecaryHandle.items.omen_courtseed,

                title = "Courtesan Seed",

                description = "A civilisation in the distant past used the flowers of this tree to commemorate royal events.",

                details = new()
                {
                    "Harvested from Courtesan Magnolias found in Druid locations.",
                },

                type = ApothecaryItem.herbalType.omen,

                convert = 1,

                resource = ExportResource.resources.tribute,

                units = 10,

            };

            potions[ApothecaryHandle.items.omen_guardhop] = new()
            {

                item = ApothecaryHandle.items.omen_guardhop,

                title = "Guardian Hop",

                description = "The Order of the Guardians of the Star breed this special variety of Hop. If properly trained, the bines can grow quickly out of reach.",

                details = new()
                {
                    "Rare drop from creatures, or harvested from Guardian Hop creepers found in Druid locations.",
                },

                type = ApothecaryItem.herbalType.omen,

                convert = 1,

                resource = ExportResource.resources.malt,

                units = 20,


            };

            potions[ApothecaryHandle.items.omen_doubtseed] = new()
            {

                item = ApothecaryHandle.items.omen_doubtseed,

                title = "Seed of Doubt",

                description = "You probably won't harvest anything from whatever grows from this seed. In fact, it probably won't even sprout. Actually, it's probably not even a seed at all.",

                details = new()
                {
                    "Rare drop from creatures.",
                },

                type = ApothecaryItem.herbalType.omen,

                recipe = AlchemyRecipe.recipes.convert_flowerseed,

                convert = 2,

                export = ExportGood.goods.omens,

                units = 1,

            };

            potions[ApothecaryHandle.items.omen_wealdseed] = new()
            {

                item = ApothecaryHandle.items.omen_wealdseed,

                title = "Weald Seed",

                description = "Only the energies of the Weald can coax this fickle seed to sprout.",

                details = new()
                {
                    "Harvested from tree varieties found in Druid locations.",
                },

                type = ApothecaryItem.herbalType.omen,

                recipe = AlchemyRecipe.recipes.convert_rareseed,

                convert = 2,

                export = ExportGood.goods.omens,

                units = 1,

            };

            potions[ApothecaryHandle.items.omen_elderbloom] = new()
            {

                item = ApothecaryHandle.items.omen_elderbloom,

                title = "Rare Elderbloom",

                description = "The perennial species is known to grow in the shade of both Oak and Holly trees.",
                
                details = new()
                {
                    "Harvested from Elderbloom bushes found in Druid locations.",
                },

                type = ApothecaryItem.herbalType.omen,

                recipe = AlchemyRecipe.recipes.convert_wildseed,

                convert = 2,

                export = ExportGood.goods.omens,

                units = 1,
            };

            potions[ApothecaryHandle.items.omen_courtbloom] = new()
            {

                item = ApothecaryHandle.items.omen_courtbloom,

                title = "Courtesan Blossoms",

                description = "The petals provide soothing relief for skin ailments.",

                details = new()
                {
                    "Harvested from Courtesan Magnolias found in Druid locations.",
                },

                type = ApothecaryItem.herbalType.omen,

                recipe = AlchemyRecipe.recipes.convert_mixedseed,

                convert = 2,

                export = ExportGood.goods.omens,

                units = 1,
            };

            potions[ApothecaryHandle.items.omen_sacredlily] = new()
            {

                item = ApothecaryHandle.items.omen_sacredlily,

                title = "Sacred Lily",

                description = "Blooms where there are concentrations of fresh water and old world energies.",

                details = new()
                {
                    "Harvested from Sacred Lilies found in Druid locations.",
                },

                type = ApothecaryItem.herbalType.omen,

                convert = 1,

                resource = ExportResource.resources.nectar,

                units = 10,

            };

            // ====================================================================
            // Trophies

            potions[ApothecaryHandle.items.trophy_shroom] = new()
            {

                item = ApothecaryHandle.items.trophy_shroom,

                title = "Dangerous Shroom",

                description = "This thing is toxic, lethal, and yet, considered a delicacy to monsters of the valley.",

                details = new()
                {
                    "Monster trophy that is a random drop from defeated monsters",
                    "Monster Battle: Provides a combatant with a counter-strike buff.",
                },

                type = ApothecaryItem.herbalType.trophy,

                convert = 2,

                export = ExportGood.goods.trophies,

                units = 1,

            };

            potions[ApothecaryHandle.items.trophy_eye] = new()
            {

                item = ApothecaryHandle.items.trophy_eye,

                title = "Eye of Focus",

                description = "There is a shadow in the iris, a shadow of something unseen by human eyes.",

                details = new()
                {
                    "Monster trophy that is a random drop from defeated monsters",
                    "Monster Battle: Provides a combatant with a critical-hit buff.",
                },

                type = ApothecaryItem.herbalType.trophy,

                convert = 2,

                export = ExportGood.goods.trophies,

                units = 1,

            };

            potions[ApothecaryHandle.items.trophy_pumpkin] = new()
            {

                item = ApothecaryHandle.items.trophy_pumpkin,

                title = "Suspicious Gourd",

                description = "This isn't a pumpkin. It can't be. No, it is something untoward. Something unnatural. Something ominous.",

                details = new()
                {
                    "Monster trophy that is a random drop from defeated monsters",
                },

                type = ApothecaryItem.herbalType.trophy,

                recipe = AlchemyRecipe.recipes.update_cooking,

                convert = 2,

                export = ExportGood.goods.trophies,

                units = 1,

            };

            potions[ApothecaryHandle.items.trophy_pearl] = new()
            {

                item = ApothecaryHandle.items.trophy_pearl,

                title = "Abyssal Pearl",

                description = "For years this thing slumbered within the roiling innards of some monstrosity. It has a nice weight.",

                details = new()
                {
                    "Monster trophy that is a random drop from defeated monsters",
                },

                type = ApothecaryItem.herbalType.trophy,

                recipe = AlchemyRecipe.recipes.update_stack,

                convert = 2,

                export = ExportGood.goods.trophies,

                units = 1,

            };

            potions[ApothecaryHandle.items.trophy_tooth] = new()
            {

                item = ApothecaryHandle.items.trophy_tooth,

                title = "Massive Tooth",

                description = "How big was the fist that knocked this tooth out?",

                details = new()
                {
                    "Monster trophy that is a random drop from defeated monsters",
                },

                type = ApothecaryItem.herbalType.trophy,

                recipe = AlchemyRecipe.recipes.update_produce,

                convert = 2,

                export = ExportGood.goods.trophies,

                units = 1,

            };

            potions[ApothecaryHandle.items.trophy_shell] = new()
            {

                item = ApothecaryHandle.items.trophy_shell,

                title = "Death Spiral",

                description = "These shells are often abandoned at the doorsteps of those doomed to a tragic fate.",

                details = new()
                {
                    "Monster trophy that is a random drop from defeated monsters",
                },

                type = ApothecaryItem.herbalType.trophy,

                recipe = AlchemyRecipe.recipes.update_artisanal,

                convert = 2,

                export = ExportGood.goods.trophies,

                units = 1,

            };

            potions[ApothecaryHandle.items.trophy_gloop] = new()
            {

                item = ApothecaryHandle.items.trophy_gloop,

                title = "Explosive Gloop",

                description = "The smell is proportional to its explosive potential.",

                details = new()
                {
                    "Rare drop from random defeated monsters.",
                },

                type = ApothecaryItem.herbalType.trophy,

                recipe = AlchemyRecipe.recipes.convert_bombs,

                convert = 2,

                export = ExportGood.goods.trophies,

                units = 1,

            };

            potions[ApothecaryHandle.items.trophy_string] = new()
            {

                item = ApothecaryHandle.items.trophy_string,

                title = "Cocoon Nut",

                description = "A mass of organic nutrients, squashed together and packaged in a mess of strings",

                details = new()
                {
                    "Random drop from defeated monsters",
                },

                type = ApothecaryItem.herbalType.trophy,

                convert = 1,

                resource = ExportResource.resources.malt,

                units = 10,

            };


            potions[ApothecaryHandle.items.trophy_tusk] = new()
            {

                item = ApothecaryHandle.items.trophy_tusk,

                title = "Weathered Ivory",

                description = "The scratched surface bears the memory of every time it was used to sign the earth.",

                details = new()
                {
                    "Wild omen that is a common drop from bears and wolves",
                },

                type = ApothecaryItem.herbalType.trophy,

                recipe = AlchemyRecipe.recipes.convert_caffeine,

                convert = 2,

                export = ExportGood.goods.trophies,

                units = 1,

            };

            potions[ApothecaryHandle.items.trophy_heart] = new()
            {

                item = ApothecaryHandle.items.trophy_heart,

                title = "Faded Heart",

                description = "This was once the pulsing heart of an elemental. Now it is just a lifeless rock.",

                details = new()
                {
                    "Rare drop from skull cavern monsters.",
                },

                type = ApothecaryItem.herbalType.trophy,

                recipe = AlchemyRecipe.recipes.convert_geodes,

                convert = 2,

                export = ExportGood.goods.trophies,

                units = 1,

            };

            potions[ApothecaryHandle.items.trophy_skull] = new()
            {

                item = ApothecaryHandle.items.trophy_skull,

                title = "Frozen Skull",

                description = "The alchemist's dream, or folly, realised by the mind of a fiend from another realm.",

                details = new()
                {
                    "Rare drop from skeleton monsters.",
                },

                type = ApothecaryItem.herbalType.trophy,

                recipe = AlchemyRecipe.recipes.convert_gold,

                convert = 2,

                export = ExportGood.goods.trophies,

                units = 1,

            };

            potions[ApothecaryHandle.items.trophy_dragon] = new()
            {

                item = ApothecaryHandle.items.trophy_dragon,

                title = "Dragon Stone",

                description = "It's difficult to perceive what geological process produced this stone, if one discounts the likely cause of dragonfire.",

                details = new()
                {
                    "Rare drop from skull cavern monsters.",
                },

                type = ApothecaryItem.herbalType.trophy,

                convert = 2,

                export = ExportGood.goods.trophies,

                units = 3,

            };

            potions[ApothecaryHandle.items.trophy_tendril] = new()
            {

                item = ApothecaryHandle.items.trophy_tendril,

                title = "Enticing Tendril",

                description = "Come hither. The tendril beckons to those who desire a taste of power.",

                details = new()
                {
                    "Rare drop from random monsters.",
                },

                type = ApothecaryItem.herbalType.trophy,

                convert = 1,

                resource = ExportResource.resources.tribute,

                units = 10,

            };

            potions[ApothecaryHandle.items.trophy_spike] = new()
            {

                item = ApothecaryHandle.items.trophy_spike,

                title = "Vile Spike",

                description = "It feels wrong to touch, or to point at anything, except if that thing is the subject of extreme spite.",

                details = new()
                {
                    "Rare drop from shadow monsters.",
                },

                type = ApothecaryItem.herbalType.trophy,

                convert = 2,

                export = ExportGood.goods.trophies,

                units = 3,

            };

            potions[ApothecaryHandle.items.trophy_wood] = new()
            {

                item = ApothecaryHandle.items.trophy_wood,

                title = "Gaping Wood",

                description = "It could be singing, or screaming, or simply caught with its mouth open at the time it was felled.",

                details = new()
                {
                    "Random drop from defeated monsters.",
                },

                type = ApothecaryItem.herbalType.trophy,

                convert = 1,

                resource = ExportResource.resources.materials,

                units = 10,

            };

            return potions;

        }

    }

}
