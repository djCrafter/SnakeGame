using System;

namespace SnakeGame
{
    enum Symbols
    {
        Horizontal = 9552, Vertical, RightDown = 9556, LeftDown = 9559, RightUp = 9562, LeftUp = 9565
    }

    class Field
    {
        public int VerticalSize { get; set; }
        public int HorizontalSize { get; set; }

        public Field(int verticalSize, int horizontalSize)
        {
            VerticalSize = verticalSize;
            HorizontalSize = horizontalSize;
        }

        public void DrawField(ConsoleColor color)
        {         
            Console.ForegroundColor = color;

            Console.Write((char)Symbols.RightDown);
            Console.Write(new string((char)Symbols.Horizontal, HorizontalSize + 1));
            Console.WriteLine((char)Symbols.LeftDown);

            for(byte i = 0; i < VerticalSize + 1; ++i)
            {
                Console.WriteLine((char)Symbols.Vertical + new string(' ', HorizontalSize + 1) +
                    (char)Symbols.Vertical);
            }

            Console.Write((char)Symbols.RightUp);
            Console.Write(new string((char)Symbols.Horizontal, HorizontalSize + 1));
            Console.WriteLine((char)Symbols.LeftUp);

            Console.ForegroundColor = ConsoleColor.Gray;          
        }

        public Point GetStartPoint()
        {
            Point point = new Point();
            point.PointX = HorizontalSize / 2;
            point.PointY = VerticalSize / 2;
       

            return point;
        }
    }
}
