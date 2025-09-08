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
using StardewDruid.Cast;
using StardewModdingAPI;
using System.IO;
using StardewValley.Tools;
using StardewDruid.Dialogue;
using StardewDruid.Handle;

namespace StardewDruid.Data
{

    public class PalData
    {

        public string name;

        public int experience;

        public int love;

        public int stage;

        public CharacterHandle.characters type = CharacterHandle.characters.PalBat;

        public int scheme;

        public int health;

        public int attack;

        public int speed;

        public int resist;

        public int caught;

        public int hired;

        public int wins;

        // transient

        public bool fedtoday;

        public bool pettoday;

        public PalData()
        {

        }

        public PalData(CharacterHandle.characters entity)
        {

            type = entity;

            name = StardewValley.Dialogue.randomName();

            caught = 1;

        }

        public void Initiate()
        {

            fedtoday = false;

            pettoday = false;

            Mod.instance.relicHandle.ReliquaryUpdate(PalHandle.PalRelic(type).ToString());

            PalLoad(CharacterHandle.CharacterSaveMode(type));

        }

        public void PalLoad(Character.Character.mode mode = Character.Character.mode.home)
        {
            
            if (!Context.IsMainPlayer)
            {

                if (Mod.instance.dopplegangers.ContainsKey(type))
                {

                    if (Mod.instance.dopplegangers[type].modeActive != mode)
                    {

                        Mod.instance.dopplegangers[type].SwitchToMode(mode, Game1.player);

                    }

                    return;

                }

            }
            else
            if (Mod.instance.characters.ContainsKey(type))
            {

                if (Mod.instance.characters[type].modeActive != mode)
                {

                    Mod.instance.characters[type].SwitchToMode(mode, Game1.player);

                }

                return;

            }

            Character.Character load;

            int level = PalHandle.UnitLevel(experience);

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

                case CharacterHandle.characters.PalSerpent:

                    load = new FriendSerpent(CharacterHandle.characters.PalSerpent, scheme, level);

                    break;

                case CharacterHandle.characters.PalGhost:

                    load = new FriendGhost(CharacterHandle.characters.PalGhost, scheme, level);

                    break;

            }

            Mod.instance.dialogue[type] = new(type,load);

            if (!Context.IsMainPlayer)
            {

                Mod.instance.dopplegangers[type] = load;

            }
            else
            {

                Mod.instance.characters[type] = load;

            }

            load.NewDay();

            load.SwitchToMode(mode, Game1.player);

        }

        public void PalRemove()
        {


            if (!Context.IsMainPlayer)
            {

                if (Mod.instance.dopplegangers.ContainsKey(type))
                {

                    StardewDruid.Character.Character entity = Mod.instance.dopplegangers[type];

                    entity.ResetActives();

                    entity.ClearLight();

                    entity.RemoveCompanionBuff(Game1.player);

                    entity.netSceneActive.Set(false);

                    if (entity.currentLocation != null)
                    {

                        entity.currentLocation.characters.Remove(entity);

                    }

                    Mod.instance.dopplegangers.Remove(type);

                    Mod.instance.dialogue.Remove(type);

                    Mod.instance.trackers.Remove(type);

                }

            }
            else
            {

                if (Mod.instance.save.characters.ContainsKey(type))
                {

                    Mod.instance.save.characters.Remove(type);

                }

                if (Mod.instance.characters.ContainsKey(type))
                {

                    StardewDruid.Character.Character entity = Mod.instance.characters[type];

                    entity.ResetActives();

                    entity.ClearLight();

                    entity.RemoveCompanionBuff(Game1.player);

                    entity.netSceneActive.Set(false);

                    if (entity.currentLocation != null)
                    {

                        entity.currentLocation.characters.Remove(entity);

                    }

                    Mod.instance.characters.Remove(type);

                    Mod.instance.dialogue.Remove(type);

                    Mod.instance.trackers.Remove(type);

                }

            }

        }

        public void PalScheme()
        {

            scheme++;

            switch (type)
            {

                default:
                case CharacterHandle.characters.PalBat:
                case CharacterHandle.characters.PalSlime:
                case CharacterHandle.characters.PalSpirit:

                    if (scheme == 3)
                    {
                        scheme = 0;
                    }

                    break;

                case CharacterHandle.characters.PalGhost:

                    if (scheme == 2)
                    {
                        scheme = 0;
                    }

                    break;

                case CharacterHandle.characters.PalSerpent:

                    if (scheme == 4)
                    {
                        scheme = 0;
                    }

                    break;
            }

            if (!Context.IsMainPlayer)
            {

                if (Mod.instance.dopplegangers.ContainsKey(type))
                {

                    Character.Character.mode mode = Mod.instance.dopplegangers[type].modeActive;

                    PalRemove();

                    PalLoad(mode);

                }

            }
            else
            {

                if (Mod.instance.characters.ContainsKey(type))
                {

                    Character.Character.mode mode = Mod.instance.characters[type].modeActive;

                    PalRemove();

                    PalLoad(mode);

                }

            }

        }

        public int BoostStat(ApothecaryHandle.items potion, bool consume = true)
        {

            if (fedtoday)
            {

                return 0;

            }

            ApothecaryHandle.items best = Mod.instance.apothecaryHandle.BestHerbal(potion);

            if (best == ApothecaryHandle.items.none)
            {

                return 0;

            }

            ApothecaryItem data = Mod.instance.apothecaryHandle.apothecary[best];

            int boost = data.level + 1;

            switch (potion)
            {

                default:
                case ApothecaryHandle.items.ligna:

                    if (health >= 50)
                    {
                        experience += 5;

                        return -1;

                    }

                    health = Math.Min(health + boost, 50);

                    break;

                case ApothecaryHandle.items.vigores:

                    if (attack >= 50)
                    {
                        experience += 5;

                        return -1;

                    }

                    attack = Math.Min(attack + boost, 50);

                    break;

                case ApothecaryHandle.items.celeri:

                    if(speed >= 50)
                    {

                        experience += 5;

                        return -1;

                    }

                    speed = Math.Min(speed + boost, 50);

                    break;

                case ApothecaryHandle.items.faeth:

                    if (resist >= 50)
                    {

                        experience += 5;

                        return -1;

                    }

                    boost = 5;

                    resist = Math.Min(resist + boost, 50);

                    break;

            }

            if (consume)
            {

                ApothecaryHandle.UpdateAmounts(best, 0 - 1);

            }

            DisplayMessage hudmessage = new(Mod.instance.Helper.Translation.Get("HerbalData.386.35").Tokens(new { potion = data.title, monster = name }), data);

            Game1.addHUDMessage(hudmessage);

            fedtoday = true;

            if (!pettoday)
            {

                love += 5;

                pettoday = true;

            }

            return boost;

        }

        public StardewDruid.Character.Character BattleVersion()
        {

            int level = PalHandle.UnitLevel(experience);

            return PalHandle.PalInstance(type, scheme, level);

        }


    }

}
