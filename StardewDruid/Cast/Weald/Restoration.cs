using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Cast.Effect;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
using StardewDruid.Journal;
using StardewDruid.Location;
using StardewDruid.Location.Druid;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using System.Collections.Generic;
using xTile.Layers;
using xTile.Tiles;


namespace StardewDruid.Cast.Weald
{
    public class Restoration : EventHandle
    {
        public Restoration()
        {

        }

        public override void EventDecimal()
        {

            decimalCounter++;

            if(decimalCounter == 8)
            {

                if (!Mod.instance.rite.specialCasts.ContainsKey(location.Name))
                {

                    Mod.instance.rite.specialCasts[location.Name] = new();

                }

                eventComplete = true;

                int maxRestoration = LocationHandle.MaxRestoration(location.Name);

                // Message failure

                if (Mod.instance.save.restoration[location.Name] >= maxRestoration)
                {

                    Mod.instance.RegisterMessage(StringData.Get(StringData.str.restoreFully));

                    return;
                
                }

                if (Mod.instance.rite.specialCasts[location.Name].Contains("restoration"))
                {

                    Mod.instance.RegisterMessage(StringData.Get(StringData.str.restoreTomorrow));

                    location.playSound(SpellHandle.Sounds.ghost.ToString());

                    return;

                }

                // Enact restoration

                Mod.instance.rite.specialCasts[location.Name].Add("restoration");

                Mod.instance.save.restoration[location.Name] += 1;

                (location as DruidLocation).RestoreTo(Mod.instance.save.restoration[location.Name]);

                // Message restoration

                if (Mod.instance.save.restoration[location.Name] == 1)
                {

                    Mod.instance.RegisterMessage(StringData.Get(StringData.str.restoreStart));

                }
                else if (Mod.instance.save.restoration[location.Name] >= maxRestoration)
                {

                    Mod.instance.RegisterMessage(StringData.Get(StringData.str.restoreFully));

                    if (Game1.player.currentLocation is Clearing clearing)
                    {

                        ReturnOwls(clearing);

                    }

                }
                else
                {

                    Mod.instance.RegisterMessage(StringData.Get(StringData.str.restorePartial));

                }

                Mod.instance.SyncProgress();

                location.playSound(SpellHandle.Sounds.secret1.ToString());

                Rite.ApplyCost(24);

                return;

            }

            for(int i = 0; i < 8; i++)
            {
                
                Vector2 sparkle;

                if (decimalCounter % 2 == 0)
                {
                    
                    sparkle = ModUtility.DirectionAsVectorOffset(i) * (64 * decimalCounter);

                }
                else
                {

                    sparkle = ModUtility.DirectionAsVector(i) * (64 * decimalCounter);

                }

                Mod.instance.iconData.ImpactIndicator(
                    Game1.player.currentLocation,
                    origin + sparkle,
                    IconData.impacts.glare,
                    4f,
                    new() { alpha = 0.5f, rotation = Mod.instance.randomIndex.Next(4) * 0.5f, });

            }

            return;

        }

        public void ReturnOwls(Clearing clearing)
        {

            Creature creature;

            string id = "creature" + location.Name;

            if (!Mod.instance.eventRegister.ContainsKey(id))
            {

                creature = new()
                {
                    eventId = id
                };

                creature.EventActivate();

            }
            else
            {

                creature = Mod.instance.eventRegister[id] as Creature;

            }

            int direction = Mod.instance.randomIndex.Next(8);

            List<CharacterHandle.characters> owls = new()
            {

                CharacterHandle.characters.BrownOwl,
                CharacterHandle.characters.GreyOwl,

            };

            int owlCount = 0;

            Vector2 exit = new Vector2(27, -8) * 64;

            foreach (TerrainField terrainTile in clearing.terrainFields)
            {

                if (terrainTile.tilesheet == IconData.tilesheets.clearing)
                {

                    Vector2 roost = terrainTile.position + new Vector2(96, 96);

                    owlCount++;

                    creature.AddCreature(location, owls[Mod.instance.randomIndex.Next(owls.Count)], exit + new Vector2(16 * owlCount, 0), roost, 2.5f + (0.25f * Mod.instance.randomIndex.Next(3)), 1);

                }

            }

        }


    }

}
