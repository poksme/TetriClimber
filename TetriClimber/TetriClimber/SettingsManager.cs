using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;

namespace TetriClimber
{
    public class SettingsManager
    {
        public enum EMode { EASY, MEDIUM, HARD, PRO }
        private static SettingsManager instance;

        public struct SettingData
        {
            public EMode mode;
            public bool sfx;
            public bool music;
        }
        SettingData data;
        String filename;
        public EMode Mode { get; private set; }
        public bool Music { get; private set; }
        public bool Sfx { get; private set; }
        public bool Shadow { get; private set; }
        public bool LimitLine { get; private set; }
        public bool NextTetrimino { get; private set; }
        public float Bonus { get; private set; }
        private Dictionary<EMode, Action> modes;

        private SettingsManager()
        {
            filename = "settings";
            Music = true;
            Sfx = true;
            modes = new Dictionary<EMode,Action>();
            modes.Add(EMode.EASY, setEasyMode);
            modes.Add(EMode.MEDIUM, setMediumMode);
            modes.Add(EMode.HARD, setHardMode);
            modes.Add(EMode.PRO, setProMode);
            
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

        //public void saveSetting()
        //{
        //    StorageContainer container;
        //    IAsyncResult result;
        //    StorageDevice device;

        //    data.mode = Mode;
        //    data.music = Music;
        //    data.sfx = Sfx;
        //    result = device.BeginOpenContainer("SettingContainer", null, null);
        //    result.AsyncWaitHandle.WaitOne();
        //    container = device.EndOpenContainer(result);
        //    result.AsyncWaitHandle.Close();

        //    if (container.FileExists(filename))
        //        container.DeleteFile(filename);

        //    Stream stream = container.CreateFile(filename);
        //    XmlSerializer serializer = new XmlSerializer(typeof(SettingData));
        //    serializer.Serialize(stream, data);
        //    stream.Close();
        //    container.Dispose();
        //}

        //public void readSetting()
        //{
        //    StorageDevice device = new StorageDevice();
        //    StorageContainer container;
        //    IAsyncResult result;

        //    result = device.BeginOpenContainer("SettingContainer", null, null);
        //    result.AsyncWaitHandle.WaitOne();
        //    container = device.EndOpenContainer(result);
        //    result.AsyncWaitHandle.Close();

        //    if (!container.FileExists(filename))
        //    {
        //        container.Dispose();
        //        setEasyMode();
        //        saveSetting();
        //        return;
        //    }

        //    Stream stream = container.OpenFile(filename, FileMode.Open);
        //    XmlSerializer serializer = new XmlSerializer(typeof(SettingData));
        //    data = (SettingData)serializer.Deserialize(stream);
        //    stream.Close();
        //    container.Dispose();
        //    modes[data.mode]();
        //}
    }
}
