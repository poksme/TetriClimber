using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class SceneManager : DrawableGameComponent
    {
        public enum EScene {ATRACT_MODE, MAIN_MENU, PLAY, PAUSE_MENU}
        Dictionary<EScene, AScene> scenes = new Dictionary<EScene, AScene>();
        private static SceneManager instance = null;

        private SceneManager():base(App.Game)
        {
            scenes.add(EScene.PLAY, new Play());
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
