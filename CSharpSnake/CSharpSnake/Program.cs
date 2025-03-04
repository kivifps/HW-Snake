namespace CSharpSnake
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConsoleInput input = new ConsoleInput();
            SnakeControlLogic snakeController = new SnakeControlLogic();
            snakeController.InitializeInput(input);
            snakeController.GotoGameplay();


            var lastFrameTime = DateTime.Now;
            while (true)
            {
                input.Update();
                var frameStartTime = DateTime.Now;
                float deltaTime = (float)(frameStartTime - lastFrameTime).TotalSeconds;
                snakeController.Update(deltaTime);
                lastFrameTime = frameStartTime;
            }
        }
    }
}
