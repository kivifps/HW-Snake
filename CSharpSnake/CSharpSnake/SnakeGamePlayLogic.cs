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


        public void GotoGameplay()
        {
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
            if (currentState != gameplayState)
                GotoGameplay();
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
    }
}

