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
using StardewModdingAPI;

namespace StardewDruid.Character
{
    public class FriendBat : StardewDruid.Character.Character
    {

        public BatRender hoverRender;

        public FriendBat()
        {
        }

        public FriendBat(CharacterHandle.characters type = CharacterHandle.characters.Bat, int scheme = 0, int level = 1)
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

            characterTexture = CharacterHandle.CharacterTexture(spriteType);

            hoverRender = new(spriteType);

            LoadIntervals();

            setScale = 3.5f + (0.25f * scale.Value);

            gait = 1.8f;

            modeActive = mode.random;

            idleFrames[idles.idle] = hoverRender.walkFrames;

            idleFrames[idles.rest] = hoverRender.restFrames;

            walkFrames = hoverRender.walkFrames;

            specialFrames[specials.special] = hoverRender.specialFrames[specials.special];

            specialIntervals[specials.special] = 15;

            specialCeilings[specials.special] = 2;

            specialFloors[specials.special] = 0;

            specialFrames[specials.sweep] = hoverRender.specialFrames[specials.special];

            specialIntervals[specials.sweep] = 15;

            specialCeilings[specials.sweep] = 3;

            specialFloors[specials.sweep] = 0;

            specialFrames[specials.tackle] = hoverRender.specialFrames[specials.tackle];

            specialIntervals[specials.tackle] = 10;

            specialCeilings[specials.tackle] = 5;

            specialFloors[specials.tackle] = 0;

            dashFrames[dashes.dash] = hoverRender.dashFrames[dashes.dash];

            dashFrames[dashes.smash] = hoverRender.dashFrames[dashes.dash];

            restSet = true;

            loadedOut = true;

        }

        public override void draw(SpriteBatch b, float alpha = -1f)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128) || characterTexture == null)
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            Vector2 usePosition = SpritePosition(localPosition);

            DrawCharacter(b, usePosition);

        }

        /*public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
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

        }*/

        public override void DrawCharacter(SpriteBatch b, Vector2 usePosition)
        {

            BatRenderAdditional hoverAdditional = new()
            {

                position = usePosition,

                scale = setScale,

                layer = (float)StandingPixel.Y / 10000f + 0.001f,

                flip = SpriteFlip(),

                fade = fadeOut,

                direction = netDirection.Value,

                series = BatRenderAdditional.batseries.none,

            };

            if (netDash.Value != 0)
            {

                hoverAdditional.direction = netDirection.Value + (netDashProgress.Value * 4);

                hoverAdditional.frame = Math.Min(dashFrame, (dashFrames[(dashes)netDash.Value][hoverAdditional.direction].Count - 1));

                hoverAdditional.series = BatRenderAdditional.batseries.dash;

            }
            else if (netSpecial.Value != 0)
            {

                hoverAdditional.frame = specialFrame;

                Character.specials special = (Character.specials)netSpecial.Value;

                switch (special)
                {
                    default:
                    case specials.special:
                        hoverAdditional.series = BatRenderAdditional.batseries.special;
                        break;

                    case specials.sweep:
                        hoverAdditional.series = BatRenderAdditional.batseries.sweep;
                        break;

                    case specials.tackle:
                        hoverAdditional.series = BatRenderAdditional.batseries.tackle;
                        break;

                }

            }
            else if (netIdle.Value == (int)idles.rest)
            {

                hoverAdditional.series = BatRenderAdditional.batseries.rest;

                hoverAdditional.frame = IdleFrame(idles.rest);

            }

            hoverRender.DrawNormal(b,hoverAdditional);

        }


        public override void normalUpdate(GameTime time, GameLocation location)
        {

            base.normalUpdate(time, location);

            hoverRender.Update(pathActive == pathing.none);

            if(modeActive == mode.track)
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

            hoverRender.Update(pathActive == pathing.none);

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

        public override bool SweepAttack(StardewValley.Monsters.Monster monster)
        {

            return SmashAttack(monster);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.special);

            specialTimer = 90;

            SetCooldown(specialTimer, 1f);

            LookAtTarget(monster.Position, true);

            SpellHandle beam = new(Game1.player, monster.Position, 320, CombatDamage() / 2)
            {

                origin = Position,

                type = SpellHandle.Spells.echo,

                missile = MissileHandle.missiles.echo

            };

            Mod.instance.spellRegister.Add(beam);

            return true;

        }

        public override Microsoft.Xna.Framework.Rectangle OverheadPortrait()
        {

            return new Rectangle(14, 5, 16, 16);

        }

    }

}
