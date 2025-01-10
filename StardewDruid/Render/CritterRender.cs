using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Monster;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static StardewDruid.Character.Character;

namespace StardewDruid.Render
{
    public class CritterRender
    {
        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> idleFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> walkFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> runningFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> sweepFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> specialFrames = new();

        public Dictionary<int, List<Microsoft.Xna.Framework.Rectangle>> dashFrames = new();

        public CritterRender(string name)
        {

            idleFrames= FrameSeries(32, 32, 0, 0, 1);

            walkFrames = FrameSeries(32, 32, 0, 0, 7);

            runningFrames = FrameSeries(32, 32, 0, 128, 6, FrameSeries(32, 32, 0, 0, 1));

            specialFrames = new()
            {

                [0] = new() { new(128, 192, 32, 32), },

                [1] = new() { new(128, 160, 32, 32), },

                [2] = new() { new(128, 128, 32, 32), },

                [3] = new() { new(128, 224, 32, 32), },

            };

            sweepFrames = FrameSeries(32, 32, 0, 128, 3);

            dashFrames = new(sweepFrames);

            dashFrames[4] = new() { new(64, 192, 32, 32), };
            dashFrames[5] = new() { new(64, 160, 32, 32), };
            dashFrames[6] = new() { new(64, 128, 32, 32), };
            dashFrames[7] = new() { new(64, 192, 32, 32), };

            dashFrames[8] = new() { new(96, 192, 32, 32), new(128, 192, 32, 32), new(160, 192, 32, 32), };
            dashFrames[9] = new() { new(96, 160, 32, 32), new(128, 160, 32, 32), new(160, 160, 32, 32), };
            dashFrames[10] = new() { new(96, 128, 32, 32), new(128, 128, 32, 32), new(160, 128, 32, 32), };
            dashFrames[11] = new() { new(96, 192, 32, 32), new(128, 192, 32, 32), new(160, 192, 32, 32), };

        }

        public virtual Dictionary<int, List<Rectangle>> FrameSeries(int width, int height, int startX = 0, int startY = 0, int length = 6, Dictionary<int, List<Rectangle>> frames = null)
        {

            if (frames == null)
            {
                frames = new()
                {
                    [0] = new(),
                    [1] = new(),
                    [2] = new(),
                    [3] = new(),
                };
            }

            Dictionary<int, int> normalSequence = new()
            {
                [0] = 2,
                [1] = 1,
                [2] = 0,
                [3] = 3
            };

            foreach (KeyValuePair<int, int> keyValuePair in normalSequence)
            {

                for (int index = 0; index < length; index++)
                {

                    Rectangle rectangle = new(startX, startY, width, height);

                    rectangle.X += width * index;

                    rectangle.Y += height * keyValuePair.Value;

                    frames[keyValuePair.Key].Add(rectangle);

                }

            }

            return frames;

        }

    }

}
