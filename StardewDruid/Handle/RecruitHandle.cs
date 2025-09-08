using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Handle
{

    public class RecruitHandle
    {

        public string name = string.Empty;

        public string display = string.Empty;

        public int level = 0;

        public Rite.Rites rite = Rite.Rites.none;

        public RecruitHandle()
        {

        }

        public static bool RecruitValid(NPC villager)
        {

            if(villager == null)
            {

                return false;

            }

            if (villager.Schedule != null)
            {

                return true;

            }

            Mod.instance.Monitor.Log(villager.Name, LogLevel.Debug);

            return RecruitHero(villager.Name);

        }

        public static bool RecruitFriendship(NPC villager)
        {

            if (!Game1.player.friendshipData.TryGetValue(villager.Name, out var value))
            {

                return false;

            }

            if (value.Points < 0)
            {

                return false;

            }

            return true;

        }

        public static bool RecruitHero(string name)
        {

            switch (name)
            {

                case "Wizard":
                case "Linus":
                case "Caroline":
                case "Clint":
                case "Gunther":
                case "GuntherSilvian":
                case "Marlon":
                case "MarlonFay":
                case "Krobus":
                case "Dwarf":
                case "Astarion":
                case "Sevinae":
                case "Lance":
                case "Sophia":

                    return true;

            }

            return false;

        }

        public static bool RecruitWitness(NPC witness)
        {

            if (!Context.IsMainPlayer)
            {

                return false;

            }

            /*if (Mod.instance.magic)
            {

                return false;

            }*/

            if (!RelicHandle.HasRelic(IconData.relics.druid_hieress))
            {

                return false;

            }

            if (!RecruitValid(witness))
            {

                return false;

            }

            if (!RecruitFriendship(witness))
            {

                if (!RecruitHero(witness.Name))
                {

                    return false;

                }

            }

            if (Mod.instance.save.recruits.Count >= 4)
            {

                Mod.instance.RegisterMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.361.2").Tokens(new { name = witness.Name, }), 0, true);

                return false;

            }

            foreach (KeyValuePair<CharacterHandle.characters, RecruitHandle> recruitData in Mod.instance.save.recruits)
            {

                if (recruitData.Value.name == witness.Name)
                {

                    Mod.instance.RegisterMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.377.5").Tokens(new { name = witness.Name, }), 0, true);

                    return false;

                }

            }

            if (witness.currentLocation == Game1.player.currentLocation)
            {

                Microsoft.Xna.Framework.Rectangle relicRect = IconData.RelicRectangles(IconData.relics.druid_hieress);

                TemporaryAnimatedSprite animation = new(0, 2000, 1, 1, witness.Position + new Microsoft.Xna.Framework.Vector2(2, -124f), false, false)
                {
                    sourceRect = relicRect,
                    sourceRectStartingPos = new(relicRect.X, relicRect.Y),
                    texture = Mod.instance.iconData.relicsTexture,
                    layerDepth = 900f,
                    delayBeforeAnimationStart = 175,
                    scale = 3f,

                };

                Game1.player.currentLocation.TemporarySprites.Add(animation);

            }

            Mod.instance.RegisterMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.361.1").Tokens(new { name = witness.Name, }), 0, true);

            List<CharacterHandle.characters> slots = new()
            {
                CharacterHandle.characters.recruit_one,
                CharacterHandle.characters.recruit_two,
                CharacterHandle.characters.recruit_three,
                CharacterHandle.characters.recruit_four,

            };

            foreach (CharacterHandle.characters c in slots)
            {

                if (Mod.instance.save.recruits.ContainsKey(c))
                {

                    continue;

                }

                Mod.instance.save.recruits[c] = new() { name = witness.Name, level = 1, rite = Rite.Rites.none, display = witness.displayName, };

                return true;

            }

            return false;

        }

        public static bool RecruitLoad(CharacterHandle.characters type)
        {

            if (!Context.IsMainPlayer)
            {

                return false;

            }

            RecruitHandle hero = Mod.instance.save.recruits[type];

            NPC witness = CharacterHandle.FindVillager(hero.name);

            if(witness == null)
            {

                return false;

            }

            if (!Mod.instance.characters.ContainsKey(type))
            {

                Mod.instance.save.recruits[type].display = witness.displayName;

                switch (hero.name)
                {

                    case "Wizard":

                        if (Mod.instance.Helper.ModRegistry.IsLoaded("Nom0ri.RomRas"))
                        {

                            Mod.instance.characters[type] = new Sorceress(type, witness);

                        }
                        else
                        {

                            Mod.instance.characters[type] = new Elementalist(type, witness);

                        }
                        break;

                    case "Linus":

                        Mod.instance.characters[type] = new Shapeshifter(type, witness);

                        break;

                    case "Caroline":

                        Mod.instance.characters[type] = new Caroline(type, witness);

                        break;

                    case "Clint":

                        Mod.instance.characters[type] = new Clint(type, witness);

                        break;

                    case "Gunther":
                    case "GuntherSilvian":

                        Mod.instance.characters[type] = new Archaeologist(type, witness);

                        break;

                    case "Marlon":
                    case "MarlonFay":

                        Mod.instance.characters[type] = new Veteran(type, witness);

                        break;

                    case "Dwarf":

                        Mod.instance.characters[type] = new Bombmaker(type, witness);

                        break;

                    case "Krobus":

                        Mod.instance.characters[type] = new Krobus(type, witness);

                        break;

                    case "Astarion":

                        Mod.instance.characters[type] = new Astarion(type, witness);

                        break;

                    case "Sevinae":

                        Mod.instance.characters[type] = new Sevinae(type, witness);

                        break;

                    case "Lance":

                        Mod.instance.characters[type] = new Lance(type, witness);

                        break;

                    case "Sophia":

                        Mod.instance.characters[type] = new Sophia(type, witness);

                        break;

                    default:

                        Mod.instance.characters[type] = new Recruit(type, witness);

                        break;

                }

                Mod.instance.characters[type].NewDay();

            }
            else
            {

                switch (Mod.instance.characters[type].modeActive)
                {

                    case Character.Character.mode.scene:
                    case Character.Character.mode.recruit:
                    case Character.Character.mode.track:

                        return false;

                }

                witness = (Mod.instance.characters[type] as Recruit).villager;

            }

            if (!RecruitValid(witness))
            {

                RecruitRemove(type);

                return false;

            }

            Mod.instance.dialogue[type] = new(type, witness);

            Mod.instance.characters[type].SwitchToMode(Character.Character.mode.recruit, Game1.player);

            return true;

        }

        public static void RecruitRemove(CharacterHandle.characters type)
        {

            if (Mod.instance.save.characters.ContainsKey(type))
            {

                Mod.instance.save.characters.Remove(type);

            }

            if (Mod.instance.characters.ContainsKey(type))
            {

                Character.Character entity = Mod.instance.characters[type];

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


        public static string RecruitTitle(string name)
        {

            switch (name)
            {

                case "Wizard":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.363.3"); //"Successor of Elements";

                case "Linus":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.363.4"); //"Successor of Wilds";

                case "Caroline":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.363.5");// "Frost Witch";

                case "Clint":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.363.6"); //"Hammer Lord";

                case "Gunther":
                case "GuntherSilvian":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.377.1"); //"Archaeology Enthusiast";

                case "Marlon":
                case "MarlonFay":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.377.2"); //"Seasoned Adventurer";

                case "Krobus":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.377.3");// "The Shadowman";

                case "Dwarf":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.377.4"); //"Explosives Expert";

                case "Astarion":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.388.1"); //"High Elven Rogue";

                case "Sevinae":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.392.1"); //"Faun Alchemist";

                case "Lance":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.397.1"); //"Monster Hunter";

                case "Sophia":

                    return Mod.instance.Helper.Translation.Get("RecruitHandle.400.1"); //"Blue Moon Avenger";

            }

            return Mod.instance.Helper.Translation.Get("CharacterHandle.386.9");

        }

        public static string RecruitSpecial(string name)
        {

            switch (name)
            {

                case "Wizard":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.386.1"); //"Successor of Elements";

                case "Linus":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.386.2"); //"Successor of Wilds";

                case "Caroline":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.386.3");// "Frost Witch";

                case "Clint":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.386.4"); //"Hammer Lord";

                case "Gunther":
                case "GuntherSilvian":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.386.5"); //"Archaeology Enthusiast";

                case "Marlon":
                case "MarlonFay":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.386.6"); //"Seasoned Adventurer";

                case "Krobus":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.386.7");// "The Shadowman";

                case "Dwarf":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.386.8"); //"Explosives Expert";

                case "Astarion":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.388.2"); //"Elven Rogue";

                case "Sevinae":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.392.2"); //"Faun Alchemist";

                case "Lance":

                    return Mod.instance.Helper.Translation.Get("CharacterHandle.397.2"); //"Monster Hunter";

                case "Sophia":

                    return Mod.instance.Helper.Translation.Get("RecruitHandle.400.2"); //"Blue Moon Avenger";

            }

            return Mod.instance.Helper.Translation.Get("CharacterHandle.386.10"); // Non-combatant

        }


        public static int UnitLevel(int experience)
        {

            int level = 1;

            if (experience > 100)
            {

                level = 5;

            }
            else if (experience > 75)
            {

                level = 4;

            }
            else if (experience > 50)
            {

                level = 3;

            }
            else if (experience > 25)
            {

                level = 2;

            }

            if (level >= 3)
            {

                if (Mod.instance.questHandle.IsGiven(QuestHandle.heirsOne))
                {

                    Mod.instance.questHandle.UpdateTask(QuestHandle.heirsOne, 1);

                }

            }

            return level;

        }

        public static string RecruitLevel(int level)
        {

            int real = UnitLevel(level);

            return StringData.Get(StringData.str.level, new { level = real });

        }

        public static int NextLevel(int level)
        {

            if (level > 100)
            {

                return -1;

            }
            else if (level > 75)
            {

                return 100;

            }
            else if (level > 50)
            {

                return 75;

            }
            else if (level > 25)
            {

                return 50;

            }

            return 25;

        }

        public static void LevelUpdate(CharacterHandle.characters character)
        {

            RecruitHandle hero = Mod.instance.save.recruits[character];

            if (RecruitHero(hero.name))
            {

                hero.level++;

            }

            switch (hero.level)
            {
                case 101:

                    Mod.instance.RegisterMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.377.14").Tokens(new { name = hero.display, }));

                    break;

                case 76:

                    Mod.instance.RegisterMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.377.13").Tokens(new { name = hero.display, }));

                    break;

                case 51:

                    Mod.instance.RegisterMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.377.12").Tokens(new { name = hero.display, }));

                    break;

                case 26:

                    Mod.instance.RegisterMessage(Mod.instance.Helper.Translation.Get("CharacterHandle.377.11").Tokens(new { name = hero.display, }));

                    break;


            }


        }

        public static string LoreOption(CharacterHandle.characters character)
        {

            if (!Mod.instance.save.recruits.ContainsKey(character))
            {

                return null;

            }

            RecruitHandle hero = Mod.instance.save.recruits[character];

            switch (hero.name)
            {
                case "Sevinae":
                case "Lance":
                case "Sophia":

                    return null;

            }

            if (hero.level > 50 && RecruitHero(hero.name))
            {

                return Mod.instance.Helper.Translation.Get("DialogueLore.377.1");

            }

            return null;

        }

        public static string LoreIntro(CharacterHandle.characters character)
        {

            if (!Mod.instance.save.recruits.ContainsKey(character))
            {

                return null;

            }

            RecruitHandle data = Mod.instance.save.recruits[character];

            string herotitle = RecruitTitle(data.name);

            string heroname = data.display;

            switch (data.name)
            {

                case "Wizard":

                    return Mod.instance.Helper.Translation.Get("DialogueLore.377.2").Tokens(new { name = heroname, title = herotitle, }); //"Successor of Elements";

                case "Linus":

                    return Mod.instance.Helper.Translation.Get("DialogueLore.377.3").Tokens(new { name = heroname, title = herotitle, }); //"Successor of Wilds";

                case "Caroline":

                    return Mod.instance.Helper.Translation.Get("DialogueLore.377.4").Tokens(new { name = heroname, title = herotitle, });// "Frost Witch";

                case "Clint":

                    return Mod.instance.Helper.Translation.Get("DialogueLore.377.5").Tokens(new { name = heroname, title = herotitle, }); //"Hammer Lord";

                case "Gunther":
                case "GuntherSilvian":

                    return Mod.instance.Helper.Translation.Get("DialogueLore.377.6").Tokens(new { name = heroname, title = herotitle, }); //"Archaeology Enthusiast";

                case "Marlon":
                case "MarlonFay":

                    return Mod.instance.Helper.Translation.Get("DialogueLore.377.7").Tokens(new { name = heroname, title = herotitle, }); //"Seasoned Adventurer";

                case "Krobus":

                    return Mod.instance.Helper.Translation.Get("DialogueLore.377.8").Tokens(new { name = heroname, title = herotitle, });// "The Shadowman";

                case "Dwarf":

                    return Mod.instance.Helper.Translation.Get("DialogueLore.377.9").Tokens(new { name = heroname, title = herotitle, }); //"Explosives Expert";

                case "Astarion":

                    return Mod.instance.Helper.Translation.Get("DialogueLore.388.1").Tokens(new { name = heroname, title = herotitle, }); //"Explosives Expert";

            }

            return null;

        }

        public static List<LoreStory> LoreStories(CharacterHandle.characters character)
        {

            List<LoreStory> list = new();

            if (!Mod.instance.save.recruits.ContainsKey(character))
            {

                return list;

            }

            RecruitHandle data = Mod.instance.save.recruits[character];

            if(data.level < 50)
            {

                return list;

            }

            switch (data.name)
            {

                case "Wizard":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Wizard_Self_1]);

                    break;

                case "Linus":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Linus_Self_1]);

                    break;

                case "Caroline":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Caroline_Self_1]);

                    break;

                case "Clint":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Clint_Self_1]);

                    break;

                case "Gunther":
                case "GuntherSilvian":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Gunther_Self_1]);

                    break;

                case "Marlon":
                case "MarlonFay":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Marlon_Self_1]);

                    break;

                case "Krobus":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Krobus_Self_1]);

                    break;

                case "Dwarf":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Dwarf_Self_1]);

                    break;

                case "Astarion":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Astarion_Self_1]);

                    break;
            }

            if(data.level < 100)
            {

                return list;

            }

            switch (data.name)
            {

                case "Wizard":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Wizard_Self_2]);

                    break;

                case "Linus":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Linus_Self_2]);

                    break;

                case "Caroline":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Caroline_Self_2]);

                    break;

                case "Clint":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Clint_Self_2]);

                    break;

                case "Gunther":
                case "GuntherSilvian":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Gunther_Self_2]);

                    break;

                case "Marlon":
                case "MarlonFay":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Marlon_Self_2]);

                    break;

                case "Krobus":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Krobus_Self_2]);

                    break;

                case "Dwarf":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Dwarf_Self_2]);

                    break;

                case "Astarion":

                    list.Add(Mod.instance.questHandle.lores[LoreStory.stories.Astarion_Self_2]);

                    break;

            }

            return list;

        }

    }

}
