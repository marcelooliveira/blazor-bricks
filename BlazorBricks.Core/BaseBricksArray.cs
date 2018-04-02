using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MVCBricks.Core.Shapes;
using System.Drawing;

namespace MVCBricks.Core
{
    public abstract class BaseBricksArray : IBricksArray
    {
        #region attributes
        protected int width = 0;
        protected int height = 0;
        protected string shapeString = "";
        protected IBrick[,] shapeArray = null;
        protected IPresenter presenter = null;
        #endregion attributes

        #region methods
        public string GetStringFromShapeArray()
        {
            string ret = "";
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    if (shapeArray[column, row] != null)
                    {
                        //ret += ((int)(shapeArray[column, row].Color)).ToString();
                        ret += "1";
                    }
                    else
                    {
                        ret += "0";
                    }
                }
            }
            return ret;
        }

        protected Color GetShapeColorFromInteger(int c)
        {
            Color ret = Color.White;

            switch (c)
            {
                case 1:
                    ret = Color.Blue;
                    break;
                case 2:
                    ret = Color.DarkGray;
                    break;
                case 3:
                    ret = Color.Green;
                    break;
                case 4:
                    ret = Color.Orange;
                    break;
                case 5:
                    ret = Color.LightGray;
                    break;
                case 6:
                    ret = Color.Red;
                    break;
                case 7:
                    ret = Color.Purple;
                    break;
                case 8:
                    ret = Color.White;
                    break;
                case 9:
                    ret = Color.Yellow;
                    break;
            }

            return ret;
        }

        public virtual void InitializeArray()
        {
            shapeArray = new IBrick[width, height];
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    shapeArray[column, row] = null;
                }
            }
        }

        private string Replicate(string s, int n)
        {
            string ret = "";
            for (int i = 0; i < n; i++)
            {
                ret += s;
            }
            return ret;
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public string ShapeString
        {
            get { return GetStringFromShapeArray(); }
        }

        public IBrick[,] ShapeArray
        {
            get { return shapeArray; }
        }

        public IPresenter Presenter
        {
            get { return presenter; }
            set { presenter = value; }
        }
        #endregion methods
    }
}
