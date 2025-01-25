using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ArcanoidDLL.Services
{
    public static class Game
    {
        public static int MouseX;

        public static int MouseY;

        private static List<Keyboard.Key> KeysCodeDown = new List<Keyboard.Key>();

        private static List<Keyboard.Key> KeysCodePressed = new List<Keyboard.Key>();

        private static List<Keyboard.Key> KeysCodeUp = new List<Keyboard.Key>();

        private static List<Keyboard.Key> KeysCodeRelased = new List<Keyboard.Key>();

        private static int MouseButtonDown;

        private static int MouseButtonUp;

        private static RenderWindow window;

        private static Clock clock;

        public static float DeltaTime;

        private static Color fillColor = Color.White;

        private static Font currentFont;

        private static Text lable = new Text();

        private static CircleShape circle = new CircleShape();

        private static RectangleShape rectangle = new RectangleShape();


        public static void InitWindow(uint width, uint height, string title = "untitled")
        {
            window = new RenderWindow(new VideoMode(width, height), title);
            window.Closed += Window_Closed;
            window.KeyPressed += Window_KeyPressed;
            window.KeyReleased += Window_KeyReleased;
            window.MouseButtonPressed += Window_MouseButtonPressed;
            window.MouseButtonReleased += Window_MouseButtonReleased;
            window.SetFramerateLimit(60u);
            clock = new Clock();
        }

        private static void Window_MouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            MouseButtonUp = (int)e.Button;
            MouseButtonDown = -1;
        }

        public static void Window_MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            MouseButtonUp = -1;
            MouseButtonDown = (int)e.Button;
        }

        private static void Window_KeyReleased(object sender, KeyEventArgs e)
        {
            if (!KeysCodeUp.Contains(e.Code) && !KeysCodeRelased.Contains(e.Code))
            {
                KeysCodeUp.Add(e.Code);
            }

            if (!KeysCodeRelased.Contains(e.Code))
            {
                KeysCodeRelased.Add(e.Code);
            }

            if (KeysCodePressed.Contains(e.Code))
            {
                KeysCodePressed.Remove(e.Code);
            }
        }

        private static void Window_KeyPressed(object sender, KeyEventArgs e)
        {
            if (!KeysCodeDown.Contains(e.Code) && !KeysCodePressed.Contains(e.Code))
            {
                KeysCodeDown.Add(e.Code);
            }

            if (!KeysCodePressed.Contains(e.Code))
            {
                KeysCodePressed.Add(e.Code);
            }

            if (KeysCodeRelased.Contains(e.Code))
            {
                KeysCodeRelased.Remove(e.Code);
            }
        }

        private static void Window_Closed(object sender, EventArgs e)
        {
            if (window != null)
            {
                window.Close();
                Environment.Exit(0);
            }
        }

        public static void DispatchEvents()
        {
            window.DispatchEvents();
            MouseX = Mouse.GetPosition(window).X;
            MouseY = Mouse.GetPosition(window).Y;
        }

        public static void DisplayWindow()
        {
            //window.Display();
            if (MouseButtonDown != -1)
            {
                MouseButtonDown = -1;
            }

            if (MouseButtonUp != -1)
            {
                MouseButtonUp = -1;
            }

            KeysCodeDown.Clear();
            KeysCodeUp.Clear();
        }

        public static bool GetKey(Keyboard.Key key)
        {
            return Keyboard.IsKeyPressed(key);
        }

        public static bool GetKeyDown(Keyboard.Key key)
        {
            return KeysCodeDown.Contains(key);
        }

        public static bool GetKeyUp(Keyboard.Key key)
        {
            return KeysCodeUp.Contains(key);
        }

        public static bool GetMouseButton(Mouse.Button button)
        {
            return Mouse.IsButtonPressed(button);
        }

        public static bool GetMouseButtonDown(Mouse.Button button)
        {
            return MouseButtonDown == (int)button;
        }

        public static bool GetMouseButtonUp(Mouse.Button button)
        {
            return MouseButtonUp == (int)button;
        }

    }
}

