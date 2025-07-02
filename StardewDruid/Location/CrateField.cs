using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace StardewDruid.Location
{

    public class CrateField
    {

        public enum crateType
        {
            content,
            sword,
        }

        public crateType type;

        public Vector2 origin;

        public enum crateStatus
        {
            closed,
            opening,
            open,
            empty

        }

        public crateStatus status;

        public int timer;

        public StardewValley.Object content;

        public SpawnData.Swords sword;

        public CrateField(Vector2 Origin)
        {

            origin = Origin;

        }

        public void draw(SpriteBatch b)
        {

            if (!Utility.isOnScreen(origin, 64))
            {

                return;

            }

            Vector2 position = new(origin.X - Game1.viewport.X, origin.Y - Game1.viewport.Y - 64);

            switch (status)
            {

                case crateStatus.closed:

                    b.Draw(
                        Mod.instance.iconData.crateTexture,
                        position + new Vector2(32, 0),
                        new(0, 0, 32, 64),
                        Color.White,
                        0f,
                        new(16, 32),
                        3f,
                        SpriteEffects.None,
                        origin.Y / 10000
                    );

                    break;

                case crateStatus.opening:

                    timer++;

                    if(timer >= 20)
                    {

                        reveal();

                    }

                    b.Draw(
                        Mod.instance.iconData.crateTexture,
                        position + new Vector2(32, 0),
                        new(32, 0, 32, 64),
                        Color.White,
                        0f,
                        new(16, 32),
                        3f,
                        SpriteEffects.None,
                        origin.Y / 10000
                    );

                    break;

                case crateStatus.open:

                    timer++;

                    if (timer >= 40)
                    {

                        release();

                    }
                    
                    b.Draw(
                        Mod.instance.iconData.crateTexture,
                        position + new Vector2(32, 0),
                        new(64, 0, 32, 64),
                        Color.White,
                        0f,
                        new(16, 32),
                        3f,
                        SpriteEffects.None,
                        origin.Y / 10000
                    );
                                        
                    break;

                case crateStatus.empty:

                    b.Draw(
                        Mod.instance.iconData.crateTexture,
                        position + new Vector2(32, 0),
                        new(64, 0, 32, 64),
                        Color.White,
                        0f,
                        new(16, 32),
                        3f,
                        SpriteEffects.None,
                        origin.Y / 10000
                    );

                    break;

            }

            b.Draw(
                Mod.instance.iconData.crateTexture,
                position + new Vector2(34, 8),
                new( 0, 0, 32, 64),
                Color.Black * 0.3f,
                0f,
                new(16, 32),
                3f,
                SpriteEffects.None,
                origin.Y / 10000 - 0.0001f
            );

        }

        public void open()
        {

            Game1.player.currentLocation.playSound(SpellHandle.Sounds.openChest.ToString());

            status = crateStatus.opening;

            timer = 0;

            switch (type)
            {
                case crateType.content:

                    new ThrowHandle(Game1.player, origin- new Vector2(0,64), content) { delay = 40, }.register();

                    DelayedAction.playSoundAfterDelay("getNewSpecialItem", 500, Game1.player.currentLocation);

                    break;

                case crateType.sword:
                    new ThrowHandle(Game1.player, origin - new Vector2(0, 64), sword) { delay = 40, }.register();
                    break;

            }

        }

        public void reveal()
        {

            List<Vector2> sparkles = new()
            {
                new Vector2(64,-128),
                new Vector2(-64,0),
                new Vector2(48,-16),
                new Vector2(-48,-112),

            };

            for(int i = 0; i < sparkles.Count; i++)
            {

                Vector2 sparkleVector = origin + sparkles[i];

                SpellHandle sparkle = new(sparkleVector, 256, IconData.impacts.glare, new())
                {
                    displayRadius = 3,

                    counter = -10 * i
                };

                Mod.instance.spellRegister.Add(sparkle);

            }

            status = crateStatus.open;

        }

        public void release()
        {

            status = crateStatus.empty;

        }

    }

}
