using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Event;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Monsters;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace StardewDruid.Character
{
    public class Character : NPC
    {

        public Texture2D characterTexture;
        public List<Vector2> targetVectors;
        public float gait;
        public int opponentThreshold;
        
        public List<Vector2> roamVectors;
        public int roamIndex;
        public double roamLapse;
        public bool roamSummon;
        public Vector2 summonVector;

        public Dictionary<int,Vector2> eventVectors;
        public List<Vector2> backTrack;
        public int eventIndex;
        public string eventName;
        public List<Event.BarrageHandle> barrages;
        public bool loadedOut;

        public enum mode
        {
            scene,
            track,
            standby,
            roam,
            random,
            sit,
        }

        public mode modeActive;

        public enum behaviour
        {
            still,
            idle,
            follow,
            hurry,
            dash,
            special,
            barrage,
            retreat,
        }

        public behaviour behaviourActive;

        public Dictionary<int, List<Rectangle>> walkFrames;
        public Dictionary<int, List<Rectangle>> dashFrames;
        public Dictionary<int, List<Rectangle>> idleFrames;
        public Dictionary<int,Rectangle> haltFrames;
        public Dictionary<int, List<Rectangle>> specialFrames;

        public int idleTimer;
        public int idleInterval;
        public int idleFrame;
        public int stationaryTimer;

        public int collideTimer;
        public int moveTimer;
        public int moveLength;
        public int moveInterval;
        public int moveFrame;

        public int dashFrame;
        public int dashInterval;
        public int dashFloor;
        public int dashCeiling;

        public int specialTimer;
        public int specialInterval;
        public int specialFrame;

        public int cooldownTimer;
        public int cooldownInterval;
        public int hitTimer;
        public int pushTimer;

        public int moveDirection;
        public int altDirection;
        public Vector2 setPosition;

        public string previousLocation;
        public Vector2 previousPosition;
        public mode previousMode;

        public NetInt netDirection = new NetInt(0);
        
        public NetInt netAlternative = new NetInt(0);

        public NetBool netSpecialActive = new NetBool(false);

        public NetBool netDashActive = new NetBool(false);

        public NetBool netHaltActive = new NetBool(false);

        public NetBool netFollowActive = new NetBool(false);

        public NetBool netStandbyActive = new NetBool(false);

        public NetBool netSceneActive = new NetBool(false);

        public Character()
        {
        }

        public Character(Vector2 position, string map, string Name)
          : base(new AnimatedSprite(Path.Combine("Characters","Abigail")), position, map, 2, Name, CharacterData.CharacterPortrait(Name), false)
        {

            willDestroyObjectsUnderfoot = false;
            
            DefaultMap = map;
            
            DefaultPosition = position;
            
            HideShadow = true;

            SimpleNonVillagerNPC = true;

            LoadOut();
        
        }

        protected override void initNetFields()
        {
            base.initNetFields();
            NetFields.AddField(netDirection);
            NetFields.AddField(netAlternative);
            NetFields.AddField(netSpecialActive);
            NetFields.AddField(netDashActive);
            NetFields.AddField(netHaltActive);
            NetFields.AddField(netFollowActive);
            NetFields.AddField(netStandbyActive);
            NetFields.AddField(netSceneActive);

        }

        public virtual void LoadBase()
        {

            barrages = new();

            roamVectors = new();

            backTrack = new();

            eventVectors = new();

            targetVectors = new();

            opponentThreshold = 640;

            gait = 1.6f;

            modeActive = mode.random;

            behaviourActive = behaviour.follow;
                
            idleInterval = 90;

            moveLength = 4;

            moveInterval = 12;

            dashInterval = 9;

            dashFloor = 1;

            dashCeiling = 4;

            specialInterval = 30;

            cooldownInterval = 180;

            haltFrames = new();

            idleFrames = new();

            specialFrames = new();

        }

        public virtual void LoadOut()
        {

            LoadBase();

            characterTexture = CharacterData.CharacterTexture(Name);

            haltFrames = new()
            {
                [0] = new(0, 64, 16, 32),
                [1] = new(0, 32, 16, 32),
                [2] = new(0, 0, 16, 32),
                [3] = new(0, 96, 16, 32),

            };

            walkFrames = WalkFrames(32, 16);

            dashFrames = walkFrames;

            loadedOut = true;

        }

        public virtual Dictionary<int, List<Rectangle>> WalkFrames(int height, int width, int startX = 0, int startY = 0)
        {

            Dictionary<int, List<Rectangle>> walkFrames = new();

            foreach (KeyValuePair<int, int> keyValuePair in new Dictionary<int, int>()
            {
                [0] = 2,
                [1] = 1,
                [2] = 0,
                [3] = 3
            })
            {
                
                walkFrames[keyValuePair.Key] = new List<Rectangle>();
                
                for (int index = 0; index < moveLength; index++)
                {
                    
                    Rectangle rectangle = new(startX, startY, width, height);
                    
                    rectangle.X += width * index;
                    
                    rectangle.Y += height * keyValuePair.Value;
                    
                    walkFrames[keyValuePair.Key].Add(rectangle);
                
                }

            }

            return walkFrames;

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                
                return;

            }

            Vector2 localPosition = getLocalPosition(Game1.viewport);

            float drawLayer = (float)StandingPixel.Y / 10000f;

            if (IsEmoting && !Game1.eventUp)
            {
                b.Draw(Game1.emoteSpriteSheet, localPosition - new Vector2(0, 160), new Microsoft.Xna.Framework.Rectangle(base.CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, base.CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16), Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, drawLayer);
            }

        }

        public override Rectangle GetBoundingBox()
        {
            
            return new Rectangle((int)Position.X + 8, (int)Position.Y+ 8, 48, 48);
        
        }

        public virtual Rectangle GetHitBox()
        {
            
            return GetBoundingBox();
        
        }

        public override void reloadSprite(bool onlyAppearance = false)
        {
            base.reloadSprite(onlyAppearance);
            Portrait = CharacterData.CharacterPortrait(Name);

        }

        public override void reloadData()
        {
            CharacterDisposition characterDisposition = CharacterData.CharacterDisposition(Name);
            Age = characterDisposition.Age;
            Manners = characterDisposition.Manners;
            SocialAnxiety = characterDisposition.SocialAnxiety;
            Optimism = characterDisposition.Optimism;
            Gender = characterDisposition.Gender;
            datable.Value = characterDisposition.datable;
            Birthday_Season = characterDisposition.Birthday_Season;
            Birthday_Day = characterDisposition.Birthday_Day;
            id = characterDisposition.id;
        }

        public override void reloadDefaultLocation()
        {
            DefaultMap = Mod.instance.CharacterMap(Name);
            if (DefaultMap == null)
            {
                DefaultMap = "FarmCave";
            }
            DefaultPosition = CharacterData.CharacterPosition(DefaultMap);
        }

        public override void receiveGift(StardewValley.Object o, Farmer giver, bool updateGiftLimitInfo = true, float friendshipChangeMultiplier = 1, bool showResponse = true)
        {

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {

            return true;

        }

        public override void Halt()
        {

            netHaltActive.Set(true);

        }

        public virtual void ResetActives()
        {
            
            behaviourActive = behaviour.follow;

            moveDirection = 2;

            netDirection.Set(2);

            altDirection = 1;

            netAlternative.Set(1);

            netHaltActive.Set(false);

            idleTimer = 0;

            idleFrame=(0);

            netDashActive.Set(false);

            moveTimer = 0;

            moveFrame=(0);

            netSpecialActive.Set(false);

            specialTimer = 0;

            specialFrame=(0);

            cooldownTimer = 0;

            hitTimer = 0;

            targetVectors.Clear();

        }

        public virtual void ResetAll()
        {

            idleFrame=(0);

            moveFrame=(0);

            specialFrame=(0);

            targetVectors.Clear();

        }

        public void NextTarget(Vector2 target, float span = 1.5f)
        {

            int moveDirection;

            int altDirection;

            Vector2 moveTarget = target;

            float distance = Vector2.Distance(Position, target);

            Vector2 difference = new(target.X - Position.X, target.Y - Position.Y);

            float absoluteX = Math.Abs(difference.X);

            float absoluteY = Math.Abs(difference.Y);

            int signX = difference.X < 0.001f ? -1 : 1;

            int signY = difference.Y < 0.001f ? -1 : 1;

            if (absoluteX > absoluteY)
            {

                moveDirection = 2 - signX;

                altDirection = 1 + signY;

            }
            else
            {
                moveDirection = 1 + signY;

                altDirection = 2 - signX;

            }

            if (distance >= 128.0 & span > 0)
            {

                float checkAhead = Math.Min(64f * span, distance);

                Vector2 checkTarget;

                if (absoluteX > absoluteY)
                {

                    float ratio = absoluteY / absoluteX;

                    float checkX = signX * checkAhead;

                    float checkY = ratio * signY * checkAhead;

                    checkTarget = new(checkX, checkY);

                }
                else
                {

                    float ratio = absoluteX / absoluteY;

                    float checkX = ratio * signX * checkAhead;

                    float checkY = signY * checkAhead;

                    checkTarget = new(checkX, checkY);

                }

                moveTarget = new Vector2(Position.X + checkTarget.X, Position.Y + checkTarget.Y);

            }

            netDirection.Set(moveDirection);

            netAlternative.Set(altDirection);

            targetVectors.Clear();

            targetVectors.Add(moveTarget);

        }

        public override void performTenMinuteUpdate(int timeOfDay, GameLocation l)
        {

        }

        public override void behaviorOnFarmerPushing()
        {
            
            if (netDashActive.Value || netSpecialActive.Value || netSceneActive.Value || netHaltActive.Value)
            {

                return;

            }

            if (Context.IsMainPlayer)
            {

                pushTimer++;

                pushTimer++;

                if(pushTimer > 5)
                {

                    TargetRandom(4);

                    pushTimer = 0;

                }

            }

        }

        public virtual void normalUpdate(GameTime time, GameLocation location)
        {

            if (!loadedOut)
            {
                LoadOut();
            }

            if (Context.IsMainPlayer)
            {

                if (shakeTimer > 0)
                {
                    shakeTimer = 0;
                }
                    
                if (textAboveHeadTimer > 0)
                {

                    if (textAboveHeadPreTimer > 0)
                    {
                    
                        textAboveHeadPreTimer -= time.ElapsedGameTime.Milliseconds;
                    
                    }
                    else
                    {
                    
                        textAboveHeadTimer -= time.ElapsedGameTime.Milliseconds;

                        if (textAboveHeadTimer > 500)
                        {
                        
                            textAboveHeadAlpha = Math.Min(1f, textAboveHeadAlpha + 0.1f);
                        
                        }
                        else
                        {
                        
                            textAboveHeadAlpha = Math.Max(0.0f, textAboveHeadAlpha - 0.04f);
                        
                        }
                            
                    
                    }
                }

                updateEmote(time);

            }

        }

        public override void update(GameTime time, GameLocation location)
        {

            normalUpdate(time, location);

            if (!Context.IsMainPlayer)
            {
                
                UpdateMultiplayer();

                return;

            }

            UpdateBehaviour();

            UpdateTarget();

            MoveTowardsTarget();

        }

        public virtual void UpdateMultiplayer()
        {
            
            if (netHaltActive.Value)
            {

                idleTimer++;

                if (idleTimer == idleInterval)
                {

                    idleFrame++;

                    if (idleFrame == haltFrames.Count)
                    {
                        idleFrame = 0;
                    }

                    idleTimer = 0;

                }

                return;

            }
            else
            {
                idleFrame = 0;

                idleTimer = 0;

            }

            if (netSpecialActive.Value)
            {

                specialTimer++;

                if (specialTimer == specialInterval)
                {

                    specialFrame++;

                    if (specialFrame == specialFrames.Count)
                    {
                        specialFrame = 0;
                    }

                    specialTimer = 0;

                }

                return;

            }
            else
            {
                specialFrame = 0;

                specialTimer = 0;

            }

            if (setPosition != Position || netDirection.Value != moveDirection || netAlternative.Value != altDirection)
            {

                setPosition = Position;

                moveDirection = netDirection.Value;

                altDirection = netAlternative.Value;

                moveTimer--;

                if (moveTimer <= 0)
                {

                    moveFrame++;

                    if (moveFrame >= moveLength)
                    {
                        moveFrame = 0;
                    }

                    moveTimer = moveInterval;

                    if (netDashActive.Value)
                    {

                        moveTimer = dashInterval;

                        dashFrame++;

                        if (dashFrame > dashCeiling)
                        {

                            dashFrame = dashFloor;

                        }

                    }

                    stationaryTimer = 30;

                }

                return;

            }

            if (stationaryTimer > 0)
            {

                stationaryTimer--;

                if (stationaryTimer == 0)
                {

                    moveFrame = 0;

                    moveTimer = 0;

                }

            }

        }

        public virtual void UpdateBehaviour()
        {

            if(netHaltActive.Value && (behaviourActive != behaviour.idle && behaviourActive != behaviour.still))
            {
                
                behaviourActive = behaviour.still;
                
                idleTimer = 540;

            }

            UpdateIdle();

            UpdateMove();

            UpdateSpecial();

            if (cooldownTimer > 0)
            {

                cooldownTimer--;


            }

            if (hitTimer > 0)
            {

                hitTimer--;


            }

            if(collideTimer > 0)
            {

                collideTimer--;


            }

            if(pushTimer > 0)
            {

                pushTimer--;


            }

        }

        public virtual void UpdateEvent(int index)
        {
            
            if (eventName == null)
            {

                return;

            }

            if (!Mod.instance.eventRegister.ContainsKey(eventName))
            {

                eventName = null;

                return;

            }

            Mod.instance.eventRegister[eventName].EventScene(index);
        
        }

        public virtual void UpdateIdle()
        {

            if (netHaltActive.Value)
            {

                idleTimer--;

                if (idleTimer <= 0)
                {

                    netHaltActive.Set(false);

                    idleFrame=(0);

                    idleTimer = 540;

                    behaviourActive = behaviour.follow;

                    return;

                }

                if (idleTimer % idleInterval == 0)
                {

                    idleFrame++;

                }

            }

        }

        public virtual void UpdateMove()
        {

            if (targetVectors.Count > 0)
            {

                if (moveTimer <= 0)
                {

                    moveTimer = moveInterval;

                    if (behaviourActive == behaviour.hurry)
                    {

                        moveTimer = dashInterval;

                    }

                    int nextFrame = moveFrame + 1;

                    if (nextFrame >= moveLength)
                    {

                        nextFrame = 0;

                    }

                    moveFrame = (nextFrame);

                    if (netDashActive.Value)
                    {
                        
                        moveTimer = dashInterval;

                        dashFrame++;

                        if (dashFrame > dashCeiling)
                        {

                            dashFrame = dashFloor;

                        }

                    }

                }

                moveTimer--;

            }

            if (netDashActive.Value) {

                if (targetVectors.Count == 0)
                {

                    netDashActive.Set(false);

                    dashFrame = 0;

                    behaviourActive = behaviour.follow;

                    cooldownTimer = cooldownInterval;

                }

            }

        }

        public virtual void UpdateSpecial()
        {

            if (netSpecialActive.Value)
            {
                
                specialTimer--;

                if (specialTimer % specialInterval == 0)
                {

                    specialFrame++;

                }

                if (specialTimer <= 0)
                {

                    netSpecialActive.Set(false);

                    specialFrame=(0);

                    behaviourActive = behaviour.follow;

                    cooldownTimer = cooldownInterval;

                }

            }

            if (barrages.Count > 0)
            {

                UpdateBarrages();

            }

        }

        public virtual bool ChangeTarget()
        {

            if(targetVectors.Count > 0)
            {

                return false;

            }

            if (netHaltActive.Value)
            {

                return false;

            }

            if (netSpecialActive.Value)
            {

                return false;

            }

            return true;

        }

        public virtual void UpdateTarget()
        {

            if (!ChangeTarget())
            {

                return;

            }

            switch (modeActive)
            {

                case mode.scene:
                case mode.standby:

                    if (TargetEvent()) { 

                        return; 

                    };

                    TargetIdle();

                    behaviourActive = behaviour.still;

                    return; 

                case mode.track:

                    if (TargetMonster())
                    {

                        return;

                    }

                    if (TargetTrack())
                    { 

                        return; 

                    };

                    break;

                case mode.roam:

                    if (TargetRoam()) { 

                        return; 

                    };

                    break;

            }

            TargetRandom();

        }

        public virtual bool TargetEvent()
        {
            
            if(eventName == null)
            {

                return false;

            }

            if (!Mod.instance.eventRegister.ContainsKey(eventName))
            {

                eventName = null;

                return false ;

            }

            if (eventVectors.Count == 0)
            {

                return false;

            }

            KeyValuePair<int, Vector2> eventVector = eventVectors.First();

            float num = Vector2.Distance(eventVector.Value, Position);

            if (num <= 8f)
            {

                Mod.instance.eventRegister[eventName].EventScene(eventVector.Key);

                eventVectors.Remove(eventVector.Key);

            }

            if (eventVectors.Count > 0)
            {

                eventVector = eventVectors.First();

                NextTarget(eventVector.Value);

                return true;

            }

            return false;

        }

        public virtual bool TargetIdle(int timer = 600)
        {
            
            netHaltActive.Set(true);

            behaviourActive = behaviour.idle;

            idleTimer = timer;

            ResetAll();

            return true;
        
        }

        public virtual bool TargetMonster()
        {

            float monsterDistance;

            float closestDistance = 9999f;

            List<StardewValley.Monsters.Monster> targetMonsters = new();

            foreach (NPC nonPlayableCharacter in currentLocation.characters)
            {

                if (nonPlayableCharacter is StardewValley.Monsters.Monster monsterCharacter)
                {

                    if (currentLocation is SlimeHutch)
                    {
                        continue;
                    }

                    if (monsterCharacter.Health > 0 && !monsterCharacter.IsInvisible)
                    {

                        monsterDistance = Vector2.Distance(Position, monsterCharacter.Position);

                        if (monsterDistance < opponentThreshold)
                        {

                            if (monsterDistance < closestDistance)
                            {

                                closestDistance = monsterDistance;

                                targetMonsters.Prepend(monsterCharacter);


                            }
                            else
                            {

                                targetMonsters.Add(monsterCharacter);

                            }

                        }

                    }

                    continue;

                }

            }

            if(targetMonsters.Count > 0)
            {

                if (cooldownTimer > 0)
                {

                    TargetRandom(4);

                }

                foreach (StardewValley.Monsters.Monster monster in targetMonsters)
                {

                    if (MonsterAttack(monster))
                    {

                        return true;

                    }

                }

            }

            return false;

        }

        public virtual bool MonsterAttack(StardewValley.Monsters.Monster monster)
        {

            if (ModUtility.GroundCheck(currentLocation, monster.Tile) != "ground")
            {

                behaviourActive = behaviour.dash;

                moveTimer = moveInterval;

                netDashActive.Set(true);

                NextTarget(monster.Position, -1);

                return true;

            }

            return false;

        }

        public virtual bool TargetTrack()
        {
            if (!Mod.instance.trackRegister.ContainsKey(Name) || Mod.instance.trackRegister[Name].trackVectors.Count == 0)
            {
                return false;
            }

            float num = Vector2.Distance(Position, Mod.instance.trackRegister[Name].trackVectors.Last());
            
            if (num >= 576f)
            {

                behaviourActive = behaviour.dash;

                moveTimer = dashInterval;

                netDashActive.Set(true);

                NextTarget(Mod.instance.trackRegister[Name].LastVector(),-1);

                return true;

            }
            else if(num >= 320)
            {

                behaviourActive = behaviour.hurry;

                Mod.instance.trackRegister[Name].TruncateTo(3);

                NextTarget(Mod.instance.trackRegister[Name].NextVector(), -1);

                return true;

            }

            NextTarget(Mod.instance.trackRegister[Name].NextVector(), -1);

            behaviourActive = behaviour.follow;

            moveTimer = moveInterval;

            return true;

        }

        public virtual bool TargetRoam()
        {
            
            if (roamVectors.Count == 0)
            {

                return false;
            
            }

            if (roamSummon)
            {
         
                float summon = Vector2.Distance(summonVector, Position);

                if ((double)summon >= 120.0)
                {

                    NextTarget(summonVector, 4f);

                    return true;

                }

                TargetIdle();

                behaviourActive = behaviour.still;

                roamSummon = false;

                return true;

            }

            Vector2 roamVector = roamVectors[roamIndex];
            
            if (roamVector == new Vector2(-1f))
            {
                
                ReachedIdlePosition();
                
                UpdateRoam();
                
                return true;
            
            }

            if(roamLapse < Game1.currentGameTime.TotalGameTime.TotalMinutes)
            {

                UpdateRoam();

            }

            float num = Vector2.Distance(roamVector, Position);
            
            if ((double)num <= 120.0)
            {
                
                ReachedRoamPosition();
                
                UpdateRoam();
                
                return true;
            
            }
            
            behaviourActive = behaviour.follow;

            moveTimer = moveInterval;

            if ((double)num >= 1200.0)
            {

                behaviourActive = behaviour.hurry;

            }

            NextTarget(roamVectors[roamIndex], 4f);

            return true;
        
        }

        public void UpdateRoam()
        {
            
            roamLapse = Game1.currentGameTime.TotalGameTime.TotalMinutes + 1.0;
            
            roamIndex++;
            
            if (roamIndex == roamVectors.Count)
            {
                
                SwitchRoamMode();

            }

        }

        public virtual void TargetRandom(int level = 8)
        {
            
            targetVectors.Clear();
            
            Random random = new Random();

            int decision = random.Next(level);

            switch(decision)
            {
                case 0:
                case 1:
                case 2:

                    Vector2 thisTile = new((int)(Position.X / 64), (int)(Position.Y / 64));

                    int newDirection = random.Next(5);

                    if(newDirection == 4)
                    {

                        newDirection = netDirection.Value;

                    }

                    bool bias = random.Next(2) == 0;

                    List<int> directions = new()
                        {

                            newDirection,
                            (newDirection + (bias ? 1 : 3)) % 4,
                            (newDirection + (bias ? 3 : 1)) % 4,
                            (newDirection + 2) % 4,

                        };

                    foreach (int direction in directions)
                    {

                        List<Vector2> tileOptions = ModUtility.GetTilesWithinRadius(currentLocation, thisTile, 1, true, direction);

                        if(new Random().Next(2) == 0)
                        {

                            tileOptions.Reverse();

                        }

                        foreach (Vector2 tileOption in tileOptions)
                        {

                            if (ModUtility.GroundCheck(currentLocation, tileOption, true) != "ground")
                            {

                                continue;

                            }

                            NextTarget(tileOption*64, 2);

                            behaviourActive = behaviour.follow;

                            moveTimer = moveInterval;

                            return;

                        }

                    }

                    TargetIdle(240);

                    break;

                case 3:

                    int layerWidth = currentLocation.map.Layers[0].LayerWidth;

                    int layerHeight = currentLocation.map.Layers[0].LayerHeight;

                    int midWidth = (layerWidth / 2) * 64;

                    int midHeight = (layerHeight / 2) * 64;

                    Vector2 middle = new(midWidth, midHeight);
                    
                    NextTarget(middle, 4);

                    break;

                default:

                    TargetIdle(360);

                    break;

            }
            
        }

        public virtual float MoveSpeed()
        {

            float speed = gait;

            if (behaviourActive == behaviour.dash)
            {
                
                speed = gait * 5;

            }
            else if (behaviourActive == behaviour.hurry)
            {

                speed = gait * 2;

            }

            if (modeActive == mode.track || modeActive == mode.scene)
            {

                speed *= 1.5f;

            }

            return speed;

        }

        public virtual void MoveTowardsTarget()
        {

            if (targetVectors.Count == 0)
            {
                return;
            }

            float speed = MoveSpeed();

            //------------- Factors

            Vector2 nextPosition = targetVectors.First();

            Vector2 diffPosition = nextPosition - Position;

            Vector2 movement;

            float distanceLeft = Vector2.Distance(nextPosition, Position);

            float distanceHurdle = speed * 1.5f;

            if (distanceLeft <= distanceHurdle)
            {

                backTrack.Add(nextPosition);

                if(backTrack.Count > 3)
                {

                    backTrack.Remove(backTrack.First());

                }

                if(behaviourActive == behaviour.retreat)
                {

                    behaviourActive = behaviour.follow;
                }

                targetVectors.Clear();

                movement = diffPosition;

            }
            else
            {
                
                float absX = Math.Abs(diffPosition.X); // x position

                float absY = Math.Abs(diffPosition.Y); // y position

                int signX = diffPosition.X < 0.001f ? -1 : 1; // x sign

                int signY = diffPosition.Y < 0.001f ? -1 : 1; // y sign

                float moveX = signX;

                float moveY = signY;

                if (absX > absY)
                {

                    moveY = (absY < 0.05f) ? 0 : (int)(absY / absX * signY);

                }
                else
                {
                    moveX = (absX < 0.05f) ? 0 : (int)(absX / absY * signX);

                }

                Vector2 factorVector = new(moveX, moveY);

                movement = factorVector * speed;

            }

            if (netSceneActive.Value || behaviourActive == behaviour.retreat)
            {

                Position += movement;

                return;

            }

            //------------- Collision check

            Microsoft.Xna.Framework.Rectangle boundingBox = GetBoundingBox();

            boundingBox.X += (int)movement.X;
            boundingBox.Y += (int)movement.Y;

            Microsoft.Xna.Framework.Rectangle farmerBox = Game1.player.GetBoundingBox();

            bool collision = false;

            if (farmerBox.Intersects(boundingBox) && collideTimer <= 0)
            {

                collision = true;

                collideTimer = 120;

                return;

            }

            Rectangle hitBox = GetHitBox();
            
            hitBox.X += (int)movement.X;
            
            hitBox.Y += (int)movement.Y;

            List<StardewValley.Monsters.Monster> damageMonsters = new();

            foreach (NPC nonPlayableCharacter in currentLocation.characters)
            {

                Microsoft.Xna.Framework.Rectangle boundingBox2 = nonPlayableCharacter.GetBoundingBox();

                if (nonPlayableCharacter is StardewValley.Monsters.Monster monsterCharacter)
                {

                    if (currentLocation is SlimeHutch)
                    {
                        continue;
                    }

                    if (monsterCharacter.Health > 0 && !monsterCharacter.IsInvisible && hitTimer <= 0)
                    {

                        if (boundingBox2.Intersects(hitBox))
                        {

                            damageMonsters.Add(monsterCharacter);


                        }

                    }

                    continue;

                }

                if (nonPlayableCharacter != this && nonPlayableCharacter is not StardewDruid.Character.Character && collideTimer <= 0)
                {

                    if (boundingBox2.Intersects(boundingBox))
                    {

                        collision = true;

                        collideTimer = 120;

                        return;

                    }

                }

            }

            if (damageMonsters.Count > 0)
            {

                foreach (StardewValley.Monsters.Monster monsterCharacter in damageMonsters)
                {

                    HitMonster(monsterCharacter);

                }

                hitTimer = 120;

            }
            
            //------------- Tile check

            if (behaviourActive != behaviour.dash)
            {

                Vector2 nextSpace = Position + (movement * 2);

                Vector2 thisTile = new((int)(Position.X / 64), (int)(Position.Y / 64));

                Vector2 nextTile = new((int)(nextSpace.X / 64), (int)(nextSpace.Y / 64));

                if ((thisTile != nextTile && Vector2.Distance(nextSpace,nextTile*64) <= 24) || collision)
                {

                    string ground = ModUtility.GroundCheck(currentLocation, nextTile, true);

                    if (ground != "ground" || collision)
                    {

                        bool bias = new Random().Next(2) == 0;

                        List<int> directions = new()
                        {

                            netDirection.Value,
                            (netDirection.Value + (bias ? 1 : 3)) % 4,
                            (netDirection.Value + (bias ? 3 : 1)) % 4,
                            (netDirection.Value + 2) % 4,

                        };

                        foreach (int direction in directions)
                        {
                           
                            List<Vector2> tileOptions = ModUtility.GetTilesWithinRadius(currentLocation, thisTile, 1, true, direction);

                            foreach (Vector2 tileOption in tileOptions)
                            {

                                if(tileOption == nextTile)
                                {

                                    continue;

                                }

                                if (backTrack.Contains(tileOption))
                                {

                                    continue;

                                }

                                if (ModUtility.GroundCheck(currentLocation, tileOption, true) != "ground")
                                {

                                    continue;

                                }

                                NextTarget(tileOption*64, 2);

                                return;

                            }

                        }

                        if(backTrack.Count > 0)
                        {

                            NextTarget(backTrack.First(),-1);

                            behaviourActive = behaviour.retreat;

                            return;

                        }

                        TargetIdle();

                        return;

                    }

                }

            }

            //-------------------------- commit movement
            
            Position += movement;

        }

        public virtual void WarpToDefault()
        {
            
            Halt();

            TargetIdle(60);

            if (currentLocation.Name != DefaultMap)
            {
                
                currentLocation.characters.Remove(this);
                
                currentLocation = Game1.getLocationFromName(DefaultMap);
                
                currentLocation.characters.Add(this);
            
            }
            
            Position = DefaultPosition;
            
            update(Game1.currentGameTime, currentLocation);
       
        }

        public virtual void SwitchSceneMode()
        {

            previousLocation = currentLocation.Name;

            previousPosition = Position;

            previousMode = modeActive;

            SwitchDefaultMode();

            modeActive = mode.scene;

            netSceneActive.Set(true);

        }

        public virtual void SwitchPreviousMode()
        {
            
            if(currentLocation.Name != previousLocation)
            {

                currentLocation.characters.Remove(this);

                GameLocation getLocation = Game1.getLocationFromName(previousLocation);

                getLocation.characters.Add(this);

                currentLocation = getLocation;

            }

            Position = previousPosition;

            switch(previousMode)
            {
                case mode.roam:

                    SwitchRoamMode();

                    break;

                case mode.track:

                    SwitchFollowMode();

                    break;

                case mode.standby:

                    SwitchFollowMode();

                    ActivateStandby();

                    break;

                default:

                    SwitchDefaultMode();

                    break;

            }

            update(Game1.currentGameTime, currentLocation);

            return;

        }

        public virtual void SwitchFollowMode(Farmer follow = null)
        { 

            SwitchDefaultMode();

            netFollowActive.Set(true);

            netStandbyActive.Set(false);

            Mod.instance.trackRegister.Add(Name, new TrackHandle(Name,follow));

            modeActive = mode.track;

        }

        public virtual void ActivateStandby()
        {
            
            if (Mod.instance.trackRegister.ContainsKey(Name))
            {

                Mod.instance.trackRegister[Name].standby = true;

            }

            netStandbyActive.Set(true);

            modeActive = mode.standby;

        }

        public virtual void DeactivateStandby()
        {

            netStandbyActive.Set(false);

            if (Mod.instance.trackRegister.ContainsKey(Name))
            {

                Mod.instance.trackRegister[Name].standby = false;

                modeActive = mode.track;

                return;

            }

            modeActive = mode.random;

        }

        public virtual void SwitchSitMode()
        {

            modeActive = mode.standby;

        }

        public virtual void SwitchRoamMode()
        {
            
            SwitchDefaultMode();

            roamVectors.Clear();

            roamIndex = 0;

            roamLapse = Game1.currentGameTime.TotalGameTime.TotalMinutes + 1.0;
            
            roamVectors = RoamAnalysis();

            modeActive = mode.roam;

        }

        public virtual void SwitchDefaultMode()
        {
            
            modeActive = mode.random;

            roamSummon = false;

            Mod.instance.trackRegister.Remove(Name);

            netSceneActive.Set(false);

            netFollowActive.Set(false);

            netStandbyActive.Set(false);

        }

        public virtual List<Vector2> RoamAnalysis()
        {
            
            int layerWidth = currentLocation.map.Layers[0].LayerWidth;
            
            int layerHeight = currentLocation.map.Layers[0].LayerHeight;
            
            int num = layerWidth / 8;

            int midWidth = (layerWidth / 2) * 64;
            
            int fifthWidth = (layerWidth / 5) * 64;
            
            int nextWidth = (layerWidth * 64) - fifthWidth;
            
            int midHeight = (layerHeight / 2) * 64;
            
            int fifthHeight = (layerHeight / 5) * 64;
            
            int nextHeight = (layerHeight * 64) - fifthHeight;

            List<Vector2> roamList = new()
            {
                new(midWidth,midHeight),
                new(midWidth,midHeight),
                new(midWidth,midHeight),
                new(midWidth,midHeight),
                new(nextWidth,nextHeight),
                new(nextWidth,fifthHeight),
                new(fifthWidth,nextHeight),
                new(fifthWidth,fifthHeight),

            };

            if(currentLocation.IsOutdoors)
            {
                roamList = new()
                {
                    new(midWidth, midHeight),
                    new(midWidth, midHeight),
                    new(midWidth, midHeight),
                    new(midWidth, midHeight),
                    new(nextWidth, nextHeight),
                    new(nextWidth, fifthHeight),
                    new(fifthWidth, nextHeight),
                    new(fifthWidth, fifthHeight),
                    new(-1f),
                    new(-1f),
                    new(-1f),
                    new(-1f),
                };

            }

            List<Vector2> randomList = new();

            Random random = new Random();

            for(int i = 0; i < 12; i++)
            {

                if(roamList.Count == 0)
                {
                    
                    break;

                }

                int j = random.Next(roamList.Count);

                Vector2 randomVector = roamList[j];

                randomList.Add(randomVector);

                roamList.Remove(randomVector);

            }

            return randomList;

        }

        public virtual void HitMonster(StardewValley.Monsters.Monster monsterCharacter)
        {

            DealDamageToMonster(monsterCharacter);

        }

        public virtual void DealDamageToMonster(StardewValley.Monsters.Monster monsterCharacter,bool kill = false,int damage = -1,bool push = true)
        {

            if (!ModUtility.MonsterVitals(monsterCharacter, currentLocation))
            {

                return;

            }

            if (damage == -1)
            {

                damage = Mod.instance.DamageLevel();

                if (behaviourActive != behaviour.dash)
                {

                    damage /= 4;

                }

            }
                
            if (!kill)
            {

                damage = Math.Min(damage, monsterCharacter.Health - 1);

            }

            List<int> pushList = new() { 0, 0 };

            if (push)
            {

                pushList = ModUtility.CalculatePush(currentLocation, monsterCharacter, Position);

            }

            ModUtility.HitMonster(currentLocation, Game1.player, monsterCharacter, damage, false, diffX: pushList[0], diffY: pushList[1]);

        }

        public virtual void UpdateBarrages()
        {

            for (int i = barrages.Count - 1; i >= 0; i--)
            {

                BarrageHandle barrage = barrages[i];

                if (!barrage.Update())
                {

                    barrages.Remove(barrage);

                }

            }

        }

        public virtual void ReachedRoamPosition()
        {

        }

        public virtual void ReachedIdlePosition()
        {
            
            TargetIdle(1200);

        }

        public virtual void SummonToPlayer(Vector2 position)
        {

            if (modeActive == mode.roam && !roamSummon && currentLocation is Farm)
            {

                summonVector = position;

                behaviourActive = behaviour.hurry;

                roamSummon = true;

            }

        }

    }

}
