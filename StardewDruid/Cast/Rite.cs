using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast.Effect;
using StardewDruid.Cast.Ether;
using StardewDruid.Cast.Fates;
using StardewDruid.Cast.Mists;
using StardewDruid.Cast.Stars;
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
using System.Threading;
using xTile.Dimensions;

namespace StardewDruid.Cast
{

    public class Rite
    {
        // -----------------------------------------------------

        public enum Rites
        {
            none,

            sworn,
            weald,
            mists,
            stars,
            fates,
            ether,
            bones,
            
        }

        public Rites castType;

        public Dictionary<Rites, QuestHandle.milestones> requirement = new()
        {

            [Rites.sworn] = QuestHandle.milestones.sworn_weapon,
            [Rites.weald] = QuestHandle.milestones.weald_weapon,
            [Rites.mists] = QuestHandle.milestones.mists_weapon,
            [Rites.stars] = QuestHandle.milestones.stars_weapon,
            [Rites.fates] = QuestHandle.milestones.fates_weapon,
            [Rites.ether] = QuestHandle.milestones.ether_weapon,
            [Rites.bones] = QuestHandle.milestones.bones_weapon,

        };

        public int castLevel;

        public int castCost;

        public Vector2 castVector;

        public int castTool;

        public string castLocation;

        public int castInterval;

        public int castTimer;

        public bool castActive;

        public List<Vector2> vectorList = new();

        public Dictionary<string, List<string>> specialCasts = new();

        public Dictionary<string, Dictionary<Vector2, string>> targetCasts = new();

        public Dictionary<string, Dictionary<Vector2, int>> terrainCasts = new();

        public Rites appliedBuff;

        // ----------------------------------------------------

        public IconData.cursors cursoring;

        public int cursorTimer;

        public Vector2 cursorVector;

        public int cursorDistance;

        // ----------------------------------------------------

        public IconData.skies channelling;

        public int channelLimit;

        public int channelTimer;

        // ----------------------------------------------------

        public Dictionary<Rites, IconData.cursors> riteCharges = new()
        {
            [Rites.weald] = IconData.cursors.wealdCharge,
            [Rites.mists] = IconData.cursors.mistsCharge,
            [Rites.stars] = IconData.cursors.starsCharge,
            [Rites.fates] = IconData.cursors.fatesCharge,
            [Rites.bones] = IconData.cursors.bonesCharge,
        };

        public IconData.cursors chargeType;

        public int chargeTimer;

        public int chargeCooldown;

        public int chargeChance;

        public bool chargeActive;

        public StardewValley.GameLocation chargeLocation;

        public int swingTimer;

        public bool swingRelease;

        public bool remindDefault;

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

        public IconData.displays selectedCrit = IconData.displays.none;

        public IconData.cursors selectedCharge = IconData.cursors.none;


        // ----------------------------------------------------

        public Rite()
        {

        }

        public void Shutdown()
        {

            castActive = false;

        }

        public void Draw(SpriteBatch b)
        {

            if (!castActive)
            {

                return;

            }

            if (castType == Rites.ether || castType == Rites.sworn)
            {

                return;

            }

            int offset = (int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds) % 2400 / 20;

            Microsoft.Xna.Framework.Vector2 drawPosition = new(Game1.player.Position.X - (float)Game1.viewport.X, Game1.player.Position.Y - (float)Game1.viewport.Y);

            float rotate = (float)Math.PI / 60 * offset;

            b.Draw(
                Mod.instance.iconData.decorationTexture,
                drawPosition +new Vector2(32),
                IconData.DecorativeRectangle(Mod.instance.iconData.riteDecorations[castType]),
                Color.White * 0.8f,
                rotate,
                new Vector2(32),
                3f,
                SpriteEffects.None,
                0.0001f
            );

            if (chargeActive)
            {

                b.Draw(
                    Mod.instance.iconData.cursorTexture,
                    drawPosition + new Vector2(32),
                    IconData.CursorRectangle(chargeType),
                    Color.White*0.8f,
                    rotate,
                    new Vector2(24),
                    3f,
                    SpriteEffects.None,
                    0.0002f
                );

            }

            if(channelling != IconData.skies.none)
            {

                b.Draw(
                    Mod.instance.iconData.skyTexture,
                    drawPosition + new Vector2(32),
                    IconData.SkyRectangle(channelling),
                    Color.White * 0.8f,
                    0,
                    new Vector2(32),
                    1f + (2f * (((float)channelLimit - (float)channelTimer) / (float)channelLimit)),
                    SpriteEffects.None,
                    0.0003f
                );

            }

            if (cursoring != IconData.cursors.none)
            {

                Vector2 cursorPosition = drawPosition;

                if(cursorVector != Vector2.Zero)
                {

                    cursorPosition = new(cursorVector.X - (float)Game1.viewport.X, cursorVector.Y - (float)Game1.viewport.Y);

                }
                else if (cursorDistance != -1)
                {

                    Vector2 cursorTarget = GetTargetCursor(Game1.player.FacingDirection, cursorDistance);

                    cursorPosition = new(cursorTarget.X - (float)Game1.viewport.X, cursorTarget.Y - (float)Game1.viewport.Y);

                }
                else
                {

                    cursorPosition += new Vector2(32);

                }
                
                b.Draw(
                    Mod.instance.iconData.cursorTexture,
                    cursorPosition,
                    IconData.CursorRectangle(cursoring),
                    Color.White * 0.8f,
                    rotate,
                    new Vector2(24),
                    3f,
                    SpriteEffects.None,
                    0.0004f
                );

            }

        }

        public void Click(EventHandle.actionButtons button)
        {

            switch (button)
            {

                case EventHandle.actionButtons.action:

                    swingRelease = true;

                    break;

                case EventHandle.actionButtons.special:

                    Charge();

                    break;

                case EventHandle.actionButtons.rite:

                    //Game1.timeOfDay = 1300;

                    //RecruitHandle.RecruitWitness(CharacterHandle.FindVillager("Marlon"));

                    //Ghostking bear = new(ModUtility.PositionToTile(Game1.player.Position + new Vector2(128,128)), Mod.instance.CombatDifficulty()/2);

                    //bear.SetMode(2);

                    //Game1.player.currentLocation.characters.Add(bear);

                    Mod.instance.RiteButtonSuppress();

                    Start();

                    break;

                case EventHandle.actionButtons.shift:

                    castLevel = 0;

                    break;

                case EventHandle.actionButtons.warp:

                    Shutdown();

                    break;

            }

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
                case Rites.bones:

                    castInterval = 120;

                    castFast = 6;

                    break;

                case Rites.sworn:

                    castInterval = 60;

                    castFast = 3;
                    break;
            }

            if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.celerity))
            {

                castInterval -= (int)(castFast * Mod.instance.herbalData.buff.applied[HerbalBuff.herbalbuffs.celerity].level);

            }

            vectorList = new();

            castLevel = 0;

            castLocation = Game1.player.currentLocation.Name;

            CursorShutdown();

        }

        public bool Start()
        {

            Rites blessing;

            int tool = Mod.instance.AttuneableWeapon();

            if (tool == 997)
            {

                blessing = Rites.sworn;

            }
            else
            if (Mod.instance.Config.slotAttune || Mod.instance.magic)
            {

                blessing = GetSlotBlessing();

                if (blessing == Rites.none)
                {

                    if (Mod.instance.CheckTrigger())
                    {

                        return false;

                    }

                    if (Mod.instance.save.milestone == QuestHandle.milestones.none)
                    {

                        Mod.instance.CastDisplay(Mod.instance.Config.journalButtons.ToString() + " " + StringData.Strings(StringData.stringkeys.openJournal));

                    }
                    else
                    {

                        Mod.instance.CastDisplay(StringData.Strings(StringData.stringkeys.noRiteAttuned) + " " + (Game1.player.CurrentToolIndex + 1));

                    }

                    return false;

                }

            }
            else 
            {

                if (tool == -1)
                {

                    Mod.instance.CastDisplay(StringData.Strings(StringData.stringkeys.riteTool));

                    return false;

                }

                Dictionary<int,Rites> attunement = SpawnData.WeaponAttunement();

                if (attunement.ContainsKey(tool))
                {

                    blessing = RequirementCheck(attunement[tool]);

                    if (blessing == Rites.none)
                    {

                        if (Mod.instance.CheckTrigger())
                        {

                            return false;

                        }

                        Mod.instance.CastDisplay(StringData.Strings(StringData.stringkeys.noToolAttunement));

                        return false;

                    }

                }
                else
                {

                    if(!remindDefault)
                    {

                        Mod.instance.CastDisplay(StringData.Strings(StringData.stringkeys.defaultToolAttunement));

                        remindDefault = true;

                    }

                    blessing = RequirementCheck(Mod.instance.save.rite, true);

                }

            }

            if(blessing != castType)
            {

                Shutdown();

            }

            castType = blessing;

            castTool = tool;

            castLocation = Game1.player.currentLocation.Name;

            CastVector();

            Reset();

            castActive = true;

            return true;

        }

        public void Update()
        {

            castTimer--;

            ChargeUpdate();

            ChannelUpdate();

            CursorUpdate();

            SwingUpdate();

            if (!castActive)
            {

                return;

            }

            if (castTimer > 0)
            {

                return;

            }

            if (!(castLevel == 0 || Mod.instance.RiteButtonHeld()))
            {

                Shutdown();

                return;

            }

            if(Game1.player.currentLocation.Name != castLocation)
            {

                castLocation = Game1.player.currentLocation.Name;

            }

            if (Mod.instance.Config.slotAttune || Mod.instance.magic)
            {

                Rites slot = GetSlotBlessing();

                if (castType != slot)
                {

                    if (!Start())
                    {

                        Shutdown();

                        return;

                    }

                }

            }
            else
            {

                int toolIndex = Mod.instance.AttuneableWeapon();

                if (castTool != toolIndex)
                {

                    if (!Start())
                    {

                        Shutdown();

                        return;

                    }

                }

            }

            if (castLevel == 0)
            {

                if (Mod.instance.CheckTrigger())
                {

                    Shutdown();

                    return;

                }

            }

            if (castType == Rites.none)
            {

                if (Mod.instance.save.milestone == QuestHandle.milestones.none)
                {

                    Mod.instance.CastMessage(Mod.instance.Config.journalButtons.ToString() + " " + StringData.Strings(StringData.stringkeys.openJournal));
                }
                else
                {

                    Mod.instance.CastMessage(StringData.Strings(StringData.stringkeys.nothingHappened));
                }

                Shutdown();

                return;

            }

            if (castType != Rites.ether && (Game1.player.Stamina <= (Game1.player.MaxStamina / 4) || Game1.player.health <= (Game1.player.maxHealth / 3)))
            {

                Mod.instance.AutoConsume();

                if (Game1.player.Stamina <= 16)
                {

                    if (castLevel > 0)
                    {
                        Mod.instance.CastMessage(StringData.Strings(StringData.stringkeys.energyContinue), 3);

                    }
                    else
                    {
                        Mod.instance.CastMessage(StringData.Strings(StringData.stringkeys.energyRite), 3);

                    }

                    Shutdown();

                    return;

                }

            }

            CastVector();

            castTimer = castInterval;

            Cast();

            castLevel++;

        }

        public void Charge()
        {

            if (!castActive) { return; }

            if (castType == Rites.ether || castType == Rites.sworn) { return; }

            if (castType == Rites.bones) 
            {

                castVector = ModUtility.PositionToTile(Game1.player.Position);

                CastMob(true);

                return; 
            
            
            }

            if(castType == Rites.stars)
            {

                if (!Mod.instance.eventRegister.ContainsKey(eventShield))
                {

                    Cast.Effect.Shield shieldEffect = new();

                    shieldEffect.EventSetup(Game1.player.Position, eventShield);

                    shieldEffect.EventActivate();

                }

            }

            return;


        }

        public void Dispense(int radius = 96)
        {

            if (Game1.player.CurrentTool is Axe)
            {

                AxeCrit();

                return;

            }
            else if (Game1.player.CurrentTool is Pickaxe)
            {

                PickaxeCrit();

                return;

            }
            else if (Game1.player.CurrentTool is WateringCan)
            {

                WatercanCrit();

                return;

            }

            if (!chargeActive) 
            { 
                
                return; 
            
            }

            int tool = Mod.instance.AttuneableWeapon();

            if (tool == -1 || tool == 997) 
            { 
                
                return; 
            
            }

            if (chargeCooldown > 0) 
            { 
                
                return; 
            
            }

            if(chargeType == IconData.cursors.none)
            { 
                
                return; 
            
            }

            int impes = 1;

            if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.vigor))
            {

                impes = Mod.instance.herbalData.buff.applied[HerbalBuff.herbalbuffs.vigor].level;

            }

            chargeChance = 25 + (5 * impes);

            if (Mod.instance.randomIndex.Next(100) > chargeChance)
            {

                return;

            }

            Vector2 checkVector = GetTargetDirectional(Game1.player.FacingDirection, radius);

            List<StardewValley.Monsters.Monster> monsters = ModUtility.MonsterProximity(Game1.player.currentLocation, new() { checkVector }, radius + 128, true);

            if(monsters.Count == 0)
            {

                return;

            }

            if (!Mod.instance.questHandle.IsComplete(QuestHandle.chargeUps))
            {

                Mod.instance.questHandle.UpdateTask(QuestHandle.chargeUps, 1);

            }

            int ligna = 1;

            if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.alignment))
            {

                ligna = Mod.instance.herbalData.buff.applied[HerbalBuff.herbalbuffs.alignment].level;

            }

            chargeTimer += 900;

            SpellHandle knockeffect = new(checkVector, radius + 32, IconData.impacts.crit, new());

            knockeffect.monsters = monsters;

            knockeffect.type = SpellHandle.Spells.explode;

            knockeffect.damageMonsters = (int)(Mod.instance.PowerLevel * 10 * (0.75f + (0.25f * ligna)));

            knockeffect.displayRadius = 2;

            knockeffect.instant = true;

            knockeffect.local = true;

            knockeffect.added.Add(ChargeEffect(chargeType));

            switch (chargeType)
            {
                
                case IconData.cursors.fatesCharge:

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.fatesTwo))
                    {

                        Mod.instance.questHandle.UpdateTask(QuestHandle.fatesTwo, 1);

                    }

                    knockeffect.scheme = IconData.schemes.fates;

                    break;

                case IconData.cursors.starsCharge:

                    knockeffect.scheme = IconData.schemes.herbal_impes;

                    knockeffect.damageMonsters *= 2;

                    break;

                case IconData.cursors.mistsCharge:

                    knockeffect.scheme = IconData.schemes.herbal_celeri;

                    break;

                case IconData.cursors.bonesCharge:

                    knockeffect.scheme = IconData.schemes.bones;

                    break;

                default: // weald

                    knockeffect.scheme = IconData.schemes.herbal_ligna;

                    break;

            }

            Mod.instance.spellRegister.Add(knockeffect);

        }

        public void Channel(IconData.skies sky, int timer)
        {

            channelling = sky;

            channelLimit = timer;

            channelTimer = timer;

        }

        public void Cursor(IconData.cursors cursor, int timer, int distance = -1, int x = 0, int y = 0)
        {

            cursoring = cursor;

            cursorTimer = timer;

            cursorDistance = distance;

            if(x > 0 || y > 0)
            {

                cursorVector = new Vector2(x, y);

            }
            else
            {

                cursorVector = Vector2.Zero;

            }

        }

        public SpellHandle.Effects ChargeEffect(IconData.cursors charge, bool ignoreCooldown = false)
        {

            float celeri = 1f;

            if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.celerity))
            {

                celeri -= 0.1f * (float)Mod.instance.herbalData.buff.applied[HerbalBuff.herbalbuffs.celerity].level;

            }

            switch (charge)
            {
                case IconData.cursors.fatesCharge:

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

                case IconData.cursors.starsCharge:

                    chargeCooldown = (int)(60f * celeri);

                    return SpellHandle.Effects.knock;

                case IconData.cursors.mistsCharge:

                    chargeCooldown = (int)(60f * celeri);

                    return SpellHandle.Effects.freeze;

                case IconData.cursors.bonesCharge:

                    chargeCooldown = (int)(180f * celeri);

                    return SpellHandle.Effects.omen;

                default:
                case IconData.cursors.wealdCharge:

                    chargeCooldown = (int)(30f * celeri);

                    return SpellHandle.Effects.drain;

            }

        }

        public void ChargeUpdate()
        {

            if (chargeCooldown > 0)
            {

                chargeCooldown--;

            }

            if (chargeTimer > 0)
            {

                chargeTimer--;

                if (chargeTimer <= 0)
                {

                    ChargeShutdown();

                    return;

                }

            }

        }

        public void ChargeShutdown()
        {

            chargeActive = false;

        }

        public void ChargeSet(IconData.cursors type)
        {

            chargeActive = true;

            chargeType = type;

            chargeTimer = 3000;

            chargeLocation = Game1.player.currentLocation;

        }

        public void ChannelUpdate()
        {

            if (channelling != IconData.skies.none)
            {

                channelTimer--;

                if(channelTimer <= 0)
                {

                    channelling = IconData.skies.none;

                }

            }

        }

        public void ChannelShutdown(IconData.skies sky)
        {

            if(channelling == sky)
            {

                channelling = IconData.skies.none;

            }

        }

        public void CursorUpdate()
        {

            if (cursoring != IconData.cursors.none)
            {

                cursorTimer--;

                if (cursorTimer <= 0)
                {

                    cursoring = IconData.cursors.none;

                }

            }

        }
        
        public void CursorShutdown()
        {

            cursoring = IconData.cursors.none;

            cursorTimer = 0;

            cursorVector = Vector2.Zero;

            cursorDistance = -1;

        }

        public void SwingUpdate()
        {

            if(swingTimer > 0)
            {

                swingTimer--;

                if (!Game1.player.canReleaseTool && swingRelease)
                {

                    Dispense();

                    swingRelease = false;

                }

            }
            else if(Game1.player.UsingTool && swingRelease)
            {

                int tool = Mod.instance.AttuneableWeapon();

                if (tool == -1 || tool == 997)
                {

                    return;

                }

                swingTimer = 8;

            }

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

            if ((int)Mod.instance.save.milestone >= (int)requirement[id])
            {

                return id;

            }

            if (next)
            {

                while((int)id > 1)
                {

                    id = (Rites)((int)id - 1);

                    if ((int)Mod.instance.save.milestone >= (int)requirement[id])
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

                    blessing = RequirementCheck(Rites.weald);

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

                case "bones":

                    blessing = RequirementCheck(Rites.bones, true);

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
                    
               case Rites.bones:
               case Rites.sworn:

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

            List<StardewValley.Monsters.Monster> checkMonsters = ModUtility.MonsterProximity(Game1.player.currentLocation, new() { checkVector }, radius);

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

                case Rites.bones:

                    CastBones();

                    break;

                case Rites.sworn:

                    CastBombs();

                    break;

                default:

                    CastWeald();

                    break;
            }

            ApplyCost(castCost);

            castCost = 0;

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

        public void ToolBuff()
        {

            switch (Game1.player.CurrentTool)
            {

                case Axe:

                    if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.alignment))
                    {

                        if (selectedCrit != IconData.displays.axe)
                        {

                            if (Game1.player.buffs.IsApplied(buffIdCrit))
                            {

                                Game1.player.buffs.Remove(buffIdCrit);

                            }

                            Buff riteBuff = new(
                                buffIdCrit,
                                source: StringData.Strings(StringData.stringkeys.stardewDruid),
                                displaySource: BuffSource(),
                                duration: Buff.ENDLESS,
                                iconTexture: Mod.instance.iconData.displayTexture,
                                iconSheetIndex: (int)IconData.displays.axe - 1,
                                displayName: StringData.Strings(StringData.stringkeys.axeBuffName),
                                description: StringData.Strings(StringData.stringkeys.axeBuffDescription)
                                );

                            Game1.player.buffs.Apply(riteBuff);

                            selectedCrit = IconData.displays.axe;

                        }

                        return;

                    }

                    break;

                case Pickaxe:

                    if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.vigor))
                    {

                        if (selectedCrit != IconData.displays.pickaxe)
                        {

                            if (Game1.player.buffs.IsApplied(buffIdCrit))
                            {

                                Game1.player.buffs.Remove(buffIdCrit);

                            }

                            Buff riteBuff = new(
                                buffIdCrit,
                                source: StringData.Strings(StringData.stringkeys.stardewDruid),
                                displaySource: BuffSource(),
                                duration: Buff.ENDLESS,
                                iconTexture: Mod.instance.iconData.displayTexture,
                                iconSheetIndex: (int)IconData.displays.pickaxe - 1,
                                displayName: StringData.Strings(StringData.stringkeys.pickBuffName),
                                description: StringData.Strings(StringData.stringkeys.pickBuffDescription)
                                );

                            Game1.player.buffs.Apply(riteBuff);

                            selectedCrit = IconData.displays.pickaxe;

                        }

                        return;

                    }

                    break;

                case Hoe:

                    if (appliedBuff == Rites.weald && Mod.instance.questHandle.IsGiven(QuestHandle.wealdThree))
                    {

                        if (selectedCrit != IconData.displays.hoe)
                        {

                            if (Game1.player.buffs.IsApplied(buffIdCrit))
                            {

                                Game1.player.buffs.Remove(buffIdCrit);

                            }

                            Buff riteBuff = new(
                                buffIdCrit,
                                source: StringData.Strings(StringData.stringkeys.stardewDruid),
                                displaySource: BuffSource(),
                                duration: Buff.ENDLESS,
                                iconTexture: Mod.instance.iconData.displayTexture,
                                iconSheetIndex: (int)IconData.displays.hoe - 1,
                                displayName: StringData.Strings(StringData.stringkeys.hoeBuffName),
                                description: StringData.Strings(StringData.stringkeys.hoeBuffDescription)
                                );

                            Game1.player.buffs.Apply(riteBuff);

                            selectedCrit = IconData.displays.hoe;

                        }

                        return;

                    }

                    break;

                case WateringCan:

                    if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.celerity))
                    {

                        if (selectedCrit != IconData.displays.watercan)
                        {

                            if (Game1.player.buffs.IsApplied(buffIdCrit))
                            {

                                Game1.player.buffs.Remove(buffIdCrit);

                            }

                            Buff riteBuff = new(
                                buffIdCrit,
                                source: StringData.Strings(StringData.stringkeys.stardewDruid),
                                displaySource: BuffSource(),
                                duration: Buff.ENDLESS,
                                iconTexture: Mod.instance.iconData.displayTexture,
                                iconSheetIndex: (int)IconData.displays.watercan - 1,
                                displayName: StringData.Strings(StringData.stringkeys.canBuffName),
                                description: StringData.Strings(StringData.stringkeys.canBuffDescription)
                                );

                            Game1.player.buffs.Apply(riteBuff);

                            selectedCrit = IconData.displays.watercan;

                        }

                        return;

                    }

                    break;

            }

            if (Game1.player.buffs.IsApplied(buffIdCrit))
            {

                Game1.player.buffs.Remove(buffIdCrit);

            }

            selectedCrit = IconData.displays.none;

        }

        public void AxeCrit()
        {

            if (chargeCooldown > 0)
            {

                return;

            }

            if (!Game1.player.currentLocation.IsOutdoors)
            {

                return;

            }

            if (!Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.alignment))
            {

                return;

            }

            int chance = Math.Max(2, 6 - Mod.instance.herbalData.buff.applied[HerbalBuff.herbalbuffs.alignment].level);

            if (Mod.instance.randomIndex.Next(chance) != 0)
            {

                return;

            }

            int axeDirection = Game1.player.FacingDirection * 2;

            Vector2 hit = Game1.player.GetBoundingBox().Center.ToVector2() + (ModUtility.DirectionAsVector(axeDirection) * 96f) - new Vector2(24, 24);

            SpellHandle critEffect = new(Game1.player.currentLocation, hit, hit, 256)
            {
                display = IconData.impacts.crit,

                scheme = IconData.schemes.herbal_ligna,

                displayRadius = 2,

                sound = SpellHandle.Sounds.stumpCrack,

                explosion = 1,

                power = 3,

                instant = true,

            };

            Mod.instance.spellRegister.Add(critEffect);

            chargeCooldown = 30;

            EventDisplay axeCrit = Mod.instance.CastDisplay(StringData.Strings(StringData.stringkeys.criticalHit));

            axeCrit.type = EventDisplay.displayTypes.quick;

            axeCrit.portrait = Mod.instance.iconData.displayTexture;

            axeCrit.portraitSource = IconData.DisplayRectangle(IconData.displays.axe);

            axeCrit.time = 20;

        }

        public void PickaxeCrit()
        {

            if (chargeCooldown > 0)
            {

                return;

            }

            if (Game1.player.currentLocation.IsOutdoors)
            {

                return;

            }

            if (!Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.vigor))
            {

                return;

            }

            int chance = Math.Max(2, 6 - Mod.instance.herbalData.buff.applied[HerbalBuff.herbalbuffs.vigor].level);

            if (Mod.instance.randomIndex.Next(chance) != 0)
            {
                return;
            }

            int axeDirection = Game1.player.FacingDirection * 2;

            Vector2 hit = Game1.player.GetBoundingBox().Center.ToVector2() + (ModUtility.DirectionAsVector(axeDirection) * 96f);

            SpellHandle critEffect = new(Game1.player.currentLocation, hit, hit, 256)
            {
                display = IconData.impacts.crit,

                displayRadius = 2,

                scheme = IconData.schemes.herbal_impes,

                sound = SpellHandle.Sounds.boulderBreak,

                explosion = 1,

                power = 2,

                instant = true,

            };

            Mod.instance.spellRegister.Add(critEffect);

            chargeCooldown = 30;

            EventDisplay pickCrit = Mod.instance.CastDisplay(StringData.Strings(StringData.stringkeys.criticalHit));

            pickCrit.type = EventDisplay.displayTypes.quick;

            pickCrit.portrait = Mod.instance.iconData.displayTexture;

            pickCrit.portraitSource = IconData.DisplayRectangle(IconData.displays.pickaxe);

            pickCrit.time = 20;

        }

        public void WatercanCrit()
        {

            if (chargeCooldown > 0)
            {

                return;

            }

            if (!Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.celerity))
            {

                return;

            }

            int chance = Math.Max(2, 6 - Mod.instance.herbalData.buff.applied[HerbalBuff.herbalbuffs.celerity].level);

            if (Mod.instance.randomIndex.Next(chance) != 0)
            {
                return;
            }

            int axeDirection = Game1.player.FacingDirection * 2;

            Vector2 hit = ModUtility.PositionToTile(Game1.player.GetBoundingBox().Center.ToVector2()) + (ModUtility.DirectionAsVector(axeDirection));

            SpellHandle critEffect = new(hit * 64, 192, IconData.impacts.splash, new() { SpellHandle.Effects.douse, })
            {

                displayRadius = 3,

                sound = SpellHandle.Sounds.bubbles,

                explosion = 3,

            };

            Mod.instance.spellRegister.Add(critEffect);

            chargeCooldown = 30;

            EventDisplay canCrit = Mod.instance.CastDisplay(StringData.Strings(StringData.stringkeys.criticalHit));

            canCrit.type = EventDisplay.displayTypes.quick;

            canCrit.portrait = Mod.instance.iconData.displayTexture;

            canCrit.portraitSource = IconData.DisplayRectangle(IconData.displays.watercan);

            canCrit.time = 20;

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

            if (Game1.player.currentLocation.terrainFeatures.ContainsKey(castVector))
            {

                if (Game1.player.currentLocation.terrainFeatures[castVector] is StardewValley.TerrainFeatures.Grass)
                {

                    BuffEffects buffEffect = new();

                    buffEffect.Speed.Set(1);

                    Buff speedBuff = new(
                        buffIdSpeed, 
                        source: StringData.RiteNames(Rite.Rites.weald), 
                        displaySource: StringData.RiteNames(Rite.Rites.weald), 
                        duration: 6000, 
                        displayName: StringData.Strings(StringData.stringkeys.druidFreneticism),
                        description: StringData.Strings(StringData.stringkeys.speedIncrease),
                        effects: buffEffect);

                    Game1.player.buffs.Apply(speedBuff);

                }

            }

            //---------------------------------------------
            // Weed destruction
            //---------------------------------------------

            if (Game1.player.currentLocation.objects.Count() > 0)// && spawnIndex.weeds)
            {

                CastClearance();

            }

            //---------------------------------------------
            // Cultivate / Wilderness
            //---------------------------------------------
            if (castLevel == 0)
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

                        foreach(Vector2 IslandVector in IslandCheck)
                        {

                            if (Game1.player.currentLocation.terrainFeatures.ContainsKey(IslandVector))
                            {

                                if (Game1.player.currentLocation.terrainFeatures[IslandVector] is HoeDirt IslandHoeDirt)
                                {

                                    if(IslandHoeDirt.crop != null)
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

            }

            //---------------------------------------------
            // Rockfall
            //---------------------------------------------

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

        public void CastClearance()
        {

            Cast.Weald.Clearance clearance = new();

            for (int i = 0; i < 5; i++)
            {

                List<Vector2> weedVectors = ModUtility.GetTilesWithinRadius(Game1.player.currentLocation, castVector, i);

                foreach (Vector2 tileVector in weedVectors)
                {

                    if (Game1.player.currentLocation.objects.ContainsKey(tileVector))
                    {

                        StardewValley.Object tileObject = Game1.player.currentLocation.objects[tileVector];

                        if (tileObject.IsBreakableStone())
                        {

                            if (SpawnData.StoneIndex().Contains(tileObject.ParentSheetIndex))
                            {

                                clearance.CastActivate(tileVector);

                                castCost += 1;

                            }
                            else
                            if (tileObject.QualifiedItemId.Contains("CoalNode"))
                            {

                                clearance.CastActivate(tileVector);

                                castCost += 1;

                            }

                        }
                        else if (tileObject.IsTwig() || 
                            tileObject.IsWeeds() || 
                            tileObject.QualifiedItemId == "(O)169" || 
                            tileObject.QualifiedItemId == "(O)590" || 
                            tileObject.QualifiedItemId == "(O)SeedSpot")
                        {

                            clearance.CastActivate(tileVector);

                            castCost += 1;

                        }
                        else if (Game1.player.currentLocation is MineShaft && tileObject is BreakableContainer)
                        {

                            clearance.CastActivate(tileVector);

                        }
                        else if (tileObject.GetContextTags().Contains("category_litter"))
                        {

                            clearance.CastActivate(tileVector);

                            castCost += 1;

                        }

                    }

                    if (Game1.player.currentLocation.terrainFeatures.ContainsKey(tileVector))
                    {

                        if (Game1.player.currentLocation.terrainFeatures[tileVector] is StardewValley.TerrainFeatures.Tree treeFeature)
                        {

                            if (treeFeature.growthStage.Value == 0 && ModUtility.NeighbourCheck(Game1.player.currentLocation, tileVector).Count > 0)
                            {

                                clearance.CastActivate(tileVector, false);

                            }

                        }

                    }

                }

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

            List<StardewValley.Monsters.Monster> victims = ModUtility.MonsterProximity(Game1.player.currentLocation, new List<Vector2>() { cursorVector, }, 384, true);

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

                        if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.spellcatch))
                        {

                            if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.capture))
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

            if (!specialCasts.ContainsKey(Game1.player.currentLocation.Name))
            {

                specialCasts[Game1.player.currentLocation.Name] = new();

            }

            int costing = 4;

            if(Game1.player.currentLocation is not MineShaft)
            {

                costing = 8;

                for (int i = 0; i < 5; i++)
                {

                    costing += i * 4;

                    if (!specialCasts[Game1.player.currentLocation.Name].Contains(eventWilderness + i.ToString()))
                    {

                        break;

                    }

                }

            }

            Wilderness wildernessEvent = new();

            wildernessEvent.EventSetup(Game1.player.Position, eventWilderness);

            wildernessEvent.costing = costing;

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

            cultivateEvent.EventSetup(Game1.player.Position, eventCultivate);

            cultivateEvent.EventActivate();

        }

        public void CastRestoration()
        {

            if (castLevel != 0)
            {

                return;

            }

            Restoration restoreEvent = new();

            restoreEvent.EventSetup(Game1.player.Position, "restoration");

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

                terrain = 2,

                sound = sound
            };

            if (!scene)
            {

                rockSpell.added = new() { SpellHandle.Effects.stone, };

                castCost += 3;

            }

            Mod.instance.spellRegister.Add(rockSpell);

        }

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

            Cursor(IconData.cursors.mists, 75, 512);
            
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

                CastWisps();

            }

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

                                ModUtility.DestroyStump(Game1.player.currentLocation, resourceClump, resourceClump.Tile);
                                break;

                            default:

                                ModUtility.DestroyBoulder(Game1.player.currentLocation, resourceClump, resourceClump.Tile);
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

                fishspotEvent.EventSetup(castVector*64, Rite.eventFishspot);

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

            List<StardewValley.Monsters.Monster> victims = ModUtility.MonsterProximity(Game1.player.currentLocation, new List<Vector2>() { castVector * 64, }, 384, true);

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

                    radius = 192,

                    display = IconData.impacts.boltnode,

                    displayRadius = 4,

                    added = new() { SpellHandle.Effects.push, }// SpellHandle.effects.shock};
                };

                if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.spellcatch))
                {

                    if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.capture))
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

                    radius = 192
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

        public void CastWisps()
        {

            if (castLevel != 0)
            {

                return;

            }

            if (Mod.instance.eventRegister.ContainsKey(eventWisps))
            {

                if (Mod.instance.eventRegister[eventWisps] is Wisps wispEvent)
                {

                    if (wispEvent.eventLocked && Vector2.Distance(wispEvent.origin,Game1.player.Position) <= 960f)
                    {

                        CastWrath();

                    }

                    return;

                }

            }

            Wisps wispNew = new();

            wispNew.EventSetup(Game1.player.Position, eventWisps);

            wispNew.EventActivate();

        }

        public static void CastWrath()
        {

            if (Mod.instance.eventRegister.ContainsKey(eventWrath))
            {

                return;

            }

            Wrath wrathNew = new();

            wrathNew.EventSetup(Game1.player.Position, eventWrath);

            wrathNew.EventActivate();

        }

        public void CastStars()
        {

            if (Mod.instance.questHandle.IsGiven(QuestHandle.starsOne))
            {

                CastMeteors();

            }

            if (Mod.instance.questHandle.IsGiven(QuestHandle.starsTwo))
            {

                CastBlackhole();

            }

            //CastTransform();

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

            bool comet = false;

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

            if (Mod.instance.questHandle.IsComplete(QuestHandle.starsTwo))
            {

                if (Mod.instance.eventRegister.ContainsKey("blackhole"))
                {

                    if (Mod.instance.eventRegister["blackhole"] is Cast.Stars.Blackhole gravityEffect)
                    {

                        if (gravityEffect.blackhole)
                        {
                            
                            if (!gravityEffect.meteor)
                            {

                                gravityEffect.meteor = true;

                                meteorVectors.Add(ModUtility.PositionToTile(gravityEffect.target));

                                comet = true;

                                meteorLimit = 1;

                            }
                            else
                            {

                                return;

                            }

                        }

                    }

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

                    List<StardewValley.Monsters.Monster> monsters = ModUtility.MonsterProximity(Game1.player.currentLocation, new() { meteorVector * 64, }, 224, true);

                    for (int i = monsters.Count - 1; i >= 0; i--)
                    {

                        Mod.instance.questHandle.UpdateTask(QuestHandle.starsOne, 1);

                    }

                }

                int radius = 4 + extra;



                //SpellHandle.Sounds sound = SpellHandle.Sounds.flameSpellHit;

                int scale = radius;

                int terrain = 0;

                Vector2 meteorTarget = meteorVector * 64;

                if (comet)
                {

                    radius = 8;

                    scale = 6;

                    damage *= 4;

                    terrain = 8;

                }

                SpellHandle meteor = new(Game1.player, meteorTarget, radius * 64, damage)
                {
                    
                    type = SpellHandle.Spells.missile,

                    missile = MissileHandle.missiles.meteor

                };

                if (damage > 0)
                {

                    if (comet)
                    {

                        meteor.display = IconData.impacts.bigimpact;

                        meteor.displayRadius = 5;

                    }
                    else
                    {

                        meteor.display = IconData.impacts.dustimpact;

                        meteor.displayRadius = 4;

                    }

                    meteor.indicator = IconData.cursors.stars;

                    meteor.factor = scale;

                    //meteor.sound = sound;

                    meteor.soundTrigger = SoundHandle.SoundCue.CastStars;

                    meteor.soundImpact = SoundHandle.SoundCue.ImpactStars;

                    meteor.explosion = radius;

                    meteor.power = 3;

                    meteor.terrain = terrain;

                }

                switch (ModUtility.GroundCheck(Game1.player.currentLocation, meteorVector))
                {

                    case "water":

                        meteor.display = IconData.impacts.splash;

                        meteor.sound = SpellHandle.Sounds.dropItemInWater;

                        break;

                }

                if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.spellcatch))
                {

                    if (Mod.instance.herbalData.buff.applied.ContainsKey(HerbalBuff.herbalbuffs.capture))
                    {

                        meteor.added.Add(SpellHandle.Effects.capture);

                    }

                }

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

        public void CastBlackhole()
        {

            if (castLevel != 0)
            {

                return;

            }

            if (Mod.instance.eventRegister.ContainsKey("blackhole"))
            {

                if (Mod.instance.eventRegister["blackhole"] is Cast.Stars.Blackhole blackholeEvent)
                {

                    if (!blackholeEvent.eventLocked && Vector2.Distance(blackholeEvent.origin, Game1.player.Position) <= 960f)
                    {

                        return;

                    }

                    if (blackholeEvent.AttemptReset())
                    {

                        return;

                    }

                    blackholeEvent.EventRemove();

                }

                Mod.instance.eventRegister.Remove("blackhole");

            }

            Vector2 blackholeVector = GetTargetCursor(Game1.player.FacingDirection, 384);

            Cast.Stars.Blackhole blackholeNew = new();

            blackholeNew.EventSetup(Game1.player.Position, "blackhole");

            blackholeNew.target = blackholeVector;

            blackholeNew.EventActivate();

        }

        public void CastFates()
        {

            CastWhisk();

            if (Mod.instance.questHandle.IsGiven(QuestHandle.fatesThree))
            {

                CastTricks();

            }

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

            if(Mod.instance.questHandle.IsComplete(QuestHandle.questJester))
            {

                CastWinds();

            }

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

                newWhisk.EventSetup(whiskTiles[i] * 64, "whisk");

                newWhisk.EventActivate();

                //castCost += 12;

                break;

            }

            if (!Mod.instance.questHandle.IsGiven(QuestHandle.fatesTwo))
            {
                
                return;

            }

            List<StardewValley.Monsters.Monster> monsters = ModUtility.MonsterProximity(Game1.player.currentLocation, new() { farVector }, 480, true);

            if (monsters.Count > 0)
            {

                if (!Mod.instance.eventRegister.ContainsKey("whisk"))
                {
                        
                    Whisk newWhisk = new();

                    newWhisk.EventSetup(Game1.player.Position, "whisk");

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

                    curseEffect.AddTarget(Game1.player.currentLocation, monster, ChargeEffect(IconData.cursors.fatesCharge, true));

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

                Mod.instance.iconData.CursorIndicator(location, witness.Position, IconData.cursors.fates, new());

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

                    factor = display
                };

                Mod.instance.spellRegister.Add(trickSpell);

            }

        }

        public void CastEnchant()
        {

            if (castLevel != 0) { return; }

            if (Mod.instance.eventRegister.ContainsKey("enchant"))
            {


                if (Mod.instance.eventRegister["enchant"] is Enchant enchantment)
                {

                    if (!enchantment.eventLocked)
                    {

                        enchantment.eventAbort = true;

                    }

                }

                return;

            }

            Enchant enchantmentEvent = new();

            enchantmentEvent.EventSetup(Game1.player.Position, "enchant");

            enchantmentEvent.giantTiles.Add(ModUtility.PositionToTile(Game1.player.Position) + (ModUtility.DirectionAsVector((Game1.player.FacingDirection * 2)) * 2));

            enchantmentEvent.EventActivate();

        }
        
        public void CastWinds()
        {

            if (castLevel != 0)
            {

                return;

            }

            if (Mod.instance.eventRegister.ContainsKey(eventWinds))
            {

                if (Mod.instance.eventRegister[eventWinds] is Winds)
                {

                    return;

                }

            }

            Winds windsNew = new();

            windsNew.EventSetup(Game1.player.Position, eventWinds);

            windsNew.EventActivate();

            windsNew.damageMonsters = Mod.instance.CombatDamage() / 5;


        }

        public void CastEther()
        {

            if (castLevel == 0)
            {

                CastTransform();

            }

        }

        public static void CastTransform()
        {


            if (Mod.instance.eventRegister.ContainsKey(eventTransform))
            {

                if(Mod.instance.eventRegister[eventTransform] is Cast.Ether.Transform transformEvent)
                {
                    
                    if (transformEvent.AttemptReset())
                    {

                        return;

                    }

                    transformEvent.EventRemove();

                }

                Mod.instance.eventRegister.Remove(eventTransform);

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

            //if (!spawnIndex.anywhere && spawnIndex.locale != Game1.player.currentLocation.Name)
            //{

            //    spawnIndex = new(Game1.player.currentLocation);

            //}

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

            //if (!spawnIndex.crate)
            //{

            //    return;

            //}

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

                treasure.EventSetup(treasureVector * 64, "crate_" + Game1.player.currentLocation.Name, true);

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

        public void CastBones()
        {

            CastMob();

            CastFlock();

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

            if (!returnToPlayer)
            {

                Cursor(IconData.cursors.feathers, 75, 1280);

            }

        }

        public void CastFlock()
        {

            if (castLevel != 0) { return; }

            if (Mod.instance.eventRegister.ContainsKey(eventCorvids))
            {

                if (Mod.instance.eventRegister[eventCorvids] is Cast.Bones.Corvids corvids)
                {

                    if (!corvids.eventLocked)
                    {

                        corvids.eventAbort = true;

                    }

                }

                return;

            }

            Cast.Bones.Corvids corvidsEvent = new();

            corvidsEvent.EventSetup(Game1.player.Position, eventCorvids);

            corvidsEvent.EventActivate();

        }

        public void CastBombs()
        {

            if (Game1.player.CurrentToolIndex == 999)
            {

                return;

            }

            SpellHandle special = new(
                Game1.player.currentLocation, 
                castVector*64, 
                Game1.player.Position - new Vector2(0,32), 
                192, 
                -1, 
                Mod.instance.CombatDamage()
                );

            special.instant = true;

            special.type = SpellHandle.Spells.missile;

            special.missile = MissileHandle.missiles.bomb;

            special.display = IconData.impacts.bomb;

            int consumption = 1;

            switch (Game1.player.CurrentItem.ItemId)
            {

                default:

                    return;

                case "390": // stone

                    special.missile = MissileHandle.missiles.boulder;

                    special.sound = SpellHandle.Sounds.flameSpellHit;

                    special.display = IconData.impacts.flashbang;

                    special.factor = 2;

                    special.displayRadius = 3;

                    special.damageMonsters /= 2;

                    consumption = 5;

                    castCost += 4;

                    break;

                case "286": // cherry bomb

                    special.scheme = IconData.schemes.bomb_one;

                    special.sound = SpellHandle.Sounds.flameSpellHit;

                    special.factor = 2;

                    special.displayRadius = 3;

                    special.power = 3;

                    special.explosion = 3;

                    special.terrain = 3;

                    break;

                case "287": // bomb

                    special.scheme = IconData.schemes.bomb_two;

                    special.sound = SpellHandle.Sounds.explosion;

                    special.display = IconData.impacts.bigimpact;

                    special.factor = 3;

                    special.radius = 320;

                    special.displayRadius = 4;

                    special.power = 3;

                    special.explosion = 5;

                    special.terrain = 5;

                    special.damageMonsters = Mod.instance.CombatDamage() * 2;

                    break;

                case "288": // mega bomb

                    special.scheme = IconData.schemes.bomb_three;

                    special.sound = SpellHandle.Sounds.explosion;

                    special.display = IconData.impacts.bigimpact;

                    special.factor = 4;

                    special.radius = 512;

                    special.displayRadius = 5;

                    special.power = 3;

                    special.explosion = 7;

                    special.terrain = 7;

                    special.damageMonsters = Mod.instance.CombatDamage() * 3;

                    break;

            }

            foreach (KeyValuePair<HerbalBuff.herbalbuffs, HerbalApplied> buff in Mod.instance.herbalData.buff.applied)
            {

                switch (buff.Key)
                {

                    case HerbalBuff.herbalbuffs.imbuement:

                        SpellHandle.Effects imbue = ChargeEffect(chargeType, true);

                        special.added.Add(imbue);

                        break;

                    case HerbalBuff.herbalbuffs.amorous:

                        if (consumption > 1)
                        {

                            break;

                        }

                        special.display = IconData.impacts.lovebomb;

                        special.added.Add(SpellHandle.Effects.glare);

                        special.added.Add(SpellHandle.Effects.charm);

                        break;

                    case HerbalBuff.herbalbuffs.donor:

                        if (Mod.instance.randomIndex.Next(3) == 0)
                        {

                            consumption = 0;

                        }

                        break;

                    case HerbalBuff.herbalbuffs.concussion:

                        if (consumption > 1)
                        {

                            break;

                        }

                        special.power = 4;

                        special.added.Add(SpellHandle.Effects.embers);

                        if (Mod.instance.randomIndex.Next(6) == 0)
                        {

                            special.added.Add(SpellHandle.Effects.bore);

                        }

                        break;

                    case HerbalBuff.herbalbuffs.jumper:

                        special.added.Add(SpellHandle.Effects.jump);

                        break;

                    case HerbalBuff.herbalbuffs.feline:

                        if(special.missile == MissileHandle.missiles.holygrenade)
                        {

                            special.missile = MissileHandle.missiles.holycat;

                        }
                        else
                        {

                            special.missile = MissileHandle.missiles.cat;

                        }

                        break;

                    case HerbalBuff.herbalbuffs.sanctified:

                        //special.display = IconData.impacts.holy;

                        if (special.missile == MissileHandle.missiles.cat)
                        {

                            special.missile = MissileHandle.missiles.holycat;

                        }
                        else
                        {

                            special.missile = MissileHandle.missiles.holygrenade;

                        }

                        special.added.Add(SpellHandle.Effects.holy);

                        if(special.factor > 3)
                        {

                            special.display = IconData.impacts.holyimpact;

                        }
                        else
                        {

                            special.display = IconData.impacts.holy;

                        }

                        break;

                    case HerbalBuff.herbalbuffs.capture:

                        if (Mod.instance.magic)
                        {

                            break;

                        }

                        special.monsters = ModUtility.MonsterProximity(Game1.player.currentLocation, new() { special.impact }, special.radius + 32, true);

                        special.added.Add(SpellHandle.Effects.capture);

                        break;

                    case HerbalBuff.herbalbuffs.rapidfire:

                        if (Mod.instance.eventRegister.ContainsKey(eventRapidfire))
                        {

                            if(Mod.instance.eventRegister[eventRapidfire] is Rapidfire rapidFire)
                            {
   
                                if (!rapidFire.Discharge(special.factor))
                                {

                                    return;

                                }

                            }

                        }
                        else
                        {

                            Rapidfire rapidFire = new Rapidfire();

                            rapidFire.EventSetup(Game1.player.Position,eventRapidfire);

                            rapidFire.EventActivate();

                            rapidFire.Discharge(special.factor);

                        }

                        castTimer = 12;

                        break;

                }

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

        public void RiteBuff()
        {

            int toolIndex = Mod.instance.AttuneableWeapon();

            Rites blessing = RequirementCheck(Mod.instance.save.rite,true);

            if (toolIndex == -1)
            {

                if (Game1.player.buffs.IsApplied(buffIdRite))
                {

                    Game1.player.buffs.Remove(buffIdRite);

                }

                return;

            }
            else if (toolIndex == Transform.toolPlaceholder && Mod.instance.eventRegister.ContainsKey(eventTransform))
            {

                blessing = Rites.ether;

            }

            if (toolIndex == 997)
            {

                blessing = Rites.sworn;

            }
            else
            if (Mod.instance.magic)
            {

                blessing = GetSlotBlessing();

            }
            else
            if (Mod.instance.save.milestone < QuestHandle.milestones.sworn_weapon)
            {

                blessing = Rites.none;

            }
            else if (Mod.instance.Config.slotAttune)
            {

                blessing = GetSlotBlessing();

            }
            else
            {

                Dictionary<int, Rites> attunement = SpawnData.WeaponAttunement();

                if (attunement.ContainsKey(toolIndex))
                {

                    blessing = RequirementCheck(attunement[toolIndex]);

                }

            }

            if(appliedBuff == blessing)
            {

                if (Game1.player.buffs.IsApplied(buffIdRite))
                {
                    return;

                }

            }

            appliedBuff = blessing;

            int buffIndex = (int)Enum.Parse<IconData.displays>(blessing.ToString()) - 1;

            if (blessing == Rites.none)
            {

                buffIndex = (int)IconData.displays.chaos - 1;

            }

            Buff riteBuff = new(
                buffIdRite, 
                source: StringData.Strings(StringData.stringkeys.stardewDruid), 
                displaySource: BuffSource(),
                duration: Buff.ENDLESS, 
                iconTexture:Mod.instance.iconData.displayTexture, 
                iconSheetIndex: buffIndex, 
                displayName: StringData.RiteNames(blessing), 
                description: StringData.Strings(StringData.stringkeys.riteBuffDescription)
                );

            Game1.player.buffs.Apply(riteBuff);

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

            if (chargeType == IconData.cursors.none || !chargeActive)
            {

                if (Game1.player.buffs.IsApplied(buffIdCharge))
                {

                    Game1.player.buffs.Remove(buffIdCharge);

                }

                selectedCharge = IconData.cursors.none;

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

            IconData.displays cursorIndex = IconData.displays.chargeweald;

            string chargeBuffName = StringData.Strings(StringData.stringkeys.chargeWealdName);
           
            string chargeBuffDescription = StringData.Strings(StringData.stringkeys.chargeWealdDescription);

            switch (chargeType)
            {

                case IconData.cursors.mistsCharge:

                    cursorIndex = IconData.displays.chargemists;
                    chargeBuffName = StringData.Strings(StringData.stringkeys.chargeMistsName);
                    chargeBuffDescription = StringData.Strings(StringData.stringkeys.chargeMistsDescription);

                    break;

                case IconData.cursors.starsCharge:

                    cursorIndex = IconData.displays.chargestars;
                    chargeBuffName = StringData.Strings(StringData.stringkeys.chargeStarsName);
                    chargeBuffDescription = StringData.Strings(StringData.stringkeys.chargeStarsDescription);

                    break;

                case IconData.cursors.fatesCharge:

                    cursorIndex = IconData.displays.chargefates;
                    chargeBuffName = StringData.Strings(StringData.stringkeys.chargeFatesName);
                    chargeBuffDescription = StringData.Strings(StringData.stringkeys.chargeFatesDescription);

                    break;

                case IconData.cursors.bonesCharge:

                    cursorIndex = IconData.displays.chargebones;
                    chargeBuffName = StringData.Strings(StringData.stringkeys.chargeBonesName);
                    chargeBuffDescription = StringData.Strings(StringData.stringkeys.chargeBonesDescription);

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