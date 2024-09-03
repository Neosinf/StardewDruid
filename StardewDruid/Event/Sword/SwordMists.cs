
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

            ModUtility.AnimateHands(Game1.player,Game1.player.FacingDirection,600);

            Mod.instance.iconData.DecorativeIndicator(location, Game1.player.Position, IconData.decorations.mists, 4f, new());

            location.playSound("thunder_small");

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

                    location.playSound(SpellHandle.sounds.thunder_small.ToString());

                    Mod.instance.spellRegister.Add(new(origin + new Vector2(64, 320), 192, IconData.impacts.splash, new()) { type = SpellHandle.spells.bolt, projectile = 3, });

                    DialogueSetups(null, 3);

                    break;

                case 301:

                    //---------------------- throw Neptune Glaive

                    location.playSound(SpellHandle.sounds.thunder.ToString());

                    Mod.instance.spellRegister.Add(new(origin + new Vector2(64, 320), 256, IconData.impacts.splash, new()) { type = SpellHandle.spells.bolt, projectile = 5, });

                    DialogueCue(4);

                    break;

                case 302:

                    eventComplete = true;

                    break;

            }

        }

    }

}