using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnake
{
    internal class SnakeGameplayState : BaseGameState
    {
        public enum SnakeDir
        {
            Up, Down, Left, Right
        }

        private char _snakeSymbol = '■';
        private char _foodSymbol = '@';

        private List<Vector2> _body = new();
        private Vector2 _apple = new();

        private SnakeDir currentDir = SnakeDir.Left;
        private float _timeToMove = 0;
        private Random _random = new Random();

        public bool gameOver {  get; private set; }
        public bool hasWon {  get; private set; }
        public int fieldWidth {  get; set; }
        public int fieldHeight { get; set; }

        public int level;

        private float ratio = 0.5f;
        public override void Reset()
        {
            
            _body.Clear();
            int middleY = fieldHeight / 2;
            int middleX = fieldWidth / 2;
            
            currentDir = SnakeDir.Left;
            gameOver = false;
            hasWon = false;
            _timeToMove = 0;

            _body.Add(new(middleX + 3, middleY));


            _apple = new Vector2(middleX, middleY);

        }
        public override void Update(float deltaTime)
        {
            _timeToMove -= deltaTime;
            if(_timeToMove > 0 || gameOver)
            {
                return;
            }
            _timeToMove = GameConfig.Instance.frameDelay / (level + 4);


            var head = _body[0];
            var nextCell = ShiftTo(head, currentDir);
            if (nextCell.Equals(_apple))
            {
                _body.Insert(0, _apple);
                if (_body.Count >= level + 3)
                    hasWon = true;
                GenerateApple();
                return;

            }

            if (nextCell.x < 0 || nextCell.y < 0 || nextCell.x >= fieldWidth || nextCell.y >= fieldHeight)
            {
                gameOver = true;
                return;
            }

            _body.RemoveAt(_body.Count - 1);
            _body.Insert(0, nextCell);


        }
        private Vector2 ShiftTo(Vector2 from, SnakeDir toDir)
        {
            switch (toDir)
            {
                case SnakeDir.Up:
                    return new Vector2(from.x, from.y - 1);
                case SnakeDir.Down:
                    return new Vector2(from.x, from.y + 1);
                case SnakeDir.Left:
                    return new Vector2(from.x - 1, from.y);
                case SnakeDir.Right:
                    return new Vector2(from.x + 1, from.y);
            }

            return from;
        }
        public void SetDirection(SnakeDir dir)
        {
            currentDir = dir;
        }
        
        public override void Draw(ConsoleRenderer renderer)
        {
            renderer.DrawString($"Level: {level}", 2, 1, ConsoleColor.White);
            renderer.DrawString($"Score: {_body.Count - 1}", 2, 2, ConsoleColor.White);

            foreach (Vector2 cell in _body)
            {

                renderer.SetPixel(cell.x, cell.y, _snakeSymbol, 3);

            }
            renderer.SetPixel(_apple.x, _apple.y, _foodSymbol, 1);
        }
        public void GenerateApple()
        {
            Vector2 cell = new Vector2(_random.Next(0, fieldWidth), _random.Next(0, fieldHeight));
            if (_body.Equals(cell))
            {
                GenerateApple();
            }
            _apple = cell;
        }
        public override bool IsDone()
        {
            return gameOver || hasWon;
        }
    }
}
