using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewDruid.Monster;
using StardewDruid.Monster.Boss;
using StardewDruid.Monster.Template;
using StardewModdingAPI;
using StardewValley;
using System.Collections.Generic;
using System.IO;
using static StardewDruid.Event.SpellHandle;

namespace StardewDruid.Map
{
    static class MonsterData
    {
        public static StardewValley.Monsters.Monster CreateMonster(int spawnMob, Vector2 spawnVector, int combatModifier = -1, bool champion = false)
        {

            if(combatModifier == -1)
            {

                combatModifier = Mod.instance.CombatDifficulty();

            }

            System.Random randomise = new();

            StardewValley.Monsters.Monster theMonster;

            switch (spawnMob)
            {
                
                default:
                case 0: // Bat

                    if (champion)
                    {

                        theMonster = new BigBat(spawnVector, combatModifier);

                        break;

                    }

                    theMonster = new Bat(spawnVector, combatModifier);

                    break;

                case 1: // Shadow Brute

                    if (champion)
                    {

                        theMonster = new Shooter(spawnVector, combatModifier);

                        break;

                    }

                    theMonster = new Shadow(spawnVector, combatModifier);

                    break;

                case 2: // Green Slime

                    if (champion)
                    {

                        if(randomise.Next(2) == 0){

                            
                            theMonster = new BlobSlime(spawnVector, combatModifier);

                        }
                        else
                        {

                            theMonster = new BigSlime(spawnVector, combatModifier);

                        }

                        break;

                    }

                    theMonster = new Slime(spawnVector, combatModifier);

                    break;

                case 3: // Skeleton

                    theMonster = new Skeleton(spawnVector, combatModifier);

                    break;

                case 4: // Golem

                    theMonster = new Golem(spawnVector, combatModifier);

                    break;

                case 5: // DustSpirit

                    theMonster = new Spirit(spawnVector, combatModifier);

                    break;

                case 6: // Gargoyle

                    theMonster = new Gargoyle(spawnVector, combatModifier);

                    if (!champion)
                    {

                        (theMonster as Gargoyle).SetMode(0);

                    }

                    (theMonster as Boss).RandomTemperment();

                    /*string scheme = randomise.Next(2) == 0 ? "Solar" : "Void";

                    (theMonster as Gargoyle).netScheme.Set(scheme);

                    (theMonster as Gargoyle).SchemeLoad();*/

                    break;

                case 7: // Demonki

                    theMonster = new Demonki(spawnVector, combatModifier);

                    if (!champion)
                    {

                        (theMonster as Demonki).SetMode(0);

                    }

                    (theMonster as Boss).RandomTemperment();

                    break;

                case 8:

                    theMonster = new Dino(spawnVector, combatModifier);

                    (theMonster as Dino).SetMode(0);

                    if (champion)
                    {

                        (theMonster as Dino).SetMode(2);

                    }

                    (theMonster as Boss).RandomTemperment();

                    break;

                case 9:

                    if(randomise.Next(2) == 0)
                    {
                        
                        theMonster = new Scavenger(spawnVector, combatModifier);

                    }
                    else
                    {

                        theMonster = new Shadowfox(spawnVector, combatModifier);

                    }

                    (theMonster as Boss).SetMode(1);

                    (theMonster as Boss).RandomTemperment();

                    break;

                case 10:

                    if (randomise.Next(2) == 0)
                    {

                        theMonster = new Rogue(spawnVector, combatModifier);

                    }
                    else
                    {

                        theMonster = new Goblin(spawnVector, combatModifier);

                    }

                    (theMonster as Boss).SetMode(1);

                    (theMonster as Boss).RandomTemperment();

                    break;

                case 11:

                    string dragon = "Purple";

                    switch (randomise.Next(4))
                    {

                        case 1:

                            dragon = "Red"; break;

                        case 2:

                            dragon = "Blue"; break;

                        case 3:

                            dragon = "Black"; break;

                    }


                    theMonster = new Dragon(spawnVector, combatModifier, dragon+"Dragon");

                    (theMonster as Dragon).SetMode(1);

                    (theMonster as Boss).RandomTemperment();

                    break;

            }

            return theMonster;

        }

        public static bool BossMonster(StardewValley.Monsters.Monster monster)
        {

            if(monster is StardewDruid.Monster.Boss.Boss)
            {

                return true;

            }

            List<System.Type> customMonsters = new()
            {
                typeof(BigBat),
                typeof(Shooter),
                typeof(BigSlime),

            };

            if (customMonsters.Contains(monster.GetType()))
            {

                return true;

            }

            return false;

        }

        public static Texture2D MonsterTexture(string characterName)
        {

            if (characterName == "Dinosaur")
            {

                return Game1.content.Load<Texture2D>("Characters\\Monsters\\Pepper Rex");

            }

            Texture2D characterTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", characterName + ".png"));

            return characterTexture;

        }

        public static StardewValley.AnimatedSprite MonsterSprite(string characterName)
        {

            StardewValley.AnimatedSprite characterSprite;

            characterSprite = new();

            characterSprite.textureName.Set("18465_" + characterName);

            characterSprite.spriteTexture = MonsterTexture(characterName);

            characterSprite.loadedTexture = "18465_" + characterName;

            if (characterName.Contains("Dragon"))
            {

                characterSprite.SpriteHeight = 64;

                characterSprite.SpriteWidth = 64;

            }
            else if (characterName.Contains("Reaper"))
            {
                characterSprite.SpriteHeight = 48;

                characterSprite.SpriteWidth = 64;
            }
            else
            {
                characterSprite.SpriteHeight = 32;

                characterSprite.SpriteWidth = 32;

            }

            characterSprite.UpdateSourceRect();

            return characterSprite;

        }

    }

}
