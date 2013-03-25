using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TetriClimber
{
    public class TwoPlayer : APlay
    {
        private GameSession player2;
        private TimeSpan timer;

        public TwoPlayer():
            base(CoordHelper.EProfile.TWOPLAYER)
        {
            player2 = new GameSession(CoordHelper.EProfile.TWOPLAYER, hud);
            timer = TimeSpan.FromMilliseconds(120000);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.IsPause)
                return;            
            //PLAYER1
            if (!player1.death)
            {
                if (App.UserInput.isPressed(AUserInput.EInputKeys.RIGHT, AUserInput.EGameMode.MULTI1P))
                {
                    if (ipt != null)
                        ipt.recenterStartingPoint(player1.rightMove() ? 1 : 0, AUserInput.EGameMode.MULTI1P);
                    else
                        player1.rightMove();
                }
                if (App.UserInput.isPressed(AUserInput.EInputKeys.LEFT, AUserInput.EGameMode.MULTI1P))
                {
                    if (ipt != null)
                        ipt.recenterStartingPoint(player1.leftMove() ? -1 : 0, AUserInput.EGameMode.MULTI1P);
                    else
                        player1.leftMove();
                }
                if (App.UserInput.isPressed(AUserInput.EInputKeys.DOWN, AUserInput.EGameMode.MULTI1P))
                    player1.rightShift();
                if (App.UserInput.isPressed(AUserInput.EInputKeys.UP, AUserInput.EGameMode.MULTI1P))
                    player1.leftShift();
                if (App.UserInput.isPressed(AUserInput.EInputKeys.SPACE_BAR, AUserInput.EGameMode.MULTI1P))
                    player1.dropDown();
                player1.addInfluence(control.leftSpeed + control.rightSpeed);
                player1.Update(gameTime);
            }
            else
            {
                SceneManager.Instance.requestRemovePlayScene();
                SceneManager.Instance.requestAddScene(SceneManager.EScene.END_GAME, new EndGame(player2.score, CoordHelper.EProfile.TWOPLAYER));
            }

            //PLAYER 2
            if (!player2.death)
            {
                if (App.UserInput.isPressed(AUserInput.EInputKeys.RIGHT, AUserInput.EGameMode.MULTI2P))
                {
                    if (ipt != null)
                        ipt.recenterStartingPoint(player2.rightMove() ? 1 : 0, AUserInput.EGameMode.MULTI2P);
                    else
                        player2.rightMove();
                }
                if (App.UserInput.isPressed(AUserInput.EInputKeys.LEFT, AUserInput.EGameMode.MULTI2P))
                {
                    if (ipt != null)
                        ipt.recenterStartingPoint(player2.leftMove() ? -1 : 0, AUserInput.EGameMode.MULTI2P);
                    else
                        player2.leftMove();
                }
                if (App.UserInput.isPressed(AUserInput.EInputKeys.DOWN, AUserInput.EGameMode.MULTI2P))
                    player2.rightShift();
                if (App.UserInput.isPressed(AUserInput.EInputKeys.UP, AUserInput.EGameMode.MULTI2P))
                    player2.leftShift();
                if (App.UserInput.isPressed(AUserInput.EInputKeys.SPACE_BAR, AUserInput.EGameMode.MULTI2P))
                    player2.dropDown();
                player2.addInfluence(control.leftSpeed + control.rightSpeed);
                player2.Update(gameTime);
            }
            else
            {
                SceneManager.Instance.requestRemovePlayScene();
                SceneManager.Instance.requestAddScene(SceneManager.EScene.END_GAME, new EndGame(player1.score, CoordHelper.EProfile.ONEPLAYER));
            }
            timer -= gameTime.ElapsedGameTime;
            if (timer <= TimeSpan.Zero)
            {
                if (player1.score.TotalScore == player2.score.TotalScore
                    && player1.score.ClimbyScore == player2.score.ClimbyScore)
                    return ;
                SceneManager.Instance.requestRemovePlayScene();

                if (player1.score.TotalScore > player2.score.TotalScore)
                    SceneManager.Instance.requestAddScene(SceneManager.EScene.END_GAME, new EndGame(player1.score, CoordHelper.EProfile.ONEPLAYER));
                else if (player1.score.TotalScore < player2.score.TotalScore)
                    SceneManager.Instance.requestAddScene(SceneManager.EScene.END_GAME, new EndGame(player2.score, CoordHelper.EProfile.TWOPLAYER));
                else if (player1.score.ClimbyScore > player2.score.ClimbyScore)
                    SceneManager.Instance.requestAddScene(SceneManager.EScene.END_GAME, new EndGame(player1.score, CoordHelper.EProfile.ONEPLAYER));
                else
                    SceneManager.Instance.requestAddScene(SceneManager.EScene.END_GAME, new EndGame(player2.score, CoordHelper.EProfile.TWOPLAYER));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            player1.Draw(gameTime);
            player2.Draw(gameTime);
            SpriteManager.Instance.drawRotatedAtPos(SpriteManager.ESprite.CLIMBYRED, new Vector2(Constants.Measures.landscapeWidth / 2 - 115/2, CoordHelper.Instance.nextPosY + CoordHelper.Instance.nextBoxH - 50), -MathHelper.ToRadians((float)(timer.TotalMilliseconds * 360) / 120000 + 90), 115);
        }
    }
}
