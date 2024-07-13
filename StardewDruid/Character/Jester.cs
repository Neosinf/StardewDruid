using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Ether;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Event;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Buildings;
using StardewValley.Characters;
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

            idleFrames = new()
            {
                [0] = new(){
                    new(0, 256, 32, 32),
                    new(32, 256, 32, 32),
                    new(64, 256, 32, 32),
                    new(96, 256, 32, 32),
                    //new(128, 256, 32, 32),
                    //new(160, 256, 32, 32),
                },

            };

            specialFrames = new()
            {

                [0] = new() { new(192, 192, 32, 32), },

                [1] = new() { new(192, 160, 32, 32), },

                [2] = new() { new(192, 128, 32, 32), },

                [3] = new() { new(192, 224, 32, 32), },

            };

            workFrames = new()
            {

                [0] = new() {new(0, 288, 32, 32),
                    new(32, 288, 32, 32),
                    new(64, 288, 32, 32),
                    new(32, 288, 32, 32),
                },

            };

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            if (!Mod.instance.questHandle.IsComplete(Journal.QuestHandle.questJester))
            {

                return false;

            }

            ResetActives();

            netSpecialActive.Set(true);

            specialTimer = 60;

            cooldownTimer = cooldownInterval;

            LookAtTarget(monster.Position, true);

            SpellHandle beam = new(Game1.player,new() { monster, },Mod.instance.CombatDamage() / 2);

            beam.origin = Position;

            beam.type = SpellHandle.spells.beam;

            beam.scheme = IconData.schemes.Void;

            Mod.instance.spellRegister.Add(beam);

            return true;

        }

        public override void CompanionBuff(Farmer player)
        {

            BuffEffects buffEffect = new();

            buffEffect.LuckLevel.Set(1);

            Buff luckBuff = new("184656", source: "Jester of Fate", displaySource: "Jester of Fate", duration: Buff.ENDLESS, displayName: "Jester's Luck", description: "Luck increased by companion", effects: buffEffect);

            player.buffs.Apply(luckBuff);

        }

        public override void RemoveCompanionBuff(Farmer player)
        {
            
            if (player.buffs.IsApplied("184656"))
            {

                player.buffs.Remove("184656");

            }

        }

        public override void ConnectSweep()
        {
                
            SpellHandle swipeEffect = new(Game1.player, Position, 192, Mod.instance.CombatDamage() / 2);

            swipeEffect.instant = true;

            swipeEffect.sound = SpellHandle.sounds.swordswipe;

            swipeEffect.display = IconData.impacts.skull;

            if (Mod.instance.questHandle.IsGiven(Journal.QuestHandle.fatesTwo))
            {
                switch (Mod.instance.randomIndex.Next(4))
                {
                    case 0:

                        swipeEffect.added.Add(SpellHandle.effects.daze);
                        break;

                    case 1:

                        swipeEffect.added.Add(SpellHandle.effects.mug);
                        break;

                    case 2:

                        swipeEffect.added.Add(SpellHandle.effects.morph);
                        break;

                    default:
                    case 3:

                        swipeEffect.added.Add(SpellHandle.effects.doom);
                        break;
                }
            }
            else
            {
                swipeEffect.added.Add(SpellHandle.effects.push);

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

            if (!Mod.instance.chests.ContainsKey(CharacterHandle.characters.Jester))
            {

                Mod.instance.chests[CharacterHandle.characters.Jester] = new();

            }

            if (currentLocation.characters.Count > 0 && !Mod.instance.eventRegister.ContainsKey("active"))
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

                        bool alreadyRubbed = Game1.player.hasPlayerTalkedToNPC(rubVictim.Name);

                        if (alreadyRubbed)
                        {

                            continue;

                        }

                        if (ModUtility.GroundCheck(currentLocation, rubVictim.Tile - new Vector2(1, 0), true) != "ground")
                        {

                            continue;

                        }

                        if (Game1.player.friendshipData.ContainsKey(rubVictim.Name))
                        {

                            Game1.player.friendshipData[rubVictim.Name].TalkedToToday = true;

                            Game1.player.changeFriendship(25, rubVictim);

                        }

                        if (!currentLocation.IsFarm)
                        {
                            
                            rubVictim.Halt();

                            rubVictim.faceDirection(2);

                        }

                        ReactionData.ReactTo(rubVictim, ReactionData.reactions.jester);

                        Position = rubVictim.Position - new Vector2(64, 0);

                        SettleOccupied();

                        Mod.instance.iconData.AnimateQuickWarp(currentLocation, Position);

                        workVector = rubVictim.Position;

                        netWorkActive.Set(true);

                        netSpecialActive.Set(true);

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

                bool milk = false;

                if (victim.isAdult())
                {

                    if (victim.CanGetProduceWithTool(Mod.instance.virtualPail))
                    {

                        if (victim.currentProduce.Value != null)
                        {

                            milk = true;

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

                FarmRub(victim);

                return true;


            }

            return false;

        }

        public void MilkRub(FarmAnimal cow)
        {

            playNearbySoundLocal("Milking");

            Chest chest = Mod.instance.chests[CharacterHandle.characters.Jester];

            StardewValley.Object @object = ItemRegistry.Create<StardewValley.Object>("(O)" + cow.currentProduce.Value);

            @object.CanBeSetDown = false;

            @object.Quality = cow.produceQuality.Value;

            if (cow.hasEatenAnimalCracker.Value)
            {
                @object.Stack = 2;
            }

            if (
                currentLocation.Name == Game1.player.currentLocation.Name &&
                Vector2.Distance(Game1.player.Position, Position) <= 640
            )
            {
                ThrowHandle throwItem = new(Game1.player, Position, @object);

                Mod.instance.throwRegister.Add(throwItem);

            }
            else
            if (chest.addItem(@object) != null)
            {

                ThrowHandle throwItem = new(Game1.player, Position, @object);

                Mod.instance.throwRegister.Add(throwItem);

            }

            Utility.RecordAnimalProduce(cow, cow.currentProduce.Value);

            cow.currentProduce.Value = null;

            cow.ReloadTextureIfNeeded();

            Game1.player.gainExperience(0, 5);

            Microsoft.Xna.Framework.Rectangle bottleRectangle = IconData.RelicRectangles(IconData.relics.bottle);

            TemporaryAnimatedSprite bottleSprite = new(0, 900, 1, 1, cow.Position + new Vector2(2, 52), false, false)
            {

                sourceRect = bottleRectangle,

                sourceRectStartingPos = new Vector2(bottleRectangle.X, bottleRectangle.Y),

                texture = Mod.instance.iconData.relicsTexture,

                scale = 3f,

                layerDepth = (cow.Position.Y + 128) / 10000,

            };

            currentLocation.temporarySprites.Add(bottleSprite);

        }

        public void FarmRub(FarmAnimal victim)
        {

            victim.Halt();

            victim.faceDirection(2);

            ModUtility.PetAnimal(Game1.player, victim);

            if (victim.Sprite.SpriteWidth > 16)
            {

                Position = victim.Position + new Vector2(-32, 32);

            }
            else
            {

                Position = victim.Position - new Vector2(64, 0);

            }


            SettleOccupied();

            Mod.instance.iconData.AnimateQuickWarp(currentLocation, Position);

            workVector = victim.Position;

            netWorkActive.Set(true);

            netSpecialActive.Set(true);

            specialTimer = 120;

        }


    }

}
