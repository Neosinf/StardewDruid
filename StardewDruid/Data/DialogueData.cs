using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewValley;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Data
{
    public static class DialogueData
    {

        public static Dictionary<int, Dictionary<int, string>> DialogueScene(string scene)
        {

            Dictionary<int, Dictionary<int, string>> sceneDialogue = new();

            switch (scene)
            {
                case QuestHandle.approachEffigy:
                    
                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.796"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.797"), },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.798"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.799"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.800"), },
                        [6] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.801"), },

                        [900] = new()
                        {
                            [999] = Mod.instance.Helper.Translation.Get("DialogueData.802").Tokens(new { button = Mod.instance.Config.riteButtons.ToString() }),
                        },
                    };

                    break;

                case QuestHandle.swordWeald:

                    sceneDialogue = new()
                    {

                        [1] = new()
                        {
                            [0] = Mod.instance.Helper.Translation.Get("DialogueData.816"),
                            [1] = Mod.instance.Helper.Translation.Get("DialogueData.817"),
                            [2] = Mod.instance.Helper.Translation.Get("DialogueData.818"),
                        },
                        [2] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.819"), },
                        [3] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.820"), },
                        [4] = new()
                        {
                            [0] = Mod.instance.Helper.Translation.Get("DialogueData.821"),
                            //[1] = Mod.instance.Helper.Translation.Get("DialogueData.822"),
                            //[2] = Mod.instance.Helper.Translation.Get("DialogueData.823"),
                        },

                    };

                    break;


                case QuestHandle.challengeWeald:

                    sceneDialogue = new()
                    {

                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.835"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.836"), },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.837"), },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.838"), },
                        [14] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.839"), },
                        [17] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.840"), },
                        [20] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.841"), },
                        [23] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.842"), },

                        [41] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.843"), },
                        [44] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.844"), },
                        [47] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.845"), },

                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.846"), },
                        [63] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.847"), },
                        [69] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.848"), },
                        [72] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.849"), },

                        [900] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.851"), },
                    };

                    break;

                case QuestHandle.swordMists:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.861"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.862"), },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.863"), },
                        [4] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.864"), },

                    };

                    break;

                case QuestHandle.questEffigy:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.875") },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.876") },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.877") },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.878") },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.879") },
                        [6] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.880") },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.881") },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.882") },
                        [9] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.883") },
                        [10] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.884") },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.885") },
                        [12] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.886") },
                        [13] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.887") },
                        [14] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.888") },
                        [15] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.889") },

                        [16] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.891") },
                        [17] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.892") },
                        [18] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.893") },
                        [19] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.894") },
                        [20] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.895") },
                        [21] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.896") },
                        [22] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.897") },
                        [23] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.898") },
                        [24] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.899") },

                        [25] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.901") },
                        [26] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.902") },
                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.903") },
                        [28] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.904") },
                        [29] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.905") },
                        [30] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.906") },

                        [31] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.908") },
                        [32] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.909") },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.910") },
                        [34] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.911") },
                        [35] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.912") },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.913") },
                        [37] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.914") },

                        [502] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.916") },
                        [506] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.917") },
                        [508] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.918"), },
                        [511] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.919"), },
                        [514] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.920"), },
                        [517] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.921"), },
                        [520] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.922"), },
                        [523] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.923"), },
                        [526] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.924"), },
                        [529] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.925"), },
                        [532] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.926"), },
                        [535] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.927"), },
                        [538] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.928"), },
                        [541] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.929"), },
                        [544] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.930"), },
                        [547] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.931"), },
                        [550] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.932"), },
                        [553] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.933"), },
                        [556] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.934"), },
                        [559] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.935"), },
                        [561] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.936"), },
                        [564] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.937"), },
                        [567] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.938"), },

                        [777] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.940"), }
                    };

                    break;

                case QuestHandle.challengeMists:

                    sceneDialogue = new()
                    {
                        [3] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.311.1"), },
                        [6] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.311.2"), },
                        [9] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.311.3"), },
                        [12] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.311.4"), },
                        [15] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.311.5"), },
                        [18] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.311.6"), },
                        [21] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.311.7"), },
                        [24] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.311.8"), },

                        [101] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.950"), },
                        [104] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.951"), },
                        [107] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.311.9"), },
                        [110] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.311.10"), },

                        [221] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.954"), },
                        [224] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.957"), },
                        [229] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.959"), },
                        [232] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.960"), },
                        [246] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.963"), },
                        [249] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.964"), },
                        [252] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.965"), },
                        [255] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.967"), },

                        [301] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.968"), },
                        [302] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.969"), },
                        [304] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.970"), },
                        [307] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.971"), },
                        [310] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.972"), },
                        [313] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.973"), },
                        [316] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.974"), },
                        [319] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.975"), },

                        [900] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.977"), },
                        [910] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.311.14"), },
                        // loading
                        [901] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.966"), },
                        [902] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.953"), },
                        [903] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.311.11"), },
                        // cancelling
                        [904] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.962"), },
                        [905] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.958"), },
                        [906] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.955"), },
                        // firing
                        [907] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.978"), },
                        [908] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.979"), },
                        [909] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.311.12"), },

                    };

                    break;

                case QuestHandle.swordStars:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.990"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.991"), },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.992"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.993"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.994"), },
                        [6] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.995"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.996"), },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.997"), },

                    };

                    break;

                case QuestHandle.challengeStars:

                    sceneDialogue = new()
                    {

                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1012"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1013"), },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1014"), },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1015"), },
                        [14] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1016"), },

                        [17] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1018"), },
                        [20] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1019"), },

                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1021"), },
                        [30] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1022"), },

                        [36] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1008"), },
                        [39] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1010"), },

                        [48] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1024"), },
                        [51] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1025"), },
                        [54] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1026"), },

                        [57] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1028"), },

                        [65] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1030"), },
                        [68] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1031"), },
                        [71] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1032"), },

                        [74] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1034"), },

                    };

                    break;

                case QuestHandle.challengeAtoll:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1045"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1046"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1047"), },
                        // cannons
                        [16] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1049"), },
                        [19] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1050"), },
                        [22] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1051"), },
                        // cannons
                        [25] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1053"), },
                        [34] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1054"), },
                        [37] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1055"), },
                        // cannons
                        [49] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1057"), },
                        [52] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1058"), },
                        [55] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1059"), },
                        // cannons
                        [64] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1061"), },
                        [67] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1062"), },
                        [70] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1063"), },
                        [73] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1064"), },
                        [76] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1065"), },
                        [79] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1066"), },

                        [990] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1068"), },
                        [991] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1069"), },
                        [992] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1070"), },
                        [993] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1071"), },
                        [994] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1072"), },
                        [995] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1073"), },


                    };

                    break;

                case QuestHandle.challengeDragon:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1085"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1086"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1087").Tokens(new { rite = Mod.instance.save.rite.ToString() }) },

                        [12] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1089"), },
                        [15] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1090"), },
                        [18] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1091"), },

                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1093"), },
                        [30] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1094"), },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1095"), },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1096"), },

                        [42] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1098"), },
                        [45] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1099"), },
                        [48] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1100"), },

                        [54] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1102"), },
                        [57] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1103"), },
                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1104"), },

                        [81] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1106"), },
                        [84] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1107"), },
                        [87] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1108"), },
                        [90] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1109"), },
                        [93] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1110"), },

                        [900] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.1112"), },

                    };

                    break;

                case QuestHandle.swordFates:

                    sceneDialogue = new()
                    {

                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1123"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1124"), },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1125"), },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1126"), },

                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1128"), },
                        [30] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1129"), },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1130"), },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1131"), },

                        [54] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1133"), },
                        [57] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1134"), },
                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1135"), },

                        [81] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1137"), },
                        [84] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1138"), },

                        [91] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1140"), },
                        [94] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1141"), },
                        [97] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1142"), },
                        [121] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1143"), },

                        [900] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.1145"), },
                        [901] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.312.1"), },
                    };

                    break;

                case QuestHandle.questJester:

                    sceneDialogue = new()
                    {


                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1157") },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1158") },
                        [3] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1159") },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1160") },
                        [5] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1161") },
                        [6] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1162") },
                        [7] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1163") },
                        [8] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1164") },
                        [9] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1165") },
                        [10] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1166") },
                        [11] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1167") },
                        [12] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1168") },
                        [13] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1169") },
                        [14] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1170") },
                        [15] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1171") },
                        [16] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1172") },
                        [17] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1173") },
                        [18] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1174") },
                        [19] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1175") },
                        [20] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1176") },
                        [21] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1177") },
                        [22] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1178") },
                        [23] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1179") },
                        [24] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1180") },

                        [25] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1182") },
                        [26] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1183") },
                        [27] = new()
                        {
                            [0] = Mod.instance.Helper.Translation.Get("DialogueData.1184"),
                            [1] = Mod.instance.Helper.Translation.Get("DialogueData.1185")
                        },
                        [28] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1186") },
                        [29] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1187") },
                        [30] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1188") },
                        [31] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1189") },
                        //[32] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1190") },
                        //[33] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1191") },
                        [34] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1192") },
                        [35] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1193") },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1194") },
                        [37] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1195") },
                        [38] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1196") },
                        [39] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1197") },
                        [40] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1198") },
                        [41] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.1199") },
                        [42] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1200") },
                        [43] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1201") },
                        [44] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.1202") },

                        [45] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1204") },
                        [100] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.379.1") },
                        [46] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1205") },
                        [47] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1206") },
                        [48] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1207") },
                        [49] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1208") },
                        [50] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1209") },
                        [51] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1210") },
                        [52] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1211") },
                        [53] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1212") },
                        [54] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1213") },
                        [55] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1214") },
                        [56] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1215") },
                        [57] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1216") },
                        [58] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1217") },
                        [59] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1218") },
                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1219") },

                        [61] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1221") },
                        [62] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1222") },
                        [63] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1223") },
                        [64] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1224") },
                        [65] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1225") },
                        [66] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1226") },
                        [67] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1227") },
                        [68] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1228") },
                        [69] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1229") },
                        [70] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1230") },
                        [71] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1231") },
                        [72] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1232") },
                        [73] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1233") },
                        [74] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1234") },
                        [75] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1235") },
                        [76] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1236") },
                        [77] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1237") },
                        [78] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1238") },
                        [79] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1239") },
                        [80] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1240") },
                        [81] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1241") },
                        [82] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1242") },
                        [83] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1243") },


                        [901] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1246"), },
                        [903] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1247"), },
                        [904] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1248"), },
                        [906] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1249"), },
                        [907] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1250"), },
                        [910] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1251"), },
                        [912] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1252"), },
                        [913] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1253"), },
                        [915] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1254"), },
                        [916] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1255"), },
                        [919] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1256"), },
                        [922] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1257"), },
                        [923] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1258"), },
                        [925] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1259"), },
                        [928] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1260"), },
                        [931] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1261"), },
                        [934] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1262"), },
                        [937] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1263"), },
                        [940] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1264"), },
                        [943] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1265"), },
                        [946] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1266"), },
                        [949] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1267"), },
                        [952] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1268"), },
                        [955] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1269"), },
                        [958] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1270"), },
                        [961] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1271"), },
                        [963] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1272"), },
                        [965] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1273"), },
                        [968] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1274"), },
                        [971] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.379.2"), },
                        [974] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.379.3"), },

                    };

                    break;


                case QuestHandle.challengeFates:

                    sceneDialogue = new()
                    {

                        [2] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1296"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1297"), },
                        [8] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1298"), },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1299"), },
                        [14] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1300"), },
                        [17] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1301"), },
                        [20] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1302"), },
                        [23] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1303"), },
                        [26] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1304"), },

                        [30] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1306"), },
                        [33] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1307"), },
                        //[36] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1308"), },

                        [40] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1310"), },
                        [43] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1311"), },
                        //[46] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1312"), },

                        [50] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1314"), },
                        [53] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1315"), },
                        [56] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1316"), },

                        [60] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1318"), },
                        [63] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1319"), },
                        //[66] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1320"), },

                        [70] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1322"), },
                        [73] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1323"), },
                        [76] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1324"), },

                        [80] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1326"), },
                        [83] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1327"), },
                        [86] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1328"), },

                        [90] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1330"), },
                        [93] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1331"), },
                        [96] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1332"), },
                        [99] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1333"), },
                        [102] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1334"), },
                        [105] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1335"), },
                        [108] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1336"), },
                        [111] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1337"), },
                        [114] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1338"), },
                        [117] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1339"), },
                        [120] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1340"), },
                        [123] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1341"), },

                        [126] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1343"), },
                        [129] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1344"), },
                        [132] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.343.8"), },
                        [135] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1345"), },
                    };

                    break;

                case QuestHandle.swordEther:

                    sceneDialogue = new()
                    {
                        [3] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1355"), },
                        [6] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1356"), },
                        [9] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1357"), },
                        [12] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1358"), },

                        [15] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1360"), },
                        [18] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1361"), },

                        [21] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1363"), },
                        [24] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1364"), },
                        [27] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1365"), },
                        [30] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1366"), },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1367"), },

                        [42] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1369"), },
                        [45] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1370"), },
                        [48] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1371"), },
                        [51] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1372"), },

                        [60] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1374"), },
                        [63] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1375"), },

                        [75] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1377"), },
                        [78] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1378"), },
                        [81] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1379"), },
                        [84] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1380"), },

                        [93] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1382"), },
                        [96] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1383"), },

                        [991] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1385"), },
                        [992] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1386"), },
                    };

                    break;

                case QuestHandle.questShadowtin:

                    sceneDialogue = new()
                    {
                        // Dwarf interaction
                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1396"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1397"), },
                        [3] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1398"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1399"), },
                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1400"), },
                        [6] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1401"), },
                        [7] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1402"), },
                        [8] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1403"), },
                        [9] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1404"), },

                        [100] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1406"), },
                        [101] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1407"), },
                        [102] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1408"), },
                        [103] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1409"), },

                        [200] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1411"), },
                        [201] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1412"), },
                        [202] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1413"), },
                        [203] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1414"), },
                        [204] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1415"), },
                        [205] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1416"), },
                        [206] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1417"), },
                        [207] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1418"), },
                        [208] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1419"), },
                        [209] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1420"), },
                        [210] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1421"), },
                        [211] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1422"), },
                        [212] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1423"), },
                        [213] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1424").Tokens(new { name = Game1.player.Name }) },
                        [214] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1425"), },
                        [215] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1426"), },

                        [300] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1428"), },
                        [301] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1429"), },
                        [302] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1430"), },
                        [303] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1431"), },
                        [304] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1432"), },
                        [305] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1433"), },
                        [306] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1434"), },
                        [307] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1435"), },
                        [308] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1436"), },
                        [309] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.1437"), },
                        [310] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.1438"), },

                        [400] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1440"), },
                        [401] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1441"), },
                        [402] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1442"), },
                        [403] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1443"), },
                        [404] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1444"), },
                        [405] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1445"), },
                        [406] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1446"), },
                        [407] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.1447") },
                        [408] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1448") },
                        [409] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1449"), },
                        [410] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.1450"), },
                        [411] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.1451"), },

                        [500] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1453"), },
                        [501] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1454"), },
                        [502] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1455"), },
                        [503] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1456"), },
                        [504] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1457"), },
                        [505] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1458"), },

                        [600] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1460"), },
                        [601] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.1461"), },
                        [602] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1462"), },
                        [603] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1463"), },
                        [604] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1464"), },
                        [605] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1465"), },
                        [606] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1466"), },
                        [607] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1467"), },
                        [608] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1468"), },
                        [609] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1469"), },
                        [610] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.1470"), },
                        [611] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1471"), },
                        [612] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1472"), },
                        [613] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1473"), },
                        [614] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1474"), },
                        [615] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.1475"), },
                        [616] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.1476"), },
                        [617] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1477"), },
                        [618] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.363.1"), },
                        [619] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.1478"), },

                    };

                    break;


                case QuestHandle.challengeEther:

                    sceneDialogue = new()
                    {

                        [5] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1490"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1491"), },
                        [10] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1492"), },
                        [13] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1493"), },
                        [16] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1494"), },

                        [19] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1496"), },
                        [22] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1497"), },
                        [25] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1498"), },

                        [30] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1500"), },
                        [33] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1501"), },
                        [36] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1502"), },
                        [39] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1503") },

                        [52] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1505"), },
                        [55] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1506"), },
                        [58] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1507"), },
                        [61] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1508"), },
                        [64] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1509"), },
                        [67] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1510"), },

                        [76] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1512"), },
                        [79] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1513"), },
                        [82] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1514"), },
                        [85] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.1515"), },

                    };

                    break;

                case QuestHandle.treasureChase:

                    sceneDialogue = new()
                    {

                        [990] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.1526"), },
                        [991] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.1527"), },

                    };

                    break;

                case QuestHandle.treasureGuardian:

                    sceneDialogue = new()
                    {

                        [990] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.366.1"), },
                        [991] = new() { [999] = Mod.instance.Helper.Translation.Get("DialogueData.366.2"), },

                    };

                    break;

                case QuestHandle.questBlackfeather:

                    sceneDialogue = new()
                    {
                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.9"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.10"), },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.11"), },

                        [101] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.12"), },
                        [102] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.13"), },
                        [103] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.14"), },
                        [104] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.15"), },
                        [105] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.16"), },
                        [106] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.17"), },
                        [107] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.18"), },
                        [108] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.19"), },
                        [109] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.324.20"), },
                        [110] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.21"), },
                        [111] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.324.22"), },
                        [112] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.324.23"), },
                        [113] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.324.24"), },
                        [114] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.25"), },

                        [201] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.26"), },
                        [202] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.27"), },
                        [203] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.28"), },
                        [204] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.29"), },
                        [205] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.30"), },
                        [206] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.31"), },
                        [207] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.32"), },
                        [208] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.33"), },
                        [209] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.34"), },
                        [210] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.35"), },
                        [211] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.36"), },
                        [212] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.37"), },
                        [213] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.38"), },
                        [214] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.39"), },
                        [215] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.40"), },
                        [216] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.41"), },
                        [217] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.42"), },
                        [218] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.43"), },
                        [219] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.324.44"), },
                        [220] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.45"), },

                        [301] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.46"), },
                        [302] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.47"), },
                        [303] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.48"), },
                        [304] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.49"), },
                        [305] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.50"), },
                        [306] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.324.51"), },
                        [307] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.324.52") },
                        [308] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.324.53"), },
                        [309] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.54"), },
                        [310] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.324.55"), },
                        [311] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.324.56"), },
                        [312] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.57"), },
                        [313] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.324.58"), },
                        [314] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.324.59"), },
                        [315] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.324.60"), },
                        [316] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.324.61"), },
                        [317] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.324.62"), },
                        [318] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.63"), },
                        [319] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.324.64"), },
                        [320] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.65"), },
                        [321] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.324.66"), },
                        [322] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.67"), },
                        [323] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.324.68"), },
                        [323] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.69"), },
                        [324] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.70"), },
                        [325] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.324.71"), },

                        [401] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.72"), },
                        [402] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.73"), },

                        [501] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.74"), },
                        [502] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.75"), },
                        [503] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.76"), },
                        [504] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.77"), },
                        [505] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.78"), },
                        [506] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.79"), },
                        [507] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.80"), },
                        [508] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.81"), },
                        [509] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.82"), },
                        [510] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.83"), },
                        [511] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.324.84"), },

                        [601] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.85"), },
                        [602] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.86"), },
                        [603] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.87"), },
                        [604] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.88"), },
                        [605] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.324.89"), },


                    };

                    break;

                case QuestHandle.questBuffin:

                    sceneDialogue = new()
                    {
                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.9"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.10"), },
                        [7] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.11"), },
                        [11] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.12"), },

                        [103] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.13"), },
                        [107] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.14"), },
                        [111] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.15"), },
                        [115] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.16"), },
                        [119] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.17"), },
                        [122] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.18"), },
                        [125] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.19"), },

                        [204] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.20"), },
                        [206] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.21"), },
                        [208] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.22"), },
                        [210] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.23"), },
                        [212] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.24"), },
                        [214] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.25"), },
                        [216] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.26"), },
                        [218] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.27"), },
                        [220] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.28"), },
                        [222] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.29"), },
                        [224] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.30"), },
                        [226] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.31"), },

                        [302] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.32"), },
                        [305] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.33"), },
                        [308] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.34"), },
                        [311] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.35"), },
                        [317] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.36"), },
                        [320] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.37"), },
                        [323] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.38"), },
                        [326] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.39"), },
                        [330] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.40"), },
                        [333] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.41") },
                        [337] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.42"), },
                        [341] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.43"), },
                        [344] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.44"), },
                        [348] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.45"), },
                        [352] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.46"), },
                        [355] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.47"), },
                        [358] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.48"), },
                        [361] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.49"), },
                        [364] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.50"), },

                        [402] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.51"), },
                        [405] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.52"), },
                        [412] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.53"), },
                        [450] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.54"), },
                        [455] = new() { [9] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.55"), },
                        [458] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.56"), },
                        [461] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.57"), },
                        [464] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.58"), },


                        [502] = new() { [9] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.59"), },
                        [506] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.60"), },
                        [510] = new() { [9] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.61"), },
                        [514] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.62"), },
                        [518] = new() { [9] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.63"), },
                        [522] = new() { [9] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.64"), },
                        [526] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.65"), },
                        [529] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.66"), },
                        [532] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.67"), },
                        [536] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.68"), },
                        [539] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.69"), },
                        [542] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.70"), },
                        [546] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.71"), },
                        [550] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.72"), },
                        [554] = new() { [9] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.73"), },
                        [557] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.74"), },
                        [560] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.75"), },
                        [564] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.76"), },
                        [567] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.77"), },
                        [570] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.78"), },

                        [601] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.339.1.79"), },


                    };

                    break;


                case QuestHandle.questRevenant:

                    sceneDialogue = new()
                    {
                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.60"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.61"), },
                        [7] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.62"), },
                        [10] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.63"), },
                        [13] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.64"), },
                        [16] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.65"), },
                        [19] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.66"), },
                        [22] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.67"), },
                        [25] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.68"), },
                        [28] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.69"), },
                        [31] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.70"), },
                        [34] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.71"), },
                        [38] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.72"), },
                        [41] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.73"), },
                        [44] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.74"), },
                        [48] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.75"), },

                        [101] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.76"), },
                        [104] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.77"), },

                        [107] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.78"), },
                        [110] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.79"), },
                        [113] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.80"), },
                        [116] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.81"), },
                        [121] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.82"), },
                        [124] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.83"), },
                        [127] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.84"), },
                        [130] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.85"), },
                        [133] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.86"), },
                        [136] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.87"), },
                        [139] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.88"), },
                        [142] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.89"), },
                        [145] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.90"), },
                        [148] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.91"), },
                        [151] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.92"), },
                        [154] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.93"), },

                        [201] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.94"), },
                        [207] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.95"), },
                        [210] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.96"), },
                        [213] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.97"), },
                        [216] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.98"), },
                        [219] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.99"), },
                        [222] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.100"), },
                        [225] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.101"), },
                        [228] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.102"), },

                        [304] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.103"), },
                        [307] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.104"), },
                        [310] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.105"), },
                        [313] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.106"), },
                        [316] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.107"), },
                        [319] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.108"), },
                        [322] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.109"), },
                        [325] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.110"), },
                        [328] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.111"), },
                        [331] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.112"), },
                        [334] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.113"), },
                        [337] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.114"), },
                        [340] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.115"), },
                        [343] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.116"), },
                        [346] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.117"), },
                        [349] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.118"), },
                        [352] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.119"), },
                        [358] = new() { [9] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.120"), },
                        [361] = new() { [9] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.121"), },
                        [362] = new() { [8] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.122"), },
                        [366] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.123"), },

                        [401] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.124"), },
                        [404] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.125"), },
                        [407] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.126"), },
                        [410] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.127"), },
                        [413] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.128"), },

                        [419] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.129"), },
                        [422] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.130"), },
                        [425] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.131"), },
                        [428] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.132"), },
                        [431] = new() { [10] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.133"), },
                        [434] = new() { [10] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.134"), },
                        [437] = new() { [10] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.135"), },
                        [440] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.136"), },
                        [443] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.137"), },
                        [446] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.138"), },
                        [449] = new() { [10] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.139"), },
                        [452] = new() { [10] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.140"), },
                        [455] = new() { [10] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.141"), },
                        [458] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.142"), },
                        [461] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.143"), },
                        [464] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.144"), },
                        [467] = new() { [10] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.145"), },
                        [470] = new() { [10] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.146"), },
                        [473] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.147"), },
                        [476] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.148"), },
                        [481] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.149"), },
                        [485] = new() { [11] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.150"), },
                        [488] = new() { [11] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.151"), },

                        [501] = new() { [11] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.152"), },
                        [504] = new() { [11] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.153"), },
                        [507] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.154"), },
                        [510] = new() { [11] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.155"), },
                        [513] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.156"), },
                        [516] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.157"), },
                        [519] = new() { [11] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.158"), },
                        [522] = new() { [11] = Mod.instance.Helper.Translation.Get("DialogueData.342.1.159"), },

                    };

                    break;

                case QuestHandle.challengeBones:

                    /*
                        [0] = "Effigy",
                        [1] = "Blackfeather",
                        [2] = "Friendly Mage",
                        [3] = "Linus",
                        [4] = "Jester",
                        [5] = "Buffin",
                        [6] = "Shadowtin",
                     */

                    sceneDialogue = new()
                    {

                        [1] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.2"), },
                        [4] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.3"), },
                        [7] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.4"), },
                        [10] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.5"), },
                        [13] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.6"), },
                        [16] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.7"), },
                        [19] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.8"), },

                        [101] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.9"), },
                        [104] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.10"), },
                        [107] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.11"), },
                        [110] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.12"), },
                        [113] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.13"), },
                        [116] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.14"), },

                        [122] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.15"), },
                        [125] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.16"), },
                        [128] = new() { [4] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.17"), },
                        [131] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.18"), },
                        [134] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.19"), },
                        [137] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.20"), },
                        [140] = new() { [7] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.21"), },
                        [143] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.22"), },
                        [146] = new()
                        {
                            [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.23"),
                            [7] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.24"),
                        },
                        [149] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.25"), },
                        [152] = new()
                        {
                            [7] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.26"),
                            [8] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.27"),
                            [9] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.28"),
                        },
                        [155] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.29"), },
                        [158] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.30"), },

                        [201] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.31"), },
                        [204] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.32"), },

                        [210] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.33"), },
                        [213] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.34"), },
                        [216] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.35"), },
                        [219] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.36"), },
                        [222] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.37"), },
                        [225] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.38"), },
                        [228] = new()
                        {
                            [2] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.39"),
                            [6] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.40"),
                        },

                        [304] = new()
                        {
                            [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.41"),
                            [2] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.42"),
                            [3] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.43"),
                            [4] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.44"),
                            [5] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.45"),
                        },
                        [307] = new()
                        {
                            [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.46"),
                            [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.47"),
                        },
                        [310] = new()
                        {
                            [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.48"),
                            [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.49"),
                        },
                        [316] = new() { [5] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.50"), },
                        [319] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.51"), },
                        [322] = new()
                        {
                            [10] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.52"),
                            [4] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.53"),
                        },
                        [325] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.54"), },
                        [328] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.55"), },
                        [331] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.56"), },
                        [334] = new()
                        {
                            [10] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.57"),
                            [3] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.58"),
                        },
                        [337] = new() { [3] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.59"), },
                        [340] = new()
                        {
                            [10] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.60"),
                            [2] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.61"),
                        },
                        [343] = new() { [6] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.62"), },
                        [346] = new() { [10] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.63"), },
                        [349] = new() { [10] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.64"), },
                        [352] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.65"), },
                        [355] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.66"), },
                        [358] = new() { [10] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.67"), },

                        [407] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.343.1.68"), },

                        [502] = new() { [11] = Mod.instance.Helper.Translation.Get("DialogueData.345.1"), },
                        [505] = new() { [11] = Mod.instance.Helper.Translation.Get("DialogueData.345.2"), },


                    };

                    break;

                case QuestHandle.swordHeirs:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.4"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.5"), },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.6"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.7"), },

                        [101] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.8"), },
                        [102] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.9"), },
                        [103] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.10"), },
                        [104] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.11"), },
                        [105] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.12"), },
                        [106] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.13"), },
                        [107] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.14"), },
                        [108] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.15"), },
                        [109] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.16"), },
                        [110] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.17"), },
                        [111] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.18"), },
                        [112] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.19"), },
                        [113] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.20"), },
                        [114] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.21"), },
                        [115] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.22"), },
                        [116] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.23"), },
                        [117] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.24"), },
                        [118] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.25"), },
                        [119] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.26"), },
                        [120] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.27"), },
                        [121] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.28"), },
                        [122] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.29"), },
                        [123] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.30"), },
                        [124] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.31"), },
                        [125] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.32"), },

                        [201] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.33"), },
                        [202] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.34"), },
                        [203] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.35"), },

                    };

                    break;

                case QuestHandle.challengeMoors:

                    sceneDialogue = new()
                    {

                        [1] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.41"), },
                        [2] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.42"), },
                        [3] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.43"), },
                        [4] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.44"), },

                        [101] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.45"), },
                        [102] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.46"), },
                        [103] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.47"), },
                        [104] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.48"), },
                        [105] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.49"), },

                        [201] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.50"), },
                        [202] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.51"), },
                        [203] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.52"), },
                        [204] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.53"), },
                        [205] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.54"), },
                        [206] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.55"), },
                        [207] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.56"), },
                        [208] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.57"), },
                        [209] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.58"), },
                        [210] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.59"), },
                        [211] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.60"), },
                        [212] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.61"), },
                        [213] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.62"), },
                        [214] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.63"), },
                        [215] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.64"), },
                        [216] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.65"), },
                        [217] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.66"), },
                        [218] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.67"), },
                        [219] = new() { [1] = Mod.instance.Helper.Translation.Get("DialogueData.362.68"), },
                        [220] = new() { [2] = Mod.instance.Helper.Translation.Get("DialogueData.362.69"), },

                        [301] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.70"), },
                        [302] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.71"), },
                        [303] = new() { [0] = Mod.instance.Helper.Translation.Get("DialogueData.362.72"), },

                    };

                    break;



            };

            return sceneDialogue;

        }

    }

}


