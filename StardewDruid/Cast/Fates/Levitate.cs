﻿using Microsoft.Xna.Framework;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Event;
using StardewModdingAPI;
using StardewValley;
using System;
using System.Collections.Generic;

namespace StardewDruid.Cast.Fates
{
    public class Levitate : EventHandle
    {

        public NPC npc;

        public Vector2 oldPosition;

        public float oldRotation;

        public float upwards;

        public bool positionReset;

        public int levitationCounter;

        public Levitate(Vector2 target,  NPC NPC)
            : base(target)
        {

            npc = NPC;

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 5;

            oldPosition = npc.Position;

            oldRotation = npc.rotation;

        }

        public override void EventTrigger()
        {

            Mod.instance.RegisterEvent(this, "levitate" + npc.Name);

        }

        public override bool EventActive()
        {

            if (levitationCounter >= 20)
            {

                return false;

            }

            if (expireEarly)
            {

                return false;

            }

            if (targetPlayer.currentLocation.Name != targetLocation.Name)
            {

                return false;

            }

            if (expireTime < Game1.currentGameTime.TotalGameTime.TotalSeconds)
            {

                return false;

            }

            return true;

        }

        public override void EventRemove()
        {

            if (!positionReset)
            {

                npc.position.Y += upwards;

                npc.rotation = oldRotation;

            }

            base.EventRemove();

        }

        public override void EventDecimal()
        {

            if (!EventActive())
            {
                return;
            }

            npc.Halt();

            levitationCounter++;

            npc.position.X = oldPosition.X;

            npc.rotation += (float)Math.PI / 10;

            if (levitationCounter <= 10)
            {

                npc.position.Y -= 6.4f;

            }
            else
            {

                npc.position.Y += 6.4f;

            }

            if (levitationCounter >= 20)
            {

                npc.Position = oldPosition;

                npc.rotation = oldRotation;

                positionReset = true;

                expireEarly = true;

            }

        }

    }

}
