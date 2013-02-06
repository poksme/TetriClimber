using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class MenuManager : DrawableGameComponent
    {
        public enum EMenu { MAIN, PAUSE, OPTIONS}
        public delegate void HandlerAction(Object data = null);
        private static MenuManager instance = null;
        private Stack<AMenu> menus = null;

        private MenuManager():base(App.Game)
        {
            menus = new Stack<AMenu>();
        }

        public static MenuManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new MenuManager();
                return instance;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            CreateMainMenu();
        }

        public void CreateMainMenu()
        {
            menus.Push(new Menu(new List<AButton>()
                {
                    new TextButton("Play", new Vector2(100, 1 * 100), MenuManager.Instance.runScene, SceneManager.EScene.PLAY),
                    new TextButton("Options", new Vector2(100, 2 * 100), MenuManager.Instance.launchMenu, EMenu.OPTIONS),
                    new TextButton("Quit", new Vector2(100, 3 *100), MenuManager.Instance.Quit)
                }));
        }

        public void CreatePauseMenu()
        {
            menus.Push(new Menu(new List<AButton>()
                {
                    new TextButton("Resume", new Vector2(100, 1 * 100), MenuManager.Instance.ResumeGame, SceneManager.EScene.PLAY),
                    new TextButton("Options", new Vector2(100, 2 * 100), MenuManager.Instance.launchMenu, EMenu.OPTIONS),
                    new TextButton("Give Up", new Vector2(100, 3 *100), MenuManager.Instance.launchMenu, EMenu.MAIN)
                }));
        }

        public void CreateOptionMenu()
        {
            menus.Push(new Menu(new List<AButton>()
                {
                    new TextButton("Sound", new Vector2(100, 1 * 100), MenuManager.Instance.CreateSoundMenu),
                    new TextButton("Difficulty", new Vector2(100, 2 * 100), MenuManager.Instance.CreateDifficultyMenu),
                    new TextButton("Back", new Vector2(100, 3 * 100), MenuManager.Instance.BackMenu),
                }));
        }

        public void CreateDifficultyMenu(Object data = null)
        {
        }

        public void CreateSoundMenu(Object data = null)
        {
            menus.Push(new Menu(new List<AButton>()
                {
                    new ToggleButton("Music", new Vector2(100, 1 * 100), SettingsManager.Instance.setMusic,  SettingsManager.Instance.Music),
                    new ToggleButton("Sound Effects", new Vector2(100, 2 * 100), SettingsManager.Instance.setSfx, SettingsManager.Instance.Sfx),
                    new TextButton("Back", new Vector2(100, 3 * 100), MenuManager.Instance.BackMenu)
                }));
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (menus.Count > 0)
                menus.First().Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (menus.Count > 0)
                menus.First().Update(gameTime);
        }

        public void Flush()
        {
            menus.Clear();
        }

        #region ActionMenu

        public void launchMenu(Object data = null)
        {
            switch ((EMenu)data)
            {
                case EMenu.MAIN:
                    CreateMainMenu();
                    SceneManager.Instance.removeScene(SceneManager.EScene.PLAY);
                    break;
                case EMenu.PAUSE:
                    CreatePauseMenu();
                    break;
                case EMenu.OPTIONS:
                    CreateOptionMenu();
                    break;
                default:
                    break;
            }
        }

        public void BackMenu(Object data = null)
        {
            menus.Pop();
        }

        public void ResumeGame(Object data = null)
        {
            BackMenu();
            SceneManager.Instance.TogglePause((SceneManager.EScene)data);
        }

        public void runScene(Object data = null)
        {
            SceneManager.Instance.addScene((SceneManager.EScene)data);
        }

        public void Quit(Object data = null)
        {
            App.Game.Exit();
        }
        #endregion
    }
}
