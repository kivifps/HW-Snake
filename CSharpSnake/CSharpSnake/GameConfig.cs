using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpSnake
{
    public class GameConfig
    {
        public float frameDelay = 0.25f;


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
