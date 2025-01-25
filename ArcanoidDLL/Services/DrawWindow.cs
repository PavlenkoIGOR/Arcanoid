using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arcanoid
{
    public class DrawWindow
    {
        public uint _frameLimit = 30;
        public uint _windowWidth = 800;
        public uint _windowHeight = 600;
        public string _windowTitle = "Arcanoid";

        public RenderWindow rw;
        public DrawWindow()
        {
            rw = new RenderWindow(new VideoMode(_windowWidth, _windowHeight), _windowTitle);
        }
        public  void DrawArkanoidWindow()
        {            
            rw.Display();
            rw.SetFramerateLimit(_frameLimit);
            rw.Closed += (s, e) => rw.Close();
        }
    }
}
