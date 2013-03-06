using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    class ModeManager : DrawableGameComponent
    {
        public enum EMode 
        { 
            ATTRACT_MODE,   // (TITLE -> TUTORIAL -> SCORE) LOOP
            GAME_MODE,    // FULL GAME WITH MENU / MULTI / SOLO
            BACKGROUND_MODE // THE APP RUNS IN BACKGROUND
        }
        private Dictionary<EMode, AMode> modes;
        private EMode curMode;
        private EMode nextMode;
        private static ModeManager instance = null;

        private ModeManager()
            : base(App.Game)
        {
            curMode = EMode.ATTRACT_MODE;
            nextMode = EMode.ATTRACT_MODE;
            //curMode = EMode.GAME_MODE;
            //nextMode = EMode.GAME_MODE;
            modes = new Dictionary<EMode, AMode>()
            {
                {EMode.ATTRACT_MODE, new AttractMode()},
                {EMode.GAME_MODE, new GameMode()},
                {EMode.BACKGROUND_MODE, new BackgroundMode()}
            };
        }

        public static ModeManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ModeManager();
                return instance;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            modes[curMode].Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (curMode != nextMode && modes[curMode].FadeOut(gameTime)) // IF A MODE CHANGE IS REQUESTED FADE OUT CURMODE
                curMode = nextMode; // IF FADE OUT FINISHED THEN SET THE NEW MODE
            if (curMode == nextMode) // IF IS FADING OUT
                modes[curMode].Update(gameTime); // DONT UPDATE
        }

        public bool TryChangeMode(EMode e) // TRY TO CHANGE MODE BUT MAY FAIL
        {
            if (curMode == nextMode) // NO RUNNING TRANSITIONS
            {
                nextMode = e;
                return true;
            }
            return false; // THERE IS ALREADY A RUNNING TRANSITION
        }
    }
}
