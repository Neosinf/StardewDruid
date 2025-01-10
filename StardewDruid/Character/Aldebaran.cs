using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Fates;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Journal;
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
    public class Aldebaran : StardewDruid.Character.Character
    {

        public Aldebaran()
        {
        }

        public Aldebaran(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {

            base.LoadOut();

            WeaponLoadout();

            weaponRender.swordScheme = WeaponRender.swordSchemes.sword_stars;

            idleFrames[idles.standby] = new()
            {
                [0] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
                [1] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
            };

            gait = 1.4f;

            restSet = true;

            idleFrames[idles.rest] = new()
            {
                [0] = new()
                {
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                },
                [1] = new()
                {
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                    new Rectangle(224, 32, 32, 32),
                },
            };

        }

        public override void DrawUnder(SpriteBatch b, Vector2 spritePosition, float drawLayer, Rectangle frame, bool flip, idles idle)
        {

            if(idle == idles.rest)
            {

                base.DrawShadow(b, spritePosition, drawLayer, 1f); return;

            }

            base.DrawUnder(b, spritePosition, drawLayer, frame, flip, idle);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeMoors))
            {

                return false;

            }

            ResetActives();

            netSpecial.Set((int)specials.hadouken);

            specialTimer = 90;

            cooldownTimer = cooldownInterval*2;

            LookAtTarget(monster.Position, true);

            int s = Mod.instance.randomIndex.Next(3);

            for (int i = 0; i < 3; i++)
            {

                SpellHandle sweep = new(Game1.player, new() { monster }, Mod.instance.CombatDamage());

                sweep.type = SpellHandle.spells.honourstrike;

                sweep.counter = 0 - (30 + (i * 10));

                int d = s + (i * 2);

                sweep.factor = d;

                sweep.display = IconData.impacts.sparkbang;

                Mod.instance.spellRegister.Add(sweep);

            }
            
            return true;

        }

        public override bool TargetWork()
        {

            if (new SpawnIndex(currentLocation).cultivate == false)
            {

                return false;

            }

            if (currentLocation.objects.Count() <= 0)
            {

                return false;

            }

            List<Vector2> tileVectors;

            for (int i = 0; i < 5; i++)
            {

                tileVectors = ModUtility.GetTilesWithinRadius(currentLocation, occupied, i);

                foreach (Vector2 scarevector in tileVectors)
                {

                    if (currentLocation.objects.ContainsKey(scarevector))
                    {

                        if (currentLocation.Objects[scarevector].IsScarecrow())
                        {

                            string workString = Game1.season.ToString() + Game1.dayOfMonth.ToString() + "_" + currentLocation.Name + "_" + scarevector.X.ToString() + "_" + scarevector.Y.ToString();

                            if(workRegister.Contains(workString))
                            {

                                continue;

                            }
                            else
                            if (Mod.instance.randomIndex.Next(4) != 0)
                            {

                                workRegister.Add(workString);

                                continue;

                            }

                            ResetActives();

                            LookAtTarget(scarevector * 64, true);

                            netSpecial.Set((int)specials.invoke);

                            specialTimer = 90;

                            workVector = scarevector;

                            return true;

                        }

                    }

                }

            }

            return false;

        }

        public override void PerformWork()
        {

            if (specialTimer == 80)
            {

                if (currentLocation.Name == Game1.player.currentLocation.Name && Utility.isOnScreen(Position, 128))
                {

                    Mod.instance.iconData.DecorativeIndicator(currentLocation, Position, IconData.decorations.weald, 3f, new());

                    TemporaryAnimatedSprite skyAnimation = Mod.instance.iconData.SkyIndicator(currentLocation, Position, IconData.skies.night, 1f, new() { interval = 1000, });

                    skyAnimation.scaleChange = 0.002f;

                    skyAnimation.motion = new(-0.064f, -0.064f);

                    skyAnimation.timeBasedMotion = true;

                    Game1.player.currentLocation.playSound("discoverMineral", null, 1000);

                }

            }

            if (specialTimer == 50)
            {

                Enchant enchantEvent = new();

                enchantEvent.EventSetup(workVector * 64, "aldebaran_enchant_" + workVector.ToString());

                enchantEvent.giantTiles = new()
                {
                    
                    workVector + new Vector2(0,-3),
                    workVector + new Vector2(0,-2),

                    workVector + new Vector2(3,0),
                    workVector + new Vector2(2,0),

                    workVector + new Vector2(0,3),
                    workVector + new Vector2(0,2),

                    workVector + new Vector2(-2,0),


                };

                enchantEvent.inabsentia = true;

                enchantEvent.EventActivate();

                string workString = Game1.season.ToString() + Game1.dayOfMonth.ToString() + "_" + currentLocation.Name + "_" + workVector.X.ToString() + "_" + workVector.Y.ToString();

                if (!workRegister.Contains(workString))
                {

                    workRegister.Add(workString);

                }

            }

        }

    }

}
