using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Event;
using StardewDruid.Handle;
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

                if (!Mod.instance.RiteButtonHeld())
                {

                    return false;

                }

                if (Vector2.Distance(origin, Game1.player.Position) > 32 && !Mod.instance.ShiftButtonHeld())
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

                    Mod.instance.rite.Channel(IconData.skies.sunset, 75);

                    channel = IconData.skies.sunset;

                }

                if (decimalCounter == 15)
                {

                    eventLocked = true;

                    Mod.instance.spellRegister.Add(new(origin, 128, IconData.impacts.smoke, new()) {  sound = SpellHandle.Sounds.getNewSpecialItem, });

                    if (!Mod.instance.questHandle.IsComplete(QuestHandle.wealdFour))
                    {

                        Mod.instance.questHandle.UpdateTask(QuestHandle.wealdFour, 1);

                    }

                    Mod.instance.rite.ChargeSet(IconData.cursors.bonesCharge);

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

                if (Mod.instance.trackers.ContainsKey(corvid))
                {

                    return true;

                }

            }

            return false;

        }


        public static void SummonCorvids(CharacterHandle.characters summon = CharacterHandle.characters.none)
        {
            List<CharacterHandle.characters> corvids = new()
            {
                summon
            };

            if (summon == CharacterHandle.characters.none)
            {
                corvids = new()
                {
                    CharacterHandle.characters.Rook,
                    CharacterHandle.characters.Crow,
                    CharacterHandle.characters.Raven,
                    CharacterHandle.characters.Magpie
                };

            }

            Dictionary<CharacterHandle.characters, float> corvidScales = new()
            {
                [CharacterHandle.characters.Raven] = 3.5f,
                [CharacterHandle.characters.Crow] = 3.25f,
                [CharacterHandle.characters.Magpie] = 3f,
                [CharacterHandle.characters.Rook] = 3f,

            };

            Dictionary<CharacterHandle.characters, int> corvidQuadrants = new()
            {
                [CharacterHandle.characters.Raven] = 1,
                [CharacterHandle.characters.Crow] = 3,
                [CharacterHandle.characters.Magpie] = 5,
                [CharacterHandle.characters.Rook] = 7,

            };

            foreach (CharacterHandle.characters corvid in corvids)
            {

                Character.Flyer flyer = new(corvid)
                {
                    setScale = corvidScales[corvid],

                    trackQuadrant = corvidQuadrants[corvid]
                };

                if (Context.IsMainPlayer)
                {

                    Mod.instance.characters[corvid] = flyer;

                }
                else
                {

                    Mod.instance.dopplegangers[corvid] = flyer;

                }

                flyer.SwitchToMode(Character.Character.mode.track, Game1.player);

                Mod.instance.trackers[corvid].WarpToPlayer();

            }


        }
        public static void SummonRaven()
        {
            SummonCorvids(CharacterHandle.characters.Raven);
        }

        public static void SummonCrow()
        {
            SummonCorvids(CharacterHandle.characters.Crow);
        }

        public static void SummonRook()
        {
            SummonCorvids(CharacterHandle.characters.Rook);
        }

        public static void SummonMagpie()
        {
            SummonCorvids(CharacterHandle.characters.Magpie);
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

                if (!Mod.instance.trackers.ContainsKey(corvid))
                {
                    
                    continue;

                }

                StardewDruid.Character.Character companion = Mod.instance.trackers[corvid].TrackSubject();

                Mod.instance.iconData.AnimateQuickWarp(companion.currentLocation, companion.Position, false, IconData.warps.corvids);

                companion.currentLocation.characters.Remove(companion);

                Mod.instance.characters.Remove(corvid);

                Mod.instance.dopplegangers.Remove(corvid);

                Mod.instance.trackers.Remove(corvid);

            }

        }

    }

}
