using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using StardewDruid.Render;
using StardewDruid.Data;
using StardewDruid.Cast;
using StardewDruid.Handle;

namespace StardewDruid.Character
{
    public class FriendSerpent: StardewDruid.Character.Character
    {

        public int hoverHeight;

        public int hoverInterval;

        public int hoverIncrements;

        public float hoverElevate;

        public int hoverFrame;

        public SerpentRender serpentRender;

        public FriendSerpent()
        {
        }

        public FriendSerpent(CharacterHandle.characters type = CharacterHandle.characters.Serpent, int scheme = 0, int level = 1)
          : base(type, scheme, level)
        {

            netScheme.Set(scheme);

        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.CharacterType(Name);

            }

            CharacterHandle.characters spriteType = PalHandle.CharacterType(characterType, netScheme.Value);

            characterTexture = CharacterHandle.CharacterTexture(spriteType);

            serpentRender = new(spriteType);

            LoadIntervals();

            setScale = 3.5f + (0.25f * scale.Value);

            gait = 1.8f;

            hoverInterval = 24;

            hoverIncrements = 2;

            hoverElevate = 0.75f;

            modeActive = mode.random;

            idleFrames[idles.idle] = serpentRender.walkFrames;

            walkFrames = serpentRender.walkFrames;

            specialFrames[specials.special] = serpentRender.specialFrames[specials.special];

            specialIntervals[specials.special] = 15;

            specialCeilings[specials.special] = 3;

            specialFloors[specials.special] = 0;

            specialFrames[specials.sweep] = serpentRender.specialFrames[specials.special];

            specialIntervals[specials.sweep] = 15;

            specialCeilings[specials.sweep] = 3;

            specialFloors[specials.sweep] = 0;

            specialFrames[specials.tackle] = serpentRender.specialFrames[specials.tackle];

            specialIntervals[specials.tackle] = 12;

            specialCeilings[specials.tackle] = 5;

            specialFloors[specials.tackle] = 0;

            dashFrames[dashes.dash] = serpentRender.dashFrames[dashes.dash];

            dashFrames[dashes.smash] = serpentRender.dashFrames[dashes.dash];

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

            if (IsInvisible || !Utility.isOnScreen(Position, 128) || characterTexture == null)
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
            
            SerpentRenderAdditional hoverAdditional = new()
            {
                scale = setScale,

                position = usePosition,

                layer = (float)StandingPixel.Y / 10000f + 0.001f,

                flip = SpriteFlip(),

                fade = fadeOut,

                direction = netDirection.Value,

                frame = hoverFrame,

                series = SerpentRenderAdditional.serpentseries.none,
            };

            if (netDash.Value != 0)
            {

                hoverAdditional.direction = netDirection.Value + (netDashProgress.Value * 4);

                hoverAdditional.frame = Math.Min(dashFrame, (dashFrames[(dashes)netDash.Value][hoverAdditional.direction].Count - 1));

                hoverAdditional.series = SerpentRenderAdditional.serpentseries.dash;

            }
            else if (netSpecial.Value != 0)
            {

                hoverAdditional.frame = specialFrame;

                switch ((Character.specials)netSpecial.Value)
                {
                    default:
                    case specials.special:
                        hoverAdditional.series = SerpentRenderAdditional.serpentseries.special;
                        break;

                    case specials.sweep:
                        hoverAdditional.series = SerpentRenderAdditional.serpentseries.sweep;
                        break;

                    case specials.tackle:
                        hoverAdditional.series = SerpentRenderAdditional.serpentseries.tackle;
                        break;

                }

            }
            else
            if ((movements)netMovement.Value == movements.run)
            {

                hoverAdditional.series = SerpentRenderAdditional.serpentseries.tackle;

                hoverAdditional.frame = (int)((Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 3000) / 750);

            }

            serpentRender.DrawNormal(b,hoverAdditional);


        }

        public override int GetWidth()
        {

            return 64;

        }


        public override Vector2 SpritePosition(Vector2 localPosition)
        {
            
            Vector2 spritePosition = base.SpritePosition(localPosition);

            if (hoverInterval > 0)
            {

                spritePosition.Y -= (float)Math.Abs(hoverHeight) * hoverElevate;

            }

            return spritePosition;


        }

        public override void normalUpdate(GameTime time, GameLocation location)
        {

            base.normalUpdate(time, location);

            serpentRender.Update(pathActive == pathing.none);

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

            serpentRender.Update(pathActive == pathing.none);

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

            MissileHandle.missiles useMissile = MissileHandle.missiles.bubbleecho;

            if(netScheme.Value == 2)
            {

                useMissile = MissileHandle.missiles.echo;

            }
            else if (netScheme.Value == 3)
            {

                useMissile = MissileHandle.missiles.fireecho;

            }

            SpellHandle beam = new(Game1.player, monster.Position, 320, CombatDamage() / 2)
            {

                origin = Position,

                type = SpellHandle.Spells.echo,

                missile = useMissile,

            };

            Mod.instance.spellRegister.Add(beam);

            return true;

        }

        public override Microsoft.Xna.Framework.Rectangle OverheadPortrait()
        {

            return new Rectangle(48, 16, 16, 16);

        }

    }

}
