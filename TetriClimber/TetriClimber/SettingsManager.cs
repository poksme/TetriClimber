using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriClimber
{
    public class SettingsManager
    {
        public enum EMode { EASY, MEDIUM, HARD, PRO }
        private static SettingsManager instance;

        public EMode Mode { get; private set; }
        public bool Music { get; private set; }
        public bool Sfx { get; private set; }
        public bool Shadow { get; private set; }
        public bool LimitLine { get; private set; }
        public bool NextTetrimino { get; private set; }
        public float Bonus { get; private set; }


        private SettingsManager()
        {
            Music = true;
            Sfx = true;
            setEasyMode();
        }

        public static SettingsManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SettingsManager();
                return instance;
            }
        }

        public void setMusic(Object b)
        {
            Music = (bool)b;
        }
        
        public void setSfx(Object b)
        {
            Sfx = (bool)b;
        }
        public void setEasyMode()
        {
            Mode = EMode.EASY;
            Bonus = 1f;
            Shadow = true;
            LimitLine = true;
            NextTetrimino = true;
        }

        public void setMediumMode()
        {
            Mode = EMode.MEDIUM;
            Bonus = 0.8f;
            Shadow = true;
            LimitLine = false;
            NextTetrimino = false;
        }
            
        public void setHardMode()
        {
            Mode = EMode.HARD;
            Bonus = 0.4f;
            Shadow = false;
            LimitLine = false;
            NextTetrimino = false;
        }

        public void setProMode()
        {
            Mode = EMode.PRO;
            Bonus = 0f;
            Shadow = false;
            LimitLine = false;
            NextTetrimino = false;
        }
    }
}
