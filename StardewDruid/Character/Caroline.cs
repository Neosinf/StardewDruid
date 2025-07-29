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
    public class Caroline : StardewDruid.Character.Recruit
    {

        public Caroline()
        {
        }

        public Caroline(CharacterHandle.characters type, NPC villager)
          : base(type, villager)
        {


        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Caroline;

            }

            if (villager == null)
            {

                villager = CharacterHandle.FindVillager("Caroline");

                Portrait = villager.Portrait;

            }

            LoadIntervals();

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Caroline);

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.WitchSpecial();

            specialIntervals = CharacterRender.WitchIntervals();

            specialCeilings = CharacterRender.WitchCeilings();

            specialFloors = CharacterRender.WitchFloors();

            loadedOut = true;

            setScale = 3.5f;

            idleFrames[idles.standby] = new()
            {
                [0] = new List<Rectangle> { new Rectangle(160, 32, 32, 32), },
            };

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

            Color circleColour = new Color(256 - (rate * 21), 256 - (rate * 9), 256 - rate);

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

        public override bool MonsterFear()
        {

            return false;

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

            SetCooldown(40, 1f);

            LookAtTarget(monster.Position, true);

            SpellHandle special = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, CombatDamage() / 2)
            {
                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.frostball,

                counter = -24,

                scheme = IconData.schemes.ether,

                displayFactor = 2,

                added = new() { SpellHandle.Effects.wisp, },

                display = IconData.impacts.puff
            };

            Mod.instance.spellRegister.Add(special);

            return true;

        }

        public override bool TrackNotReady()
        {

            if (villager.Name == Game1.player.spouse)
            {

                return false;

            }

            if (Game1.timeOfDay < 800)
            {

                return true;

            }

            return false;

        }

        public override bool TrackOutOfTime()
        {

            if (villager.Name == Game1.player.spouse)
            {

                return false;

            }

            if (Game1.timeOfDay > 2200)
            {

                return true;

            }

            return false;

        }

    }

}
