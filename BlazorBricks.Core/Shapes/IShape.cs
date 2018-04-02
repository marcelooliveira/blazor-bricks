using System.Collections.Generic;
//using System.Drawing;
using MVCBricks.Core;
using System.Drawing;
//using System.Drawing;

namespace MVCBricks.Core.Shapes
{
    public interface IShape
    {
        bool Rotate90();
        bool Rotate270();
        void Anchor();
        bool MoveLeft();
        bool MoveRight();
        bool MoveDown();

        int X { get; set; }
        int Y { get; set; }
        int Width { get; }
        int Height { get; }
        Color Color { get; set; }
        string ShapeString { get; }
        IBrick[,] ShapeArray { get; }
        IBoard ContainerBoard { get; set;}
        bool Anchored { get; }
    }
}
