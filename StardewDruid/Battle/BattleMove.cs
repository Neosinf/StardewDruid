using StardewDruid.Handle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Battle
{

    public class BattleMove
    {

        public bool champion;

        public int damage;

        public float speed;

        public float accuracy;

        public float defense;

        public float resist;

        public float effect;

        public bool hit;

        public bool block;

        public bool critical;

        public int counter;

        public int absorb;

        public int harm;

        public bool special;

        public bool self;

        public bool reaction;

        public BattleCombatant.battleoptions option;

        public BattleAbility.battleabilities ability;

        public BattleBuff.battlebuffs buff;

        public ApothecaryHandle.items item;

    }

}
