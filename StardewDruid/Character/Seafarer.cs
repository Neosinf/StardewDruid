using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
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
using System.Reflection.Metadata.Ecma335;

namespace StardewDruid.Character
{
    public class Seafarer : StardewDruid.Character.Character
    {

        public Seafarer()
        {
        }

        public Seafarer(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {
            
            base.LoadOut();

            LoadNuance();

        }

        public void LoadNuance()
        {

            WeaponLoadout(WeaponRender.weapons.cutlass);

            switch (characterType)
            {
                
                default:

                    hatSelect = 9;

                    break;

                case CharacterHandle.characters.SeafarerCaptain:

                    hatSelect = 8;

                    break;

                case CharacterHandle.characters.SeafarerMate:

                    hatSelect = 7;

                    break;

            }

        }

        public override void behaviorOnFarmerPushing()
        {

            return;

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {

            return false;

        }
    }

}
