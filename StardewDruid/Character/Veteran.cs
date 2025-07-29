using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Location.Druid;
using StardewDruid.Render;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
    public class Veteran : StardewDruid.Character.Recruit
    {

        public Veteran()
        {
        }

        public Veteran(CharacterHandle.characters type, NPC villager)
          : base(type, villager)
        {


        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Veteran;

            }

            if (villager == null)
            {

                string whichMarlon = "Marlon";

                if (Mod.instance.Helper.ModRegistry.IsLoaded("FlashShifter.SVECode"))
                {
                    
                    whichMarlon = "MarlonFay";
                
                }

                villager = CharacterHandle.FindVillager(whichMarlon);

                Portrait = villager.Portrait;

            }

            characterTexture = CharacterHandle.CharacterTexture(CharacterHandle.characters.Marlon);

            LoadIntervals();

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.HumanoidSpecial();

            specialIntervals = CharacterRender.HumanoidIntervals();

            specialCeilings = CharacterRender.HumanoidCeilings();

            specialFloors = CharacterRender.HumanoidFloors();

            hatVectors = CharacterRender.HumanoidHats();

            idleFrames[idles.standby] = new()
            {
                [0] = new() { new(192, 0, 32, 32), },
                [1] = new() { new(192, 0, 32, 32), },
                [2] = new() { new(192, 0, 32, 32), },
                [3] = new() { new(192, 0, 32, 32), },
            };

            WeaponLoadout(WeaponRender.weapons.estoc);

            loadedOut = true;

        }

        public override bool MonsterFear()
        {

            return false;

        }

        public override void DrawLaunch(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            DrawStandby(b, spritePosition, drawLayer, fade);

        }

        public override void DrawStandby(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            base.DrawStandby(b, spritePosition, drawLayer, fade);

            int rate = Math.Abs((int)(Game1.currentGameTime.TotalGameTime.TotalSeconds % 12) - 6);

            Color circleColour = new Color(256 - rate, 256 - (rate * 9), 256 - (rate * 21));

            b.Draw(
                 Mod.instance.iconData.sheetTextures[IconData.tilesheets.ritual],
                 spritePosition + new Vector2(0, 14 * setScale),
                 new Rectangle(0, 96, 64, 48),
                 Color.White*fade,
                 0f,
                 new Vector2(32, 24),
                 setScale,
                 0,
                 drawLayer - 0.0005f
             );

            b.Draw(
                 Mod.instance.iconData.sheetTextures[IconData.tilesheets.ritual],
                 spritePosition + new Vector2(0, 14 * setScale),
                 new Rectangle(64, 96, 64, 48),
                 circleColour * fade,
                 0f,
                 new Vector2(32, 24),
                 setScale,
                 0,
                 drawLayer - 0.0006f
             );


        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.invoke);

            specialTimer = 90;

            SetCooldown(specialTimer, 1f);

            switch (Mod.instance.randomIndex.Next(5))
            {

                case 1: // igni

                    Mod.instance.spellRegister.Add(new(Position - new Vector2(0, 96), 64, IconData.impacts.igni, new()));

                    LookAtTarget(monster.Position, true);

                    SpellHandle fireecho = new(Game1.player, monster.Position, 320, CombatDamage() / 2)
                    {

                        origin = Position,

                        type = SpellHandle.Spells.echo,

                        missile = MissileHandle.missiles.fireecho,

                        added = new() { SpellHandle.Effects.embers, }

                    };

                    Mod.instance.spellRegister.Add(fireecho);

                    return true;

                case 2: // axii

                    LookAtTarget(monster.Position, true);

                    Mod.instance.spellRegister.Add(new(Position - new Vector2(0, 96), 64, IconData.impacts.axii, new()));

                    Mod.instance.spellRegister.Add(new(monster.Position, 320, IconData.impacts.lovebomb, new() { SpellHandle.Effects.charm}));

                    return true;

                case 3: // quen

                    if (!Mod.instance.eventRegister.ContainsKey(Rite.eventShield))
                    {

                        Mod.instance.spellRegister.Add(new(Position - new Vector2(0,96),64,IconData.impacts.quen,new()));

                        LookAtTarget(Game1.player.Position, true);

                        Cast.Effect.Shield shieldEffect = new();

                        shieldEffect.EventSetup(currentLocation, Game1.player.Position, Rite.eventShield);

                        shieldEffect.EventActivate();

                        return true;

                    }

                    break;

                case 4: // yrden

                    LookAtTarget(monster.Position, true);

                    Mod.instance.spellRegister.Add(new(Position - new Vector2(0, 96), 64, IconData.impacts.yrden, new()));

                    Mod.instance.spellRegister.Add(new(monster.Position, 320, IconData.impacts.none, new() { SpellHandle.Effects.binds }));

                    return true;

            }

            Mod.instance.spellRegister.Add(new(Position - new Vector2(0, 96), 64, IconData.impacts.aard, new()));

            LookAtTarget(monster.Position, true);

            SpellHandle beam = new(Game1.player, monster.Position, 320, CombatDamage() / 2)
            {

                origin = Position,

                type = SpellHandle.Spells.echo,

                missile = MissileHandle.missiles.windecho,

                added = new() { SpellHandle.Effects.push,}

            };

            Mod.instance.spellRegister.Add(beam);

            return true;

        }

        public override bool TrackNotReady()
        {

            if (villager.Name == Game1.player.spouse)
            {

                return false;

            }

            if (Game1.timeOfDay < 600)
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

        public override bool TrackConflict(Farmer player)
        {

            if (Mod.instance.characters.ContainsKey(CharacterHandle.characters.Marlon))
            {

                if (Mod.instance.characters[CharacterHandle.characters.Marlon].currentLocation.Name == player.currentLocation.Name)
                {

                    return true;

                }

            }

            if (Mod.instance.activeEvent.Count > 0)
            {

                foreach (KeyValuePair<int, StardewDruid.Character.Character> character in Mod.instance.eventRegister[Mod.instance.activeEvent.First().Key].companions)
                {

                    if (character.Value is Marlon)
                    {

                        return true;

                    }

                }

            }

            return base.TrackConflict(player);

        }

    }

}