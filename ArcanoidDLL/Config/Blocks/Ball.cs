using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace ArcanoidDLL.Config.Blocks
{
    internal class Ball : BlockData
    {
        public float speed;
        public Vector2f direction = new Vector2f();
        private bool isBallMoving = false;

        public Ball()
        {
            this._texture = new Texture(Path.Combine(Environment.CurrentDirectory, "GameResources", "Ball.png"));
            this.sprite = new Sprite(_texture);
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
                float rX = random.Next(-10, 10);

                Start(400, new Vector2f(rX / 10 + 0.1f, -1)); // Устанавливаем скорость и направление


                isBallMoving = true; // Мяч начинает движение
            }

            // Перемещение шара
            if (isBallMoving)
            {
                //Console.WriteLine($"sprite posX {sprite.Position.X} posY{sprite.Position.Y}");
                sprite.Position += direction * speed * 0.016f; // Обновление позиции с учетом времени (при 60 FPS это примерно 0.016)

                if (sprite.Position.Y <= 0)
                {
                    direction.Y *= -1;
                }
                if (sprite.Position.X <= 0 || sprite.Position.X + sprite.GetGlobalBounds().Width >= rw.Size.X)
                {
                    direction.X *= -1;
                }
                if (sprite.Position.Y >= rw.Size.Y)
                {
                    Console.WriteLine("out of bottom screen");
                    isBallMoving = false;
                    direction.Y *= -1;
                    
                }

                /*
                 
                 */

                // Проверка коллизий с другими спрайтами
                foreach (var block in blocks)
                {
                    var deltaPass = direction - block.sprite.Position;
                    if (sprite.GetGlobalBounds().Intersects(block.sprite.GetGlobalBounds()) && block.hitTimes > 0 || sprite.GetGlobalBounds().Intersects(block.sprite.GetGlobalBounds()) && block.GetType() == typeof(Platform))
                    {                                                                        
                        if (block.GetType() == typeof(Platform))
                        {
                            direction.Y = -1;

                        }
                        if (block.GetType() != typeof(Platform))
                        {
                            if (sprite.GetGlobalBounds().Top <= block.sprite.GetGlobalBounds().Top + block.sprite.GetGlobalBounds().Height) //снизу
                            {
                                if (direction.X > 0 && direction.Y < 0)
                                {
                                    direction.Y *= -1;
                                    block.hitTimes--;
                                    return;
                                }
                                if (direction.X > 0 && direction.Y < 0 || direction.X < 0 && direction.Y < 0)
                                {
                                    direction.Y *= -1;
                                    block.hitTimes--;
                                    return;
                                }
                            }
                            if (sprite.GetGlobalBounds().Top  + sprite.GetGlobalBounds().Height >= block.sprite.GetGlobalBounds().Top) //сверху
                            {
                                if (direction.X > 0 && direction.Y > 0 || direction.X < 0 && direction.Y > 0)
                                {
                                    direction.Y *= -1; block.hitTimes--; return ;
                                }
                            }
                            if (sprite.GetGlobalBounds().Left + sprite.GetGlobalBounds().Width >= block.sprite.GetGlobalBounds().Left) //справа
                            {
                                if (direction.X > 0 && direction.Y > 0 || direction.X > 0 && direction.Y < 0)
                                {
                                    direction.X *= -1; block.hitTimes--; return;
                                }
                            }
                            if (sprite.GetGlobalBounds().Left <= block.sprite.GetGlobalBounds().Left + block.sprite.GetLocalBounds().Width)  //слева
                            {
                                if (direction.X < 0 && direction.Y > 0 || direction.X < 0 && direction.Y < 0)
                                {
                                    direction.X *= -1; block.hitTimes--; return;
                                }
                            }

                            
                        }
                    }                    
                }
            }
        }
    }
}
