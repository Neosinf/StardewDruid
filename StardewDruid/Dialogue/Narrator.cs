﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Map;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using xTile.Layers;

namespace StardewDruid.Dialogue
{

    public class Narrator
    {

        public Color color;

        public string name;

        public Queue<string> buffs;

        public Narrator(string Name, Color Color)
        {

            name = Name;

            color = Color;

        }

        public void DisplayHUD(string message)
        {

            Game1.addHUDMessage(new Banter(name, color, message));

        }

        public void BufferHUD(List<string> messages)
        {

            buffs = new(messages);

            buffs.Enqueue(name);

            BufferNext();

            for(int i = 1; i < messages.Count; i++)
            {

                DelayedAction.functionAfterDelay(BufferNext, 2400 * i);

            }

            DelayedAction.functionAfterDelay(BufferLast, 2400 * messages.Count);

        }

        public void BufferNext()
        {

            string message = buffs.Dequeue();

            Game1.addHUDMessage(new Banter(name, Color.White, message, 1));

        }

        public void BufferLast()
        {

            Game1.addHUDMessage(new Banter(name, color, name, 1));

        }


    }

}
