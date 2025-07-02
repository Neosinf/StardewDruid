using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;

namespace StardewDruid.Character
{
    public class Krobus : StardewDruid.Character.Recruit
    {
        public Krobus()
        {
        }

        public Krobus(CharacterHandle.characters type, NPC villager)
          : base(type, villager)
        {

            
        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Krobus;

            }

            if (villager == null)
            {

                villager = CharacterHandle.FindVillager("Krobus");

                Portrait = villager.Portrait;

            }

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Krobus);

            LoadIntervals();

            setScale = 3.75f;

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.HumanoidSpecial();

            specialIntervals = CharacterRender.HumanoidIntervals();

            specialCeilings = CharacterRender.HumanoidCeilings();

            specialFloors = CharacterRender.HumanoidFloors();

            idleFrames[idles.standby] = new(specialFrames[specials.sweep]);

            WeaponLoadout(WeaponRender.weapons.symbol);

            idleFrames[idles.kneel] = new()
            {
                [0] = new()
                {
                    new Rectangle(128, 32, 32, 32),
                },
            };

            hatSelect = 10;

            hatVectors = new()
            {

                [hats.stand] = new()
                {
                    [0] = new(0, 10),
                    [1] = new(-1, 10),
                    [2] = new(0, 10),
                    [3] = new(1, 10),
                    [4] = new(0, 10),
                    [6] = new(0, 10),
                },
                [hats.jump] = new()
                {
                    [0] = new(0, 10),
                    [1] = new(-1, 10),
                    [2] = new(0, 10),
                    [3] = new(1, 10),
                    [4] = new(0, 10),
                    [6] = new(0, 10),
                },
                [hats.kneel] = new()
                {
                    [0] = new(0, 4),
                    [1] = new(-1, 4),
                    [2] = new(0, 4),
                    [3] = new(1, 4),
                    [4] = new(0, 4),
                    [6] = new(0, 4),
                },
                [hats.launch] = new()
                {
                    [0] = new(0, 4),
                    [1] = new(-1, 4),
                    [2] = new(0, 7),
                    [3] = new(1, 4),
                    [4] = new(0, 4),
                    [6] = new(0, 7),
                },
            };

            loadedOut = true;

        }

        public override bool MonsterFear()
        {

            return false;

        }


        public override void DrawLaunch(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            DrawStandby(b, spritePosition, drawLayer, fade);

        }

        public override void DrawStandby(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            base.DrawStandby(b, spritePosition, drawLayer, fade);

            int rate = Math.Abs((int)(Game1.currentGameTime.TotalGameTime.TotalSeconds % 12) - 6);

            Color circleColour = new Color(256 - rate, 256 - (rate * 9), 256 - (rate * 21));

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

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.launch);

            specialTimer = 90;

            SetCooldown(specialTimer, 2f);

            LookAtTarget(monster.Position, true);

            SpellHandle beam = new(monster.Position, 384, IconData.impacts.holyimpact, new() { SpellHandle.Effects.holy, })
            {

                origin = Position,

                counter = -30,

                type = SpellHandle.Spells.explode,

                displayRadius = 5,

                instant = true,

                indicator = IconData.cursors.divineCharge,

                sound = SpellHandle.Sounds.explosion,

            };

            Mod.instance.spellRegister.Add(beam);

            return true;

        }

        public override bool TrackNotReady()
        {

            if(villager.Name == Game1.player.spouse)
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
