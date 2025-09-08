using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
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


namespace StardewDruid.Cast.Effect
{
    public class Creature : EventHandle
    {

        public Dictionary<Vector2, CreatureHandle> creatures = new();

        public Creature()
        {

            

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

            if(creatures.Count == 0)
            {

                eventComplete = true;

            }

        }

        public void AddCreature(GameLocation Location, CharacterHandle.characters CharacterType, Vector2 Origin, Vector2 Target, float Scale, int Drop = 0)
        {

            if (creatures.ContainsKey(Origin))
            {

                return;

            }

            creatures[Origin] =  new(Location,CharacterType,Origin,Target,eventId, Scale, Drop);

        }


    }

    public class CreatureHandle
    {

        public GameLocation location;

        public CharacterHandle.characters characterType;

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

        public ApothecaryHandle.items herbal = ApothecaryHandle.items.omen_feather;

        public CreatureHandle(GameLocation Location, CharacterHandle.characters CharacterType, Vector2 Origin, Vector2 Target, string EventId, float Scale, int DropRate)
        {

            origin = Origin;

            target = Target;

            fadeRate = 0f;

            location = Location;

            characterType = CharacterType;

            eventId = EventId;

            scale = Scale;

            switch (DropRate)
            {

                case 0:

                    if (Mod.instance.randomIndex.Next(3) != 0)
                    {

                        drop = true;

                    }
                    
                    break;

                case 2:

                    drop = true;

                    break;
            }

            load();

        }

        public void load()
        {

            switch (characterType)
            {

                case CharacterHandle.characters.BrownWolf:
                case CharacterHandle.characters.BlackWolf:

                    creature = new StardewDruid.Character.Wolf(characterType);

                    if(Vector2.Distance(origin,target) > 512)
                    {

                        run = true;

                    }

                    herbal = ApothecaryHandle.items.omen_tuft;

                    break;

                case CharacterHandle.characters.BrownBear:
                case CharacterHandle.characters.BlackBear:

                    creature = new StardewDruid.Character.Bear(characterType);

                    if (Vector2.Distance(origin, target) > 512)
                    {

                        run = true;

                    }

                    herbal = ApothecaryHandle.items.omen_glass;

                    if(Mod.instance.randomIndex.Next(2) == 0)
                    {

                        herbal = ApothecaryHandle.items.omen_elderbloom;

                    }

                    break;

                case CharacterHandle.characters.RedFox:
                case CharacterHandle.characters.YellowFox:
                case CharacterHandle.characters.BlackCat:
                case CharacterHandle.characters.GingerCat:
                case CharacterHandle.characters.TabbyCat:

                    creature = new StardewDruid.Character.Critter(characterType);

                    if (Vector2.Distance(origin, target) > 512)
                    {

                        run = true;

                    }
                    herbal = ApothecaryHandle.items.omen_tuft;
                    break;

                case CharacterHandle.characters.CorvidCrow:
                case CharacterHandle.characters.CorvidRaven:
                case CharacterHandle.characters.CorvidRook:
                case CharacterHandle.characters.CorvidMagpie:

                    creature = new StardewDruid.Character.Flyer(characterType);

                    if (Mod.instance.randomIndex.Next(4) == 0)
                    {

                        sounded = true;

                    }

                    location.playSound(SpellHandle.Sounds.batFlap.ToString());
                    herbal = ApothecaryHandle.items.omen_nest;
                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {

                        herbal = ApothecaryHandle.items.omen_down;

                    }
                    break;

                case CharacterHandle.characters.SeaGull:

                    creature = new StardewDruid.Character.Flyer(characterType);

                    if (Mod.instance.randomIndex.Next(4) == 0)
                    {

                        sounded = true;

                    }

                    location.playSound(SpellHandle.Sounds.batFlap.ToString());

                    dropFish = true;

                    herbal = ApothecaryHandle.items.omen_shell;

                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {

                        herbal = ApothecaryHandle.items.omen_coral;

                    }

                    break;

                case CharacterHandle.characters.BrownOwl:
                case CharacterHandle.characters.GreyOwl:

                    creature = new StardewDruid.Character.Flyer(characterType);

                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {
                        
                        sounded = true;

                    }

                    location.playSound(SpellHandle.Sounds.batFlap.ToString());

                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {

                        herbal = ApothecaryHandle.items.omen_down;

                    }
                    break;

                case CharacterHandle.characters.LavaSerpent:
                case CharacterHandle.characters.RiverSerpent:
                case CharacterHandle.characters.NightSerpent:
                case CharacterHandle.characters.Serpent:

                    creature = new StardewDruid.Character.Serpent(characterType);

                    run = true;//Mod.instance.randomIndex.NextBool();

                    SpellHandle splash = new(origin, 160, IconData.impacts.fish, new())
                    {
                        sound = SpellHandle.Sounds.pullItemFromWater
                    };

                    Mod.instance.spellRegister.Add(splash);

                    dropFish = true;
                    herbal = ApothecaryHandle.items.omen_glass;

                    if (Mod.instance.randomIndex.Next(2) == 0)
                    {

                        herbal = ApothecaryHandle.items.omen_coral;

                    }

                    break;

                case CharacterHandle.characters.Bat:
                case CharacterHandle.characters.BrownBat:

                    creature = new StardewDruid.Character.Bat(characterType);

                    run = Mod.instance.randomIndex.NextBool();

                    location.playSound(SpellHandle.Sounds.batFlap.ToString());

                    break;

                default:

                    creature = new StardewDruid.Character.Bat(CharacterHandle.characters.Bat);

                    run = Mod.instance.randomIndex.NextBool();

                    location.playSound(SpellHandle.Sounds.batFlap.ToString());

                    break;
            }

            creature.setScale = scale;

            creature.gait = 2.8f;

            creature.currentLocation = location;

            creature.fadeSet = 1f;
            
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

            if(creature is null)
            {

                return false;

            }

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

                        if (Mod.instance.randomIndex.Next(4) == 0)
                        {
                            new ThrowHandle(Game1.player, creature.Position, herbal, 1).register();
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

                            Mod.instance.GiveExperience(1, 24); // gain fishing experience

                            Game1.player.NotifyQuests(quest => quest.OnFishCaught(fishDrop.ItemId, 1, 1));//checkForQuestComplete(null, -1, 1, null, randomFish, 7);

                        }

                        if (Mod.instance.randomIndex.Next(4) == 0)
                        {
                            new ThrowHandle(Game1.player, creature.Position, herbal, 1).register();
                        }

                        drop = true;

                        playCall();

                        sounded = true;

                    }

                }
                else if (Vector2.Distance(creature.Position, origin) >= 240)
                {
                    switch(Mod.instance.randomIndex.Next(2))
                    {

                        case 0:

                            Item bushDrop = ItemRegistry.Create(SpawnData.RandomBushForage());

                            if (bushDrop != null)
                            {

                                location.debris.Add(new Debris(bushDrop, creature.Position));

                            }

                            break;

                        case 1:

                            new ThrowHandle(Game1.player, creature.Position, herbal, 1).register();

                            break;


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


                    location.playSound(SpellHandle.Sounds.owl.ToString());

                }
                else if (creature.characterType == CharacterHandle.characters.SeaGull)
                {

                    location.playSound("seagulls");
                }
                else
                {

                    location.playSound(SpellHandle.Sounds.crow.ToString());


                }
            }
            else
            if (creature is StardewDruid.Character.Bat)
            {

                //location.playSound(SpellHandle.Sounds.batScreech.ToString());
                Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.BatScreech);
            }
            else
            if (creature is Critter)
            {
                if (creature.characterType == CharacterHandle.characters.YellowFox || creature.characterType == CharacterHandle.characters.RedFox)
                {

                    location.playSound(SpellHandle.Sounds.dog_bark.ToString());

                }
                else
                {

                    location.playSound(SpellHandle.Sounds.cat.ToString());

                }

            }
            else
            if (creature is Wolf)
            {

                location.playSound(SpellHandle.Sounds.dog_bark.ToString());

            }
            else
            if (creature is Bear)
            {

                switch (Mod.instance.randomIndex.Next(3))
                {

                    default:

                        Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.BearGrowl);

                        break;

                    case 2:

                        Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.BearRoar);

                        break;

                }

            }
            else
            if (creature is StardewDruid.Character.Serpent)
            {

                //location.playSound("serpentHit");
                Mod.instance.sounds.PlayCue(Handle.SoundHandle.SoundCue.SerpentCall);
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
