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
            new Vector2(16,19)*64,
            new Vector2(17,13)*64,
            new Vector2(21,10)*64,
            new Vector2(34,11)*64,
            new Vector2(37,15)*64,
            new Vector2(37,20)*64,

        };

        public ChallengeWeald()
        {

            activeLimit = 75;

            mainEvent = true;

        }

        public override void TriggerInterval()
        {

            base.TriggerInterval();

            TrashAnimation();

        }

        public void TrashAnimation()
        {

            TemporaryAnimatedSprite newAnimation;

            if (animations.Count == 0)
            {

                foreach (Vector2 trashVector in trashVectors)
                {

                    Microsoft.Xna.Framework.Rectangle targetRectangle = Game1.getSourceRectForStandardTileSheet(Game1.objectSpriteSheet, 168, 16, 16);

                    newAnimation = new(
                        "Maps\\springobjects",
                        targetRectangle,
                        trashVector - new Vector2(16, 0),
                        flipped: false,
                        0f,
                        Color.White * 0.5f
                    )
                    {
                        interval = 99999f,
                        totalNumberOfLoops = 99999,
                        scale = 4f,
                    };

                    location.temporarySprites.Add(newAnimation);

                    animations.Add(newAnimation);

                }

            }
            else
            {

                foreach (TemporaryAnimatedSprite trashAnimation in animations)
                {

                    trashAnimation.reset();

                }

            }

            foreach (Vector2 trashVector in trashVectors)
            {

                newAnimation = new(
                    "LooseSprites\\Cursors",
                    new Microsoft.Xna.Framework.Rectangle(372, 1956, 10, 10),
                    trashVector,
                    flipped: false,
                    0.001f,
                    Color.Teal
                )
                {
                    alpha = 0.75f,
                    motion = new Vector2(0f, -0.5f),
                    //acceleration = new Vector2(0.002f, 0f),
                    interval = 9999f,
                    layerDepth = 0.001f,
                    scale = 2.75f,
                    scaleChange = 0.02f,
                    rotationChange = Game1.random.Next(-5, 6) * MathF.PI / 256f,
                };

                location.temporarySprites.Add(newAnimation);

            }

        }

        public override void EventActivate()
        {

            base.EventActivate();

            monsterHandle = new(origin, location);

            monsterHandle.spawnSchedule = new();

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

            eventProximity = 768;

            EventBar(Mod.instance.questHandle.quests[eventId].title, 0);

            EventDisplay trashbar = EventBar(DialogueData.Strings(DialogueData.stringkeys.trashCollected), 0);

            trashbar.colour = Color.LightGreen;

            DialogueCue(900);

            SetTrack("tribal");

            EventRender ritePortal = new(eventId, location.Name, origin, IconData.skies.mountain,5f);

            eventRenders.Add(ritePortal);

            ModUtility.AnimateHands(Game1.player, Game1.player.FacingDirection, 600);

            location.playSound("discoverMineral");

        }

        public override float SpecialProgress(int displayId)
        {

            return (float)eventRating / 36;

        }

        public override void EventInterval()
        {

            activeCounter++;

            TrashAnimation();

            if (activeCounter % 3 == 0 && Vector2.Distance(Game1.player.Position, origin) <= 320)
            {

                ThrowTrash();

            }
            
            if (activeCounter % 2 == 0)
            {
                
                Mod.instance.iconData.ImpactIndicator(location, origin, IconData.impacts.nature, 6f, new() { alpha = 0.3f, });
            
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

                    bosses[0] = new Batwing(new Vector2(27, 11), Mod.instance.CombatDifficulty());

                    bosses[0].SetMode(3);

                    bosses[0].netPosturing.Set(true);

                    bosses[0].netDirection.Set(2);

                    bosses[0].netAlternative.Set(3);

                    bosses[0].netScheme.Set(1);

                    bosses[0].tempermentActive = Boss.temperment.cautious;

                    location.characters.Add(bosses[0]);

                    bosses[0].update(Game1.currentGameTime, location);

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

                        SpellHandle rockSpell = new(Game1.player, bosses[0].Position, 384, 9999);

                        rockSpell.display = IconData.impacts.impact;

                        rockSpell.type = SpellHandle.spells.orbital;

                        rockSpell.projectile = 5;

                        rockSpell.scheme = IconData.schemes.rock;

                        rockSpell.sound = SpellHandle.sounds.explosion;

                        rockSpell.missile = IconData.missiles.rockfall;

                        rockSpell.terrain = 3;

                        rockSpell.added = new() { SpellHandle.effects.stone, };

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

                int objectIndex = SpawnData.RandomTrash(location);

                throwObject = new(splash, origin, objectIndex, 0) { pocket = true };

            }

            throwObject.register();

            location.playSound("pullItemFromWater");

            Mod.instance.iconData.ImpactIndicator(location, splash, IconData.impacts.fish, 3f, new());

            eventRating++;

        }

    }

}
