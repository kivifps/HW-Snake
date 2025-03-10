using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnake
{
    public class GameConfig
    {
        public float frameDelay = 1f;
        public float targetFrameTime = 1f / 120f;


        private static GameConfig instance;
        private GameConfig() { }    
        public static GameConfig Instance
        {
            get
            {

                if (instance == null)
                {
                    instance = new GameConfig();
                }
                return instance;
            }
        }

    }
}
