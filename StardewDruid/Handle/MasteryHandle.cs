using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Journal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Handle
{
    public class MasteryHandle
    {

        public Dictionary<MasterySection.sectionTypes, MasterySection> sections = new();

        public Dictionary<MasteryNode.masteryTypes, MasteryNode> nodes = new();

        public MasteryHandle() { }

        public void LoadMasteries()
        {

        }

        public Dictionary<int, Journal.ContentComponent> SectionComponents()
        {

            Dictionary<int, ContentComponent> journal = new();

            foreach (KeyValuePair<MasterySection.sectionTypes, MasterySection> section in sections)
            {

                int sectionKey = (int)section.Value.type;

                ContentComponent content = new(ContentComponent.contentTypes.masterysection, section.Key.ToString());

                content.text[0] = section.Value.name;

                content.textureSources[0] = new(sectionKey * 32, 64, 32, 64);

                journal[sectionKey] = content;

            }

            return journal;

        }

        public Dictionary<int, Journal.ContentComponent> NodeComponents()
        {
            Dictionary<int, ContentComponent> journal = new();

            foreach (KeyValuePair<MasteryNode.masteryTypes, MasteryNode> node in nodes)
            {

                int nodeKey = (int)node.Value.type;

                ContentComponent content = new(ContentComponent.contentTypes.masterynode, node.Key.ToString());

                content.text[0] = node.Value.name;

                content.textureSources[0] = IconData.MasteryRectangles(node.Value.display);

                journal[nodeKey] = content;

            }

            return journal;
        }

    }

}
