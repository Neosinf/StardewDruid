using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Handle;
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
    public class Revenant : Character
    {

        public Revenant()
        {


        }

        public Revenant(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {

            base.LoadOut();

            WeaponLoadout();

            //weaponRender.swordScheme = IconData.schemes.stars;

            idleFrames[idles.standby] = new()
            {
                [0] = new(){ new(192, 0, 32, 32), },
                [1] = new() { new(192, 0, 32, 32), },
                [2] = new() { new(192, 0, 32, 32), },
                [3] = new() { new(192, 0, 32, 32), },
            };

            restSet = true;

            idleFrames[idles.rest] = new()
            {
                [0] = new() { new(0, 64, 32, 32), },

            };


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

            specialTimer = 120;

            SetCooldown(specialTimer, 3f);

            List<StardewValley.Monsters.Monster> monsters = FindMonsters();

            if (monsters.Count > 0)
            {

                foreach (StardewValley.Monsters.Monster targetMonster in monsters)
                {

                    SpellHandle special = new(currentLocation, targetMonster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, Mod.instance.CombatDamage())
                    {
                        type = SpellHandle.Spells.judgement,

                        indicator = IconData.cursors.divineCharge,

                        factor = 4
                    };

                    special.TargetCursor();

                    special.counter = -60;

                    Mod.instance.spellRegister.Add(special);

                }

            }

            return true;

        }

        public override mode SpecialMode(mode modechoice)
        {

            switch (modechoice)
            {

                case mode.home:

                case mode.random:

                case mode.roam:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.questRevenant))
                    {

                        return mode.limbo;

                    }

                    break;

            }

            return modechoice;

        }

    }

}
