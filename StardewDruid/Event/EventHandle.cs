using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Cast;
using StardewDruid.Cast.Witch;
using StardewDruid.Character;
using StardewDruid.Data;
using StardewDruid.Dialogue;
using StardewDruid.Handle;
using StardewDruid.Location;
using StardewDruid.Monster;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Buildings;
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

        public string eventTitle = "none";

        public GameLocation location;

        public Vector2 origin = Vector2.Zero;

        public List<string> locales = new();

        public int costCounter = 0;

        public enum abortBehaviour
        {
            ignore,
            reset,
            abort,
            stall,
        }

        //public abortBehaviour pauseAbort = abortBehaviour.abort;

        public abortBehaviour locationAbort = abortBehaviour.abort;

        public abortBehaviour healthAbort = abortBehaviour.ignore;

        public bool stalled;

        // ----------------- local management

        public bool localEvent;

        public bool localActive;

        // ----------------- trigger management

        public bool triggerEvent;

        public bool triggerActive;

        public Rite.Rites triggerRite = Rite.Rites.none;

        public int triggerTime;

        // ------------------ event management

        public enum actionButtons
        {
            none,
            action,
            special,
            rite,
            warp,
            favourite,
        }

        public bool eventActive;

        public bool eventComplete;

        public bool mainEvent = false;

        public int activeScene;

        public int sceneLimit;

        public int activeCounter;

        public int activeLimit;

        public int decimalCounter;

        public int proximityCounter = 0;

        public int eventRating;

        public List<actionButtons> clicks = new();

        public Dictionary<string, EventRender> eventRenders = new();

        public Dictionary<int, Vector2> sceneVectors = new();

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

        public virtual void EventSetup(Farmer player, Vector2 target, string id)
        {

            origin = target;

            eventId = id;

            locales = new() { player.currentLocation.Name, };

            location = player.currentLocation;

            Mod.instance.RegisterEvent(this, eventId);

        }

        public virtual void EventSetup(string id)
        {

            if(origin == Vector2.Zero)
            {

                origin = Mod.instance.questHandle.quests[id].origin;

            }

            eventId = id;

            eventTitle = Mod.instance.questHandle.quests[id].title;

            triggerEvent = Mod.instance.questHandle.quests[id].trigger;

            triggerRite = Mod.instance.questHandle.quests[id].triggerRite;

            triggerTime = Mod.instance.questHandle.quests[id].triggerTime;

            localEvent = Mod.instance.questHandle.quests[id].local;

            locales = new() { Mod.instance.questHandle.quests[eventId].triggerLocation, };

            //pauseAbort = Mod.instance.questHandle.quests[id].pauseAbort;

            locationAbort = Mod.instance.questHandle.quests[id].locationAbort;

            healthAbort = Mod.instance.questHandle.quests[id].healthAbort;

            Mod.instance.RegisterEvent(this, eventId);

            SetupNarration(eventId);

        }

        public virtual void EventSetup(Vector2 target, string id, List<string> Locations, bool trigger, bool local)
        {

            if (origin == Vector2.Zero)
            {

                origin = target;

            }

            eventId = id;

            locales = Locations;

            triggerEvent = trigger;

            localEvent = local;

            Mod.instance.RegisterEvent(this, eventId);

            SetupNarration(eventId);

        }

        public virtual void EventSetup(GameLocation Location, Vector2 target, string id)
        {

            origin = target;

            eventId = id;

            locationAbort = abortBehaviour.ignore;

            locales = new() { Location.Name, };

            location = Location;

            Mod.instance.RegisterEvent(this, eventId);

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

            foreach (KeyValuePair<string,EventRender> er in eventRenders)
            {

                er.Value.draw(b);

            }

        }

        // ------------------------------------
        
        public virtual bool LocalUpdate()
        {

            if (TriggerLocation())
            {

                EventActivate();

                return true;

            }

            return false;

        }

        // ------------------------------------


        public virtual void TriggerAbort()
        {

            if (triggerActive)
            {

                TriggerRemove();

            }

        }

        public virtual bool TriggerUpdate()
        {

            if (TriggerLocation())
            {

                if (!triggerActive)
                {

                    TriggerCreate();

                }

                return true;

            }

            if (triggerActive)
            {

                TriggerRemove();

            }

            return true;

        }

        public virtual bool TriggerLocation()
        {

            return locales.Contains(Game1.player.currentLocation.Name);

        }

        public virtual void TriggerCreate()
        {

            triggerActive = true;

            Mod.instance.triggerRegister[eventId] = true;

            EventRender triggerField = new(eventId, Game1.player.currentLocation.Name, origin, Mod.instance.iconData.riteDisplays[triggerRite]);

            eventRenders.Add(eventId + "_trigger", triggerField);

            location = Game1.player.currentLocation;

        }
        
        public virtual void TriggerRemove()
        {

            triggerActive = false;

            Mod.instance.triggerRegister.Remove(eventId);

            eventRenders.Clear();

            location = null;

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

            if (triggerTime != 0)
            {

                if (Game1.timeOfDay < triggerTime)
                {

                    Mod.instance.RegisterDisplay(StringData.Strings(StringData.stringkeys.returnLater));

                    return false;

                }

            }

            if (triggerRite != Rite.Rites.none)
            {

                if (Mod.instance.rite.appliedBuff != triggerRite)
                {

                    return false;

                }

            }

            EventActivate();

            return true;

        }

        public virtual void SendFriendsHome()
        {

            CorvidHandle.RemoveCorvids();

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

            activeScene = 0;

            activeCounter = 0;

            decimalCounter = 0;

            if (location == null)
            {

                location = Game1.player.currentLocation;

            }

        }

        public virtual void EventReset()
        {

            EventRemove();

            Mod.instance.activeEvent.Remove(eventId);

            eventActive = false;

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

        public virtual EventBar SceneBar(string Title, int Id)
        {

            EventBar bar = Mod.instance.RegisterBar(Title, EventBar.barTypes.scene, Id);

            bar.eventId = eventId;

            bar.colour = Color.LightGreen;

            return bar;

        }

        public virtual EventBar ProgressBar(string Title, int Id)
        {

            EventBar bar = Mod.instance.RegisterBar(Title, EventBar.barTypes.progress, Id);

            bar.eventId = eventId;

            bar.colour = Color.LightBlue;

            return bar;

        }

        public virtual EventBar BossBar(int bossId, int narratorId)
        {

            EventBar bar = Mod.instance.RegisterBar(narrators[narratorId], EventBar.barTypes.boss, bossId);

            bar.eventId = eventId;

            bar.colour = Microsoft.Xna.Framework.Color.LightCoral;

            return bar;

        }

        public virtual bool EventActive()
        {

            if (eventComplete)
            {

                return false;

            }

            bool stallCheck = false;

            if (!EventLocation())
            {

                switch (locationAbort)
                {

                    case abortBehaviour.ignore:

                        break;

                    case abortBehaviour.reset:

                        OnLocationReset();

                        break;

                    case abortBehaviour.abort:

                        OnLocationAbort();

                        return false;

                    case abortBehaviour.stall:

                        if (!stalled)
                        {

                            OnStall();

                        }

                        stallCheck = true;

                        break;

                }

            }

            if(Game1.player.health <= 7)
            {

                switch (healthAbort)
                {

                    case abortBehaviour.ignore:

                        break;

                    case abortBehaviour.reset:

                        OnHealthReset();

                        break;

                    case abortBehaviour.abort:

                        OnHealthAbort();

                        return false;

                    case abortBehaviour.stall:

                        if (!stalled)
                        {

                            OnStall();

                        }

                        stallCheck = true;

                        break;
                }

            }

            if(stalled && !stallCheck)
            {

                OnResume();

            }

            return true;

        }

        public virtual void OnPauseReset()
        {
            
            EventReset();

        }

        public virtual void OnPauseAbort()
        {

        }

        public virtual void OnLocationReset()
        {

            EventReset();

        }

        public virtual void OnLocationAbort()
        {

        }

        public virtual void OnHealthReset()
        {

            EventReset();

        }

        public virtual void OnHealthAbort()
        {

        }

        public virtual void OnStall()
        {

            stalled = true;

        }

        public virtual void OnResume()
        {

            stalled = false;

        }

        public virtual void EventRemove()
        {

            RemoveMonsters();

            RemoveActors();

            RemoveAnimations();

            RemoveClicks();

            StopTrack();

            eventRenders.Clear();

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

            LeaveLocations();

            Mod.instance.RemoveBars(eventId);

            Mod.instance.RemoveDisplays(eventId);

            stalled = false;

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

        public virtual bool EventPerformAction(actionButtons Action = actionButtons.action)
        {

            return false;

        }

        public virtual void EventDecimal()
        {

        }

        public virtual void EventInterval()
        {

        }

        public virtual void NextScene(int index = -1)
        {

            activeCounter = 0;

            switch (index)
            {

                case -1:

                    activeScene++;

                    break;

                default:

                    activeScene = index;

                    break;

            }

        }

        public virtual void NextCounter(int index = -1)
        {

            switch (index)
            {

                case -1:

                    activeCounter++;

                    break;

                default:

                    activeCounter = index;

                    break;

            }

        }

        public virtual void SkipEvent(int index = -1)
        {



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

        public virtual void LoadCompanion(CharacterHandle.characters character, Vector2 companionPosition, int companionId = 0, int voiceId = 0, bool animateWarp = false)
        {

            CharacterHandle.CharacterLoad(character, StardewDruid.Character.Character.mode.scene);

            companions[companionId] = Mod.instance.characters[character];

            CharacterMover.Warp(location, companions[companionId], companionPosition, animateWarp, companions[companionId].warpDisplay);

            companions[companionId].LookAtTarget(Game1.player.Position, true);

            companions[companionId].eventName = eventId;

            if(voiceId != -1)
            {

                voices[voiceId] = companions[companionId];

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

                BossBar(index,voice);

            }

        }

        // ------------------------------------ dialogue
        public virtual float DisplayScene(int displayId = 0)
        {

            if (!eventActive)
            {

                return -1f;

            }


            switch (displayId)
            {

                default:

                    if (activeScene == 0)
                    {

                        return 0f;

                    }

                    return (float)activeScene / sceneLimit;

                case 1:

                    if (activeCounter == 0)
                    {

                        return 0f;

                    }

                    return (float)activeCounter / activeLimit;

            }

        }

        public virtual float DisplayProgress(int displayId)
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

                        Mod.instance.RegisterDisplay(cue.Value,0.ToString(),EventDisplay.displayTypes.strong);

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

                        bool cueFrame = false;

                        string speakerName = string.Empty;

                        if (voices[cue.Key] is StardewDruid.Monster.Boss bossMonster)
                        {

                            if (!ModUtility.MonsterVitals(bossMonster,location))
                            {

                                voices.Remove(cue.Key);

                                return;

                            }

                            cuePortrait = bossMonster.OverheadTexture();

                            cueFrame = bossMonster.OverheadFrame();

                            cueSource = bossMonster.OverheadPortrait();

                            speakerName = bossMonster.Name;

                        }
                        else if(voices[cue.Key] is StardewDruid.Character.Character charSpeaker)
                        {

                            cuePortrait = charSpeaker.OverheadTexture();

                            cueFrame = charSpeaker.OverheadFrame();

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

                            EventDisplay cueDisplay = Mod.instance.RegisterDisplay(cue.Value, cueTitle);

                            if (Mod.instance.Config.captionOption == ModData.captionOptions.auto.ToString() && cuePortrait != null)
                            {

                                cueDisplay.imageTexture = cuePortrait;

                                cueDisplay.imageFrame = cueFrame;

                                cueDisplay.imageSource = cueSource;

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

            Mod.instance.RemoveDisplays(eventId);

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

                    value = conversations[dialogueId].questId,

                    description = dialogueId.ToString(),

                    location = location.Name,

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

            DialogueSpecial currentConversation = conversations[dialogueIndex];

            if (currentConversation.updateScene != 999)
            {

                NextScene(currentConversation.updateScene);

            }

            if (currentConversation.updateCounter != 999)
            {

                NextCounter(currentConversation.updateCounter);

            }

            if (currentConversation.choices.Count > 0)
            {

                if (currentConversation.choices.ContainsKey(answerIndex) && currentConversation.dialogueId != null)
                {

                    Mod.instance.save.dialogues[currentConversation.dialogueId] = (int)currentConversation.choices[answerIndex];

                }

            }

            if (currentConversation.answers.Count == 0)
            {

                return;

            }
            
            if (companions.ContainsKey(currentConversation.companion))
            {

                StardewValley.NPC npc = companions[currentConversation.companion];

                if (currentConversation.answers.ContainsKey(answerIndex))
                {

                    DialogueDraw(npc, currentConversation.answers[answerIndex]);

                }
                else
                {

                    DialogueDraw(npc, currentConversation.answers.First().Value);

                }

            }
            else
            {

                if (currentConversation.answers.ContainsKey(answerIndex))
                {

                    DialogueDraw(null, currentConversation.answers[answerIndex]);

                }
                else
                {

                    DialogueDraw(null, currentConversation.answers.First().Value);

                }

            }
            

        }

        public virtual void DialogueDraw(NPC npc, string text)
        {

            Game1.currentSpeaker = npc;

            if (npc == null)
            {

                List<string> answers = ModUtility.SplitStringByLength(text, 185);

                Game1.activeClickableMenu = new DialogueBox(answers);

            }
            else
            {

                StardewValley.Dialogue dialogue = new(npc, "0", text);

                Game1.activeClickableMenu = new DialogueBox(dialogue);

            }

            Game1.player.Halt();

            Game1.player.CanMove = false;

        }

        public virtual void SetDialogueChoices(int from, int to)
        {

            for (int i = from; i <= to; i++)
            {

                if (conversations.ContainsKey(i))
                {

                    DialogueSpecial currentConversation = conversations[i];

                    if (currentConversation.choices.Count > 0)
                    {

                        if (currentConversation.dialogueId != null)
                        {

                            Mod.instance.save.dialogues[currentConversation.dialogueId] = (int)DialogueSpecial.dialoguemanner.impatient;

                        }

                    }

                }

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
                Rite.eventWisps,
                Rite.eventWinds,
            };

            foreach(string riteSummon in summons)
            {

                if (Mod.instance.eventRegister.ContainsKey(riteSummon))
                {

                    Mod.instance.eventRegister[riteSummon].eventComplete = true;

                }

            }

            CorvidHandle.RemoveCorvids();

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

        public virtual void LeaveLocations()
        {

        }

    }

}
