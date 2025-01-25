using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcanoidDLL.Config
{
    public class ScreenHandler
    {
        public List<Screen> screens;
        public ScreenHandler(RenderWindow rw)
        {
            screens = new List<Screen>() {
                new MainMenuScreen(rw, this),
                new GameLevelScreen(rw, this),
                new ChooseLevelScreen(rw, this)
            };
        }
    }
}
