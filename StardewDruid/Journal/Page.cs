using System.Collections.Generic;

namespace StardewDruid.Journal
{
    public class Page
    {
        public string title;
        public string description;
        public List<string> objectives;
        public string icon;
        public bool active;

        public Page() => this.objectives = new List<string>();
    }
}