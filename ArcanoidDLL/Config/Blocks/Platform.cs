using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcanoidDLL.Config.Blocks
{
    internal class Platform : BlockData
    {
        internal Platform() 
        {
            this._texture = new Texture(Path.Combine(Environment.CurrentDirectory, "GameResources", "Platform.png"));
            this.sprite = new Sprite(_texture);
            this.isDestroyable = false;
        }

        internal void UpdatePosition(Vector2f mousePosition, float windowWidth)
        {

            float halfWidth = sprite.GetGlobalBounds().Width / 2;
            float xPos = mousePosition.X - halfWidth;

            // Убедимся, что платформа не выходит за пределы окна
            if (xPos < 0)
            {
                xPos = 0;
            }
            else if (xPos + sprite.GetGlobalBounds().Width > windowWidth)
            {
                xPos = windowWidth - sprite.GetGlobalBounds().Width;
            }

            sprite.Scale = new Vector2f(0.4f, 0.4f);
            sprite.Position = new Vector2f(xPos, sprite.Position.Y);
        }
    }
}
