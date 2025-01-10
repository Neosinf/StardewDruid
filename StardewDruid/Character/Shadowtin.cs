using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.Machines;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Character
{
    public class Shadowtin : StardewDruid.Character.Character
    {

        public Shadowtin()
        {
        }

        public Shadowtin(CharacterHandle.characters type)
          : base(type)
        {

            
        }

        public override void LoadOut()
        {
            
            base.LoadOut();

            idleFrames[idles.standby] = new(specialFrames[specials.sweep]);

            WeaponLoadout();

            idleFrames[idles.alert] = new()
            {
                [0] = new()
                {
                    new Rectangle(192, 288, 32, 32),
                },
                [1] = new()
                {
                    new Rectangle(224, 288, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(128, 288, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(160, 288, 32, 32),
                },
            };

            switch (characterType)
            {

                default:
                case CharacterHandle.characters.Shadowtin:

                    weaponRender.LoadWeapon(WeaponRender.weapons.carnyx);

                    break;

                case CharacterHandle.characters.DarkRogue:

                    weaponRender.LoadWeapon(WeaponRender.weapons.estoc);

                    idleFrames[idles.kneel] = new()
                    {
                        [0] = new()
                        {
                            new Rectangle(128, 32, 32, 32),
                        },

                    };

                    break;

                case CharacterHandle.characters.DarkGoblin:

                    weaponRender.LoadWeapon(WeaponRender.weapons.axe);

                    idleFrames[idles.kneel] = new()
                    {
                        [0] = new()
                        {
                            new Rectangle(128, 32, 32, 32),
                        },

                    };

                    break;



            }

            weaponRender.LoadWeapon(WeaponRender.weapons.bazooka);

            restSet = true;

        }

        public override void DrawRest(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            base.DrawRest(b, spritePosition, drawLayer+0.064f, fade);

        }

        public override void NewDay()
        {

            CharacterHandle.RetrieveInventory(characterType);

            List<StardewValley.Item> marmalade = new();

            StardewValley.Object preserveJar = ItemRegistry.Create<StardewValley.Object>("(BC)15");

            MachineData machineData = preserveJar.GetMachineData();

            GameLocation farm = Game1.getFarm();

            MachineOutputRule outputRule;

            int? overrideMinutesUntilReady;

            for (int i = Mod.instance.chests[characterType].Items.Count - 1; i >= 0; i--)
            {

                Item getItem = Mod.instance.chests[characterType].Items.ElementAt(i);

                StardewValley.Object @object = ItemRegistry.Create<StardewValley.Object>(getItem.QualifiedItemId);

                if (@object.Category == -79)
                {

                    if (!MachineDataUtility.TryGetMachineOutputRule(preserveJar, machineData, MachineOutputTrigger.ItemPlacedInMachine, getItem, Game1.player, farm, out outputRule, out var _, out var _, out var _))
                    {
                        continue;
                    }

                    MachineItemOutput outputData = MachineDataUtility.GetOutputData(preserveJar, machineData, outputRule, getItem, Game1.player, farm);

                    Item outputItem = MachineDataUtility.GetOutputItem(preserveJar, outputData, getItem, Game1.player, false, out overrideMinutesUntilReady);

                    if(outputItem == null)
                    {

                        continue;

                    }

                    outputItem.Stack = getItem.Stack;

                    marmalade.Add(outputItem);

                    Mod.instance.chests[characterType].Items.RemoveAt(i);

                }

            }

            foreach (StardewValley.Item conversion in marmalade)
            {

                Mod.instance.chests[characterType].addItem(conversion);

            }

        }

        public override void ConnectSweep()
        {

            SpellHandle swipeEffect = new(Game1.player, Position, 192, Mod.instance.CombatDamage() / 2);

            swipeEffect.instant = true;

            swipeEffect.added = new() { SpellHandle.effects.knock, };

            swipeEffect.sound = SpellHandle.sounds.swordswipe;

            Mod.instance.spellRegister.Add(swipeEffect);

        }

        public override bool TargetWork()
        {

            CharacterHandle.RetrieveInventory(CharacterHandle.characters.Shadowtin);

            List<Vector2> objectVectors = new List<Vector2>();

            for (int i = 0; i < 6; i++)
            {

                if (currentLocation.objects.Count() == 0)
                {
                    break;

                }

                objectVectors = ModUtility.GetTilesWithinRadius(currentLocation, occupied, i); ;

                foreach (Vector2 objectVector in objectVectors)
                {

                    if (currentLocation.objects.ContainsKey(objectVector))
                    {

                        string workString = Game1.season.ToString() + Game1.dayOfMonth.ToString() + "_" + currentLocation.Name + "_" + objectVector.X.ToString() + "_" + objectVector.Y.ToString();

                        if (workRegister.Contains(workString))
                        {

                            continue;

                        }

                        workRegister.Add(workString);

                        StardewValley.Object targetObject = currentLocation.objects[objectVector];

                        if (ValidWorkTarget(targetObject))
                        {

                            ResetActives();

                            workVector = objectVector;

                            Mod.instance.iconData.AnimateQuickWarp(currentLocation, Position, true);

                            Position = (workVector * 64);

                            Mod.instance.iconData.AnimateQuickWarp(currentLocation, Position);

                            netSpecial.Set((int)specials.pickup);

                            specialTimer = 60;

                            return true;

                        }

                    }

                }

            }

            return false;

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

                    if (
                        keyValuePair.Value.Name.Contains("Artifact Spot") ||
                        keyValuePair.Value.isForage() ||
                        keyValuePair.Value.QualifiedItemId == "(BC)9" ||
                        keyValuePair.Value.QualifiedItemId == "(BC)10" ||
                        keyValuePair.Value.QualifiedItemId == "(BC)MushroomLog" ||
                        keyValuePair.Value.IsTapper()
                    )
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

            }

            scarelist.AddRange(collection);

            return scarelist;

        }

        public bool ValidWorkTarget(StardewValley.Object targetObject)
        {

            if (targetObject.Name.Contains("Artifact Spot"))
            {

                return true;
            
            }

            if (targetObject.isForage())
            {

                return true;

            }

            if (
                targetObject.QualifiedItemId == "(BC)9" ||
                targetObject.QualifiedItemId == "(BC)10" ||
                targetObject.QualifiedItemId == "(BC)MushroomLog" ||
                targetObject.IsTapper()
                )
            {

                if (targetObject.heldObject.Value != null && targetObject.MinutesUntilReady == 0)
                {

                    return true;
                
                }

            }

            return false;

        }

        public override void PerformWork()
        {

            if (specialTimer == 30)
            {

                if (currentLocation.objects.ContainsKey(workVector))
                {

                    Chest chest = Mod.instance.chests[CharacterHandle.characters.Shadowtin];

                    StardewValley.Object targetObject = currentLocation.objects[workVector];

                    if (targetObject.Name.Contains("Artifact Spot"))
                    {

                        currentLocation.digUpArtifactSpot((int)workVector.X, (int)workVector.Y, Game1.player);

                        currentLocation.objects.Remove(workVector);

                        return;

                    }
                    
                    if (SpawnData.ForageCheck(targetObject))
                    {

                        StardewValley.Item objectInstance = ModUtility.ExtractForage(currentLocation, workVector, false);

                        if (
                            currentLocation.Name == Game1.player.currentLocation.Name &&
                            Vector2.Distance(Game1.player.Position,Position) <= 640
                        )
                        {

                            ThrowHandle throwItem = new(Game1.player, Position, objectInstance);

                            Mod.instance.throwRegister.Add(throwItem);

                        }
                        else
                        if (chest.addItem(objectInstance) != null)
                        {

                            return;

                        }

                        currentLocation.objects.Remove(workVector);

                        return;

                    }

                    if (
                        targetObject.QualifiedItemId == "(BC)9" ||
                        targetObject.QualifiedItemId == "(BC)10" ||
                        targetObject.QualifiedItemId == "(BC)MushroomLog" ||
                        targetObject.IsTapper()
                        )
                    {

                        if (targetObject.heldObject.Value != null && targetObject.MinutesUntilReady == 0)
                        {

                            StardewValley.Item objectInstance = targetObject.heldObject.Value.getOne();

                            if(targetObject.QualifiedItemId == "(BC)MushroomLog")
                            {

                                objectInstance.Quality = 4;

                            }

                            objectInstance.Stack = targetObject.heldObject.Value.Stack;

                            if (
                                currentLocation.Name == Game1.player.currentLocation.Name &&
                                Vector2.Distance(Game1.player.Position, Position) <= 640
                            )
                            {
                                ThrowHandle throwItem = new(Game1.player, Position, objectInstance);

                                Mod.instance.throwRegister.Add(throwItem);

                            }
                            else
                            if (chest.addItem(objectInstance) != null)
                            {

                                return;

                            }

                            targetObject.heldObject.Value = null;

                            targetObject.MinutesUntilReady = 0;

                            targetObject.performDropDownAction(Game1.player);

                        }

                        return;

                    }

                }

            }

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.questShadowtin))
            {

                return false;

            }

            ResetActives();

            netSpecial.Set((int)specials.launch);

            specialTimer = 90;

            cooldownTimer = cooldownInterval*2;

            LookAtTarget(monster.Position, true);

            SpellHandle fireball = new(Game1.player, monster.Position, 384, Mod.instance.CombatDamage() * 3);

            fireball.origin = GetBoundingBox().Center.ToVector2();

            fireball.counter = -30;

            fireball.type = SpellHandle.spells.missile;

            fireball.missile = MissileHandle.missiles.rocket;

            fireball.display = IconData.impacts.impact;

            fireball.indicator = IconData.cursors.scope;

            fireball.factor =3;

            fireball.scheme = IconData.schemes.stars;

            fireball.sound = SpellHandle.sounds.explosion;

            fireball.added = new() { SpellHandle.effects.embers, };

            fireball.power = 4;

            fireball.explosion = 4;

            fireball.terrain = 4;

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

    }

}
