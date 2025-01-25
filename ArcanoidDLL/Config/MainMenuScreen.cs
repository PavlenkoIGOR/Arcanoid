using ArcanoidDLL.ArcanoidResources;
using ArcanoidDLL.Config.Buttons;
using Arkanoid.Data;
using SFML.Graphics;
using SFML.System;


namespace ArcanoidDLL.Config;

public class MainMenuScreen : Screen//ILevel
{
    //private ArcanoidWindowData _awindowData;
    private string[] _menuText = { "Start new game", "Choose level", "Save level", "Options", "Exit"};
    
    private Text _versionText;
    private List<ButtonDTO> _buttons;
    public MainMenuScreen(RenderWindow rw, ScreenHandler lH) : base(rw, lH)
    {
        status = 1;
        //_awindowData = awindowData;
        _buttons = new List<ButtonDTO>();
        GameResources gr = new GameResources();

        // Создание текста "Version"
        _versionText = new Text("Version 1.0", new Font(gr.menuTextPath))
        {
            Position = new Vector2f(10, 10), // Позиция в левом верхнем углу
            FillColor = Color.White, // Цвет текста
            CharacterSize = 18 // Размер шрифта
        };
    }

    public override void DrawLevel()
    {
        float buttonHeight = 40; // Высота кнопки
        float buttonSpacing = 10; // Промежуток между кнопками
        float startY = (_rw.Size.X - (_menuText.Length * (buttonHeight + buttonSpacing) - buttonSpacing)) / 2; // Центрирование по Y
        for (int i = 0; i < _menuText.Length; i++)
        {
            // Расчет Y позиции каждой кнопки
            float buttonY = startY + i * (buttonHeight + buttonSpacing);
            ButtonDTO button = new ButtonDTO(
                _rw,
                _menuText[i],
                new Vector2f(_rw.Size.X / 2 - 100, buttonY),
                new Vector2f(200, buttonHeight),
                _lHandler);

            button.Update(Color.Yellow);
            _rw.Draw(button._shape);
            _rw.Draw(button._text);
            _buttons.Add(button); // Сохраняем кнопку в список
        }
        _rw.Draw(_versionText);
    }



}
