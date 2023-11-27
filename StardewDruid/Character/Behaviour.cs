using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;
using Microsoft.Xna.Framework;
using StardewModdingAPI;

namespace StardewDruid.Character
{
    public class Behaviour : StardewValley.NPC
    {

        public bool busy;

        public bool follow;

        public List<string> mode;

        public Behaviour(Vector2 position, string map, string Name)
            : base(
            StardewDruid.Map.CharacterData.CharacterSprite(Name),
            position,
            map,
            2,
            Name,
            new(),
            StardewDruid.Map.CharacterData.CharacterPortrait(Name),
            false
            )
        {

        }



    }

}