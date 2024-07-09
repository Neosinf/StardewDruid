using StardewDruid.Character;
using StardewValley.Locations;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StardewDruid.Character.CharacterHandle;
using StardewValley.Minigames;

namespace StardewDruid.Journal
{
    public class LoreData
    {

        public static string RequestLore(CharacterHandle.characters character)
        {

            switch (character)
            {
                default:
                case characters.Effigy:

                    return "(lore) Tell me more about the Circle of Druids.";

                case characters.Jester:

                    return "(lore) I have some questions about your quest";

                case characters.Revenant:

                    return "(lore) After so many years, you must have a lot to talk about.";

                case characters.Buffin:

                    return "(lore) I'm eager to know the Chaos perspective on things happening around here";

                case characters.Shadowtin:

                    return "(lore) Do you have time to talk?";

            }

        }

        public static string CharacterLore(CharacterHandle.characters character)
        {

            switch (character)
            {

                default:
                case characters.Effigy:

                    return "The Effigy: Our traditions are etched into the bedrock of the valley.";

                case characters.Jester:

                    return "The Jester of Fate: I enjoy answering questions. One of my dearest sisters was a sphinx.";

                case characters.Revenant:

                    return "The Revenant: The bats are chatty. So I never want for something to talk to. Or yell at.";

                case characters.Buffin:

                    return "The Buffoonette of Chaos: Sure... listening to the Stream's opinions is like hearing a cryptic riddle spoken underwater.";

                case characters.Shadowtin:

                    return "Shadowtin Bear: Certainly. As long as the discussion relates to treasure. Or Dragons.";

            }

        }

        public enum stories
        {
            //------------------           
            
            Effigy_Weald,
            Effigy_self_1,

            //------------------

            Effigy_Mists,

            //------------------
            
            Effigy_self_2,

            //------------------

            Effigy_Stars,
            Revenant_Stars,
            Revenant_self_1,

            //------------------

            Effigy_Jester,
            Jester_Effigy,
            Jester_self_1,
            Jester_Fates,
            Revenant_Fates,

            //------------------

            Effigy_Buffin,
            Jester_Buffin,
            Revenant_court,
            Revenant_Marlon,

            //------------------

            Effigy_Shadowtin,
            Jester_Shadowtin,
            Shadowtin_Effigy,
            Shadowtin_Jester,
            Shadowtin_self_1,
            Buffin_court,

            //------------------

            Effigy_Ether,
            Jester_Tomb,
            Jester_Ether,
            Shadowtin_Ether,
            Revenant_Ether,
            Buffin_Ether,

            //------------------

            Effigy_Circle,
            Effigy_Circle_2,
            Jester_Circle,
            Revenant_Circle,
            Buffin_Circle,
            Shadowtin_Circle,

        }

        public static Dictionary<string,List<stories>> StorySets()
        {
            
            Dictionary<string, List<stories>> storySets = new()
            {

                [QuestHandle.swordWeald] = new(){
                    LoreData.stories.Effigy_Weald,
                    LoreData.stories.Effigy_self_1,
                },

                [QuestHandle.swordMists] = new(){
                    LoreData.stories.Effigy_Mists,
                },

                [QuestHandle.questEffigy] = new(){
                    LoreData.stories.Effigy_self_2,
                },

                [QuestHandle.swordStars] = new(){

                    LoreData.stories.Effigy_Stars,

                    LoreData.stories.Revenant_Stars,
                    LoreData.stories.Revenant_self_1,
                },

                [QuestHandle.approachJester] = new(){

                    LoreData.stories.Effigy_Jester,

                    LoreData.stories.Jester_Effigy,
                    LoreData.stories.Jester_self_1,
                    LoreData.stories.Jester_Fates,

                    LoreData.stories.Revenant_Fates,
                },

                [QuestHandle.questJester] = new(){

                    LoreData.stories.Effigy_Buffin,

                    LoreData.stories.Jester_Buffin,

                    LoreData.stories.Revenant_court,

                    LoreData.stories.Revenant_Marlon,


                },

                [QuestHandle.challengeEther] = new(){
                    
                    LoreData.stories.Effigy_Shadowtin,

                    LoreData.stories.Jester_Shadowtin,

                    LoreData.stories.Shadowtin_Effigy,
                    LoreData.stories.Shadowtin_Jester,
                    LoreData.stories.Shadowtin_self_1,

                    LoreData.stories.Buffin_court,

                },

                [QuestHandle.swordEther] = new(){


                    LoreData.stories.Effigy_Ether,

                    LoreData.stories.Jester_Tomb,
                    LoreData.stories.Jester_Ether,

                    LoreData.stories.Shadowtin_Ether,

                    LoreData.stories.Revenant_Ether,

                    LoreData.stories.Buffin_Ether,
                
                },

                [QuestHandle.questShadowtin] = new(){

                    LoreData.stories.Effigy_Circle,
                    LoreData.stories.Effigy_Circle_2,

                    LoreData.stories.Jester_Circle,
                    
                    LoreData.stories.Revenant_Circle,

                    LoreData.stories.Buffin_Circle,

                    LoreData.stories.Shadowtin_Circle,
                
                },

            };

            return storySets;

        }

        public static Dictionary<LoreData.stories,Lorestory> LoreList()
        {

            Dictionary<LoreData.stories, Lorestory> storylist = new();

            // ===========================================
            // Weald

            storylist[stories.Effigy_Weald] = new()
            {
                story = stories.Effigy_Weald,
                character = characters.Effigy,
                question = "What role do the Two Kings play?",
                answer = "In times past, the King of Oaks and the King of Holly would war upon the Equinox. " +
                "Their warring would conclude for the sake of new life in Spring. When need arose, they lent their strength to a conflict from which neither could fully recover. " +
                "They rest now, dormant. May those awake guard the change of seasons.",

            };

            storylist[stories.Effigy_self_1] = new()
            {
                story = stories.Effigy_self_1,
                character = characters.Effigy,
                question = "I want to know more about the First Farmer",
                answer = "The first farmer was blessed by the elderborn, the monarchs of the valley, to cultivate and protect this special land. " +
                    "He and the Circle elders taught me their techniques and stories, that I might be a repository of tradition for future generations of the circle. " +
                    "Though my former mentors long gone, I remain, because the power of the elders remain. For now.",

            };

            // ===========================================
            // Mists

            storylist[stories.Effigy_Mists] = new()
            {
                story = stories.Effigy_Mists,
                character = characters.Effigy,
                question = "Who is the Lady Beyond the Shore?",
                answer = "The Lady of the Isle of Mists is as beautiful and distant as the sunset on the Gem Sea. She was once a courtier of the Two Kings, from a time before the war. " +
                "The first farmer was closest to her in counsel and in conviction. She helped establish the circle and remained here a shortwhile before she was called to the Isle. " +
                "(The Effigy's eyes flicker a brilliant turqoise). There is a feeling that comes with my memories of that time, a feeling I cannot describe.",

            };

            // ===========================================
            // Effigy Quest

            storylist[stories.Effigy_self_2] = new()
            {
                story = stories.Effigy_self_2,
                character = characters.Effigy,
                question = "What happened after the Lady Beyond left the circle",
                answer = "The Lady never returned to the valley. We received a single letter, to inform us she had become the Mistress of the Isles, " +
                    "having settled her kin into their enduring slumber, and she continued to fulfill her duty to the Weald and to Yoba. " +
                    "After a time, I began to hear her voice in the storms, and mist would blanket the valley at odd times. " +
                    "It became a mark of her presence, a sign of her enduring affection for the sacred places. " +
                    "The circle weakened in her absence. The First Farmer's attention shifted to other matters, and he began to neglect his duty. " +
                    "He became obsessed with fanciful ideas, and a desire for something beyond his grasp. Then he left. " +
                    "For a long time I was alert for his return. " +
                    "Then, after a century, when it became certain he no longer walked this world, I shut myself away. ",

            };

            // ===========================================
            // Stars

            storylist[stories.Effigy_Stars] = new()
            {
                story = stories.Effigy_Stars,
                character = characters.Effigy,
                question = "Do the Stars have names?",
                answer = "The Stars have no names that can be uttered by earthly dwellers. " +
                    "They exist in the Great Expanse of the celestial realm, and care not for the life of our world, though their light sustains much of it. " +
                    "The Stars that have shone on you belong to a broken constellation of Sisters, who all mourn for one who fell to our realm. " +
                    "In fact, the circle of druids afforded a worldly name to the missing star-born. (Effigy's flaming eyes flicker). Merra. ",

            };

            storylist[stories.Revenant_Stars] = new()
            {
                story = stories.Revenant_Stars,
                character = characters.Revenant,
                question = "Tell me how you ended up like this",
                answer = "Well now. It's hard to believe, seeing as they are so pretty and we're so, well, plain, but a star came here once. " +
                        "For a time, as the star graced our humble realm, the kings stopped warring over the seasons. They gardened palaces, wrote ballads, made wine, played music. " +
                        "Still, the world turns, the seasons must change, and the old war resumed. But the stakes had changed. The ancient ones, the dragons, wanted the star for themselves. " +
                        "What came next was fire, death, and misery everywhere. I believed in the mission of the Star Guardians, so I trained as a holy warrior, and was accepted into their order. " +
                        "I survived much of the fighting, but many fell to the wrath of the heavens, so much so that it was hard to know whether our cause was just or folly. " +
                        "I think for all those lost, Yoba wept, because the Fates were sent to make it right. The Star-born, however, had already vanished. " +
                        "The Reaper of Fate tracked me here, and when he found me, he accused me of circumventing justice. He cursed me, never to die, never to rest, and never to leave. " +
                        "So I remain here, a feeble skeleton of a man, while the reaper is still out there, still looking for the star that fell so long ago."

            };

            storylist[stories.Revenant_self_1] = new()
            {
                story = stories.Revenant_self_1,
                character = characters.Revenant,
                question = "So, giant bats. Friends of yours?",
                answer = "Heh heh. It's the sacred water that gives them vitality, and fills their heads with all the whispers of the mists. " +
                        "After a few generations they started to speak, shrieking and flapping about, yapping on about the Voice beyond the Shore. " +
                        "But there's only so much a creature can do before it betrays it's nature. " +
                        "So they hang in the dark. They wait. They sing when it rains."

            };

            // ===========================================
            // Fates

            storylist[stories.Effigy_Jester] = new()
            {
                story = stories.Effigy_Jester,
                character = characters.Effigy,
                question = "What do you know of Jester and the Fates?",
                answer = "Jester serves the Priesthood of the Fates. They weave the cords of destiny into the great tapestry that is the story of the world. " +
                            "It is said that each member of the clergy serve a special purpose known only to Yoba, and so they often appear to work by mystery and happenchance, by whim even. " +
                            "(Effigy motions ever so slightly in the direction of Jester) They should not be underestimated or trifled with. " +
                            "No matter how whimsical they appear, their adherance to the decrees of their high priestess is absolute.",

            };

            storylist[stories.Jester_Effigy] = new()
            {
                story = stories.Jester_Effigy,
                character = characters.Jester,
                question = "Do you think the Effigy could learn the mysteries of the fates?",
                answer = "As powerful as he is, I think he's set in his ways. Set in the past. Well at least I think that's why he doesn't like my tricks. " +
                            "He talks about the first of the valley farmers all the time. They must have have been good friends. Has he asked you to build a pyre yet? (Jester gives a mischievous smirk)",

            };

            storylist[stories.Jester_self_1] = new()
            {
                story = stories.Jester_self_1,
                character = characters.Jester,
                question = "How goes your search for the fallen one?",
                answer = "I'm as lost as when I started. But, I have found out something about myself, something embarrassing, even disturbing, and yet, I must accept it. I like to hide in boxes.",

            };

            storylist[stories.Jester_Fates] = new()
            {
                story = stories.Jester_Fates,
                character = characters.Jester,
                question = "Tell me more about your kin, the Fates",
                answer = "Every Fate of the Priesthood has a special role we're given by Fortumei, the greatest of us, priestess of Yoba. " +
                "Some of us are like the fae from your legends, haunting creepy woods and caring for the lives of plants and little things. " +
                "For my contribution, well, I've had some pretty cool moments... (Jester is pensive as his voice trails off)",

            };

            storylist[stories.Revenant_Fates] = new()
            {
                story = stories.Revenant_Fates,
                character = characters.Revenant,
                question = "Any information you can give me to help Jester find Thanatoshi?",
                answer = "Not really, knight guardian, but I hope for my own sake as much as your friend's that he can find the mad reaper, so you can plead with him to let me pass on from this plane. " +
                "Just, be careful. The Priesthood of the Fates are the nice guys. The others, the crafters, the morbid ones, and that great crazy thing they call Chaos, they might not be so happy for you to stick your nose into forgotten affairs. " +
                "Envoys have visited us before, and the last group settled here, in the valley, somewhere on the banks of the river. " +
                "They couldn't agree amongst themselves what to do about the mess the elderfolk and dragons made of this world, and that was when the Reaper took it upon himself to administer his own kind of justice. (Revenant silently turns to the great open window).",

            };

            // ===========================================
            // Jester Quest

            storylist[stories.Effigy_Buffin] = new()
            {
                story = stories.Effigy_Buffin,
                character = characters.Effigy,
                question = "I think the Fates sent another envoy, unofficially. Another court entertainer.",
                answer = "I have started to suspect that the assignment of such indistinguished individuals as envoys of the Fae Court was intentional. The jurisdiction of the Fates has ended, and they cannot act openly. " +
                "So it is a time for subterfuge and the gathering of information, of an outward display of weakness and whimsy, so that the seats of the Fae Court might prepare to maneuver in secret. " +
                "The Morticians are not known for subtleties, but if the Artisans, the most curious of all the sects, send an undistinguished envoy, then we'll have certainty. " +
                "The mysterious games of the Fates are at play in the valley.",
            };

            storylist[stories.Jester_Buffin] = new()
            {
                story = stories.Jester_Buffin,
                character = characters.Jester,
                question = "Doesn't it strike you as a little odd that Buffin's still here?",
                answer = "It's how it's always been, farmer. You know, she's a mere moment older than me. When she was born, the artisans knew she would be claimed by Chaos, which is obvious, from the way she looks. And walks. " +
                "Anyway, the great oracle, Senkenomei, foresaw how great of an agent of Chaos Buffin would be, and panicked, because balance, or something, so she asked for the next Fate born to be dedicated to the priesthood, to become... A Hero Of Fortune. " +
                "Guess who that was? I'll give you a hint, it wasn't someone the oracle wanted at first, because that someone is not priestly in any way, but she grew fond of me eventually. " +
                "From that time forth Buffin's been a constant friend because we thought why hate each other just because of some stupid rule about balance. Still. Anything I do she has to do the same or better. That's why she's here. " +
                "She's probably trying to outdo me right now in some manner. But she still can't reach the places I do when I lick myself!",
 

            };

            storylist[stories.Revenant_court] = new()
            {
                story = stories.Revenant_court,
                character = characters.Revenant,
                question = "I've seen the sculpted likeness of the first envoys of the Fates. What were they really like?",
                answer = "Eh, the monuments are more symbolic than anything. I remember the Artisan being a bit of a hag. The Justiciar was like Thanatoshi, another seven foot tall staff-wielding thug. " +
                "The Hound of Chaos, well, the moniker is deceptive, because that creature was massive. The point is, they weren't cute little creatures of the forest like the sculptor would have you believe. " +
                "The Fae Court sent envoys strong enough to wrestle dragons and arrest celestials. " +
                "We could not defy them, and even your scarecrow friend was forbidden to approach them, lest they capture him and 'undo' all of the hardwork that went into making him. " +
                "I managed to avoid them all until, well, you've heard that story already. Don't be in any hurry to meet the Reaper.",

            };

            storylist[stories.Revenant_Marlon] = new()
            {
                story = stories.Revenant_Marlon,
                character = characters.Revenant,
                question = "Do you follow the exploits of today's adventurer's guild? The Jester of Fate delivered a portent for one of the members, Marlon.",
                answer = "Every time an iteration of some monster-slaying club shows up here I have to convince them that I'm not some evil that arises from my coffin at dusk to feast on the blood of innocents. " +
                "I am familiar with the two veterans of the current guild. They rely on me to parlay with the church of bats. Or they'll stay a while and listen while I identify some rare item for them. In regards to the foretelling, " +
                "that's probably a burden the envoy wasn't supposed to share with you, knight druid, though I doubt that the Jester is disciplined in the discretion of the Priesthood, unless, well, " +
                "there's the possibility that the vision was his. If so, he probably knows how it might or might not work out for Marlon, and may even have some sway. We can only hope he's wise enough to make the right call when it matters.",

            };

            // ===========================================
            // Shadowtin

            storylist[stories.Effigy_Shadowtin] = new()
            {
                story = stories.Effigy_Shadowtin,
                character = characters.Effigy,
                question = "Our circle now has it's own treasure hunter",
                answer = "All manner of otherfolk traded and befriended with the first farmer, but he always had the most trouble with the shadowfolk. " +
                "It's difficult to see their intentions. It's difficult to see them in any lack of light."

            };

            storylist[stories.Jester_Shadowtin] = new()
            {
                story = stories.Jester_Shadowtin,
                character = characters.Jester,
                question = "Shadowtin doesn't believe in luck. Or chance. Or fortune.",
                answer = "I think I get what he wants, I mean, trinkets and shiny things are great. But they aren't everything. He said he'd help me uncover the secret of the Fallen Star, but he doesn't care about my sacred mission. " +
                "Still, I think he has a part to play for Yoba in our great purpose. (Jester grins) You can just beat him up again if he tries to double-cross us.",

            };

            storylist[stories.Shadowtin_Effigy] = new()
            {
                story = stories.Shadowtin_Effigy,
                character = characters.Shadowtin,
                question = "From your perspective, the Effigy must seem a strange mystical artifact",
                answer = "Of all the constructs embued with the power of the elderborn, I've never heard of one so loyal to his former master. " +
                "I've done my own assessment of the quality of his make. The clothes and head-dress are cheap garbage. And threadbare. " +
                "I suspect a large cat has been kneading them, as the back is scratched and covered in fur. " +
                "You'll probably need to replace them at some point. Or burn them. The real value in the Effigy is a fashioned inner core that is saturated with elder power. " +
                "It's the heart, and the brain. A treasure from the elder age.",

            };

            storylist[stories.Shadowtin_Jester] = new()
            {
                story = stories.Shadowtin_Jester,
                character = characters.Shadowtin,
                question = "Jester has no care for treasures.",
                answer = "He claims to be a powerful agent of destiny, but he's as blind as a newborn kitten, and naive about the horrors that await him on his quest for the Fallen Star. " +
                "I doubt he's been sent by Yoba. I doubt Yoba still cares about any of us."

            };

            storylist[stories.Shadowtin_self_1] = new()
            {
                story = stories.Shadowtin_self_1,
                character = characters.Shadowtin,
                question = "How did your company come into the service of Lord Deep?",
                answer = "We're a band of professional looters. Which has been a wonderful career for me, as I'm fascinated by the elder age, and the cultural heritage of exotic cultures. " +
                "How my motivations diverged from my former associates is a consequence, I suppose, of my over-analytical personality. " +
                "The folklore of shadows is enscribed on the surface of the Great Shadow Vessel of our city. The narrative begins with the coalescence of shadows into sentient life by the cosmic power of Lord Deep. " +
                "But I have uncovered something to the contrary, a bizarre account that suggests those first enscriptions have been tampered with, and that Lord Deep is a fraud. " +
                "It's a dangerous opinion to have in my homeland, as we are a warring people, and he is a warlord of great measure, " +
                "but I have come to admire the cultures that we have battled and plundered, and I can't shake these dissident thoughts, that he has burdened and slowly corrupted my people. " +
                "I hope my travels and the treasures we uncover yield answers.",

            };


            storylist[stories.Shadowtin_self_1] = new()
            {
                story = stories.Shadowtin_self_1,
                character = characters.Shadowtin,
                question = "Some of your former band members mentioned a human agent",
                answer = "Emissaries from your plane visited the Lord Deep and established a partnership to re-acquire the dormant powers of the Ancient Ones. " +
                "There are many shadow mercenaries at work in the nations of your world, and this valley was my assignment. " +
                "The human contact I conferred with does not live here, and I am purposefully ignorant of their daylight identity. I do know they're wealthy. And they are disappointed in my progress. " +
                "I used more resources than they were prepared to allocate, but I believe that it was necessary to conduct a thorough investigation of the prospects here. " +
                "I suppose demotion could be considered my contract evaluation.",

            };


            storylist[stories.Buffin_court] = new()
            {
                story = stories.Buffin_court,
                character = characters.Buffin,
                question = "What role did Chaos play in the low court?",
                answer = "Well my great great predecessor, Carnevellion, was the likeness of that big hunky statue with the large mouth. They called him the Hound of Chaos. " +
                "It's a little regarded fact that none of the original envoys returned to report on their administration of the valley. " +
                "We only know Thanatoshi is out there, because the morticians do not decease like other entities, their essense must be forcefully returned to the heavenly fields by one of their number. " +
                "It's an unofficial bet, but I'll find the bones of the Hound before Jester finds the Reaper. Then I might be able to answer your question. What happened here all those centuries ago?",

            };


            // ===========================================
            // Ether

            storylist[stories.Effigy_Ether] = new()
            {
                story = stories.Effigy_Ether,
                character = characters.Effigy,
                question = "What do you know of Dragonkind?",
                answer = "The Lady Beyond declined to dwell on the nature of the aggressors of a war that claimed her kin. What I know of Dragonkind comes from the poetry of myth. " +
                "They were the first servants of Yoba, and had diminished long before the event that marks the nadir of their noble race. " +
                "Their bones have become the foundation of the otherworld, their potent life essence has become the streams of ether that flow through the planes. "

            };

            storylist[stories.Jester_Tomb] = new()
            {
                story = stories.Jester_Tomb,
                character = characters.Jester,
                question = "I'm sorry about what happened in the Tomb",
                answer = "(Jester looks away) I just... I thought that when we found Thanatoshi, then that would be the end of the 'grand quest'. Now the end seems evern further away. " +
                "I never met the Reaper before, but I've told a few stories about him, and he was always this great, unbending hero. " +
                "Well he didn't seem so great when he was trying to slice our heads off. (Jester looks up) I still haven't informed the Morticians where his broken soul is.",

            };

            storylist[stories.Jester_Ether] = new()
            {
                story = stories.Jester_Ether,
                character = characters.Jester,
                question = "Any thoughts on the Ancient ones?",
                answer = "Oh I've heard lots of scary tales about those things. Monstrous. Scaley. Big teeth. Warts on their nose (Jester isn't sure about that last one). " +
                "They could burn you with a look and make you into roast dinner with a single word! Actually I... (Jester's hairs raise across it's backside) get kind os scared just thinking about them. I'm glad you have my back!",

            };

            storylist[stories.Shadowtin_Ether] = new()
            {
                story = stories.Shadowtin_Ether,
                character = characters.Shadowtin,
                question = "So what dragon treasures have you found?",
                answer = "I've found this cloak, and this carnyx, and this bear mask. The Dragons would demand the best tributes, of craftsmanship that would inspire an Artisan of the Fates, with the finest materials available. " +
                "All Shadowfolk prize ethereal technology, and it's a very competitive society, so I have to carry mine with me at all times.",

            };

            storylist[stories.Revenant_Ether] = new()
            {
                story = stories.Revenant_Ether,
                character = characters.Revenant,
                question = "Did you ever get to fight a dragon?",
                answer = "Heh heh. Not a chance, the low-born guards like me could only man the ramparts and the great machines designed to strike them from the sky. " +
                "The elders had magic to stun and wound the ancient ones, and survive the heat from the hellscape made in the dragons' wake, but it was a lost cause until, well, until someone arrived that could match their strength. " +
                "I admit, the form you're able to take now, it's terrifying, but if you can imagine being several times bigger, with a mind that holds the wisdom of ages and a soul that's even older, then you'll have an idea what the original dragons were like. " +
                "I would not want to see them take to the skies of our realm again, that's for sure.",

            };

            storylist[stories.Buffin_Ether] = new()
            {
                story = stories.Buffin_Ether,
                character = characters.Buffin,
                question = "Is chaos older than the dragons?",
                answer = "Chaos has 'been' since the beginning, since the void, since the ether, which spawned a multitude of ancient races, of which the dragons either devoured or subjugated, in their ascent to become the single masters of the ether. " +
                "But they could never tame Chaos, and perhaps some might have served it, if they weren't so arrogant. The Saurus though, the other great race of lizards, they favoured the Stream. They all perished though. " +
                "A shame, but such is the nature of chaos, it possesses no permanence. It is the true opposition of destiny woven by the Fate Priesthood.",

            };


            // ===========================================
            // Ether

            storylist[stories.Effigy_Circle] = new()
            {
                story = stories.Effigy_Circle,
                character = characters.Effigy,
                question = "About the old circle, what can you tell me of Knight Wyrven",
                answer = "Good Knight Wyrven was the most senior of the Guardians that survived the war. In those times all the disparate factions of humans banded together to settle disputes and rebuild communities. " +
                "Then the Fates arrived, and their interference unsettled Wyrven the most. He believed humanity should be completely autonomous, and from the few interactions I had with him, it appeared to me he sought the power to claim that autonomy. " +
                "He was most interested in the shapeshifting traditions of calico, and visited the valley to converse with those shamans that forsook their dragon-serving traditions to become Druids. I'm not sure if his efforts proved worthwhile.",

            };


            storylist[stories.Effigy_Circle_2] = new()
            {
                story = stories.Effigy_Circle_2,
                character = characters.Effigy,
                question = "About the old circle, what can you tell me of the brothers Cannoli and Gelatin?",
                answer = "The brothers were stewards of the Weald. They served in the kitchens during the conflict, and when the circle was founded, their knowledge of all the different varieties of flora and fauna proved invaluable to our efforts. " +
                "Cannoli even claimed he and Gelatin were pastry chefs to the royal court, in the personal service of the elderborn prince of the Isles. " +
                "Despite their oaths to the Weald, the brothers fell to wickedness, and had to be put down by Wyrven's guardians, though I never got to witness the fight, or attend the funerary rites. " +
                "It was about the time the Fates and their zealots began to openly oppose the circle. Wyrven and the rest retreated to the mountain. The First Farmer told me to hide. " +
                "Though it pains me to admit my suspicions, it appears he abandoned the circle, to pursue unrequited love across the gem sea. When I finally emerged from the cave, the circle was gone.",

            };

            storylist[stories.Jester_Circle] = new()
            {
                story = stories.Jester_Ether,
                character = characters.Jester,
                question = "I think you were right about Shadowtin, he's proven himself",
                answer = "When we were fighting him and his buddies, I pounced on burgundy boy as soon as I could. " +
                "I thought I would rip off his mask and reveal his face to the world, and it would be like an awesome moment, with him yelling, and me saying some punch line in a deep, serious voice like, 'Face us with your real face, Shadowman'. " +
                "Anyway, soon as I touched him I saw a vision of him and you, working together, except he was a cat, like me, so I thought maybe that means we will combine our powers and become a super team. So that's why I wanted to give him a chance. I'm glad I was right.",

            };

            storylist[stories.Revenant_Circle] = new()
            {
                story = stories.Revenant_Ether,
                character = characters.Revenant,
                question = "The Mother of Crows. Why would you keep the identity of the Artisan of Fate, the one that accompanied Thanatoshi and the other original envoys, a secret?",
                answer = "Because she swore me to secrecy. I still cannot reveal the few details I know about what transpired when the Crowmother was here, and why, or who, for that matter. " +
                    "I am bound by dual terms, to the Artisans and the Morticians, as you know now, that is how the Fates operate, always a dichotomy, tied at both ends. " +
                    "I can only say that the crowmother will return for the rite of bones. How that's going to happen is beyond me. " +
                    "She was old when she left, and even fates have limited life spans according to their purpose, so she'll be ancient bones by now, maybe even the bones referred to in the foretelling. " +
                    "I hope you sort this all out soon, knight druid. ",

            };

            storylist[stories.Buffin_Circle] = new()
            {
                story = stories.Buffin_Ether,
                character = characters.Buffin,
                question = "Did the Great Stream witness what happened here after the War for the Star?",
                answer = "This is what I remember from the account scrawled on one of the walls of the Chaos sanitarium, written by the Watcher who surmised these events from the patterns of the Stream. " +
                "When the dragons perished, waves of liberated, ethereal energy swept across the realms, and from this, the artisans set to work, capturing and organising this energy into new forms. " +
                "The Stream observed this artifice, and was displeased, as the designs possessed no nuances of interest, for they were boring, mechanical and utilitarian. " +
                "So the Stream sabotaged the craft, and from the imperfections, the elementals emerged, and were unleashed upon this world. It was the advent of the arcane epoch. " +
                "As was the intention of Chaos, many of the mortal races were threatened by the new, subsuming powers, and were forced to adapt to survive. " +
                "But the Artisans opposed the initiative of Chaos, and provided gifts of knowledge and inspiration, that some races might harness the elementals for their own purposes, and achieve the zenith of their civilisations. " +
                "I believe the engine you beheld is one such divinely inspired device. It is a precursor to the marvels of Stellarfolk and Shadowfolk engineering. " +
                "Still, The Stream resented the development of such organised systems, and presented it's own gifts, in dreams and mysteries, to the first mortals to learn the arcane arts, to rival the technomancers. " +
                "Is that sufficient to explain the elemental wars or have you not learned about that yet?",

            };

            storylist[stories.Shadowtin_Circle] = new()
            {
                story = stories.Buffin_Ether,
                character = characters.Buffin,
                question = "",
                answer = "We've been slowly consumed by the hunger of a tyrant, who expends the lives of our best folk to construct his cities and continue his conquests, " +
                "all under the pretense that we are his property, his souls to enflame and extinguish to his purposes. "+
                "I want to expose him as an imposter, before he gains the power of dragons and heaps the souls of my people into an engine of war, " +
                "and to do that I need the truth of his past. Unequivocable testimony of his fraud.",
            };

            return storylist;

        }

    }

    public class Lorestory
    {

        public LoreData.stories story;

        public CharacterHandle.characters character;

        public string question;

        public string answer;

    }

}
