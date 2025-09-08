using StardewDruid.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Data
{
    public class WorkshopData
    {

        public static Dictionary<ExportMachine.machines, ExportMachine> LoadMachines()
        {

            Dictionary<ExportMachine.machines, ExportMachine> machines = new();

            // --------------------------------------------

            ExportMachine crushers = new()
            {

                machine = ExportMachine.machines.crushers,
                name = "Crusher",
                description = "Turns fruit into pulp and grain into grist",
                technical = " Items processed into resources per day",
                graduation = new()
                {
                    10,
                    20,
                    50,
                    100,
                    150,
                    250,
                    400,
                    550,
                    750,
                    999,
                },
                labour = ExportResource.resources.heavy,
                hireage = new List<int>()
                {
                    1,
                    1,
                    2,
                    2,
                    4,
                    4,
                    8,
                    8,
                    16,
                    16,
                },
                materials = new()
                {
                    10,
                    10,
                    20,
                    20,
                    50,
                    50,
                    100,
                    100,
                    200,
                    200,
                }
            };

            machines.Add(ExportMachine.machines.crushers, crushers);

            // --------------------------------------------
            ExportMachine press = new()
            {
                machine = ExportMachine.machines.press,
                name = "Wine Press",
                description = "Extracts juice from fermented fruit pulp",
                technical = "% Bonus Must produced per item",
                graduation = new()
                {
                    10,
                    20,
                    30,
                    40,
                    50,
                    60,
                    70,
                    80,
                    90,
                    100,
                },
                labour = ExportResource.resources.heavy,
                hireage = new List<int>()
                {
                    1,
                    1,
                    2,
                    2,
                    4,
                    4,
                    8,
                    8,
                    16,
                    16,
                },
                materials = new()
                {
                    5,
                    5,
                    15,
                    15,
                    40,
                    40,
                    75,
                    75,
                    100,
                    100,
                }
            };

            machines.Add(ExportMachine.machines.press, press);

            // --------------------------------------------
            ExportMachine kiln = new()
            {

                machine = ExportMachine.machines.kiln,
                name = "Kiln",
                description = "Dries grist to stabilize it and prevent further germination",
                technical = "% Increased Chance for Tribute byproduct",
                graduation = new()
                {
                    2,
                    3,
                    4,
                    6,
                    8,
                    10,
                    12,
                    14,
                    17,
                    20,
                },
                labour = ExportResource.resources.special,
                hireage = new List<int>()
                {
                    1,
                    1,
                    2,
                    2,
                    4,
                    4,
                    8,
                    8,
                    16,
                    16,
                },
                materials = new()
                {
                    5,
                    5,
                    15,
                    15,
                    40,
                    40,
                    75,
                    75,
                    100,
                    100,
                }

            };

            machines.Add(ExportMachine.machines.kiln, kiln);

            // --------------------------------------------
            ExportMachine mashtun = new()
            {

                machine = ExportMachine.machines.mashtun,
                name = "Mash Tun",
                description = "Turns grist into wet mash",
                technical = "% Bonus Malt produced per item",
                graduation = new()
                {
                    5,
                    10,
                    15,
                    25,
                    40,
                    60,
                    80,
                    100,
                    125,
                    150,
                },
                labour = ExportResource.resources.labour,
                hireage = new List<int>()
                {
                    2,
                    2,
                    4,
                    4,
                    8,
                    8,
                    12,
                    12,
                    20,
                    20,
                },
                materials = new()
                {
                    10,
                    10,
                    20,
                    20,
                    50,
                    50,
                    100,
                    100,
                    200,
                    200,
                }
            };

            machines.Add(ExportMachine.machines.mashtun, mashtun);

            // --------------------------------------------
            ExportMachine fermentation = new()
            {
                machine = ExportMachine.machines.fermentation,
                name = "Fermentation Tank",
                description = "Used early in Must processing to ferment fruit pulp, and later in Whiskey production to ferment the malt",
                technical = " Units of each resource processed per day. Also increases chance to produce Nectar byproduct.",
                graduation = new()
                {
                    50,
                    100,
                    150,
                    200,
                    300,
                    400,
                    500,
                    650,
                    800,
                    999,
                },
                labour = ExportResource.resources.labour,
                hireage = new List<int>()
                {
                    1,
                    1,
                    2,
                    2,
                    4,
                    4,
                    8,
                    8,
                    16,
                    16,
                },
                materials = new()
                {
                    10,
                    10,
                    20,
                    20,
                    50,
                    50,
                    100,
                    100,
                    200,
                    200,
                }
            };

            machines.Add(ExportMachine.machines.fermentation, fermentation);

            // --------------------------------------------
            ExportMachine distillery = new()
            {

                name = "Distillation",
                description = "Seperates the alcohol from the other stuff",
                technical = "% Increase in resource processing capacity",
                machine = ExportMachine.machines.distillery,
                graduation = new()
                {
                    0,
                    100,
                    200,
                    300,
                    400,
                    500,
                    600,
                    700,
                    800,
                    900,
                },
                labour = ExportResource.resources.labour,
                hireage = new List<int>()
                {
                    1,
                    1,
                    2,
                    2,
                    4,
                    4,
                    8,
                    8,
                    16,
                    16,
                },
                materials = new()
                {
                    10,
                    10,
                    20,
                    20,
                    50,
                    50,
                    100,
                    100,
                    200,
                    200,
                }
            };
            
            machines.Add(ExportMachine.machines.distillery, distillery);


            // --------------------------------------------
            ExportMachine barrel = new()
            {

                name = "Aging Barrels",
                description = "The alcohol continues to evolve, gaining flavours and aromas",
                technical = "% potential bonus product per resource",
                graduation = new()
                {
                    5,
                    10,
                    15,
                    20,
                    25,
                    30,
                    35,
                    40,
                    45,
                    50,
                },
                labour = ExportResource.resources.special,
                hireage = new List<int>()
                {
                    1,
                    1,
                    2,
                    2,
                    4,
                    4,
                    8,
                    8,
                    16,
                    16,
                },
                materials = new()
                {
                    5,
                    5,
                    15,
                    15,
                    40,
                    40,
                    75,
                    75,
                    100,
                    100,
                }
            };

            machines.Add(ExportMachine.machines.barrel, barrel);


            // --------------------------------------------
            ExportMachine packer = new()
            {

                name = "Packing Station",
                description = "High quality batches are bottled, branded and prepared for delivery",
                technical = "% increase to overall production per resource",
                graduation = new()
                {
                    1,
                    2,
                    4,
                    6,
                    8,
                    10,
                    12,
                    16,
                    20,
                    25,
                },
                labour = ExportResource.resources.heavy,
                hireage = new List<int>()
                {
                    1,
                    1,
                    2,
                    2,
                    4,
                    4,
                    8,
                    8,
                    16,
                    16,
                },
                materials = new()
                {
                    5,
                    5,
                    15,
                    15,
                    40,
                    40,
                    75,
                    75,
                    100,
                    100,
                }
            };

            machines.Add(ExportMachine.machines.packer, packer);

            return machines;

        }

    }

}
