﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using System;
using System.Threading;

namespace StardewDruid.Character
{
    public class Actor : StardewDruid.Character.Character
    {

        public bool drawSlave;

        public Actor()
        {

        }

        public Actor(CharacterHandle.characters type)
          : base(type)
        {
        }

        public override void LoadOut()
        {

            characterType = CharacterHandle.characters.disembodied;

            loadedOut = true;

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {
            
            if (Utility.isOnScreen(Position, 128))
            {

                DrawEmote(b);

            }

            if (Context.IsMainPlayer && drawSlave)
            {
                
                foreach (NPC character in currentLocation.characters)
                {
                    
                    character.drawAboveAlwaysFrontLayer(b);
                
                }

            }

        }

        public override void drawAboveAlwaysFrontLayer(SpriteBatch b)
        {
            
            if (textAboveHeadTimer > 0 && textAboveHead != null)
            {
                
                Point standingPixel = base.StandingPixel;
                
                Vector2 vector = Game1.GlobalToLocal(new Vector2(standingPixel.X, standingPixel.Y - 144f));
                
                if (textAboveHeadStyle == 0)
                {
                    vector += new Vector2(Game1.random.Next(-1, 2), Game1.random.Next(-1, 2));
                }
                
                Point tilePoint = base.TilePoint;

                SpriteText.drawStringWithScrollCenteredAt(b, textAboveHead, (int)vector.X, (int)vector.Y, "", textAboveHeadAlpha, textAboveHeadColor, 1, (float)(tilePoint.Y * 64) / 10000f + 0.001f + (float)tilePoint.X / 10000f);

            }

        }

        public override void behaviorOnFarmerPushing()
        {

            return;

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {

            return false;

        }

        public override void update(GameTime time, GameLocation location)
        {
            
            if (Context.IsMainPlayer)
            {

                if (shakeTimer > 0)
                {
                    shakeTimer = 0;
                }

                if (textAboveHeadTimer > 0)
                {
                    if (textAboveHeadPreTimer > 0)
                    {
                        textAboveHeadPreTimer -= time.ElapsedGameTime.Milliseconds;
                    }
                    else
                    {
                        textAboveHeadTimer -= time.ElapsedGameTime.Milliseconds;
                        if (textAboveHeadTimer > 500)
                        {
                            textAboveHeadAlpha = Math.Min(1f, textAboveHeadAlpha + 0.1f);
                        }
                        else
                        {
                            float newAlpha = textAboveHeadAlpha - 0.04f;

                            textAboveHeadAlpha = newAlpha < 0f ? 0f : newAlpha;
                        }
                    }
                }

                updateEmote(time);

            }

            return;

        }

    }

}
