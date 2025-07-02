
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using StardewDruid.Render;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Cast;

namespace StardewDruid.Character
{
    public class Bear : StardewDruid.Character.Character
    {

        public BearRender bearRender;

        public Bear()
        {
        }

        public Bear(CharacterHandle.characters type = CharacterHandle.characters.BrownBear)
          : base(type)
        {

        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.CharacterType(Name);

            }

            characterTexture = CharacterHandle.CharacterTexture(characterType);

            bearRender = new(characterType.ToString());

            LoadIntervals();

            setScale = 3.75f;

            gait = 1.25f;

            modeActive = mode.random;

            idleFrames = new()
            {
                [idles.idle] = bearRender.idleFrames,
            };

            walkFrames = bearRender.walkFrames;

            dashFrames = new()
            {
                [dashes.dash] = bearRender.dashFrames,
                [dashes.smash] = bearRender.dashFrames,
            };

            specialFrames = new()
            {
                [specials.special] = bearRender.specialFrames,
                [specials.sweep] = bearRender.sweepFrames,
            };

            specialIntervals = new()
            {
                [specials.special] = 12,
                [specials.sweep] = 9,
            };

            specialFloors = new()
            {
                [specials.special] = 0,
                [specials.sweep] = 0,
            };

            specialCeilings = new()
            {
                [specials.special] = 8,
                [specials.sweep] = 5,
            };

            loadedOut = true;

        }

        public override bool SpriteAngle()
        {

            return SpriteFlip();

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128) || characterTexture == null)
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            Vector2 usePosition = SpritePosition(localPosition);

            DrawCharacter(b, usePosition);

        }

        public override void DrawCharacter(SpriteBatch b, Vector2 usePosition)
        {

            BearRenderAdditional additional = new()
            {
                layer = ((float)StandingPixel.Y + (float)LayerOffset()) / 10000f,

                scale = setScale,

                position = usePosition,

                flip = netDirection.Value == 3 || (netDirection.Value % 2 == 0 && netAlternative.Value == 3),

                fade = fadeOut
            };

            DrawEmote(b);

            if (netSpecial.Value != 0)
            {

                specials useSpecial = (specials)netSpecial.Value;

                if (!specialFrames.ContainsKey(useSpecial))
                {

                    useSpecial = specials.none;

                }

                additional.direction = netDirection.Value;

                additional.frame = specialFrame;

                switch (useSpecial)
                {

                    case specials.none:

                        break;

                    case specials.sweep:

                        additional.series = BearRenderAdditional.bearseries.sweep;

                        bearRender.DrawNormal(b, additional);

                        return;

                    default:

                        additional.series = BearRenderAdditional.bearseries.special;

                        bearRender.DrawNormal(b, additional);

                        return;

                }

            }

            if (netDash.Value != 0)
            {

                int dashSeries = netDirection.Value + (netDashProgress.Value * 4);

                int dashSetto = Math.Min(dashFrame, (dashFrames[(dashes)netDash.Value][dashSeries].Count - 1));

                additional.series = BearRenderAdditional.bearseries.dash;

                additional.direction = dashSeries;

                additional.frame = dashSetto;

                bearRender.DrawNormal(b, additional);

                return;

            }

            additional.direction = netDirection.Value;

            additional.frame = moveFrame;

            bearRender.DrawNormal(b, additional);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.special);

            specialTimer = 72;

            SetCooldown(specialTimer, 1f);

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

        public override Microsoft.Xna.Framework.Rectangle OverheadPortrait()
        {

            return new Rectangle(230, 33, 16, 16);

        }

    }

}
