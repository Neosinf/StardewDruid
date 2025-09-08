using StardewDruid.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Data
{
    public class ResourceData
    {

        public static Dictionary<ExportResource.resources, ExportResource> LoadResources()
        {

            Dictionary<ExportResource.resources, ExportResource> resources = new();

            resources[ExportResource.resources.malt] = new()
            {
                name = "Malt",

                description = "Steeped and Kiln-dried grain.",

                details = new(){
                    "Primarily made from Wheat, Unmilled Rice, Hops, Corn, Green Beans, Coffee Beans, Potato left in the Distillery inventory",
                    "Upgrade the Crusher and Mashtun to increase processing capacity and efficiency",
                    "It takes 5 units to make one batch of Whiskey.",
                },

            };

            resources[ExportResource.resources.must] = new()
            {
                name = "Must",

                description = "Fermented fruit pulp.",

                details = new(){
                    "Primarily made from Fruit left in the Distillery inventory",
                    "Upgrade the Crusher and Wine Press to increase processing capacity and efficiency",
                    "It takes 10 units to make one batch of Brandy.",
                },

            };

            resources[ExportResource.resources.tribute] = new()
            {
                name = "Tribute",

                description = "Grains steeped in mystical energies. ",

                details = new(){
                    "Occurs as a byproduct from Malt production.",
                    "Upgrade the Kiln to increase chance of byproduct.",
                    "It takes 20 units to make one Aqua Vitae.",
                },

            };

            resources[ExportResource.resources.nectar] = new()
            {
                name = "Nectar",

                description = "Made from fruits with special qualities.",

                details = new(){
                    "Occurs as a byproduct from Must production.",
                    "Upgrade the Fermentation Tank to increase chance of byproduct.",
                    "It takes 10 units to make one batch of Ambrosia.",
                },

            };

            resources[ExportResource.resources.labour] = new()
            {
                name = "Labour",

                description = "Consists of the number of captured creatures that can work and maintain the distillery equipment.",

                details = new()
                {
                    "Capture more Bats, Dust Sprites and other small creatures to increase labour supply.",
                    "Needed to upgrade Mashtun, Fermentation Tank and Distillery.",
                }

            };

            resources[ExportResource.resources.heavy] = new()
            {
                name = "Heavy Labour",

                description = "Consists of the number of captured creatures that can work and maintain the heavy machinery.",

                details = new()
                {
                    "Capture more Slimes and other large creatures to increase heavy labour supply.",
                    "Needed to upgrade Crusher, Press and Packer machines.",
                }

            };

            resources[ExportResource.resources.special] = new()
            {
                name = "Special Labour",

                description = "Consists of the number of captured creatures that apply special qualities to batches.",

                details = new()
                {
                    "Capture more Spectres, Serpents and other mystical creatures to increase special labour supply.",
                    "Needed to upgrade Kiln and Aging Barrels.",
                }

            };

            resources[ExportResource.resources.materials] = new()
            {
                name = "Materials",

                description = "The amount of mineral and lumber materials provided to the distillery for machine upgrades.",

                details = new()
                {
                    "Leave stone, ores, bars, wood, hardwood and other materials in the Distillery inventory to increase supply.",
                    "Needed to upgrade every Distillery machine.",
                }

            };

            return resources;

        }

    }

}
