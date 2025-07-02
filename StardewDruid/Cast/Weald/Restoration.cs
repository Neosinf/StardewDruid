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

        public override bool EventActive()
        {

            if (!inabsentia && !eventLocked)
            {

                if (!Mod.instance.RiteButtonHeld())
                {

                    return false;

                }

                if (Vector2.Distance(origin, Game1.player.Position) > 32 && !Mod.instance.ShiftButtonHeld())
                {

                    return false;

                }

            }
            
            return base.EventActive();

        }

        public override void EventDecimal()
        {

            if (!EventActive())
            {

                RemoveAnimations();

                return;

            }

            decimalCounter++;

            if (!inabsentia && !eventLocked)
            {

                if (decimalCounter == 5)
                {

                    Mod.instance.rite.Channel(IconData.skies.mountain, 75);

                    channel = IconData.skies.mountain;

                }

                if (decimalCounter == 15)
                {

                    eventLocked = true;

                    SpellHandle circleHandle = new(origin, 256, IconData.impacts.summoning, new())
                    {
                        displayRadius = 3,

                        scheme = IconData.schemes.herbal_ligna,

                        sound = SpellHandle.Sounds.discoverMineral
                    };

                    Mod.instance.spellRegister.Add(circleHandle);

                    decimalCounter = 0;

                }

                return;

            }

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

                    Mod.instance.CastMessage(StringData.Strings(StringData.stringkeys.restoreFully));

                    return;
                
                }

                if (Mod.instance.rite.specialCasts[location.Name].Contains("restoration"))
                {

                    Mod.instance.CastMessage(StringData.Strings(StringData.stringkeys.restoreTomorrow));

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

                    Mod.instance.CastMessage(StringData.Strings(StringData.stringkeys.restoreStart));

                }
                else if (Mod.instance.save.restoration[location.Name] >= maxRestoration)
                {

                    Mod.instance.CastMessage(StringData.Strings(StringData.stringkeys.restoreFully));

                    if (Game1.player.currentLocation is Clearing clearing)
                    {

                        ReturnOwls(clearing);

                    }

                }
                else
                {

                    Mod.instance.CastMessage(StringData.Strings(StringData.stringkeys.restorePartial));

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
