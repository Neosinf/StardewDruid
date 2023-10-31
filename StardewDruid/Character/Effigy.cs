using Microsoft.Xna.Framework.Graphics;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using xTile.Dimensions;
using System.Xml.Serialization;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewModdingAPI;
using System.IO;

namespace StardewDruid.Character
{
    public class Effigy : StardewValley.NPC
    {

        public int movementTimer;

        public int wanderTimer;

        public Effigy()
        {

        }

        public Effigy(string Name = "Effigy")
            : base(
            CharacterData.CharacterSprite(Name),
            CharacterData.CharacterPosition(Name),
            CharacterData.CharacterMap(Name),
            2,
            Name, 
            new(),
            CharacterData.CharacterPortrait(Name),
            false
            )
        {

        }

        public override void reloadSprite()
        {
            
            Sprite = CharacterData.CharacterSprite("effigy");
            
            Portrait = CharacterData.CharacterPortrait("effigy");

        }

        public override void reloadData()
        {

            CharacterDisposition disposition = CharacterData.CharacterDisposition(name);

            Age = disposition.Age;
            
            Manners = disposition.Manners;
            
            SocialAnxiety = disposition.SocialAnxiety;
            
            Optimism = disposition.Optimism;
            
            Gender = disposition.Gender;
            
            datable.Value = disposition.datable;
            
            Birthday_Season = disposition.Birthday_Season;
            
            Birthday_Day = disposition.Birthday_Day;
            
            id = disposition.id;

            speed = disposition.speed;

        }

        public override void reloadDefaultLocation()
        {
        
            DefaultMap = CharacterData.CharacterMap(name);

            DefaultPosition = CharacterData.CharacterPosition(name);
        
        }

        protected override string translateName(string name)
        { 
            return name;
        
        }

        public override void tryToReceiveActiveObject(Farmer who)
        {
            return;
        }

        public override bool checkAction(Farmer who, GameLocation l)
        {
           
            Halt();

            movementTimer = 180;

            wanderTimer = 900;

            faceGeneralDirection(who.Position);

            /*if (!Context.IsMainPlayer)
            {

                mod.MultiplayerMessage("EffigyDialogue",who.UniqueMultiplayerID);

                return true;

            }*/

            Mod.instance.dialogueEffigy.effigy = this;

            Mod.instance.dialogueEffigy.DialogueApproach();

            return true;

        }

        public override void performTenMinuteUpdate(int timeOfDay, GameLocation l)
        {

        }

        public override void Halt()
        {
            
            moveUp = false;

            moveDown = false;

            moveRight = false;

            moveLeft = false;

            Sprite.StopAnimation();


        }

        public Dictionary<int,float> TargetProximity(string target = "origin")
        {

            Dictionary<int, float> proximity = new() { [0] = -1f };

            Vector2 targetPosition;

            if (target == "player")
            {

                if (Game1.player.currentLocation != currentLocation)
                {
                    return proximity;
                }

                if (currentLocation.map.GetLayer("Back").Tiles[(int)Game1.player.getTileLocation().X, (int)Game1.player.getTileLocation().Y] == null)
                {
                    return proximity;
                }

                if (currentLocation.map.GetLayer("Back").Tiles[(int)Game1.player.getTileLocation().X, (int)Game1.player.getTileLocation().Y].Properties.ContainsKey("NPCBarrier"))
                {
                    return proximity;
                }

                targetPosition = Game1.player.GetBoundingBox().Center.ToVector2();

            }
            else
            {

                targetPosition = CharacterData.CharacterPosition(name);


            }

            Vector2 currentPosition = GetBoundingBox().Center.ToVector2();

            float targetDistance = Vector2.Distance(targetPosition, currentPosition);

            if (target == "player" && targetDistance > 447)
            {

                return proximity;

            }
            else if(target == "origin" && targetDistance <= 447)
            {

                return proximity;

            }

            float differentialX = targetPosition.X - currentPosition.X;

            float differentialY = targetPosition.Y - currentPosition.Y;

            if (Math.Abs(differentialX) < Math.Abs(differentialY))
            {

                if (differentialY < 0)
                {

                    proximity[0] = 0f;


                }
                else
                {

                    proximity[0] = 2f;

                }

            }
            else
            {

                if (differentialX < 0)
                {

                    proximity[0] = 3f;

                }
                else
                {

                    proximity[0] = 1f;

                }

            }

            proximity[1] = targetDistance;

            proximity[2] = differentialX;

            proximity[3] = differentialY;

            return proximity;

        }

        public override void update(GameTime time, GameLocation location)
        {

            if(shakeTimer > 0)
            {
                shakeTimer = 0;
            }

            if (Game1.IsMasterGame)
            {

                if (movementTimer > 0)
                {

                    movementTimer--;

                }

                if (wanderTimer > 0)
                {

                    wanderTimer--;

                }

                updateMovement(location, time);

                MovePosition(time, Game1.viewport, location);

            }
            else
            {

                if(Sprite.loadedTexture == null || Sprite.loadedTexture.Length == 0)
                {

                    Sprite.spriteTexture = CharacterData.CharacterTexture(name);

                    Sprite.loadedTexture = Sprite.textureName.Value;

                    Portrait = CharacterData.CharacterPortrait(name);

                }

                updateSlaveAnimation(time);
            
            }


        }

        public override void updateMovement(GameLocation location, GameTime time)
        {

            if (Game1.IsClient)
            {
                
                return;
            
            }
            
            MoveTowardsOrigin();

            MoveTowardsPlayer();

            MoveRandomDirection();

        }

        public void MoveTowardsOrigin()
        {

            if (movementTimer > 0)
            {

                return;

            }

            Dictionary<int, float> proximity = TargetProximity("origin");

            if (proximity[0] < 0f)
            {
                return;
            }

            switch ((int)proximity[0])
            {
                case 0:

                    SetMovingOnlyUp();

                    break;

                case 1:

                    SetMovingOnlyRight();

                    break;
                case 2:

                    SetMovingOnlyDown();

                    break;

                default:

                    SetMovingOnlyLeft();

                    break;

            }

            movementTimer = 30;

            return;

        }

        public void MoveTowardsPlayer()
        {

            if (movementTimer > 0)
            {

                return;

            }

            if (wanderTimer > 0)
            {

                return;

            }

            Dictionary<int, float> proximity = TargetProximity("player");

            if (proximity[0] < 0f)
            {
 
                return;
            }

            if (proximity[1] <= 80f)
            {
                
                Halt();

                movementTimer = 180;

                wanderTimer = 900;

                faceDirection((int)proximity[0]);

                return;

            }

            switch ((int)proximity[0])
            {
                case 0:

                    SetMovingOnlyUp();

                    break;

                case 1: 
                    
                    SetMovingOnlyRight();
                    
                    break;
                case 2:
                    
                    SetMovingOnlyDown();
                    
                    break;
                
                default:
                    
                    SetMovingOnlyLeft();
                    
                    break;

            }

            movementTimer = 30;

            return;

        }

        public void MoveRandomDirection()
        {

            if (movementTimer > 0)
            {

                return;

            }

            int num = Game1.random.Next(5);

            if (num != (FacingDirection + 2) % 4)
            {
                if (num < 4)
                {
                        
                    int direction = FacingDirection;
                        
                    faceDirection(num);
                        
                    if (currentLocation.isCollidingPosition(nextPosition(num), Game1.viewport, this))
                    {
                            
                        faceDirection(direction);
                            
                        return;
                        
                    }
                    
                }

                switch (num)
                {
                    case 0:
                        SetMovingUp(b: true);
                        break;
                    case 1:
                        SetMovingRight(b: true);
                        break;
                    case 2:
                        SetMovingDown(b: true);
                        break;
                    case 3:
                        SetMovingLeft(b: true);
                        break;
                    default:
                        Halt();
                        Sprite.StopAnimation();
                        break;
                }

                movementTimer = 30;

            }

            return;

        }

        public override void MovePosition(GameTime time, xTile.Dimensions.Rectangle viewport, GameLocation currentLocation)
        {
            
            if (Game1.IsClient)
            {
                return;
            }

            Location location = nextPositionTile();

            Microsoft.Xna.Framework.Rectangle next;

            if (moveUp)
            {
                next = nextPosition(0);

                if (!currentLocation.isCollidingPosition(next, Game1.viewport, isFarmer: false, 0, glider: false, this, pathfinding: false))
                {
                    position.Y -= base.speed;
                    Sprite.AnimateUp(time);
                }
                else if (!HandleCollision(next, 0))
                {
                    Halt();
  
                    if (Game1.random.NextDouble() < 0.6)
                    {
                        SetMovingDown(b: true);
                    }
                }

                faceDirection(0);
            }
            else if (moveRight)
            {

                next = nextPosition(1);

                if (!currentLocation.isCollidingPosition(next, Game1.viewport, isFarmer: false, 0, glider: false, this))
                {
                    position.X += base.speed;
                    Sprite.AnimateRight(time);
                }
                else if (!HandleCollision(next, 1))
                {
                    Halt();

                    if (Game1.random.NextDouble() < 0.6)
                    {
                        SetMovingLeft(b: true);
                    }
                }

                faceDirection(1);
            }
            else if (moveDown)
            {

                next = nextPosition(2);

                if (!currentLocation.isCollidingPosition(next, Game1.viewport, isFarmer: false, 0, glider: false, this))
                {
                    position.Y += base.speed;
                    Sprite.AnimateDown(time);
                }
                else if (!HandleCollision(next, 2))
                {
                    Halt();

                    if (Game1.random.NextDouble() < 0.6)
                    {
                        SetMovingUp(b: true);
                    }
                }

                faceDirection(2);
            }
            else
            {
                if (!moveLeft)
                {
                    return;
                }
                next = nextPosition(3);

                if (!currentLocation.isCollidingPosition(next, Game1.viewport, isFarmer: false, 0, glider: false, this))
                {
                    position.X -= base.speed;
                    Sprite.AnimateRight(time);
                }
                else if (!HandleCollision(next, 3))
                {
                    Halt();

                    if (Game1.random.NextDouble() < 0.6)
                    {
                        SetMovingRight(b: true);
                    }
                }

                faceDirection(3);

            }
        }

        public virtual bool HandleCollision(Microsoft.Xna.Framework.Rectangle next, int direction = 0)
        {

            Dictionary<int, float> proximity = TargetProximity("player");

            if (proximity[0] < 0f)
            {

                return false;

            }

            if (proximity[1] <= 80f && (int)proximity[0] == direction)
            {

                Halt();

                movementTimer = 180;

                wanderTimer = 900;

                return true;

            }

            return false;

        }


    }

}
