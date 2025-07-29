using Microsoft.Xna.Framework;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Event;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace StardewDruid.Cast.Fates
{
    public class Levitate : EventHandle
    {

        public NPC npc;

        public bool invis;

        public Levitate(NPC witness, bool Invis)
        {

            npc = witness;

            invis = Invis;

            eventId = "levitation_" + witness.Name;

            if (Invis)
            {

                npc.IsInvisible = true;

            }

            // NPC frame

            Microsoft.Xna.Framework.Rectangle npcSource = npc.Sprite.SourceRect;

            TemporaryAnimatedSprite frame = new(0, 5000, 1, 1, npc.Position, false, false)
            {
                texture = npc.Sprite.Texture,
                sourceRect = npcSource,
                sourceRectStartingPos = new Vector2(npcSource.X, npcSource.Y),
                layerDepth = npc.Position.Y / 10000 + 0.0005f,
                scale = 4f,
            };

            frame.Position -= new Vector2(0, npc.Sprite.SpriteHeight * 2f);

            npc.currentLocation.temporarySprites.Add(frame);

            animations.Add(frame);

        }

        public override void EventRemove()
        {

            if (invis)
            {

                npc.IsInvisible = false;

            }

            base.EventRemove();

        }

        public override void EventDecimal()
        {

            decimalCounter++;

            animations[0].rotation += (float)Math.PI / 10;

            animations[0].position = npc.Position;

            animations[0].position -= new Vector2(0, npc.Sprite.SpriteHeight * 2f);

            animations[0].position.Y -= 12f * (10 - Math.Abs(decimalCounter - 10));

            if (decimalCounter >= 20)
            {

                eventComplete = true;

            }

        }

    }

}
