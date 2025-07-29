using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Ether;
using StardewDruid.Cast.Fates;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Weald;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Event;
using StardewDruid.Handle;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Location.Druid;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Buildings;
using StardewValley.Extensions;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.Quests;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Security.AccessControl;
using System.Threading;
using xTile.Dimensions;
using static StardewDruid.Data.RiteData;

namespace StardewDruid.Cast
{

    public class Rite
    {
        // -----------------------------------------------------

        public enum Rites
        {
            none,
            winds,
            weald,
            mists,
            stars,
            voide,
            fates,
            ether,
            witch,
            
        }

        public Rites castType;

        public Dictionary<Rites, string> requirement = new()
        {

            [Rites.winds] = QuestHandle.squireWinds,
            [Rites.weald] = QuestHandle.swordWeald,
            [Rites.mists] = QuestHandle.swordMists,
            [Rites.stars] = QuestHandle.swordStars,
            [Rites.fates] = QuestHandle.swordFates,
            [Rites.ether] = QuestHandle.swordEther,
            [Rites.witch] = QuestHandle.questBlackfeather,

        };

        public int castLevel;

        public int castCost;

        public Vector2 castVector;

        public int castTool;

        public bool castHeld;

        public string castLocation;

        public int castInterval;

        public int castTimer;

        public List<Vector2> vectorList = new();

        public Dictionary<string, List<string>> specialCasts = new();

        public Dictionary<string, Dictionary<Vector2, string>> targetCasts = new();

        public Dictionary<string, Dictionary<Vector2, int>> terrainCasts = new();

        public Rites appliedBuff;

        // ----------------------------------------------------

        public Rites displayRite;

        public int castFactor;

        public bool displayChannel;

        public bool displayCircle;

        public int circleFactor;

        // ----------------------------------------------------

        public bool channelActive = false;

        public Vector2 channelPosition = new Vector2();

        public int channelLevel = 0;

        public bool channelContinuum = false;

        // ----------------------------------------------------

        public enum riteCharges
        {
            none,
            weald,
            mists,
            stars,
            fates,
            witch,

        }

        public riteCharges chargeType;

        public riteCharges selectedCharge = riteCharges.none;

        public int chargeTimer;

        public int chargeCooldown;

        public int chargeChance;

        public bool chargeActive;

        public StardewValley.GameLocation chargeLocation;

        public bool remindDefault;

        public bool remindAttunement;

        public bool remindBegin;

        public bool remindTool;

        public enum ritetools
        {
            none,
            melee,
            stone,
            cherry,
            bomb,
            megabomb,

        }

        // ----------------------------------------------------

        public const string eventCultivate = "eventCultivate";

        public const string eventWilderness = "eventWilderness";

        public const string eventShield = "eventShield";

        public const string eventTransform = "eventTransform";

        public const string eventCorvids = "eventCorvids";

        public const string eventWisps = "eventWisps";

        public const string eventWinds = "eventWinds";

        public const string eventWrath = "eventWrath";

        public const string eventDeathwinds = "eventDeathwinds";

        public const string eventCurse = "eventCurse";

        public const string eventSnare = "eventSnare";

        public const string eventFishspot = "eventFishspot";

        public const string eventRapidfire = "eventRapidfire";

        public const string buffIdRite = "184650001";

        public const string buffIdSpeed = "184650003";

        public const string buffIdDragon = "184650005";

        public const string buffIdJester = "184650006";

        public const string buffIdShield = "184650007";

        public const string buffIdCrit = "184650010";

        public const string buffIdCharge = "184650011";



        // ----------------------------------------------------

        public Rite()
        {

        }

        public void Shutdown()
        {

            castType = Rites.none;

            castLevel = 0;

            ChannelShutdown();

        }

        public void Draw(SpriteBatch b)
        {

            int displayFactor = 120;

            if (displayChannel)
            {

                displayFactor += 120;

            }

            Rites blessing = castType;

            switch (castType)
            {

                case Rites.none:
                case Rites.ether:

                    if (castFactor > 0)
                    {

                        castFactor -= 2;//Math.Max(2, (int)Math.Sqrt(displayCast));

                        blessing = displayRite;

                    }
                    else
                    {

                        castFactor = 0;

                    }

                    break;

                default:

                    displayRite = castType;

                    if (castFactor < displayFactor)
                    {

                        castFactor++;//= Math.Max(1,(int)Math.Sqrt(displayFactor-displayCast));

                    }
                    else if (castFactor > displayFactor)
                    {

                        castFactor--;

                        castFactor--;

                    }

                    break;

            }

            if (castFactor <= 0)
            {

                return;

            }

            int time = (int)Game1.currentGameTime.TotalGameTime.TotalMilliseconds;

            int offset = (int)((time % 3000) / 25);

            Microsoft.Xna.Framework.Vector2 drawPosition = new(Game1.player.Position.X - (float)Game1.viewport.X + 32, Game1.player.Position.Y - (float)Game1.viewport.Y);

            // cast array
            float castScale = (float)castFactor / 120f;

            float fade = Math.Min(0.8f, castFactor * 0.005f);

            switch (castType)
            {

                default:

                    Dictionary<int, IconData.cursors> runeLayout = new()
                    {
                        [0] = IconData.cursors.winds1,
                        [1] = IconData.cursors.winds2,
                        [2] = IconData.cursors.winds3,
                        [3] = IconData.cursors.winds1,
                        [4] = IconData.cursors.winds2,
                        [5] = IconData.cursors.winds3,
                    };

                    for (int i = 0; i < 6; i++)
                    {

                        int runeoffset = (offset + (i * 20)) % 120;

                        int runeframe = runeoffset == 0 ? 0 : runeoffset / 30;

                        Vector2 runePosition = drawPosition + (ModUtility.TopDownRatioFactor(runeoffset) * (1f + (castScale/2)));

                        b.Draw(
                            Mod.instance.iconData.cursorTexture,
                            runePosition,
                            IconData.CursorRectangle(runeLayout[i], runeframe),
                            Color.White * fade,
                            0,
                            new Vector2(12),
                            2f + castScale,
                            SpriteEffects.None,
                            0.0003f
                        );

                    }

                    break;

            }

            if(displayCircle)
            {

                circleFactor++;

                float channelFade = Math.Min(0.99f, circleFactor * 0.01f);

                Microsoft.Xna.Framework.Rectangle circleSprite = IconData.RiteRectangle(IconData.ritecircles.winds);

                b.Draw(
                     Mod.instance.iconData.ritecircleTexture,
                     drawPosition + new Vector2(0, 32),
                     circleSprite,
                     Color.White * channelFade,
                     0f,
                     new Vector2(40, 32),
                     4f,
                     0,
                     0.0002f
                 );

                b.Draw(
                     Mod.instance.iconData.ritecircleTexture,
                     drawPosition + new Vector2(0, 32),
                     new Microsoft.Xna.Framework.Rectangle(circleSprite.X+80, circleSprite.Y, circleSprite.Width, circleSprite.Height),
                     Color.White * (channelFade/3),
                     0f,
                     new Vector2(40, 32),
                     4f,
                     0,
                     0.0001f
                 );

            } else
            {

                circleFactor = 0;

            }


        }

        public void Click()
        {

            Mod.instance.RiteButtonSuppress();

            Start();

        }

        public void Reset()
        {

            castInterval = 60;

            int castFast = 3;

            switch (castType)
            {

                case Rites.mists:

                    castInterval = 50;

                    castFast = 3;

                    break;

                case Rites.stars:

                    castInterval = 40;

                    castFast = 2;

                    break;

                case Rites.fates:
                case Rites.witch:

                    castInterval = 120;

                    castFast = 6;

                    break;

                case Rites.winds:

                    castInterval = 60;

                    castFast = 3;
                    break;
            }

            if (Mod.instance.herbalHandle.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.celerity))
            {

                castInterval -= (int)(castFast * Mod.instance.herbalHandle.buff.applied[HerbalBuff.herbalbuffs.celerity].level);

            }

            vectorList = new();

            castLevel = 0;

            castLocation = Game1.player.currentLocation.Name;

        }

        public bool Start()
        {

            int tool = Mod.instance.AttuneableWeapon();

            Rites blessing = SelectBlessing(tool, true);

            if(blessing == Rites.none)
            {

                return false;

            }

            castType = blessing;

            castLevel = 0;

            castTool = tool;

            castLocation = Game1.player.currentLocation.Name;

            CastVector();

            Reset();

            return true;

        }

        public bool BeginnerLock()
        {

            return RequirementCheck(Rites.winds) == Rites.none;

        }

        public Rites SelectBlessing(int tool,bool notify = false)
        {

            return Rites.winds;
            Rites blessing;

            if (tool == 997)
            {

                return Rites.winds;

            }

            if (tool == Transform.toolPlaceholder)
            {

                if (Mod.instance.eventRegister.ContainsKey(eventTransform))
                {

                    return Rites.ether;

                }

            }

            if (Mod.instance.magic)
            {

                blessing = GetSlotBlessing();

                if (blessing == Rites.none)
                {

                    if (!remindAttunement && notify)
                    {

                        Mod.instance.RegisterDisplay(RiteData.Strings(RiteData.riteStrings.noRiteAttuned) + " " + (Game1.player.CurrentToolIndex + 1));

                        remindAttunement = true;

                    }

                }

                return blessing;

            }

            if (BeginnerLock())
            {

                if (!remindBegin && notify)
                {

                    Mod.instance.RegisterDisplay(Mod.instance.Config.journalButtons.ToString() + " " + RiteData.Strings(RiteData.riteStrings.openJournal));

                    remindBegin = true;

                }

                return Rites.none;

            }

            if (Mod.instance.Config.slotAttune)
            {

                blessing = GetSlotBlessing();

                if (blessing == Rites.none)
                {

                    ritetools riteTool = HoldingTool(tool);

                    if(riteTool != ritetools.none)
                    {

                        return Rites.winds;

                    }

                    if (notify)
                    {

                        if (!remindAttunement)
                        {

                            Mod.instance.RegisterDisplay(RiteData.Strings(RiteData.riteStrings.noRiteAttuned) + " " + (Game1.player.CurrentToolIndex + 1));

                            remindAttunement = true;

                        }

                    }

                }

                return blessing;

            }

            if (tool == -1)
            {

                if (!remindTool && notify)
                {

                    Mod.instance.RegisterDisplay(RiteData.Strings(RiteData.riteStrings.riteTool));

                    remindTool = true;

                }

                return Rites.none;

            }

            Dictionary<int, Rites> attunement = SpawnData.WeaponAttunement();

            if (attunement.ContainsKey(tool))
            {

                blessing = RequirementCheck(attunement[tool]);

                if (blessing == Rites.none)
                {

                    if (notify)
                    {

                        if (!remindAttunement)
                        {


                            Mod.instance.RegisterDisplay(RiteData.Strings(RiteData.riteStrings.noToolAttunement));

                            remindAttunement = true;

                        }

                    }

                }

            }
            else
            {

                ritetools riteTool = HoldingTool(tool);

                if (riteTool != ritetools.none)
                {

                    return Rites.winds;

                }

                if (!remindDefault && notify)
                {

                    Mod.instance.RegisterDisplay(RiteData.Strings(RiteData.riteStrings.defaultToolAttunement));

                    remindDefault = true;

                }

                blessing = RequirementCheck(Mod.instance.save.rite, true);

            }

            return blessing;

        }

        public void Update()
        {

            castTimer--;

            chargeCooldown--;

            if (castType == Rites.none)
            {

                return;

            }

            castHeld = Mod.instance.RiteButtonHeld();

            if (channelActive)
            {

                Channel();

            }

            if (castTimer > 0)
            {

                return;

            }

            if (!castHeld)
            {

                if(castLevel != 0)
                {

                    Shutdown();

                    return;

                }

            }

            int tool = Mod.instance.AttuneableWeapon();

            if (tool != castTool)
            {

                Shutdown();

                return;

            }

            Rites blessing = SelectBlessing(tool);

            if (blessing != castType)
            {

                Shutdown();

                return;

            }

            if (Game1.player.Stamina <= 16)
            {

                if (castLevel > 0)
                {

                    Mod.instance.RegisterMessage(RiteData.Strings(RiteData.riteStrings.energyContinue), 3);

                }
                else
                {

                    Mod.instance.RegisterMessage(RiteData.Strings(RiteData.riteStrings.energyRite), 3);

                }

                Shutdown();

                return;

            }

            CastVector();
            
            castTimer = castInterval;

            Cast();

            castLevel++;

        }

        public void ChannelStart()
        {

            if (channelActive)
            {

                return;

            }

            if (!Mod.instance.ShiftButtonHeld())
            {

                if(channelContinuum && castHeld)
                {

                    // continue

                }
                else
                if (castLevel != 0)
                {

                    return;

                }

            }

            channelActive = true;

            channelPosition = Game1.player.Position;

            channelContinuum = false;

        }

        public void ChannelShutdown()
        {

            channelActive = false;

            channelLevel = 0;

            displayChannel = false;

            displayCircle = false;

        }

        public SpellHandle.Effects ChargeEffect(riteCharges charge, bool ignoreCooldown = false)
        {

            if (!chargeActive)
            {

                return SpellHandle.Effects.none;

            }

            if (chargeCooldown > 0)
            {

                return SpellHandle.Effects.none;

            }

            float celeri = 1f;

            if (Mod.instance.herbalHandle.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.celerity))
            {

                celeri -= 0.1f * (float)Mod.instance.herbalHandle.buff.applied[HerbalBuff.herbalbuffs.celerity].level;

            }

            switch (charge)
            {
                case riteCharges.fates:

                    chargeCooldown = (int)(180f * celeri);

                    switch (Mod.instance.randomIndex.Next(5))
                    {

                        default:

                            return SpellHandle.Effects.daze;

                        case 1:

                            return SpellHandle.Effects.morph;
                        
                        case 2:

                            return SpellHandle.Effects.doom;

                        case 3:

                            return SpellHandle.Effects.mug;

                        case 4:

                            return SpellHandle.Effects.glare;

                    }

                case riteCharges.stars:

                    chargeCooldown = (int)(60f * celeri);

                    return SpellHandle.Effects.knock;

                case riteCharges.mists:

                    chargeCooldown = (int)(60f * celeri);

                    return SpellHandle.Effects.wisp;

                case riteCharges.witch:

                    chargeCooldown = (int)(180f * celeri);

                    return SpellHandle.Effects.omen;

                default:
                case riteCharges.weald:

                    chargeCooldown = (int)(30f * celeri);

                    return SpellHandle.Effects.drain;

            }

        }

        public void ChargeSet(riteCharges type)
        {

            chargeActive = true;

            chargeType = type;

            chargeTimer = 3000;

            chargeLocation = Game1.player.currentLocation;

        }

        public Rites RequirementCheck(Rites id, bool next = false)
        {

            if (Mod.instance.magic)
            {
                
                return id;

            }

            if(id == Rites.none)
            {

                return id;

            }

            if (Mod.instance.questHandle.IsComplete(requirement[id]))
            {

                return id;

            }

            if (next)
            {

                while((int)id > 1)
                {

                    id = (Rites)((int)id - 1);

                    if (Mod.instance.questHandle.IsComplete(requirement[id]))
                    {

                        return id;

                    }

                }

            }

            return Rites.none;

        }

        public Rites GetSlotBlessing()
        {

            int num = Game1.player.CurrentToolIndex;

            if (Game1.player.CurrentToolIndex == Transform.toolPlaceholder && Mod.instance.eventRegister.ContainsKey(eventTransform))
            {

                num = (Mod.instance.eventRegister[eventTransform] as Transform).toolIndex;

            }

            int real = num % 12;

            Rites blessing = Rites.none;

            Dictionary<int, string> slots= SlotNormal();

            switch (slots[real])
            {

                case "weald":

                    blessing = RequirementCheck(Rites.weald, true);

                    break;

                case "mists":

                    blessing = RequirementCheck(Rites.mists, true);

                    break;

                case "stars":

                    blessing = RequirementCheck(Rites.stars, true);

                    break;

                case "fates":

                    blessing = RequirementCheck(Rites.fates, true);

                    break;

                case "ether":

                    blessing = RequirementCheck(Rites.ether, true);

                    break;

                case "witch":

                    blessing = RequirementCheck(Rites.witch, true);

                    break;

            }

            return blessing;

        }

        public static Dictionary<int,string> SlotNormal()
        {

            return new()
            {
                [0] = Mod.instance.Config.slotOne,
                [1] = Mod.instance.Config.slotTwo,
                [2] = Mod.instance.Config.slotThree,
                [3] = Mod.instance.Config.slotFour,
                [4] = Mod.instance.Config.slotFive,
                [5] = Mod.instance.Config.slotSix,
                [6] = Mod.instance.Config.slotSeven,
                [7] = Mod.instance.Config.slotEight,
                [8] = Mod.instance.Config.slotNine,
                [9] = Mod.instance.Config.slotTen,
                [10] = Mod.instance.Config.slotEleven,
                [11] = Mod.instance.Config.slotTwelve,

            };

        }

        public void CastVector()
        {

            switch (castType)
            {

                case Rites.mists:
                case Rites.fates:
                    
                    Vector2 cursorVector = GetTargetCursor(Game1.player.FacingDirection, 512);

                    castVector = ModUtility.PositionToTile(cursorVector);

                    break;  
                    
               case Rites.witch:
               case Rites.winds:

                    castVector = ModUtility.PositionToTile(GetTargetCursor(Game1.player.FacingDirection, 1280, -1));

                    break;

                default: // weald / stars / ether
                    
                    castVector = Game1.player.Tile;

                    break;

            }

        }

        public static List<StardewValley.Monsters.Monster> GetMonstersAround(int direction, int distance, int radius)
        {

            Vector2 checkVector = GetTargetDirectional(direction, distance);

            List<StardewValley.Monsters.Monster> checkMonsters = ModUtility.MonsterProximity(Game1.player.currentLocation, checkVector, radius);

            return checkMonsters;

        }

        public static Vector2 GetTargetCursor(int direction, int distance = 320, int threshhold = 64)
        {

            Point mousePoint = Game1.getMousePosition();

            if (mousePoint.Equals(new(0)))
            {
                
                return GetTargetDirectional(direction, distance);

            }

            Vector2 playerPosition = Game1.player.Position;

            Vector2 viewPortPosition = Game1.viewportPositionLerp;

            Vector2 mousePosition = new(mousePoint.X + viewPortPosition.X, mousePoint.Y + viewPortPosition.Y);

            float vectorDistance = Vector2.Distance(playerPosition, mousePosition);

            if (threshhold != -1 && vectorDistance <= threshhold + 32)
            {

                return GetTargetDirectional(direction, distance);

            }

            Vector2 diffVector = mousePosition - playerPosition;

            int vectorLimit = distance + 32;

            if (vectorDistance > vectorLimit)
            {

                diffVector *= vectorLimit;

                diffVector /= vectorDistance;

            }

            return playerPosition + diffVector;

        }

        public static Vector2 GetTargetDirectional(int direction, int distance = 320)
        {
            Vector2 vector = Game1.player.GetBoundingBox().Center.ToVector2();

            Dictionary<int, Vector2> vectorIndex = new()
            {

                [0] = vector - new Vector2(0, distance) - new Vector2(0,48),// up
                [1] = vector + new Vector2(distance, 0) - new Vector2(0, 16), // right
                [2] = vector + new Vector2(0, distance) - new Vector2(0,16),// down
                [3] = vector + new Vector2(-distance, 0) - new Vector2(0, 16), // left

            };

            return vectorIndex[direction];

        }

        public void Cast()
        {

            castCost = 0;

            switch (castType)
            {

                case Rites.stars:

                    CastStars();

                    break;

                case Rites.mists:

                    CastMists();

                    break;

                case Rites.fates:

                    CastFates();

                    break;

                case Rites.ether:

                    CastEther();

                    break;

                case Rites.witch:

                    CastWitch();

                    break;

                case Rites.winds:

                    CastSworn();

                    break;

                default:

                    CastWeald();

                    break;
            }

            ApplyCost(castCost);

            castCost = 0;

        }

        public void Channel()
        {

            bool moveAbort = false;

            if (Vector2.Distance(channelPosition, Game1.player.Position) > 32)
            {

                if (!Mod.instance.ShiftButtonHeld())
                {

                    moveAbort = true;

                }

            }

            channelLevel++;

            switch (castType)
            {

                case Rites.stars:

                    if (channelLevel == 30)
                    {

                        displayChannel = true;

                        if (!Mod.instance.eventRegister.ContainsKey(eventShield))
                        {

                            Cast.Effect.Shield shieldEffect = new();

                            shieldEffect.EventSetup(Game1.player, Game1.player.Position, eventShield);

                            shieldEffect.EventActivate();

                        }

                    }

                    ChannelStars();

                    break;

                case Rites.mists:

                    ChannelMists();

                    break;

                case Rites.fates:

                    ChannelFates();

                    break;

                case Rites.ether:

                    ChannelEther();

                    break;

                case Rites.witch:

                    ChannelWitch();
                    ReleaseWitch();
                    break;

                case Rites.winds:

                    if (moveAbort || !castHeld)
                    {

                        ReleaseSworn();

                        ChannelShutdown();

                    }

                    if (channelLevel == 30)
                    {

                        displayChannel = true;

                    }

                    if (channelLevel == 60)
                    {

                        Mod.instance.sounds.PlayCue(SoundHandle.SoundCue.RisingWind);

                        displayCircle = true;

                    }

                    if (channelLevel >= 150)
                    {

                        Mod.instance.spellRegister.Add(new(Game1.player, IconData.ritecircles.winds));

                        ReleaseSworn();

                        ChannelShutdown();

                        channelContinuum = true;

                    }

                    break;

                default:

                    ChannelWeald();

                    break;
            }

        }

        public static void ApplyCost(int appliedCost = -1)
        {

            if(appliedCost == -1)
            {

                return;

            }

            float oldStamina = Game1.player.Stamina;

            int totalCost = (int)(appliedCost * (0.17f * (float)Mod.instance.ModDifficulty()));

            float staminaCost = Math.Min(totalCost, oldStamina - 16);

            if (staminaCost > 0)
            {

                Game1.player.Stamina -= staminaCost;

            }

            Game1.player.checkForExhaustion(oldStamina);

        }

        // ------------------------------------------------------------------------------

        public static Rite.ritetools HoldingBomb()
        {

            if (Game1.player.CurrentItem == null)
            {

                return Rite.ritetools.none;

            }

            switch (Game1.player.CurrentItem.ItemId)
            {

                case "286":

                    return Rite.ritetools.cherry;

                case "287":

                    return Rite.ritetools.bomb;

                case "288":

                    return Rite.ritetools.megabomb;

                case "390":

                    return Rite.ritetools.stone;

            }

            return Rite.ritetools.none;

        }
        
        public static Rite.ritetools HoldingTool(int tool)
        {

            switch (tool)
            {

                case 999:
                case -1:

                    return ritetools.none;

                default:

                    if (Game1.player.CurrentTool is MeleeWeapon weapon)
                    {

                        return ritetools.melee;

                    }

                    return ritetools.none;

                case 997:

                    return HoldingBomb();

            }

        }

        public void CastSworn()
        {

            int holding = Mod.instance.AttuneableWeapon();

            ritetools tool = HoldingTool(holding);

            if(tool == ritetools.none)
            {

                return;

            }

            if (tool == ritetools.melee)
            {

                ChannelStart();

                return;

            }

            // ------------------------------------------------------------------------------

            int consumption = 1;

            int cost = 0;

            // ------------------------------------------------------------------------------

            SpellHandle special = new(Game1.player, castVector * 64, 192, Mod.instance.CombatDamage());

            special.instant = true;

            special.display = IconData.impacts.none;

            special.type = SpellHandle.Spells.missile;

            special.missile = MissileHandle.missiles.bomb;

            special.displayFactor = 2;

            special.displayRadius = 3;

            switch (tool)
            {

                case ritetools.stone: // stone

                    special.missile = MissileHandle.missiles.boulder;

                    special.sound = SpellHandle.Sounds.flameSpellHit;

                    special.display = IconData.impacts.flashbang;

                    special.damageMonsters /= 2;

                    special.effectRadius = 3;

                    special.added.Add(SpellHandle.Effects.swipe);

                    consumption = 5;

                    cost += 4;

                    break;

                case ritetools.cherry: // cherry bomb

                    special.scheme = IconData.schemes.bomb_one;

                    special.sound = SpellHandle.Sounds.flameSpellHit;

                    special.display = IconData.impacts.bomb;

                    special.damageRadius = 4 * 64;

                    special.effectRadius = 4;

                    special.added.Add(SpellHandle.Effects.explode);

                    break;

                case ritetools.bomb: // bomb

                    special.scheme = IconData.schemes.bomb_two;

                    special.sound = SpellHandle.Sounds.explosion;

                    special.display = IconData.impacts.bigimpact;

                    special.displayFactor = 3;

                    special.damageRadius = 6 * 64;

                    special.displayRadius = 4;

                    special.effectRadius = 6;

                    special.damageMonsters = Mod.instance.CombatDamage() * 2;

                    special.added.Add(SpellHandle.Effects.explode);

                    break;

                case ritetools.megabomb: // mega bomb

                    special.scheme = IconData.schemes.bomb_three;

                    special.sound = SpellHandle.Sounds.explosion;

                    special.display = IconData.impacts.bigimpact;

                    special.displayFactor = 4;

                    special.damageRadius = 8 * 64;

                    special.displayRadius = 5;

                    special.effectRadius = 8;

                    special.damageMonsters = Mod.instance.CombatDamage() * 3;

                    special.added.Add(SpellHandle.Effects.explode);

                    break;

            }

            if (Mod.instance.herbalHandle.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.feline) && Mod.instance.randomIndex.Next(3) == 0)
            {

                consumption = 0;

                cost = 0;

            }

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.bombs))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.bombs, 1);

            }

            Mod.instance.spellRegister.Add(special);

            if (consumption > 0)
            {
                
                Game1.player.Items.ReduceId(Game1.player.CurrentItem.ItemId, consumption);

            }

        }

        public void ReleaseSworn()
        {

            int distance = (channelLevel * 4);

            Vector2 range = GetTargetCursor(Game1.player.FacingDirection, 96 + distance, distance);

            SpellHandle special = new(Game1.player.currentLocation, range, Game1.player.Position + new Vector2(32, 0), 192, -1, Mod.instance.CombatDamage() / 2);

            special.instant = true;

            special.display = IconData.impacts.none;

            special.type = SpellHandle.Spells.wave;

            special.targetType = SpellHandle.TargetTypes.conic;

            special.effectRadius = 3;

            special.added.Add(SpellHandle.Effects.swipe);

            special.added.Add(SpellHandle.Effects.toolhack);

            special.added.Add(SpellHandle.Effects.toolbreak);

            special.soundTrigger = SoundHandle.SoundCue.WindSlash;

            SwornEffects(special, ritetools.melee);

            Mod.instance.spellRegister.Add(special);

            castTimer = castInterval;

            ApplyCost(4);

        }

        public void SwornEffects(SpellHandle special, ritetools tool)
        {

            SpellHandle.Effects charge = ChargeEffect(chargeType, true);

            if(charge != SpellHandle.Effects.none)
            {

                special.added.Add(charge);

            }

            foreach (KeyValuePair<HerbalBuff.herbalbuffs, HerbalApplied> buff in Mod.instance.herbalHandle.buff.applied)
            {

                switch (buff.Key)
                {
                    case HerbalBuff.herbalbuffs.imbuement:

                        special.added.Add(SpellHandle.Effects.drain);

                        break;

                    case HerbalBuff.herbalbuffs.amorous:

                        switch (tool)
                        {

                            case ritetools.cherry: // cherry bomb

                            case ritetools.bomb: // bomb

                            case ritetools.megabomb: // mega bomb

                                special.display = IconData.impacts.lovebomb;

                                special.added.Add(SpellHandle.Effects.charm);

                                break;

                        }

                        special.added.Add(SpellHandle.Effects.glare);

                        break;

                    case HerbalBuff.herbalbuffs.macerari:

                        special.added.Add(SpellHandle.Effects.douse);

                        break;

                    case HerbalBuff.herbalbuffs.concussion:

                        switch (tool)
                        {

                            case ritetools.cherry: // cherry bomb

                            case ritetools.bomb: // bomb

                            case ritetools.megabomb: // mega bomb

                                special.added.Add(SpellHandle.Effects.sunder);

                                if (Mod.instance.randomIndex.Next(6) == 0)
                                {

                                    special.added.Add(SpellHandle.Effects.bore);

                                }
                                break;
                        }

                        special.added.Add(SpellHandle.Effects.embers);

                        break;

                    case HerbalBuff.herbalbuffs.jumper:

                        special.added.Add(SpellHandle.Effects.jump);

                        break;

                    case HerbalBuff.herbalbuffs.feline:

                        special.missile = MissileHandle.missiles.cat;

                        break;

                    case HerbalBuff.herbalbuffs.sanctified:

                        //special.display = IconData.impacts.holy;

                        switch (tool)
                        {

                            case ritetools.stone:

                            case ritetools.cherry: // cherry bomb

                                special.display = IconData.impacts.holy;

                                if (special.missile != MissileHandle.missiles.cat)
                                {

                                    special.missile = MissileHandle.missiles.holygrenade;

                                }

                                break;

                            case ritetools.bomb: // bomb

                            case ritetools.megabomb: // mega bomb

                                special.display = IconData.impacts.holyimpact;

                                if (special.missile != MissileHandle.missiles.cat)
                                {

                                    special.missile = MissileHandle.missiles.holygrenade;

                                }

                                break;
                        }

                        special.added.Add(SpellHandle.Effects.holy);

                        break;

                    case HerbalBuff.herbalbuffs.capture:

                        if (Mod.instance.magic)
                        {

                            break;
                        }

                        special.monsters = ModUtility.MonsterProximity(Game1.player.currentLocation, special.impact, special.damageRadius + 32, true);

                        special.added.Add(SpellHandle.Effects.capture);

                        break;
                    case HerbalBuff.herbalbuffs.rapidfire:

                        if (Mod.instance.eventRegister.ContainsKey(eventRapidfire))
                        {

                            if (Mod.instance.eventRegister[eventRapidfire] is Rapidfire rapidFire)
                            {

                                if (!rapidFire.Discharge(special.displayFactor))
                                {

                                    return;

                                }

                            }

                        }
                        else
                        {

                            Rapidfire rapidFire = new Rapidfire();

                            rapidFire.EventSetup(Game1.player, Game1.player.Position, eventRapidfire);

                            rapidFire.EventActivate();

                            rapidFire.Discharge(special.displayFactor);

                        }

                        castTimer = 12;

                        break;

                }

            }

        }

        // ------------------------------------------------------------------------------

        public void CastWeald()
        {

            //---------------------------------------------
            // Weald Sound
            //---------------------------------------------

            if (castLevel % 3 == 0)
            {

                //Game1.player.currentLocation.playSound("discoverMineral");

                Mod.instance.sounds.PlayCue(SoundHandle.SoundCue.CastWeald);

            }

            castVector = ModUtility.PositionToTile(Game1.player.Position);

            CastFreneticism();

            //---------------------------------------------
            // Rockfall
            //---------------------------------------------

            ChannelStart();

            if (
                Game1.player.currentLocation is MineShaft ||
                Game1.player.currentLocation is VolcanoDungeon ||
                Game1.player.currentLocation is Lair ||
                Game1.player.currentLocation is Tomb ||
                (Game1.player.currentLocation is Spring && Mod.instance.activeEvent.Count > 0)
            )
            {

                if (Mod.instance.questHandle.IsGiven(QuestHandle.wealdFive))
                {

                    CastRockfall();

                    CastRockfall();

                }

                return;

            }

            //---------------------------------------------
            // Friendship
            //---------------------------------------------

            if (Mod.instance.questHandle.IsGiven(QuestHandle.wealdTwo))
            {
                
                if (castLevel % 3 == 0)
                {

                    CastGlimmer();

                }

                if(castLevel % 5 == 0)
                {

                    CastRoots();

                }

                CastBounty();

            }

        }

        public void ChannelWeald()
        {

            if (LocationHandle.MaxRestoration(Game1.player.currentLocation.Name) != -1 && Context.IsMainPlayer)
            {

                CastRestoration();

            }
            else
            if (Game1.player.currentLocation.IsOutdoors && Game1.player.currentLocation is not DruidLocation)
            {

                bool cultivate = false;

                if (Game1.player.currentLocation.IsFarm)
                {

                    cultivate = true;

                }

                if (Game1.player.currentLocation is IslandWest islandWest)
                {

                    List<Vector2> IslandCheck = ModUtility.GetTilesWithinRadius(islandWest, ModUtility.PositionToTile(Game1.player.Position), 1);

                    IslandCheck.Add(ModUtility.PositionToTile(Game1.player.Position));

                    foreach (Vector2 IslandVector in IslandCheck)
                    {

                        if (Game1.player.currentLocation.terrainFeatures.ContainsKey(IslandVector))
                        {

                            if (Game1.player.currentLocation.terrainFeatures[IslandVector] is HoeDirt IslandHoeDirt)
                            {

                                if (IslandHoeDirt.crop != null)
                                {

                                    cultivate = true;

                                    break;

                                }

                            }

                        }

                    }

                }

                if (cultivate)
                {

                    CastCultivate();

                }
                else
                {

                    CastWilderness();

                }

            }
            else if (
                Game1.player.currentLocation is MineShaft ||
                Game1.player.currentLocation is VolcanoDungeon)
            {

                CastWilderness();

            }
            else if (Game1.player.currentLocation.isGreenhouse.Value || Game1.player.currentLocation is Shed || Game1.player.currentLocation is AnimalHouse)
            {

                CastCultivate();

            }

            Mod.instance.rite.ChargeSet(riteCharges.weald);

        }

        public void CastFreneticism()
        {

            BuffEffects buffEffect = new();

            buffEffect.Speed.Set(1);

            Buff speedBuff = new(
                buffIdSpeed,
                source: RiteData.RiteNames(Rites.weald),
                displaySource: RiteData.RiteNames(Rites.weald),
                duration: 6000,
                displayName: RiteData.Strings(RiteData.riteStrings.druidFreneticism),
                description: RiteData.Strings(RiteData.riteStrings.speedIncrease),
                effects: buffEffect);

            Game1.player.buffs.Apply(speedBuff);

        }

        public void CastClearance()
        {

            List<Vector2> popVectors = new();

            Vector2 castPosition = GetTargetCursor(Game1.player.FacingDirection, 256, -1);

            List<Vector2> weedVectors = ModUtility.GetTilesWithinCone(Game1.player.currentLocation, castPosition, Game1.player.Position, 5);

            foreach (Vector2 tileVector in weedVectors)
            {

                if (Game1.player.currentLocation.objects.ContainsKey(tileVector))
                {

                    StardewValley.Object tileObject = Game1.player.currentLocation.objects[tileVector];

                    if (tileObject.IsBreakableStone())
                    {

                        if (SpawnData.StoneIndex().Contains(tileObject.ParentSheetIndex))
                        {

                            popVectors.Add(tileVector);

                            castCost += 1;

                        }
                        else
                        if (tileObject.QualifiedItemId.Contains("CoalNode"))
                        {

                            popVectors.Add(tileVector);

                            castCost += 1;

                        }

                    }
                    else if (tileObject.IsTwig() ||
                        tileObject.IsWeeds() ||
                        tileObject.QualifiedItemId == "(O)169" ||
                        tileObject.QualifiedItemId == "(O)590" ||
                        tileObject.QualifiedItemId == "(O)SeedSpot")
                    {

                        popVectors.Add(tileVector);

                        castCost += 1;

                    }
                    else if (Game1.player.currentLocation is MineShaft && tileObject is BreakableContainer)
                    {

                        popVectors.Add(tileVector);

                    }
                    else if (tileObject.GetContextTags().Contains("category_litter"))
                    {

                        popVectors.Add(tileVector);

                        castCost += 1;

                    }

                }

            }

            float damageLevel = Mod.instance.CombatDamage() * 0.5f;

            int number = 0;

            foreach (Vector2 popVector in popVectors)
            {

                SpellHandle explode = new(Game1.player, popVector * 64, 128, damageLevel * 0.2f);

                if (number == 0)
                {

                    explode.sound = SpellHandle.Sounds.flameSpellHit;

                    number++;

                }

                explode.display = IconData.impacts.smoke;

                explode.displayRadius = 2;

                explode.added.Add(SpellHandle.Effects.explode);

                Mod.instance.spellRegister.Add(explode);

            }
        }

        public static void CastGlimmer()
        {

            Glimmer glimmer = new();

            glimmer.CastActivate();

        }

        public void CastRoots()
        {

            Vector2 cursorVector = GetTargetCursor(Game1.player.FacingDirection, 512);

            List<StardewValley.Monsters.Monster> victims = ModUtility.MonsterProximity(Game1.player.currentLocation, cursorVector, 384, true);

            if (victims.Count > 0)
            {

                foreach (StardewValley.Monsters.Monster monster in victims)
                {

                    if (ModUtility.GroundCheck(monster.currentLocation, ModUtility.PositionToTile(monster.Position)) == "ground")
                    {

                        SpellHandle grasp = new(Game1.player, monster.Position + new Vector2(32), 256, 0f)
                        {
                            type = SpellHandle.Spells.grasp,

                            sound = SpellHandle.Sounds.treethud,

                            display = IconData.impacts.flasher,

                            instant = true,

                            added = new() { SpellHandle.Effects.snare, SpellHandle.Effects.glare, }
                        };

                        if (Mod.instance.herbalHandle.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.spellcatch))
                        {

                            if (Mod.instance.herbalHandle.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.capture))
                            {

                                grasp.added.Add(SpellHandle.Effects.capture);

                            }

                        }

                        Mod.instance.spellRegister.Add(grasp);

                        castCost += 6;

                        break;

                    }

                }

            }

        }

        public void CastBounty()
        {

            // ---------------------------------------------
            // Random Effect Center Selection
            // ---------------------------------------------

            if (!terrainCasts.ContainsKey(Game1.player.currentLocation.Name))
            {

                terrainCasts[Game1.player.currentLocation.Name] = new();

            }

            Vector2 playerTile = ModUtility.PositionToTile(Game1.player.Position);

            Vector2 glareTile = playerTile - new Vector2(8);

            for (int i = 0; i < 3; i++)
            {
                Mod.instance.iconData.ImpactIndicator(
                    Game1.player.currentLocation,
                    (glareTile + new Vector2(Mod.instance.randomIndex.Next(16), Mod.instance.randomIndex.Next(16))) * 64,
                    IconData.impacts.glare,
                    4f,
                    new() { alpha = 0.65f, rotation = Mod.instance.randomIndex.Next(4) * 0.5f, }
                );

            }

            Vector2 sqtVector = new((int)(playerTile.X - (playerTile.X % 16)), (int)(playerTile.Y - (playerTile.Y % 16)));

            if (terrainCasts[Game1.player.currentLocation.Name].ContainsKey(sqtVector))
            {

                return;

            }

            terrainCasts[Game1.player.currentLocation.Name][sqtVector] = 0;

            Bounty bounty = new(); bounty.CastActivate(Game1.player.currentLocation, sqtVector);

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.wealdTwo))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.wealdTwo, 1);

            }

        }

        public void CastWilderness()
        {

            if (castLevel != 0)
            {

                return;

            }
            
            if (!Mod.instance.questHandle.IsGiven(QuestHandle.wealdFour))
            {

                return;

            }

            if (Game1.player.stamina <= 48)
            {

                return;

            }

            if (Mod.instance.eventRegister.ContainsKey(eventWilderness))
            {

                return;

            }

            Wilderness wildernessEvent = new();

            wildernessEvent.EventSetup(Game1.player, Game1.player.Position, eventWilderness);

            wildernessEvent.EventActivate();

        }

        public void CastCultivate()
        {

            if (castLevel != 0)
            {

                return;

            }

            if (!Mod.instance.questHandle.IsGiven(QuestHandle.wealdThree))
            {

                return;

            }

            if (Mod.instance.eventRegister.ContainsKey(eventCultivate))
            {

                return;

            }

            Cultivate cultivateEvent = new();

            cultivateEvent.EventSetup(Game1.player, Game1.player.Position, eventCultivate);

            cultivateEvent.EventActivate();

        }

        public void CastRestoration()
        {

            if (castLevel != 0)
            {

                return;

            }

            Restoration restoreEvent = new();

            restoreEvent.EventSetup(Game1.player, Game1.player.Position, "restoration");

            restoreEvent.EventActivate();

            return;

        }

        public void CastRockfall(bool scene = false)
        {

            Vector2 rockVector = Vector2.Zero;

            IconData.impacts display = IconData.impacts.impact;

            SpellHandle.Sounds sound = SpellHandle.Sounds.flameSpellHit;

            for (int i = 0; i < 3; i++)
            {

                List<Vector2> castSelection = ModUtility.GetTilesWithinRadius(Game1.player.currentLocation, ModUtility.PositionToTile(Game1.player.Position), Mod.instance.randomIndex.Next(1, 6), true, Mod.instance.randomIndex.Next(8));

                if (castSelection.Count > 0)
                {

                    Vector2 tryVector = castSelection[Mod.instance.randomIndex.Next(castSelection.Count)];

                    if (!scene)
                    {

                        string ground = ModUtility.GroundCheck(Game1.player.currentLocation, tryVector);

                        if (ground == "ground")
                        {

                            rockVector = tryVector;

                            break;

                        }

                        if (ground == "water")
                        {

                            rockVector = tryVector;

                            display = IconData.impacts.splash;

                            sound = SpellHandle.Sounds.dropItemInWater;

                            break;

                        }
                    }
                    else
                    {

                        rockVector = tryVector;

                        break;

                    }
                    
                }

            }

            if(rockVector == Vector2.Zero) { return; }

            float damage = -1f;

            if (Mod.instance.questHandle.IsComplete(QuestHandle.wealdFive))
            {
                
                damage = Mod.instance.CombatDamage() * 0.5f;

            }

            SpellHandle rockSpell = new(Game1.player, rockVector * 64, 192, damage)
            {
                displayRadius = 3,

                display = display,

                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.rockfall,

                sound = sound
            };

            rockSpell.added.Add(SpellHandle.Effects.reave);

            if (!scene)
            {

                rockSpell.added.Add( SpellHandle.Effects.stone );

                castCost += 3;

            }

            Mod.instance.spellRegister.Add(rockSpell);

        }

        // ------------------------------------------------------------------------------

        public void CastMists()
        {

            //---------------------------------------------
            // Mists
            //---------------------------------------------
            
            if (castLevel % 3 == 0)
            {

                //Game1.player.currentLocation.playSound("thunder_small");
                Mod.instance.sounds.PlayCue(SoundHandle.SoundCue.CastMists);

            }

            //---------------------------------------------
            // Sunder
            //---------------------------------------------

            if (Mod.instance.questHandle.IsGiven(QuestHandle.mistsOne))
            {

                if(castLevel % 2 == 0)
                {

                    CastSunder();

                }

            }

            if(Mod.instance.activeEvent.Count == 0)
            {

                if (Mod.instance.questHandle.IsGiven(QuestHandle.mistsTwo))
                {

                    Artifice artifice = new();

                    artifice.CastActivate(castVector);

                }

                if (Mod.instance.questHandle.IsGiven(QuestHandle.mistsThree))
                {

                    CastFishspot();

                }

            }

            if (Mod.instance.questHandle.IsGiven(QuestHandle.mistsFour))
            {

                CastSmite();

            }

            if (Mod.instance.questHandle.IsComplete(QuestHandle.questEffigy))
            {

                ChannelStart();

            }

        }

        public void ChannelMists()
        {

            Mod.instance.rite.ChargeSet(riteCharges.mists);

            CastWrath();

        }

        public void CastSunder()
        {

            int sundered = 0;

            //bool extraDebris = Mod.instance.questHandle.IsComplete(QuestHandle.mistsFour);

            if (Game1.player.currentLocation.resourceClumps.Count > 0)
            {

                for (int r = Game1.player.currentLocation.resourceClumps.Count -1; r >= 0; r--)
                {

                    ResourceClump resourceClump = Game1.player.currentLocation.resourceClumps[r];

                    if(resourceClump is GiantCrop)
                    {

                        continue;

                    }

                    int tryCost = 32 - Game1.player.ForagingLevel * 3;

                    int cost = tryCost < 8 ? 8 : tryCost;

                    if (Vector2.Distance(resourceClump.Tile, castVector) <= 4)
                    {

                        Vector2 resourcePosition = resourceClump.Tile * 64 + new Vector2(32);

                        Mod.instance.spellRegister.Add(new(resourcePosition, 128, IconData.impacts.boltnode, new()) { type = SpellHandle.Spells.quickbolt, soundTrigger = SoundHandle.SoundCue.CastBolt, });

                        switch (resourceClump.parentSheetIndex.Value)
                        {
                            case ResourceClump.stumpIndex:
                            case ResourceClump.hollowLogIndex:

                                Explosion.DestroyStump(Game1.player.currentLocation, resourceClump, resourceClump.Tile);
                                break;

                            default:

                                Explosion.DestroyBoulder(Game1.player.currentLocation, resourceClump, resourceClump.Tile);
                                break;

                        }


                        castCost += cost;

                        sundered++;

                    }

                }

            }

            List<Vector2> stumpVectors = ModUtility.GetTilesWithinRadius(Game1.player.currentLocation, castVector, 1, false);

            stumpVectors.Add(castVector);

            foreach(Vector2 stumpVector in stumpVectors)
            {

                if (Game1.player.currentLocation.terrainFeatures.ContainsKey(stumpVector))
                {

                    if (Game1.player.currentLocation.terrainFeatures[stumpVector] is Tree tree)
                    {

                        if (tree.stump.Value)
                        {

                            Mod.instance.spellRegister.Add(new(stumpVector * 64 + new Vector2(32), 128, IconData.impacts.puff, new()) { type = SpellHandle.Spells.quickbolt, display = IconData.impacts.puff, displayRadius = 2, });

                            tree.performToolAction(Mod.instance.virtualAxe, 0, stumpVector);

                            Game1.player.currentLocation.terrainFeatures.Remove(stumpVector);

                            sundered++;

                        }

                    }

                }
            }

            if (sundered > 0)
            {

                if (!Mod.instance.questHandle.IsComplete(QuestHandle.mistsOne))
                {

                    Mod.instance.questHandle.UpdateTask(QuestHandle.mistsOne, sundered);

                }
            
            }


        }

        public void CastFishspot()
        {

            // ---------------------------------------------
            // Water effect
            // ---------------------------------------------

            if((castLevel % 4) != 0)
            {
            
                return; 
            
            }

            //if (spawnIndex.fishspot)
            //{

            if (ModUtility.WaterCheck(Game1.player.currentLocation, castVector))
            {

                int tryCost = 32 - Game1.player.FishingLevel * 3;

                castCost += tryCost < 8 ? 8 : tryCost;

                Fishspot fishspotEvent = new();

                fishspotEvent.EventSetup(Game1.player, castVector * 64, Rite.eventFishspot);

                fishspotEvent.EventActivate();
            }

            //}

            if (Game1.player.currentLocation is VolcanoDungeon volcanoLocation)
            {
                int tileX = (int)castVector.X;
                int tileY = (int)castVector.Y;

                if (volcanoLocation.waterTiles[tileX, tileY] && !volcanoLocation.cooledLavaTiles.ContainsKey(castVector))
                {
                    int waterRadius = Math.Min(5, Mod.instance.PowerLevel);

                    for (int i = 0; i < waterRadius + 1; i++)
                    {

                        List<Vector2> radialVectors = ModUtility.GetTilesWithinRadius(volcanoLocation, castVector, i);

                        foreach (Vector2 radialVector in radialVectors)
                        {
                            int radX = (int)radialVector.X;
                            int radY = (int)radialVector.Y;

                            if (volcanoLocation.waterTiles[radX, radY] && !volcanoLocation.cooledLavaTiles.ContainsKey(radialVector))
                            {

                                volcanoLocation.CoolLava(radX, radY);

                                volcanoLocation.UpdateLavaNeighbor(radX, radY);

                            }

                        }

                    }

                    List<Vector2> fourthVectors = ModUtility.GetTilesWithinRadius(volcanoLocation, castVector, waterRadius + 1);

                    foreach (Vector2 fourthVector in fourthVectors)
                    {
                        int fourX = (int)fourthVector.X;
                        int fourY = (int)fourthVector.Y;

                        volcanoLocation.UpdateLavaNeighbor(fourX, fourY);

                    }

                    //Mod.instance.iconData.AnimateBolt(volcanoLocation, castVector * 64 + new Vector2(32));
                    Mod.instance.spellRegister.Add(new(castVector * 64 + new Vector2(32), 128, IconData.impacts.puff, new()) { type = SpellHandle.Spells.bolt, displayRadius = 2, });

                }

            }

        }

        public void CastSmite()
        {

            if (Mod.instance.eventRegister.ContainsKey(eventWrath))
            {

                return;

            }

            // ---------------------------------------------
            // Monster iteration
            // ---------------------------------------------

            float damage = Mod.instance.CombatDamage();

            List<StardewValley.Monsters.Monster> victims = ModUtility.MonsterProximity(Game1.player.currentLocation, castVector * 64, 384, true);

            if (victims.Count > 0)
            {

                List<float> crits = Mod.instance.CombatCritical();

                if (!Mod.instance.questHandle.IsComplete(QuestHandle.mistsFour))
                {

                    Mod.instance.questHandle.UpdateTask(QuestHandle.mistsFour, 1);

                }
                else
                {
                    crits[0] += 0.2f;
                }

                SpellHandle bolt = new(Game1.player, new() { victims.First(), }, damage)
                {
                    type = SpellHandle.Spells.bolt,

                    critical = crits[0],

                    criticalModifier = crits[1],

                    damageRadius = 192,

                    display = IconData.impacts.boltnode,

                    displayRadius = 4,

                    added = new() { SpellHandle.Effects.push, }// SpellHandle.effects.shock};
                };

                if (Mod.instance.herbalHandle.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.spellcatch))
                {

                    if (Mod.instance.herbalHandle.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.capture))
                    {

                        bolt.added.Add(SpellHandle.Effects.capture);

                    }

                }

                Mod.instance.spellRegister.Add(bolt);

                int tryCost = 24 - Game1.player.CombatLevel;

                castCost += tryCost < 12 ? 12 : tryCost;

            }

            // ---------------------------------------------
            // Villager iteration
            // ---------------------------------------------

            if (castLevel % 3 != 0 || Mod.instance.activeEvent.Count > 0)
            {

                return;

            }

            List<NPC> villagers = ModUtility.GetFriendsInLocation(Game1.player.currentLocation, true);

            float threshold = 640;

            foreach (NPC witness in villagers)
            {

                if (Mod.instance.Witnessed(ReactionData.reactions.mists, witness))
                {

                    //continue;

                }

                if (Vector2.Distance(witness.Position, castVector*64) >= threshold)
                {

                    continue;

                }

                Microsoft.Xna.Framework.Rectangle box = witness.GetBoundingBox();

                SpellHandle bolt = new(new(box.Left, box.Top), 192, IconData.impacts.boltnode, new())
                {
                    type = SpellHandle.Spells.bolt,

                    scheme = IconData.schemes.mists,

                    damageRadius = 192
                };

                Mod.instance.spellRegister.Add(bolt);

                witness.faceTowardFarmerForPeriod(3000, 4, false, Game1.player);

                if (RecruitHandle.RecruitWitness(witness))
                {

                    Mod.instance.AddWitness(ReactionData.reactions.mists, witness.Name);

                    continue;

                }

                ModUtility.ChangeFriendship(witness, -10);

                ReactionData.ReactTo(witness, ReactionData.reactions.mists, -10, new());

            }

        }

        public static void CastWrath()
        {

            if (Mod.instance.eventRegister.ContainsKey(eventWrath))
            {

                return;

            }

            Wrath wrathNew = new();

            wrathNew.EventSetup(Game1.player, Game1.player.Position, eventWrath);

            wrathNew.EventActivate();

        }

        // --------------------------------------------------------------------------------------

        public void CastStars()
        {

            if (Mod.instance.questHandle.IsGiven(QuestHandle.starsOne))
            {

                CastMeteors();

            }

            if (Mod.instance.questHandle.IsGiven(QuestHandle.starsTwo))
            {

                ChannelStart();

            }

        }

        public void ChannelStars()
        {

            CastComet();

            CastBlackhole();

            Mod.instance.rite.ChargeSet(riteCharges.stars);

        }

        public void CastMeteors()
        {


            if(
                Game1.player.currentLocation.IsOutdoors ||
                Game1.player.currentLocation is Shed ||
                Game1.player.currentLocation is FarmHouse ||
                Game1.player.currentLocation is IslandFarmHouse ||
                Game1.player.currentLocation.IsGreenhouse
            )
            {

                if (Game1.player.CurrentTool is not MeleeWeapon || Game1.player.CurrentTool.isScythe())
                {

                    return;

                }

            }

            List<Vector2> meteorVectors = new();

            int difficulty = Mod.instance.Config.meteorBehaviour;

            int meteorLimit = 1;

            if (Mod.instance.questHandle.IsComplete(QuestHandle.starsOne) && Mod.instance.randomIndex.Next(2) == 0)
            {

                meteorLimit = 2;

            }

            if(castLevel % 4 == 0 || vectorList.Count < meteorLimit)
            {

                if(difficulty == 5)
                {
                    
                    vectorList = new();

                    List<Vector2> innerTiles = ModUtility.GetTilesWithinRadius(Game1.player.currentLocation, Vector2.Zero, 3, false);

                    for (int iv = 0; iv < 3; iv++)
                    {
                        
                        vectorList.Add(innerTiles[Mod.instance.randomIndex.Next(innerTiles.Count)]);
                        
                    }

                    List<Vector2> outerTiles = ModUtility.GetTilesWithinRadius(Game1.player.currentLocation, Vector2.Zero, 4, false);

                    for (int ov = 0; ov < 3; ov++)
                    {
                        
                        vectorList.Add(outerTiles[Mod.instance.randomIndex.Next(outerTiles.Count)]);

                    }

                }
                else
                {
                    vectorList = new()
                    {
                        new Vector2(3,-4),
                        new Vector2(-3,-4),
                        new Vector2(5,0),
                        new Vector2(-5,0),
                        new Vector2(3,4),
                        new Vector2(-3,4),
                    };

                }

            }

            if (difficulty == 1 || difficulty == 2)
            {

                foreach (NPC nonPlayableCharacter in Game1.player.currentLocation.characters)
                {

                    if (meteorVectors.Count >= meteorLimit)
                    {

                        break;

                    }

                    if (nonPlayableCharacter is StardewValley.Monsters.Monster monsterCharacter)
                    {

                        Vector2 monsterVector = monsterCharacter.Tile;

                        if(meteorVectors.Count > 0)
                        {

                            if(Vector2.Distance(monsterVector,meteorVectors.First()) < 4)
                            {
                                
                                continue;
                            
                            }

                        }

                        if (Vector2.Distance(castVector, monsterVector) > 6)
                        {

                            continue;

                        }

                        meteorVectors.Add(monsterVector);

                    }

                }

            }

            if((difficulty == 1 || difficulty == 3) && (Game1.player.currentLocation is MineShaft || Game1.player.currentLocation is VolcanoDungeon))
            {

                for (int i = 2; i < 6; i++)
                {

                    if (meteorVectors.Count >= meteorLimit)
                    {

                        break;

                    }

                    List<Vector2> objectVectors = ModUtility.GetTilesWithinRadius(Game1.player.currentLocation, castVector, i);

                    foreach (Vector2 objectVector in objectVectors)
                    {

                        if (meteorVectors.Count >= meteorLimit)
                        {

                            break;

                        }

                        if (Game1.player.currentLocation.objects.ContainsKey(objectVector))
                        {

                            StardewValley.Object targetObject = Game1.player.currentLocation.objects[objectVector];

                            if (targetObject.Name == "Stone")
                            {

                                if (meteorVectors.Count > 0)
                                {

                                    if (Vector2.Distance(objectVector, meteorVectors.First()) < 5)
                                    {

                                        continue;

                                    }

                                }

                                meteorVectors.Add(objectVector);

                            }

                        }

                    }

                }

            }

            if(meteorVectors.Count > 0)
            {

                foreach(Vector2 meteorVector in meteorVectors)
                {

                    for(int i = vectorList.Count - 1; i >= 0; i--)
                    {

                        Vector2 tryVector = castVector + vectorList[i];

                        if (Vector2.Distance(meteorVector, tryVector) <= 3)
                        {

                            vectorList.RemoveAt(i);

                            break;

                        }

                    }

                }

            }

            if (meteorVectors.Count < meteorLimit)
            {

                for(int i = 0; i < meteorLimit - meteorVectors.Count; i++)
                {
                    
                    Vector2 randomVector = vectorList[Mod.instance.randomIndex.Next(vectorList.Count)];

                    string groundCheck = ModUtility.GroundCheck(Game1.player.currentLocation, castVector + randomVector);

                    if(groundCheck == "water" || groundCheck == "ground")
                    {

                        vectorList.Remove(randomVector);

                        meteorVectors.Add(castVector + randomVector);

                    }

                }

            }

            if (meteorVectors.Count == 0)
            {

                return;

            }

            float damage = Mod.instance.CombatDamage() * 1.5f;

            int extra = 0;

            switch (difficulty)
            {
                case 2:
                case 3:

                    damage *= 1.1f;

                    break;

                case 4:

                    damage *= 1.25f;

                    if (Mod.instance.randomIndex.Next(3) == 0) { extra++; }

                    break;

                case 5:

                    damage *= 1.6f;

                    if(Mod.instance.randomIndex.Next(2) == 0) { extra++; }

                    break;

            }

            foreach (Vector2 meteorVector in meteorVectors)
            {
                
                if (!Mod.instance.questHandle.IsComplete(QuestHandle.starsOne))
                {

                    List<StardewValley.Monsters.Monster> monsters = ModUtility.MonsterProximity(Game1.player.currentLocation, meteorVector * 64, 224, true);

                    for (int i = monsters.Count - 1; i >= 0; i--)
                    {

                        Mod.instance.questHandle.UpdateTask(QuestHandle.starsOne, 1);

                    }

                }

                int radius = 4 + extra;

                int scale = radius;

                SpellHandle meteor = MeteorInstance(meteorVector, radius, scale, damage);

                Mod.instance.spellRegister.Add(meteor);

                int tryCost = 12 - Game1.player.CombatLevel;

                castCost += tryCost < 5 ? 5 : tryCost;

            }

            // ---------------------------------------------
            // Villager iteration
            // ---------------------------------------------

            if (castLevel % 5 != 0)
            {

                return;

            }

            if (Mod.instance.activeEvent.Count > 0)
            {

                return;

            }

            if (meteorVectors.Count == 0)
            {

                return;

            }

            Vector2 impactVector = meteorVectors.First() * 64;

            List<NPC> villagers = ModUtility.GetFriendsInLocation(Game1.player.currentLocation, true);

            float threshold = 640;

            foreach (NPC witness in villagers)
            {

                if (Mod.instance.Witnessed(ReactionData.reactions.stars, witness))
                {

                    continue;

                }

                if (Vector2.Distance(witness.Position, impactVector) >= threshold)
                {

                    continue;

                }

                witness.faceTowardFarmerForPeriod(3000, 4, false, Game1.player);

                ReactionData.ReactTo(witness, ReactionData.reactions.stars, 0, new());

            }

        }

        public SpellHandle MeteorInstance(Vector2 meteorVector, int radius, int scale, float damage)
        {


            Vector2 meteorTarget = meteorVector * 64;

            SpellHandle meteor = new(Game1.player, meteorTarget, radius * 64, damage)
            {

                type = SpellHandle.Spells.missile,

                missile = MissileHandle.missiles.meteor

            };

            meteor.added = new() { SpellHandle.Effects.explode, SpellHandle.Effects.hack, };

            meteor.effectRadius = radius;

            if (damage > 0)
            {

                meteor.display = IconData.impacts.dustimpact;

                meteor.displayRadius = 4;

                meteor.indicator = IconData.cursors.stars;

                meteor.displayFactor = scale;

                meteor.soundTrigger = SoundHandle.SoundCue.CastStars;

                meteor.soundImpact = SoundHandle.SoundCue.ImpactStars;

            }

            switch (ModUtility.GroundCheck(Game1.player.currentLocation, meteorVector))
            {

                case "water":

                    meteor.display = IconData.impacts.splash;

                    meteor.sound = SpellHandle.Sounds.dropItemInWater;

                    break;

            }

            if (Mod.instance.herbalHandle.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.spellcatch))
            {

                if (Mod.instance.herbalHandle.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.capture))
                {

                    meteor.added.Add(SpellHandle.Effects.capture);

                }

            }

            return meteor;

        }

        public void CastComet()
        {

            SpellHandle meteor = MeteorInstance(castVector, 8, 5, Mod.instance.CombatDamage() * 4);

            meteor.display = IconData.impacts.bigimpact;

            meteor.displayRadius = 5;

            meteor.added.Add(SpellHandle.Effects.reave);

            Mod.instance.spellRegister.Add(meteor);

        }

        public void CastBlackhole()
        {

            Vector2 blackholeVector = GetTargetCursor(Game1.player.FacingDirection, 384);

            bool water = false;

            if (ModUtility.GroundCheck(Game1.player.currentLocation, ModUtility.PositionToTile(blackholeVector)) == "water")
            {

                water = true;

            }

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.starsTwo))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.starsTwo, 1);
            }

            SpellHandle hole = new(Game1.player, blackholeVector, 5 * 64, Mod.instance.CombatDamage() * 4)
            {
                
                type = SpellHandle.Spells.blackhole
            
            };

            castCost = 48;

            if (water)
            {

                hole.added = new() { SpellHandle.Effects.tornado, };

                castCost = 96;

            }
            else if (Game1.player.CurrentTool is not MeleeWeapon || Game1.player.CurrentTool.isScythe())
            {

                hole.added = new() { SpellHandle.Effects.harvest, };

                hole.added = new() { SpellHandle.Effects.gravity, };

            }

            Mod.instance.spellRegister.Add(hole);

            ApplyCost();

        }

        // --------------------------------------------------------------------------------------

        public void CastFates()
        {

            CastWhisk();

            if (Mod.instance.questHandle.IsGiven(QuestHandle.fatesThree))
            {

                CastTricks();

            }

            if(Mod.instance.questHandle.IsComplete(QuestHandle.questJester))
            {

                ChannelStart();

            }

        }

        public void ChannelFates()
        {

            Mod.instance.rite.ChargeSet(riteCharges.fates);

            if (Mod.instance.questHandle.IsGiven(QuestHandle.fatesFour))
            {

                if (
                    Game1.player.currentLocation.IsFarm
                    || Game1.player.currentLocation.IsGreenhouse
                    || Game1.player.currentLocation is AnimalHouse
                    || Game1.player.currentLocation is Shed
                    )
                {

                    CastEnchant();

                    return;

                }

            }

            CastWinds();

        }

        public void CastWhisk()
        {

            // ---------------------------------------------
            // Whisk
            // ---------------------------------------------

            if (castLevel != 0)
            {

                return;

            }

            if (Mod.instance.eventRegister.ContainsKey("whisk"))
            {

                return;

            }

            int whiskRange = 18;

            if (Mod.instance.questHandle.IsComplete(QuestHandle.fatesOne))
            {

                whiskRange += 6;

            }

            Vector2 farVector = GetTargetCursor(Game1.player.FacingDirection, whiskRange * 64);

            Vector2 nearVector = GetTargetCursor(Game1.player.FacingDirection, 6 * 64);

            List<Vector2> whiskTiles = ModUtility.GetTilesBetweenPositions(Game1.player.currentLocation, farVector, nearVector);

            for (int i = whiskTiles.Count - 1; i >= 0; i--)
            {

                if (ModUtility.TileAccessibility(Game1.player.currentLocation, whiskTiles[i]) != 0)
                {

                    continue;

                }

                Whisk newWhisk = new();

                newWhisk.EventSetup(Game1.player, whiskTiles[i] * 64, "whisk");

                newWhisk.EventActivate();

                //castCost += 12;

                break;

            }

            if (!Mod.instance.questHandle.IsGiven(QuestHandle.fatesTwo))
            {
                
                return;

            }

            List<StardewValley.Monsters.Monster> monsters = ModUtility.MonsterProximity(Game1.player.currentLocation, farVector, 480, true);

            if (monsters.Count > 0)
            {

                if (!Mod.instance.eventRegister.ContainsKey("whisk"))
                {
                        
                    Whisk newWhisk = new();

                    newWhisk.EventSetup(Game1.player, Game1.player.Position, "whisk");

                    newWhisk.whiskreturn = true;

                    newWhisk.EventActivate();

                }

                Curse curseEffect;

                if (!Mod.instance.eventRegister.ContainsKey(eventCurse))
                {

                    curseEffect = new()
                    {
                        eventId = eventCurse
                    };

                    curseEffect.EventActivate();

                }
                else
                {

                    curseEffect = Mod.instance.eventRegister[eventCurse] as Curse;

                }

                foreach (StardewValley.Monsters.Monster monster in monsters)
                {

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.fatesTwo))
                    {

                        Mod.instance.questHandle.UpdateTask(QuestHandle.fatesTwo, 1);

                    }

                    curseEffect.AddTarget(Game1.player.currentLocation, monster, ChargeEffect(riteCharges.fates, true));

                }
                
            }


        }

        public static void CastTricks()
        {

            GameLocation location = Game1.player.currentLocation;

            List<NPC> villagers = ModUtility.GetFriendsInLocation(location, true);

            Vector2 castAt = GetTargetCursor(Game1.player.FacingDirection, 12 * 64);

            float threshold = 640;

            foreach (NPC witness in villagers)
            {

                if (Vector2.Distance(witness.Position, castAt) >= threshold)
                {

                    continue;

                }

                if (Mod.instance.Witnessed(ReactionData.reactions.fates, witness))
                {

                    continue;

                }

                // Witness behaviour

                if (!Mod.instance.questHandle.IsComplete(QuestHandle.fatesThree))
                {

                    Mod.instance.questHandle.UpdateTask(QuestHandle.fatesThree, 1);

                }

                witness.faceTowardFarmerForPeriod(3000, 4, false, Game1.player);

                witness.doEmote(8);

                Mod.instance.iconData.CursorIndicator(location, witness.Position + new Vector2(32), IconData.cursors.fates, new());

                int display = Mod.instance.randomIndex.Next(5);

                int friendship = Mod.instance.randomIndex.Next(0, 5) * 25 - 25;

                ModUtility.ChangeFriendship(witness, friendship);

                Game1.player.friendshipData[witness.Name].TalkedToToday = true;

                string trick = Mod.instance.Helper.Translation.Get("ReactionData.220");

                switch (display)
                {

                    case 1:
                        trick = Mod.instance.Helper.Translation.Get("ReactionData.226");
                        break;
                    case 2:
                        trick = Mod.instance.Helper.Translation.Get("ReactionData.229");
                        break;
                    case 3:
                        trick = Mod.instance.Helper.Translation.Get("ReactionData.232");
                        break;
                    case 4:
                        trick = Mod.instance.Helper.Translation.Get("ReactionData.346.1");
                        break;
                }

                ReactionData.ReactTo(witness, ReactionData.reactions.fates, friendship, new() { trick, });

                // Trick performance

                SpellHandle trickSpell = new(witness.Position, 192, IconData.impacts.sparkle, new())
                {
                    instant = true,

                    displayRadius = 3,

                    type = SpellHandle.Spells.trick,

                    scheme = IconData.schemes.fates,

                    displayFactor = display
                };

                Mod.instance.spellRegister.Add(trickSpell);

            }

        }

        public void CastEnchant()
        {

            if (Mod.instance.eventRegister.ContainsKey("enchant"))
            {


                if (Mod.instance.eventRegister["enchant"] is Enchant enchantment)
                {

                    return;

                }

            }

            Enchant enchantmentEvent = new();

            enchantmentEvent.EventSetup(Game1.player, Game1.player.Position, "enchant");

            enchantmentEvent.giantTiles.Add(ModUtility.PositionToTile(Game1.player.Position) + (ModUtility.DirectionAsVector((Game1.player.FacingDirection * 2)) * 2));

            enchantmentEvent.EventActivate();

        }

        public void CastWinds()
        {

            if (Mod.instance.eventRegister.ContainsKey(eventWinds))
            {

                if (Mod.instance.eventRegister[eventWinds] is Winds)
                {

                    return;

                }

            }

            Winds windsNew = new();

            windsNew.EventSetup(Game1.player, Game1.player.Position, eventWinds);

            windsNew.EventActivate();

            windsNew.damageMonsters = Mod.instance.CombatDamage() / 5;

        }

        // --------------------------------------------------------------------------------------

        public void CastEther()
        {

            ChannelStart();

        }

        public void ChannelEther()
        {

            if (Mod.instance.eventRegister.ContainsKey(eventTransform))
            {

                if (Mod.instance.eventRegister[eventTransform] is Cast.Ether.Transform transformEvent)
                {

                    if (transformEvent.AttemptShutdown())
                    {

                        Mod.instance.eventRegister.Remove(eventTransform);

                    }

                }

                return;

            }

            Cast.Ether.Transform transform = new()
            {

                leftActive = true

            };

            if (Mod.instance.questHandle.IsGiven(QuestHandle.etherTwo))
            {

                transform.rightActive = true;

            }

            transform.EventActivate();

        }

        public void CreateTreasure()
        {

            if (!Mod.instance.questHandle.IsGiven(QuestHandle.etherFour))
            {

                return;

            }

            if (Mod.instance.eventRegister.ContainsKey("crate_"+ Game1.player.currentLocation.Name))
            {

                return;

            }

            bool crate = false;

            if (
                Game1.player.currentLocation is Forest || 
                Game1.player.currentLocation is Mountain || 
                Game1.player.currentLocation is Desert || 
                Game1.player.currentLocation is BugLand || 
                Game1.player.currentLocation is Beach || 
                Game1.player.currentLocation is Atoll )
            {

                crate = true;

            }
            else
            if (Game1.player.currentLocation is Farm)
            {



            }
            else
            if (
                Game1.player.currentLocation.IsOutdoors && 
                Game1.player.currentLocation.Map.Layers[0].LayerWidth * Game1.player.currentLocation.Map.Layers[0].LayerHeight > 2400)
            {

                crate = true;

            }
            else if (Game1.player.currentLocation is MineShaft mineShaft)
            {

                List<int> mineLevels = new() { 3, 7 };

                if (mineLevels.Contains(mineShaft.mineLevel % 10) && Mod.instance.questHandle.IsComplete(QuestHandle.etherFour))
                {

                    crate = true;

                }

            }

            if (!crate)
            {

                return;

            }

            if (!specialCasts.ContainsKey(Game1.player.currentLocation.Name))
            {

                specialCasts[Game1.player.currentLocation.Name] = new();

            }

            if (specialCasts[Game1.player.currentLocation.Name].Contains("crate"))
            {

                return;

            }

            int layerWidth = Game1.player.currentLocation.map.Layers[0].LayerWidth;

            int layerHeight = Game1.player.currentLocation.map.Layers[0].LayerHeight;

            Crate treasure;

            for (int i = 0; i < 10; i++)
            {

                int X = Mod.instance.randomIndex.Next(6, layerWidth - 6);

                int Y = Mod.instance.randomIndex.Next(6, layerHeight - 6);

                if(Game1.player.currentLocation is Atoll)
                {

                    X = Mod.instance.randomIndex.Next(2, layerWidth - 2);

                    Y = Mod.instance.randomIndex.Next(6, layerHeight - 2);

                }

                Vector2 treasureVector = new(X, Y);

                if (ModUtility.NeighbourCheck(Game1.player.currentLocation, treasureVector, 0, 0).Count > 0)
                {

                    continue;

                }

                if (ModUtility.ActionCheck(Game1.player.currentLocation, treasureVector, 0, 1).Count > 0)
                {

                    continue;

                }

                string treasureTerrain = ModUtility.GroundCheck(Game1.player.currentLocation, treasureVector);

                treasure = new Crate();

                treasure.EventSetup(Game1.player, treasureVector * 64, "crate_" + Game1.player.currentLocation.Name);

                treasure.triggerEvent = true;

                treasure.crateThief = Mod.instance.randomIndex.NextBool();

                treasure.crateTreasure = true;

                treasure.location = Game1.player.currentLocation;

                switch (treasureTerrain)
                {

                    case "ground":

                        treasure.crateTerrain = 1;

                        return;

                    case "water":

                        treasure.crateTerrain = 2;

                        return;

                }

            }
            
        }

        // --------------------------------------------------------------------------------------

        public void CastWitch()
        {

            ChannelStart();

        }

        public void ReleaseWitch()
        {

            CastMob();

        }

        public void ChannelWitch()
        {

            Mod.instance.rite.ChargeSet(riteCharges.witch);

            StardewDruid.Cast.Witch.CorvidHandle.ToggleCorvids();

        }

        public void CastMob(bool returnToPlayer = false)
        {

            List<CharacterHandle.characters> corvids = new()
            {
                CharacterHandle.characters.Raven,
                CharacterHandle.characters.Crow,
                CharacterHandle.characters.Rook,
                CharacterHandle.characters.Magpie,
                CharacterHandle.characters.PalBat,
                CharacterHandle.characters.PalSlime,
                CharacterHandle.characters.PalSpirit,
                CharacterHandle.characters.PalSerpent,
            };

            if (!returnToPlayer)
            {

                returnToPlayer = Vector2.Distance(castVector * 64, Game1.player.Position) <= 160;

            }

            foreach (CharacterHandle.characters corvid in corvids)
            {

                if (Mod.instance.trackers.ContainsKey(corvid))
                {

                    Mod.instance.trackers[corvid].TrackSubject().ResetActives();

                    if (returnToPlayer && Mod.instance.trackers[corvid].linger != 0)
                    {

                        Mod.instance.trackers[corvid].linger = 0;

                        Mod.instance.trackers[corvid].lingerSpot = Vector2.Zero;

                        continue;

                    }

                    Mod.instance.trackers[corvid].linger = 120;

                    Mod.instance.trackers[corvid].lingerSpot = castVector * 64;

                }

            }

        }

        // --------------------------------------------------------------------------------------

        public void RiteBuff()
        {

            int tool = Mod.instance.AttuneableWeapon();

            Rites blessing = SelectBlessing(tool);

            if(appliedBuff == blessing)
            {

                if (Game1.player.buffs.IsApplied(buffIdRite))
                {
                    
                    return;

                }

            }

            appliedBuff = blessing;

            IconData.displays buffIndex = RiteDisplay(blessing, tool);

            Buff riteBuff = new(
                buffIdRite,
                source: StringData.Strings(StringData.stringkeys.stardewDruid),
                displaySource: BuffSource(),
                duration: Buff.ENDLESS,
                iconTexture: Mod.instance.iconData.displayTexture,
                iconSheetIndex: (int)buffIndex - 1,
                displayName: RiteData.BuffNames(buffIndex),
                description: RiteData.Strings(riteStrings.clickJournal)
                );

            Game1.player.buffs.Apply(riteBuff);

        }

        public static IconData.displays RiteDisplay(Rites blessing, int tool = -1)
        {

            IconData.displays buffIndex = IconData.displays.druid;

            switch (blessing)
            {

                case Rites.weald:

                    buffIndex = IconData.displays.weald;

                    break;

                case Rites.mists:

                    buffIndex = IconData.displays.mists;

                    break;

                case Rites.fates:

                    buffIndex = IconData.displays.fates;

                    break;

                case Rites.stars:

                    buffIndex = IconData.displays.stars;

                    break;

                case Rites.ether:

                    buffIndex = IconData.displays.ether;

                    break;

                case Rites.witch:

                    buffIndex = IconData.displays.witch;

                    break;

                case Rites.winds:

                    switch (tool)
                    {

                        default:

                            if (Game1.player.CurrentTool is MeleeWeapon weapon)
                            {

                                buffIndex = IconData.displays.winds;

                            }

                            break;

                        case 997:

                            buffIndex = IconData.displays.bombs;

                            break;

                    }

                    break;

            }

            return buffIndex;

        }

        public string BuffSource()
        {

            string magic_mode = StringData.Strings(StringData.stringkeys.stardewDruid);

            if (Mod.instance.magic)
            {

                magic_mode = StringData.Strings(StringData.stringkeys.magicByNeosinf);

            }

            return magic_mode;

        }

        public void ChargeBuff()
        {

            if (chargeType == riteCharges.none || !chargeActive)
            {

                if (Game1.player.buffs.IsApplied(buffIdCharge))
                {

                    Game1.player.buffs.Remove(buffIdCharge);

                }

                selectedCharge = riteCharges.none;

                return;

            }

            if (selectedCharge == chargeType)
            {

                return;

            }

            if (Game1.player.buffs.IsApplied(buffIdCharge))
            {

                Game1.player.buffs.Remove(buffIdCharge);

            }

            IconData.displays cursorIndex = IconData.displays.sword;

            string chargeBuffName = RiteData.Strings(RiteData.riteStrings.chargeWealdName);
           
            string chargeBuffDescription = RiteData.Strings(RiteData.riteStrings.chargeWealdDescription);

            switch (chargeType)
            {

                case riteCharges.mists:

                    cursorIndex = IconData.displays.chargemists;
                    chargeBuffName = RiteData.Strings(RiteData.riteStrings.chargeMistsName);
                    chargeBuffDescription = RiteData.Strings(RiteData.riteStrings.chargeMistsDescription);

                    break;

                case riteCharges.stars:

                    cursorIndex = IconData.displays.chargestars;
                    chargeBuffName = RiteData.Strings(RiteData.riteStrings.chargeStarsName);
                    chargeBuffDescription = RiteData.Strings(RiteData.riteStrings.chargeStarsDescription);

                    break;

                case riteCharges.fates:

                    cursorIndex = IconData.displays.chargefates;
                    chargeBuffName = RiteData.Strings(RiteData.riteStrings.chargeFatesName);
                    chargeBuffDescription = RiteData.Strings(RiteData.riteStrings.chargeFatesDescription);

                    break;

                case riteCharges.witch:

                    cursorIndex = IconData.displays.chargewitch;
                    chargeBuffName = RiteData.Strings(RiteData.riteStrings.chargeWitchName);
                    chargeBuffDescription = RiteData.Strings(RiteData.riteStrings.chargeWitchDescription);

                    break;

            }

            Buff riteBuff = new(
                buffIdCharge,
                source: StringData.Strings(StringData.stringkeys.stardewDruid),
                displaySource: BuffSource(),
                duration: Buff.ENDLESS,
                iconTexture: Mod.instance.iconData.displayTexture,
                iconSheetIndex: (int)cursorIndex-1,
                displayName: chargeBuffName,
                description: chargeBuffDescription
                );

            Game1.player.buffs.Apply(riteBuff);

            selectedCharge = chargeType;

        }


    }

}