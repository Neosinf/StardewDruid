using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Handle;
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

            WeaponLoadout(WeaponRender.weapons.starsword);

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
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
                [1] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(192, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                    new Rectangle(224, 0, 32, 32),
                },
            };

            gait = 1.4f;

            restSet = true;

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

            CharacterHandle.RetrieveInventory(CharacterHandle.characters.Effigy);

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

                    if (currentLocation.objects.ContainsKey(scarevector))
                    {

                        if (currentLocation.Objects[scarevector].IsScarecrow())
                        {

                            string workString = Game1.season.ToString() + Game1.dayOfMonth.ToString() + "_" + currentLocation.Name + "_" + scarevector.X.ToString() + "_" + scarevector.Y.ToString();

                            if (workRegister.Contains(workString))
                            {

                                continue;

                            }

                            ResetActives();

                            LookAtTarget(scarevector * 64,true);

                            netSpecial.Set((int)specials.invoke);

                            specialTimer = 90;

                            workVector = scarevector;

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

                if(Mod.instance.chests[CharacterHandle.characters.Effigy].Items.Count > 0)
                {

                    Cultivate cultivateEvent = new();

                    cultivateEvent.EventSetup(currentLocation, workVector * 64, "effigy_cultivate_" + workVector.ToString());

                    cultivateEvent.inventory = Mod.instance.chests[CharacterHandle.characters.Effigy].Items;

                    cultivateEvent.EventActivate();

                }

                string workString = Game1.season.ToString() + Game1.dayOfMonth.ToString() + "_" + currentLocation.Name + "_" + workVector.X.ToString() + "_" + workVector.Y.ToString();

                if (!workRegister.Contains(workString))
                {

                    workRegister.Add(workString);

                }

            }

            if (specialTimer == 20 && !Game1.IsRainingHere(currentLocation) && Game1.currentSeason != "winter")
            {

                Artifice artificeHandle = new();

                artificeHandle.ArtificeScarecrow(currentLocation, workVector, true);

            }

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.hadouken);

            specialTimer = 90;

            SetCooldown(specialTimer, 1f);

            LookAtTarget(monster.Position, true);

            SpellHandle special = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, Mod.instance.CombatDamage() / 2)
            {
                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.fireball,

                counter = -30,

                scheme = IconData.schemes.stars,

                factor = 2,

                power = 4,

                explosion = 4,

                terrain = 4,

                display = IconData.impacts.bomb
            };

            Mod.instance.spellRegister.Add(special);

            return true;

        }

        public override mode SpecialMode(mode modechoice)
        {

            switch (modechoice)
            {

                case mode.home:

                case mode.random:

                case mode.roam:

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.challengeBones))
                    {

                        return mode.limbo;

                    }


                    break;

            }

            return modechoice;

        }


    }

}
