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
        public List<Screen> levels;
        public ScreenHandler(RenderWindow rw)
        {
            levels = new List<Screen>() {
                new MainMenu(rw, this),
                new GameLevel(rw, this),
            };
        }
    }
}
