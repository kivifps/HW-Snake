using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<Vector2> _body = new();
        private SnakeDir currentDir = SnakeDir.Left;
        private float _timeToMove = 0;

        public int fieldWidth {  get; set; }
        public int fieldHeight { get; set; }
        public override void Reset()
        {
            
            _body.Clear();
            int middleY = fieldHeight / 2;
            int middleX = fieldWidth / 2;
            currentDir = SnakeDir.Left;
            _timeToMove = 0;
            _body.Add(new(middleX + 3, middleY));

        }
        public override void Update(float deltaTime)
        {
            _timeToMove -= deltaTime;
            if(_timeToMove > 0)
            {
                return;
            }
            _timeToMove = GameConfig.Instance.frameDelay;
            var head = _body[0];
            var nextCell = ShiftTo(head, currentDir);

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
            foreach(Vector2 cell in _body)
            {

                renderer.SetPixel(cell.x, cell.y, '■', 3);

            }
        }
    }
}
