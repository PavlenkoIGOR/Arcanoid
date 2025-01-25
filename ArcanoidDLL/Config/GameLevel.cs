using ArcanoidDLL.Config.Blocks;
using Arkanoid.Data;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ArcanoidDLL.Config;
public class GameLevel : Screen
{
    private Ball ball;
    private bool isBallInMotion;
    private Platform platform;
    private const int TileSpacing = 5;

    private List<BlockData> spritesInLevel;

    int[,] level_1 =
    {
        { 1, 1, 1, 1, 1, 1 },
        { 2, 2, 2, 2, 2, 2 },
        { 3, 3, 3, 3, 3, 3 },
        { 2, 2, 2, 2, 2, 2 },
        { 1, 1, 1, 1, 1, 1 },
    };

    public GameLevel(RenderWindow rw, ScreenHandler lH) : base(rw, lH)
    {
        platform = new Platform();
        platform.sprite.Position = new Vector2f(rw.Size.X / 2, rw.Size.Y - 50);
        platform.sprite.Scale = new Vector2f(0.5f, 0.5f);
        ball = new Ball();
        ball.sprite.Position = new Vector2f(platform.sprite.Position.X + (platform.sprite.GetGlobalBounds().Width / 2) - (ball.sprite.GetGlobalBounds().Width / 2), platform.sprite.Position.Y - ball.sprite.GetGlobalBounds().Height);
        isBallInMotion = false;
        spritesInLevel = new List<BlockData>();
        ConstuctLevel();
    }

    public override void DrawLevel()
    {
        _rw.SetMouseCursorVisible(false);

        foreach (var item in spritesInLevel)
        {
            _rw.Draw(item.sprite);
        }

        Vector2i mousePos = Mouse.GetPosition(_rw);
        platform.UpdatePosition(new Vector2f(mousePos.X, mousePos.Y), _rw.Size.X);
        ball.Move(_rw, platform.sprite.Position, spritesInLevel);
        _rw.Draw(platform.sprite);
        _rw.Draw(ball.sprite); // Рисуем шарик
    }
    private void ConstuctLevel()
    {
        Sprite[,] sprites = new Sprite[level_1.GetLength(0), level_1.GetLength(1)];

        // Отображение блоков
        for (int i = 0; i < level_1.GetLength(0); i++)
        {
            for (int j = 0; j < level_1.GetLength(1); j++)
            {
                switch (level_1[i, j])
                {
                    case 1:
                        BlueBlock blueBlock = new BlueBlock();
                        sprites[i, j] = blueBlock.sprite;
                        sprites[i, j].Position = new SFML.System.Vector2f(
                            (_rw.Size.X - (level_1.GetLength(1) * (blueBlock.sprite.TextureRect.Width + TileSpacing))) / 2 + j * (blueBlock.sprite.TextureRect.Width + TileSpacing),
                            (_rw.Size.Y - (level_1.GetLength(0) * (blueBlock.sprite.TextureRect.Height + TileSpacing))) / 2 + i * (blueBlock.sprite.TextureRect.Height + TileSpacing)
                        );
                        
                        spritesInLevel.Add(blueBlock);
                        break;
                    case 2:
                        YellowBlock yellowBlock = new YellowBlock();
                        sprites[i, j] = yellowBlock.sprite;
                        sprites[i, j].Position = new SFML.System.Vector2f(
                            (_rw.Size.X - (level_1.GetLength(1) * (yellowBlock.sprite.TextureRect.Width + TileSpacing))) / 2 + j * (yellowBlock.sprite.TextureRect.Width + TileSpacing),
                            (_rw.Size.Y - (level_1.GetLength(0) * (yellowBlock.sprite.TextureRect.Height + TileSpacing))) / 2 + i * (yellowBlock.sprite.TextureRect.Height + TileSpacing)
                        );

                        spritesInLevel.Add(yellowBlock);
                        break;
                    case 3:
                        RedBlock redBlock = new RedBlock();
                        sprites[i, j] = redBlock.sprite;
                        sprites[i, j].Position = new SFML.System.Vector2f(
                            (_rw.Size.X - (level_1.GetLength(1) * (redBlock.sprite.TextureRect.Width + TileSpacing))) / 2 + j * (redBlock.sprite.TextureRect.Width + TileSpacing),
                            (_rw.Size.Y - (level_1.GetLength(0) * (redBlock.sprite.TextureRect.Height + TileSpacing))) / 2 + i * (redBlock.sprite.TextureRect.Height + TileSpacing)
                        );

                        spritesInLevel.Add(redBlock);
                        break;
                }
            }
        }
        spritesInLevel.Add(platform);
    }

    private void CheckCollision()
    {
        // Проверка столкновения с платформой
        if (ball.sprite.GetGlobalBounds().Intersects(platform.sprite.GetGlobalBounds()))
        {
            ball.Velocity = new Vector2f(ball.Velocity.X, -Math.Abs(ball.Velocity.Y)); // Отскок от платформы
        }

        // Проверка столкновений с блоками
        // Вам нужно будет реализовать проверку для всех блоков в состоянии игры.
        // Например, храня блоки в списке и перебирая их для столкновений.
    }
}