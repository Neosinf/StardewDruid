using Microsoft.Xna.Framework;
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
using static StardewDruid.Cast.SpellHandle;

namespace StardewDruid.Character
{
    public class Blackfeather : StardewDruid.Character.Character
    {

        public List<Vector2> ritesDone = new();

        public Blackfeather()
        {
        }

        public Blackfeather(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {
            base.LoadOut();

            setScale = 3.75f;

            cooldownInterval = 90;

            specialFrames[specials.launch] = new()
            {

                [0] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpUp,
                        CharacterRender.humanoidFrames.boxUp1,
                        CharacterRender.humanoidFrames.boxUp2,
                        CharacterRender.humanoidFrames.jumpUp,
                    }),
                [1] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpRight,
                        CharacterRender.humanoidFrames.boxRight1,
                        CharacterRender.humanoidFrames.boxRight2,
                        CharacterRender.humanoidFrames.jumpRight,
                    }),
                [2] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpDown,
                        CharacterRender.humanoidFrames.boxDown1,
                        CharacterRender.humanoidFrames.boxDown2,
                        CharacterRender.humanoidFrames.jumpDown,
                    }),
                [3] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpLeft,
                        CharacterRender.humanoidFrames.boxLeft1,
                        CharacterRender.humanoidFrames.boxLeft2,
                        CharacterRender.humanoidFrames.jumpLeft,
                    }),

            };

            specialIntervals[specials.launch] = 12;
            specialCeilings[specials.launch] = 3;
            specialFloors[specials.launch] = 0;

            specialFrames[specials.liftup] = new()
            {

                [0] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpUp,
                        CharacterRender.humanoidFrames.boxUp1,
                    }),
                [1] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpRight,
                        CharacterRender.humanoidFrames.boxRight1,
                    }),
                [2] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpDown,
                        CharacterRender.humanoidFrames.boxDown1,
                    }),
                [3] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.jumpLeft,
                        CharacterRender.humanoidFrames.boxLeft1,
                    }),

            };

            specialIntervals[specials.liftup] = 15;
            specialCeilings[specials.liftup] = 1;
            specialFloors[specials.liftup] = 1;

            specialFrames[specials.liftdown] = new()
            {

                [0] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.boxUp2,
                        CharacterRender.humanoidFrames.jumpUp,
                    }),
                [1] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.boxRight2,
                        CharacterRender.humanoidFrames.jumpRight,
                    }),
                [2] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.boxDown2,
                        CharacterRender.humanoidFrames.jumpDown,
                    }),
                [3] = CharacterRender.RectangleHumanoidList(
                    new()
                    {
                        CharacterRender.humanoidFrames.boxLeft2,
                        CharacterRender.humanoidFrames.jumpLeft,
                    }),

            };

            specialIntervals[specials.liftdown] = 15;
            specialCeilings[specials.liftdown] = 1;
            specialFloors[specials.liftdown] = 1;

            restSet = true;

        }

        public override void DrawLaunch(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {
            
            Rectangle useFrame = specialFrames[specials.launch][netDirection.Value][specialFrame];

            int specialHover = 0;

            switch (specialFrame)
            {
                case 0:
                case 3:

                    specialHover = 16;

                    break;

                case 1:
                case 2:

                    specialHover = 32;

                    break;

            }

            b.Draw(
                characterTexture,
                spritePosition - new Vector2(0,specialHover),
                useFrame,
                Color.White * fade,
                0.0f,
                new Vector2(useFrame.Width / 2, useFrame.Height / 2),
                setScale,
                SpriteAngle() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawShadow(b, spritePosition, drawLayer);

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

            if (new SpawnIndex(currentLocation).cultivate == false)
            {

                return false;

            }

            if (currentLocation.objects.Count() <= 0)
            {

                return false;

            }

            List<Vector2> tileVectors;

            for (int i = 0; i < 5; i++)
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

                            string scid = "scarecrow_companion_" + scarevector.X.ToString() + "_" + scarevector.Y.ToString();

                            if (!Mod.instance.rite.specialCasts.ContainsKey(currentLocation.Name))
                            {

                                Mod.instance.rite.specialCasts[currentLocation.Name] = new();

                            }

                            if (Mod.instance.rite.specialCasts[currentLocation.Name].Contains(scid))
                            {

                                continue;

                            }

                            ResetActives();

                            LookAtTarget(scarevector * 64, true);

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

            if (specialTimer == 80)
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

            if (specialTimer == 50)
            {

                CharacterHandle.RetrieveInventory(CharacterHandle.characters.Blackfeather);

                if (Mod.instance.chests[CharacterHandle.characters.Blackfeather].Items.Count > 0)
                {

                    Cultivate cultivateEvent = new();

                    cultivateEvent.EventSetup(workVector * 64, "effigy_cultivate_" + workVector.ToString());

                    cultivateEvent.inabsentia = true;

                    cultivateEvent.inventory = Mod.instance.chests[CharacterHandle.characters.Blackfeather].Items;

                    cultivateEvent.EventActivate();

                }

            }

            if (specialTimer == 20 && !Game1.IsRainingHere(currentLocation) && Game1.currentSeason != "winter")
            {

                Artifice artificeHandle = new();

                artificeHandle.ArtificeScarecrow(currentLocation, workVector,true);

            }
        }

        public override bool SmashAttack(StardewValley.Monsters.Monster monster)
        {
            
            return SpecialAttack(monster);

        }

        public override bool SweepAttack(StardewValley.Monsters.Monster monster)
        {
            
            return SpecialAttack(monster);
        
        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.launch);

            specialTimer = 48;

            cooldownTimer = cooldownInterval;

            LookAtTarget(monster.Position, true);

            SpellHandle special = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, Mod.instance.CombatDamage() / 3);

            special.type = SpellHandle.spells.zap;

            special.projectile = 6;

            //special.scheme = IconData.schemes.stars;

            special.counter = -24;

            special.added = new() { effects.shock, };

            Mod.instance.spellRegister.Add(special);

            return true;

        }

        public override void NewDay()
        {

            ritesDone.Clear();

        }


    }

}
