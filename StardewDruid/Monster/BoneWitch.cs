using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewValley;
using StardewValley.Monsters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Monster
{
    public class BoneWitch : Boss
    {

        public float fadeOut;

        public HoverRender hoverRender;

        public BoneWitch()
        {


        }

        public BoneWitch(Vector2 vector, int CombatModifier, string name = "BoneWitch")
          : base(vector, CombatModifier, name)
        {

        }

        public override void LoadOut()
        {

            characterTexture = MonsterHandle.MonsterTexture(realName.Value);

            hoverRender = new(CharacterHandle.CharacterType(realName.Value));

            cooldownInterval = 240;

            GhostWalk();

            GhostFlight();

            GhostSpecial();

            fadeOut = 0.005f;

            loadedOut = true;

            lightId = "18465_" + realName.Value + "_" + ModUtility.PositionToSerial(Position);

            warp = IconData.warps.death;

        }

        public void GhostWalk()
        {

            baseMode = 0;

            baseJuice = 4;

            basePulp = 35;

            walkInterval = 9;

            gait = 2;

            idleFrames = hoverRender.walkFrames;

            walkFrames = hoverRender.walkFrames;

        }

        public void GhostFlight()
        {

            flightSet = true;

            flightSpeed = 12;

            flightHeight = 2;

            flightDefault = flightTypes.past;

            flightInterval = 9;

            flightFrames = hoverRender.dashFrames[Character.Character.dashes.dash];

            smashSet = true;

            smashFrames = hoverRender.dashFrames[Character.Character.dashes.dash];

        }

        public virtual void GhostSpecial()
        {

            specialSet = true;

            specialCeiling = 3;

            specialFloor = 0;

            specialInterval = 9;

            cooldownTimer = cooldownInterval;

            specialFrames = hoverRender.specialFrames[Character.Character.specials.special];

            sweepSet = true;

            sweepInterval = 12;

            sweepFrames = walkFrames;

        }

        public override float GetScale()
        {

            return 3.5f + netMode.Value * 0.5f;

        }

        public override int GetHeight()
        {

            return 48;

        }

        public override int GetWidth()
        {

            return 48;

        }

        public override Rectangle GetBoundingBox()
        {

            Rectangle box = base.GetBoundingBox();

            if (netFlightActive.Value || netSmashActive.Value)
            {

                Point center = box.Center;

                box.X = center.X;

                box.Y = center.Y;

                box.Width = 1;

                box.Height = 1;

            }

            return box;

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {


        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {

            base.drawAboveAlwaysFrontLayer(b);

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            HoverRenderAdditional hoverAdditional = new()
            {
                scale = GetScale()
            };

            hoverAdditional.position = GetPosition(localPosition, hoverAdditional.scale);

            hoverAdditional.layer = (float)StandingPixel.Y / 10000f + 0.001f;

            hoverAdditional.flip = netDirection.Value == 3 || netDirection.Value % 2 == 0 && netAlternative.Value == 3;

            hoverAdditional.fade = fadeOut;

            hoverAdditional.direction = netDirection.Value;

            if (netFlightActive.Value || netSmashActive.Value)
            {

                hoverAdditional.direction = netDirection.Value + (netFlightProgress.Value * 4);

                hoverAdditional.frame = Math.Min(flightFrame, (flightFrames[hoverAdditional.direction].Count - 1));

                hoverAdditional.series = HoverRenderAdditional.hoverseries.dash;

            }
            else if (netSpecialActive.Value)
            {

                hoverAdditional.series = HoverRenderAdditional.hoverseries.special;

            }

            hoverRender.DrawNormal(b, hoverAdditional);

            DrawEmote(b, localPosition, hoverAdditional.layer);

        }

        public override void update(GameTime time, GameLocation location)
        {

            base.update(time, location);

            if (fadeOut < 0.75f)
            {

                fadeOut += 0.0025f;

            }

            hoverRender.Update(inMotion == false);

        }

        public override bool PerformSweep(Vector2 target)
        {
            if (!sweepSet) { return false; }

            List<Farmer> targets = ModUtility.FarmerProximity(currentLocation, new() { Position, }, 96f + (GetWidth() * 3));

            if (targets.Count > 0)
            {

                Vector2 targetFarmer = targets.First().Position;

                return PerformFlight(targetFarmer);

            }

            return false;

        }

        public override bool PerformSpecial(Vector2 target)
        {

            LookAtTarget(target);

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(2);

            Vector2 spellCenter = GetBoundingBox().Center.ToVector2();

            Vector2 diff = target - spellCenter;

            Vector2 spellDestination = spellCenter + (diff * 2);

            List<Vector2> points = new()
            {
                spellDestination + new Vector2(160),
                spellDestination + new Vector2(-160),
                spellDestination+ new Vector2(160,-160),
                spellDestination + new Vector2(-160,160),
                spellDestination+ new Vector2(320,0),
                spellDestination + new Vector2(-320,0),
                spellDestination + new Vector2(0,-320),
                spellDestination + new Vector2(0,320),

            };

            SpellHandle bat = new(currentLocation, spellDestination, spellCenter, 256, GetThreat())
            {
                type = SpellHandle.Spells.missile,

                factor = 4,

                missile = MissileHandle.missiles.bats,

                display = IconData.impacts.flashbang,

                displayRadius = 3,

                sound = SpellHandle.Sounds.batFlap,

                boss = this

            };

            Mod.instance.spellRegister.Add(bat);

            foreach (Vector2 point in points)
            {

                SpellHandle otherbats = new(currentLocation, point, spellCenter, 64, -1)
                {

                    type = SpellHandle.Spells.missile,

                    factor = Mod.instance.randomIndex.Next(2,5),

                    missile = MissileHandle.missiles.bats,

                    display = IconData.impacts.flashbang,

                    displayRadius = 3,

                    sound = SpellHandle.Sounds.batFlap,

                    boss = this

                };

                Mod.instance.spellRegister.Add(otherbats);

            }

            return true;

        }

        public override void ConnectSweep()
        {

            List<Farmer> targets = ModUtility.FarmerProximity(currentLocation, new() { Position, }, 128f);

            if (targets.Count > 0)
            {

                SpellHandle bang = new(currentLocation, targets.First().Position, GetBoundingBox().Center.ToVector2(), 160, GetThreat())
                {
                    type = SpellHandle.Spells.explode,

                    display = IconData.impacts.flasher,

                    instant = true,

                    boss = this
                };

                if (Mod.instance.randomIndex.Next(2) == 0)
                {

                    bang.added = new() { SpellHandle.Effects.chain };

                }

                Mod.instance.spellRegister.Add(bang);

            }

        }

        public override bool PerformChannel(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            int offset = Mod.instance.randomIndex.Next(2);

            for (int i = 0; i < 4; i++)
            {

                List<Vector2> castSelection = ModUtility.GetTilesWithinRadius(currentLocation, ModUtility.PositionToTile(Game1.player.Position), Mod.instance.randomIndex.Next(2, 3), true, (i * 2) + offset % 8);

                if (castSelection.Count > 0)
                {

                    Vector2 tryVector = castSelection[Mod.instance.randomIndex.Next(castSelection.Count)];

                    SpellHandle fireball = new(currentLocation, tryVector * 64, GetBoundingBox().Center.ToVector2(), 192, GetThreat())
                    {
                        type = SpellHandle.Spells.missile,

                        factor = 3,

                        missile = MissileHandle.missiles.deathfall,

                        display = IconData.impacts.deathbomb,

                        scheme = IconData.schemes.death,

                        indicator = IconData.cursors.death,

                        boss = this
                    };

                    Mod.instance.spellRegister.Add(fireball);

                }

            }

            return true;

        }

        public override Texture2D OverheadTexture()
        {

            return characterTexture;

        }

        public override Microsoft.Xna.Framework.Rectangle OverheadPortrait()
        {

            return new Rectangle(25, 17, 16, 16);

        }

    }

}

