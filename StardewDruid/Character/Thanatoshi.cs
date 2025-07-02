using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewValley;
using System;
using System.Collections.Generic;

namespace StardewDruid.Character
{
    public class Thanatoshi : StardewDruid.Character.Character
    {

        public Thanatoshi()
        {
        }

        public Thanatoshi(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {
            base.LoadOut();

            WeaponLoadout(WeaponRender.weapons.scythetwo);

        }

        public override void behaviorOnFarmerPushing()
        {

            return;

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {

            if (netSceneActive.Value)
            {

                return EngageDialogue(who);

            }

            return false;

        }

    }

}
