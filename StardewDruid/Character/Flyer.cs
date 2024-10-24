
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using StardewValley.Minigames;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewValley.Objects;
using StardewDruid.Journal;
using System.Linq;
using StardewDruid.Render;
using static StardewDruid.Cast.Rite;
using StardewValley.Monsters;
using static StardewDruid.Data.SpawnData;



namespace StardewDruid.Character
{
    public class Flyer : StardewDruid.Character.Character
    {

        public bool liftOff;

        public bool circling;

        public List<StardewValley.Item> carryItems = new();

        public bool pat;

        public Flyer()
        {
        }

        public Flyer(CharacterHandle.characters type = CharacterHandle.characters.ShadowCrow)
          : base(type)
        {

        }

        public override void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.CharacterType(Name);

            }

            characterTexture = CharacterHandle.CharacterTexture(characterType);

            LoadIntervals();

            if (Mod.instance.questHandle.IsComplete(QuestHandle.bonesOne))
            {

                cooldownInterval = 225;

            }

            warpDisplay = IconData.warps.corvids;

            moveInterval = 12;

            dashInterval = 9;

            specialIntervals[specials.sweep] = 7;

            overhead = 112;

            setScale = 3.75f;

            gait = 3.6f;

            modeActive = mode.random;

            idleFrames = FlyerRender.FlyerIdle();

            specialFrames = FlyerRender.FlyerSpecial();

            specialIntervals[specials.special] = 12;

            specialIntervals[specials.sweep] = 15;

            specialCeilings[specials.special] = 5;

            specialCeilings[specials.sweep] = 3;

            specialFloors[specials.special] = 0;

            specialFloors[specials.sweep] = 0;

            walkFrames = FlyerRender.FlyerWalk();

            dashFrames = FlyerRender.FlyerDash();

            loadedOut = true;
        }

        public override Vector2 SpritePosition(Vector2 localPosition)
        {

            return localPosition + new Vector2(32, 64 - (int)(24 * setScale));

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {

            if (netDash.Value != 0 || netSceneActive.Value)
            {

                return;

            }

            base.draw(b, alpha);

        }
        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {

            if (netDash.Value != 0 || netSceneActive.Value)
            {

                base.draw(b, 1f);

            }

            base.drawAboveAlwaysFrontLayer(b);

        }

        public override void DrawSweep(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            Rectangle useFrame = specialFrames[(specials)netSpecial.Value][netDirection.Value][specialFrame];

            b.Draw(
                characterTexture,
                spritePosition,
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

        public override bool SpriteAngle()
        {

            return SpriteFlip();

        }

        public override void DrawShadow(SpriteBatch b, Vector2 spritePosition, float drawLayer)
        {

            float shadowRatio = 0.6f;

            Vector2 shadowPosition = spritePosition + new Vector2(0, setScale * 12);

            if(netDash.Value != 0)
            {

                shadowPosition.Y += dashHeight;

                shadowRatio = 0.55f;

            }
            else
            if(moveFrame > 0)
            {

                shadowPosition.Y += 48;

                shadowRatio = 0.5f;

            }

            if (netDirection.Value % 2 == 1)
            {
                shadowPosition.Y += 4;
            }

            b.Draw(
                Mod.instance.iconData.cursorTexture, 
                shadowPosition, 
                Mod.instance.iconData.shadowRectangle, 
                Color.White * 0.35f, 
                0.0f, 
                new Vector2(24), 
                setScale * shadowRatio, 
                0, 
                drawLayer - 0.001f
            );

        }

        public override Rectangle GetBoundingBox()
        {

            if (netDirection.Value % 2 == 0)
            {

                return new Rectangle((int)Position.X + 8, (int)Position.Y + 8, 48, 48);

            }

            return new Rectangle((int)Position.X - 16, (int)Position.Y + 8, 96, 48);

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {

            if (!Mod.instance.rite.specialCasts.ContainsKey("corvid" + characterType.ToString()) && who.CurrentItem != null)
            {
                switch (who.CurrentItem.ItemId)
                {

                    case "770":
                    case "MixedFlowerSeeds":
                    case "495":
                    case "496":
                    case "497":
                    case "498":
                    case "684":

                        who.Items.ReduceId(who.CurrentItem.ItemId, 1);

                        Mod.instance.rite.specialCasts["corvid" + characterType.ToString()] = new();

                        StardewValley.Object dropItem = new(SpawnData.RandomForageFrom(Game1.getLocationFromName("Forest")),Mod.instance.randomIndex.Next(1,3));

                        Game1.createItemDebris(dropItem, Position + new Vector2(0, 32), 2, currentLocation, -1);

                        doEmote(20);

                        return false;

                }

            }

            if (!pat && !IsEmoting)
            {

                currentLocation.playSound(SpellHandle.sounds.crow.ToString());

                doEmote(32);

                pat = true;

            }

            return false;

        }

        public override bool TrackToClose(int close = 128)
        {

            return false;

        }

        public override bool TrackToFar(int limit = 960, int nodeLimit = 20)
        {

            return false;

        }

        public override bool PathPlayer()
        {

            Vector2 target = Mod.instance.trackers[characterType].TrackPosition();

            if(Mod.instance.trackers[characterType].linger > 0 && Vector2.Distance(target,Position) >= 128)
            {

                Vector2 offset = ModUtility.DirectionAsVector(trackQuadrant) * 128;

                Vector2 tryPosition = target + offset;

                List<Vector2> open = ModUtility.GetOccupiableTilesNearby(currentLocation, ModUtility.PositionToTile(tryPosition), -1, 2, 0);

                if (open.Count > 0)
                {
                    
                    Vector2 tryPath = open[Mod.instance.randomIndex.Next(open.Count)];

                    traversal[tryPath] = 1;

                    destination = traversal.Keys.Last();

                    Mod.instance.trackers[characterType].nodes.Clear();

                    followTimer = 180 + (60 * Mod.instance.randomIndex.Next(5));

                    pathActive = pathing.player;

                    return true;

                }

            }

            return CircleAround(target);

        }

        public bool CircleAround(Vector2 target)
        {

            traversal.Clear();

            List<Vector2> circleArounds = ModUtility.GetTilesWithinRadius(currentLocation, ModUtility.PositionToTile(target), 6 + Mod.instance.randomIndex.Next(3), false);

            List<Vector2> circleBacks = new();

            int quadrant = ModUtility.DirectionToTarget(Position, target)[2];

            int segment = (circleArounds.Count / 8);

            int upperLevel = segment * quadrant;

            int lowerLevel = segment * ((quadrant + 7) % 8);

            for (int i = 0; i < circleArounds.Count; i += 2)
            {
                    
                if (i > upperLevel && i < upperLevel + 6)
                {

                    continue;

                }

                if (i > lowerLevel && i < lowerLevel + 6)
                {

                    continue;

                }
                
                if (i < lowerLevel)
                {

                    circleBacks.Add(circleArounds[i]);
                    
                    continue;
                
                }

                traversal.Add(circleArounds[i], 0);

            }

            for(int i = 0;i < circleBacks.Count; i++)
            {

                traversal.Add(circleBacks[i], 0);

            }

            if (traversal.Count > 0)
            {

                destination = traversal.Keys.Last();

                pathActive = pathing.circling;

                return true;

            }

            return false;

        }

        public override bool PathTrack()
        {

            if (base.PathTrack())
            {

                return true;

            }

            return false;

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {
            
            return base.SmashAttack(monster);

        }

        public override void ConnectSweep()
        {

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.bonesOne))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.bonesOne, 1);

            }

            SpellHandle swipeEffect = new(Game1.player, Position, 192, Mod.instance.CombatDamage() / 2);

            swipeEffect.type = SpellHandle.spells.swipe;

            swipeEffect.added = new() { SpellHandle.effects.mug, };

            swipeEffect.sound = SpellHandle.sounds.crow;

            Mod.instance.spellRegister.Add(swipeEffect);

        }


        public override bool TargetWork()
        {

            if (carryItems.Count > 0)
            {
                
                ResetActives();

                if (PathTarget(Game1.player.Position, 2, 2))
                {

                    pathActive = pathing.player;

                    SetDash(Game1.player.Position, false);

                    Mod.instance.trackers[characterType].linger = 0;

                    cooldownTimer = cooldownInterval / 2;

                }

                pat = false;

                return true;
            
            }

            if (ValidVillagerTarget())
            {

                pat = false;

                return true;

            };

            if (ValidDebrisTarget())
            {

                return true;

            }

            return false;

        }

        public bool ValidVillagerTarget()
        {

            if (!Mod.instance.questHandle.IsGiven(QuestHandle.bonesThree))
            {
                
                return false;

            }

            foreach (NPC witness in ModUtility.GetFriendsInLocation(Game1.player.currentLocation, true))
            {

                float distance = Vector2.Distance(witness.Position, Position);

                if (distance < 256f)
                {

                    if (Mod.instance.Witnessed(ReactionData.reactions.corvid, witness))
                    {

                        continue;

                    }

                    workVector = ModUtility.PositionToTile(witness.Position);

                    netSpecial.Set((int)specials.sweep);

                    specialTimer = 60;

                    if (distance > 80)
                    {
                        Vector2 offset = ModUtility.DirectionAsVector(trackQuadrant) * 64;

                        if (PathTarget(witness.Position + offset, 2, 1))
                        {

                            SetDash(destination * 64, false);

                            specialTimer += 30;

                        }

                    }

                    return true;

                }

            }

            return false;

        }

        public bool ValidDebrisTarget()
        {

            if (!Mod.instance.questHandle.IsGiven(QuestHandle.bonesTwo))
            {
                
                return false;

            }

            List<Vector2> objectVectors = new List<Vector2>();

            for (int i = 0; i < 4; i++)
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

                        if (ValidWorkTarget(currentLocation.objects[objectVector]))
                        {

                            LookAtTarget(objectVector * 64, true);

                            workVector = objectVector;

                            netSpecial.Set((int)specials.special);

                            specialTimer = 60;

                            Vector2 workPosition = workVector * 64;

                            if (Vector2.Distance(workPosition, Position) > 80)
                            {

                                if (PathTarget(workPosition, 2, 1))
                                {

                                    SetDash(workPosition, false);

                                    specialTimer += 30;

                                }

                            }

                            return true;

                        }

                    }

                }

            }

            foreach (Debris debris in currentLocation.debris)
            {

                if (debris.Chunks.Count > 0)
                {

                    if (Vector2.Distance(debris.Chunks.First().position.Value, Position) <= 192)
                    {

                        switch (debris.debrisType.Value)
                        {

                            case Debris.DebrisType.ARCHAEOLOGY:
                            case Debris.DebrisType.OBJECT:
                            case Debris.DebrisType.RESOURCE:

                                workVector = ModUtility.PositionToTile(debris.Chunks.First().position.Value);

                                netSpecial.Set((int)specials.special);

                                specialTimer = 30;

                                return true;

                        }

                    }

                }

            }

            return false;

        }

        public bool ValidWorkTarget(StardewValley.Object targetObject)
        {

            if (targetObject.isForage())
            {

                return true;

            }

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.bonesTwo))
            {

                return false;

            }

            if (
                targetObject.IsBreakableStone() ||
                targetObject.IsTwig() ||
                targetObject.QualifiedItemId == "(O)169" ||
                targetObject.IsWeeds() ||
                targetObject.Name.Contains("SupplyCrate") ||
                targetObject is BreakableContainer breakableContainer ||
                targetObject.QualifiedItemId == "(O)590" ||
                targetObject.QualifiedItemId == "(O)SeedSpot"
            )
            {

                return true;

            }
            return false;

        }


        public override void PerformWork()
        {

            if (specialTimer == 30)
            {

                MidWorkFunction();
            
            }

            if(specialTimer == 5)
            {
                
                foreach (Debris debris in currentLocation.debris)
                {

                    if (debris.Chunks.Count > 0)
                    {
                        
                        if (Vector2.Distance(debris.Chunks.First().position.Value, Position) <= 192)
                        {
                            
                            switch (debris.debrisType.Value)
                            {

                                case Debris.DebrisType.ARCHAEOLOGY:
                                case Debris.DebrisType.OBJECT:
                                case Debris.DebrisType.RESOURCE:

                                    string debrisId = debris.itemId.Value;

                                    if (debris.item != null)
                                    {

                                        debrisId = debris.item.QualifiedItemId;

                                    }

                                    Item carryDebris = ItemRegistry.Create(debrisId);

                                    carryItems.Add(carryDebris);

                                    debris.item = null;

                                    debris.Chunks.Clear();

                                    break;

                            }

                        }

                    }

                }

            }

        }

        public virtual void MidWorkFunction()
        {

            foreach (NPC witness in ModUtility.GetFriendsInLocation(Game1.player.currentLocation, true))
            {

                float distance = Vector2.Distance(witness.Position, Position);

                if (distance < 256f)
                {

                    if (Mod.instance.Witnessed(ReactionData.reactions.corvid, witness))
                    {

                        continue;

                    }

                    int friendship = 15;

                    int friendshipBehaviour = Mod.instance.randomIndex.Next(2);

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.bonesThree))
                    {

                        friendshipBehaviour = 1;

                    }

                    switch (friendshipBehaviour)
                    {

                        case 0:

                            friendship = 0;

                            if (Game1.NPCGiftTastes.TryGetValue(witness.Name, out var giftValue))
                            {

                                string[] giftArray = giftValue.Split('/');

                                List<string> giftList = new();

                                string[] lovedGifts = ArgUtility.SplitBySpace(giftArray[1]);

                                for (int j = 0; j < lovedGifts.Length; j++)
                                {

                                    if (lovedGifts[j].Length > 0 && !lovedGifts[j].StartsWith('-'))
                                    {

                                        giftList.Add(lovedGifts[j]);

                                    }

                                }

                                string[] likedGifts = ArgUtility.SplitBySpace(giftArray[3]);

                                for (int j = 0; j < likedGifts.Length; j++)
                                {

                                    if (likedGifts[j].Length > 0 && !likedGifts[j].StartsWith('-'))
                                    {

                                        giftList.Add(likedGifts[j]);

                                    }

                                }

                                if (giftList.Count > 0)
                                {

                                    Item stealGift = ItemRegistry.Create(giftList[Mod.instance.randomIndex.Next(giftList.Count)],1,2,true);

                                    if (stealGift != null)
                                    {

                                        carryItems.Add(stealGift);

                                    }

                                }


                            }

                            break;

                        case 1:

                            friendship = 25;

                            break;

                    }

                    ModUtility.ChangeFriendship(Game1.player, witness, friendship);

                    ReactionData.ReactTo(witness, ReactionData.reactions.corvid, friendship);

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.bonesThree))
                    {

                        Mod.instance.questHandle.UpdateTask(QuestHandle.bonesThree, 1);

                    }

                    return;

                }

            }

            if (!currentLocation.objects.ContainsKey(workVector))
            {

                return;

            }

            if (SpawnData.ForageCheck(currentLocation.objects[workVector]))
            {

                StardewValley.Item objectInstance = ModUtility.ExtractForage(currentLocation, workVector, false);

                currentLocation.objects.Remove(workVector);

                carryItems.Add(objectInstance);

                return;

            }

            if(!Mod.instance.questHandle.IsComplete(QuestHandle.bonesTwo))
            {

                return;

            }

            SpellHandle explode = new(Game1.player, workVector * 64, 128, -1);

            explode.type = SpellHandle.spells.explode;

            explode.indicator = IconData.cursors.none;

            if (currentLocation.objects[workVector].IsBreakableStone())
            {

                explode.sound = SpellHandle.sounds.hammer;

            }

            explode.power = 2;

            explode.terrain = 2;

            Mod.instance.spellRegister.Add(explode);

        }

        public override void DashAscension()
        {

            if(pathProgress > 1 && carryItems.Count > 0 && pathActive == pathing.player)
            {

                foreach(Item item in carryItems)
                {
                    
                    ThrowHandle throwItem = new(Game1.player, Position, item);

                    Mod.instance.throwRegister.Add(throwItem);

                }

                if (!Mod.instance.questHandle.IsComplete(QuestHandle.bonesTwo))
                {

                    Mod.instance.questHandle.UpdateTask(QuestHandle.bonesTwo, 1);

                }

                carryItems.Clear();

            }

            base.DashAscension();


        }

    }

}
