using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TetriClimber
{
    public class SceneManager
    {
        public enum EScene {ATRACT_MODE, MAIN_MENU, PLAY, PAUSE_MENU}
        Dictionary<EScene, AScene> scenes = new Dictionary<EScene, AScene>();
        private static SceneManager instance = null;

        private SceneManager()
        {
            //scenes.add(escene.atract_mode, new atractmode());
        }

        public static SceneManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new SceneManager();
                return instance;
            }
        }
    }
}
