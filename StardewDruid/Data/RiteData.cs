using StardewDruid.Cast;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StardewDruid.Data.StringData;

namespace StardewDruid.Data
{
    public static class RiteData
    {

        public enum riteStrings
        {

            openJournal,
            riteTool,
            noToolAttunement,
            defaultToolAttunement,
            nothingHappened,
            invalidLocation,
            energyContinue,
            energyRite,
            druidFreneticism,
            speedIncrease,
            normalAttunementActive,
            slotAttunementActive,
            noRiteAttuned,
            clickJournal,

            chargeWealdName,
            chargeWealdDescription,
            chargeMistsName,
            chargeMistsDescription,
            chargeStarsName,
            chargeStarsDescription,
            chargeFatesName,
            chargeFatesDescription,
            chargeWitchName,
            chargeWitchDescription,

            circleofdruids,
            riteoftheweald,
            riteofmists,
            riteofthestars,
            riteofthevoide,
            riteofthefates,
            riteofether,
            riteofthewitch,
            bombthrows,
            windslash,
        }

        public static string RiteNames(Cast.Rite.Rites rite)
        {
            switch (rite)
            {
                default:
                case Cast.Rite.Rites.none:
                    return "No Rite Selected";
                case Cast.Rite.Rites.weald:
                    return Strings(riteStrings.riteoftheweald);
                case Cast.Rite.Rites.mists:
                    return Strings(riteStrings.riteofmists);
                case Cast.Rite.Rites.stars:
                    return Strings(riteStrings.riteofthestars);
                case Cast.Rite.Rites.voide:
                    return Strings(riteStrings.riteofthevoide);
                case Cast.Rite.Rites.fates:
                    return Strings(riteStrings.riteofthefates);
                case Cast.Rite.Rites.ether:
                    return Strings(riteStrings.riteofether);
                case Cast.Rite.Rites.witch:
                    return Strings(riteStrings.riteofthewitch);

            }

        }

        public static string BuffNames(IconData.displays display = IconData.displays.none)
        {
            switch (display)
            {
                default:
                case IconData.displays.none:
                    return "No Rite Selected";
                case IconData.displays.weald:
                    return Strings(riteStrings.riteoftheweald);
                case IconData.displays.mists:
                    return Strings(riteStrings.riteofmists);
                case IconData.displays.stars:
                    return Strings(riteStrings.riteofthestars);
                case IconData.displays.voide:
                    return Strings(riteStrings.riteofthevoide);
                case IconData.displays.fates:
                    return Strings(riteStrings.riteofthefates);
                case IconData.displays.ether:
                    return Strings(riteStrings.riteofether);
                case IconData.displays.witch:
                    return Strings(riteStrings.riteofthewitch);
                case IconData.displays.bombs:
                    return Strings(riteStrings.bombthrows);
                case IconData.displays.winds:
                    return Strings(riteStrings.windslash);
            }

        }

        public static string Strings(riteStrings stringId)
        {

            switch (stringId)
            {

                default:

                    return string.Empty;


                case riteStrings.normalAttunementActive:

                    return "Normal Attunement: Rite is based on the attunement of current tool / weapon";

                case riteStrings.slotAttunementActive:

                    return "Slot Attunement: Rite is based on currently selected inventory slot";

                case riteStrings.openJournal:

                    return "to open Druid Journal and get started";

                case riteStrings.druidFreneticism:

                    return "Druidic Freneticism";

                case riteStrings.speedIncrease:

                    return "Speed increased when casting amongst Grass";

                case riteStrings.riteTool:

                    return "Rite casts require a melee weapon or tool";

                case riteStrings.noToolAttunement:

                    return "This tool has not been attuned to a rite";

                case riteStrings.defaultToolAttunement:

                    return "This tool is not attuned, and will cast {{rite}} by default";//.Tokens(new { rite = RiteData.RiteNames(Mod.instance.save.rite), });

                case riteStrings.nothingHappened:

                    return "Nothing happened... ";

                case riteStrings.invalidLocation:

                    return "Unable to reach the otherworldly plane from this location";

                case riteStrings.energyContinue:

                    return "Not enough energy to continue rite";

                case riteStrings.energyRite:

                    return "Not enough energy to perform rite";

                case riteStrings.noRiteAttuned:

                    return "No rite attuned to slot ";

                case riteStrings.clickJournal:

                    return "Click to open Journal";


                case riteStrings.circleofdruids:

                    return "Circle of Druids";

                case riteStrings.riteoftheweald:

                    return "Rite of the Weald";

                case riteStrings.riteofmists:

                    return "Rite of Mists";

                case riteStrings.riteofthestars:

                    return "Rite of the Stars";

                case riteStrings.riteofthevoide:

                    return "Rite of the Voide";

                case riteStrings.riteofthefates:

                    return "Rite of the Fates";

                case riteStrings.riteofether:

                    return "Rite of Ether";

                case riteStrings.riteofthewitch:

                    return "Rite of the Witch";

                case riteStrings.bombthrows:

                    return "Bomb Throws";

                case riteStrings.windslash:

                    return "Wind Slash";


                case riteStrings.chargeWealdName:

                    return "Charge: Sap";

                case riteStrings.chargeWealdDescription:

                    return "Bombs and Windslash drain enemy health and stamina";

                case riteStrings.chargeMistsName:

                    return "Charge: Shock";

                case riteStrings.chargeMistsDescription:

                    return "Bombs and Windslash apply the shock debuff";

                case riteStrings.chargeStarsName:

                    return "Charge: Knock";

                case riteStrings.chargeStarsDescription:

                    return "Bombs and Windslash apply the stun debuff";

                case riteStrings.chargeFatesName:

                    return "Charge: Curse";

                case riteStrings.chargeFatesDescription:

                    return "Bombs and Windslash apply curse debuffs";

                case riteStrings.chargeWitchName:

                    return "Charge: Glean";

                case riteStrings.chargeWitchDescription:

                    return "Bombs and Windslash effects yield more omens, trophies";

            }

        }

    }

}
