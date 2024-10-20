using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Journal;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Tools;
using System.Collections.Generic;

namespace StardewDruid.Cast.Bones
{
    public class Corvids : EventHandle
    {

        public Corvids()
        {

        }

        public override bool EventActive()
        {

            if (!inabsentia && !eventLocked)
            {

                if (Mod.instance.Config.riteButtons.GetState() != SButtonState.Held)
                {

                    return false;

                }

                if (Vector2.Distance(origin, Game1.player.Position) > 32)
                {

                    return false;

                }

            }
            
            return base.EventActive();

        }

        public override void EventDecimal()
        {

            if (!EventActive())
            {

                RemoveAnimations();

                return;

            }

            if (!inabsentia && !eventLocked)
            {
                
                decimalCounter++;

                if (decimalCounter == 5)
                {

                    Mod.instance.rite.channel(IconData.skies.sunset, 75);

                    channel = IconData.skies.sunset;

                }

                if (decimalCounter == 15)
                {

                    eventLocked = true;

                    Mod.instance.spellRegister.Add(new(origin, 384, IconData.impacts.nature, new()) {  sound = SpellHandle.sounds.getNewSpecialItem, });

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.wealdFour))
                    {

                        Mod.instance.questHandle.UpdateTask(QuestHandle.wealdFour, 1);

                    }

                }

                return;

            }

            if (CheckCorvids())
            {

                if (!Mod.instance.trackers[CharacterHandle.characters.Raven].eventLock)
                {

                    RemoveCorvids();

                }

            }
            else
            {

                SummonCorvids();

            }

            eventComplete = true;

        }

        public static bool CheckCorvids()
        {

            List<CharacterHandle.characters> corvids = new()
            {
                CharacterHandle.characters.Rook,
                CharacterHandle.characters.Crow,
                CharacterHandle.characters.Raven,
                CharacterHandle.characters.Magpie
            };

            foreach (CharacterHandle.characters corvid in corvids)
            {

                if (Mod.instance.characters.ContainsKey(corvid))
                {

                    return true;

                }

            }

            return false;

        }


        public static void SummonCorvids()
        {

            SummonRaven();

            SummonCrow();

            SummonRook();

            SummonMagpie();

        }
        public static void SummonRaven()
        {
            // -------------------------------------- Raven

            Mod.instance.characters[CharacterHandle.characters.Raven] = new Character.Flyer(CharacterHandle.characters.Raven);

            Mod.instance.characters[CharacterHandle.characters.Raven].setScale = 3.5f;

            Mod.instance.characters[CharacterHandle.characters.Raven].trackQuadrant = 1;

            Mod.instance.characters[CharacterHandle.characters.Raven].SwitchToMode(Character.Character.mode.track, Game1.player);

            Mod.instance.trackers[CharacterHandle.characters.Raven].WarpToPlayer();
        }

        public static void SummonCrow()
        {
            // -------------------------------------- Crow

            Mod.instance.characters[CharacterHandle.characters.Crow] = new Character.Flyer(CharacterHandle.characters.Crow);

            Mod.instance.characters[CharacterHandle.characters.Crow].setScale = 3.25f;

            Mod.instance.characters[CharacterHandle.characters.Crow].trackQuadrant = 3;

            Mod.instance.characters[CharacterHandle.characters.Crow].SwitchToMode(Character.Character.mode.track, Game1.player);

            Mod.instance.trackers[CharacterHandle.characters.Crow].WarpToPlayer();
        }

        public static void SummonRook()
        {
            // -------------------------------------- Rook

            Mod.instance.characters[CharacterHandle.characters.Rook] = new Character.Flyer(CharacterHandle.characters.Rook);

            Mod.instance.characters[CharacterHandle.characters.Rook].setScale = 3f;

            Mod.instance.characters[CharacterHandle.characters.Rook].trackQuadrant = 5;

            Mod.instance.characters[CharacterHandle.characters.Rook].SwitchToMode(Character.Character.mode.track, Game1.player);

            Mod.instance.trackers[CharacterHandle.characters.Rook].WarpToPlayer();
        }

        public static void SummonMagpie()
        {
            // -------------------------------------- Magpie

            Mod.instance.characters[CharacterHandle.characters.Magpie] = new Character.Flyer(CharacterHandle.characters.Magpie);

            Mod.instance.characters[CharacterHandle.characters.Magpie].setScale = 3f;

            Mod.instance.characters[CharacterHandle.characters.Magpie].trackQuadrant = 7;

            Mod.instance.characters[CharacterHandle.characters.Magpie].SwitchToMode(Character.Character.mode.track, Game1.player);

            Mod.instance.trackers[CharacterHandle.characters.Magpie].WarpToPlayer();
        }

        public static void RemoveCorvids()
        {
            
            List<CharacterHandle.characters> corvids = new()
            {
                CharacterHandle.characters.Rook,
                CharacterHandle.characters.Crow,
                CharacterHandle.characters.Raven,
                CharacterHandle.characters.Magpie
            };

            foreach (CharacterHandle.characters corvid in corvids)
            {

                if (!Mod.instance.characters.ContainsKey(corvid))
                {
                    
                    continue;

                }

                Mod.instance.iconData.AnimateQuickWarp(Mod.instance.characters[corvid].currentLocation, Mod.instance.characters[corvid].Position, false, IconData.warps.corvids);

                Mod.instance.characters[corvid].currentLocation.characters.Remove(Mod.instance.characters[corvid]);

                Mod.instance.characters.Remove(corvid);

                Mod.instance.trackers.Remove(corvid);

            }

        }

    }

}
