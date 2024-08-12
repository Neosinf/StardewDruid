﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.FruitTrees;
using StardewValley.Internal;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
    public class Effigy : StardewDruid.Character.Character
    {

        public List<Vector2> ritesDone = new();

        public Effigy()
        {
        }

        public Effigy(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {

            base.LoadOut();

            WeaponLoadout();

            weaponRender.swordScheme = IconData.schemes.stars;

            specialFrames[specials.launch] = new()
            {
                [0] = CharacterRender.RectangleHumanoidList(new() { CharacterRender.humanoidFrames.sweepRight1, CharacterRender.humanoidFrames.sweepRight2, CharacterRender.humanoidFrames.sweepRight3, CharacterRender.humanoidFrames.sweepRight4, }),
                [1] = CharacterRender.RectangleHumanoidList(new() { CharacterRender.humanoidFrames.sweepRight1, CharacterRender.humanoidFrames.sweepRight2, CharacterRender.humanoidFrames.sweepRight3, CharacterRender.humanoidFrames.sweepRight4, }),
                [2] = CharacterRender.RectangleHumanoidList(new() { CharacterRender.humanoidFrames.sweepRight1, CharacterRender.humanoidFrames.sweepRight2, CharacterRender.humanoidFrames.sweepRight3, CharacterRender.humanoidFrames.sweepRight4, }),
                [3] = CharacterRender.RectangleHumanoidList(new() { CharacterRender.humanoidFrames.sweepRight1, CharacterRender.humanoidFrames.sweepRight2, CharacterRender.humanoidFrames.sweepRight3, CharacterRender.humanoidFrames.sweepRight4, }),
            };

            specialIntervals[specials.launch] = 15;
            specialCeilings[specials.launch] = 3;
            specialFloors[specials.launch] = 3;

            idleFrames[idles.standby] = new()
            {
                [0] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
                [1] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
            };

            gait = 1.4f;

        }

        public override void DrawLaunch(SpriteBatch b, Vector2 localPosition, float drawLayer, float fade)
        {
            Rectangle useFrame = specialFrames[specials.launch][netDirection.Value][specialFrame];

            b.Draw(
                characterTexture,
                SpritePosition(localPosition),
                useFrame,
                Color.White * fade,
                0.0f,
                new Vector2(useFrame.Width / 2, useFrame.Height / 2),
                setScale,
                SpriteFlip() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawShadow(b, localPosition, drawLayer);

        }

        public override void DrawDash(SpriteBatch b, Vector2 localPosition, float drawLayer, float fade)
        {


            if (netDash.Value != (int)dashes.smash)
            {

                base.DrawDash(b, localPosition, drawLayer, fade);

                return;

            }

            int dashSeries = netDirection.Value + (netDashProgress.Value * 4);

            int dashSetto = Math.Min(dashFrame, (dashFrames[(dashes)netDash.Value][dashSeries].Count - 1));

            Vector2 dashVector = SpritePosition(localPosition) - new Vector2(0, dashHeight);

            Rectangle dashTangle = dashFrames[(dashes)netDash.Value][dashSeries][dashSetto];

            b.Draw(
                characterTexture,
                dashVector,
                dashTangle,
                Color.White * fade,
                0f,
                new Vector2(16),
                setScale,
                SpriteFlip() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawShadow(b, localPosition, drawLayer);

            weaponRender.DrawWeapon(b, dashVector - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = dashTangle, flipped = SpriteFlip() });

            if (netDashProgress.Value >= 2)
            {

                weaponRender.DrawSwipe(b, dashVector - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = dashTangle, flipped = SpriteFlip() });

            }

        }

        public override void DrawSweep(SpriteBatch b, Vector2 localPosition, float drawLayer, float fade)
        {
            Vector2 sweepVector = SpritePosition(localPosition);

            Rectangle sweepFrame = specialFrames[(specials)netSpecial.Value][netDirection.Value][specialFrame];

            b.Draw(
                characterTexture,
                sweepVector,
                sweepFrame,
                Color.White * fade,
                0.0f,
                new Vector2(16),
                setScale,
                0,
                drawLayer
            );

            DrawShadow(b, localPosition, drawLayer);

            weaponRender.DrawWeapon(b, sweepVector - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = sweepFrame, });

            weaponRender.DrawSwipe(b, sweepVector - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = sweepFrame, });


        }

        public override void DrawAlert(SpriteBatch b, Vector2 localPosition, float drawLayer, float fade)
        {

            Vector2 alertVector = SpritePosition(localPosition);

            Rectangle alertFrame = idleFrames[idles.alert][netDirection.Value][0];

            b.Draw(
                 characterTexture,
                 alertVector,
                 alertFrame,
                 Color.White * fade,
                 0f,
                 new Vector2(16),
                 setScale,
                 SpriteAngle() ? (SpriteEffects)1 : 0,
                 drawLayer
             );

            DrawShadow(b, localPosition, drawLayer);

            weaponRender.DrawWeapon(b, alertVector - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = alertFrame, flipped = SpriteAngle() });

        }

        public override void DrawStandby(SpriteBatch b, Vector2 localPosition, float drawLayer, float fade)
        {

            int chooseFrame = IdleFrame(idles.standby);

            b.Draw(
                characterTexture,
                SpritePosition(localPosition),
                idleFrames[idles.standby][0][chooseFrame],
                Color.White,
                0f,
                new Vector2(16),
                4f,
                netDirection.Value == 1 || netAlternative.Value == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer
            );

            b.Draw(
                characterTexture,
                SpritePosition(localPosition) + new Vector2(2,4),
                idleFrames[idles.standby][0][chooseFrame],
                Color.Black * 0.25f,
                0f,
                new Vector2(16),
                setScale,
                netDirection.Value == 1 || netAlternative.Value == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer - 0.001f
            );

            return;

        }

        public override List<Vector2> RoamAnalysis()
        {

            List<Vector2> collection = base.RoamAnalysis();

            if (Game1.currentSeason == "winter")
            {
                
                return collection;

            }

            List<Vector2> scarelist = new List<Vector2>();

            int takeABreak = 0;

            foreach (Dictionary<Vector2, StardewValley.Object> dictionary in currentLocation.Objects)
            {
                
                foreach (KeyValuePair<Vector2, StardewValley.Object> keyValuePair in dictionary)
                {
                    
                    if (keyValuePair.Value.IsScarecrow())
                    {

                        Vector2 scareVector = new(keyValuePair.Key.X * 64f, keyValuePair.Key.Y * 64f);

                        scarelist.Add(scareVector);

                        takeABreak++;

                    }

                    if (takeABreak >= 4)
                    {

                        scarelist.Add(new Vector2(-1f));

                        takeABreak = 0;

                    }

                }

                if (scarelist.Count >= 30)
                {

                    break;

                }
                    
            }

            scarelist.AddRange(collection);
            
            return scarelist;
        
        }

        public override bool TargetWork()
        {

            if (Game1.currentSeason == "winter")
            {
                
                return false;

            }

            if (Game1.IsRainingHere(currentLocation))
            {

                return false;

            }

            if(new SpawnIndex(currentLocation).cultivate == false)
            {

                return false;

            }

            if (currentLocation.objects.Count() < 0)
            {
                
                return false;
            
            }
            
            List<Vector2> tileVectors;

            for (int i = 0; i < 4; i++)
            {

                tileVectors = ModUtility.GetTilesWithinRadius(currentLocation, occupied, i);

                foreach (Vector2 scarevector in tileVectors)
                {

                    if (ritesDone.Contains(scarevector))
                    {

                        continue;

                    }

                    if (currentLocation.objects.ContainsKey(scarevector))
                    {

                        if (currentLocation.Objects[scarevector].IsScarecrow())
                        {

                            ResetActives();

                            LookAtTarget(scarevector * 64,true);

                            netSpecial.Set((int)specials.invoke);

                            specialTimer = 90;

                            workVector = scarevector;

                            ritesDone.Add(scarevector);

                            return true;

                        }

                    }
                
                }
            
            }

            return false;

        }

        public override void PerformWork()
        {

            if(specialTimer == 80)
            {

                if (currentLocation.Name == Game1.player.currentLocation.Name && Utility.isOnScreen(Position, 128))
                {

                    Mod.instance.iconData.DecorativeIndicator(currentLocation, Position, IconData.decorations.weald, 3f, new());

                    TemporaryAnimatedSprite skyAnimation = Mod.instance.iconData.SkyIndicator(currentLocation, Position, IconData.skies.valley, 1f, new() { interval = 1000, });

                    skyAnimation.scaleChange = 0.002f;

                    skyAnimation.motion = new(-0.064f, -0.064f);

                    skyAnimation.timeBasedMotion = true;

                    Game1.player.currentLocation.playSound("discoverMineral", null, 1000);

                }

            }

            if(specialTimer == 50)
            {

                CharacterHandle.RetrieveInventory(CharacterHandle.characters.Effigy);

                if(Mod.instance.chests[CharacterHandle.characters.Effigy].Items.Count > 0)
                {

                    Cultivate cultivateEvent = new();

                    cultivateEvent.EventSetup(workVector * 64, "effigy_cultivate_" + workVector.ToString());

                    cultivateEvent.inabsentia = true;

                    cultivateEvent.inventory = Mod.instance.chests[CharacterHandle.characters.Effigy].Items;

                    cultivateEvent.EventActivate();

                }

            }

            if(specialTimer == 20)
            {

                Artifice artificeHandle = new();

                artificeHandle.ArtificeScarecrow(currentLocation, workVector);

            }

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.launch);

            specialTimer = 90;

            cooldownTimer = cooldownInterval;

            LookAtTarget(monster.Position, true);

            //Mod.instance.iconData.DecorativeIndicator(currentLocation, Position, IconData.decorations.mists, 5f, new());

            SpellHandle special = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, Mod.instance.CombatDamage() / 2);

            special.type = SpellHandle.spells.missile;

            special.missile = IconData.missiles.fireball;

            special.scheme = IconData.schemes.stars;

            special.projectile = 2;

            special.power = 4;

            special.explosion = 256;

            special.terrain = 4;

            special.display = IconData.impacts.impact;

            Mod.instance.spellRegister.Add(special);

            return true;

        }

        public override void NewDay()
        {

            ritesDone.Clear();

        }


    }

}
