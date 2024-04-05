using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Dialogue;
using StardewDruid.Event;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Monsters;
using StardewValley.Network;
using StardewValley.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StardewDruid.Character
{
    public class Shadowtin : StardewDruid.Character.Character
    {

        public Texture2D swipeTexture;
        public Dictionary<int, List<Rectangle>> swipeFrames;

        public Shadowtin()
        {
        }

        public Shadowtin(Vector2 position, string map)
          : base(position, map, nameof(Shadowtin))
        {

        }

        public override void LoadOut()
        {

            LoadBase();

            characterTexture = CharacterData.CharacterTexture(Name);

            haltFrames = FrameSeries(32, 32, 0, 0, 1);

            walkFrames = FrameSeries(32, 32, 0, 128, 6, haltFrames);

            walkLeft = 1;

            walkRight = 4;

            idleFrames = new()
            {
                [0] = new()
                {
                    new Rectangle(128, 320, 64, 64),
                    new Rectangle(192, 320, 64, 64),
                    new Rectangle(0, 320, 64, 64),
                    new Rectangle(64, 320, 64, 64),
                },
                [1] = new()
                {
                    new Rectangle(192, 320, 64, 64),
                    new Rectangle(0, 320, 64, 64),
                    new Rectangle(64, 320, 64, 64),
                    new Rectangle(128, 320, 64, 64),
                },
                [2] = new()
                {
                    new Rectangle(0, 320, 64, 64),
                    new Rectangle(64, 320, 64, 64),
                    new Rectangle(128, 320, 64, 64),
                    new Rectangle(192, 320, 64, 64),
                },
                [3] = new()
                {
                    new Rectangle(64, 320, 64, 64),
                    new Rectangle(128, 320, 64, 64),
                    new Rectangle(192, 320, 64, 64),
                    new Rectangle(0, 320, 64, 64),
                },
            };

            specialFrames = new()
            {
                [0] = new()
                {

                    new(64, 64, 32, 32),
                    new(96, 64, 32, 32),

                },
                [1] = new()
                {

                    new(64, 32, 32, 32),
                    new(96, 32, 32, 32),

                },
                [2] = new()
                {

                    new(64, 0, 32, 32),
                    new(96, 0, 32, 32),

                },
                [3] = new()
                {

                    new(64, 96, 32, 32),
                    new(96, 96, 32, 32),

                },

            };

            specialInterval = 30;
            specialCeiling = 1;
            specialFloor = 1;

            dashCeiling = 10;
            dashFloor = 0;

            dashFrames = new()
            {
                [0] = new()
                {
                    new(0, 192, 32, 32),
                    new(32, 64, 32, 32),
                    new(32, 64, 32, 32),
                    new(32, 64, 32, 32),
                    new(32, 64, 32, 32),
                    new(32, 64, 32, 32),
                    new(32, 64, 32, 32),
                    new(32, 64, 32, 32),
                    new(96,192,32,32),
                    new(128,192,32,32),
                    new(160,192,32,32),
                },
                [1] = new()
                {
                    new(0, 160, 32, 32),
                    new(32, 32, 32, 32),
                    new(32, 32, 32, 32),
                    new(32, 32, 32, 32),
                    new(32, 32, 32, 32),
                    new(32, 32, 32, 32),
                    new(32, 32, 32, 32),
                    new(32, 32, 32, 32),
                    new(96,160,32,32),
                    new(128,160,32,32),
                    new(160,160,32,32),
                },
                [2] = new()
                {
                    new(0, 128, 32, 32),
                    new(32, 0, 32, 32),
                    new(32, 0, 32, 32),
                    new(32, 0, 32, 32),
                    new(32, 0, 32, 32),
                    new(32, 0, 32, 32),
                    new(32, 0, 32, 32),
                    new(32, 0, 32, 32),
                    new(96,128,32,32),
                    new(128,128,32,32),
                    new(160,128,32,32),
                },
                [3] = new()
                {
                    new(0, 224, 32, 32),
                    new(32, 96, 32, 32),
                    new(32, 96, 32, 32),
                    new(32, 96, 32, 32),
                    new(32, 96, 32, 32),
                    new(32, 96, 32, 32),
                    new(32, 96, 32, 32),
                    new(32, 96, 32, 32),
                    new(96,224,32,32),
                    new(128,224,32,32),
                    new(160,224,32,32),
                },
            };

            sweepFrames = new()
            {
                [0] = new()
                {
                    new Rectangle(128, 320, 64, 64),
                    new Rectangle(192, 320, 64, 64),
                    new Rectangle(0, 320, 64, 64),
                    new Rectangle(64, 320, 64, 64),
                },
                [1] = new()
                {
                    new Rectangle(0, 384, 64, 64),
                    new Rectangle(64, 384, 64, 64),
                    new Rectangle(128, 384, 64, 64),
                    new Rectangle(192, 384, 64, 64),
                },
                [2] = new()
                {
                    new Rectangle(0, 256, 64, 64),
                    new Rectangle(64, 256, 64, 64),
                    new Rectangle(128, 256, 64, 64),
                    new Rectangle(192, 256, 64, 64),
                },
                [3] = new()
                {
                    new Rectangle(0, 320, 64, 64),
                    new Rectangle(64, 320, 64, 64),
                    new Rectangle(128, 320, 64, 64),
                    new Rectangle(192, 320, 64, 64),
                },
            };

            swipeTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Swipe.png"));

            swipeFrames = new()
            {
                [0] = new()
                {
                    new Rectangle(128, 64, 64, 64),
                    new Rectangle(192, 64, 64, 64),
                    new Rectangle(0, 64, 64, 64),
                    new Rectangle(64, 64, 64, 64),
                },
                [1] = new()
                {
                    new Rectangle(0, 128, 64, 64),
                    new Rectangle(64, 128, 64, 64),
                    new Rectangle(128, 128, 64, 64),
                    new Rectangle(192, 128, 64, 64),
                },
                [2] = new()
                {
                    new Rectangle(0, 0, 64, 64),
                    new Rectangle(64, 0, 64, 64),
                    new Rectangle(128, 0, 64, 64),
                    new Rectangle(192, 0, 64, 64),
                },
                [3] = new()
                {
                    new Rectangle(0, 64, 64, 64),
                    new Rectangle(64, 64, 64, 64),
                    new Rectangle(128, 64, 64, 64),
                    new Rectangle(192, 64, 64, 64),
                },
            };

            workFrames = new()
            {

                [0] = new(){
                    new Rectangle(128, 0, 32, 32),
                    new Rectangle(160, 0, 32, 32),
                },

            };

            loadedOut = true;

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {

            if (IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            if (characterTexture == null)
            {

                return;

            }

            Vector2 localPosition = getLocalPosition(Game1.viewport);

            float drawLayer = (float)StandingPixel.Y / 10000f;

            DrawEmote(b);

            if (netStandbyActive.Value)
            {

                DrawStandby(b, localPosition, drawLayer);

                DrawShadow(b, localPosition, drawLayer);

                return;

            }
            else
            if (netHaltActive.Value)
            {

                b.Draw(
                    characterTexture,
                    localPosition - new Vector2(28, 56f),
                    haltFrames[netDirection.Value][0],
                    Color.White,
                    0f,
                    Vector2.Zero,
                    3.75f,
                    flip || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

                return;

            }
            else if( netSweepActive.Value)
            {

                b.Draw(
                     characterTexture,
                     localPosition - new Vector2(88, 112f),
                     sweepFrames[netDirection.Value][sweepFrame],
                     Color.White,
                     0f,
                     Vector2.Zero,
                     3.75f,
                     flip || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                     drawLayer
                 );

                b.Draw(
                     swipeTexture,
                     localPosition - new Vector2(88, 112f),
                     swipeFrames[netDirection.Value][sweepFrame],
                     Color.White *0.5f,
                     0f,
                     Vector2.Zero,
                     3.75f,
                     flip || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                     drawLayer
                 );

            }
            else if (netWorkActive.Value)
            {
                
                b.Draw(
                    characterTexture,
                    localPosition - new Vector2(56, 56f), // -32 offset to be in middle of forage tile
                    workFrames[0][specialFrame],
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    3.75f,
                    SpriteEffects.None,
                    drawLayer
                );

                DrawShadow(b, localPosition, drawLayer, -32f);

                return;

            }
            else if (netSpecialActive.Value)
            {

                b.Draw(
                    characterTexture,
                    localPosition - new Vector2(28, 56f),
                    specialFrames[netDirection.Value][specialFrame],
                    Color.White,
                    0.0f,
                    Vector2.Zero,
                    3.75f,
                    flip || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? (SpriteEffects)1 : 0,
                    drawLayer
                );

            }
            else if (netDashActive.Value)
            {

                b.Draw(
                    characterTexture,
                    localPosition - new Vector2(28, 56f + dashHeight),
                    dashFrames[netDirection.Value][dashFrame],
                    Color.White,
                    0f,
                    Vector2.Zero,
                    3.75f,
                    flip || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

            }
            else
            {

                if (TightPosition() && idleTimer > 0 && currentLocation.IsOutdoors)
                {

                    DrawStandby(b, localPosition, drawLayer);

                    DrawShadow(b, localPosition, drawLayer);

                    return;

                }

                b.Draw(
                    characterTexture,
                    localPosition - new Vector2(28, 56f),
                    walkFrames[netDirection.Value][moveFrame],
                    Color.White,
                    0f,
                    Vector2.Zero,
                    3.75f,
                    flip || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );
                
            }

            DrawShadow(b, localPosition, drawLayer);

        }

        public virtual void DrawStandby(SpriteBatch b,  Vector2 localPosition, float drawLayer)
        {

            int frame = IdleFrame();

            b.Draw(
                 characterTexture,
                 localPosition - new Vector2(88, 112f),
                 idleFrames[netDirection.Value][frame],
                 Color.White,
                 0f,
                 Vector2.Zero,
                 3.75f,
                 flip || (netDirection.Value % 2 == 0 && netAlternative.Value == 3) ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                 drawLayer
             );

        }


        public override Rectangle GetBoundingBox()
        {

            return new Rectangle ((int)Position.X+ 8, (int)Position.Y + 8, 48, 48);

        }

        public override Rectangle GetHitBox()
        {
            return new Rectangle((int)Position.X-32, (int)Position.Y -32, 128, 128);
        }

        public override bool EngageDialogue()
        {
            
            if (!Mod.instance.dialogue.ContainsKey(nameof(Shadowtin)))
            {

                Dictionary<string, StardewDruid.Dialogue.Dialogue> dialogue = Mod.instance.dialogue;

                StardewDruid.Dialogue.Shadowtin shadowtin = new StardewDruid.Dialogue.Shadowtin();

                shadowtin.npc = this;

                dialogue[nameof(Shadowtin)] = shadowtin;

            }

            if (netSceneActive.Value && Mod.instance.dialogue[nameof(Shadowtin)].specialDialogue.Count == 0)
            {

                return false;

            }

            Mod.instance.dialogue[nameof(Shadowtin)].DialogueApproach();

            return true;

        }

        public override bool TargetWork()
        {

            if (!InventoryAvailable())
            {

                return false;

            }

            Vector2 currentVector = new((int)(Position.X / 64), (int)(Position.Y / 64));

            List<Vector2> objectVectors = new List<Vector2>();

            for (int i = 0; i < 6; i++)
            {

                if (currentLocation.objects.Count() == 0)
                {
                    break;

                }

                objectVectors = ModUtility.GetTilesWithinRadius(currentLocation, currentVector, i); ;

                foreach (Vector2 objectVector in objectVectors)
                {

                    if (currentLocation.objects.ContainsKey(objectVector))
                    {

                        StardewValley.Object targetObject = currentLocation.objects[objectVector];

                        if (targetObject.Name.Contains("Artifact Spot") || targetObject.isForage())
                        {

                            workVector = objectVector;

                            Position = (workVector * 64);

                            SettleOccupied();

                            ModUtility.AnimateQuickWarp(currentLocation, Position);

                            netWorkActive.Set(true);

                            netSpecialActive.Set(true);

                            specialTimer = 60;

                            return true;

                        }

                    }

                }

            }

            return false;

        }

        public override void PerformWork()
        {

            if(specialTimer == 30)
            {

                if (currentLocation.objects.ContainsKey(workVector))
                {

                    StardewValley.Object targetObject = currentLocation.objects[workVector];

                    if (targetObject.Name.Contains("Artifact Spot"))
                    {

                        currentLocation.digUpArtifactSpot((int)workVector.X, (int)workVector.Y, Mod.instance.trackRegister["Shadowtin"].followPlayer);

                        currentLocation.objects.Remove(workVector);
                        
                    }
                    else if (targetObject.isForage())
                    {

                        List<Chest> chests = CaveChests();

                        if (chests.Count != 0)
                        {

                            Chest chest = chests.Last();

                            StardewValley.Item objectInstance = new StardewValley.Object(targetObject.ParentSheetIndex.ToString(), 1, false, -1, 4);

                            chest.addItem(objectInstance);

                            currentLocation.objects.Remove(workVector);

                        }
                        else
                        {

                            new Throw(Mod.instance.trackRegister["Shadowtin"].followPlayer, Position, targetObject.ParentSheetIndex, 4).ThrowObject();

                            currentLocation.objects.Remove(workVector);

                        }

                    }
                    
                }

            }

        }

        public virtual bool InventoryAvailable()
        {

            if(CaveChests().Count > 0) { return true; }

            if(modeActive == mode.track)
            {

                if (Vector2.Distance(Mod.instance.trackRegister[Name].followPlayer.Position,Position) <= 720f)
                {
                    return true;

                }

            }

            return false;

        }

    }

}
