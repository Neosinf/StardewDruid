using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Event
{
    public class PirateSkeleton: Skeleton
    {

        public List<string> ouchList;

        public List<string> dialogueList;

        public int tickCount;

        public TemporaryAnimatedSprite hatAnimation;

        public Texture2D hatsTexture;

        public Rectangle hatSourceRect;

        public Vector2 hatOffset;

        public int hatIndex;

        public Dictionary<int, Rectangle> hatSourceRects;

        public Dictionary<int, Vector2> hatOffsets;

        public TemporaryAnimatedSprite swordAnimation;

        public Rectangle swordSourceRect;

        public bool swordFlip;

        public Vector2 swordOffset;

        public int swordIndex;

        public float swordRotate;

        public Dictionary<int, Vector2> swordOffsets;

        public Dictionary<int, bool> swordFlips;

        public Dictionary<int, float> swordRotates;

        public Dictionary<int, float> swordDepths;

        public float swordDepth;

        public PirateSkeleton(Vector2 position, int difficultyMod)
            : base(position, false)
        {

            focusedOnFarmers = true;

            IsWalkingTowardPlayer = true;

            moveTowardPlayerThreshold.Value = 16;

            Slipperiness = 3;

            HideShadow = false;

            jitteriness.Value = 0.0;

            DamageToFarmer += difficultyMod;

            Health += (int)(difficultyMod * difficultyMod *2);

            ExperienceGained += difficultyMod;

            objectsToDrop.Clear();

            if (Game1.random.Next(2) == 0)
            {
                objectsToDrop.Add(378);
            }
            else if (Game1.random.Next(3) == 0 && difficultyMod >= 5)
            {
                objectsToDrop.Add(380);
            }
            else if (Game1.random.Next(4) == 0 && difficultyMod >= 8)
            {
                objectsToDrop.Add(384);
            }

            ouchList = new()
            {
                "oooft",
                "yeoww",
                "crikey!",
            };

            dialogueList = new()
            {
                "DEEP",
                "shivers",
                "timbers",
                "yarr",
            };

            hatsTexture = Game1.content.Load<Texture2D>("Characters\\Farmer\\hats");

            List<int> hatList = new()
            {
                242,
                292,
                3,
            };

            hatIndex = hatList[Game1.random.Next(hatList.Count)];

            hatSourceRects = new()
            {
                [2] = Game1.getSourceRectForStandardTileSheet(hatsTexture, hatIndex, 20, 20),       // down
                [1] = Game1.getSourceRectForStandardTileSheet(hatsTexture, hatIndex + 12, 20, 20),  // right
                [3] = Game1.getSourceRectForStandardTileSheet(hatsTexture, hatIndex + 24, 20, 20),  // left
                [0] = Game1.getSourceRectForStandardTileSheet(hatsTexture, hatIndex + 36, 20, 20),  // up
            };

            hatOffsets = new()
            {
                [2] = new Vector2(5, -81),
                [1] = new Vector2(6, -81),
                [3] = new Vector2(2, -81),
                [0] = new Vector2(6, -81),
            };

            List<int> swordList = new()
            {
                0,
                12,
                49,
            };

            swordIndex = swordList[Game1.random.Next(swordList.Count)];

            swordSourceRect = new(swordIndex % 8 * 16, (swordIndex - swordIndex % 8) / 8 * 16, 16, 16);

            swordOffsets = new()
            {
                [2] = new Vector2(38, -8), // down
                [1] = new Vector2(10, -8), // right
                [3] = new Vector2(26, -8), // left
                [0] = new Vector2(2, -8), // up
            };

            swordFlips = new()
            {
                [2] = false,
                [1] = true,
                [3] = false,
                [0] = true,
            };

            swordRotates = new()
            {
                [2] = 2.4f,//2.4f,//4.0f,
                [1] = 4.8f,//1.6f,//4.8f,
                [3] = 1.6f,//0f,
                [0] = 4.0f,//0.8f,
            };

            swordDepths = new()
            {
                [2] = 999f,
                [1] = 999f,
                [3] = 0.0999f,
                [0] = 999f,
            };

        }

        public override int takeDamage(int damage, int xTrajectory, int yTrajectory, bool isBomb, double addedPrecision, Farmer who)
        {

            int num = Math.Max(1, damage - resilience.Value);

            /*if (Game1.random.NextDouble() < (double)missChance.Value - (double)missChance.Value * addedPrecision)
            {
                num = -1;

            }
            else
            {*/

                Health -= num;

                setTrajectory(xTrajectory, yTrajectory);

                if (Health <= 0)
                {
                    deathAnimation();
                }

                //base.currentLocation.playSound("hitEnemy");
                int ouchIndex = Game1.random.Next(10);
                if (ouchList.Count - 1 >= ouchIndex)
                {
                    showTextAboveHead(ouchList[ouchIndex], duration: 2000);
                }

            //}

            return num;
        }
        
        public override List<Item> getExtraDropItems()
        {
            return new List<Item>();
        }

        public void triggerPanic()
        {

            List<string> panicList = new()
            {
                "cover?",
                "deep deep",
                "RUN!",
            };

            showTextAboveHead(panicList[Game1.random.Next(3)], duration: 3000);

        }

        public override void behaviorAtGameTick(GameTime time)
        {

            tickCount++;

            if (tickCount >= 200)
            {
                int dialogueIndex = Game1.random.Next(15);
                if (dialogueList.Count - 1 >= dialogueIndex)
                {
                    showTextAboveHead(dialogueList[dialogueIndex], duration: 2000);
                }
                tickCount = 0;
            }

            int facingDirection = getFacingDirection();

            hatSourceRect = hatSourceRects[facingDirection];

            hatOffset = hatOffsets[facingDirection];

            switch (Sprite.currentFrame % 4)
            {

                case 1:

                    hatOffset += new Vector2(0, 3); // down

                    break;

                case 3:

                    hatOffset += new Vector2(0, 3); // down

                    break;

                default: break;

            }

            hatAnimation = new("Characters\\Farmer\\hats", hatSourceRect, 17f, 1, 1, position + hatOffset, flicker: false, flipped: false, 999f, 0f, Color.White, 3f, 0, 0, 0);

            currentLocation.temporarySprites.Add(hatAnimation);

            swordOffset = swordOffsets[facingDirection];

            swordFlip = swordFlips[facingDirection];

            swordRotate = swordRotates[facingDirection];

            swordDepth = swordDepths[facingDirection];

            swordAnimation = new("TileSheets\\weapons", swordSourceRect, 17f, 1, 1, position + swordOffset, flicker: false, flipped: swordFlip, swordDepth, 0f, Color.White, 2f, 0f, swordRotate, 0f);

            currentLocation.temporarySprites.Add(swordAnimation);

            base.behaviorAtGameTick(time);

        }

    }

}
