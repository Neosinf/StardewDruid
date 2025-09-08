using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Journal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Handle
{

    public class MasteryHandle
    {

        public Dictionary<MasteryDiscipline.disciplines, MasteryDiscipline> disciplines = new();

        public Dictionary<MasteryPath.paths, MasteryPath> paths = new();

        public Dictionary<MasteryNode.nodes, MasteryNode> nodes = new();

        public MasteryHandle() { }

        public void LoadMasteries()
        {

            disciplines = MasteryData.DisciplineList();

            paths = MasteryData.PathList();

            nodes = MasteryData.NodeList();

        }

        public void LevelUp(MasteryPath.paths path)
        {

            int available = AvailableExperience();

            int nextLevel = PathLevel(path) + 1;

            int requirement = RequiredExperience(path, nextLevel);

            if (requirement > available)
            {

                return;

            }

            MasteryNode.nodes nodeId = paths[path].nodes[nextLevel - 1];

            Mod.instance.save.masteries[nodeId] = requirement;

            return;

        }

        public int AvailableExperience()
        {

            int used = Mod.instance.save.masteries.Values.Sum();

            int available = Mod.instance.save.experience - used;

            if(available < 0)
            {

                return 0;

            }

            return available;

        }

        public int RequiredExperience(MasteryPath.paths pathId, int level)
        {

            if(level == 0)
            {

                return 0;

            }

            MasteryNode.nodes nodeId = paths[pathId].nodes[level-1];

            if (nodes[nodeId].cost != -1)
            {

                return nodes[nodeId].cost;

            }

            switch (level)
            {

                default:

                    return 0;

                case 1:

                    return 500;

                case 2:

                    return 1000;

                case 3:

                    return 1500;

                case 4:

                    return 2500;

            }

        }

        public static bool HasMastery(MasteryNode.nodes node)
        {

            if (Mod.instance.magic)
            {

                return true;

            }

            if (Mod.instance.save.masteries.ContainsKey(node))
            {

                if (Mod.instance.save.masteries[node] > 0)
                {

                    return true;

                }

            }

            return false;

        }

        public bool PathLocked(MasteryPath.paths path)
        {

            if (paths[path].rite != Rite.Rites.none)
            {

                if (Mod.instance.rite.RequirementCheck(paths[path].rite) == Rite.Rites.none)
                {

                    return true;

                }

            }

            return false;

        }

        public int PathLevel(MasteryPath.paths path)
        {

            int level = 0;

            for (int i = 0; i < 4; i++)
            {

                if (!HasMastery(paths[path].nodes[i]))
                {

                    break;

                }

                level++;

            }

            return level;

        }

        public Dictionary<int, Journal.ContentComponent> DisciplineComponents()
        {

            Dictionary<int, ContentComponent> journal = new();

            foreach (KeyValuePair<MasteryDiscipline.disciplines, MasteryDiscipline> section in disciplines)
            {

                int sectionKey = (int)section.Value.discipline;

                ContentComponent content = new(ContentComponent.contentTypes.text, section.Key.ToString());

                content.text[0] = section.Value.name;

                content.textureSources[0] = MasteryDiscipline.DisciplineRectangles(section.Key);

                int pathIndex = 1;

                foreach (MasteryPath.paths path in disciplines[section.Key].paths)
                {

                    if (PathLocked(path))
                    {

                        break;

                    }

                    int pathLevel = PathLevel(path);

                    content.textureSources[pathIndex] = MasteryPath.PathRectangles(paths[path].icon, pathLevel);

                    pathIndex++;

                }

                journal[sectionKey] = content;

            }

            return journal;

        }

        public Dictionary<int, Journal.ContentComponent> NodeComponents(MasteryDiscipline.disciplines discipline)
        {

            Dictionary<int, ContentComponent> journal = new();

            int s = 0;

            foreach (MasteryPath.paths path in disciplines[discipline].paths)
            {

                if (PathLocked(path)) 
                {

                    break;

                }

                int level = PathLevel(path);

                ContentComponent content = new(ContentComponent.contentTypes.text, path.ToString());

                content.text[0] = paths[path].name;

                content.text[1] = paths[path].description;

                content.textScales[1] = 0.8f;

                if (content.text[1].Length > 80)
                {

                    content.textScales[1] = 0.7f;


                }

                for (int i = 0; i <= level; i++)
                {

                    content.textureSources[i] = MasteryPath.PathRectangles(paths[path].icon, i);

                }

                journal[s++] = content;

            }

            return journal;

        }

        // --------------------------------------------------------------

        public float ReplenishmentFactor()
        {

            float difficulty = 1.6f - Mod.instance.ModDifficulty() * 0.1f;

            if (HasMastery(MasteryNode.nodes.potion_bolster))
            {

                difficulty += 0.4f;

            }

            return difficulty;

        }

    }

}
