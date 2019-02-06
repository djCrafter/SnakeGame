using System;
using System.Threading;

namespace SnakeGame
{
    class Game
    {
        Field field = new Field(20, 30);
        Snake snake;

        Point fruit_position = new Point();
        bool fruitOnField = false;

        bool game_continues = true;
        int pause_interval = 400;
        int min_interval = 50;
        int correct = 1;
        int score = 0;

        int defaultFruitCounter = 20;
        int fruitCounter = 5;

        public Game()
        {
            snake = new Snake(field.GetStartPoint());
        }

        public void StartGame()
        {
            Console.CursorVisible = false;
            field.DrawField(ConsoleColor.Red);
            Draw(snake.snakePoints[0], SnakePart.Head);
            Console.SetCursorPosition(0, field.VerticalSize + 3);
            Controls(Console.ReadKey().Key);
            snake.Attach_the_tail();
            DrawScore(0);

            Process();
        }

        public void Process()
        {
            while (game_continues)
            {             
                if (Console.KeyAvailable)
                {
                    Controls(Console.ReadKey().Key);
                }

                DrawingSnake();
                Move();

                Thread.Sleep(pause_interval);
                DrawScore(score);
            }

            Console.Clear();
            Console.WriteLine("Game Over!"); ;
            Console.ReadLine();
        }

        public void DrawingSnake()
        {
            Draw(snake.snakePoints[0], SnakePart.Head);

            if (snake.snakePoints.Count > 2)
                Draw(snake.snakePoints[1], SnakePart.Body);

            Draw(snake.snakePoints[snake.snakePoints.Count - 1], SnakePart.Tail);
        }


        public void Draw(Point point, SnakePart snakePart)
        {
            Console.SetCursorPosition(point.PointX + correct, point.PointY + correct);

            switch (snakePart)
            {
                case SnakePart.Head:
                    Console.Write('@');
                    break;
                case SnakePart.Body:
                    Console.Write('*');
                    break;
                case SnakePart.Tail:
                    Console.Write(' ');
                    break;
                case SnakePart.Fruit:
                    Console.Write('$');
                    break;
            }

            Console.SetCursorPosition(0, field.VerticalSize + 3);
        }

        public void Controls(ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    if (snake.Direction != Direction.Down)
                        snake.Direction = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    if (snake.Direction != Direction.Up)
                        snake.Direction = Direction.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    if (snake.Direction != Direction.Right)
                        snake.Direction = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    if (snake.Direction != Direction.Left)
                        snake.Direction = Direction.Right;
                    break;
            }
        }


        public void Move()
        {
            Point point = new Point();
            point = snake.snakePoints[0];
            point = snake.ChangePoint(point, snake.Direction);
            
            FruitTimer();
          
            if(point == fruit_position)
            {
                InsertNewPart(point);
                if(pause_interval >= min_interval)
                {
                    pause_interval -= 25;
                    score += 100;
                    
                }
            }
            else if (Collision(point))
            {
                game_continues = false;
            }
            else
            {
                InsertNewPart(point);
                snake.snakePoints.RemoveAt(snake.snakePoints.Count - 1);
            }           
        }
        

        public void InsertNewPart(Point point)
        {
            switch (IfBoard(point))
            {
                case Direction.Right:
                    point.PointX = 0;
                    snake.snakePoints.Insert(0, point);
                    break;

                case Direction.Left:
                    point.PointX = field.HorizontalSize;
                    snake.snakePoints.Insert(0, point);
                    break;

                case Direction.Down:
                    point.PointY = 0;
                    snake.snakePoints.Insert(0, point);
                    break;

                case Direction.Up:
                    point.PointY = field.VerticalSize;
                    snake.snakePoints.Insert(0, point);
                    break;

                case Direction.None:
                    snake.snakePoints.Insert(0, point);
                    break;
            }
        }


        public Direction IfBoard(Point point)
        {
            if (point.PointX > field.HorizontalSize)
                return Direction.Right;

            if (point.PointX < 0)
                return Direction.Left;

            if (point.PointY > field.VerticalSize)
                return Direction.Down;

            if (point.PointY < 0)
                return Direction.Up;


            return Direction.None;
        }


        public void ShowCoord(Point point)
        {
            Console.SetCursorPosition(0, field.VerticalSize + 3);
            Console.WriteLine(point);
        }


        public void FruitTimer()
        {
            fruitCounter--;
            if (fruitCounter < 0)
            {
                if (fruitOnField)
                    Draw(fruit_position, SnakePart.Tail);

                FruitSet();
                fruitCounter = defaultFruitCounter;
            }

        }

        public void FruitSet()
        {
            Random random = new Random();
            do
            {
                fruit_position.PointX = random.Next(0, field.HorizontalSize);
                fruit_position.PointY = random.Next(0, field.VerticalSize);
            } while (Collision(fruit_position));


            fruitOnField = true;
            Draw(fruit_position, SnakePart.Fruit);
        }

        public bool Collision(Point point)
        {
            for (int i = 1; i < snake.snakePoints.Count - 1; ++i)
                if (point == snake.snakePoints[i])
                    return true;

            return false;
        }

      public void DrawScore(int score)
        {
            Console.SetCursorPosition(field.HorizontalSize + 4, 1);
            Console.Write(new string(' ', 15));
            Console.SetCursorPosition(field.HorizontalSize + 4, 1);
            Console.Write("Score Point: " + score);
        }
    }
}
