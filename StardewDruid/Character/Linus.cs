using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Render;
using StardewValley;
using System;


namespace StardewDruid.Character
{
    public class Linus : StardewDruid.Character.Recruit
    {

        public BearRender bearRender;
        public CueWrapper growlCue;
        public CueWrapper growlCueTwo;
        public CueWrapper growlCueThree;

        public Linus()
        {
        }

        public Linus(CharacterHandle.characters type, NPC villager)
          : base(type, villager)
        {


        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.recruit_one;

            }

            if (villager == null)
            {

                villager = CharacterHandle.FindVillager("Linus");

            }

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Linus);

            LoadIntervals();

            setScale = 4f;

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.HumanoidSpecial();

            bearRender = new("YellowBear");

            dashFrames[dashes.smash] = bearRender.dashFrames;

            specialFrames[specials.special] = bearRender.sweepFrames;

            specialFrames[specials.sweep] = bearRender.sweepFrames;

            specialIntervals[specials.special] = 12;

            specialIntervals[specials.sweep] = 9;

            specialFloors[specials.special] = 0;

            specialFloors[specials.sweep] = 0;

            specialCeilings[specials.special] = 0;

            specialCeilings[specials.sweep] = 5;

            growlCue = Game1.soundBank.GetCue("BearGrowl") as CueWrapper;

            growlCue.Volume *= 2;

            growlCue.Pitch /= 2;

            growlCueTwo = Game1.soundBank.GetCue("BearGrowlTwo") as CueWrapper;

            growlCueTwo.Volume *= 2;

            growlCueTwo.Pitch /= 2;

            growlCueThree = Game1.soundBank.GetCue("BearGrowlThree") as CueWrapper;

            growlCueThree.Volume *= 2;

            growlCueThree.Pitch /= 2;


            loadedOut = true;

        }

        public override void DrawSmash(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            int dashSeries = netDirection.Value + (netDashProgress.Value * 4);

            int dashSetto = Math.Min(dashFrame, (dashFrames[(dashes)netDash.Value][dashSeries].Count - 1));

            BearRenderAdditional additional = new();

            additional.layer = drawLayer;

            additional.scale = setScale;

            additional.position = spritePosition;

            additional.flip = SpriteFlip();

            additional.fade = fade;

            additional.series = BearRenderAdditional.bearseries.dash;

            additional.direction = dashSeries;

            additional.frame = dashSetto;

            bearRender.DrawNormal(b, additional);

        }

        public override void DrawSweep(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            BearRenderAdditional additional = new();

            additional.layer = drawLayer;

            additional.scale = setScale;

            additional.position = spritePosition;

            additional.flip = SpriteFlip();

            additional.fade = fade;

            additional.direction = netDirection.Value;

            additional.frame = specialFrame;

            additional.mode = BearRenderAdditional.bearmode.growl;

            additional.series = BearRenderAdditional.bearseries.sweep;

            bearRender.DrawNormal(b, additional);

        }

        public override bool MonsterFear()
        {

            return false;

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            SmashAttack(monster);

            return true;

        }

        public override bool SweepAttack(StardewValley.Monsters.Monster monster)
        {

            if (base.SweepAttack(monster))
            {

                SpellHandle explode = new(Position, 256, IconData.impacts.smoke, new());

                explode.instant = true;

                explode.sound = SpellHandle.sounds.warrior;

                Mod.instance.spellRegister.Add(explode);

                return true;

            }

            return false;
        
        }

        public override bool SmashAttack(StardewValley.Monsters.Monster monster)
        {

            if (base.SmashAttack(monster))
            {

                SpellHandle explode = new(Position, 256, IconData.impacts.smoke, new());

                explode.instant = true;

                Mod.instance.spellRegister.Add(explode);

                BearGrowl();

                return true;

            }

            return false;

        }

        public override void ConnectSweep()
        {

            SpellHandle swipeEffect = new(Game1.player, Position, 256, Mod.instance.CombatDamage() / 2);

            swipeEffect.type = SpellHandle.spells.swipe;

            swipeEffect.added = new() { SpellHandle.effects.push, };

            swipeEffect.sound = SpellHandle.sounds.swordswipe;

            swipeEffect.display = IconData.impacts.flashbang;

            Mod.instance.spellRegister.Add(swipeEffect);

        }

        public override bool TrackNotReady()
        {

            if (villager.Name == Game1.player.spouse)
            {

                return false;

            }
            if (Game1.timeOfDay < 800)
            {

                return true;

            }

            return false;

        }

        public override bool TrackOutOfTime()
        {

            if (villager.Name == Game1.player.spouse)
            {

                return false;

            }

            if (Game1.timeOfDay > 2200)
            {

                return true;

            }

            return false;

        }

        public void BearGrowl()
        {

            if (growlCue.IsPlaying || growlCueTwo.IsPlaying || growlCueThree.IsPlaying)
            {

                return;

            }

            switch (Mod.instance.randomIndex.Next(6))
            {

                case 0:

                    growlCue.Play();

                    break;

                case 1:

                    growlCueTwo.Play();

                    break;

                case 2:

                    growlCueThree.Play();

                    break;

            }

        }

    }

}

