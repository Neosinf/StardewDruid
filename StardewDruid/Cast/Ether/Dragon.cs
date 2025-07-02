using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewDruid.Monster;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.Buffs;
using StardewValley.Buildings;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using xTile.Tiles;

namespace StardewDruid.Cast.Ether
{
    public class Dragon : NPC
    {

        public NetLong netAnchor = new NetLong(0);
        public Farmer anchor;
        public bool avatar;

        public NetColor netPrimary = new NetColor(Color.White);
        public NetColor netSecondary = new NetColor(Color.White);
        public NetColor netTertiary = new NetColor(Color.White);
        public NetInt netFire = new NetInt((int)IconData.schemes.stars);
        //public NetString netColour = new NetString("");
        public DragonRender dragonRender;
        public float dragonScale;

        public int moveDirection;
        public int altDirection;
        public NetInt netDirection = new NetInt(0);
        public NetInt netAlternative = new NetInt(0);
        public bool loadedOut;
        public Vector2 setPosition;

        public float walkTimer;
        public int walkFrame;
        public int stationaryTimer;

        public bool endTransform;
        public SButton leftButton;
        public SButton rightButton;

        public NetBool netDashActive = new NetBool(false);

        public int flightFrame;
        public int flightHeight;

        public bool flightActive;
        public int flightDelay;
        public int flightTimer;
        public Vector2 flightPosition;
        public Vector2 flightTo;
        public Vector2 flightInterval;
        public bool flightFlip;
        public int flightIncrement;
        public bool flightLift;
        public int flightExtend;
        public string flightTerrain;

        public List<Rectangle> sweepFrames;
        public bool sweepActive;
        public NetBool netSweepActive = new NetBool(false);
        public int sweepFrame;
        public int sweepDelay;
        public int sweepTimer;
        public bool cooldownActive;
        public int cooldownTimer;

        public NetBool netSpecialActive = new NetBool(false);
        public bool specialActive;
        public int specialDelay;
        public int specialTimer;
        public int fireTimer;
        public int roarTimer;
        public bool roarActive;

        public NetBool netBreathActive = new NetBool(false);

        public NetBool netDigActive = new NetBool(false);
        public bool digActive;
        public int digTimer;
        public Vector2 digPosition;
        public int digMoment;

        public NetBool netDiveActive = new NetBool(false);
        public NetBool netSwimActive = new NetBool(false);
        public List<float> diveRotates;
        public bool swimActive;
        public int swimCheckTimer;
        public bool diveActive;
        public int diveTimer;
        public Vector2 divePosition;
        public int diveMoment;

        //public CueWrapper flightCue;
        //public CueWrapper fireCue;
        //public CueWrapper fireCueTwo;
        //public CueWrapper roarCue;

        public Dragon()
        {
        }

        public Dragon(Farmer Farmer, Vector2 position, string map, string Name)
          : base(new AnimatedSprite("StardewDruid.Characters.Dragon") { SpriteWidth = 64, SpriteHeight = 64, }, position, map, 2, Name, CharacterHandle.CharacterPortrait(CharacterHandle.characters.none), false)
        {

            willDestroyObjectsUnderfoot = false;

            SimpleNonVillagerNPC = true;

            DefaultMap = map;

            DefaultPosition = position;

            HideShadow = true;

            breather.Value = false;

            avatar = true;

            anchor = Farmer;

            netAnchor.Set(Farmer.UniqueMultiplayerID);

            dragonScale = 2f + ((float)Mod.instance.Config.dragonScale * 0.5f);

            scale.Set(dragonScale);

            moveDirection = Game1.player.FacingDirection;

            netDirection.Set(moveDirection);

            FacingDirection = moveDirection;

            if (ModUtility.GroundCheck(currentLocation, new Vector2((int)(Position.X / 64), (int)(Position.Y / 64))) == "water")
            {

                swimActive = true;

                netSwimActive.Set(true);

            }

            AnimateMovement(false);

            LoadOut();

            dragonRender.LoadConfigScheme();

            /*List<int> array = new()
            {
                dragonRender.primary.R,
                dragonRender.primary.G,
                dragonRender.primary.B,
                dragonRender.secondary.R,
                dragonRender.secondary.G,
                dragonRender.secondary.B,
                dragonRender.tertiary.R,
                dragonRender.tertiary.G,
                dragonRender.tertiary.B,
                (int)dragonRender.fire,
            };

            string colours = System.Text.Json.JsonSerializer.Serialize(array);

            netColour.Set(colours);*/

            netPrimary.Set(dragonRender.primary);

            netSecondary.Set(dragonRender.secondary);

            netTertiary.Set(dragonRender.tertiary);

            netFire.Set((int)dragonRender.fire);

        }

        public override bool IsVillager { get { return false; } }

        public override bool CanSocialize { get { return false; } }

        public void LoadOut()
        {

            anchor = Game1.GetPlayer(netAnchor.Value);

            if(dragonScale == 0f)
            {

                dragonScale = scale.Value;

            }

            dragonRender = new();

            if (!avatar)
            {

                /*List<int> colours = System.Text.Json.JsonSerializer.Deserialize<List<int>>(netColour.Value);

                dragonRender.primary = new Microsoft.Xna.Framework.Color(colours[0],colours[1],colours[2]);

                dragonRender.secondary = new Microsoft.Xna.Framework.Color(colours[3], colours[4], colours[5]);

                dragonRender.tertiary = new Microsoft.Xna.Framework.Color(colours[6], colours[7], colours[8]);

                dragonRender.fire = (DragonRender.breathSchemes)colours[9];*/

                dragonRender.primary = netPrimary.Value;

                dragonRender.secondary = netSecondary.Value;

                dragonRender.tertiary = netTertiary.Value;

                dragonRender.fire = (DragonRender.breathSchemes)netFire.Value;

            }

            loadedOut = true;

            flightTo = Vector2.Zero;

            flightInterval = Vector2.Zero;

            //flightCue = Game1.soundBank.GetCue("DragonFlight") as CueWrapper;

            //flightCue.Pitch *= 2;

            /*fireCue = Game1.soundBank.GetCue("DragonFire") as CueWrapper;

            fireCue.Volume *= 2;

            fireCue.Pitch /= 2;

            fireCueTwo = Game1.soundBank.GetCue("DragonFireTwo") as CueWrapper;

            fireCueTwo.Volume *= 2;

            fireCueTwo.Pitch /= 2;*/

           /* roarCue = Game1.soundBank.GetCue("DragonRoar") as CueWrapper;

            roarCue.Volume *= 2;

            roarCue.Pitch /= 2;*/

        }

        protected override void initNetFields()
        {
            base.initNetFields();

            NetFields.AddField(netAnchor, "netAnchor");
            NetFields.AddField(netDirection, "netDirection");
            NetFields.AddField(netAlternative, "netAlternative");
            NetFields.AddField(netSweepActive, "netSweepActive");
            NetFields.AddField(netDashActive, "netDashActive");
            NetFields.AddField(netSpecialActive, "netSpecialActive");
            NetFields.AddField(netBreathActive, "netBreathActive");
            NetFields.AddField(netDigActive, "netDigActive");
            NetFields.AddField(netDiveActive, "netDiveActive");
            NetFields.AddField(netSwimActive, "netSwimActive");
            NetFields.AddField(netPrimary, "netPrimary");
            NetFields.AddField(netSecondary, "netSecondary");
            NetFields.AddField(netTertiary, "netTertiary");
            NetFields.AddField(netFire, "netFire");
        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {

            if (anchor == null)
            {

                return;

            }

            if (IsInvisible)
            {

                return;

            }

            if (!Utility.isOnScreen(Position, 128))
            {

                return;

            }

            if (avatar && Game1.displayFarmer)
            {

                return;

            }

            if (netDashActive.Value)
            {

                return;

            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            Vector2 spritePosition = new Vector2(localPosition.X + 32 - (32 * dragonScale), localPosition.Y + 64 - (64 * dragonScale));

            DrawCharacter(b, spritePosition);

        }

        public virtual void DrawCharacter(SpriteBatch b, Vector2 localPosition)
        {

            bool flippant = netDirection.Value % 2 == 0 && netAlternative.Value == 3 || netDirection.Value == 3;

            float drawLayer = anchor.getDrawLayer() + 0.0001f;

            DragonAdditional additional = new()
            {

                direction = netDirection.Value,

                scale = dragonScale,

                flip = flippant,

                layer = drawLayer,

            };

            if (netDashActive.Value)
            {

                additional.flight = flightHeight;

                additional.frame = flightFrame;

                if (netSpecialActive.Value)
                {

                    additional.version = 1;

                    additional.breath = netBreathActive.Value;

                    dragonRender.drawFlight(b, localPosition, additional);

                }
                else
                {

                    dragonRender.drawFlight(b, localPosition, additional);

                }

                return;

            }

            if (netDiveActive.Value)
            {

                additional.frame = diveMoment;

                dragonRender.drawDive(b, localPosition, additional);

                return;
            }

            if (netSwimActive.Value)
            {

                additional.frame = walkFrame;

                dragonRender.drawSwim(b, localPosition, additional);

                return;

            }

            if (netDigActive.Value)
            {

                int digFrame = digMoment % 2;

                additional.frame = digFrame;

                dragonRender.drawDig(b, localPosition, additional);

                return;

            }

            if (netSweepActive.Value)
            {

                additional.frame = sweepFrame;

                if (netSpecialActive.Value)
                {

                    additional.version = 1;

                    dragonRender.drawSweep(b, localPosition, additional);

                }
                else
                {

                    dragonRender.drawSweep(b, localPosition, additional);

                }

                return;

            }

            if (netSpecialActive.Value)
            {

                additional.version = 1;

                additional.breath = netBreathActive.Value;

                additional.frame = walkFrame;

                dragonRender.drawWalk(b, localPosition, additional);

            }
            else
            {
                additional.frame = walkFrame;

                dragonRender.drawWalk(b, localPosition, additional);

            }

        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {

            if (netDashActive.Value)
            {


                if (anchor == null)
                {

                    return;

                }

                if (IsInvisible)
                {

                    return;

                }

                if (!Utility.isOnScreen(Position, 128))
                {

                    return;

                }

                if (avatar && Game1.displayFarmer)
                {

                    return;

                }

                Vector2 localPosition = Game1.GlobalToLocal(Position);

                Vector2 spritePosition = new Vector2(localPosition.X + 32 - (32 * dragonScale), localPosition.Y + 64 - (64 * dragonScale));

                DrawCharacter(b, spritePosition);

            }

            base.drawAboveAlwaysFrontLayer(b);

        }

        public override void reloadSprite(bool onlyAppearance = false)
        {

            base.reloadSprite(onlyAppearance);

            Portrait = CharacterHandle.CharacterPortrait(CharacterHandle.characters.none);

        }

        public override bool checkAction(Farmer who, GameLocation l) => false;

        public override void collisionWithFarmerBehavior()
        {
        }

        public override void behaviorOnFarmerPushing()
        {
        }

        public override Rectangle GetBoundingBox() => new Rectangle(-1, -1, 0, 0);

        public override void update(GameTime time, GameLocation location)
        {

            if (!loadedOut)
            {

                LoadOut();

            }

            if (anchor == null)
            {

                Mod.instance.movers.Add(CharacterHandle.characters.Dragon,new(CharacterHandle.characters.Dragon));

                return;

            }

            if (!avatar)
            {

                UpdateMultiplayer();

                return;

            }

            if (Mod.CasterGone())
            {

                return;

            }

            if (currentLocation != Game1.player.currentLocation)
            {

                return;

            }

            if (!Mod.instance.eventRegister.ContainsKey(Rite.eventTransform))
            {

                Mod.instance.movers.Add(CharacterHandle.characters.Dragon, new(CharacterHandle.characters.Dragon));

                return;

            }

            if (Mod.instance.CasterBusy())
            {

                specialActive = false;

                specialTimer = 0;

                netSpecialActive.Set(false);

                netBreathActive.Set(false);

                return;

            }

            if (anchor.FarmerSprite.PauseForSingleAnimation)
            {

                return;

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

            int stepAnimation = Game1.player.FarmerSprite.currentAnimationIndex % 4;

            if (stepAnimation == 0)
            {

                Game1.player.FarmerSprite.currentAnimationIndex++;

            }
            else if (stepAnimation == 3)
            {

                Game1.player.FarmerSprite.currentAnimationIndex -= 2;

            }

            updateEmote(time);

            moveDirection = Game1.player.FacingDirection;

            if (flightActive)
            {
                UpdateFlight();
            }

            if (sweepActive)
            {
                UpdateSweep();
            }

            if (swimActive)
            {
                UpdateSwim();

            }

            if (diveActive)
            {
                UpdateDive();
            }

            if (digActive)
            {

                UpdateDig();

            }

            if (!flightActive)
            {
                UpdateFollow();
            }

            if (specialActive)
            {
                UpdateSpecial();
            }

            if (cooldownActive)
            {
                UpdateCooldown();
            }

        }

        public void UpdateMultiplayer()
        {

            if (netDashActive.Value)
            {

                flightTimer--;

                if (flightTimer <= 0)
                {

                    flightFrame++;

                    if (flightFrame > 4)
                    {
                        flightFrame = 1;

                    }

                    flightTimer = 12;

                }

                return;

            }
            else
            {
                
                flightFrame = 0;

                flightTimer = 12;

            }

            if (netSweepActive.Value)
            {

                sweepTimer--;

                if (sweepTimer <= 0)
                {

                    sweepFrame++;

                    if (sweepFrame > 4)
                    {
                        sweepFrame = 0;
                    }

                    sweepTimer = 9;

                }

                return;

            }
            else
            {
                sweepFrame = 0;

                sweepTimer = 9;

            }

            if (setPosition != Position || netDirection.Value != moveDirection || netAlternative.Value != altDirection)
            {

                //CheckSwim();

                if (walkFrame == 0)
                {

                    walkFrame = 1;

                    walkTimer = 9;

                    stationaryTimer = 30;

                }

                walkTimer--;

                if (walkTimer <= 0)
                {

                    moveDirection = netDirection.Value;

                    altDirection = netAlternative.Value;

                    setPosition = Position;

                    walkFrame++;

                    if (walkFrame > 6)
                    {

                        walkFrame = 1;

                    }

                    walkTimer = 9;

                    stationaryTimer = 30;

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

        public void UpdateFollow()
        {

            if (Position != Game1.player.Position)
            {
                if (moveDirection % 2 == 1)
                {
                    netAlternative.Set(Game1.player.Position.X > (double)Position.X ? 1 : 3);
                }

                netDirection.Set(moveDirection);

                FacingDirection = moveDirection;

                Position = Game1.player.Position;

                AnimateMovement();

                //CheckSwim();

                return;

            }

            if (netDirection.Value != moveDirection)
            {

                netDirection.Set(moveDirection);

                FacingDirection = moveDirection;

                netAlternative.Set(netDirection.Value);

                AnimateMovement();

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

        public void CheckSwim()
        {
            
            if (netDashActive.Value)
            {
                
                return;

            }

            if (ModUtility.GroundCheck(currentLocation, ModUtility.PositionToTile(Position)) == "water")
            {

                swimActive = true;

                netSwimActive.Set(true);

            }

        }

        public void AnimateMovement(bool movement = true)
        {

            if (movement)
            {

                walkTimer--;

                if (walkTimer <= 0)
                {

                    walkFrame++;

                    if (walkFrame > 6)
                    {
                        walkFrame = 1;
                    }

                    walkTimer = 9;

                    stationaryTimer = 5;

                    if (!avatar)
                    {
                        stationaryTimer = 30;

                    }

                }

                return;

            }

            walkFrame = 0;

            walkTimer = 0;

            stationaryTimer = 0;

        }

        public void LeftClickAction(SButton Button)
        {

            leftButton = Button;

            if (netDiveActive.Value || netSweepActive.Value || netDashActive.Value || netDigActive.Value)
            {

                return;

            }

            PerformFlight();

        }

        public void RightClickAction(SButton Button)
        {

            rightButton = Button;

            if (netSwimActive.Value && !netDashActive.Value)
            {

                if (!diveActive && Mod.instance.activeEvent.Count == 0)
                {
                    
                    if (AccountStamina())
                    {
                        
                        PerformDive();

                    }

                }

                return;

            }

            if (!netSpecialActive.Value)
            {

                PerformSpecial();

            }

        }

        public void PerformFlight()
        {

            if (ModUtility.MonsterProximity(Game1.player.currentLocation, new() { Game1.player.Position, }, 160).Count > 0)
            {

                PerformSweep();

                return;

            }

            int flightRange = FlightDestination();

            if (flightRange == 0)
            {
                return;
            }

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.etherOne))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.etherOne, 1);

            }

            flightActive = true;

            flightDelay = 3;

            flightFrame = 0;

            flightTimer = flightIncrement * flightRange;

            flightLift = false;

            //if (!flightCue.IsPlaying)
            //{

            //flightCue.Play();

            //}

            Mod.instance.sounds.PlayCue(SoundHandle.SoundCue.DragonFlight);

            flightInterval = new((flightTo.X - Position.X) / flightTimer, (flightTo.Y - Position.Y) / flightTimer);

            flightPosition = Game1.player.Position;

            flightExtend = flightIncrement * 6;

            Game1.player.temporarilyInvincible = true;

            Game1.player.temporaryInvincibilityTimer = 0;

            Game1.player.currentTemporaryInvincibilityDuration = 100 * flightIncrement;

            if (swimActive)
            {
                currentLocation.playSound("waterSlosh");

                Mod.instance.iconData.ImpactIndicator(currentLocation, Position - new Vector2(0, 64), IconData.impacts.fish, dragonScale+1f, new() { interval = 100f, alpha = 0.5f, layer = 999f, });

            }


        }

        public void PerformSweep()
        {

            sweepActive = true;

            sweepDelay = 3;

            sweepFrame = 0;

            sweepTimer = 25;

        }

        public bool UpdateFlight()
        {

            if (flightDelay > 0)
            {

                flightDelay--;

                return true;

            }

            netDashActive.Set(true);

            Game1.player.temporarySpeedBuff = 5f;

            flightTimer--;

            /*if (strikeTimer > 0)
            {

                if (strikeTimer % (flightIncrement * 3) == 0)
                {

                    FlightStrike();

                }

                strikeTimer--;

            }*/

            if (flightTimer == 0)
            {

                flightActive = false;

                netDashActive.Set(false);

                /*if (flightCue.IsPlaying)
                {

                    flightCue.Stop(AudioStopOptions.Immediate);

                }*/

                Mod.instance.sounds.StopCue(SoundHandle.SoundCue.DragonFlight);

                if (flightTerrain == "water")
                {

                    swimActive = true;

                    netSwimActive.Set(true);

                    if (flightLift)
                    {
                        
                        currentLocation.playSound("waterSlosh");

                        Mod.instance.iconData.ImpactIndicator(currentLocation, Position - new Vector2(0, 24 * dragonScale), IconData.impacts.splash, dragonScale + 1f, new() { interval = 100f, alpha = 0.5f, layer = 999f, });

                    }

                }
                else
                if (flightLift)
                {

                    FlightStrike();

                }

                Game1.player.temporarySpeedBuff = 0;

                return false;

            }

            if (flightHeight < 128 && flightTimer > 16)
            {

                flightHeight = flightHeight + 8;

            }
            else if (flightHeight > 0 && flightTimer <= 16)
            {

                flightHeight = flightHeight - 8;

            }

            flightPosition += flightInterval;

            Game1.player.Position = flightPosition;

            Position = flightPosition;

            if (flightTimer % flightIncrement == 0)
            {

                if (flightFrame == 4)
                {

                    flightFrame = 1;

                }
                else
                {

                    flightFrame = flightFrame + 1;

                    flightLift = true;

                }

                if (Mod.instance.Helper.Input.IsDown(leftButton))
                {

                    int num = FlightDestination();

                    if (num != 0)
                    {

                        /*if (!flightCue.IsPlaying)
                        {

                            flightCue.Play();

                        }*/

                        Mod.instance.sounds.PlayCue(SoundHandle.SoundCue.DragonFlight);

                        flightTimer = flightIncrement * num;

                        flightInterval = new((flightTo.X - Position.X) / flightTimer, (flightTo.Y - Position.Y) / flightTimer);

                    }

                }

                if (flightTimer == flightIncrement)
                {

                    flightFrame = 0;

                    Game1.player.temporarilyInvincible = true;

                    Game1.player.temporaryInvincibilityTimer = 0;

                    Game1.player.currentTemporaryInvincibilityDuration = 500;

                }

            }

            return true;

        }

        public void FlightStrike()
        {
            
            if (!Mod.instance.questHandle.IsComplete(QuestHandle.etherOne))
            {

                return;

            }

            SpellHandle sweep = new(Game1.player, Game1.player.Position, 256, Mod.instance.CombatDamage() * 2 / 3)
            {
                display = IconData.impacts.shockwave,

                displayRadius = 3,

                instant = true,

                added = new() { SpellHandle.Effects.knock, SpellHandle.Effects.stomping }
            };

            Mod.instance.spellRegister.Add(sweep);

            Mod.instance.rite.Dispense(160);

        }

        public bool UpdateSweep()
        {

            if (sweepDelay > 0)
            {
                sweepDelay--;

                return true;

            }

            if (sweepTimer == 25)
            {

                if (Game1.player.CurrentTool is MeleeWeapon)
                {
                    (Game1.player.CurrentTool as MeleeWeapon).isOnSpecial = true;
                }

                netSweepActive.Set(true);

                flightHeight = 0;

            }

            sweepTimer--;

            if (sweepTimer == 15)
            {

                SweepStrike();

            }

            if (sweepTimer > 12)
            {

                flightHeight = flightHeight + 2;

            }
            else
            {

                flightHeight = flightHeight - 2;

            }

            if (sweepTimer <= 0)
            {

                if (Game1.player.CurrentTool is MeleeWeapon)
                {
                    (Game1.player.CurrentTool as MeleeWeapon).isOnSpecial = false;
                }

                sweepActive = false;

                netSweepActive.Set(false);

                flightHeight = 0;

                return false;

            }

            if (sweepTimer % 5 == 0)
            {

                sweepFrame++;

            }

            return true;

        }

        public void UpdateCooldown()
        {

            cooldownTimer--;

            if (cooldownTimer <= 0)
            {
                cooldownActive = false;

            }

        }

        public void SweepStrike()
        {

            SpellHandle sweep = new(Game1.player, Game1.player.Position, 160, Mod.instance.CombatDamage() * 2 / 3)
            {
                instant = true
            };

            Mod.instance.spellRegister.Add(sweep);

            Mod.instance.rite.Dispense(160);

        }

        public int FlightDestination()
        {

            Dictionary<int, Vector2> flightVectors = new Dictionary<int, Vector2>()
            {
                [0] = new Vector2(1f, -2f),
                [1] = new Vector2(-1f, -2f),
                [2] = new Vector2(2f, 0.0f),
                [3] = new Vector2(1f, 2f),
                [4] = new Vector2(-1f, 2f),
                [5] = new Vector2(-2f, 0.0f),
                [6] = new Vector2(0f, -2f),
                [7] = new Vector2(0f, 2f),
            };

            int key = 0;

            int increment = 11;

            int alternate = netAlternative.Value;

            if (netDirection.Value != moveDirection)
            {
                alternate = netDirection.Value;

            }

            bool cardinal = Mod.instance.Config.cardinalMovement;

            switch (moveDirection)
            {
                case 0:
                    if (alternate == 3)
                    {
                        key = 1;
                    }
                    if (cardinal)
                    {
                        key = 6;
                    }
                    increment = 12;
                    break;
                case 1:
                    key = 2;
                    break;
                case 2:
                    key = 3;
                    if (alternate == 3)
                    {
                        key = 4;
                    }
                    if (cardinal)
                    {
                        key = 7;
                    }
                    increment = 12;
                    break;
                case 3:
                    key = 5;
                    break;
            }

            Vector2 flightOffset = flightVectors[key];

            Vector2 tile = ModUtility.PositionToTile(Position);

            List<int> flightRanges = new()
            {
                4,
                5,
                6,
                7,
                8,
                9,
                10,
                11,
                3,
                2,
                1,
            };

            foreach(int checkRange in flightRanges)
            {

                Vector2 neighbour = tile + (flightOffset * checkRange);

                List<string> safeTerrain = new() { "ground", "water", };

                string groundCheck = ModUtility.GroundCheck(currentLocation, neighbour);

                if (!safeTerrain.Contains(groundCheck))
                {

                    continue;

                }

                if (groundCheck == "water")
                {

                    if (!ModUtility.WaterCheck(currentLocation, neighbour, 2))
                    {
                        
                        continue;

                    }

                }

                Vector2 neighbourPosition = neighbour * 64;

                Rectangle boundingBox = Game1.player.GetBoundingBox();

                int xOffset = boundingBox.X - (int)Game1.player.Position.X;

                int yoffset = boundingBox.Y - (int)Game1.player.Position.Y;

                boundingBox.X = (int)neighbourPosition.X + xOffset;

                boundingBox.Y = (int)neighbourPosition.Y + yoffset;

                if (!currentLocation.isCollidingPosition(boundingBox, Game1.viewport, false, 0, false, Game1.player, false, false, false))
                {

                    flightTo = neighbourPosition;

                    flightIncrement = increment;

                    flightTerrain = groundCheck;

                    netAlternative.Set(alternate);

                    netDirection.Set(moveDirection);

                    FacingDirection = moveDirection;

                    AnimateMovement(false);

                    return checkRange;

                }


            }

            return 0;

        }

        public bool TreasureZone(bool activate = false)
        {

            if (Mod.instance.activeEvent.Count > 0)
            {

                return false;

            }

            if (!swimActive && currentLocation.objects.Count() > 0)
            {

                Vector2 tile = new Vector2((int)(Position.X / 64), (int)(Position.Y / 64));

                List<Vector2> tileVectors = ModUtility.GetTilesWithinRadius(currentLocation, tile, 1);

                tileVectors.Add(tile);

                foreach (Vector2 tileVector in tileVectors)
                {

                    if (currentLocation.objects.ContainsKey(tileVector))
                    {

                        StardewValley.Object targetObject = currentLocation.objects[tileVector];

                        if (targetObject.Name.Contains("Artifact Spot"))
                        {

                            if (activate)
                            {

                                currentLocation.digUpArtifactSpot((int)tileVector.X, (int)tileVector.Y, anchor);

                                currentLocation.objects.Remove(tileVector);

                            }

                            digPosition = tileVector * 64f;

                            return true;

                        }

                    }

                    continue;

                }

            }

            if (Mod.instance.eventRegister.ContainsKey("crate_"+currentLocation.Name))
            {

                if (Mod.instance.eventRegister["crate_" + currentLocation.Name] is Crate treasureEvent)
                {

                    if (!treasureEvent.eventActive)
                    {

                        float treasureDistance = Vector2.Distance(Position, treasureEvent.origin);

                        if (treasureDistance <= 128f)
                        {

                            if (activate)
                            {

                                treasureEvent.EventActivate();

                            }

                            digPosition = treasureEvent.origin;

                            return true;

                        }

                    }

                };

            }

            return false;

        }

        public void PerformSpecial()
        {

            if (!flightActive)
            {

                if (TreasureZone())
                {

                    digActive = true;

                    specialDelay = 6;

                    digTimer = -1;

                    return;

                }

                FaceCursorTarget();

            }

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.etherTwo))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.etherTwo, 1);

            }

            specialActive = true;

            specialDelay = 6;

            specialTimer = -1;

            netSpecialActive.Set(true);

        }

        public void UpdateDig()
        {
            if (specialDelay > 0)
            {

                specialDelay--;

                return;

            }

            if (digTimer == -1)
            {

                netDigActive.Set(true);

                digTimer = 108;

                digMoment = 0;

            }

            digTimer--;

            if (digTimer == 0)
            {

                digActive = false;

                netDigActive.Set(false);

                return;

            }

            Game1.player.Position = digPosition;

            Position = digPosition;

            if (digTimer % 12 == 0)
            {

                digMoment++;

            }

            if (digTimer == 24)
            {

                TreasureZone(true);

            }

        }

        public void FaceCursorTarget()
        {
            Point mousePoint = Game1.getMousePosition();

            if (mousePoint.Equals(new(0)))
            {
                return;

            }

            Vector2 viewPortPosition = Game1.viewportPositionLerp;

            Vector2 mousePosition = new(mousePoint.X + viewPortPosition.X, mousePoint.Y + viewPortPosition.Y);

            Vector2 diffPosition = mousePosition - Position;

            float rotate = (float)Math.Atan2(diffPosition.Y, diffPosition.X);

            if (rotate < 0.0001f)
            {

                rotate = (float)(Math.PI * 2) + rotate;

            }

            if (rotate < 0.525 || rotate > 5.75)
            {

                moveDirection = 1;
                altDirection = 0;

            }
            else if (rotate < 1.575)
            {

                moveDirection = 2;
                altDirection = 1;

            }
            else if (rotate < 2.625)
            {

                moveDirection = 2;
                altDirection = 3;

            }
            else if (rotate < 3.675)
            {

                moveDirection = 3;
                altDirection = 0;

            }
            else if (rotate < 4.725)
            {

                moveDirection = 0;
                altDirection = 3;

            }
            else if (rotate < 5.775)
            {

                moveDirection = 0;
                altDirection = 1;

            }

            Game1.player.FacingDirection = moveDirection;

            netDirection.Set(moveDirection);

            FacingDirection = moveDirection;

            netAlternative.Set(altDirection);

            AnimateMovement(false);

        }

        public bool RoarCheck(Vector2 zero)
        {

            Vector2 target = zero * 64;

            roarActive = false;

            if (currentLocation.characters.Count > 0)
            {

                foreach (NPC witness in ModUtility.GetFriendsInLocation(Game1.player.currentLocation, false))
                {

                    if (Vector2.Distance(witness.Position, target) < 320)
                    {

                        if (roarTimer <= 0)
                        {

                            switch (Mod.instance.randomIndex.Next(2))
                            {
                                default:

                                    //showTextAboveHead("RWWWRRR", duration: 2000);

                                    Mod.instance.sounds.PlayCue(SoundHandle.SoundCue.DragonGrowl);

                                    break;
                                case 1:

                                    //showTextAboveHead("RWWWRRR", duration: 2000);

                                    Mod.instance.sounds.PlayCue(SoundHandle.SoundCue.DragonRoar);

                                    break;

                            }

                        roarTimer = 60;

                        }

                        roarActive = true;

                        netBreathActive.Set(false);

                        return true;

                    }

                }

            }

            return false;

        }

        public bool UpdateSpecial()
        {
            if (specialDelay > 0)
            {

                specialDelay--;

                return true;

            }

            if (specialTimer == -1)
            {

                List<Vector2> zeroes = BlastTarget();

                RoarCheck(zeroes[0]);

                specialTimer = 48;

            }

            specialTimer--;

            fireTimer--;

            roarTimer--;

            if (specialTimer == 0 || netSwimActive.Value)
            {

                specialActive = false;

                netSpecialActive.Set(false);

                netBreathActive.Set(false);

                return false;

            }

            if (Mod.instance.Helper.Input.IsDown(rightButton) && specialTimer == 12)
            {

                specialTimer = 18;

            }

            if (fireTimer <= 0)
            {

                List<Vector2> zeroes = BlastTarget();

                if (RoarCheck(zeroes[0]) || !AccountStamina())
                {

                    fireTimer = 24;

                    return true;

                };

                Vector2 minus = new((int)(Position.X / 64), (int)(Position.Y / 64));

                if (roarTimer <= 0)
                {

                    /*if (!fireCue.IsPlaying)
                    {

                        fireCue.Play();

                    }

                    if (!fireCueTwo.IsPlaying)
                    {

                        fireCueTwo.Play();

                    }*/

                    Mod.instance.sounds.PlayCue(SoundHandle.SoundCue.DragonFire);

                    roarTimer = 75;

                }

                List<Vector2> splash = new();

                if (netSweepActive.Value)
                {

                    splash = new()
                    {
                        zeroes[1],
                        zeroes[1] + new Vector2(3,-4),
                        zeroes[1] + new Vector2(-3,-4),
                        zeroes[1] + new Vector2(5,1),
                        zeroes[1] + new Vector2(3,3),
                        zeroes[1] + new Vector2(-3,3),
                        zeroes[1] + new Vector2(-5,-1),

                    };

                }
                else
                {

                    splash.Add(zeroes[0]);

                }

                for (int i = 0; i < splash.Count; i++)
                {

                    Vector2 burnVector = splash[i];

                    SpellHandle burn = new(Game1.player, burnVector * 64, 320, Mod.instance.CombatDamage() / 2)
                    {
                        type = SpellHandle.Spells.explode,

                        scheme = Enum.Parse<IconData.schemes>(dragonRender.fire.ToString().Replace("breath_","")),

                        //burn.display = IconData.impacts.combustion;

                        instant = true,

                        power = 4,

                        terrain = 2,

                        explosion = 2,

                        added = new() { SpellHandle.Effects.embers, }

                    };

                    if (Mod.instance.questHandle.IsComplete(QuestHandle.etherTwo))
                    {

                         burn.added.Add(SpellHandle.Effects.immolate);

                    }

                    if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.spellcatch))
                    {

                        if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.capture))
                        {

                            burn.added.Add(SpellHandle.Effects.capture);

                        }

                    }

                    Mod.instance.spellRegister.Add(burn);

                }

                netBreathActive.Set(true);

                fireTimer = 24;

            }
            
            return true;

        }

        public List<Vector2> BlastTarget()
        {

            Vector2 tile = new Vector2((int)(Position.X / 64), (int)(Position.Y / 64));

            Vector2 zero = tile;

            Vector2 start = tile;

            List<Vector2> zeroes = new();

            bool cardinal = Mod.instance.Config.cardinalMovement;

            switch (netDirection.Value)
            {
                case 0:


                    if (cardinal)
                    {
                        start.Y -= 1;
                        zero.Y -= 4;
                        break;

                    }

                    zero.X += 3;

                    zero.Y -= 3;

                    if (netAlternative.Value == 3 || flip)
                    {
                        zero.X -= 6;

                        break;

                    }

                    start.Y -= 1;

                    break;

                case 1:

                    zero.X += 5;

                    start.X += 1;

                    break;

                case 2:

                    if (cardinal)
                    {

                        zero.Y += 4;


                        break;

                    }

                    zero.X += 3;

                    zero.Y += 3;

                    if (netAlternative.Value == 3 || flip)
                    {
                        zero.X -= 6;

                        break;

                    }

                    break;

                default:

                    zero.X -= 5;

                    start.X -= 1;

                    break;

            }

            zeroes.Add(zero);

            zeroes.Add(start);

            return zeroes;

        }

        public void UpdateSwim()
        {

            if (netDashActive.Value)
            {

                swimActive = false;

                netSwimActive.Set(false);

                return;

            }

            string ground = ModUtility.GroundCheck(currentLocation, ModUtility.PositionToTile(Position));

            if (ground == "water" || ground == "void")
            {

                return;

            }

            swimActive = false;

            netSwimActive.Set(false);

        }

        public void PerformDive()
        {

            diveActive = true;

            netDiveActive.Set(true);

            diveTimer = 150;

            divePosition = Game1.player.Position;

            diveMoment = 0;

            currentLocation.playSound("pullItemFromWater");

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.etherThree))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.etherThree, 1);

            }

            Mod.instance.iconData.ImpactIndicator(currentLocation, Position - new Vector2(0, 16 * dragonScale), IconData.impacts.splash, dragonScale + 1f, new() { interval = 100f, alpha = 0.5f, layer = 999f, });

        }

        public bool UpdateDive()
        {
            diveTimer--;

            if (diveTimer <= 0)
            {

                diveActive = false;

                netDiveActive.Set(false);
                
                return false;

            }

            Game1.player.Position = divePosition;

            Position = divePosition;

            if (diveTimer % 30 == 0)
            {

                diveMoment++;

            }

            if (diveTimer == 90)
            {

                currentLocation.playSound("quickSlosh");

                Mod.instance.iconData.ImpactIndicator(currentLocation, Position - new Vector2(0, 16 * dragonScale), IconData.impacts.splash, dragonScale + 1f, new() { interval = 100f, alpha = 0.5f, layer = 999f, });

                Mod.instance.iconData.ImpactIndicator(currentLocation, Position - new Vector2(0, 16 * dragonScale), IconData.impacts.fish, dragonScale, new() { interval = 100f, alpha = 0.5f, layer = 999f, });

                if (TreasureZone(true))
                {

                    return true;

                }

                Vector2 treasurePosition = Position + new Vector2(64, 0);

                StardewValley.Object treasureItem = SpawnData.DiveTreasure(currentLocation, ModUtility.PositionToTile(treasurePosition), Mod.instance.questHandle.IsComplete(QuestHandle.etherThree));

                if (currentLocation is not Town && Mod.instance.ModDifficulty() > 5)
                {

                    if (treasureItem.sellToStorePrice() > 200)
                    {

                        string treasureId = "treasure_chase_bounty_" + Game1.player.currentLocation.Name;

                        Crate treasureEvent = new();

                        treasureEvent.EventSetup(treasurePosition, treasureId, false);

                        treasureEvent.crateThief = true;

                        treasureEvent.heldTreasure = true;

                        treasureEvent.crateTerrain = 2;

                        treasureEvent.treasures = new() { treasureItem };

                        treasureEvent.location = Game1.player.currentLocation;

                        treasureEvent.EventActivate();

                        return true;

                    }

                }

                ThrowHandle treasure = new(Game1.player, treasurePosition, treasureItem);

                if (treasure.item.Category == StardewValley.Object.FishCategory)
                {

                    Game1.player.caughtFish(treasure.item.ItemId, 1, false, 1);

                    Mod.instance.GiveExperience(1, treasure.item.Quality * 12); // gain fishing experience

                }

                treasure.fade = 0.0005f;

                treasure.height = 320;

                treasure.register();

            }

            return true;

        }

        public void ShutDown()
        {

            if (flightActive && currentLocation.Name == Game1.player.currentLocation.Name)
            {

                Game1.player.Position = flightTo;

                Game1.player.temporarySpeedBuff = 0;

            }

            /*if (flightCue.IsPlaying)
            {

                flightCue.Stop(AudioStopOptions.Immediate);

            }*/

            Mod.instance.sounds.StopCue(SoundHandle.SoundCue.DragonFlight);

        }

        public bool SafeExit()
        {

            if (
                (ModUtility.GroundCheck(currentLocation,ModUtility.PositionToTile(Position)) == "water" && swimActive) || 
                (flightActive && flightTerrain == "water"))
            {

                return false;

            }

            return true;

        }

        public bool AccountStamina()
        {

            if (Game1.player.Stamina <= 32 || Game1.player.health <= 25)
            {

                Mod.instance.AutoConsume();

            }

            int cost = (int)Mod.instance.ModDifficulty() / 2;

            if (Game1.player.Stamina <= cost)
            {

                Mod.instance.CastMessage(StringData.Strings(StringData.stringkeys.energySkill), 3);

                return false;

            }

            float oldStamina = Game1.player.Stamina;

            float staminaCost = Math.Min(cost, oldStamina - 1);

            Game1.player.Stamina -= staminaCost;

            Game1.player.checkForExhaustion(oldStamina);

            return true;

        }

    }

}
