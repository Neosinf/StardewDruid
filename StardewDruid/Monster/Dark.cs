using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Render;
using StardewValley;
using System;
using System.Collections.Generic;
using static StardewDruid.Character.Character;

namespace StardewDruid.Monster
{
    public class Dark : Boss
    {

        public WeaponRender weaponRender;

        public bool firearmSet;

        public bool meleeSet;

        public float fadeOut = 1f;

        public Dictionary<StardewDruid.Character.Character.hats, Dictionary<int, Vector2>> hatVectors = new();

        public int hatSelect = -1;

        public Dark()
        {


        }

        public Dark(Vector2 vector, int CombatModifier, string name = "DarkRogue")
          : base(vector, CombatModifier, name)
        {
            
            SpawnData.MonsterDrops(this, SpawnData.Drops.shadow);

        }

        public override void LoadOut()
        {

            baseMode = 3;

            baseJuice = 3;
            
            basePulp = 25;

            fadeOut = 1f;

            cooldownInterval = 180;

            DarkWalk();

            DarkFlight();

            DarkCast();

            DarkSmash();

            DarkSword();

            hatVectors = CharacterRender.HumanoidHats();

            weaponRender = new();

            weaponRender.LoadWeapon(WeaponRender.weapons.estoc);

            loadedOut = true;

        }

        public virtual void DarkWalk()
        {
            
            characterTexture = MonsterHandle.MonsterTexture(realName.Value);

            walkInterval = 12;

            gait = 2;

            idleFrames = FrameSeries(32, 32, 0, 0, 1);

            walkFrames = FrameSeries(32, 32, 0, 128, 6, FrameSeries(32, 32, 0, 0, 1));

            alertFrames = new(idleFrames);

            walkSwitch = true;

            woundedFrames = new()
            {
                [0] = new()
                {

                    new(128, 64, 32, 32),

                },
                [1] = new()
                {

                    new(128, 32, 32, 32),

                },
                [2] = new()
                {

                    new(128, 0, 32, 32),

                },
                [3] = new()
                {

                    new(128, 32, 32, 32),

                },

            };

            specialFrames = new Dictionary<int, List<Rectangle>>()
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
        }

        public virtual void DarkFlight()
        {

            flightSet = true;

            flightInterval = 9;

            flightSpeed = 9;

            flightPeak = 128;

            flightDefault = flightTypes.close;

            flightFrames = new Dictionary<int, List<Rectangle>>()
            {
                [0] = new()
                {
                    new(0, 192, 32, 32),
                },
                [1] = new()
                {
                    new(0, 160, 32, 32),
                },
                [2] = new()
                {
                    new(0, 128, 32, 32),
                },
                [3] = new()
                {
                    new(0, 224, 32, 32),
                },
                [4] = new()
                {
                    new(32, 64, 32, 32),
                },
                [5] = new()
                {
                    new(32, 32, 32, 32),
                },
                [6] = new()
                {
                    new(32, 0, 32, 32),
                },
                [7] = new()
                {
                    new(32, 96, 32, 32),
                },
                [8] = new()
                {
                    new(96,192,32,32),
                    new(128,192,32,32),
                    new(160,192,32,32),
                },
                [9] = new()
                {
                    new(96,160,32,32),
                    new(128,160,32,32),
                    new(160,160,32,32),
                },
                [10] = new()
                {
                    new(96,128,32,32),
                    new(128,128,32,32),
                    new(160,128,32,32),
                },
                [11] = new()
                {
                    new(96,224,32,32),
                    new(128,224,32,32),
                    new(160,224,32,32),
                },
            };

        }

        public virtual void DarkSmash()
        {

            smashSet = true;

            smashFrames = new()
            {
                [0] = new()
                {
                    new(0, 320, 32, 32),new(32, 320, 32, 32),
                },
                [1] = new()
                {
                    new(0, 288, 32, 32),new(32, 288, 32, 32),
                },
                [2] = new()
                {
                    new(0, 256, 32, 32),new(32, 256, 32, 32),
                },
                [3] = new()
                {
                    new(0, 288, 32, 32),new(32, 288, 32, 32),
                },
                [4] = new()
                {
                    new(64, 320, 32, 32),
                },
                [5] = new()
                {
                    new(64, 288, 32, 32),
                },
                [6] = new()
                {
                    new(64, 256, 32, 32),
                },
                [7] = new()
                {
                    new(64, 288, 32, 32),
                },
                [8] = new()
                {
                    new(96, 320, 32, 32),
                },
                [9] = new()
                {
                    new(96, 288, 32, 32),
                },
                [10] = new()
                {
                    new(96, 256, 32, 32),
                },
                [11] = new()
                {
                    new(96, 288, 32, 32),
                },
            };

        }

        public virtual void DarkBrawl()
        {
            
            sweepSet = true;

            sweepInterval = 12;

            sweepFrames = new ()
            {
                [0] = new ()
                {
                    new Rectangle(96, 192, 32, 32),
                    new Rectangle(192, 192, 32, 32),
                    new Rectangle(224, 192, 32, 32),
                },
                [1] = new ()
                {
                    new Rectangle(96, 160, 32, 32),
                    new Rectangle(192, 160, 32, 32),
                    new Rectangle(224, 160, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(96, 128, 32, 32),
                    new Rectangle(192, 128, 32, 32),
                    new Rectangle(224, 128, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(96, 160, 32, 32),
                    new Rectangle(192, 160, 32, 32),
                    new Rectangle(224, 160, 32, 32),
                },
            };

            alertFrames = new()
            {
                [0] = new()
                {
                    new Rectangle(192, 320, 32, 32),
                },
                [1] = new()
                {
                    new Rectangle(160, 320, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(128, 320, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(224, 320, 32, 32),
                },
            };

        }

        public virtual void DarkSword()
        {

            sweepSet = true;

            meleeSet = true;

            sweepInterval = 8;

            sweepFrames = new()
            {
                [0] = new()
                {
                    new Rectangle(192, 288, 32, 32),
                    new Rectangle(224, 288, 32, 32),
                    new Rectangle(128, 288, 32, 32),
                    new Rectangle(160, 288, 32, 32),
                },
                [1] = new()
                {
                    new Rectangle(128, 256, 32, 32),
                    new Rectangle(160, 256, 32, 32),
                    new Rectangle(192, 256, 32, 32),
                    new Rectangle(224, 256, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(128, 288, 32, 32),
                    new Rectangle(160, 288, 32, 32),
                    new Rectangle(192, 288, 32, 32),
                    new Rectangle(224, 288, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(128, 256, 32, 32),
                    new Rectangle(160, 256, 32, 32),
                    new Rectangle(192, 256, 32, 32),
                    new Rectangle(224, 256, 32, 32),
                },
            };

            alertFrames = new()
            {
                [0] = new()
                {
                    new Rectangle(192, 320, 32, 32),
                },
                [1] = new()
                {
                    new Rectangle(160, 320, 32, 32),
                },
                [2] = new()
                {
                    new Rectangle(128, 320, 32, 32),
                },
                [3] = new()
                {
                    new Rectangle(224, 320, 32, 32),
                },
            };

        }

        public void DarkCast()
        {

            specialSet = true;

            specialCeiling = 1;

            specialFloor = 0;

            specialInterval = 30;


        }

        public void DarkBarrage()
        {

            flightDefault = flightTypes.circle;

            channelSet = true;

            channelFrames = new Dictionary<int, List<Rectangle>>()
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

        }

        public virtual void DarkBlast()
        {

            firearmSet = true;

            specialSet = true;

            specialCeiling = 1;

            specialFloor = 0;

            specialInterval = 30;

            flightDefault = flightTypes.circle;

            specialFrames = new Dictionary<int, List<Rectangle>>()
            {
                [0] = new()
                {

                    new(128, 64, 32, 32),
                    new(160, 64, 32, 32),

                },
                    [1] = new()
                {

                    new(128, 32, 32, 32),
                    new(160, 32, 32, 32),

                },
                    [2] = new()
                {

                    new(128, 0, 32, 32),
                    new(160, 0, 32, 32),

                },
                    [3] = new()
                {

                    new(128, 32, 32, 32),
                    new(160, 32, 32, 32),

                },

            };

            channelSet = true;

            channelFrames = new Dictionary<int, List<Rectangle>>()
            {

                [0] = new()
                {
                    new(160, 64, 32, 32),
                    new(160, 64, 32, 32),
                    new(160, 64, 32, 32),
                    new(128, 64, 32, 32),

                },
                    [1] = new()
                {
                    new(160, 32, 32, 32),
                    new(160, 32, 32, 32),
                    new(160, 32, 32, 32),
                    new(128, 32, 32, 32),

                },
                    [2] = new()
                {
                    new(160, 0, 32, 32),
                    new(160, 0, 32, 32),
                    new(160, 0, 32, 32),
                    new(128, 0, 32, 32),

                },
                    [3] = new()
                {
                    new(160, 32, 32, 32),
                    new(160, 32, 32, 32),
                    new(160, 32, 32, 32),
                    new(128, 32, 32, 32),

                },

            };


        }

        public override float GetScale()
        {

            //float spriteScale = 3.25f + (0.25f * netMode.Value);

            return 4f;

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {
            
            if (!loadedOut || IsInvisible || !Utility.isOnScreen(Position, 128))
            {
                return;
            }

            //DrawBoundingBox(b);

            Vector2 localPosition = Game1.GlobalToLocal(Position);

            float drawLayer = (float)StandingPixel.Y / 10000f;

            DrawEmote(b, localPosition, drawLayer);

            float spriteScale = GetScale();

            Vector2 spritePosition = GetPosition(localPosition, spriteScale);

            bool flippant = (netDirection.Value % 2 == 0 && netAlternative.Value == 3);

            bool flippity = flippant || netDirection.Value == 3;

            Microsoft.Xna.Framework.Color colour = Color.White * fadeOut;

            int hatOverride = -1;

            Character.Character.hats hatType = hats.stand;

            if (netSweepActive.Value)
            {

                Rectangle useSweepFrame = sweepFrames[netDirection.Value][sweepFrame];

                b.Draw(
                     characterTexture,
                     spritePosition,
                     useSweepFrame,
                     colour,
                     0f,
                     new Vector2(16),
                     spriteScale,
                     flippity ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                     drawLayer
                );

                if (meleeSet)
                {

                    weaponRender.DrawWeapon(b, spritePosition, drawLayer, new() { scale = spriteScale, source = useSweepFrame, flipped = flippity, fade = fadeOut, });

                    weaponRender.DrawSwipe(b, spritePosition, drawLayer, new() { scale = spriteScale, source = useSweepFrame, flipped = flippity });

                }

                if (useSweepFrame.Y == 288)
                {
                    switch (useSweepFrame.X)
                    {

                        case 128:

                            hatOverride = 2;

                            break;

                        case 160:

                            hatOverride = 1;

                            break;

                        case 192:

                            hatOverride = 0;

                            break;

                        case 224:

                            hatOverride = 3;

                            break;

                    }

                }

            }
            else if (netSpecialActive.Value)
            {

                b.Draw(
                    characterTexture,
                    spritePosition,
                    specialFrames[netDirection.Value][specialFrame],
                    colour,
                    0.0f,
                    new Vector2(16),
                    spriteScale,
                    flippant ? (SpriteEffects)1 : 0,
                    drawLayer
                );

                /*if (firearmSet)
                {

                    weaponRender.DrawFirearm(b, spritePosition, drawLayer, new() { scale = spriteScale, source = specialFrames[netDirection.Value][specialFrame], flipped = flippant, fade = fadeOut, });
                }*/
            
            }
            else if (netChannelActive.Value)
            {

                b.Draw(
                    characterTexture, 
                    spritePosition, 
                    new Rectangle?(channelFrames[netDirection.Value][specialFrame]),
                    colour,
                    0.0f,
                    new Vector2(16), 
                    spriteScale,
                    flippity ? (SpriteEffects)1 : 0, 
                    drawLayer
                    );

                if (firearmSet)
                {

                    weaponRender.DrawFirearm(b, spritePosition, drawLayer, new() { scale = spriteScale, source = channelFrames[netDirection.Value][specialFrame], flipped = flippity, fade = fadeOut,});
                }

                hatType = hats.launch;

            }
            else if (netFlightActive.Value)
            {

                int setFlightSeries = netDirection.Value + (netFlightProgress.Value * 4);

                int setFlightFrame = Math.Min(flightFrame, (flightFrames[setFlightSeries].Count - 1));

                b.Draw(
                    characterTexture,
                    spritePosition,
                    new Rectangle?(flightFrames[setFlightSeries][setFlightFrame]),
                    colour,
                    0f,
                    new Vector2(16),
                    spriteScale,
                    flippant ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

            }
            else if (netSmashActive.Value)
            {

                int setFlightSeries = netDirection.Value + (netFlightProgress.Value * 4);

                int setFlightFrame = Math.Min(flightFrame, (smashFrames[setFlightSeries].Count - 1));

                b.Draw(
                    characterTexture, 
                    spritePosition, 
                    smashFrames[setFlightSeries][setFlightFrame],
                    colour,
                    0f,
                    new Vector2(16),
                    spriteScale,
                    flippity ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

                if (meleeSet)
                {

                    weaponRender.DrawWeapon(b, spritePosition, drawLayer, new() { scale = spriteScale, source = smashFrames[setFlightSeries][setFlightFrame], flipped = flippity, fade = fadeOut, });

                    if (netFlightProgress.Value >= 2)
                    {

                        weaponRender.DrawSwipe(b, spritePosition, drawLayer, new() { scale = spriteScale, source = smashFrames[setFlightSeries][setFlightFrame], flipped = flippity });

                    }

                }

            }
            else if (netWoundedActive.Value)
            {

                b.Draw(
                    characterTexture,
                    spritePosition,
                    woundedFrames[netDirection.Value][0],
                    colour,
                    0f,
                    new Vector2(16),
                    spriteScale,
                    flippity ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

                hatType = hats.kneel;

            }
            else if (netHaltActive.Value)
            {
                
                if (netAlert.Value)
                {

                    b.Draw(
                        characterTexture,
                        spritePosition,
                        alertFrames[netDirection.Value][0],
                        colour,
                        0f,
                        new Vector2(16),
                        spriteScale,
                        flippant ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                        drawLayer
                    );
                    
                    if (meleeSet)
                    {
                        
                        weaponRender.DrawWeapon(b, spritePosition, drawLayer, new() { scale = spriteScale, source = alertFrames[netDirection.Value][0], flipped = flippant, fade = fadeOut, });
                    
                    }

                }
                else
                {

                    b.Draw(
                        characterTexture,
                        spritePosition,
                        idleFrames[netDirection.Value][0],
                        colour,
                        0f,
                        new Vector2(16),
                        spriteScale,
                        flippant ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                        drawLayer
                    );

                }

            }
            else
            {

                b.Draw(
                    characterTexture,
                    spritePosition,
                    walkFrames[netDirection.Value][walkFrame],
                    colour,
                    0f,
                    new Vector2(16),
                    spriteScale,
                    flippant ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                    drawLayer
                );

            }

            if (netShieldActive.Value)
            {

                DrawShield(b, spritePosition, spriteScale, drawLayer);

            }

            DrawShadow(b, spritePosition, spriteScale, drawLayer);

            DrawHat(b, spritePosition, spriteScale, drawLayer, hatType, hatOverride);

        }

        public virtual void DrawShadow(SpriteBatch b, Vector2 spritePosition, float spriteScale, float drawLayer)
        {

            Vector2 shadowPosition = spritePosition + new Vector2(0, spriteScale * 14);

            b.Draw(Mod.instance.iconData.cursorTexture, shadowPosition, Mod.instance.iconData.shadowRectangle, Color.White * 0.35f * fadeOut, 0.0f, new Vector2(24), spriteScale / 2, 0, drawLayer - 1E-06f);

        }

        public virtual void DrawHat(SpriteBatch b, Vector2 spritePosition, float spriteScale, float drawLayer, hats hat, int hatDirection = -1)
        {

            if (hatSelect == -1)
            {

                return;

            }

            bool flip = false;

            if (hatDirection == -1)
            {

                hatDirection = netDirection.Value;

                flip = (netDirection.Value % 2 == 0 && netAlternative.Value == 3);

            }

            int hatFrame = 40;

            if (hat == hats.kneel)
            {

                if (netDirection.Value == 3)
                {
                    hatFrame = 60;
                }
                else
                {
                    hatFrame = 20;
                }

            }
            else
            {
                switch (netDirection.Value)
                {
                    case 1:
                        hatFrame = 20;
                        break;

                    case 2:
                        hatFrame = 0;
                        break;

                    case 3:
                        hatFrame = 60;
                        break;
                }


            }

            switch (flip)
            {
                case true:

                    Vector2 hatVector = hatVectors[hat][hatDirection + 4];

                    b.Draw(
                        Mod.instance.iconData.hatTexture,
                        spritePosition - (new Vector2(0 - hatVector.X, hatVector.Y) * spriteScale),
                        new Rectangle(hatFrame, 20 * hatSelect, 20, 20),
                        Color.White * fadeOut,
                        0.0f,
                        new Vector2(10),
                        spriteScale,
                        SpriteEffects.FlipHorizontally,
                        drawLayer + 0.0001f
                    );

                    break;

                case false:
                    b.Draw(
                        Mod.instance.iconData.hatTexture,
                        spritePosition - (hatVectors[hat][hatDirection] * spriteScale),
                        new Rectangle(hatFrame, 20 * hatSelect, 20, 20),
                        Color.White * fadeOut,
                        0.0f,
                        new Vector2(10),
                        spriteScale,
                        0,
                        drawLayer + 0.0001f
                    );

                    break;

            }

        }

        public virtual void DrawShield(SpriteBatch b, Vector2 spritePosition, float spriteScale, float drawLayer)
        {

            b.Draw(
                Mod.instance.iconData.shieldTexture,
                spritePosition,
                new Microsoft.Xna.Framework.Rectangle(0, 0, 48, 48),
                Mod.instance.iconData.schemeColours[shieldScheme] * 0.2f,
                0f,
                new Vector2(24),
                spriteScale,
                0,
                drawLayer + 0.0004f
            );

            int sparkle = (int)(Game1.currentGameTime.TotalGameTime.TotalMilliseconds % 1000) / 200;

            b.Draw(
                Mod.instance.iconData.shieldTexture,
                spritePosition,
                new Microsoft.Xna.Framework.Rectangle(48 + (48 * sparkle), 0, 48, 48),
                Color.White * 0.6f,
                0f,
                new Vector2(24),
                spriteScale,
                0,
                drawLayer + 0.0005f
            );

        }

        public override Texture2D OverheadTexture()
        {

            return characterTexture;

        }

        public override Microsoft.Xna.Framework.Rectangle OverheadPortrait()
        {

            return new Rectangle(8, 0, 16, 16);

        }

        public override bool PerformSpecial(Vector2 target)
        {

            specialTimer = (specialCeiling + 1) * specialInterval;

            netSpecialActive.Set(true);

            SetCooldown(1);

            SpellHandle fireball = new(currentLocation, target, GetBoundingBox().Center.ToVector2(), 128, GetThreat() * 2 / 3)
            {
                type = SpellHandle.Spells.missile,

                factor = 2,

                missile = MissileHandle.missiles.shuriken,

                display = IconData.impacts.none,

                boss = this,

                scheme = IconData.schemes.ether
            };

            Mod.instance.spellRegister.Add(fireball);

            return true;

        }


    }


}

