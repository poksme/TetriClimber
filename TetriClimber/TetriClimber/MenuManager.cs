﻿using System;
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

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            if (menus.Count > 0)
            {
                if (menus.First().mt == AMenu.MenuType.MAIN_MENU)
                    SpriteManager.Instance.drawAtPos(SpriteManager.ESprite.LOGO, new Vector2((1920 - 1001) / 2, 70));
                menus.First().Draw(gameTime);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (menus.Count > 0)
            {
                if (menus.First().mt == AMenu.MenuType.MAIN_MENU)
                    SoundManager.Instance.stop(SoundManager.EChannel.BGM);
                menus.First().Update(gameTime);
            }
        }

        public void Flush()
        {
            menus.Clear();
        }

        #region CreateMenu
        public void CreateMainMenu()
        {
            Menu main = new Menu(AMenu.MenuType.MAIN_MENU);
            main.setButtons(new List<AButton>()
                {
                    new TextButton(main, "Solo", new Vector2(0, 4 * 100 + 50), MenuManager.Instance.runScene, SceneManager.EScene.SOLO),
                    new TextButton(main, "Multi", new Vector2(0, 5 * 100 + 50), MenuManager.Instance.runScene, SceneManager.EScene.MULTI),
                    new TextButton(main, "Options", new Vector2(0, 6 * 100 + 50), MenuManager.Instance.launchMenu, EMenu.OPTIONS),
                    new TextButton(main, "Tutorial", new Vector2(0, 7 * 100 + 50), MenuManager.Instance.changeMode),
                    new TextButton(main, "Quit", new Vector2(0, 8 * 100 + 50), MenuManager.Instance.Quit)
                });
            main.Center();
            menus.Push(main);
        }

        public void CreatePauseMenu(AScene target)
        {
            Menu pause = new Menu();
            pause.setButtons(new List<AButton>()
                {
                    new TextButton(pause, "Resume", new Vector2(0, 0), MenuManager.Instance.ResumeGame, target),
                    new TextButton(pause, "Options", new Vector2(0, 1 * 100), MenuManager.Instance.launchMenu, EMenu.OPTIONS),
                    new TextButton(pause, "Give Up", new Vector2(0, 2 *100), MenuManager.Instance.launchMenu, EMenu.MAIN)
                });
            pause.Center();
            menus.Push(pause);
            SoundManager.Instance.play(SoundManager.EChannel.BGM, SoundManager.ESound.OPTBGM, 0, 0.5f, true);
        }

        public void CreateOptionMenu()
        {
            Menu option = new Menu();
            option.setButtons(new List<AButton>()
                {
                    new TextButton(option, "Sound", new Vector2(0, 0), MenuManager.Instance.CreateSoundMenu),
                    new TextButton(option, "Difficulty", new Vector2(0, 1 * 100), MenuManager.Instance.CreateDifficultyMenu),
                    new TextButton(option, "Back", new Vector2(0, 2 * 100), MenuManager.Instance.SaveSetting),
                });
            option.Center();
            menus.Push(option);
        }

        public void CreateDifficultyMenu(Object data = null)
        {
            Menu dm = new Menu();
            dm.setButtons(new List<AButton>()
                {
                    new TextButton(dm, "Easy", new Vector2(0,0), MenuManager.Instance.setMode, SettingsManager.EMode.EASY),
                    new TextButton(dm, "Narmol", new Vector2(0, 1 * 100), MenuManager.Instance.setMode, SettingsManager.EMode.MEDIUM),
                    new TextButton(dm, "Hard", new Vector2(0, 2 * 100), MenuManager.Instance.setMode, SettingsManager.EMode.HARD),
                    new TextButton(dm, "Pro", new Vector2(0, 3 * 100), MenuManager.Instance.setMode, SettingsManager.EMode.PRO)
                });
            dm.Select((int)SettingsManager.Instance.Mode);
            dm.Center();
            menus.Push(dm);
        }

        public void CreateSoundMenu(Object data = null)
        {
            Menu sound = new Menu();
            sound.setButtons(new List<AButton>()
                {
                    new ToggleButton(sound, "Music", new Vector2(0,0), SettingsManager.Instance.setMusic,  SettingsManager.Instance.Music),
                    new ToggleButton(sound, "Sound Fx", new Vector2(0, 1 * 100), SettingsManager.Instance.setSfx, SettingsManager.Instance.Sfx),
                    new TextButton(sound, "Back", new Vector2(0, 2 * 100), MenuManager.Instance.BackMenu)
                });
            sound.Center();
            menus.Push(sound);
        }
        #endregion

        #region ActionMenu

        public void launchMenu(Object data = null)
        {
            switch ((EMenu)data)
            {
                case EMenu.MAIN:
                    CreateMainMenu();
                    SceneManager.Instance.requestRemovePlayScene();
                    SoundManager.Instance.stop(SoundManager.EChannel.BGM);
                    break;
                case EMenu.OPTIONS:
                    CreateOptionMenu();
                    SoundManager.Instance.play(SoundManager.EChannel.BGM, SoundManager.ESound.OPTBGM, 0, 0.5f, false);
                    //SoundManager.Instance.bgmPlay(SoundManager.ESound.OPTBGM);
                    break;
                default:
                    break;
            }
        }

        public void setMode(Object data)
        {
            SettingsManager.Instance.setMode((SettingsManager.EMode)data);
            BackMenu();
        }

        public void SaveSetting(Object data = null)
        {
            SettingsManager.Instance.saveSetting();
            BackMenu();
        }

        public void BackMenu(Object data = null)
        {
            menus.Pop();
        }

        public void ResumeGame(Object target)
        {
            BackMenu();
            SceneManager.Instance.TogglePause((AScene)target);
            SoundManager.Instance.play(SoundManager.EChannel.BGM, SoundManager.ESound.BGM);
        }

        public void runScene(Object data = null)
        {
            SceneManager.Instance.addScene((SceneManager.EScene)data);
            // if one player
            SoundManager.Instance.play(SoundManager.EChannel.BGM, SoundManager.ESound.BGM);
        }

        public void changeMode(Object data = null)
        {
            ModeManager.Instance.TryChangeMode(ModeManager.EMode.ATTRACT_MODE, SceneManager.EScene.TUTO);
        }

        public void Quit(Object data = null)
        {
            App.Game.Exit();
        }
        #endregion
    }
}
