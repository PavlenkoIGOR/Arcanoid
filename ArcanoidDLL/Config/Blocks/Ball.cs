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

        public void Move(RenderWindow rw, Vector2f vec, List<BlockData> blocks)
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

                Start(200, new Vector2f(rX / 10, -1)); // Устанавливаем скорость и направление


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
                foreach (var block in blocks)
                {

                    if (sprite.Position.Y <= block.sprite.Position.Y + block.sprite.Texture.Size.Y) //проверка на столкновение по Y сверху
                    {
                        direction.Y = -Math.Abs(direction.Y);
                        Console.WriteLine("Y столкновение снизу");
                    }

                    //if (sprite.Position.Y + sprite.Texture.Size.Y >= block.sprite.Position.Y) //проверка на столкновение по Y сверху
                    //{
                    //    direction.Y = -Math.Abs(direction.Y);
                    //    Console.WriteLine("Y столкновение сверху");
                    //}

                    // Уменьшаем количество жизней у другого спрайта
                    block.hitTimes--;
                        if (block.hitTimes == 0 && block.isDestroyable)
                        {
                            // Логика уничтожения спрайта, если он разрушаем
                        }
                    
                }
            }
        }

    }
}
