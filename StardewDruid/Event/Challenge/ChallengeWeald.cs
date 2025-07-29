using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Journal;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using xTile.Dimensions;
using xTile.Layers;
using xTile.Tiles;
using System;
using StardewDruid.Dialogue;

namespace StardewDruid.Event.Challenge
{
    public class ChallengeWeald : EventHandle
    {

        public List<Vector2> trashVectors = new()
        {
            new Vector2(16,16)*64,
            new Vector2(17,13)*64,
            new Vector2(21,10)*64,
            new Vector2(34,11)*64,
            new Vector2(36,14)*64,
            new Vector2(37,16)*64,

        };

        public ChallengeWeald()
        {

            activeLimit = 75;

            mainEvent = true;

        }

        public override void EventActivate()
        {

            base.EventActivate();

            monsterHandle = new(origin, location)
            {
                spawnSchedule = new()
            };

            int monsterRange = 2;

            if (Mod.instance.questHandle.IsComplete(eventId))
            {

                monsterRange = 3;

            }

            for (int i = 1; i <= 55; i += 4)
            {

                monsterHandle.spawnSchedule.Add(i, new() { new(MonsterHandle.bosses.batwing, 4, Mod.instance.randomIndex.Next(monsterRange)) });

            }

            monsterHandle.spawnWithin = new(17, 10);

            monsterHandle.spawnRange = new(9, 9);

            monsterHandle.spawnWater = true;

            ProgressBar(Mod.instance.questHandle.quests[eventId].title, 0);

            EventBar trashbar = ProgressBar(StringData.Strings(StringData.stringkeys.trashCollected), 1);

            trashbar.colour = Color.LightGreen;

            DialogueCue(900);

            SetTrack("tribal");

            EventRender ritePortal = new(eventId, location.Name, origin, IconData.circles.summoning, Color.White);

            eventRenders.Add("ritePortal",ritePortal);

            Mod.instance.spellRegister.Add(new(Game1.player.Position, 288, IconData.impacts.supree, new()) { displayRadius = 4, sound = SpellHandle.Sounds.getNewSpecialItem, });

        }

        public override float DisplayProgress(int displayId)
        {

            return (float)eventRating / 36;

        }

        public override void EventInterval()
        {

            activeCounter++;

            if (activeCounter % 3 == 0 && Vector2.Distance(Game1.player.Position, origin) <= 320)
            {

                ThrowTrash();

            }
            
            if (activeCounter % 20 == 0)
            {

                DialogueCue(900);

            }

            if (activeCounter >= 12)
            {

                monsterHandle.SpawnCheck();

                monsterHandle.SpawnInterval();

            }

            if (bosses.Count > 0)
            {
                
                if (!ModUtility.MonsterVitals(bosses[0], location))
                {

                    bosses[0].currentLocation.characters.Remove(bosses[0]);

                    bosses.Clear();

                }
                
                DialogueCue(activeCounter);

            }

            switch (activeCounter)
            {

                case 1:

                    bosses[0] = new ShadowBat(new Vector2(27, 11), Mod.instance.CombatDifficulty(),"Batking");

                    bosses[0].SetMode(3);

                    bosses[0].netPosturing.Set(true);

                    bosses[0].netDirection.Set(2);

                    bosses[0].netAlternative.Set(3);

                    bosses[0].netScheme.Set(1);

                    bosses[0].tempermentActive = Boss.temperment.cautious;

                    location.characters.Add(bosses[0]);

                    bosses[0].update(Game1.currentGameTime, location);

                    Mod.instance.iconData.AnimateQuickWarp(location, bosses[0].Position);

                    voices[0] = bosses[0];

                    break;

                case 24:

                    bosses[0].netPosturing.Set(false);

                    BossBar(0, 0);

                    break;

                case 73:

                    if (bosses.Count > 0)
                    {
                        bosses[0].Halt();

                        SpellHandle rockSpell = new(Game1.player, bosses[0].Position, 384, 9999)
                        {
                            display = IconData.impacts.impact,

                            type = SpellHandle.Spells.missile,

                            displayFactor = 5,

                            scheme = IconData.schemes.rock,

                            sound = SpellHandle.Sounds.explosion,

                            missile = MissileHandle.missiles.rockfall,

                        };

                        Mod.instance.spellRegister.Add(rockSpell);
                    }

                    break;

                case 75:

                    eventComplete = true;

                    break;

            }

        }

        public void ThrowTrash()
        {

            Vector2 splash = trashVectors[Mod.instance.randomIndex.Next(trashVectors.Count)];

            ThrowHandle throwObject;

            if (eventRating == 8 && !Mod.instance.questHandle.IsComplete(eventId))
            {

                throwObject = new(Game1.player, origin, new Ring("517"));


            }
            else if (eventRating == 16 && !Mod.instance.questHandle.IsComplete(eventId))
            {

                throwObject = new(Game1.player, origin, new Ring("519"));

            }
            else
            {

                int objectIndex = SpawnData.RandomTrash();

                throwObject = new(splash, origin, objectIndex, 0) { pocket = true };

            }

            throwObject.register();

            location.playSound("pullItemFromWater");

            Mod.instance.iconData.ImpactIndicator(location, splash, IconData.impacts.fish, 3f, new());

            eventRating++;

        }

    }

}
