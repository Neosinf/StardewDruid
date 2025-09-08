using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Ether;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Buildings;
using StardewValley.Characters;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Objects;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace StardewDruid.Character
{
    public class Jester : Critter
    {

        public int laserCooldown = 0;

        public Jester()
        {
        }

        public Jester(CharacterHandle.characters characterType = CharacterHandle.characters.Jester)
          : base(characterType)
        {
            
        }

        public override void LoadOut()
        {
            
            base.LoadOut();

            restSet = true;

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.questJester))
            {

                return false;

            }

            if(laserCooldown > 0)
            {

                return false;

            }

            ResetActives();

            netSpecial.Set((int)specials.special);

            specialTimer = 90;

            SetCooldown(specialTimer, 1.5f);

            laserCooldown = 600;

            LookAtTarget(monster.Position, true);

            SpellHandle beam = new(Game1.player, monster.Position, 320, Mod.instance.CombatDamage())
            {
                origin = Position,

                type = SpellHandle.Spells.beam,

                display = IconData.impacts.flasher,

                sound = SpellHandle.Sounds.explosion
            };

            Mod.instance.spellRegister.Add(beam);

            return true;

        }

        public override void normalUpdate(GameTime time, GameLocation location)
        {

            base.normalUpdate(time, location);

            if (laserCooldown > 0)
            {

                laserCooldown--;

            }

        }

        public override void CompanionBuff(Farmer player)
        {

            BuffEffects buffEffect = new();

            buffEffect.LuckLevel.Set(1);

            Buff luckBuff = new(
                Rite.buffIdJester, 
                source: StringData.Get(StringData.str.jesterBuff), 
                displaySource: StringData.Get(StringData.str.jesterBuff), 
                duration: Buff.ENDLESS, 
                displayName: StringData.Get(StringData.str.jesterBuffTitle), 
                description: StringData.Get(StringData.str.jesterBuffDescription), 
                effects: buffEffect
                );

            player.buffs.Apply(luckBuff);

        }

        public override void RemoveCompanionBuff(Farmer player)
        {
            
            if (player.buffs.IsApplied(Rite.buffIdJester))
            {

                player.buffs.Remove(Rite.buffIdJester);

            }

        }
        public override List<StardewValley.Monsters.Monster> FindMonsters()
        {

            return ModUtility.MonsterProximity(currentLocation, Position, 960f, true);

        }

        public override void ConnectSweep()
        {

            SpellHandle swipeEffect = new(Game1.player, Position, 192, Mod.instance.CombatDamage() / 2)
            {
                instant = true,

                sound = SpellHandle.Sounds.swordswipe,

                display = IconData.impacts.flashbang
            };

            if (Mod.instance.questHandle.IsGiven(QuestHandle.fatesTwo))
            {
                switch (Mod.instance.randomIndex.Next(6))
                {
                    case 0:

                        swipeEffect.added.Add(SpellHandle.Effects.daze);
                        break;

                    case 1:

                        swipeEffect.added.Add(SpellHandle.Effects.glare);
                        break;

                    case 2:

                        swipeEffect.added.Add(SpellHandle.Effects.morph);
                        break;

                    case 3:

                        swipeEffect.added.Add(SpellHandle.Effects.doom);
                        break;
                }
            }
            else
            {
                swipeEffect.added.Add(SpellHandle.Effects.push);

            }

            Mod.instance.spellRegister.Add(swipeEffect);

        }

        public override List<Vector2> RoamAnalysis()
        {

            List<Vector2> collection = base.RoamAnalysis();

            if (Game1.currentSeason == "winter")
            {

                return collection;

            }

            List<Vector2> scarelist = new List<Vector2>();

            if (currentLocation is Farm farm)
            {

                for (int i = 0; i < 2; i++)
                {

                    foreach (Building building in farm.buildings)
                    {

                        if (building.buildingType.Contains("Coop") || building.buildingType.Contains("Barn"))
                        {

                            Vector2 scareVector = new(building.tileX.Value * 64f, building.tileY.Value * 64f);

                            scarelist.Add(scareVector);

                            scarelist.Add(new Vector2(-1f));

                        }

                    }

                }

            }

            scarelist.AddRange(collection);

            return scarelist;

        }

        public override bool TargetWork()
        {
            ChestHandle.RetrieveInventory(ChestHandle.chests.Companions);

            if (currentLocation.characters.Count > 0 && Mod.instance.activeEvent.Count == 0)
            {

                foreach (NPC rubVictim in currentLocation.characters)
                {

                    if (rubVictim is StardewValley.Monsters.Monster)
                    {
                        continue;
                    }

                    if (rubVictim is StardewDruid.Character.Character)
                    {
                        continue;
                    }

                    if (rubVictim is Dragon)
                    {
                        continue;
                    }

                    if (rubVictim is Pet)
                    {
                        continue;
                    }

                    if (!Game1.NPCGiftTastes.ContainsKey(rubVictim.Name))
                    {
                        continue;
                    }

                    if (Mod.instance.Witnessed(ReactionData.reactions.jester, rubVictim))
                    {
                        continue;
                    }

                    if (Vector2.Distance(rubVictim.Position, Position) < 480)
                    {

                        if (ModUtility.GroundCheck(currentLocation, rubVictim.Tile - new Vector2(1, 0), true) != "ground")
                        {

                            continue;

                        }

                        if (Game1.player.friendshipData.ContainsKey(rubVictim.Name))
                        {

                            Game1.player.friendshipData[rubVictim.Name].TalkedToToday = true;

                            ModUtility.ChangeFriendship(rubVictim,25);

                        }

                        if (!currentLocation.IsFarm)
                        {

                            if (rubVictim.isMoving())
                            {
                                
                                rubVictim.Halt();

                            }

                        }

                        ReactionData.ReactTo(rubVictim, ReactionData.reactions.jester, 0, new());

                        ResetActives();

                        Position = rubVictim.Position - new Vector2(64, 0);

                        Mod.instance.iconData.AnimateQuickWarp(currentLocation, Position);

                        workVector = rubVictim.Position;

                        netSpecial.Set((int)specials.greet);

                        specialTimer = 120;

                        return true;

                    }

                }

            }

            List<FarmAnimal> victims = new();

            if (currentLocation is Farm farmLocation)
            {

                foreach (KeyValuePair<long, FarmAnimal> pair in farmLocation.animals.Pairs)
                {

                    if (Vector2.Distance(pair.Value.Position, Position) >= 480)
                    {

                        continue;

                    }

                    if (ModUtility.GroundCheck(currentLocation, pair.Value.Tile - new Vector2(1, 0), true) != "ground")
                    {

                        continue;

                    }

                    victims.Add(pair.Value);

                }

            }

            if (currentLocation is AnimalHouse animalLocation)
            {

                foreach (KeyValuePair<long, FarmAnimal> pair in animalLocation.animals.Pairs)
                {

                    if (Vector2.Distance(pair.Value.Position, Position) >= 480)
                    {

                        continue;

                    }

                    if (ModUtility.GroundCheck(currentLocation, pair.Value.Tile - new Vector2(1, 0), true) != "ground")
                    {

                        continue;

                    }

                    victims.Add(pair.Value);

                }

            }

            foreach(FarmAnimal victim in victims)
            {

                if(victim is null)
                {

                    continue;

                }

                bool milk = false;

                bool shear = false;

                if (victim.isAdult())
                {

                    if(Mod.instance.virtualPail != null)
                    {

                        if (victim.CanGetProduceWithTool(Mod.instance.virtualPail))
                        {

                            if (victim.currentProduce.Value != null)
                            {

                                milk = true;

                            }

                        }

                    }
                    
                    if (Mod.instance.virtualPail != null)
                    {
                        
                        if (victim.CanGetProduceWithTool(Mod.instance.virtualShears))
                        {

                            if (victim.currentProduce.Value != null)
                            {

                                shear = true;

                            }

                        }

                    }

                }

                if (!milk && victim.wasPet.Value)
                {

                    continue;

                }

                if (milk)
                {

                    MilkRub(victim);

                }

                if (shear)
                {

                    MilkRub(victim,false);

                }

                FarmRub(victim);

                return true;


            }

            return false;

        }

        public void MilkRub(FarmAnimal cow, bool milk = true)
        {

            ParsedItemData dataOrErrorItem;

            if (milk)
            {

                dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(Mod.instance.virtualPail.QualifiedItemId);

                playNearbySoundLocal("Milking");

            }
            else
            {
                
                dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(Mod.instance.virtualShears.QualifiedItemId);
            
                playNearbySoundLocal("scissors");
            
            }

            Chest chest = Mod.instance.chests[ChestHandle.chests.Companions];

            StardewValley.Object @object = ItemRegistry.Create<StardewValley.Object>("(O)" + cow.currentProduce.Value);

            @object.CanBeSetDown = false;

            @object.Quality = cow.produceQuality.Value;

            if (cow.hasEatenAnimalCracker.Value)
            {
                @object.Stack = 2;
            }

            if (
                currentLocation.Name == Game1.player.currentLocation.Name &&
                Vector2.Distance(Game1.player.Position, cow.Position) <= 640
            )
            {
                ThrowHandle throwItem = new(Game1.player, cow.Position, @object);

                Mod.instance.throwRegister.Add(throwItem);

            }
            else
            if (chest.addItem(@object) != null)
            {

                ThrowHandle throwItem = new(Game1.player, cow.Position, @object);

                Mod.instance.throwRegister.Add(throwItem);

            }

            cow.HandleStatsOnProduceCollected(@object);

            cow.currentProduce.Value = null;

            cow.ReloadTextureIfNeeded();

            Mod.instance.GiveExperience(0, 5);

            Rectangle bottleRectangle = dataOrErrorItem.GetSourceRect();

            TemporaryAnimatedSprite bottleSprite = new(0, 900, 1, 1, cow.Position + new Vector2(2, 52), false, false)
            {

                sourceRect = bottleRectangle,

                sourceRectStartingPos = new Vector2(bottleRectangle.X, bottleRectangle.Y),

                texture = dataOrErrorItem.GetTexture(),

                scale = 3f,

                layerDepth = (cow.Position.Y + 128) / 10000,

            };

            currentLocation.temporarySprites.Add(bottleSprite);

        }

        public void FarmRub(FarmAnimal victim)
        {

            victim.Halt();

            victim.faceDirection(2);

            ModUtility.PetAnimal(victim);

            if (victim.Sprite.SpriteWidth > 16)
            {

                Position = victim.Position + new Vector2(-32, 32);

            }
            else
            {

                Position = victim.Position - new Vector2(64, 0);

            }

            ResetActives();

            SettleOccupied();

            Mod.instance.iconData.AnimateQuickWarp(currentLocation, Position);

            workVector = victim.Position;

            netSpecial.Set((int)specials.greet);

            specialTimer = 120;

        }


    }

}
