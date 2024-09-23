using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Journal;
using StardewDruid.Location;
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

                if (Mod.instance.Config.riteButtons.GetState() != SButtonState.Held)
                {

                    return false;

                }

                if (Vector2.Distance(origin, Game1.player.Position) > 32)
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

                    Mod.instance.rite.channel(IconData.skies.mountain, 75);

                    channel = IconData.skies.mountain;

                }

                if (decimalCounter == 15)
                {

                    eventLocked = true;

                    //SpellHandle spellHandle = new(origin, 384, IconData.impacts.nature, new());

                    //Mod.instance.spellRegister.Add(spellHandle);

                    SpellHandle circleHandle = new(origin, 256, IconData.impacts.summoning, new());

                    circleHandle.scheme = IconData.schemes.grannysmith;

                    circleHandle.sound = SpellHandle.sounds.discoverMineral;

                    Mod.instance.spellRegister.Add(circleHandle);

                    ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 600);

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

                int maxRestoration = LocationData.RestoreLocation(location.Name);

                if (Mod.instance.save.restoration[location.Name] >= maxRestoration)
                {

                    Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.restoreFully));

                    return;
                
                }

                if (Mod.instance.rite.specialCasts[location.Name].Contains("restoration"))
                {

                    Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.restoreTomorrow));

                    location.playSound(SpellHandle.sounds.ghost.ToString());

                    return;

                }

                Mod.instance.rite.specialCasts[location.Name].Add("restoration");

                Mod.instance.save.restoration[location.Name] += 1;

                if (Mod.instance.save.restoration[location.Name] == 1)
                {

                    Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.restoreStart));

                }
                else if (Mod.instance.save.restoration[location.Name] >= maxRestoration)
                {

                    Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.restoreFully));

                    if (Game1.player.currentLocation is Clearing)
                    {

                        Bounty bounty = new();

                        bounty.creatureProspects.Add(origin, Bounty.bounties.orchard);

                        bounty.location = location;

                        bounty.SpawnCreature();

                    }

                }
                else
                {

                    Mod.instance.CastMessage(DialogueData.Strings(DialogueData.stringkeys.restorePartial));

                }

                Mod.instance.SyncMultiplayer();

                location.playSound(SpellHandle.sounds.secret1.ToString());

                Mod.instance.rite.castCost = 24;

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
                    0.8f + (Mod.instance.randomIndex.Next(5) * 0.2f),
                    new() { alpha = 0.35f }
                );


            }

            return;

        }

        public override void EventRemove()
        {
            base.EventRemove();

            Mod.instance.rite.ApplyCost();

        }

    }

}
