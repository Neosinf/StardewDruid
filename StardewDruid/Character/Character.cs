using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Dialogue;
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
using System.Threading;

namespace StardewDruid.Character
{
    public class Character : NPC
    {

        public Texture2D characterTexture;
        //public List<Vector2> targetVectors = new();

        public Vector2 occupied;
        public Vector2 destination;
        public Dictionary<Vector2,int> traversal = new();
        public Vector2 tether;

        public float gait;

        public NetInt netDirection = new NetInt(0);
        public NetInt netAlternative = new NetInt(0);
        public NetBool netFollowActive = new NetBool(false);
        public NetBool netWorkActive = new NetBool(false);

        public List<Vector2> roamVectors = new();
        public int roamIndex;
        public double roamLapse;
        public bool roamSummon;
        public Vector2 summonVector;

        public NetBool netSceneActive = new NetBool(false);
        public Dictionary<int,Vector2> eventVectors = new();
        public List<Vector2> closedVectors = new();
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
        public Dictionary<int, List<Rectangle>> workFrames = new();

        public NetBool netHaltActive = new NetBool(false);
        public NetBool netStandbyActive = new NetBool(false);
        public int idleTimer;
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
        public int lookTimer;
        public int followTimer;

        public NetBool netDashActive = new NetBool(false);
        public int dashFrame;
        public int dashFloor;
        public int dashCeiling;
        public bool dashSweep;
        public int dashHeight;

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
        public Vector2 workVector;

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

            SettleOccupied();

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
            NetFields.AddField(netWorkActive, "netWorkActive");
        }

        public virtual void LoadBase()
        {

            collidePriority = new Random().Next(20);

            gait = 1.6f;

            moveInterval = 12;

            modeActive = mode.random;

            walkLeft = 1;

            walkRight = 3;

            dashFloor = 1;

            dashCeiling = 4;

            sweepInterval = 7;

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

            DrawEmote(b);

        }

        public override void DrawEmote(SpriteBatch b)
        {

            if (IsEmoting && !Game1.eventUp)
            {
                Vector2 localPosition = getLocalPosition(Game1.viewport);

                float drawLayer = (float)StandingPixel.Y / 10000f;

                b.Draw(Game1.emoteSpriteSheet, localPosition - new Vector2(0, 160), new Microsoft.Xna.Framework.Rectangle(base.CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, base.CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16), Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, drawLayer);
            }

        }

        public virtual void DrawShadow(SpriteBatch b, Vector2 localPosition, float drawLayer, float offset = 0)
        {

            b.Draw(
                Game1.shadowTexture,
                localPosition + new Vector2(6 + offset, 44f),
                Game1.shadowTexture.Bounds,
                Color.White * 0.65f,
                0f,
                Vector2.Zero,
                4f,
                SpriteEffects.None,
                drawLayer - 0.0001f
                );

        }

        public int IdleFrame()
        {

            int interval = 12000 / idleFrames[0].Count();

            int timeLapse = (int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 12000);

            if (timeLapse == 0) { return 0; }

            int frame = (int)timeLapse / interval;

            return frame;

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
            
            if (Mod.instance.eventRegister.ContainsKey("transform"))
            {

                Mod.instance.CastMessage("Unable to converse while transformed");

                return false;

            }

            foreach (NPC character in currentLocation.characters)
            {

                if (character is StardewValley.Monsters.Monster monster && (double)Vector2.Distance(Position, monster.Position) <= 1280.0)
                {

                    return false;

                }

            }

            if (netDashActive.Value || netSpecialActive.Value)
            {

                return false;

            }

            if (EngageDialogue())
            {
                return false;
            }

            Halt();

            LookAtTarget(who.Position, true);

            return true;

        }

        public virtual bool EngageDialogue()
        {

            return true;

        }

        public override void Halt()
        {

            netHaltActive.Set(true);
            //ModUtility.LogStrings(new() { Name, "idle", "halt"});
            TargetIdle();

        }

        public virtual void ResetActives()
        {

            ClearIdle();

            ClearMove();

            ClearSweep();

            ClearSpecial();

            SettleOccupied();

            //targetVectors.Clear();

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

            idleTimer = 0;

        }

        public virtual void ClearMove()
        {

            destination = Vector2.Zero;

            traversal.Clear();

            if (netDashActive.Value)
            {

                netDashActive.Set(false);

            }

            dashFrame = 0;

            moveTimer = 0;

            moveFrame = 0;

            dashSweep = false;

        }

        public virtual void ClearSweep()
        {

            if (netSweepActive.Value)
            {

                netSweepActive.Set(false);

            }

            sweepFrame = 0;

            sweepTimer = 0;

        }

        public virtual void ClearSpecial()
        {
            
            if (netSpecialActive.Value)
            {

                netSpecialActive.Set(false);

            }

            specialTimer = 0;

            specialFrame = 0;

            if (netWorkActive.Value)
            {

                netWorkActive.Set(false);

            }

        }

        public void LookAtTarget(Vector2 target, bool force = false)
        {
            
            if (lookTimer > 0 && !force) { return; }

            List<int> directions = ModUtility.DirectionToTarget(Position, target);

            moveDirection = directions[0];

            altDirection = directions[1];

            netDirection.Set(moveDirection);

            netAlternative.Set(altDirection);

            lookTimer = 30;

        }

        /*public void NextTarget(Vector2 target, float span = -1)
        {

            targetVectors.Clear();

            closedVectors.Clear();

            /*List<Vector2> paths = ModUtility.GetPath(currentLocation, new() { }, Position, target, span, true);

            Vector2 path = paths[0];

            if (path != new Vector2(-1))
            {

                target = path;

                targetVectors.Add(target);
                
            }
            else
            {

                TargetIdle();

            }*/

            /*if(span != -1)
            {

                float distance = Vector2.Distance(Position, target);

                float limit = span * 64;

                if (distance > (limit))
                {

                    Vector2 shorten = Position + (((target - Position) / distance) * limit);

                    Vector2 tiled = new((int)(shorten.X / 64),(int)(shorten.Y / 64));

                    target = tiled * 64;

                }

            }

            targetVectors.Add(target);

            LookAtTarget(target);

        }*/

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

            /*Vector2 thisTile = new((int)(Position.X / 64), (int)(Position.Y / 64));

            if (ModUtility.GroundCheck(currentLocation, thisTile, false) != "ground")
            {

                WarpToEntrance(thisTile);

                return;

            }*/

            UpdateBehaviour();

            ChooseBehaviour();

            //MoveTowardsTarget();

            Traverse();

        }
        
        // ========================================
        // SET BEHAVIOUR
        // ========================================

        public virtual bool ChangeBehaviour()
        {

            if (netHaltActive.Value)
            {
                /*if (collideTimer <= 0)
                {

                    if (CollideCharacters())
                    {
 
                        return true;

                    }

                }*/

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

            if (netDashActive.Value)
            {

                return false;

            }

            if (destination != Vector2.Zero)
            {

                if(modeActive == mode.roam)
                {

                    if (roamSummon)
                    {

                        float distance = Vector2.Distance(summonVector, occupied*64);

                        if (distance <= 192f)
                        {

                            ClearMove();

                            return true;

                        }

                    }
                    else
                    {

                        float distance = Vector2.Distance(roamVectors[roamIndex], occupied*64);

                        if (distance <= 160f)
                        {

                            ClearMove();

                            return true;

                        }

                    }

                }

                return false;

            }

            if (idleTimer > 0)
            {

                if (modeActive == mode.track)
                {

                    if (cooldownTimer <= 0)
                    {

                        if (ModUtility.MonsterProximity(currentLocation, new() { Position, }, 10f ).Count > 0)
                        {

                            ClearIdle();

                            return true;

                        }

                    }

                    if (Vector2.Distance(Position, Mod.instance.trackRegister[Name].followPlayer.Position) >= 960 || !Utility.isOnScreen(Position,128))
                    {

                        ClearIdle();

                        followTimer = 0;

                        return true;

                    }

                }

                if (collideTimer<= 0)
                {
                    
                    if (CollideCharacters(occupied))
                    {

                        ClearIdle();

                        TargetRandom(4);

                    }

                }

                return false;

            }

            return true;

        }

        public virtual void ChooseBehaviour()
        {

            if (!ChangeBehaviour())
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

                    if (TargetWork())
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

                    if (TargetWork())
                    {

                        return;

                    }

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
                //ModUtility.LogStrings(new() { Name, "going to eventVector", eventVector.Value.ToString() });
                PathTarget(eventVector.Value,0,0);

                return true;

            }

            return false;

        }

        public virtual bool TargetIdle(int timer = -1)
        {

            if (ModUtility.TileAccessibility(currentLocation, occupied) != 0)
            {

                WarpToEntrance(occupied);

            }

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

                        timer = 720;

                        break;

                }

            }

            Random random = new();

            if(random.Next(2) == 0)
            {

                moveDirection = 2;
                netDirection.Set(2);

            }

            idleTimer = timer;

            if (modeActive == mode.standby)
            {

                netStandbyActive.Set(true);

            }

            return true;

        }

        public virtual bool TargetMonster()
        {
            
            if(cooldownTimer > 0)
            {

                return false;

            }

            List<StardewValley.Monsters.Monster> monsters = ModUtility.MonsterProximity(currentLocation, new() { Position, }, 10f);

            if (monsters.Count > 0)
            {

                foreach(StardewValley.Monsters.Monster monster in monsters)
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

            float distance = Vector2.Distance(Position, monster.Position);

            string terrain = ModUtility.GroundCheck(currentLocation, new Vector2((int)(monster.Position.X/64),(int)(monster.Position.Y/64)));

            if (new Random().Next(3) == 0 || terrain != "ground")
            {

                return SpecialAttack(monster);

            }

            if (distance >= 192f)
            {

                return CloseDistance(monster);

            }

            return SweepAttack(monster);

        }

        public virtual bool CloseDistance(StardewValley.Monsters.Monster monster)
        {

            ResetActives();
            //ModUtility.LogStrings(new() { Name, "going to monster Position", monster.Position.ToString() });
            if (PathTarget(monster.Position, 1, 1))
            {

                netDashActive.Set(true);

                return true;

            }

            return false;

        }

        public virtual bool SweepAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();
            //ModUtility.LogStrings(new() { Name, "going to monster Position", monster.Position.ToString() });
            if (PathTarget(monster.Position, 1, 0))
            {

                netSweepActive.Set(true);

                sweepFrame = 0;

                sweepTimer = sweepFrames[0].Count() * sweepInterval;

                int stun = Math.Max(monster.stunTime.Value, 500);

                monster.stunTime.Set(stun);

                return true;

            }

            return false;

        }

        public virtual bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecialActive.Set(true);

            specialTimer = 90;

            LookAtTarget(monster.Position, true);

            SpellHandle fireball = new(currentLocation, monster.Position, GetBoundingBox().Center.ToVector2(), 2, 1, -1, Mod.instance.CombatDamage(), 3);

            fireball.type = SpellHandle.spells.fireball;

            fireball.scheme = specialScheme;

            fireball.indicator = specialIndicator;

            fireball.counter = -30;

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

        public virtual bool TargetTrack()
        {
            
            if(followTimer > 0)
            {

                return false;

            }

            if (!Mod.instance.trackRegister.ContainsKey(Name))
            {
                
                return false;
            
            }

            if (Vector2.Distance(Position, Mod.instance.trackRegister[Name].followPlayer.Position) <= 256)
            {
                followTimer = 120;
                return false;

            }

            //ModUtility.LogStrings(new() { Name, "going to player Position", Mod.instance.trackRegister[Name].followPlayer.Position.ToString() });
            if (PathTarget(Mod.instance.trackRegister[Name].followPlayer.Position, 2, 2))
            {
                
                return true;

            }

            Vector2 closestPath = Mod.instance.trackRegister[Name].ClosestVector(Position);

            if(closestPath == Vector2.Zero)
            {

                return false;

            }

            //ModUtility.LogStrings(new() { Name, "going to closest vector", closestPath.ToString() });
            if (PathTarget(closestPath,1,0))
            {

                return true;

            }

            return false;

        }

        public virtual bool TargetRoam()
        {

            if (roamVectors.Count == 0)
            {

                return false;

            }

            if (roamSummon)
            {

                roamSummon = false;

                if (Vector2.Distance(summonVector, Position) <= 192f)
                {

                    ClearMove();
                    //ModUtility.LogStrings(new() { Name, "idle", "reached summon" });
                    TargetIdle(720);

                    netStandbyActive.Set(true);

                    summonVector = Vector2.Zero;

                    return true;

                }
                //ModUtility.LogStrings(new() { Name, "going to summon vector", summonVector.ToString() });
                if (PathTarget(summonVector, 1, 2))
                {

                    roamSummon = true;

                    roamLapse = Game1.currentGameTime.TotalGameTime.TotalMinutes + 1;

                    return true;

                }

            }

            Vector2 roamVector = roamVectors[roamIndex];

            if (roamVector.X < 0)
            {

                UpdateRoam();
                //ModUtility.LogStrings(new() { Name, "idle", "roam break" });
                TargetIdle(720);

                netStandbyActive.Set(true);

                return true;

            }

            if (Vector2.Distance(roamVectors[roamIndex], Position) <= 160f || roamLapse < Game1.currentGameTime.TotalGameTime.TotalMinutes)
            {

                ClearMove();

                UpdateRoam();

                return true;

            }

            //ModUtility.LogStrings(new() { Name, "attempting roam", roamVectors[roamIndex].ToString() });
            
            if (PathTarget(roamVectors[roamIndex],1, 2))
            {

                return true;

            }

            return false;

        }

        public virtual void TargetRandom(int level = 8)
        {

            Random random = new Random();

            int decision = random.Next(level);

            switch (decision)
            {
                case 0:
                case 1:
                case 2:
                case 3:

                    int newDirection = random.Next(10);

                    if (newDirection >= 8)
                    {

                        newDirection = ModUtility.DirectionToTarget(Position, tether)[2];

                    }

                    List<int> directions = new()
                    {

                        (newDirection + 4) % 8,
                        (newDirection + 6) % 8,
                        (newDirection + 2) % 8,

                    };

                    foreach (int direction in directions)
                    {

                        //ModUtility.LogStrings(new() { Name, "attempting random direction", direction.ToString() });
                        if (PathTarget(occupied*64, 3, 1, direction))
                        {

                            return;

                        }

                    }

                    break;

            }
            //ModUtility.LogStrings(new() { Name, "idle", "random failed" });
            TargetIdle();

        }

        public virtual bool TargetWork()
        {

            return false;

        }

        public virtual void PerformWork()
        {


        }


        // ========================================
        // UPDATE
        // ========================================

        public virtual void UpdateBehaviour()
        {

            UpdateIdle();

            UpdateMove();

            UpdateSweep();

            UpdateSpecial();

            if (cooldownTimer > 0)
            {

                cooldownTimer--;

            }

            if (hitTimer > 0)
            {

                hitTimer--;


            }

            if (lookTimer > 0)
            {

                lookTimer--;

            }

            if (collideTimer > 0)
            {

                collideTimer--;

            }

            if (pushTimer > 0)
            {

                pushTimer--;


            }

            if (dashHeight > 0)
            {

                dashHeight--;

            }

            if(followTimer > 0)
            {

                followTimer--;

            }

        }

        public virtual void UpdateMultiplayer()
        {
            
            if (netHaltActive.Value)
            {

                return;

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

                if (netDashActive.Value)
                {
                    if (dashFrame < (dashCeiling / 2) && dashHeight < 128)
                    {

                        dashHeight += 2;

                    }
                    else if (dashHeight > 1)
                    {

                        dashHeight -= 2;

                    }
                }

                if (moveTimer <= 0)
                {

                    moveFrame++;

                    if (moveFrame >= walkFrames[0].Count)
                    {

                        moveFrame = 1;
                    
                    }

                    moveTimer = moveInterval;

                    moveTimer -= 3;

                    dashFrame++;

                    if (dashFrame > dashCeiling)
                    {

                        dashFrame = dashFloor;

                    }

                    stationaryTimer = 30;

                    idleTimer = 0;

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
            else
            {

                idleTimer++;
            
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

            if (idleTimer > 0)
            {

                idleTimer--;

            }

            if (netHaltActive.Value)
            {

                if (idleTimer <= 0)
                {

                    ClearIdle();

                    ClearMove();

                    return;

                }

            }

        }

        public virtual void UpdateSweep()
        {

            if (sweepTimer > 0)
            {

                sweepTimer--;

            }

            if (netSweepActive.Value)
            {

                if (sweepTimer <= 0)
                {
                    
                    ClearSweep();

                    cooldownTimer = cooldownInterval;

                }
                else
                {

                    if (sweepTimer % sweepInterval == 0)
                    {

                        sweepFrame++;

                        if(sweepFrame % 2 == 1)
                        {

                            List<StardewValley.Monsters.Monster> monsters = ModUtility.MonsterProximity(currentLocation, new() { Position, }, 1.5f);

                            foreach (StardewValley.Monsters.Monster monster in monsters)
                            {

                                DealDamageToMonster(monster,Mod.instance.CombatDamage()/2);

                            }

                        }

                        if (sweepFrame == sweepFrames[0].Count)
                        {

                            ClearSweep();

                        }

                    }

                }

            }

        }

        public virtual void UpdateMove()
        {

            if (moveTimer > 0)
            {

                moveTimer--;

            }

            if (destination == Vector2.Zero)
            {

                ClearMove();

                return;

            }

            float distance = Vector2.Distance(occupied, destination);

            if (moveTimer <= 0)
            {

                moveTimer = FrameSpeed(distance);

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

                    if (distance <= 128)

                    netDashActive.Set(false);

                    netSweepActive.Set(true);

                    dashSweep = false;

                }

            }

            if (netDashActive.Value)
            {
                if (dashFrame < (dashCeiling / 2) && dashHeight < 128)
                {

                    dashHeight += 2;

                }
                else if (dashHeight > 1)
                {

                    dashHeight -= 2;

                }
            }

        }

        public virtual void UpdateSpecial()
        {

            if (specialTimer > 0)
            {

                specialTimer--;

            }

            if (netSpecialActive.Value)
            {

                if (specialTimer <= 0)
                {

                    ClearSpecial();

                    cooldownTimer = cooldownInterval;

                }
                else if (specialTimer % specialInterval == 0)
                {

                    specialFrame++;

                    if(specialFrame > specialCeiling)
                    {

                        specialFrame = specialFloor;

                    }

                }

            }

            if (netWorkActive.Value)
            {
                
                if (specialTimer <= 0)
                {

                    ClearSpecial();

                }

                PerformWork();

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

            tether = roamVectors[roamIndex];

        }
        
        public virtual int FrameSpeed(float distance = 0)
        {

            int speed = moveInterval;

            if (distance > 640)
            {

                speed -= 3;

            }
            else if (distance > 360)
            {

                speed -= 2;

            }

            if (modeActive == mode.scene || modeActive == mode.track)
            {

                speed -= 1;

            }

            return speed;

        }

        public virtual float MoveSpeed(float distance = 0)
        {

            float speed = gait;

            if (distance > 640 || netDashActive.Value)
            {

                speed = gait * 2f;

            }
            else if (distance > 360)
            {

                speed = gait * 1.5f;

            }

            if (modeActive == mode.scene || modeActive == mode.track)
            {

                speed *= 1.25f;

            }

            if (modeActive == mode.roam)
            {

                speed *= 0.75f;

            }

            return speed;

        }

        /*public virtual void MoveTowardsTarget()
        {

            if (targetVectors.Count == 0)
            {

                return;
            
            }

            //------------- Factors

            Vector2 targetPosition;

            targetPosition = targetVectors.Last();

            float distance = Vector2.Distance(targetPosition, Position);

            float speed = MoveSpeed(distance);

            Vector2 movement = ModUtility.MovementWithinTileBounds(Position, targetPosition, speed);

            if (distance <= speed * 1.5f)
            {

                if (moveRetreat)
                {

                    moveRetreat = false;

                }

                targetVectors.RemoveAt(targetVectors.Count - 1);

                Vector2 nextTile = new((int)(targetPosition.X / 64), (int)(targetPosition.Y / 64));

                if (ModUtility.PathMoveForward(currentLocation, nextTile) != Vector2.Zero)
                {

                    CollideMonsters(movement);

                    Position = targetPosition;

                    if (targetVectors.Count > 0)
                    {

                        LookAtTarget(targetVectors.Last());

                    }

                    return;

                }

            }

            if (netSceneActive.Value || moveRetreat)
            {

                Position += movement;

                return;

            }

            //------------- Tile check

            for(int i = 0; i < 1; i++)
            {
                
                if (netDashActive.Value)
                { 
                    break; 
                }

                Vector2 thisTile = new((int)(Position.X / 64), (int)(Position.Y / 64));

                Vector2 movePosition = Position + movement;

                Vector2 nextTile = new((int)(movePosition.X / 64), (int)(movePosition.Y / 64));

                if (thisTile == nextTile)
                {
                    break;
                }

                if (ModUtility.PathMoveForward(currentLocation, nextTile) != Vector2.Zero)
                {

                    break;

                }

                closedVectors.Add(thisTile);

                List<Vector2> paths = ModUtility.PathWalkToward(currentLocation, closedVectors, Position, targetPosition);

                if(paths.Count == 0)
                {

                    List<Vector2>jumps = ModUtility.PathJumpToward(currentLocation, Position, targetPosition, 6);

                    if(jumps.Count == 0)
                    {

                        TargetRandom();

                        break;

                    }

                    netDashActive.Set(true);

                    paths = new() { (jumps.Last()*64 )+ new Vector2(32,32), };

                }

                targetVectors.Clear();

                foreach(Vector2 path in paths)
                {

                    targetVectors.Prepend((path * 64) + new Vector2(32, 32));

                }

                targetVectors.Prepend(targetPosition);

                movement = ModUtility.MovementWithinTileBounds(Position, paths.Last(), speed);

                if (movement == paths.Last())
                {
                    //ModUtility.LogStrings(new() { movement.ToString(), Position.ToString() });
                    Position = movement;

                    targetVectors.RemoveAt(targetVectors.Count - 1);

                    if(targetVectors.Count > 0)
                    {

                        LookAtTarget(paths.Last());

                    }

                    CollideMonsters(movement);

                    return;

                }

            }

            //-------------------------- commit movement

            CollideMonsters(movement);

            Position += movement;

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

        public virtual bool CollideCharacters()
        {
            //------------- Collision check

            collideTimer = 180;

            foreach (NPC NPChar in currentLocation.characters)
            {

                if(Vector2.Distance(Position,NPChar.Position) <= 32f)
                {

                    if(NPChar is StardewDruid.Character.Actor || NPChar is StardewDruid.Character.Dragon || NPChar == this || NPChar is StardewValley.Monsters.Monster)
                    {

                        continue;

                    }

                    if (NPChar is StardewDruid.Character.Character Buddy)
                    {

                        if (Buddy.collidePriority >= collidePriority)
                        {

                            return true;

                        }

                    }
                    else
                    {
                        
                        return true;

                    }

                }   

            }

            return false;

        }*/

        // ========================================
        // MOVEMENT
        // ========================================

        public bool PathTarget(Vector2 target, int importance, int proximity, int direction = -1)
        {

            //ModUtility.LogIntegers(new() { (int)target.X, (int)target.Y, importance, proximity, direction });

            Vector2 center = new Vector2((int)(target.X / 64), (int)(target.Y / 64));

            if(importance == 0)
            {

                destination = center;

                LookAtTarget(destination*64, false);

                return true;

            }

            if(direction == -1)
            {

                direction = ModUtility.DirectionToTarget(Position, target)[2];

            }

            List<Vector2> open = ModUtility.GetOccupiableTilesNearby(currentLocation, center, direction, proximity, 1);

            //ModUtility.LogVectors(open);

            Dictionary<Vector2,int> access = new();

            Dictionary<Vector2, int> points = new();

            bool warp = false;

            bool jump = false;

            int span = 0;

            int accessibility;

            if (open.Count == 0)
            {

                return false;

            }

            foreach(Vector2 lead in open)
            {

                List<Vector2> paths = ModUtility.GetTilesBetweenPositions(currentLocation, lead*64, occupied*64);

                paths.Add(lead);

                foreach(Vector2 point in paths)
                {

                    if (!access.ContainsKey(point))
                    {
                            
                        accessibility = ModUtility.TileAccessibility(currentLocation, point);

                        access[point] = accessibility;

                    }
                    else
                    {

                        accessibility = access[point];

                    }

                    if (accessibility == 2)
                    {

                        if(importance <= 1)
                        {
                                
                            warp = true;

                            continue;
                            
                        }
                            
                        break;

                    }

                    if (accessibility == 1)
                    {

                        if (importance <= 2 && span <= 6)
                        {

                            jump = true;

                            span++;

                            continue;

                        }
                            
                        break;

                    }

                    points[point] = 0;

                    if (warp)
                    {

                        points[point] = 2;

                        span = 0;

                    } else if (jump)
                    {
                        // create wind up
                        if(points.Count >= 2)
                        {
                            KeyValuePair<Vector2, int> previous = points.ElementAt(points.Count - 1);

                            if (previous.Value == 0)
                            {

                                points.Remove(previous.Key);

                            }

                        }

                        points[point] = 1;

                        span = 0;

                    }

                    jump = false;

                    warp = false;

                    if (point == lead)
                    {

                        destination = lead;

                        traversal = points;

                        LookAtTarget(destination * 64, false);

                        return true;

                    }
                }

            }

            return false;

        }

        public virtual void SettleOccupied()
        {

            occupied = new Vector2((int)(Position.X / 64), (int)(Position.Y / 64));

        }

        public virtual void Traverse()
        {

            if (destination == Vector2.Zero || netHaltActive.Value || traversal.Count == 0)
            {

                SettlePosition();

                return;

            }

            KeyValuePair<Vector2,int> target = traversal.First();

            LookAtTarget(target.Key * 64, false);

            if (target.Value == 2)
            {

                ModUtility.AnimateQuickWarp(currentLocation, Position, true);

                Position = target.Key * 64;

                ModUtility.AnimateQuickWarp(currentLocation, Position);

                occupied = target.Key;

                traversal.Remove(target.Key);

            }
            else
            {

                if(target.Value == 1 && !netDashActive.Value)
                {

                    netDashActive.Set(true);

                }

                Position = ModUtility.PathMovement(Position, target.Key*64, MoveSpeed(Vector2.Distance(Position, target.Key * 64)));

                if(Vector2.Distance(Position,target.Key*64) <= 4f)
                {

                    occupied = target.Key;

                    traversal.Remove(target.Key);

                }

            }

            if(occupied == destination || traversal.Count == 0)
            {

                destination = Vector2.Zero;

            }

        }

        public virtual void SettlePosition()
        {

            // Settle position slowly shifts the character towards the set occupied tile
            // This is because Position can be offset by the floating coordinates obtained from traversal
            // and the occupied tile position might not match up

            Vector2 occupation = occupied * 64;

            if (Position != occupation)
            {

                if(Vector2.Distance(Position,occupation) >= 32f)
                {

                    LookAtTarget(occupation, false);

                }

                Position = ModUtility.PathMovement(Position, occupation, 2);

            }

        }

        public virtual bool TightPosition()
        {

            if (destination != Vector2.Zero)
            {

                return false;

            }

            if (Position / 64 == new Vector2((int)(Position.X/64), (int)(Position.Y / 64)))
            {

                return true;

            }

            return false;

        }

        public virtual bool CollideCharacters(Vector2 tile)
        {
            //------------- Collision check

            collideTimer = 180;

            foreach(Farmer farmer in currentLocation.farmers)
            {

                if(farmer.Tile == tile)
                {

                    return false;

                }

            }

            foreach (NPC NPChar in currentLocation.characters)
            {

                if (NPChar is StardewDruid.Character.Actor || NPChar is StardewDruid.Character.Dragon || NPChar == this || NPChar is StardewValley.Monsters.Monster)
                {

                    continue;

                }

                if (NPChar is StardewDruid.Character.Character Buddy)
                {

                    Vector2 check = Buddy.destination != Vector2.Zero ? Buddy.destination : Buddy.occupied;

                    if(tile == check)
                    {

                        if (Buddy.collidePriority > collidePriority)
                        {

                            return true;

                        }

                    }

                }
                else if(!NPChar.isMoving() && NPChar.Tile == tile)
                {

                    return true;

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

            Vector2 warppoint = new Vector2(-1);

            if(modeActive == mode.track)
            {

                if (Mod.instance.trackRegister[Name].WarpToTarget(false))
                {

                    return;
                
                }

            }

            if (currentLocation is MineShaft)
            {

                warppoint = WarpData.WarpXZone(currentLocation, targetVector);

                if (warppoint.X < 0)
                {

                    for(int i = 0; i < 5; i++)
                    {

                        if(i == 4)
                        {

                            WarpToDefault(false);

                        }

                        if(ModUtility.GroundCheck(currentLocation, new Vector2((int)(warppoint.X / 64), (int)(warppoint.Y / 64)),true) == "ground")
                        {

                            break;

                        }

                        warppoint += new Vector2(0, 64);

                    }

                    Position = warppoint;

                    SettleOccupied();

                    Mod.instance.Monitor.Log(Name + " warped to the entrance of " + currentLocation.DisplayName + " because they got stuck", LogLevel.Debug);

                    ModUtility.AnimateQuickWarp(currentLocation, Position);

                    return;

                }

            }
            else
            {

                warppoint = WarpData.WarpStart(currentLocation.Name);

                if (warppoint.X < 0)
                {

                    warppoint = WarpData.WarpEntrance(currentLocation, targetVector);

                }

                if (warppoint.X >= 0)
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

                        Vector2 warppointTile = new Vector2((int)(warppoint.X / 64), (int)(warppoint.Y / 64));

                        string groundCheck = ModUtility.GroundCheck(currentLocation, warppointTile, true);

                        if (groundCheck == "ground")
                        {
                            
                            break;

                        }

                        warppoint += centerMovement;

                    }

                    Position = warppoint;

                    SettleOccupied();

                    if (currentLocation is not FarmCave)
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

            SettleOccupied();

            if (updateAfter)
            {
                
                //ModUtility.LogStrings(new() { Name, "idle", "warp default" });
                
                TargetIdle(120);

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

            SettleOccupied();

            switch (previousMode)
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

            TetherMiddle();

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

        public virtual void DealDamageToMonster(StardewValley.Monsters.Monster monsterCharacter,int damage = -1,bool push = true)
        {

            if (!ModUtility.MonsterVitals(monsterCharacter, currentLocation))
            {

                return;

            }

            if (damage == -1)
            {

                damage = Mod.instance.CombatDamage();

            }
                
            List<int> pushList = new() { 0, 0 };

            if (push)
            {

                pushList = ModUtility.CalculatePush(currentLocation, monsterCharacter, Position);

            }

            ModUtility.HitMonster(currentLocation, Game1.player, monsterCharacter, damage, false, diffX: pushList[0], diffY: pushList[1]);

        }

        public virtual void SummonToPlayer(Vector2 position)
        {

            if (modeActive == mode.roam && !roamSummon && currentLocation is Farm)
            {

                summonVector = position;

                roamSummon = true;

                roamLapse = Game1.currentGameTime.TotalGameTime.TotalMinutes + 0.5;

            }

        }

        public virtual void TetherMiddle()
        {
            
            tether = new((int)(currentLocation.map.Layers[0].LayerWidth / 2), (int)(currentLocation.map.Layers[0].LayerHeight / 2));


        }

        public virtual List<Chest> CaveChests()
        {

            List<Chest> chests = new();

            GameLocation farmcave = Game1.getLocationFromName("FarmCave");

            int chestCount = 0;

            foreach (Dictionary<Vector2, StardewValley.Object> dictionary in farmcave.Objects)
            {

                foreach (KeyValuePair<Vector2, StardewValley.Object> keyValuePair in dictionary)
                {

                    if (keyValuePair.Value is Chest foundChest)
                    {

                        chests.Add(foundChest);

                        if (chestCount == 2)
                        {

                            break;

                        }

                        chestCount++;

                    }

                }

            }

            return chests;

        }


    }

}
