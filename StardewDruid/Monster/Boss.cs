
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Companions;
using StardewValley.Events;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Monsters;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using static StardewDruid.Cast.SpellHandle;


namespace StardewDruid.Monster
{
    public class Boss : StardewValley.Monsters.Monster
    {

        public NetString realName = new NetString();

        public int moveDirection;
        public int altDirection;

        public bool localMonster;
        public bool loadedOut;
        public Texture2D characterTexture;
        public int combatModifier;
        public NetInt netDirection = new NetInt(0);
        public NetInt netAlternative = new NetInt(0);
        public NetInt netLayer = new NetInt(0);

        // ============================= Differentiation

        public int baseMode;
        public int baseJuice;
        public float debuffJuice;
        public int basePulp;
        public int threat;
        public bool groupMode;
        public NetInt netMode = new NetInt(0);
        public NetBool netPosturing = new NetBool(false);
        public string lightId = string.Empty;

        public NetInt netScheme = new(0);
        public Dictionary<int, List<Rectangle>> schemeFrames = new();

        public enum difficulty
        {
            basic,
            medium,
            hard,
            miniboss,
            boss,
            thief,
        }

        public enum temperment
        {

            cautious,
            coward,
            aggressive,
            odd,
            random,
            ranged,

        }

        public temperment tempermentActive;

        public IconData.warps warp = IconData.warps.death;

        //public bool nextCritical;

        // ============================= Behaviour

        public Dictionary<int, List<Rectangle>> idleFrames = new();
        public int idleTimer;
        public NetBool netHaltActive = new NetBool(false);
        public int idleFrame;
        public NetBool netAlert = new NetBool(false);

        public NetBool netWoundedActive = new NetBool(false);
        public Dictionary<int, List<Rectangle>> woundedFrames = new();
        public bool setWounded;

        // ============================= Follow behaviour

        public bool inMotion;

        public Vector2 pushVector;
        public int pushTimer;

        public Vector2 followIncrement;
        public int followTimer;
        public float gait;

        public Dictionary<int, List<Rectangle>> walkFrames = new();
        public int walkFrame;
        public int walkTimer;
        public bool walkSwitch;
        public bool walkSide;
        public int walkLeft;
        public int walkRight;
        public int walkInterval;
        public Vector2 setPosition;
        public int stationaryTimer;

        public Vector2 tether;
        public float tetherLimit;
        public Vector2 stuck;
        public int stuckLimit;

        public bool cooldownActive;
        public int cooldownTimer;
        public int cooldownInterval;
        public int talkTimer;

        // ============================= Sweep

        public bool sweepSet;
        public int sweepFrame;
        public int sweepTimer;
        public NetBool netSweepActive = new NetBool(false);
        public Dictionary<int, List<Rectangle>> sweepFrames = new();
        public int sweepInterval;
        public Vector2 sweepIncrement;

        // ============================= Flight

        public NetBool netFlightActive = new NetBool(false);
        public NetBool netSmashActive = new NetBool(false);
        public Dictionary<int, List<Rectangle>> flightFrames = new();
        public bool smashSet;
        public Dictionary<int, List<Rectangle>> smashFrames = new();
        public Dictionary<int, List<Rectangle>> alertFrames = new();

        public bool flightSet;
        public int flightFrame; // current animation frame of flight
        public int flightTimer; // current tick of flight
        public int flightInterval; // flight timer intervals to adjust frame speed
        public int flightTotal; // amount of ticks of full flight
        
        public Vector2 flightFrom; // origin of flight path
        public Vector2 flightTo; // destination of flight path
        public Vector2 flightIncrement; // how much to shift position toward destination
        public int flightSpeed; // how fast to travel during flight
        public bool flightFlip; // flips flight animation
        public int flightPeak; // peak height during flight
        public int flightHeight; // tracked height during flight
        public NetInt netFlightProgress = new NetInt(0);
        public int flightSegment;
        public int trackFlightProgress;

        public enum flightTypes
        {
            none,
            close,
            past,
            away,
            circle,
            target,

        }
        public flightTypes flightDefault; // determines whether to fly at, over or near target

        // ============================= Special attack

        public bool specialSet;
        public int specialFrame;
        public int specialTimer;
        public NetBool netSpecialActive = new NetBool(false);
        public Dictionary<int, List<Rectangle>> specialFrames = new();
        public Dictionary<int, List<Rectangle>> channelFrames = new();
        public int specialCeiling;
        public int specialFloor;
        public int specialInterval;

        // ============================= Barrage attack

        public bool channelSet;
        public NetBool netChannelActive = new NetBool(false);
        public int channelCeiling;
        public int channelFloor;

        // ============================= Shield ability

        public IconData.schemes shieldScheme = IconData.schemes.none;
        public NetBool netShieldActive = new NetBool(false);
        public int shieldTimer;

        public Boss()
        {
        }

        public Boss(Vector2 vector, int CombatModifier, string name = "Boss")
          : base("Pepper Rex", new(vector.X * 64, vector.Y * 64))
        {

            realName.Set(name);
            localMonster = true;
            combatModifier = CombatModifier;
            Name = SpawnData.MonsterSlayer(name);

            //=================== base fields

            focusedOnFarmers = true;
            objectsToDrop.Clear();
            breather.Value = false;
            hideShadow.Value = true;
            DamageToFarmer = 0;

            //=================== reconfigurable fields

            SetBase();

            LoadOut();

            SetMode(baseMode);

            Halt();

            idleTimer = 30;

            tether = Vector2.Zero;

            stuck = Vector2.Zero;

        }

        public virtual void SetBase()
        {

            tempermentActive = temperment.cautious;

            baseMode = 2;
            baseJuice = 4;
            basePulp = 25;
            cooldownInterval = 180;

        }

        protected override void initNetFields()
        {
            base.initNetFields();
            NetFields.AddField(realName, "realName");
            NetFields.AddField(netMode, "netMode");
            NetFields.AddField(netPosturing, "netPosturing");
            NetFields.AddField(netHaltActive, "netHaltActive");
            NetFields.AddField(netDirection, "netDirection");
            NetFields.AddField(netAlternative, "netAlternative");
            NetFields.AddField(netFlightActive, "netFlightActive");
            NetFields.AddField(netFlightProgress, "netFlightProgress");
            NetFields.AddField(netSpecialActive, "netSpecialActive");
            NetFields.AddField(netChannelActive, "netChannelActive");
            NetFields.AddField(netSweepActive, "netSweepActive");
            NetFields.AddField(netWoundedActive, "netWoundedActive");
            NetFields.AddField(netShieldActive, "netShieldActive");
            NetFields.AddField(netScheme, "netScheme");
            NetFields.AddField(netLayer, "netLayer");

        }
        
        public virtual int LayerOffset()
        {
            
            return netLayer.Value;

        }

        public virtual float GetScale()
        {

            float spriteScale = 3.25f + (0.25f * netMode.Value);

            return spriteScale;

        }

        public virtual int GetHeight()
        {

            return 32;

        }

        public virtual int GetWidth()
        {

            return 32;

        }

        public virtual float GetGait(int mode = 0)
        {

            return 2f + 0.1f * mode;

        }

        public virtual Vector2 GetPosition(Vector2 localPosition, float spriteScale = -1f)
        {

            if (spriteScale == -1f)
            {
                
                spriteScale = GetScale();

            }

            int width = GetWidth();

            int height = GetHeight();

            Vector2 spritePosition = localPosition + new Vector2(32,64) - new Vector2(0, height / 2 * spriteScale);//new Vector2((width / 2f) * spriteScale, height / 2 * spriteScale);

            if (netFlightActive.Value || netSmashActive.Value)
            {

                spritePosition.Y -= flightHeight;

            }

            return spritePosition;

        }

        public override Rectangle GetBoundingBox()
        {

            Vector2 spritePosition = GetPosition(Position);

            float spriteScale = GetScale();

            int width = GetWidth() - 4;

            int height = GetHeight() - 4;

            float scaleWidth = spriteScale * width;

            float scaleHeight = spriteScale * height;

            Rectangle box =  new(
                (int)(spritePosition.X - (scaleWidth / 2)), 
                (int)(spritePosition.Y - (scaleHeight / 2)), 
                (int)scaleWidth, 
                (int)scaleHeight
            );

            return box;

        }

        public virtual void SetMode(int mode)
        {

            netMode.Set(mode);

            DamageToFarmer = 0;

            switch (mode)
            {

                case 0: // small mode

                    MaxHealth = combatModifier * basePulp;

                    Health = MaxHealth;

                    experienceGained.Set(10);

                    gait = GetGait(0);

                    break;

                case 1: // slightly bigger

                    MaxHealth = combatModifier * basePulp * 3;

                    Health = MaxHealth;

                    experienceGained.Set(20);

                    gait = GetGait(1);

                    break;

                default:
                case 2: // multiple bosses

                    MaxHealth = combatModifier * basePulp * 7;

                    Health = MaxHealth;

                    experienceGained.Set(50);

                    gait = GetGait(2);

                    break;

                case 3: // single boss

                    MaxHealth = combatModifier * basePulp * 11;

                    Health = MaxHealth;

                    experienceGained.Set(100);

                    gait = GetGait(3);

                    break;

                case 4: // hard boss

                    MaxHealth = combatModifier * basePulp * 15;

                    Health = MaxHealth;

                    experienceGained.Set(200);

                    gait = GetGait(4);

                    break;

            }

            GetThreat();

        }

        public virtual int GetThreat()
        {
            
            switch (netMode.Value)
            {

                case 0: // small mode

                    threat = Math.Max(baseJuice * 2, (int)((float)combatModifier * 0.6f));

                    break;

                case 1: // slightly bigger

                    threat = Math.Max(baseJuice * 3, (int)((float)combatModifier * 0.8f));

                    break;

                default:
                case 2: // multiple bosses

                    threat = Math.Max(baseJuice * 4, combatModifier);

                    break;

                case 3: // single boss

                    threat = Math.Max(baseJuice * 6, (int)(combatModifier * 1.2f));

                    break;

                case 4: // hard boss

                    threat = Math.Max(baseJuice * 8, (int)(combatModifier * 1.6f));

                    break;

                case 5: // mega boss

                    threat = Math.Max(baseJuice * 10, (int)(combatModifier * 2f));

                    break;

            }

            if(debuffJuice > 0f)
            {

                threat = (int)((float)threat * debuffJuice);

            }

            return threat;

        }

        public virtual void RandomTemperment()
        {

            switch (Mod.instance.randomIndex.Next(3))
            {

                case 1:

                    tempermentActive = temperment.cautious;

                    break;

                case 2:

                    tempermentActive = temperment.odd;

                    break;

                default:

                    tempermentActive = temperment.aggressive;

                    break;

            }

        }

        public virtual void LoadOut()
        {

            BaseWalk();

            BaseFlight();

            BaseSpecial();

            loadedOut = true;

        }

        public void BaseWalk()
        {

            characterTexture = MonsterHandle.MonsterTexture(realName.Value);

            walkInterval = 9;

            gait = GetGait();

            idleFrames = FrameSeries(32, 32,0,0,1);

            walkFrames = FrameSeries(32, 32,0,0,7);

        }

        public void BaseFlight()
        {

            flightInterval = 9;

            flightSpeed = 8;

            flightPeak = 192;

            flightFrames = walkFrames;

            flightDefault = flightTypes.past;

            smashFrames = flightFrames;

        }

        public virtual void BaseSpecial()
        {

            specialCeiling = 6;

            specialFloor = 1;

            channelCeiling = 6;

            channelFloor = 1;

            specialInterval = 12;

            specialFrames = walkFrames;

            channelFrames = walkFrames;

            sweepInterval = 12;

            sweepFrames = walkFrames;

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {
            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            float drawLayer = (Position.Y + (float)LayerOffset()) / 10000f;

            int adjustDirection = netDirection.Value == 3 ? 1 : netDirection.Value;

            DrawEmote(b, localPosition, drawLayer);

            b.Draw(
                characterTexture, 
                localPosition + new Vector2(32,64 - (16f*GetScale())), 
                new Rectangle?(walkFrames[adjustDirection][walkFrame]), 
                Color.White, 
                0.0f, 
                new Vector2(GetWidth()/2, GetHeight()/2), 
                GetScale(),
                (netDirection.Value % 2 == 0 && netAlternative.Value == 3) || netDirection.Value == 3 ? (SpriteEffects)1 : 0, 
                drawLayer
            );

        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {

            DrawTextAboveHead(b);

        }

        public virtual void DrawBoundingBox(SpriteBatch b)
        {

            Microsoft.Xna.Framework.Rectangle container = GetBoundingBox();

            Vector2 localPosition = Game1.GlobalToLocal(new Vector2(container.X, container.Y));

            b.Draw(Game1.staminaRect, localPosition, new Rectangle(0, 0, container.Width, container.Height), Color.Blue, 0f, Vector2.Zero, 1f, 0, 0.0001f);

            Vector2 actualPosition = Game1.GlobalToLocal(Position);

            b.Draw(Game1.staminaRect, actualPosition, new Rectangle(0, 0, 64, 64), Color.Red, 0f, Vector2.Zero, 1f, 0, 0.0002f);

        }

        public virtual void DrawEmote(SpriteBatch b, Vector2 localPosition, float drawLayer)
        {

            if (IsEmoting && !Game1.eventUp)
            {

                localPosition.Y -= 32 + Sprite.SpriteHeight * 4;

                b.Draw(Game1.emoteSpriteSheet, localPosition, new Rectangle?(new Rectangle(CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16)), Color.White, 0.0f, Vector2.Zero, 4f, 0, drawLayer);

            }

        }

        public virtual void DrawTextAboveHead(SpriteBatch b)
        {

            if (textAboveHeadTimer <= 0 || textAboveHead == null)
            {
                return;
            }

            Rectangle bounding = GetBoundingBox();

            Vector2 topCenter = new Vector2(bounding.Center.X, bounding.Top);

            Vector2 topPosition = Game1.GlobalToLocal(topCenter);

            SpriteText.drawStringWithScrollCenteredAt(
                b, 
                textAboveHead, 
                (int)topPosition.X,
                (int)topPosition.Y - 128, 
                "", 
                textAboveHeadAlpha, 
                textAboveHeadColor, 
                1, 
                (float)(Tile.Y * 64 / 10000.0 + 1.0 / 1000.0 + Tile.X / 10000.0), 
                false
                );

        }

        public virtual Dictionary<int, List<Rectangle>> FrameSeries(int width, int height, int startX = 0, int startY = 0, int length = 6, Dictionary<int, List<Rectangle>> frames = null)
        {

            if (frames == null)
            {
                frames = new()
                {
                    [0] = new(),
                    [1] = new(),
                    [2] = new(),
                    [3] = new(),
                };
            }

            Dictionary<int, int> normalSequence = new()
            {
                [0] = 2,
                [1] = 1,
                [2] = 0,
                [3] = 3
            };

            foreach (KeyValuePair<int, int> keyValuePair in normalSequence)
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

        public virtual SpellHandle.Effects IsCursable(SpellHandle.Effects effect = SpellHandle.Effects.knock)
        {

            switch (effect)
            {
                case Effects.knock:
                case Effects.morph:
                case Effects.mug:
                case Effects.doom:

                    return SpellHandle.Effects.daze;

            }

            return effect;

        }
        
        //=================== overriden base fields

        public override List<Item> getExtraDropItems()
        {
            
            return new List<Item>();
        
        }

        public override int takeDamage(int damage, int xTrajectory, int yTrajectory, bool isBomb, double addedPrecision, Farmer who)
        {

            if (netPosturing.Value || netWoundedActive.Value || netShieldActive.Value)
            {
                
                if (netChannelActive.Value)
                {

                    ClearSpecial();

                }

                return 0;

            }

            if (!Context.IsMainPlayer && !localMonster)
            {

                damage = Math.Min(damage, Health - 2);

                if (damage <= 0)
                {

                    return -1;

                }

                Health -= damage;

                return damage;

            }

            damage = damage < 1 ? 1 : damage;

            Health -= damage;

            if (ValidPush())
            {

                pushVector = new(xTrajectory, yTrajectory);

                pushTimer = 4;

            }

            if (Health <= 0)
            {

                if (setWounded)
                {

                    ResetActives();

                    netWoundedActive.Set(true);

                    Health = MaxHealth;

                    return damage;

                }

                deathIsNoEscape();

            }

            return damage;

        }

        public virtual void deathIsNoEscape()
        {

            Microsoft.Xna.Framework.Rectangle box = GetBoundingBox();

            SpellHandle death = new(new(box.Center.X, box.Top), 64 + (64* (int)GetScale()), IconData.impacts.deathbomb, new()) { displayRadius = 3, };

            Mod.instance.spellRegister.Add(death);

        }

        public override void shedChunks(int number, float scale)
        {

            float size = 2f;

            if(walkFrames.Count > 0)
            {

                if (walkFrames[0][0].Width > 32) { size += 1f; }

            }

            Mod.instance.iconData.ImpactIndicator(currentLocation, Position, IconData.impacts.flashbang, size, new() { frame = 3, frames = 5, interval = 50, });

        }

        public override bool isInvincible()
        {

            if (netChannelActive.Value)
            {

                return false;

            }

            if (netPosturing.Value)
            {

                return true;

            }

            if(invincibleCountdown > 0)
            {

                return true;

            }

            return false;

        }


        //=================== behaviour methods

        public override void Halt()
        {

            if (!localMonster)
            {
                return;
            }

            ResetActives();

            netHaltActive.Set(true);

            idleTimer = 60;

        }

        public virtual void ResetActives()
        {

            moveDirection = 2;

            netDirection.Set(2);

            altDirection = 1;

            netAlternative.Set(1);

            ClearIdle();

            ClearMove();

            ClearSpecial();

        }

        public virtual void ResetFrames()
        {

            idleFrame = 0;

            walkFrame = 0;

            flightFrame = 0;

            sweepFrame = 0;

            specialFrame = 0;

        }

        public virtual void ClearIdle()
        {

            if (netHaltActive.Value)
            {

                netHaltActive.Set(false);

                netAlert.Set(false);

            }

            idleFrame = 0;

            idleTimer = 540;

        }

        public virtual void ClearMove()
        {

            if (netFlightActive.Value)
            {

                netFlightActive.Set(false);
                
                netFlightProgress.Set(0);
            
            }

            if (netSmashActive.Value)
            {

                netSmashActive.Set(false);
                
                netFlightProgress.Set(0);
            
            }

            flightTimer = 0;

            flightFrame = 0;

            if (netSweepActive.Value)
            {

                netSweepActive.Set(false);

            }

            sweepTimer = 0;

            sweepFrame = 0;

            walkFrame = 0;

            walkTimer = 0;

            followIncrement = Vector2.Zero;

        }

        public virtual void ClearSpecial()
        {
            
            if (netSpecialActive.Value)
            {

                netSpecialActive.Set(false);

            }

            if (netChannelActive.Value)
            {

                netChannelActive.Set(false);

            }

            specialTimer = 0;

            specialFrame = 0;

            //cooldownTimer = 0;

        }

        public virtual void ClearShield(bool reset = true)
        {

            netShieldActive.Set(false);

            if (reset)
            {

                shieldTimer = 0;

            }

        }

        public void LookAtTarget(Vector2 target)
        {

            int moveDirection;

            int altDirection;

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

            netDirection.Set(moveDirection);

            netAlternative.Set(altDirection);

            FacingDirection = netDirection.Value;

        }

        public virtual void LookAtFarmer()
        {

            if(currentLocation == null)
            {

                return;

            }

            List<Farmer> targets = ModUtility.FarmerProximity(currentLocation, new() { Position, }, 15 * 64);

            if (targets.Count > 0)
            {

                LookAtTarget(targets.First().Position);

            }

        }

        public virtual void TalkSmack()
        {

            if (talkTimer > (int)Game1.currentGameTime.TotalGameTime.TotalSeconds || textAboveHeadTimer > 0)
            {
                return;
            }

        }

        public virtual bool baseupdate(GameTime time, GameLocation location)
        {
            
            if (localMonster)
            {

                currentLocation = location;

            }

            if (!location.farmers.Any())
            {

                return false;

            }

            if (invincibleCountdown > 0)
            {

                invincibleCountdown -= time.ElapsedGameTime.Milliseconds;

                if (invincibleCountdown <= 0)
                {

                    stopGlowing();

                }

            }

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

                        float newAlpha = textAboveHeadAlpha - 0.04f;

                        textAboveHeadAlpha = newAlpha < 0f ? 0f : newAlpha;

                    }

                }

            }

            updateGlow();

            updateEmote(time);

            updateFaceTowardsFarmer(time, location);

            if (localMonster)
            {

                if (stunTime.Value > 0)
                {

                    stunTime.Set(stunTime.Value - (int)time.ElapsedGameTime.TotalMilliseconds);

                    if (!netHaltActive.Value)
                    {
                        
                        Halt();
                        
                        idleTimer = stunTime.Value;
                    
                    }

                    return false;

                }

            }

            return true;

        }

        public override void update(GameTime time, GameLocation location)
        {

            if (!loadedOut)
            {

                LoadOut();

            }

            if (Mod.CasterGone() || Mod.instance.CasterBusy() || Mod.CasterMenu())
            {
                return;
            }

            if (!baseupdate(time, location))
            {

                return;

            };

            if (netWoundedActive.Value)
            {

                return;

            }

            inMotion = false;

            if (!localMonster)
            {

                UpdateMultiplayer();

                return;

            }

            ChooseBehaviour();

            bool notBusy = true;

            if (netSweepActive.Value)
            {

                UpdateSweep();

                notBusy = false;

            }

            if (netFlightActive.Value || netSmashActive.Value)
            {

                UpdateFlight();

                notBusy = false;

            }

            if (netSpecialActive.Value || netChannelActive.Value)
            {

                UpdateSpecial();

                notBusy = false;

            }

            if (cooldownActive && notBusy)
            {

                UpdateCooldown();

            }

            if (netHaltActive.Value || netAlert.Value)
            {

                UpdateHalt();

            }

            UpdatePush();

            UpdateWalk();

        }

        public virtual void UpdateMultiplayer()
        {

            if (netSweepActive.Value)
            {

                inMotion = true;

                sweepTimer--;

                if (sweepTimer <= 0)
                {

                    sweepFrame++;

                    sweepTimer = sweepInterval;

                }

                return;

            }
            else
            {
                sweepFrame = 0;

                sweepTimer = flightInterval;

            }

            if (netFlightActive.Value || netSmashActive.Value)
            {

                inMotion = true;

                if (netFlightProgress.Value != trackFlightProgress)
                {

                    flightFrame = 0;

                    trackFlightProgress = netFlightProgress.Value;

                    flightTimer = flightInterval;

                }

                flightTimer--;

                if (flightTimer <= 0)
                {

                    flightFrame++;

                    flightTimer = flightInterval;

                }

                if (netFlightProgress.Value == 0 && flightHeight <= flightPeak)
                {

                    flightHeight += 2;

                }
                else if (netFlightProgress.Value == 2 && flightHeight > 0)
                {

                    flightHeight -= Math.Min(flightHeight, 2);

                }

                return;

            }
            else
            {

                if (flightHeight > 0)
                {

                    flightHeight -= Math.Min(flightHeight, 2);

                }

                flightFrame = 0;

                flightTimer = sweepInterval;

            }

            if (netSpecialActive.Value || netChannelActive.Value)
            {

                specialTimer--;

                if (specialTimer <= 0)
                {

                    specialFrame++;

                    if (netSpecialActive.Value)
                    {

                        if (specialFrame > specialCeiling)
                        {
                            specialFrame = specialFloor;
                        }

                    }

                    if (netChannelActive.Value)
                    {

                        if (specialFrame > channelCeiling)
                        {
                            specialFrame = channelFloor;
                        }

                    }

                    specialTimer = specialInterval;

                }

                return;

            }
            else
            {
                specialFrame = 0;

                specialTimer = specialInterval;

            }

            if (setPosition != Position || netDirection.Value != moveDirection || netAlternative.Value != altDirection)
            {

                inMotion = true;

                setPosition = Position;

                moveDirection = netDirection.Value;

                altDirection = netAlternative.Value;

                walkTimer--;

                if (walkTimer <= 0)
                {

                    walkFrame++;

                    if (walkFrame >= WalkCount())
                    {

                        walkFrame = 1;

                    }

                    walkTimer = walkInterval;

                    stationaryTimer = walkInterval * 3;

                }

                return;

            }

            if (stationaryTimer > 0)
            {

                stationaryTimer--;

                if (stationaryTimer == 0)
                {

                    walkFrame = 0;

                    walkTimer = 0;

                }

            }

        }

        public virtual void UpdateHalt()
        {

            idleTimer--;

            if (idleTimer % 20 == 0)
            {

                LookAtFarmer();

            }

            if (idleTimer <= 0)
            {

                ClearIdle();

            }

        }

        public float WalkSpeed()
        {

            return gait + (0.25f * (GetScale() - 1));

        }

        public virtual bool ValidPush()
        {

            if(netMode.Value <= 2)
            {

                return true;

            }

            return false;

        }

        public void UpdatePush()
        {

            if(pushVector != Vector2.Zero)
            {

                if(pushTimer <= 0)
                {

                    pushVector = Vector2.Zero;

                    return;

                }

                Vector2 increment = pushVector * (0.1f * pushTimer);

                Position += increment;

                pushTimer--;

            }

        }

        public virtual void UpdateWalk()
        {

            if (!netFlightActive.Value)
            {

                if (flightHeight > 0)
                {

                    flightHeight--;

                }

            }

            if (shieldScheme != IconData.schemes.none)
            {

                if (shieldTimer > 0)
                {

                    shieldTimer--;

                }

                if (netShieldActive.Value)
                {

                    if (shieldTimer <= 300)
                    {

                        ClearShield();

                    }

                }

            }

            if (lightId != string.Empty)
            {

                Microsoft.Xna.Framework.Rectangle boundingbox = GetBoundingBox();

                if (Game1.currentLightSources.ContainsKey(lightId))
                {

                    Game1.currentLightSources[lightId].position.Set(boundingbox.Center.ToVector2());

                }
                else
                {

                    LightSource light = new LightSource(lightId, LightSource.lantern, boundingbox.Center.ToVector2(), GetHeight()/16, Microsoft.Xna.Framework.Color.Black * 0.5f);

                    Game1.currentLightSources.Add(lightId, light);

                }

            }

            followTimer--;

            if (followIncrement == Vector2.Zero)
            {

                return;

            }

            Position += followIncrement * WalkSpeed();

            inMotion = true;

            walkTimer--;

            if (walkTimer > 0)
            {

                return;

            }

            walkFrame++;

            int right = 1 + ((WalkCount() - 1) / 2);

            if (walkSwitch)
            {

                if (walkFrame == 1)
                {

                    if (walkSide)
                    {

                        walkFrame = right;

                    }

                }

                if (walkFrame == right)
                {

                    walkSide = false;

                }

            }

            if (walkFrame >= WalkCount())
            {

                walkFrame = 1;

                walkSide = true;

            }

            walkTimer = walkInterval;

        }

        public void UpdateSweep()
        {

            sweepTimer--;

            if (sweepTimer <= 0)
            {

                ClearMove();

            }
            else
            {

                Position += sweepIncrement;

                inMotion = true;

                if (sweepTimer == sweepInterval * 2)
                {

                    ConnectSweep();

                }

                if (sweepTimer % sweepInterval == 0)
                {

                    sweepFrame++;

                    if (sweepFrame == SweepCount())
                    {

                        sweepFrame = 0;

                    }



                }

            }

        }

        public void UpdateFlight()
        {

            flightTimer--;

            if (flightTimer <= 0)
            {
                
                bool sweep = false;
                
                if (netSmashActive.Value)
                {

                    sweep = true;

                }

                ClearMove();

                if (sweep)
                {
                    ConnectSweep();
                }

            }
            else
            {

                FlightAscension();

                Position += flightIncrement;

                inMotion = true;

                if (flightTimer % flightSegment != 0)
                {

                    return;

                }

                flightFrame++;

                if (netFlightActive.Value)
                {

                    if (flightTimer + (flightSegment * FlightCount()) <= flightTotal)
                    {

                        if (netFlightProgress.Value != 1)
                        {

                            netFlightProgress.Set(1);

                            flightFrame = 0;

                        }

                    }

                    if (flightTimer <= (flightSegment * FlightCount(2)))
                    {

                        if (netFlightProgress.Value != 2)
                        {

                            netFlightProgress.Set(2);

                            flightFrame = 0;

                        }

                    }

                }

                if (netSmashActive.Value)
                {

                    if (flightTimer + (flightSegment * SmashCount()) <= flightTotal)
                    {

                        if (netFlightProgress.Value != 1)
                        {

                            netFlightProgress.Set(1);

                            flightFrame = 0;

                        }

                    }

                    if (flightTimer <= (flightSegment * SmashCount(2)))
                    {

                        if (netFlightProgress.Value != 2)
                        {

                            netFlightProgress.Set(2);

                            flightFrame = 0;

                        }

                    }

                }

            }

        }

        public virtual void FlightAscension()
        {

            if(flightPeak == 0)
            {

                return;

            }

            float distance = Vector2.Distance(flightFrom, flightTo);

            float length = distance / 2;

            float lengthSq = (length * length);

            float heightFr = 4 * flightPeak;

            float coefficient = lengthSq / heightFr;

            int midpoint = (flightTotal / 2);

            float newHeight = 0;

            if (flightTimer != midpoint)
            {
                float newLength;

                if (flightTimer < midpoint)
                {

                    newLength = length * (midpoint - flightTimer) / midpoint;

                }
                else
                {

                    newLength = (length * (flightTimer - midpoint) / midpoint);

                }

                float newLengthSq = newLength * newLength;

                float coefficientFr = (4 * coefficient);

                newHeight = newLengthSq / coefficientFr;

            }

            flightHeight = flightPeak - (int)newHeight;

        }

        public virtual void UpdateSpecial()
        {

            specialTimer--;

            if (specialTimer <= 0) //|| Game1.player.IsBusyDoingSomething())
            {

                ClearSpecial();

                return;

            }

            if (specialTimer % specialInterval == 0)
            {

                specialFrame++;

                if (netSpecialActive.Value)
                {

                    if (specialFrame > specialCeiling)
                    {
                        specialFrame = specialFloor;
                    }

                }

                if (netChannelActive.Value)
                {

                    if (specialFrame > channelCeiling)
                    {
                        specialFrame = channelFloor;
                    }

                }


            }

        }

        public void SetCooldown(float factor = 1f)
        {

            cooldownActive = true;

            int range = Math.Abs(10 - netMode.Value);

            float offset = 0.5f + (Mod.instance.randomIndex.Next(range) * 0.1f);

            cooldownTimer = (int)((float)cooldownInterval * offset * factor);

        }

        public virtual void UpdateCooldown()
        {

            cooldownTimer--;

            if (cooldownTimer == cooldownInterval * 0.5 && new Random().Next(3) == 0)
            {

                TalkSmack();

            }

            if (cooldownTimer <= 0)
            {

                cooldownActive = false;

            }

        }

        //=================== behaviour control

        public virtual bool ChangeBehaviour()
        {

            if (netHaltActive.Value)
            {

                return false;

            }

            if (netSpecialActive.Value || netChannelActive.Value)
            {

                return false;

            }

            if (netSweepActive.Value)
            {

                return false;

            }

            if (netFlightActive.Value || netSmashActive.Value)
            {

                return false;

            }

            if (followTimer > 0)
            {

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

            if (netPosturing.Value)
            {

                netHaltActive.Value = true;

                idleTimer = 180;

                return;

                //LookAtFarmer();

            }

            Random random = new Random();

            List<Vector2> targets = new();

            List<Farmer> farmers = ModUtility.FarmerProximity(currentLocation, new() { Position, }, 20 * 64);

            if (farmers.Count > 0)
            {
                
                targets.Add(farmers.First().Position);

            }

            if(groupMode)
            {

                List<StardewDruid.Character.Character> characters = ModUtility.CompanionProximity(currentLocation, new() { Position, }, 20 * 64);

                if (characters.Count > 0)
                {

                    targets.Add(characters.First().Position);

                }

            }

            if (targets.Count == 0)
            {

                PerformRandom();

                return;

            }

            Vector2 target = targets[Mod.instance.randomIndex.Next(targets.Count)];

            float threshold = Vector2.Distance(Position, target);

            ResetActives();

            LookAtTarget(target);

            if (!cooldownActive)
            {

                if(ChooseSpecial(threshold, target))
                {

                    return;

                }

            }

            ChooseMovement(threshold, target);

        }

        public virtual bool ChooseSpecial(float threshold, Vector2 target)
        {

            float specialFactor = GetScale();

            if (threshold > 576f + (96f * specialFactor))
            {

                return false;

            }

            if(threshold <= 96 + (32 * specialFactor))
            {

                if (smashSet)
                {

                    return PerformSweep(target);

                }

                return false;

            }

            if (threshold >= 320 + (64 * specialFactor))
            {

                if (channelSet)
                {

                    if (tempermentActive == temperment.ranged || Mod.instance.randomIndex.Next(2) == 0)
                    {

                        if (PerformChannel(target))
                        {

                            return true;

                        }

                    }

                }

                return false;

            }

            switch (Mod.instance.randomIndex.Next(3))
            {

                default:

                    if (specialSet && tempermentActive == temperment.ranged)
                    {

                        return PerformSpecial(target);

                    }

                    if (flightSet && tempermentActive == temperment.aggressive)
                    {

                        return PerformFlight(target);

                    }

                    return false;

                case 1:

                    if (specialSet)
                    {

                        return PerformSpecial(target);

                    }

                    return false;

                case 2:

                    if (flightSet)
                    {

                        return PerformFlight(target);

                    }

                    return false;

            }

        }

        public virtual void ChooseMovement(float threshold, Vector2 target)
        {

            if(tether != Vector2.Zero)
            {

                float checkTether = Vector2.Distance(tether, Position);

                if (checkTether > tetherLimit * 1.5f)
                {

                    PerformWarp(tether);

                    return;

                }

                if (checkTether > tetherLimit)
                {

                    if (flightSet)
                    {

                        if (PerformFlight(tether, flightTypes.circle))
                        {

                            return;

                        }

                    }

                    if (PerformFollow(tether))
                    {

                        return;

                    }

                }

            }

            switch (tempermentActive)
            {

                case temperment.coward:

                    if (PerformRetreat(target))
                    {

                        return;

                    }

                    break;

                case temperment.ranged:

                    float distance = Vector2.Distance(target, Position);

                    if(distance >= 192)
                    {

                        switch (new Random().Next(2))
                        {

                            case 0:

                                if (PerformCircle(target))
                                {

                                    return;

                                }

                                break;

                            case 1:

                                netHaltActive.Value = true;

                                idleTimer = 60;

                                LookAtFarmer();

                                TalkSmack();

                                return;

                        }

                    }

                    if (PerformRetreat(target))
                    {

                        return;

                    }

                    break;

                case temperment.aggressive:

                    switch (Mod.instance.randomIndex.Next(4))
                    {
                        case 1:

                            netHaltActive.Value = true;

                            idleTimer = 60;

                            LookAtFarmer();

                            TalkSmack();

                            return;

                        case 2:

                            if (PerformCircle(target))
                            {

                                return;

                            }

                            break;

                    }

                    if (PerformFollow(target))
                    {

                        return;

                    }
      
                    break;

                case temperment.odd:

                    switch (new Random().Next(4))
                    {

                        case 0: 
                            
                            if (PerformCircle(target))
                            { 
                                
                                return; 
                            
                            }  
                            
                            break;

                        case 1: 
                            
                            if (PerformRetreat(target))
                            { 
                                
                                return; 
                            
                            } 
                            
                            break;

                    }

                    break;

                default:

                    switch (Mod.instance.randomIndex.Next(4))
                    {
                        case 1:

                            netHaltActive.Value = true;

                            idleTimer = 30;

                            LookAtFarmer();

                            TalkSmack();

                            return;

                        case 2:

                            if (threshold <= 320)
                            {

                                if (PerformCircle(target))
                                {

                                    return;

                                }

                            }

                            break;

                        case 3:

                            if (PerformFollow(target))
                            {

                                return;

                            }

                            break;

                    }

                    break;


            }

            PerformRandom();

        }

        public virtual bool PerformSweep(Vector2 target)
        {

            if (!sweepSet) { return false; }

            List<Farmer> targets = ModUtility.FarmerProximity(currentLocation, new() { Position, }, 96f + (GetWidth() * 3));

            if (targets.Count > 0)
            {

                Vector2 targetFarmer = targets.First().Position;

                LookAtTarget(targetFarmer);

                sweepTimer = sweepInterval * SweepCount();

                SetCooldown(0.5f);

                float limit = 32f + GetWidth();

                float distance = Vector2.Distance(targetFarmer, Position);

                if (distance > limit)
                {

                    float sweepDistance = (distance - limit) / sweepTimer;

                    sweepIncrement = ModUtility.PathFactor(Position, targetFarmer) * sweepDistance;

                }

                netSweepActive.Set(true);

                return true;

            }

            return false;

        }

        public virtual void ConnectSweep()
        {

            Microsoft.Xna.Framework.Rectangle box = GetBoundingBox();

            Microsoft.Xna.Framework.Vector2 sweepVector = new(box.Center.X, box.Top);

            switch (netDirection.Value)
            {

                case 1:

                    sweepVector = new(box.Right, box.Center.Y);

                    break;

                case 2:

                    sweepVector = new(box.Center.X, box.Bottom);

                    break;

                case 3:


                    sweepVector = new(box.Left, box.Center.Y);

                    break;

            }

            List<Farmer> targets = ModUtility.FarmerProximity(currentLocation, new() { sweepVector, }, GetWidth() * GetScale() * 0.7f);

            if(targets.Count > 0)
            {

                ModUtility.DamageFarmers(targets, GetThreat(), this);

            }

        }

        public virtual bool PerformFlight(Vector2 target, flightTypes flightType = flightTypes.none)
        {

            bool smash = false;

            if (flightType == flightTypes.none)
            {

                switch (tempermentActive)
                {

                    case temperment.coward:


                        flightType = flightTypes.away;

                        break;

                    case temperment.ranged:

                        flightType = flightTypes.circle;

                        break;

                    default:

                        flightType = flightDefault;

                        break;

                }
            }

            switch (flightType)
            {
                case flightTypes.close: // dash close to target

                    List<Vector2> closeTargets = ModUtility.GetOccupiableTilesNearby(currentLocation, ModUtility.PositionToTile(target), (ModUtility.DirectionToTarget(Position, target)[2] + 4) % 8, 1, 2);

                    if(closeTargets.Count > 0)
                    {

                        flightTo = closeTargets.First() * 64;

                    }

                    if (smashSet)
                    {

                        smash = true;

                    }

                    break;

                case flightTypes.past: // dash past target

                    List<Vector2> farTargets = ModUtility.GetOccupiableTilesNearby(currentLocation, ModUtility.PositionToTile(target), ModUtility.DirectionToTarget(Position, target)[2], 2, 3);

                    if (farTargets.Count > 0)
                    {

                        flightTo = farTargets.First() * 64;

                    }

                    if (smashSet)
                    {

                        smash = true;

                    }

                    break;

                case flightTypes.away: // get away from target

                    List<Vector2> retreatTargets = ModUtility.GetOccupiableTilesNearby(currentLocation, ModUtility.PositionToTile(Position), (ModUtility.DirectionToTarget(Position, target)[2] + 4) % 8, 5, 3);

                    if (retreatTargets.Count > 0)
                    {

                        flightTo = retreatTargets.Last() * 64;

                    }
                    else
                    {

                        return false;

                    }

                    break;

                case flightTypes.circle: // circle target

                    int direction = ModUtility.DirectionToTarget(Position, target)[2];

                    int offset = Mod.instance.randomIndex.Next(2) == 0 ? 2 : -2;

                    int tangent = (direction + 8 + offset) % 8;

                    int distance = Math.Min(6, (int)Vector2.Distance(ModUtility.PositionToTile(Position), ModUtility.PositionToTile(target)));

                    List<Vector2> circleTargets = ModUtility.GetOccupiableTilesNearby(currentLocation, ModUtility.PositionToTile(target), tangent, distance, 2);

                    if(circleTargets.Count > 0)
                    {

                        flightTo = circleTargets.First() * 64;

                    }
                    else
                    {

                        return false;

                    }

                    break;

                default:

                    flightTo = target;

                    if (smashSet)
                    {

                        smash = true;

                    }

                    break;

                case flightTypes.target:

                    flightTo = target;

                    break;

            }

            SetCooldown(1);

            if (smash)
            {

                netSmashActive.Set(true);

            }
            else
            {

                netFlightActive.Set(true);

            }

            flightFrom = new(Position.X, Position.Y);

            flightIncrement = ModUtility.PathFactor(Position, flightTo) * flightSpeed;

            flightTimer = (int)(Vector2.Distance(Position, flightTo) / Vector2.Distance(new(0, 0), flightIncrement));

            flightTotal = flightTimer;

            flightSegment = flightInterval;

            int pathRequirement;

            if (!smash)
            {

                pathRequirement = FlightCount(3); 
            }
            else
            {
                pathRequirement = SmashCount(3);

            }

            int pathSqueeze = (int)(flightTimer / pathRequirement);

            if (pathSqueeze < flightInterval)
            {

                flightSegment = Math.Max(6,pathSqueeze);

            }

            return true;

        }

        public virtual List<Vector2> BlastZero(int randomisation = -1)
        {

            Vector2 centerPosition = GetBoundingBox().Center.ToVector2();

            Vector2 tile = new Vector2((int)(centerPosition.X / 64), (int)(centerPosition.Y / 64));

            Vector2 zero = tile;

            Vector2 start = tile;

            List<Vector2> zeroes = new();

            switch (netDirection.Value)
            {
                case 0:

                    zero.X += 3;

                    zero.Y -= 4;

                    if (netAlternative.Value == 3)
                    {
                        zero.X -= 6;

                        break;

                    }

                    start.Y -= 1;

                    break;

                case 1:

                    zero.X += 6;

                    start.X += 1;

                    break;

                case 2:

                    zero.X += 3;

                    zero.Y += 4;

                    if (netAlternative.Value == 3)
                    {
                        zero.X -= 6;
                        break;

                    }

                    start.X += 1;

                    break;

                default:

                    zero.X -= 6;

                    start.X -= 1;
                    break;

            }

            if (randomisation != -1)
            {
                Random random = new();

                zero.X -= randomisation;
                zero.X += random.Next(randomisation * 2);
                zero.Y -= randomisation;
                zero.Y += random.Next(randomisation * 2);

            }

            zeroes.Add(zero);

            zeroes.Add(start);

            return zeroes;

        }

        public virtual bool PerformSpecial(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1f);

            TalkSmack();

            return true;

        }

        public virtual Vector2 PerformRedirect(Vector2 target)
        {

            return target;

        }

        public virtual bool PerformFollow(Vector2 target)
        {

            target = PerformRedirect(target);

            LookAtTarget(target);

            followIncrement = ModUtility.PathFactor(Position, target);

            followTimer = (WalkCount() -1) * walkInterval;

            if (Vector2.Distance(Position,target) <= 32)
            {

                followTimer *= 2;

            }

            return true;

        }

        public virtual bool PerformRetreat(Vector2 target)
        {

            int tangent = (ModUtility.DirectionToTarget(Position, target)[2] + 4) % 8;

            List<Vector2> retreatTargets = ModUtility.GetOccupiableTilesNearby(currentLocation, ModUtility.PositionToTile(Position), tangent, 2, 3);

            if (retreatTargets.Count > 0)
            {

                PerformFollow(retreatTargets.Last() * 64);

                return true;

            }

            return false;

        }

        public bool PerformCircle(Vector2 target)
        {

            int direction = ModUtility.DirectionToTarget(Position, target)[2];

            int offset = Mod.instance.randomIndex.Next(2) == 0 ? 2 : -2;

            int tangent = (direction + 8 + offset) % 8;

            List<Vector2> circleTargets = ModUtility.GetOccupiableTilesNearby(currentLocation, ModUtility.PositionToTile(target), tangent, 2, 3);

            if(circleTargets.Count > 0)
            {

                PerformFollow(circleTargets.First() * 64);

                return true;

            }

            return false;
        
        }

        public void PerformRandom()
        {

            List<Vector2> circleTargets = ModUtility.GetOccupiableTilesNearby(currentLocation, ModUtility.PositionToTile(Position), -1, 2, 3);

            if (circleTargets.Count > 0)
            {
                
                Vector2 target = circleTargets[Mod.instance.randomIndex.Next(circleTargets.Count)] * 64;

                PerformFollow(target);

            }

            if(tether != Vector2.Zero)
            {

                if(stuck != Position)
                {

                    stuck = Position;

                    stuckLimit = 0;

                }

                stuckLimit++;

                if(stuckLimit >= 4)
                {

                    PerformWarp(tether);

                    stuckLimit = 0;

                }

            }

        }

        public virtual bool PerformChannel(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval * 2;

            netChannelActive.Set(true);

            SetCooldown(2f);

            return true;

        }

        public void PerformWarp(Vector2 target)
        {

            Mod.instance.iconData.AnimateQuickWarp(currentLocation, Position, true, warp);

            Mod.instance.iconData.AnimateQuickWarp(currentLocation, target, false, warp);

            Position = target;

            LookAtFarmer();

            netHaltActive.Value = true;

            idleTimer = 120;

        }

        //=================== frame offsets

        public virtual int WalkCount()
        {

            return walkFrames[0].Count;

        }        
        
        public virtual int IdleCount()
        {

            return idleFrames[0].Count;

        }        
        
        public virtual int SweepCount()
        {

            return sweepFrames[0].Count;

        }        
        
        public virtual int FlightCount(int segment = 0)
        {

            switch (segment)
            {
                default:
                case 0: return flightFrames[0].Count;

                case 1: return flightFrames[4].Count;
                
                case 2: return flightFrames[8].Count;

                case 3: return flightFrames[0].Count + flightFrames[4].Count + flightFrames[8].Count;
            
            }

        }

        public virtual int SmashCount(int segment = 0)
        {

            switch (segment)
            {
                default:
                case 0: return smashFrames[0].Count;

                case 1: return smashFrames[4].Count;

                case 2: return smashFrames[8].Count;

                case 3: return smashFrames[0].Count + smashFrames[4].Count + smashFrames[8].Count;

            }

        }

        public virtual Vector2 CastPosition()
        {

            Vector2 castAt = Position + new Vector2(32, 32);

            switch (netDirection.Value)
            {

                case 0:

                    castAt.Y -= 96;

                    if (netAlternative.Value == 1)
                    {

                        castAt.X += 96;

                        break;
                    }

                    castAt.X -= 96;

                    break;

                case 1:

                    castAt.Y -= 32;

                    castAt.X += 96;

                    break;

                case 2:

                    if(netAlternative.Value == 1)
                    {

                        castAt.X += 96;

                        break;
                    }

                    castAt.X -= 96;

                    break;

                case 3:

                    castAt.Y -= 32;

                    castAt.X -= 96;

                    break;

            }

            return castAt;

        }

        public virtual Texture2D OverheadTexture()
        {

            return Mod.instance.iconData.displayTexture;

        }

        public virtual Microsoft.Xna.Framework.Rectangle OverheadPortrait()
        {

            return IconData.DisplayRectangle(Data.IconData.displays.transform);

        }


    }

}