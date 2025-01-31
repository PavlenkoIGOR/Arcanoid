﻿using ArcanoidDLL.ArcanoidResources;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ArcanoidDLL.Config.Buttons
{
    public class ButtonDTO
    {
        private Font _font;
        public RectangleShape _shape;
        public Text _text;
        private RenderWindow _window;
        private string _bttnText;
        public ScreenHandler _levelHandler;

        public Action _onClick;

        public ButtonDTO(RenderWindow window, string bttnText, Vector2f x1y1, Vector2f x2y2, ScreenHandler levelHandler)
        {
            _levelHandler = levelHandler;
            GameResources gr = new GameResources();
            _bttnText = bttnText;
            _font = new Font(File.ReadAllBytes(gr.menuTextPath));

            _window = window;

            _text = new Text(bttnText, _font)
            {
                FillColor = Color.Black,
                Style = Text.Styles.Bold,
                CharacterSize = 24
            };

            // позиция текста
            SetTextPosition(x1y1, x2y2);

            // Создание кнопки
            _shape = new RectangleShape(x2y2)
            {
                Position = x1y1,
                FillColor = Color.Cyan
            };
        }
        public bool IsMouseOver(Vector2i mousePosition)
        {
            if (_shape.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y))
            {
                //Console.WriteLine("над кнопкой {0}", _bttnText);
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    Vector2i mousePos = Mouse.GetPosition(_window);
                    MouseButtonEventArgs args = new MouseButtonEventArgs(new MouseButtonEvent())
                    {
                        Button = Mouse.Button.Left,
                        X = mousePos.X,
                        Y = mousePos.Y
                    };

                    if (_bttnText == "Start new game")
                    {
                        _window.Clear();
                        
                        //Console.WriteLine("_bttnText == \"Start new game\"");
                        foreach (var level in _levelHandler.screens)
                        {
                            level.status = 0;
                        }
                        //Console.WriteLine("_bttnText == \"Start new game\"");
                        foreach (var level in _levelHandler.screens)
                        {
                            //Console.WriteLine($"{level.GetType().Name}");
                            if (level.GetType().Name == typeof(GameLevelScreen).Name)
                            {
                                Console.WriteLine($"{nameof(level)}");
                                level.status = 1;
                                (_levelHandler.screens[1] as GameLevelScreen).choosenLevel = 1;
                            }
                        }
                    }
                    if (_bttnText == "Choose level")
                    {
                        _window.Clear();
                        
                        foreach (var level in _levelHandler.screens)
                        {
                            level.status = 0;
                        }
                        //Console.WriteLine("_bttnText == \"Start new game\"");
                        foreach (var level in _levelHandler.screens)
                        {
                            //Console.WriteLine($"{level.GetType().Name}");
                            if (level.GetType().Name == typeof(ChooseLevelScreen).Name)
                            {
                                Console.WriteLine($"{nameof(level)}");
                                level.status = 1;
                            }
                        }
                    }
                    if (_bttnText == "Exit")
                    {                      
                        _window.Close();
                        Console.WriteLine("Window closed");
                    }
                }
            }
            else
            {
                //Console.WriteLine("не над кнопкой");
            }
            return _shape.GetGlobalBounds().Contains(mousePosition.X, mousePosition.Y);
        }

        public void Update(Color hoverColor)
        {
            _shape.FillColor = IsMouseOver(Mouse.GetPosition(_window)) ? hoverColor : Color.Cyan;
        }
        private void SetTextPosition(Vector2f position, Vector2f size)
        {
            _text.Position = new Vector2f(
                position.X + (size.X - _text.GetGlobalBounds().Width) / 2,
                position.Y + (size.Y - _text.GetGlobalBounds().Height) / 2
            );
        }
    }
}
