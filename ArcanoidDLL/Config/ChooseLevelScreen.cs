using ArcanoidDLL.ArcanoidResources;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ArcanoidDLL.Config
{
    public class ChooseLevelScreen : Screen
    {
        RenderWindow _rw;
        ScreenHandler _levelHandler;

        // текстуры
        Texture _texture;
        Texture _textureFrame;
        // спрайты
        Sprite _sprite1;
        Sprite _sprite2;
        Sprite _sprite3;
        Sprite _spriteFrame;

        // расстояние между спрайтами
        float distance = 50f;

        Text _text;
        GameResources _resources;
        Font _font;
        public ChooseLevelScreen(RenderWindow rw, ScreenHandler handler) : base(rw, handler)
        {
            _rw = rw;
            _levelHandler = handler;
            _texture = new Texture(Path.Combine(Environment.CurrentDirectory, "GameResources", "level1_3.png"));
            _textureFrame = new Texture(Path.Combine(Environment.CurrentDirectory, "GameResources", "frame.png"));

            GameResources gr = new GameResources();
            _font = new Font(File.ReadAllBytes(gr.menuTextPath));
            _text = new Text();


            _sprite1 = new Sprite(_texture) { TextureRect = new IntRect(0, 0, 300, 225) };
            _sprite2 = new Sprite(_texture) { TextureRect = new IntRect(301, 0, 300, 225) };
            _sprite3 = new Sprite(_texture) { TextureRect = new IntRect(602, 0, 300, 225) };
            _spriteFrame = new Sprite(_textureFrame);
        }

        public override void DrawLevel()
        {
            // размеры спрайтов
            float spriteHeight = 225f; // высота одного изображения
            float spriteWidth = 300f; // ширина одного изображения
            float totalWidth = (spriteWidth * 3) + (distance * 2);
            float startX = (_rw.Size.X - totalWidth) / 2;

            _sprite1.Position = new Vector2f(startX, (_rw.Size.Y - spriteHeight) / 2);
            _sprite2.Position = new Vector2f(startX + spriteWidth + distance, (_rw.Size.Y - spriteHeight) / 2);
            _sprite3.Position = new Vector2f(startX + 2 * (spriteWidth + distance), (_rw.Size.Y - spriteHeight) / 2);

            Vector2i mousePosition = Mouse.GetPosition(_rw);
            Vector2f mouseWorldPosition = _rw.MapPixelToCoords(mousePosition);

            byte choosenLevel = 0;
 
            // попадает ли мышь на какой-либо спрайт
            if (_sprite1.GetGlobalBounds().Contains(mouseWorldPosition.X, mouseWorldPosition.Y))
            {
                _spriteFrame.Position = _sprite1.Position;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                {
                    Mouse.SetPosition(new Vector2i(0, 0));
                    choosenLevel = 1;
                    foreach (var screen in _levelHandler.screens)
                    {
                        if (screen.GetType() == typeof(GameLevelScreen))
                        {
                            (screen as GameLevelScreen).choosenLevel = choosenLevel;
                            screen.status = 1;
                        }
                        else
                        {
                            screen.status = 0;
                        }
                    }
                }
            }
            else if (_sprite2.GetGlobalBounds().Contains(mouseWorldPosition.X, mouseWorldPosition.Y))
            {
                _spriteFrame.Position = _sprite2.Position;
                if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                {
                    Mouse.SetPosition(new Vector2i(0, 0));
                    choosenLevel = 2;
                    foreach (var screen in _levelHandler.screens)
                    {
                        if (screen.GetType() == typeof(GameLevelScreen))
                        {
                            (screen as GameLevelScreen).choosenLevel = choosenLevel;
                            screen.status = 1;
                        }
                        else
                        {
                            screen.status = 0;
                        }
                    }
                }
            }
            else if (_sprite3.GetGlobalBounds().Contains(mouseWorldPosition.X, mouseWorldPosition.Y))
            {
                _spriteFrame.Position = _sprite3.Position; // Перемещение рамки на sprite3
                if (Keyboard.IsKeyPressed(Keyboard.Key.Enter))
                {
                    Mouse.SetPosition(new Vector2i(0, 0));
                    choosenLevel = 3;
                    foreach (var screen in _levelHandler.screens)
                    {
                        if (screen.GetType() == typeof(GameLevelScreen))
                        {
                            (screen as GameLevelScreen).choosenLevel = choosenLevel;
                            screen.status = 1;
                        }
                        else
                        {
                            screen.status = 0;
                        }
                    }
                }
            }

            _text.Font = _font;
            _text.FillColor = Color.White;
            _text.CharacterSize = 40;
                // позицию текста под рамкой
            _text.Position = new Vector2f(startX + (totalWidth - _text.GetGlobalBounds().Width) / 2, _spriteFrame.Position.Y + _spriteFrame.GetGlobalBounds().Height + 10); 

            _rw.Draw(_sprite1);
            _rw.Draw(_sprite2);
            _rw.Draw(_sprite3);
            
            _rw.Draw(_spriteFrame);
            _rw.Draw(_text);
        }
    }
}







