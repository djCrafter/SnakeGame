using System.Collections.Generic;


namespace SnakeGame
{
    class Snake
    {
        public List<Point> snakePoints { get; set; } = new List<Point>();
        public Direction Direction { get; set; }

        public Snake(Point startPosition)
        {
            Direction = Direction.None;
            snakePoints.Add(startPosition);
        }       

        public void Move()
        {
            snakePoints.Insert(0, ChangePoint(snakePoints[0], Direction));

            snakePoints.RemoveAt(snakePoints.Count - 1);
        }


        public void Attach_the_tail()
        {
            snakePoints.Add(CreateNewPart());
        }

        public Point CreateNewPart()
        {
            Point point = new Point();
        
            switch (Direction)
            {
                case Direction.Up:
                    point = ChangePoint(point, Direction.Down);
                    break;
                case Direction.Down:
                    point = ChangePoint(point, Direction.Up);
                    break;
                case Direction.Left:
                    point = ChangePoint(point, Direction.Right);
                    break;
                case Direction.Right:
                    point = ChangePoint(point, Direction.Left);
                    break;
            }
            return point;
        }

        public Point ChangePoint(Point point, Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    point.PointX = snakePoints[0].PointX;
                    point.PointY = snakePoints[0].PointY - 1;
                    break;
                case Direction.Down:
                    point.PointX = snakePoints[0].PointX;
                    point.PointY = snakePoints[0].PointY + 1;
                    break;
                case Direction.Left:
                    point.PointX = snakePoints[0].PointX - 1;
                    point.PointY = snakePoints[0].PointY;
                    break;
                case Direction.Right:
                    point.PointX = snakePoints[0].PointX + 1;
                    point.PointY = snakePoints[0].PointY;
                    break;
                default:
                    break;
            }

            return point;
        }

    }
}
