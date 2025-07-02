using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewValley;
using System;


namespace StardewDruid.Character
{
    public class Linus : StardewDruid.Character.Character
    {

        public BearRender bearRender;

        public Linus()
        {

        }

        public Linus(CharacterHandle.characters type)
          : base(type)
        {


        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.recruit_one;

            }

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Linus);

            if (Portrait == null)
            {

                Portrait = CharacterHandle.CharacterPortrait(CharacterHandle.characters.Linus);

            }

            LoadIntervals();

            setScale = 4f;

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.HumanoidSpecial();

            bearRender = new("GreyBear");

            dashFrames[dashes.smash] = bearRender.dashFrames;

            specialFrames[specials.special] = bearRender.specialFrames;

            specialFrames[specials.sweep] = bearRender.sweepFrames;

            specialIntervals[specials.special] = 12;

            specialIntervals[specials.sweep] = 9;

            specialFloors[specials.special] = 0;

            specialFloors[specials.sweep] = 0;

            specialCeilings[specials.special] = 8;

            specialCeilings[specials.sweep] = 5;

            loadedOut = true;

        }

        public override void DrawSmash(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            int dashSeries = netDirection.Value + (netDashProgress.Value * 4);

            int dashSetto = Math.Min(dashFrame, (dashFrames[(dashes)netDash.Value][dashSeries].Count - 1));

            BearRenderAdditional additional = new()
            {
                layer = drawLayer,

                scale = setScale,

                position = spritePosition,

                flip = SpriteFlip(),

                fade = fade,

                series = BearRenderAdditional.bearseries.dash,

                direction = dashSeries,

                frame = dashSetto
            };

            bearRender.DrawNormal(b, additional);

        }

        public override void DrawLaunch(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            BearRenderAdditional additional = new()
            {
                layer = drawLayer,

                scale = setScale,

                position = spritePosition,

                flip = SpriteFlip(),

                fade = fade,

                direction = netDirection.Value,

                frame = specialFrame,

                series = BearRenderAdditional.bearseries.special,
            };

            bearRender.DrawNormal(b, additional);

        }

        public override void DrawSweep(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            BearRenderAdditional additional = new()
            {
                layer = drawLayer,

                scale = setScale,

                position = spritePosition,

                flip = SpriteFlip(),

                fade = fade,

                direction = netDirection.Value,

                frame = specialFrame,

                series = BearRenderAdditional.bearseries.sweep

            };

            bearRender.DrawNormal(b, additional);

        }

        public override void UpdateSpecial()
        {

            base.UpdateSpecial();

            switch (specialTimer)
            {
                case 48:

                    if (netSpecial.Value == (int)specials.special)
                    {

                        Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.BearRoar);

                    }

                    break;

                case 36:

                    if (netSpecial.Value == (int)specials.sweep)
                    {

                        if (Mod.instance.randomIndex.Next(2) == 0)
                        {

                            return;

                        }

                        if (Mod.instance.sounds.ActiveCue(Handle.SoundHandle.SoundCue.BearGrowl))
                        {

                            return;

                        }

                        if (Mod.instance.sounds.ActiveCue(Handle.SoundHandle.SoundCue.BearRoar))
                        {

                            return;

                        }

                        Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.BearGrowl);

                    }

                    break;

            }

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.launch);

            specialTimer = 72;

            SetCooldown(specialTimer, 1f);

            SpellHandle shapeshift = new(Position, 256, IconData.impacts.smoke, new())
            {

                displayRadius = 3,

                instant = true,

            };

            Mod.instance.spellRegister.Add(shapeshift);

            SpellHandle explode = new(monster.Position, 256, IconData.impacts.flashbang, new())
            {

                displayRadius = 5,

                instant = true,

                sound = SpellHandle.Sounds.warrior,

                counter = -36,

                damageMonsters = CombatDamage() / 3,

            };

            Mod.instance.spellRegister.Add(explode);

            return true;

        }

        public override bool SweepAttack(StardewValley.Monsters.Monster monster)
        {

            if (base.SweepAttack(monster))
            {

                SpellHandle shapeshift = new(Position, 256, IconData.impacts.smoke, new())
                {

                    displayRadius = 3,

                    instant = true,

                };

                Mod.instance.spellRegister.Add(shapeshift);

                return true;

            }

            return false;

        }

        public override bool SmashAttack(StardewValley.Monsters.Monster monster)
        {

            if (base.SmashAttack(monster))
            {

                SpellHandle explode = new(Position, 256, IconData.impacts.smoke, new())
                {
                    displayRadius = 3,

                    instant = true
                };

                Mod.instance.spellRegister.Add(explode);

                return true;

            }

            return false;

        }

        public override void ConnectSweep()
        {

            SpellHandle swipeEffect = new(Game1.player, Position, 256, CombatDamage() / 2)
            {

                type = SpellHandle.Spells.swipe,

                added = new() { SpellHandle.Effects.push, },

                sound = SpellHandle.Sounds.swordswipe,

                display = IconData.impacts.flashbang

            };

            Mod.instance.spellRegister.Add(swipeEffect);

        }

    }

}

