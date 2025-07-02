using Microsoft.Xna.Framework.Audio;
using StardewDruid.Cast;
using StardewValley;
using StardewValley.TerrainFeatures;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace StardewDruid.Handle
{
    public class SoundHandle
    {

        public enum SoundCue
        {

            None,
            CastWeald,
            CastMists,
            CastBolt,
            CastBeam,
            CastStars,
            ImpactStars,
            DragonFlight,
            DragonRoar,
            DragonGrowl,
            DragonFire,
            BearGrowl,
            BearRoar,
            WolfGrowl,
            BatScreech,
            SerpentCall,

        }

        public Dictionary<SoundCue, SoundInstance> soundcues = new();


        public SoundHandle()
        {

            /* Sound Effects Mixed and Produced by Neosinf from Generated Samples Attribute 3.0 ElevenStudios.com */

            soundcues.Clear();

            FileStream CastWealdStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "CastWeald.wav"), FileMode.Open);

            SoundEffect CastWealdSound = SoundEffect.FromStream(CastWealdStream);

            SoundInstance CastWealdInstance = new(CastWealdSound, -0.5f, 0.125f, 0.8f, 0.05f, 8);

            soundcues.Add(SoundCue.CastWeald, CastWealdInstance);

            // ---------------------------------

            FileStream CastMistsStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "CastMists.wav"), FileMode.Open);

            SoundEffect CastMistsSound = SoundEffect.FromStream(CastMistsStream);

            SoundInstance CastMistsInstance = new(CastMistsSound, -1f, 0.125f, 0.6f, 0.05f, 8);

            soundcues.Add(SoundCue.CastMists, CastMistsInstance);

            // ---------------------------------

            FileStream CastBoltStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "CastBolt.wav"), FileMode.Open);

            SoundEffect CastBoltSound = SoundEffect.FromStream(CastBoltStream);

            SoundInstance CastBoltInstance = new(CastBoltSound, -1f, 0.125f, 0.5f, 0.05f, 8);

            soundcues.Add(SoundCue.CastBolt, CastBoltInstance);

            // ---------------------------------

            FileStream CastBeamStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "CastBeam.wav"), FileMode.Open);

            SoundEffect CastBeamSound = SoundEffect.FromStream(CastBeamStream);

            SoundInstance CastBeamInstance = new(CastBeamSound, 0, 0.125f, 0.8f, 0.05f, 8);

            soundcues.Add(SoundCue.CastBeam, CastBeamInstance);

            // ---------------------------------

            FileStream CastStarsStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "CastStars.wav"), FileMode.Open);

            SoundEffect CastStarsSound = SoundEffect.FromStream(CastStarsStream);

            SoundInstance CastStarsInstance = new(CastStarsSound, -1, 0.125f, 0.3f, 0.02f, 8);

            soundcues.Add(SoundCue.CastStars, CastStarsInstance);

            // ---------------------------------

            FileStream ImpactStarsStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "ImpactStars.wav"), FileMode.Open);

            SoundEffect ImpactStarsSound = SoundEffect.FromStream(ImpactStarsStream);

            SoundInstance ImpactStarsInstance = new(ImpactStarsSound, -1, 0.125f, 0.5f, 0.05f, 8);

            soundcues.Add(SoundCue.ImpactStars, ImpactStarsInstance);

            // ---------------------------------

            FileStream DragonFlightStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "DragonFlight.wav"), FileMode.Open);

            SoundEffect DragonFlightSound = SoundEffect.FromStream(DragonFlightStream);

            SoundInstance DragonFlightInstance = new(DragonFlightSound, 0, 0.05f, 0.6f, 0.05f, 8);

            DragonFlightInstance.limit = 1;

            soundcues.Add(SoundCue.DragonFlight, DragonFlightInstance);

            // ---------------------------------

            FileStream DragonRoarStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "Roar.wav"), FileMode.Open);

            SoundEffect DragonRoarSound = SoundEffect.FromStream(DragonRoarStream);

            SoundInstance DragonRoarInstance = new(DragonRoarSound, -1, 0.25f, 1f, 0.05f, 5);

            DragonRoarInstance.limit = 1;

            soundcues.Add(SoundCue.DragonRoar, DragonRoarInstance);

            // ---------------------------------

            FileStream DragonGrowlStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "DragonGrowl.wav"), FileMode.Open);

            SoundEffect DragonGrowlSound = SoundEffect.FromStream(DragonGrowlStream);

            SoundInstance DragonGrowlInstance = new(DragonGrowlSound, -1f, 0.25f, 1f, 0.05f, 5);

            DragonGrowlInstance.limit = 1;

            soundcues.Add(SoundCue.DragonGrowl, DragonGrowlInstance);

            // ---------------------------------

            FileStream DragonFireStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "DragonFire.wav"), FileMode.Open);

            SoundEffect DragonFireSound = SoundEffect.FromStream(DragonFireStream);

            SoundInstance DragonFireInstance = new(DragonFireSound, -0.5f, 0.125f, 0.8f, 0.02f, 10);

            DragonFireInstance.limit = 3;

            soundcues.Add(SoundCue.DragonFire, DragonFireInstance);

            // ---------------------------------

            FileStream BearGrowlStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "BearGrowl.wav"), FileMode.Open);

            SoundEffect BearGrowlSound = SoundEffect.FromStream(BearGrowlStream);

            SoundInstance BearGrowlInstance = new(BearGrowlSound, -0.5f, 0.25f, 0.75f, 0.05f, 5);

            BearGrowlInstance.limit = 1;

            soundcues.Add(SoundCue.BearGrowl, BearGrowlInstance);

            // ---------------------------------

            FileStream BearRoarStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "BearRoar.wav"), FileMode.Open);

            SoundEffect BearRoarSound = SoundEffect.FromStream(BearRoarStream);

            SoundInstance BearRoarInstance = new(BearRoarSound, -0.5f, 0.25f, 0.5f, 0.05f, 5);

            BearRoarInstance.limit = 1;

            soundcues.Add(SoundCue.BearRoar, BearRoarInstance);

            // ---------------------------------

            FileStream WolfGrowlStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "WolfGrowl.wav"), FileMode.Open);

            SoundEffect WolfGrowlSound = SoundEffect.FromStream(WolfGrowlStream);

            SoundInstance WolfGrowlInstance = new(WolfGrowlSound, -0.5f, 0.25f, 1f, 0.05f, 5);

            WolfGrowlInstance.limit = 1;

            soundcues.Add(SoundCue.WolfGrowl, WolfGrowlInstance);

            // ---------------------------------

            FileStream BatScreechStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "BatScreech.wav"), FileMode.Open);

            SoundEffect BatScreechSound = SoundEffect.FromStream(BatScreechStream);

            SoundInstance BatScreechInstance = new(BatScreechSound, 0f, 0.125f, 1f, 0.05f, 5);

            BatScreechInstance.limit = 1;

            soundcues.Add(SoundCue.BatScreech, BatScreechInstance);

            // ---------------------------------

            FileStream SerpentCallStream = new(Path.Combine(Mod.instance.Helper.DirectoryPath, "Sounds", "SerpentCall.wav"), FileMode.Open);

            SoundEffect SerpentCallSound = SoundEffect.FromStream(SerpentCallStream);

            SoundInstance SerpentCallInstance = new(SerpentCallSound, 0f, 0.125f, 1f, 0.05f, 5);

            SerpentCallInstance.limit = 1;

            soundcues.Add(SoundCue.SerpentCall, SerpentCallInstance);

        }

        public bool PlayCue(SoundCue sound)
        {

            if (!soundcues.ContainsKey(sound))
            {

                return false;

            }

            if (!soundcues[sound].Check())
            {

                return false;

            }

            soundcues[sound].Play();

            return true;

        }

        public void StopCue(SoundCue sound)
        {

            if (soundcues.ContainsKey(sound))
            {

                soundcues[sound].Stop();

            }

            return;

        }

        public bool ActiveCue(SoundCue sound)
        {

            if (soundcues.ContainsKey(sound))
            {

                return soundcues[sound].Active();

            }

            return false;

        }

    }

    public class SoundInstance
    {

        public SoundEffect effect;

        public float pitch;

        public float pitchFloor;

        public float pitchAscend;


        public float volume;

        public float volumeFloor;

        public float volumeAscend;


        public double played;

        public int ascend;

        public int ascended;

        public double interval;

        public double timeout;


        public int limit = 5;

        public List<SoundEffectInstance> instances = new();

        public SoundInstance(SoundEffect Effect, float Pitch, float PitchRaise, float Volume, float VolumeRaise, int Ascend, double Timeout = 10.0)
        {
            
            effect = Effect;

            // -------------------------

            pitch = Pitch;

            pitchFloor = Pitch;

            pitchAscend = PitchRaise;

            // ------------------------

            volume = Volume;

            volumeFloor = Volume;

            volumeAscend = VolumeRaise;

            // -----------------------

            played = 0.0;

            ascend = Ascend;

            ascended = 0;

            timeout = Timeout;

        }

        public void Clean()
        {

            for (int i = instances.Count - 1; i >= 0; i--)
            {

                SoundEffectInstance effect = instances.ElementAt(i);

                if (effect.State == SoundState.Stopped || effect.State == SoundState.Paused)
                {

                    instances.RemoveAt(i);

                }

            }

        }

        public bool Check()
        {

            Clean();

            if (instances.Count >= limit)
            {

                return false;

            }

            return true;

        }

        public bool Active()
        {

            Clean();

            if (instances.Count > 0)
            {

                return true;

            }

            return false;

        }

        public void Play()
        {

            double current = Game1.currentGameTime.TotalGameTime.TotalSeconds;

            if (played + timeout < current)
            {

                ascended = 0;

                pitch = pitchFloor;

                volume = volumeFloor;

            }
            else
            {

                ascended++;

                if(ascended >= ascend)
                {

                    ascended = 0;

                    pitch = pitchFloor;

                    volume = volumeFloor;

                }
                else if (ascended == ascend - 1)
                {

                    pitch += (pitchAscend * 2);

                    volume += (volumeAscend * 3);
 
                }
                else
                {

                    pitch += pitchAscend;

                    volume += volumeAscend;

                }

            }

            played = current;

            float useVolume = Game1.options.soundVolumeLevel * volume;

            if (useVolume < 0f)
            {

                useVolume = 0f;

            }
            else if (useVolume > 1f)
            {

                useVolume = 1f;

            }

            float usePitch = pitch;

            if (usePitch < -1f)
            {

                usePitch = -1f;

            }
            else if (usePitch > 1f)
            {

                usePitch = 1f;

            }

            SoundEffectInstance instance = effect.CreateInstance();

            instance.Pitch = usePitch;

            instance.Volume = useVolume;

            instance.Pan = 0;
            
            instance.Play();

            instances.Add(instance);

            return;

        }

        public void Stop()
        {

            foreach (SoundEffectInstance instance in instances)
            {

                if (instance is SoundEffectInstance effect)
                {

                    if (effect.IsDisposed)
                    {

                        continue;

                    }

                    effect.Stop();

                }

            }

            instances.Clear();

        }

    }

}
