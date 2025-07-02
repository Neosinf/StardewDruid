using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.FruitTrees;
using StardewValley.Internal;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
    public class Gunther : StardewDruid.Character.Character
    {

        public Gunther()
        {
        }

        public Gunther(CharacterHandle.characters type)
          : base(type)
        {


        }

        public override void LoadOut()
        {

            base.LoadOut();

            WeaponLoadout();

            weaponRender.LoadWeapon(WeaponRender.weapons.estoc);

            idleFrames[idles.standby] = new()
            {
                [0] = new List<Rectangle> { new Rectangle(192, 0, 32, 32), },
            };

            hatSelect = 2;

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.invoke);

            specialTimer = 90;

            SetCooldown(specialTimer, 1f);

            LookAtTarget(monster.Position, true);

            List<int> intList = new List<int>()
            {
                96,
                97,
                98,
                99,
                100,
                101,
                103,
                104,
                105,
                106,
                107,
                108,
                109,
                110,
                111,
                112,
                113,
                114,
                115,
                116,
                117,
                118,
                119,
                120,
                121,
                122,
                123,
                124,
                125,
                126,
                127,
                579,
                580,
                581,
                582,
                583,
                584,
                585,
                586,
                587,
                588,
                589
            };

            ThrowHandle throwJunk = new(Position, monster.Position, intList[Mod.instance.randomIndex.Next(intList.Count)])
            {
                pocket = true
            };

            throwJunk.register();

            return true;

        }


    }

}
