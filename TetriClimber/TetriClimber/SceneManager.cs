﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Surface;
using Microsoft.Surface.Core;
using System.Windows.Forms;

namespace TetriClimber
{
    public class SceneManager : DrawableGameComponent
    {
        public enum EScene { ATRACT_MODE, SOLO, MULTI, BACKGROUND }
        private Dictionary<EScene, AScene> scenes = new Dictionary<EScene, AScene>();
        private static SceneManager instance = null;
        private EScene current;

        private SceneManager():base(App.Game)
        {
            scenes.Add(EScene.BACKGROUND, new Background());
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

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            foreach (KeyValuePair<EScene, AScene> pair in scenes)
                pair.Value.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            foreach (KeyValuePair<EScene, AScene> pair in scenes)
                pair.Value.Update(gameTime);
        }

        public void addScene(EScene e)
        {
            switch (e)
            {
                case EScene.SOLO:
                    scenes.Add(e, new OnePlayer());
                    current = EScene.SOLO;
                    MenuManager.Instance.Flush();
                    break;
                case EScene.MULTI:
                    scenes.Add(e, new TwoPlayer());
                    current = EScene.MULTI;
                    MenuManager.Instance.Flush();
                    break;
                default:
                    break;
            }
        }

        public void removeScene(EScene eScene)
        {
            scenes.Remove(eScene);
        }

        public void TogglePause(AScene scene)
        {
            scene.TogglePause();
        }

        public void removePlayScene()
        {
            removeScene(current);
        }
    }
}
