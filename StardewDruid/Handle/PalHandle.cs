using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StardewDruid.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley;
using System.Reflection.Metadata;
using StardewValley.Menus;
using StardewDruid.Battle;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewModdingAPI;

namespace StardewDruid.Handle
{

    public class PalHandle
    {

        public static StardewDruid.Character.Character PalInstance(CharacterHandle.characters type, int scheme, int level)
        {

            Character.Character load;

            switch (type)
            {
                default:

                    load = new FriendBat(CharacterHandle.characters.PalBat, scheme, level);

                    break;

                case CharacterHandle.characters.PalSlime:

                    load = new FriendSlime(CharacterHandle.characters.PalSlime, scheme, level);

                    break;

                case CharacterHandle.characters.PalSpirit:

                    load = new FriendFiend(CharacterHandle.characters.PalSpirit, scheme, level);

                    break;

                case CharacterHandle.characters.PalGhost:

                    load = new FriendGhost(CharacterHandle.characters.PalGhost, scheme, level);

                    break;

                case CharacterHandle.characters.PalSerpent:

                    load = new FriendSerpent(CharacterHandle.characters.PalSerpent, scheme, level);

                    break;

            }

            return load;

        }

        public static PalData DataFromRelic(IconData.relics relic)
        {

            CharacterHandle.characters entity = CharacterHandle.characters.none;

            switch (relic)
            {

                case IconData.relics.monster_bat:

                    entity = CharacterHandle.characters.PalBat; break;

                case IconData.relics.monster_slime:

                    entity = CharacterHandle.characters.PalSlime; break;

                case IconData.relics.monster_spirit:

                    entity = CharacterHandle.characters.PalSpirit; break;

                case IconData.relics.monster_ghost:

                    entity = CharacterHandle.characters.PalGhost; break;

                case IconData.relics.monster_serpent:

                    entity = CharacterHandle.characters.PalSerpent; break;

            }

            if (Mod.instance.save.pals.ContainsKey(entity))
            {

                return Mod.instance.save.pals[entity];

            }

            return Mod.instance.save.pals.ElementAt(Mod.instance.randomIndex.Next(Mod.instance.save.pals.Count)).Value;

        }

        public static IconData.relics PalRelic(CharacterHandle.characters type)
        {

            IconData.relics stone = IconData.relics.monster_bat;

            switch (type)
            {

                case CharacterHandle.characters.PalSlime:

                    stone = IconData.relics.monster_slime;

                    break;

                case CharacterHandle.characters.PalSpirit:

                    stone = IconData.relics.monster_spirit;

                    break;

                case CharacterHandle.characters.PalGhost:

                    stone = IconData.relics.monster_ghost;

                    break;

                case CharacterHandle.characters.PalSerpent:

                    stone = IconData.relics.monster_serpent;

                    break;
            }

            return stone;

        }

        public static CharacterHandle.characters CharacterType(CharacterHandle.characters pal, int scheme)
        {

            switch (pal)
            {

                default:

                case CharacterHandle.characters.PalBat:

                    switch (scheme)
                    {
                        default:
                            return CharacterHandle.characters.Bat;
                        case 1:
                            return CharacterHandle.characters.BrownBat;
                        case 2:
                            return CharacterHandle.characters.RedBat;
                    }

                case CharacterHandle.characters.PalSlime:

                    switch (scheme)
                    {
                        default:
                            return CharacterHandle.characters.Jellyfiend;
                        case 1:
                            return CharacterHandle.characters.BlueJellyfiend;
                        case 2:
                            return CharacterHandle.characters.PinkJellyfiend;
                    }

                case CharacterHandle.characters.PalSpirit:

                    switch (scheme)
                    {
                        default:
                            return CharacterHandle.characters.Dustfiend;
                        case 1:
                            return CharacterHandle.characters.Firefiend;
                        case 2:
                            return CharacterHandle.characters.Etherfiend;
                    }

                case CharacterHandle.characters.PalGhost:

                    switch (scheme)
                    {
                        default:
                            return CharacterHandle.characters.Spectre;
                        case 1:
                            return CharacterHandle.characters.WhiteSpectre;
                    }

                case CharacterHandle.characters.PalSerpent:

                    switch (scheme)
                    {
                        default:
                            return CharacterHandle.characters.Serpent;
                        case 1:
                            return CharacterHandle.characters.RiverSerpent;
                        case 2:
                            return CharacterHandle.characters.NightSerpent;
                        case 3:
                            return CharacterHandle.characters.LavaSerpent;
                    }

            }

        }

        public static Microsoft.Xna.Framework.Rectangle CharacterSource(CharacterHandle.characters pal)
        {

            switch (pal)
            {

                default:

                case CharacterHandle.characters.PalBat:

                    return new(0, 0, 32, 32);

                case CharacterHandle.characters.PalSlime:

                    return new(144, 48, 48, 48);

                case CharacterHandle.characters.PalSpirit:

                    return new(0, 0, 32, 32);

                case CharacterHandle.characters.PalGhost:

                    return new(0, 0, 32, 32);

                case CharacterHandle.characters.PalSerpent:

                    return new(0, 0, 64, 32);

            }

        }

        public static Microsoft.Xna.Framework.Rectangle PalFace(CharacterHandle.characters pal)
        {

            switch (pal)
            {

                default:
                case CharacterHandle.characters.PalBat:

                    return new Microsoft.Xna.Framework.Rectangle(0, 0, 32, 32);

                case CharacterHandle.characters.PalSlime:

                    return new Microsoft.Xna.Framework.Rectangle(0, 0, 32, 32);

                case CharacterHandle.characters.PalSpirit:

                    return new Microsoft.Xna.Framework.Rectangle(0, 0, 32, 32);

                case CharacterHandle.characters.PalGhost:

                    return new Microsoft.Xna.Framework.Rectangle(0, 0, 32, 32);

                case CharacterHandle.characters.PalSerpent:

                    return new Microsoft.Xna.Framework.Rectangle(32, 0, 32, 32);

            }

        }

        public static void LevelUpdate(CharacterHandle.characters pal)
        {

            if (Mod.instance.save.pals.ContainsKey(pal))
            {

                Mod.instance.save.pals[pal].experience++;

                UnitLevel(Mod.instance.save.pals[pal].experience);

            }

        }

        public static string PalName(CharacterHandle.characters pal)
        {

            if (Mod.instance.save.pals.ContainsKey(pal))
            {

                return Mod.instance.save.pals[pal].name;

            }

            return StardewValley.Dialogue.randomName();

        }

        public static string PalScheme(CharacterHandle.characters pal)
        {

            return PalTitle(pal, Mod.instance.save.pals[pal].scheme);

        }

        public static string PalTitle(CharacterHandle.characters pal, int scheme = 0)
        {

            switch (pal)
            {

                default:

                case CharacterHandle.characters.PalBat:

                    switch (scheme)
                    {
                        default:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.1");
                        case 1:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.2");
                        case 2:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.3");
                    }

                case CharacterHandle.characters.PalSlime:

                    switch (scheme)
                    {
                        default:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.4");
                        case 1:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.5");
                        case 2:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.6");
                    }

                case CharacterHandle.characters.PalSpirit:

                    switch (scheme)
                    {
                        default:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.7");
                        case 1:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.8");
                        case 2:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.9");
                    }

                case CharacterHandle.characters.PalGhost:

                    switch (scheme)
                    {
                        default:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.10");
                        case 1:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.11");
                    }

                case CharacterHandle.characters.PalSerpent:

                    switch (scheme)
                    {
                        default:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.12");
                        case 1:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.13");
                        case 2:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.14");
                        case 3:
                            return Mod.instance.Helper.Translation.Get("PalHandle.390.15");
                    }

            }

        }

        public static int UnitLevel(int experience)
        {

            int level = 1;

            if (experience >= 100)
            {

                level = 5;

            }
            else if (experience >= 60)
            {

                level = 4;

            }
            else if (experience >= 30)
            {

                level = 3;

            }
            else if (experience >= 10)
            {

                level = 2;

            }

            if (level >= 3)
            {

                if (Mod.instance.questHandle.IsGiven(QuestHandle.captures))
                {

                    Mod.instance.questHandle.UpdateTask(QuestHandle.captures, 1);

                }

            }

            return level;

        }

        public static int LoveLevel(int love)
        {

            return Math.Min(1 + (int)(love / 10),10);

        }

        public static int NextLevel(int level)
        {

            if (level > 100)
            {

                return -1;

            }
            else if (level >= 60)
            {

                return 100;

            }
            else if (level >= 30)
            {

                return 60;

            }
            else if (level >= 10)
            {

                return 30;

            }

            return 10;

        }

        public static void CheckDefault()
        {

            if(Mod.instance.save.pals.Count == 0)
            {

                CharacterHandle.characters entity = (CharacterHandle.characters)((int)CharacterHandle.characters.PalBat + Mod.instance.randomIndex.Next(5));

                Mod.instance.save.pals[entity] = new PalData(entity);

            }

        }

        public static void Capture(CharacterHandle.characters entity, Vector2 position)
        {

            bool pocket = true;

            Mod.instance.iconData.AnimateQuickWarp(Game1.player.currentLocation, position, false, IconData.warps.capture);

            if (Mod.instance.save.pals.ContainsKey(entity))
            {

                Mod.instance.save.pals[entity].caught += 1;

                pocket = false;

            }
            else
            {

                Mod.instance.save.pals[entity] = new PalData(entity);

            }

            IconData.relics throwStone = IconData.relics.monster_bat;

            switch (entity)
            {

                case CharacterHandle.characters.PalSlime:

                    throwStone = IconData.relics.monster_slime;

                    break;

                case CharacterHandle.characters.PalSpirit:

                    throwStone = IconData.relics.monster_spirit;

                    break;

                case CharacterHandle.characters.PalGhost:

                    throwStone = IconData.relics.monster_ghost;

                    break;

                case CharacterHandle.characters.PalSerpent:

                    throwStone = IconData.relics.monster_serpent;

                    break;

            }

            ThrowHandle throwRelic = new(Game1.player, position, throwStone) { pocket = pocket, delay = 40, };

            throwRelic.register();

        }

        public static void ReceiveHelp(CharacterHandle.characters entity, int amount)
        {

            if (Mod.instance.save.pals.ContainsKey(entity))
            {

                Mod.instance.save.pals[entity].caught += 1;

            }

        }
        
        public static int BattleWins(CharacterHandle.characters type)
        {

            if (Mod.instance.save.pals.ContainsKey(type))
            {

                return Mod.instance.save.pals[type].wins;

            }

            return 0;

        }

        public static void UpdateExp(CharacterHandle.characters type, int update)
        {

            if (Mod.instance.save.pals.ContainsKey(type))
            {

                Mod.instance.save.pals[type].experience += update;

            }

        }

        public static void UpdateWins(CharacterHandle.characters type, int update)
        {

            if (Mod.instance.save.pals.ContainsKey(type))
            {

                Mod.instance.save.pals[type].wins += update;

            }

        }

        public static void UpdateLove(CharacterHandle.characters type)
        {

            if (Mod.instance.save.pals.ContainsKey(type))
            {

                if (Mod.instance.save.pals[type].pettoday)
                {

                    return;

                }

                Mod.instance.save.pals[type].love += 5;

                Mod.instance.save.pals[type].pettoday = true;

            }

        }

        public static void ReceivePotion(CharacterHandle.characters type, HerbalHandle.herbals potion, bool consume = true)
        {

            if (Mod.instance.save.pals.ContainsKey(type))
            {

                Mod.instance.save.pals[type].BoostStat(potion, consume);

            }

        }

        public static int HealthLevel(CharacterHandle.characters type, int level, int health)
        {

            switch (type)
            {

                default:

                case CharacterHandle.characters.PalBat:

                    return level * 10 + health;

                case CharacterHandle.characters.PalSlime:

                    return level * 20 + health;

                case CharacterHandle.characters.PalSpirit:

                    return level * 10 + health;

                case CharacterHandle.characters.PalGhost:

                    return level * 15 + health;

                case CharacterHandle.characters.PalSerpent:

                    return level * 15 + health;

            }

        }

        public static int AttackLevel(CharacterHandle.characters type, int level, int attack)
        {

            switch (type)
            {

                default:

                case CharacterHandle.characters.PalBat:

                    return level * 10 + attack;

                case CharacterHandle.characters.PalSlime:

                    return level * 15 + attack;

                case CharacterHandle.characters.PalSpirit:

                    return level * 10 + attack;

                case CharacterHandle.characters.PalGhost:

                    return level * 10 + attack;

                case CharacterHandle.characters.PalSerpent:

                    return level * 20 + attack;

            }

        }

        public static int SpeedLevel(CharacterHandle.characters type, int level, int speed)
        {

            switch (type)
            {

                default:

                case CharacterHandle.characters.PalBat:

                    return level * 15 + speed;

                case CharacterHandle.characters.PalSlime:

                    return speed;

                case CharacterHandle.characters.PalSpirit:

                    return level * 20 + speed;

                case CharacterHandle.characters.PalGhost:

                    return level * 10 + speed;

                case CharacterHandle.characters.PalSerpent:

                    return level * 5 + speed;

            }

        }

        public static int ResistLevel(CharacterHandle.characters type, int level, int resist)
        {

            switch (type)
            {

                default:

                case CharacterHandle.characters.PalBat:

                    return (level * 10) + resist;

                case CharacterHandle.characters.PalSlime:

                    return resist;

                case CharacterHandle.characters.PalSpirit:

                    return (level * 15) + resist;

                case CharacterHandle.characters.PalGhost:

                    return (level * 25) + resist;

                case CharacterHandle.characters.PalSerpent:

                    return (level * 5) + resist;

            }

        }

        public static Dictionary<BattleCombatant.battleoptions, BattleAbility.battleabilities> SpecialMoves(Dictionary<BattleCombatant.battleoptions, BattleAbility.battleabilities> moves,CharacterHandle.characters type, int level)
        {

            switch (type)
            {

                default:
                case CharacterHandle.characters.PalBat:

                    for(int i = 1; i <= level; i++)
                    {

                        switch (i)
                        {

                            case 1:
                                moves[BattleCombatant.battleoptions.attack] = BattleAbility.battleabilities.wing;
                                break;

                            case 3:
                                moves[BattleCombatant.battleoptions.tackle] = BattleAbility.battleabilities.batdive;
                                break;

                            case 5:
                                moves[BattleCombatant.battleoptions.special] = BattleAbility.battleabilities.screech;
                                break;

                        }

                    }

                    break;

                case CharacterHandle.characters.PalSlime:

                    for (int i = 1; i <= level; i++)
                    {

                        switch (i)
                        {

                            case 1:
                                moves[BattleCombatant.battleoptions.block] = BattleAbility.battleabilities.absorption;
                                break;

                            case 3:
                                moves[BattleCombatant.battleoptions.special] = BattleAbility.battleabilities.splatter;
                                break;

                            case 5:
                                moves[BattleCombatant.battleoptions.tackle] = BattleAbility.battleabilities.bodyslam;
                                break;

                        }

                    }

                    break;

                case CharacterHandle.characters.PalSpirit:

                    for (int i = 1; i <= level; i++)
                    {

                        switch (i)
                        {

                            case 1:
                                moves[BattleCombatant.battleoptions.special] = BattleAbility.battleabilities.explode;
                                break;

                            case 3:
                                moves[BattleCombatant.battleoptions.attack] = BattleAbility.battleabilities.dustup;
                                break;

                            case 5:
                                moves[BattleCombatant.battleoptions.block] = BattleAbility.battleabilities.smoke;
                                moves[BattleCombatant.battleoptions.counter] = BattleAbility.battleabilities.smokecounter;
                                break;

                        }

                    }

                    break;


                case CharacterHandle.characters.PalGhost:

                    for (int i = 1; i <= level; i++)
                    {

                        switch (i)
                        {

                            case 1:
                                moves[BattleCombatant.battleoptions.attack] = BattleAbility.battleabilities.whisper;
                                break;

                            case 3:
                                moves[BattleCombatant.battleoptions.block] = BattleAbility.battleabilities.ghostform;
                                break;

                            case 5:
                                moves[BattleCombatant.battleoptions.special] = BattleAbility.battleabilities.scares;
                                break;

                        }

                    }

                    break;

                case CharacterHandle.characters.PalSerpent:

                    for (int i = 1; i <= level; i++)
                    {

                        switch (i)
                        {

                            case 1:
                                moves[BattleCombatant.battleoptions.attack] = BattleAbility.battleabilities.tail;
                                break;

                            case 3:
                                moves[BattleCombatant.battleoptions.special] = BattleAbility.battleabilities.breath;
                                break;

                            case 5:
                                moves[BattleCombatant.battleoptions.tackle] = BattleAbility.battleabilities.dragondive;
                                break;

                        }

                    }
                    break;


            }

            return moves;

        }

    }

}
