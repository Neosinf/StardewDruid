using StardewDruid.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Data
{
    public class GuildData
    {

        public static Dictionary<ExportGuild.guilds, ExportGuild> LoadGuilds()
        {

            Dictionary<ExportGuild.guilds, ExportGuild> guilds = new();

            ExportGuild church = new()
            {
                guild = ExportGuild.guilds.church,
                name = Mod.instance.Helper.Translation.Get("ExportData.386.302"),
                intro = Mod.instance.Helper.Translation.Get("ExportData.386.303"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.304") +
                Mod.instance.Helper.Translation.Get("ExportData.386.305"),
                benefits = new()
                {
                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.309"),
                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.310"),
                    [4] = Mod.instance.Helper.Translation.Get("ExportData.390.1"),
                },
                license = IconData.relics.crest_church,

                orderTitles = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.371"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.373"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.375"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.377"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.379"),
                },

                orderDescriptions = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.438"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.440"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.442"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.444"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.446"),
                },

                orderFulfilled = Mod.instance.Helper.Translation.Get("ExportData.386.501"),

            };

            guilds.Add(ExportGuild.guilds.church, church);

            // --------------------------------------------

            ExportGuild dwarf = new()
            {
                guild = ExportGuild.guilds.dwarf,
                name = Mod.instance.Helper.Translation.Get("ExportData.386.318"),
                intro = Mod.instance.Helper.Translation.Get("ExportData.386.319"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.320"),
                benefits = new()
                {
                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.324"),
                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.325"),
                    [4] = Mod.instance.Helper.Translation.Get("ExportData.390.2"),
                },
                license = IconData.relics.crest_dwarf,

                orderTitles = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.386"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.388"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.390"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.392"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.394"),
                },

                orderDescriptions = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.453"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.455"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.457"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.459"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.461"),
                },

                orderFulfilled = Mod.instance.Helper.Translation.Get("ExportData.386.503"),

            };

            guilds.Add(ExportGuild.guilds.dwarf, dwarf);

            // --------------------------------------------

            ExportGuild associate = new()
            {
                guild = ExportGuild.guilds.associate,
                name = Mod.instance.Helper.Translation.Get("ExportData.386.333"),
                intro = Mod.instance.Helper.Translation.Get("ExportData.386.334"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.335"),
                benefits = new()
                {
                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.339"),
                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.340"),
                    [4] = Mod.instance.Helper.Translation.Get("ExportData.390.3"),
                },
                license = IconData.relics.crest_associate,

                orderTitles = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.401"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.403"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.405"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.407"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.409"),
                },

                orderDescriptions = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.468"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.470"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.472"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.474"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.476"),
                },

                orderFulfilled = Mod.instance.Helper.Translation.Get("ExportData.386.505"),

            };

            guilds.Add(ExportGuild.guilds.associate, associate);

            // --------------------------------------------

            ExportGuild smuggler = new()
            {
                guild = ExportGuild.guilds.smuggler,
                name = Mod.instance.Helper.Translation.Get("ExportData.386.348"),
                intro = Mod.instance.Helper.Translation.Get("ExportData.386.349"),
                description = Mod.instance.Helper.Translation.Get("ExportData.386.350"),
                benefits = new()
                {
                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.354"),
                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.355"),
                    [4] = Mod.instance.Helper.Translation.Get("GuildData.500.sell.1"),
                },
                license = IconData.relics.crest_smuggler,

                orderTitles = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.416"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.418"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.420"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.422"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.424"),
                },

                orderDescriptions = new()
                {
                    [0] = Mod.instance.Helper.Translation.Get("ExportData.386.483"),

                    [1] = Mod.instance.Helper.Translation.Get("ExportData.386.485"),

                    [2] = Mod.instance.Helper.Translation.Get("ExportData.386.487"),

                    [3] = Mod.instance.Helper.Translation.Get("ExportData.386.489"),

                    [4] = Mod.instance.Helper.Translation.Get("ExportData.386.491"),
                },

                orderFulfilled = Mod.instance.Helper.Translation.Get("ExportData.386.507"),

            };

            guilds.Add(ExportGuild.guilds.smuggler, smuggler);

            return guilds;

        }

    }

}
