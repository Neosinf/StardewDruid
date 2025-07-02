using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewValley;
using StardewValley.Monsters;
using StardewValley.Objects;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Data
{
    public static class NarratorData
    {

        public enum narrators
        {

            unknown,
            effigy,
            rustling,
            whispers,
            sighs,
            clericbat,
            murmurs,
            jellyking,
            firstfarmer,
            ladybeyond,
            sergeant,
            thug,
            shadowleader,
            revenant,
            captaindrowned,
            lesserdragon,
            jester,
            buffin,
            marlon,
            gunther,
            saurus,
            goblin,
            bandit,
            thanatoshi,
            shadowtin,
            dwarf,
            shadowcat,
            wizard,
            witch,
            dustchef,
            treasurethief,
            blackfeather,
            crowmother,
            wyrven,
            masayoshi,
            argyle,
            macarbi,
            carnivellion,
            wildbear,
            linus,
            doja,
            attendant,
            vesselgrief,
            vesselrage,
            aldebaran,
            cultists,
            honourguard,
            treasureguardian,
            bonewitch,
            peatwitch,
            moorwitch,

        }

        public static Dictionary<int, string> DialogueNarrators(string scene)
        {
            Dictionary<int, string> sceneNarrators = new();

            switch (scene)
            {

                case QuestHandle.approachEffigy:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.unknown),//Mod.instance.Helper.Translation.Get("DialogueData.601"),
                        [1] = NarratorTitle(narrators.effigy),//Mod.instance.Helper.Translation.Get("DialogueData.602"),
                    };

                    break;

                case QuestHandle.swordWeald:
                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.rustling),//Mod.instance.Helper.Translation.Get("DialogueData.610"),
                        [1] = NarratorTitle(narrators.whispers),//Mod.instance.Helper.Translation.Get("DialogueData.611"),
                        [2] = NarratorTitle(narrators.sighs),//Mod.instance.Helper.Translation.Get("DialogueData.612"),
                    };
                    break;

                case QuestHandle.challengeWeald:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.clericbat),//Mod.instance.Helper.Translation.Get("DialogueData.619"),
                    };

                    break;

                case QuestHandle.swordMists:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.murmurs),//Mod.instance.Helper.Translation.Get("DialogueData.628"),
                        [1] = NarratorTitle(narrators.ladybeyond),//Mod.instance.Helper.Translation.Get("DialogueData.629"),
                    };

                    break;

                case QuestHandle.questEffigy:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.effigy),//Mod.instance.Helper.Translation.Get("DialogueData.638"),
                        [1] = NarratorTitle(narrators.jellyking),//Mod.instance.Helper.Translation.Get("DialogueData.639"),
                        [2] = NarratorTitle(narrators.firstfarmer),//Mod.instance.Helper.Translation.Get("DialogueData.640"),
                        [3] = NarratorTitle(narrators.ladybeyond),// Mod.instance.Helper.Translation.Get("DialogueData.641"),
                    };

                    break;

                case QuestHandle.challengeMists:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.sergeant),//Mod.instance.Helper.Translation.Get("DialogueData.649"),
                        [1] = NarratorTitle(narrators.thug),//Mod.instance.Helper.Translation.Get("DialogueData.650"),
                        [2] = NarratorTitle(narrators.shadowleader),//Mod.instance.Helper.Translation.Get("DialogueData.651"),
                        [3] = NarratorTitle(narrators.effigy),//Mod.instance.Helper.Translation.Get("DialogueData.652"),
                        [4] = NarratorTitle(narrators.doja),//Mod.instance.Helper.Translation.Get("DialogueData.311.13"),
                    };

                    break;

                case QuestHandle.swordStars:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.revenant),//Mod.instance.Helper.Translation.Get("DialogueData.661"),
                    };

                    break;

                case QuestHandle.challengeStars:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.jellyking),//Mod.instance.Helper.Translation.Get("DialogueData.670"),
                        [1] = NarratorTitle(narrators.effigy),// Mod.instance.Helper.Translation.Get("DialogueData.671"),
                    };

                    break;

                case QuestHandle.challengeAtoll:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.captaindrowned),//Mod.instance.Helper.Translation.Get("DialogueData.680"),
                        [1] = NarratorTitle(narrators.effigy),//Mod.instance.Helper.Translation.Get("DialogueData.681"),
                    };

                    break;

                case QuestHandle.challengeDragon:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.lesserdragon),//Mod.instance.Helper.Translation.Get("DialogueData.690"),
                    };

                    break;

                case QuestHandle.swordFates:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.jester),//Mod.instance.Helper.Translation.Get("DialogueData.699"),
                    };

                    break;

                case QuestHandle.questJester:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.jester),//Mod.instance.Helper.Translation.Get("DialogueData.708"),
                        [1] = NarratorTitle(narrators.buffin),//Mod.instance.Helper.Translation.Get("DialogueData.709"),
                        [2] = NarratorTitle(narrators.marlon),//Mod.instance.Helper.Translation.Get("DialogueData.710"),
                        [3] = NarratorTitle(narrators.gunther),//Mod.instance.Helper.Translation.Get("DialogueData.711"),
                        [4] = NarratorTitle(narrators.saurus),//Mod.instance.Helper.Translation.Get("DialogueData.712"),
                    };

                    break;

                case QuestHandle.challengeFates:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.effigy),//Mod.instance.Helper.Translation.Get("DialogueData.721"),
                        [1] = NarratorTitle(narrators.jester),//Mod.instance.Helper.Translation.Get("DialogueData.722"),
                        [2] = NarratorTitle(narrators.buffin),//Mod.instance.Helper.Translation.Get("DialogueData.723"),
                        [3] = NarratorTitle(narrators.shadowleader),//Mod.instance.Helper.Translation.Get("DialogueData.724"),
                        [4] = NarratorTitle(narrators.sergeant),//Mod.instance.Helper.Translation.Get("DialogueData.725"),
                        [5] = NarratorTitle(narrators.goblin),//Mod.instance.Helper.Translation.Get("DialogueData.726"),
                        [6] = NarratorTitle(narrators.bandit),//Mod.instance.Helper.Translation.Get("DialogueData.727"),
                        //[7] = NarratorTitle(narrators.shadowtin),//Mod.instance.Helper.Translation.Get("DialogueData.728"),
                    };

                    break;

                case QuestHandle.swordEther:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.jester),//Mod.instance.Helper.Translation.Get("DialogueData.737"),
                        [1] = NarratorTitle(narrators.thanatoshi),//Mod.instance.Helper.Translation.Get("DialogueData.738"),
                    };

                    break;

                case QuestHandle.questShadowtin:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.shadowtin),//Mod.instance.Helper.Translation.Get("DialogueData.747"),
                        [1] = NarratorTitle(narrators.dwarf),//Mod.instance.Helper.Translation.Get("DialogueData.748"),
                        [2] = NarratorTitle(narrators.wizard),//Mod.instance.Helper.Translation.Get("DialogueData.749"),
                        [3] = NarratorTitle(narrators.witch),//Mod.instance.Helper.Translation.Get("DialogueData.750"),
                        [4] = NarratorTitle(narrators.shadowcat),//Mod.instance.Helper.Translation.Get("DialogueData.751"),
                        [5] = NarratorTitle(narrators.wizard),// Mod.instance.Helper.Translation.Get("DialogueData.752"),
                        [6] = NarratorTitle(narrators.wizard),//Mod.instance.Helper.Translation.Get("DialogueData.753"),
                        [7] = NarratorTitle(narrators.bandit),//Mod.instance.Helper.Translation.Get("DialogueData.754"),
                        [8] = NarratorTitle(narrators.goblin),// Mod.instance.Helper.Translation.Get("DialogueData.755"),

                    };

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("Nom0ri.RomRas"))
                    {

                        sceneNarrators[5] = NarratorTitle(narrators.witch);//Mod.instance.Helper.Translation.Get("DialogueData.339.5");
                        sceneNarrators[6] = NarratorTitle(narrators.witch);//Mod.instance.Helper.Translation.Get("DialogueData.339.6");
                    }

                    break;

                case QuestHandle.challengeEther:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.dustchef),//Mod.instance.Helper.Translation.Get("DialogueData.765"),
                    };

                    break;

                case QuestHandle.treasureChase:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.treasurethief),//Mod.instance.Helper.Translation.Get("DialogueData.774"),
                    };

                    break;

                case QuestHandle.treasureGuardian:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.treasureguardian),//Mod.instance.Helper.Translation.Get("DialogueData.774"),
                    };

                    break;

                case QuestHandle.questBlackfeather:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.blackfeather),//Mod.instance.Helper.Translation.Get("DialogueData.324.1"),
                        [1] = NarratorTitle(narrators.crowmother),//Mod.instance.Helper.Translation.Get("DialogueData.324.2"),
                        [2] = NarratorTitle(narrators.ladybeyond),//Mod.instance.Helper.Translation.Get("DialogueData.324.3"),
                        [3] = NarratorTitle(narrators.wyrven),//Mod.instance.Helper.Translation.Get("DialogueData.324.4"),
                        [4] = NarratorTitle(narrators.firstfarmer),//Mod.instance.Helper.Translation.Get("DialogueData.324.5"),
                        [5] = NarratorTitle(narrators.masayoshi),//Mod.instance.Helper.Translation.Get("DialogueData.324.6"),
                        [6] = NarratorTitle(narrators.thanatoshi),//Mod.instance.Helper.Translation.Get("DialogueData.324.7"),
                        [7] = NarratorTitle(narrators.captaindrowned),//Mod.instance.Helper.Translation.Get("DialogueData.324.8"),
                    };

                    break;

                case QuestHandle.questBuffin:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.buffin),//Mod.instance.Helper.Translation.Get("DialogueData.339.1.185"),
                        [1] = NarratorTitle(narrators.argyle),//Mod.instance.Helper.Translation.Get("DialogueData.339.1.186"),
                        [2] = NarratorTitle(narrators.jester),// Mod.instance.Helper.Translation.Get("DialogueData.339.1.187"),
                        [3] = NarratorTitle(narrators.macarbi),//Mod.instance.Helper.Translation.Get("DialogueData.339.1.188"),
                        [4] = NarratorTitle(narrators.blackfeather),//Mod.instance.Helper.Translation.Get("DialogueData.339.1.189"),
                        [5] = NarratorTitle(narrators.effigy),//Mod.instance.Helper.Translation.Get("DialogueData.339.1.190"),
                        [6] = NarratorTitle(narrators.shadowtin),//Mod.instance.Helper.Translation.Get("DialogueData.339.1.191"),
                        [7] = NarratorTitle(narrators.carnivellion),//Mod.instance.Helper.Translation.Get("DialogueData.339.1.192"),
                        [8] = NarratorTitle(narrators.wildbear),//Mod.instance.Helper.Translation.Get("DialogueData.339.1.193"),
                        [9] = NarratorTitle(narrators.linus),//Mod.instance.Helper.Translation.Get("DialogueData.339.1.194"),

                    };


                    break;


                case QuestHandle.questRevenant:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.shadowtin),//Mod.instance.Helper.Translation.Get("DialogueData.342.1.46"),
                        [1] = NarratorTitle(narrators.jester),//Mod.instance.Helper.Translation.Get("DialogueData.342.1.47"),
                        [2] = NarratorTitle(narrators.sergeant),//Mod.instance.Helper.Translation.Get("DialogueData.342.1.48"),
                        [3] = NarratorTitle(narrators.revenant),//Mod.instance.Helper.Translation.Get("DialogueData.342.1.49"),
                        [4] = NarratorTitle(narrators.doja),//Mod.instance.Helper.Translation.Get("DialogueData.342.1.50"),
                        [5] = NarratorTitle(narrators.bandit),//Mod.instance.Helper.Translation.Get("DialogueData.342.1.51"),
                        [6] = NarratorTitle(narrators.goblin),//Mod.instance.Helper.Translation.Get("DialogueData.342.1.52"),
                        [7] = NarratorTitle(narrators.marlon),//Mod.instance.Helper.Translation.Get("DialogueData.342.1.53"),
                        [8] = NarratorTitle(narrators.macarbi),//Mod.instance.Helper.Translation.Get("DialogueData.342.1.54"),
                        [9] = NarratorTitle(narrators.attendant),//Mod.instance.Helper.Translation.Get("DialogueData.342.1.55"),
                        [10] = NarratorTitle(narrators.vesselgrief),//Mod.instance.Helper.Translation.Get("DialogueData.342.1.56"),
                        [11] = NarratorTitle(narrators.thanatoshi),//Mod.instance.Helper.Translation.Get("DialogueData.342.1.57"),

                    };

                    break;

                case QuestHandle.challengeBones:

                    sceneNarrators = new()
                    {
                        [0] = NarratorTitle(narrators.effigy),//Mod.instance.Helper.Translation.Get("DialogueData.343.1.69"),
                        [1] = NarratorTitle(narrators.blackfeather),//Mod.instance.Helper.Translation.Get("DialogueData.343.1.70"),
                        [2] = NarratorTitle(narrators.wizard),//Mod.instance.Helper.Translation.Get("DialogueData.343.1.71"),
                        [3] = NarratorTitle(narrators.linus),//Mod.instance.Helper.Translation.Get("DialogueData.343.1.72"),
                        [4] = NarratorTitle(narrators.jester),//Mod.instance.Helper.Translation.Get("DialogueData.343.1.73"),
                        [5] = NarratorTitle(narrators.buffin),//Mod.instance.Helper.Translation.Get("DialogueData.343.1.74"),
                        [6] = NarratorTitle(narrators.shadowtin),//Mod.instance.Helper.Translation.Get("DialogueData.343.1.75"),
                        [7] = NarratorTitle(narrators.sighs),//Mod.instance.Helper.Translation.Get("DialogueData.343.1.76"),
                        [8] = NarratorTitle(narrators.rustling),//Mod.instance.Helper.Translation.Get("DialogueData.343.1.77"),
                        [9] = NarratorTitle(narrators.whispers),//Mod.instance.Helper.Translation.Get("DialogueData.343.1.78"),
                        [10] = NarratorTitle(narrators.vesselrage),//Mod.instance.Helper.Translation.Get("DialogueData.343.1.79"),
                        [11] = NarratorTitle(narrators.aldebaran),//Mod.instance.Helper.Translation.Get("DialogueData.346.1"),
                    };

                    if (Mod.instance.Helper.ModRegistry.IsLoaded("Nom0ri.RomRas"))
                    {

                        sceneNarrators[2] = NarratorTitle(narrators.witch);

                    }

                    break;

                case QuestHandle.swordHeirs:

                    sceneNarrators = new()
                    {

                        [0] = NarratorTitle(narrators.aldebaran),
                        [1] = NarratorTitle(narrators.ladybeyond),
                        [2] = NarratorTitle(narrators.crowmother),

                    };

                    break; 
                case QuestHandle.challengeMoors:

                    sceneNarrators = new()
                    {

                        [0] = NarratorTitle(narrators.aldebaran),
                        [1] = NarratorTitle(narrators.bonewitch),
                        [2] = NarratorTitle(narrators.honourguard),
                        [3] = NarratorTitle(narrators.peatwitch),
                        [4] = NarratorTitle(narrators.moorwitch),

                    };

                    break;

            }

            return sceneNarrators;

        }

        public static string NarratorTitle(narrators narrator)
        {

            switch (narrator)
            {

                case narrators.unknown: return Mod.instance.Helper.Translation.Get("NarratorData.361.1");
                case narrators.effigy: return Mod.instance.Helper.Translation.Get("NarratorData.361.2");
                case narrators.rustling: return Mod.instance.Helper.Translation.Get("NarratorData.361.3");
                case narrators.whispers: return Mod.instance.Helper.Translation.Get("NarratorData.361.4");
                case narrators.sighs: return Mod.instance.Helper.Translation.Get("NarratorData.361.5");
                case narrators.clericbat: return Mod.instance.Helper.Translation.Get("NarratorData.361.6");
                case narrators.murmurs: return Mod.instance.Helper.Translation.Get("NarratorData.361.7");
                case narrators.jellyking: return Mod.instance.Helper.Translation.Get("NarratorData.361.8");
                case narrators.firstfarmer: return Mod.instance.Helper.Translation.Get("NarratorData.361.9");
                case narrators.ladybeyond: return Mod.instance.Helper.Translation.Get("NarratorData.361.10");
                case narrators.sergeant: return Mod.instance.Helper.Translation.Get("NarratorData.361.11");
                case narrators.thug: return Mod.instance.Helper.Translation.Get("NarratorData.361.12");
                case narrators.shadowleader: return Mod.instance.Helper.Translation.Get("NarratorData.361.13");
                case narrators.revenant: return Mod.instance.Helper.Translation.Get("NarratorData.361.14");
                case narrators.captaindrowned: return Mod.instance.Helper.Translation.Get("NarratorData.361.15");
                case narrators.lesserdragon: return Mod.instance.Helper.Translation.Get("NarratorData.361.16");
                case narrators.jester: return Mod.instance.Helper.Translation.Get("NarratorData.361.17");
                case narrators.buffin: return Mod.instance.Helper.Translation.Get("NarratorData.361.18");
                case narrators.marlon: return Mod.instance.Helper.Translation.Get("NarratorData.361.19");
                case narrators.gunther: return Mod.instance.Helper.Translation.Get("NarratorData.361.20");
                case narrators.saurus: return Mod.instance.Helper.Translation.Get("NarratorData.361.21");
                case narrators.goblin: return Mod.instance.Helper.Translation.Get("NarratorData.361.22");
                case narrators.bandit: return Mod.instance.Helper.Translation.Get("NarratorData.361.23");
                case narrators.thanatoshi: return Mod.instance.Helper.Translation.Get("NarratorData.361.24");
                case narrators.shadowtin: return Mod.instance.Helper.Translation.Get("NarratorData.361.25");
                case narrators.dwarf: return Mod.instance.Helper.Translation.Get("NarratorData.361.26");
                case narrators.shadowcat: return Mod.instance.Helper.Translation.Get("NarratorData.361.27");
                case narrators.wizard: return Mod.instance.Helper.Translation.Get("NarratorData.361.28");
                case narrators.witch: return Mod.instance.Helper.Translation.Get("NarratorData.361.29");
                case narrators.dustchef: return Mod.instance.Helper.Translation.Get("NarratorData.361.30");
                case narrators.treasurethief: return Mod.instance.Helper.Translation.Get("NarratorData.361.31");
                case narrators.blackfeather: return Mod.instance.Helper.Translation.Get("NarratorData.361.32");
                case narrators.crowmother: return Mod.instance.Helper.Translation.Get("NarratorData.361.33");
                case narrators.wyrven: return Mod.instance.Helper.Translation.Get("NarratorData.361.34");
                case narrators.masayoshi: return Mod.instance.Helper.Translation.Get("NarratorData.361.35");
                case narrators.argyle: return Mod.instance.Helper.Translation.Get("NarratorData.361.36");
                case narrators.macarbi: return Mod.instance.Helper.Translation.Get("NarratorData.361.37");
                case narrators.carnivellion: return Mod.instance.Helper.Translation.Get("NarratorData.361.38");
                case narrators.wildbear: return Mod.instance.Helper.Translation.Get("NarratorData.361.39");
                case narrators.linus: return Mod.instance.Helper.Translation.Get("NarratorData.361.40");
                case narrators.doja: return Mod.instance.Helper.Translation.Get("NarratorData.361.41");
                case narrators.attendant: return Mod.instance.Helper.Translation.Get("NarratorData.361.42");
                case narrators.vesselgrief: return Mod.instance.Helper.Translation.Get("NarratorData.361.43");
                case narrators.vesselrage: return Mod.instance.Helper.Translation.Get("NarratorData.361.44");
                case narrators.aldebaran: return Mod.instance.Helper.Translation.Get("NarratorData.361.45");
                case narrators.cultists: return Mod.instance.Helper.Translation.Get("NarratorData.361.46");
                case narrators.honourguard: return Mod.instance.Helper.Translation.Get("NarratorData.361.47");
                case narrators.treasureguardian: return Mod.instance.Helper.Translation.Get("NarratorData.366.1");
                case narrators.bonewitch: return Mod.instance.Helper.Translation.Get("NarratorData.380.1");
                case narrators.peatwitch: return Mod.instance.Helper.Translation.Get("NarratorData.380.2");
                case narrators.moorwitch: return Mod.instance.Helper.Translation.Get("NarratorData.380.3");
            }

            return string.Empty;

        }



    }

}


