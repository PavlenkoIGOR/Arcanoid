using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ArcanoidDLL.Config.Blocks
{
    internal class Ball : BlockData
    {
        public Vector2f Velocity { get; set; }
        public float speed;
        public Vector2f direction = new Vector2f();
        private bool isBallMoving = false;

        public Ball()
        {
            this._texture = new Texture(Path.Combine(Environment.CurrentDirectory, "GameResources", "Ball.png"));
            this.sprite = new Sprite(_texture);
            this.Velocity = new Vector2f(0, -300); // Начальная скорость (можно регулировать)
            this.sprite.Scale = new Vector2f(0.2f, 0.2f);
            // размер спрайта в оригинале до масштабирования 200px на 200px
        }

        public void Start(float speed, Vector2f dir)
        {
            if (this.speed != 0) return;

            this.speed = speed;
            this.direction = dir;
        }

        public void Move(RenderWindow rw, Vector2f vec, List<BlockData> otherSprites)
        {
            // Если мяч не движется, устанавливаем его позицию в vec
            if (!isBallMoving)
            {
                sprite.Position = new Vector2f(vec.X + 50, vec.Y - 40); // Устанавливаем позицию мяча
            }

            // Проверка нажатия пробела для начала движения
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && !isBallMoving)
            {
                Console.WriteLine("Move");
                Random random = new Random();
                float rX = (float)(random.Next(-10, 10));
                Start(100, new Vector2f(rX / 10, -1)); // Устанавливаем скорость и направление
                isBallMoving = true; // Мяч начинает движение
            }

            // Перемещение шара
            if (isBallMoving)
            {
                sprite.Position += direction * speed * 0.016f; // Обновление позиции с учетом времени (при 60 FPS это примерно 0.016)

                // Проверка коллизий с границами окна
                if (sprite.Position.X < 0) // Левый край
                {
                    sprite.Position = new Vector2f(0, sprite.Position.Y); // Возвращаем к границе
                    direction.X = Math.Abs(direction.X); // Меняем направление на правое
                }

                if (sprite.Position.X + sprite.GetGlobalBounds().Width > rw.Size.X) // Правый край
                {
                    sprite.Position = new Vector2f(rw.Size.X - sprite.GetGlobalBounds().Width, sprite.Position.Y); // Возвращаем к границе
                    direction.X = -Math.Abs(direction.X); // Меняем направление на левое
                }

                if (sprite.Position.Y < 0) // Верхний край
                {
                    sprite.Position = new Vector2f(sprite.Position.X, 0); // Возвращаем к границе
                    direction.Y = Math.Abs(direction.Y); // Меняем направление на вниз
                }

                if (sprite.Position.Y + sprite.GetGlobalBounds().Height > rw.Size.Y) // Нижний край
                {
                    isBallMoving = false;
                    sprite.Position = vec;
                    direction.Y = -direction.Y;
                }

                // Проверка коллизий с другими спрайтами
                foreach (var otherSprite in otherSprites)
                {
                    if (sprite.GetGlobalBounds().Intersects(otherSprite.sprite.GetGlobalBounds()))
                    {
                        // Получаем границы другого спрайта
                        FloatRect otherBounds = otherSprite.sprite.GetGlobalBounds();
                        Vector2f ballCenter = new Vector2f(
                            sprite.Position.X + sprite.GetLocalBounds().Width / 2,
                            sprite.Position.Y + sprite.GetLocalBounds().Height / 2);

                        Vector2f closestPoint = new Vector2f(
                            Math.Clamp(ballCenter.X, otherBounds.Left, otherBounds.Left + otherBounds.Width),
                            Math.Clamp(ballCenter.Y, otherBounds.Top, otherBounds.Top + otherBounds.Height));

                        Vector2f collisionVector = ballCenter - closestPoint;

                        // Определяем, по какой оси произошло столкновение
                        float absCollisionX = Math.Abs(collisionVector.X);
                        float absCollisionY = Math.Abs(collisionVector.Y);

                        if (absCollisionX > absCollisionY)
                        {
                            direction.X *= -1; // Меняем направление по X (горизонтальный отскок)
                            // Избавляем мяч от повторного пересечения
                            sprite.Position = new Vector2f(sprite.Position.X + (collisionVector.X > 0 ? absCollisionX : -absCollisionX), sprite.Position.Y);
                        }
                        else
                        {
                            direction.Y *= -1; // Меняем направление по Y (вертикальный отскок)
                            // Избавляем мяч от повторного пересечения
                            sprite.Position = new Vector2f(sprite.Position.X, sprite.Position.Y + (collisionVector.Y > 0 ? absCollisionY : -absCollisionY));
                        }

                        // Уменьшаем количество жизней у другого спрайта
                        otherSprite.hitTimes--;
                        if (otherSprite.hitTimes == 0 && otherSprite.isDestroyable)
                        {
                            // Логика уничтожения спрайта, если он разрушаем
                        }
                    }
                }
            }
        }
    }
}
