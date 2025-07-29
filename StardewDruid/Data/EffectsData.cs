
using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewValley;
using StardewValley.GameData.Characters;
using StardewValley.Objects;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Data
{

    public static class EffectsData
    {

        public enum EffectPage
        {

            none,

            // --------------------------------

            ritesOfTheDruids,
            attunement,
            herbalism,
            bomblobbing,
            omens,
            relicsets,

            // --------------------------------

            riteOfTheWeald,
            clearance,
            gentleTouch,
            cultivate,
            wildgrowth,
            rockfall,
            crowhammer,
            sap,

            // --------------------------------

            riteOfMists,
            cursorTargetting,
            sunder,
            artifice,
            totemShrines,
            campfire,
            rodMaster,
            smite,
            veilCharge,
            summonWisps,
            summonWrath,

            // --------------------------------

            riteOfTheStars,
            meteorRain,
            starBurst,
            goods,
            gravityWell,
            monsters,
            monsterbattles,
            distillery,

            // --------------------------------

            riteOfTheFates,
            whisk,
            warpstrike,
            curses,
            tricks,
            windsOfFate,
            enchant,

            // --------------------------------

            riteOfEther,
            dragonForm,
            dragonFlight,
            dragonBreath,
            dragonDive,
            dragonTreasure,

            // --------------------------------

            riteOfBones,
            corvidsSummon,
            corvidsRetrieve,
            corvidsOpportunist,

            // --------------------------------

            ledgerOfTheCircle,

        }

        public static Effect RetrieveEffect(EffectPage page)
        {

            switch (page)
            {

                case EffectPage.ritesOfTheDruids:

                    if (!Mod.instance.magic)
                    {
                        return new()
                        {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.6"),
                        icon = IconData.displays.druid,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.8"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.9"),
                        details = new()
                            {
                                Mod.instance.Helper.Translation.Get("EffectsData.12"),
                                Mod.instance.Helper.Translation.Get("EffectsData.13"),
                                Mod.instance.Helper.Translation.Get("EffectsData.14"),
                                Mod.instance.Helper.Translation.Get("EffectsData.15")
                            }
                        };

                    }
                    else
                    {

                        return new()
                        {
                            title = Mod.instance.Helper.Translation.Get("EffectsData.313.10"),
                            icon = IconData.displays.skills,
                            description = Mod.instance.Helper.Translation.Get("EffectsData.313.11"),
                            details = new()
                            {
                                Mod.instance.Helper.Translation.Get("EffectsData.313.12"),
                                Mod.instance.Helper.Translation.Get("EffectsData.313.13"),
                                Mod.instance.Helper.Translation.Get("EffectsData.313.14"),
                                Mod.instance.Helper.Translation.Get("EffectsData.313.15"),
                                Mod.instance.Helper.Translation.Get("EffectsData.313.15")
                            }
                        };

                    }

                case EffectPage.attunement:

                    return new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.51"),
                        icon = IconData.displays.druid,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.53"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.54"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.340.1"),
                            Mod.instance.Helper.Translation.Get("EffectsData.340.2"),
                            Mod.instance.Helper.Translation.Get("EffectsData.340.3"),
                            Mod.instance.Helper.Translation.Get("EffectsData.57"),
                            Mod.instance.Helper.Translation.Get("EffectsData.58"),
                            Mod.instance.Helper.Translation.Get("EffectsData.59")

                        }
                    };

                case EffectPage.herbalism:

                    Effect herbalism = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.21"),
                        icon = IconData.displays.herbalism,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.23"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.24"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.311.1"),
                            Mod.instance.Helper.Translation.Get("EffectsData.311.2"),
                            Mod.instance.Helper.Translation.Get("EffectsData.28"),
                            Mod.instance.Helper.Translation.Get("EffectsData.29"),
                            Mod.instance.Helper.Translation.Get("EffectsData.30")
                        }
                    };

                    if (!Mod.instance.magic)
                    {

                        herbalism.details.Prepend(Mod.instance.Helper.Translation.Get("EffectsData.27"));

                    }

                    return herbalism;

                case EffectPage.bomblobbing:

                    Effect bomblobbing = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.361.1"),
                        icon = IconData.displays.bombs,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.361.2"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.361.3")
                        + Mod.instance.Helper.Translation.Get("EffectsData.361.4"),
                        details = new()
                            {
                                Mod.instance.Helper.Translation.Get("EffectsData.386.1"),
                                Mod.instance.Helper.Translation.Get("HerbalData.361.29") + StringData.colon + Mod.instance.Helper.Translation.Get("HerbalData.500.buff.7"),
                                Mod.instance.Helper.Translation.Get("HerbalData.361.30") + StringData.colon + Mod.instance.Helper.Translation.Get("HerbalData.373.2"),
                                Mod.instance.Helper.Translation.Get("HerbalData.500.buff.6") + StringData.colon + Mod.instance.Helper.Translation.Get("HerbalData.500.buff.8"),
                                Mod.instance.Helper.Translation.Get("HerbalData.386.29") + StringData.colon + Mod.instance.Helper.Translation.Get("HerbalData.386.30"),

                                Mod.instance.Helper.Translation.Get("HerbalData.361.32") + StringData.colon + Mod.instance.Helper.Translation.Get("HerbalData.373.4"),
                                Mod.instance.Helper.Translation.Get("HerbalData.361.33") + StringData.colon + Mod.instance.Helper.Translation.Get("HerbalData.373.5"),
                                Mod.instance.Helper.Translation.Get("HerbalData.361.34") + StringData.colon + Mod.instance.Helper.Translation.Get("HerbalData.373.6"),
                                Mod.instance.Helper.Translation.Get("HerbalData.386.31") + StringData.colon + Mod.instance.Helper.Translation.Get("HerbalData.386.32"),

                                Mod.instance.Helper.Translation.Get("HerbalData.386.33") + StringData.colon + Mod.instance.Helper.Translation.Get("HerbalData.386.34"),

                            }
                    };

                    return bomblobbing;

                case EffectPage.omens:

                    Effect omens = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.390.1"),
                        icon = IconData.displays.omens,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.390.2"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.390.3"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.390.4"),
                            Mod.instance.Helper.Translation.Get("EffectsData.390.5"),
                            Mod.instance.Helper.Translation.Get("EffectsData.390.6"),
                            Mod.instance.Helper.Translation.Get("EffectsData.390.7"),

                        }
                    };

                    return omens;

                case EffectPage.relicsets:

                    Effect relicsets = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.189"),
                        icon = IconData.displays.relic,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.191"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.192"),
                        details = new()
                            {
                                Mod.instance.Helper.Translation.Get("EffectsData.195") +
                                Mod.instance.Helper.Translation.Get("EffectsData.196"),
                                Mod.instance.Helper.Translation.Get("EffectsData.197"),
                                Mod.instance.Helper.Translation.Get("EffectsData.198"),
                                Mod.instance.Helper.Translation.Get("EffectsData.330.1"),
                                Mod.instance.Helper.Translation.Get("EffectsData.330.2"),
                            }
                        };

                    return relicsets;


                case EffectPage.riteOfTheWeald:


                    Effect riteOfTheWeald = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.340.4"),
                        icon = IconData.displays.weald,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.340.5"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.340.6"),
                    };

                    if (!Mod.instance.magic)
                    {

                        riteOfTheWeald.details.Add(Mod.instance.Helper.Translation.Get("EffectsData.340.7"));

                    }

                    return riteOfTheWeald;

                case EffectPage.clearance:

                    Effect clearance = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.36"),
                        icon = IconData.displays.weald,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.38"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.39"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.42"),
                            Mod.instance.Helper.Translation.Get("EffectsData.43"),
                            Mod.instance.Helper.Translation.Get("EffectsData.44"),
                            Mod.instance.Helper.Translation.Get("EffectsData.310.1"),
                        }
                    };

                    return clearance;

                case EffectPage.gentleTouch:

                    Effect caress = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.68"),
                        icon = IconData.displays.weald,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.70") +
                        Mod.instance.Helper.Translation.Get("EffectsData.71"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.72"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.75"),
                            Mod.instance.Helper.Translation.Get("EffectsData.76"),
                            Mod.instance.Helper.Translation.Get("EffectsData.77"),
                            Mod.instance.Helper.Translation.Get("EffectsData.78"),
                            Mod.instance.Helper.Translation.Get("EffectsData.375.2"),
                            Mod.instance.Helper.Translation.Get("EffectsData.79"),

                        }

                    };

                    return caress;

                case EffectPage.cultivate:


                    Effect cultivate = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.109"),
                        icon = IconData.displays.weald,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.111"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.112") +
                        Mod.instance.Helper.Translation.Get("EffectsData.113"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.116"),
                            Mod.instance.Helper.Translation.Get("EffectsData.117"),
                            Mod.instance.Helper.Translation.Get("EffectsData.375.1"),
                            Mod.instance.Helper.Translation.Get("EffectsData.118"),
                            Mod.instance.Helper.Translation.Get("EffectsData.119"),
                            Mod.instance.Helper.Translation.Get("EffectsData.120"),
                            Mod.instance.Helper.Translation.Get("EffectsData.121"),
                            Mod.instance.Helper.Translation.Get("EffectsData.122"),
                            Mod.instance.Helper.Translation.Get("EffectsData.123"),
                            Mod.instance.Helper.Translation.Get("EffectsData.124"),
                            Mod.instance.Helper.Translation.Get("EffectsData.125"),
                            Mod.instance.Helper.Translation.Get("EffectsData.126"),
                            Mod.instance.Helper.Translation.Get("EffectsData.386.2"),
                        }

                    };

                    return cultivate;

                case EffectPage.wildgrowth:

                    Effect wildgrowth = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.89"),
                        icon = IconData.displays.weald,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.91"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.92") +
                        Mod.instance.Helper.Translation.Get("EffectsData.93") +
                        Mod.instance.Helper.Translation.Get("EffectsData.94"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.97"),
                            Mod.instance.Helper.Translation.Get("EffectsData.98"),
                            Mod.instance.Helper.Translation.Get("EffectsData.99"),
                            Mod.instance.Helper.Translation.Get("EffectsData.100"),
                            Mod.instance.Helper.Translation.Get("EffectsData.101"),
                        }
                    };

                    return wildgrowth;

                case EffectPage.rockfall:

                    Effect rockfall = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.136"),
                        icon = IconData.displays.weald,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.138"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.139"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.142"),
                            Mod.instance.Helper.Translation.Get("EffectsData.143"),
                            Mod.instance.Helper.Translation.Get("EffectsData.144"),
                            Mod.instance.Helper.Translation.Get("EffectsData.145"),

                        }

                    };

                    return rockfall;

                case EffectPage.crowhammer:


                    Effect crowhammer = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.168"),
                        icon = IconData.displays.druid,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.170") +
                        Mod.instance.Helper.Translation.Get("EffectsData.171"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.172"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.175"),
                            Mod.instance.Helper.Translation.Get("EffectsData.176"),
                            Mod.instance.Helper.Translation.Get("EffectsData.177"),
                        }

                    };

                    return crowhammer;

                case EffectPage.sap:

                    Effect sap = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.153"),
                        icon = IconData.displays.weald,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.155"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.156"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.159"),
                            Mod.instance.Helper.Translation.Get("EffectsData.396.1"),
                            Mod.instance.Helper.Translation.Get("EffectsData.396.2"),
                            Mod.instance.Helper.Translation.Get("EffectsData.160"),
                            Mod.instance.Helper.Translation.Get("EffectsData.161")
                        }

                    };

                    return sap;

                case EffectPage.riteOfMists:

                    Effect riteOfMists = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.340.8"),
                        icon = IconData.displays.mists,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.340.9"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.340.10"),

                    };

                    if (!Mod.instance.magic)
                    {

                        riteOfMists.details.Add(Mod.instance.Helper.Translation.Get("EffectsData.340.11"));

                    }

                    return riteOfMists;

                case EffectPage.cursorTargetting:

                    Effect cursorTargetting = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.209"),
                        icon = IconData.displays.mists,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.211"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.212"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.215"),
                            Mod.instance.Helper.Translation.Get("EffectsData.216"),
                            Mod.instance.Helper.Translation.Get("EffectsData.217") +
                                Mod.instance.Helper.Translation.Get("EffectsData.218"),
                            Mod.instance.Helper.Translation.Get("EffectsData.219")
                        }

                    };

                    return cursorTargetting;

                case EffectPage.sunder:

                    Effect sunder = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.226"),
                        icon = IconData.displays.mists,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.228"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.229"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.232"),
                            Mod.instance.Helper.Translation.Get("EffectsData.233"),
                        }
                    };

                    return sunder;

                case EffectPage.campfire:

                    Effect campfire = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.241"),
                        icon = IconData.displays.mists,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.243"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.244"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.247"),
                            Mod.instance.Helper.Translation.Get("EffectsData.248"),
                            Mod.instance.Helper.Translation.Get("EffectsData.249"),
                            Mod.instance.Helper.Translation.Get("EffectsData.250"),
                        }
                    };

                    return campfire;

                case EffectPage.totemShrines:

                    Effect totemShrines = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.256"),
                        icon = IconData.displays.mists,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.258"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.259"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.262"),
                        }
                    };

                    return totemShrines;

                case EffectPage.artifice:

                    Effect artifice = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.269"),
                        icon = IconData.displays.mists,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.271"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.272"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.275"),
                            Mod.instance.Helper.Translation.Get("EffectsData.276"),
                            Mod.instance.Helper.Translation.Get("EffectsData.277"),
                            Mod.instance.Helper.Translation.Get("EffectsData.278"),

                        }
                    };

                    return artifice;

                case EffectPage.rodMaster:

                    Effect rodMaster = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.287"),
                        icon = IconData.displays.mists,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.289"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.290") +
                        Mod.instance.Helper.Translation.Get("EffectsData.291"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.294"),
                            Mod.instance.Helper.Translation.Get("EffectsData.295"),
                            Mod.instance.Helper.Translation.Get("EffectsData.296"),
                        }
                    };

                    return rodMaster;

                case EffectPage.smite:

                    Effect smite = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.304"),
                        icon = IconData.displays.mists,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.306"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.307"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.310"),
                            Mod.instance.Helper.Translation.Get("EffectsData.311"),
                        }
                    };

                    return smite;

                case EffectPage.veilCharge:

                    Effect veilCharge = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.317"),
                        icon = IconData.displays.mists,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.319"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.320"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.323"),
                            Mod.instance.Helper.Translation.Get("EffectsData.324"),
                            Mod.instance.Helper.Translation.Get("EffectsData.325"),
                        }
                    };

                    return veilCharge;

                case EffectPage.summonWisps:

                    Effect summonWisps = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.333"),
                        icon = IconData.displays.mists,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.335") +
                        Mod.instance.Helper.Translation.Get("EffectsData.336"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.337"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.340"),
                            Mod.instance.Helper.Translation.Get("EffectsData.341"),
                            Mod.instance.Helper.Translation.Get("EffectsData.342")

                        }
                    };

                    return summonWisps;

                case EffectPage.summonWrath:

                    Effect summonWrath = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.363.1"),
                        icon = IconData.displays.mists,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.363.2"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.363.3"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.363.4"),
                            Mod.instance.Helper.Translation.Get("EffectsData.363.5"),

                        }
                    };

                    return summonWrath;

                case EffectPage.riteOfTheStars:

                    // ====================================================================
                    // Stars effects

                    Effect riteOfTheStars = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.340.12"),
                        icon = IconData.displays.stars,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.340.13"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.340.14"),

                    };

                    if (!Mod.instance.magic)
                    {

                        riteOfTheStars.details.Add(Mod.instance.Helper.Translation.Get("EffectsData.340.15"));

                    }

                    return riteOfTheStars;

                case EffectPage.meteorRain:

                    // ====================================================================

                    Effect meteorRain = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.369"),
                        icon = IconData.displays.stars,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.371"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.372") +
                        Mod.instance.Helper.Translation.Get("EffectsData.373"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.376"),
                            Mod.instance.Helper.Translation.Get("EffectsData.377"),
                            Mod.instance.Helper.Translation.Get("EffectsData.378"),
                            Mod.instance.Helper.Translation.Get("EffectsData.379"),
                            Mod.instance.Helper.Translation.Get("EffectsData.380"),
                            Mod.instance.Helper.Translation.Get("EffectsData.381"),
                            Mod.instance.Helper.Translation.Get("EffectsData.382"),
                            Mod.instance.Helper.Translation.Get("EffectsData.383"),
                            Mod.instance.Helper.Translation.Get("EffectsData.384"),
                            Mod.instance.Helper.Translation.Get("EffectsData.385"),
                            Mod.instance.Helper.Translation.Get("EffectsData.386"),
                        }
                    };

                    return meteorRain;

                case EffectPage.starBurst:

                    Effect starBurst = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.392"),
                        icon = IconData.displays.stars,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.394"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.395"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.398"),
                            Mod.instance.Helper.Translation.Get("EffectsData.399"),
                            Mod.instance.Helper.Translation.Get("EffectsData.400"),
                            Mod.instance.Helper.Translation.Get("EffectsData.401"),
                        }
                    };

                    return starBurst;

                case EffectPage.goods:

                    Effect goods = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.388.1"),
                        icon = IconData.displays.goods,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.388.2"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.388.3"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.388.4"),
                            Mod.instance.Helper.Translation.Get("EffectsData.388.5"),
                            Mod.instance.Helper.Translation.Get("EffectsData.388.6"),
                            Mod.instance.Helper.Translation.Get("EffectsData.388.7")
                        }
                    };

                    return goods;

                case EffectPage.gravityWell:

                    Effect gravityWell = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.409"),
                        icon = IconData.displays.stars,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.411"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.412"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.323.1"),
                            Mod.instance.Helper.Translation.Get("EffectsData.415"),
                            Mod.instance.Helper.Translation.Get("EffectsData.416"),
                            Mod.instance.Helper.Translation.Get("EffectsData.417"),
                            Mod.instance.Helper.Translation.Get("EffectsData.418"),
                            Mod.instance.Helper.Translation.Get("EffectsData.419"),
                        }
                    };

                    return gravityWell;

                case EffectPage.monsters:

                        Effect monsters = new()
                        {
                            title = Mod.instance.Helper.Translation.Get("EffectsData.386.3"),
                            icon = IconData.displays.pals,
                            description = Mod.instance.Helper.Translation.Get("EffectsData.386.4"),
                            instruction = Mod.instance.Helper.Translation.Get("EffectsData.386.5"),
                            details = new()
                            {
                                Mod.instance.Helper.Translation.Get("EffectsData.386.6"),
                                Mod.instance.Helper.Translation.Get("EffectsData.386.7"),
                                Mod.instance.Helper.Translation.Get("EffectsData.386.8"),
                                Mod.instance.Helper.Translation.Get("EffectsData.386.9")
                            }
                        };

                    return monsters;

                case EffectPage.monsterbattles:

                    Effect monsterbattles = new()
                        {
                            title = Mod.instance.Helper.Translation.Get("EffectsData.390.8"),
                            icon = IconData.displays.pals,
                            description = Mod.instance.Helper.Translation.Get("EffectsData.390.9"),
                            instruction = Mod.instance.Helper.Translation.Get("EffectsData.390.10"),
                            details = new()
                    {
                        Mod.instance.Helper.Translation.Get("EffectsData.390.11"),
                        Mod.instance.Helper.Translation.Get("EffectsData.390.12"),
                        Mod.instance.Helper.Translation.Get("EffectsData.390.13"),
                        Mod.instance.Helper.Translation.Get("EffectsData.390.14"),
                        Mod.instance.Helper.Translation.Get("EffectsData.393.8"),
                        Mod.instance.Helper.Translation.Get("EffectsData.393.9"),
                        Mod.instance.Helper.Translation.Get("EffectsData.393.10"),
                    }
                        };

                    return monsterbattles;

                case EffectPage.distillery:


                        Effect distillery = new()
                        {
                            title = Mod.instance.Helper.Translation.Get("EffectsData.393.1"),
                            icon = IconData.displays.goods,
                            description = Mod.instance.Helper.Translation.Get("EffectsData.393.2"),
                            instruction = Mod.instance.Helper.Translation.Get("EffectsData.393.3"),
                            details = new()
                    {
                        Mod.instance.Helper.Translation.Get("EffectsData.393.4"),
                        Mod.instance.Helper.Translation.Get("EffectsData.393.5"),
                        Mod.instance.Helper.Translation.Get("EffectsData.393.6"),
                        Mod.instance.Helper.Translation.Get("EffectsData.393.7"),
                        Mod.instance.Helper.Translation.Get("EffectsData.401.1"),
                        Mod.instance.Helper.Translation.Get("EffectsData.401.2"),
                        Mod.instance.Helper.Translation.Get("EffectsData.401.3"),
                        Mod.instance.Helper.Translation.Get("EffectsData.401.4")
                    }
                        };


                    return distillery;

                case EffectPage.riteOfTheFates:

                    // ====================================================================
                    // Fates effects

                    Effect riteOfTheFates = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.340.16"),
                        icon = IconData.displays.fates,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.340.17"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.340.18"),

                    };

                    if (!Mod.instance.magic)
                    {

                        riteOfTheFates.details.Add(Mod.instance.Helper.Translation.Get("EffectsData.340.19"));

                    }

                    return riteOfTheFates;

                case EffectPage.whisk:

                    Effect whisk = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.447"),
                        icon = IconData.displays.fates,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.449"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.450"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.453"),
                            Mod.instance.Helper.Translation.Get("EffectsData.454"),
                            Mod.instance.Helper.Translation.Get("EffectsData.455"),
                            Mod.instance.Helper.Translation.Get("EffectsData.456"),
                            Mod.instance.Helper.Translation.Get("EffectsData.457"),
                        }
                    };

                    return whisk;

                case EffectPage.warpstrike:

                    Effect warpstrike = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.465"),
                        icon = IconData.displays.fates,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.467"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.468"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.471"),
                            Mod.instance.Helper.Translation.Get("EffectsData.472"),
                            Mod.instance.Helper.Translation.Get("EffectsData.473"),
                            Mod.instance.Helper.Translation.Get("EffectsData.474"),
                        }
                    };

                    return warpstrike;

                case EffectPage.curses:

                    Effect curses = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.480"),
                        icon = IconData.displays.fates,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.482"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.483"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.486"),
                            Mod.instance.Helper.Translation.Get("EffectsData.487"),
                            Mod.instance.Helper.Translation.Get("EffectsData.488"),
                            Mod.instance.Helper.Translation.Get("EffectsData.489"),
                            Mod.instance.Helper.Translation.Get("EffectsData.490"),
                        }
                    };

                    return curses;

                case EffectPage.tricks:

                    Effect tricks = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.498"),
                        icon = IconData.displays.fates,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.500"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.501"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.504"),
                            Mod.instance.Helper.Translation.Get("EffectsData.505"),
                            Mod.instance.Helper.Translation.Get("EffectsData.506"),
                            Mod.instance.Helper.Translation.Get("EffectsData.507"),
                            Mod.instance.Helper.Translation.Get("EffectsData.508"),
                        }
                    };

                    return tricks;

                case EffectPage.windsOfFate:

                    Effect windsOfFate = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.367.1"),
                        icon = IconData.displays.fates,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.367.2"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.367.3"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.367.4"),
                            Mod.instance.Helper.Translation.Get("EffectsData.367.5"),
                        }
                    };

                    return windsOfFate;

                case EffectPage.enchant:

                    Effect enchant = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.516"),
                        icon = IconData.displays.fates,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.518"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.519"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.522"),
                            Mod.instance.Helper.Translation.Get("EffectsData.523"),
                            Mod.instance.Helper.Translation.Get("EffectsData.524"),
                            Mod.instance.Helper.Translation.Get("EffectsData.525"),
                            Mod.instance.Helper.Translation.Get("EffectsData.526"),
                            Mod.instance.Helper.Translation.Get("EffectsData.356.1"),
                            Mod.instance.Helper.Translation.Get("EffectsData.356.2"),
                            Mod.instance.Helper.Translation.Get("EffectsData.527"),
                        }
                    };

                    return enchant;

                case EffectPage.riteOfEther:

                    Effect riteOfEther = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.340.20"),
                        icon = IconData.displays.ether,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.340.21"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.340.22"),

                    };

                    if (!Mod.instance.magic)
                    {

                        riteOfEther.details.Add(Mod.instance.Helper.Translation.Get("EffectsData.340.23"));

                    }

                    return riteOfEther;

                case EffectPage.dragonForm:

                    Effect dragonForm = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.557"),
                        icon = IconData.displays.ether,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.559") +
                        Mod.instance.Helper.Translation.Get("EffectsData.560"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.561"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.564"),
                            Mod.instance.Helper.Translation.Get("EffectsData.565"),
                        }
                    };

                    return dragonForm;

                case EffectPage.dragonFlight:

                    Effect dragonFlight = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.571"),
                        icon = IconData.displays.ether,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.573"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.574"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.577"),
                            Mod.instance.Helper.Translation.Get("EffectsData.578"),
                            Mod.instance.Helper.Translation.Get("EffectsData.579"),
                            Mod.instance.Helper.Translation.Get("EffectsData.580"),
                        }
                    };

                    return dragonFlight;

                case EffectPage.dragonBreath:

                    Effect dragonBreath = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.588"),
                        icon = IconData.displays.ether,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.590"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.591"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.594"),
                            Mod.instance.Helper.Translation.Get("EffectsData.595"),
                            Mod.instance.Helper.Translation.Get("EffectsData.596"),
                            Mod.instance.Helper.Translation.Get("EffectsData.597")
                        }
                    };

                    return dragonBreath;

                case EffectPage.dragonDive:

                    Effect dragonDive = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.605"),
                        icon = IconData.displays.ether,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.607"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.608"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.611") ,
                            Mod.instance.Helper.Translation.Get("EffectsData.612"),
                        }
                    };

                    return dragonDive;

                case EffectPage.dragonTreasure:

                        Effect dragonTreasure = new()
                        {
                            title = Mod.instance.Helper.Translation.Get("EffectsData.620"),
                            icon = IconData.displays.ether,
                            description = Mod.instance.Helper.Translation.Get("EffectsData.622"),
                            instruction = Mod.instance.Helper.Translation.Get("EffectsData.623"),
                            details = new()
                            {
                                Mod.instance.Helper.Translation.Get("EffectsData.626"),
                                Mod.instance.Helper.Translation.Get("EffectsData.627"),
                                Mod.instance.Helper.Translation.Get("EffectsData.628"),
                                Mod.instance.Helper.Translation.Get("EffectsData.629"),
                            }
                        };

                    return dragonTreasure;

                case EffectPage.riteOfBones:


                    Effect riteOfBones = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.340.24"),
                        icon = IconData.displays.witch,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.340.25"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.340.26"),

                    };

                    if (!Mod.instance.magic)
                    {

                        riteOfBones.details.Add(Mod.instance.Helper.Translation.Get("EffectsData.340.27"));

                    }

                    return riteOfBones;

                case EffectPage.corvidsSummon:

                    // ====================================================================

                    Effect corvidsSummon = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.313.1"),
                        icon = IconData.displays.witch,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.313.2"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.313.3") + Mod.instance.Helper.Translation.Get("EffectsData.313.4"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.346.1"),
                            Mod.instance.Helper.Translation.Get("EffectsData.313.5"),
                            Mod.instance.Helper.Translation.Get("EffectsData.313.6"),
                            Mod.instance.Helper.Translation.Get("EffectsData.313.7"),
                        }
                    };

                    return corvidsSummon;

                case EffectPage.corvidsRetrieve:

                    Effect corvidsRetrieve = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.324.1"),
                        icon = IconData.displays.witch,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.324.2"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.324.3"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.324.4"),
                            Mod.instance.Helper.Translation.Get("EffectsData.324.5"),
                        }
                    };

                    return corvidsRetrieve;

                case EffectPage.corvidsOpportunist:

                    Effect corvidsOpportunist = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.324.6"),
                        icon = IconData.displays.witch,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.324.7"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.324.8"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.324.9"),
                            Mod.instance.Helper.Translation.Get("EffectsData.324.10")
                        }
                    };

                    return corvidsOpportunist;

                case EffectPage.ledgerOfTheCircle:

                    // ====================================================================

                    Effect ledgerOfTheCircle = new()
                    {
                        title = Mod.instance.Helper.Translation.Get("EffectsData.362.1"),
                        icon = IconData.displays.heroes,
                        description = Mod.instance.Helper.Translation.Get("EffectsData.362.2"),
                        instruction = Mod.instance.Helper.Translation.Get("EffectsData.362.3"),
                        details = new()
                        {
                            Mod.instance.Helper.Translation.Get("EffectsData.362.4"),
                            Mod.instance.Helper.Translation.Get("EffectsData.362.5"),
                            Mod.instance.Helper.Translation.Get("EffectsData.362.6"),
                            Mod.instance.Helper.Translation.Get("EffectsData.377.1"),
                            Mod.instance.Helper.Translation.Get("EffectsData.377.2"),
                            Mod.instance.Helper.Translation.Get("EffectsData.377.3")
                        }
                    };

                    return ledgerOfTheCircle;

            }

            return null;

        }

        public static Dictionary<string,List<EffectPage>> RetrieveList()
        {

            Dictionary<string, List<EffectPage>> effects = new();

            effects[QuestHandle.approachEffigy] = new() { EffectPage.ritesOfTheDruids, };

            effects[QuestHandle.swordWeald] = new() { EffectPage.herbalism, EffectPage.bomblobbing, EffectPage.omens, };

            if (!Mod.instance.magic)
            {

                effects[QuestHandle.swordWeald].Add(EffectPage.attunement);

                effects[QuestHandle.swordWeald].Add(EffectPage.relicsets);
            
            }

            // ----------------------------------

            effects[QuestHandle.wealdOne] = new() { EffectPage.riteOfTheWeald, EffectPage.clearance };

            effects[QuestHandle.wealdTwo] = new() { EffectPage.gentleTouch, };

            effects[QuestHandle.wealdThree] = new() { EffectPage.cultivate };

            effects[QuestHandle.wealdFour] = new() { EffectPage.wildgrowth };

            effects[QuestHandle.wealdFive] = new() { EffectPage.rockfall, };

            if (!Mod.instance.magic)
            {

                effects[QuestHandle.wealdFive].Add(EffectPage.crowhammer);

            }

            effects[QuestHandle.chargeUps] = new() { EffectPage.sap, };

            effects[QuestHandle.mistsOne] = new() { EffectPage.riteOfMists, EffectPage.cursorTargetting, EffectPage.sunder };

            effects[QuestHandle.mistsTwo] = new() { EffectPage.artifice, EffectPage.totemShrines, EffectPage.campfire, };

            effects[QuestHandle.mistsThree] = new() { EffectPage.rodMaster, };

            effects[QuestHandle.mistsFour] = new() { EffectPage.smite, EffectPage.veilCharge, };

            effects[QuestHandle.questEffigy] = new() { EffectPage.summonWisps, EffectPage.summonWrath, };

            effects[QuestHandle.starsOne] = new() { EffectPage.riteOfTheStars, EffectPage.meteorRain, EffectPage.starBurst, };

            if (!Mod.instance.magic)
            {

                effects[QuestHandle.orders] = new() { EffectPage.goods, };

            }

            effects[QuestHandle.starsTwo] = new() { EffectPage.gravityWell, };

            if (!Mod.instance.magic)
            {

                effects[QuestHandle.captures] = new() { EffectPage.monsters, EffectPage.monsterbattles, };

            }

            effects[QuestHandle.distillery] = new() { EffectPage.distillery, };

            effects[QuestHandle.fatesOne] = new() { EffectPage.riteOfTheFates, EffectPage.whisk, };

            effects[QuestHandle.fatesTwo] = new() { EffectPage.warpstrike, EffectPage.curses, };

            effects[QuestHandle.fatesThree] = new() { EffectPage.tricks, };

            effects[QuestHandle.questJester] = new() { EffectPage.windsOfFate, };

            effects[QuestHandle.fatesFour] = new() { EffectPage.enchant, };

            effects[QuestHandle.etherOne] = new() { EffectPage.riteOfEther, EffectPage.dragonForm, EffectPage.dragonFlight, };

            effects[QuestHandle.etherTwo] = new() { EffectPage.dragonBreath, };

            effects[QuestHandle.etherThree] = new() { EffectPage.dragonDive, };

            if (!Mod.instance.magic)
            {

                effects[QuestHandle.etherFour] = new() { EffectPage.dragonTreasure, };

            }

            effects[QuestHandle.witchOne] = new() { EffectPage.riteOfBones, EffectPage.corvidsSummon, };

            effects[QuestHandle.witchTwo] = new() { EffectPage.corvidsRetrieve, };

            effects[QuestHandle.witchThree] = new() { EffectPage.corvidsOpportunist, };

            effects[QuestHandle.swordHeirs] = new() { EffectPage.ledgerOfTheCircle, };

            return effects;

        }

    }

    public class Effect
    {

        // -----------------------------------------------
        // journal

        public string title;

        public IconData.displays icon = IconData.displays.none;

        public string description;

        public string instruction;

        public List<string> details = new();


    }

}
