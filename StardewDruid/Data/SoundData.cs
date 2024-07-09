using Microsoft.Xna.Framework.Audio;
using StardewValley;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Data
{
    internal static class SoundData
    {

        internal static void AddSounds()
        {

            /*DragonRoar: Dinosaur Roar by Mike Koenig Attribute 3.0 SoundBank.com */

            CueDefinition myCueDefinition = new CueDefinition();

            myCueDefinition.name = "DragonRoar";

            myCueDefinition.instanceLimit = 1;

            myCueDefinition.limitBehavior = CueDefinition.LimitBehavior.ReplaceOldest;

            FileStream soundstream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "Roar.wav"), FileMode.Open);

            SoundEffect roarSound = SoundEffect.FromStream(soundstream);

            myCueDefinition.SetSound(roarSound, Game1.audioEngine.GetCategoryIndex("Sound"), false);

            Game1.soundBank.AddCue(myCueDefinition);

            /*DragonFlight: Fire (7s) by Matt Script Personal Use License SoundSnap.com */

            CueDefinition dragonFlight = new CueDefinition();

            dragonFlight.name = "DragonFlight";

            dragonFlight.instanceLimit = 1;

            dragonFlight.limitBehavior = CueDefinition.LimitBehavior.ReplaceOldest;

            FileStream dragonFlightStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "DragonFlight.wav"), FileMode.Open);

            SoundEffect dragonFlightSound = SoundEffect.FromStream(dragonFlightStream);

            dragonFlight.SetSound(dragonFlightSound, Game1.audioEngine.GetCategoryIndex("Sound"), false);

            Game1.soundBank.AddCue(dragonFlight);

            /*DragonBreath: Fire (2s) by Lynne Publishing Personal Use License SoundSnap.com */

            CueDefinition dragonFire = new CueDefinition();

            dragonFire.name = "DragonFire";

            dragonFire.instanceLimit = 1;

            dragonFire.limitBehavior = CueDefinition.LimitBehavior.ReplaceOldest;

            FileStream dragonFireStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "DragonFire.wav"), FileMode.Open);

            SoundEffect dragonFireSound = SoundEffect.FromStream(dragonFireStream);

            dragonFire.SetSound(dragonFireSound, Game1.audioEngine.GetCategoryIndex("Sound"), false);

            Game1.soundBank.AddCue(dragonFire);

            /*DragonBreath: Fire (1s) by Matt Script Personal Use License SoundSnap.com */

            CueDefinition dragonFireTwo = new CueDefinition();

            dragonFireTwo.name = "DragonFireTwo";

            dragonFireTwo.instanceLimit = 1;

            dragonFireTwo.limitBehavior = CueDefinition.LimitBehavior.ReplaceOldest;

            FileStream dragonFireTwoStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "DragonFireTwo.wav"), FileMode.Open);

            SoundEffect dragonFireTwoSound = SoundEffect.FromStream(dragonFireTwoStream);

            dragonFireTwo.SetSound(dragonFireTwoSound, Game1.audioEngine.GetCategoryIndex("Sound"), false);

            Game1.soundBank.AddCue(dragonFireTwo);

        }

    }

}
