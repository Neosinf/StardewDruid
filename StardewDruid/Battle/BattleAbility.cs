using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewValley.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Battle
{

    public class BattleAbility
    {

        public enum battleabilities
        {

            attack,
            block,
            special,
            tackle,
            staredown,
            item,
            
            absorb,
            counter,
            harm,

            recover,
            buff,
            stun,
            daze,
            burn,

            wing,
            dustup,
            tail,
            whisper,

            absorption,
            smoke,
            smokecounter,
            ghostform,

            batdive,
            bodyslam,
            dragondive,

            screech,
            splatter,
            explode,
            breath,
            scares,

            dragonsweep,

            dragonsap,
            dragonmists,
            dragonslam,
            dragoncritical,
            dragonstance,
            dragonstare,

            starbreath,
            chaosbreath,
            etherbreath,

        }

        public battleabilities ability;

        public BattleCombatant.battleoptions option;

        public IconData.displays display;

        public IconData.impacts impact = IconData.impacts.none;

        public MissileHandle.missiles missile = MissileHandle.missiles.none;

        public string button;

        public string title;

        public string description;

        public string onTry;

        public string onHit;

        public string onCrit;

        public string onBlock;

        public string onBypass;

        public string onMiss;

        public int damage;

        public int speed;

        public int accuracy;

        public int defense;

        public int resist;

        public int effect;

        public int counter;

        public int absorb;

        public int harm;

        public bool self;

        public bool special;

        public bool reaction;

        public BattleBuff.battlebuffs buff = BattleBuff.battlebuffs.none;

        public BattleAbility()
        {



        }

        public BattleAbility(battleabilities Ability)
        {

            ability = Ability;

            AbilityBase();

            AbilitySpecial();

        }

        public void AbilityBase()
        {

            switch (ability)
            {

                default:
                case battleabilities.wing:
                case battleabilities.dustup:
                case battleabilities.tail:
                case battleabilities.whisper:
                case battleabilities.dragonsweep:
                case battleabilities.dragonsap:
                case battleabilities.dragoncritical:
                case battleabilities.attack:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.1950");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.1951");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.1952");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.1953");

                    onHit = Mod.instance.Helper.Translation.Get("BattleHandle.388.1954");

                    onCrit = Mod.instance.Helper.Translation.Get("BattleAbility.390.1");

                    onBlock = Mod.instance.Helper.Translation.Get("BattleHandle.388.1955");

                    onBypass = Mod.instance.Helper.Translation.Get("BattleHandle.388.1956");

                    onMiss = Mod.instance.Helper.Translation.Get("BattleHandle.388.1957");

                    display = IconData.displays.quest;

                    impact = IconData.impacts.flashbang;

                    damage = 50;

                    speed = 75;

                    accuracy = 80;

                    defense = 50;

                    effect = 50;

                    resist = 50;

                    break;

                case battleabilities.absorption:
                case battleabilities.smoke:
                case battleabilities.ghostform:
                case battleabilities.dragonmists:
                case battleabilities.dragonstance:
                case battleabilities.block:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.1968");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.1969");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.1970");

                    display = IconData.displays.shield;

                    defense = 80;

                    resist = 70;

                    counter = 25;

                    self = true;

                    break;

                case battleabilities.screech:
                case battleabilities.splatter:
                case battleabilities.explode:
                case battleabilities.breath:
                case battleabilities.scares:
                case battleabilities.starbreath:
                case battleabilities.chaosbreath:
                case battleabilities.etherbreath:
                case battleabilities.special:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.1983");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.1984");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.1985");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.1986");

                    onHit = Mod.instance.Helper.Translation.Get("BattleHandle.388.1987");

                    onCrit = Mod.instance.Helper.Translation.Get("BattleAbility.390.2");

                    onBlock = Mod.instance.Helper.Translation.Get("BattleHandle.388.1988");

                    onBypass = Mod.instance.Helper.Translation.Get("BattleHandle.388.1989");

                    onMiss = Mod.instance.Helper.Translation.Get("BattleHandle.388.1990");

                    display = IconData.displays.effect;

                    impact = IconData.impacts.impact;

                    damage = 100;

                    speed = 50;

                    accuracy = 70;

                    defense = 50;

                    effect = 70;

                    resist = 60;

                    special = true;

                    break;

                case battleabilities.batdive:
                case battleabilities.bodyslam:
                case battleabilities.dragondive:
                case battleabilities.dragonslam:
                case battleabilities.tackle:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2002");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2003");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.2004");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2005");

                    onHit = Mod.instance.Helper.Translation.Get("BattleHandle.388.2006");

                    onCrit = Mod.instance.Helper.Translation.Get("BattleAbility.390.3");

                    onBlock = Mod.instance.Helper.Translation.Get("BattleHandle.388.2007");

                    onBypass = Mod.instance.Helper.Translation.Get("BattleHandle.388.2008");

                    onMiss = Mod.instance.Helper.Translation.Get("BattleHandle.388.2009");

                    display = IconData.displays.relic;

                    impact = IconData.impacts.sparkbang;

                    damage = 75;

                    speed = 100;

                    accuracy = 60;

                    defense = 40;

                    effect = 80;

                    resist = 30;

                    break;

                case battleabilities.dragonstare:
                case battleabilities.staredown:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2018");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2019");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2020");

                    onHit = Mod.instance.Helper.Translation.Get("BattleHandle.388.2021");

                    display = IconData.displays.skull;

                    buff = BattleBuff.battlebuffs.slow;

                    damage = 0;

                    speed = 0;

                    accuracy = 70;

                    defense = 70;

                    effect = 50;

                    resist = 50;

                    break;

                case battleabilities.item:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2029");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2030");

                    display = IconData.displays.herbalism;

                    self = true;

                    damage = 0;

                    speed = 80;

                    accuracy = 0;

                    defense = 50;

                    resist = 50;

                    break;

                case battleabilities.smokecounter:
                case battleabilities.counter:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2039");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2040");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2041");

                    onHit = Mod.instance.Helper.Translation.Get("BattleHandle.388.2042");

                    impact = IconData.impacts.flashbang;

                    reaction = true;

                    effect = 70;

                    break;

                case battleabilities.absorb:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2051");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2052");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2053");

                    onHit = Mod.instance.Helper.Translation.Get("BattleHandle.388.2054");

                    reaction = true;

                    self = true;

                    break;

                case battleabilities.harm:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2062");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2063");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2064");

                    onHit = Mod.instance.Helper.Translation.Get("BattleHandle.388.2065");

                    reaction = true;

                    self = true;

                    break;

                case battleabilities.recover:

                    title = Mod.instance.Helper.Translation.Get("BattleAbility.390.4");

                    reaction = true;

                    self = true;

                    break;

                case battleabilities.buff:

                    title = Mod.instance.Helper.Translation.Get("BattleAbility.390.5");

                    reaction = true;

                    self = true;

                    break;

                case battleabilities.stun:

                    title = Mod.instance.Helper.Translation.Get("BattleAbility.390.6");

                    onHit = Mod.instance.Helper.Translation.Get("BattleAbility.390.7");

                    reaction = true;

                    self = true;

                    break;

                case battleabilities.daze:

                    title = Mod.instance.Helper.Translation.Get("BattleAbility.390.8");

                    onTry = Mod.instance.Helper.Translation.Get("BattleAbility.390.9");

                    onHit = Mod.instance.Helper.Translation.Get("BattleAbility.390.10");

                    self = true;

                    damage = 25;

                    break;

                case battleabilities.burn:

                    title = Mod.instance.Helper.Translation.Get("BattleAbility.391.1");

                    onTry = Mod.instance.Helper.Translation.Get("BattleAbility.391.2");

                    onHit = Mod.instance.Helper.Translation.Get("BattleAbility.391.3");

                    reaction = true;

                    self = true;

                    damage = 15;

                    break;

            }


        }

        public void AbilitySpecial()
        {

            switch (ability)
            {

                case battleabilities.wing:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2079");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2080");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.2081");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2082");

                    damage += 15;

                    break;

                case battleabilities.dustup:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2086");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2087");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.2088");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2089");

                    impact = IconData.impacts.puff;

                    damage -= 10;

                    buff = BattleBuff.battlebuffs.sense;

                    effect += 10;

                    break;

                case battleabilities.tail:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2096");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2097");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.2098");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2099");

                    buff = BattleBuff.battlebuffs.slow;

                    effect += 10;

                    break;

                case battleabilities.whisper:

                    button = Mod.instance.Helper.Translation.Get("BattleAbility.390.11");

                    title = Mod.instance.Helper.Translation.Get("BattleAbility.390.12"); 

                    description = Mod.instance.Helper.Translation.Get("BattleAbility.390.13");

                    onTry = Mod.instance.Helper.Translation.Get("BattleAbility.390.14");

                    buff = BattleBuff.battlebuffs.sense;

                    special = true;

                    damage = 40;

                    accuracy = 70;

                    effect = 70;

                    defense = 40;

                    resist = 60;

                    break;

                // ------------------------------------------------------

                case battleabilities.smoke:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2105");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2106");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.2107");

                    onBlock = Mod.instance.Helper.Translation.Get("BattleHandle.388.2108");

                    counter += 30;

                    break;

                case battleabilities.smokecounter:

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2112");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2113");

                    impact = IconData.impacts.smoke;

                    buff = BattleBuff.battlebuffs.sense;

                    effect = 70;

                    break;

                case battleabilities.absorption:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2119");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2120");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.2121");

                    onBlock = Mod.instance.Helper.Translation.Get("BattleHandle.388.2122");

                    absorb = 40;

                    break;

                case battleabilities.ghostform:

                    button = Mod.instance.Helper.Translation.Get("BattleAbility.390.15");

                    title = Mod.instance.Helper.Translation.Get("BattleAbility.390.16");

                    description = Mod.instance.Helper.Translation.Get("BattleAbility.390.17");

                    onBlock = Mod.instance.Helper.Translation.Get("BattleAbility.390.18");

                    buff = BattleBuff.battlebuffs.daze;

                    effect = 70;

                    defense = 90;

                    resist = 30;

                    break;

                // ------------------------------------------------------

                case battleabilities.batdive:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2127");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2128");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.2129");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2130");

                    damage -= 10;

                    accuracy += 10;

                    defense += 10;

                    break;

                case battleabilities.bodyslam:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2136");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2137");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.2138");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2139");

                    damage += 50;

                    accuracy += 10;

                    defense = 20;

                    speed = 20;

                    impact = IconData.impacts.splatter;

                    break;

                case battleabilities.dragondive:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2147");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2148");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.2149");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2150");

                    damage += 25;

                    accuracy -= 15;

                    buff = BattleBuff.battlebuffs.stun;

                    effect += 10;

                    break;

                // ------------------------------------------------------

                case battleabilities.screech:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2158");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2159");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.2160");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2161");

                    impact = IconData.impacts.none;

                    missile = MissileHandle.missiles.echo;

                    damage -= 20;

                    buff = BattleBuff.battlebuffs.daze;

                    effect += 10;

                    break;

                case battleabilities.splatter:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2169");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2170");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.2171");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2172");

                    impact = IconData.impacts.splatter;

                    accuracy -= 10;

                    buff = BattleBuff.battlebuffs.slow;

                    effect += 10;

                    break;

                case battleabilities.explode:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2179");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2180");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.2181");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2182");

                    impact = IconData.impacts.bomb;

                    damage += 50;

                    accuracy += 20;

                    harm = 35;

                    break;

                case battleabilities.breath:

                    button = Mod.instance.Helper.Translation.Get("BattleHandle.388.2189");

                    title = Mod.instance.Helper.Translation.Get("BattleHandle.388.2190");

                    description = Mod.instance.Helper.Translation.Get("BattleHandle.388.2191");

                    onTry = Mod.instance.Helper.Translation.Get("BattleHandle.388.2192");

                    impact = IconData.impacts.none;

                    missile = MissileHandle.missiles.bubbleecho;

                    damage += 25;

                    speed -= 25;

                    break;

                case battleabilities.scares:

                    button = Mod.instance.Helper.Translation.Get("BattleAbility.390.19");

                    title = Mod.instance.Helper.Translation.Get("BattleAbility.390.20");

                    description = Mod.instance.Helper.Translation.Get("BattleAbility.390.21");

                    onTry = Mod.instance.Helper.Translation.Get("BattleAbility.390.22");

                    impact = IconData.impacts.none;

                    missile = MissileHandle.missiles.deathecho;

                    speed = 0;

                    defense += 15;

                    resist += 15;

                    break;


                case battleabilities.dragonsweep:

                    button = "sweep";

                    title = "Dragon Sweep";

                    description = "A strong, wide arcing attack with poor defense";

                    onTry = " sweeps with their tail!";

                    damage += 10;

                    accuracy += 10;

                    defense -= 20;

                    resist -= 20;

                    break;

                case battleabilities.dragonsap:

                    button = "sap";

                    title = "Sap Strength";

                    description = "The power of the Weald saps the strength of foes struck by this attack";

                    onTry = " attempts a strength sapping strike!";

                    display = IconData.displays.weald;

                    damage -= 10;

                    absorb = 20;

                    break;

                case battleabilities.dragonmists:

                    button = "mists";

                    title = "Mist Form";

                    description = "The power of Mists creates a defensive mists that increases resistance against special attacks and effects";

                    display = IconData.displays.mists;

                    resist += 20;

                    break;

                case battleabilities.dragonslam:

                    button = "slam";

                    title = "Star Slam";

                    description = "The power of the Stars embues a heavy attack that is difficult to lane but may leave the opponent dazed";

                    buff = BattleBuff.battlebuffs.daze;

                    display = IconData.displays.stars;

                    effect += 30;

                    accuracy -= 20;

                    break;

                case battleabilities.dragoncritical:


                    button = "critical";

                    title = "Fates Critical";

                    description = "The power of the Fates offers a critical strike that is also far more likely to miss";

                    display = IconData.displays.fates;

                    damage += 75;

                    accuracy -= 50;

                    break;

                case battleabilities.dragonstance:

                    button = "stance";

                    title = "Dragon Stance";

                    description = "Adopt an Ether-charged stance that deals powerful counter strikes";

                    display = IconData.displays.ether;

                    counter += 25;

                    break;

                case battleabilities.dragonstare:

                    onTry = " glared at their opponent with eyes of mystery and prophecy";

                    onHit = " dazzled their opponent!";

                    buff = BattleBuff.battlebuffs.daze;

                    speed += 20;

                    effect += 20;

                    break;

                case battleabilities.starbreath:

                    button = "breath";

                    title = "Fire Breath";

                    description = "A consistent breath attack that is difficult to fight back against";

                    onTry = " attempts to singe their opponent with fire breath!";

                    onHit = " singed their opponent for ";

                    display = IconData.displays.blaze;

                    buff = BattleBuff.battlebuffs.burn;

                    damage -= 20;

                    defense += 10;

                    resist += 10;

                    break;

                case battleabilities.chaosbreath:

                    button = "breath";

                    title = "Chaos Breath";

                    description = "Wildly chaotic breath attack that could be devastating or completely ineffective";

                    onTry = " attempts to singe their opponent with flames of chaos!";

                    onHit = " singed their opponent for ";

                    display = IconData.displays.blaze;

                    buff = BattleBuff.battlebuffs.burn;

                    damage += 50;

                    speed += 30;

                    effect -= 30;

                    resist -= 30;

                    defense -= 30;

                    break;

                case battleabilities.etherbreath:

                    button = "breath";

                    title = "Ether Breath";

                    description = "An overwhelming breath attack that is sure to singe the opponent";

                    onTry = " attempts to singe their opponent with ethereal power!";

                    onHit = " singed their opponent for ";

                    display = IconData.displays.blaze;

                    buff = BattleBuff.battlebuffs.burn;

                    effect += 20;

                    break;

            }

        }

    }

}
