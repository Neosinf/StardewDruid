using Microsoft.Xna.Framework;
using Netcode;
using StardewDruid.Map;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Monsters;
using StardewValley.Network;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StardewDruid.Character
{
  public class Character : NPC
  {
    public List<Vector2> moveVectors;
    public int moveDirection;
    public int altDirection;
    public Dictionary<string, int> timers;
    public List<string> priorities;
    public float gait;
    public List<StardewValley.Monsters.Monster> targetOpponents;
    public int opponentThreshold;
    public int roamIndex;
    public List<Vector2> roamVectors;
    public double roamLapse;
    public List<Vector2> eventVectors;

    public Character()
    {
    }

    public Character(Vector2 position, string map, string Name)
      : base(CharacterData.CharacterSprite(Name), position, map, 2, Name, new Dictionary<int, int[]>(), CharacterData.CharacterPortrait(Name), false, (string) null)
    {
      willDestroyObjectsUnderfoot = false;
      priorities = new List<string>();
      timers = new Dictionary<string, int>();
      moveVectors = new List<Vector2>();
      roamVectors = new List<Vector2>();
      eventVectors = new List<Vector2>();
      moveDirection = 0;
      targetOpponents = new();
      opponentThreshold = 640;
      gait = 1.2f;
      DefaultMap = map;
      DefaultPosition = position;
    }

    public override void reloadSprite()
    {
      Sprite = CharacterData.CharacterSprite(Name);
      Portrait = CharacterData.CharacterPortrait(Name);
    }

    public override void reloadData()
    {
      CharacterDisposition characterDisposition = CharacterData.CharacterDisposition(Name);
      Age = characterDisposition.Age;
      Manners = characterDisposition.Manners;
      SocialAnxiety = characterDisposition.SocialAnxiety;
      Optimism = characterDisposition.Optimism;
      Gender = characterDisposition.Gender;
        datable.Value = characterDisposition.datable;
      Birthday_Season = characterDisposition.Birthday_Season;
      Birthday_Day = characterDisposition.Birthday_Day;
      id = characterDisposition.id;
    }

    public override void reloadDefaultLocation()
    {
      DefaultMap = Mod.instance.CharacterMap(Name);
      if (DefaultMap == null)
        DefaultMap = "FarmCave";
      DefaultPosition = CharacterData.CharacterPosition(DefaultMap);
    }

    protected override string translateName(string name) => name;

    public override void tryToReceiveActiveObject(Farmer who)
    {
    }

    public override bool checkAction(Farmer who, GameLocation l)
    {
      if (Mod.instance.eventRegister.ContainsKey("transform"))
        return false;
      foreach (NPC character in currentLocation.characters)
      {
        if (character is StardewValley.Monsters.Monster monster && (double) Vector2.Distance(Position, ((StardewValley.Character) monster).Position) <= 1280.0)
          return false;
      }
      Halt();
      faceGeneralDirection(((StardewValley.Character) who).Position, 0, false);
      moveDirection = FacingDirection;
      switch (moveDirection)
      {
        case 0:
          moveUp = true;
          break;
        case 1:
          moveRight = true;
          break;
        case 2:
          moveDown = true;
          break;
        default:
          moveLeft = true;
          break;
      }
      return true;
    }

    public override void performTenMinuteUpdate(int timeOfDay, GameLocation l)
    {
    }

    public override void behaviorOnFarmerPushing()
    {
      if (!Context.IsMainPlayer || priorities.Contains("frozen"))
        return;
      if (timers.ContainsKey("push"))
      {
        timers["push"] += 2;
        if (timers["push"] <= 10)
          return;
        moveVectors.Clear();
        TargetDirection(findPlayer().facingDirection, 2);
        timers.Remove("Push");
      }
      else
        timers["push"] = 2;
    }

    public override void Halt()
    {
      if (Context.IsMainPlayer)
      {
        moveVectors.Clear();
        timers.Clear();
        timers["stop"] = 60;
      }
      moveDown = false;
      moveLeft = false;
      moveRight = false;
      moveUp = false;
      Sprite.currentFrame -= Sprite.currentFrame % Sprite.framesPerAnimation;
      Sprite.UpdateSourceRect();
    }

    public virtual void normalUpdate(GameTime time, GameLocation location)
    {
      if (!Context.IsMainPlayer)
      {
        if (Sprite.loadedTexture == null || Sprite.loadedTexture.Length == 0)
        {
          Sprite.spriteTexture = CharacterData.CharacterTexture(Name);
          Sprite.loadedTexture = Sprite.textureName.Value;
          Portrait = CharacterData.CharacterPortrait(Name);
        }
        updateSlaveAnimation(time);
      }
      else
      {
        if (shakeTimer > 0)
          shakeTimer = 0;
        if (textAboveHeadTimer > 0)
        {
          if (textAboveHeadPreTimer > 0)
          {
            textAboveHeadPreTimer -= time.ElapsedGameTime.Milliseconds;
          }
          else
          {
            textAboveHeadTimer -= time.ElapsedGameTime.Milliseconds;
            if (textAboveHeadTimer > 500)
              textAboveHeadAlpha = Math.Min(1f, textAboveHeadAlpha + 0.1f);
            else
              textAboveHeadAlpha = Math.Max(0.0f, textAboveHeadAlpha - 0.04f);
          }
        }
        updateEmote(time);
      }
    }

    public override void update(GameTime time, GameLocation location)
    {
      normalUpdate(time, location);
      if (!Context.IsMainPlayer)
        return;
      for (int index = timers.Count - 1; index >= 0; --index)
      {
        KeyValuePair<string, int> keyValuePair = timers.ElementAt(index);
        timers[keyValuePair.Key]--;
        if (timers[keyValuePair.Key] <= 0)
          timers.Remove(keyValuePair.Key);
      }
      if (!timers.ContainsKey("stop"))
      {
        if (moveVectors.Count > 0 && (double) Vector2.Distance(moveVectors.First(), Position) <= 16.0)
          moveVectors.RemoveAt(0);
        if (moveVectors.Count <= 0)
          UpdateTarget();
        MoveTowardsTarget(time);
      }
      Sprite.animateOnce(time);
    }

    public virtual Rectangle GetHitBox() => GetBoundingBox();

    public virtual void UpdateTarget()
    {
      if (Game1.IsClient)
        return;
      foreach (string priority in priorities)
      {
        switch (priority)
        {
          case "event":
            if (TargetEvent())
              return;
            break;
          case "frozen":
            timers["stop"] = 1000;
            Sprite.CurrentFrame = 0;
            if (new Random().Next(2) != 0)
              return;
            timers["idle"] = 1000;
            return;
          case "attack":
            if (TargetOpponent())
              return;
            break;
          case "track":
            if (TargetTrack())
              return;
            break;
          case "roam":
            if (TargetRoam())
              return;
            break;
        }
      }
      TargetRandom();
    }

    public virtual bool TargetEvent()
    {
      if (eventVectors.Count <= 0)
        return false;
      Vector2 target = eventVectors.First<Vector2>();
      if ((double) Vector2.Distance(target, Position) <= 32.0)
      {
        ReachedEventPosition();
        return true;
      }
      VectorForTarget(target, 4f);
      return true;
    }

    public virtual bool TargetOpponent()
    {
      if (timers.ContainsKey("cooldown"))
        return false;
      for (int index = targetOpponents.Count - 1; index >= 0; --index)
      {
        if (!ModUtility.MonsterVitals(targetOpponents[index], currentLocation))
          targetOpponents.RemoveAt(index);
        else if ((double) Vector2.Distance(targetOpponents[index].Position, Position) >= (double) opponentThreshold)
          targetOpponents.RemoveAt(index);
      }
      if (targetOpponents.Count == 0)
        return false;
      VectorForTarget(targetOpponents.First().Position);
      timers["attack"] = 120;
      return true;
    }

    public virtual bool TargetTrack()
    {
      if (!Mod.instance.trackRegister.ContainsKey(Name) || Mod.instance.trackRegister[Name].trackVectors.Count == 0)
        return false;
      float num = Vector2.Distance(Position, ((StardewValley.Character) Game1.player).Position);
      if ((double) num <= 180.0 && !timers.ContainsKey("track"))
        return false;
      if ((double) Vector2.Distance(Mod.instance.trackRegister[Name].trackVectors.First<Vector2>(), Position) >= 180.0)
        WarpToTarget();
      if (Mod.instance.trackRegister[Name].trackVectors.Count == 0)
      {
        if (new Random().Next(2) != 0)
          return false;
        timers["stop"] = 300;
        timers["idle"] = 300;
        return true;
      }
      VectorForTarget(Mod.instance.trackRegister[Name].NextVector(), -1f, false);
      timers["track"] = 120;
      if ((double) num > 480.0)
        timers["sprint"] = 180;
      else
        timers["hurry"] = 120;
      return true;
    }

    public virtual void WarpToTarget()
    {
      if (currentLocation.Name != ((StardewValley.Character) Game1.player).currentLocation.Name)
      {
        Halt();
        currentLocation.characters.Remove((NPC) this);
        currentLocation = ((StardewValley.Character) Game1.player).currentLocation;
        currentLocation.characters.Add((NPC) this);
      }
      if (Mod.instance.trackRegister[Name].trackVectors.Count > 0)
      {
        Mod.instance.trackRegister[Name].TruncateTo(3);
        Position = Mod.instance.trackRegister[Name].NextVector();
      }
            else
            {

                Position = new Vector2(Position.X, Position.Y + 64f);//Vector2.op_Addition(((StardewValley.Character)Game1.player).Position, new Vector2(0.0f, 64f));

            }
      
      Vector2 warpPosition = new(Position.X, Position.Y + 32f);

            ModUtility.AnimateQuickWarp(currentLocation, warpPosition, "Solar");
    }

    public virtual bool TargetRoam()
    {
      if (roamVectors.Count == 0)
      {
        roamLapse = Game1.currentGameTime.TotalGameTime.TotalMinutes + 1.0;
        roamVectors = RoamAnalysis();
      }
      Vector2 roamVector = roamVectors[roamIndex];
      if (roamVector == new Vector2(-1f))
      {
        ReachedIdlePosition();
        UpdateRoam(true);
        return true;
      }
      float num = Vector2.Distance(roamVector, Position);
      if ((double) num <= 120.0)
      {
        ReachedRoamPosition();
        UpdateRoam(true);
        return true;
      }
      if ((double) num >= 1200.0)
        timers["hurry"] = 300;
      UpdateRoam();
      VectorForTarget(roamVectors[roamIndex], 4f);
      return true;
    }

    public void UpdateRoam(bool reset = false)
    {
      if (!(roamLapse < Game1.currentGameTime.TotalGameTime.TotalMinutes | reset))
        return;
      roamLapse = Game1.currentGameTime.TotalGameTime.TotalMinutes + 1.0;
      ++roamIndex;
      if (roamIndex == roamVectors.Count)
        roamIndex = 0;
    }

    public void VectorForTarget(Vector2 target, float ahead = 1.5f, bool check = true)
    {
      Vector2 vector2_1 = target;
      float val2 = Vector2.Distance(Position, target);
            Vector2 vector2_2 = new(target.X - Position.X, target.Y - Position.Y);//Vector2.op_Subtraction(target, Position);
      float num1 = Math.Abs(vector2_2.X);
      float num2 = Math.Abs(vector2_2.Y);
      int num3 = (double) vector2_2.X < 0.001 ? -1 : 1;
      int num4 = (double) vector2_2.Y < 0.001 ? -1 : 1;
      if ((double) num1 > (double) num2)
      {
        moveDirection = 2 - num3;
        altDirection = 1 + num4;
      }
      else
      {
        moveDirection = 1 + num4;
        altDirection = 2 - num3;
      }
      if ((double) val2 >= 128.0 & check)
      {
        float num5 = Math.Min(64f * ahead, val2);
        Vector2 vector2_3;
        if ((double) num1 > (double) num2)
        {
          moveDirection = 2 - num3;
          altDirection = 1 + num4;


                    vector2_3 = new Vector2((float)num3*num5, (float)(int)((double)num2 / (double)num1 * (double)num4)*num5);//Vector2.op_Multiply(new Vector2((float) num3, (float) (int) ((double) num2 / (double) num1 * (double) num4)), num5);
        }
        else
        {
          moveDirection = 1 + num4;
          altDirection = 2 - num3;

          vector2_3 = vector2_3 = new Vector2((float)(int)((double)num1 / (double)num2 * (double)num3) * num5, (float)num4 * num5);//Vector2.op_Multiply(new Vector2((float) (int) ((double) num1 / (double) num2 * (double) num3), (float) num4), num5);
                }
                vector2_1 = new Vector2(Position.X + vector2_3.X, Position.Y + vector2_3.Y);//Vector2.op_Addition(Position, vector2_3);
      }
      moveVectors.Add(vector2_1);
    }

    public virtual void TargetRandom()
    {
      moveVectors.Clear();
      timers.Clear();
      Random random = new Random();
      if (random.Next(2) == 0)
        TargetDirection(random.Next(4), random.Next(1, 4));
      else
        Halt();
    }

    public virtual void TargetDirection(int direction, int distance)
    {
      int num = 64 * distance;
      Vector2 vector2;
      switch (direction)
      {
        case 0:
                    vector2 = new(Position.X, Position.Y - num);//Vector2.op_Subtraction(Position, new Vector2(0.0f, (float) num));
          break;
        case 1:
                    vector2 = new(Position.X + num, Position.Y);//vector2 = Vector2.op_Addition(Position, new Vector2((float) num, 0.0f));
                    break;
        case 2:
                    vector2 = new(Position.X, Position.Y + num);//vector2 = Vector2.op_Addition(Position, new Vector2(0.0f, (float) num));
                    break;
        default:
                    vector2 = new(Position.X- num, Position.Y);//vector2 = Vector2.op_Subtraction(Position, new Vector2((float) num, 0.0f));
                    break;
      }
      moveDirection = direction;
      altDirection = moveDirection + 1;
      moveVectors.Add(vector2);
    }

    public virtual void MoveTowardsTarget(GameTime time)
    {
      if (Game1.IsClient)
        return;
      if (moveVectors.Count == 0)
      {
        Sprite.currentFrame -= Sprite.currentFrame % Sprite.framesPerAnimation;
        Sprite.UpdateSourceRect();
      }
      else
      {
        Vector2 vector2_1 = moveVectors.First<Vector2>();
                Vector2 vector2_2 = new(vector2_1.X - Position.X, vector2_1.Y - Position.Y);//Vector2.op_Subtraction(vector2_1, Position);
        float num1 = Math.Abs(vector2_2.X);
        float num2 = Math.Abs(vector2_2.Y);
        int num3 = (double) vector2_2.X < 1.0 / 1000.0 ? -1 : 1;
        int num4 = (double) vector2_2.Y < 1.0 / 1000.0 ? -1 : 1;
        float num5 = (float) num3;
        float num6 = (float) num4;
        if ((double) num1 > (double) num2)
          num6 = (double) num2 < 0.05f ? 0.0f : (float) (int) ((double) num2 / (double) num1 * (double) num4);
        else
          num5 = (double) num1 < 0.05f ? 0.0f : (float) (int) ((double) num1 / (double) num2 * (double) num3);
        Vector2 vector2_3 = new(num5, num6);
        float num7 = gait;
        if (timers.ContainsKey("sprint") || timers.ContainsKey("attack"))
          num7 = gait * 5f;
        else if (timers.ContainsKey("hurry"))
          num7 = gait * 2f;
                Vector2 vector2_4 = new(vector2_3.X * num7, vector2_1.Y * num7);//Vector2.op_Multiply(vector2_3, num7);
        if ((double) Vector2.Distance(vector2_1, Position) <= (double) num7 * 1.25)
          vector2_4 = vector2_2;
        if (timers.ContainsKey("force"))
        {
          Position = new(Position.X +vector2_4.X, Position.Y * vector2_4.Y);//Vector2.op_Addition(Position, vector2_4);
                    AnimateMovement(time);
        }
        else
        {
          Rectangle boundingBox1 = GetBoundingBox();
          boundingBox1.X += (int) vector2_4.X;
          boundingBox1.Y += (int) vector2_4.Y;
          Rectangle boundingBox2 = ((StardewValley.Character) Game1.player).GetBoundingBox();
          bool flag = false;
          if (boundingBox2.Intersects(boundingBox1))
            flag = true;
          Rectangle hitBox = GetHitBox();
          hitBox.X += (int) vector2_4.X;
          hitBox.Y += (int) vector2_4.Y;
          List<StardewValley.Monsters.Monster> monsterList = new();
          float num8 = 9999f;
          foreach (NPC character in currentLocation.characters)
          {
            Rectangle boundingBox3 = character.GetBoundingBox();
            if (character is StardewValley.Monsters.Monster monster)
            {
              if (monster.Health > 0 && !((NPC) monster).IsInvisible)
              {
                if (boundingBox3.Intersects(hitBox))
                  monsterList.Add(monster);
                if (!timers.ContainsKey("attack"))
                {
                  float num9 = Vector2.Distance(Position, ((StardewValley.Character) monster).Position);
                  if ((double) num9 < (double) opponentThreshold && (double) num9 < (double) num8)
                  {
                    num8 = num9;
                    targetOpponents.Clear();
                    targetOpponents.Add(monster);
                  }
                }
              }
            }
            else if (character != this && boundingBox3.Intersects(boundingBox1))
              flag = true;
          }
          if (monsterList.Count > 0 && !timers.ContainsKey("cooldown"))
          {
            foreach (StardewValley.Monsters.Monster monsterCharacter in monsterList)
              DealDamageToMonster(monsterCharacter);
            timers["attack"] = 10;
            timers["cooldown"] = 120;
          }
          else if (flag && !timers.ContainsKey("collide"))
          {
            TargetRandom();
            timers.Add("collide", 120);
          }
          else
          {
                        Vector2 vector2_5 = new(Position.X + (vector2_3.X * 64f), Position.Y + (vector2_3.Y * 64f));//Vector2.op_Addition(Position, Vector2.op_Multiply(vector2_3, 64f));
                        Vector2 vector2_6 = new(Position.X / 64f, Position.Y / 64f);//;
            // ISSUE: explicit constructor call
            //((Vector2) ref vector2_6).\u002Ector((float) (int) ((double) Position.X / 64.0), (float) (int) ((double) Position.Y / 64.0));
            Vector2 neighbour = new(vector2_5.X / 64f, vector2_5.Y / 64f);//;
                                                                          // ISSUE: explicit constructor call
                                                                          //((Vector2) ref neighbour).\u002Ector((float) (int) ((double) vector2_5.X / 64.0), (float) (int) ((double) vector2_5.Y / 64.0));
                                                                          //if (Vector2.op_Inequality(vector2_6, neighbour) && !ModUtility.GroundCheck(currentLocation, neighbour, true))
                        if (vector2_6 != neighbour && !ModUtility.GroundCheck(currentLocation, neighbour, true))
                        {
              TargetRandom();
            }
            else
            {

                            Position = new(Position.X + vector2_4.X, Position.Y + vector2_4.Y);//Vector2.op_Addition(Position, vector2_4);
              AnimateMovement(time);
            }
          }
        }
      }
    }

    public virtual void AnimateMovement(GameTime time)
    {
      moveDown = false;
      moveLeft = false;
      moveRight = false;
      moveUp = false;
      FacingDirection = moveDirection;
      switch (moveDirection)
      {
        case 0:
          moveUp = true;
          Sprite.AnimateUp(time, 0, "");
          break;
        case 1:
          moveRight = true;
          Sprite.AnimateRight(time, 0, "");
          break;
        case 2:
          moveDown = true;
          Sprite.AnimateDown(time, 0, "");
          break;
        default:
          moveLeft = true;
          Sprite.AnimateLeft(time, 0, "");
          break;
      }
    }

    public virtual void WarpToDefault()
    {
      Halt();
      if (currentLocation.Name != DefaultMap)
      {
        currentLocation.characters.Remove((NPC) this);
        currentLocation = Game1.getLocationFromName(DefaultMap);
        currentLocation.characters.Add((NPC) this);
      }
      Position = DefaultPosition;
      update(Game1.currentGameTime, currentLocation);
    }

    public virtual void SwitchEventMode()
    {
      SwitchDefaultMode();
      priorities = new List<string>()
      {
        "event",
        "frozen"
      };
    }

    public virtual void SwitchFrozenMode()
    {
      SwitchDefaultMode();
      priorities = new List<string>() { "frozen" };
    }

    public virtual void SwitchFollowMode()
    {
      SwitchDefaultMode();
      Mod.instance.trackRegister.Add(Name, new TrackHandle(Name));
      priorities = new List<string>()
      {
        "attack",
        "track"
      };
    }

    public virtual void SwitchRoamMode()
    {
      SwitchDefaultMode();
      roamLapse = Game1.currentGameTime.TotalGameTime.TotalMinutes + 1.0;
      priorities = new List<string>() { "roam" };
    }

    public virtual void SwitchDefaultMode()
    {
      Halt();
      priorities = new List<string>();
      Mod.instance.trackRegister.Remove(Name);
    }

    public virtual List<Vector2> RoamAnalysis()
    {
      int layerWidth = currentLocation.map.Layers[0].LayerWidth;
      int layerHeight = currentLocation.map.Layers[0].LayerHeight;
      int num = layerWidth / 8;

            int midWidth = (layerWidth / 2) * 64;
            int eighthWidth = (layerWidth / 8) * 64;
            int nextWidth = (layerWidth * 64) - eighthWidth;
            int midHeight = (layerHeight / 2) * 64;
            int eighthHeight= (layerHeight / 8) * 64;
            int nextHeight = (layerHeight * 64) - eighthHeight;
            return new List<Vector2>()
      {
        new(midWidth, midHeight),
        new(-1f),
        new(nextWidth,eighthHeight),
        new(midWidth,midHeight),
        new(-1f),
        new(nextWidth,nextHeight),
        new(midWidth,midHeight),
        new(-1f),
        new(eighthWidth,nextHeight),
        new(midHeight,midHeight),
        new(-1f),
        new(eighthWidth,eighthHeight),
        //Vector2.op_Multiply(new Vector2((float) (layerWidth / 2), (float) (layerHeight / 2)), 64f),

        //Vector2.op_Multiply(new Vector2((float) (layerWidth - num), (float) num), 64f),
        //Vector2.op_Multiply(new Vector2((float) (layerWidth / 2), (float) (layerHeight / 2)), 64f),
        //new Vector2(-1f),
        //Vector2.op_Multiply(new Vector2((float) (layerWidth - num), (float) (layerHeight - num)), 64f),
        //Vector2.op_Multiply(new Vector2((float) (layerWidth / 2), (float) (layerHeight / 2)), 64f),
        //new Vector2(-1f),
        //Vector2.op_Multiply(new Vector2((float) num, (float) (layerHeight - num)), 64f),
        //Vector2.op_Multiply(new Vector2((float) (layerWidth / 2), (float) (layerHeight / 2)), 64f),
        //new Vector2(-1f),
        //Vector2.op_Multiply(new Vector2((float) num, (float) num), 64f)
      };
    }

    public virtual void DealDamageToMonster(
      StardewValley.Monsters.Monster monsterCharacter,
      bool kill = false,
      int damage = -1,
      bool push = true)
    {
      if (damage == -1)
        damage = Mod.instance.DamageLevel() / 2;
      if (!kill)
        damage = Math.Min(damage, monsterCharacter.Health - 1);
      int diffX = 0;
      int diffY = 0;
      if (!monsterCharacter.isGlider.Value && !MonsterData.CustomMonsters().Contains(monsterCharacter.GetType()) && push)
      {
        float num1 = monsterCharacter.Position.X - Position.X;
        float num2 = monsterCharacter.Position.Y - Position.Y;
        int num3 = 1;
        int num4 = 1;
        if ((double) num2 < 0.0)
          num3 = -1;
        if ((double) num1 < 0.0)
          num4 = -1;
        if ((double) Math.Abs(num1) < (double) Math.Abs(num2))
        {
          float num5 = Math.Abs(num1) / Math.Abs(num2);
          diffX = (int) ((double) (128 * num4) * (double) num5);
          diffY = 128 * num3;
        }
        else
        {
          float num6 = Math.Abs(num2) / Math.Abs(num1);
          diffX = 128 * num4;
          diffY = (int) ((double) (128 * num3) * (double) num6);
        }
      }
      ModUtility.HitMonster(currentLocation, Game1.player, monsterCharacter, damage, false, diffX: diffX, diffY: diffY);
    }

    public virtual void ReachedEventPosition()
    {
      Halt();
      eventVectors.RemoveAt(0);
    }

    public virtual void ReachedRoamPosition()
    {
    }

    public virtual void ReachedIdlePosition()
    {
      Halt();
      timers["stop"] = 1000;
      timers["idle"] = 1000;
    }

    public virtual void LeftClickAction(SButton Button)
    {
    }

    public virtual void RightClickAction(SButton Button)
    {
    }

    public virtual void ShutDown()
    {
    }

    public virtual void PlayerBusy()
    {
    }
  }
}
