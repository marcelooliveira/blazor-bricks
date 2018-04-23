using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using BlazorBricks.Core.Shapes;


namespace BlazorBricks.Core
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
