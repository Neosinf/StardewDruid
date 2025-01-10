using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley.ItemTypeDefinitions;
using StardewValley;
using StardewValley.Network;
using StardewValley.Projectiles;
using System;
using System.Collections.Generic;
using System.IO;
using static StardewDruid.Data.IconData;
using StardewDruid.Character;

namespace StardewDruid.Render
{
    public class WeaponRender
    {

        public enum weapons
        {

            none,
            sword,
            axe,
            hammer,
            estoc,
            carnyx,
            scythe,
            scythetwo,

            bazooka,

        }

        public weapons weaponSelection;

        public Texture2D weaponTexture;
        public Dictionary<int, List<Rectangle>> weaponFrames;
        public Dictionary<int, List<float>> weaponOffsets;

        public Texture2D swipeTexture;
        public Dictionary<int, List<Rectangle>> swipeFrames;
        public Dictionary<int, List<float>> swipeOffsets;

        public Texture2D firearmTexture;
        public Dictionary<int, List<Rectangle>> firearmFrames;

        public enum swordSchemes
        {
            sword_steel,
            sword_stars,
            sword_lightsaber,
            sword_warp,
        }

        public swordSchemes swordScheme;

        public Dictionary<swordSchemes, List<Microsoft.Xna.Framework.Color>> swordColours = new()
        {
            [swordSchemes.sword_steel] = new() { new(200, 212, 212), new(68, 80, 80), new(48, 60, 60) },
            [swordSchemes.sword_stars] = new() { new(255, 230, 166), new(255, 173, 84), new(231, 102, 84), },//new() { new(255, 192, 128),  new(192, 140, 60), new(64, 48, 16), },
            [swordSchemes.sword_lightsaber] = new() { new(214, 0, 175), new(16, 16, 16), new(37, 34, 74), },
            [swordSchemes.sword_warp] = new() { new(16, 16, 16), new(159, 80, 191), new(37, 34, 74), },
        };

        public bool melee;
        public bool firearm;


        public static Dictionary<int,List<Rectangle>> GetWeaponFrames()
        {

            return new()
            {

                [256] = new()
                {
                    // 1
                    new Rectangle(0, 0, 64, 64),
                    new Rectangle(64, 0, 64, 64),
                    new Rectangle(128, 0, 64, 64),
                    new Rectangle(192, 0, 64, 64),
                    // 4
                    new Rectangle(0, 192, 64, 64),
                    new Rectangle(64, 192, 64, 64),
                    new Rectangle(128, 192, 64, 64),
                    new Rectangle(192, 192, 64, 64),
                },
                [288] = new()
                {
                    // 3
                    new Rectangle(0, 64, 64, 64),
                    new Rectangle(64, 64, 64, 64),
                    new Rectangle(128, 64, 64, 64),
                    new Rectangle(192, 64, 64, 64),
                    // 5
                    new Rectangle(0, 256, 64, 64),
                    new Rectangle(64, 256, 64, 64),
                    new Rectangle(128, 256, 64, 64),
                    new Rectangle(192, 256, 64, 64),
                },
                [320] = new()
                {
                    // 2

                    new Rectangle(0, 128, 64, 64),
                    new Rectangle(64, 128, 64, 64),
                    new Rectangle(128, 128, 64, 64),
                    new Rectangle(192, 128, 64, 64),
                    // 6
                    new Rectangle(0, 320, 64, 64),
                    new Rectangle(64, 320, 64, 64),
                    new Rectangle(128, 320, 64, 64),
                    new Rectangle(192, 320, 64, 64),
                },

            };

        }

        public static Dictionary<int, List<float>> GetWeaponOffsets()
        {

            return new()
            {
                [256] = new()
                {
                    // 1
                    -0.0005f,
                    0.0005f,
                    0.0005f,
                    0.0005f,
                    // 4
                    0.0005f,
                    0.0005f,
                    0.0005f,
                    -0.0005f,
                },
                [288] = new()
                {
                    // 3
                    -0.0005f,
                    -0.0005f,
                    -0.0005f,
                    -0.0005f,
                    // 5
                    -0.0005f,
                    0.0005f,
                    0.0005f,
                    -0.0005f,
                },
                [320] = new()
                {
                    // 2
                    0.0005f,
                    0.0005f,
                    0.0005f,
                    0.0005f,
                    // 6
                    0.0005f,
                    0.0005f,
                    -0.0005f,
                    0.0005f,
                },
            };

        }

        public static Dictionary<int, List<Rectangle>> GetSwipeFrames()
        {

            return new()
            {
                [256] = new()
                {
                    new Rectangle(0, 0, 64, 64),
                    new Rectangle(64, 0, 64, 64),
                    new Rectangle(128, 0, 64, 64),
                    new Rectangle(192, 0, 64, 64),
                    new Rectangle(0, 192, 64, 64),
                    new Rectangle(64, 192, 64, 64),
                    new Rectangle(128, 192, 64, 64),
                    new Rectangle(192, 192, 64, 64),
                },
                [288] = new()
                {   
                    new Rectangle(0, 64, 64, 64),
                    new Rectangle(64, 64, 64, 64),
                    new Rectangle(128, 64, 64, 64),
                    new Rectangle(192, 64, 64, 64),
                    new Rectangle(0, 256, 64, 64),
                    new Rectangle(64, 256, 64, 64),
                    new Rectangle(128, 256, 64, 64),
                    new Rectangle(192, 256, 64, 64),
                },
                [320] = new()
                {
                    new Rectangle(0, 128, 64, 64),
                    new Rectangle(64, 128, 64, 64),
                    new Rectangle(128, 128, 64, 64),
                    new Rectangle(192, 128, 64, 64),

                },
            };

        }

        public static Dictionary<int, List<float>> GetSwipeOffsets()
        {

            return new()
            {
                [256] = new()
                {
                    // 1
                    -0.0005f,
                    -0.0005f,
                    -0.0005f,
                    0.0005f,
                    // 4
                    -0.0005f,
                    0.0005f,
                    0.0005f,
                    -0.0005f,
                },
                [288] = new()
                {
                    // 3
                    0.0005f,
                    -0.0005f,
                    -0.0005f,
                    -0.0005f,
                    // 5
                    -0.0005f,
                    0.0005f,
                    0.0005f,
                    -0.0005f,
                },
                [320] = new()
                {
                    // 2
                    0.0005f,
                    -0.0005f,
                    0.0005f,
                    0.0005f,
                },
            };


        }

        public static Dictionary<int,List<Rectangle>> GetFirearmFrames()
        {

            return new()
            {
                [0] = new()
                {
                    new(),
                    new(),
                    new(),
                    new(),
                    new(0, 0, 64, 64),
                    new(64, 0, 64, 64),
                },
                [32] = new()
                {
                    new(),
                    new(),
                    new(),
                    new(),
                    new(0, 64, 64, 64),
                    new(64, 64, 64, 64),

                },
                [64] = new()
                {
                    new(),
                    new(),
                    new(),
                    new(),
                    new(0, 128, 64, 64),
                    new(64, 128, 64, 64),

                },
            };

        }


        public WeaponRender()
        {

            melee = true;

            swordScheme = swordSchemes.sword_steel;

            weaponSelection = weapons.sword;

            weaponTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "WeaponSword.png"));

            weaponFrames = GetWeaponFrames();

            weaponOffsets = GetWeaponOffsets();

            swipeTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "WeaponSwipe.png"));

            swipeFrames = GetSwipeFrames();

            swipeOffsets = GetSwipeOffsets();

        }

        public virtual void LoadWeapon(weapons weapon)
        {

            switch (weapon)
            {
                default:
                case weapons.none:

                    melee = false;

                    break;

                case weapons.sword:

                    weaponTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "WeaponSword.png"));

                    swordScheme = swordSchemes.sword_steel;

                    melee = true;

                    break;

                case weapons.axe:

                    weaponTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "WeaponAxe.png"));

                    melee = true;

                    break;
                    
                case weapons.hammer:

                    weaponTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "WeaponHammer.png"));

                    melee = true;

                    break;

                case weapons.estoc:

                    weaponTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "WeaponEstoc.png"));

                    melee = true;

                    break;

                case weapons.carnyx:

                    weaponTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "WeaponCarnyx.png"));

                    melee = true;

                    break;

                case weapons.scythe:

                    weaponTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "WeaponScythe.png"));

                    melee = true;

                    break;

                case weapons.scythetwo:

                    weaponTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "WeaponScytheTwo.png"));

                    melee = true;

                    break;

                case weapons.bazooka:

                    firearmTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "FirearmBazooka.png"));

                    firearm = true;

                    firearmFrames = GetFirearmFrames();

                    break;

            }

            weaponSelection = weapon;

        }

        public virtual void DrawWeapon(SpriteBatch b, Vector2 spriteVector, float drawLayer, WeaponAdditional additional)
        {

            if (!melee) { return; }

            if(weaponSelection == weapons.sword)
            {

                DrawWeaponScheme(b, spriteVector, drawLayer, additional);

                return;

            }

            b.Draw(
                 weaponTexture,
                 spriteVector,
                 weaponFrames[additional.source.Y][additional.source.X == 0 ? 0 : additional.source.X / 32],
                 Color.White,
                 0f,
                 new Vector2(32),
                 additional.scale,
                 additional.flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                 drawLayer + weaponOffsets[additional.source.Y][additional.source.X == 0 ? 0 : additional.source.X / 32]
            );

        }

        public virtual void DrawWeaponScheme(SpriteBatch b, Vector2 spriteVector, float drawLayer, WeaponAdditional additional)
        {


            Rectangle weaponRectangle = weaponFrames[additional.source.Y][additional.source.X == 0 ? 0 : additional.source.X / 32];

            float weaponLayer = drawLayer + weaponOffsets[additional.source.Y][additional.source.X == 0 ? 0 : additional.source.X / 32];

            b.Draw(
                 weaponTexture,
                 spriteVector,
                 weaponRectangle,
                 swordColours[swordScheme][0],
                 0f,
                 new Vector2(32),
                additional.scale,
                additional.flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                weaponLayer
            );

            weaponRectangle.X += 256;

            b.Draw(
                 weaponTexture,
                 spriteVector,
                 weaponRectangle,
                 swordColours[swordScheme][1],
                 0f,
                 new Vector2(32),
                additional.scale,
                additional.flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                weaponLayer
            );

            weaponRectangle.X += 256;

            b.Draw(
                 weaponTexture,
                 spriteVector,
                 weaponRectangle,
                 swordColours[swordScheme][2],
                 0f,
                 new Vector2(32),
                additional.scale,
                additional.flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                weaponLayer
            );


        }

        public virtual void DrawSwipe(SpriteBatch b, Vector2 spriteVector, float drawLayer, WeaponAdditional additional)
        {

            if (!melee) { return; }

            int swipeIndex = additional.source.X == 0 ? 0 : additional.source.X / 32;

            if (swipeFrames[additional.source.Y].Count <= swipeIndex)
            {

                return;

            }

            Microsoft.Xna.Framework.Color colour = Color.White;

            b.Draw(
                 swipeTexture,
                 spriteVector,
                 swipeFrames[additional.source.Y][swipeIndex],
                 colour * 0.65f,
                 0f,
                 new Vector2(32),
                additional.scale,
                additional.flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer + swipeOffsets[additional.source.Y][swipeIndex]
            );

        }

        public virtual void DrawFirearm(SpriteBatch b, Vector2 spriteVector, float drawLayer, WeaponAdditional additional)
        {

            if (!firearm) { return; }

            b.Draw(
                 firearmTexture,
                 spriteVector,
                 firearmFrames[additional.source.Y][additional.source.X == 0 ? 0 : additional.source.X / 32],
                 Color.White,
                 0f,
                 new Vector2(32),
                additional.scale,
                additional.flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer + 0.002f
            );

        }

        public static Rectangle StrikeRectangle(int direction)
        {

            Rectangle source = new(0, 256, 32, 32);

            switch (direction)
            {
                case 0:

                    source.X += 128;

                    source.Y += 32;

                    break;
                case 1:

                    source.Y += 64;

                    break;

                case 2:

                    source.Y += 32;

                    break;

                case 3:

                    break;

                case 4:

                    source.X += 128;

                    source.Y += 32;

                    break;

                case 5:

                    break;

                case 6:

                    source.Y += 32;

                    break;

                case 7:

                    source.Y += 64;

                    break;

            }

            return source;

        }

        public static Rectangle WeaponRectangle(Rectangle strike)
        {
            int xOffset = strike.X == 0 ? 0 : strike.X / 32;
            return GetWeaponFrames()[strike.Y][xOffset];

        }

        public static Rectangle SwipeRectangle(Rectangle strike)
        {
            int xOffset = strike.X == 0 ? 0 : strike.X / 32;
            return GetSwipeFrames()[strike.Y][xOffset];

        }

        public static float WeaponOffset(Rectangle strike)
        {
            int xOffset = strike.X == 0 ? 0 : strike.X / 32;
            return GetWeaponOffsets()[strike.Y][xOffset];

        }

        public static float SwipeOffset(Rectangle strike)
        {
            int xOffset = strike.X == 0 ? 0 : strike.X / 32;
            return GetSwipeOffsets()[strike.Y][xOffset];

        }

        public static Vector2 StrikePosition(Vector2 offset, int direction)
        {

            switch (direction)
            {
                case 0:

                    offset.Y += 96;

                    break;

                case 1:

                    offset.X -= 64;

                    offset.Y += 64;

                    break;

                case 2:

                    offset.X -= 96;

                    break;

                case 3:

                    offset.X -= 64;

                    offset.Y -= 96;

                    break;

                case 4:

                    offset.Y -= 96;

                    break;

                case 5:

                    offset.X += 64;

                    offset.Y -= 96;

                    break;

                case 6:

                    offset.X += 96;

                    break;

                case 7:

                    offset.X += 64;

                    offset.Y += 64;

                    break;

            }

            return offset;

        }

        public static bool StrikeFlip(int direction)
        {
            bool flip = false;

            switch (direction)
            {
                case 0:

                    flip = true;

                    break;

                case 5:

                    flip = true;

                    break;
                case 6:

                    flip = true;

                    break;

                case 7:

                    flip = true;

                    break;

            }

            return flip;
        }

        public List<TemporaryAnimatedSprite> AnimateWarpStrike(GameLocation location, CharacterHandle.characters character, Vector2 origin, int direction)
        {

            Texture2D warpstrikeTexture = CharacterHandle.CharacterTexture(character);

            List<TemporaryAnimatedSprite> animations = new();

            Microsoft.Xna.Framework.Rectangle source = StrikeRectangle(direction);

            Vector2 center = StrikePosition(origin, direction) - new Vector2(32);

            bool flip = StrikeFlip(direction);

            float layer = center.Y / 10000;

            int interval = 100;

            // strike

            TemporaryAnimatedSprite strike = new(0, interval, 4, 1, center, false, flip)
            {
                sourceRect = source,
                sourceRectStartingPos = new Vector2(source.X, source.Y),
                texture = warpstrikeTexture,
                scale = 4f,
                layerDepth = layer + 0.001f,
                alpha = 0.75f,
            };

            location.temporarySprites.Add(strike);

            animations.Add(strike);

            // weapon 1

            Microsoft.Xna.Framework.Rectangle weaponSource = WeaponRectangle(source);

            float layerOffset = WeaponOffset(source);

            center.X -= 64;

            center.Y -= 64;

            TemporaryAnimatedSprite weapon = new(0, interval, 4, 1, center, false, flip)
            {
                sourceRect = weaponSource,
                sourceRectStartingPos = new Vector2(weaponSource.X, weaponSource.Y),
                texture = weaponTexture,
                color = swordColours[swordScheme][0],
                scale = 4f,
                layerDepth = layer + 0.001f + layerOffset,
                alpha = 0.75f,
            };

            location.temporarySprites.Add(weapon);

            animations.Add(weapon);

            // weapon 2

            weaponSource.X += 256;

            TemporaryAnimatedSprite weaponTwo = new(0, interval, 4, 1, center, false, flip)
            {
                sourceRect = weaponSource,
                sourceRectStartingPos = new Vector2(weaponSource.X, weaponSource.Y),
                texture = weaponTexture,
                color = swordColours[swordScheme][1],
                scale = 4f,
                layerDepth = layer + 0.001f + layerOffset,
                alpha = 0.75f,
            };

            location.temporarySprites.Add(weaponTwo);

            animations.Add(weaponTwo);

            // weapon 3

            weaponSource.X += 256;

            TemporaryAnimatedSprite weaponThree = new(0, interval, 4, 1, center, false, flip)
            {
                sourceRect = weaponSource,
                sourceRectStartingPos = new Vector2(weaponSource.X, weaponSource.Y),
                texture = weaponTexture,
                color = swordColours[swordScheme][2],
                scale = 4f,
                layerDepth = layer + 0.001f + layerOffset,
                alpha = 0.75f,
            };

            location.temporarySprites.Add(weaponThree);

            animations.Add(weaponThree);

            // swipe

            layerOffset = SwipeOffset(source);

            Microsoft.Xna.Framework.Rectangle swipeSource = SwipeRectangle(source);

            TemporaryAnimatedSprite swipe = new(0, interval, 4, 1, center, false, flip)
            {
                sourceRect = swipeSource,
                sourceRectStartingPos = new Vector2(swipeSource.X, swipeSource.Y),
                texture = swipeTexture,
                color = swordColours[swordScheme][0],
                scale = 4f,
                layerDepth = layer + 0.001f + layerOffset,
                alpha = 0.5f,
            };

            location.temporarySprites.Add(swipe);

            animations.Add(swipe);

            return animations;

        }

    }


    public class WeaponAdditional
    {

        public Microsoft.Xna.Framework.Rectangle source;

        public bool flipped;

        public float scale = 4f;

        public int direction;

        public int frame;

    }

}
