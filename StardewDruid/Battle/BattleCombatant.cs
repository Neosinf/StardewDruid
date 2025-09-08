using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Battle
{
    public class BattleCombatant
    {

        public CharacterHandle.characters championType;

        public bool champion;

        public string name;

        public string possessive;

        public string title;

        public int level;

        public string levelString;

        public int max;

        public int min;

        public int health;

        public int vitality;

        public int attack;

        public int resist;

        public int speed;

        public int experience;

        public int bounty;

        public int critical;

        public int counter;

        public int buff;

        public int fatigue;

        public int resilience;

        public int itemStat;

        public Dictionary<battleoptions, BattleAbility.battleabilities> moveset = new();

        public Dictionary<BattleAbility.battleabilities, BattleAbility> abilities = new();

        public List<BattleBuff> debuffs = new();

        // ---------------------------------------------------

        public enum battleoptions
        {
            pending,
            chosen,
            attack,
            block,
            tackle,
            special,
            staredown,
            absorb,
            counter,
            harm,
            stun,
            daze,
            burn,
            recover,
            buff,
            item,
            forfeit,
        }

        public battleoptions chosen;

        public ApothecaryHandle.items item;

        public BattleCombatant()
        {

        }

        public BattleCombatant(int Level)
        {

            championType = CharacterHandle.characters.Bat;

            name = StardewValley.Dialogue.randomName();

            level = Level;

            levelString = StringData.Get(StringData.str.level, new { level = Level });

            possessive = StringData.Get(StringData.str.your);

            title = Mod.instance.Helper.Translation.Get("BattleHandle.388.1701");

            moveset = BaseMoveset();

            vitality = Mod.instance.randomIndex.Next(10,21) * 5;

            attack = Mod.instance.randomIndex.Next(10, 21) * 5;

            speed = Mod.instance.randomIndex.Next(10, 21) * 5;

            resist = Mod.instance.randomIndex.Next(10, 21) * 2;

            resilience = 10;

            fatigue = -10;

            SetHealth();

            LoadMoves();

            SetRewards();

        }

        public BattleCombatant(PalData pal)
        {

            championType = pal.type;

            name = pal.name;

            int Level = PalHandle.UnitLevel(pal.experience);

            level = Level;

            levelString = StringData.Get(StringData.str.level, new { level = Level });

            possessive = StringData.Get(StringData.str.your);

            title = PalHandle.PalTitle(pal.type, pal.scheme);

            moveset = BaseMoveset();

            moveset = PalHandle.SpecialMoves(moveset, pal.type, level);

            vitality = PalHandle.HealthLevel(pal.type, level, pal.health);

            attack = PalHandle.AttackLevel(pal.type, level, pal.attack);

            speed = PalHandle.SpeedLevel(pal.type, level, pal.speed);

            resist = PalHandle.ResistLevel(pal.type, level, pal.resist);

            resilience = 8 + PalHandle.LoveLevel(pal.love);

            fatigue = 0 - resilience;

            SetHealth();

            LoadMoves();

            SetRewards();

        }

        public BattleCombatant(CharacterHandle.characters Type, int Level, int Health, int Attack, int Speed, int Resist, int Scheme)
        {

            championType = Type;

            name = StardewValley.Dialogue.randomName();

            level = Level;

            levelString = StringData.Get(StringData.str.level, new { level = Level });

            possessive = StringData.Get(StringData.str.your);

            title = PalHandle.PalTitle(Type, Scheme);

            moveset = BaseMoveset();

            moveset = PalHandle.SpecialMoves(moveset, Type, level);

            vitality = PalHandle.HealthLevel(Type, level, Health);

            attack = PalHandle.AttackLevel(Type, level, Attack);

            speed = PalHandle.SpeedLevel(Type, level, Speed);

            resist = PalHandle.ResistLevel(Type, level, Resist);

            resilience = 10;

            fatigue = -10;

            SetHealth();

            LoadMoves();

            SetRewards();

        }

        public BattleCombatant(BattleDragon Dragon, int Level)
        {

            championType = CharacterHandle.characters.Dragon;

            name = Dragon.useDragon.anchor.Name;

            level = Level;

            levelString = StringData.Get(StringData.str.level, new { level = Level });

            possessive = StringData.Get(StringData.str.your);

            title = BattleDragon.DragonTitle();

            moveset = BaseMoveset();

            moveset = BattleDragon.BattleMoveset(moveset);

            vitality = BattleDragon.BattleVitality(Level);

            attack = BattleDragon.BattleAttack(Level);

            speed = BattleDragon.BattleSpeed(Level);

            resist = BattleDragon.BattleResist(Level);

            resilience = 12;

            fatigue = -12;

            SetHealth();

            LoadMoves();

            SetRewards();

        }

        public static Dictionary<BattleCombatant.battleoptions, BattleAbility.battleabilities> BaseMoveset()
        {

            Dictionary<BattleCombatant.battleoptions, BattleAbility.battleabilities> moveset = new()
            {

                [BattleCombatant.battleoptions.attack] = BattleAbility.battleabilities.attack,

                [BattleCombatant.battleoptions.block] = BattleAbility.battleabilities.block,

                [BattleCombatant.battleoptions.special] = BattleAbility.battleabilities.special,

                [BattleCombatant.battleoptions.tackle] = BattleAbility.battleabilities.tackle,

                [BattleCombatant.battleoptions.staredown] = BattleAbility.battleabilities.staredown,

                [BattleCombatant.battleoptions.item] = BattleAbility.battleabilities.item,

                [BattleCombatant.battleoptions.counter] = BattleAbility.battleabilities.counter,

                [BattleCombatant.battleoptions.absorb] = BattleAbility.battleabilities.absorb,

                [BattleCombatant.battleoptions.harm] = BattleAbility.battleabilities.harm,

                [BattleCombatant.battleoptions.recover] = BattleAbility.battleabilities.recover,

                [BattleCombatant.battleoptions.buff] = BattleAbility.battleabilities.buff,

                [BattleCombatant.battleoptions.stun] = BattleAbility.battleabilities.stun,

                [BattleCombatant.battleoptions.daze] = BattleAbility.battleabilities.daze,

                [BattleCombatant.battleoptions.burn] = BattleAbility.battleabilities.burn,

            };

            return moveset;

        }

        public void SetHealth()
        {

            int vital = Math.Max(50, vitality * 3);

            health = vital;

            max = vital;

            min = vital;

            critical = Math.Max(10, (int)(attack / 8));

            counter = Math.Max(20, (int)(vitality / 3));

        }

        public void LoadMoves()
        {

            foreach (KeyValuePair<battleoptions, BattleAbility.battleabilities> set in moveset)
            {

                abilities[set.Value] = new BattleAbility(set.Value);

            }

        }

        public void SetRewards()
        {

            experience = 2 * level;

            bounty = 200 * level;

        }

        public float HealthBar()
        {

            if (health > min)
            {

                int healthleft = health - min;

                health -= Math.Max(2, healthleft / 30);

                if (health < min)
                {

                    health = min;

                }

            }
            else if (health < min)
            {

                int healthleft = min - health;

                health += Math.Max(2, healthleft / 30);

                if (health > min)
                {

                    health = min;

                }

            }

            return health / (float)max;

        }

        public List<BattleMove> AttemptRecover()
        {

            List<BattleMove> buffMoves = new();

            // buffs -----------------------------------

            for (int b = debuffs.Count - 1; b >= 0; b--)
            {

                if (debuffs[b].permanent)
                {

                    continue;

                }

                if (Mod.instance.randomIndex.Next(debuffs[b].effect * 15) <= Mod.instance.randomIndex.Next(resist))
                {

                    buffMoves.Add(new()
                    {
                        ability = moveset[battleoptions.recover],
                        option = battleoptions.recover,
                        champion = champion,
                        reaction = true,
                        buff = debuffs[b].buff
                    });

                    debuffs.RemoveAt(b);

                    continue;

                }

                debuffs[b].effect -= 1;

                if (debuffs[b].effect <= 0)
                {

                    buffMoves.Add(new()
                    {
                        ability = moveset[battleoptions.recover],
                        option = battleoptions.recover,
                        champion = champion,
                        reaction = true,
                        buff = debuffs[b].buff
                    });

                    debuffs.RemoveAt(b);


                }
                else
                if (debuffs[b].buff == BattleBuff.battlebuffs.burn)
                {

                    buffMoves.Add(new()
                    {
                        ability = moveset[battleoptions.burn],
                        option = battleoptions.burn,
                        champion = champion,
                        reaction = true,
                        self = true,
                        hit = true,
                        damage = (int)(max / 15),
                    });

                }
                
            }

            if(fatigue == 1)
            {

                buffMoves.Add(new()
                {
                    ability = moveset[battleoptions.buff],
                    option = battleoptions.buff,
                    champion = champion,
                    buff = BattleBuff.battlebuffs.fatigue,
                    reaction = true,
                    hit = true,
                });

            }

            return buffMoves;

        }

        public BattleMove CreateMove(battleoptions option = battleoptions.chosen, bool ignoreDebuff = false)
        {

            BattleMove move = new();

            if (!ignoreDebuff)
            {

                for (int b = debuffs.Count - 1; b >= 0; b--)
                {

                    switch (debuffs[b].buff)
                    {

                        case BattleBuff.battlebuffs.stun:

                            option = battleoptions.stun;

                            break;

                        case BattleBuff.battlebuffs.daze:

                            if (Mod.instance.randomIndex.Next(50) > (resist / 3))
                            {

                                option = battleoptions.daze;

                            }

                            break;

                    }

                }

            }

            if(option == battleoptions.chosen)
            {

                option = chosen;

            }

            move.option = option;

            move.ability = moveset[option];

            move.item = item;

            BattleAbility ability = abilities[move.ability];

            move.champion = champion;

            move.special = ability.special;

            move.reaction = ability.reaction;

            move.self = ability.self;

            // damage ------------------------------------------------------

            int baseDamage = Mod.instance.randomIndex.Next(attack, (int)(attack * 1.5f));

            int useDamage = Math.Max(10, ability.damage * baseDamage / 100);

            move.damage = useDamage;

            // speed ------------------------------------------------------

            float baseSpeed = Math.Min(speed, (int)(speed * 1.5f));

            float useSpeed = (int)(ability.speed * baseSpeed / 100);

            move.speed = useSpeed;

            // accuracy ------------------------------------------------------

            int baseAccuracy = speed / 8;

            int accuracy = Mod.instance.randomIndex.Next(ability.accuracy, ability.accuracy + baseAccuracy);

            move.accuracy = accuracy;

            // defense ------------------------------------------------------

            int baseDefense = speed / 8;

            int defense = Mod.instance.randomIndex.Next(ability.defense, ability.defense + baseDefense);

            move.defense = defense;

            // resist ------------------------------------------------------

            int baseResist = resist / 8;

            int resistance = Mod.instance.randomIndex.Next(ability.resist, ability.resist + baseResist);

            move.resist = resistance;

            // effect ------------------------------------------------------

            int baseEffect = resist / 8;

            int effect = Mod.instance.randomIndex.Next(ability.effect, ability.effect + baseEffect);

            move.effect = effect;

            // augments -------------------------------------------------------

            if (Mod.instance.randomIndex.Next(100) <= critical)
            {

                move.damage = (int)(1.5f * useDamage);

                move.critical = true;

                fatigue++;

            }

            if (ability.counter > 0)
            {

                if (Mod.instance.randomIndex.Next(100) <= counter)
                {

                    move.counter = ability.counter;

                    fatigue++;

                }

            }

            if (ability.absorb > 0)
            {

                if (Mod.instance.randomIndex.Next(100) <= counter)
                {

                    move.absorb = ability.absorb;

                    fatigue++;

                }

            }

            if (ability.harm > 0)
            {

                move.harm = ability.harm;

            }

            if (ability.buff != BattleBuff.battlebuffs.none)
            {

                move.buff = ability.buff;

            }

            if (!ignoreDebuff)
            {

                for (int b = debuffs.Count - 1; b >= 0; b--)
                {

                    switch (debuffs[b].buff)
                    {

                        case BattleBuff.battlebuffs.sense:

                            move.accuracy -= 20;

                            if (move.accuracy <= 0) { move.accuracy = 0; }

                            break;

                        case BattleBuff.battlebuffs.slow:

                            move.speed -= 20;

                            if (move.speed <= 0) { move.speed = 0; }

                            break;

                        case BattleBuff.battlebuffs.fatigue:

                            for(int i = 0; i < 2; i++)
                            {

                                switch (Mod.instance.randomIndex.Next(2))
                                {
                                    case 0:

                                        move.damage -= (4 * fatigue);

                                        if (move.damage <= 10) { move.damage = 10; }

                                        break;

                                    case 1:

                                        move.accuracy -= (4 * fatigue);

                                        if (move.accuracy <= 10) { move.accuracy = 10; }

                                        break;

                                    case 2:

                                        move.speed -= (4 * fatigue);

                                        if (move.speed <= 10) { move.speed = 10; }

                                        break;

                                    case 3:

                                        move.defense -= (4 * fatigue);

                                        if (move.defense <= 10) { move.defense = 10; }

                                        break;

                                    case 4:

                                        move.resist -= (4 * fatigue);

                                        if (move.resist <= 10) { move.resist = 10; }

                                        break;

                                    case 5:

                                        move.effect -= (4 * fatigue);

                                        if (move.effect <= 10) { move.effect = 10; }

                                        break;

                                }

    

                            }


                            break;

                    }

                }

            }

            return move;

        }

        public int PotionHealth(ApothecaryHandle.items Item = ApothecaryHandle.items.ligna)
        {

            int extra = max / 3;

            switch (Item)
            {
                case ApothecaryHandle.items.ligna:

                    extra = Math.Max(20, extra);

                    break;

                case ApothecaryHandle.items.satius_ligna:

                    extra = Math.Max(40, extra);

                    break;

                case ApothecaryHandle.items.magnus_ligna:

                    extra = Math.Max(60, extra);

                    break;

                case ApothecaryHandle.items.optimus_ligna:

                    extra = Math.Max(100, extra);

                    break;

            }

            extra = Math.Min(extra, max - min);

            min += extra;

            return extra;

        }

        public int PotionAttack(ApothecaryHandle.items Item = ApothecaryHandle.items.vigores)
        {
            int extra = attack / 4;

            switch (Item)
            {

                case ApothecaryHandle.items.vigores:

                    extra = Math.Max(6, extra);

                    break;

                case ApothecaryHandle.items.satius_vigores:

                    extra = Math.Max(8, extra);

                    break;

                case ApothecaryHandle.items.magnus_vigores:

                    extra = Math.Max(10, extra);

                    break;

                case ApothecaryHandle.items.optimus_vigores:
                    extra = Math.Max(12, extra);

                    break;

            }

            attack += extra;

            return extra;

        }

        public int PotionSpeed(ApothecaryHandle.items Item = ApothecaryHandle.items.celeri)
        {

            int extra = speed / 4;

            switch (Item)
            {

                case ApothecaryHandle.items.celeri:

                    extra = Math.Max(6, extra);

                    break;

                case ApothecaryHandle.items.satius_celeri:

                    extra = Math.Max(8, extra);

                    break;

                case ApothecaryHandle.items.magnus_celeri:

                    extra = Math.Max(10, extra);

                    break;

                case ApothecaryHandle.items.optimus_celeri:

                    extra = Math.Max(12, extra);

                    break;

            }

            speed += extra;

            return extra;

        }

        public int PotionResist()
        {

            int extra = resist / 4;

            extra = Math.Max(5, extra);

            resist += extra;

            return extra;

        }

        public int PotionCritical()
        {

            int extra = attack / 10;

            extra = Math.Max(7, extra);

            critical += extra;

            return extra;

        }

        public int PotionCounter()
        {

            int extra = vitality / 10;

            extra = Math.Max(10, extra);

            counter += extra;

            return extra;

        }

        public void AddBuff(BattleBuff.battlebuffs addbuff, bool champion = false)
        {

            BattleBuff debuff = new(addbuff);

            for (int b = debuffs.Count - 1; b >= 0; b--)
            {

                if (debuff.buff == debuffs[b].buff)
                {

                    debuffs.RemoveAt(b);

                }

            }

            debuffs.Add(debuff);

        }

        public string DescribeMove(BattleMove useMove)
        {

            string prefix = possessive + title;

            string description;

            switch (useMove.option)
            {

                case BattleCombatant.battleoptions.recover:

                    BattleBuff rebuff = new(useMove.buff);

                    description = prefix + rebuff.onRecover;

                    break;

                case BattleCombatant.battleoptions.buff:

                    BattleBuff newbuff = new(useMove.buff);

                    description = prefix + newbuff.onApply;

                    break;

                case BattleCombatant.battleoptions.staredown:
                case BattleCombatant.battleoptions.stun:
                case BattleCombatant.battleoptions.daze:
                

                    description = prefix + abilities[useMove.ability].onHit;

                    break;

                case BattleCombatant.battleoptions.item:

                    switch (useMove.item)
                    {
                        default:
                        case ApothecaryHandle.items.satius_ligna:

                            //int health = PotionHealth();

                            description = prefix + Mod.instance.Helper.Translation.Get("BattleHandle.388.912").Tokens(new { health = itemStat, });

                            break;

                        case ApothecaryHandle.items.satius_vigores:

                            //int attack = PotionAttack();

                            description = prefix + Mod.instance.Helper.Translation.Get("BattleHandle.388.917").Tokens(new { attack = itemStat, });

                            break;

                        case ApothecaryHandle.items.satius_celeri:

                            //int speed = PotionSpeed();

                            description = prefix + Mod.instance.Helper.Translation.Get("BattleHandle.388.922").Tokens(new { speed = itemStat, });

                            break;

                        case ApothecaryHandle.items.faeth:

                            //int resist = PotionResist();

                            description = prefix + Mod.instance.Helper.Translation.Get("BattleCombatant.390.1").Tokens(new { resist = itemStat, });

                            break;

                        case ApothecaryHandle.items.trophy_shroom:

                            //int critical = PotionCritical();

                            description = prefix + Mod.instance.Helper.Translation.Get("BattleCombatant.390.2").Tokens(new { critical = itemStat, });

                            break;

                        case ApothecaryHandle.items.trophy_eye:

                            //int counter = PotionCounter();

                            description = prefix + Mod.instance.Helper.Translation.Get("BattleCombatant.390.3").Tokens(new { counter = itemStat, });

                            break;

                    }

                    break;

                case BattleCombatant.battleoptions.absorb:

                    description = prefix + abilities[useMove.ability].onHit + useMove.damage + StringData.Get(StringData.str.health);

                    break;

                default:

                    if (useMove.block)
                    {

                        if (useMove.hit)
                        {

                            description = prefix + abilities[useMove.ability].onBypass + useMove.damage + StringData.Get(StringData.str.damage);

                        }
                        else
                        {
                            description = prefix + abilities[useMove.ability].onBlock;

                        }

                    }
                    else
                    if (useMove.hit)
                    {

                        description = prefix + abilities[useMove.ability].onHit + useMove.damage + StringData.Get(StringData.str.damage);
                    }
                    else
                    {

                        description = prefix + abilities[useMove.ability].onMiss;

                    }

                break;

            }

            return description;

        }

        public void PerformMove(BattleMove useMove)
        {

            switch (useMove.option)
            {

                case BattleCombatant.battleoptions.absorb:

                    min += useMove.damage;

                    break;

                case BattleCombatant.battleoptions.daze:
                case BattleCombatant.battleoptions.harm:

                    min -= useMove.damage;

                    break;

                case BattleCombatant.battleoptions.item:

                    if (champion)
                    {

                        ApplyItem(useMove.item);

                    }

                    break;

                case BattleCombatant.battleoptions.buff:

                    AddBuff(useMove.buff);

                    break;
            }

        }

        public void ReceiveMove(BattleMove useMove)
        {

            switch (useMove.option)
            {

                case BattleCombatant.battleoptions.attack:
                case BattleCombatant.battleoptions.tackle:
                case BattleCombatant.battleoptions.counter:
                case BattleCombatant.battleoptions.special:

                    if (useMove.hit)
                    {

                        min -= useMove.damage;

                    }

                    break;


            }

        }

        public void ApplyItem(ApothecaryHandle.items Item)
        {

            itemStat = 0;

            switch (Item)
            {
                case ApothecaryHandle.items.ligna:
                case ApothecaryHandle.items.satius_ligna:
                case ApothecaryHandle.items.magnus_ligna:
                case ApothecaryHandle.items.optimus_ligna:

                    itemStat = PotionHealth(Item);

                    if (champion)
                    {

                        ApothecaryHandle.UpdateAmounts(ApothecaryHandle.items.satius_ligna, 0 - 1);

                        PalHandle.ReceivePotion(championType,ApothecaryHandle.items.ligna, false);

                    }
                    
                    break;

                case ApothecaryHandle.items.vigores:
                case ApothecaryHandle.items.satius_vigores:
                case ApothecaryHandle.items.magnus_vigores:
                case ApothecaryHandle.items.optimus_vigores:

                    itemStat = PotionAttack(Item);

                    AddBuff(BattleBuff.battlebuffs.strength);

                    if (champion)
                    {

                        ApothecaryHandle.UpdateAmounts(ApothecaryHandle.items.satius_vigores, 0 - 1);

                        PalHandle.ReceivePotion(championType, ApothecaryHandle.items.vigores, false);

                    }
                    break;

                case ApothecaryHandle.items.celeri:
                case ApothecaryHandle.items.satius_celeri:
                case ApothecaryHandle.items.magnus_celeri:
                case ApothecaryHandle.items.optimus_celeri:

                    itemStat = PotionSpeed(Item);

                    AddBuff(BattleBuff.battlebuffs.speed);

                    if (champion)
                    {

                        ApothecaryHandle.UpdateAmounts(ApothecaryHandle.items.satius_celeri, 0 - 1);

                        PalHandle.ReceivePotion(championType, ApothecaryHandle.items.celeri, false);

                    }
                    break;

                case ApothecaryHandle.items.faeth:

                    itemStat = PotionResist();

                    AddBuff(BattleBuff.battlebuffs.resist);

                    for (int b = debuffs.Count - 1; b >= 0; b--)
                    {

                        if (debuffs[b].permanent)
                        {

                            continue;

                        }

                        debuffs[b].effect = 1;

                    }

                    if (champion)
                    {

                        ApothecaryHandle.UpdateAmounts(ApothecaryHandle.items.faeth, 0 - 1);

                        PalHandle.ReceivePotion(championType, ApothecaryHandle.items.faeth, false);

                    }
                    break;

                case ApothecaryHandle.items.trophy_shroom:

                    itemStat = PotionCritical();

                    AddBuff(BattleBuff.battlebuffs.critical);

                    if (champion)
                    {

                        ApothecaryHandle.UpdateAmounts(ApothecaryHandle.items.trophy_shroom, 0 - 1);

                    }
                    break;

                case ApothecaryHandle.items.trophy_eye:

                    itemStat = PotionCounter();

                    AddBuff(BattleBuff.battlebuffs.counter);

                    if (champion)
                    {

                        ApothecaryHandle.UpdateAmounts(ApothecaryHandle.items.trophy_eye, 0 - 1);

                    }
                    break;

            }

        }

    }

}
