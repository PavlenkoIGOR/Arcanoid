using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcanoidDLL.Config
{
    public class ChooseLevelScreen : Screen
    {
        // Инициализация окна
        RenderWindow _rw;
        ScreenHandler _levelHandler;

        // Загрузка текстуры
        Texture texture;
        Texture textureFrame;
        // Создание спрайтов
        Sprite sprite1;
        Sprite sprite2;
        Sprite sprite3;
        Sprite spriteFrame;

        // Задаем расстояние между спрайтами и получаем размеры
        float distance = 50f;
        float spriteWidth = 400f;


        private int[,] level_1 =
{
        { 1, 1, 1, 1, 1, 1 },
        { 1, 1, 0, 0, 1, 1 },
        { 1, 1, 1, 1, 1, 1 }

    };

        private int[,] level_2 =
    {
        { 1, 1, 1, 1, 1, 1 },
        { 2, 2, 2, 2, 2, 2 },
        { 3, 3, 3, 3, 3, 3 },
        { 2, 2, 2, 2, 2, 2 },
        { 1, 1, 1, 1, 1, 1 }
    };

        int[,] level_3 =
    {
        { 1, 1, 1, 1, 1, 1, 1, 1 },
        { 1, 3, 3, 3, 3, 3, 3, 1 },
        { 1, 3, 2, 2, 2, 2, 3, 1 },
        { 1, 3, 3, 3, 3, 3, 3, 1 },
        { 1, 3, 2, 2, 2, 2, 3, 1 },
        { 1 ,3, 3, 3, 3, 3, 3, 1 },
        { 1, 1, 1, 1, 1, 1, 1, 1 },
    };

        public ChooseLevelScreen(RenderWindow rw, ScreenHandler handler) : base(rw, handler)
        {
            _rw = rw;
            _levelHandler = handler;
            texture = new Texture(Path.Combine(Environment.CurrentDirectory, "GameResources", "level1_3.png"));
            textureFrame = new Texture(Path.Combine(Environment.CurrentDirectory, "GameResources", "frame.png"));


            sprite1 = new Sprite(texture) { TextureRect = new IntRect(0, 0, 300, 225) };
            sprite2 = new Sprite(texture) { TextureRect = new IntRect(301, 0, 300, 225) };
            sprite3 = new Sprite(texture) { TextureRect = new IntRect(601, 0, 300, 225) };
            spriteFrame = new Sprite(textureFrame);
        }

        public override void DrawLevel()
        {
            // Размеры спрайтов
            float spriteHeight = 225f; // высота одного изображения
            float spriteWidth = 300f; // ширина одного изображения

            // Полная ширина (включая расстояния)
            float totalWidth = (spriteWidth * 3) + (distance * 2);

            // Вычисляем начальную позицию для центрирования спрайтов
            float startX = (_rw.Size.X - totalWidth) / 2;

            // Устанавливаем позиции спрайтов
            sprite1.Position = new Vector2f(startX, (_rw.Size.Y - spriteHeight) / 2);
            sprite2.Position = new Vector2f(startX + spriteWidth + distance, (_rw.Size.Y - spriteHeight) / 2);
            sprite3.Position = new Vector2f(startX + 2 * (spriteWidth + distance), (_rw.Size.Y - spriteHeight) / 2);

            // Получение позиции мыши
            Vector2i mousePosition = Mouse.GetPosition(_rw);
            Vector2f mouseWorldPosition = _rw.MapPixelToCoords(mousePosition);

            byte choosenLevel = 0;
            // Проверка, попадает ли мышь на какой-либо спрайт
            if (sprite1.GetGlobalBounds().Contains(mouseWorldPosition.X, mouseWorldPosition.Y))
            {
                spriteFrame.Position = sprite1.Position; // Перемещение рамки на sprite1
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
            else if (sprite2.GetGlobalBounds().Contains(mouseWorldPosition.X, mouseWorldPosition.Y))
            {
                spriteFrame.Position = sprite2.Position; // Перемещение рамки на sprite2
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
            else if (sprite3.GetGlobalBounds().Contains(mouseWorldPosition.X, mouseWorldPosition.Y))
            {
                spriteFrame.Position = sprite3.Position; // Перемещение рамки на sprite3
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


            // Отрисовка спрайтов
            _rw.Clear(); // Очистка окна перед отрисовкой
            _rw.Draw(sprite1);
            _rw.Draw(sprite2);
            _rw.Draw(sprite3);
            _rw.Draw(spriteFrame); // Отрисовка рамки
            _rw.Display(); // Отображение отрисованного фрейма
        }
    }
}
