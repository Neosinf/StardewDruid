using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Data;
using StardewDruid.Handle;
using StardewModdingAPI;
using StardewValley;
using StardewValley.BellsAndWhistles;
using System;
using System.Threading;

namespace StardewDruid.Character
{
    public class Actor : StardewDruid.Character.Character
    {

        public bool drawSlave;

        public Actor()
        {

        }

        public Actor(CharacterHandle.characters type)
          : base(type)
        {
        }

        public override void LoadOut()
        {

            characterType = CharacterHandle.characters.disembodied;

            loadedOut = true;

        }

        public override void draw(SpriteBatch b, float alpha = 1f)
        {
            
            if (Context.IsMainPlayer && drawSlave)
            {
                
                foreach (NPC character in currentLocation.characters)
                {
                    
                    character.drawAboveAlwaysFrontLayer(b);
                
                }

            }

        }

        public override Texture2D OverheadTexture()
        {

            return Mod.instance.iconData.displayTexture;

        }

        public override Microsoft.Xna.Framework.Rectangle OverheadPortrait()
        {

            return IconData.DisplayRectangle(Data.IconData.displays.chaos);

        }

        public override void behaviorOnFarmerPushing()
        {

            return;

        }

        public override bool checkAction(Farmer who, GameLocation l)
        {

            return false;

        }

        public override void update(GameTime time, GameLocation location)
        {
            
            if (!checkUpdate())
            {

                return;

            }

            normalUpdate(time, location);

            return;

        }

    }

}
