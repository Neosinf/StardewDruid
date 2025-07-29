using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewValley.Buffs;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using StardewValley.Monsters;
using StardewModdingAPI;
using StardewValley.Objects;
using xTile.Layers;

namespace StardewDruid.Cast.Effect
{
    public class Snare : EventHandle
    {

        public Dictionary<StardewDruid.Character.Character, SnareVictim> snareVictims = new();

        public Dictionary<StardewValley.Monsters.Monster, SnareVictim> snareMonsters = new();

        public Dictionary<StardewValley.Farmer, SnareVictim> snarePlayers = new();

        public Snare()
            : base()
        {

            

        }

        public override void EventActivate()
        {

            base.EventActivate();

            EventClicks(actionButtons.action);

        }

        public override bool EventPerformAction(actionButtons Action = actionButtons.action)
        {

            if (!EventActive())
            {

                return false;

            }

            int axeDirection = Game1.player.FacingDirection * 2;

            Vector2 hit = Game1.player.GetBoundingBox().Center.ToVector2() + (ModUtility.DirectionAsVector(axeDirection) * 96);

            for (int m = snareMonsters.Count - 1; m >= 0; m--)
            {

                KeyValuePair<StardewValley.Monsters.Monster, SnareVictim> snare = snareMonsters.ElementAt(m);

                snare.Value.DamageMonster(hit);

            }

            for(int c = snareVictims.Count - 1; c >= 0; c--)
            {

                KeyValuePair<StardewDruid.Character.Character, SnareVictim> snare = snareVictims.ElementAt(c);

                if(snare.Value.timer > 0)
                {

                    snare.Value.timer -= 10;

                    snare.Value.hot = 10;

                }

                if (snare.Value.timer <= 0)
                {

                    snare.Value.Explode();

                }

            }


            for (int p = snarePlayers.Count - 1; p >= 0; p--)
            {

                KeyValuePair<StardewValley.Farmer, SnareVictim> snare = snarePlayers.ElementAt(p);

                if (snare.Value.timer > 0)
                {

                    snare.Value.timer -= 10;

                    snare.Value.hot = 10;

                }

                if (snare.Value.timer <= 0)
                {

                    snare.Value.Explode();

                    snarePlayers.Remove(snare.Key);

                }

            }

            return true;

        }

        public override void EventDraw(SpriteBatch b)
        {


            for (int m = snareMonsters.Count - 1; m >= 0; m--)
            {

                KeyValuePair<StardewValley.Monsters.Monster, SnareVictim> snare = snareMonsters.ElementAt(m);

                snare.Value.draw(b);


            }

            for (int v = snareVictims.Count - 1; v >= 0; v--)
            {

                KeyValuePair<StardewDruid.Character.Character, SnareVictim> snare = snareVictims.ElementAt(v);

                snare.Value.draw(b);

            }

            for (int p = snarePlayers.Count - 1; p >= 0; p--)
            {

                KeyValuePair < StardewValley.Farmer, SnareVictim> snare = snarePlayers.ElementAt(p);

                snare.Value.draw(b);

            }

        }

        public override void EventRemove()
        {

            for (int m = snareMonsters.Count - 1; m >= 0; m--)
            {

                KeyValuePair<StardewValley.Monsters.Monster, SnareVictim> snare = snareMonsters.ElementAt(m);

                snare.Value.Shutdown();


            }

            for (int m = snareVictims.Count - 1; m >= 0; m--)
            {

                KeyValuePair<StardewDruid.Character.Character, SnareVictim> snare = snareVictims.ElementAt(m);

                snare.Value.Shutdown();

            }

            for (int p = snarePlayers.Count - 1; p >= 0; p--)
            {

                KeyValuePair<StardewValley.Farmer, SnareVictim> snare = snarePlayers.ElementAt(p);

                snare.Value.Shutdown();

            }

            base.EventRemove();

        }

        public override void EventDecimal()
        {

            for(int m = snareMonsters.Count - 1; m >= 0; m--)
            {

                KeyValuePair<StardewValley.Monsters.Monster, SnareVictim> snare =snareMonsters.ElementAt(m);

                if (!snare.Value.update())
                {

                    snareMonsters.Remove(snare.Key);

                }

            }

            for (int m = snareVictims.Count - 1; m >= 0; m--)
            {

                KeyValuePair<StardewDruid.Character.Character, SnareVictim> snare = snareVictims.ElementAt(m);

                if (!snare.Value.update())
                {

                    snareVictims.Remove(snare.Key);

                }

            }

            for (int p = snarePlayers.Count - 1; p >= 0; p--)
            {

                KeyValuePair<StardewValley.Farmer, SnareVictim> snare = snarePlayers.ElementAt(p);

                if (!snare.Value.update())
                {

                    snarePlayers.Remove(snare.Key);

                }

            }

            if (snareMonsters.Count == 0 && snareVictims.Count == 0 && snarePlayers.Count == 0)
            {

                eventComplete = true;

            }

        }

        public override float DisplayProgress(int displayId)
        {

            for (int m = snareVictims.Count - 1; m >= 0; m--)
            {

                KeyValuePair<StardewDruid.Character.Character, SnareVictim> snare = snareVictims.ElementAt(m);

                if(snare.Value.index == displayId)
                {

                    return (float)snare.Value.timer / (float)snare.Value.total;

                }

            }

            for (int p = snarePlayers.Count - 1; p >= 0; p--)
            {

                KeyValuePair<StardewValley.Farmer, SnareVictim> snare = snarePlayers.ElementAt(p);

                if (snare.Value.index == displayId)
                {

                    return (float)snare.Value.timer / (float)snare.Value.total;

                }

            }

            return -1f;

        }

        public virtual void AddMonster(StardewValley.Monsters.Monster Monster, int Timer = 4, SpellHandle.Effects Effect = SpellHandle.Effects.snare)
        {


            if (snareMonsters.ContainsKey(Monster))
            {
                return;

            }

            if (Monster is StardewDruid.Monster.Boss bossMonster)
            {

                if (bossMonster.IsCursable(Effect) != Effect)
                {

                    return;

                }

            }

            SnareVictim snare = new(Monster, Timer, Effect);

            snareMonsters.Add(Monster, snare);

        }

        public virtual void AddVictim(StardewDruid.Character.Character Victim, int Timer = 4, SpellHandle.Effects Effect = SpellHandle.Effects.snare)
        {

            if (snareVictims.ContainsKey(Victim))
            {

                return;

            }

            Victim.ResetActives();

            SnareVictim snare = new(Victim, Timer, Effect);

            snareVictims.Add(Victim, snare);

            EventBar bar = ProgressBar(StringData.Strings(StringData.stringkeys.chained) + Victim.displayName, snare.index);

            bar.colour = Mod.instance.iconData.SchemeColour(IconData.schemes.death);

        }

        public virtual void TargetPlayer(int Timer = 6, SpellHandle.Effects Effect = SpellHandle.Effects.snare)
        {

            if(snarePlayers.Count > 0)
            {

                return;

            }

            if (!Game1.displayFarmer)
            {

                return;

            }


            SnareVictim snare = new(Timer, Effect);

            snarePlayers.Add(Game1.player,snare);

            EventBar bar = ProgressBar(StringData.Strings(StringData.stringkeys.chained) + Game1.player.Name, snare.index);

            bar.colour = Mod.instance.iconData.SchemeColour(IconData.schemes.death);

        }


    }

    public class SnareVictim
    {

        public int timer;

        public int cooldown;

        public bool player;

        public StardewDruid.Character.Character victim;

        public StardewValley.Monsters.Monster monster;

        public GameLocation location;

        public Vector2 position;

        public Vector2 center;

        public SpellHandle.Effects effect = SpellHandle.Effects.snare;

        public int engage;

        public int disengage;

        public int hot;

        public List<int> roots;

        public List<Vector2> rootVectors;

        public List<float> rootAngles;

        public int damage;

        public Microsoft.Xna.Framework.Color colour;

        public int total;

        public int index;

        public SnareVictim(int Timer = 40, SpellHandle.Effects Effect = SpellHandle.Effects.snare)
        {

            timer = Timer;

            total = timer;

            effect = Effect;

            player = true;

            position = Game1.player.Position;

            center = position + new Vector2(32);

            location = Game1.player.currentLocation;

            index = (int)((position.X * 10000) + position.Y);

            Game1.player.completelyStopAnimatingOrDoingAction();

            setup();

            timer++;

            update();

        }

        public SnareVictim(StardewDruid.Character.Character Victim, int Timer = 40, SpellHandle.Effects Effect = SpellHandle.Effects.snare)
        {

            timer = Timer;

            total = timer;

            effect = Effect;

            victim = Victim;

            position = victim.Position;
            
            center = position + new Vector2(32);

            location = victim.currentLocation;

            index = (int)((position.X * 10000) + position.Y);

            setup();

            timer++;

            update();

        }

        public SnareVictim(StardewValley.Monsters.Monster Monster, int Timer = 40, SpellHandle.Effects Effect = SpellHandle.Effects.snare)
        {

            timer = Timer;

            total = timer;

            effect = Effect;

            monster = Monster;

            position = monster.Position;

            center = position + new Vector2(32);

            location = monster.currentLocation;

            index = (int)((position.X * 10000) + position.Y);

            setup();

            timer++;

            update();

        }

        public void setup()
        {


            switch (effect)
            {

                case SpellHandle.Effects.snare:

                    roots = new()
                    {
                        Mod.instance.randomIndex.Next(3),
                        Mod.instance.randomIndex.Next(3),
                        Mod.instance.randomIndex.Next(3),
                    };
                    colour = Mod.instance.iconData.SchemeColour(IconData.schemes.weald);
                    break;

                case SpellHandle.Effects.chain:

                    roots = new()
                    {
                        3,
                        3,
                        3,
                    };
                    colour = Mod.instance.iconData.SchemeColour(IconData.schemes.death);
                    break;

                case SpellHandle.Effects.binds:

                    roots = new()
                    {
                        4,
                        4,
                        4,
                    };
                    colour = Mod.instance.iconData.SchemeColour(IconData.schemes.fates);
                    break;
            }

            rootVectors = new()
            {
                center + new Vector2(-48,-32),
                center + new Vector2(0,64),
                center + new Vector2(48,-32),

            };
            /*rootVectors = new()
            {
                center + new Vector2(-24,-18),
                center + new Vector2(0,20),
                center + new Vector2(24,-18),

            };*/
            rootAngles = new()
            {
                (float)(Math.PI / 6),
                0-(float)(Math.PI / 2),
                (float)(Math.PI / 6 * 5),
            };

            damage = Mod.instance.CombatDamage() / 2;

        }

        public void draw(SpriteBatch b)
        {

            engage++;

            // ground circle ----------------------

            float fade = 1f;

            if(timer <= 10)
            {

                disengage++;

                fade = (1f - 0.01f * disengage);

            }
            else
            if(engage < 60)
            {

                fade = (0.4f + 0.01f * engage);

            }

            Vector2 useCenter = Game1.GlobalToLocal(center);

            Color useColour = colour;

            if(hot > 0)
            {

                hot--;

                useColour = new(useColour.R + (hot * 25), useColour.G, useColour.B);

            }

            b.Draw(
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.ritual],
                    useCenter,
                    new Rectangle(0, 96, 64, 48),
                    Color.White * fade,
                    0f,
                    new Vector2(32, 24),
                    4f,
                    0,
                    0.0002f
                );

            b.Draw(
                    Mod.instance.iconData.sheetTextures[IconData.tilesheets.ritual],
                    useCenter,
                    new Rectangle(64, 96, 64, 48),
                    useColour * fade,
                    0f,
                    new Vector2(32, 24),
                    4f,
                    0,
                    0.0003f
                );


            // roots ------------------

            for (int r = 0; r < 3; r++)
            {

                Microsoft.Xna.Framework.Rectangle source = new(0, 64 * roots[r], 64, 64);

                float rootFade = 1f;

                if(effect == SpellHandle.Effects.chain)
                {

                    rootFade = 0.75f;

                }

                if (timer <= 10)
                {
                    int grasping = (int)(disengage / 12);

                    source.X = 320 + grasping * 64;
                    
                }
                else
                if (engage < 60)
                {

                    int grasping = (int)(engage / 12);

                    source.X = grasping * 64;

                }
                else
                {

                    int struggle = (int)((engage % 60)/15);

                    source.X = 192 + struggle * 64;

                }

                b.Draw(
                        Mod.instance.iconData.graspTexture,
                        Game1.GlobalToLocal(rootVectors[r]),
                        source,
                        Color.White*rootFade,
                        rootAngles[r],
                        new Vector2(32),
                        2.8f,
                        0,
                        0.0004f + 0.0001f*r
                    );

            }

        }

        public bool update()
        {

            timer--;

            if(timer <= 0)
            {

                Shutdown();

                return false;

            }

            if (player)
            {

                if(!Game1.displayFarmer)
                {

                    timer = 0;

                    Shutdown();

                    Explode();

                    return false;

                }

                Game1.player.Position = position;

                return true;

            }

            if (monster != null)
            {

                if (!ModUtility.MonsterVitals(monster, location))
                {

                    monster = null;

                    return false;

                }

                if (monster is StardewDruid.Monster.Boss bossMonster)
                {

                    bossMonster.Halt();

                    bossMonster.Position = position;

                }
                else
                {

                    monster.stunTime.Set(2000);

                    monster.faceDirection(ModUtility.DirectionToTarget(monster.Position, Game1.player.Position)[0]);

                    monster.Position = position;

                }

            }

            if (victim != null)
            {

                victim.ResetActives();

                victim.idleTimer = 60;

                victim.idleFrame = 0;

                victim.cooldownTimer = 60;

                victim.netIdle.Set((int)StardewDruid.Character.Character.idles.kneel);

                victim.Position = position;

                victim.SettleOccupied();

            }

            return true;

        }

        public void Shutdown()
        {

            if (monster != null)
            {

                if (!ModUtility.MonsterVitals(monster, location))
                {

                    monster = null;

                    return;

                }

                if (monster is StardewDruid.Monster.Boss bossMonster)
                {

                    bossMonster.ResetActives();

                }
                else
                {

                    monster.stunTime.Set(0);

                }

            }

            if (victim != null)
            {

                victim.ResetActives();


            }

        }

        public void DamageMonster(Vector2 hitPosition)
        {

            if (monster != null)
            {

                if (!ModUtility.MonsterVitals(monster, location))
                {

                    monster = null;

                    return;

                }

            }
            else
            {

                return;

            }
            
            if(Vector2.Distance(hitPosition,monster.Position) <= 128)
            {

                SpellHandle critEffect = new(Game1.player, new() { monster, }, damage)
                {
                    display = IconData.impacts.flashbang,

                    instant = true,

                    counter = -4
                };

                Mod.instance.spellRegister.Add(critEffect);

            }
           
        }

        public void Explode()
        {

            switch (effect)
            {

                case SpellHandle.Effects.chain:

                    SpellHandle death = new(position-new Vector2(32), 192, IconData.impacts.deathbomb, new()) { displayRadius = 3, };

                    Mod.instance.spellRegister.Add(death);

                    break;

            }

        }

    }

}

