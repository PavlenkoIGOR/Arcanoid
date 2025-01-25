// See https://aka.ms/new-console-template for more information
using ArcanoidDLL.ArcanoidResources;
using ArcanoidDLL.Config;
using Arkanoid.Data;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

Clock frameClock = new Clock();
ArcanoidWindowData awindowData = new ArcanoidWindowData();
RenderWindow rw = new RenderWindow(new VideoMode(awindowData.windowWidth, awindowData.windowHeight), awindowData.windowTitle);
rw.Closed += (s, e) => rw.Close();


GameResources gr = new GameResources();

ScreenHandler levelHandler = new ScreenHandler(rw);
#region
SFML.Graphics.Text text = new SFML.Graphics.Text();
text.FillColor = Color.Yellow;
text.Font = new SFML.Graphics.Font(File.ReadAllBytes(gr.menuTextPath)); //путь к нужному шрифту
text.DisplayedString = "sdfsdf";
text.CharacterSize = 30;
text.Origin = new SFML.System.Vector2f(text.GetGlobalBounds().Width / 2, text.GetGlobalBounds().Height / 2);
text.Position = new SFML.System.Vector2f(awindowData.windowWidth / 2, awindowData.windowHeight / 2); // Установка позиции текста в центр окна
                                                                                                     // Отображение текста
#endregion

Clock clock = new Clock();
float timeDelay = 3.0f;


while (rw.IsOpen)
{
    // Обработка событий
    rw.DispatchEvents();

    // Очистка окна
    rw.Clear();

    foreach (var level in levelHandler.screens)
    {
        if (level.status == 1)
            level.DrawLevel();
    }

    AdjustFrameRate(frameClock);
    // Отображение
    rw.Display();
}

static void AdjustFrameRate(Clock clock)
{
    Time elapsed = clock.Restart(); // Получаем время, прошедшее с последнего кадра
    const int targetFrameTime = 16; // ~60fps
    if (elapsed.AsMilliseconds() < targetFrameTime)
    {
        System.Threading.Thread.Sleep(targetFrameTime - (int)elapsed.AsMilliseconds());
    }
}

#region установка задержки
//if (clock.ElapsedTime.AsSeconds() <= timeDelay)
//{
//    text.Origin = new SFML.System.Vector2f(text.GetGlobalBounds().Width / 2, text.GetGlobalBounds().Height / 2);
//    text.Position = new SFML.System.Vector2f(awindowData.windowWidth / 2, awindowData.windowHeight / 2); // Установка позиции текста в центр окна                                                                                     
//    // Отображение текста                                                                                     
//    rw.Draw(text);
//}
//else
//{
//    text.DisplayedString = "More then 3 sec";
//    text.Origin = new SFML.System.Vector2f(text.GetGlobalBounds().Width / 2, text.GetGlobalBounds().Height / 2);
//    text.Position = new SFML.System.Vector2f(awindowData.windowWidth / 2, awindowData.windowHeight / 2);
//    rw.Draw(text);
//}
#endregion