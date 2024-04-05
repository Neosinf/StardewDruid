using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Cast.Fates;
using StardewDruid.Cast.Weald;
using StardewDruid.Dialogue;
using StardewDruid.Event;
using StardewDruid.Map;
using StardewDruid.Monster.Boss;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Characters;
using StardewValley.Monsters;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
    public class Jester : StardewDruid.Character.Character
    {

        public Texture2D iconTexture;

        public double luckTimer;

        public Jester()
        {
        }

        public Jester(Vector2 position, string map, string name = "Jester")
          : base(position, map, name)
        {
            
        }

        public override void LoadOut()
        {

            characterTexture = CharacterData.CharacterTexture(Name);

            iconTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Icons.png"));

            LoadBase();

            haltFrames = FrameSeries(32, 32, 0, 0, 1);

            walkFrames = FrameSeries(32, 32, 32, 0, 6, haltFrames);
            
            walkLeft = 1;

            walkRight = 4;

            dashFrames = FrameSeries(32, 32, 0, 128, 6);

            idleFrames = new()
            {
                [0] = new(){
                    new(0, 256, 32, 32),
                    new(32, 256, 32, 32),
                    new(64, 256, 32, 32),
                    new(96, 256, 32, 32),
                    new(128, 256, 32, 32),
                    new(160, 256, 32, 32),
                },

            };

            specialFrames = new()
            {

                [0] = new() { new(128, 192, 32, 32), },

                [1] = new() { new(128, 160, 32, 32), },

                [2] = new() { new(128, 128, 32, 32), },

                [3] = new() { new(128, 224, 32, 32), },

            };

            specialInterval = 30;

            specialCeiling = 1;

            specialFloor = 1;

            sweepFrames = FrameSeries(32, 32, 0, 128, 3);

            workFrames = new()
            {

                [0] = new() {new(0, 288, 32, 32),
                    new(32, 288, 32, 32),
                    new(64, 288, 32, 32),
                    new(32, 288, 32, 32),
                },

            };

            loadedOut = true;

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            if (characterTexture == null)
            {

                return;

            }

            Vector2 localPosition = getLocalPosition(Game1.viewport);

            float drawLayer = (float)StandingPixel.Y / 10000f;

            if (IsEmoting && !Game1.eventUp)
            {
                b.Draw(Game1.emoteSpriteSheet, localPosition - new Vector2(0, 160), new Microsoft.Xna.Framework.Rectangle(base.CurrentEmoteIndex * 16 % Game1.emoteSpriteSheet.Width, base.CurrentEmoteIndex * 16 / Game1.emoteSpriteSheet.Width * 16, 16, 16), Color.White, 0f, Vector2.Zero, 4f, SpriteEffects.None, drawLayer);
            }

            if (netStandbyActive.Value)
            {

                DrawStandby(b, localPosition, drawLayer);

                DrawShadow(b, localPosition, drawLayer);

                return;

            }
            else if (netHaltActive.Value)
            {

                b.Draw(
                    characterTexture,
                    localPosition - new Vector2(28, 56f),
                    haltFrames[netDirection.Value][0],
                    Color.White,
                    0f,
                    Vector2.Zero,
                    3.75f,
                    (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

            }
            else if (netDashActive.Value)
            {

                b.Draw(
                    characterTexture,
                    localPosition - new Vector2(28, 56f + dashHeight),
                    dashFrames[netDirection.Value][dashFrame],
                    Color.White,
                    0f,
                    Vector2.Zero,
                    3.75f,
                    (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

            }
            else if (netWorkActive.Value)
            {

                b.Draw(
                    characterTexture,
                    localPosition - new Vector2(24, 56f),
                    workFrames[0][specialFrame],
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    3.75f,
                    SpriteEffects.None,
                    drawLayer
                );

            }
            else if (netSpecialActive.Value)
            {

                b.Draw(
                    characterTexture,
                    localPosition - new Vector2(28, 56f),
                    specialFrames[netDirection.Value][0], 
                    Color.White, 
                    0.0f,
                    Vector2.Zero,
                    3.75f,
                    (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? (SpriteEffects)1 : 0, 
                    drawLayer
                );

            }
            else
            {

                if (TightPosition() && idleTimer > 0 && currentLocation.IsOutdoors)
                {

                    DrawStandby(b, localPosition, drawLayer);

                    DrawShadow(b, localPosition, drawLayer);

                    return;

                }

                b.Draw(
                    characterTexture,
                    localPosition - new Vector2(28, 56f),
                    walkFrames[netDirection.Value][moveFrame],
                    Color.White,
                    0f,
                    Vector2.Zero,
                    3.75f,
                    (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

            }

            DrawShadow(b, localPosition, drawLayer);

        }

        public void DrawStandby(SpriteBatch b, Vector2 localPosition, float drawLayer)
        {

            int chooseFrame = IdleFrame();

            b.Draw(
                characterTexture,
                localPosition - new Vector2(28, 56f),
                idleFrames[0][chooseFrame],
                Color.White,
                0f,
                Vector2.Zero,
                3.75f,
                flip || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                drawLayer
            );

            return;

        }

        public override Rectangle GetBoundingBox()
        {

            if (netDirection.Value % 2 == 0)
            {

                return new Rectangle((int)Position.X + 8, (int)Position.Y + 8, 48, 48);

            }

            return new Rectangle((int)Position.X - 16, (int)Position.Y + 8, 96, 48);

        }

        public override void UpdateMove()
        {

            base.UpdateMove();

            if(modeActive == mode.track)
            {

                if(Game1.currentGameTime.TotalGameTime.TotalSeconds > luckTimer)
                {

                    BuffEffects buffEffect = new();

                    buffEffect.LuckLevel.Set(1);

                    //Buff luckBuff = new("184656", source: "Jester of Fate", iconTexture: iconTexture, iconSheetIndex: 4, displaySource: "Jester of Fate", duration: 10000, displayName: "Jester's Luck", description: "Jester of Fate", effects: buffEffect);
                    Buff luckBuff = new("184656", source: "Jester of Fate", displaySource: "Jester of Fate", duration: 10000, displayName: "Jester's Luck", description: "Luck increased by companion", effects: buffEffect);

                    Mod.instance.trackRegister["Jester"].followPlayer.buffs.Apply(luckBuff);

                    luckTimer = Game1.currentGameTime.TotalGameTime.TotalSeconds + 10;

                }

            }

        }

        public override bool SpecialAttack(StardewValley.Monsters.Monster monster)
        {

            ResetActives();

            netSpecialActive.Set(true);

            specialTimer = 60;

            LookAtTarget(monster.Position, true);

            SpellHandle beam = new(currentLocation, monster.GetBoundingBox().Center.ToVector2(), Position, 2, 1, -1, Mod.instance.CombatDamage(), 3);

            beam.type = SpellHandle.spells.beam;

            Mod.instance.spellRegister.Add(beam);

            return true;

        }

        public override bool EngageDialogue()
        {
            
            if (!Mod.instance.dialogue.ContainsKey(nameof(Jester)))
            {

                Dictionary<string, StardewDruid.Dialogue.Dialogue> dialogue = Mod.instance.dialogue;

                StardewDruid.Dialogue.Jester jester = new StardewDruid.Dialogue.Jester();

                jester.npc = this;

                dialogue[nameof(Jester)] = jester;

            }

            if (netSceneActive.Value && Mod.instance.dialogue[nameof(Jester)].specialDialogue.Count == 0)
            {

                return false;

            }

            Mod.instance.dialogue[nameof(Jester)].DialogueApproach();

            return true;

        }

        public override void DealDamageToMonster(StardewValley.Monsters.Monster monster, int damage = -1, bool push = true)
        {
            
            base.DealDamageToMonster(monster, damage, push);
            
            if (Mod.instance.CurrentProgress < 25)
            {
            
                return;
            
            }

            if (!Mod.instance.eventRegister.ContainsKey("polymorph"))
            {

                if (!MonsterData.BossMonster(monster))
                {

                    Polymorph morph = new(Tile, monster);
                    
                    monster.Halt();
                    
                    monster.stunTime.Set(Math.Max(monster.stunTime.Value,4000));

                    morph.EventTrigger();

                    return;
                
                }

            }

            ApplyDazeEffect(monster);

        }

        public void ApplyDazeEffect(StardewValley.Monsters.Monster monster)
        {
            
            if (Mod.instance.eventRegister.ContainsKey("gravity"))
            {
                
                return;
            
            }

            List<int> source = new List<int>();
            
            for (int index = 0; index < 5; ++index)
            {
                string key = "daze" + index.ToString();

                if (!Mod.instance.eventRegister.ContainsKey(key))
                {
                    
                    source.Add(index);
                
                }
                else if ((Mod.instance.eventRegister[key] as Daze).victim == monster)
                {
                    
                    return;
                
                }

            }

            if (source.Count <= 0)
            {
                
                return;
            
            }

            Daze daze = new(Tile, monster, source.First<int>(),Mod.instance.CombatDamage());
            
            if (!MonsterData.BossMonster(monster))
            {
                
                monster.Halt();

                monster.stunTime.Set(4000);

            }

            daze.EventTrigger();

        }

        public override bool TargetWork()
        {

            if (currentLocation.characters.Count > 0 && !Mod.instance.eventRegister.ContainsKey("active"))
            {

                foreach (NPC rubVictim in currentLocation.characters)
                {

                    if (rubVictim is StardewValley.Monsters.Monster)
                    {
                        continue;
                    }

                    if (rubVictim is StardewDruid.Character.Character)
                    {
                        continue;
                    }

                    if (rubVictim is StardewDruid.Character.Dragon)
                    {
                        continue;
                    }

                    if (rubVictim is Pet)
                    {
                        continue;
                    }

                    if (!Game1.NPCGiftTastes.ContainsKey(rubVictim.Name))
                    {
                        continue;
                    }

                    if (Mod.instance.rite.Witnessed("Jester", rubVictim))
                    {
                        continue;
                    }

                    if (Vector2.Distance(rubVictim.Position, Position) < 480)
                    {

                        bool alreadyRubbed = Game1.player.hasPlayerTalkedToNPC(rubVictim.Name);

                        if (alreadyRubbed)
                        {

                            continue;

                        }

                        if (ModUtility.GroundCheck(currentLocation, rubVictim.Tile - new Vector2(1, 0), true) != "ground")
                        {

                            continue;

                        }

                        if (Game1.player.friendshipData.ContainsKey(rubVictim.Name))
                        {

                            Game1.player.friendshipData[rubVictim.Name].TalkedToToday = true;

                            Game1.player.changeFriendship(25, rubVictim);

                        }

                        rubVictim.Halt();

                        rubVictim.faceDirection(2);

                        Reaction.ReactTo(rubVictim, "Jester");

                        Position = rubVictim.Position - new Vector2(64, 0);

                        SettleOccupied();

                        ModUtility.AnimateQuickWarp(currentLocation, Position);

                        workVector = rubVictim.Position;

                        netWorkActive.Set(true);

                        netSpecialActive.Set(true);

                        specialTimer = 120;

                        return true;

                    }

                }

            }

            if (currentLocation is Farm farmLocation)
            {

                foreach (KeyValuePair<long, FarmAnimal> pair in farmLocation.animals.Pairs)
                {

                    if (pair.Value.wasPet.Value)
                    {

                        continue;

                    }

                    if (Vector2.Distance(pair.Value.Position, Position) >= 480)
                    {

                        continue;

                    }

                    if (ModUtility.GroundCheck(currentLocation, pair.Value.Tile - new Vector2(1, 0), true) != "ground")
                    {

                        continue;

                    }

                    pair.Value.Halt();

                    pair.Value.faceDirection(2);

                    ModUtility.PetAnimal(Game1.player, pair.Value);

                    Position = pair.Value.Position - new Vector2(64, 0);

                    SettleOccupied();

                    ModUtility.AnimateQuickWarp(currentLocation, Position);

                    workVector = pair.Value.Position;

                    netWorkActive.Set(true);

                    netSpecialActive.Set(true);

                    specialTimer = 120;

                    return true;

                }

            }

            if (currentLocation is AnimalHouse animalLocation)
            {

                foreach (KeyValuePair<long, FarmAnimal> pair in animalLocation.animals.Pairs)
                {
                    if (pair.Value.wasPet.Value)
                    {

                        continue;

                    }

                    if (Vector2.Distance(pair.Value.Position, Position) >= 480)
                    {

                        continue;

                    }

                    if (ModUtility.GroundCheck(currentLocation, pair.Value.Tile - new Vector2(1, 0), true) != "ground")
                    {

                        continue;

                    }

                    pair.Value.Halt();

                    pair.Value.faceDirection(2);

                    ModUtility.PetAnimal(Game1.player, pair.Value);

                    Position = pair.Value.Position - new Vector2(64, 0);

                    SettleOccupied();

                    ModUtility.AnimateQuickWarp(currentLocation, Position);

                    netWorkActive.Set(true);

                    netSpecialActive.Set(true);

                    specialTimer = 120;

                    return true;

                }

            }
            return false;

        }

    }

}
