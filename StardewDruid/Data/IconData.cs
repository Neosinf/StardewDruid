using Force.DeepCloner;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewDruid.Cast;
using StardewDruid.Dialogue;
using StardewDruid.Render;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Companions;
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
using xTile.Dimensions;
using xTile.Layers;

namespace StardewDruid.Data
{
    public class IconData
    {

        public enum cursors
        {
            none,

            winds1,
            winds2,
            winds3,

            weald,
            mists,
            stars,
            fates,

            empty,
            empty1,
            empty2,
            whisk,

            scope,
            death,
            shadow,
            target,

            divine,
            feathers,

        }

        public Texture2D cursorTexture;

        public Texture2D shadowTexture;

        public enum displays
        {
            none,

            weald,
            mists,
            stars,
            fates,
            ether,
            druid,

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
            skills,
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

            witch,
            skip,
            save,
            aldebaran,
            question,
            bombs,

            powderbox,
            heroes,
            holy,
            orders,
            goods,
            shield,

            vitality,
            strength,
            speed,
            trophies,
            axe,
            pickaxe,
            
            hoe,
            watercan,
            fatigue,
            fullheart,
            halfheart,
            sword,

            chargemists,
            chargestars,
            chargefates,
            chargewitch,
            tree,
            winds,

            omens,
            alchemy,
            effects,
            lunchbox,
            voide,
            levelup,

            malt,
            must,
            tribute,
            nectar,
            labour,
            heavy,

            special,
            materials,
            previous,
            attunement,
            craft,
            noselect,

            inventory,
            sell,
            upgrade,
            guilds,

        }

        public Texture2D displayTexture;

        public enum ritecircles
        {
            none,
            druid,
            winds,
            weald,
            mists,
            stars,
            voide,
            fates,
            ether,
            witch,

        }

        public Dictionary<Rite.Rites, ritecircles> riteDecorations = new()
        {

            [Rite.Rites.weald] = ritecircles.weald,

            [Rite.Rites.mists] = ritecircles.mists,

            [Rite.Rites.stars] = ritecircles.stars,

            [Rite.Rites.fates] = ritecircles.fates,

            [Rite.Rites.ether] = ritecircles.ether,

            [Rite.Rites.witch] = ritecircles.witch,

        };

        public Dictionary<Rite.Rites, displays> riteDisplays = new()
        {

            [Rite.Rites.none] = displays.druid,

            [Rite.Rites.weald] = displays.weald,

            [Rite.Rites.mists] = displays.mists,

            [Rite.Rites.stars] = displays.stars,

            [Rite.Rites.fates] = displays.fates,

            [Rite.Rites.ether] = displays.ether,

            [Rite.Rites.witch] = displays.witch,

        };

        public Texture2D ritecircleTexture;

        public enum impacts
        {
            none,

            impact,
            flashbang,
            glare,
            sparkle,
            splash,
            fish,

            boltnode,
            sparknode,
            clouds,
            puff,
            pop,
            skull,

            plume,
            steam,
            smoke,
            love,
            slashes,
            critical,

            splat,
            splatter,
            splattwo,
            splattertwo,
            bomb,
            holy,

            slash,
            wave,
            drops,
            dust,
            empty1,
            empty2,

            supree,
            dustimpact,
            bigimpact,
            holyimpact,
            mists,
            summoning,
            shockwave,
            lovebomb,
            flasher,
            spiral,
            deathbomb,
            grasp,
            roots,
            megaslash,

            aard,
            igni,
            axii,
            quen,
            yrden,

        }

        public Texture2D impactsTexture;

        public Texture2D impactsTextureTwo;

        public Texture2D impactsTextureThree;

        public Texture2D impactsTextureFour;

        public Texture2D impactsTextureFive;

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

        public Texture2D witchersignsTexture;

        public enum tilesheets
        {
            none,
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
            pavement,
            ritual,
            access,
            sanctuary,
            temple,

            groundspring,
            groundsummer,
            groundautumn,
            groundwinter,
            
            cavern,
            cavernwater,
            cavernground,

            moors,
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
            vessels,
            candles,
            mushrooms,
            waterflowers,

            ray

        }

        public Dictionary<tilesheets, Texture2D> sheetTextures = new();

        public enum relics
        {
            none,

            companion_crest,
            companion_badge,
            companion_dice,
            companion_tome,
            companion_glove,
            stardew_druid,

            lantern_pot,
            lantern_censer,
            lantern_guardian,
            lantern_water,
            lantern_ceremony,
            blank_lantern_6,

            wayfinder_stone,
            wayfinder_key,
            wayfinder_glove,
            wayfinder_eye,
            blank_wayfinder_5,
            blank_wayfinder_6,

            druid_apothecary,
            herbalism_mortar,
            herbalism_pan,
            herbalism_still,
            herbalism_crucible,
            herbalism_gauge,

            druid_grimoire,
            druid_runeboard,
            druid_hammer,
            druid_dragonomicon,
            druid_hieress,
            blank_druid_6,

            tactical_discombobulator,
            tactical_mask,
            tactical_cell,
            tactical_lunchbox,
            tactical_peppermint,
            blank_tactical_6,

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
            book_annal,
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
            restore_cloth,
            restore_3,
            restore_4,
            restore_5,

            monster_bat,
            monster_slime,
            monster_spirit,
            monster_ghost,
            monster_serpent,
            monster_six,

            crest_church,
            crest_dwarf,
            crest_associate,
            crest_smuggler,
            crest_4,
            crest_5,

            shadowtin_cell,
            shadowtin_bazooka,
            shadowtin_chart,
            shadowtin_4,
            shadowtin_5,
            shadowtin_6,

            skills_druid,
            skills_alchemy,
            skills_industry,
            skills_community,
            skills_curiosity,
            skills_6,

        }

        public Texture2D relicsTexture;

        public Texture2D itemsTexture;

        public Texture2D exportTexture;

        public Texture2D masteryTexture;

        public enum warps
        {

            portal,
            death,
            capture,
            smoke,
            corvids,
            circle,
            mist,

        }

        public Texture2D missileTexture;

        public Texture2D missileTextureTwo;

        public Texture2D boltTexture;

        public Texture2D laserTexture;

        public Texture2D lightningTexture;

        public Texture2D judgementTexture;

        public Texture2D artemisTexture;

        public Texture2D graspTexture;

        public Texture2D warpTexture;

        public Texture2D gravityTexture;

        public Texture2D emberTexture;

        public Texture2D crateTexture;

        public Texture2D echoTexture;

        public Texture2D wispTexture;

        public Texture2D windwispTexture;

        public Texture2D deathwispTexture;

        public Texture2D shieldTexture;

        public Texture2D blobTexture;

        public Texture2D hatTexture;

        public Microsoft.Xna.Framework.Rectangle shadowRectangle;

        public double glyphTime;

        public string glyphSpot;

        public enum schemes
        {
            none,

            winds,
            weald,
            mists,
            stars,
            fates,
            ether,
            witch,

            death,
            golden,
            snazzle,
            stardew,
            wisps,
            white,
            gray,
            darkgray,

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

            [schemes.winds] = new(96, 116, 130),
            [schemes.weald] =  Microsoft.Xna.Framework.Color.Green,
            [schemes.mists] = new(75, 138, 187),
            [schemes.stars] =  new(231, 102, 84),
            [schemes.fates] = new(119, 75, 131),
            [schemes.ether] = new(13, 114, 185),
            [schemes.witch] = new(81, 115, 131),

            [schemes.death] = new(119, 201, 177),
            [schemes.golden] = new(251, 201, 38),
            [schemes.snazzle] = new(214, 0, 175),
            [schemes.stardew] = new(210,164,89),
            [schemes.white] = Microsoft.Xna.Framework.Color.White,
            [schemes.gray] = Microsoft.Xna.Framework.Color.Gray,
            [schemes.darkgray] = Microsoft.Xna.Framework.Color.DarkGray,

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
            [schemes.witch] = new() { new(81, 115, 131), new(51, 85, 101), new(21, 55, 71), },
            

            [schemes.death] = new() { new(109, 175, 156), new(119, 201, 177),  new(162, 216, 202), },
            [schemes.golden] = new() { new(255, 250, 194), new(255, 232, 155), new(251, 201, 38), },
            [schemes.snazzle] = new() { new(214, 0, 175), new(155, 0, 126), new(37, 34, 74), },
            [schemes.wisps] = new() { new(75, 138, 187), new(69, 186, 235), new(74, 243, 255), new(186, 225, 255), },

        };

        public IconData()
        {

            cursorTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Cursors.png"));

            masteryTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Masteries.png"));

            shadowTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Shadow.png"));

            displayTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Displays.png"));

            ritecircleTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "RiteCircles.png"));

            emberTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Embers.png"));

            impactsTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Impacts.png"));

            impactsTextureTwo = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "ImpactsTwo.png"));

            impactsTextureThree = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "ImpactsThree.png"));

            impactsTextureFour = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "ImpactsFour.png"));

            impactsTextureFive = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "ImpactsFive.png"));

            swirlsTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Swirl.png"));

            skyTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Skies.png"));

            circleTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Circle.png"));

            witchersignsTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "WitcherSigns.png"));


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

            
            sheetTextures[tilesheets.temple] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Temple.png"));

            sheetTextures[tilesheets.sanctuary] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Sanctuary.png"));

            sheetTextures[tilesheets.ritual] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Ritual.png"));

            sheetTextures[tilesheets.access] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Access.png"));


            sheetTextures[tilesheets.groundspring] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundSpring.png"));

            sheetTextures[tilesheets.groundsummer] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundSummer.png"));

            sheetTextures[tilesheets.groundautumn] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundAutumn.png"));

            sheetTextures[tilesheets.groundwinter] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundWinter.png"));


            sheetTextures[tilesheets.cavern] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Cavern.png"));

            sheetTextures[tilesheets.cavernwater] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "CavernWater.png"));

            sheetTextures[tilesheets.cavernground] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "CavernGround.png"));


            sheetTextures[tilesheets.moors] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Moors.png"));

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

            sheetTextures[tilesheets.vessels] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Vessels.png"));

            sheetTextures[tilesheets.candles] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Candles.png"));

            sheetTextures[tilesheets.mushrooms] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Mushrooms.png"));

            sheetTextures[tilesheets.waterflowers] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Waterflower.png"));
            
            sheetTextures[tilesheets.ray] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Ray.png"));


            relicsTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Relics.png"));

            itemsTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Potions.png"));

            exportTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Workshop.png"));


            missileTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Missiles.png"));

            missileTextureTwo = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "MissilesTwo.png"));

            boltTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Bolt.png"));

            laserTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Laser.png"));

            lightningTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Lightning.png"));

            artemisTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Artemis.png"));

            judgementTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Judgement.png"));

            warpTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Warp.png"));

            gravityTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Gravity.png"));

            graspTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Grasp.png"));

            crateTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "DragonCrate.png"));

            echoTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Echo.png"));

            wispTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Wisp.png"));

            windwispTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "WindWisp.png"));

            deathwispTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "DeathWisp.png"));

            shieldTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Shield.png"));

            blobTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Blob.png"));

            hatTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Hats.png"));

            shadowRectangle = new Microsoft.Xna.Framework.Rectangle(0, 0, 32, 32);

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

                
                case tilesheets.temple: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Temple.png"));

                case tilesheets.ritual: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Ritual.png"));

                case tilesheets.access: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Access.png"));


                case tilesheets.groundspring: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundSpring.png"));

                case tilesheets.groundsummer: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundSummer.png"));

                case tilesheets.groundautumn: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundAutumn.png"));

                case tilesheets.groundwinter: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "GroundWinter.png"));


                case tilesheets.cavern: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Cavern.png"));

                case tilesheets.cavernwater: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "CavernWater.png"));

                case tilesheets.cavernground: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "CavernGround.png"));


                case tilesheets.moors: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Moors.png"));

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

                case tilesheets.vessels: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Vessels.png"));

                case tilesheets.candles: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Candles.png"));

                case tilesheets.mushrooms: return Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Mushrooms.png"));

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

        public static ritecircles SchemeToCircle(schemes scheme)
        {

            switch (scheme)
            {
                default: return ritecircles.druid;

                case schemes.winds:

                    return ritecircles.winds;

                case schemes.weald:

                    return ritecircles.weald;
            }

        }

        public static Microsoft.Xna.Framework.Rectangle CursorRectangle(cursors id, int frame = 0)
        {

            if (id == cursors.none) { return new(); }

            int slot = (int)id - 1;

            return new(frame * 24, slot * 24, 24, 24);


        }

        public TemporaryAnimatedSprite CursorIndicator(GameLocation location, Vector2 origin, cursors cursorId, CursorAdditional additional)
        {

            Vector2 originOffset = origin - new Vector2(12 * additional.scale, 12 * additional.scale);

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

            /*if (additional.rotation > 0)
            {

                animation.rotationChange = (float)(Math.PI / additional.rotation);

            }*/

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

        public Microsoft.Xna.Framework.Rectangle QuestDisplay(Quest.questTypes questType)
        {
            switch (questType)
            {
                case Quest.questTypes.challenge:

                    return DisplayRectangle(displays.quest);

                case Quest.questTypes.lesson:

                    return DisplayRectangle(displays.skills);

                case Quest.questTypes.miscellaneous:

                    return DisplayRectangle(displays.active);

                default:

                    return DisplayRectangle(displays.speech);

            }

        }

        public static Microsoft.Xna.Framework.Rectangle RiteRectangle(ritecircles id)
        {

            if (id == ritecircles.none) { return new(); }

            int slot = Convert.ToInt32(id) - 1;

            return new(0, slot * 64, 80, 64);

        }

        public TemporaryAnimatedSprite RiteCircle(ritecircles circleId)
        {

            return RiteCircle(Game1.player.currentLocation, Game1.player.Position + new Vector2(32), circleId, 4f, new() { interval = 2000, loops = 1, fade = 0.0005f});

        }

        public TemporaryAnimatedSprite RiteCircle(GameLocation location, Vector2 origin, ritecircles circleId, float scale, CircleAdditional additional)
        {

            Vector2 originOffset = origin - (new Vector2(40f, 32f) * scale);

            Microsoft.Xna.Framework.Rectangle rect = RiteRectangle(circleId);

            if(additional.layer < 0f)
            {

                additional.layer = (originOffset.Y - 32) / 10000;

            }

            TemporaryAnimatedSprite animation = new(0, additional.interval, 1, additional.loops, originOffset, false, false)
            {

                sourceRect = rect,

                sourceRectStartingPos = new Vector2(rect.X, rect.Y),

                texture = ritecircleTexture,

                scale = scale,

                layerDepth = additional.layer,

                Parent = location,

                alpha = additional.alpha,

                alphaFade = additional.fade,

                delayBeforeAnimationStart = additional.delay,

                timeBasedMotion = true,

            };

            location.temporarySprites.Add(animation);

            Microsoft.Xna.Framework.Rectangle recttwo = new(rect.X + 80,rect.Y,rect.Width,rect.Height);

            TemporaryAnimatedSprite animationtwo = new(0, additional.interval, 1, additional.loops, originOffset, false, false)
            {

                sourceRect = recttwo,

                sourceRectStartingPos = new Vector2(recttwo.X, recttwo.Y),

                texture = ritecircleTexture,

                scale = scale,

                layerDepth = additional.layer,

                Parent = location,

                alpha = additional.alpha / 3,

                alphaFade = additional.fade / 3,

                delayBeforeAnimationStart = additional.delay,

                timeBasedMotion = true,

            };

            location.temporarySprites.Add(animationtwo);

            return animation;

        }

        public void ImpactIndicator(GameLocation location, Vector2 origin, impacts impact, float size, ImpactAdditional additional)
        {


            switch (impact)
            {

                case impacts.none:

                    return;

                case impacts.impact:
                case impacts.bomb:

                    additional.alpha = 0.85f;

                    break;

                case impacts.glare:

                    additional.light = 0.01f;

                    break;

                case impacts.splat:
                case impacts.splatter:

                    additional.alpha = 0.8f;

                    additional.layer -= 0.0128f;

                    break;

                case impacts.puff:

                    additional.color = SchemeColour(additional.scheme);

                    size = Math.Max(Math.Min(5, size), 3);

                    break;

                case impacts.boltnode:

                    additional.flip = Mod.instance.randomIndex.NextBool();

                    additional.rotation = Mod.instance.randomIndex.NextBool() ? (float)(Math.PI/2) : 0;

                    additional.alpha = 0.75f;

                    break;

                case impacts.sparknode:
                case impacts.slashes:
                case impacts.critical:

                    additional.flip = Mod.instance.randomIndex.NextBool();

                    additional.frames = 4;

                    additional.frame = Mod.instance.randomIndex.Next(2) * 4;

                    additional.interval = 75;

                    additional.alpha = 0.5f;

                    additional.color = SchemeColour(additional.scheme);

                    break;

                case impacts.drops:

                    additional.alphaFade = 0.001f;

                    break;

                case impacts.skull:

                    ImpactAdditional skullAdditional = additional.Copy();

                    skullAdditional.alpha = 0.8f;

                    skullAdditional.color = schemeColours[schemes.death];

                    skullAdditional.alphaFade = 0.001f;

                    CreateImpact(location, origin, IconData.impacts.skull, size + 1, skullAdditional);

                    //-------------------------------------

                    CreateDustup(location, origin, size, additional);

                    CreateSmoke(location, origin, size, additional);

                    return;

                case impacts.deathbomb:

                    additional.alpha = 0.8f;

                    additional.color = schemeColours[schemes.death];

                    additional.alphaFade = 0.001f;

                    CreateImpact(location, origin, IconData.impacts.skull, size + 1, additional);

                    //-------------------------------------

                    CreateDustup(location, origin, size, additional);

                    CreateSmoke(location, origin, size, additional);

                    return;

                case impacts.megaslash:

                    additional.frames = 4;

                    additional.frame = Mod.instance.randomIndex.Next(2) == 0 ? 0 : 4;

                    additional.flip = Mod.instance.randomIndex.Next(2) == 0;

                    additional.interval = 75;

                    additional.alpha = 0.5f;

                    additional.delay = 225;

                    CreateImpact(location, origin, IconData.impacts.critical, 3f, additional);

                    ImpactAdditional megaslashAdditional = additional.Copy();

                    megaslashAdditional.frames = 4;

                    additional.frame = Mod.instance.randomIndex.Next(2) == 0 ? 0 : 4;

                    megaslashAdditional.flip = Mod.instance.randomIndex.Next(2) == 0;

                    megaslashAdditional.light = 0f;

                    megaslashAdditional.color = schemeColours[megaslashAdditional.scheme];

                    megaslashAdditional.alpha = 0.4f;

                    CreateImpact(location, origin, IconData.impacts.slashes, 2.5f, megaslashAdditional);

                    return;

                case impacts.fish:
                case impacts.splash:

                    additional.alpha = 0.35f;

                    break;

                case impacts.clouds:

                    additional.girth = 2;

                    break;

                case impacts.dustimpact:

                    additional.light = 0.005f;

                    additional.layerOffset = 0f;

                    additional.alpha = 0.8f;

                    CreateImpact(location, origin, IconData.impacts.impact, size, additional);

                    //-------------------------------------

                    CreateDustup(location, origin, size, additional);

                    return;

                case impacts.bigimpact:

                    //--------------------------------

                    additional.light = 0.005f;

                    additional.layerOffset = 0f;

                    additional.alpha = 0.8f;

                    additional.delay = 0;

                    CreateImpact(location, origin, IconData.impacts.bomb, size, additional);

                    //-------------------------------------

                    CreateDustup(location, origin, size, additional);

                    CreateSmoke(location, origin, size, additional);

                    return;

                case impacts.holyimpact:

                    additional.light = 0.005f;

                    additional.layerOffset = 0f;

                    additional.alpha = 0.8f;

                    additional.delay = 0;

                    CreateImpact(location, origin, IconData.impacts.holy, size, additional);

                    //-------------------------------------

                    CreateDustup(location, origin, size, additional);

                    CreateSparkles(location, origin, size, additional);

                    return;

                case impacts.supree:

                    //-------------------------------------

                    CreateSupree(location, origin, size, additional);

                    CreateSparklesTwo(location, origin, size, additional);

                    return;

                case impacts.spiral:

                    additional.interval = 115;

                    CreateSwirl(location, origin, Math.Max(4f,size), additional);

                    return;

                case impacts.mists:

                    additional.light = -1f;

                    additional.girth = 2;

                    if(additional.interval == 100f)
                    {

                        additional.interval = 4000f;

                        additional.alpha = 0.2f;

                    }

                    additional.alphaFade = additional.alpha / additional.interval;

                    additional.frames = 1;

                    additional.layer = -1f;

                    additional.frame = Mod.instance.randomIndex.Next(4) * 2;

                    additional.motion = new Vector2(-0.016f, 0);

                    additional.flip = false;

                    CreateImpact(location, origin, IconData.impacts.clouds, size, additional);

                    ImpactAdditional cloudAdditional = additional.Copy();

                    cloudAdditional.frame = Mod.instance.randomIndex.Next(4) * 2;

                    cloudAdditional.motion = new Vector2(0.016f, 0);

                    cloudAdditional.flip = true;

                    CreateImpact(location, origin + new Vector2(0,16), IconData.impacts.clouds, size, cloudAdditional);

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


                    additional.light = 0.005f;

                    additional.layerOffset = 0.002f;

                    additional.alpha = 0.9f;

                    additional.color = schemeColours[schemes.herbal_impes];

                    CreateImpact(location, origin, IconData.impacts.love, size, additional);

                    //-------------------------------------

                    CreateDustup(location, origin, size, additional);

                    ImpactAdditional loveAdditional = additional.Copy();

                    loveAdditional.layerOffset = 0.001f;

                    loveAdditional.alpha = 0.7f;

                    CreateImpact(location, origin, IconData.impacts.plume, size, loveAdditional);

                    return;

                case impacts.flasher:

                    additional.alpha = 0.45f;

                    CreateImpact(location, origin, IconData.impacts.flashbang, size, additional);

                    CreateBangs(location, origin, size, additional);

                    return;

                case impacts.grasp:

                    additional.rotation = (float)(Math.PI) * 0.25f * Mod.instance.randomIndex.Next(8);

                    CreateGrasp(location, origin, Mod.instance.randomIndex.Next(3),4f, additional);

                    return;

                case impacts.roots:

                    additional.alpha = 1f;

                    additional.layer = 700f;

                    additional.color = gradientColours[additional.scheme][0];

                    additional.light = 0.005f;

                    List<int> graspIndex = new()
                    {
                        Mod.instance.randomIndex.Next(3),
                        Mod.instance.randomIndex.Next(3),
                        Mod.instance.randomIndex.Next(3),
                        Mod.instance.randomIndex.Next(3),
                        Mod.instance.randomIndex.Next(3),
                        Mod.instance.randomIndex.Next(3),
                        Mod.instance.randomIndex.Next(3),
                        Mod.instance.randomIndex.Next(3),
                    };

                    // ============================= First Wave

                    // up

                    additional.rotation = (float)(Math.PI);

                    CreateGrasp(location, origin + new Vector2(0, -208), graspIndex[0],4f, additional);

                    // right

                    additional.rotation = (float)(Math.PI * 1.50);

                    CreateGrasp(location, origin + new Vector2(208, 0), graspIndex[1], 4f, additional);

                    // down

                    additional.rotation = 0f;

                    CreateGrasp(location, origin + new Vector2(0, 208), graspIndex[2], 4f, additional);

                    // left

                    additional.rotation = (float)(Math.PI * 0.50);

                    CreateGrasp(location, origin + new Vector2(-208, 0), graspIndex[3], 4f, additional);

                    // up-right

                    additional.delay = 250;

                    additional.rotation = (float)(Math.PI * 1.25);

                    CreateGrasp(location, origin + new Vector2(160, -160), graspIndex[4], 4f, additional);

                    // down-right

                    additional.rotation = (float)(Math.PI * 1.75);

                    CreateGrasp(location, origin + new Vector2(160, 160), graspIndex[5], 4f, additional);

                    // down-left

                    additional.rotation = (float)(Math.PI * 0.25);

                    CreateGrasp(location, origin + new Vector2(-160, 160), graspIndex[6], 4f, additional);

                    // up-left

                    additional.rotation = (float)(Math.PI * 0.75);

                    CreateGrasp(location, origin + new Vector2(-160, -160), graspIndex[7], 4f, additional);

                    // ============================= Second Wave

                    // up

                    additional.delay = 500;

                    additional.rotation = (float)(Math.PI);

                    CreateGrasp(location, origin + new Vector2(0, -416), graspIndex[0], 3f, additional);

                    // right

                    additional.rotation = (float)(Math.PI * 1.50);

                    CreateGrasp(location, origin + new Vector2(416, 0), graspIndex[1], 3f, additional);

                    // down

                    additional.rotation = 0f;

                    CreateGrasp(location, origin + new Vector2(0, 416), graspIndex[2], 3f, additional);

                    // left

                    additional.rotation = (float)(Math.PI * 0.50);

                    CreateGrasp(location, origin + new Vector2(-416, 0), graspIndex[3], 3f, additional);

                    // up-right

                    additional.delay = 750;

                    additional.rotation = (float)(Math.PI * 1.25);

                    CreateGrasp(location, origin + new Vector2(320, -320), graspIndex[4], 3f, additional);

                    // down-right

                    additional.rotation = (float)(Math.PI * 1.75);

                    CreateGrasp(location, origin + new Vector2(320), graspIndex[5], 3f, additional);

                    // down-left

                    additional.rotation = (float)(Math.PI * 0.25);

                    CreateGrasp(location, origin + new Vector2(-320, 320), graspIndex[6], 3f, additional);

                    // up-left

                    additional.rotation = (float)(Math.PI * 0.75);

                    CreateGrasp(location, origin + new Vector2(-320), graspIndex[7], 3f, additional);

                    return;

                case impacts.aard:
                    CreateWitcherSign(location, origin, impact, size, additional);
                    return;
                case impacts.igni:
                    CreateWitcherSign(location, origin, impact, size, additional);
                    return;
                case impacts.axii:
                    CreateWitcherSign(location, origin, impact, size, additional);
                    return;
                case impacts.quen:
                    CreateWitcherSign(location, origin, impact, size, additional);
                    return;
                case impacts.yrden:
                    CreateWitcherSign(location, origin, impact, size, additional);
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
            else if (Y >= 18 && Y < 24)
            {

                sheet = impactsTextureFour;

            }
            else if (Y >= 24 && Y < 30)
            {

                sheet = impactsTextureFive;

            }

            return sheet;

        }

        public TemporaryAnimatedSprite CreateImpact(GameLocation location, Vector2 origin, impacts impact, float scale, ImpactAdditional additional)
        {

            Vector2 originOffset = origin + new Vector2(32f, 32f) - (new Vector2(32f * additional.girth, 32f) * scale );

            float interval = additional.interval;

            Microsoft.Xna.Framework.Color color = additional.color;

            float layer = additional.layer;

            if (additional.layer <= 0f)
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
                rotation = additional.rotation,
                alpha = additional.alpha,
                alphaFade = additional.alphaFade,
                motion = additional.motion,
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

            Microsoft.Xna.Framework.Rectangle source = new(128 * additional.frame, 0,128, 128);

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

        public TemporaryAnimatedSprite CreateGrasp(GameLocation location, Vector2 origin, int type, float scale, ImpactAdditional additional)
        {

            Vector2 originOffset = origin + new Vector2(32f, 32f) - (new Vector2(32, 32) * scale);

            float layer = additional.layer;

            if (additional.layer == -1)
            {

                layer = originOffset.Y / 10000;

            }

            layer += additional.layerOffset;

            if (additional.frames == 8)
            {

                additional.frames = 10;

            }

            Microsoft.Xna.Framework.Rectangle source = new(64 * additional.frame, 64 * type, 64, 64);

            TemporaryAnimatedSprite bomb = new(0, 125f, additional.frames, additional.loops, originOffset, false, false)
            {
                sourceRect = source,
                sourceRectStartingPos = new Vector2(source.X, source.Y),
                texture = graspTexture,
                scale = scale,
                timeBasedMotion = true,
                layerDepth = layer,
                rotation = additional.rotation,
                flipped = additional.flip,
                delayBeforeAnimationStart = additional.delay,

            };

            location.temporarySprites.Add(bomb);

            TemporaryAnimatedSprite bombshadow = new(0, 125f, additional.frames, additional.loops, originOffset + new Vector2(0,6), false, false)
            {
                sourceRect = source,
                sourceRectStartingPos = new Vector2(source.X, source.Y),
                texture = graspTexture,
                scale = scale,
                timeBasedMotion = true,
                layerDepth = layer - 0.0002f,
                rotation = additional.rotation,
                flipped = additional.flip,
                delayBeforeAnimationStart = additional.delay,

                color = Microsoft.Xna.Framework.Color.Black,
                alpha = 0.25f,

            };

            location.temporarySprites.Add(bombshadow);

            return bomb;

        }

        public void CreateDustup(GameLocation location, Vector2 origin, float size, ImpactAdditional additional)
        {

            ImpactAdditional bigImpactAdditional = additional.Copy();

            bigImpactAdditional.alpha = 0.2f;

            bigImpactAdditional.interval = 80f;

            bigImpactAdditional.layerOffset = -0.001f;

            CreateImpact(location, origin, IconData.impacts.puff, size + 0.5f, bigImpactAdditional);
        }

        public void CreateSmoke(GameLocation location, Vector2 origin, float size, ImpactAdditional additional)
        {

            ImpactAdditional bigImpactAdditionalTwo = additional.Copy();

            bigImpactAdditionalTwo.alpha = 0.3f;

            bigImpactAdditionalTwo.layerOffset = -0.002f;

            bigImpactAdditionalTwo.delay = 15;

            CreateImpact(location, origin + new Vector2(0, -224), IconData.impacts.smoke, size, bigImpactAdditionalTwo);

            CreateImpact(location, origin + new Vector2(-160, -48), IconData.impacts.steam, size - 1, bigImpactAdditionalTwo);

            bigImpactAdditionalTwo.flip = true;

            CreateImpact(location, origin + new Vector2(160, -48), IconData.impacts.steam, size - 1, bigImpactAdditionalTwo);

            CreateImpact(location, origin + new Vector2(0, 80), IconData.impacts.smoke, size, bigImpactAdditionalTwo);

        }

        public void CreateSupree(GameLocation location, Vector2 origin, float size, ImpactAdditional additional)
        {

            ImpactAdditional sparkles = additional.Copy();

            sparkles.alpha = 0.75f;

            sparkles.layer = 700f;

            sparkles.color = gradientColours[additional.scheme][0];

            sparkles.light = 0.005f;

            CreateImpact(location, origin + new Vector2(128, -128), IconData.impacts.sparkle, 4f, sparkles);

            sparkles.rotation = (float)(Math.PI * 0.50);

            CreateImpact(location, origin + new Vector2(128, 128), IconData.impacts.sparkle, 4f, sparkles);

            sparkles.rotation = (float)(Math.PI);

            CreateImpact(location, origin + new Vector2(-128, 128), IconData.impacts.sparkle, 4f, sparkles);

            sparkles.rotation = (float)(Math.PI * 1.50);

            CreateImpact(location, origin + new Vector2(-128, -128), IconData.impacts.sparkle, 4f, sparkles);

        }

        public void CreateSparkles(GameLocation location, Vector2 origin, float size, ImpactAdditional additional)
        {

            ImpactAdditional sparkles = additional.Copy();

            sparkles.alpha = 0.4f;

            sparkles.layerOffset = -0.002f;

            sparkles.delay = 15;

            CreateImpact(location, origin + new Vector2(160, -160), IconData.impacts.glare, size - 1, sparkles);

            additional.rotation = (float)(Math.PI * 0.50);

            CreateImpact(location, origin + new Vector2(160, 160), IconData.impacts.glare, size - 1, sparkles);

            sparkles.rotation = (float)(Math.PI);

            CreateImpact(location, origin + new Vector2(-160, 160), IconData.impacts.glare, size - 1, sparkles);

            sparkles.rotation = (float)(Math.PI * 1.50);

            CreateImpact(location, origin + new Vector2(-160, -160), IconData.impacts.glare, size - 1, sparkles);

        }

        public void CreateSparklesTwo(GameLocation location, Vector2 origin, float size, ImpactAdditional additional)
        {

            ImpactAdditional supreeGlare = new()
            {
                color = gradientColours[additional.scheme][1],

                light = 0f,

                alpha = 0.75f,

                rotation = (float)(Math.PI * 0.25)
            };

            CreateImpact(location, origin + new Vector2(0, -128), IconData.impacts.glare, 4f, supreeGlare);

            supreeGlare.rotation = (float)(Math.PI * 0.75);

            CreateImpact(location, origin + new Vector2(128, 0), IconData.impacts.glare, 4f, supreeGlare);

            supreeGlare.rotation = (float)(Math.PI * 1.25);

            CreateImpact(location, origin + new Vector2(0, 128), IconData.impacts.glare, 4f, supreeGlare);

            supreeGlare.rotation = (float)(Math.PI * 1.75);

            CreateImpact(location, origin + new Vector2(-128, 0), IconData.impacts.glare, 4f, supreeGlare);

        }

        public void CreateBangs(GameLocation location, Vector2 origin, float size, ImpactAdditional OldAdditional)
        {

            ImpactAdditional additional = OldAdditional.Copy();

            additional.light = 0f;

            additional.layerOffset = 0.001f;

            additional.frames = 4;

            additional.color = SchemeColour(additional.scheme);

            additional.delay = 150;

            CreateImpact(location, origin - new Vector2(8 * size), IconData.impacts.flashbang, size - 1, additional);

            additional.color = Microsoft.Xna.Framework.Color.White;

            additional.delay = 300;

            additional.frame = 4;

            CreateImpact(location, origin + new Vector2(8 * size), IconData.impacts.flashbang, size - 1, additional);

            additional.color = SchemeColour(additional.scheme);

            additional.delay = 450;

            additional.frame = 0;

            CreateImpact(location, origin + new Vector2(12 * size, -12 * size), IconData.impacts.flashbang, size - 1, additional);

            additional.color = Microsoft.Xna.Framework.Color.White;

            additional.delay = 600;

            additional.frame = 4;

            CreateImpact(location, origin + new Vector2(-12 * size, 12 * size), IconData.impacts.flashbang, size - 1, additional);

        }

        public TemporaryAnimatedSprite CreateWitcherSign(GameLocation location, Vector2 origin, impacts impact, float scale, ImpactAdditional additional)
        {

            Vector2 originOffset = origin + new Vector2(32f) - new Vector2(8f * scale);

            int signrow = 0;

            switch (impact)
            {
                case impacts.igni:
                    signrow = 16; break;
                case impacts.axii:
                    signrow = 32; break;
                case impacts.quen:
                    signrow = 48; break;
                case impacts.yrden:
                    signrow = 64; break;
            }

            Microsoft.Xna.Framework.Rectangle source = new(0, signrow, 16, 16);

            TemporaryAnimatedSprite sign = new(0, 125, 6, 1, originOffset, false, false)
            {

                sourceRect = source,
                sourceRectStartingPos = new Vector2(source.X, source.Y),
                texture = witchersignsTexture,
                scale = scale,
                layerDepth = 990f,

            };

            location.temporarySprites.Add(sign);

            Microsoft.Xna.Framework.Rectangle sourceTwo = new(80, signrow, 16, 16);

            TemporaryAnimatedSprite signtwo = new(0, 1000, 1, 1, originOffset, false, false)
            {

                sourceRect = sourceTwo,
                sourceRectStartingPos = new Vector2(80, sourceTwo.Y),
                texture = witchersignsTexture,
                scale = scale,
                alphaFade = 0.001f,
                layerDepth = 990f,
                delayBeforeAnimationStart = 750,
                timeBasedMotion = true
            };

            location.temporarySprites.Add(signtwo);

            return sign;

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

            SpellHandle warpSpell = new(location, origin, origin);

            warpSpell.displayFactor = (int)warp;

            warpSpell.type = reverse ? SpellHandle.Spells.warpout : SpellHandle.Spells.warpin;

            warpSpell.scheme = scheme;

            Mod.instance.spellRegister.Add(warpSpell);

        }

        public void WarpIndicator(GameLocation location, Vector2 origin, bool reverse = false, warps warp = warps.portal, schemes scheme = schemes.none)
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

                    animation = new(0, 75, 10, 1, originOffset, false, false)
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

                case warps.death:

                    originOffset = origin - new Vector2(32, 32);

                    rect = reverse ? new(0, 96, 32, 32) : new(0, 64, 32, 32);

                    animation = new(0, 75, 10, 1, originOffset, false, false)
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

                case warps.capture:

                    originOffset = origin - new Vector2(32, 32);

                    rect = new(0, 160, 32, 32);

                    animation = new(0, 75, 10, 1, originOffset, false, false)
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

                    if(glyphTime + 5.00 > Game1.currentGameTime.TotalGameTime.TotalSeconds && glyphSpot == Game1.player.currentLocation.Name)
                    {

                        break;

                    }

                    glyphSpot = Game1.player.currentLocation.Name;

                    glyphTime = Game1.currentGameTime.TotalGameTime.TotalSeconds;

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

                    CreateImpact(location, origin, impacts.plume, 4f, new() { alpha = 0.7f, color = schemeColours[schemes.gray] });

                    CreateImpact(location, origin, impacts.smoke, 4f, new() { alpha = 0.5f, layerOffset = -0.001f, color = schemeColours[schemes.darkgray] });

                    break;

                case warps.corvids:

                    CreateImpact(location, origin, impacts.plume, 2f, new() { color = schemeColours[schemes.witch], alpha = 1f, });

                    break;

                case warps.mist:

                    ImpactIndicator(location, origin, impacts.mists, 3f, new());

                    break;
            }

        }

    }

    public class CursorAdditional
    {

        public float interval = 1200f;

        public float alpha = 0.65f;

        public float fade = 0f;

        //public float rotation = 60;

        public int delay = 0;

        public float scale = 4f;

        public int loops = 1;

        public float layer = -1f;

        public IconData.schemes scheme = IconData.schemes.none;

    }

    public class CircleAdditional
    {

        public float interval = 3000f;

        public float alpha = 0.99f;

        public int delay = 0;

        public int loops = 1;

        public float fade = 0.00033f;

        public Microsoft.Xna.Framework.Color color = Microsoft.Xna.Framework.Color.White;

        public float layer = -1f;

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

        public float rotation = 0f;

        public Microsoft.Xna.Framework.Color color = Microsoft.Xna.Framework.Color.White;

        public List<Microsoft.Xna.Framework.Color> colours = new();

        public int delay = 0;

        public float light = 0.03f;

        public bool flip = false;

        public float layer = -1;

        public float layerOffset = 0f;

        public Vector2 motion = Vector2.Zero;

        public IconData.schemes scheme = IconData.schemes.none;

        public ImpactAdditional()
        {

        }

        public ImpactAdditional Copy()
        {

            ImpactAdditional copy = new();

            copy.frame = frame;

            copy.frames = frames;

            copy.interval = interval;

            copy.girth = girth;

            copy.loops = loops;

            copy.alpha = alpha;

            copy.alphaFade = alphaFade;

            copy.rotation = rotation;

            copy.color = color;

            copy.colours = colours;

            copy.delay = delay;

            copy.light = 0f;

            copy.flip = flip;

            copy.layer = layer;

            copy.layerOffset = layerOffset;

            copy.motion = motion;

            copy.scheme = scheme;

            return copy;

        }

    }

    public class SkyAdditional
    {

        public float interval = 1000f;

        public float alpha = 0.75f;

        public int delay = 0;

        public int loops = 1;

    }

}
