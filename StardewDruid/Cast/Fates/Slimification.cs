using Microsoft.Xna.Framework;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast.Fates
{
    public class Slimification : EventHandle
    {
        public NPC npc;

        public bool invis;

        public Slimification(NPC witness, bool Invis)
        {

            npc = witness;

            invis = Invis;

            eventId = "slimification_" + witness.Name;

            if (Invis)
            {

                npc.IsInvisible = true;

            }

            activeLimit = 5;

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

            frame.position -= new Vector2(0, npc.Sprite.SpriteHeight * 2);

            npc.currentLocation.temporarySprites.Add(frame);

            animations.Add(frame);

            // SlimeFrame

            Microsoft.Xna.Framework.Rectangle slimeSource = new(0, 240, 48, 48);

            int scheme = (int)Data.IconData.schemes.herbal_ligna + Mod.instance.randomIndex.Next(5);

            TemporaryAnimatedSprite slime = new(0, 250, 6, 4, npc.Position, false, false)
            {
                texture = Mod.instance.iconData.blobTexture,
                sourceRect = slimeSource,
                sourceRectStartingPos = new Vector2(slimeSource.X, slimeSource.Y),
                layerDepth = npc.Position.Y / 10000 + 0.0006f,
                alpha = 0.35f,
                scale = 5f,
                color = Mod.instance.iconData.schemeColours[(IconData.schemes)scheme],

            };

            slime.position -= new Vector2(80, 112);

            npc.currentLocation.temporarySprites.Add(slime);

            animations.Add(slime);

            // shadow

            slimeSource.Y += 48;

            TemporaryAnimatedSprite slimeshadow = new(0, 250, 6, 4, npc.Position, false, false)
            {
                texture = Mod.instance.iconData.blobTexture,
                sourceRect = slimeSource,
                sourceRectStartingPos = new Vector2(slimeSource.X, slimeSource.Y + 48),
                layerDepth = npc.Position.Y / 10000 + 0.0005f,
                alpha = 0.15f,
                scale = 5f,
                color = Color.White,

            };

            slimeshadow.position -= new Vector2(80, 112);

            npc.currentLocation.temporarySprites.Add(slimeshadow);

            animations.Add(slimeshadow);

            // gleam

            slimeSource.Y += 48;

            TemporaryAnimatedSprite slimegleam = new(0, 250, 6, 4, npc.Position, false, false)
            {
                texture = Mod.instance.iconData.blobTexture,
                sourceRect = slimeSource,
                sourceRectStartingPos = new Vector2(slimeSource.X, slimeSource.Y + 96),
                layerDepth = npc.Position.Y / 10000 + 0.0007f,
                alpha = 0.75f,
                scale = 5f,
                color = Color.White,

            };

            slimegleam.position -= new Vector2(80, 112);

            npc.currentLocation.temporarySprites.Add(slimegleam);

            animations.Add(slimegleam);

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

            if (!EventActive())
            {

                return;

            }

            decimalCounter++;

            animations[0].position = npc.Position;

            animations[0].position -= new Vector2(0, npc.Sprite.SpriteHeight * 2);

            animations[0].position.Y += 2f * (5 - Math.Abs((decimalCounter % 10) - 5));

            animations[1].position = npc.Position;

            animations[1].position -= new Vector2(80, 112);

            animations[2].position = npc.Position;

            animations[2].position -= new Vector2(80, 112);

            animations[3].position = npc.Position;

            animations[3].position -= new Vector2(80, 112);

            if (decimalCounter >= 30)
            {

                eventComplete = true;

            }

        }

    }

}
