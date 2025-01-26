using ArcanoidDLL.ArcanoidResources;
using ArcanoidDLL.Config.Blocks;
using Arkanoid.Data;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ArcanoidDLL.Config;
public class GameLevelScreen : Screen
{
    private Ball ball;
    private bool isBallInMotion;
    private Platform platform;
    private const int TileSpacing = 10;

    private Text _lifesRemain;
    private byte _lifes;

    private List<BlockData> spritesInLevel;

    public byte choosenLevel;
    private bool isConstruct = false;

    GameResources gr;
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

    public GameLevelScreen(RenderWindow rw, ScreenHandler lH) : base(rw, lH)
    {
        platform = new Platform();
        platform.sprite.Position = new Vector2f(rw.Size.X / 2, rw.Size.Y - 50);
        platform.sprite.Scale = new Vector2f(0.4f, 0.4f);
        ball = new Ball();
        ball.sprite.Position = new Vector2f(platform.sprite.Position.X + (platform.sprite.GetGlobalBounds().Width / 2) - (ball.sprite.GetGlobalBounds().Width / 2), platform.sprite.Position.Y - ball.sprite.GetGlobalBounds().Height);
        isBallInMotion = false;
        spritesInLevel = new List<BlockData>();


        // Создание текста "Version"
        gr = new GameResources();
        
        _lifesRemain = new Text()
        {
            Position = new Vector2f(10, 10), // Позиция в левом верхнем углу
            FillColor = Color.White, // Цвет текста
            CharacterSize = 24 // Размер шрифта            
        };
        _lifesRemain.Font = new Font(gr.menuTextPath);
    }

    public override void DrawLevel()
    {
        if (isConstruct == false)
        {
            ConstructLevel();
            isConstruct = true;
        }

        _rw.SetMouseCursorVisible(false);

        if (ball.lifes != 0)
        {
            foreach (var item in spritesInLevel)
            {
                if (item.hitTimes > 0 || item.GetType() == typeof(Platform))
                {
                    _rw.Draw(item.sprite);
                    
                }

                
            }

            Vector2i mousePos = Mouse.GetPosition(_rw);
            platform.UpdatePosition(new Vector2f(mousePos.X, mousePos.Y), _rw.Size.X);
            ball.Move(_rw, platform.sprite.Position, spritesInLevel);
            _lifesRemain.DisplayedString = $"x{ball.lifes}";
            _rw.Draw(ball.sprite); // Рисуем шарик            

            if (spritesInLevel.Where(h => h.hitTimes == 0).Count() == spritesInLevel.Count())
            {
                //Console.WriteLine("spritesInLevel.Where(h => h.hitTimes == 0).Count() == spritesInLevel.Count()");
                isConstruct = false;
                choosenLevel++;
            }
        }
        if (ball.lifes == 0)
        {
            _lifesRemain.DisplayedString = "Game over!";
        }
        _rw.Draw(_lifesRemain);
    }
    private void ConstructLevel()
    {
        int [,] level_Arr = new int[,] { };
        if (choosenLevel == 1)
        {
            level_Arr = level_1;
        }
        if (choosenLevel == 2)
        {
            level_Arr = level_2;
        }
        if (choosenLevel == 3)
        {
            level_Arr = level_3;
        }
        Sprite[,] sprites = new Sprite[level_Arr.GetLength(0), level_Arr.GetLength(1)];

        // Отображение блоков
        for (int i = 0; i < level_Arr.GetLength(0); i++)
        {
            for (int j = 0; j < level_Arr.GetLength(1); j++)
            {
                switch (level_Arr[i, j])
                {
                    case 1:
                        BlueBlock blueBlock = new BlueBlock();
                        sprites[i, j] = blueBlock.sprite;
                        sprites[i, j].Position = new SFML.System.Vector2f(
                            (_rw.Size.X - (level_Arr.GetLength(1) * (blueBlock.sprite.TextureRect.Width + TileSpacing))) / 2 + j * (blueBlock.sprite.TextureRect.Width + TileSpacing),
                            (_rw.Size.Y - (level_Arr.GetLength(0) * (blueBlock.sprite.TextureRect.Height + TileSpacing))) / 2 + i * (blueBlock.sprite.TextureRect.Height + TileSpacing)
                        );
                        
                        spritesInLevel.Add(blueBlock);
                        break;
                    case 2:
                        YellowBlock yellowBlock = new YellowBlock();
                        sprites[i, j] = yellowBlock.sprite;
                        sprites[i, j].Position = new SFML.System.Vector2f(
                            (_rw.Size.X - (level_Arr.GetLength(1) * (yellowBlock.sprite.TextureRect.Width + TileSpacing))) / 2 + j * (yellowBlock.sprite.TextureRect.Width + TileSpacing),
                            (_rw.Size.Y - (level_Arr.GetLength(0) * (yellowBlock.sprite.TextureRect.Height + TileSpacing))) / 2 + i * (yellowBlock.sprite.TextureRect.Height + TileSpacing)
                        );

                        spritesInLevel.Add(yellowBlock);
                        break;
                    case 3:
                        RedBlock redBlock = new RedBlock();
                        sprites[i, j] = redBlock.sprite;
                        sprites[i, j].Position = new SFML.System.Vector2f(
                            (_rw.Size.X - (level_Arr.GetLength(1) * (redBlock.sprite.TextureRect.Width + TileSpacing))) / 2 + j * (redBlock.sprite.TextureRect.Width + TileSpacing),
                            (_rw.Size.Y - (level_Arr.GetLength(0) * (redBlock.sprite.TextureRect.Height + TileSpacing))) / 2 + i * (redBlock.sprite.TextureRect.Height + TileSpacing)
                        );

                        spritesInLevel.Add(redBlock);
                        break;
                }
            }
        }
        spritesInLevel.Add(platform);
    }
}