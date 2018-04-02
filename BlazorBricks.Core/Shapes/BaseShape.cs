using System;
//using System.Drawing;
using System.Diagnostics;
using MVCBricks.Core;
using MVCBricks.Core.Exceptions;
using System.Drawing;

namespace MVCBricks.Core.Shapes
{
    /// <summary>
    /// Represents the base shape for Bricks game.
    /// </summary>
    public abstract class BaseShape : BaseBricksArray, IShape
    {
        protected int x = 0;
        protected int y = 0;
        protected Color color = Color.White;
        protected bool anchored = false;
        protected IBoard containerBoard = null;

        public BaseShape(int x, int y, int width, int height, string shapeString)
        {
            if (x < 0)
                throw new ArgumentOutOfRangeException("x");

            if (y < 0)
                throw new ArgumentOutOfRangeException("y");

            this.x = x;
            this.y = y;
            LoadData(width, height, shapeString);
        }

        public BaseShape(int width, int height, string shapeString)
        {
            LoadData(width, height, shapeString);
        }

        private void LoadData(int width, int height, string shapeString)
        {
            if (shapeString.Length != width * height)
            {
                throw new InvalidShapeSizeException();
            }
            else if (HasInvalidShapeCharacter(shapeString))
            {
                throw new InvalidShapeStringCharacterException();
            }
            else
            {
                this.width = width;
                this.height = height;
                this.shapeString = shapeString;
                this.shapeArray = new IBrick[width, height];
                int i = 0;
                for (int row = 0; row < height; row++)
                {
                    for(int column = 0; column < width; column++)
                    {
                        int nColor = Convert.ToInt32(shapeString.Substring(i, 1));
                        IBrick brick = null;
                        if (nColor > 0)
                        {
                            brick = new Brick(column, row, GetShapeColorFromInteger(nColor));
                        }
                        shapeArray[column, row] = brick;
                        i++;
                    }
                }
            }
        }

        private static bool HasInvalidShapeCharacter(string shapeString)
        {
            bool ret = false;
            foreach(Char c in shapeString.ToCharArray())
            {
                if (c.ToString() != "0" && c.ToString() != "1")
                {
                    ret = true;
                }
            }
            return ret;
        }

        public void Anchor()
        {
            anchored = true;
        }

        public bool MoveLeft()
        {
            bool test = false;
            if (!anchored)
            {
                if (containerBoard == null)
                    throw new NullContainerBoardException();

                containerBoard.RemovePieceFromCurrentPosition(this);

                test = containerBoard.TestPieceOnPosition(this, this.X - 1, this.Y);
                if (test)
                {
                    containerBoard.RemovePieceFromCurrentPosition(this);
                    containerBoard.PutPieceOnPosition(this, this.X - 1, this.Y);
                }
            }
            return test;
        }

        public bool MoveRight()
        {
            bool test = false;
            if (!anchored)
            {
                if (containerBoard == null)
                    throw new NullContainerBoardException();

                containerBoard.RemovePieceFromCurrentPosition(this);

                test = containerBoard.TestPieceOnPosition(this, this.X + 1, this.Y);
                if (test)
                {
                    containerBoard.PutPieceOnPosition(this, this.X + 1, this.Y);
                }
            }
            return test;
        }

        public bool MoveDown()
        {
            bool test = false;

            if (!anchored)
            {
                containerBoard.RemovePieceFromCurrentPosition(this);

                //should anchor if shape can't move down from current position
                if (!containerBoard.TestPieceOnPosition(this, this.X, this.Y + 1))
                {
                    containerBoard.PutPieceOnPosition(this, this.X, this.Y);
                    this.Anchor();
                }
                else
                {
                    if (containerBoard == null)
                        throw new NullContainerBoardException();

                    test = containerBoard.TestPieceOnPosition(this, this.X, this.Y + 1);
                    if (test)
                    {
                        containerBoard.PutPieceOnPosition(this, this.X, this.Y + 1);
                    }
                }
            }

            return test;
        }

        public bool Rotate90()
        {
            bool test = false;
            if (!anchored)
            {
                if (containerBoard == null)
                    throw new NullContainerBoardException();

                IBrick[,] newShapeArray = new IBrick[height, width];
                IBrick[,] oldShapeArray = new IBrick[width, height];
                for (int row = 0; row < height; row++)
                {
                    for (int column = 0; column < width; column++)
                    {
                        newShapeArray[height - row - 1, column] = shapeArray[column, row];
                        oldShapeArray[column, row] = shapeArray[column, row];
                    }
                }

                containerBoard.RemovePieceFromCurrentPosition(this);

                int w = width;
                int h = height;
                this.width = h;
                this.height = w;
                this.shapeArray = newShapeArray;

                if (containerBoard.TestPieceOnPosition(this, this.X, this.Y))
                {
                    containerBoard.PutPieceOnPosition(this, this.X, this.Y);
                }
                else
                {
                    this.width = w;
                    this.height = h;
                    this.shapeArray = oldShapeArray;
                    containerBoard.PutPieceOnPosition(this, this.X, this.Y);
                }
            }
            return test;
        }

        public bool Rotate270()
        {
            bool test = false;
            if (!anchored)
            {
                if (containerBoard == null)
                    throw new NullContainerBoardException();

                containerBoard.RemovePieceFromCurrentPosition(this);

                IBrick[,] newShapeArray = new IBrick[height, width];
                IBrick[,] oldShapeArray = new IBrick[width, height];
                for (int row = 0; row < height; row++)
                {
                    for (int column = 0; column < width; column++)
                    {
                        newShapeArray[row, width - column - 1] = shapeArray[column, row];
                        oldShapeArray[column, row] = shapeArray[column, row];
                    }
                }

                int w = width;
                int h = height;
                this.width = h;
                this.height = w;
                this.shapeArray = newShapeArray;

                if (containerBoard.TestPieceOnPosition(this, this.X, this.Y))
                {
                    containerBoard.PutPieceOnPosition(this, this.X, this.Y);
                }
                else
                {
                    this.width = w;
                    this.height = h;
                    this.shapeArray = oldShapeArray;
                    containerBoard.PutPieceOnPosition(this, this.X, this.Y);
                }
            }
            return test;
        }

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Color Color
        {
            get {return color;}
            set
            {
                color = value;
                for (int row = 0; row < height; row++)
                {
                    for (int column = 0; column < width; column++)
                    {
                        IBrick brick = shapeArray[column, row];
                        if (brick != null)
                        {
                            brick.Color = value;
                        }
                    }
                }
            }
        }

        public IBoard ContainerBoard
        {
            get { return containerBoard; }
            set { containerBoard = value; }
        }

        public bool Anchored
        { 
            get {return anchored;}
        }
    }

    //public enum ShapeColor
    //{
    //    Black = 0,
    //    Blue,
    //    Gold,
    //    Green,
    //    Orange,
    //    Pink,
    //    Red,
    //    Violet,
    //    White,
    //    Yellow
    //}
}
