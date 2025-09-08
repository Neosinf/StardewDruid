using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Companions;
using StardewValley.Locations;
using StardewValley.Monsters;
using StardewValley.Projectiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

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

        public void AddTarget(GameLocation location, StardewValley.Monsters.Monster monster, SpellHandle.Effects effect)
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

                case SpellHandle.Effects.none:

                    return;

                case SpellHandle.Effects.morph:

                    if (monster.isGlider.Value || ModUtility.GroundCheck(location,monster.Tile) != "ground" || monster is DustSpirit)
                    {

                        effect = SpellHandle.Effects.daze;

                    }

                    DustSpirit morph = new(monster.Position);

                    morph.update(Game1.currentGameTime, location);

                    morph.MaxHealth = morph.MaxHealth * 3;

                    morph.resilience.Set(0);

                    morph.Health = morph.MaxHealth;

                    morph.Scale *= 3;

                    morph.Scale /= 2;

                    location.characters.Add(morph);

                    monster.Health = 1;

                    ModUtility.DamageMonsters(new() { monster, },999);

                    monster = morph;

                    break;

                case SpellHandle.Effects.knock:
                case SpellHandle.Effects.charm:

                    if (monster.isGlider.Value)
                    {
                        
                        effect = SpellHandle.Effects.daze;

                    }

                    break;

                case SpellHandle.Effects.holy:

                    switch (monster)
                    {

                        case ShadowBrute:
                        case ShadowGirl:
                        case ShadowGuy:
                        case ShadowShaman:
                        case Mummy:
                        case Skeleton:
                        case Ghost:
                        case DarkPhantom:
                        case DarkShooter:
                        case Spectre:

                            break;

                        default:

                            effect = SpellHandle.Effects.glare;

                            break;

                    }

                    Mod.instance.iconData.ImpactIndicator(location, monster.Position, IconData.impacts.glare, 3f, new());

                    break;


            }

            victims.Add(monster, new(location, monster, 5, effect));

        }

        public override void EventRemove()
        {

            for (int g = victims.Count - 1; g >= 0; g--)
            {

                KeyValuePair<StardewValley.Monsters.Monster, CurseTarget> cursed = victims.ElementAt(g);

                cursed.Value.ShutDown();

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

            if(victims.Count == 0)
            {

                eventComplete = true;

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

        public SpellHandle.Effects type;

        public IconData.displays display;

        public int damageLevel;

        public CurseTarget(GameLocation Location, StardewValley.Monsters.Monster Victim, int Timer, SpellHandle.Effects Type = SpellHandle.Effects.knock)
        {

            location = Location;

            victim = Victim;

            timer = Timer * 10;

            type = Type;

            switch (type)
            {

                case SpellHandle.Effects.morph:

                    display = IconData.displays.morph;

                    break;
                
                case SpellHandle.Effects.mug:

                    display = IconData.displays.herbalism;

                    if (victim.objectsToDrop.Count > 0)
                    {

                        string dropNormal = victim.objectsToDrop[Mod.instance.randomIndex.Next(victim.objectsToDrop.Count)];

                        StardewValley.Object itemNormal = new StardewValley.Object(dropNormal, 1);

                        if (!itemNormal.QualifiedItemId.Contains("-") && itemNormal.Name != Item.ErrorItemName)
                        {

                            Game1.createItemDebris(itemNormal, victim.Position + new Vector2(0, 32), 2, location, -1);

                            break;

                        }

                        victim.objectsToDrop.Clear();

                    }

                    SpawnData.MonsterDrops(victim, (SpawnData.Drops)Mod.instance.randomIndex.Next(1,5));

                    if(victim.objectsToDrop.Count > 0)
                    {

                        string drop = victim.objectsToDrop[Mod.instance.randomIndex.Next(victim.objectsToDrop.Count)];

                        StardewValley.Object dropItem = new StardewValley.Object(drop, 1);

                        Game1.createItemDebris(dropItem, victim.Position + new Vector2(0, 32), 2, location, -1);

                    }

                    break;

                case SpellHandle.Effects.omen:

                    display = IconData.displays.omens;

                    ApothecaryHandle.RandomApothecaryItem(victim.Position + new Vector2(32));

                    break;

                case SpellHandle.Effects.glare:

                    display = IconData.displays.glare;

                    stats.Add((float)victim.Health);

                    break;

                case SpellHandle.Effects.daze:

                    display = IconData.displays.blind;

                    if (victim is StardewDruid.Monster.Boss boss)
                    {
                        
                        boss.debuffJuice = 0.5f;

                        break;

                    }

                    stats.Add((float)victim.speed);

                    victim.speed = victim.speed / 2;

                    victim.randomSquareMovement(Game1.currentGameTime);

                    break;
                
                case SpellHandle.Effects.doom:

                    if((double)victim.Health <= (double)(victim.MaxHealth * 0.07))
                    {

                        PerformInstantDeath();

                    }

                    timer *= 2;

                    display = IconData.displays.skull;

                    break;
                
                case SpellHandle.Effects.immolate:

                    display = IconData.displays.blaze;

                    break;

                case SpellHandle.Effects.holy:

                    display = IconData.displays.holy;

                    break;

                default:
                case SpellHandle.Effects.knock:

                    display = IconData.displays.knock;

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

                case SpellHandle.Effects.charm:

                    display = IconData.displays.fullheart;

                    Victim.Halt();

                    stats.Add(Victim.DamageToFarmer);

                    victim.DamageToFarmer = 1;

                    break;

            }

            damageLevel = Mod.instance.CombatDamage();

        }

        public void ShutDown()
        {
            
            switch (type)
            {

                case SpellHandle.Effects.knock:

                    victim.rotation = stats[0];

                    break;

                /*case SpellHandle.effects.glare:

                    if (victim is StardewDruid.Monster.Boss glareVictim)
                    {

                        glareVictim.nextCritical = false;

                        break;

                    }

                    break;*/

                case SpellHandle.Effects.charm:

                    victim.DamageToFarmer = (int)stats[0];

                    break;

                case SpellHandle.Effects.daze:

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

                if(type == SpellHandle.Effects.immolate)
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

                    ModUtility.HitMonster(mummy, 1, false);

                    return false;

                }

            }

            switch (type)
            {

                /*case SpellHandle.effects.glare:

                if (victim is StardewDruid.Monster.Boss glareVictim)
                {

                    if (!glareVictim.nextCritical)
                    {

                        timer = 1;

                        return false;

                    }

                }
                else
                {
                    if (stats[0] >= ((float)victim.Health - 10f))
                    {

                        PerformCritical();

                        timer = 1;

                        return false;

                    }

                }

                break;*/

                case SpellHandle.Effects.daze:
                    
                    if (victim is not StardewDruid.Monster.Boss dazeVictim)
                    {

                        if (timer % 10 == 0)
                        {

                            victim.randomSquareMovement(Game1.currentGameTime);

                        }

                    }

                    break;

                case SpellHandle.Effects.knock:

                    victim.Halt();

                    if(victim.stunTime.Value <= 0)
                    {

                        victim.stunTime.Set(timer * 100);

                    }

                    break;

                case SpellHandle.Effects.doom:

                    if(timer == 1)
                    {

                        PerformInstantDeath();

                    }

                    break;

                case SpellHandle.Effects.immolate:

                    if (timer % 10 == 5)
                    {

                        ModUtility.HitMonster(victim, damageLevel / 10, false, 0, 0);

                    }

                    break;

                case SpellHandle.Effects.holy:

                    if (timer % 10 == 5)
                    {

                        ModUtility.HitMonster(victim, damageLevel / 5, false, 0, 0);

                    }

                    break;

            }

            return true;

        }

        public void PerformInstantDeath()
        {

            SpellHandle death = new(Game1.player, new() { victim }, 999)
            {
                display = IconData.impacts.deathbomb,

                scheme = IconData.schemes.fates
            };


            if (Mod.instance.apothecaryHandle.buff.applied.ContainsKey(BuffHandle.buffTypes.spellcatch))
            {

                if (Mod.instance.apothecaryHandle.buff.applied.ContainsKey(BuffHandle.buffTypes.capture))
                {

                    death.added.Add(SpellHandle.Effects.capture);

                }

            }

            Mod.instance.spellRegister.Add(death);

        }

        public void PerformCritical()
        {

            SpellHandle critical = new(Game1.player, new() { victim }, Mod.instance.CombatDamage() / 2)
            {
                display = IconData.impacts.flashbang,

                scheme = IconData.schemes.golden
            };

            if (Mod.instance.apothecaryHandle.buff.applied.ContainsKey(BuffHandle.buffTypes.spellcatch))
            {

                if (Mod.instance.apothecaryHandle.buff.applied.ContainsKey(BuffHandle.buffTypes.capture))
                {

                    critical.added.Add(SpellHandle.Effects.capture);

                }

            }

            Mod.instance.spellRegister.Add(critical);

        }

        public void PerformBarbeque()
        {

            if (new Random().Next(5) == 0)
            {

                Mod.instance.iconData.ImpactIndicator(location, victim.Position, IconData.impacts.deathbomb, 4f, new() { scheme = IconData.schemes.stars, });

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

            //b.Draw(Game1.staminaRect, new Vector2(bounding.Left - (float)Game1.viewport.X, bounding.Top - (float)Game1.viewport.Y), bounding, new(254, 240, 192), 0f, Vector2.Zero, 1f, 0, 990f);
            
            Microsoft.Xna.Framework.Vector2 drawPosition = new(bounding.Center.X - (float)Game1.viewport.X, bounding.Center.Y - (float)Game1.viewport.Y - 120);

            switch (type)
            {

                case SpellHandle.Effects.doom:

                    float displayFade = 0.8f;

                    Microsoft.Xna.Framework.Color displayColor = Color.White;

                    if(timer <= 12)
                    {
                        int colorOffset = (12 - timer) * 8;

                        displayColor = new(256, 256 - colorOffset, 256 - colorOffset);

                    }

                    if(timer <= 8)
                    {

                        if(timer % 2 == 0)
                        {

                            displayFade = 0.3f;

                        }

                    }

                    b.Draw(
                        Mod.instance.iconData.displayTexture,
                        drawPosition,
                        IconData.DisplayRectangle(display),
                        displayColor * displayFade,
                        0f,
                        new Vector2(8),
                        3f,
                        SpriteEffects.None,
                        0.9f
                    );

                    break;

                default:

                    b.Draw(
                        Mod.instance.iconData.displayTexture,
                        drawPosition,
                        IconData.DisplayRectangle(display),
                        Color.White * 0.8f,
                        0f,
                        new Vector2(8),
                        3f,
                        SpriteEffects.None,
                        0.9f
                    );

                    break;

            }

        }
    
    }

}
