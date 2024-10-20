using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.GameData.Characters;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Timers;
using System.Xml;
using System.Xml.Serialization;

namespace StardewDruid.Character
{
    
    public class Character : NPC
    {

        public Texture2D characterTexture;
        public CharacterHandle.characters characterType = CharacterHandle.characters.none;

        public WeaponRender weaponRender;

        public NetInt netLayer = new NetInt(0);
        public int overhead;

        public Vector2 occupied;
        public Vector2 destination;
        public Dictionary<Vector2,int> traversal = new();
        public Vector2 tether;
        public bool restSet;

        public float gait;
        public float fadeOut;
        public float setScale;

        public IconData.warps warpDisplay = IconData.warps.portal;

        public NetInt netDirection = new NetInt(2);
        public NetInt netAlternative = new NetInt(1);
        public NetInt netIdle = new NetInt(0);
        public NetInt netMovement = new NetInt(0);
        public NetInt netSpecial = new NetInt(0);

        public List<Vector2> roamVectors = new();
        public int roamIndex;
        public double roamLapse;
        public int stuck;
        public int roamSonar;

        public NetBool netSceneActive = new NetBool(false);
        public Dictionary<int,Vector2> eventVectors = new();
        public List<Vector2> closedVectors = new();
        public int eventIndex;
        public string eventName;
        public bool loadedOut;

        public enum mode
        {
            home,
            scene,
            track,
            roam,
            random,
            limbo,
        }

        public mode modeActive;

        public enum pathing
        {
            none,
            monster,
            player,
            roam,
            scene,
            random,
            circling,

        }

        public pathing pathActive;

        public enum idles
        {
            none,
            standby,
            alert,
            rest,
            kneel,
            daze,
            jump,
            idle,
        }

        public enum movements
        {
            none,
            walk,
            run,
        }

        public enum dashes
        {
            none,
            dash,
            smash,
        }
        public enum specials
        {
            none,
            special,
            channel,
            invoke,
            pickup,
            greet,
            sweep,
            gesture,
            launch,
            point,
            liftup,
            liftdown,

        }

        public Dictionary<int, List<Rectangle>> walkFrames = new();
        public Dictionary<idles, Dictionary<int, List<Rectangle>>> idleFrames = new();
        public Dictionary<dashes, Dictionary<int, List<Rectangle>>> dashFrames = new();
        public Dictionary<specials,Dictionary<int, List<Rectangle>>> specialFrames = new();
        public Dictionary<int, List<Rectangle>> hatFrames = new();

        public int idleTimer;
        public int idleInterval;
        public int stationaryTimer;
        public bool onAlert;

        public int collidePriority;
        public int collideTimer;
        public int moveTimer;
        public int moveInterval;
        public int moveFrame;
        public bool moveRetreat;
        public bool walkSide;
        public int lookTimer;
        public int followTimer;
        public int attentionTimer;
        public int trackQuadrant;

        public NetInt netDash = new(0);
        public int dashFrame;
        public int dashInterval;
        public int dashPeak;
        public int dashHeight;
        public NetInt netDashProgress = new NetInt(0);

        public Vector2 pathFrom;
        public float pathTotal;
        public float pathProgress;
        public Vector2 pathIncrement;
        public int pathSegment;

        public Dictionary<specials, int> specialIntervals = new();
        public Dictionary<specials, int> specialCeilings = new();
        public Dictionary<specials, int> specialFloors = new();

        public bool specialDisable;
        public int specialTimer;
        public int specialFrame;
        public Vector2 workVector;
        public int cooldownTimer;
        public int cooldownInterval;
        public int hitTimer;
        public int pushTimer;

        public int moveDirection;
        public int altDirection;
        public int trackDashProgress;
        public Vector2 setPosition = Vector2.Zero;
        
        public override Stack<StardewValley.Dialogue> CurrentDialogue
        {
            get
            {

                if(IsEmoting && currentEmote == 12)
                {

                    return new Stack<StardewValley.Dialogue>();
                }

                return base.CurrentDialogue;

            }
            set
            {

                base.CurrentDialogue = value;

            }

        }

        public Character()
        {
        }

        public Character(CharacterHandle.characters type)
          : base(
                new AnimatedSprite("StardewDruid.Characters." + type.ToString()) { SpriteWidth = 32, SpriteHeight = 32, }, 
                CharacterHandle.CharacterStart(CharacterHandle.CharacterHome(type)),
                CharacterHandle.CharacterLocation(CharacterHandle.CharacterHome(type)),
                2,
                CharacterHandle.CharacterName(type), 
                CharacterHandle.CharacterPortrait(type), 
                false
                )
        {

            characterType = type;

            willDestroyObjectsUnderfoot = false;

            HideShadow = true;

            SimpleNonVillagerNPC = true;

            SettleOccupied();

            LoadOut();

        }

        public override bool IsVillager { get { return false; } }

        public override bool CanSocialize { get { return false; } }

        protected override void initNetFields()
        {
            base.initNetFields();
            NetFields.AddField(netDirection, "netDirection");
            NetFields.AddField(netAlternative, "netAlternative");
            NetFields.AddField(netIdle, "netIdle");
            NetFields.AddField(netMovement, "netMovement");
            NetFields.AddField(netSpecial, "netSpecial");
            NetFields.AddField(netSceneActive, "netSceneActive");
            NetFields.AddField(netDash, "netDash");
            NetFields.AddField(netDashProgress, "netDashProgress");
            NetFields.AddField(netLayer, "netLayer");
        }

        public virtual void LoadIntervals()
        {

            modeActive = mode.random;

            collidePriority = new Random().Next(20);

            gait = 1.6f;

            setScale = 4f;

            fadeOut = 1;

            moveInterval = 12;

            specialIntervals[specials.none] = 30;
            specialCeilings[specials.none] = 1;
            specialFloors[specials.none] = 1;

            cooldownInterval = 300;

            dashPeak = 128;

            dashInterval = 9;

            specialIntervals[specials.sweep] = 8;
            specialCeilings[specials.sweep] = 3;
            specialFloors[specials.sweep] = 0;

        }

        public virtual void LoadOut()
        {

            if (characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.CharacterType(Name);

            }

            LoadIntervals();

            characterTexture = CharacterHandle.CharacterTexture(characterType);

            walkFrames = CharacterRender.HumanoidWalk();

            idleFrames = CharacterRender.HumanoidIdle();

            dashFrames = CharacterRender.HumanoidDash();

            specialFrames = CharacterRender.HumanoidSpecial();

            specialIntervals = CharacterRender.HumanoidIntervals();

            specialCeilings = CharacterRender.HumanoidCeilings();

            specialFloors = CharacterRender.HumanoidFloors();

            loadedOut = true;

        }

        public virtual void WeaponLoadout()
        {
            
            weaponRender = new();

            weaponRender.LoadWeapon(WeaponRender.weapons.sword);

            weaponRender.swordScheme = IconData.schemes.sword_stars;

            dashFrames[dashes.smash] = CharacterRender.WeaponSmash();

            specialFrames[specials.sweep] = CharacterRender.WeaponSweep();

            idleFrames[idles.alert] = CharacterRender.WeaponAlert();

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

            Dictionary<int, int> normalSequence = new()
            {
                [0] = 2,
                [1] = 1,
                [2] = 0,
                [3] = 3
            };

            foreach (KeyValuePair<int, int> keyValuePair in normalSequence )
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

        public virtual int LayerOffset()
        {
            return 10 + netLayer.Value;

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128) || characterTexture == null)
            {
                return;
            }

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            Vector2 usePosition = SpritePosition(localPosition);

            float drawLayer = ((float)StandingPixel.Y + (float)LayerOffset()) / 10000f ;

            float fade = fadeOut == 0 ? 1f : fadeOut;

            DrawEmote(b);

            if (netSpecial.Value != 0)
            {
                
                specials useSpecial = (specials)netSpecial.Value;

                if (!specialFrames.ContainsKey(useSpecial))
                {

                    useSpecial = specials.none;

                }

                switch (useSpecial)
                {

                    case specials.none:

                        break;

                    case specials.sweep:

                        DrawSweep(b, usePosition, drawLayer, fade);

                        return;

                    case specials.launch:

                        DrawLaunch(b, usePosition, drawLayer, fade);

                        return;

                    case specials.gesture:

                        DrawGesture(b, usePosition, drawLayer, fade);

                        return;

                    case specials.pickup:

                        DrawPickup(b, usePosition, drawLayer, fade);

                        return;

                    default:

                        int useFrame = specialFrame;

                        if (specialFrames[useSpecial][netDirection.Value].Count <= specialFrame)
                        {

                            useFrame = 0;

                        };

                        Microsoft.Xna.Framework.Rectangle useSource = specialFrames[useSpecial][netDirection.Value][useFrame];

                        b.Draw(
                            characterTexture,
                            usePosition,
                            useSource,
                            Color.White * fade,
                            0.0f,
                            new Vector2(useSource.Width / 2, useSource.Height / 2),
                            setScale,
                            SpriteAngle() ? (SpriteEffects)1 : 0,
                            drawLayer
                        );

                        DrawHat(b, usePosition, drawLayer, fade);

                        DrawShadow(b, usePosition, drawLayer);

                        return;

                }

            }
            
            if (netDash.Value != 0)
            {

                DrawDash(b, usePosition, drawLayer, fade);

                return;

            }

            idles useIdle = (idles)netIdle.Value;
                
            if (!idleFrames.ContainsKey(useIdle))
            {

                useIdle = idles.none;

            }

            switch (useIdle)
            {

                case idles.standby:

                    DrawStandby(b, usePosition, drawLayer, fade);

                    break;

                case idles.alert:

                    DrawAlert(b, usePosition, drawLayer, fade);

                    break;

                case idles.idle:

                    DrawIdle(b, usePosition, drawLayer, fade);

                    break;

                case idles.kneel:

                    List<int> kneltOffsets = new()
                    {
                        5,
                        6,
                        3,
                        6

                    };

                    b.Draw(
                        characterTexture,
                        usePosition,
                        idleFrames[useIdle][0][0],
                        Color.White * fade,
                        0.0f,
                        new Vector2(16),
                        setScale,
                        SpriteFlip() ? (SpriteEffects)1 : 0,
                        drawLayer
                    );

                    //DrawHat(b, usePosition + new Vector2(0, setScale * kneltOffsets[netDirection.Value]), drawLayer, fade);

                    DrawShadow(b, usePosition, drawLayer);

                    break;

                case idles.rest:

                    DrawRest(b, usePosition, drawLayer,fade);

                    break;

                case idles.jump:

                    b.Draw(
                        characterTexture,
                        usePosition - new Vector2(0, 24),
                        idleFrames[useIdle][netDirection.Value][0],
                        Color.White * fade,
                        0.0f,
                        new Vector2(16),
                        setScale,
                        SpriteAngle() ? (SpriteEffects)1 : 0,
                        drawLayer
                    );

                    DrawHat(b, usePosition - new Vector2(0,24), drawLayer, fade);

                    DrawShadow(b, usePosition, drawLayer);

                    break;

                case idles.daze:

                    b.Draw(
                        Mod.instance.iconData.displayTexture,
                        usePosition - new Vector2(0, overhead == 0 ? 144 : overhead),
                        IconData.DisplayRectangle(IconData.displays.glare),
                        Color.White,
                        0f,
                        new Vector2(8),
                        setScale,
                        SpriteEffects.None,
                        drawLayer
                    );

                    DrawWalk(b, usePosition, drawLayer, fade);

                    break;

                default:

                    DrawWalk(b, usePosition, drawLayer, fade);

                    break;

            }

        }

        public virtual Vector2 SpritePosition(Vector2 localPosition)
        {

            return localPosition + new Vector2(32, 64 - (int)(16 * setScale));

        }

        public virtual bool SpriteAngle()
        {
 
            return (netDirection.Value % 2 == 0 && netAlternative.Value == 3);

        }

        public bool SpriteFlip()
        {

            return (netDirection.Value % 2 == 0 && netAlternative.Value == 3) || netDirection.Value == 3;

        }

        public override void DrawEmote(SpriteBatch b)
        {
            float drawLayer = (float)StandingPixel.Y / 10000f + 0.001f;

            if (IsEmoting && !Game1.eventUp)
            {

                if(currentEmote == 12)
                {

                    return;

                }

                b.Draw(
                    Game1.emoteSpriteSheet, 
                    Game1.GlobalToLocal(Position)-new Vector2(0,overhead == 0 ? 144 : overhead), 
                    new Microsoft.Xna.Framework.Rectangle(base.CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, base.CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16), 
                    Color.White, 
                    0f, 
                    Vector2.Zero, 
                    4f, 
                    SpriteEffects.None, 
                    drawLayer
                );
            
            }
            else if (netSceneActive.Value && eventName != null)
            {
                
                if (Mod.instance.eventRegister.ContainsKey(eventName))
                {

                    if (Mod.instance.eventRegister[eventName].dialogueLoader.ContainsKey(Name))
                    {

                        b.Draw(
                            Mod.instance.iconData.displayTexture,
                            Game1.GlobalToLocal(Position) - new Vector2(0, overhead == 0 ? 144 : overhead),
                            Mod.instance.iconData.QuestDisplay(Journal.Quest.questTypes.approach),
                            Color.White,
                            0f,
                            Vector2.Zero,
                            4f,
                            SpriteEffects.None,
                            drawLayer
                        );

                    }

                }
                
            }
            else if (Mod.instance.dialogue.ContainsKey(characterType))
            {

                if(Mod.instance.dialogue[characterType].promptDialogue.Count > 0)
                {

                    b.Draw(
                        Mod.instance.iconData.displayTexture, 
                        Game1.GlobalToLocal(Position) - new Vector2(0, overhead == 0 ? 144 : overhead),
                        Mod.instance.iconData.QuestDisplay(Mod.instance.dialogue[characterType].promptDialogue.First().Value), 
                        Color.White, 
                        0f, 
                        Vector2.Zero, 
                        4f, 
                        SpriteEffects.None, 
                        drawLayer
                    );

                }

            }

        }

        public virtual void DrawSweep(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {
            Rectangle useFrame = specialFrames[(specials)netSpecial.Value][netDirection.Value][specialFrame];

            b.Draw(
                characterTexture,
                spritePosition,
                useFrame,
                Color.White * fade,
                0.0f,
                new Vector2(useFrame.Width/2, useFrame.Height/2),
                setScale,
                SpriteAngle() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawHat(b, spritePosition, drawLayer, fade);

            DrawShadow(b, spritePosition, drawLayer);

        }
        
        public virtual void DrawLaunch(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            Rectangle useFrame = specialFrames[specials.launch][netDirection.Value][specialFrame];

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

            DrawHat(b, spritePosition, drawLayer, fade);

            DrawShadow(b, spritePosition, drawLayer);

        }
       
        public virtual void DrawGesture(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {
            Rectangle useFrame = specialFrames[specials.gesture][0][0];

            b.Draw(
                characterTexture,
                spritePosition,
                useFrame,
                Color.White * fade,
                0.0f,
                new Vector2(useFrame.Width / 2, useFrame.Height / 2),
                setScale,
                SpriteFlip() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawHat(b, spritePosition, drawLayer, fade);

            DrawShadow(b, spritePosition, drawLayer);

        }

        public virtual void DrawPickup(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
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
                SpriteFlip() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawShadow(b, spritePosition, drawLayer);

        }

        public virtual void DrawDash(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            int dashSeries = netDirection.Value + (netDashProgress.Value * 4);

            int dashSetto = Math.Min(dashFrame, (dashFrames[(dashes)netDash.Value][dashSeries].Count - 1));

            Rectangle useFrame = dashFrames[(dashes)netDash.Value][dashSeries][dashSetto];

            b.Draw(
                characterTexture,
                spritePosition - new Vector2(0,dashHeight),
                useFrame,
                Color.White * fade,
                0f,
                new Vector2(useFrame.Width / 2, useFrame.Height / 2),
                setScale,
                SpriteAngle() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawHat(b, spritePosition - new Vector2(0, dashHeight), drawLayer, fade);

            DrawShadow(b, spritePosition, drawLayer);

        }

        public virtual void DrawAlert(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            Rectangle alertFrame = idleFrames[idles.alert][netDirection.Value][0];

            b.Draw(
                 characterTexture,
                 spritePosition,
                 alertFrame,
                 Color.White * fade,
                 0f,
                 new Vector2(16),
                 setScale,
                 SpriteAngle() ? (SpriteEffects)1 : 0,
                 drawLayer
             );

            DrawHat(b, spritePosition, drawLayer, fade);

            DrawShadow(b, spritePosition, drawLayer);

            if (weaponRender != null) {

                weaponRender.DrawWeapon(b, spritePosition - new Vector2(16) * setScale, drawLayer, new() { scale = setScale, source = alertFrame, flipped = SpriteAngle() });

            }

        }

        public virtual void DrawStandby(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            Rectangle standbyFrame = idleFrames[idles.standby][netDirection.Value][0];

            b.Draw(
                 characterTexture,
                 spritePosition,
                 standbyFrame,
                 Color.White * fade,
                 0f,
                 new Vector2(16),
                 setScale,
                 SpriteAngle() ? (SpriteEffects)1 : 0,
                 drawLayer
             );

            DrawHat(b, spritePosition, drawLayer, fade);

            DrawShadow(b, spritePosition, drawLayer);

        }
        
        public virtual void DrawRest(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            b.Draw(
                characterTexture,
                spritePosition,
                idleFrames[idles.rest][0][0],
                Color.White * fade,
                0.0f,
                new Vector2(16),
                setScale,
                0,
                drawLayer
            );

            DrawUnder(b, spritePosition, drawLayer, idleFrames[idles.rest][0][0], false);

        }

        public virtual void DrawIdle(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {

            int standbyTo = IdleFrame(idles.idle);

            Rectangle standbyFrame = idleFrames[idles.idle][netDirection.Value][standbyTo];

            b.Draw(
                 characterTexture,
                 spritePosition,
                 standbyFrame,
                 Color.White * fade,
                 0f,
                 new Vector2(16),
                 setScale,
                 SpriteAngle() ? (SpriteEffects)1 : 0,
                 drawLayer
             );

            DrawHat(b, spritePosition, drawLayer, fade);

            DrawShadow(b, spritePosition, drawLayer);

        }

        public int IdleFrame(idles idleType)
        {

            int count = idleFrames[idleType][0].Count();

            if (count == 0) { return 0; }

            int frame = (int)((Game1.currentGameTime.TotalGameTime.TotalSeconds + (int)(Position.Y / 64)) % count);

            return frame;

        }

        public virtual void DrawWalk(SpriteBatch b, Vector2 spritePosition, float drawLayer, float fade)
        {
            Rectangle useFrame = walkFrames[netDirection.Value][moveFrame];

            b.Draw(
                characterTexture,
                spritePosition,
                useFrame,
                Color.White * fade,
                0f,
                new Vector2(useFrame.Width / 2, useFrame.Height / 2),
                setScale,
                SpriteAngle() ? (SpriteEffects)1 : 0,
                drawLayer
            );

            DrawHat(b, spritePosition, drawLayer, fade);

            DrawShadow(b, spritePosition, drawLayer);

        }

        public virtual void DrawShadow(SpriteBatch b, Vector2 spritePosition, float drawLayer)
        {

            Vector2 shadowPosition = spritePosition + new Vector2(0, setScale * 14);

            float offset = 2f + (Math.Abs(0 - (walkFrames[0].Count() / 2) + moveFrame) * 0.1f);

            if(netDirection.Value % 2 == 1)
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
                setScale/ offset, 
                0, 
                drawLayer - 0.0001f
            );

        }

        public virtual void DrawUnder(SpriteBatch b, Vector2 spritePosition, float drawLayer, Rectangle frame, bool flip)
        {

            b.Draw(
                characterTexture,
                spritePosition + new Vector2(2, 4),
                frame,
                Color.Black * 0.25f,
                0f,
                new Vector2(16),
                setScale,
                flip ? (SpriteEffects)1 : 0,
                drawLayer - 0.001f
            );

        }
        
        public virtual void DrawHat(SpriteBatch b, Vector2 localPosition, float drawLayer, float fade)
        {

        }

        public override Rectangle GetBoundingBox()
        {

            return new Rectangle((int)Position.X + 8, (int)Position.Y + 8, 48, 48);

        }

        public virtual Rectangle GetHitBox()
        {
            
            return new Rectangle((int)Position.X - 32, (int)Position.Y - 32, 128, 128);
        
        }

        public override void reloadSprite(bool onlyAppearance = false)
        {
            
            base.reloadSprite(onlyAppearance);
            
            Portrait = CharacterHandle.CharacterPortrait(characterType);

        }

        public override void reloadData()
        {
            CharacterDisposition characterDisposition = CharacterHandle.CharacterDisposition(characterType);
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
            
            if(characterType == CharacterHandle.characters.none)
            {

                characterType = CharacterHandle.characters.Effigy;

            }

            DefaultMap = CharacterHandle.CharacterLocation(CharacterHandle.CharacterHome(characterType));

            DefaultPosition = CharacterHandle.CharacterStart(CharacterHandle.CharacterHome(characterType));


        }

        public override void receiveGift(StardewValley.Object o, Farmer giver, bool updateGiftLimitInfo = true, float friendshipChangeMultiplier = 1, bool showResponse = true)
        {

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {

            if (Mod.instance.Config.actionButtons.GetState() == SButtonState.Held)
            {

                return false;

            }

            if (Mod.instance.Config.specialButtons.GetState() == SButtonState.Held)
            {

                return false;

            }

            if (Mod.instance.eventRegister.ContainsKey(Rite.eventTransform))
            {

                Mod.instance.CastMessage("Unable to converse while transformed");

                return false;
            
            }

            /*if (l.Name != currentLocation.Name)
            {

                if (!Context.IsMainPlayer)
                {

                    Mod.instance.characters[characterType] = this;

                }

                CharacterMover mover = new(characterType);

                mover.RemovalSet(l.Name);

                Mod.instance.movers[characterType] = mover;

                return false;

            }*/

            foreach (NPC character in currentLocation.characters)
            {

                if (character is StardewValley.Monsters.Monster monster && (double)Vector2.Distance(Position, monster.Position) <= 800.0)
                {

                    return false;

                }

            }

            if (netDash.Value != 0) //|| netSpecialActive.Value)
            {

                return false;

            }

            if (!EngageDialogue(who))
            {
                
                return false;
            
            }

            LookAtTarget(who.Position, true);

            return true;

        }

        public virtual bool EngageDialogue(Farmer who)
        {

            if (!Mod.instance.dialogue.ContainsKey(characterType))
            {
                
                Mod.instance.dialogue[characterType] = new(characterType);

            }

            if (netSceneActive.Value)
            {

                if(eventName == null)
                {

                    return false;

                }

                if (Mod.instance.eventRegister.ContainsKey(eventName))
                {

                    LookAtTarget(who.Position, true);

                    if (Mod.instance.eventRegister[eventName].DialogueNext(this))
                    {
                        
                        Halt();

                        clearTextAboveHead();

                        return true;

                    };

                }

                return false;

            }

            if(Mod.instance.activeEvent.Count > 0)
            {

                return false;

            }

            Halt();

            Mod.instance.dialogue[characterType].DialogueApproach();

            return true;

        }

        public override void Halt()
        {
            
            if (!Context.IsMainPlayer)
            {

                QueryData queryData = new()
                {

                    name = characterType.ToString(),
                    
                };

                Mod.instance.EventQuery(queryData, QueryData.queries.HaltCharacter);

                return;

            }

            StopMoving();

            idleTimer = 180;

        }

        public virtual void ResetActives(bool clearEvents = false)
        {

            ClearIdle();

            ClearMove();

            StopMoving();

            ClearSpecial();

            ResetTimers();

            SettleOccupied();

            if (clearEvents)
            {
                
                eventVectors.Clear();

            }

        }

        public virtual void ResetTimers()
        {

            idleTimer = 0;

            moveTimer = 0;

            specialTimer = 0;

            cooldownTimer = 0;

            hitTimer = 0;

            lookTimer = 0;

            collideTimer = 0;

            pushTimer = 0;

            dashHeight = 0;

            followTimer = 0;

            attentionTimer = 0;

            stationaryTimer = 0;

        }

        public virtual void ClearIdle()
        {

            netIdle.Set(0);

            idleTimer = 0;

            onAlert = false;

        }

        public virtual void ClearMove()
        {

            pathActive = pathing.none;

            netMovement.Set(0);

            destination = Vector2.Zero;

            traversal.Clear();

            netDash.Set(0);

            netDashProgress.Set(0);

            pathProgress = 0;

            pathTotal = 0;

            dashFrame = 0;

            stationaryTimer = moveInterval * 2;

        }

        public virtual void StopMoving()
        {

            moveTimer = 0;

            moveFrame = 0;


        }

        public virtual void ClearSpecial()
        {

            netSpecial.Set(0);

            specialTimer = 0;

            specialFrame = 0;

            workVector = Vector2.Zero;

        }

        public void LookAtTarget(Vector2 target, bool force = false)
        {
            
            if (lookTimer > 0 && !force) { return; }

            List<int> directions = ModUtility.DirectionToTarget(Position, target);

            moveDirection = directions[0];

            if (directions[3] == 1)
            {

                altDirection = directions[1];

            }

            netDirection.Set(moveDirection);

            netAlternative.Set(altDirection);

            lookTimer = (int)(2f * MoveSpeed(Vector2.Distance(Position,target)));

        }

        public override void performTenMinuteUpdate(int timeOfDay, GameLocation l)
        {

        }

        public override void behaviorOnFarmerPushing()
        {

            if (Context.IsMainPlayer && !netSceneActive.Value)
            {

                pushTimer += 2;

                if(pushTimer > 30)
                {
                    
                    if (ChangeBehaviour(true))
                    {

                        TargetRandom(4);

                        if(idleTimer > 0)
                        {

                            Vector2 offset = ModUtility.DirectionAsVector(ModUtility.DirectionToTarget(Position, Game1.player.Position)[2]) * 64;

                            Position = Game1.player.Position + offset;

                            Mod.instance.iconData.AnimateQuickWarp(currentLocation, Position, false, warpDisplay);

                            if (Mod.instance.trackers.ContainsKey(characterType))
                            {

                                attentionTimer = 180;

                            }

                        }

                    }

                    pushTimer = 0;

                }

            }

        }

        public override void update(GameTime time, GameLocation location)
        {

            if (!checkUpdate())
            {
                
                if (restSet)
                {

                    CheckRest();

                }

                return;

            }

            normalUpdate(time, location);

            if (!Context.IsMainPlayer)
            {

                UpdateMultiplayer();

                return;

            }

            if (modeActive == mode.scene)
            {

                ProgressScene();

                return;

            }

            UpdateBehaviour();

            ChooseBehaviour();

            Traverse();

        }

        public virtual void CheckRest()
        {

            if (idleTimer <= 0)
            {

                if (currentLocation.Name != CharacterHandle.CharacterLocation(CharacterHandle.CharacterRest(characterType)))
                {

                    TargetRest();

                }

            }

        }

        public virtual bool checkUpdate()
        {

            if (!loadedOut)
            {
                LoadOut();
            }

            if (modeActive == mode.random && currentLocation != Game1.player.currentLocation)
            {

                return false;

            }

            return true;

        }

        public virtual void normalUpdate(GameTime time, GameLocation location)
        {

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

                            float newAlpha = textAboveHeadAlpha - 0.04f;

                            textAboveHeadAlpha = newAlpha < 0f ? 0f : newAlpha;

                        }
                            
                    
                    }
                }

                updateEmote(time);

            }

        }

        // ======================================== SET BEHAVIOUR
        
        public virtual void ProgressScene()
        {

            if(eventName == null || !Mod.instance.eventRegister.ContainsKey(eventName))
            {

                SwitchToMode(mode.random,Game1.player);

            }

            if(netSpecial.Value != 0)
            {

                UpdateSpecial();

            }

            if (eventVectors.Count > 0)
            {

                KeyValuePair<int, Vector2> eventVector = eventVectors.First();

                float distance = Vector2.Distance(Position, eventVector.Value);

                Position = ModUtility.PathMovement(Position, eventVector.Value, MoveSpeed(distance));

                LookAtTarget(eventVector.Value);

                if(netDash.Value != 0)
                {
                    
                    if (pathProgress > 0)
                    {
                        
                        pathProgress--;

                    }

                }

                UpdateMove();

                if (Vector2.Distance(Position, eventVector.Value) <= 4f)
                {

                    Position = eventVector.Value;

                    eventVectors.Remove(eventVector.Key);

                    if (Mod.instance.eventRegister.ContainsKey(eventName))
                    {

                        Mod.instance.eventRegister[eventName].EventScene(eventVector.Key);

                    }
                    
                    if(eventVectors.Count > 0)
                    {

                        LookAtTarget(eventVectors.First().Value, true);

                    }
                    else
                    {
                        
                        ClearMove();
                    
                    }

                }

            }
            else
            {

                StopMoving();

            }

        }

        public virtual void TargetEvent(int key, Vector2 target, bool clear = true)
        {

            pathActive = pathing.scene;

            if (clear)
            {

                eventVectors.Clear();

            }

            ClearIdle();

            ClearSpecial();

            eventVectors.Add(key, target);

            destination = ModUtility.PositionToTile(target); //target / 64; //ModUtility.PositionToTile(target);

            if (eventVectors.Count == 1)
            {

                LookAtTarget(target, true);

            }

        }

        public virtual bool ChangeBehaviour(bool urgent = false)
        {

            if (netDash.Value != 0)
            {

                return false;

            }

            if (netSpecial.Value != 0)
            {

                return false;

            }

            if (urgent)
            {

                return true;

            }

            if (destination != Vector2.Zero)
            {

                if (ArrivedDestination())
                {

                    return true;

                }

                if(pathActive == pathing.roam)
                {

                    if(roamSonar <= 0)
                    {

                        TargetWork();

                        roamSonar = 60;

                    }

                    roamSonar--;

                    return false;

                }

                if(pathActive == pathing.player)
                {

                    if (TrackToClose())
                    {

                        ClearMove();

                        return true;

                    }

                    if (modeActive == mode.track)
                    {
                        
                        if (TrackToFar())
                        {
                            
                            followTimer = 0;
                            
                            ClearMove();

                            return true;

                        }

                    }

                }

                if(pathActive == pathing.circling)
                {

                    if (CircleToFar())
                    {

                        return true;

                    }

                }

                return false;

            }

            if (idleTimer > 0)
            {

                if (modeActive == mode.track)
                {

                    // need to stay where the action is

                    if (TrackToFar(640,7))
                    {
                        
                        followTimer = 0;

                        ClearIdle();

                        return true;

                    }

                }

                if (collideTimer <= 0)
                {
                    
                    if (CollideCharacters(occupied))
                    {

                        ClearIdle();

                        TargetRandom(2);

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

                    if (TargetRest())
                    {

                        return;

                    }
                    
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

                case mode.random:

                    if (TargetRest())
                    {

                        return;

                    }

                    if (TargetWork())
                    {

                        return;

                    }

                    break;

            }

            TargetRandom();

        }

        public virtual bool TargetIdle(int timer = -1)
        {

            StopMoving();

            if (!netSceneActive.Value)
            {

                if (ModUtility.TileAccessibility(currentLocation, occupied) != 0)
                {

                    switch (modeActive)
                    {

                        case mode.track:

                            Mod.instance.trackers[characterType].WarpToPlayer();

                            return true;

                        case mode.random:
                            
                            stuck++;

                            if (stuck >= 4)
                            {

                                CharacterHandle.locations home = CharacterHandle.CharacterHome(characterType);

                                CharacterMover mover = new( this, CharacterHandle.CharacterLocation(home), CharacterHandle.CharacterStart(home), true);

                                Mod.instance.movers[characterType] = mover;

                                stuck = 0;

                                return true;

                            }

                            break;

                        case mode.roam:

                            if (TargetRoam())
                            {

                                return true;

                            }
                            if (PathTarget(tether, 2, 2))
                            {

                                return true;

                            }
                            else
                            {
                                stuck++;

                                if (stuck >= 4)
                                {

                                    CharacterMover mover = new(this, Game1.getFarm(), CharacterHandle.CharacterStart(CharacterHandle.locations.farm), true);

                                    Mod.instance.movers[characterType] = mover;

                                    stuck = 0;

                                    return true;

                                }

                            }

                            break;

                    }

                }

            }

            stuck = 0;

            if (timer == -1)
            {

                switch (modeActive)
                {

                    case mode.track:
                    case mode.roam:

                        timer = 180;

                        break;

                    default: //mode.scene: mode.standby:

                        timer = 480;

                        break;

                }

            }

            Random random = new();

            if(random.Next(2) == 0)
            {

                moveDirection = 2;

                netDirection.Set(2);

            }

            netIdle.Set((int)idles.idle);

            idleTimer = timer;

            return true;

        }

        public virtual bool TargetMonster()
        {

            List<StardewValley.Monsters.Monster> monsters = ModUtility.MonsterProximity(currentLocation, new() { Position, }, 640f, true);

            if (monsters.Count > 0)
            {

                if (cooldownTimer <= 0)
                {

                    foreach (StardewValley.Monsters.Monster monster in monsters)
                    {

                        if (MonsterAttack(monster))
                        {

                            return true;

                        }

                    }

                }

                onAlert = true;

                return TargetIdle(180);

            }

            return false;

        }

        public virtual bool MonsterAttack(StardewValley.Monsters.Monster monster)
        {

            float distance = Vector2.Distance(Position, monster.Position);

            string terrain = ModUtility.GroundCheck(currentLocation, new Vector2((int)(monster.Position.X/64),(int)(monster.Position.Y/64)));

            if (terrain != "ground")
            {

                if (!specialDisable)
                {

                    return SpecialAttack(monster);

                }

                return false;

            }

            if (distance >= 192f)
            {

                switch (Mod.instance.randomIndex.Next(3))
                {
                    case 0:

                        if (!specialDisable)
                        {
                            
                            return SpecialAttack(monster);

                        }

                        break;

                    case 1:

                        return SmashAttack(monster);

                }

                return PathTarget(monster.Position, 2, 1);

            }

            return SweepAttack(monster);

        }

        public virtual bool SmashAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            if (PathTarget(monster.Position, 2, 1))
            {
                
                pathActive = pathing.monster;

                SetDash(monster.Position,true);

                cooldownTimer = cooldownInterval / 2;

                return true;

            }

            return false;

        }

        public virtual bool SweepAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            if (PathTarget(monster.Position, 2, 0))
            {

                pathActive = pathing.monster;

                netSpecial.Set((int)specials.sweep);

                cooldownTimer = cooldownInterval / 2;

                specialFrame = 0;

                specialTimer = specialFrames[specials.sweep][0].Count() * specialIntervals[specials.sweep];

                //int stun = Math.Max(monster.stunTime.Value, 500);

                //monster.stunTime.Set(stun);

                return true;

            }

            return false;

        }

        public virtual bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecial.Set((int)specials.invoke);

            specialTimer = 90;

            cooldownTimer = cooldownInterval;

            LookAtTarget(monster.Position, true);

            SpellHandle fireball = new(Game1.player, new() { monster, }, Mod.instance.CombatDamage() / 2);

            fireball.origin = GetBoundingBox().Center.ToVector2();

            fireball.type = SpellHandle.spells.missile;

            fireball.missile = IconData.missiles.fireball;
            
            fireball.display = IconData.impacts.impact;

            fireball.added = new() { SpellHandle.effects.aiming, };

            fireball.power = 3;

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }

        public virtual void SpecialSweep()
        {

            if (specialTimer == specialIntervals[specials.sweep])
            {

                ConnectSweep();

            }

        }

        public virtual void ConnectSweep()
        {

            SpellHandle swipeEffect = new(Game1.player, Position, 192, Mod.instance.CombatDamage() / 2);

            swipeEffect.type = SpellHandle.spells.swipe;

            swipeEffect.added = new() { SpellHandle.effects.push, };

            swipeEffect.sound = SpellHandle.sounds.swordswipe;

            Mod.instance.spellRegister.Add(swipeEffect);

        }

        public virtual void SetDash(Vector2 target, bool smash = false)
        {

            LookAtTarget(target,true);

            StopMoving();

            if (!smash)
            {

                netDash.Set((int)dashes.dash);

            }
            else
            {

                netDash.Set((int)dashes.smash);

            }

            pathFrom = Position;

            float pathDistance = Vector2.Distance(pathFrom, target);

            pathIncrement = ModUtility.PathFactor(Position, target) * MoveSpeed(pathDistance);

            pathProgress = (int)(Vector2.Distance(Position, target) / Vector2.Distance(new(0, 0), pathIncrement));

            pathTotal = pathProgress;

            pathSegment = dashInterval;

            int pathRequirement;

            if (!smash)
            {

                pathRequirement = dashFrames[dashes.dash][0].Count + dashFrames[dashes.dash][4].Count + dashFrames[dashes.dash][8].Count;
            }
            else
            {
                pathRequirement = dashFrames[dashes.smash][0].Count + dashFrames[dashes.smash][4].Count + dashFrames[dashes.smash][8].Count;

            }

            int pathSqueeze = (int)(pathProgress / pathRequirement);

            if(pathSqueeze < dashInterval)
            {

                pathSegment = pathSqueeze;

            }

        }

        public virtual bool TargetTrack()
        {
            
            if(followTimer > 0)
            {

                return false;

            }

            if (!Mod.instance.trackers.ContainsKey(characterType))
            {
                
                return false;
            
            }

            if (TrackToClose(192))
            {
                
                if(attentionTimer == 0)
                {
                    
                    // idle or random direction

                    followTimer = 120;
                    
                    return false;

                }

                // keep attention on farmer

                idleTimer = 10;

                LookAtTarget(Mod.instance.trackers[characterType].followPlayer.Position);

                return true;

            }

            if (TrackToFar())
            {

                Vector2 lastPosition = Position;

                if (Mod.instance.trackers[characterType].WarpToPlayer())
                {

                    return true;

                }

            }

            if (PathPlayer())
            {

                return true;

            }

            if (PathTrack())
            {

                return true;

            }

            return false;

        }

        public virtual bool TrackToClose(int close = 128)
        {

            if (Mod.instance.trackers.ContainsKey(characterType))
            {

                if (Vector2.Distance(Position, Mod.instance.trackers[characterType].TrackPosition()) <= close)
                {

                    return true;

                }

            }
            else
            {

                if (Vector2.Distance(Position, Game1.player.Position) <= close)
                {

                    return true;

                }

            }

            return false;

        }

        public virtual bool TrackToFar(int limit = 960, int nodeLimit = 20)
        {

            if (Mod.instance.trackers[characterType].nodes.Count >= nodeLimit)
            {

                return true;

            }

            if (Vector2.Distance(Position, Mod.instance.trackers[characterType].TrackPosition()) >= limit || !Utility.isOnScreen(Position, 128))
            {

                return true;

            }

            return false;

        }

        public virtual bool CircleToFar()
        {

            if(!Utility.isOnScreen(Position, 128))
            {

                return true;

            }

            Vector2 check = Mod.instance.trackers[characterType].TrackPosition();

            if (Vector2.Distance(Position, check) >= 640)
            {
                
                List<int> checks = ModUtility.DirectionToTarget(Position, check);

                if (checks[0] == netDirection.Value)
                {
                    
                    return true;

                }

            }

            return false;

        }

        public virtual bool PathPlayer()
        {

            // check if can walk / jump to player

            if (PathTarget(Mod.instance.trackers[characterType].TrackPosition(), 1, 2))
            {

                // dont need the tracked path now

                pathActive = pathing.player;

                Mod.instance.trackers[characterType].nodes.Clear();

                //followTimer = 90 + (30 * Mod.instance.randomIndex.Next(5));

                return true;

            }

            return false;

        }

        public virtual bool PathTrack()
        {

            Dictionary<Vector2, int> paths = Mod.instance.trackers[characterType].NodesToTraversal();

            if (paths.Count > 0)
            {

                // walk / jump to the start of the path

                if (PathTarget(paths.Keys.First() * 64, 1, 0))
                {

                    // add remaining path segments to traversal

                    if (paths.Count > 1)
                    {

                        for (int p = paths.Count - 2; p >= 0; p--)
                        {

                            traversal.Append(paths.ElementAt(p));

                        }

                    }

                }
                else
                {

                    // might have to warp to the start of the path

                    paths[paths.ElementAt(0).Key] = 2;

                    traversal = paths;

                }

                if(traversal.Count > 0)
                {

                    pathActive = pathing.player;

                    destination = traversal.Keys.Last();

                    return true;

                }

            }

            return false;

        }

        public virtual bool TargetRoam()
        {

            if (roamVectors.Count == 0)
            {

                return false;

            }

            Vector2 roamVector = roamVectors[roamIndex];

            if (roamVector.X < 0)
            {

                UpdateRoam();

                TargetIdle(720);

                netIdle.Set((int)idles.standby);

                return true;

            }

            if (Vector2.Distance(roamVectors[roamIndex], Position) <= 192f || roamLapse < Game1.currentGameTime.TotalGameTime.TotalMinutes)
            {

                UpdateRoam();

                return true;

            }

            if (PathTarget(roamVectors[roamIndex], 2, 2))
            {
                
                pathActive = pathing.roam;

                return true;

            }

            return false;

        }

        public virtual void TargetRandom(int level = 8)
        {
            
            Random random = new Random();

            int decision = random.Next(level);

            int stretch = 0;

            if (Mod.instance.trackers.ContainsKey(characterType))
            {

                

            }
            else
            if (Vector2.Distance(tether, Position) >= 1280)
            {

                stretch = 4;

            }
            else if (Vector2.Distance(tether, Position) >= 960)
            {

                stretch = 3;

            }
            else if (Vector2.Distance(tether,Position) >= 640)
            {

                stretch = 2;

            }
            else if (Vector2.Distance(tether, Position) >= 320)
            {

                stretch = 1;

            }

            switch (decision)
            {
                case 0:
                case 1:
                case 2:
                case 3:

                    int newDirection = random.Next(10);

                    if (newDirection >= (8 -stretch))
                    {

                        newDirection = ModUtility.DirectionToTarget(Position, tether)[2];

                    }

                    List<int> directions = new()
                    {

                        (newDirection + 1) % 8,
                        (newDirection + 2) % 8,
                        (newDirection + 7) % 8,

                    };

                    foreach (int direction in directions)
                    {

                        if (PathTarget(occupied*64, 0, 1+ stretch, direction))
                        {

                            pathActive = pathing.random;

                            return;

                        }

                    }

                    break;

            }

            TargetIdle();

        }

        public virtual bool TargetWork()
        {

            return false;

        }

        public virtual void PerformWork()
        {


        }

        public virtual bool TargetRest()
        {

            if (!restSet)
            {

                return false;

            }

            if(Game1.timeOfDay < 2100)
            {

                return false;

            }

            CharacterHandle.locations characterRest = CharacterHandle.CharacterRest(characterType);

            if (currentLocation.Name != CharacterHandle.CharacterLocation(characterRest))
            {

                ResetActives();

                CharacterHandle.CharacterWarp(this, characterRest, false);

                idleTimer = 180;

                collideTimer = 180;

                return true;

            }

            Vector2 restPosition = CharacterHandle.CharacterStart(CharacterHandle.locations.rest, characterType);

            if(Position != restPosition)
            {

                Position = restPosition;

                Mod.instance.iconData.AnimateQuickWarp(currentLocation, Position, true, warpDisplay);

            }

            netIdle.Set((int)idles.rest);

            idleTimer = 300;

            collideTimer = 300;

            return true;

        }

        // ======================================== UPDATE

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

            if (attentionTimer > 0)
            {

                attentionTimer--;

            }


        }

        public virtual void UpdateMultiplayer()
        {
            
            if (netIdle.Value != 0)
            {
                
                idleTimer++;

                return;

            }

            if (netSpecial.Value != 0)
            {

                specialTimer++;

                specials specialCheck = (specials)netSpecial.Value;

                if (!specialIntervals.ContainsKey(specialCheck))
                {

                    specialCheck = specials.none;

                }

                if (specialTimer == specialIntervals[specialCheck])
                {

                    specialFrame++;

                    if (specialFrame > specialCeilings[specialCheck])
                    {

                        specialFrame = specialFloors[specialCheck];

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

                if (netDash.Value != 0)
                {
                    if (netDashProgress.Value == 0 && dashHeight <= dashPeak)
                    {

                        dashHeight += 2;

                    }
                    else if (netDashProgress.Value == 2 && dashHeight > 1)
                    {

                        dashHeight -= 2;

                    }
                    else
                    {

                        dashHeight = 0;

                    }

                    if (netDashProgress.Value != trackDashProgress)
                    {

                        dashFrame = 0;

                        trackDashProgress = netDashProgress.Value;

                        moveTimer = moveInterval;

                    }

                }
                else if (dashHeight > 1)
                {

                    dashHeight -= 2;

                }
                else
                {

                    dashHeight = 0;

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

            if (netIdle.Value > 0)
            {

                CheckAlert();

                if (idleTimer <= 0)
                {

                    ClearIdle();

                    ClearMove();

                    return;

                }

            }

        }

        public virtual void CheckAlert()
        {

            if (Context.IsMainPlayer)
            {

                if (idleTimer % 10 == 0)
                {

                    List<StardewValley.Monsters.Monster> monsters = ModUtility.MonsterProximity(currentLocation, new() { Position }, 960);

                    if (monsters.Count > 0)
                    {

                        netIdle.Set((int)idles.alert);

                        LookAtTarget(monsters.First().Position, true);

                    }

                }

            }

        }

        public virtual void UpdateMove()
        {

            if (destination == Vector2.Zero)
            {

                return;

            }

            if (moveTimer > 0)
            {

                idleTimer = 0;

                moveTimer--;

            }

            float distance = Vector2.Distance(Position, destination*64);

            if (netDash.Value != 0)
            {

                DashAscension();

                if(pathProgress % pathSegment != 0)
                {
                    
                    return;

                }

                dashFrame++;

                if (pathProgress + (pathSegment * dashFrames[(dashes)netDash.Value][0].Count) <= pathTotal)
                {

                    if (netDashProgress.Value != 1)
                    {

                        netDashProgress.Set(1);

                        dashFrame = 0;

                    }

                }

                if (pathProgress <= (pathSegment * dashFrames[(dashes)netDash.Value][8].Count))
                {

                    if (netDashProgress.Value != 2)
                    {

                        netDashProgress.Set(2);

                    }

                }

                if((dashes)netDash.Value == dashes.smash)
                {

                    if (pathProgress == pathSegment)
                    {

                        ConnectSweep();

                    }

                }

                return;

            }

            if (moveTimer <= 0)
            {

                moveTimer = (int)MoveSpeed(distance, true);

                moveFrame++;

                if (moveFrame >= walkFrames[0].Count)
                {

                    moveFrame = 1;

                }

            }

        }

        public virtual void DashAscension()
        {

            if (dashPeak == 0)
            {

                return;

            }

            Vector2 dashPoint = destination;

            if (traversal.Count > 0)
            {

                dashPoint = traversal.First().Key;

            }

            float distance = Vector2.Distance(pathFrom, dashPoint * 64);

            float length = distance / 2;

            float lengthSq = (length * length);

            float heightFr = 4 * dashPeak;

            float coefficient = lengthSq / heightFr;

            int midpoint = (int)(pathTotal / 2);

            float newHeight = 0;

            if (pathProgress != midpoint)
            {
                
                float newLength;

                if (pathProgress < midpoint)
                {

                    newLength = length * (midpoint - pathProgress) / midpoint;

                }
                else
                {

                    newLength = (length * (pathProgress - midpoint) / midpoint);

                }

                float newLengthSq = newLength * newLength;

                float coefficientFr = (4 * coefficient);

                newHeight = newLengthSq / coefficientFr;

            }

            dashHeight = dashPeak - (int)newHeight;

            if(dashHeight < 0)
            {

                dashHeight = 0;

            }

        }

        public virtual void UpdateSpecial()
        {

            if (specialTimer > 0)
            {

                specialTimer--;

            }

            if (specialTimer <= 0)
            {

                ClearSpecial();

                return;

            }

            specials specialCheck = (specials)netSpecial.Value;

            if(specialCheck == specials.sweep)
            {
                
                SpecialSweep();

            }

            if(workVector != Vector2.Zero)
            {

                PerformWork();

            }

            if (!specialIntervals.ContainsKey(specialCheck))
            {

                specialCheck = specials.none;

                specialIntervals[specials.none] = 30;

                specialCeilings[specials.none] = 1;

                specialFloors[specials.none] = 0;

            }

            if (specialTimer % specialIntervals[specialCheck] == 0)
            {

                specialFrame++;

                if (specialFrame > specialCeilings[specialCheck])
                {

                    specialFrame = specialFloors[specialCheck];

                }

            }

        }

        public void UpdateRoam()
        {

            roamLapse = Game1.currentGameTime.TotalGameTime.TotalMinutes + 1.0;

            roamIndex++;

            if (roamIndex == roamVectors.Count)
            {
                
                roamVectors.Clear();

                roamIndex = 0;

                roamLapse = Game1.currentGameTime.TotalGameTime.TotalMinutes + 1.0;

                roamVectors = RoamAnalysis();

                return;

            }

            tether = roamVectors[roamIndex];

        }

        public virtual float MoveSpeed(float distance = 0, bool useFrames = false)
        {

            float useSpeed = gait;

            float useFrame = moveInterval;


            if (netDash.Value != 0)
            {

                useSpeed *= 1.5f + (1.5f * netDash.Value);

                return useFrames ? useFrame : useSpeed;

            }

            switch (pathActive)
            {

                case pathing.circling:

                    useSpeed = gait *2f;

                    break;

                case pathing.monster:

                    useSpeed = gait * 2f;

                    useFrame -= 2;

                    break;

                case pathing.scene:

                    
                    if (distance > 640 || netMovement.Value == (int)movements.run)
                    {

                        useFrame -= 3;

                        useSpeed = gait * 3f;

                    }
                    else if (distance > 360)
                    {

                        useFrame -= 2;

                        useSpeed = gait * 2.25f;

                    }
                    else
                    {
                        useFrame -= 1;

                        useSpeed = gait * 1.5f;

                    }

                    break;

                case pathing.player:

                    if(modeActive == mode.track && Mod.instance.trackers.ContainsKey(characterType))
                    {

                        distance = Vector2.Distance(Position, Mod.instance.trackers[characterType].followPlayer.Position);

                    }
                    else
                    {

                        distance = Vector2.Distance(Position, Game1.player.Position);

                    }

                    if (distance > 512)
                    {
                        
                        useFrame -= 3;

                        useSpeed = gait * 4f;

                        if(netMovement.Value != (int)movements.run)
                        {

                            netMovement.Set((int)movements.run);

                        }

                        break;

                    }

                    if (netMovement.Value != (int)movements.walk)
                    {

                        netMovement.Set((int)movements.walk);

                    }

                    if (distance > 256)
                    {

                        if (netDash.Value != 0)
                        {

                            useSpeed = gait * 1.5f;

                        }
                        else
                        {

                            useFrame -= 2;
                            useSpeed = gait * 3f;

                        }

                    }
                    else
                    {

                        useSpeed = gait;

                    }

                    break;

                case pathing.random:

                    break;

                case pathing.roam:

                    if (distance > 360)
                    {

                        useFrame -= 2;

                        useSpeed *= 1.5f;

                    }

                    break;

                case pathing.none:
                    
                    break;

            }

            return useFrames ? useFrame : useSpeed;

        }

        // ======================================== MOVEMENT

        public bool PathTarget(Vector2 target, int ability, int proximity, int direction = -1, int limit = -1)
        {

            Vector2 center = ModUtility.PositionToTile(target);

            if(center.Equals(occupied) && proximity == 0)
            {

                return true;

            }

            if (direction == -1)
            {

                // direction from target (center) to origin (occupied), will search for tiles in between
                direction = ModUtility.DirectionToTarget(target, Position)[2]; // uses 64

            }

            Dictionary<Vector2, int> paths = ModUtility.TraversalToTarget(currentLocation, occupied, center, ability, proximity, direction); // uses tiles

            if (paths.Count > 0)
            {

                destination = paths.Keys.Last();

                traversal = paths;

                LookAtTarget(destination * 64, false); // uses 64

                return true;

            }

            return false;

        }

        public virtual void SettleOccupied()
        {

            occupied = ModUtility.PositionToTile(Position);//new Vector2((int)(Position.X / 64), (int)(Position.Y / 64));

        }

        public virtual void Traverse()
        {
            
            if (destination == Vector2.Zero || netIdle.Value != 0 || netSceneActive.Value)
            {

                if (stationaryTimer > 0)
                {

                    stationaryTimer--;

                }

                if (stationaryTimer <= 0)
                {

                    StopMoving();

                }
                
                return;

            }

            if (ArrivedDestination())
            {

                return;

            }

            KeyValuePair<Vector2,int> target = traversal.First();

            if (target.Value == 2)
            {

                Mod.instance.iconData.AnimateQuickWarp(currentLocation, Position, true, warpDisplay);

                Position = target.Key * 64;

                //Mod.instance.iconData.AnimateQuickWarp(currentLocation, Position, false, warpDisplay);

                occupied = target.Key;

                traversal.Remove(target.Key);

            }
            else
            {

                if(target.Value == 1 && netDash.Value == 0)
                {

                    SetDash(target.Key * 64);

                }

                if(netDash.Value != 0)
                {

                    if(pathTotal <= 0)
                    {

                        SetDash(target.Key * 64, netDash.Value == (int)dashes.smash);

                    }

                    Position += pathIncrement;

                    pathProgress--;

                    if(pathProgress <= 0)
                    {

                        occupied = target.Key;

                        traversal.Remove(target.Key);

                    }

                }
                else
                {

                    float speed = MoveSpeed(Vector2.Distance(Position, target.Key * 64));

                    Position = ModUtility.PathMovement(Position, target.Key * 64, speed);

                    float remain = Vector2.Distance(Position, target.Key * 64);

                    if (remain <= 4f)
                    {

                        occupied = target.Key;

                        traversal.Remove(target.Key);

                    }


                    if (netSpecial.Value != (int)specials.sweep && remain >= 32f)
                    {

                        LookAtTarget(target.Key * 64, false);

                    };


                }

            }

            ArrivedDestination();

        }

        public virtual bool ArrivedDestination()
        {

            if (occupied.Equals(destination) || traversal.Count == 0)
            {

                ClearMove();

                return true;

            }

            return false;

        }

        public virtual bool TightPosition()
        {

            if (destination != Vector2.Zero)
            {

                return false;

            }

            //if (Position / 64 == new Vector2((int)(Position.X/64), (int)(Position.Y / 64)))
            //{

                return true;

            //}

            //return false;

        }

        public virtual bool CollideCharacters(Vector2 tile)
        {
            
            //------------- Collision check

            collideTimer = 180;

            foreach(Farmer farmer in currentLocation.farmers)
            {

                if(ModUtility.PositionToTile(farmer.Position) == tile)
                {

                    return true;

                }

            }

            foreach (NPC NPChar in currentLocation.characters)
            {

                if (NPChar is StardewDruid.Character.Actor || NPChar is Cast.Ether.Dragon || NPChar == this || NPChar is StardewValley.Monsters.Monster)
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

        // ======================================== ADJUST MODE

        public virtual void SwitchToMode(mode modechoice, Farmer player)
        {

            ResetActives();

            RemoveCompanionBuff(player);

            netSceneActive.Set(false);

            Mod.instance.trackers.Remove(characterType);

            modeActive = mode.random;

            modechoice = SpecialMode(modechoice);

            switch (modechoice)
            {

                case mode.home:

                    CharacterHandle.CharacterWarp(this, CharacterHandle.CharacterHome(characterType), true);

                    tether = CharacterHandle.RoamTether(currentLocation);

                    break;

                case mode.random:

                    tether = CharacterHandle.RoamTether(currentLocation);

                    break;

                case mode.track:

                    modeActive = mode.track;

                    Mod.instance.trackers[characterType] = new TrackHandle(characterType, player, trackQuadrant);

                    specialDisable = false;

                    CompanionBuff(player);

                    break;

                case mode.scene:

                    modeActive = mode.scene;

                    netSceneActive.Set(true);

                    if(Mod.instance.activeEvent.Count > 0)
                    {

                        eventName = Mod.instance.activeEvent.First().Key;

                    }

                    break;

                case mode.roam:

                    modeActive = mode.roam;

                    CharacterHandle.CharacterWarp(this, CharacterHandle.locations.farm, true);

                    roamVectors.Clear();

                    roamIndex = 0;

                    roamLapse = Game1.currentGameTime.TotalGameTime.TotalMinutes + 1.0;

                    roamVectors = RoamAnalysis();

                    tether = CharacterHandle.RoamTether(currentLocation);

                    break;

                case mode.limbo:

                    modeActive = mode.limbo;

                    CharacterMover mover = new(characterType, CharacterMover.moveType.limbo);

                    Mod.instance.movers[characterType] = mover;

                    break;


            }

        }

        public virtual mode SpecialMode(mode modechoice)
        {

            return modechoice;

        }

        public virtual void CompanionBuff(Farmer player)
        {

        }

        public virtual void RemoveCompanionBuff(Farmer player)
        {


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

        public virtual void NewDay()
        {



        }

    }

}
