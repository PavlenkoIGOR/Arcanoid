using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcanoidDLL.Config
{
    public class Screen
    {
        protected RenderWindow _rw;
        protected ScreenHandler _lHandler;

        public byte status = 0;
        public Screen(RenderWindow rw, ScreenHandler levelHandler) 
        {
            _rw = rw;
            _lHandler = levelHandler;
        }
        public virtual void DrawLevel()
        {

        }
    }
}
