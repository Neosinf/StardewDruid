using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Location;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Audio;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Monsters;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;


namespace StardewDruid.Cast.Effect
{
    public class Creature : EventHandle
    {

        public Dictionary<Vector2, CreatureHandle> creatures = new();

        public Creature()
        {

            activeLimit = -1;

        }

        public override void EventRemove()
        {
            
            foreach(KeyValuePair<Vector2, CreatureHandle> pair in creatures)
            {

                pair.Value.shutdown();

            }
            
            base.EventRemove();

        }

        public override void EventDecimal()
        {

            for(int c = creatures.Count - 1; c >= 0; c--)
            {

                KeyValuePair<Vector2, CreatureHandle> pair = creatures.ElementAt(c);

                if (!pair.Value.update())
                {

                    pair.Value.shutdown();

                    creatures.Remove(pair.Key);

                }

            }

        }

        public void AddCreature(GameLocation Location, Character.CharacterHandle.characters CharacterType, Vector2 Origin, Vector2 Target, float Scale)
        {

            if (creatures.ContainsKey(Origin))
            {

                return;

            }

            creatures[Origin] =  new(Location,CharacterType,Origin,Target,eventId, Scale);

        }


    }

    public class CreatureHandle
    {

        public GameLocation location;

        public Character.CharacterHandle.characters characterType;

        public Vector2 origin;

        public Vector2 target;

        public StardewDruid.Character.Character creature;

        public float fadeRate;

        public string eventId;

        public float scale;

        public bool drop;

        public bool sounded;

        public CreatureHandle(GameLocation Location, Character.CharacterHandle.characters CharacterType, Vector2 Origin, Vector2 Target, string EventId, float Scale)
        {

            origin = Origin;

            target = Target;

            fadeRate = 0f;

            location = Location;

            characterType = CharacterType;

            eventId = EventId;

            scale = Scale;

            if(Mod.instance.randomIndex.Next(3) != 0)
            {

                drop = true;

            }

            load();

        }

        public void load()
        {

            switch (characterType)
            {

                case Character.CharacterHandle.characters.Shadowcat:
                case Character.CharacterHandle.characters.Shadowfox:

                    creature = new StardewDruid.Character.Critter(characterType);

                    break;

                case Character.CharacterHandle.characters.ShadowCrow:
                case Character.CharacterHandle.characters.ShadowRaven:
                case Character.CharacterHandle.characters.ShadowRook:
                case Character.CharacterHandle.characters.ShadowMagpie:

                    creature = new StardewDruid.Character.Flyer(characterType);

                    location.playSound(SpellHandle.sounds.batFlap.ToString());

                    break;

                default:

                    IconData.relics tacticalRelic = Mod.instance.relicsData.RelicTacticalLocations();

                    if (tacticalRelic != IconData.relics.none)
                    {

                        if (!Journal.RelicData.HasRelic(tacticalRelic))
                        {

                            ThrowHandle throwRelic = new(Game1.player, origin, tacticalRelic);

                            throwRelic.register();

                        }

                    }

                    creature = new StardewDruid.Character.Hoverer(Character.CharacterHandle.characters.Shadowbat);

                    location.playSound(SpellHandle.sounds.batFlap.ToString());

                    break;

            }

            creature.setScale = scale;

            creature.currentLocation = location;

            creature.fadeOut = 1f;

            creature.Position = origin;

            location.characters.Add(creature);

            creature.SwitchToMode(Character.Character.mode.scene, Game1.player);

            creature.eventName = eventId;

            creature.TargetEvent(0, target, true);

            creature.pathActive = Character.Character.pathing.running;

        }

        public bool update()
        {

            if(Vector2.Distance(creature.Position, target) <= 32)
            {

                return false;

            }

            if(!sounded && creature is Character.Flyer)
            {

                if(Mod.instance.randomIndex.Next(24) == 0)
                {

                    location.playSound(SpellHandle.sounds.crow.ToString());

                }

                sounded = true;

            }

            if (!drop)
            {

                if (location is Clearing)
                {

                    if (Vector2.Distance(creature.Position, origin) >= 480)
                    {
                        Item bushDrop = ItemRegistry.Create(SpawnData.RandomSeasonalFruit());

                        if (bushDrop != null)
                        {

                            location.debris.Add(new Debris(bushDrop, creature.Position));

                        }

                        drop = true;

                        if (creature is Flyer)
                        {

                            location.playSound(SpellHandle.sounds.crow.ToString());

                        }
                        else
                        if (creature is Hoverer)
                        {

                            location.playSound(SpellHandle.sounds.batScreech.ToString());

                        }

                    }

                }
                else if (Vector2.Distance(creature.Position, origin) >= 240)
                {
 
                    Item bushDrop = ItemRegistry.Create(SpawnData.RandomBushForage());

                    if (bushDrop != null)
                    {

                        location.debris.Add(new Debris(bushDrop, creature.Position));

                    }

                    drop = true;

                    if (creature is Flyer)
                    {

                        location.playSound(SpellHandle.sounds.crow.ToString());

                    }
                    else
                    if (creature is Hoverer)
                    {

                        location.playSound(SpellHandle.sounds.batScreech.ToString());

                    }

                }

            }

            if (creature is StardewDruid.Character.Critter)
            {
                
                string check = ModUtility.GroundCheck(creature.currentLocation, creature.occupied, true);

                if (check != "ground")
                {

                    creature.fadeOut -= 0.2f;

                }

            }

            return true;

        }

        public void shutdown()
        {

            if (creature != null)
            {

                creature.currentLocation.characters.Remove(creature);

                creature = null;

            }

        }

    }

}
