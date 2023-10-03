==========================================
STARDEW DRUID
==========================================

A druidry themed mod that adds magic mechanics to the core game experience

Requires SMAPI version 3.18.5 +

Optimised for:
- PC
- Single Player
- English

Expected to work without issue on multiplayer and expansion mods

-----------------------------------------
CHALLENGE REWARDS
-----------------------------------------

Some of the main questlines offer melee weapons that are each aligned with a druidic theme

Forest Sword will always activate Rite of the Earth regardless of what blessing has been chosen with the Effigy

Neptune Glaive and Lava Katana behave in similar fashion for Rite of the Water and Rite of The Stars, respectively

-----------------------------------------
CONFIG FILE
-----------------------------------------

GMCM has been integrated

Rite Buttons "riteButtons" is a list of keybinds set to MouseX1, MouseX2 and LeftShoulder

- Holding the specified control will increase the range of the mechanic, up to ~7.5 tiles radius.

- Keybinds can be changed in mod configuration after installing

Master Start "masterStart"

- Progress can be changed by enabling master start

- Default false as recommended to play Stardew Druid on a new save at Day 1, Spring, Year 1, 
  with a new ability unlocked at the Effigy each day via lessons and quests

-----------------------------------------
(The following config parameters can only be changed in the file)

BlessingList "blessingList"

Determines how many abilities of each rite are unlocked

Earth:

1. Explode weeds and twigs. Greet Villagers, Pets and Animals remotely, once a day
2. Extract foragables from large bushes, wood from trees, fibre and seeds from grass and small fish from water. Might spawn monsters.
3. Sprout trees, grass, seasonal forage and flowers in empty spaces.
4. Increase the growth rate and quality of growing crops. Convert planted wild seeds into random cultivations.
5. Shake loose rocks free from the ceilings of mine shafts. Explode gem ores.

Water:

1. Strike warp shrines once a day to extract totems, and boulders and stumps to extract resources.
2. Strike scarecrows, campfires and lightning rods to activate special functions. Villager firepits will work too..
3. Strike deep water to produce a fishing-spot that yields rare species of fish.
4. Expend high amounts of stamina to instantly destroy enemies.
5. Strike candle torches to create monster portals. Only works in remote outdoor locations.

Stars:

1. Summon meteors (This is the only ability for this rite)

-----------------------------------------

QuestList "questList"
Determines what quests are available to complete

False. Quest is not considered complete, and the next available quest in the list will provided by the Effigy when approached
True. Considered complete for purposes of the game

-----------------------------------------
COMPATIBILITY AND SAVE DATA
-----------------------------------------

Custom data model stored in save files
- only affects the mod, so won't impact any save files after uninstalling/installing

Enable and Disable the mod with confidence on any of your save files

Stardew Druid is stable with most popular mods, including
- SVE
- DeepWoods
- Content Patcher
- SpaceCore
- JSON Assets

Recommended download via Nexus Vortex Mod Manager application

-----------------------------------------
OPENSOURCE
-----------------------------------------

Opensource repository
https://github.com/Neosinf/StardewDruid
GNU GPL 3 License