using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using StardewDruid.Render;
using StardewDruid.Data;
using StardewDruid.Cast;
using StardewDruid.Event;
using StardewDruid.Handle;
using System.Linq;

namespace StardewDruid.Character
{
    public class FriendSlime : StardewDruid.Character.Character
    {

        public SlimeRender slimeRender;

        public FriendSlime()
        {
        }

        public FriendSlime(CharacterHandle.characters type = CharacterHandle.characters.PalSlime, int scheme = 0, int level = 1)
          : base(type, scheme,level)
        {


        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.CharacterType(Name);

            }

            CharacterHandle.characters spriteType = PalHandle.CharacterType(characterType,netScheme.Value);

            slimeRender = new(spriteType);

            characterTexture = slimeRender.slimeTexture;

            LoadIntervals();

            idleInterval = 24;

            setScale = 3.5f + (0.25f * scale.Value);

            gait = 1.8f;

            modeActive = mode.random;

            idleFrames[idles.idle] = slimeRender.idleFrames[idles.idle];

            idleFrames[idles.standby] = slimeRender.idleFrames[idles.standby];

            walkFrames = slimeRender.idleFrames[idles.none];

            specialFrames[specials.special] = slimeRender.specialFrames[specials.special];

            specialIntervals[specials.special] = 12;

            specialCeilings[specials.special] = 4;

            specialFloors[specials.special] = -1;

            specialFrames[specials.sweep] = slimeRender.specialFrames[specials.special];

            specialIntervals[specials.sweep] = 12;

            specialCeilings[specials.sweep] = 3;

            specialFloors[specials.sweep] = 0;

            specialFrames[specials.tackle] = slimeRender.specialFrames[specials.tackle];

            specialIntervals[specials.tackle] = 12;

            specialCeilings[specials.tackle] = 3;

            specialFloors[specials.tackle] = 0;

            dashFrames[dashes.dash] = slimeRender.dashFrames;

            dashFrames[dashes.smash] = slimeRender.dashFrames;

            loadedOut = true;

        }

        public override void draw(SpriteBatch b, float alpha = -1f)
        {

            if (IsInvisible )
            {
                return;
            }
            
            if (!Utility.isOnScreen(Position, 128) )
            {
                return;
            }

            if (characterTexture == null)
            {
                return;
            }

            DrawEmote(b);

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            Vector2 usePosition = SpritePosition(localPosition);

            DrawCharacter(b, usePosition);

        }

        public override void DrawCharacter(SpriteBatch b, Vector2 spritePosition)
        {

            SlimeRenderAdditional hoverAdditional = new()
            {

                position = spritePosition,

                shadow = spritePosition + new Vector2(0, 2 * setScale),

                scale = setScale,

                layer = (float)StandingPixel.Y / 10000f + 0.001f,

                flip = SpriteFlip(),

                fade = fadeOut,

                direction = netDirection.Value,

            };

            if (netDash.Value != 0)
            {

                hoverAdditional.direction = netDirection.Value + (netDashProgress.Value * 4);

                hoverAdditional.frame = dashFrame;

                hoverAdditional.series = SlimeRenderAdditional.slimeseries.dash;

            }
            else if (netSpecial.Value != 0)
            {

                hoverAdditional.frame = specialFrame;

                switch ((Character.specials)netSpecial.Value)
                {
                    default:
                    case specials.special:
                        hoverAdditional.series = SlimeRenderAdditional.slimeseries.special;
                        break;

                    case specials.sweep:
                        hoverAdditional.series = SlimeRenderAdditional.slimeseries.sweep;
                        break;

                    case specials.tackle:
                        hoverAdditional.series = SlimeRenderAdditional.slimeseries.tackle;
                        break;

                }

            }
            else
            {

                switch ((Character.idles)netIdle.Value)
                {

                    case idles.idle:
                    case idles.alert:

                        hoverAdditional.frame = IdleFrame(idles.idle);

                        hoverAdditional.series = SlimeRenderAdditional.slimeseries.idle;

                        break;

                    case idles.standby:
                    case idles.rest:

                        hoverAdditional.frame = IdleFrame(idles.standby);

                        hoverAdditional.series = SlimeRenderAdditional.slimeseries.standby;

                        break;

                }

            }

            slimeRender.DrawNormal(b, hoverAdditional);

        }

        public override void normalUpdate(GameTime time, GameLocation location)
        {

            base.normalUpdate(time, location);

            bool adjust = netIdle.Value == 0;

            slimeRender.Update(adjust);

            if (modeActive == mode.track)
            {

                fellowship++;

                if (fellowship >= 6000)
                {

                    PalHandle.LevelUpdate(characterType);

                    fellowship = 0;

                }

            }

        }

        public override void UpdateBattle()
        {

            base.UpdateBattle();

            slimeRender.Update(true);

        }

        public override int CombatDamage()
        {

            float damage = (float)Mod.instance.CombatDamage();

            float refactor = 0.7f;

            int experience = Mod.instance.save.pals[characterType].experience;

            if (refactor > 100)
            {

                refactor = 1.2f;


            }
            else if (refactor > 75)
            {

                refactor = 1f;

            }
            else if (refactor > 50)
            {


                refactor = 0.9f;


            }
            else if (refactor > 25)
            {

                refactor = 0.8f;
            }

            damage *= refactor;

            return (int)damage;

        }

        public override void ConnectSweep()
        {

            SpellHandle swipeEffect = new(Game1.player, Position, 192, CombatDamage() / 2)
            {
                type = SpellHandle.Spells.swipe,

                added = new() { SpellHandle.Effects.push, },

                sound = SpellHandle.Sounds.slime
            };

            Mod.instance.spellRegister.Add(swipeEffect);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.special);

            specialTimer = specialFrames[specials.special][0].Count * specialIntervals[specials.special];

            SetCooldown(specialTimer, 1f);

            LookAtTarget(monster.Position, true);

            SpellHandle fireball = new(
                currentLocation, 
                monster.Position, 
                GetBoundingBox().Center.ToVector2(), 
                48 + (int)(24 * setScale), 
                -1,
                CombatDamage()/2
                )
            {
                display = IconData.impacts.splatter,

                displayRadius = (int)setScale,

                displayFactor = Math.Min(4,1 + (int)setScale),

                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.slimeball,

                sound = SpellHandle.Sounds.slime,

                counter = -20,

            };

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

        public override Microsoft.Xna.Framework.Rectangle OverheadPortrait()
        {

            return new Rectangle(14, 5, 16, 16);

        }

    }

}
