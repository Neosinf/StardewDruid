using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewValley;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StardewValley.Network;
using StardewDruid.Character;
using StardewDruid.Cast.Mists;

namespace StardewDruid.Journal
{
    public class InterfaceComponent
    {

        public DruidJournal.journalButtons button;

        public Microsoft.Xna.Framework.Vector2 position;

        public IconData.displays display;

        public Microsoft.Xna.Framework.Rectangle bounds;

        public Microsoft.Xna.Framework.Rectangle source;

        public int hover;

        public string text;

        public bool active;

        public float fade = 1f;

        public JournalAdditional spec = new();

        public InterfaceComponent(DruidJournal.journalButtons Button, Vector2 Position, IconData.displays Display, JournalAdditional additional)
        {

            position = Position;

            button = Button;

            display = Display;

            spec = additional;

            setBounds();

        }

        public void setBounds()
        {

            source = IconData.DisplayRectangle(display);

            bounds = new((int)position.X - (int)(8f * spec.scale), (int)position.Y - (int)(8f * spec.scale), (int)(16f * spec.scale), (int)(16f * spec.scale));

            text = JournalData.ButtonStrings(button);

        }

        public void draw(SpriteBatch b)
        {

            switch (button)
            {

                default:

                    b.Draw(
                        Mod.instance.iconData.displayTexture,
                        position,
                        source,
                        Color.White * fade,
                        0f,
                        new Vector2(8),
                        spec.scale + (0.05f * hover),
                        spec.flip ? SpriteEffects.FlipHorizontally : 0,
                        999f
                    );

                    break;

                case DruidJournal.journalButtons.HP:

                    b.DrawString(Game1.smallFont, text, position - new Vector2(28,32), Color.Wheat, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.88f);

                    b.DrawString(Game1.smallFont, Game1.player.health + StringData.slash + Game1.player.maxHealth, position - new Vector2(28, 0), Color.Wheat, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.88f);

                    break;

                case DruidJournal.journalButtons.STM:

                    b.DrawString(Game1.smallFont, text, position - new Vector2(28,32), Color.Wheat, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.88f);

                    b.DrawString(Game1.smallFont, (int)Game1.player.Stamina + StringData.slash + Game1.player.MaxStamina, position - new Vector2(28, 0), Color.Wheat, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0.88f);

                    break;


            }


        }

    }

    public class JournalAdditional
    {

        public float scale = 3.5f;

        public bool flip;

        public float hoverLimit = 5;

    }

}
