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


            /*WolfGrowl: 410920-Monster_Within_-Werewolf_-Bark-03-04 by Sound Morph, Personal Use License SoundSnap.com */

            CueDefinition wolfGrowl = new CueDefinition();

            wolfGrowl.name = "WolfGrowl";

            wolfGrowl.instanceLimit = 1;

            wolfGrowl.limitBehavior = CueDefinition.LimitBehavior.ReplaceOldest;

            FileStream wolfGrowlStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "WolfGrowl.wav"), FileMode.Open);

            SoundEffect wolfGrowlSound = SoundEffect.FromStream(wolfGrowlStream);

            wolfGrowl.SetSound(wolfGrowlSound, Game1.audioEngine.GetCategoryIndex("Sound"), false);

            Game1.soundBank.AddCue(wolfGrowl);
            

            /*BearGrowl: 83105-Monster_snarl_slow_growl_creature-BLASTWAVEFX-20519 Personal Use License SoundSnap.com */

            CueDefinition bearGrowl = new CueDefinition();

            bearGrowl.name = "BearGrowl";

            bearGrowl.instanceLimit = 1;

            bearGrowl.limitBehavior = CueDefinition.LimitBehavior.ReplaceOldest;

            FileStream bearGrowlStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "BearGrowl.wav"), FileMode.Open);

            SoundEffect bearGrowlSound = SoundEffect.FromStream(bearGrowlStream);

            bearGrowl.SetSound(bearGrowlSound, Game1.audioEngine.GetCategoryIndex("Sound"), false);

            Game1.soundBank.AddCue(bearGrowl);


            /*BearGrowlTwo: 83056-Monster_snarl_growl_creature-BLASTWAVEFX-20517 Personal Use License SoundSnap.com */

            CueDefinition bearGrowlTwo = new CueDefinition();

            bearGrowlTwo.name = "BearGrowlTwo";

            bearGrowlTwo.instanceLimit = 1;

            bearGrowlTwo.limitBehavior = CueDefinition.LimitBehavior.ReplaceOldest;

            FileStream bearGrowlTwoStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "BearGrowlTwo.wav"), FileMode.Open);

            SoundEffect bearGrowlTwoSound = SoundEffect.FromStream(bearGrowlTwoStream);

            bearGrowlTwo.SetSound(bearGrowlTwoSound, Game1.audioEngine.GetCategoryIndex("Sound"), false);

            Game1.soundBank.AddCue(bearGrowlTwo);


            /*BearGrowlThree: 74733-Monster_snarl_growl_creature-BLASTWAVEFX-10267 Personal Use License SoundSnap.com */

            CueDefinition bearGrowlThree = new CueDefinition();

            bearGrowlThree.name = "BearGrowlThree";

            bearGrowlThree.instanceLimit = 1;

            bearGrowlThree.limitBehavior = CueDefinition.LimitBehavior.ReplaceOldest;

            FileStream bearGrowlThreeStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "BearGrowlThree.wav"), FileMode.Open);

            SoundEffect bearGrowlThreeSound = SoundEffect.FromStream(bearGrowlThreeStream);

            bearGrowlThree.SetSound(bearGrowlThreeSound, Game1.audioEngine.GetCategoryIndex("Sound"), false);

            Game1.soundBank.AddCue(bearGrowlThree);



        }

    }

}
