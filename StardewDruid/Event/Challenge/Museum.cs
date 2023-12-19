using Microsoft.Xna.Framework;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Map;
using StardewDruid.Monster;
using StardewValley;
using StardewValley.Locations;
using System;
using System.Collections.Generic;
using xTile.Layers;
using xTile.Tiles;

#nullable disable
namespace StardewDruid.Event.Challenge
{
    public class Museum : ChallengeHandle
    {
        public bool modifiedLocation;
        public BossDino bossMonster;
        public Vector2 returnPosition;
        public NPC Gunther;

        public Museum(Vector2 target, Rite rite, Quest quest)
          : base(target, rite, quest)
        {
            this.targetVector = target;
            this.voicePosition = Vector2.op_Addition(Vector2.op_Multiply(this.targetVector, 64f), new Vector2(0.0f, -32f));
            this.expireTime = Game1.currentGameTime.TotalGameTime.TotalSeconds + 60.0;
            this.returnPosition = ((Character)rite.caster).Position;
            this.Gunther = this.targetLocation.getCharacterFromName(nameof(Gunther));
        }

        public override void EventTrigger()
        {
            ModUtility.AnimateRadiusDecoration(this.targetLocation, this.targetVector, "Mists", 1f, 1f);
            ModUtility.AnimateBolt(this.targetLocation, this.targetVector);
            Mod.instance.RegisterEvent((EventHandle)this, "active");
            this.AddActor(this.voicePosition, true);
        }

        public override bool EventActive()
        {
            if (((Character)this.targetPlayer).currentLocation == this.targetLocation && !this.eventAbort)
            {
                double totalSeconds = Game1.currentGameTime.TotalGameTime.TotalSeconds;
                if (this.expireTime < totalSeconds || this.expireEarly)
                    return this.EventExpire();
                int num = (int)Math.Round(this.expireTime - totalSeconds);
                if (this.activeCounter != 0 && num % 10 == 0 && num != 0)
                    Game1.addHUDMessage(new HUDMessage(string.Format("{0} more minutes left!", (object)num), "2"));
                return true;
            }
            this.EventAbort();
            return false;
        }

        public override void RemoveMonsters()
        {
            if (this.bossMonster != null)
            {
                this.riteData.castLocation.characters.Remove((NPC)this.bossMonster);
                this.bossMonster = (BossDino)null;
            }
            base.RemoveMonsters();
        }

        public override void EventRemove()
        {
            this.ResetLocation();
            base.EventRemove();
        }

        public override bool EventExpire()
        {
            if (this.eventLinger == -1)
            {
                this.RemoveMonsters();
                this.ResetLocation();
                this.eventLinger = 4;
                return true;
            }
            if (this.eventLinger == 3)
            {
                if (this.expireEarly)
                {
                    this.GuntherVoice("A glorious battle");
                    Vector2 vector2 = Vector2.op_Addition(((Character)this.Gunther).getTileLocation(), new Vector2(0.0f, 1f));
                    if (!this.questData.name.Contains("Two"))
                        Game1.createObjectDebris(74, (int)vector2.X, (int)vector2.Y, -1, 0, 1f, (GameLocation)null);
                    Mod.instance.CompleteQuest(this.questData.name);
                }
                else
                {
                    this.GuntherVoice("ughh... what a mess");
                    Mod.instance.CastMessage("Try again tomorrow");
                }
            }
            return base.EventExpire();
        }

        public override void EventInterval()
        {
            ++this.activeCounter;
            if (this.eventLinger != -1)
                return;
            if (this.activeCounter < 6)
            {
                switch (this.activeCounter)
                {
                    case 1:
                        this.CastVoice("croak");
                        this.GuntherVoice("Farmer? What have you done?");
                        using (List<NPC>.Enumerator enumerator = this.targetLocation.characters.GetEnumerator())
                        {
                            while (enumerator.MoveNext())
                            {
                                NPC current = enumerator.Current;
                                if (current.isVillager() && ((Character)current).Name != "Gunther")
                                    ((Character)current).doEmote(8, true);
                            }
                            break;
                        }
                    case 3:
                        this.GuntherVoice("Oh... oh no no no");
                        break;
                    case 4:
                        this.CastVoice("CROAK");
                        break;
                    case 5:
                        this.GuntherVoice("Farmer! Protect the library!");
                        break;
                }
                Vector2 vector2 = Vector2.op_Subtraction(Vector2.op_Addition(this.targetVector, new Vector2(0.0f, 1f)), new Vector2((float)this.randomIndex.Next(7), (float)this.randomIndex.Next(3)));
                ModUtility.AnimateRadiusDecoration(this.targetLocation, vector2, "Mists", 1f, 1f);
                ModUtility.AnimateBolt(this.targetLocation, vector2);
            }
            else if (this.activeCounter == 6)
            {
                this.ModifyLocation();
                this.bossMonster = MonsterData.CreateMonster(14, Vector2.op_Addition(this.targetVector, new Vector2(2f, 0.0f)), this.riteData.combatModifier) as BossDino;
                if (this.questData.name.Contains("Two"))
                    this.bossMonster.HardMode();
                this.riteData.castLocation.characters.Add((NPC)this.bossMonster);
                ((Character)this.bossMonster).update(Game1.currentGameTime, this.riteData.castLocation);
                this.SetTrack("heavy");
            }
            else if (this.bossMonster.defeated || ((StardewValley.Monsters.Monster)this.bossMonster).Health <= 0 || this.bossMonster == null || !this.riteData.castLocation.characters.Contains((NPC)this.bossMonster))
            {
                this.expireEarly = true;
            }
            else
            {
                switch (this.activeCounter)
                {
                    case 10:
                        this.GuntherVoice("What have I got to throw here...");
                        this.GuntherThrowRandomShit();
                        break;
                    case 15:
                        this.GuntherVoice("Pre-cretacious creep!");
                        break;
                    case 20:
                        this.GuntherVoice("It's defacing my inlaid hardwood panelling!");
                        this.GuntherThrowRandomShit();
                        break;
                    case 25:
                        this.GuntherVoice("I need a weapon... but I loaned them all to Zuzu Mid");
                        break;
                    case 30:
                        this.GuntherVoice("Marlon has a lot to answer for");
                        this.GuntherThrowRandomShit();
                        break;
                    case 35:
                        this.GuntherVoice("Tell him I wont accept any more cursed artifacts");
                        break;
                    case 40:
                        this.GuntherVoice("Thank goodness we have a resident Druid");
                        this.GuntherThrowRandomShit();
                        break;
                    case 41:
                        ((NPC)this.bossMonster).showTextAboveHead("Stop throwing things at me old man!", -1, 2, 3000, 0);
                        this.bossMonster.dialogueTimer = 300;
                        break;
                    case 45:
                        this.GuntherVoice("Can't you perform a rite of banishment or something?");
                        break;
                    case 50:
                        this.GuntherVoice("Goodbye, priceless artifact. Trash anyway.");
                        this.GuntherThrowRandomShit();
                        break;
                    case 55:
                        this.GuntherVoice("Leave the corpse. I might be able to sell it's parts.");
                        break;
                    case 59:
                        this.GuntherVoice("This is going to cost the historic trust society");
                        this.GuntherThrowRandomShit();
                        this.expireEarly = true;
                        break;
                }
            }
        }

        public void GuntherVoice(string speech)
        {
            this.Gunther.showTextAboveHead(speech, 3000, 2, 3000, 0);
        }

        public void GuntherThrowRandomShit()
        {
            List<int> intList = new List<int>()
      {
        96,
        97,
        98,
        99,
        100,
        101,
        103,
        104,
        105,
        106,
        107,
        108,
        109,
        110,
        111,
        112,
        113,
        114,
        115,
        116,
        117,
        118,
        119,
        120,
        121,
        122,
        123,
        124,
        125,
        126,
        (int) sbyte.MaxValue,
        579,
        580,
        581,
        582,
        583,
        584,
        585,
        586,
        587,
        588,
        589
      };
            new Throw(this.targetPlayer, ((Character)this.bossMonster).Position, new Object(intList[this.riteData.randomIndex.Next(intList.Count)], 1, false, -1, 0), ((Character)this.Gunther).Position).AnimateObject();
        }

        public void ResetLocation()
        {
            if (!this.modifiedLocation)
                return;
            this.targetLocation.loadMap(((NetFieldBase<string, NetString>)this.targetLocation.mapPath).Value, true);
            foreach (NPC character in this.targetLocation.characters)
            {
                if (character.isVillager() && ((Character)character).Name != "Gunther" && character.IsInvisible)
                    character.IsInvisible = false;
            }
            if (Game1.eventUp || Game1.fadeToBlack || Game1.currentMinigame != null || Game1.isWarping || Game1.killScreen || !(((Character)Game1.player).currentLocation is LibraryMuseum))
                return;
            Game1.fadeScreenToBlack();
            ((Character)this.targetPlayer).Position = this.returnPosition;
            if (this.soundTrack)
            {
                Game1.stopMusicTrack((Game1.MusicContext)0);
                this.soundTrack = false;
            }
            this.modifiedLocation = false;
        }

        public void ModifyLocation()
        {
            this.modifiedLocation = true;
            this.targetLocation.temporarySprites.Clear();
            foreach (NPC character in this.targetLocation.characters)
            {
                if (character.isVillager() && ((Character)character).Name != "Gunther")
                    character.IsInvisible = true;
            }
            Layer layer1 = this.targetLocation.map.GetLayer("Back");
            Layer layer2 = this.targetLocation.map.GetLayer("Buildings");
            Layer layer3 = this.targetLocation.map.GetLayer("Front");
            Layer layer4 = this.targetLocation.map.GetLayer("AlwaysFront");
            TileSheet tileSheet = this.targetLocation.map.TileSheets[1];
            Vector2 vector2_1 = Vector2.op_Subtraction(this.targetVector, new Vector2(8f, 5f));
            for (int index1 = 0; index1 < 14; ++index1)
            {
                for (int index2 = 0; index2 < 13; ++index2)
                {
                    Vector2 vector2_2 = Vector2.op_Addition(vector2_1, new Vector2((float)index2, (float)index1));
                    if (layer2.Tiles[(int)vector2_2.X, (int)vector2_2.Y] != null)
                        layer2.Tiles[(int)vector2_2.X, (int)vector2_2.Y] = (Tile)null;
                    if (layer3.Tiles[(int)vector2_2.X, (int)vector2_2.Y] != null)
                        layer3.Tiles[(int)vector2_2.X, (int)vector2_2.Y] = (Tile)null;
                    if (layer4.Tiles[(int)vector2_2.X, (int)vector2_2.Y] != null)
                        layer4.Tiles[(int)vector2_2.X, (int)vector2_2.Y] = (Tile)null;
                    layer1.Tiles[(int)vector2_2.X, (int)vector2_2.Y] = this.randomIndex.Next(4) == 0 ? (this.randomIndex.Next(5) != 0 ? (this.randomIndex.Next(5) != 0 ? (Tile)new StaticTile(layer1, tileSheet, (BlendMode)0, 607) : (Tile)new StaticTile(layer1, tileSheet, (BlendMode)0, 606)) : (Tile)new StaticTile(layer1, tileSheet, (BlendMode)0, 639)) : (Tile)new StaticTile(layer1, tileSheet, (BlendMode)0, 638);
                }
            }
        }
    }
}
