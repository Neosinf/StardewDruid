using StardewDruid.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Battle
{
    public class BattleBuff
    {

        // buffs ------------------------------------------------------------

        public enum battlebuffs
        {
            none,

            strength,
            speed,
            resist,
            critical,
            counter,

            stun,
            daze,
            sense,
            slow,
            burn,

            fatigue,

        }

        public battlebuffs buff;

        public string onApply;

        public string onRecover;

        public bool permanent;

        public int effect;

        public IconData.displays display;

        public BattleBuff()
        {

        }

        public BattleBuff(battlebuffs Buff)
        {

            buff = Buff;

            BuffBase();

        }

        public void BuffBase()
        {

            switch (buff)
            {

                case battlebuffs.strength:

                    display = IconData.displays.strength;

                    permanent = true;

                    break;

                case battlebuffs.speed:

                    display = IconData.displays.speed;

                    permanent = true;

                    break;

                case battlebuffs.resist:

                    display = IconData.displays.holy;

                    permanent = true;

                    break;

                case battlebuffs.critical:

                    display = IconData.displays.omens;

                    permanent = true;

                    break;

                case battlebuffs.counter:

                    display = IconData.displays.quest;

                    permanent = true;

                    break;

                case battlebuffs.stun:

                    onApply = Mod.instance.Helper.Translation.Get("BattleBuff.390.1");

                    onRecover = Mod.instance.Helper.Translation.Get("BattleBuff.390.2");

                    display = IconData.displays.knock;

                    effect = 3;

                    break;

                case battlebuffs.daze:

                    onApply = Mod.instance.Helper.Translation.Get("BattleBuff.390.3");

                    onRecover = Mod.instance.Helper.Translation.Get("BattleBuff.390.4");

                    display = IconData.displays.glare;

                    effect = 4;

                    break;

                case battlebuffs.sense:

                    onApply = Mod.instance.Helper.Translation.Get("BattleBuff.390.5");

                    onRecover = Mod.instance.Helper.Translation.Get("BattleBuff.390.6");

                    display = IconData.displays.blind;

                    effect = 5;

                    break;

                case battlebuffs.slow:

                    onApply = Mod.instance.Helper.Translation.Get("BattleBuff.390.7");

                    onRecover = Mod.instance.Helper.Translation.Get("BattleBuff.390.8");

                    display = IconData.displays.down;

                    effect = 5;

                    break;

                case battlebuffs.burn:

                    onApply = Mod.instance.Helper.Translation.Get("BattleBuff.391.1");

                    onRecover = Mod.instance.Helper.Translation.Get("BattleBuff.391.2");

                    display = IconData.displays.blaze;

                    effect = 5;

                    break;

                case battlebuffs.fatigue:

                    onApply = Mod.instance.Helper.Translation.Get("BattleBuff.393.1");

                    display = IconData.displays.fatigue;

                    permanent = true;

                    break;

            }

        }

    }

}
