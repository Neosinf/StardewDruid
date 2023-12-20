using System.Collections.Generic;

namespace StardewDruid.Map
{
    static class VillagerData
    {

        public static List<string> VillagerIndex(string residentType)
        {

            List<string> villagerList = new();

            Dictionary<string, List<string>> villagerIndex = new()
            {
                ["mountain"] = new(){
                    "Sebastian", "Sam",
                    "Maru", "Abigail",
                    "Robin", "Demetrius", "Linus", "Dwarf",
                },
                ["town"] = new(){

                    "Alex", "Elliott", "Harvey",
                    "Emily", "Penny",
                    "Caroline", "Clint", "Evelyn", "George", "Gus", "Jodi", "Kent", "Lewis", "Pam", "Pierre", "Vincent",
                },
                ["forest"] = new(){

                    "Shane",
                    "Leah", "Haley",
                    "Marnie", "Jas", "Krobus", "Wizard", "Willy",
                }
            };

            if (villagerIndex.ContainsKey(residentType))
            {

                villagerList = villagerIndex[residentType];

            }

            return villagerList;

        }

    }
}
