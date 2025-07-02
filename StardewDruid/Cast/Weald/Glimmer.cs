using Microsoft.Xna.Framework;
using StardewDruid.Data;
using StardewDruid.Location;
using StardewValley.Characters;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley.Locations;
using StardewValley.Buildings;
using System.Reflection.Metadata;

namespace StardewDruid.Cast.Weald
{
    public class Glimmer
    {

        public Glimmer()
        {


        }

        public void CastActivate()
        {

            GameLocation location = Game1.player.currentLocation;

            Vector2 origin = Game1.player.Position;

            List<NPC> villagers = ModUtility.GetFriendsInLocation(location, true);

            float threshold = 640;

            foreach (NPC witness in villagers)
            {

                if (Mod.instance.Witnessed(ReactionData.reactions.weald, witness))
                {

                    continue;

                }

                if (Vector2.Distance(witness.Position, origin) >= threshold)
                {

                    continue;

                }

                witness.faceTowardFarmerForPeriod(3000, 4, false, Game1.player);

                Game1.player.friendshipData[witness.Name].TalkedToToday = true;

                ModUtility.ChangeFriendship(witness, 25);

                ReactionData.ReactTo(witness, ReactionData.reactions.weald, 25, new());

                SpellHandle sparklesparkle = new(witness.Position, 256, IconData.impacts.sparkle, new()) { instant = true, displayRadius = 3, };

                Mod.instance.spellRegister.Add(sparklesparkle);

            }

            if (location is Farm farmLocation)
            {

                foreach(Building building in farmLocation.buildings)
                {

                    if(building is PetBowl petBowl)
                    {
                        
                        if (Vector2.Distance(new Vector2(petBowl.tileX.Value,petBowl.tileY.Value)*64, origin) <= threshold)
                        {

                            petBowl.watered.Set(true);

                        }
                        

                    }

                }

                /*Vector2 bowl = farmLocation.GetStarterPetBowlLocation();

                if (Vector2.Distance(bowl * 64, origin) <= threshold)
                {

                    Mod.instance.virtualCan.WaterLeft = 100;

                    farmLocation.performToolAction(Mod.instance.virtualCan, (int)bowl.X + 1, (int)bowl.Y);
                
                }*/

                foreach (NPC witness in location.characters)
                {

                    if (witness is Pet petPet)
                    {

                        if (Mod.instance.Witnessed(ReactionData.reactions.weald, witness))
                        {

                            continue;

                        }

                        if (Vector2.Distance(petPet.Position, origin) >= threshold)
                        {
                            continue;
                        }

                        petPet.checkAction(Game1.player, location);

                        continue;

                    }

                }

                foreach (KeyValuePair<long, FarmAnimal> pair in farmLocation.animals.Pairs)
                {

                    if (Mod.instance.Witnessed(ReactionData.reactions.weald, pair.Value.myID.ToString()))
                    {

                        continue;

                    }

                    if (Vector2.Distance(pair.Value.Position, origin) >= threshold)
                    {

                        continue;

                    }

                    ModUtility.PetAnimal(pair.Value);

                    Mod.instance.AddWitness(ReactionData.reactions.weald, pair.Value.myID.ToString());

                }

            }

            if(location is FarmHouse farmhouse)
            {

                foreach (NPC witness in location.characters)
                {

                    if (witness is Pet petPet)
                    {

                        if (Mod.instance.Witnessed(ReactionData.reactions.weald, witness))
                        {

                            continue;

                        }

                        if (Vector2.Distance(petPet.Position, origin) >= threshold)
                        {
                            continue;
                        }

                        petPet.checkAction(Game1.player, location);

                        continue;

                    }

                }

            }

            if (location is AnimalHouse animalLocation)
            {

                foreach (KeyValuePair<long, FarmAnimal> pair in animalLocation.animals.Pairs)
                {

                    if (Mod.instance.Witnessed(ReactionData.reactions.weald, pair.Value.myID.ToString()))
                    {

                        continue;

                    }

                    if (Vector2.Distance(pair.Value.Position, origin) >= threshold)
                    {

                        continue;

                    }


                    ModUtility.PetAnimal(pair.Value);

                    Mod.instance.AddWitness(ReactionData.reactions.weald, pair.Value.myID.ToString());

                }

                for (int i = 0; i < location.map.Layers[0].LayerWidth; i++)
                {

                    for (int j = 0; j < location.map.Layers[0].LayerHeight; j++)
                    {

                        if (location.doesTileHaveProperty(i, j, "Trough", "Back") == null)
                        {
                            continue;
                        }
                        Vector2 trough = new Vector2(i, j);

                        if (!location.objects.ContainsKey(trough))
                        {
                            location.objects.Add(trough, new StardewValley.Object("178", 1));

                        }

                    }

                }

            }


        }

    }

}
