using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewValley.Network;
using StardewValley;
using System;
using System.Collections.Generic;
using StardewDruid.Render;
using StardewDruid.Data;
using StardewDruid.Cast;
using StardewDruid.Handle;
using StardewDruid.Event.Challenge;
using System.Linq;
using StardewDruid.Battle;
using StardewDruid.Cast.Mists;

namespace StardewDruid.Character
{
    public class BattleDragon: StardewDruid.Character.Character
    {

        public Cast.Ether.Dragon useDragon;

        public int battleLevel;

        public BattleDragon()
        {
        }

        public BattleDragon(Cast.Ether.Dragon UseDragon, int Level)
          : base()
        {

            useDragon = UseDragon;
            battleLevel = Level;
        }

        public override void DrawCharacter(SpriteBatch b, Vector2 usePosition)
        {

            Vector2 spritePosition = new Vector2(usePosition.X + 32 - (32 * useDragon.dragonScale), usePosition.Y + 64 - (32 * useDragon.dragonScale));

            bool flippant = netDirection.Value % 2 == 0 && netAlternative.Value == 3 || netDirection.Value == 3;

            float drawLayer = 0.0001f;

            DragonAdditional additional = new()
            {

                direction = netDirection.Value,

                scale = useDragon.dragonScale,

                flip = flippant,

                layer = drawLayer,

                frame = specialFrame,

            };

            switch ((Character.specials)netSpecial.Value)
            {
                default:

                    additional.frame = 0;

                    useDragon.dragonRender.drawWalk(b, spritePosition, additional);

                    break;

                case specials.special:

                    additional.version = 1;

                    additional.breath = true;

                    useDragon.dragonRender.drawWalk(b, spritePosition, additional);

                    break;

                case specials.sweep:

                    useDragon.dragonRender.drawSweep(b, spritePosition, additional);

                    break;

                case specials.tackle:

                    useDragon.dragonRender.drawFlight(b, spritePosition, additional);

                    break;

            }

        }


        public override void UpdateBattle()
        {

            if (specialTimer > 0)
            {

                specialTimer--;

            }

            if (specialTimer <= 0)
            {

                ClearSpecial();

                return;

            }

            switch ((specials)netSpecial.Value)
            {

                case specials.special:

                    break;

                case specials.sweep:

                    if (specialTimer % 9 == 0)
                    {

                        specialFrame++;

                        if (specialFrame > 4)
                        {

                            specialFrame = 0;

                        }

                    }

                    break;

                case specials.tackle:

                    if (specialTimer % 12 == 0)
                    {

                        specialFrame++;

                        if (specialFrame > 4)
                        {

                            specialFrame = 1;

                        }

                    }

                    break;


            }

        }

        public override bool BattleMove(specials useSpecial)
        {

            netSpecial.Set((int)useSpecial);

            switch (useSpecial)
            {

                case specials.special:

                    specialTimer = 60;

                    break;

                case specials.sweep:

                    specialTimer = 45;

                    break;

                case specials.tackle:

                    specialTimer = 60;

                    break;


            }

            return true;

        }

        public static string DragonTitle()
        {

            switch (Mod.instance.Config.dragonScheme)
            {
                default:
                case 0:
                    
                    return Mod.instance.Helper.Translation.Get("BattleDragon.391.1");
                case 1:

                    return Mod.instance.Helper.Translation.Get("BattleDragon.391.2");
                case 2:

                    return Mod.instance.Helper.Translation.Get("BattleDragon.391.3");
                case 3:

                    return Mod.instance.Helper.Translation.Get("BattleDragon.391.4");
                case 4:

                    return Mod.instance.Helper.Translation.Get("BattleDragon.391.5");
                case 5:

                    return Mod.instance.Helper.Translation.Get("BattleDragon.391.6");
            }

        }


        public static Dictionary<BattleCombatant.battleoptions, BattleAbility.battleabilities> BattleMoveset(Dictionary<BattleCombatant.battleoptions, BattleAbility.battleabilities> moves)
        {

            moves[BattleCombatant.battleoptions.attack] = BattleAbility.battleabilities.dragonsweep;

            moves[BattleCombatant.battleoptions.tackle] = BattleAbility.battleabilities.dragondive;

            switch (Mod.instance.rite.castType)
            {

                default:
                case Rite.Rites.weald:

                    moves[BattleCombatant.battleoptions.attack] = BattleAbility.battleabilities.dragonsap;

                    break;

                case Rite.Rites.mists:

                    moves[BattleCombatant.battleoptions.block] = BattleAbility.battleabilities.dragonmists;

                    break;

                case Rite.Rites.stars:

                    moves[BattleCombatant.battleoptions.tackle] = BattleAbility.battleabilities.dragonslam;

                    break;

                case Rite.Rites.fates:

                    moves[BattleCombatant.battleoptions.attack] = BattleAbility.battleabilities.dragoncritical;

                    break;

                case Rite.Rites.ether:

                    moves[BattleCombatant.battleoptions.block] = BattleAbility.battleabilities.dragonstance;

                    break;

                case Rite.Rites.witch:

                    moves[BattleCombatant.battleoptions.attack] = BattleAbility.battleabilities.dragonstare;

                    break;

            }

            switch (Mod.instance.Config.dragonBreath)
            {

                default:
                case 0:

                    moves[BattleCombatant.battleoptions.special] = BattleAbility.battleabilities.starbreath;

                    break;

                case 1:

                    moves[BattleCombatant.battleoptions.special] = BattleAbility.battleabilities.chaosbreath;

                    break;

                case 2:

                    moves[BattleCombatant.battleoptions.special] = BattleAbility.battleabilities.etherbreath;

                    break;
            }

            return moves;

        }

        public static int BattleVitality(int battleLevel)
        {

            switch (Mod.instance.Config.dragonScale)
            {
                default:
                case 1:

                    return battleLevel * 16;
                case 2:

                    return battleLevel * 20;
                case 3:

                    return battleLevel * 24;
                case 4:

                    return battleLevel * 28;
                case 5:

                    return battleLevel * 32;

            }

        }

        public static int BattleAttack(int battleLevel)
        {

            switch (Mod.instance.Config.dragonScheme)
            {
                default:
                case 0:

                    return battleLevel * 26;
                case 1:

                    return battleLevel * 32;
                case 2:

                    return battleLevel * 29;
                case 3:

                    return battleLevel * 26;
                case 4:

                    return battleLevel * 23;
                case 5:

                    return battleLevel * 20;
            }

        }

        public static int BattleSpeed(int battleLevel)
        {

            switch (Mod.instance.Config.dragonScale)
            {
                default:
                case 1:

                    return battleLevel * 32;
                case 2:

                    return battleLevel * 28;
                case 3:

                    return battleLevel * 24;
                case 4:

                    return battleLevel * 20;
                case 5:

                    return battleLevel * 16;

            }
        }

        public static int BattleResist(int battleLevel)
        {

            switch (Mod.instance.Config.dragonScheme)
            {

                default:
                case 0:

                    return battleLevel * 26;
                case 1:

                    return battleLevel * 20;
                case 2:

                    return battleLevel * 23;
                case 3:

                    return battleLevel * 26;
                case 4:

                    return battleLevel * 29;
                case 5:

                    return battleLevel * 32;
            }

        }

    }

}
