using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnake
{
    internal abstract class BaseGamePlayLogic : ConsoleInput.IArrowListener
    {
        protected BaseGameState? currentState {  get; private set; }
        protected int screenWidth { get; private set; }
        protected int screenHeight { get; private set; }

        public void DrawNewState(float deltaTime, ConsoleRenderer renderer)
        {
            screenWidth = renderer.width;
            screenHeight = renderer.height;

            currentState?.Update(deltaTime);
            currentState?.Draw(renderer);

            Update(deltaTime);

        }
        public abstract void OnArrowDown();


        public abstract void OnArrowLeft();


        public abstract void OnArrowRight();


        public abstract void OnArrowUp();
        public abstract ConsoleColor[] CreatePalette();

        public void InitializeInput(ConsoleInput input)
        {
            input.Subscribe(this);
        }
        public abstract void Update(float deltaTime);

        protected void ChangeState(BaseGameState? state)
        {
            currentState?.Reset();
            currentState = state;
        }

    }
}
