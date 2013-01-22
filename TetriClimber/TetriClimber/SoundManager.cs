﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace TetriClimber
{
    public class SoundManager
    {
        public enum ESound { CLEARLINE, DROP, FASTDROP, SHIFT, BGM, NONE }
        private Dictionary<ESound, SoundEffect> sounds;
        private SoundEffectInstance playing;
        private SoundEffectInstance bgm_;
        private static SoundManager instance = null;
        private ESound cur;

        private SoundManager()
        {
            return;
            sounds = new Dictionary<ESound, SoundEffect>();
            sounds.Add(ESound.CLEARLINE, App.ContentManager.Load<SoundEffect>("ClearLineSFX"));
            sounds.Add(ESound.DROP, App.ContentManager.Load<SoundEffect>("DropSFX"));
            sounds.Add(ESound.FASTDROP, App.ContentManager.Load<SoundEffect>("FastDropSFX"));
            sounds.Add(ESound.SHIFT, App.ContentManager.Load<SoundEffect>("ShiftSFX"));
            sounds.Add(ESound.BGM, App.ContentManager.Load<SoundEffect>("StageMusic"));
            playing = sounds[ESound.SHIFT].CreateInstance();
            cur = ESound.SHIFT;
            bgm_ = sounds[ESound.BGM].CreateInstance();
            bgm_.IsLooped = true;
            bgm_.Volume = 0.7f;
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


        internal void play(ESound eSound, float pitch = 0, float volume = 0.5f)
        {
            return;
            cur = eSound;
            if (!playing.IsDisposed)
            {
                playing.Stop();
                playing.Dispose();
            }
            playing = sounds[eSound].CreateInstance();
            playing.Pitch = pitch;
            playing.Volume = volume;
            playing.Play();
        }
        internal void bgmPause()
        {
            return;
            bgm_.Pause();
        }

        internal void stopPlaying()
        {
            return;
            if (!playing.IsDisposed)
            {
                playing.Stop();
                playing.Dispose();
            }
        }

        internal void bgmPlay()
        {
            return;
            bgm_.Play();
        }

        internal void setBgmPitch(float p)
        {
            return;
            bgm_.Pitch = p;
        }

        internal ESound getPlayingSound()
        {
            return ESound.NONE;
            if (playing.State == SoundState.Playing)
                return cur;
            return ESound.NONE;
        }

        internal void playClearLines(int i)
        {
            throw new NotImplementedException();
        }
    }
}
