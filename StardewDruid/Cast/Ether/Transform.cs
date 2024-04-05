
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Dialogue;
using StardewDruid.Event;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Minigames;
using System;
using System.Drawing;
using System.IO;
using xTile.Dimensions;

namespace StardewDruid.Cast.Ether
{
    public class Transform : EventHandle
    {
        public Dragon avatar;
        public int toolIndex;
        public int attuneableIndex;
        public int extendTime;
        public int moveTimer;
        public int castTimer;
        public SButton leftButton;
        public bool leftActive;
        public SButton rightButton;
        public bool rightActive;

        public bool warpTrigger;
        public string warpLocation;

        public Transform(Vector2 target,  int extend)
          : base(target)
        {
            extendTime = extend;
        }

        public override void EventTrigger()
        {

            Mod.instance.RegisterEvent(this, "transform");

            Mod.instance.clickRegister[0] = "transform";

            expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + extendTime;

            CreateAvatar();

        }

        public void CreateAvatar()
        {

            Game1.displayFarmer = false;

            avatar = new Dragon(Mod.instance.rite.caster, Mod.instance.rite.caster.Position, Mod.instance.rite.caster.currentLocation.Name, Mod.instance.ColourPreference() + "Dragon");

            avatar.currentLocation = Game1.player.currentLocation;

            Game1.player.currentLocation.characters.Add(avatar);

            Game1.player.currentLocation.playSound("warrior", Game1.player.Position, 700);

        }


        public override bool EventActive()
        {

            if (expireEarly)
            {

                return false;

            }

            if (avatar == null)
            {

                return false;

            }

            if (expireTime <= Game1.currentGameTime.TotalGameTime.TotalSeconds)
            {

                if (!avatar.SafeExit())
                {

                    expireTime += 30;

                    return true;

                }

                return false;

            }

            return true;

        }

        public override bool AttemptAbort()
        {
            if (warpTrigger)
            {

                return false;

            }

            if (avatar != null)
            {

                if (avatar.SafeExit())
                {

                    EventRemove();

                    expireEarly = true;

                    return true;

                }

                return false;

            }

            return true;

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

        }

        public override bool EventPerformAction(SButton Button, string Type)
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

            if (Type == "Special" && rightActive)
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
                    
                    if (SpawnData.SpawnIndex(Game1.player.currentLocation).Count == 0)
                    {

                        expireEarly = true;

                        return;

                    }

                    avatar.ShutDown();

                    CreateAvatar();

                    TemporaryAnimatedSprite radiusAnimation = new(0, 2000, 1, 1, avatar.Position - new Vector2(64, 64), false, false)
                    {

                        sourceRect = new(0, 64, 64, 64),

                        sourceRectStartingPos = new Vector2(0, 64),

                        texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Decorations.png")),

                        scale = 3f,

                        timeBasedMotion = true,

                        layerDepth = 999f,

                        rotationChange = (float)(Math.PI / 120),

                        alpha = 0.65f,
                    };

                    targetLocation.temporarySprites.Add(radiusAnimation);

                    animations.Add(radiusAnimation);

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

            TemporaryAnimatedSprite radiusAnimation = new(0, 2000, 1, 1, avatar.Position - new Vector2(64,64), false, false)
            {

                sourceRect = new(0, 64, 64, 64),

                sourceRectStartingPos = new Vector2(0, 64),

                texture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Decorations.png")),

                scale = 3f,

                timeBasedMotion = true,

                layerDepth = 999f,

                rotationChange = (float)(Math.PI / 120),

                alpha = 0.65f,
            };

            targetLocation.temporarySprites.Add(radiusAnimation);

            animations.Add(radiusAnimation);

            warpTrigger = true;

            warpLocation = Game1.locationRequest.Location.Name;

        }

        public override void EventInterval()
        {

            foreach (NPC character in Game1.player.currentLocation.characters)
            {

                if (character is StardewValley.Monsters.Monster)
                {

                    continue;
                }

                if (character is Character.Character)
                {

                    continue;
                }

                if (character is Dragon)
                {

                    continue;
                }

                if (!character.IsVillager)
                {

                    continue;
                }

                float distance = Vector2.Distance(character.Position, Game1.player.Position);

                if (distance < 640f)
                {

                    if (Mod.instance.rite.Witnessed("ether", character))
                    {
                        continue;
                    }

                    if (!Mod.instance.TaskList.ContainsKey("masterTransform"))
                    {

                        Mod.instance.UpdateTask("lessonTransform", 1);

                    }

                    ModUtility.GreetVillager(Game1.player, character, 15);

                    Reaction.ReactTo(character, "Ether");

                }

            }

        }


    }

}
