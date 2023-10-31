using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StardewDruid.Character
{
    public static class CharacterData
    {

        public static Texture2D CharacterTexture(string characterName)
        {

            Texture2D characterTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Effigy.png"));

            return characterTexture;

        }

        public static StardewValley.AnimatedSprite CharacterSprite(string characterName)
        {

            StardewValley.AnimatedSprite characterSprite = new();

            characterSprite.textureName.Value = "18465_Effigy_Sprite";

            characterSprite.spriteTexture = CharacterTexture(characterName);

            characterSprite.loadedTexture = "18465_Effigy_Sprite";

            characterSprite.SpriteHeight = 32;

            characterSprite.SpriteWidth = 16;

            return characterSprite;
        
        }

        public static Texture2D CharacterPortrait(string characterName)
        {

            Texture2D characterPortrait = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "EffigyPortrait.png"));

            return characterPortrait;

        }

        public static Dictionary<int, int[]> CharacterSchedule(string characterName)
        {

            return new Dictionary<int, int[]>();

        }

        public static CharacterDisposition CharacterDisposition(string characterName)
        {

            return new CharacterDisposition()
            {
                Age = 1,
                Manners = 2,
                SocialAnxiety = 1,
                Optimism = 0,
                Gender = 0,
                datable = false,
                Birthday_Season = "fall",
                Birthday_Day = 27,
                id = 18465001,
                speed = 1,

            };

        }

        public static string CharacterMap(string characterName)
        {

            return "FarmCave";

        }

        public static Vector2 CharacterPosition(string characterName)
        {

            foreach (Warp warp in Game1.getFarm().warps)
            {

                if(warp.TargetName == "FarmCave")
                {

                    int offsetY = (warp.TargetY <= 3) ? warp.TargetY + 1 : warp.TargetY - 1;

                    return new Vector2(warp.TargetX, offsetY) * 64;

                }

            }

            return new Vector2(6, 6) * 64;

        }

    }

}
