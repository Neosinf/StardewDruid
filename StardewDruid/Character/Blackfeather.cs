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

        public override bool SmashAttack(StardewValley.Monsters.Monster monster)
        {
            
            return SpecialAttack(monster);

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.invoke);

            specialTimer = 90;

            cooldownTimer = cooldownInterval;

            LookAtTarget(monster.Position, true);

            Mod.instance.iconData.DecorativeIndicator(currentLocation, Position, IconData.decorations.mists, 5f, new());

            SpellHandle special = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 256, -1, Mod.instance.CombatDamage() / 3);

            special.type = SpellHandle.spells.zap;

            special.scheme = IconData.schemes.golden;

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
