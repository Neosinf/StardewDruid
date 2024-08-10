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
using StardewDruid.Event;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Locations;
using StardewValley.Objects;
using StardewValley.TerrainFeatures;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using xTile.Layers;
using static StardewValley.Minigames.CraneGame;

namespace StardewDruid.Cast
{

    public class Rite
    {
        // -----------------------------------------------------

        public enum rites
        {
            none,

            weald,
            mists,
            stars,
            fates,
            ether,
            bones,

        }

        public rites castType;

        public Dictionary<rites, Journal.QuestHandle.milestones> requirement = new()
        {
            [rites.weald] = QuestHandle.milestones.weald_weapon,
            [rites.mists] = QuestHandle.milestones.mists_weapon,
            [rites.stars] = QuestHandle.milestones.stars_weapon,
            [rites.fates] = QuestHandle.milestones.fates_weapon,
            [rites.ether] = QuestHandle.milestones.ether_weapon,
            [rites.bones] = QuestHandle.milestones.ether_challenge,

        };

        public int castLevel;

        public int castCost;

        public Vector2 castVector;

        public int castTool;

        public string castLocation;

        public SpawnIndex spawnIndex = new();

        public int castInterval;

        public int castTimer;

        public bool castActive;

        public List<Vector2> vectorList = new();

        public Dictionary<string, List<string>> specialCasts = new();

        public Dictionary<string, Dictionary<Vector2, string>> targetCasts = new();

        public Dictionary<string, Dictionary<Vector2, int>> terrainCasts = new();

        public rites appliedBuff;

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

        public enum charges
        {
            none,

            wealdCharge,
            mistsCharge,
            starsCharge,
            fatesCharge,

            //divineCharge,

        }

        public Dictionary<rites, charges> riteCharges = new()
        {
            [rites.weald] = charges.wealdCharge,
            [rites.mists] = charges.mistsCharge,
            [rites.stars] = charges.starsCharge,
            [rites.fates] = charges.fatesCharge,

        };

        public Dictionary<charges, IconData.cursors> chargeCursors = new()
        {
            [charges.wealdCharge] = IconData.cursors.wealdCharge,
            [charges.mistsCharge] = IconData.cursors.mistsCharge,
            [charges.starsCharge] = IconData.cursors.starsCharge,
            [charges.fatesCharge] = IconData.cursors.fatesCharge,
            
            //[charges.divineCharge] = IconData.cursors.divineCharge,

        };

        public Dictionary<rites, string> chargeRequirement = new()
        {
            [rites.weald] = QuestHandle.wealdFour,
            [rites.mists] = QuestHandle.mistsFour,
            [rites.stars] = QuestHandle.starsOne,
            [rites.fates] = QuestHandle.fatesTwo,
        };

        public charges chargeType;

        public int chargeTimer;

        public int chargeCooldown;

        public bool chargeActive;

        public StardewValley.GameLocation chargeLocation;

        // ----------------------------------------------------

        public Rite()
        {

        }

        public void shutdown()
        {

            castActive = false;

        }

        public void draw(SpriteBatch b)
        {

            if (!castActive)
            {

                return;

            }

            if (castType == rites.ether)
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
                    IconData.CursorRectangle(chargeCursors[chargeType]),
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

        public void click(EventHandle.actionButtons button)
        {

            switch (button)
            {
                case EventHandle.actionButtons.action:

                    dispense();

                    break;

                case EventHandle.actionButtons.special:

                    charge();

                    break;

                case EventHandle.actionButtons.rite:

                    start();

                    /*Mod.instance.Monitor.Log(ModUtility.PositionToTile(Game1.player.Position).ToString(), LogLevel.Debug);

                    Mod.instance.Monitor.Log(Game1.player.currentLocation.Name, LogLevel.Debug);

                    for (int i = 0; i < Game1.player.currentLocation.map.Layers.Count; i++)
                    {

                        Layer layer = Game1.player.currentLocation.map.Layers.ElementAt(i);

                        Mod.instance.Monitor.Log(layer.Id, LogLevel.Debug);

                        for (int y = 0; y < layer.LayerHeight; y++)
                        {

                            string debug = "";

                            for(int x = 0; x < layer.LayerWidth; x++)
                            {

                                if(layer.Tiles[x, y] != null)
                                {

                                    debug += layer.Tiles[x, y].TileIndex.ToString();


                                }

                                debug += ",";

                            }

                            Mod.instance.Monitor.Log(debug,LogLevel.Debug);

                        }

                    }*/

                    break;

            }

        }

        public void reset()
        {

            castInterval = 60;

            int castFast = 4;

            if (castType == rites.stars)
            {

                castInterval = 45;

                castFast = 3;

            }

            if (castType == rites.fates)
            {

                castInterval = 120;

                castFast = 8;

            }

            if (castType == rites.bones)
            {

                castInterval = 120;

                castFast = 8;

            }

            if (Mod.instance.herbalData.applied.ContainsKey(HerbalData.herbals.celeri))
            {

                castInterval -= (int)(castFast * Mod.instance.herbalData.applied[HerbalData.herbals.celeri].level);

            }

            vectorList = new();

            castLevel = 0;

            castLocation = Game1.player.currentLocation.Name;

            CursorShutdown();

        }

        public bool start()
        {

            rites blessing;

            int tool = Mod.instance.AttuneableWeapon();

            if (Mod.instance.Config.slotAttune || Mod.instance.magic)
            {

                blessing = GetSlotBlessing();

                if (blessing == rites.none)
                {

                    if (Mod.instance.CheckTrigger())
                    {

                        return false;

                    }

                    if (Mod.instance.save.milestone == Journal.QuestHandle.milestones.none)
                    {

                        Mod.instance.CastMessage(Mod.instance.Config.journalButtons.ToString() + " " + DialogueData.Strings(DialogueData.stringkeys.openJournal));

                    }
                    else
                    {

                        Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.noRiteAttuned) + " " + (Game1.player.CurrentToolIndex + 1));

                    }

                    return false;

                }

            }
            else
            {

                if (tool == -1)
                {

                    Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.riteTool));

                    return false;

                }

                if (Mod.instance.Attunement.ContainsKey(tool))
                {

                    blessing = RequirementCheck(Mod.instance.Attunement[tool]);

                    if (blessing == rites.none)
                    {

                        if (Mod.instance.CheckTrigger())
                        {

                            return false;

                        }

                        Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.noToolAttunement));

                        return false;

                    }

                }
                else
                {

                    blessing = Mod.instance.save.rite;

                }

            }

            if(blessing != castType)
            {

                shutdown();

            }

            castType = blessing;

            castTool = tool;

            GetLocation();

            CastVector();

            reset();

            castActive = true;

            return true;

        }

        public void update()
        {

            castTimer--;

            ChargeUpdate();

            ChannelUpdate();

            CursorUpdate();

            if (!castActive)
            {

                return;

            }

            if (castTimer <= 0)
            {

                if (!(castLevel == 0 || Mod.instance.Config.riteButtons.GetState() == SButtonState.Held))
                {

                    shutdown();

                    return;

                }

                if(Game1.player.currentLocation.Name != castLocation)
                {

                    GetLocation();

                }

                if (Mod.instance.Config.slotAttune || Mod.instance.magic)
                {

                    rites slot = GetSlotBlessing();

                    if (castType != slot)
                    {

                        if (!start())
                        {

                            shutdown();

                            return;

                        }

                    }

                }
                else
                {

                    int toolIndex = Mod.instance.AttuneableWeapon();

                    if (castTool != toolIndex)
                    {

                        if (!start())
                        {

                            shutdown();

                            return;

                        }

                    }

                }

                if (castLevel == 0)
                {

                    if (Mod.instance.CheckTrigger())
                    {

                        shutdown();

                        return;

                    }

                }

                if (castType == rites.none)
                {

                    if (Mod.instance.save.milestone == Journal.QuestHandle.milestones.none)
                    {

                        Mod.instance.CastMessage(Mod.instance.Config.journalButtons.ToString() + " " + DialogueData.Strings(DialogueData.stringkeys.openJournal));
                    }
                    else
                    {

                        Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.nothingHappened));
                    }

                    shutdown();

                    return;

                }

                if (!spawnIndex.cast && Mod.instance.activeEvent.Count == 0)
                {

                    Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.invalidLocation));

                    shutdown();

                    return;

                }

                if (castType != rites.ether && Game1.player.Stamina <= (Game1.player.MaxStamina / 4) || Game1.player.health <= (Game1.player.maxHealth / 3))
                {

                    Mod.instance.AutoConsume();

                    if (Game1.player.Stamina <= 16)
                    {

                        if (castLevel > 0)
                        {
                            Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.energyContinue), 3);

                        }
                        else
                        {
                            Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.energyRite), 3);

                        }

                        shutdown();

                        return;

                    }

                }

                CastVector();

                cast();

                castTimer = castInterval;

                castLevel++;

            }

        }

        public void charge()
        {

            if (!castActive) { return; }

            if (castType == rites.ether) { return; }

            if (castType == rites.bones) { return; }

            if (!Mod.instance.questHandle.IsGiven(chargeRequirement[castType]))
            {

                return;

            }

            if (chargeActive)
            {

                if (chargeType != riteCharges[castType])
                {

                    ChargeShutdown();

                }

            }

            ChargeSet(riteCharges[castType]);

            return;


        }

        public void dispense(int radius = 96)
        {

            if (!chargeActive) { return; }

            if (Mod.instance.AttuneableWeapon() == -1) { return; }

            if (chargeCooldown > 0) { return; }

            chargeTimer += 900;

            List<StardewValley.Monsters.Monster> checkMonsters;

            switch (chargeType)
            {

                case charges.fatesCharge:

                    checkMonsters = GetMonstersAround(Game1.player.FacingDirection, radius, radius);

                    if (checkMonsters.Count > 0)
                    {

                        if (!Mod.instance.questHandle.IsComplete(QuestHandle.fatesTwo))
                        {

                            Mod.instance.questHandle.UpdateTask(QuestHandle.fatesTwo, 1);

                        }

                        SpellHandle knockeffect = new(Game1.player, checkMonsters, 0);

                        knockeffect.type = SpellHandle.spells.explode;

                        knockeffect.added.Add(ChargeEffect(charges.fatesCharge));
                                                
                        knockeffect.added.Add(SpellHandle.effects.backstab);

                        knockeffect.monsters = checkMonsters;

                        knockeffect.display = IconData.impacts.puff;

                        knockeffect.scheme = IconData.schemes.fates;

                        knockeffect.instant = true;

                        knockeffect.local = true;

                        Mod.instance.spellRegister.Add(knockeffect);

                    }

                    break;

                case charges.starsCharge:

                    checkMonsters = GetMonstersAround(Game1.player.FacingDirection, radius, radius);

                    if (checkMonsters.Count > 0)
                    {

                        int impes = 1;

                        if (Mod.instance.herbalData.applied.ContainsKey(HerbalData.herbals.impes))
                        {

                            impes = Mod.instance.herbalData.applied[HerbalData.herbals.impes].level;

                        }

                        int burst = Mod.instance.PowerLevel * 5 * impes;

                        SpellHandle knockeffect = new(Game1.player, checkMonsters, burst);

                        knockeffect.type = SpellHandle.spells.explode;

                        knockeffect.added.Add(ChargeEffect(charges.starsCharge));

                        knockeffect.added.Add(SpellHandle.effects.backstab);

                        knockeffect.monsters = checkMonsters;

                        knockeffect.display = IconData.impacts.flashbang;

                        knockeffect.instant = true;

                        knockeffect.local = true;

                        Mod.instance.spellRegister.Add(knockeffect);

                    }

                    break;

                case charges.mistsCharge:

                    checkMonsters = GetMonstersAround(Game1.player.FacingDirection, radius, radius);

                    if (checkMonsters.Count > 0)
                    {

                        SpellHandle draineffect = new(Game1.player, checkMonsters, Mod.instance.CombatDamage());

                        draineffect.type = SpellHandle.spells.effect;

                        draineffect.added.Add(ChargeEffect(charges.mistsCharge));

                        draineffect.added.Add(SpellHandle.effects.backstab);

                        draineffect.display = IconData.impacts.mists;

                        draineffect.local = true;

                        Mod.instance.spellRegister.Add(draineffect);

                    }

                    break;

                default: // weald

                    checkMonsters = GetMonstersAround(Game1.player.FacingDirection, radius, radius);

                    if (checkMonsters.Count > 0)
                    {

                        SpellHandle sapeffect = new(Game1.player, checkMonsters, 0);

                        sapeffect.type = SpellHandle.spells.effect;

                        sapeffect.added.Add(ChargeEffect(charges.wealdCharge));

                        sapeffect.added.Add(SpellHandle.effects.backstab);

                        sapeffect.scheme = IconData.schemes.weald;

                        //sapeffect.display = IconData.impacts.glare;

                        sapeffect.radius = 192;

                        sapeffect.local = true;

                        Mod.instance.spellRegister.Add(sapeffect);

                    }

                    break;

            }

        }

        public void channel(IconData.skies sky, int timer)
        {

            channelling = sky;

            channelLimit = timer;

            channelTimer = timer;

        }

        public void cursor(IconData.cursors cursor, int timer, int distance = -1, int x = 0, int y = 0)
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

        public SpellHandle.effects ChargeEffect(charges charge)
        {

            float celeri = 1f;

            if (Mod.instance.herbalData.applied.ContainsKey(HerbalData.herbals.celeri))
            {

                celeri -= 0.1f * (float)Mod.instance.herbalData.applied[HerbalData.herbals.celeri].level;

            }

            if(chargeCooldown > 0)
            {

                return SpellHandle.effects.none;

            }

            switch (charge)
            {
                case charges.fatesCharge:

                    chargeCooldown = (int)(180f * celeri);

                    //switch (Mod.instance.randomIndex.Next(4))
                    switch (Mod.instance.randomIndex.Next(3))
                    {
                        
                        case 0:

                            return SpellHandle.effects.daze;

                        //case 1:

                        //return SpellHandle.effects.mug;

                        case 2:

                            return SpellHandle.effects.morph;

                        default:
                        //case 3:

                            return SpellHandle.effects.doom;
                    }

                case charges.starsCharge:

                    chargeCooldown = (int)(120f * celeri);

                    return SpellHandle.effects.knock;

                case charges.mistsCharge:

                    chargeCooldown = (int)(60f * celeri);

                    return SpellHandle.effects.drain;

                default:
                case charges.wealdCharge:

                    chargeCooldown = (int)(30f * celeri);

                    return SpellHandle.effects.sap;

            }

        }

        public void ChargeUpdate()
        {

            if (!chargeActive)
            {

                return;

            }

            if (chargeCooldown > 0)
            {

                chargeCooldown--;

            }

            if (chargeTimer > 0)
            {

                chargeTimer--;

            }

            if (chargeTimer <= 0)
            {

                ChargeShutdown();

                return;

            }

        }

        public void ChargeShutdown()
        {

            chargeActive = false;

        }

        public void ChargeSet(charges type)
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

        public rites RequirementCheck(rites id, bool next = false)
        {

            if (Mod.instance.magic)
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

                    id = (rites)((int)id - 1);

                    if ((int)Mod.instance.save.milestone >= (int)requirement[id])
                    {

                        return id;

                    }

                }

            }

            return rites.none;

        }

        public rites GetSlotBlessing()
        {

            int num = Game1.player.CurrentToolIndex;

            if (Game1.player.CurrentToolIndex == 999 && Mod.instance.eventRegister.ContainsKey("transform"))
            {
                num = (Mod.instance.eventRegister["transform"] as Transform).toolIndex;

            }

            int real = num % 12;

            rites blessing = rites.none;

            Dictionary<int, string> slots= SlotNormal();

            switch (slots[real])
            {

                case "weald":

                    blessing = RequirementCheck(rites.weald);

                    break;

                case "mists":

                    blessing = RequirementCheck(rites.mists, true);

                    break;

                case "stars":

                    blessing = RequirementCheck(rites.stars, true);

                    break;

                case "fates":

                    blessing = RequirementCheck(rites.fates, true);

                    break;

                case "ether":

                    blessing = RequirementCheck(rites.ether, true);

                    break;

                case "bones":

                    blessing = RequirementCheck(rites.bones, true);

                    break;

            }

            return blessing;

        }

        public Dictionary<int,string> SlotNormal()
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

        public void GetLocation()
        {
            
            castLocation = Game1.player.currentLocation.Name;

            spawnIndex = new SpawnIndex(Game1.player.currentLocation);

            if (!spawnIndex.cast && Mod.instance.eventRegister.ContainsKey("active"))
            {

                spawnIndex.cast = true;

            }

        }

        public void CastVector()
        {

            switch (castType)
            {

                case rites.mists:
                case rites.fates:

                    Vector2 cursorVector = GetTargetCursor(Game1.player.FacingDirection, 320);

                    castVector = ModUtility.PositionToTile(cursorVector);

                    break;  
                    
               case rites.bones:

                    castVector = ModUtility.PositionToTile(GetTargetCursor(Game1.player.FacingDirection, 1280, -1));

                    break;

                default: // earth / stars / ether

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
            Vector2 vector = Game1.player.Position;

            Dictionary<int, Vector2> vectorIndex = new()
            {

                [0] = vector + new Vector2(0, -distance),// up
                [1] = vector + new Vector2(distance, 0), // right
                [2] = vector + new Vector2(0, distance),// down
                [3] = vector + new Vector2(-distance, 0), // left

            };

            return vectorIndex[direction];

        }

        public void cast()
        {

            castCost = 0;

            switch (castType)
            {

                case rites.stars:

                    CastStars();

                    break;

                case rites.mists:

                    CastMists();

                    break;

                case rites.fates:

                    CastFates();

                    break;

                case rites.ether:

                    CastEther();

                    break;

                case rites.bones:

                    CastBones();

                    break;

                default:

                    CastWeald();

                    break;
            }

            ApplyCost();

        }

        public void ApplyCost()
        {

            float oldStamina = Game1.player.Stamina;

            float totalCost = castCost * 0.25f * (float)Mod.instance.ModDifficulty();

            float staminaCost = Math.Min(castCost, oldStamina - 16);

            if (staminaCost > 0)
            {

                Game1.player.Stamina -= staminaCost;

            }

            Game1.player.checkForExhaustion(oldStamina);

            castCost = 0;

        }

        public void CastWeald()
        {


            //---------------------------------------------
            // Weald Sound
            //---------------------------------------------

            if (castLevel % 6 == 0)
            {
                
                Game1.player.currentLocation.playSound("discoverMineral");

            }

            castVector = ModUtility.PositionToTile(Game1.player.Position);

            if (Game1.player.currentLocation.terrainFeatures.ContainsKey(castVector))
            {

                if (Game1.player.currentLocation.terrainFeatures[castVector] is StardewValley.TerrainFeatures.Grass)
                {

                    BuffEffects buffEffect = new();

                    buffEffect.Speed.Set(1);

                    Buff speedBuff = new(
                        "184653", 
                        source: DialogueData.RiteNames(Rite.rites.weald), 
                        displaySource: DialogueData.RiteNames(Rite.rites.weald), 
                        duration: 6000, 
                        displayName: DialogueData.Strings(DialogueData.stringkeys.druidFreneticism),
                        description: DialogueData.Strings(DialogueData.stringkeys.speedIncrease),
                        effects: buffEffect);

                    Game1.player.buffs.Apply(speedBuff);

                }

            }

            //---------------------------------------------
            // Weed destruction
            //---------------------------------------------

            if (Game1.player.currentLocation.objects.Count() > 0 && spawnIndex.weeds)
            {

                CastClearance();

            }

            //---------------------------------------------
            // Rockfall
            //---------------------------------------------

            if (Game1.player.currentLocation is MineShaft || 
                Game1.player.currentLocation is VolcanoDungeon ||
                Game1.player.currentLocation is Vault ||
                Game1.player.currentLocation is Tomb ||
                Game1.player.currentLocation is Spring
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

                CastBounty();

            }

            //---------------------------------------------
            // Cultivate / Wilderness
            //---------------------------------------------

            if (Mod.instance.questHandle.IsGiven(QuestHandle.wealdThree))
            {

                CastWilderness();

            }

            if (Mod.instance.questHandle.IsGiven(QuestHandle.wealdFour))
            {

                CastCultivate();

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

                            }

                        }
                        else if (tileObject.IsTwig() || 
                            tileObject.IsWeeds() || 
                            tileObject.QualifiedItemId == "(O)169" || 
                            tileObject.QualifiedItemId == "(O)590" || 
                            tileObject.QualifiedItemId == "(O)SeedSpot")
                        {

                            clearance.CastActivate(tileVector);

                        }
                        else if (Game1.player.currentLocation is MineShaft && tileObject is BreakableContainer)
                        {

                            clearance.CastActivate(tileVector);

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

        public void CastGlimmer()
        {

            Glimmer glimmer = new();

            glimmer.CastActivate();

        }

        public void CastBounty()
        {

            // ---------------------------------------------
            // Random Effect Center Selection
            // ---------------------------------------------

            List<Vector2> centerVectors = new();

            if (!terrainCasts.ContainsKey(Game1.player.currentLocation.Name))
            {

                terrainCasts[Game1.player.currentLocation.Name] = new();

            }

            Vector2 playerTile = ModUtility.PositionToTile(Game1.player.Position);

            Vector2 sqtVector = new((int)(playerTile.X - (playerTile.X % 16)), (int)(playerTile.Y -(playerTile.Y % 16)));
            
            if (terrainCasts[Game1.player.currentLocation.Name].ContainsKey(sqtVector))
            {
                
                for(int i = 0; i < 4; i++)
                {
                    
                    Mod.instance.iconData.ImpactIndicator(
                        Game1.player.currentLocation,
                        (sqtVector + new Vector2((i % 2) * 4,i == 0 ? 0 : (i / 2) * 4) + new Vector2(Mod.instance.randomIndex.Next(8), Mod.instance.randomIndex.Next(8))) * 64,
                        IconData.impacts.glare,
                        0.8f + (Mod.instance.randomIndex.Next(5) * 0.2f),
                        new() { alpha = 0.35f }
                    );

                }

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

            if (spawnIndex.wilderness)
            {

                if (Mod.instance.eventRegister.ContainsKey("wilderness"))
                {

                    return;

                }

                if (!specialCasts.ContainsKey(Game1.player.currentLocation.Name))
                {

                    specialCasts[Game1.player.currentLocation.Name] = new();

                }

                int costing = 8;

                for (int i = 0; i < 5; i++)
                {

                    costing += i * 4;

                    if (!specialCasts[Game1.player.currentLocation.Name].Contains("wilderness" + i.ToString()))
                    {

                        break;

                    }

                }

                Wilderness wildernessEvent = new();

                wildernessEvent.EventSetup(Game1.player.Position, "wilderness");

                wildernessEvent.costing = costing;

                wildernessEvent.EventActivate();

            }

        }

        public void CastCultivate()
        {

            if (castLevel != 0)
            {

                return;

            }

            if (spawnIndex.cultivate)
            {

                if (Mod.instance.eventRegister.ContainsKey("cultivate"))
                {

                    return;

                }

                Cultivate cultivateEvent = new();

                cultivateEvent.EventSetup(Game1.player.Position, "cultivate");

                cultivateEvent.EventActivate();

            }

        }

        public void CastRockfall(bool scene = false)
        {

            Vector2 rockVector = Vector2.Zero;

            IconData.impacts display = IconData.impacts.impact;

            SpellHandle.sounds sound = SpellHandle.sounds.flameSpellHit;

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

                            sound = SpellHandle.sounds.dropItemInWater;

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

            SpellHandle rockSpell = new(Game1.player, rockVector * 64, 192, damage);

            rockSpell.display = display;

            rockSpell.type = SpellHandle.spells.orbital;

            rockSpell.missile = IconData.missiles.rockfall;

            rockSpell.projectile = 2;

            rockSpell.terrain = 2;

            rockSpell.sound = sound;

            if (!scene)
            {

                rockSpell.added = new() { SpellHandle.effects.stone, };

                castCost += 3;

            }

            if (chargeActive && chargeCooldown == 0)
            {

                rockSpell.added.Add(ChargeEffect(chargeType));

            }

            Mod.instance.spellRegister.Add(rockSpell);

        }

        public void CastMists()
        {

            //---------------------------------------------
            // Mists
            //---------------------------------------------

            if (castLevel % 6 == 0)
            {

                Game1.player.currentLocation.playSound("thunder_small");

            }

            cursor(IconData.cursors.mists, 75, 320);

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

                        if (Mod.instance.questHandle.IsComplete(QuestHandle.mistsFour))
                        {

                            resourceClump.destroy(Mod.instance.virtualAxe, Game1.player.currentLocation, resourceClump.Tile);
                        
                        }

                        Mod.instance.spellRegister.Add(new(resourceClump.Tile * 64 + new Vector2(32), 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt, display = IconData.impacts.puff, });

                        resourceClump.destroy(Mod.instance.virtualAxe, Game1.player.currentLocation, resourceClump.Tile);

                        resourceClump.health.Set(1f);

                        resourceClump.performToolAction(Mod.instance.virtualPick, 1, resourceClump.Tile);

                        resourceClump.NeedsUpdate = false;

                        if(resourceClump.parentSheetIndex.Value == ResourceClump.stumpIndex || resourceClump.parentSheetIndex.Value == ResourceClump.hollowLogIndex)
                        {
                            Game1.createMultipleObjectDebris("(O)388", (int)resourceClump.Tile.X, (int)resourceClump.Tile.Y, 20);
                        }

                        Game1.player.currentLocation._activeTerrainFeatures.Remove(resourceClump);

                        Game1.player.currentLocation.resourceClumps.Remove(resourceClump);

                        castCost += cost;

                        sundered++;

                    }

                }

            }

            if (Game1.player.currentLocation.terrainFeatures.ContainsKey(castVector))
            {

                if(Game1.player.currentLocation.terrainFeatures[castVector] is Tree tree)
                {

                    if (tree.stump.Value)
                    {

                        Mod.instance.spellRegister.Add(new(castVector * 64 + new Vector2(32), 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt, display = IconData.impacts.puff, });

                        tree.performToolAction(Mod.instance.virtualAxe, 0, castVector);

                        Game1.player.currentLocation.terrainFeatures.Remove(castVector);

                        sundered++;

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

            //if (progressLevel >= 11 && chargeLevel == 1)
            if((castLevel % 4) != 0)
            {
            
                return; 
            
            }

            if (spawnIndex.fishspot)
            {

                if (ModUtility.WaterCheck(Game1.player.currentLocation, castVector))
                {

                    int tryCost = 32 - Game1.player.FishingLevel * 3;

                    castCost += tryCost < 8 ? 8 : tryCost;

                    Fishspot fishspotEvent = new();

                    fishspotEvent.EventSetup(castVector*64, "fishspot");

                    fishspotEvent.EventActivate();
                }

            }

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
                    Mod.instance.spellRegister.Add(new(castVector * 64 + new Vector2(32), 128, IconData.impacts.puff, new()) { type = SpellHandle.spells.bolt });

                }

            }

        }

        public void CastSmite()
        {

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

                SpellHandle bolt = new(Game1.player, new(){victims.First(),}, damage);

                bolt.type = SpellHandle.spells.bolt;

                bolt.critical = crits[0];

                bolt.criticalModifier = crits[1];

                if (chargeActive && chargeCooldown == 0)
                {

                    bolt.added = new() { ChargeEffect(chargeType), SpellHandle.effects.shock, };

                }
                else
                {

                    bolt.added = new() { SpellHandle.effects.push, SpellHandle.effects.shock, };
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

                    continue;

                }

                if (Vector2.Distance(witness.Position, castVector*64) >= threshold)
                {

                    continue;

                }

                Microsoft.Xna.Framework.Rectangle box = witness.GetBoundingBox();

                SpellHandle bolt = new(new(box.Center.X, box.Top), 192, IconData.impacts.deathbomb, new());

                bolt.type = SpellHandle.spells.bolt;

                bolt.display = IconData.impacts.puff;

                Mod.instance.spellRegister.Add(bolt);

                witness.faceTowardFarmerForPeriod(3000, 4, false, Game1.player);

                Game1.player.changeFriendship(-10, witness);

                ReactionData.ReactTo(witness, ReactionData.reactions.mists, -10);

            }

        }

        public void CastWisps()
        {

            if (castLevel != 0)
            {

                return;

            }

            if (Mod.instance.eventRegister.ContainsKey("wisps"))
            {

                if (Mod.instance.eventRegister["wisps"] is Wisps wispEvent)
                {

                    if (wispEvent.eventLocked && Vector2.Distance(wispEvent.origin,Game1.player.Position) <= 960f)
                    {

                        return;

                    }

                    if (wispEvent.AttemptReset())
                    {

                        return;

                    }

                    wispEvent.EventRemove();

                }

            }

            Wisps wispNew = new();

            wispNew.EventSetup(Game1.player.Position, "wisps");

            wispNew.EventActivate();

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

            if (!Game1.player.currentLocation.IsOutdoors)
            {

                if(
                    Game1.player.currentLocation is not MineShaft 
                    && Game1.player.currentLocation is not VolcanoDungeon 
                    && Game1.player.currentLocation is not DruidLocation
                )
                {

                    return;

                }

            }

            if(Game1.player.currentLocation.IsFarm && Game1.player.CurrentTool is not MeleeWeapon)
            {

                return;

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

            float damage = Mod.instance.CombatDamage() * 1.2f;

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

                int radius = 3 + extra;

                SpellHandle.sounds sound = SpellHandle.sounds.flameSpellHit;

                int scale = radius;

                int terrain = 0;

                Vector2 meteorTarget = meteorVector * 64;

                if (comet)
                {

                    radius = 8;

                    scale = 5;

                    sound = SpellHandle.sounds.explosion;

                    damage *= 4;

                    terrain = 8;

                }

                SpellHandle meteor = new(Game1.player, meteorTarget, radius * 64, damage);

                meteor.type = SpellHandle.spells.orbital;

                meteor.missile = IconData.missiles.meteor;

                if (damage > 0)
                {

                    meteor.indicator = IconData.cursors.stars;

                    meteor.display = IconData.impacts.bomb;

                    meteor.projectile = scale;

                    meteor.sound = sound;

                    meteor.explosion = radius;

                    meteor.power = 3;

                    meteor.terrain = terrain;

                }

                switch (ModUtility.GroundCheck(Game1.player.currentLocation, meteorVector))
                {

                    case "water":

                        meteor.display = IconData.impacts.splash;

                        meteor.sound = SpellHandle.sounds.dropItemInWater;

                        break;

                }

                if (chargeActive && chargeCooldown == 0)
                {

                    meteor.added = new() { ChargeEffect(chargeType) };

                }

                Mod.instance.spellRegister.Add(meteor);

                int tryCost = 12 - Game1.player.CombatLevel;

                castCost += tryCost < 5 ? 5 : tryCost;

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

            if (Mod.instance.questHandle.IsGiven(Journal.QuestHandle.fatesThree))
            {

                CastTricks();

            }

            if (Mod.instance.questHandle.IsGiven(Journal.QuestHandle.fatesFour))
            {

                CastEnchant();

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

                castCost += 12;

                break;

            }

            if (Mod.instance.questHandle.IsGiven(QuestHandle.fatesTwo))
            {

                List<StardewValley.Monsters.Monster> monsters = ModUtility.MonsterProximity(Game1.player.currentLocation, new() { farVector }, 320, true);

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

                    if (!Mod.instance.eventRegister.ContainsKey("curse"))
                    {

                        curseEffect = new();

                        curseEffect.eventId = "curse";

                        curseEffect.EventActivate();

                    }
                    else
                    {

                        curseEffect = Mod.instance.eventRegister["curse"] as Curse;

                    }

                    foreach (StardewValley.Monsters.Monster monster in monsters)
                    {

                        if (!Mod.instance.questHandle.IsComplete(QuestHandle.fatesTwo))
                        {

                            Mod.instance.questHandle.UpdateTask(QuestHandle.fatesTwo, 1);

                        }

                        switch (Mod.instance.randomIndex.Next(4))
                        {
                            case 0:

                                curseEffect.AddTarget(Game1.player.currentLocation, monster, SpellHandle.effects.daze);
  
                                break;

                            //case 1:
                            //    curseEffect.AddTarget(Game1.player.currentLocation, monster, SpellHandle.effects.mug);

                            //    break;

                            case 2:
                                curseEffect.AddTarget(Game1.player.currentLocation, monster, SpellHandle.effects.morph);

                                break;

                            default:
                            //case 3:
                                curseEffect.AddTarget(Game1.player.currentLocation, monster, SpellHandle.effects.doom);

                                break;
                        }

                        castCost += 12;

                    }
                
                }

            }

        }

        public void CastTricks()
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

                // Trick reaction

                int roster = 3;

                if (Mod.instance.questHandle.IsComplete(QuestHandle.fatesThree))
                {

                    roster++;

                }

                int display = Mod.instance.randomIndex.Next(roster);

                int friendship = Mod.instance.randomIndex.Next(0, 5) * 25 - 25;

                Game1.player.changeFriendship(friendship, witness);

                Game1.player.friendshipData[witness.Name].TalkedToToday = true;

                ReactionData.ReactTo(witness, ReactionData.reactions.fates, friendship, new() { display, });

                // Trick performance

                if (display == 3)
                {

                    Levitate levitation = new(witness);

                    levitation.EventActivate();

                }
                else
                {

                    SpellHandle trick = new(witness.Position, 192, IconData.impacts.nature, new()) { instant = true };

                    trick.type = SpellHandle.spells.trick;

                    trick.projectile = display;

                    Mod.instance.spellRegister.Add(trick);

                }

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

            if (!Game1.player.currentLocation.IsFarm && !Game1.player.currentLocation.IsGreenhouse && Game1.player.currentLocation is not AnimalHouse && Game1.player.currentLocation is not Shed) { return; }

            Enchant enchantmentEvent = new();

            enchantmentEvent.EventSetup(Game1.player.Position, "enchant");

            enchantmentEvent.EventActivate();

        }

        public void CastEther()
        {

            if (castLevel == 0)
            {

                CastTransform();

            }

        }

        public void CastTransform()
        {


            if (Mod.instance.eventRegister.ContainsKey("transform"))
            {

                if(Mod.instance.eventRegister["transform"] is Cast.Ether.Transform transformEvent)
                {
                    
                    if (transformEvent.AttemptReset())
                    {

                        return;

                    }

                    transformEvent.EventRemove();

                }

                Mod.instance.eventRegister.Remove("transform");

                return;

            }

            Cast.Ether.Transform transform = new();

            transform.leftActive = true;

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

            if (!spawnIndex.anywhere && spawnIndex.locale != Game1.player.currentLocation.Name)
            {

                spawnIndex = new(Game1.player.currentLocation);

            }

            if (!spawnIndex.crate)
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

                Vector2 treasureVector = new Vector2(X, Y);

                if (ModUtility.NeighbourCheck(Game1.player.currentLocation, treasureVector, 0, 0).Count > 0)
                {

                    continue;

                }

                string treasureTerrain = ModUtility.GroundCheck(Game1.player.currentLocation, treasureVector);

                switch (treasureTerrain)
                {

                    case "ground":

                        treasure = new Crate();

                        treasure.EventSetup(treasureVector*64, "crate_" + Game1.player.currentLocation.Name, true);

                        treasure.crateThief = Mod.instance.randomIndex.Next(2) == 0;

                        treasure.crateTerrain = 1;

                        treasure.location = Game1.player.currentLocation;

                        return;

                    case "water":

                        treasure = new Crate();

                        treasure.EventSetup(treasureVector * 64, "crate_" + Game1.player.currentLocation.Name, true);

                        treasure.crateTerrain = 2;

                        treasure.location = Game1.player.currentLocation;

                        return;

                }

            }
            
        }

        public void CastBones()
        {

            CastMob();

            CastFlock();

        }

        public void CastMob()
        {

            List<CharacterHandle.characters> corvids = new()
            {
                CharacterHandle.characters.Raven,
                CharacterHandle.characters.Crow,
                CharacterHandle.characters.Rook,
                CharacterHandle.characters.Magpie,
            };

            bool returnToPlayer = Vector2.Distance(castVector * 64, Game1.player.Position) <= 192;

            foreach (CharacterHandle.characters corvid in corvids)
            {

                if (Mod.instance.characters.ContainsKey(corvid))
                {

                    Mod.instance.characters[corvid].ResetActives();

                    if (returnToPlayer)
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

                cursor(IconData.cursors.feathers, 75, 1280);

            }

        }

        public void CastFlock()
        {

            if (castLevel != 0) { return; }

            if (Mod.instance.eventRegister.ContainsKey("corvids"))
            {

                if (Mod.instance.eventRegister["corvids"] is Cast.Bones.Corvids corvids)
                {

                    if (!corvids.eventLocked)
                    {

                        corvids.eventAbort = true;

                    }

                }

                return;

            }

            Cast.Bones.Corvids corvidsEvent = new();

            corvidsEvent.EventSetup(Game1.player.Position, "corvids");

            corvidsEvent.EventActivate();

        }

        public void RiteBuff()
        {

            int toolIndex = Mod.instance.AttuneableWeapon();

            rites blessing = Mod.instance.save.rite;

            if (toolIndex == -1)
            {

                RemoveBuff();

                return;

            }
            else if (toolIndex == 999 && Mod.instance.eventRegister.ContainsKey("transform"))
            {

                blessing = rites.ether;

            }

            if(Mod.instance.magic)
            {

                blessing = GetSlotBlessing();

            }
            else
            if (Mod.instance.save.milestone < Journal.QuestHandle.milestones.effigy)
            {

                RemoveBuff();

                return;

            }
            else if (Mod.instance.Config.slotAttune)
            {

                blessing = GetSlotBlessing();

            }
            else
            {

                if (Mod.instance.Attunement.ContainsKey(toolIndex))
                {

                    blessing = RequirementCheck(Mod.instance.Attunement[toolIndex]);

                }

            }

            if (blessing == rites.none)
            {

                RemoveBuff();

                return;

            }

            if(appliedBuff == blessing)
            {

                if (Game1.player.buffs.IsApplied("184651"))
                {
                    return;

                }

            }

            appliedBuff = blessing;

            Buff riteBuff = new(
                "184651", 
                source: DialogueData.Strings(DialogueData.stringkeys.stardewDruid), 
                displaySource: DialogueData.Strings(DialogueData.stringkeys.stardewDruid),
                duration: Buff.ENDLESS, 
                iconTexture:Mod.instance.iconData.displayTexture, 
                iconSheetIndex: (int)Mod.instance.iconData.riteDisplays[blessing] - 1, 
                displayName: DialogueData.RiteNames(blessing), 
                description: DialogueData.Strings(DialogueData.stringkeys.riteBuffDescription)
                );

            Game1.player.buffs.Apply(riteBuff);

        }

        public void RemoveBuff()
        {

            if (Game1.player.buffs.IsApplied("184651"))
            {

                Game1.player.buffs.Remove("184651");

            }

        }

    }

}