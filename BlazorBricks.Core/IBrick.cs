using System;
using System.Collections.Generic;
using System.Text;
using BlazorBricks.Core.Shapes;
//using System.Drawing;
using System.ComponentModel;
using System.Drawing;

namespace BlazorBricks.Core
{
    public interface IBrick : INotifyPropertyChanged
    {
        double X { get; set; }
        double Y { get; set; }
        Color Color { get; set; }
    }
}
