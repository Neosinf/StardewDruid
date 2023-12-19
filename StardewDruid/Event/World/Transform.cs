
using Microsoft.Xna.Framework;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Dialogue;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Monsters;
using StardewValley.Network;

namespace StardewDruid.Event.World
{
  public class Transform : EventHandle
  {
    public StardewDruid.Character.Character avatar;
    public int toolIndex;
    public int attuneableIndex;
    public int extendTime;
    public int moveTimer;
    public Vector2 castPosition;
    public int castTimer;
    public SButton leftButton;
    public bool leftActive;
    public SButton rightButton;
    public bool rightActive;

    public Transform(Vector2 target, Rite rite, int extend)
      : base(target, rite)
    {
      this.extendTime = extend;
    }

    public override void EventTrigger()
    {
      Mod.instance.RegisterEvent((EventHandle) this, "transform");
      this.expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + (double) this.extendTime;
      Game1.displayFarmer = false;
      this.avatar = (StardewDruid.Character.Character) new Dragon(((StardewValley.Character) this.riteData.caster).Position, ((StardewValley.Character) this.riteData.caster).currentLocation.Name, Mod.instance.ColourPreference() + "Dragon");
      ((StardewValley.Character) this.avatar).currentLocation = ((StardewValley.Character) Game1.player).currentLocation;
      Game1.currentLocation.characters.Add((NPC) this.avatar);
      Game1.currentLocation.playSoundPitched("warrior", 700, (NetAudio.SoundContext) 0);
      this.castPosition = ((StardewValley.Character) this.avatar).Position;
    }

    public override bool EventActive()
    {
      return !(((StardewValley.Character) Game1.player).currentLocation.Name != this.targetLocation.Name) && !this.expireEarly && !this.avatar.timers.ContainsKey("end") && this.expireTime >= Game1.currentGameTime.TotalGameTime.TotalSeconds;
    }

    public override void EventRemove()
    {
      if (Game1.player.CurrentToolIndex == 999)
        Game1.player.CurrentToolIndex = this.toolIndex;
      Game1.displayFarmer = true;
      if (this.avatar == null)
        return;
      this.avatar.ShutDown();
      ((StardewValley.Character) this.avatar).currentLocation.characters.Remove((NPC) this.avatar);
      this.avatar = (StardewDruid.Character.Character) null;
    }

    public override bool EventPerformAction(SButton Button)
    {
      if (!this.EventActive())
        return false;
      if (Game1.player.CurrentToolIndex != 999)
      {
        int num = Mod.instance.AttuneableWeapon();
        if (num == -1)
          return false;
        this.toolIndex = Game1.player.CurrentToolIndex;
        this.attuneableIndex = num;
        Game1.player.CurrentToolIndex = 999;
      }
      if (!Game1.shouldTimePass(false))
        return false;
      if (Game1.didPlayerJustRightClick(false) && this.rightActive)
      {
        this.avatar.RightClickAction(Button);
        this.rightButton = Button;
        return true;
      }
      if (!this.leftActive)
        return false;
      this.avatar.LeftClickAction(Button);
      this.leftButton = Button;
      return true;
    }

    public override void EventExtend()
    {
      if (this.avatar == null)
        return;
      this.avatar.PlayerBusy();
    }

    public override void EventDecimal()
    {
      if (this.avatar != null && !Game1.shouldTimePass(false))
        this.avatar.PlayerBusy();
      if (Game1.player.CurrentToolIndex != 999 || Mod.instance.Helper.Input.IsDown(this.rightButton) || Mod.instance.Helper.Input.IsDown(this.leftButton))
        return;
      Game1.player.CurrentToolIndex = this.toolIndex;
    }

    public override void EventInterval()
    {
      foreach (NPC character in ((StardewValley.Character) this.avatar).currentLocation.characters)
      {
        if (!(character is Monster) && !(character is StardewDruid.Character.Character) && (double) Vector2.Distance(((StardewValley.Character) character).Position, ((StardewValley.Character) this.avatar).Position) < 740.0 && !Mod.instance.WitnessedRite("ether", character) && character.isVillager())
        {
          if (!Mod.instance.TaskList().ContainsKey("masterTransform"))
            Mod.instance.UpdateTask("lessonTransform", 1);
          Reaction.ReactTo(character, "Ether");
        }
      }
      base.EventInterval();
    }
  }
}
