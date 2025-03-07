using Shared;

namespace CSharpSnake
{
    internal class Program
    {
       
        static void Main(string[] args)
        {
            ConsoleInput input = new ConsoleInput();
            SnakeGamePlayLogic snakeLogic = new SnakeGamePlayLogic();
            var colors = snakeLogic.CreatePalette();

            ConsoleRenderer currRenderer = new ConsoleRenderer(colors);
            ConsoleRenderer prevRenderer = new ConsoleRenderer(colors);


            snakeLogic.InitializeInput(input);


            var lastFrameTime = DateTime.Now;
            while (true)
            {
                var startFrameTime = DateTime.Now;
                float deltatime = (float)(startFrameTime - lastFrameTime).TotalSeconds;
                input.Update();
                snakeLogic.DrawNewState(deltatime, currRenderer);
                lastFrameTime = startFrameTime;
                if (!currRenderer.Equals(prevRenderer)) currRenderer.Render();

                var tmp = prevRenderer;
                prevRenderer = currRenderer;
                currRenderer = tmp;
                currRenderer.Clear();

                var nextFrameTime = startFrameTime + TimeSpan.FromSeconds(GameConfig.Instance.targetFrameTime);
                var endFrameTime = DateTime.Now;
                if (nextFrameTime > endFrameTime)
                {
                    Thread.Sleep((int)(nextFrameTime - endFrameTime).TotalMilliseconds);
                }
            }
        }
    }
}
