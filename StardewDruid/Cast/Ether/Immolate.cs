﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Event;
using StardewDruid.Map;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;

namespace StardewDruid.Cast.Ether
{
    public class Immolate : EventHandle
    {

        public int skipCounter;

        public Dictionary<StardewValley.Monsters.Monster, Immolated> monsterVictims;

        public Dictionary<Farmer, Immolated> farmerVictims;

        public bool immolate;

        public Immolate(Vector2 target)
            : base(target)
        {

            monsterVictims = new();

            farmerVictims = new();

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 10;

        }

        public override void EventTrigger()
        {

            Mod.instance.RegisterEvent(this, "immolate");

        }

        public override void EventInterval()
        {

            for (int f = farmerVictims.Count - 1; f >= 0; f--)
            {

                KeyValuePair<Farmer, Immolated> victim = farmerVictims.ElementAt(f);

                victim.Value.timer--;

                if (victim.Value.timer <= 0)
                {

                    farmerVictims.Remove(victim.Key);

                    continue;

                }

                ModUtility.DamageFarmers(targetLocation, new() { victim.Key, }, victim.Value.damage, null);

            }


            for (int f = monsterVictims.Count - 1; f >= 0; f--)
            {

                KeyValuePair<StardewValley.Monsters.Monster, Immolated> victim = monsterVictims.ElementAt(f);

                if (!ModUtility.MonsterVitals(victim.Key, targetLocation))
                {
                    monsterVictims.Remove(victim.Key);

                    continue;

                }

                victim.Value.timer--;

                if (victim.Value.timer <= 0)
                {

                    monsterVictims.Remove(victim.Key);

                    continue;

                }

                Vector2 monsterPosition = victim.Key.Position;

                ModUtility.DamageMonsters(targetLocation, new() { victim.Key, }, Mod.instance.rite.caster, victim.Value.damage, false);

                if (Mod.instance.rite.castTask.ContainsKey("masterBlast"))
                {

                    if (new Random().Next(5) == 0)
                    {

                        if (!ModUtility.MonsterVitals(victim.Key, targetLocation))
                        {

                            Vector2 targetPosition = Mod.instance.rite.caster.Position;

                            TemporaryAnimatedSprite immolation = new(0, 125f, 5, 1, monsterPosition - new Vector2(32, 64), false, monsterPosition.Y < targetPosition.Y)
                            {

                                sourceRect = new(0, 0, 32, 32),

                                sourceRectStartingPos = new Vector2(0, 0),

                                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Immolation.png")),

                                scale = 4f, //* size,

                                layerDepth = monsterPosition.Y / 640000,

                                alphaFade = 0.002f,

                            };

                            targetLocation.temporarySprites.Add(immolation);

                            int barbeque = SpawnData.RandomBarbeque();

                            Throw throwMeat = new(Mod.instance.rite.caster, monsterPosition, barbeque);

                            throwMeat.ThrowObject();

                        }

                    }

                }

            }

        }

    }

    public class Immolated
    {

        public int damage;

        public int timer;

        public Immolated(int Damage, int Timer = 3)
        {

            damage = Damage;

            timer = Timer;

        }

    }



}
