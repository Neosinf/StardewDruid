
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Event;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Buffs;
using StardewValley.Minigames;
using System;
using System.Drawing;
using System.IO;
using xTile.Dimensions;
using static StardewDruid.Cast.Rite;

namespace StardewDruid.Cast.Ether
{
    public class Transform : EventHandle
    {
        public Dragon avatar;
        public int toolIndex;
        public int attuneableIndex;
        public int moveTimer;
        public int castTimer;
        public SButton leftButton;
        public bool leftActive;
        public SButton rightButton;
        public bool rightActive;

        public bool warpTrigger;
        public string warpLocation;

        public Transform()
        {
            
        }

        public override void EventActivate()
        {

            eventId = "transform";

            Mod.instance.RegisterEvent(this, "transform");

            EventClicks(actionButtons.action);

            EventClicks(actionButtons.special);

            CreateAvatar();

            eventActive = true;

        }

        public void CreateAvatar()
        {

            Game1.displayFarmer = false;

            avatar = new Dragon(Game1.player, Game1.player.Position, Game1.player.currentLocation.Name, "RedDragon");

            avatar.currentLocation = Game1.player.currentLocation;

            Game1.player.currentLocation.characters.Add(avatar);

            Game1.player.currentLocation.playSound("warrior", null, 700);

            BuffEffects buffEffect = new();

            buffEffect.Defense.Set(5);

            Buff speedBuff = new("184655", 
                source: DialogueData.RiteNames(Rite.rites.ether),
                displaySource: DialogueData.RiteNames(Rite.rites.ether),
                duration: Buff.ENDLESS, 
                displayName: DialogueData.Strings(DialogueData.stringkeys.dragonBuff),
                description: DialogueData.Strings(DialogueData.stringkeys.dragonBuffDescription),
                effects: buffEffect);

            Game1.player.buffs.Apply(speedBuff);

            Mod.instance.iconData.DecorativeIndicator(Game1.player.currentLocation, Game1.player.Position,IconData.decorations.ether, 3f, new() { interval = 1200, });

        }

        public override bool EventActive()
        {

            if (eventComplete)
            {

                return false;

            }

            if (avatar == null)
            {

                return false;

            }

            return true;

        }

        public override bool AttemptReset()
        {
            
            if (warpTrigger)
            {

                return true;

            }

            if (avatar != null)
            {

                return !avatar.SafeExit();

            }

            return false;

        }


        public override void EventRemove()
        {
            
            if (Game1.player.CurrentToolIndex == 999)
            {

                Game1.player.CurrentToolIndex = toolIndex;

            }

            Game1.displayFarmer = true;

            if (avatar == null)
            {

                return;

            }

            avatar.ShutDown();

            avatar = null;

            if (Game1.player.buffs.IsApplied("184655"))
            {

                Game1.player.buffs.Remove("184655");

            }

        }

        public override bool EventPerformAction(SButton Button, actionButtons Action = actionButtons.action)
        {

            if (!EventActive())
            {

                return false;

            }

            if (Game1.player.CurrentToolIndex != 999)
            {

                int num = Mod.instance.AttuneableWeapon();

                if (num == -1)
                {

                    return false;

                }

                toolIndex = Game1.player.CurrentToolIndex;

                attuneableIndex = num;

                Game1.player.CurrentToolIndex = 999;

            }

            if (!Game1.shouldTimePass(false))
            {
                return false;
            }

            if (Action == actionButtons.special && rightActive)
            {

                avatar.RightClickAction(Button);

                rightButton = Button;

                return true;

            }

            if (!leftActive)
            {
                return false;
            }

            avatar.LeftClickAction(Button);

            leftButton = Button;

            return true;

        }

        public override void EventDecimal()
        {

            if(warpTrigger)
            {

                if(Game1.player.currentLocation.Name == warpLocation)
                {
                   
                    warpTrigger = false;

                    SpawnIndex spawnCheck = new(Game1.player.currentLocation);

                    if (!spawnCheck.cast)
                    {

                        eventComplete = true;

                        return;

                    }

                    avatar.ShutDown();

                    CreateAvatar();

                }

            }

            if (!EventActive()) { return; }

            if (Game1.player.CurrentToolIndex != 999 || Mod.instance.Helper.Input.IsDown(rightButton) || Mod.instance.Helper.Input.IsDown(leftButton))
            {
                return;
            }

            Game1.player.CurrentToolIndex = toolIndex;

        }

        public virtual void EventWarp()
        {

            if (warpTrigger) { return; }

            Mod.instance.iconData.DecorativeIndicator(Game1.player.currentLocation, avatar.Position - new Vector2(64, 64), IconData.decorations.ether, 3f, new() {interval = 2000, });

            warpTrigger = true;

            warpLocation = Game1.locationRequest.Location.Name;

        }

        public override void EventInterval()
        {

            if (warpTrigger) { return; }
            
            if((int)Game1.currentGameTime.TotalGameTime.TotalSeconds % 3 != 0)
            {

                return;

            }

            Mod.instance.rite.CreateTreasure();

            foreach (NPC character in ModUtility.GetFriendsInLocation(Game1.player.currentLocation,true))
            {

                float distance = Vector2.Distance(character.Position, Game1.player.Position);

                if (distance < 448f)
                {

                    if (Mod.instance.Witnessed(ReactionData.reactions.dragon, character))
                    {
                        continue;
                    }

                    character.faceTowardFarmerForPeriod(3000, 4, false, Game1.player);

                    Game1.player.changeFriendship(15, character);

                    ReactionData.ReactTo(character, ReactionData.reactions.dragon, 15);

                }

            }

        }

    }

}
