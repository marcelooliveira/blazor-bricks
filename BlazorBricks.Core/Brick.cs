using System;
using System.Collections.Generic;
using System.Text;
using BlazorBricks.Core.Shapes;
using System.Drawing;

namespace BlazorBricks.Core
{
    public class Brick : IBrick
    {
        private double x = 0;
        private double y = 0;
        private double left = 0;
        private double top = 0;
        private double leftMargin = 4;
        private double topMargin = 4;
        private double brickSize = 20;
        private Color color = Color.White;


        public Brick(double x, double y, Color color)
        {
            this.x = x;
            this.y = y;
            this.color = color;
        }

        public double X
        {
            get { return x; }
            set
            {
                x = value;
                Left = x * brickSize + leftMargin;
            }
        }

        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                Top = y * brickSize + topMargin;
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
        }

        public double Left
        {
            get { return left; }
            set
            {
                left = value;
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Left"));
                OnPropertyChanged("Left");
            }
        }

        public double Top
        {
            get { return top; }
            set
            {
                top = value;
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("Top"));
                OnPropertyChanged("Top");
            }
        }

        #region INotifyPropertyChanged Members
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this,
                    new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
