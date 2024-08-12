using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using static StardewValley.Minigames.TargetGame;


namespace StardewDruid.Character
{
    public class Shadowtin : StardewDruid.Character.Character
    {

        public Dictionary<string,Dictionary<Vector2,int>> workVectors = new();

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

            WeaponLoadout();

            weaponRender.LoadWeapon(WeaponRender.weapons.carnyx);

            weaponRender.LoadWeapon(WeaponRender.weapons.bazooka);

            specialFrames[specials.launch] = CharacterRender.WeaponLaunch();

            idleFrames[idles.standby] = new(specialFrames[specials.sweep]);

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

            weaponRender.DrawWeapon(b, dashVector - new Vector2(16)*setScale, drawLayer, new() { scale = setScale, source = dashTangle, flipped = SpriteFlip() });

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

        public override void DrawLaunch(SpriteBatch b, Vector2 localPosition, float drawLayer, float fade)
        {

            Vector2 launchVector = SpritePosition(localPosition);

            Rectangle launchFrame = specialFrames[specials.launch][netDirection.Value][specialFrame];

            b.Draw(
                characterTexture,
                launchVector,
                launchFrame,
                Color.White * fade,
                0.0f,
                new Vector2(16),
                4f,
                SpriteFlip() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawShadow(b, localPosition, drawLayer);

            weaponRender.DrawFirearm(b, launchVector - new Vector2(16) * setScale, drawLayer, new() { scale = 4f, source = launchFrame, flipped = SpriteFlip() });

        }

        public override void NewDay()
        {

            workVectors = new();

            CharacterHandle.RetrieveInventory(characterType);

            List<StardewValley.Object> marmalade = new();

            for (int i = Mod.instance.chests[characterType].Items.Count - 1; i >= 0; i--)
            {

                StardewValley.Object @object = ItemRegistry.Create<StardewValley.Object>(Mod.instance.chests[characterType].Items[i].QualifiedItemId);

                if (@object.Category == -79)
                {

                    StardewValley.Object conversion = new StardewValley.Object("344", 1);

                    conversion.name = @object.Name + " Marmalade";
                    
                    conversion.Price = 50 + @object.Price * 2;
                    
                    conversion.preserve.Value = StardewValley.Object.PreserveType.Jelly;
                    
                    conversion.preservedParentSheetIndex.Value = @object.ParentSheetIndex.ToString();

                    conversion.Stack = Mod.instance.chests[characterType].Items[i].Stack;

                    marmalade.Add(conversion);

                    Mod.instance.chests[characterType].Items.RemoveAt(i);

                }

            }

            foreach (StardewValley.Object conversion in marmalade)
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

            if (!Mod.instance.chests.ContainsKey(CharacterHandle.characters.Shadowtin))
            {

                Mod.instance.chests[CharacterHandle.characters.Shadowtin] = new();

            }

            Chest chest = Mod.instance.chests[CharacterHandle.characters.Shadowtin];

            if (!workVectors.ContainsKey(currentLocation.Name))
            {

                workVectors[currentLocation.Name] = new();

            }


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

                        if (workVectors[currentLocation.Name].ContainsKey(objectVector))
                        {

                            continue;

                        }

                        workVectors[currentLocation.Name][objectVector] = 1;

                        StardewValley.Object targetObject = currentLocation.objects[objectVector];

                        if (ValidWorkTarget(targetObject))
                        {

                            workVector = objectVector;

                            Mod.instance.iconData.AnimateQuickWarp(currentLocation, Position, true);

                            Position = (workVector * 64);

                            SettleOccupied();

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

            ResetActives();

            netSpecial.Set((int)specials.launch);

            specialTimer = 90;

            cooldownTimer = cooldownInterval;

            LookAtTarget(monster.Position, true);

            SpellHandle fireball = new(Game1.player, monster.Position, 384, Mod.instance.CombatDamage() / 2);

            fireball.origin = GetBoundingBox().Center.ToVector2();

            fireball.counter = -30;

            fireball.type = SpellHandle.spells.ballistic;

            fireball.missile = IconData.missiles.fireball;

            fireball.display = IconData.impacts.impact;

            fireball.indicator = IconData.cursors.scope;

            fireball.projectile = 3;

            fireball.scheme = IconData.schemes.ether;

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
