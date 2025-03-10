using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CSharpSnake.SnakeGameplayState;

namespace CSharpSnake
{
    internal class SnakeGamePlayLogic : BaseGamePlayLogic
    {
        private SnakeGameplayState gameplayState = new SnakeGameplayState();
        private ShowTextState showTextState = new ShowTextState(2);
        private bool newGamePanding = false;
        private int currentLevel;


        public void GotoGameplay()
        {
            gameplayState.level = currentLevel;
            gameplayState.fieldHeight = screenHeight;
            gameplayState.fieldWidth = screenWidth;
            ChangeState(gameplayState);
            gameplayState.Reset();
        }
        public override void OnArrowUp()
        {
            gameplayState.SetDirection(SnakeDir.Up);
        }

        public override void OnArrowDown()
        {
            gameplayState.SetDirection(SnakeDir.Down);
        }

        public override void OnArrowLeft()
        {
            gameplayState.SetDirection(SnakeDir.Left);
        }

        public override void OnArrowRight()
        {
            gameplayState.SetDirection(SnakeDir.Right);
        }

        public override void Update(float deltaTime)
        {
            if (currentState != null && !currentState.IsDone())
                return;
            if(currentState == null || currentState == gameplayState && !gameplayState.gameOver)
            {
                GoToNextLevel();
            }
            else if (currentState == gameplayState && gameplayState.gameOver)
            {
                GoToGameOver();
            }
            else if(currentState != null && newGamePanding == true)
            {
                GoToNextLevel();
            }
            else if (currentState != gameplayState && newGamePanding == false)
            {
                GotoGameplay();
            }
        }
        public override ConsoleColor[] CreatePalette()
        {
            return
            [
                ConsoleColor.Green,
                ConsoleColor.Red,
                ConsoleColor.White,
                ConsoleColor.Blue,
            ];
        }
        public void GoToGameOver()
        {
            currentLevel = 0;
            newGamePanding = true;
            showTextState.text = "Game Over!!";
            ChangeState(showTextState);
        }

        public void GoToNextLevel()
        {
            currentLevel++;
            newGamePanding = false;
            showTextState.text = $"Level: {currentLevel}";
            ChangeState(showTextState);
        }
    }
}

