using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    class AttractMode : AMode
    {
        public AttractMode()
            : base(TimeSpan.Zero) // ZERO FOR NO TRANSITION
        {

        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            SceneManager.Instance.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // // PLACED IN SCENES
            //if (App.UserInput is KeyboardInput)
            //{
            //    foreach (AUserInput.EInputKeys ipt in Enum.GetValues(typeof(AUserInput.EInputKeys)))
            //        if (App.UserInput.isPressed(ipt, AUserInput.EGameMode.SOLO))
            //        {
            //            ModeManager.Instance.TryChangeMode(ModeManager.EMode.GAME_MODE);
            //            return;
            //        }
            //}
            //else
            //{
            //    if ((App.UserInput as TouchInput).hasTapEvent)
            //    {
            //        ModeManager.Instance.TryChangeMode(ModeManager.EMode.GAME_MODE);
            //        return;
            //    }
            //}
            if (!SceneManager.Instance.HasScene(SceneManager.EScene.TITLE) &&
                !SceneManager.Instance.HasScene(SceneManager.EScene.TUTO) &&
                !SceneManager.Instance.HasScene(SceneManager.EScene.LEADER_BOARD))
            {
                if ((ModeManager.Instance.getArguments() is SceneManager.EScene) && (SceneManager.EScene)(ModeManager.Instance.getArguments()) == SceneManager.EScene.TUTO)
                    SceneManager.Instance.requestAddScene(SceneManager.EScene.TUTO, new TutoScene());
                else
                    SceneManager.Instance.requestAddScene(SceneManager.EScene.TITLE, new TitleScene());

            }
            SceneManager.Instance.Update(gameTime);
        }

        public override bool FadeOut(GameTime gt)
        {
            if (base.FadeOut(gt) == true) // VERIFY IF FADE TIME IS SPENT
            {
                ReinitFadeTime(TimeSpan.Zero); // ZERO FOR NO TRANSITIONS

                // HERE RE-INIT VALUES BEFORE NEVER BEING UPDATED AGAIN
                if (SceneManager.Instance.HasScene(SceneManager.EScene.TITLE))
                    SceneManager.Instance.requestRemoveScene(SceneManager.EScene.TITLE);
                if (SceneManager.Instance.HasScene(SceneManager.EScene.TUTO))
                    SceneManager.Instance.requestRemoveScene(SceneManager.EScene.TUTO);
                if (SceneManager.Instance.HasScene(SceneManager.EScene.LEADER_BOARD))
                    SceneManager.Instance.requestRemoveScene(SceneManager.EScene.LEADER_BOARD);

                return true;
            }
            else
            {

                // HERE PUT FADE OUT ANIMATION USING THE FADETIME TO ANIMATE IT
                // THIS WILL REPLACE THE UPDATE

                return false;
            }
        }
    }
}
