using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Fates;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Journal;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;


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

            WeaponLoadout(WeaponRender.weapons.starsword);

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
                    new Rectangle(225, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                },
                [1] = new()
                {
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(192, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                    new Rectangle(225, 32, 32, 32),
                },
            };

        }

        public override void DrawHat(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade, hats hat, int hatDirection = -1)
        {

            if(netSpecial.Value != (int)Character.specials.invoke)
            {

                return;

            }

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

            int specialFactor = 3;

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.challengeMoors))
            {

                specialFactor = 2;

            }

            ResetActives();

            specialTimer = 90;

            SetCooldown(90, 1f);

            LookAtTarget(monster.Position, true);

            switch (Mod.instance.randomIndex.Next(specialFactor))
            {

                default:
                case 0:

                    netSpecial.Set((int)specials.hadouken);

                    SpellHandle beam = new(Game1.player, monster.Position, 256, CombatDamage())
                    {

                        origin = Position,

                        counter = -30,

                        indicator = IconData.cursors.divineCharge,

                        type = SpellHandle.Spells.lightbeam,

                        display = IconData.impacts.flasher,

                        sound = SpellHandle.Sounds.flameSpellHit

                    };

                    Mod.instance.spellRegister.Add(beam);

                    return true;

                case 1:

                    netSpecial.Set((int)specials.invoke);

                    List<StardewValley.Monsters.Monster> monsters = FindMonsters();

                    foreach (StardewValley.Monsters.Monster targetMonster in monsters)
                    {

                        SpellHandle special = new(currentLocation, targetMonster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, CombatDamage() / 2)
                        {
                            type = SpellHandle.Spells.judgement,

                            indicator = IconData.cursors.divineCharge,

                            factor = 4
                        };

                        special.TargetCursor();

                        special.counter = -60;

                        Mod.instance.spellRegister.Add(special);

                    }

                    return true;

                case 2:

                    netSpecial.Set((int)specials.invoke);

                    int s = Mod.instance.randomIndex.Next(3);

                    for (int i = 0; i < 3; i++)
                    {

                        SpellHandle sweep = new(Game1.player, new() { monster }, Mod.instance.CombatDamage())
                        {
                            type = SpellHandle.Spells.honourstrike,

                            counter = 0 - (30 + (i * 10))
                        };

                        int d = s + (i * 2);

                        sweep.factor = d;

                        sweep.display = IconData.impacts.sparkbang;

                        Mod.instance.spellRegister.Add(sweep);

                    }
                    return true;

            }

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

            /*if (specialTimer == 80)
            {

                if (currentLocation.Name == Game1.player.currentLocation.Name && Utility.isOnScreen(Position, 128))
                {

                    //Mod.instance.iconData.DecorativeIndicator(currentLocation, Position, IconData.decorations.stars, 3f, new());

                    //TemporaryAnimatedSprite skyAnimation = Mod.instance.iconData.SkyIndicator(currentLocation, Position, IconData.skies.night, 1f, new() { interval = 1000, });

                    //skyAnimation.scaleChange = 0.002f;

                    //skyAnimation.motion = new(-0.064f, -0.064f);

                    //skyAnimation.timeBasedMotion = true;

                    Game1.player.currentLocation.playSound("discoverMineral", null, 1000);

                }

            }*/

            if (specialTimer == 50)
            {

                SpellHandle special = new(currentLocation, workVector * 64, GetBoundingBox().Center.ToVector2(), 256, -1, -1)
                {

                    type = SpellHandle.Spells.judgement,

                    indicator = IconData.cursors.divineCharge,

                    factor = 4

                };

                Mod.instance.spellRegister.Add(special);

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
