using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Render;
using StardewValley;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using System.Reflection;
using xTile.Dimensions;

namespace StardewDruid.Monster
{
    public class Reaper : Dark
    {

        public Reaper()
        {


        }

        public Reaper(Vector2 vector, int CombatModifier, string name = "Reaper")
          : base(vector, CombatModifier, name)
        {

        }

        public override void LoadOut()
        {

            baseMode = 3;

            baseJuice = 4;
            
            basePulp = 40;

            cooldownInterval = 120;

            DarkWalk();

            DarkFlight();

            DarkCast();

            flightDefault = flightTypes.close;

            channelSet = true;

            channelFrames = new Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>>()
            {

                [0] = new()
                {

                    new(32, 64, 32, 32),
                    new(32, 64, 32, 32),

                },
                [1] = new()
                {

                    new(32, 32, 32, 32),
                    new(32, 32, 32, 32),

                },
                [2] = new()
                {

                    new(32, 0, 32, 32),
                    new(32, 0, 32, 32),

                },
                [3] = new()
                {

                    new(32, 96, 32, 32),
                    new(32, 96, 32, 32),

                },

            };

            DarkSmash();

            DarkSword();

            weaponRender = new();

            weaponRender.LoadWeapon(WeaponRender.weapons.scythe);

            overHead = new(16, -144);

            loadedOut = true;

        }

        public override float GetScale()
        {
            return 4f + 0.25f * netMode.Value;
        }

        public override void DrawShield(SpriteBatch b, Vector2 spritePosition, float spriteScale, float drawLayer, IconData.schemes scheme = IconData.schemes.white)
        {

            base.DrawShield(b, spritePosition, spriteScale, drawLayer, IconData.schemes.death);

        }

        public override bool PerformSpecial(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            if (!Mod.instance.eventRegister.ContainsKey(Rite.eventDeathwinds))
            {

                netChannelActive.Set(true);

                SetCooldown(2);

                SpellHandle deathwind = new(currentLocation, Position, Position, 256, GetThreat());

                deathwind.type = SpellHandle.spells.deathwind;

                deathwind.boss = this;

                Mod.instance.spellRegister.Add(deathwind);

                SpellHandle capture = new(Position, 8 * 64, IconData.impacts.none, new() { SpellHandle.effects.capture, }) { instant = true, };

                Mod.instance.spellRegister.Add(capture);

                return true;

            }

            netSpecialActive.Set(true);

            SetCooldown(1);

            if(Mod.instance.randomIndex.Next(2) == 0 && !netShieldActive.Value && shieldTimer <= 0)
            {

                netShieldActive.Set(true);

                shieldTimer = 600;

                SpellHandle capture = new(Position, 8*64, IconData.impacts.none, new() { SpellHandle.effects.capture, }) { instant = true, };

                Mod.instance.spellRegister.Add(capture);

                return true;

            }

            SpellHandle fireball = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 192, GetThreat());

            fireball.type = SpellHandle.spells.missile;

            fireball.factor = 3;

            fireball.missile = MissileHandle.missiles.death;

            fireball.display = IconData.impacts.skull;

            fireball.scheme = IconData.schemes.death;

            fireball.boss = this;

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

        public override bool PerformChannel(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            netChannelActive.Set(true);
            
            SetCooldown(2);

            int offset = Mod.instance.randomIndex.Next(2);

            for (int i = 0; i < 4; i++)
            {
                
                List<Vector2> castSelection = ModUtility.GetTilesWithinRadius(currentLocation, ModUtility.PositionToTile(Position), Mod.instance.randomIndex.Next(6,9), true, (i*2) + offset % 8);
                
                if (castSelection.Count > 0)
                {

                    Vector2 tryVector = castSelection[Mod.instance.randomIndex.Next(castSelection.Count)];

                    SpellHandle fireball = new(currentLocation, tryVector*64, GetBoundingBox().Center.ToVector2(), 256, GetThreat());

                    fireball.type = SpellHandle.spells.missile;

                    fireball.factor = 4;

                    fireball.missile = MissileHandle.missiles.deathfall;

                    fireball.display = IconData.impacts.skull;

                    fireball.indicator = IconData.cursors.death;

                    fireball.scheme = IconData.schemes.death;

                    fireball.boss = this;

                    fireball.added = new() { SpellHandle.effects.capture, };

                    Mod.instance.spellRegister.Add(fireball);

                }
            
            }

            return true;

        }

    }

}

