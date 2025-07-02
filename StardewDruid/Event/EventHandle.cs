using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewDruid.Location;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Companions;
using StardewValley.GameData;
using StardewValley.Menus;
using StardewValley.Projectiles;
using StardewValley.Quests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace StardewDruid.Event
{
    public class EventHandle
    {

        public string eventId = "none";

        public GameLocation location;

        public Vector2 origin = Vector2.Zero;

        public bool inabsentia;

        public List<string> locales = new();

        public int costCounter = 0;

        // ----------------- trigger management

        public bool triggerEvent;

        public bool triggerActive;

        public bool triggerAbort;

        public int triggerCounter;

        // ------------------ event management

        public enum actionButtons
        {
            none,
            action,
            special,
            rite,
            shift,
            warp,
            favourite,
        }

        public int eventCounter;

        public bool eventActive;

        public bool eventComplete;

        public bool mainEvent = false;

        public int activeCounter;

        public int decimalCounter;

        public int activeLimit = 60;

        public bool eventAbort;

        public int proximityCounter = 0;

        public bool eventLocked;

        public int eventRating;

        public List<actionButtons> clicks = new();

        public List<EventRender> eventRenders = new();

        public IconData.skies channel = IconData.skies.none;

        // ------------------- event entities

        public MonsterHandle monsterHandle;

        public Dictionary<int, StardewDruid.Character.Actor> actors = new();

        public Dictionary<int, StardewDruid.Character.Character> companions = new();

        public Dictionary<int, Character.Character.mode> companionModes = new();

        public List<TemporaryAnimatedSprite> animations = new();

        public Dictionary<int, Dictionary<int, string>> cues = new();

        public Dictionary<int, string> narrators = new();

        public Dictionary<int, NPC> voices = new();

        public Dictionary<string, int> dialogueLoader = new();

        public Dictionary<int, StardewDruid.Monster.Boss> bosses = new();

        public Dictionary<int, DialogueSpecial> conversations = new();

        public bool soundTrack;

        // ------------------------------------

        public EventHandle()
        {

        }

        public virtual void EventSetup(string id)
        {

            origin = Mod.instance.questHandle.quests[id].origin;

            eventId = id;

            triggerEvent = Mod.instance.questHandle.quests[id].trigger;

            locales = new() { Mod.instance.questHandle.quests[eventId].triggerLocation, };

            Mod.instance.RegisterEvent(this, eventId);

            SetupNarration(eventId);

            SetupCompanion();

        }

        public virtual void EventSetup(Vector2 target, string id, bool trigger = false)
        {

            origin = target;

            eventId = id;
            
            triggerEvent = trigger;

            if(locales.Count == 0)
            {

                locales = new() { Game1.player.currentLocation.Name, };

            }

            Mod.instance.RegisterEvent(this, eventId);

            SetupNarration(eventId);

            SetupCompanion();

        }

        public virtual void EventSetup(GameLocation Location, Vector2 target, string id)
        {

            origin = target;

            eventId = id;

            inabsentia = true;

            locales = new() { Location.Name, };

            location = Location;

            Mod.instance.RegisterEvent(this, eventId);

        }

        public virtual void SetupCompanion()
        {



        }

        public virtual void SetupNarration(string Id)
        {

            narrators = NarratorData.DialogueNarrators(Id);

            cues = DialogueData.DialogueScene(Id);

            conversations = ConversationData.SceneConversations(Id);

        }

        public virtual void EventDraw(SpriteBatch b)
        {

            if(eventRenders.Count == 0)
            {

                return;

            }

            foreach (EventRender er in eventRenders)
            {

                er.draw(b);

            }

        }

        // ------------------------------------

        public virtual bool TriggerActive()
        {

            if (!TriggerLocation())
            {

                if (triggerActive)
                {

                    triggerActive = false;

                    TriggerRemove();

                }

                return false;

            }

            if (!triggerActive)
            {

                location = Game1.player.currentLocation;

                triggerActive = true;

                TriggerField();

            }

            return true;

        }

        public virtual void TriggerField()
        {

            EventRender triggerField = new(eventId, location.Name, origin, Mod.instance.iconData.riteDisplays[Mod.instance.questHandle.quests[eventId].triggerRite]);

            eventRenders.Add(triggerField);

        }

        public virtual bool TriggerLocation()
        {

            if (!locales.Contains(Game1.player.currentLocation.Name))
            {

                return false;

            }

            return true;

        }

        public virtual bool TriggerCheck()
        {

            if (!triggerActive)
            {

                return false;

            }

            if (Vector2.Distance(Game1.player.Position, origin) > (6 * 64))
            {

                return false;

            }

            if (Mod.instance.questHandle.quests[eventId].triggerRite != Rite.Rites.none)
            {

                if (Mod.instance.rite.castType != Mod.instance.questHandle.quests[eventId].triggerRite)
                {

                    return false;

                }

            }

            if (Mod.instance.questHandle.quests[eventId].triggerTime != 0)
            {

                if (Game1.timeOfDay < Mod.instance.questHandle.quests[eventId].triggerTime)
                {

                    Mod.instance.CastDisplay(StringData.Strings(StringData.stringkeys.returnLater));

                    return false;

                }

            }

            EventActivate();

            return true;

        }

        public virtual void TriggerRemove()
        {

            RemoveActors();

            RemoveAnimations();

            if (soundTrack)
            {

                Game1.stopMusicTrack(MusicContext.Default);

            }

            triggerActive = false;

            triggerCounter = 0;

            eventRenders.Clear();

            location = null;

        }

        public virtual bool TriggerAbort()
        {

            return triggerAbort;

        }

        public virtual void TriggerInterval()
        {

            triggerCounter++;

        }

        public virtual void ResetToTrigger()
        {

            EventRemove();

            eventActive = false;

        }

        public virtual void SendFriendsHome()
        {

            if (Mod.instance.eventRegister.ContainsKey(Rite.eventCorvids))
            {

                if(Mod.instance.eventRegister[Rite.eventCorvids] is Cast.Bones.Corvids corvids)
                {

                    Cast.Bones.Corvids.RemoveCorvids();

                    corvids.EventRemove();

                    Mod.instance.eventRegister.Remove(Rite.eventCorvids);

                }

            }

            for (int t = Mod.instance.trackers.Count - 1; t >= 0; t--)
            {

                TrackHandle tracker = Mod.instance.trackers.ElementAt(t).Value;

                if (Mod.instance.characters[tracker.trackFor].modeActive == Character.Character.mode.recruit)
                {

                    RecruitHandle.RecruitRemove(tracker.trackFor);

                }
                else
                {

                    Mod.instance.characters[tracker.trackFor].SwitchToMode(Character.Character.mode.home, Game1.player);


                }

            }

        }

        // ------------------------------------

        public virtual void EventActivate()
        {

            if (triggerActive)
            {

                TriggerRemove();

            }

            Mod.instance.RegisterEvent(this, eventId, mainEvent);

            eventActive = true;

            eventAbort = false;

            eventCounter = 0;

            activeCounter = 0;

            decimalCounter = 0;

            if (!inabsentia || location == null)
            {

                location = Game1.player.currentLocation;

            }

        }

        public virtual void EventClicks(actionButtons button)
        {

            if (!Mod.instance.clickRegister.ContainsKey(button))
            {

                Mod.instance.clickRegister[button] = new();

            }

            Mod.instance.clickRegister[button].Add(eventId);
            
            clicks.Add(button);

        }

        public virtual EventDisplay EventBar(string Title, int Id)
        {

            EventDisplay bar = Mod.instance.CastDisplay(Title,Title);

            bar.uniqueId = Id;

            bar.eventId = eventId;

            bar.type = EventDisplay.displayTypes.bar;

            bar.colour = Color.Blue;

            return bar;

        }

        public virtual EventDisplay BossBar(int bossId, int narratorId)
        {

            EventDisplay bar = Mod.instance.CastDisplay(narrators[narratorId], narrators[narratorId]);

            bar.boss = bossId;

            bar.eventId = eventId;

            bar.type = EventDisplay.displayTypes.bar;

            bar.colour = Microsoft.Xna.Framework.Color.Red;

            return bar;

        }

        public virtual void EventTimer()
        {

            eventCounter++;

        }

        public virtual bool EventActive()
        {

            if (eventAbort)
            {

                return AttemptReset();

            }
            
            if (!inabsentia)
            {

                if (!EventLocation())
                {

                    return AttemptReset();

                }

            }

            if (eventComplete)
            {

                return EventComplete();

            }

            if (activeLimit != -1 && eventCounter > activeLimit)
            {

                return EventExpire();

            }

            return true;

        }

        public virtual bool AttemptReset()
        {

            return false;

        }

        public virtual bool EventComplete()
        {

            return false;

        }

        public virtual bool EventExpire()
        {

            return false;

        }

        public virtual void EventRemove()
        {

            RemoveMonsters();

            RemoveActors();

            RemoveAnimations();

            RemoveClicks();

            StopTrack();

            eventRenders.Clear();

            if(channel != IconData.skies.none)
            {

                Mod.instance.rite.ChannelShutdown(channel);

            }

            foreach (string local in locales)
            {

                if (Mod.instance.locations.ContainsKey(local))
                {

                    Mod.instance.locations[local].updateWarps();

                }

            }

            if (Mod.instance.activeEvent.ContainsKey(eventId))
            {

                Mod.instance.activeEvent.Remove(eventId);

            }

            if (eventComplete)
            {

                EventCompleted();

            }

        }

        public virtual bool EventLocation()
        {

            if(locales.Count > 0)
            {

                if (locales.Contains(Game1.player.currentLocation.Name))
                {

                    return true;

                }

            }

            if (location.Name != Game1.player.currentLocation.Name)
            {

                return false;

            }

            return true;

        }

        public virtual bool EventPerformAction(SButton Button, actionButtons Action = actionButtons.action)
        {

            return false;

        }

        public virtual void EventDecimal()
        {


        }

        public virtual void EventInterval()
        {

            activeCounter++;

        }

        // ------------------------------------ entities
        
        public void CastVoice(int id, string message, int duration = 2000)
        {

            NPC speaker;

            if(Mod.instance.Config.dialogueOption == ModData.dialogueOptions.none.ToString())
            {
                return;
            }
            
            if (voices.ContainsKey(id))
            {

                speaker = voices[id];

            } else if (companions.ContainsKey(id))
            {

                speaker = companions[id];

            } else if (actors.ContainsKey(id))
            {

                speaker = actors[id];

            } else if (id == 0)
            {

                AddActor(0, origin);

                speaker = actors[0];

            }
            else
            {

                return;
            }
   
            if (message == "...")
            {
                speaker.doEmote(40);

                return;
            }
            else if (message == "!")
            {

                speaker.doEmote(16);

                return;
            }
            else if (message == "?")
            {

                speaker.doEmote(8);

                return;
            }
            else if (message == "x")
            {

                speaker.doEmote(36);

                return;
            }

            if(Mod.instance.Config.dialogueOption == ModData.dialogueOptions.emotes.ToString())
            {

                speaker.doEmote(40);

                return;

            }

            speaker.showTextAboveHead(message, duration: duration);

        }

        public virtual void HoldCompanions(int idle = 300)
        {

            foreach (KeyValuePair<CharacterHandle.characters, TrackHandle> tracker in Mod.instance.trackers)
            {

                Mod.instance.characters[tracker.Key].ResetActives();

                Mod.instance.characters[tracker.Key].netIdle.Set((int)Character.Character.idles.alert);

                Mod.instance.characters[tracker.Key].idleTimer = idle;

            }

        }

        public void SetTrack(string track)
        {

            Game1.stopMusicTrack(MusicContext.Default);

            Game1.changeMusicTrack(track, false, MusicContext.MusicPlayer);

            soundTrack = true;

        }

        public void AddActor(int id, Vector2 position, bool slave = false)
        {

            if (actors.ContainsKey(id))
            {

                actors[id].Position = position;

                return;

            }

            Actor actor = new Actor(CharacterHandle.characters.disembodied);

            actor.SwitchToMode(Character.Character.mode.scene,Game1.player);

            actor.collidesWithOtherCharacters.Value = true;
            
            actor.farmerPassesThrough = true;

            actor.drawSlave = slave;

            actor.Position = position;

            actor.currentLocation = location;

            location.characters.Add(actor);

            actors.Add(id,actor);

            voices[id] = actor;

        }

        public virtual void EventScene(int index)
        {
            
        }

        public void LoadBoss(StardewDruid.Monster.Boss boss, int index = 0, int mode = 2, int voice = -1)
        {

            bosses[index] = boss;

            boss.SetMode(mode);

            boss.netPosturing.Set(true);

            location.characters.Add(boss);

            boss.currentLocation = location;

            boss.update(Game1.currentGameTime, location);

            if(voice != -1)
            {

                voices[voice] = boss;

                EventDisplay bossBar = BossBar(index,voice);

                bossBar.colour = Microsoft.Xna.Framework.Color.LightCoral;

            }

        }

        // ------------------------------------ dialogue

        public virtual float DisplayProgress(int displayId = 0)
        {

            if (!eventActive)
            {

                return -1f;

            }

            if (activeCounter == 0)
            {

                return 0f;

            }

            float progress = 0f;

            switch (displayId)
            {

                case 0:

                    progress = (float)activeCounter / (float)activeLimit;

                    break;

                default:

                    progress = SpecialProgress(displayId);

                    break;


            }

            return progress;

        }

        public virtual float SpecialProgress(int displayId)
        {

            return -1f;

        }

        public virtual void DialogueCue(int cueIndex = -1)
        {

            if (cues.ContainsKey(cueIndex))
            {

                foreach (KeyValuePair<int, string> cue in cues[cueIndex])
                {

                    if(cue.Key == 999)
                    {

                        Mod.instance.CastDisplay(cue.Value,0);

                        if (Context.IsMultiplayer)
                        {

                            QueryData queryData = new()
                            {

                                name = "999",

                                value = cue.Value,

                                location = location.Name,

                            };

                            Mod.instance.EventQuery(queryData, QueryData.queries.EventDisplay);

                        }

                        continue;

                    }

                    if (Game1.viewport.Height > 960)
                    {

                        CastVoice(cue.Key, cue.Value, 3000);

                    }
                    else
                    {
                        if(cue.Value.Length > 4 && Mod.instance.Config.dialogueOption != ModData.dialogueOptions.fulltext.ToString())
                        {

                            CastVoice(cue.Key, "...", 2000);

                        }
                        else
                        {

                            CastVoice(cue.Key, cue.Value, 3000);

                        }

                    }

                    if (voices.ContainsKey(cue.Key) && cue.Value.Length > 4)
                    {

                        Texture2D cuePortrait = null;

                        Rectangle cueSource = Rectangle.Empty;

                        string speakerName = string.Empty;

                        if (voices[cue.Key] is StardewDruid.Monster.Boss bossMonster)
                        {

                            if (!ModUtility.MonsterVitals(bossMonster,location))
                            {

                                voices.Remove(cue.Key);

                                return;

                            }

                            cuePortrait = bossMonster.OverheadTexture();

                            cueSource = bossMonster.OverheadPortrait();

                            speakerName = bossMonster.Name;

                        }
                        else if(voices[cue.Key] is StardewDruid.Character.Character charSpeaker)
                        {

                            cuePortrait = charSpeaker.OverheadTexture();

                            cueSource = charSpeaker.OverheadPortrait();

                            speakerName = charSpeaker.Name;

                        }

                        string cueTitle = voices[cue.Key].Name;  
                        
                        if(narrators.ContainsKey(cue.Key))
                        {

                            cueTitle = narrators[cue.Key];

                        }

                        if (Mod.instance.Config.captionOption != ModData.captionOptions.none.ToString())
                        {

                            EventDisplay cueDisplay = Mod.instance.CastDisplay(cueTitle, cue.Value);

                            if (Mod.instance.Config.captionOption == ModData.captionOptions.auto.ToString() && cuePortrait != null)
                            {

                                cueDisplay.portrait = cuePortrait;

                                cueDisplay.portraitSource = cueSource;

                            }

                        }

                        if (Context.IsMultiplayer)
                        {
                            
                            QueryData queryData = new()
                            {
                                
                                name = speakerName != string.Empty ? speakerName : null,

                                value = cue.Value,

                                location = location.Name,

                                description = cueTitle,

                            };

                            Mod.instance.EventQuery(queryData, QueryData.queries.EventDisplay);

                        }

                    }

                }

            }

        }

        public void DialogueCueWithFeeling(int cueIndex, int frame = 1, Character.Character.specials special = Character.Character.specials.point)
        {

            if (cues.ContainsKey(cueIndex))
            {

                foreach (KeyValuePair<int, string> cue in cues[cueIndex])
                {

                    if (voices.ContainsKey(cue.Key))
                    {

                        if (voices[cue.Key] is StardewDruid.Character.Character companion)
                        {

                            companion.netSpecial.Set((int)special);

                            companion.specialFrame = frame;

                            companion.specialTimer =60;

                        }
                        else if (voices[cue.Key] is StardewDruid.Monster.Boss boss)
                        {

                            boss.netSpecialActive.Set(true);

                            boss.specialFrame = frame;

                            boss.specialTimer = boss.specialInterval;

                        }

                    }

                }

            }

            DialogueCue(cueIndex);

        }

        public void BossesAddressPlayer()
        {

            foreach(KeyValuePair<int,StardewDruid.Monster.Boss> boss in bosses)
            {

                boss.Value.LookAtFarmer();

            }

        }

        public void DialogueCueWithThreat(int cueIndex)
        {

            if (cues.ContainsKey(cueIndex))
            {

                foreach (KeyValuePair<int, string> cue in cues[cueIndex])
                {

                    if (voices.ContainsKey(cue.Key))
                    {

                        if (voices[cue.Key] is StardewDruid.Character.Character companion)
                        {

                            companion.netIdle.Set((int)Character.Character.idles.alert);

                        }
                        else if (voices[cue.Key] is StardewDruid.Monster.Boss boss)
                        {

                            boss.netAlert.Set(true);

                            boss.idleTimer = 180;

                        }

                    }

                }

            }


            DialogueCue(cueIndex);

        }

        public virtual void DialogueLoad(StardewDruid.Character.Character npc,int dialogueId)
        {

            dialogueLoader[npc.Name] = dialogueId;

            return;

        }

        public virtual void DialogueLoad(int companionId, int dialogueId)
        {

            if (!companions.ContainsKey(companionId))
            {

                return;

            }

            dialogueLoader[companions[companionId].Name] = dialogueId;

            return;

        }

        public virtual bool DialogueNext(StardewDruid.Character.Character npc)
        {

            if (dialogueLoader.ContainsKey(npc.Name))
            {

                DialogueSetups(npc, dialogueLoader[npc.Name]);

                dialogueLoader.Remove(npc.Name);

                return true;

            }

            return false;

        }

        public virtual void DialogueClear(StardewDruid.Character.Character npc)
        {

            if (dialogueLoader.ContainsKey(npc.Name))
            {

                dialogueLoader.Remove(npc.Name);

            }

        }

        public virtual void DialogueClear(int companionId)
        {

            if (!companions.ContainsKey(companionId))
            {

                return;

            }

            DialogueClear(companions[companionId]);

        }

        public virtual void DialogueSetups(StardewDruid.Character.Character npc, int dialogueId)
        {

            if (!conversations.ContainsKey(dialogueId))
            {

                return;

            }

            string intro = conversations[dialogueId].intro;

            List<Response> responseList = new();

            for(int r = 0; r < conversations[dialogueId].responses.Count; r++)
            {

                string response = conversations[dialogueId].responses[r];

                responseList.Add(new Response(eventId + "." + dialogueId.ToString() + "." + r.ToString(), response));

            }

            responseList.First().SetHotKey(Microsoft.Xna.Framework.Input.Keys.Enter);

            if(conversations[dialogueId].responses.Count < 3)
            {

                responseList.Add(new Response(eventId + "." + dialogueId.ToString() + ".999", Mod.instance.Helper.Translation.Get("DialogueData.Nothing")));

            }

            responseList.Last().SetHotKey(Microsoft.Xna.Framework.Input.Keys.Escape);

            GameLocation.afterQuestionBehavior questionBehavior = new(DialogueResponses);

            Game1.player.currentLocation.createQuestionDialogue(intro, responseList.ToArray(), questionBehavior, npc);


            if (Context.IsMultiplayer)
            {

                QueryData queryData = new()
                {

                    name = npc != null ? npc.Name : null,

                    value = intro,

                    location = location.Name,

                    description = string.Join("|", conversations[dialogueId].responses) 

                };

                Mod.instance.EventQuery(queryData, QueryData.queries.EventQuestion);

            }

            return;

        }

        public virtual void DialogueResponses(Farmer visitor, string dialogueId)
        {

            List<string> dialogueIndexes = new(dialogueId.Split("."));

            int dialogueIndex = Convert.ToInt32(dialogueIndexes[1]);

            int answerIndex = Convert.ToInt32(dialogueIndexes[2]);

            if(conversations[dialogueIndex].questContext != 0)
            {

                activeCounter = Math.Max(conversations[dialogueIndex].questContext, activeCounter);

            }

            if(companions.ContainsKey(conversations[dialogueIndex].companion))
            {

                StardewValley.NPC npc = companions[conversations[dialogueIndex].companion];

                if (conversations[dialogueIndex].answers.Count > answerIndex)
                {

                    DialogueDraw(npc, conversations[dialogueIndex].answers[answerIndex]);

                }
                else if (conversations[dialogueIndex].answers.Count > 0)
                {

                    DialogueDraw(npc, conversations[dialogueIndex].answers.First());

                }

            }

        }

        public virtual void DialogueDraw(NPC npc, string text)
        {

            Game1.currentSpeaker = npc;

            StardewValley.Dialogue dialogue = new(npc, "0", text);

            Game1.activeClickableMenu = new DialogueBox(dialogue);

            Game1.player.Halt();

            Game1.player.CanMove = false;

            if (Context.IsMultiplayer)
            {

                QueryData queryData = new()
                {

                    name = npc != null ? npc.Name : null,

                    value = text,

                    location = npc.currentLocation.Name,

                };

                Mod.instance.EventQuery(queryData, QueryData.queries.EventDialogue);

            }

        }

        // ---------------------------------- clean up

        public virtual void EventCompleted()
        {

            if (Mod.instance.questHandle.quests.ContainsKey(eventId))
            {

                if (Mod.instance.questHandle.quests[eventId].type != Data.Quest.questTypes.lesson)
                {

                    Mod.instance.questHandle.CompleteQuest(eventId,eventRating);

                }

            }

        }

        public virtual void RemoveMonsters()
        {

            for (int b = bosses.Count - 1; b >= 0; b--)
            {

                Boss boss = bosses.ElementAt(b).Value;

                Mod.instance.iconData.AnimateQuickWarp(location, boss.Position, true, boss.warp);

                location.characters.Remove(boss);

            }

            bosses.Clear();

            if (monsterHandle != null)
            {

                monsterHandle.ShutDown();

            }

        }

        public virtual void RemoveSummons()
        {

            List<string> summons = new()
            {
                Rite.eventCorvids,
                Rite.eventWisps,

            };

            foreach(string riteSummon in summons)
            {

                if (Mod.instance.eventRegister.ContainsKey(riteSummon))
                {

                    Mod.instance.eventRegister[riteSummon].eventAbort = true;

                }

            }

            Cast.Bones.Corvids.RemoveCorvids();

        }

        public virtual void RemoveActors()
        {

            if (actors.Count > 0)
            {

                foreach (KeyValuePair<int, StardewDruid.Character.Actor> actor in actors)
                {

                    actor.Value.currentLocation.characters.Remove(actor.Value);

                }

                actors.Clear();

            }

            if (companions.Count > 0)
            {

                foreach (KeyValuePair<int,StardewDruid.Character.Character> companion in companions)
                {

                    RemoveCompanion(companion.Key);

                }

                companions.Clear();

                companionModes.Clear();

            }

            voices.Clear();

        }

        public virtual void RemoveCompanion(int companionIndex)
        {

            if (!companions.ContainsKey(companionIndex))
            {

                return;

            }

            StardewDruid.Character.Character companion = companions[companionIndex];

            if (Mod.instance.characters.ContainsKey(companion.characterType))
            {

                companion.eventName = null;

                if (companionModes.ContainsKey(companionIndex))
                {

                    companion.SwitchToMode(companionModes[companionIndex], Game1.player);

                    return;

                }
                else if(CharacterHandle.CharacterSave(companion.characterType))
                {

                    companion.SwitchToMode(Character.Character.mode.random, Game1.player);

                    return;

                }

            }

            if(companion.currentLocation != null)
            {

                if(companion.currentLocation == Game1.player.currentLocation)
                {

                    Mod.instance.iconData.AnimateQuickWarp(companion.currentLocation, companion.Position, true);

                }

                companion.currentLocation.characters.Remove(companion);

            }

        }

        public virtual void RemoveAnimations()
        {

            if (animations.Count > 0)
            {

                foreach (TemporaryAnimatedSprite animation in animations)
                {
                    if (animation.lightId != null)
                    {

                        Utility.removeLightSource(animation.lightId);

                    }
                    location.temporarySprites.Remove(animation);

                }

                animations.Clear();

            }

        }

        public virtual void RemoveClicks()
        {

            foreach(actionButtons button in clicks)
            {

                if (Mod.instance.clickRegister[button].Contains(eventId))
                {

                    Mod.instance.clickRegister[button].Remove(eventId);

                    if(Mod.instance.clickRegister[button].Count == 0)
                    {

                        Mod.instance.clickRegister.Remove(button);

                    }

                }

            }

            clicks.Clear();

        }

        public virtual void StopTrack()
        {

            if (soundTrack)
            {

                Game1.stopMusicTrack(MusicContext.MusicPlayer);

                soundTrack = false;

            }

        }


    }

}
