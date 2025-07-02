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

namespace StardewDruid.Character
{
    public class FriendFiend : StardewDruid.Character.Character
    {

        public FiendRender fiendRender;

        public FriendFiend()
        {
        }

        public FriendFiend(CharacterHandle.characters type = CharacterHandle.characters.Dustfiend, int scheme = 0, int level = 1)
          : base(type, scheme, level)
        {


        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.CharacterType(Name);

            }

            CharacterHandle.characters spriteType = PalHandle.CharacterType(characterType,netScheme.Value);

            fiendRender = new(spriteType);

            characterTexture = fiendRender.fiendTexture;

            LoadIntervals();

            setScale = 2.75f + (0.25f * scale.Value);

            gait = 1.8f;

            modeActive = mode.random;

            cooldownInterval = 300;

            idleFrames[idles.idle] = fiendRender.idleFrames;

            walkFrames = fiendRender.idleFrames;

            specialFrames[specials.special] = fiendRender.specialFrames[specials.special];

            specialIntervals[specials.special] = 8;

            specialCeilings[specials.special] = 8;

            specialFloors[specials.special] = 0;

            specialFrames[specials.sweep] = fiendRender.specialFrames[specials.sweep];

            specialIntervals[specials.sweep] = 8;

            specialCeilings[specials.sweep] = 6;

            specialFloors[specials.sweep] = 0;

            specialFrames[specials.tackle] = fiendRender.specialFrames[specials.tackle];

            specialIntervals[specials.tackle] = 7;

            specialCeilings[specials.tackle] = 8;

            specialFloors[specials.tackle] = 0;

            dashFrames[dashes.dash] = fiendRender.dashFrames;

            dashPeak = 128;

            dashInterval = 9;

            dashFrames[dashes.smash] = fiendRender.dashFrames;

            loadedOut = true;

        }

        public override void draw(SpriteBatch b, float alpha = -1f)
        {

            setScale = 2.75f + (0.25f * scale.Value);

            if (IsInvisible)
            {

                return;
            }
            if (!Utility.isOnScreen(Position, 128))
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
            FiendRenderAdditional hoverAdditional = new()
            {

                position = spritePosition,

                shadow = spritePosition + new Vector2(0, 4 * setScale),

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

                hoverAdditional.series = FiendRenderAdditional.fiendseries.dash;

            }
            else if (netSpecial.Value != 0)
            {

                hoverAdditional.frame = specialFrame;

                switch ((Character.specials)netSpecial.Value)
                {
                    default:
                    case specials.special:
                        hoverAdditional.series = FiendRenderAdditional.fiendseries.special;
                        break;

                    case specials.sweep:
                        hoverAdditional.series = FiendRenderAdditional.fiendseries.sweep;
                        break;

                    case specials.tackle:
                        hoverAdditional.series = FiendRenderAdditional.fiendseries.tackle;
                        break;

                }

            }

            fiendRender.DrawNormal(b, hoverAdditional);

        }


        public override void normalUpdate(GameTime time, GameLocation location)
        {

            base.normalUpdate(time, location);

            bool adjust = (idles)netIdle.Value != idles.idle;

            fiendRender.Update(adjust);

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

            fiendRender.Update(true);

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

            SpellHandle fireball = new(
                currentLocation, 
                GetBoundingBox().Center.ToVector2(), 
                GetBoundingBox().Center.ToVector2(), 
                128 + (int)(32 * setScale), 
                -1, 
                CombatDamage()/2)
            {

                display = IconData.impacts.bigimpact,

                displayRadius = (int)setScale,

                type = SpellHandle.Spells.explode,

                instant = true,

                sound = SpellHandle.Sounds.fireball,

            };

            Mod.instance.spellRegister.Add(fireball);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            SmashAttack(monster);

            return true;

        }

        public override Microsoft.Xna.Framework.Rectangle OverheadPortrait()
        {

            return new Rectangle(14, 5, 16, 16);

        }

    }

}
