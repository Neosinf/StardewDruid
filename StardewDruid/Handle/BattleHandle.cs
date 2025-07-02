using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using StardewDruid.Cast;
using StardewDruid.Cast.Mists;
using StardewDruid.Data;
using StardewDruid.Battle;
using StardewDruid.Dialogue;
using StardewDruid.Journal;
using StardewValley;
using StardewValley.Companions;
using StardewValley.Extensions;
using StardewValley.GameData.HomeRenovations;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Minigames;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using StardewDruid.Character;
using StardewValley.Constants;
using StardewDruid.Cast.Ether;
using StardewDruid.Cast.Effect;
using xTile.ObjectModel;

namespace StardewDruid.Handle
{
    public class BattleHandle
    {

        public static string readoutintro = "intro";

        public static string readoutsummon = "summon";

        public static string readoutforfeit = "forfeit";

        public static string readoutreturn = "return";

        public static string readoutengage = "engage";

        public static string readoutpending = "pending";

        public static string readoutfinish = "finish";

        public static string readoutbuff = "buff";

        public static string readoutapply = "apply";

        public NPC opposition;

        // rendering -------------------------------------------------------

        public bool opponentLoaded;

        public bool championLoaded;

        public Character.Character opponent;

        public Character.Character champion;

        public BattleCombatant opponentCombat;

        public BattleCombatant championCombat;

        public Vector2 opponentOffset;

        public Vector2 opponentDecrement;

        public Vector2 opponentIncrement;

        public Vector2 opponentDestination;

        public Vector2 championOffset;

        public Vector2 championDecrement;

        public Vector2 championIncrement;

        public Vector2 championDestination;

        public float opponentFade;

        public float championFade;

        public int opponentFlashing;

        public int championFlashing;

        // interface -------------------------------------------------------

        public int interfaceWidth = 960;

        public int interfaceHeight = 640;

        public int controlFocus;

        public bool header;

        public string headerText;

        public List<TemporaryAnimatedSprite> battleAnimations = new();

        // states ----------------------------------------------------------

        public bool multiplayer;

        public int stateTimer;

        public Dictionary<int, BattleMove> moveStack = new();

        public int currentMove;

        public enum battlestates
        {

            intro,
            choose,
            summon,
            select,
            item,
            pending,
            engage,
            apply,
            finish,
            forfeit,

        }

        public battlestates state = battlestates.intro;

        public enum winstates
        {
            pending,
            champion,
            opponent,

        }

        public winstates winState = winstates.pending;

        public string thiefId;

        // Battle Logic ======================================================

        public BattleHandle()
        {

        }

        public void SearchChallengers()
        {

            if (Mod.instance.activeEvent.Count > 0)
            {

                return;

            }

            List<NPC> villagers = ModUtility.GetFriendsInLocation(Game1.player.currentLocation, true);

            float threshold = 640;

            foreach (NPC witness in villagers)
            {

                if (Mod.instance.Witnessed(ReactionData.reactions.battle, witness))
                {

                    continue;

                }

                if (Vector2.Distance(witness.Position, Game1.player.Position) >= threshold)
                {

                    continue;

                }

                witness.faceTowardFarmerForPeriod(3000, 4, false, Game1.player);

                witness.doEmote(16, true);

                opposition = witness;

                ChallengeEngage();

                return;

            }

            List<StardewDruid.Character.Character> companions = ModUtility.CompanionProximity(Game1.player.currentLocation, new() { Game1.player.Position, }, threshold);

            List<CharacterHandle.characters> safelist = new()
            {
                CharacterHandle.characters.Effigy,
                CharacterHandle.characters.Jester,
                CharacterHandle.characters.Revenant,
                CharacterHandle.characters.Buffin,
                CharacterHandle.characters.Aldebaran,
                CharacterHandle.characters.Shadowtin,
                CharacterHandle.characters.Blackfeather,
                CharacterHandle.characters.recruit_one,
                CharacterHandle.characters.recruit_two,
                CharacterHandle.characters.recruit_three,
                CharacterHandle.characters.recruit_four,
            };

            foreach (StardewDruid.Character.Character witness in companions)
            {

                if (!safelist.Contains(witness.characterType))
                {

                    continue;

                }

                if (Mod.instance.Witnessed(ReactionData.reactions.battle, witness))
                {

                    continue;

                }

                witness.LookAtTarget(Game1.player.Position, true);

                witness.doEmote(16, true);

                opposition = witness;

                ChallengeEngage();

                return;

            }

        }

        public void ChallengeEngage()
        {

            List<Response> responseList = new();

            string intro = opposition.displayName + Mod.instance.Helper.Translation.Get("BattleHandle.388.71");

            responseList.Add(new Response(1.ToString(), Mod.instance.Helper.Translation.Get("BattleHandle.388.73")));

            if (RelicData.HasRelic(IconData.relics.dragon_form))
            {

                responseList.Add(new Response(2.ToString(), Mod.instance.Helper.Translation.Get("BattleHandle.391.2")));

            }

            responseList.Add(new Response(999.ToString(), Mod.instance.Helper.Translation.Get("BattleHandle.388.74")));

            responseList.First().SetHotKey(Keys.Enter);

            responseList.Last().SetHotKey(Keys.Escape);

            GameLocation.afterQuestionBehavior questionBehavior = new(ChallengeResponse);

            Game1.player.currentLocation.createQuestionDialogue(intro, responseList.ToArray(), questionBehavior, opposition);

        }

        public void ThiefEngage(string ThiefId)
        {

            thiefId = ThiefId;

            List<Response> responseList = new();

            string intro = opposition.displayName + Mod.instance.Helper.Translation.Get("BattleHandle.391.1");

            responseList.Add(new Response(1.ToString(), Mod.instance.Helper.Translation.Get("BattleHandle.388.73")));

            if (RelicData.HasRelic(IconData.relics.dragon_form))
            {

                responseList.Add(new Response(2.ToString(), Mod.instance.Helper.Translation.Get("BattleHandle.391.2")));

            }

            responseList.Add(new Response(999.ToString(), Mod.instance.Helper.Translation.Get("BattleHandle.391.3")));

            responseList.First().SetHotKey(Keys.Enter);

            responseList.Last().SetHotKey(Keys.Escape);

            GameLocation.afterQuestionBehavior questionBehavior = new(ChallengeResponse);

            Game1.player.currentLocation.createQuestionDialogue(intro, responseList.ToArray(), questionBehavior, opposition);

        }

        public void ChallengeResponse(Farmer visitor, string dialogueId)
        {

            switch (Convert.ToInt32(dialogueId))
            {

                case 1:

                    Mod.instance.AddWitness(ReactionData.reactions.battle, opposition.Name);

                    Register();

                    break;

                case 2:

                    Mod.instance.AddWitness(ReactionData.reactions.battle, opposition.Name);

                    int maxLevel = GetMaxLevel();

                    Cast.Ether.Dragon dragon = null;

                    if (Mod.instance.eventRegister.ContainsKey(Rite.eventTransform))
                    {

                        if (Mod.instance.eventRegister[Rite.eventTransform] is Cast.Ether.Transform transform)
                        {

                            dragon = transform.avatar;

                        }

                    }

                    if(dragon == null)
                    {

                        dragon = new(Game1.player, Game1.player.Position, Game1.player.currentLocation.Name, "RedDragon")
                        {

                            currentLocation = Game1.player.currentLocation

                        };

                    }

                    BattleDragon battleDragon = new BattleDragon(dragon, maxLevel);

                    BattleCombatant battleCombatant = new(battleDragon, maxLevel);

                    LoadChampion(battleDragon, battleCombatant);

                    Register();

                    break;

                case 999:

                    if (thiefId != null)
                    {

                        if (Mod.instance.eventRegister.ContainsKey(thiefId))
                        {

                            if (Mod.instance.eventRegister[thiefId] is Crate crateEvent)
                            {

                                crateEvent.InitiateContest();

                            }

                        }

                    }

                    break;

            }

        }

        public int GetMaxLevel()
        {

            int maxLevel = 1;

            foreach (KeyValuePair<CharacterHandle.characters, PalData> pal in Mod.instance.save.pals)
            {

                int unitLevel = PalHandle.UnitLevel(pal.Value.experience);

                if (unitLevel > maxLevel)
                {

                    maxLevel = unitLevel;

                }

            }

            return maxLevel;

        }

        public void Register()
        {

            int maxLevel = GetMaxLevel();


            if (thiefId != null)
            {

                ThiefOpponent(maxLevel);

            }

            if (opponent is null)
            {

                RandomOpponent(maxLevel);

            }

            Mod.instance.battleRegister.Add(this);

            if (Game1.activeClickableMenu != null)
            {

                Game1.activeClickableMenu.exitThisMenu(false);

            }

            InitiateState(battlestates.intro);

            Game1.activeClickableMenu = new BattlePage(null, 0, this);

        }

        public void ThiefOpponent(int maxLevel = 1)
        {

            if (Mod.instance.eventRegister.ContainsKey(thiefId))
            {

                if (Mod.instance.eventRegister[thiefId] is Crate crateEvent)
                {

                    if (crateEvent.bosses.Count == 0)
                    {
                        return;
                    }

                    switch (crateEvent.bosses[0].realName.Value)
                    {

                        case "Serpent":

                            RandomOpponent(maxLevel, CharacterHandle.characters.PalSerpent, 0);

                            opponentCombat.possessive = Mod.instance.Helper.Translation.Get("NarratorData.366.1") + StringData.pluralism;

                            break;

                        case "RiverSerpent":

                            RandomOpponent(maxLevel, CharacterHandle.characters.PalSerpent, 1);

                            opponentCombat.possessive = Mod.instance.Helper.Translation.Get("NarratorData.366.1") + StringData.pluralism;

                            break;

                        case "NightSerpent":

                            RandomOpponent(maxLevel, CharacterHandle.characters.PalSerpent, 2);

                            opponentCombat.possessive = Mod.instance.Helper.Translation.Get("NarratorData.366.1") + StringData.pluralism;

                            break;

                        case "LavaSerpent":

                            RandomOpponent(maxLevel, CharacterHandle.characters.PalSerpent, 3);

                            opponentCombat.possessive = Mod.instance.Helper.Translation.Get("NarratorData.366.1") + StringData.pluralism;

                            break; 

                    }

                }

            }



        }

        public void RandomOpponent(int maxLevel = 1, CharacterHandle.characters opponentType = CharacterHandle.characters.none, int opponentScheme = 0)
        {

            if (opponentType == CharacterHandle.characters.none)
            {

                opponentType = CharacterHandle.characters.PalBat + Mod.instance.randomIndex.Next(4);

                opponentScheme = Mod.instance.randomIndex.Next(3);


            }

            int opponentLevel = Mod.instance.randomIndex.Next(1, maxLevel+1);

            int opponentHealth = Mod.instance.randomIndex.Next(10) * opponentLevel;

            int opponentAttack = Mod.instance.randomIndex.Next(10) * opponentLevel;

            int opponentSpeed = Mod.instance.randomIndex.Next(10) * opponentLevel;

            int opponentResist = Mod.instance.randomIndex.Next(4) * opponentLevel;

            BattleCombatant battleCombatant = new(opponentType, opponentLevel, opponentHealth, opponentAttack, opponentSpeed, opponentResist, opponentScheme);

            LoadOpponent(PalHandle.PalInstance(opponentType, opponentScheme, opponentLevel), battleCombatant);

            if (opposition != null)
            {

                opponentCombat.possessive = opposition.displayName + StringData.pluralism;

            }
            else
            {

                opponentCombat.possessive = Mod.instance.Helper.Translation.Get("BattleHandle.388.166");

            }

        }

        public void LoadChampion(Character.Character character, BattleCombatant combatant)
        {

            champion = character;

            character.setScale = 4.5f;

            character.netDirection.Set(0);

            character.netAlternative.Set(1);

            championCombat = combatant;

            combatant.champion = true;

            championLoaded = true;

        }

        public void LoadOpponent(Character.Character character, BattleCombatant combatant)
        {

            opponent = character;

            character.setScale = 3.5f;

            character.netDirection.Set(2);

            character.netAlternative.Set(3);

            opponentCombat = combatant;

            combatant.champion = false;

            opponentLoaded = true;

        }

        public void DrawBattle(SpriteBatch b, int xP, int yP, int width, int height, int offset)
        {

            Vector2 cP = new Vector2(xP, yP);

            // -----------------------------------------------------

            if (opponentLoaded)
            {

                opponent.DrawCharacter(b, cP + new Vector2(width - 160, 160) + opponentOffset);

                // -----------------------------------------------------

                string opponentTitle = opponentCombat.name + StringData.longbreak + opponentCombat.levelString + StringData.longbreak + opponentCombat.title;

                float opponentFadein = 1f - opponentFade;

                b.DrawString(Game1.dialogueFont, opponentTitle, cP + new Vector2(47, 50f), Color.Brown * 0.35f * opponentFadein, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.900f);

                b.DrawString(Game1.dialogueFont, opponentTitle, cP + new Vector2(48, 48), Game1.textColor * opponentFadein, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.901f);

                float opponentHP = opponentCombat.HealthBar();

                Rectangle container = new((int)cP.X + 48, (int)cP.Y + 104, 448, 48);

                IClickableMenu.drawTextureBox(
                    b,
                    Game1.mouseCursors,
                    new Rectangle(384, 396, 15, 15),
                    container.X,
                    container.Y,
                    container.Width,
                    container.Height,
                    Color.White * opponentFadein,
                    3f,
                    false,
                    0.0001f
                );

                b.Draw(Game1.staminaRect, new Vector2(container.X + 9, container.Y + 9), new Rectangle(container.X + 9, container.Y + 9, container.Width - 18, container.Height - 18), new Color(254, 240, 192) * opponentFadein, 0f, Vector2.Zero, 1f, 0, 0.0001f);

                b.Draw(Game1.staminaRect, new Vector2(container.X + 9, container.Y + 9), new Rectangle(container.X + 9, container.Y + 9, (int)((container.Width - 18) * opponentHP), container.Height - 18), Color.DarkRed * 0.25f * opponentFadein, 0f, Vector2.Zero, 1f, 0, 0.0007f);

                int buffOffset = 0;

                foreach (BattleBuff debuff in opponentCombat.debuffs)
                {

                    b.Draw(
                        Mod.instance.iconData.displayTexture,
                        new Vector2((int)cP.X + 64 + buffOffset, (int)cP.Y + 176),
                        IconData.DisplayRectangle(debuff.display),
                        Microsoft.Xna.Framework.Color.White,
                        0f,
                        new Vector2(8),
                        3f,
                        0,
                        0.900f
                    );

                    buffOffset += 48;

                }

            }

            // -----------------------------------------------------
            if (championLoaded)
            {

                champion.DrawCharacter(b, cP + new Vector2(160, height - 320) + championOffset);

                string championTitle = championCombat.name + StringData.longbreak + championCombat.levelString + StringData.longbreak + championCombat.title;

                float championFadein = 1f - championFade;

                Vector2 championVector = Game1.dialogueFont.MeasureString(championTitle);

                b.DrawString(Game1.dialogueFont, championTitle, cP + new Vector2(width - championVector.X - 49, height - 254f), Color.Brown * 0.35f * championFadein, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.900f);

                b.DrawString(Game1.dialogueFont, championTitle, cP + new Vector2(width - championVector.X - 48, height - 256), Game1.textColor * championFadein, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.901f);

                // -----------------------------------------------------

                float championHP = championCombat.HealthBar();

                Rectangle container = new((int)cP.X + width - 448, (int)cP.Y + height - 320, 400, 48);

                IClickableMenu.drawTextureBox(
                    b,
                    Game1.mouseCursors,
                    new Rectangle(384, 396, 15, 15),
                    container.X,
                    container.Y,
                    container.Width,
                    container.Height,
                    Color.White * championFadein,
                    3f,
                    false,
                    0.0001f
                );

                b.Draw(Game1.staminaRect, new Vector2(container.X + 9, container.Y + 9), new Rectangle(container.X + 9, container.Y + 9, container.Width - 18, container.Height - 18), new Color(254, 240, 192) * championFadein, 0f, Vector2.Zero, 1f, 0, 0.0001f);

                b.Draw(Game1.staminaRect, new Vector2(container.X + 9, container.Y + 9), new Rectangle(container.X + 9, container.Y + 9, (int)((container.Width - 18) * championHP), container.Height - 18), Color.DarkGreen * 0.25f * championFadein, 0f, Vector2.Zero, 1f, 0, 0.0007f);

                int buffOffset = 0;

                foreach (BattleBuff debuff in championCombat.debuffs)
                {

                    b.Draw(
                        Mod.instance.iconData.displayTexture,
                        new Vector2((int)cP.X + width - 512 - buffOffset, (int)cP.Y + height - 320),
                        IconData.DisplayRectangle(debuff.display),
                        Microsoft.Xna.Framework.Color.White,
                        0f,
                        Vector2.Zero,
                        3f,
                        0,
                        0.900f
                    );

                    buffOffset += 64;

                }
            }

            if (header)
            {

                Rectangle container = new((int)cP.X + 16, (int)cP.Y + height - 272, width - 32, 256);

                IClickableMenu.drawTextureBox(
                    b,
                    Game1.mouseCursors,
                    new Rectangle(384, 396, 15, 15),
                    container.X,
                    container.Y,
                    container.Width,
                    container.Height,
                    Color.White,
                    3f,
                    false,
                    0.0001f
                );

                b.DrawString(Game1.dialogueFont, headerText, new((int)cP.X + 31, (int)cP.Y + height - 254), Color.Brown * 0.35f, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.900f);

                b.DrawString(Game1.dialogueFont, headerText, new((int)cP.X + 32, (int)cP.Y + height - 256), Game1.textColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.901f);

            }

            for (int a = battleAnimations.Count - 1; a >= 0; a--)
            {

                battleAnimations[a].draw(b, true);

            }

        }

        public void InitiateState(battlestates battlestate)
        {

            ResetState();

            state = battlestate;

            switch (state)
            {

                case battlestates.intro:

                    stateTimer = 120;

                    IntroPosition();

                    break;

                case battlestates.summon:

                    stateTimer = 120;

                    SummonAnimate();

                    break;

                case battlestates.select:

                    break;

                case battlestates.pending:

                    stateTimer = 120;

                    opponentCombat.chosen = BattleCombatant.battleoptions.pending;

                    break;

                case battlestates.engage:

                    stateTimer = 120;

                    EngageMove();

                    break;

                case battlestates.apply:

                    stateTimer = 120;

                    ApplyMove();

                    break;

                case battlestates.finish:

                    stateTimer = 120;

                    //OutroPosition();

                    break;

                case battlestates.forfeit:

                    stateTimer = 120;

                    break;

            }

            ResetInterface();

        }

        public void ResetInterface()
        {

            if (Game1.activeClickableMenu is BattlePage battlemenu)
            {

                battlemenu.populateContent();

                battlemenu.resetInterface();

            }

        }
        
        public bool Update()
        {

            if (Game1.activeClickableMenu is not BattlePage)
            {

                if (winState == winstates.pending)
                {

                    Mod.instance.CastDisplay(Mod.instance.Helper.Translation.Get("BattleHandle.388.415"));

                }

                return false;

            }

            if (stateTimer > 0)
            {

                stateTimer--;

            }

            for (int a = battleAnimations.Count - 1; a >= 0; a--)
            {

                if (battleAnimations[a].update(Game1.currentGameTime))
                {

                    battleAnimations.RemoveAt(a);

                }

            }

            switch (state)
            {

                case battlestates.intro:

                    UpdateOffsets();

                    UpdateFades();

                    if (stateTimer <= 0)
                    {

                        if (championCombat != null)
                        {

                            InitiateState(battlestates.select);

                        }
                        else
                        {

                            InitiateState(battlestates.choose);

                        }

                    }

                    break;

                case battlestates.summon:

                    UpdateFades();

                    if (stateTimer <= 0)
                    {

                        if (championCombat != null)
                        {

                            InitiateState(battlestates.select);

                        }

                    }

                    break;

                default:
                case battlestates.select:

                    break;

                case battlestates.pending:

                    if (!multiplayer)
                    {

                        OpponentMove();

                    }
                    else
                    {

                        if (opponentCombat.chosen == BattleCombatant.battleoptions.pending)
                        {

                            break;

                        }

                    }

                    CalculateMove();

                    if (moveStack.Count > 0)
                    {

                        BattleMove buffMove = moveStack.First().Value;

                        if (buffMove.reaction)
                        {

                            InitiateState(battlestates.apply);

                        }
                        else
                        {

                            InitiateState(battlestates.engage);

                        }

                    }

                    break;

                case battlestates.engage:

                    UpdateOffsets();

                    if (stateTimer <= 0)
                    {

                        InitiateState(battlestates.apply);

                    }

                    break;

                case battlestates.apply:

                    UpdateFades();

                    if (stateTimer <= 0)
                    {

                        if (moveStack.Count > 0)
                        {

                            moveStack.Remove(moveStack.First().Key);

                        }

                        if (winState != winstates.pending)
                        {

                            InitiateState(battlestates.finish);

                        }
                        else
                        if (moveStack.Count > 0)
                        {

                            BattleMove buffMove = moveStack.First().Value;

                            if (buffMove.reaction)
                            {

                                InitiateState(battlestates.apply);

                            }
                            else
                            {

                                InitiateState(battlestates.engage);

                            }

                        }
                        else
                        {

                            InitiateState(battlestates.select);

                        }

                    }

                    break;

                case battlestates.finish:

                    //UpdateOffsets();

                    if (stateTimer <= 0)
                    {

                        if (Game1.activeClickableMenu is BattlePage)
                        {

                            if (thiefId != null)
                            {

                                if (Mod.instance.eventRegister.ContainsKey(thiefId))
                                {

                                    if (Mod.instance.eventRegister[thiefId] is Crate crateEvent)
                                    {

                                        if (winState == winstates.champion)
                                        {

                                            crateEvent.eventComplete = true;

                                            crateEvent.ReleaseReward();

                                        }
                                        else
                                        {

                                            crateEvent.InitiateContest();

                                        }

                                    }

                                }

                            }

                            if (winState == winstates.champion)
                            {

                                DistributeRewards();

                                Mod.instance.CastDisplay(Mod.instance.Helper.Translation.Get("BattleHandle.388.524"));

                            }
                            else
                            {

                                Mod.instance.CastDisplay(Mod.instance.Helper.Translation.Get("BattleHandle.388.528"));

                            }

                            Game1.exitActiveMenu();

                            if (opposition is NPC witness)
                            {

                                if (Game1.player.friendshipData.ContainsKey(witness.Name))
                                {

                                    int friendship = 0;

                                    if (winState == winstates.champion)
                                    {

                                        friendship = 25;

                                    }

                                    ModUtility.ChangeFriendship(witness, friendship);

                                    ReactionData.ReactTo(witness, ReactionData.reactions.battle, friendship, new() { championCombat.title, opponentCombat.title});

                                }

                            }

                        }

                        return false;

                    }

                    break;

                case battlestates.forfeit:


                    if (stateTimer <= 0)
                    {

                        if (thiefId != null)
                        {

                            if (Mod.instance.eventRegister.ContainsKey(thiefId))
                            {

                                if (Mod.instance.eventRegister[thiefId] is Crate crateEvent)
                                {

                                    crateEvent.InitiateContest();

                                }

                            }

                        }

                        if (Game1.activeClickableMenu is BattlePage)
                        {

                            Mod.instance.CastDisplay(Mod.instance.Helper.Translation.Get("BattleHandle.388.415"));

                            Game1.exitActiveMenu();

                        }

                        return false;

                    }

                    break;

            }

            if (championLoaded)
            {

                champion.UpdateBattle();

            }

            if (opponentLoaded)
            {

                opponent.UpdateBattle();

            }


            return true;

        }

        public void UpdateOffsets()
        {

            if (opponentDestination != Vector2.Zero)
            {

                opponentOffset += opponentIncrement;

                if (Vector2.Distance(opponentOffset, opponentDestination) <= 4f)
                {

                    opponentDestination = Vector2.Zero;

                }

            }
            else
            if (opponentOffset != Vector2.Zero)
            {

                opponentOffset -= opponentDecrement;

                if (Vector2.Distance(opponentOffset, Vector2.Zero) <= 4f)
                {

                    opponentOffset = Vector2.Zero;

                }

            }

            if (championDestination != Vector2.Zero)
            {

                championOffset += championIncrement;

                if (Vector2.Distance(championOffset, championDestination) <= 4f)
                {

                    championDestination = Vector2.Zero;

                }

            }
            else
            if (championOffset != Vector2.Zero)
            {

                championOffset -= championDecrement;

                if (Vector2.Distance(championOffset, Vector2.Zero) <= 4f)
                {

                    championOffset = Vector2.Zero;

                }

            }

        }

        public void UpdateFades()
        {

            if (opponentFade > 0f)
            {

                opponentFade -= 0.0075f;

                if (opponentFade <= 0.2f)
                {

                    opponentFade = 0f;

                }

            }
            else
            {

                opponentFade = 0f;

            }

            if (championFade > 0f)
            {

                championFade -= 0.0075f;

                if (championFade <= 0.2f)
                {

                    championFade = 0f;

                }

            }
            else
            {

                championFade = 0f;

            }

            if (opponentFlashing > 0)
            {

                int flash = 0 - 10 + opponentFlashing % 20;

                opponent.fadeOut = opponent.fadeSet - 0.5f + Math.Abs(flash) * 0.05f;

                opponentFlashing--;

            }

            if (championFlashing > 0)
            {

                int flash = 0 - 10 + championFlashing % 20;

                champion.fadeOut = champion.fadeSet - 0.5f + Math.Abs(flash) * 0.05f;

                championFlashing--;

            }

        }

        public void ResetState()
        {

            stateTimer = 0;

            if (championLoaded)
            {

                champion.ResetActives();

                championCombat.health = championCombat.min;

                champion.fadeOut = champion.fadeSet;

            }

            if (opponentLoaded)
            {

                opponent.ResetActives();

                opponentCombat.health = opponentCombat.min;

                opponent.fadeOut = opponent.fadeSet;

            }

            opponentDestination = Vector2.Zero;

            championDestination = Vector2.Zero;

            opponentOffset = Vector2.Zero;

            championOffset = Vector2.Zero;

            opponentFade = 0f;

            championFade = 0f;
        }

        // Interface States ===================================================

        public int ControlFocus()
        {

            switch (state)
            {
                default:

                    return 0;

                case battlestates.select:

                    return controlFocus;


            }

        }

        public Dictionary<int, ContentComponent> InterfaceState()
        {

            header = false;

            headerText = string.Empty;

            switch (state)
            {

                case battlestates.intro:

                    return IntroReadout();

                case battlestates.choose:

                    return SummonOptions();

                case battlestates.summon:

                    return SummonReadout();

                default:
                case battlestates.select:

                    return SelectOptions();

                case battlestates.item:

                    return ItemOptions();

                case battlestates.pending:

                    return PendingReadout();

                case battlestates.engage:

                    return EngageReadout();

                case battlestates.apply:

                    return ApplyReadout();

                case battlestates.finish:

                    return FinishReadout();

                case battlestates.forfeit:

                    return ForfeitReadout();

            }

        }

        public Dictionary<int, ContentComponent> IntroReadout()
        {

            Dictionary<int, ContentComponent> components = new();

            ContentComponent component = new(ContentComponent.contentTypes.battlereadout, readoutintro);

            component.readoutPace = 2;

            component.text[0] = opponentCombat.title + Mod.instance.Helper.Translation.Get("BattleHandle.388.700");

            components.Add(0, component);

            return components;

        }

        public Dictionary<int, ContentComponent> SummonReadout()
        {

            Dictionary<int, ContentComponent> components = new();

            ContentComponent component = new(ContentComponent.contentTypes.battlereadout, readoutsummon);

            component.readoutPace = 2;

            component.text[0] = championCombat.name + StringData.comma + championCombat.title + Mod.instance.Helper.Translation.Get("BattleHandle.388.709");

            components.Add(0, component);

            return components;

        }

        public Dictionary<int, ContentComponent> SummonOptions()
        {

            Dictionary<int, ContentComponent> components = new();

            int start = 0;

            header = true;

            headerText = Mod.instance.Helper.Translation.Get("BattleHandle.388.718");

            PalHandle.CheckDefault();

            foreach (IconData.relics relicName in Mod.instance.relicsData.lines[RelicData.relicsets.monsterstones])
            {

                ContentComponent content = new(ContentComponent.contentTypes.relic, relicName.ToString());

                content.textureSources[0] = IconData.RelicRectangles(relicName);

                content.textureColours[0] = Color.White;

                if (!RelicData.HasRelic(relicName))
                {

                    continue;

                }

                if (relicName == IconData.relics.monsterbadge)
                {

                    content.text[1] = Mod.instance.Helper.Translation.Get("BattleHandle.388.730");

                    content.text[2] = Mod.instance.Helper.Translation.Get("BattleHandle.388.731");

                }
                else
                {

                    PalData pal = PalHandle.DataFromRelic(relicName);

                    int level = PalHandle.UnitLevel(pal.experience);

                    content.text[1] = PalHandle.PalScheme(pal.type);

                    content.text[2] = StringData.LevelStrings(level);

                    int vitality = PalHandle.HealthLevel(pal.type, level, pal.health);

                    int attack = PalHandle.AttackLevel(pal.type, level, pal.attack);

                    int speed = PalHandle.SpeedLevel(pal.type, level, pal.speed);

                    content.text[3] = Mod.instance.Helper.Translation.Get("BattleHandle.388.743").Tokens(new { vitality = vitality, attack = attack, speed = speed, });

                }

                components[start++] = content;

            }

            ContentComponent returnButton = new(ContentComponent.contentTypes.battlereturn, readoutforfeit);

            returnButton.icons[0] = IconData.displays.exit;

            returnButton.text[0] = Mod.instance.Helper.Translation.Get("BattleHandle.388.749");

            returnButton.text[1] = Mod.instance.Helper.Translation.Get("BattleHandle.388.750");

            returnButton.text[2] = Mod.instance.Helper.Translation.Get("BattleHandle.388.751");

            components[start++] = returnButton;

            return components;

        }
        
        public bool SummonOption(string selectOption, int ControlIndex)
        {

            IconData.relics relic = Enum.Parse<IconData.relics>(selectOption);

            PalData pal = PalHandle.DataFromRelic(relic);

            BattleCombatant battleCombatant = new(pal);

            LoadChampion(PalHandle.PalInstance(pal.type, pal.scheme, battleCombatant.level), battleCombatant);

            InitiateState(battlestates.summon);

            return true;

        }

        public Dictionary<int, ContentComponent> SelectOptions()
        {

            Dictionary<int, ContentComponent> components = new();

            int start = 0;

            // ------------------------------------------- controls

            Dictionary<int, BattleCombatant.battleoptions> buttons = new()
            {

                [0] = BattleCombatant.battleoptions.attack,
                [1] = BattleCombatant.battleoptions.block,
                [2] = BattleCombatant.battleoptions.item,

                [3] = BattleCombatant.battleoptions.tackle,
                [4] = BattleCombatant.battleoptions.special,
                [5] = BattleCombatant.battleoptions.forfeit,

            };

            foreach (KeyValuePair<int, BattleCombatant.battleoptions> button in buttons)
            {

                ContentComponent component = new(ContentComponent.contentTypes.battle, button.Value.ToString());

                switch (button.Value)
                {


                    case BattleCombatant.battleoptions.item:

                        component.text[0] = Mod.instance.Helper.Translation.Get("BattleHandle.388.775");

                        component.text[1] = Mod.instance.Helper.Translation.Get("BattleHandle.388.776");

                        component.text[2] = Mod.instance.Helper.Translation.Get("BattleHandle.388.777");

                        component.icons[0] = IconData.displays.herbalism;

                        break;

                    case BattleCombatant.battleoptions.forfeit:

                        component.text[0] = Mod.instance.Helper.Translation.Get("BattleHandle.388.781");

                        component.text[1] = Mod.instance.Helper.Translation.Get("BattleHandle.388.782");

                        component.text[2] = Mod.instance.Helper.Translation.Get("BattleHandle.388.783");

                        component.icons[0] = IconData.displays.exit;

                        break;

                    default:

                        BattleAbility.battleabilities ability = championCombat.moveset[button.Value];

                        BattleAbility useAbility = championCombat.abilities[ability];

                        component.text[0] = useAbility.button;

                        component.text[1] = useAbility.title;

                        component.text[2] = useAbility.description;

                        component.text[3] = Mod.instance.Helper.Translation.Get("BattleHandle.388.792").Tokens(new { damage = useAbility.damage, speed = useAbility.speed, accuracy = useAbility.accuracy, defense = useAbility.defense, });

                        component.icons[0] = championCombat.abilities[ability].display;

                        break;


                }


                components.Add(start++, component);


            }

            return components;

        }

        public bool SelectOption(string selectOption, int ControlIndex)
        {

            controlFocus = ControlIndex;

            BattleCombatant.battleoptions battleoption = Enum.Parse<BattleCombatant.battleoptions>(selectOption);

            switch (battleoption)
            {

                case BattleCombatant.battleoptions.attack:

                case BattleCombatant.battleoptions.block:

                case BattleCombatant.battleoptions.tackle:

                case BattleCombatant.battleoptions.special:

                    championCombat.chosen = battleoption;

                    InitiateState(battlestates.pending);

                    break;

                case BattleCombatant.battleoptions.item:

                    InitiateState(battlestates.item);

                    break;

                case BattleCombatant.battleoptions.forfeit:

                    InitiateState(battlestates.forfeit);

                    break;

            }

            return true;

        }
        
        public bool ReturnOption(string selectOption, int ControlIndex)
        {

            switch (state)
            {

                case battlestates.item:

                    InitiateState(battlestates.select);

                    break;

                case battlestates.choose:

                    InitiateState(battlestates.forfeit);

                    break;

            }

            return true;

        }

        public Dictionary<int, ContentComponent> ItemOptions()
        {

            Dictionary<int, ContentComponent> components = new();

            int start = 0;

            header = true;

            headerText = Mod.instance.Helper.Translation.Get("BattleHandle.388.805");

            HerbalHandle.herbals bestLigna = Mod.instance.herbalData.BestHerbal(HerbalHandle.herbals.ligna);
            HerbalHandle.herbals bestImpes = Mod.instance.herbalData.BestHerbal(HerbalHandle.herbals.impes);
            HerbalHandle.herbals bestCeleri = Mod.instance.herbalData.BestHerbal(HerbalHandle.herbals.celeri);

            List<HerbalHandle.herbals> potions = new()
            {
                bestLigna,
                bestImpes,
                bestCeleri,
                HerbalHandle.herbals.faeth,
                HerbalHandle.herbals.trophy_shroom,
                HerbalHandle.herbals.trophy_eye,
            };

            foreach (HerbalHandle.herbals potionName in potions)
            {

                ContentComponent content = new(ContentComponent.contentTypes.potion, potionName.ToString());

                int amount = HerbalHandle.GetHerbalism(potionName);

                string amountString = amount.ToString();

                content.text[0] = amount.ToString();

                if (amount == 0)
                {

                    content.textureSources[0] = IconData.PotionRectangles(Mod.instance.herbalData.herbalism[potionName.ToString()].grayed);
                }
                else
                {

                    content.textureSources[0] = IconData.PotionRectangles(Mod.instance.herbalData.herbalism[potionName.ToString()].display);

                }

                switch (potionName)
                {

                    default:

                        if(potionName == bestLigna)
                        {
                            content.text[1] = Mod.instance.Helper.Translation.Get("BattleHandle.388.830");

                            content.text[2] = Mod.instance.Helper.Translation.Get("BattleHandle.388.831");

                        }
                        else if (potionName == bestImpes)
                        {

                            content.text[1] = Mod.instance.Helper.Translation.Get("BattleHandle.388.834");

                            content.text[2] = Mod.instance.Helper.Translation.Get("BattleHandle.388.835");

                        }
                        else if (potionName == bestCeleri)
                        {

                            content.text[1] = Mod.instance.Helper.Translation.Get("BattleHandle.388.838");

                            content.text[2] = Mod.instance.Helper.Translation.Get("BattleHandle.388.839");

                        }

                        break;

                    case HerbalHandle.herbals.faeth:

                        content.text[1] = Mod.instance.Helper.Translation.Get("BattleHandle.390.4");

                        content.text[2] = Mod.instance.Helper.Translation.Get("BattleHandle.390.5");

                        break;

                    case HerbalHandle.herbals.trophy_shroom:

                        content.text[1] = Mod.instance.Helper.Translation.Get("BattleHandle.390.6");

                        content.text[2] = Mod.instance.Helper.Translation.Get("BattleHandle.390.7");

                        break;

                    case HerbalHandle.herbals.trophy_eye:

                        content.text[1] = Mod.instance.Helper.Translation.Get("BattleHandle.390.8");

                        content.text[2] = Mod.instance.Helper.Translation.Get("BattleHandle.390.9");

                        break;

                    }


                components[start++] = content;

            }

            ContentComponent returnButton = new(ContentComponent.contentTypes.battlereturn, readoutreturn);

            returnButton.icons[0] = IconData.displays.replay;

            returnButton.text[0] = Mod.instance.Helper.Translation.Get("BattleHandle.388.846");

            returnButton.text[1] = Mod.instance.Helper.Translation.Get("BattleHandle.388.847");

            returnButton.text[2] = Mod.instance.Helper.Translation.Get("BattleHandle.388.848");

            components[start++] = returnButton;

            return components;

        }
        
        public bool ItemOption(string selectOption, int ControlIndex)
        {

            HerbalHandle.herbals herb = Enum.Parse<HerbalHandle.herbals>(selectOption);

            if(HerbalHandle.GetHerbalism(herb) <= 0)
            {

                return false;

            }

            championCombat.chosen = BattleCombatant.battleoptions.item;

            championCombat.item = herb;

            InitiateState(battlestates.pending);

            return true;

        }

        public Dictionary<int, ContentComponent> PendingReadout()
        {

            Dictionary<int, ContentComponent> components = new();

            ContentComponent component = new(ContentComponent.contentTypes.battlereadout, readoutpending);

            components.Add(0, component);

            return components;

        }

        public Dictionary<int, ContentComponent> EngageReadout()
        {

            Dictionary<int, ContentComponent> components = new();

            ContentComponent component = new(ContentComponent.contentTypes.battlereadout, readoutengage);

            component.readoutPace = 2;

            BattleMove useMove = moveStack.First().Value;

            string moveDescription;

            if (useMove.champion)
            {

                moveDescription = championCombat.abilities[useMove.ability].onTry;

                if (useMove.ability is BattleAbility.battleabilities.item)
                {

                    moveDescription = ItemDescription();

                }

                component.text[0] = championCombat.possessive + championCombat.title + moveDescription;

            }
            else
            {

                moveDescription = opponentCombat.abilities[useMove.ability].onTry;

                if (useMove.ability is BattleAbility.battleabilities.item)
                {

                    moveDescription = ItemDescription();

                }

                component.text[0] = opponentCombat.possessive + opponentCombat.title + moveDescription;

            }

            components.Add(0, component);

            return components;

        }

        public string ItemDescription()
        {

            BattleMove useMove = moveStack.First().Value;

            switch (useMove.item)
            {
                default:
                case HerbalHandle.herbals.satius_ligna:
                    return Mod.instance.Helper.Translation.Get("BattleHandle.388.894");

                case HerbalHandle.herbals.satius_impes:
                    return Mod.instance.Helper.Translation.Get("BattleHandle.388.897");

                case HerbalHandle.herbals.satius_celeri:
                    return Mod.instance.Helper.Translation.Get("BattleHandle.388.899");

                case HerbalHandle.herbals.faeth:
                    return Mod.instance.Helper.Translation.Get("BattleHandle.390.1");

                case HerbalHandle.herbals.trophy_shroom:
                    return Mod.instance.Helper.Translation.Get("BattleHandle.390.2");

                case HerbalHandle.herbals.trophy_eye:
                    return Mod.instance.Helper.Translation.Get("BattleHandle.390.3");
            }

        }

        public Dictionary<int, ContentComponent> ApplyReadout()
        {

            Dictionary<int, ContentComponent> components = new();

            ContentComponent component = new(ContentComponent.contentTypes.battlereadout, readoutapply);

            component.readoutPace = 2;

            BattleMove useMove = moveStack.First().Value;

            if (useMove.champion)
            {

                component.text[0] = championCombat.DescribeMove(useMove);

            }
            else
            {

                component.text[0] = opponentCombat.DescribeMove(useMove);

            }

            components.Add(0, component);

            return components;

        }

        public Dictionary<int, ContentComponent> FinishReadout()
        {

            Dictionary<int, ContentComponent> components = new();

            ContentComponent component = new(ContentComponent.contentTypes.battlereadout, readoutfinish);

            component.readoutPace = 2;

            switch (winState)
            {

                default:
                case winstates.champion:

                    Game1.playSound(SpellHandle.Sounds.yoba.ToString());

                    component.readoutPace = 0;

                    if(champion is BattleDragon)
                    {

                        component.text[0] = championCombat.possessive + championCombat.title + Mod.instance.Helper.Translation.Get("BattleHandle.388.1075");

                    }
                    else
                    {

                        component.text[0] = championCombat.possessive + championCombat.title + Mod.instance.Helper.Translation.Get("BattleHandle.388.1075") +
                            Mod.instance.Helper.Translation.Get("BattleHandle.388.1076").Tokens(new { experience = opponentCombat.experience, bounty = opponentCombat.bounty + StringData.currency, });

                    }

                    break;

                case winstates.opponent:

                    Game1.playSound(SpellHandle.Sounds.ghost.ToString());

                    component.text[0] = championCombat.possessive + championCombat.title + Mod.instance.Helper.Translation.Get("BattleHandle.388.1080");

                    break;

            }


            components.Add(0, component);

            return components;

        }

        public Dictionary<int, ContentComponent> ForfeitReadout()
        {

            Game1.playSound(SpellHandle.Sounds.ghost.ToString());

            Dictionary<int, ContentComponent> components = new();

            ContentComponent component = new(ContentComponent.contentTypes.battlereadout, readoutforfeit);

            component.readoutPace = 2;

            if (championLoaded)
            {

                component.text[0] = championCombat.possessive + championCombat.title + Mod.instance.Helper.Translation.Get("BattleHandle.388.1095");

            }
            else
            {

                component.text[0] = Mod.instance.Helper.Translation.Get("BattleHandle.388.1099");

            }

            components.Add(0, component);

            return components;

        }

        public void DistributeRewards()
        {

            if (champion is not BattleDragon)
            {

                PalHandle.UpdateExp(champion.characterType, opponentCombat.experience);

                PalHandle.UpdateWins(champion.characterType, 1);

                PalHandle.UpdateLove(champion.characterType);

                Game1.player.Money += opponentCombat.bounty;

            }

            HerbalHandle.RandomHerbal(Game1.player.Position + new Vector2(32,-128));

        }

        // Move Management ======================================================

        public void OpponentMove()
        {

            switch (Mod.instance.randomIndex.Next(4))
            {

                default:
                case 0:
                    opponentCombat.chosen = BattleCombatant.battleoptions.attack;
                    break;

                case 1:
                    opponentCombat.chosen = BattleCombatant.battleoptions.block;
                    break;

                case 2:
                    opponentCombat.chosen = BattleCombatant.battleoptions.special;
                    break;

                case 3:
                    opponentCombat.chosen = BattleCombatant.battleoptions.tackle;
                    break;

            }

            opponentCombat.item = HerbalHandle.herbals.none;

        }

        public void CalculateMove()
        {

            // block -----------------------------------

            if (championCombat.chosen == BattleCombatant.battleoptions.block && 
                opponentCombat.chosen == BattleCombatant.battleoptions.block)
            {

                championCombat.chosen = BattleCombatant.battleoptions.staredown;

                opponentCombat.chosen = BattleCombatant.battleoptions.staredown;

            }

            // calculate -----------------------------------

            moveStack.Clear();

            List<BattleMove> championRecover = championCombat.AttemptRecover();

            List<BattleMove> opponentRecover = opponentCombat.AttemptRecover();

            if(championRecover.Count > 0)
            {

                foreach(BattleMove recoverMove in championRecover)
                {

                    moveStack[moveStack.Count] = recoverMove;

                }

            }

            if (opponentRecover.Count > 0)
            {

                foreach (BattleMove recoverMove in opponentRecover)
                {

                    moveStack[moveStack.Count] = recoverMove;

                }

            }

            BattleMove championUse = championCombat.CreateMove();

            BattleMove opponentUse = opponentCombat.CreateMove();

            // stack --------------------------------------------
            if (championUse.option == BattleCombatant.battleoptions.staredown)
            {

                if (championUse.speed > opponentUse.speed)
                {

                    moveStack[moveStack.Count] = championUse;

                    ProcessMove(championUse, championCombat, opponentUse, opponentCombat);

                    ProcessMove(opponentUse, opponentCombat, championUse, championCombat);

                }
                else
                {

                    moveStack[moveStack.Count] = opponentUse;

                    ProcessMove(opponentUse, opponentCombat, championUse, championCombat);

                    ProcessMove(championUse, championCombat, opponentUse, opponentCombat);

                }

            }
            else
            if (championUse.option == BattleCombatant.battleoptions.block)
            {

                opponentUse.block = true;

                moveStack[moveStack.Count] = opponentUse;

                ProcessMove(opponentUse, opponentCombat, championUse, championCombat);

                ProcessMove(championUse, championCombat, opponentUse, opponentCombat);

            }
            else
            if (opponentUse.option == BattleCombatant.battleoptions.block)
            {

                championUse.block = true;

                moveStack[moveStack.Count] = championUse;

                ProcessMove(championUse, championCombat, opponentUse, opponentCombat);

                ProcessMove(opponentUse, opponentCombat, championUse, championCombat);

            }
            else
            if (championUse.speed > opponentUse.speed)
            {

                moveStack[moveStack.Count] = championUse;

                ProcessMove(championUse, championCombat, opponentUse, opponentCombat);

                moveStack[moveStack.Count] = opponentUse;

                ProcessMove(opponentUse, opponentCombat, championUse, championCombat);

            }
            else
            {

                moveStack[moveStack.Count] = opponentUse;

                ProcessMove(opponentUse, opponentCombat, championUse, championCombat);

                moveStack[moveStack.Count] = championUse;

                ProcessMove(championUse, championCombat, opponentUse, opponentCombat);

            }


        }

        public void ProcessMove(BattleMove championUse, BattleCombatant championComb, BattleMove opponentUse, BattleCombatant opponentComb)
        {

            if (championUse.special)
            {

                if (championUse.effect > opponentUse.resist)
                {

                    championUse.hit = true;

                }
                else
                {

                    opponentComb.fatigue++;

                }

            }
            else
            {

                if (championUse.accuracy > opponentUse.defense)
                {

                    championUse.hit = true;

                }
                else
                {

                    opponentComb.fatigue++;

                }

            }

            if (championUse.buff != BattleBuff.battlebuffs.none)
            {

                if (championUse.effect > opponentUse.resist)
                {

                    BattleMove championDebuff = new();

                    championDebuff.ability = opponentComb.moveset[BattleCombatant.battleoptions.buff];

                    championDebuff.option = BattleCombatant.battleoptions.buff;

                    championDebuff.champion = opponentComb.champion;

                    championDebuff.reaction = true;

                    championDebuff.buff = championUse.buff;

                    championDebuff.hit = true;

                    moveStack[moveStack.Count] = championDebuff;

                    championComb.fatigue++;

                }

            }

            if (championUse.hit && championUse.harm > 0)
            {

                BattleMove championHarm = championComb.CreateMove(BattleCombatant.battleoptions.harm, true);

                championHarm.hit = true;

                championHarm.damage = championUse.damage * championUse.harm / 100;

                moveStack[moveStack.Count] = championHarm;

            }

            if (championUse.self)
            {
                
                return;

            }

            if (opponentUse.counter > 0)
            {

                BattleMove opponentCounter = opponentComb.CreateMove(BattleCombatant.battleoptions.counter, true);

                opponentCounter.hit = true;

                opponentCounter.damage = championUse.damage * opponentUse.counter / 100;

                moveStack[moveStack.Count] = opponentCounter;

                if (opponentCounter.buff != BattleBuff.battlebuffs.none)
                {

                    if (opponentCounter.effect > championUse.resist)
                    {

                        BattleMove counterDebuff = new();

                        counterDebuff.ability = opponentComb.moveset[BattleCombatant.battleoptions.buff];

                        counterDebuff.option = BattleCombatant.battleoptions.buff;

                        counterDebuff.champion = championComb.champion;

                        counterDebuff.reaction = true;

                        counterDebuff.buff = opponentCounter.buff;

                        counterDebuff.hit = true;

                        moveStack[moveStack.Count] = counterDebuff;

                    }

                }

            }

            if (championUse.absorb > 0)
            {

                BattleMove championAbsorb = championComb.CreateMove(BattleCombatant.battleoptions.absorb, true);

                championAbsorb.hit = true;

                if (championUse.self)
                {

                    championAbsorb.damage = opponentUse.damage * championUse.absorb / 100;

                }
                else
                {

                    championAbsorb.damage = championUse.damage * championUse.absorb / 100;

                }

                moveStack[moveStack.Count] = championAbsorb;

            }

        }

        public void ApplyMove()
        {

            BattleMove useMove = moveStack.First().Value;

            int championatm = championCombat.min;

            int opponentatm = opponentCombat.min;

            if (useMove.champion)
            {

                championCombat.PerformMove(useMove);

                opponentCombat.ReceiveMove(useMove);

            }
            else
            {
                opponentCombat.PerformMove(useMove);

                championCombat.ReceiveMove(useMove);

            }

            if(opponentCombat.min < opponentatm)
            {

                opponentFlashing = 60;

                opponent.fadeSet = opponent.fadeOut;

            }

            if (championCombat.min < championatm)
            {

                championFlashing = 60;

                champion.fadeSet = champion.fadeOut;

            }

            if (championCombat.min <= 0)
            {

                championCombat.min = 0;

                winState = winstates.opponent;

            }

            if (opponentCombat.min <= 0)
            {

                opponentCombat.min = 0;

                winState = winstates.champion;

            }

        }

        // Combatant Positioning ======================================================

        public void IntroPosition()
        {

            opponentOffset = new Vector2(0 - interfaceWidth + 192, 0);

            opponentDecrement = opponentOffset / 60;

            championOffset = new Vector2(interfaceWidth - 192, 0);

            championDecrement = championOffset / 60;

            opponentFade = 0.99f;

            championFade = 0.99f;

        }

        public void SummonAnimate()
        {

            Vector2 center = Vector2.Zero;

            if (Game1.activeClickableMenu is BattlePage bp)
            {

                center = new(bp.xPositionOnScreen + bp.width / 2, bp.yPositionOnScreen + bp.height / 2);

            }

            Rectangle rect = new(0, 160, 32, 32);

            TemporaryAnimatedSprite animation = new(0, 75, 10, 1, center + new Vector2(-416, 0), false, false)
            {

                sourceRect = rect,

                sourceRectStartingPos = new Vector2(0, rect.Y),

                texture = Mod.instance.iconData.warpTexture,

                scale = 5f,

                layerDepth = 1f,

                alpha = 0.65f,

            };

            battleAnimations.Add(animation);

            championFade = 0.99f;

        }

        public void EngageMove()
        {

            BattleMove useMove = moveStack.First().Value;

            Character.Character performer = useMove.champion ? champion : opponent;

            switch (useMove.option)
            {

                case BattleCombatant.battleoptions.attack:

                    MoveForward();

                    if (!useMove.hit)
                    {
                        MoveDodge();
                    }

                    performer.BattleMove(Character.Character.specials.sweep);

                    break;

                case BattleCombatant.battleoptions.special:

                    MoveForward();

                    if (!useMove.hit)
                    {
                        MoveDodge();
                    }

                    performer.BattleMove(Character.Character.specials.special);

                    break;

                case BattleCombatant.battleoptions.tackle:

                    MoveForward(0.5f);

                    if (!useMove.hit)
                    {
                        MoveDodge();
                    }

                    performer.BattleMove(Character.Character.specials.tackle);

                    break;
            }

            MoveAnimation();

        }

        public void MoveForward(float factor = 1f)
        {

            BattleMove useMove = moveStack.First().Value;

            float shortMove = 40 * factor;

            float longMove = 80 * factor;

            if (useMove.champion)
            {

                championDestination = new Vector2(128, -32);

                championIncrement = championDestination / shortMove;

                championDecrement = championDestination / longMove;

            }
            else
            {

                opponentDestination = new Vector2(-128, 32);

                opponentIncrement = opponentDestination / shortMove;

                opponentDecrement = opponentDestination / longMove;

            }

        }

        public void MoveDodge()
        {

            BattleMove useMove = moveStack.First().Value;

            if (useMove.champion)
            {

                opponentDestination = new Vector2(-64, -16);

                opponentIncrement = opponentDestination / 20;

                opponentDecrement = opponentDestination / 40;

            }
            else
            {

                championDestination = new Vector2(64, 16);

                championIncrement = championDestination / 20;

                championDecrement = championDestination / 40;

            }

        }

        public void MoveAnimation()
        {

            Vector2 center = Vector2.Zero;

            if (Game1.activeClickableMenu is BattlePage bp)
            {

                center = new(bp.xPositionOnScreen + bp.width / 2, bp.yPositionOnScreen + bp.height / 2);

            }

            BattleMove useMove = moveStack.First().Value;

            BattleAbility ability = useMove.champion ? championCombat.abilities[useMove.ability] : opponentCombat.abilities[useMove.ability];

            switch (ability.impact)
            {

                case IconData.impacts.none:

                    break;

                default:

                    Rectangle rect = new(0, Mod.instance.iconData.ImpactIndex(ability.impact) * 64, 64, 64);

                    TemporaryAnimatedSprite animation = new(0, 100, 8, 1, center - new Vector2(128, 192), false, false)
                    {

                        sourceRect = rect,

                        sourceRectStartingPos = new Vector2(rect.X, rect.Y),

                        texture = Mod.instance.iconData.ImpactSheet(ability.impact),

                        scale = 4f,

                        layerDepth = 1f,

                        alpha = 0.65f,

                        delayBeforeAnimationStart = 750,

                    };

                    battleAnimations.Add(animation);

                    break;

            }

            switch (ability.missile)
            {

                case MissileHandle.missiles.echo:

                    Rectangle rect = new(0, 0, 64, 64);

                    for (int i = 0; i < 4; i++)
                    {

                        TemporaryAnimatedSprite echo = new(0, 100, 5, 3, center - new Vector2(64, 128), false, false)
                        {
                            sourceRect = rect,

                            sourceRectStartingPos = new Vector2(rect.X, rect.Y),

                            texture = Mod.instance.iconData.echoTexture,

                            scale = 1f,

                            scaleChange = 0.003f,

                            layerDepth = 1f,

                            alpha = 0.65f,

                            motion = new Vector2(-0.096f, -0.096f),

                            delayBeforeAnimationStart = 500 + (500 * i),

                            timeBasedMotion = true,
                        };

                        battleAnimations.Add(echo);

                    }

                    break;

                case MissileHandle.missiles.bubbleecho:

                    Rectangle rect2 = new(0, 128, 64, 64);

                    for (int i = 0; i < 4; i++)
                    {

                        TemporaryAnimatedSprite bubble = new(0, 100, 5, 3, center - new Vector2(64, 128), false, false)
                        {
                            sourceRect = rect2,

                            sourceRectStartingPos = new Vector2(rect2.X, rect2.Y),

                            texture = Mod.instance.iconData.echoTexture,

                            scale = 1f,

                            scaleChange = 0.003f,

                            layerDepth = 1f,

                            alpha = 0.65f,

                            motion = new Vector2(-0.096f, -0.096f),

                            delayBeforeAnimationStart = 500 + (500 * i),

                            timeBasedMotion = true,
                        };

                        battleAnimations.Add(bubble);

                    }

                    break;

            }


        }

        public void OutroPosition()
        {

            if (winState == winstates.champion)
            {

                opponentDestination = new Vector2(interfaceWidth + 64, 0);

                opponentIncrement = opponentDestination / 180;

                opponentDecrement = opponentDestination / 180;

            }
            else if (winState == winstates.opponent)
            {

                championDestination = new Vector2(-64, 0);

                championIncrement = championDestination / 180;

                championDecrement = championDestination / 180;

            }

        }

    }

}
