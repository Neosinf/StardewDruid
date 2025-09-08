using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.GameData.Machines;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using static StardewDruid.Character.Character;

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

            WeaponLoadout(WeaponRender.weapons.carnyx);

            weaponRender.LoadWeapon(WeaponRender.weapons.bazooka);

            restSet = true;

        }

        public override void DrawRest(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            base.DrawRest(b, spritePosition, drawLayer+0.0032f, fade);

        }

        public override void NewDay()
        {
            ChestHandle.RetrieveInventory(ChestHandle.chests.Companions);

            List<StardewValley.Item> marmalade = new();

            StardewValley.Object preserveJar = ItemRegistry.Create<StardewValley.Object>("(BC)15");

            MachineData machineData = preserveJar.GetMachineData();

            GameLocation farm = Game1.getFarm();

            MachineOutputRule outputRule;

            int? overrideMinutesUntilReady;

            for (int i = Mod.instance.chests[ChestHandle.chests.Companions].Items.Count - 1; i >= 0; i--)
            {

                Item getItem = Mod.instance.chests[ChestHandle.chests.Companions].Items.ElementAt(i);

                if (getItem is StardewValley.Object && getItem.Category == -79)
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

                    Mod.instance.chests[ChestHandle.chests.Companions].Items.RemoveAt(i);

                }

            }

            foreach (StardewValley.Item conversion in marmalade)
            {

                Mod.instance.chests[ChestHandle.chests.Companions].addItem(conversion);

            }

        }

        public override void ConnectSweep()
        {

            SpellHandle swipeEffect = new(Game1.player, Position, 192, Mod.instance.CombatDamage() / 2)
            {
                instant = true,

                added = new() { SpellHandle.Effects.knock, },

                sound = SpellHandle.Sounds.swordswipe
            };

            Mod.instance.spellRegister.Add(swipeEffect);

        }

        public override bool TargetWork()
        {
            ChestHandle.RetrieveInventory(ChestHandle.chests.Companions);

            List<Vector2> objectVectors = new List<Vector2>();

            if (currentLocation.objects.Count() == 0)
            {

                return false; //break;

            }

            for (int i = 0; i < 6; i++)
            {

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

                            LookAtTarget(objectVector * 64, true);

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

            if (specialTimer != 30)
            {
                return;

            }

            if (!currentLocation.objects.ContainsKey(workVector))
            {

                return;

            }

            Chest chest = Mod.instance.chests[ChestHandle.chests.Companions];

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

                    targetObject.heldObject.Set(null);

                    targetObject.MinutesUntilReady = 0;

                    targetObject.performDropDownAction(Game1.player);

                }

                return;

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

            SetCooldown(specialTimer, 2f);

            LookAtTarget(monster.Position, true);

            SpellHandle fireball = new(Game1.player, monster.Position, 384, Mod.instance.CombatDamage() * 3)
            {
                origin = GetBoundingBox().Center.ToVector2(),

                counter = -30,

                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.rocket,

                display = IconData.impacts.bigimpact,

                indicator = IconData.cursors.scope,

                displayFactor = 3,

                scheme = IconData.schemes.stars,

                sound = SpellHandle.Sounds.explosion,

                effectRadius = 5,

                added = new() { SpellHandle.Effects.explode, SpellHandle.Effects.reave, SpellHandle.Effects.embers, },

            };

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

    }

}
