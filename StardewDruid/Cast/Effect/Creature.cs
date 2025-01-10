using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Location.Druid;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Audio;
using StardewValley.Extensions;
using StardewValley.Locations;
using StardewValley.Menus;
using StardewValley.Monsters;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using xTile.Tiles;


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

        public void AddCreature(GameLocation Location, Character.CharacterHandle.characters CharacterType, Vector2 Origin, Vector2 Target, float Scale, bool forceDrop = false)
        {

            if (creatures.ContainsKey(Origin))
            {

                return;

            }

            creatures[Origin] =  new(Location,CharacterType,Origin,Target,eventId, Scale, forceDrop);

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

        public bool dropFish;

        public bool sounded;

        public bool run;

        public CreatureHandle(GameLocation Location, Character.CharacterHandle.characters CharacterType, Vector2 Origin, Vector2 Target, string EventId, float Scale, bool forceDrop = false)
        {

            origin = Origin;

            target = Target;

            fadeRate = 0f;

            location = Location;

            characterType = CharacterType;

            eventId = EventId;

            scale = Scale;

            if(Mod.instance.randomIndex.Next(3) != 0 && !forceDrop)
            {

                drop = true;

            }

            load();

        }

        public void load()
        {

            switch (characterType)
            {

                case Character.CharacterHandle.characters.GreyWolf:
                case Character.CharacterHandle.characters.BlackWolf:

                    creature = new StardewDruid.Character.Wolf(characterType);

                    run = Mod.instance.randomIndex.NextBool();

                    break;

                case Character.CharacterHandle.characters.BrownBear:
                case Character.CharacterHandle.characters.BlackBear:

                    creature = new StardewDruid.Character.Bear(characterType);

                    break;
                case Character.CharacterHandle.characters.RedFox:
                case Character.CharacterHandle.characters.YellowFox:
                case Character.CharacterHandle.characters.BlackCat:
                case Character.CharacterHandle.characters.GingerCat:
                case Character.CharacterHandle.characters.TabbyCat:

                    creature = new StardewDruid.Character.Critter(characterType);

                    run = Mod.instance.randomIndex.NextBool();

                    break;

                case Character.CharacterHandle.characters.CorvidCrow:
                case Character.CharacterHandle.characters.CorvidRaven:
                case Character.CharacterHandle.characters.CorvidRook:
                case Character.CharacterHandle.characters.CorvidMagpie:

                    creature = new StardewDruid.Character.Flyer(characterType);

                    if (Mod.instance.randomIndex.Next(4) == 0)
                    {

                        sounded = true;

                    }

                    location.playSound(SpellHandle.sounds.batFlap.ToString());

                    break;

                case Character.CharacterHandle.characters.SeaGull:

                    creature = new StardewDruid.Character.Flyer(characterType);

                    if (Mod.instance.randomIndex.Next(4) == 0)
                    {

                        sounded = true;

                    }

                    location.playSound(SpellHandle.sounds.batFlap.ToString());

                    dropFish = true;

                    break;

                case Character.CharacterHandle.characters.BrownOwl:
                case Character.CharacterHandle.characters.GreyOwl:

                    creature = new StardewDruid.Character.Flyer(characterType);

                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {
                        
                        sounded = true;

                    }

                    location.playSound(SpellHandle.sounds.batFlap.ToString());

                    break;

                case Character.CharacterHandle.characters.LavaSerpent:
                case Character.CharacterHandle.characters.RiverSerpent:
                case Character.CharacterHandle.characters.Serpent:

                    creature = new StardewDruid.Character.Serpent(characterType);

                    run = true;//Mod.instance.randomIndex.NextBool();

                    SpellHandle splash = new(origin, 320, IconData.impacts.fish, new());

                    splash.sound = SpellHandle.sounds.pullItemFromWater;

                    Mod.instance.spellRegister.Add(splash);

                    dropFish = true;

                    break;

                default:

                    creature = new StardewDruid.Character.Hoverer(Character.CharacterHandle.characters.Bat);

                    run = Mod.instance.randomIndex.NextBool();

                    location.playSound(SpellHandle.sounds.batFlap.ToString());

                    break;

            }

            creature.setScale = scale;

            creature.gait = 2.8f;

            creature.currentLocation = location;

            creature.fadeOut = 1f;

            creature.Position = origin;

            location.characters.Add(creature);

            creature.SwitchToMode(Character.Character.mode.scene, Game1.player);

            creature.eventName = eventId;

            creature.TargetEvent(0, target, true);

            if (run)
            {
                creature.netMovement.Set((int)Character.Character.movements.run);

            }

        }

        public bool update()
        {

            /*if (water != Vector2.Zero)
            {

                return waterUpdate();

            }*/

            if(Vector2.Distance(creature.Position, target) <= 32)
            {

                return false;

            }

            if(!sounded)
            {

                if(Mod.instance.randomIndex.Next(24) == 0)
                {

                    playCall();

                    sounded = true;

                }

            }

            if (!drop)
            {

                if (location is Clearing)
                {

                    if (Vector2.Distance(creature.Position, origin) >= 128)
                    {
                        
                        Item bushDrop = ItemRegistry.Create(SpawnData.RandomSeasonalFruit());

                        if (bushDrop != null)
                        {

                            location.debris.Add(new Debris(bushDrop, creature.Position));

                        }

                        drop = true;

                        playCall();

                        sounded = true;

                    }

                }
                else if (dropFish)
                {

                    if (
                        Vector2.Distance(creature.Position, Game1.player.Position) <= 640 && 
                        Mod.instance.randomIndex.Next(5) == 0 && 
                        ModUtility.GroundCheck(location,ModUtility.PositionToTile(creature.Position)) == "ground"
                        )
                    {

                        string randomFish = SpawnData.RandomLowFish(location, ModUtility.PositionToTile(origin));

                        Item fishDrop = ItemRegistry.Create(randomFish);

                        if (fishDrop != null)
                        {

                            location.debris.Add(new Debris(fishDrop, creature.Position));

                            Game1.player.gainExperience(1, 24); // gain fishing experience

                            Game1.player.NotifyQuests(quest => quest.OnFishCaught(fishDrop.ItemId, 1, 1));//checkForQuestComplete(null, -1, 1, null, randomFish, 7);

                        }

                        drop = true;

                        playCall();

                        sounded = true;

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

                    playCall();

                    sounded = true;

                }

            }

            return true;

        }

        public void playCall()
        {

            if (creature is Flyer)
            {
                if (creature.characterType == CharacterHandle.characters.BrownOwl || creature.characterType == CharacterHandle.characters.GreyOwl)
                {


                    location.playSound(SpellHandle.sounds.owl.ToString());

                }
                else if (creature.characterType == CharacterHandle.characters.SeaGull)
                {

                    location.playSound("seagulls");
                }
                else
                {

                    location.playSound(SpellHandle.sounds.crow.ToString());


                }
            }
            else
            if (creature is Hoverer)
            {

                location.playSound(SpellHandle.sounds.batScreech.ToString());

            }
            else
            if (creature is Critter)
            {
                if (creature.characterType == CharacterHandle.characters.YellowFox || creature.characterType == CharacterHandle.characters.RedFox)
                {

                    location.playSound(SpellHandle.sounds.dog_bark.ToString());

                }
                else
                {

                    location.playSound(SpellHandle.sounds.cat.ToString());

                }

            }
            else
            if (creature is Wolf)
            {

                location.playSound(SpellHandle.sounds.dog_bark.ToString());

            }
            else
            if (creature is Bear)
            {

                switch (Mod.instance.randomIndex.Next(3))
                {

                    case 0:

                        location.playSound("BearGrowl");

                        break;

                    case 1:

                        location.playSound("BearGrowlTwo");

                        break;

                    case 2:

                        location.playSound("BearGrowlThree");

                        break;

                }

            }
            else
            if (creature is StardewDruid.Character.Serpent)
            {

                location.playSound("serpentHit");

            }

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
