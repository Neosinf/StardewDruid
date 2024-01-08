using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Map;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using xTile.Layers;

namespace StardewDruid.Event
{
    public class EventHandle
    {

        public int activeCounter;

        public readonly Rite riteData;

        public GameLocation targetLocation;

        public Vector2 targetVector;

        public Farmer targetPlayer;

        public double expireTime;

        public bool expireEarly;

        public bool eventAbort;

        public int eventLinger;

        public Random randomIndex;

        public MonsterHandle monsterHandle;

        //public List<Torch> torchList;

        public List<StardewDruid.Event.Brazier> braziers;

        public List<StardewDruid.Character.Actor> actors;

        public Vector2 voicePosition;

        public bool soundTrack;

        public bool eventSync;

        public List<Vector2> ladders;

        public EventHandle(Vector2 target, Rite rite)
        {

            riteData = rite;

            targetVector = target;

            targetPlayer = rite.caster;

            targetLocation = rite.castLocation;

            randomIndex = rite.randomIndex;

            eventLinger = -1;

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 60;

            voicePosition = target * 64;

            braziers = new();

            actors = new();

            ladders = new();

        }

        public virtual void EventTrigger()
        {

            Mod.instance.RegisterEvent(this, "event");

        }

        public virtual void MinutesLeft(int minutes)
        {

            Game1.addHUDMessage(new HUDMessage($"{minutes} more minutes left!", "2"));

        }

        public virtual bool EventActive()
        {

            if (eventAbort)
            {

                EventAbort();

                return false;

            }

            if (targetPlayer.currentLocation.Name != targetLocation.Name)
            {

                EventAbort();

                return false;

            }

            if (expireEarly)
            {

                return EventExpire();

            }

            double nowTime = Game1.currentGameTime.TotalGameTime.TotalSeconds;

            if (expireTime < nowTime)
            {

                return EventExpire();

            }

            return true;

        }

        public virtual bool EventPerformAction(SButton Button, string type = "Action")
        {

            return false;

        }

        public virtual void EventDecimal()
        {


        }

        public virtual void EventExtend()
        {

            expireTime++;

        }

        public virtual void AttemptAbort()
        {


        }

        public virtual void EventAbort()
        {

            eventLinger = 0;

        }

        public virtual bool EventExpire()
        {

            if (eventLinger > 0)
            {

                eventLinger--;

                return true;

            }

            return false;

        }

        public virtual void RemoveMonsters()
        {

            if (monsterHandle != null)
            {

                monsterHandle.ShutDown();

            }

        }

        public virtual void RemoveBraziers()
        {

            if (braziers.Count > 0)
            {

                foreach (Brazier brazier in braziers)
                {

                    brazier.shutdown();

                }

                braziers.Clear();

            }

        }

        public virtual void RemoveActors()
        {

            if (actors.Count > 0)
            {

                foreach (StardewDruid.Character.Character actor in actors)
                {

                    actor.currentLocation.characters.Remove(actor);

                }

                actors.Clear();


            }


        }


        public virtual void EventRemove()
        {

            RemoveMonsters();

            RemoveBraziers();

            RemoveActors();

            if (soundTrack)
            {

                Game1.stopMusicTrack(Game1.MusicContext.Default);

            }

        }

        public virtual void EventInterval()
        {

            activeCounter++;

            if (eventLinger != -1)
            {

                return;

            }

            if (monsterHandle != null)
            {

                monsterHandle.SpawnInterval();

            }

        }

        public void CastVoice(string message, int duration = 2000)
        {

            if (actors.Count <= 0)
            {

                this.AddActor(this.voicePosition);

            }

            actors[0].showTextAboveHead(message, duration: duration);

        }

        public void SetTrack(string track)
        {

            Game1.changeMusicTrack(track, false, Game1.MusicContext.Default);

            soundTrack = true;

        }

        public void AddActor(Vector2 position, bool slave = false)
        {
            
            Actor actor = CharacterData.DisembodiedVoice(this.targetLocation, position);

            actor.drawSlave = slave;

            targetLocation.characters.Add(actor);

            actors.Add(actor);

        }

        public void RemoveLadders()
        {
            Layer layer = targetLocation.map.GetLayer("Buildings");

            for (int index1 = 0; index1 < layer.LayerHeight; ++index1)
            {
                
                for (int index2 = 0; index2 < layer.LayerWidth; ++index2)
                {
                    
                    if (layer.Tiles[index2, index1] != null && layer.Tiles[index2, index1].TileIndex == 173)
                    {
                        
                        layer.Tiles[index2, index1] = null;
                        
                        Game1.player.TemporaryPassableTiles.Clear();
                        
                        if (ladders.Count == 0)
                        {
                            ladders.Add(new Vector2(index2, index1));
                        }

                    }
                
                }
            
            }
        
        }

    }

}
