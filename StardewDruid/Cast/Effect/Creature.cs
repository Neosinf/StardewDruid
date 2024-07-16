using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Monsters;
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

            inabsentia = true;

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

        public void AddCreature(GameLocation Location, Character.CharacterHandle.characters CharacterType, Vector2 Origin, int Direction, float Scale)
        {

            if (creatures.ContainsKey(Origin))
            {

                return;

            }

            creatures[Origin] =  new(Location,CharacterType,Origin,Direction,eventId, Scale);

            activeLimit = eventCounter += 10;

        }


    }

    public class CreatureHandle
    {

        public GameLocation location;

        public Character.CharacterHandle.characters characterType;

        public Vector2 origin;

        public Vector2 target;

        public int direction;

        public StardewDruid.Character.Character creature;

        public float fadeRate;

        public string eventId;

        public float scale;

        public CreatureHandle(GameLocation Location, Character.CharacterHandle.characters CharacterType, Vector2 Origin, int Direction, string EventId, float Scale)
        {

            origin = Origin;

            direction = Direction;

            fadeRate = 0.05f;

            location = Location;

            characterType = CharacterType;

            eventId = EventId;

            scale = Scale;

            direct();

            load();

        }

        public void direct()
        {

            Vector2 offset = (ModUtility.DirectionAsVector(direction) * 64 * location.Map.Layers[0].TileWidth);

            target = origin + offset;

        }

        public void load()
        {

            switch (characterType)
            {

                case Character.CharacterHandle.characters.Shadowcat:
                case Character.CharacterHandle.characters.Shadowfox:

                    creature = new StardewDruid.Character.Critter(characterType);

                    break;

                default:

                    creature = new StardewDruid.Character.Hoverer(Character.CharacterHandle.characters.Shadowbat);

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

                shutdown();

                return false;

            }

            string check = ModUtility.GroundCheck(creature.currentLocation, creature.occupied, true);

            if (check == "void")
            {

                fadeRate = 0.4f;

            }
            else
            if (creature is StardewDruid.Character.Critter && check == "water")
            {

                fadeRate = 0.4f;

            }
            else if (creature is StardewDruid.Character.Critter && check != "ground")
            {

                fadeRate = 0.2f;

            }

            creature.fadeOut -= fadeRate;

            if(creature.fadeOut <= 0)
            {

                shutdown();

                return false;

            }

            return true;

        }

        public void shutdown()
        {

            creature.currentLocation.characters.Remove(creature);

        }

    }

}
