using System.Collections.Generic;
//
using BlazorBricks.Core;

//

namespace BlazorBricks.Core.Shapes
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
        string ShapeString { get; }
        IBrick[,] ShapeArray { get; }
        IBoard ContainerBoard { get; set;}
        bool Anchored { get; }
    }
}
