using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Windows.Forms;
using Microsoft.Surface;
using Microsoft.Surface.Core;

namespace TetriClimber
{
    public class SceneManager : DrawableGameComponent
    {
        public enum EScene { SOLO, MULTI, BACKGROUND, END_GAME, TUTO, LEADER_BOARD, TITLE }
        private Dictionary<EScene, AScene> scenes = new Dictionary<EScene, AScene>();
        private static SceneManager instance = null;
        private EScene current;
        private List<EScene> removeRqst = new List<EScene>();
        private Dictionary<EScene, AScene> addRqst = new Dictionary<EScene,AScene>();

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
            foreach (EScene rqst in removeRqst)
                scenes.Remove(rqst);
            foreach (KeyValuePair<EScene, AScene> rqst in addRqst)
                scenes.Add(rqst.Key, rqst.Value);
            removeRqst.Clear();
            addRqst.Clear();
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

        public void requestRemoveScene(EScene e)
        {
            removeRqst.Add(e);
        }

        public void TogglePause(AScene scene)
        {
            scene.TogglePause();
        }

        public void requestRemovePlayScene()
        {
            removeRqst.Add(current);
        }

        public void requestAddScene(EScene e, AScene s)
        {
            //If KEY EXIST REMBALLE
            addRqst.Add(e, s);
        }

        public bool HasScene(EScene eScene)
        {
            return scenes.ContainsKey(eScene);
        }
    }
}
