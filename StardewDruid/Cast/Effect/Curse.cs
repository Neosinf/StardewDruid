using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Companions;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Projectiles;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Threading;
using static StardewDruid.Cast.SpellHandle;
using static StardewDruid.Data.IconData;


namespace StardewDruid.Cast.Effect
{
    public class Curse : EventHandle
    {

        public Dictionary<StardewValley.Monsters.Monster, CurseTarget> victims = new();

        public Curse()
          : base()
        {

        }

        public override void EventDraw(SpriteBatch b)
        {

            foreach(KeyValuePair<StardewValley.Monsters.Monster, CurseTarget> victim in victims)
            {

                victim.Value.draw(b);

            }

        }

        public void AddTarget(GameLocation location, StardewValley.Monsters.Monster monster, SpellHandle.effects effect)
        {

            if (!ModUtility.MonsterVitals(monster, location))
            {

                return;

            }

            if (victims.ContainsKey(monster))
            {

                return;

            }

            if (monster is Monster.Boss boss)
            {

                effect = boss.IsCursable(effect);

            }

            switch (effect)
            {

                case SpellHandle.effects.none:

                    return;

                case SpellHandle.effects.morph:

                    if (monster.isGlider.Value || ModUtility.GroundCheck(location,monster.Tile) != "ground" || monster is DustSpirit)
                    {

                        effect = SpellHandle.effects.daze;

                    }

                    DustSpirit morph = new(monster.Position);

                    morph.update(Game1.currentGameTime, location);

                    morph.MaxHealth = monster.MaxHealth * 2;

                    morph.Health = morph.MaxHealth;

                    morph.Scale *= 3;

                    morph.Scale /= 2;

                    location.characters.Add(morph);

                    monster.Health = 0;

                    monster.currentLocation.characters.Remove(monster);

                    monster = morph;

                    break;

                case SpellHandle.effects.knock:

                    if (monster.isGlider.Value)
                    {
                        
                        effect = SpellHandle.effects.daze;

                    }

                    break;


            }

            victims.Add(monster, new(location, monster, 5, effect));

            activeLimit = eventCounter + 5;

        }

        public override void EventRemove()
        {

            for (int g = victims.Count - 1; g >= 0; g--)
            {

                KeyValuePair<StardewValley.Monsters.Monster, CurseTarget> knockTarget = victims.ElementAt(g);

                knockTarget.Value.ShutDown();

            }

            base.EventRemove();

        }

        public override void EventDecimal()
        {

            for (int g = victims.Count - 1; g >= 0; g--)
            {

                KeyValuePair<StardewValley.Monsters.Monster, CurseTarget> knockTarget = victims.ElementAt(g);

                if (!knockTarget.Value.Update())
                {

                    victims.Remove(knockTarget.Key);

                }

            }

        }


    }

    public class CurseTarget
    {

        public GameLocation location;

        public StardewValley.Monsters.Monster victim;

        public TemporaryAnimatedSprite animation;

        public int timer;

        public int warptimer;

        public List<float> stats = new();

        public SpellHandle.effects type;

        public IconData.displays display;

        public CurseTarget(GameLocation Location, StardewValley.Monsters.Monster Victim, int Timer, SpellHandle.effects Type = SpellHandle.effects.knock)
        {

            location = Location;

            victim = Victim;

            timer = Timer * 10;

            type = Type;

            switch (type)
            {

                case SpellHandle.effects.morph:

                    display = displays.morph;

                    break;
                
                case SpellHandle.effects.mug:

                    display = displays.herbalism;

                    if (victim.objectsToDrop.Count > 0)
                    {

                        string dropNormal = victim.objectsToDrop[Mod.instance.randomIndex.Next(victim.objectsToDrop.Count)];

                        StardewValley.Object itemNormal = new StardewValley.Object(dropNormal, 1);

                        if (itemNormal.QualifiedItemId.Contains("-"))
                        {

                            victim.objectsToDrop.Clear();

                        }

                        Game1.createItemDebris(itemNormal, victim.Position + new Vector2(0, 32), 2, location, -1);

                        break;

                    }

                    SpawnData.MonsterDrops(victim, (SpawnData.drops)Mod.instance.randomIndex.Next(1,5));

                    string drop = victim.objectsToDrop[Mod.instance.randomIndex.Next(victim.objectsToDrop.Count)];

                    StardewValley.Object dropItem = new StardewValley.Object(drop, 1);

                    Game1.createItemDebris(dropItem, victim.Position + new Vector2(0, 32), 2, location, -1);

                    break;

                case SpellHandle.effects.glare:

                    display = displays.glare;

                    stats.Add((float)victim.Health);

                    break;

                case SpellHandle.effects.daze:

                    display = displays.blind;

                    if (victim is StardewDruid.Monster.Boss boss)
                    {
                        
                        boss.debuffJuice = 0.5f;

                        break;

                    }

                    stats.Add((float)victim.speed);

                    victim.speed = victim.speed / 2;

                    victim.randomSquareMovement(Game1.currentGameTime);

                    break;
                
                case SpellHandle.effects.doom:

                    if((double)victim.Health <= (double)(victim.MaxHealth * 0.07))
                    {

                        PerformInstantDeath();

                    }

                    timer *= 2;

                    display = displays.skull;

                    break;
                
                case SpellHandle.effects.immolate:

                    display = displays.blaze;

                    break;

                default:
                case SpellHandle.effects.knock:

                    display = displays.knock;

                    stats.Add(Victim.rotation);

                    Victim.Halt();

                    Victim.stunTime.Set(Math.Max(Victim.stunTime.Value, Timer * 1000));

                    if (Victim.Sprite.getWidth() > 16)
                    {

                        Victim.rotation = (float)(Math.PI);

                    }
                    else
                    {

                        Victim.rotation = (float)(Math.PI / 2);

                    }

                    break;

            }

        }

        public void ShutDown()
        {
            
            switch (type)
            {

                case SpellHandle.effects.knock:

                    victim.rotation = stats[0];

                    break;

                case SpellHandle.effects.daze:

                    if (victim is StardewDruid.Monster.Boss boss)
                    {
                        
                        boss.debuffJuice = 0f;

                        break;

                    }
                    
                    victim.speed = (int)stats[0];

                    break;

            }

        }

        public bool Update()
        {

            timer--;

            if (timer <= 0)
            {

                ShutDown();

                return false;

            }

            if (!ModUtility.MonsterVitals(victim, location))
            {

                if(type == SpellHandle.effects.immolate)
                {

                    PerformBarbeque();

                }

                ShutDown();

                return false;

            }

            if (victim is StardewValley.Monsters.Mummy mummy)
            {

                if (mummy.reviveTimer.Value > 0)
                {

                    ModUtility.HitMonster(Game1.player, mummy, 1, false);

                    return false;

                }

            }

            switch (type)
            {

                case SpellHandle.effects.glare:

                    if(stats[0] <= ((float)victim.Health - 10f))
                    {

                        PerformCritical();

                        ShutDown();

                        return false;

                    }

                    break;

                case SpellHandle.effects.daze:
                    
                    if (victim is not StardewDruid.Monster.Boss boss)
                    {

                        if (timer % 10 == 0)
                        {

                            victim.randomSquareMovement(Game1.currentGameTime);

                        }

                    }


                    break;

                case SpellHandle.effects.knock:

                    victim.Halt();

                    if(victim.stunTime.Value <= 0)
                    {

                        victim.stunTime.Set(timer * 100);

                    }

                    break;

                case SpellHandle.effects.doom:

                    if(timer == 1)
                    {

                        PerformInstantDeath();

                    }

                    break;

                case SpellHandle.effects.immolate:

                    if (timer % 10 == 5)
                    {

                        ModUtility.HitMonster(Game1.player, victim, Mod.instance.CombatDamage() / 10, false, 0, 0);

                    }

                    break;

            }

            return true;

        }

        public void PerformInstantDeath()
        {

            SpellHandle death = new(Game1.player, new() { victim }, 999);

            death.display = impacts.deathbomb;

            death.scheme = schemes.fates;

            Mod.instance.spellRegister.Add(death);

        }

        public void PerformCritical()
        {

            SpellHandle critical = new(Game1.player, new() { victim }, Mod.instance.CombatDamage() / 2);

            critical.display = impacts.flashbang;

            critical.scheme = schemes.golden;

            Mod.instance.spellRegister.Add(critical);

        }

        public void PerformBarbeque()
        {

            if (new Random().Next(5) == 0)
            {

                Mod.instance.iconData.ImpactIndicator(location, victim.Position, IconData.impacts.deathbomb, 4f, new() { scheme = schemes.stars, });

                int barbeque = SpawnData.RandomBarbeque();

                ThrowHandle throwMeat = new(Game1.player, victim.Position, barbeque);

                throwMeat.register();

            }

        }

        public void draw(SpriteBatch b)
        {

            if (!Utility.isOnScreen(victim.Position, 128))
            {

                return;

            }

            Microsoft.Xna.Framework.Rectangle bounding = victim.GetBoundingBox();

            Microsoft.Xna.Framework.Vector2 drawPosition = new(bounding.Center.X - (float)Game1.viewport.X, bounding.Top - 32 - (float)Game1.viewport.Y);

            b.Draw(
                Mod.instance.iconData.displayTexture,
                drawPosition,
                IconData.DisplayRectangle(display),
                Color.White * 0.8f,
                0f,
                new Vector2(8),
                4f,
                SpriteEffects.None,
                0.9f
            );

        }

    }

}
