using Microsoft.Xna.Framework;
using StardewValley;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Reflection;
using StardewValley.Monsters;
using StardewDruid.Cast;
using StardewDruid.Map;
using System.Linq;
using StardewDruid.Cast.Ether;
using Force.DeepCloner;


namespace StardewDruid.Event
{
    public class BarrageHandle
    {

        public GameLocation location;

        public Vector2 destination;

        public Vector2 origin;

        public Vector2 destinationPosition;

        public Vector2 originPosition;

        public int missiles;

        public int counter;

        public int radius;

        public float damageFarmers;

        public float damageMonsters;

        public int damageEnvironment;

        public int damageDirt;

        public List<TemporaryAnimatedSprite> animations;
        
        public List<TemporaryAnimatedSprite> aoes;

        public Dictionary<int, List<Vector2>> impacts;

        public StardewValley.Monsters.Monster monster;

        public string colour;

        public enum barrageType
        {
            missile,
            beam,
            burn,
            fireball,
            cometball,
            wisp,
            bullet,
            chaos,
        }

        public barrageType type;

        public BarrageHandle(GameLocation Location, Vector2 Destination, Vector2 Origin, int Radius = 3, int Missiles = 1, float Farmers = -1, float Monsters = -1, int Environment = -1, int Dirt = -1)
        {

            location = Location;

            destination = Destination;

            origin = Origin;

            originPosition = origin * 64;

            destinationPosition = destination * 64;

            radius = Radius;

            damageFarmers = Farmers;

            damageMonsters = Monsters;

            damageEnvironment = Environment;

            damageDirt = Dirt;

            missiles = Missiles;

            animations = new();

            aoes = new();

            impacts = new() { [0] = new(){destination,} };

            type = barrageType.missile;

            colour = "Red";

        }

        public bool Update()
        {

            counter++;

            switch (type)
            {

                case barrageType.missile:

                    if (counter == 1)
                    {

                        TargetCircle(2);

                        LaunchBarrage();

                        return true;

                    }

                    if (counter == 120)
                    {

                        RadialDamage();

                        LightRadius();

                        RadialImpact(0, radius - 1);

                        return true;

                    }

                    if (counter == 180)
                    {

                        Shutdown();

                        return false;

                    }


                    return true;

                case barrageType.burn:

                    if (counter == 1)
                    {

                        BurnCircle();

                        BurnImpacts();

                        LightRadius();

                        return true;

                    }

                    if (counter == 180)
                    {

                        Shutdown();

                        return false;

                    }

                    if (counter % 60 == 0)
                    {

                        BurnImpacts();

                        LightRadius();

                    }

                    return true;

                case barrageType.fireball:
                case barrageType.cometball:

                    if (counter == 1)
                    {

                        TargetCircle();
                        return true;
                    }

                    if (counter == 30)
                    {

                        LaunchFireball(2);

                        return true;
                    }

                    if (counter == 50)
                    {

                        RadialDamage(1);

                        LightRadius(1);

                        return true;

                    }

                    if (counter == 70)
                    {
                        RadialDamage();

                        LightRadius();

                        RadialImpact(0,radius-1);

                        return true;

                    }

                    if (counter == 120)
                    {

                        Shutdown();

                        return false;
                    }
                 
                    return true;

                case barrageType.beam:

                    if (counter == 1)
                    {

                        LaunchBeam();

                        return true;

                    }

                    if (counter == 30)
                    {

                        RadialDamage();

                        LightRadius();

                        return true;
                    }

                    if (counter == 90)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case barrageType.chaos:

                    if (counter == 1)
                    {

                        LaunchChaos();

                        return true;

                    }

                    if (counter == 30)
                    {

                        RadialDamage();

                        LightRadius();

                        RadialImpact(0, 3);

                        return true;

                    }

                    if (counter == 60)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

            }

            return true;
        }

        public void Shutdown()
        {

            if (animations.Count > 0)
            {

                foreach (TemporaryAnimatedSprite animatedSprite in animations)
                {

                    location.temporarySprites.Remove(animatedSprite);

                }

            }

            if (aoes.Count > 0)
            {

                foreach (TemporaryAnimatedSprite animatedSprite in animations)
                {

                    location.temporarySprites.Remove(animatedSprite);

                }

            }
        }
        
        public void TargetCircle(int duration = 1)
        {

            TemporaryAnimatedSprite innerCircle = ModUtility.AnimateCursor(location, destinationPosition, destinationPosition, "Target", duration*1000);

            animations.Add(innerCircle);

        }

        public void LightRadius(int impact = 0)
        {
            Vector2 source;

            if (impact < 0)
            {

                source = originPosition;

            }
            else
            {

                if (!impacts.ContainsKey(impact))
                {

                    return;

                }

                if(impacts[impact].Count == 0)
                {

                    return;

                }

                source = impacts[impact].First() * 64;

            }

            TemporaryAnimatedSprite lightCircle = new(23, 200f, 6, 1, source, false, Game1.random.NextDouble() < 0.5)
            {
                texture = Game1.mouseCursors,
                light = true,
                lightRadius = 3f,
                lightcolor = Color.Black,
                alphaFade = 0.03f,
                Parent = location,
            };

            location.temporarySprites.Add(lightCircle);

            animations.Add(lightCircle);


        }

        public void BurnCircle()
        {

            List<Vector2> impactVectors;
            
            Dictionary<Vector2,TemporaryAnimatedSprite> burnSprites = new();

            Texture2D flameTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", colour+"Embers.png"));

            for (int i = 0; i < Math.Min(3,radius + 1); i++)
            {

                impactVectors = ModUtility.GetTilesWithinRadius(location, destination, i);

                foreach (Vector2 vector in impactVectors)
                {

                    Vector2 position = new((vector.X * 64), (vector.Y * 64));

                    TemporaryAnimatedSprite burnSprite =  new(0, 150, 4, 8, position, false, false)
                    {

                        sourceRect = new(0, i * 32, 32, 32),

                        sourceRectStartingPos = new(0, i * 32),

                        texture = flameTexture,

                        scale = 2f,

                        extraInfoForEndBehavior = 99999,

                        layerDepth = vector.Y / 10000,

                        alpha = 0.5f,

                    };

                    burnSprites.Add(vector,burnSprite);

                }

            }

            /*Texture2D flameTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Bonfire.png"));

            Vector2 position = new((destination.X * 64)-32, (destination.Y * 64)-32);

            TemporaryAnimatedSprite burnSprite = new(0, 125, 4, 8, position, false, false)
            {

                sourceRect = new(0, 0, 32, 32),

                sourceRectStartingPos = new(0,0),

                texture = flameTexture,

                scale = 3f,

                extraInfoForEndBehavior = 99999,

                layerDepth = destination.Y / 10000,

                alpha = 0.65f,
            };

            burnSprites.Add(destination, burnSprite);*/

            for (int i = location.temporarySprites.Count - 1; i >= 0; i--)
            {

                TemporaryAnimatedSprite sprite = location.temporarySprites.ElementAt(i);

                if (sprite.extraInfoForEndBehavior == 99999)
                {

                    Vector2 localVector;

                    //localVector = new((int)(sprite.Position.X / 64), (int)(sprite.Position.Y / 64));

                    localVector = new((int)((sprite.Position.X) / 64), (int)((sprite.Position.Y) / 64));

                    if (burnSprites.ContainsKey(localVector))
                    {

                        if(sprite.sourceRect.Y < burnSprites[localVector].sourceRect.Y)
                        {
                            burnSprites[localVector].sourceRect = sprite.sourceRect;
                            burnSprites[localVector].sourceRectStartingPos = sprite.sourceRectStartingPos;
                            burnSprites[localVector].Position = sprite.Position;
                            burnSprites[localVector].timer = sprite.timer;
                        }

                        location.temporarySprites.Remove(sprite);

                    }
                    else
                    {

                        aoes.Add(sprite);

                    }

                }

            }

            foreach(KeyValuePair< Vector2,TemporaryAnimatedSprite> spritePair in burnSprites)
            {

                location.temporarySprites.Add(spritePair.Value);

                aoes.Add(spritePair.Value);

            }

        }

        public void BurnImpacts()
        {

            List<Vector2> burnVectors = new();

            for (int i = aoes.Count - 1; i >= 0; i--)
            {

                TemporaryAnimatedSprite sprite = aoes.ElementAt(i);

                if (!location.temporarySprites.Contains(sprite))
                {

                    aoes.RemoveAt(i);

                    continue;

                }

                Vector2 localVector = new((int)(sprite.Position.X / 64), (int)(sprite.Position.Y / 64));

                burnVectors.Add(localVector*64);

            }

            if (!Mod.instance.eventRegister.ContainsKey("immolate"))
            {

                new Immolate(Game1.player.Position).EventTrigger();

            }

            Immolate immolateEvent = Mod.instance.eventRegister["immolate"] as Immolate;

            immolateEvent.expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 10;

            if (damageFarmers > 0)
            {

                List<Farmer> farmerVictims = new();

                foreach(Vector2 vector in burnVectors)
                {
                    
                    List<Farmer> foundVictims = ModUtility.FarmerProximity(location, vector, radius, true);

                    if(foundVictims.Count > 0)
                    {

                        foreach(Farmer found in foundVictims)
                        {

                            if (!farmerVictims.Contains(found))
                            {

                                farmerVictims.Add(found);

                            }

                        }

                    }

                }

                foreach(Farmer victim in  farmerVictims)
                {

                    if(immolateEvent.farmerVictims.ContainsKey(victim))
                    {

                        immolateEvent.farmerVictims[victim].timer = 3;

                        immolateEvent.farmerVictims[victim].damage = (int)damageFarmers;

                    }
                    else
                    {

                        immolateEvent.farmerVictims.Add(victim, new((int)damageFarmers));
                    
                    }

                }

            }

            if (damageMonsters > 0)
            {

                List<StardewValley.Monsters.Monster> monsterVictims = ModUtility.MonsterProximity(location, burnVectors, radius, true);

                foreach (StardewValley.Monsters.Monster victim in monsterVictims)
                {

                    if (immolateEvent.monsterVictims.ContainsKey(victim))
                    {

                        immolateEvent.monsterVictims[victim].timer = 3;

                        immolateEvent.monsterVictims[victim].damage = (int)damageMonsters;

                    }
                    else
                    {

                        immolateEvent.monsterVictims.Add(victim, new((int)damageMonsters));

                    }

                }

            }

        }

        public void LaunchBarrage()
        {

            impacts = new();

            Random random = new();

            List<Vector2> scatters = new()
            {

                new(0,-1),
                new(0,-2),
                new(1,0),
                new(2,0),
                new(0,1),
                new(0,2),
                new(-1,0),
                new(-2,0),

            };

            for (int i = 0; i < missiles; i++)
            {

                if (i == 0)
                {
                    impacts.Add(i,new() { destination, });

                    continue;
                }

                Vector2 scatter = destination + scatters[random.Next(scatters.Count)];

                impacts.Add(i, new() { scatter, });

            }

            for (int i = 0; i < impacts.Count; i++)
            {

                Vector2 impactPosition = impacts[i][0] * 64;

                TemporaryAnimatedSprite missile = ModUtility.AnimateCursor(location, originPosition, impactPosition, "Barrage", 2000);

                animations.Add(missile);

            }

        }

        public void LaunchFireball(int hits = 1)
        {

            impacts = new();

            Vector2 diff = (destination - origin);

            List<Vector2> hitVectors = new();

            for (int i = 0; i < hits; i++)
            {

                if(i == 0)
                {

                    hitVectors.Add(destination);
                
                    continue;
                
                }

                hitVectors.Add(origin + ((diff / hits) * i));

            }

            impacts.Add(0, hitVectors);

            Vector2 originPositionTwo = originPosition;

            originPositionTwo.Y -= 8 * radius;

            string cursor = type == barrageType.cometball ? "Comet" : "Missile";

            TemporaryAnimatedSprite missile = ModUtility.AnimateCursor(location, originPositionTwo, destinationPosition, cursor, 700, radius);

            animations.Add(missile);

        }

        public void LaunchBeam()
        {

            Texture2D beamTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Laser.png"));

            Vector2 diff = (destinationPosition - originPosition) / Vector2.Distance(originPosition, destinationPosition) * 384;

            float rotate = (float)Math.Atan2(diff.Y, diff.X);

            Vector2 setPosition = originPosition + diff - new Vector2(320,64);

            TemporaryAnimatedSprite beam = new(0, 125f, 8, 1, setPosition, false, false)
            {
                sourceRect = new(0, 0, 160, 32),
                sourceRectStartingPos = new Vector2(0.0f, 0.0f),
                texture = beamTexture,
                scale = 4f,
                timeBasedMotion = true,
                layerDepth = 999f,
                rotation = rotate,

            };

            location.temporarySprites.Add(beam);

            animations.Add(beam);

            TemporaryAnimatedSprite beam2 = new(0, 500f, 1, 1, setPosition, false, false)
            {
                sourceRect = new(0, 224, 160, 32),
                sourceRectStartingPos = new Vector2(0.0f, 224f),
                texture = beamTexture,
                scale = 4f,
                timeBasedMotion = true,
                layerDepth = 999f,
                rotation = rotate,
                alphaFade = 0.002f,
                delayBeforeAnimationStart = 1000,
            };

            location.temporarySprites.Add(beam2);

            animations.Add(beam2);

        }

        public void LaunchChaos()
        {

            Texture2D beamTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Chaos.png"));

            Vector2 ratio = (destinationPosition - originPosition) / Vector2.Distance(originPosition, destinationPosition);

            Vector2 diff = ratio * 240;

            Vector2 terminus = originPosition + (ratio * 360);

            Vector2 zone = new((int)(terminus.X / 64), (int)(terminus.Y / 64));

            impacts = new() { [0] = new() { zone, } };

            radius = 4;

            float rotate = (float)Math.Atan2(diff.Y, diff.X);

            Vector2 setPosition = originPosition + diff - new Vector2(240, 96);

            TemporaryAnimatedSprite beam = new(0, 120f, 5, 1, setPosition, false, false)
            {
                sourceRect = new(0, 0, 160, 64),
                sourceRectStartingPos = new Vector2(0.0f, 0.0f),
                texture = beamTexture,
                scale = 3f,
                timeBasedMotion = true,
                layerDepth = 999f,
                rotation = rotate,
                alphaFade = 0.001f,

            };

            location.temporarySprites.Add(beam);


        }

        public void RadialDamage(int hit = 0)
        {

            int zone;

            for(int i = 0; i < impacts.Count; i++)
            {

                if (impacts[i].Count == 0)
                {

                    continue;

                }

                Vector2 impact = impacts[i][hit];

                if (type == barrageType.burn)
                {
                    zone = radius - 1;
                }
                else if (i == 0 && hit == 0)
                {
                    zone = radius;
                }
                else
                {
                    zone = radius - 1;

                }

                if (damageFarmers > 0)
                {

                    ModUtility.DamageFarmers(location, ModUtility.FarmerProximity(location, impact * 64, zone, true), (int)damageFarmers, monster);
                
                }

                if (damageMonsters > 0)
                {
                    
                    ModUtility.DamageMonsters(location, ModUtility.MonsterProximity(location, new() { impact * 64 }, zone, true), Game1.player, (int)damageMonsters, true);

                }

                if(damageEnvironment > 0)
                {

                    ModUtility.Explode(location, impact, Game1.player, zone, damageEnvironment, damageDirt);

                }

            }

        }

        public void RadialImpact(int hit = 0, int reach = 1)
        {

            for (int i = 0; i < impacts.Count; i++)
            {

                if (impacts[i].Count == 0)
                {

                    continue;

                }

                Vector2 impact = impacts[i][hit];

                ModUtility.AnimateImpact(location, impact, reach);

            }

        }

    }

}
