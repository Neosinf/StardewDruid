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
using StardewModdingAPI;
using System.Threading;
using StardewDruid.Cast.Stars;
using StardewDruid.Monster;


namespace StardewDruid.Event
{
    public class SpellHandle
    {

        public GameLocation location;

        public Vector2 destination;

        public Vector2 origin;

        public int missiles;

        public int counter;

        public int radius;

        public float damageFarmers;

        public float damageMonsters;

        public int power;

        public int environment;

        public int terrain;

        public int debris;

        public List<TemporaryAnimatedSprite> animations = new();

        public List<TemporaryAnimatedSprite> aoes = new();

        public Vector2 impact;

        public StardewDruid.Monster.Boss.Boss boss;

        public List<StardewValley.Monsters.Monster> monsters = new();

        public float critical;

        public bool external;

        public enum spells
        {
            explode,
            ballistic,
            beam,
            burn,
            fireball,
            wisp,
            chaos,
            meteor,
            bolt,
            rockfall,

        }

        public spells type;

        public enum indicators
        {
            target,
            weald,
            stars,
            fates,
            death,
        }

        public indicators indicator;

        public enum schemes
        {
            fire,
            stars,
            fates,
            ether,
            death,
        }

        public schemes scheme;

        public enum effects
        {
            sap,
            drain,
            knockdown,
            slow,
            burn,

        }

        public List<effects> added = new();

        public enum displays
        {
            none,
            Impact,
            Flashbang,
            Glare,
            Sparkle,
            Blaze,

        }

        public displays display;

        public enum sounds
        {
            none,
            explosion,
            flameSpellHit,
        }

        public sounds sound;
        

        public SpellHandle(GameLocation Location, Vector2 Destination, Vector2 Origin, int Radius = 2, int Missiles = 1, float Farmers = -1, float Monsters = -1, int Power = -1)
        {

            location = Location;

            origin = Origin;

            destination = Destination;

            radius = Radius;

            damageFarmers = Farmers;

            damageMonsters = Monsters;

            power = Power;

            environment = radius;

            terrain = 0;

            debris = 0;

            missiles = Missiles;

            impact = Destination;

            type = spells.fireball;

            scheme = schemes.fire;

            indicator = indicators.target;

            display = displays.none;

        }

        public void SpellQuery()
        {

            List<int> array = new()
            {
                (int)destination.X,
                (int)destination.Y,
                (int)origin.X,
                (int)origin.Y,
                radius,
                Convert.ToInt32(type),
                Convert.ToInt32(scheme),
                Convert.ToInt32(indicator),
            };

            QueryData query = new()
            {
                name = type.ToString(),

                value = System.Text.Json.JsonSerializer.Serialize<List<int>>(array),

                location = location.Name,

                longId = Game1.player.UniqueMultiplayerID,

            };

            Mod.instance.EventQuery(query, "SpellHandle");        
        
        }

        public bool Update()
        {

            counter++;

            if (counter == 1 && Context.IsMultiplayer && !external)
            {

                SpellQuery();

            }

            if(counter % 10 == 0 && boss != null)
            {

                if (!ModUtility.MonsterVitals(boss, location))
                {

                    Shutdown();

                    return false;

                }

            }

            switch (type)
            {

                case spells.explode:

                    if (counter == 1)
                    {

                        TargetCircle(0.35f);

                        return true;

                    }

                    if (counter == 15)
                    {

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, false);

                        RadialExplode();

                        RadialDisplay();

                        return true;

                    }

                    if (counter == 120)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case spells.meteor:

                    if (counter == 1)
                    {

                        indicator = indicators.stars;

                        scheme = schemes.stars;

                        display = displays.Impact;

                        if (radius > 5)
                        {
                            sound = sounds.explosion;
                        }
                        else
                        {
                            sound = sounds.flameSpellHit;
                        }

                        origin = destination - new Vector2(320, 640);

                        LaunchMissile();

                        TargetCircle();

                        return true;

                    }

                    if (counter == 60)
                    {

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters,false);

                        RadialExplode();

                        RadialDisplay();

                        return true;

                    }

                    if (counter == 120)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;


                case spells.bolt:

                    if (counter == 1)
                    {

                        LaunchBolt();

                        return true;

                    }

                    if (counter == 15)
                    {

                        LocalDamage();

                        return true;

                    }

                    if (counter == 30)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case spells.rockfall:

                    if (counter == 1)
                    {

                        LaunchRockfall();

                        return true;

                    }

                    if (counter == 36)
                    {

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters,false);

                        RadialExplode();

                        ModUtility.AnimateImpact(location, impact, 1);

                        RockDebris();

                        return true;

                    }

                    if (counter == 60)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case spells.ballistic:


                    if (counter == 1)
                    {

                        display = displays.Impact;

                        AdjustTarget();

                        TargetCircle(2);

                        return true;

                    }

                    if (counter == 60)
                    {

                        LaunchMissile();

                        return true;

                    }

                    if (counter == 120)
                    {

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, false);

                        RadialExplode();

                        RadialDisplay();

                        Game1.currentLocation.playSound("flameSpellHit", destination, 800 + new Random().Next(7) * 100);

                        return true;

                    }

                    if (counter == 180)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case spells.burn:

                    if (counter == 1)
                    {

                        BurnCircle();

                        BurnImpacts();

                        LightRadius(origin);

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

                        LightRadius(origin);

                    }

                    return true;

                case spells.fireball:

                    if (counter == 1)
                    {

                        display = displays.Impact;

                        AdjustTarget();

                        TargetCircle();

                        return true;

                    }

                    if (counter == 30)
                    {

                        LaunchMissile();

                        return true;
                    }

                    if (counter == 40)
                    {

                        GrazeDamage(1, 4);

                        return true;

                    }

                    if (counter == 70)
                    {

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, false);

                        RadialExplode();

                        RadialDisplay();

                        return true;

                    }

                    if (counter == 120)
                    {

                        Shutdown();

                        return false;
                    }

                    return true;

                case spells.beam:

                    if (counter == 1)
                    {

                        LaunchBeam();

                        LightRadius(origin);

                    }

                    if (counter == 20)
                    {

                        GrazeDamage(1, 2);

                    }

                    if (counter == 40)
                    {

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, true);

                        LightRadius(destination);

                    }

                    if (counter == 90)
                    {

                        Shutdown();

                        return false;

                    }

                    return true;

                case spells.chaos:

                    if (counter == 1)
                    {

                        LaunchChaos();

                    }

                    if (counter == 10)
                    {

                        GrazeDamage(1, 2);

                        Game1.currentLocation.playSound("flameSpellHit", impact, 600 + new Random().Next(1, 8) * 100);

                    }

                    if (counter == 15)
                    {

                        ApplyDamage(impact, radius, damageFarmers, damageMonsters, true);

                        LightRadius(impact);

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
        
        public void TargetCircle(float duration = 1)
        {

            TemporaryAnimatedSprite innerCircle = ModUtility.AnimateCursor(location, destination, indicator.ToString(), duration*1000);

            animations.Add(innerCircle);

        }

        public void LightRadius(Vector2 source)
        {

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

        public string ColourSchemes(string id)
        {

            if(id == "ether") { return "Blue"; }

            return "Red";

        }

        public void BurnCircle()
        {

            List<Vector2> impactVectors;
            
            Dictionary<Vector2,TemporaryAnimatedSprite> burnSprites = new();

            string colour = ColourSchemes(scheme.ToString());

            Texture2D flameTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", colour+"Embers.png"));

            for (int i = 0; i < Math.Min(3,radius + 1); i++)
            {

                impactVectors = ModUtility.GetTilesWithinRadius(location, new Vector2((int)(destination.X/64),(int)(destination.Y/64)), i);

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

            if(external) { return; }

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

                List<Farmer> farmerVictims = ModUtility.FarmerProximity(location, burnVectors, radius, true);

                /* (Vector2 vector in burnVectors)
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

                }*/

                foreach (Farmer victim in  farmerVictims)
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

        public Rectangle MissileRectangles(string id)
        {

            Microsoft.Xna.Framework.Rectangle rect = new(0, 0, 96, 96);

            switch (id)
            {

                case "stars":

                    rect.Y += 96;

                    break;

                case "fates":

                    rect.Y += 192;

                    break;

                case "ether":

                    rect.Y += 288;

                    break;

                case "death":

                    rect.Y += 384;

                    break;

            }

            return rect;

        }

        public Rectangle OverlayRectangles(string id)
        {

            Microsoft.Xna.Framework.Rectangle rect = new(-1, 0, 32, 32);

            switch (id)
            {

                case "stars":

                    rect = new(0, 32, 32, 32);

                    break;

                case "fates":

                    rect = new(96, 64, 32, 32);

                    break;

            }

            return rect;

        }

        public void LaunchBolt()
        {
            
            ModUtility.AnimateBolt(location, new Vector2(destination.X, destination.Y - 64), 600 + new Random().Next(1, 8) * 100);

        }

        public void AdjustTarget(int threshold = 384)
        {

            if (Vector2.Distance(origin, destination) < threshold)
            {

                Vector2 diff = ((destination - origin) / Vector2.Distance(origin, destination)) * threshold;

                destination = origin + diff;

                impact = destination;

            }

        }

        public void LaunchMissile()
        {

            Game1.currentLocation.playSound("fireball");

            float targetDepth = location.IsOutdoors ? (destination.Y / 640000) + 0.00001f : 999f;

            Vector2  diff = (destination - origin);
            
            float rotate = (float)Math.Atan2(diff.Y, diff.X);

            Vector2 motion = diff / 900;

            Rectangle rect = MissileRectangles(scheme.ToString());

            Vector2 setat = origin - (new Vector2(48,48)*radius);

            TemporaryAnimatedSprite missile = new(0, 75, 4, 3, setat, false, false)
            {

                sourceRect = rect,

                sourceRectStartingPos = new Vector2(rect.X, rect.Y),

                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Missiles.png")),

                scale = radius,

                layerDepth = targetDepth,

                timeBasedMotion = true,

                alpha = 0.65f,

                rotation = rotate,

                motion = motion,

            };

            location.temporarySprites.Add(missile);

            animations.Add(missile);

            Rectangle overlay = OverlayRectangles(scheme.ToString());

            if(overlay.X == -1)
            {

                return;

            }

            Vector2 setattwo = origin - (new Vector2(16, 16) * radius);

            TemporaryAnimatedSprite cursorAnimation = new(0, 900, 1, 1, setattwo, false, false)
            {

                sourceRect = overlay,

                sourceRectStartingPos = new Vector2(overlay.X, overlay.Y),

                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Cursors.png")),

                scale = radius,

                layerDepth = targetDepth + 0.0001f,

                timeBasedMotion = true,

                alpha = 0.75f,

                rotationChange = (float)(Math.PI / 60),

                motion = motion,

            };

            location.temporarySprites.Add(cursorAnimation);

            animations.Add(cursorAnimation);

        }

        public void LaunchBeam()
        {

            Texture2D beamTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Laser.png"));

            Vector2 diff = (destination - origin) / Vector2.Distance(origin, destination);

            Vector2 middle = diff * 336;

            impact = origin + (diff * 560);

            float rotate = (float)Math.Atan2(diff.Y, diff.X);

            Vector2 setPosition = origin + middle - new Vector2(320,64);

            TemporaryAnimatedSprite beam = new(0, 125f, 8, 1, setPosition, false, false)
            {
                sourceRect = new(0, 0, 160, 32),
                sourceRectStartingPos = new Vector2(0.0f, 0.0f),
                texture = beamTexture,
                scale = 4f,
                timeBasedMotion = true,
                layerDepth = 999f,
                rotation = rotate,
                alpha = 0.65f,

            };

            location.temporarySprites.Add(beam);

            animations.Add(beam);

        }

        public void LaunchChaos()
        {

            Texture2D beamTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Chaos.png"));

            Vector2 ratio = (destination - origin) / Vector2.Distance(origin, destination);

            Vector2 diff = ratio * 192;

            impact = origin + (ratio * 384);

            float rotate = (float)Math.Atan2(diff.Y, diff.X);

            Vector2 setPosition = origin + diff - new Vector2(192, 96);

            TemporaryAnimatedSprite beam = new(0, 75f, 6, 1, setPosition, false, false)
            {
                sourceRect = new(0, 0, 128, 64),
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

        public void LaunchRockfall()
        {

            List<int> indexes = Map.SpawnData.RockFall(location, Game1.player, Mod.instance.rockCasts[Mod.instance.rite.castLocation.Name]);

            int objectIndex = indexes[0];

            int scatterIndex = indexes[1];

            if (debris == 1)
            {

                debris = indexes[2];

            }

            ModUtility.AnimateRockfall(location, destination / 64, 0, objectIndex, scatterIndex);

        }

        public void RadialDisplay()
        {

            LightRadius(impact);

            if (display != displays.none)
            {

                ModUtility.AnimateImpact(location, impact, radius,0, display.ToString());

            }

            if(sound != sounds.none)
            {

                Game1.currentLocation.playSound(sound.ToString());

            }

        }

        public void GrazeDamage(int piece, int division, float reach = -1)
        {

            if (external)
            {

                return;

            }

            Vector2 diff = (impact - origin) / division * piece;

            Vector2 current = origin + diff;

            if(reach == -1)
            {
                reach = radius / 2;

            }

            ApplyDamage(current, reach, (int)(damageFarmers / 2), (int)(damageMonsters / 2), true);

        }

        public void ApplyDamage(Vector2 position, float reach, float hitfarmers, float hitmonsters, bool individuals = true)
        {
            
            if (external)
            {

                return;

            }

            if (hitfarmers > 0 && boss != null)
            {

                List<Farmer> farmers = ModUtility.FarmerProximity(location, new() { position }, reach, true);

                if (farmers.Count > 0 && individuals && display != displays.none)
                {

                    foreach (Farmer farmer in farmers)
                    {

                        ModUtility.AnimateImpact(location, farmer.Position, 0, 2, display.ToString(), 50);

                    }

                }

                ModUtility.DamageFarmers(location, farmers, (int)hitfarmers, boss);

            }
            
            if (hitmonsters > 0)
            {

                if(monsters.Count == 0)
                {
                    
                    monsters = ModUtility.MonsterProximity(location, new() { position }, reach, true);

                }

                if (monsters.Count > 0 && individuals && display != displays.none)
                {
                    
                    foreach (StardewValley.Monsters.Monster victim in monsters)
                    {

                        if (!MonsterData.BossMonster(victim))
                        {
                            
                            ModUtility.AnimateImpact(location, victim.Position, 0, 2, display.ToString(), 50);
                        
                        }

                    }

                }

                ModUtility.DamageMonsters(location, monsters, Game1.player, (int)hitmonsters, true);

                if(added.Count > 0)
                {

                    foreach(effects effect in added)
                    {

                        switch (effect)
                        {
                            case effects.sap:

                                SapEffect(Mod.instance.PowerLevel);

                                break;

                            case effects.knockdown:

                                KnockEffect(2000);

                                break;

                        }

                    }

                }

            }

        }

        public void LocalDamage()
        {

            if (external)
            {

                return;

            }

            int damageApplied = (int)(damageMonsters * 0.7);

            bool critApplied = false;

            float critDamage = ModUtility.CalculateCritical(damageApplied, critical);

            if (critDamage > 0)
            {

                damageApplied = (int)critDamage;

                critApplied = true;

            }

            List<int> diff = ModUtility.CalculatePush(location, monsters.First(), Game1.player.Position, 64);

            ModUtility.HitMonster(location, Game1.player, monsters.First(), damageApplied, critApplied, diffX: diff[0], diffY: diff[1]);

        }

        public void RadialExplode()
        {

            if (external)
            {

                return;

            }

            if (power > 0)
            {

                ModUtility.Explode(location, impact / 64, Game1.player, environment, power);

            }

            if (terrain > 0)
            {

                ModUtility.Reave(location, impact / 64, Game1.player, terrain);

            }

        }

        public void RockDebris()
        {

            if (external)
            {

                return;

            }

            if (debris == 0)
            {
                
                return;

            }

            Random randomIndex = new();

            int rockCut = randomIndex.Next(2);

            int generateAmt = Math.Max(1,randomIndex.Next(Mod.instance.PowerLevel));

            Vector2 targetVector = destination / 64;

            for (int i = 0; i < generateAmt; i++)
            {
                if (i == 0)
                {

                    if (Game1.player.professions.Contains(21) && rockCut == 0)
                    {

                        Game1.createObjectDebris("382", (int)targetVector.X, (int)targetVector.Y);

                    }
                    else if (Game1.player.professions.Contains(19) && rockCut == 0)
                    {

                        Game1.createObjectDebris(debris.ToString(), (int)targetVector.X, (int)targetVector.Y);

                    }

                    Game1.createObjectDebris(debris.ToString(), (int)targetVector.X, (int)targetVector.Y);

                }
                else
                {

                    Game1.createObjectDebris("390", (int)targetVector.X, (int)targetVector.Y);

                }

            }

            if (!Mod.instance.rite.castTask.ContainsKey("masterRockfall"))
            {

                Mod.instance.UpdateTask("lessonRockfall", generateAmt);

            }

        }

        public void KnockEffect(int knock)
        {

            if (external)
            {

                return;

            }

            if (!Mod.instance.eventRegister.ContainsKey("knockout"))
            {

                Cast.Stars.Knockout Knockout = new();

                Knockout.EventTrigger();

            }

            foreach (var monster in monsters)
            {

                if (!ModUtility.MonsterVitals(monster, monster.currentLocation))
                {
                    continue;
                }

                (Mod.instance.eventRegister["knockout"] as Knockout).AddVictim(monster, knock);

            }


        }

        public void SapEffect(int drain)
        {

            if (external)
            {

                return;

            }

            int leech = 0;

            foreach (var monster in monsters)
            {

                if (!ModUtility.MonsterVitals(monster, monster.currentLocation))
                {
                    continue;
                }

                ModUtility.AnimateGlare(monster.currentLocation, monster.Position, Color.Teal);

                ModUtility.DamageMonsters(monster.currentLocation, new List<StardewValley.Monsters.Monster>() { monster }, Game1.player, drain);

                leech += drain;

            }

            Mod.instance.CastMessage(leech.ToString() + " drained", 0, true);

            int num = Math.Min(leech, Mod.instance.rite.caster.MaxStamina - (int)Mod.instance.rite.caster.stamina);

            if (num > 0)
            {

                Mod.instance.rite.caster.stamina += num;

                Rectangle healthBox = Mod.instance.rite.caster.GetBoundingBox();

                location.debris.Add(
                    new Debris(
                        num,
                        new Vector2(healthBox.Center.X + 16, healthBox.Center.Y),
                        Color.Teal,
                        0.75f,
                        Mod.instance.rite.caster
                    )
                );

            }

        }

    }

}
