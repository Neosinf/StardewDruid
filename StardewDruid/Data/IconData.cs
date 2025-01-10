using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Dialogue;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Extensions;
using StardewValley.Minigames;
using StardewValley.Monsters;
using StardewValley.Objects;
using StardewValley.Projectiles;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace StardewDruid.Data
{
    public class IconData
    {

        public enum cursors
        {
            none,

            weald,
            mists,
            stars,
            fates,

            wealdCharge,
            mistsCharge,
            starsCharge,
            fatesCharge,

            scope,
            death,
            shadow,
            target,

            divineCharge,
            feathers,

        }

        public Texture2D cursorTexture;

        public enum displays
        {
            none,

            weald,
            mists,
            stars,
            fates,
            ether,
            chaos,

            effigy,
            revenant,
            jester,
            buffin,
            shadowtin,
            blackfeather,

            active,
            reverse,
            forward,
            end,
            exit,
            complete,

            quest,
            effect,
            relic,
            herbalism,
            lore,
            transform,

            speech,
            blaze,
            morph,
            skull,
            blind,
            knock,

            glare,
            up,
            down,
            scroll,
            replay,
            flag,

            bones,
            skip,
            save,
            aldebaran,
            question,
            bombs,

            powderbox,
            heroes,

        }

        public Texture2D displayTexture;

        public enum decorations
        {
            none,
            weald,
            mists,
            stars,
            fates,
            ether,
            bones,

        }

        public Dictionary<Rite.rites, decorations> riteDecorations = new()
        {

            [Rite.rites.weald] = decorations.weald,

            [Rite.rites.mists] = decorations.mists,

            [Rite.rites.stars] = decorations.stars,

            [Rite.rites.fates] = decorations.fates,

            [Rite.rites.ether] = decorations.ether,

            [Rite.rites.bones] = decorations.bones,

        };

        public Dictionary<Rite.rites, displays> riteDisplays = new()
        {

            [Rite.rites.none] = displays.chaos,

            [Rite.rites.weald] = displays.weald,

            [Rite.rites.mists] = displays.mists,

            [Rite.rites.stars] = displays.stars,

            [Rite.rites.fates] = displays.fates,

            [Rite.rites.ether] = displays.ether,

            [Rite.rites.bones] = displays.bones,

        };

        public Texture2D decorationTexture;

        public enum impacts
        {
            none,

            impact,
            flashbang,
            glare,
            splash,
            sparkle,
            fish,

            boltnode,
            sparknode,
            clouds,
            puff,
            splatter,
            skull,

            plume,
            steam,
            smoke,
            love,
            sparkbang,
            empty2,

            supree,
            bomb,
            mists,
            summoning,
            shockwave,
            lovebomb,
            flasher,
            spiral,
            deathbomb,

        }

        public Texture2D impactsTexture;

        public Texture2D impactsTextureTwo;

        public Texture2D impactsTextureThree;

        public Texture2D swirlsTexture;

        public enum skies
        {
            none,
            valley,
            mountain,
            moon,
            night,
            temple,
            sunset,
            hellscape,
        }

        public Texture2D skyTexture;

        public enum circles
        {
            none,
            summoning,
        }

        public Texture2D circleTexture;

        
        public enum tilesheets
        {
            none,
            outdoors,
            outdoorsTwo,
            grove,
            spring,
            graveyard,
            atoll,
            chapel,
            clearing,
            lair,
            court,
            tomb,
            engineum,
            gate,
            pavement,
            ritual,
            access,
            
            ground,
            groundspring,
            groundsummer,
            groundautumn,
            groundwinter,

            mounds,
            skyblue,
            skynight,
            overlook,

            magnolia,
            magnolialeaf,
            magnolialeaftwo,
            magnoliashade,
            darkoak,
            darkoakleaf,
            hawthorn,
            hawthornleaf,
            hawthornshade,
            holly,
            hollyleaf,
            flowers,
        }

        public Dictionary<tilesheets, Texture2D> sheetTextures = new();

        public enum relics
        {
            none,

            effigy_crest,
            jester_dice,
            shadowtin_tome,
            dragon_form,
            blackfeather_glove,
            heiress_gift,

            wayfinder_pot,
            wayfinder_censer,
            wayfinder_lantern,
            wayfinder_water,
            wayfinder_ceremonial,
            wayfinder_dwarf,

            herbalism_mortar,
            herbalism_pan,
            herbalism_still,
            herbalism_crucible,
            herbalism_gauge,
            dwarven_bazooka,

            wayfinder_stone,
            crow_hammer,
            wayfinder_key,
            wayfinder_glove,
            wayfinder_eye,
            stardew_druid,

            tactical_discombobulator,
            tactical_mask,
            tactical_cell,
            tactical_lunchbox,
            tactical_peppermint,
            tactical_6,

            runestones_spring,
            runestones_farm,
            runestones_moon,
            runestones_cat,
            runestones_alchemistry,
            runestones_6,

            avalant_disc,
            avalant_chassis,
            avalant_gears,
            avalant_casing,
            avalant_needle,
            avalant_measure,

            book_wyrven,
            book_letters,
            book_manual, 
            book_druid, 
            book_chart,
            book_knight,

            box_measurer,
            box_mortician,
            box_artisan,
            box_chaos,
            box_4,
            box_5,

            skull_saurus,
            skull_gelatin,
            skull_cannoli,
            skull_fox,
            golden_pot,
            golden_core,

            restore_goshuin,
            restore_offering,
            restore_cloth

        }

        public Texture2D relicsTexture;

        public enum potions
        {
            none,
            
            // potions
            ligna,
            ligna1,
            ligna2,
            ligna3,
            ligna4,
            faeth,
            
            impes,
            impes1,
            impes2,
            impes3,
            impes4,
            aether,
            
            celeri,
            celeri1,
            celeri2,
            celeri3,
            celeri4,
            voil,
            
            lignaGray,
            lignaGray1,
            lignaGray2,
            lignaGray3,
            lignaGray4,
            faethGray,
            
            impesGray,
            impesGray1,
            impesGray2,
            impesGray3,
            impesGray4,
            aetherGray,

            celeriGray,
            celeriGray1,
            celeriGray2,
            celeriGray3,
            celeriGray4,
            voilGray,

            // powders
            imbus,
            amori,
            donis,
            concutere,
            jumere,
            felis,

            imbusGray,
            amoriGray,
            donisGray,
            concutereGray,
            jumereGray,
            felisGray,

        }

        public Texture2D potionsTexture;

        public enum warps
        {

            portal,
            smoke,
            corvids,
            circle,

        }

        public Texture2D boltTexture;

        public Texture2D laserTexture;

        public Texture2D lightningTexture;

        public Texture2D warpTexture;

        public Texture2D gravityTexture;

        public Texture2D emberTexture;

        public Texture2D crateTexture;

        public Texture2D echoTexture;

        public Texture2D wispTexture;

        public Texture2D windwispTexture;

        public Texture2D deathwispTexture;

        public Texture2D shieldTexture;

        public Microsoft.Xna.Framework.Rectangle shadowRectangle;

        public enum schemes
        {
            none,

            weald,
            mists,
            stars,
            fates,
            ether,
            bones,

            death,
            golden,
            snazzle,
            stardew,
            wisps,
            white,

            rock,
            rockTwo,
            rockThree,

            herbal_ligna,
            herbal_impes,
            herbal_celeri,
            herbal_faeth,
            herbal_aether,
            herbal_voil,

            slime_one,
            slime_two,
            slime_three,

            bomb_one,
            bomb_two,
            bomb_three,

        }

        public Dictionary<IconData.schemes, Microsoft.Xna.Framework.Color> schemeColours = new()
        {
            [schemes.none] = Microsoft.Xna.Framework.Color.White,

            [schemes.weald] =  Microsoft.Xna.Framework.Color.Green,
            [schemes.mists] = new(75, 138, 187),
            [schemes.stars] =  new(231, 102, 84),
            [schemes.fates] = new(119, 75, 131),
            [schemes.ether] = new(13, 114, 185),
            [schemes.bones] = new(81, 115, 131),

            [schemes.death] = new(119, 201, 177),
            [schemes.golden] = new(251, 201, 38),
            [schemes.snazzle] = new(214, 0, 175),
            [schemes.stardew] = new(210,164,89),
            [schemes.white] = Microsoft.Xna.Framework.Color.White,

            [schemes.rock] = new(120, 154, 160),
            [schemes.rockTwo] = new(196, 164, 122),
            [schemes.rockThree] = new(144, 126, 144),

            [schemes.herbal_ligna] = new(186, 255, 201),
            [schemes.herbal_impes] = new(255, 179, 186),
            [schemes.herbal_celeri] = new(186, 225, 255),
            [schemes.herbal_faeth] = new(255, 255, 186),
            [schemes.herbal_aether] = new(186, 225, 255),
            [schemes.herbal_voil] = new(86, 125, 155),

            [schemes.slime_one] = new(174, 0, 0),
            [schemes.slime_two] = new(120, 122, 131),
            [schemes.slime_three] = new(180, 60, 60),

            [schemes.bomb_one] = new(174, 0, 0),
            [schemes.bomb_two] = new(120, 122, 131),
            [schemes.bomb_three] = new(180, 60, 60),

        };

        public Dictionary<IconData.schemes, List<Microsoft.Xna.Framework.Color>> gradientColours = new()
        {
            [schemes.none] = new() { Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Gray, Microsoft.Xna.Framework.Color.DarkGray, },

            [schemes.weald] = new() { Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.LightGreen, Microsoft.Xna.Framework.Color.Green },
            [schemes.mists] = new() { new(75, 138, 187), new(70, 81, 144), new(83, 96, 150), },
            [schemes.stars] = new() { new(255, 230, 166), new(255, 173, 84), new(231, 102, 84), },
            [schemes.fates] = new() { new(119, 75, 131), new(59, 55, 100), new(37, 34, 74), new(236, 118, 124), },
            [schemes.ether] = new() { new(111, 203, 220), new(84, 163, 218), new(13, 114, 185), },

            [schemes.death] = new() { new(109, 175, 156), new(119, 201, 177),  new(162, 216, 202), },
            [schemes.golden] = new() { new(255, 250, 194), new(255, 232, 155), new(251, 201, 38), },
            [schemes.snazzle] = new() { new(214, 0, 175), new(155, 0, 126), new(37, 34, 74), },
            [schemes.wisps] = new() { new(75, 138, 187), new(69, 186, 235), new(74, 243, 255), new(186, 225, 255), },

        };

        public IconData()
        {

            cursorTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Cursors.png"));

            displayTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Displays.png"));

            decorationTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Decorations.png"));

            emberTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Embers.png"));

            impactsTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Impacts.png"));

            impactsTextureTwo = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "ImpactsTwo.png"));

            impactsTextureThree = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "ImpactsThree.png"));

            swirlsTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Swirl.png"));

            skyTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Skies.png"));

            circleTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Circle.png"));


            sheetTextures[tilesheets.grove] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Grove.png"));

            sheetTextures[tilesheets.spring] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Spring.png"));

            sheetTextures[tilesheets.atoll] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Atoll.png"));

            sheetTextures[tilesheets.graveyard] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Graveyard.png"));

            sheetTextures[tilesheets.chapel] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Chapel.png"));

            sheetTextures[tilesheets.clearing] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Clearing.png"));

            sheetTextures[tilesheets.lair] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Lair.png"));

            sheetTextures[tilesheets.court] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Court.png"));

            sheetTextures[tilesheets.tomb] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Tomb.png"));

            sheetTextures[tilesheets.engineum] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Engineum.png"));

            
            sheetTextures[tilesheets.gate] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Gate.png"));

            sheetTextures[tilesheets.pavement] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Pavement.png"));

            sheetTextures[tilesheets.ritual] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Ritual.png"));

            sheetTextures[tilesheets.access] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Access.png"));


            sheetTextures[tilesheets.ground] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundSpring.png"));

            sheetTextures[tilesheets.groundspring] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundSpring.png"));

            sheetTextures[tilesheets.groundsummer] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundSummer.png"));

            sheetTextures[tilesheets.groundautumn] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundAutumn.png"));

            sheetTextures[tilesheets.groundwinter] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundWinter.png"));


            sheetTextures[tilesheets.mounds] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Mounds.png"));

            sheetTextures[tilesheets.skyblue] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Skyblue.png"));

            sheetTextures[tilesheets.skynight] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Skynight.png"));

            sheetTextures[tilesheets.overlook] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Overlook.png"));


            sheetTextures[tilesheets.magnolia] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Magnolia.png"));

            sheetTextures[tilesheets.magnolialeaf] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "MagnoliaLeaf.png"));
            
            sheetTextures[tilesheets.magnolialeaftwo] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "MagnoliaLeafTwo.png"));
            
            sheetTextures[tilesheets.magnoliashade] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "MagnoliaShade.png"));

            sheetTextures[tilesheets.darkoak] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "DarkOak.png"));

            sheetTextures[tilesheets.darkoakleaf] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "DarkOakLeaf.png"));

            sheetTextures[tilesheets.hawthorn] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Hawthorn.png"));

            sheetTextures[tilesheets.hawthornleaf] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "HawthornLeaf.png"));

            sheetTextures[tilesheets.hawthornshade] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "HawthornShade.png"));

            sheetTextures[tilesheets.holly] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Holly.png"));

            sheetTextures[tilesheets.hollyleaf] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "HollyLeaf.png"));

            sheetTextures[tilesheets.flowers] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Flowers.png"));


            relicsTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Relics.png"));

            potionsTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Potions.png"));

            boltTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Bolt.png"));

            laserTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Laser.png"));

            lightningTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Lightning.png"));

            warpTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Warp.png"));

            gravityTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Gravity.png"));

            crateTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "DragonCrate.png"));

            echoTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Echo.png"));

            wispTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Wisp.png"));

            windwispTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "WindWisp.png"));

            deathwispTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "DeathWisp.png"));

            shieldTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Shield.png"));


            shadowRectangle = CursorRectangle(cursors.shadow);

            LoadNuances();


        }

        public void LoadNuances()
        {

            sheetTextures[tilesheets.outdoors] = Mod.instance.Helper.GameContent.Load<Texture2D>(Path.Combine("Maps", Game1.currentSeason + "_outdoorsTileSheet"));

            sheetTextures[tilesheets.outdoorsTwo] = Mod.instance.Helper.GameContent.Load<Texture2D>(Path.Combine("Maps", Game1.currentSeason + "_outdoorsTileSheet2"));

        }

        public static Texture2D GetTilesheet(tilesheets tilesheet)
        {
            switch (tilesheet) 
            {

                default:
                case tilesheets.grove: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Grove.png"));

                case tilesheets.spring: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Spring.png"));

                case tilesheets.atoll: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Atoll.png"));

                case tilesheets.graveyard: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Graveyard.png"));

                case tilesheets.chapel: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Chapel.png"));

                case tilesheets.clearing: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Clearing.png"));

                case tilesheets.lair: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Lair.png"));

                case tilesheets.court: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Court.png"));

                case tilesheets.tomb: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Tomb.png"));

                case tilesheets.engineum: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Engineum.png"));

                
                case tilesheets.gate: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Gate.png"));

                case tilesheets.pavement: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Pavement.png"));

                case tilesheets.ritual: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Ritual.png"));

                case tilesheets.access: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Access.png"));


                case tilesheets.ground:

                    switch (Game1.season)
                    {

                        default:

                            return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundAutumn.png"));

                        case Season.Summer:

                            return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundSummer.png"));

                        case Season.Fall:

                            return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundAutumn.png"));

                        case Season.Winter:

                            return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundWinter.png"));

                    }

                case tilesheets.groundspring: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundSpring.png"));

                case tilesheets.groundsummer: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundSummer.png"));

                case tilesheets.groundautumn: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundAutumn.png"));

                case tilesheets.groundwinter: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundWinter.png"));


                case tilesheets.mounds: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Mounds.png"));

                case tilesheets.skyblue: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Skyblue.png"));

                case tilesheets.skynight: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Skynight.png"));

                case tilesheets.overlook: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Overlook.png"));


                case tilesheets.magnolia: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Magnolia.png"));

                case tilesheets.magnolialeaf: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "MagnoliaLeaf.png"));

                case tilesheets.magnolialeaftwo: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "MagnoliaLeafTwo.png"));

                case tilesheets.magnoliashade: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "MagnoliaShade.png"));

                case tilesheets.darkoak: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "DarkOak.png"));

                case tilesheets.darkoakleaf: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "DarkOakLeaf.png"));

                case tilesheets.hawthorn: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Hawthorn.png"));
                    
                case tilesheets.hawthornleaf: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "HawthornLeaf.png"));
                    
                case tilesheets.hawthornshade: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "HawthornShade.png"));

                case tilesheets.holly: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Holly.png"));

                case tilesheets.hollyleaf: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "HollyLeaf.png"));

                case tilesheets.flowers: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Flowers.png"));

            }

        }


        public Microsoft.Xna.Framework.Color SchemeColour(schemes scheme)
        {

            if (schemeColours.ContainsKey(scheme))
            {
                
                return schemeColours[scheme];
            
            }

            return Microsoft.Xna.Framework.Color.White;

        }

        public static Microsoft.Xna.Framework.Rectangle CursorRectangle(cursors id)
        {

            if (id == cursors.none) { return new(); }

            int slot = (int)id - 1;

            return new(slot % 4 * 48, slot / 4 * 48, 48, 48);


        }

        public TemporaryAnimatedSprite CursorIndicator(GameLocation location, Vector2 origin, cursors cursorId, CursorAdditional additional)
        {

            Vector2 originOffset = origin + new Vector2(32f, 32f) - new Vector2(24 * additional.scale, 24 * additional.scale);

            Microsoft.Xna.Framework.Rectangle cursorRect = CursorRectangle(cursorId);

            if(additional.layer == -1f)
            {

                additional.layer = (origin.Y - 128) / 10000;

            }

            Microsoft.Xna.Framework.Color color = Microsoft.Xna.Framework.Color.White;

            switch (cursorId)
            {
                case cursors.scope:
                case cursors.death:
                case cursors.target:

                    color = SchemeColour(additional.scheme);

                    break;

            }

            TemporaryAnimatedSprite animation = new(0, additional.interval, 1, additional.loops, originOffset, false, false)
            {

                sourceRect = cursorRect,

                sourceRectStartingPos = new Vector2(cursorRect.X, cursorRect.Y),

                texture = cursorTexture,

                scale = additional.scale,

                layerDepth = additional.layer,

                timeBasedMotion = true,

                Parent = location,

                alpha = additional.alpha,

                alphaFade = additional.fade,

                delayBeforeAnimationStart = additional.delay,

                color = color,

            };

            if (additional.rotation > 0)
            {

                animation.rotationChange = (float)(Math.PI / additional.rotation);

            }

            location.temporarySprites.Add(animation);

            return animation;

        }

        public static Microsoft.Xna.Framework.Rectangle DisplayRectangle(displays id)
        {

            if (id == displays.none) { return new(); }

            int slot = Convert.ToInt32(id) - 1;

            return new(slot % 6 * 16, slot / 6 * 16, 16, 16);

        }

        public static Microsoft.Xna.Framework.Rectangle RelicRectangles(relics relic)
        {

            if (relic == relics.none) { return new(); }

            int slot = Convert.ToInt32(relic) - 1;

            return new(slot % 6 * 20, slot == 0 ? 0 : slot / 6 * 20, 20, 20);

        }

        public static Microsoft.Xna.Framework.Rectangle PotionRectangles(potions potion)
        {

            if (potion == potions.none) { return new(); }

            int slot = Convert.ToInt32(potion) - 1;

            return new(slot % 6 * 20, slot == 0 ? 0 : slot / 6 * 20, 20, 20);

        }

        public Microsoft.Xna.Framework.Rectangle QuestDisplay(Quest.questTypes questType)
        {
            switch (questType)
            {
                case Quest.questTypes.challenge:

                    return DisplayRectangle(displays.quest);

                case Quest.questTypes.lesson:

                    return DisplayRectangle(displays.effect);

                case Quest.questTypes.miscellaneous:

                    return DisplayRectangle(displays.active);

                default:

                    return DisplayRectangle(displays.speech);

            }

        }

        public static Microsoft.Xna.Framework.Rectangle DecorativeRectangle(decorations id)
        {

            if (id == decorations.none) { return new(); }

            int slot = Convert.ToInt32(id) - 1;

            return new(slot * 64, 0, 64, 64);

        }

        public TemporaryAnimatedSprite DecorativeIndicator(GameLocation location, Vector2 origin, decorations decorationId, float scale, DecorativeAdditional additional)
        {

            Vector2 originOffset = origin + new Vector2(32f, 32f) - (new Vector2(32f, 32f) * scale);

            Microsoft.Xna.Framework.Rectangle rect = DecorativeRectangle(decorationId);

            float interval = additional.interval;

            float rotation = 0;

            if (additional.rotation > 0)
            {

                rotation = (float)(Math.PI / additional.rotation);

            }

            float alpha = additional.alpha;

            int delay = additional.delay;

            int loops = additional.loops;

            float layer = (originOffset.Y - 32) / 10000;

            TemporaryAnimatedSprite animation = new(0, interval, 1, loops, originOffset, false, false)
            {

                sourceRect = rect,

                sourceRectStartingPos = new Vector2(rect.X, rect.Y),

                texture = decorationTexture,

                scale = scale,

                timeBasedMotion = true,

                layerDepth = layer,

                rotationChange = rotation,

                Parent = location,

                alpha = alpha,

                delayBeforeAnimationStart = delay,

            };

            location.temporarySprites.Add(animation);

            return animation;

        }

        public void ImpactIndicator(GameLocation location, Vector2 origin, impacts impact, float size, ImpactAdditional additional)
        {


            switch (impact)
            {

                case impacts.none:

                    return;

                case impacts.impact:

                    additional.alpha = 0.85f;

                    break;

                case impacts.glare:

                    additional.light = 0.01f;

                    break;

                case impacts.splatter:

                    size += 0.5f;

                    additional.color = SchemeColour(additional.scheme);

                    additional.alpha = 0.7f;

                    break;

                case impacts.puff:

                    additional.color = SchemeColour(additional.scheme);

                    size = Math.Max(Math.Min(5, size), 3);

                    break;

                case impacts.boltnode:

                    additional.flip = Mod.instance.randomIndex.NextBool();

                    additional.rotation = (float)Mod.instance.randomIndex.Next(2);

                    additional.alpha = 0.75f;

                    break;

                case impacts.sparknode:

                    additional.flip = Mod.instance.randomIndex.NextBool();

                    additional.alpha = 1f;

                    additional.frames = 4;

                    additional.frame = Mod.instance.randomIndex.Next(2) * 4;

                    size = 4;

                    break;

                case impacts.sparkbang:

                    size = Math.Min(size, 3);

                    additional.frame = 2;

                    additional.layerOffset = 0.001f;

                    additional.alpha = 0.75f;

                    additional.color = SchemeColour(additional.scheme);

                    CreateImpact(location, origin, impact, size, additional);
                    
                    return;

                case impacts.skull:

                    additional.alpha = 0.8f;

                    additional.color = schemeColours[schemes.death];

                    additional.alphaFade = 0.001f;

                    CreateImpact(location, origin, IconData.impacts.skull, size + 1, additional);

                    additional.alphaFade = 0f;

                    additional.alpha = 0.25f;

                    additional.light = 0f;

                    additional.layerOffset = -0.001f;

                    additional.color = Microsoft.Xna.Framework.Color.White;

                    CreateImpact(location, origin, IconData.impacts.puff, size, additional);

                    return;

                case impacts.deathbomb:

                    additional.alpha = 0.8f;

                    additional.color = schemeColours[schemes.death];

                    additional.alphaFade = 0.001f;

                    CreateImpact(location, origin, IconData.impacts.skull, size + 1, additional);

                    additional.alphaFade = 0f;

                    additional.alpha = 0.25f;

                    additional.light = 0f;

                    additional.layerOffset = -0.001f;

                    additional.color = Microsoft.Xna.Framework.Color.White;

                    CreateImpact(location, origin, IconData.impacts.puff, size, additional);

                    additional.layerOffset = -0.002f;

                    CreateImpact(location, origin + new Vector2(0, -192), IconData.impacts.smoke, 3f, additional);

                    CreateImpact(location, origin + new Vector2(-160, -48), IconData.impacts.steam, 3f, additional);

                    additional.flip = true;

                    CreateImpact(location, origin + new Vector2(160, -48), IconData.impacts.steam, 3f, additional);

                    CreateImpact(location, origin + new Vector2(0, 48), IconData.impacts.smoke, 3f, additional);

                    return;

                case impacts.fish:
                case impacts.splash:

                    additional.alpha = 0.35f;

                    break;

                case impacts.clouds:

                    additional.girth = 2;

                    break;

                case impacts.bomb:

                    additional.alpha = 0.5f;

                    additional.light = 0f;

                    additional.layerOffset = 0.001f;

                    CreateImpact(location, origin, IconData.impacts.puff, size + 1, additional);

                    additional.light = 0.005f;

                    additional.layerOffset = 0f;

                    additional.alpha = 0.7f;

                    CreateImpact(location, origin, IconData.impacts.impact, size, additional);

                    return;

                case impacts.supree:

                    if(additional.alpha == 0.35f)
                    {

                        additional.alpha = 0.45f;

                    }

                    additional.layer = 700f;

                    additional.color = gradientColours[additional.scheme][0];

                    additional.light = 0f;

                    CreateImpact(location, origin, IconData.impacts.sparkle, size, additional);

                    additional.color = gradientColours[additional.scheme][1];

                    additional.light = 0.005f;

                    CreateImpact(location, origin, IconData.impacts.sparkle, size+2, additional);


                    return;

                case impacts.spiral:

                    CreateSwirl(location, origin, Math.Max(4f,size), additional);

                    return;

                case impacts.mists:

                    AnimateMistic(origin,(int)size);

                    return;

                case impacts.summoning:

                    CircleIndicator(location, origin, circles.summoning, size, new() { color = schemeColours[additional.scheme],});

                    return;

                case impacts.shockwave:

                    CreateImpact(location, origin + new Vector2(0, -192), IconData.impacts.smoke, 3f, additional);

                    CreateImpact(location, origin + new Vector2(-160, -48), IconData.impacts.steam, 3f, additional);

                    additional.flip = true;

                    CreateImpact(location, origin + new Vector2(160, -48), IconData.impacts.steam, 3f, additional);

                    CreateImpact(location, origin + new Vector2(0, 48), IconData.impacts.smoke, 3f, additional);

                    return;

                case impacts.lovebomb:

                    additional.alpha = 0.5f;

                    additional.light = 0f;

                    CreateImpact(location, origin, IconData.impacts.puff, size + 1, additional);

                    additional.layerOffset = 0.001f;

                    additional.alpha = 0.7f;

                    CreateImpact(location, origin, IconData.impacts.plume, size, additional);

                    additional.light = 0.005f;

                    additional.layerOffset = 0.002f;

                    additional.alpha = 0.9f;

                    additional.color = schemeColours[schemes.herbal_impes];

                    CreateImpact(location, origin, IconData.impacts.love, size, additional);

                    return;

                case impacts.flasher:

                    additional.alpha = 0.45f;

                    CreateImpact(location, origin, IconData.impacts.flashbang, size, additional);

                    additional.light = 0f;

                    additional.layerOffset = 0.001f;

                    additional.frames = 6;

                    additional.frame = 2;

                    additional.color = SchemeColour(additional.scheme);

                    additional.delay = 150;

                    CreateImpact(location, origin - new Vector2(8 * size), IconData.impacts.sparkbang, size-1, additional);

                    additional.color = Microsoft.Xna.Framework.Color.White;

                    additional.delay = 300;

                    CreateImpact(location, origin + new Vector2(8*size), IconData.impacts.sparkbang, size-1, additional);

                    additional.color = SchemeColour(additional.scheme);

                    additional.delay = 450;

                    CreateImpact(location, origin + new Vector2(12 * size, -12 * size), IconData.impacts.sparkbang, size-1, additional);

                    additional.color = Microsoft.Xna.Framework.Color.White;

                    additional.delay = 600;

                    CreateImpact(location, origin + new Vector2(-12 * size, 12 * size), IconData.impacts.sparkbang, size-1, additional);

                    return;



            }

            CreateImpact(location, origin, impact, size, additional);


        }

        public int ImpactIndex(impacts impact)
        {

            int Y = (Convert.ToInt32(impact) - 1);

            return Y % 6;

        }

        public Texture2D ImpactSheet(impacts impact)
        {

            Texture2D sheet = impactsTexture;

            int Y = (Convert.ToInt32(impact) - 1);

            if (Y >= 6 && Y < 12)
            {

                sheet = impactsTextureTwo;

            }
            else if (Y >= 12 && Y < 18)
            {

                sheet = impactsTextureThree;

            }

            return sheet;

        }

        public TemporaryAnimatedSprite CreateImpact(GameLocation location, Vector2 origin, impacts impact, float scale, ImpactAdditional additional)
        {

            Vector2 originOffset = origin + new Vector2(32f, 32f) - (new Vector2(32f * additional.girth, 32f) * scale );

            float interval = additional.interval;

            Microsoft.Xna.Framework.Color color = additional.color;

            float rotation = 0;

            if (additional.rotation > 0)
            {

                rotation = (float)(Math.PI / additional.rotation);

            }

            float layer = additional.layer;

            if (additional.layer == -1)
            {

                layer = originOffset.Y / 10000 + (scale * 0.0064f);

            }

            layer += additional.layerOffset;

            Texture2D sheet = ImpactSheet(impact);

            int Y = ImpactIndex(impact);

            Microsoft.Xna.Framework.Rectangle source = new((64) * additional.frame, Y * 64, 64 * additional.girth, 64);

            TemporaryAnimatedSprite bomb = new(0, interval, additional.frames, additional.loops, originOffset, false, false)
            {
                sourceRect = source,
                sourceRectStartingPos = new Vector2(source.X, source.Y),
                texture = sheet,
                scale = scale,
                timeBasedMotion = true,
                layerDepth = layer,
                color = color,
                rotation = rotation,
                alpha = additional.alpha,
                alphaFade = additional.alphaFade,
                flipped = additional.flip,
                delayBeforeAnimationStart = additional.delay,
            };

            location.temporarySprites.Add(bomb);

            if (additional.light > 0f)
            {

                string lightid = "18465_" + (origin.X * 10000 + origin.Y).ToString();

                TemporaryAnimatedSprite flash = new(23, 500f, 6, 1, origin, false, Game1.random.NextDouble() < 0.5)
                {
                    texture = Game1.mouseCursors,
                    lightId = lightid,
                    lightRadius = Math.Max(0.5f,scale * 0.3f),
                    lightcolor = Microsoft.Xna.Framework.Color.Black,
                    alphaFade = additional.light,
                    Parent = location
                };

                location.temporarySprites.Add(flash);

            }

            return bomb;

        }

        public TemporaryAnimatedSprite CreateSwirl(GameLocation location, Vector2 origin, float scale, ImpactAdditional additional)
        {

            Vector2 originOffset = origin + new Vector2(32f, 32f) - (new Vector2(64, 64) * scale);

            float layer = additional.layer;

            if (additional.layer == -1)
            {

                layer = originOffset.Y / 10000;

            }

            layer += additional.layerOffset;

            Microsoft.Xna.Framework.Rectangle source = new((128) * additional.frame, 0,128, 128);

            TemporaryAnimatedSprite bomb = new(0, additional.interval, additional.frames, additional.loops, originOffset, false, false)
            {
                color = additional.color,
                sourceRect = source,
                sourceRectStartingPos = new Vector2(source.X, source.Y),
                texture = swirlsTexture,
                scale = scale,
                timeBasedMotion = true,
                layerDepth = layer,
                alpha = additional.alpha,
                flipped = additional.flip,
                delayBeforeAnimationStart = additional.delay,

            };

            location.temporarySprites.Add(bomb);

            return bomb;

        }

        public static Microsoft.Xna.Framework.Rectangle SkyRectangle(skies sky)
        {

            return new((((int)sky-1) % 6) * 64, (((int)sky-1) / 6) *64, 64, 64);

        }

        public TemporaryAnimatedSprite SkyIndicator(GameLocation location, Vector2 origin, skies sky, float scale, SkyAdditional additional)
        {

            Vector2 originOffset = origin + new Vector2(32f, 32f) - (new Vector2(32f, 32f) * scale);

            if (sky == skies.none)
            {

                return null;

            }

            float interval = additional.interval;

            float alpha = additional.alpha;

            int delay = additional.delay;

            int loops = additional.loops;

            float layer = originOffset.Y / 10000;

            Microsoft.Xna.Framework.Rectangle skyRectangle = SkyRectangle(sky);

            TemporaryAnimatedSprite skyAnimation= new(0, interval, 1, loops, originOffset, false, false)
            {

                sourceRect = skyRectangle,

                sourceRectStartingPos = new Vector2(skyRectangle.X,skyRectangle.Y),

                texture = skyTexture,

                scale = scale,

                layerDepth = layer,

                alpha = alpha,

                delayBeforeAnimationStart = delay,

            };

            location.temporarySprites.Add(skyAnimation);

            return skyAnimation;

        }

        public static Microsoft.Xna.Framework.Rectangle CircleRectangle(circles circle)
        {

            return new(((int)circle - 1) * 144, 0, 144, 144);

        }

        public TemporaryAnimatedSprite CircleIndicator(GameLocation location, Vector2 origin, circles circle, float scale, CircleAdditional additional)
        {

            Vector2 originOffset = origin + new Vector2(32f, 32f) - (new Vector2(72f, 72f) * scale);

            if (circle == circles.none)
            {

                return null;

            }

            float layer = 0.0002f;

            Microsoft.Xna.Framework.Rectangle circleRectangle = CircleRectangle(circle);

            TemporaryAnimatedSprite circleAnimation = new(0, additional.interval, 1, additional.loops, originOffset, false, false)
            {

                sourceRect = circleRectangle,

                sourceRectStartingPos = new Vector2(circleRectangle.X, circleRectangle.Y),

                texture = circleTexture,

                color = additional.color,

                scale = scale,

                layerDepth = layer,

                alpha = additional.alpha,

                delayBeforeAnimationStart = additional.delay,

                timeBasedMotion = true,

                alphaFade = additional.fade,

            };

            location.temporarySprites.Add(circleAnimation);

            return circleAnimation;

        }

        public void AnimateQuickWarp(GameLocation location, Vector2 origin, bool reverse = false, warps warp = warps.portal, schemes scheme = schemes.none)
        {

            TemporaryAnimatedSprite animation;

            Microsoft.Xna.Framework.Rectangle rect;

            Vector2 originOffset;

            switch (warp)
            {
                default:
                case warps.portal:

                    originOffset = origin - new Vector2(32, 32);

                    rect = reverse ? new(0, 32, 32, 32) : new(0, 0, 32, 32);

                    animation = new(0, 75, 8, 1, originOffset, false, false)
                    {

                        sourceRect = rect,

                        sourceRectStartingPos = new Vector2(0, rect.Y),

                        texture = warpTexture,

                        scale = 4f,

                        layerDepth = 0.001f,

                        alpha = 0.65f,

                    };

                    location.temporarySprites.Add(animation);

                    break;

                case warps.circle:

                    rect = CircleRectangle((circles)1);

                    animation = new(0, 5000, 1, 1, origin + new Vector2(32) - new Vector2(rect.Width * 2), false, false)
                    {

                        sourceRect = rect,

                        sourceRectStartingPos = new Vector2(rect.X, rect.Y),

                        color = schemeColours[scheme],

                        texture = circleTexture,

                        scale = 4f,

                        layerDepth = 0.001f,

                        alpha = 0.6f,

                        alphaFade = 0.0003f,

                        timeBasedMotion = true

                    };

                    location.temporarySprites.Add(animation);

                    break;

                case warps.smoke:

                    CreateImpact(location, origin, impacts.plume, 3f, new() { alpha = 1f, });

                    CreateImpact(location, origin, impacts.smoke, 2f, new() { alpha = 1f, });

                    break;

                case warps.corvids:

                    CreateImpact(location, origin, impacts.plume, 2f, new() { color = schemeColours[schemes.bones], alpha = 1f, });

                    break;
            }

        }

        public void AnimateMistic()
        {

            AnimateMistic(Game1.player.Position - new Vector2(96, 128));

        }

        public void AnimateMistic(Vector2 position, int amount = 4)
        {
            
            Vector2 mistCorner = position;

            for (int i = 0; i < amount; i++)
            {

                for (int j = 0; j < amount; j++)
                {

                    if ((i == 0 || i == 5) && (j == 0 || j == 5))
                    {
                        continue;
                    }

                    Vector2 glowVector = mistCorner + new Vector2(i * 32, j * 32);

                    TemporaryAnimatedSprite glowSprite = new TemporaryAnimatedSprite(0, 3000f, 1, 1, glowVector, false, false)
                    {
                        sourceRect = new Microsoft.Xna.Framework.Rectangle(88, 1779, 30, 30),
                        sourceRectStartingPos = new Vector2(88, 1779),
                        texture = Game1.mouseCursors,
                        motion = new(0.016f * (Mod.instance.randomIndex.Next(2) == 0 ? 1 : -1) * Mod.instance.randomIndex.Next(1, 4), 0.016f * (Mod.instance.randomIndex.Next(2) == 0 ? 1 : -1) * Mod.instance.randomIndex.Next(1, 4)),
                        scale = 4f,
                        layerDepth = 999f,
                        timeBasedMotion = true,
                        alpha = 0.7f,
                        alphaFade = 0.0005f,
                        color = new Microsoft.Xna.Framework.Color(0.75f, 0.75f, 1f, 1f),
                    };

                    Game1.player.currentLocation.temporarySprites.Add(glowSprite);

                }

            }

        }

    }

    public class CursorAdditional
    {

        public float interval = 1200f;

        public float alpha = 0.65f;

        public float fade = 0f;

        public float rotation = 60;

        public int delay = 0;

        public float scale = 3f;

        public int loops = 1;

        public float layer = -1f;

        public IconData.schemes scheme = IconData.schemes.none;

    }

    public class DecorativeAdditional
    {

        public float interval = 1000f;

        public float alpha = 0.65f;

        public float rotation = 120;

        public int delay = 0;

        public int loops = 1;

    }

    public class ImpactAdditional
    {

        public int frame = 0;

        public int frames = 8;

        public float interval = 100f;

        public int girth = 1;

        public int loops = 1;

        public float alpha = 0.35f;

        public float alphaFade = 0f;

        public float rotation = 0;

        public Microsoft.Xna.Framework.Color color = Microsoft.Xna.Framework.Color.White;

        public List<Microsoft.Xna.Framework.Color> colours = new();

        public int delay = 0;

        public float light = 0.03f;

        public bool flip = false;

        public float layer = -1;

        public float layerOffset = 0f;

        public IconData.schemes scheme = IconData.schemes.none;

    }

    public class SkyAdditional
    {

        public float interval = 1000f;

        public float alpha = 0.75f;

        public int delay = 0;

        public int loops = 1;

    }

    public class CircleAdditional
    {

        public float interval = 3000f;

        public float alpha = 0.99f;

        public int delay = 0;

        public int loops = 1;

        public float fade = 0.00033f;

        public Microsoft.Xna.Framework.Color color = Microsoft.Xna.Framework.Color.White;

    }

}
