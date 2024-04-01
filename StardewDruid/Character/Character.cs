using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Event;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using static StardewValley.Minigames.TargetGame;

namespace StardewDruid.Character
{
    public class Character : NPC
    {

        public Texture2D characterTexture;
        public List<Vector2> targetVectors = new();
        public float gait;
        public int opponentThreshold;

        public NetInt netDirection = new NetInt(0);
        public NetInt netAlternative = new NetInt(0);
        public NetBool netFollowActive = new NetBool(false);

        public List<Vector2> roamVectors = new();
        public int roamIndex;
        public double roamLapse;
        public bool roamSummon;
        public Vector2 summonVector;

        public NetBool netSceneActive = new NetBool(false);
        public Dictionary<int,Vector2> eventVectors = new();
        public List<Vector2> backTrack = new();
        public int eventIndex;
        public string eventName;
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

        public Dictionary<int, List<Rectangle>> walkFrames = new();
        public Dictionary<int, List<Rectangle>> dashFrames = new();
        public Dictionary<int, List<Rectangle>> idleFrames = new();
        public Dictionary<int, List<Rectangle>> haltFrames = new();
        public Dictionary<int, List<Rectangle>> specialFrames = new();

        public NetBool netHaltActive = new NetBool(false);
        public NetBool netStandbyActive = new NetBool(false);
        public int idleTimer;
        public int idleInterval;
        public int idleFrame;
        public int stationaryTimer;

        public int collidePriority;
        public int collideTimer;
        public int moveTimer;
        public int moveInterval;
        public int moveFrame;
        public bool moveRetreat;
        public bool walkSide;
        public int walkLeft;
        public int walkRight;

        public NetBool netDashActive = new NetBool(false);
        public List<Vector2> dashVectors = new();
        public int dashFrame;
        public int dashFloor;
        public int dashCeiling;
        public bool dashSweep;

        public NetBool netSweepActive = new(false);
        public Dictionary<int, List<Rectangle>> sweepFrames = new();
        public int sweepTimer;
        public int sweepFrame;
        public int sweepInterval;

        public NetBool netSpecialActive = new NetBool(false);
        public int specialTimer;
        public int specialInterval;
        public int specialCeiling;
        public int specialFloor;
        public int specialFrame;
        public SpellHandle.schemes specialScheme;
        public SpellHandle.indicators specialIndicator;

        public int cooldownTimer;
        public int cooldownInterval;
        public int hitTimer;
        public int pushTimer;

        public int moveDirection;
        public int altDirection;
        public Vector2 setPosition = Vector2.Zero;

        public string previousLocation;
        public Vector2 previousPosition = Vector2.Zero;
        public mode previousMode;
        
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
            NetFields.AddField(netDirection, "netDirection");
            NetFields.AddField(netAlternative, "netAlternative");
            NetFields.AddField(netSpecialActive, "netSpecialActive");
            NetFields.AddField(netDashActive, "netDashActive");
            NetFields.AddField(netHaltActive, "netHaltActive");
            NetFields.AddField(netFollowActive, "netFollowActive");
            NetFields.AddField(netStandbyActive, "netStandbyActive");
            NetFields.AddField(netSceneActive, "netSceneActive");
            NetFields.AddField(netSweepActive, "netSweepActive");

        }

        public virtual void LoadBase()
        {

            opponentThreshold = 760;

            collidePriority = new Random().Next(20);

            gait = 1.6f;

            idleInterval = 90;

            moveInterval = 12;

            modeActive = mode.random;

            walkLeft = 1;

            walkRight = 3;

            dashFloor = 1;

            dashCeiling = 4;

            sweepInterval = 9;

            specialInterval = 30;

            specialCeiling = 1;

            specialFloor = 1;

            cooldownInterval = 180;

            specialScheme = SpellHandle.schemes.fire;

            specialIndicator = SpellHandle.indicators.target;

        }

        public virtual void LoadOut()
        {

            LoadBase();

            characterTexture = CharacterData.CharacterTexture(Name);

            haltFrames = FrameSeries(16, 32, 0, 0, 1);

            walkFrames = FrameSeries(16, 32, 0, 0, 4);

            dashFrames = walkFrames;

            loadedOut = true;

        }

        public virtual Dictionary<int, List<Rectangle>> FrameSeries( int width, int height, int startX = 0, int startY = 0, int length = 6, Dictionary<int, List<Rectangle>> frames = null)
        {

            if(frames == null)
            {
                frames = new()
                {
                    [0] = new(),
                    [1] = new(),
                    [2] = new(),
                    [3] = new(),
                };
            }

            foreach (KeyValuePair<int, int> keyValuePair in new Dictionary<int, int>()
            {
                [0] = 2,
                [1] = 1,
                [2] = 0,
                [3] = 3
            })
            {

                for (int index = 0; index < length; index++)
                {
                    
                    Rectangle rectangle = new(startX, startY, width, height);
                    
                    rectangle.X += width * index;
                    
                    rectangle.Y += height * keyValuePair.Value;
                    
                    frames[keyValuePair.Key].Add(rectangle);
                
                }

            }

            return frames;

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
            DefaultPosition = WarpData.WarpStart(DefaultMap);
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

            idleTimer = 300;

        }

        public virtual void ResetActives()
        {

            ClearIdle();

            ClearMove();

            ClearSpecial();

            targetVectors.Clear();

        }

        public virtual void ResetFrames()
        {

            idleFrame = 0;

            moveFrame=0;

            dashFrame = 0;

            sweepFrame = 0;

            specialFrame= 0;

            targetVectors.Clear();

        }

        public virtual void ClearIdle()
        {

            if (netHaltActive.Value)
            {

                netHaltActive.Set(false);

            }

            if (netStandbyActive.Value)
            {

                netStandbyActive.Set(false);

            }

            idleFrame = (0);

            idleTimer = 540;

        }

        public virtual void ClearMove()
        {

            if (netDashActive.Value)
            {

                netDashActive.Set(false);

            }

            dashVectors.Clear();

            dashFrame = 0;

            moveTimer = 0;

            moveFrame = 0;

            dashSweep = false;

            if (netSweepActive.Value)
            {

                netSweepActive.Set(false);

            }

            sweepTimer = 0;

            sweepFrame = (0);

        }

        public void ClearSpecial()
        {
            if (netSpecialActive.Value)
            {

                netSpecialActive.Set(false);

            }

            specialTimer = 0;

            specialFrame = 0;

            cooldownTimer = 0;

            hitTimer = 0;


        }

        public void LookAtTarget(Vector2 target)
        {

            List<int> directions = ModUtility.DirectionToTarget(target, Position);

            moveDirection = directions[0];

            altDirection = directions[1];

            netDirection.Set(moveDirection);

            netAlternative.Set(altDirection);

        }

        public void NextTarget(Vector2 target, float span = 1.5f)
        {

            List<int> directions = ModUtility.DirectionToTarget(target, Position);

            moveDirection = directions[0];

            altDirection = directions[1];

            netDirection.Set(moveDirection);

            netAlternative.Set(altDirection);

            Vector2 moveTarget = target;

            float distance = Vector2.Distance(Position, target);

            Vector2 difference = new(target.X - Position.X, target.Y - Position.Y);

            float absoluteX = Math.Abs(difference.X);

            float absoluteY = Math.Abs(difference.Y);

            int signX = difference.X < 0.001f ? -1 : 1;

            int signY = difference.Y < 0.001f ? -1 : 1;

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

                if(pushTimer > 3)
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

            Vector2 thisTile = new((int)(Position.X / 64), (int)(Position.Y / 64));

            if (ModUtility.GroundCheck(currentLocation, thisTile, true) != "ground")
            {

                WarpToEntrance(thisTile);

            }

            UpdateBehaviour();

            ChooseBehaviour();

            MoveTowardsTarget();

        }
        
        // ========================================
        // SET BEHAVIOUR
        // ========================================

        public virtual bool ChangeTarget()
        {

            if (targetVectors.Count > 0)
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

            if (netSweepActive.Value)
            {

                return false;

            }

            return true;

        }

        public virtual void ChooseBehaviour()
        {

            if (!ChangeTarget())
            {

                return;

            }

            switch (modeActive)
            {

                case mode.scene:
                case mode.standby:

                    if (TargetEvent())
                    {

                        return;

                    };

                    TargetIdle();

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

                    TargetRandom(12);

                    return;

                case mode.roam:

                    if (TargetRoam())
                    {

                        return;

                    };

                    TargetRandom(12);

                    return;

            }

            TargetRandom();

        }

        public virtual bool TargetEvent()
        {

            if (eventName == null)
            {

                return false;

            }

            if (!Mod.instance.eventRegister.ContainsKey(eventName))
            {

                eventName = null;

                return false;

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

        public virtual bool TargetIdle(int timer = -1)
        {

            if(timer == -1)
            {

                switch (modeActive)
                {

                    case mode.track:

                        timer = 180;

                        break;

                    case mode.roam:

                        timer = 360;

                        break;

                    default: //mode.scene: mode.standby:

                        timer = 600;

                        break;

                }

            }

            ResetActives();

            netHaltActive.Set(true);

            if (modeActive == mode.standby)
            {

                netStandbyActive.Set(true);

            }

            idleTimer = timer;

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

            if (targetMonsters.Count > 0)
            {

                if (cooldownTimer > 0)
                {

                    TargetRandom(4);

                }

                MonsterAttack(targetMonsters.First());

                return true;

            }

            return false;

        }

        public virtual void MonsterAttack(StardewValley.Monsters.Monster monster)
        {

            float distance = Vector2.Distance(Position, monster.Position);

            string terrain = ModUtility.GroundCheck(currentLocation, monster.Tile);

            if (new Random().Next(3) == 0 || terrain != "ground")
            {

                SpecialAttack(monster);

            }

            if (distance >= 192f)
            {

                CloseDistance(monster, distance);

            }

            SweepAttack(monster);

        }

        public virtual void CloseDistance(StardewValley.Monsters.Monster monster, float distance)
        {

            ResetActives();

            float newDistance = distance - 160;

            netDashActive.Set(true);

            Vector2 diff = (monster.Position - Position) / distance * newDistance;

            Vector2 destination = Position + diff;

            NextTarget(destination, -1);

        }

        public virtual void SweepAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            targetVectors.Clear();

            netSweepActive.Set(true);

            sweepFrame = 0;

            sweepTimer = sweepFrames[0].Count() * sweepInterval;

            NextTarget(monster.Position, -1);

        }

        public virtual void SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecialActive.Set(true);

            specialTimer = 90;

            LookAtTarget(monster.Position);

            SpellHandle fireball = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 2, 1, -1, Mod.instance.DamageLevel(), 3);

            fireball.type = SpellHandle.spells.fireball;

            fireball.scheme = specialScheme;

            fireball.indicator = specialIndicator;

            fireball.counter = -30;

            Mod.instance.spellRegister.Add(fireball);

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

                netDashActive.Set(true);

                NextTarget(Mod.instance.trackRegister[Name].LastVector(), -1);

                return true;

            }
            else if (num >= 320)
            {

                Mod.instance.trackRegister[Name].TruncateTo(3);

                NextTarget(Mod.instance.trackRegister[Name].NextVector(), -1);

                return true;

            }

            NextTarget(Mod.instance.trackRegister[Name].NextVector(), -1);

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

            if (roamLapse < Game1.currentGameTime.TotalGameTime.TotalMinutes)
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

            NextTarget(roamVectors[roamIndex], 4f);

            return true;

        }

        public virtual void TargetRandom(int level = 8)
        {

            targetVectors.Clear();

            if (CollideCharacters(Vector2.Zero) && collideTimer <= 0)
            {

                level = 4; // cant stay still

            }

            Random random = new Random();

            int decision = random.Next(level);

            switch (decision)
            {
                case 0:
                case 1:
                case 2:

                    Vector2 thisTile = new((int)(Position.X / 64), (int)(Position.Y / 64));

                    int newDirection = random.Next(5);

                    if (newDirection == 4)
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

                        if (new Random().Next(2) == 0)
                        {

                            tileOptions.Reverse();

                        }

                        foreach (Vector2 tileOption in tileOptions)
                        {

                            if (ModUtility.GroundCheck(currentLocation, tileOption, true) != "ground")
                            {

                                continue;

                            }

                            NextTarget(tileOption * 64, 2);

                            return;

                        }

                    }

                    TargetIdle();

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

                    TargetIdle();

                    break;

            }

        }

        // ========================================
        // UPDATE
        // ========================================

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

            if (netSweepActive.Value)
            {

                sweepTimer++;

                if (sweepTimer == sweepInterval)
                {

                    sweepFrame++;

                    sweepTimer = 0;

                }

            }
            else
            {
                sweepFrame = 0;

                sweepTimer = 0;

            }

            if (netSpecialActive.Value)
            {

                specialTimer++;

                if (specialTimer == specialInterval)
                {

                    specialFrame++;

                    if (specialFrame > specialCeiling)
                    {

                        specialFrame = specialFloor;

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

                    if (moveFrame >= walkFrames[0].Count)
                    {

                        moveFrame = 1;
                    
                    }

                    moveTimer = moveInterval;

                    if (netDashActive.Value)
                    {

                        moveTimer -= 3;

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

                    ClearIdle();

                    ClearMove();

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

            if (targetVectors.Count == 0)
            {

                ClearMove();

                return;

            }

            moveTimer--;

            if (moveTimer <= 0)
            {

                moveTimer = moveInterval - (int)MoveSpeed();

                moveFrame++;

                if(moveFrame == walkLeft)
                {

                    if (walkSide)
                    {

                        moveFrame = walkRight;

                    }

                }

                if (moveFrame == walkRight)
                {

                    walkSide = false;

                }

                if (moveFrame >= walkFrames[0].Count)
                {

                    moveFrame = walkLeft;

                    walkSide = true;

                }

                dashFrame++;

                if (dashFrame > dashCeiling)
                {

                    dashFrame = dashFloor;

                }

                if (dashSweep)
                {

                    if(Vector2.Distance(targetVectors.First(), Position) <= 128)

                    netDashActive.Set(false);

                    netSweepActive.Set(true);

                    dashSweep = false;

                }

            }

            if (netSweepActive.Value)
            {

                sweepTimer--;

                if (sweepTimer % sweepInterval == 0)
                {

                    sweepFrame++;

                }

                if (sweepTimer == sweepInterval)
                {

                    List<StardewValley.Monsters.Monster> monsters = ModUtility.MonsterProximity(currentLocation, new() { targetVectors.First(), }, 1);

                    foreach (StardewValley.Monsters.Monster monster in monsters)
                    {

                        DealDamageToMonster(monster);

                    }

                }

                if (sweepTimer <= 0)
                {

                    netSweepActive.Set(false);

                    sweepFrame = (0);

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

                    if(specialFrame > specialCeiling)
                    {

                        specialFrame = specialFloor;

                    }

                }

                if (specialTimer <= 0)
                {

                    ClearSpecial();

                    cooldownTimer = cooldownInterval;

                }

            }

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
        
        public virtual float MoveSpeed(float distance = 0)
        {

            float speed = gait;

            if (netSweepActive.Value)
            {

                return (int)(gait * 0.75);

            }

            if (distance > 640 || netDashActive.Value)
            {

                speed = gait * 3;

            }
            else if (distance > 360)
            {

                speed = gait * 2;

            }

            if (modeActive == mode.scene)
            {

                speed *= 1.25f;

            }

            return speed;

        }

        public virtual void MoveTowardsTarget()
        {

            if (targetVectors.Count == 0)
            {

                return;
            
            }

            //------------- Factors

            Vector2 targetPosition;

            if(dashVectors.Count > 0 && netDashActive.Value)
            {

                targetPosition = dashVectors.First();

            }
            else
            {

                targetPosition = targetVectors.First();

            }

            Vector2 diffPosition = targetPosition - Position;

            Vector2 movement;

            float distanceLeft = Vector2.Distance(targetPosition, Position);

            float speed = MoveSpeed(distanceLeft);

            float distanceHurdle = speed * 1.5f;

            bool addBack = false;

            if (distanceLeft <= distanceHurdle)
            {

                if (moveRetreat)
                {

                    moveRetreat = false;

                }

                if(dashVectors.Count > 0)
                {

                    dashVectors.Clear();
                }
                else
                {
                    
                    targetVectors.Clear();

                }

                movement = diffPosition;

                addBack = true;

            }
            else
            {

                /*float absX = Math.Abs(diffPosition.X); // x position

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

                Vector2 factorVector = new(moveX, moveY);*/

                Vector2 factorVector = diffPosition / distanceLeft;

                movement = factorVector * speed;

            }

            if (netSceneActive.Value || moveRetreat)
            {

                Position += movement;

                return;

            }

            CollideMonsters(movement);

            //------------- Tile check

            if (!netDashActive.Value)
            {

                Vector2 thisTile = new((int)(Position.X / 64), (int)(Position.Y / 64));

                Vector2 movePosition = Position + movement;

                Vector2 nextTile = new((int)(movePosition.X/64), (int)(movePosition.Y/64));

                Vector2 checkTile;

                bool jump = false;

                bool check = true;

                Queue<Vector2> nodes = new();

                Queue<Vector2> jumps = new();

                if(thisTile == nextTile)
                {

                    check = false;

                }
                else
                {

                    nodes = ModUtility.GetOpenSet(currentLocation, thisTile, thisTile, nextTile, 6);

                }

                while (check)
                {

                    if (nodes.Count == 0)
                    {

                        if (jumps.Count > 0)
                        {

                            // jump if little distance remaining
                            if (distanceLeft <= 196)
                            {

                                netDashActive.Value = true;

                                check = false;

                                continue;

                            }

                            Vector2 jumpTile = jumps.Dequeue();

                            // see if clear on other side
                            nodes = ModUtility.GetOpenSet(currentLocation, thisTile, jumpTile, jumpTile, 6);

                            jump = true;

                        }

                    }

                    if (nodes.Count > 0)
                    {

                        checkTile = nodes.Dequeue();

                    }
                    else
                    {

                        TargetRandom();

                        check = false;

                        continue;

                    }

                    if (backTrack.Contains(checkTile))
                    {

                        continue;

                    }

                    Dictionary<string, List<Vector2>> ObjectCheck = ModUtility.NeighbourCheck(currentLocation, checkTile, 0);

                    if(ObjectCheck.ContainsKey("Building") || ObjectCheck.ContainsKey("Wall"))
                    {

                        continue;

                    }

                    if(ObjectCheck.ContainsKey("BigObject") || ObjectCheck.ContainsKey("Fence"))
                    {

                        jumps.Enqueue(checkTile);

                        continue;
                    }

                    string groundCheck = ModUtility.GroundCheck(currentLocation, checkTile, true);

                    if (groundCheck == "ground")
                    {
                        Vector2 adjustTo = checkTile * 64 + new Vector2(32, 32);

                        if (jump)
                        {

                            netDashActive.Set(true);

                            dashVectors.Add(adjustTo);

                        }

                        Vector2 adjustDiff = adjustTo - Position;

                        float adjustDistance = Vector2.Distance(adjustTo, Position);

                        Vector2 adjustFactor = adjustDiff / adjustDistance;

                        movement = adjustFactor * speed;

                        backTrack.Add(thisTile);

                        check = false;

                        continue;

                    }

                    if (groundCheck == "water")
                    {

                        jumps.Enqueue(checkTile);

                        continue;

                    }

                }

            }

            //-------------------------- commit movement

            Position += movement;

            if (addBack)
            {
                
                backTrack.Add(new((int)(Position.X / 64), (int)(Position.Y / 64)));

                if (backTrack.Count > 3)
                {

                    backTrack.Remove(backTrack.First());

                }

            }

        }

        public virtual void CollideMonsters(Vector2 movement)
        {
            
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

            }

            if (damageMonsters.Count > 0)
            {

                foreach (StardewValley.Monsters.Monster monsterCharacter in damageMonsters)
                {

                    HitMonster(monsterCharacter);

                }

                hitTimer = 120;

            }

        }

        public virtual bool CollideCharacters(Vector2 movement)
        {
            //------------- Collision check

            Microsoft.Xna.Framework.Rectangle boundingBox = GetBoundingBox();

            boundingBox.X += (int)movement.X;
            boundingBox.Y += (int)movement.Y;

            Microsoft.Xna.Framework.Rectangle farmerBox = Game1.player.GetBoundingBox();

            if (farmerBox.Intersects(boundingBox) && collideTimer <= 0)
            {

                collideTimer = 120;
                return true;

            }

            foreach (NPC nonPlayableCharacter in currentLocation.characters)
            {

                Microsoft.Xna.Framework.Rectangle boundingBox2 = nonPlayableCharacter.GetBoundingBox();

                if (nonPlayableCharacter != this && collideTimer <= 0)
                {

                    if (nonPlayableCharacter is StardewDruid.Character.Character buddy)
                    {

                        if (collidePriority > buddy.collidePriority)
                        {

                            if (boundingBox2.Intersects(boundingBox))
                            {

                                collideTimer = 120;
                                return true;

                            }

                        }

                    }
                    else if (boundingBox2.Intersects(boundingBox))
                    {

                        collideTimer = 120;
                        return true;

                    }

                }

            }
            return false;

        }

        // ========================================
        // ADJUST MODE
        // ========================================

        public virtual void WarpToEntrance(Vector2 targetVector)
        {

            ResetActives();

            ModUtility.AnimateQuickWarp(currentLocation, Position, true);

            Vector2 destination = new Vector2(-1);

            if(modeActive == mode.track)
            {

                if (Mod.instance.trackRegister[Name].WarpToTarget(false))
                {

                    return;
                
                }

            }

            if (currentLocation is MineShaft)
            {

                destination = WarpData.WarpXZone(currentLocation, targetVector);

                if (destination != new Vector2(-1))
                {

                    for(int i = 0; i < 5; i++)
                    {

                        if(i == 4)
                        {

                            WarpToDefault(false);

                        }

                        if(ModUtility.GroundCheck(currentLocation, new Vector2((int)(destination.X / 64), (int)(destination.Y / 64)),true) == "ground")
                        {

                            break;

                        }

                        destination += new Vector2(0, 64);

                    }

                    Position = destination;

                    Mod.instance.Monitor.Log(Name + " warped to the entrance of " + currentLocation.DisplayName + " because they got stuck", LogLevel.Debug);

                    ModUtility.AnimateQuickWarp(currentLocation, Position);

                    return;

                }

            }
            else
            {

                destination = WarpData.WarpStart(currentLocation.Name);

                if (destination == new Vector2(-1))
                {

                    destination = WarpData.WarpEntrance(currentLocation, targetVector);

                }
                else
                {

                    destination *= 64;

                }

                if (destination != new Vector2(-1))
                {

                    int centerDirection = ModUtility.DirectionToCenter(currentLocation, Position)[2];

                    Vector2 centerMovement = ModUtility.DirectionAsVector(centerDirection) * 64;

                    for (int i = 0; i < 5; i++)
                    {

                        if (i == 4)
                        {
                            
                            Mod.instance.Monitor.Log(Name + " warped home because they got stuck and couldnt find a warp point", LogLevel.Debug);

                            WarpToDefault(false);

                        }

                        Vector2 destinationTile = new Vector2((int)(destination.X / 64), (int)(destination.Y / 64));

                        if (ModUtility.GroundCheck(currentLocation, destinationTile, true) == "ground")
                        {

                            break;

                        }

                        destination += centerMovement;

                    }

                    Position = destination;

                    if(currentLocation is not FarmCave)
                    {

                        Mod.instance.Monitor.Log(Name + " warped to the entrance of " + currentLocation.DisplayName + " because they got stuck", LogLevel.Debug);

                    }

                    ModUtility.AnimateQuickWarp(currentLocation, Position);

                    return;

                }

            }

            Mod.instance.Monitor.Log(Name + " warped home because they got stuck and couldnt find a warp point", LogLevel.Debug);

            WarpToDefault(false);

        }

        public virtual void WarpToDefault(bool updateAfter = true)
        {

            if (currentLocation.Name != DefaultMap)
            {
                
                currentLocation.characters.Remove(this);
                
                currentLocation = Game1.getLocationFromName(DefaultMap);
                
                currentLocation.characters.Add(this);
            
            }
            
            Position = DefaultPosition;

            if (updateAfter)
            {
                
                Halt();

                TargetIdle(60);

                update(Game1.currentGameTime, currentLocation);

                return;

            }

            ResetActives();

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

                if (!netDashActive.Value)
                {

                    damage /= 4;

                }

            }
                
            //if (!kill)
            //{

            //    damage = Math.Min(damage, monsterCharacter.Health - 1);

            //}

            List<int> pushList = new() { 0, 0 };

            if (push)
            {

                pushList = ModUtility.CalculatePush(currentLocation, monsterCharacter, Position);

            }

            ModUtility.HitMonster(currentLocation, Game1.player, monsterCharacter, damage, false, diffX: pushList[0], diffY: pushList[1]);

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

                roamSummon = true;

            }

        }

    }

}
