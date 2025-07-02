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

namespace StardewDruid.Character
{
    public class Witch : StardewDruid.Character.Character
    {

        public Witch()
        {

        }

        public Witch(CharacterHandle.characters type)
          : base(type)
        {


        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Witch;

            }

            LoadOutLady();

            hatSelect = 4;

        }

        public override void DrawLaunch(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            Rectangle useFrame = specialFrames[specials.launch][netDirection.Value][specialFrame];

            int specialHover = 0;

            switch (specialFrame)
            {
                case 0:
                case 3:

                    specialHover = 16;

                    break;

                case 1:
                case 2:

                    specialHover = 32;

                    break;

            }

            b.Draw(
                characterTexture,
                spritePosition - new Vector2(0, specialHover),
                useFrame,
                Color.White * fade,
                0.0f,
                new Vector2(useFrame.Width / 2, useFrame.Height / 2),
                setScale,
                SpriteAngle() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawShadow(b, spritePosition, drawLayer, fade);

            int rate = Math.Abs((int)(Game1.currentGameTime.TotalGameTime.TotalSeconds % 12) - 6);

            Color circleColour = new Color(256 - rate, 256 - (rate * 21), 256 - rate);

            b.Draw(
                 Mod.instance.iconData.sheetTextures[IconData.tilesheets.ritual],
                 spritePosition + new Vector2(0, 14 * setScale),
                 new Rectangle(0, 96, 64, 48),
                 Color.White * fade,
                 0f,
                 new Vector2(32, 24),
                 setScale,
                 0,
                 drawLayer - 0.0005f
             );

            b.Draw(
                 Mod.instance.iconData.sheetTextures[IconData.tilesheets.ritual],
                 spritePosition + new Vector2(0, 14 * setScale),
                 new Rectangle(64, 96, 64, 48),
                 circleColour * fade,
                 0f,
                 new Vector2(32, 24),
                 setScale,
                 0,
                 drawLayer - 0.0006f
             );

        }

        public override bool SmashAttack(StardewValley.Monsters.Monster monster)
        {

            return SpecialAttack(monster);

        }

        public override bool SweepAttack(StardewValley.Monsters.Monster monster)
        {

            return SpecialAttack(monster);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.launch);

            specialTimer = 60;

            SetCooldown(specialTimer, 1f);

            LookAtTarget(monster.Position, true);

            SpellHandle special = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, CombatDamage() / 2)
            {

                counter = -24,

                type = SpellHandle.Spells.missile,

                factor = 4,

                missile = MissileHandle.missiles.bats,

                display = IconData.impacts.flashbang,

                displayRadius = 3,

                sound = SpellHandle.Sounds.batFlap,

                added = new() { Mod.instance.rite.ChargeEffect(IconData.cursors.fatesCharge, true), }

            };

            Mod.instance.spellRegister.Add(special);

            return true;

        }

    }

}
