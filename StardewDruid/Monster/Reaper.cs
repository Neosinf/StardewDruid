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

            shieldScheme = IconData.schemes.death;

            loadedOut = true;

        }

        public override float GetScale()
        {
            return 4f + 0.25f * netMode.Value;
        }

        public override bool PerformSpecial(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            if (!Mod.instance.eventRegister.ContainsKey(Rite.eventDeathwinds))
            {

                netChannelActive.Set(true);

                SetCooldown(2);

                SpellHandle deathwind = new(currentLocation, Position, Position, 256, GetThreat())
                {
                    type = SpellHandle.Spells.deathwind,

                    boss = this
                };

                Mod.instance.spellRegister.Add(deathwind);

                SpellHandle capture = new(Position, 8 * 64, IconData.impacts.none, new() ) { instant = true, };

                Mod.instance.spellRegister.Add(capture);

                return true;

            }

            netSpecialActive.Set(true);

            SetCooldown(1);

            if(Mod.instance.randomIndex.Next(2) == 0 && !netShieldActive.Value && shieldTimer <= 0)
            {

                netShieldActive.Set(true);

                shieldTimer = 600;

                return true;

            }

            SpellHandle fireball = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 192, GetThreat())
            {
                type = SpellHandle.Spells.missile,

                factor = 3,

                missile = MissileHandle.missiles.death,

                display = IconData.impacts.skull,

                scheme = IconData.schemes.death,

                added = new() { SpellHandle.Effects.chain, },

                boss = this
            };

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

                    SpellHandle fireball = new(currentLocation, tryVector * 64, GetBoundingBox().Center.ToVector2(), 256, GetThreat())
                    {
                        type = SpellHandle.Spells.missile,

                        factor = 4,

                        missile = MissileHandle.missiles.deathfall,

                        display = IconData.impacts.skull,

                        indicator = IconData.cursors.death,

                        scheme = IconData.schemes.death,

                        boss = this,

                        added = new() { SpellHandle.Effects.chain, }
                    };

                    Mod.instance.spellRegister.Add(fireball);

                }
            
            }

            return true;

        }

    }

}

