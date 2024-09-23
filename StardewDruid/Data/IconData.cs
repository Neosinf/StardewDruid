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
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using xTile.Dimensions;
using static StardewDruid.Data.IconData;
using static StardewValley.Menus.CharacterCustomization;
using static System.Formats.Asn1.AsnWriter;

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

            spiral,
            clouds,
            splatter,
            puff,
            cinder,
            skull,

            plume,
            steam,
            smoke,
            three3,
            three4,
            three5,

            nature,
            bomb,
            boltswirl,
            deathbomb,
            mists,
            deathwhirl,

            summoning,

        }

        public Texture2D impactsTexture;

        public Texture2D impactsTextureTwo;

        public Texture2D impactsTextureThree;

        public enum skies
        {
            none,
            valley,
            mountain,
            moon,
            night,
            temple,
            sunset,
        }

        public Texture2D skyTexture;

        public enum circles
        {
            none,
            summoning,
        }

        public Texture2D circleTexture;

        public Texture2D missileTexture;

        public enum missiles
        {
            none,
            fireball,
            cannonball,
            meteor,
            death,
            slimeball,
            rockfall,
            whisk,
        }

        public enum missileIndexes
        {
            blazeCore1,
            blazeCore2,
            blazeCore3,
            blazeCore4,
            blazeInner1,
            blazeInner2,
            blazeInner3,
            blazeInner4,
            blazeOuter1,
            blazeOuter2,
            blazeOuter3,
            blazeOuter4,
            blazeOutline1,
            blazeOutline2,
            blazeOutline3,
            blazeOutline4,
            trail1,
            trail2,
            trail3,
            trail4,
            trailOutline1,
            trailOutline2,
            trailOutline3,
            trailOutline4,
            sparkCore1,
            sparkCore2,
            sparkCore3,
            sparkCore4,
            sparkInner1,
            sparkInner2,
            sparkInner3,
            sparkInner4,
            sparkOutline1,
            sparkOutline2,
            sparkOutline3,
            sparkOutline4,
            scatter1,
            scatter2,
            scatter3,
            scatter4,
            meteor1,
            meteor2,
            meteor3,
            rock1,
            rock2, 
            rock3, 
            death,
            cannonball,
            star1,
            star2,
            star3,
            star4,

        }

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
            lair,
            court,
            tomb,
            engineum,
            gate,
            magnolia,
            pavement,
        }

        public Dictionary<tilesheets, Texture2D> sheetTextures = new();

        public enum relics
        {
            none,
            flask,
            flask1,
            flask2,
            flask3,
            flask4,
            flask5,
            bottle,
            bottle1,
            bottle2,
            bottle3,
            bottle4,
            bottle5,
            vial,
            vial1,
            vial2,
            vial3,
            vial4,
            vial5,
            effigy_crest,
            jester_dice,
            shadowtin_tome,
            dragon_form,
            blackfeather_glove,
            stardew_druid,
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
            herbalism_6,
            wayfinder_stone,
            crow_hammer,
            wayfinder_key,
            wayfinder_glove,
            wayfinder_eye,
            other_6,
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
            runestones_5,
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
            texts_5,
            box_measurer,
            box_mortician,
            box_artisan,
            box_chaos,
            box_4,
            box_5,
            skull_saurus,
            skull_gelatin,
            skull_3,
            skull_4,
            skull_5,
            skull_6,
            restore_goshuin,
            restore_offering,
            restore_cloth

        }

        public enum warps
        {

            portal,
            smoke,

        }

        public Texture2D relicsTexture;

        public int relicColumns;

        public Texture2D boltTexture;

        public Texture2D laserTexture;

        public Texture2D warpTexture;

        public Texture2D gravityTexture;

        public Texture2D emberTexture;

        public Texture2D crateTexture;

        public Texture2D warpstrikeTexture;
        
        public Texture2D warpswordTexture;
        
        public Texture2D warpswipeTexture;

        public Texture2D echoTexture;

        public Texture2D wispTexture;

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

            death,
            golden,
            snazzle,
            stardew,
            wisps,

            rock,
            rockTwo,
            rockThree,

            Emerald,
            Aquamarine,
            Ruby,
            Amethyst,
            Topaz,
            Solar,
            Void,

            apple,
            grannysmith,
            pumpkin,
            plum,
            blueberry,
            melon,

            dragon_custom,
            dragon_red,
            dragon_blue,
            dragon_black,
            dragon_green,
            dragon_purple,

            sword_steel,
            sword_stars,
            sword_lightsaber,
            sword_warp,

            herbal_ligna,
            herbal_impes,
            herbal_celeri,
            herbal_faeth,
            herbal_aether,

            npc_blackfeather,

        }

        public Dictionary<IconData.schemes, Microsoft.Xna.Framework.Color> schemeColours = new()
        {
            [schemes.none] = Microsoft.Xna.Framework.Color.White,

            [schemes.weald] =  Microsoft.Xna.Framework.Color.Green,
            [schemes.mists] =  new(83, 96, 150),
            [schemes.stars] =  new(231, 102, 84),
            [schemes.fates] = new(119, 75, 131),
            [schemes.ether] = new(13, 114, 185),

            [schemes.death] = new(70, 60, 70),
            [schemes.golden] = new(251, 201, 38),
            [schemes.snazzle] = new(214, 0, 175),
            [schemes.stardew] = new(210,164,89),

            [schemes.rock] = new(120, 154, 160),
            [schemes.rockTwo] = new(196, 164, 122),
            [schemes.rockThree] = new(144, 126, 144),

            [schemes.Emerald] = new(67, 255, 83),
            [schemes.Aquamarine] = new(74, 243, 255),
            [schemes.Ruby] = new(255, 38, 38),
            [schemes.Amethyst] = new(255, 67, 251),
            [schemes.Topaz] = new(255, 156, 33),
            [schemes.Solar] = new(255, 194, 128),
            [schemes.Void] = new(200, 100, 190),

            [schemes.apple] = new(237, 32, 36),
            [schemes.grannysmith] = new(141, 198, 63),
            [schemes.pumpkin] = new(238, 102, 37),
            [schemes.plum] = new(196, 35, 86),
            [schemes.blueberry] = new(84, 104, 177),
            [schemes.melon] = new(107, 192, 111),

            [schemes.herbal_ligna] = new(186, 255, 201),
            [schemes.herbal_impes] = new(255, 179, 186),
            [schemes.herbal_celeri] = new(186, 225, 255),
            [schemes.herbal_faeth] = new(255, 255, 186),
            [schemes.herbal_aether] = new(186, 225, 255),
            
            [schemes.npc_blackfeather] = new(81,115,131),

        };

        public Dictionary<IconData.schemes, List<Microsoft.Xna.Framework.Color>> gradientColours = new()
        {
            [schemes.none] = new() { Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Gray, Microsoft.Xna.Framework.Color.DarkGray, },

            [schemes.weald] = new() { Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.LightGreen, Microsoft.Xna.Framework.Color.Green },
            [schemes.mists] = new() { new(75, 138, 187), new(70, 81, 144), new(83,96,150), },
            [schemes.stars] = new() { new(255, 230, 166), new(255, 173, 84), new(231, 102, 84), },
            [schemes.fates] = new() { new(119, 75, 131), new(59, 55, 100), new(37,34,74), new(236, 118, 124), },
            [schemes.ether] = new() { new(111, 203, 220), new(84, 163, 218), new(13, 114, 185), },

            [schemes.death] = new() { new(60, 66, 72), new(72, 84, 96), new(96, 108, 128) },
            [schemes.golden] = new() { new(255, 250, 194), new(255, 232, 155), new(251, 201, 38), },
            [schemes.snazzle] = new() { new(214, 0, 175), new(155, 0, 126), new(37, 34, 74), },
            [schemes.wisps] = new() { new(75, 138, 187), new(69, 186, 235), new(74, 243, 255), new(186, 225, 255), },

            [schemes.dragon_red] = new() { new(190, 30, 45), new(191, 142, 93), new(39, 170, 225) },
            [schemes.dragon_green] = new() { new(121,172,66), new(207,165,73), new(248, 149, 32) },
            [schemes.dragon_blue] = new() { new(53, 70, 179), new(147, 149, 151), new(247, 148, 29) },
            [schemes.dragon_black] = new() { new(57, 52, 53), new(69, 186, 235), new(146,86,163) },
            [schemes.dragon_purple] = new() { new(159, 80, 191), new(69, 186, 235), new(0, 167, 157) },

            [schemes.sword_steel] = new() { new(200,212,212), new(68, 80, 80), new(48, 60, 60) },
            [schemes.sword_stars] = new() { new(255, 192, 128),  new(48, 20, 16), new(64, 48, 16), },
            [schemes.sword_lightsaber] = new() { new(214, 0, 175), new(16, 16, 16), new(37, 34, 74), },
            [schemes.sword_warp] = new() { new(159, 80, 191), new(16, 16, 16), new(37, 34, 74), },
        };

        public Dictionary<IconData.schemes,string> DragonOptions = new()
        {
            [schemes.dragon_custom] = "Custom",
            [schemes.dragon_red] = "Red",
            [schemes.dragon_blue] = "Blue",
            [schemes.dragon_black] = "Black",
            [schemes.dragon_green] = "Green",
            [schemes.dragon_purple] = "Purple",

        };

        public Dictionary<string, IconData.schemes> DragonSchemery = new()
        {
            ["Custom"] = schemes.dragon_custom,
            ["Red"] = schemes.dragon_red,
            ["Blue"] = schemes.dragon_blue,
            ["Black"] = schemes.dragon_black,
            ["Green"] = schemes.dragon_green,
            ["Purple"] = schemes.dragon_purple,

        };

        public Dictionary<IconData.schemes, string> BreathOptions = new()
        {
            [schemes.stars] = "Stars",
            [schemes.fates] = "Fates",
            [schemes.ether] = "Ether",

        };

        public Dictionary<string, IconData.schemes> BreathSchemery = new()
        {
            ["Stars"] = schemes.stars,
            ["Fates"] = schemes.fates,
            ["Ether"] = schemes.ether,

        };

        public IconData()
        {

            cursorTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Cursors.png"));

            displayTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Displays.png"));

            decorationTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Decorations.png"));

            impactsTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Impacts.png"));

            impactsTextureTwo = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "ImpactsTwo.png"));

            impactsTextureThree = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "ImpactsThree.png"));

            skyTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Skies.png"));

            circleTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Circle.png"));

            missileTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Missiles.png"));

            sheetTextures[tilesheets.grove] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Grove.png"));

            sheetTextures[tilesheets.spring] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Spring.png"));

            sheetTextures[tilesheets.atoll] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Atoll.png"));

            sheetTextures[tilesheets.graveyard] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Graveyard.png"));

            sheetTextures[tilesheets.chapel] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Chapel.png"));

            sheetTextures[tilesheets.lair] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Lair.png"));

            sheetTextures[tilesheets.court] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Court.png"));

            sheetTextures[tilesheets.tomb] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Tomb.png"));

            sheetTextures[tilesheets.engineum] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Engineum.png"));

            sheetTextures[tilesheets.gate] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Gate.png"));

            sheetTextures[tilesheets.magnolia] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Magnolia.png"));

            sheetTextures[tilesheets.pavement] = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Sheets", "Pavement.png"));

            relicsTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Relics.png"));

            boltTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Bolt.png"));

            laserTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Laser.png"));

            warpTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Warp.png"));

            gravityTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Gravity.png"));

            emberTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Embers.png"));

            crateTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "DragonCrate.png"));

            warpstrikeTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Reaper.png"));

            warpswordTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "WeaponSword.png"));

            warpswipeTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "WeaponSwipe.png"));

            echoTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Echo.png"));

            wispTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Wisp.png"));

            shieldTexture = Mod.instance.Helper.ModContent.Load<Texture2D>(Path.Combine("Images", "Shield.png"));

            shadowRectangle = CursorRectangle(cursors.shadow);

            LoadNuances();

        }

        public void LoadNuances()
        {

            sheetTextures[tilesheets.outdoors] = Mod.instance.Helper.GameContent.Load<Texture2D>(Path.Combine("Maps", Game1.currentSeason + "_outdoorsTileSheet"));

            sheetTextures[tilesheets.outdoorsTwo] = Mod.instance.Helper.GameContent.Load<Texture2D>(Path.Combine("Maps", Game1.currentSeason + "_outdoorsTileSheet2"));

            CustomDragonScheme();

        }

        public void CustomDragonScheme()
        {

            gradientColours[schemes.dragon_custom] = new()
            {
                new(Mod.instance.Config.dragonPrimaryR,Mod.instance.Config.dragonPrimaryG,Mod.instance.Config.dragonPrimaryB),
                new(Mod.instance.Config.dragonSecondaryR,Mod.instance.Config.dragonSecondaryG,Mod.instance.Config.dragonSecondaryB),
                new(Mod.instance.Config.dragonTertiaryR,Mod.instance.Config.dragonTertiaryG,Mod.instance.Config.dragonTertiaryB),
            };

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

        public Microsoft.Xna.Framework.Rectangle QuestDisplay(Journal.Quest.questTypes questType)
        {
            switch (questType)
            {
                case Journal.Quest.questTypes.challenge:

                    return DisplayRectangle(displays.quest);

                case Journal.Quest.questTypes.lesson:

                    return DisplayRectangle(displays.effect);

                case Journal.Quest.questTypes.miscellaneous:

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

            //List<Microsoft.Xna.Framework.Color> colours;

            switch (impact)
            {

                case impacts.none:

                    return;

                case impacts.impact:

                    additional.alpha = 0.85f;

                    break;

                case impacts.glare:

                    additional.light = 0.01f;

                    //additional.alpha = 0.85f;

                    break;

                case impacts.splatter:

                    size += 0.5f;

                    additional.color = SchemeColour(additional.scheme);

                    additional.alpha = 0.7f;

                    break;

                case impacts.puff:

                    size = Math.Max(Math.Min(5, size), 3);

                    break;

                case impacts.skull:
                case impacts.deathbomb:

                    additional.alpha = 0.7f;

                    TemporaryAnimatedSprite skull = CreateImpact(location, origin, IconData.impacts.skull, size, additional);

                    skull.alphaFade = 0.001f;

                    additional.alpha = 0.5f;

                    additional.light = 0f;

                    additional.layerOffset = 0.001f;

                    additional.color = schemeColours[schemes.death];

                    CreateImpact(location, origin, IconData.impacts.puff, size, additional);

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

                case impacts.nature:

                    if(additional.scheme == schemes.none)
                    {

                        additional.scheme = schemes.weald;

                    }

                    if(additional.alpha == 0.35f)
                    {

                        additional.alpha = 0.75f;

                    }

                    additional.layer = 700f;

                    additional.color = gradientColours[additional.scheme][2];

                    additional.light = 0f;

                    CreateImpact(location, origin, IconData.impacts.sparkle, size - 1, additional);

                    additional.color = gradientColours[additional.scheme][1];

                    additional.light = 0f;

                    CreateImpact(location, origin, IconData.impacts.sparkle, size - 2, additional);

                    additional.color = gradientColours[additional.scheme][0];

                    additional.light = 0.005f;

                    CreateImpact(location, origin, IconData.impacts.sparkle, size, additional);
                    return;

                case impacts.boltswirl:

                    if(additional.scheme == schemes.none)
                    {

                        additional.scheme = schemes.mists;

                    }

                    additional.interval = 80;

                    additional.light = 0f;

                    additional.color = Microsoft.Xna.Framework.Color.White;

                    additional.alpha = 0.3f;

                    additional.layer = 997f;

                    CreateImpact(location, origin, IconData.impacts.spiral, 4f, additional);

                    additional.color = gradientColours[additional.scheme][0];

                    additional.alpha = 0.2f;

                    additional.layer = 996f;

                    CreateImpact(location, origin, IconData.impacts.spiral, 6f, additional);

                    additional.color = gradientColours[additional.scheme][1];

                    additional.alpha = 0.1f;

                    additional.layer = 995f;

                    additional.light = 0.005f;

                    CreateImpact(location, origin, IconData.impacts.spiral, 8f, additional);

                    return;

                case impacts.deathwhirl:

                    additional.alpha = 0.7f;

                    TemporaryAnimatedSprite skullCore = CreateImpact(location, origin, IconData.impacts.skull, size, additional);

                    skullCore.alphaFade = 0.001f;

                    additional.interval = 80;

                    additional.light = 0f;

                    additional.color = Microsoft.Xna.Framework.Color.White;

                    additional.alpha = 0.3f;

                    additional.layer = 997f;

                    CreateImpact(location, origin, IconData.impacts.spiral, 4f, additional);

                    additional.color = gradientColours[schemes.death][0];

                    additional.alpha = 0.2f;

                    additional.layer = 996f;

                    CreateImpact(location, origin, IconData.impacts.spiral, 6f, additional);

                    additional.color = gradientColours[schemes.death][1];

                    additional.alpha = 0.1f;

                    additional.layer = 995f;

                    additional.light = 0.005f;

                    CreateImpact(location, origin, IconData.impacts.spiral, 8f, additional);

                    return;

                case impacts.mists:

                    AnimateMistic();

                    additional.alpha = 0.1f;

                    CreateImpact(location, Game1.player.Position - new Vector2(0,64), IconData.impacts.spiral, 5f, additional);

                    return;

                case impacts.summoning:

                    CircleIndicator(location, origin, circles.summoning, size, new() { color = schemeColours[additional.scheme],});

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

                layer = originOffset.Y / 10000;

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
                flipped = additional.flip,
                delayBeforeAnimationStart = additional.delay,
            };

            location.temporarySprites.Add(bomb);

            if (additional.light > 0f)
            {

                TemporaryAnimatedSprite flash = new(23, 500f, 6, 1, origin, false, Game1.random.NextDouble() < 0.5)
                {
                    texture = Game1.mouseCursors,
                    light = true,
                    lightRadius = scale,
                    lightcolor = Microsoft.Xna.Framework.Color.Black,
                    alphaFade = additional.light,
                    Parent = location
                };

                location.temporarySprites.Add(flash);

            }

            return bomb;

        }

        public static Microsoft.Xna.Framework.Rectangle SkyRectangle(skies sky)
        {

            return new(((int)sky - 1) * 64, 0, 64, 64);

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

        public List<TemporaryAnimatedSprite> MissileConstruct(GameLocation location,missiles missile,Vector2 origin, int scale, int increments, float depth, schemes scheme)
        {

            List<TemporaryAnimatedSprite> missileAnimations = new();

            Microsoft.Xna.Framework.Rectangle rect = new(0, ((int)missile - 1) * 96, 96, 96);

            Vector2 setat = origin - (new Vector2(48, 48) * scale) + new Vector2(32, 32);

            int loops = (int)Math.Ceiling(increments * 0.5);

            int frames = 4;

            int interval = (increments * 250) / (loops * frames);

            Microsoft.Xna.Framework.Color coreColour = Microsoft.Xna.Framework.Color.White;

            List<Microsoft.Xna.Framework.Color> gradient;

            schemes rockScheme = schemes.rock;

            rockScheme = (schemes)((int)rockScheme + Mod.instance.randomIndex.Next(3));

            Microsoft.Xna.Framework.Color rockColour = SchemeColour(rockScheme);

            switch (missile)
            {

                case missiles.fireball:

                    gradient = ConvertToGradient(scheme);

                    missileAnimations.Add(MissileAnimation(location,missileIndexes.blazeCore1,setat,scale,interval,frames,loops,depth,coreColour,0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, gradient[0], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, gradient[1], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, gradient[2], 0.75f));

                    break;

                case missiles.meteor:

                    //lineColour = new(76, 72, 125);

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, coreColour, 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, gradientColours[schemes.stars][0], 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, gradientColours[schemes.stars][1], 1f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, gradientColours[schemes.stars][2], 1f));

                    missileIndexes meteorite = missileIndexes.meteor1;

                    meteorite = (missileIndexes)((int)meteorite + Mod.instance.randomIndex.Next(6));

                    Vector2 coreSet = origin - (new Vector2(48, 48) * (scale*0.75f)) + new Vector2(32, 32);

                    TemporaryAnimatedSprite meteor = MissileAnimation(location, meteorite, coreSet, scale * 0.75f, interval * frames * loops, 1, 1, depth + 0.0001f, rockColour, 1f);
                   
                    meteor.rotation = (float)Math.PI * 0.5f * Mod.instance.randomIndex.Next(4);

                    meteor.rotationChange = (float)Math.PI / 60;

                    missileAnimations.Add(meteor);

                    break;

                case missiles.rockfall:

                    missileIndexes rockfalling = missileIndexes.rock1;

                    rockfalling = (missileIndexes)((int)rockfalling + Mod.instance.randomIndex.Next(3));

                    TemporaryAnimatedSprite rockScatter = MissileAnimation(location, missileIndexes.scatter1, setat, scale, interval, frames, loops, depth, rockColour, 1f);

                    switch (Mod.instance.randomIndex.Next(2))
                    {

                        case 1: rockScatter.verticalFlipped = true; break;

                    }

                    missileAnimations.Add(rockScatter);

                    TemporaryAnimatedSprite rockCore = MissileAnimation(location, rockfalling, setat, scale, interval * frames * loops, 1, 1, depth + 0.0001f, rockColour, 1f);

                    rockCore.rotation = (float)Math.PI * 0.5f * Mod.instance.randomIndex.Next(4);

                    missileAnimations.Add(rockCore);

                    break;

                case missiles.slimeball:

                    gradient = ConvertToGradient(scheme);

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, coreColour, 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, gradient[0], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.trail1, setat, scale, interval, frames, loops, depth, gradient[1], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.trailOutline1, setat, scale, interval, frames, loops, depth, gradient[2], 0.75f));

                    break;

                case missiles.cannonball:

                    gradient = ConvertToGradient(scheme);

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, coreColour, 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, gradient[0], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, gradient[1], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, gradient[2], 0.75f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.cannonball, setat, (int)(scale * 0.75f), interval * frames * loops, 1, 1, depth + 0.0001f, SchemeColour(schemes.death), 0.75f));

                    break;

                case missiles.death:

                    gradient = ConvertToGradient(scheme);

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeCore1, setat, scale, interval, frames, loops, depth, gradient[2], 0.9f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeInner1, setat, scale, interval, frames, loops, depth, gradient[1], 0.9f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOuter1, setat, scale, interval, frames, loops, depth, gradient[0], 0.9f));

                    missileAnimations.Add(MissileAnimation(location, missileIndexes.blazeOutline1, setat, scale, interval, frames, loops, depth, coreColour, 0.9f));

                    TemporaryAnimatedSprite deathAnimation = MissileAnimation(location, missileIndexes.death, setat, (int)(scale * 0.9f), interval * frames * loops, 1, 1, depth + 0.0001f, coreColour, 0.9f);

                    deathAnimation.rotationChange = 0.0001f;

                    missileAnimations.Add(deathAnimation);

                    break;

                case missiles.whisk:

                    TemporaryAnimatedSprite whisk1 = MissileAnimation(location, missileIndexes.star3, setat, scale, interval * frames * loops, 1, 1, depth, gradientColours[schemes.fates][3], 0.8f);

                    whisk1.rotationChange = (float)(Math.PI / 60);

                    missileAnimations.Add(whisk1);

                    TemporaryAnimatedSprite whisk2 = MissileAnimation(location, missileIndexes.star2, setat, scale, interval * frames * loops, 1, 1, depth, gradientColours[schemes.fates][0], 0.8f);

                    whisk2.rotationChange = (float)(Math.PI / 60);

                    missileAnimations.Add(whisk2);

                    TemporaryAnimatedSprite whisk3 = MissileAnimation(location, missileIndexes.star1, setat, scale, interval * frames * loops, 1, 1, depth, gradientColours[schemes.fates][1], 0.8f);

                    whisk3.rotationChange = (float)(Math.PI / 60);

                    missileAnimations.Add(whisk3);

                    break;

            }

            return missileAnimations;

        }

        public List<Microsoft.Xna.Framework.Color> ConvertToGradient(schemes scheme)
        {

            if (gradientColours.ContainsKey(scheme))
            {

                return gradientColours[scheme];

            }

            Microsoft.Xna.Framework.Color schemeColour = SchemeColour(scheme);

            Microsoft.Xna.Framework.Color schemeLight = new(Math.Min((int)schemeColour.R + 32, 255), Math.Min((int)schemeColour.G + 32, 255), Math.Min((int)schemeColour.B + 32, 255));

            int trydarkR = (int)schemeColour.R - 24;

            int trydarkG = (int)schemeColour.G - 24;

            int trydarkB = (int)schemeColour.B - 24;

            Microsoft.Xna.Framework.Color schemeDark = new(trydarkR < 0 ? 0 : trydarkR, trydarkG < 0 ? 0 : trydarkG, trydarkB < 0 ? 0 : trydarkB);

            return new() { schemeLight, schemeColour, schemeDark, };


        }

        public Microsoft.Xna.Framework.Color ConvertToOutline(Microsoft.Xna.Framework.Color schemeColour)
        {

            int trydarkR = (int)schemeColour.R - 72;

            int trydarkG = (int)schemeColour.G - 72;

            int trydarkB = (int)schemeColour.B - 72;

            Microsoft.Xna.Framework.Color schemeDark = new(trydarkR < 0 ? 0 : trydarkR, trydarkG < 0 ? 0 : trydarkG, trydarkB < 0 ? 0 : trydarkB);

            return schemeDark;

        }

        public TemporaryAnimatedSprite MissileAnimation(GameLocation location, missileIndexes missile, Vector2 origin, float scale, int interval, int frames, int loops,  float depth, Microsoft.Xna.Framework.Color color, float alpha)
        {

            Microsoft.Xna.Framework.Rectangle rect = new((int)missile % 4 * 96, (int)((int)missile / 4) * 96, 96, 96);

            TemporaryAnimatedSprite missileAnimation = new(0, interval, frames, loops, origin, false, false)
            {

                sourceRect = rect,

                sourceRectStartingPos = new Vector2(rect.X, rect.Y),

                texture = missileTexture,

                scale = scale,

                layerDepth = depth,

                alpha = alpha,

                color = color,

            };

            location.temporarySprites.Add(missileAnimation);

            return missileAnimation;

        }

        public List<TemporaryAnimatedSprite> EmberConstruct(GameLocation location, schemes scheme, Vector2 origin, float scale, int Time = 1, float layer = -1f)
        {

            List<TemporaryAnimatedSprite> emberAnimations = new();

            if (layer <= 0)
            {

                layer = origin.Y / 10000;

                layer -= (0.0001f * scale);

            }

            float fade = 1f - (0.05f * Mod.instance.randomIndex.Next(8));

            bool smokeFlip = Game1.player.FacingDirection < 2;

            switch (Mod.instance.randomIndex.Next(4))
            {
                default:

                    break;

                case 0:

                    emberAnimations.Add(CreateImpact(location, origin + new Vector2(32) - (new Vector2(16, 28) * scale), impacts.plume, scale, new() { interval = 200, loops = Math.Min(3,Time), layer = layer - 0.0001f, alpha = 0.75f, flip = Mod.instance.randomIndex.NextBool() }));

                    break;

                case 1:

                    emberAnimations.Add(CreateImpact(location, origin + new Vector2(32) - (new Vector2(16, 28) * scale), impacts.plume, scale, new() { interval = 200, loops = Math.Min(3, Time), color = Microsoft.Xna.Framework.Color.LightGray, layer = layer - 0.0001f, alpha = 0.75f, flip = Mod.instance.randomIndex.NextBool() }));

                    break;

                case 2:

                    emberAnimations.Add(CreateImpact(location, origin + new Vector2(32) - (new Vector2(16, 28) * scale), impacts.smoke, scale, new() { interval = 200, loops = Math.Min(3, Time), layer = layer - 0.0001f, alpha = 0.75f, flip = Mod.instance.randomIndex.NextBool() }));

                    break;

                case 3:

                    emberAnimations.Add(CreateImpact(location, origin + new Vector2(32) - (new Vector2(16, 28) * scale), impacts.smoke, scale, new() { interval = 200, loops = Math.Min(3, Time), color = Microsoft.Xna.Framework.Color.LightGray, layer = layer - 0.0001f, alpha = 0.75f, flip = Mod.instance.randomIndex.NextBool() }));

                    break;
            }

            emberAnimations.Add(emberAnimation(location, origin, scale, 0, gradientColours[scheme][2], layer, fade, Time));

            emberAnimations.Add(emberAnimation(location, origin, scale, 1, gradientColours[scheme][1], layer, fade, Time));

            emberAnimations.Add(emberAnimation(location, origin, scale, 2, gradientColours[scheme][0], layer, fade, Time));

            emberAnimations.Add(emberAnimation(location, origin, scale, 3, Microsoft.Xna.Framework.Color.White, layer, fade, Time));

            emberAnimations.Add(emberAnimation(location, origin + new Vector2(0, 8), scale, 3, Microsoft.Xna.Framework.Color.Black * 0.25f, layer - 0.0002f, 1f, Time));
            
            return emberAnimations;

        }

        public TemporaryAnimatedSprite emberAnimation(GameLocation location, Vector2 origin, float scale, int part, Microsoft.Xna.Framework.Color color, float layer, float alpha, int Time)
        {
            
            TemporaryAnimatedSprite burnAnimation;

            if (part == 2)
            {

                burnAnimation = new(0, 200, 15, Time, origin + new Vector2(32) - (new Vector2(16) * scale), false, false)
                {

                    sourceRect = new(0,  (part * 32), 32, 32),

                    sourceRectStartingPos = new(0,  (part * 32)),

                    texture = emberTexture,

                    scale = scale,

                    layerDepth = layer,

                    alpha = alpha,

                    color = color,

                    light = true,

                    lightRadius = 2,

                };

            }
            else
            {

                burnAnimation = new(0, 200, 15, Time, origin + new Vector2(32) - (new Vector2(16) * scale), false, false)
                {

                    sourceRect = new(0, (part * 32), 32, 32),

                    sourceRectStartingPos = new(0, (part * 32)),

                    texture = emberTexture,

                    scale = scale,

                    layerDepth = layer,

                    alpha = alpha,

                    color = color,

                };


            }

            location.TemporarySprites.Add(burnAnimation);

            return burnAnimation;

        }

        public static Microsoft.Xna.Framework.Rectangle RelicRectangles(relics relic)
        {

            if (relic == relics.none) { return new(); }

            int slot = Convert.ToInt32(relic) - 1;

            return new(slot % 6 * 20, slot == 0 ? 0 : slot / 6 * 20, 20, 20);

        }

        public List<TemporaryAnimatedSprite> BoltAnimation(GameLocation location, Vector2 origin, IconData.schemes scheme = schemes.mists, int size = 2)
        {

            List<TemporaryAnimatedSprite> animations = new();

            float boltScale = 1.5f + 0.5f * size;

            int viewY = Game1.viewport.Y;

            int offset = 0;

            Vector2 originOffset = new((int)origin.X + 32 - (int)(32 * boltScale), (int)origin.Y + 48 - (int)(400 * boltScale));

            if ((int)originOffset.Y < viewY && (int)origin.Y - viewY >= 200)
            {

                offset = (int)((viewY - (int)originOffset.Y) / boltScale);

                originOffset.Y = viewY;

            }

            int randSet1 = 48 * Mod.instance.randomIndex.Next(4);

            bool flippit = (Game1.random.NextDouble() < 0.5) ? true : false;

            TemporaryAnimatedSprite bolt1 = new(0, 500, 1, 999, originOffset, false, flippit)
            {

                sourceRect = new(randSet1, offset, 48, 400 - offset),

                sourceRectStartingPos = new Vector2(randSet1, offset),

                texture = boltTexture,

                layerDepth = 803f,

                scale = boltScale,

            };

            location.temporarySprites.Add(bolt1);

            animations.Add(bolt1);

            TemporaryAnimatedSprite boltX = new(0, 500, 1, 999, originOffset, false, flippit)
            {

                sourceRect = new(192 + randSet1, offset, 48, 400 - offset),

                sourceRectStartingPos = new Vector2(192 + randSet1, offset),

                texture = boltTexture,

                layerDepth = 802f,

                scale = boltScale,

                color = gradientColours[scheme][0],

            };

            location.temporarySprites.Add(boltX);

            animations.Add(boltX);

            int randSet2 = 48 * Mod.instance.randomIndex.Next(4);

            TemporaryAnimatedSprite bolt2 = new(0, 500, 1, 999, originOffset - new Vector2(2 * boltScale, 0), false, (Game1.random.NextDouble() < 0.5) ? true : false)
            {

                sourceRect = new(384 + randSet2, offset, 48, 400 - offset),

                sourceRectStartingPos = new Vector2(192 + randSet2, offset),

                texture = boltTexture,

                layerDepth = 801f,

                scale = boltScale,

                color = gradientColours[scheme][0],

            };

            location.temporarySprites.Add(bolt2);

            animations.Add(bolt2);

            int randSet3 = 48 * Mod.instance.randomIndex.Next(4);

            TemporaryAnimatedSprite bolt3 = new(0, 500, 1, 999, originOffset + new Vector2(2*boltScale,0), false, (Game1.random.NextDouble() < 0.5) ? true : false)
            {

                sourceRect = new(576 + randSet3, offset, 48, 385 - offset),

                sourceRectStartingPos = new Vector2(384 + randSet3, offset),

                texture = boltTexture,

                layerDepth = 800f,

                scale = boltScale,

                color = gradientColours[scheme][1],

            };

            location.temporarySprites.Add(bolt3);

            animations.Add(bolt3);

            // ---------------------- clouds

            if (size <= 1)
            {

                return animations;

            }

            TemporaryAnimatedSprite bolt4 = CreateImpact(
                location,
                originOffset + new Vector2(32, -24),
                impacts.clouds, 
                boltScale + 1f,
                new() {
                    color = gradientColours[scheme][2],
                    girth = 2,
                    layer = 803f,
                    interval = 200,
                    frames = 4,
                    alpha = 0.7f,
                });

            animations.Add(bolt4);

            TemporaryAnimatedSprite bolt5 = CreateImpact(
                location,
                originOffset + new Vector2(48, -12),
                impacts.clouds, 
                boltScale +0.5f,
                new()
                {
                    color = gradientColours[scheme][0],
                    girth = 2,
                    layer = 804f,
                    interval = 200,
                    frames = 4,
                    alpha = 0.7f,
                });

            animations.Add(bolt5);

            TemporaryAnimatedSprite bolt6 = CreateImpact(
                location,
                originOffset + new Vector2(64, 0),
                impacts.clouds, 
                boltScale,
                new()
                {
                    girth = 2,
                    layer = 805f,
                    interval = 200,
                    frames = 4,
                    alpha = 0.7f,
                });

            animations.Add(bolt6);

            for(int i = 0; i < ((400-offset)*boltScale); i += 192)
            {

                Vector2 lightSource = new(origin.X, originOffset.Y + 128 + i);

                TemporaryAnimatedSprite lightCircle = new(23, 200f, 6, 1, lightSource, false, Game1.random.NextDouble() < 0.5)
                {
                    texture = Game1.mouseCursors,
                    light = true,
                    lightRadius = 3f,
                    lightcolor = Microsoft.Xna.Framework.Color.Black,
                    alphaFade = 0.03f,
                    Parent = location,
                };

                location.temporarySprites.Add(lightCircle);

                animations.Add(lightCircle);

            }

            return animations;

            /*Vector2 originOffset = new(origin.X - 64, origin.Y - 320);

            //Mod.instance.iconData.ImpactIndicator(Game1.player.currentLocation, origin + new Vector2(0, -320), IconData.impacts.cloud, 1.5f, new() { interval = 75, color = Mod.instance.iconData.schemeColours[schemes.mists], flip = true, layer = 999f, });

            Mod.instance.iconData.ImpactIndicator(Game1.player.currentLocation, origin + new Vector2(0, -320), IconData.impacts.cloud, 4f, new() { interval = 125, frame = 3, color = Microsoft.Xna.Framework.Color.White, layer = 998f, alpha = 1f,});

            TemporaryAnimatedSprite boltAnimation = new(0, 75, 6, 1, originOffset, false, (Game1.random.NextDouble() < 0.5) ? true : false)
            {

                sourceRect = new(0, 0, 64, 128),

                sourceRectStartingPos = new Vector2(0, 0),

                texture = boltTexture,

                layerDepth = 997f,

                alpha = 0.65f,

                scale = 3f,

            };

            location.temporarySprites.Add(boltAnimation);

            Vector2 lightOffset = new(origin.X, origin.Y - 192);

            TemporaryAnimatedSprite lightAnimation = new(23, 500f, 1, 1, lightOffset, flicker: false, (Game1.random.NextDouble() < 0.5) ? true : false)
            {
                light = true,
                lightRadius = 6,
                lightcolor = Microsoft.Xna.Framework.Color.Black,
                alpha = 0.5f,
                alphaFade = 0.001f,
                Parent = location,

            };

            location.temporarySprites.Add(lightAnimation);

            location.playSound("flameSpellHit");

            return;*/

        }

        public List<TemporaryAnimatedSprite> ZapAnimation(GameLocation location, Vector2 origin, Vector2 destination, IconData.schemes scheme = schemes.golden, int size = 2)
        {

            List<TemporaryAnimatedSprite> animations = new();

            float boltScale = 0.5f * size;

            float distance = Vector2.Distance(origin, destination);

            Vector2 diff = destination - origin;

            Vector2 point = diff / distance;

            Vector2 middle = origin + (diff / 2);

            //Vector2 end = origin + (point * 144 * boltScale);

            float rotate = (float)Math.Atan2(diff.Y, diff.X) - (float)(Math.PI / 2);

            Vector2 originOffset = new(middle.X - (24*boltScale), middle.Y - ((int)distance / 2) - 8);

            Microsoft.Xna.Framework.Rectangle sourceRect2 = new(0, 400 - (int)(distance / boltScale), 48, (int)(distance / boltScale));

            TemporaryAnimatedSprite bolt1 = new(0, 75, 4, 1, originOffset, false, false)
            {

                sourceRect = sourceRect2,

                sourceRectStartingPos = new Vector2(sourceRect2.X, sourceRect2.Y),

                texture = boltTexture,

                layerDepth = 801f,

                scale = boltScale,

                color = Microsoft.Xna.Framework.Color.White,

                rotation = rotate,

            };

            location.temporarySprites.Add(bolt1);

            animations.Add(bolt1);

            sourceRect2.X += 192;

            TemporaryAnimatedSprite bolt2 = new(0, 75, 4, 1, originOffset, false, false)
            {

                sourceRect = sourceRect2,

                sourceRectStartingPos = new Vector2(sourceRect2.X, sourceRect2.Y),

                texture = boltTexture,

                layerDepth = 800f,

                scale = boltScale,

                color = gradientColours[scheme][0],

                rotation = rotate,

            };

            location.temporarySprites.Add(bolt2);

            animations.Add(bolt2);

            sourceRect2.X += 192;

            TemporaryAnimatedSprite bolt3 = new(0, 75, 4, 1, originOffset, false, false)
            {

                sourceRect = sourceRect2,

                sourceRectStartingPos = new Vector2(sourceRect2.X, sourceRect2.Y),

                texture = boltTexture,

                layerDepth = 800f,

                scale = boltScale,

                color = gradientColours[scheme][0],

                rotation = rotate,

            };

            location.temporarySprites.Add(bolt3);

            animations.Add(bolt3);

            // Zap indicator

            Microsoft.Xna.Framework.Rectangle sourceRect3 = new(12 + Mod.instance.randomIndex.Next(2)*48, 400, 24, 24);

            TemporaryAnimatedSprite zap1 = new(0, 300, 1, 1, origin - new Vector2(boltScale * 12), false, false)
            {

                sourceRect = sourceRect3,

                sourceRectStartingPos = new Vector2(sourceRect3.X, sourceRect3.Y),

                texture = boltTexture,

                layerDepth = 803f,

                scale = boltScale,

                color = Microsoft.Xna.Framework.Color.White,

            };

            location.temporarySprites.Add(zap1);

            animations.Add(zap1);

            sourceRect3.X += 192;

            TemporaryAnimatedSprite zap2 = new(0, 300, 1, 1, origin - new Vector2(boltScale * 12), false, false)
            {

                sourceRect = sourceRect3,

                sourceRectStartingPos = new Vector2(sourceRect3.X, sourceRect3.Y),

                texture = boltTexture,

                layerDepth = 802f,

                scale = boltScale,

                color = gradientColours[scheme][0],

            };

            location.temporarySprites.Add(zap2);

            animations.Add(zap2);


            // Light

            TemporaryAnimatedSprite lightCircle = new(23, 200f, 6, 1, destination, false, Game1.random.NextDouble() < 0.5)
            {
                texture = Game1.mouseCursors,
                light = true,
                lightRadius = 3f,
                lightcolor = Microsoft.Xna.Framework.Color.Black,
                alphaFade = 0.03f,
                Parent = location,
            };

            location.temporarySprites.Add(lightCircle);

            animations.Add(lightCircle);

            return animations;

        }

        public void AnimateQuickWarp(GameLocation location, Vector2 origin, bool reverse = false, warps warp = warps.portal)
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

                case warps.smoke:

                    CreateImpact(location, origin, impacts.plume, 2f, new() { color = schemeColours[schemes.npc_blackfeather], alpha = 1f, });

                    break;
            }

        }

        public List<TemporaryAnimatedSprite> AnimateWarpStrike(GameLocation location, Vector2 origin, int direction, IconData.schemes scheme = schemes.sword_steel)
        {

            List<TemporaryAnimatedSprite> animations = new();

            Microsoft.Xna.Framework.Rectangle source = WeaponRender.StrikeRectangle(direction);

            Vector2 center = WeaponRender.StrikePosition(origin, direction) - new Vector2(32);

            bool flip = WeaponRender.StrikeFlip(direction);

            float layer = center.Y / 10000;

            int interval = 150;

            // strike

            TemporaryAnimatedSprite strike = new(0, interval, 4, 1, center, false, flip)
            {
                sourceRect = source,
                sourceRectStartingPos = new Vector2(source.X, source.Y),
                texture = warpstrikeTexture,
                scale = 4f,
                layerDepth = layer + 0.001f,
                alpha = 0.75f,
            };

            location.temporarySprites.Add(strike);

            animations.Add(strike);

            // weapon 1

            Microsoft.Xna.Framework.Rectangle weaponSource = WeaponRender.WeaponRectangle(source);

            float layerOffset = WeaponRender.WeaponOffset(source);

            center.X -= 64;

            center.Y -= 64;

            TemporaryAnimatedSprite weapon = new(0, interval, 4, 1, center, false, flip)
            {
                sourceRect = weaponSource,
                sourceRectStartingPos = new Vector2(weaponSource.X, weaponSource.Y),
                texture = warpswordTexture,
                color = gradientColours[scheme][0],
                scale = 4f,
                layerDepth = layer + 0.001f + layerOffset,
                alpha = 0.75f,
            };

            location.temporarySprites.Add(weapon);

            animations.Add(weapon);

            // weapon 2

            weaponSource.X += 256;

            TemporaryAnimatedSprite weaponTwo = new(0, interval, 4, 1, center, false, flip)
            {
                sourceRect = weaponSource,
                sourceRectStartingPos = new Vector2(weaponSource.X, weaponSource.Y),
                texture = warpswordTexture,
                color = gradientColours[scheme][1],
                scale = 4f,
                layerDepth = layer + 0.001f + layerOffset,
                alpha = 0.75f,
            };

            location.temporarySprites.Add(weaponTwo);

            animations.Add(weaponTwo);

            // weapon 3

            weaponSource.X += 256;

            TemporaryAnimatedSprite weaponThree = new(0, interval, 4, 1, center, false, flip)
            {
                sourceRect = weaponSource,
                sourceRectStartingPos = new Vector2(weaponSource.X, weaponSource.Y),
                texture = warpswordTexture,
                color = gradientColours[scheme][2],
                scale = 4f,
                layerDepth = layer + 0.001f + layerOffset,
                alpha = 0.75f,
            };

            location.temporarySprites.Add(weaponThree);

            animations.Add(weaponThree);

            // swipe

            layerOffset = WeaponRender.SwipeOffset(source);

            Microsoft.Xna.Framework.Rectangle swipeSource = WeaponRender.SwipeRectangle(source);

            TemporaryAnimatedSprite swipe = new(0, interval, 4, 1, center, false, flip)
            {
                sourceRect = swipeSource,
                sourceRectStartingPos = new Vector2(swipeSource.X, swipeSource.Y),
                texture = warpswipeTexture,
                color = gradientColours[scheme][0],
                scale = 4f,
                layerDepth = layer + 0.001f + layerOffset,
                alpha = 0.5f,
            };

            location.temporarySprites.Add(swipe);

            animations.Add(swipe);

            return animations;

        }

        public void AdjustWarpStrke(List<TemporaryAnimatedSprite> animations,Vector2 origin,int direction)
        {

            Vector2 center = WeaponRender.StrikePosition(origin, direction) - new Vector2(32);

            animations[0].position = center;

            float layerOffset = WeaponRender.WeaponOffset(animations[0].sourceRect);

            center.X -= 64;

            center.Y -= 64;

            for(int i = 1; i < animations.Count - 1; i++)
            {

                animations[i].position = center;

                animations[i].layerDepth = animations[0].layerDepth + layerOffset;

            }

            layerOffset = WeaponRender.SwipeOffset(animations[0].sourceRect);

            animations[4].position = center;

            animations[4].layerDepth = animations[0].layerDepth + layerOffset;

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

        public float rotation = 0;

        public Microsoft.Xna.Framework.Color color = Microsoft.Xna.Framework.Color.White;

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
