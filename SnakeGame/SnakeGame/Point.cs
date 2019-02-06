namespace SnakeGame
{
    struct Point
    {
        public int PointX { get; set; }
        public int PointY { get; set; }

        public override string ToString()
        {
            return "X = " + PointX + " Y = " + PointY;
        }

        public static bool operator ==(Point point1, Point point2)
        {
            if (point1.PointX == point2.PointX && point1.PointY == point2.PointY)
                return true;
            return false;
        }

        public static bool operator !=(Point point1, Point point2)
        {
            if (point1.PointX == point2.PointX && point1.PointY == point2.PointY)
                return false;
            return true;
        }
    }
}
