
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
        public int warpTimeout;
        public string warpLocation;

        public Transform()
        {
            
        }

        public virtual void EventWarp()
        {

            warpTrigger = true;

            warpTimeout = 10;

            warpLocation = Game1.locationRequest.Location.Name;

            if (avatar != null)
            {

                EventRemove();

            }

            /*if (warpTrigger)
            {

                if (warpLocation != Game1.locationRequest.Location.Name)
                {

                    warpTimeout = 30;

                    warpLocation = Game1.locationRequest.Location.Name;

                }

                return;

            }*/


        }

        public override void EventActivate()
        {

            eventId = Rite.eventTransform;

            Mod.instance.RegisterEvent(this, Rite.eventTransform);

            CreateAvatar();

            eventActive = true;

        }

        public void CreateAvatar()
        {

            EventClicks(actionButtons.action);

            EventClicks(actionButtons.special);

            Game1.displayFarmer = false;

            avatar = new Dragon(Game1.player, Game1.player.Position, Game1.player.currentLocation.Name, "RedDragon");

            avatar.currentLocation = Game1.player.currentLocation;

            Game1.player.currentLocation.characters.Add(avatar);

            Game1.player.currentLocation.playSound(SpellHandle.sounds.warrior.ToString());

            BuffEffects buffEffect = new();

            buffEffect.Defense.Set(5);

            Buff dragonBuff = new(Rite.buffIdDragon, 
                source: StringData.RiteNames(Rite.rites.ether),
                displaySource: StringData.RiteNames(Rite.rites.ether),
                duration: Buff.ENDLESS, 
                displayName: StringData.Strings(StringData.stringkeys.dragonBuff),
                description: StringData.Strings(StringData.stringkeys.dragonBuffDescription),
                effects: buffEffect);

            Game1.player.buffs.Apply(dragonBuff);

        }

        public override bool EventActive()
        {

            eventCounter = 0;

            if (eventComplete)
            {

                return false;

            }

            if (warpTrigger && !Game1.isWarping)
            {

                warpTrigger = false;

                warpTimeout = 0;

                SpawnIndex spawnCheck = new(Game1.player.currentLocation);

                if (spawnCheck.cast)
                {

                    CreateAvatar();

                }

            }

            if (warpTrigger)
            {

                warpTimeout--;

                if (warpTimeout > 0)
                {

                    return true;

                }

                warpTrigger = false;

                eventComplete = true;

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

            /*if (warpTrigger)
            {

                return true;

            }*/

            if (avatar == null)
            {

                return false;

            }

            return !avatar.SafeExit();

        }


        public override void EventRemove()
        {

            if (Game1.player.CurrentToolIndex == 999)
            {

                Game1.player.CurrentToolIndex = toolIndex;

            }

            Game1.displayFarmer = true;

            if (avatar != null)
            {

                avatar.ShutDown();

                avatar.currentLocation.characters.Remove(avatar);

                avatar = null;

            }

            if (Game1.player.buffs.IsApplied(Rite.buffIdDragon))
            {

                Game1.player.buffs.Remove(Rite.buffIdDragon);

            }

            RemoveClicks();

        }

        public override bool EventPerformAction(SButton Button, actionButtons Action = actionButtons.action)
        {

            if (!EventActive())
            {

                return false;

            }

            if (warpTrigger)
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

            if (!EventActive()) { 
                
                return; 
            
            }

            if (warpTrigger)
            {
                
                return;

            }

            if (
                Game1.player.CurrentToolIndex != 999 || 
                Mod.instance.Helper.Input.IsDown(rightButton) || 
                Mod.instance.Helper.Input.IsDown(leftButton)
            )
            {
                return;
            }

            Game1.player.CurrentToolIndex = toolIndex;

        }

        public override void EventInterval()
        {

            if (warpTrigger) 
            { 
                
                return; 
            
            }
            
            if((int)Game1.currentGameTime.TotalGameTime.TotalSeconds % 3 != 0)
            {

                return;

            }

            if (!Mod.instance.magic)
            {

                Mod.instance.rite.CreateTreasure();

            }

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

                    ModUtility.ChangeFriendship(Game1.player, character, 15);

                    ReactionData.ReactTo(character, ReactionData.reactions.dragon, 15);

                }

            }

        }

    }

}
