
using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewModdingAPI;
using StardewValley;


namespace StardewDruid.Event.Sword
{
    internal class SwordMists : EventHandle
    {

        public SwordMists()
        {

        }

        public override void EventActivate()
        {

            base.EventActivate();

            Mod.instance.spellRegister.Add(new(Game1.player.Position, 384, IconData.impacts.supree, new()) { sound = SpellHandle.Sounds.thunder_small, });

            AddActor(0, origin + new Vector2(-64, 256));
            AddActor(1, origin + new Vector2(192, 320));
        }

        public override void EventInterval()
        {

            activeCounter++;

            switch (activeCounter)
            {

                case 1:


                    DialogueCue(1);

                    break;

                case 4:

                    DialogueCue(2);

                    break;

                case 7:

                    DialogueCue(3);

                    break;

                case 10:

                    DialogueSetups(null, 1);

                    break;

                case 101:

                    DialogueSetups(null, 2);

                    break;

                case 201:

                    location.playSound(SpellHandle.Sounds.thunder_small.ToString());

                    Mod.instance.spellRegister.Add(new(origin + new Vector2(64, 320), 192, IconData.impacts.splash, new()) { type = SpellHandle.Spells.bolt, displayFactor = 3, displayRadius = 2, });

                    DialogueSetups(null, 3);

                    break;

                case 301:

                    //---------------------- throw Neptune Glaive

                    location.playSound(SpellHandle.Sounds.thunder.ToString());

                    Mod.instance.spellRegister.Add(new(origin + new Vector2(64, 320), 256, IconData.impacts.splash, new()) { type = SpellHandle.Spells.bolt, displayFactor = 5, displayRadius = 3, });

                    DialogueCue(4);

                    break;

                case 302:

                    eventComplete = true;

                    break;

            }

        }

    }

}