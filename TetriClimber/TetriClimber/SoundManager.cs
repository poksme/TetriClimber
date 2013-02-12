using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace TetriClimber
{
    public class SoundManager
    {
        private static SoundManager instance = null;
        public enum ESound { CLEARLINE, DROP, FASTDROP, SHIFT, BGM, OPTBGM, NONE }
        public enum EChannel { BGM, SFX }
        private Dictionary<ESound, SoundEffect> sounds;
        private Dictionary<EChannel, SoundEffectInstance> channels;

        private SoundManager()
        {
            sounds = new Dictionary<ESound, SoundEffect>();
            sounds.Add(ESound.CLEARLINE, App.ContentManager.Load<SoundEffect>("ClearLineSFX"));
            sounds.Add(ESound.DROP, App.ContentManager.Load<SoundEffect>("DropSFX"));
            sounds.Add(ESound.FASTDROP, App.ContentManager.Load<SoundEffect>("FastDropSFX"));
            sounds.Add(ESound.SHIFT, App.ContentManager.Load<SoundEffect>("ShiftSFX"));
            sounds.Add(ESound.BGM, App.ContentManager.Load<SoundEffect>("StageMusic"));
            sounds.Add(ESound.OPTBGM, App.ContentManager.Load<SoundEffect>("OptionMusic"));

            channels = new Dictionary<EChannel, SoundEffectInstance>();
            channels.Add(EChannel.SFX, sounds[ESound.SHIFT].CreateInstance());
            channels.Add(EChannel.BGM, sounds[ESound.BGM].CreateInstance());
        }

        public static SoundManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SoundManager();
                return instance;
            }
        }


        internal void play(EChannel eChannel, ESound eSound, float pitch = 0, float volume = 0.5f, bool muteOther = true)
        {
            if ((eChannel == EChannel.BGM && !SettingsManager.Instance.Music)
                || (eChannel == EChannel.SFX && !SettingsManager.Instance.Sfx)
                || (muteOther == false && !channels[eChannel].IsDisposed && channels[eChannel].State == SoundState.Playing))// && channels[eChannel].Equals(tmp)))
                return;
            if (!channels[eChannel].IsDisposed)
            {
                channels[eChannel].Stop();
                channels[eChannel].Dispose();
            }
            channels[eChannel] = sounds[eSound].CreateInstance();
            channels[eChannel].Pitch = pitch;
            channels[eChannel].Volume = volume;
            channels[eChannel].IsLooped = (eChannel == EChannel.BGM);
            //channels[eChannel].IsLooped = loop;
            channels[eChannel].Play();
        }

        internal void pause(EChannel eChannel)
        {
            channels[eChannel].Pause();
        }

        internal void stop(EChannel eChannel)
        {
            if (!channels[eChannel].IsDisposed)
            {
                channels[eChannel].Stop();
                channels[eChannel].Dispose();
            }
        }
    }
}
