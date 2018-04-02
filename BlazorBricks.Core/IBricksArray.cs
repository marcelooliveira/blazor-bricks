using System;
namespace MVCBricks.Core
{
    interface IBricksArray
    {
        string GetStringFromShapeArray();
        void InitializeArray();
        int Height { get; }
        IBrick[,] ShapeArray { get; }
        string ShapeString { get; }
        int Width { get; }
    }
}
