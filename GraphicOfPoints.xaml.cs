using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FourierCircles
{
    /// <summary>
    /// Interaction logic for GraphicOfPoints.xaml
    /// </summary>
    public partial class GraphicOfPoints : UserControl
    {
        private int maxPoints = 500;
        private int nbPoints = 0;

        public GraphicOfPoints()
        {
            InitializeComponent();
        }

        internal void AddPoint(Point endPosition, double sineGraphicX)
        {
            nbPoints++;
            if (nbPoints == maxPoints)
            {
                GraphicCanvas.Children.Clear();
                nbPoints = 0;
            }

            ScrollPreviousPoints();
            Ellipse ellipse = new Ellipse() { Height = 2, Width = 2 , Stroke=Brushes.Blue, StrokeThickness=2 };
            Canvas.SetLeft(ellipse, 0);
            Canvas.SetTop(ellipse, endPosition.Y);
            GraphicCanvas.Children.Add(ellipse);
        }

        private void ScrollPreviousPoints()
        {
            foreach (var item in GraphicCanvas.Children)
            {
                if (item is Ellipse ellipse)
                {
                    Canvas.SetLeft(ellipse, Canvas.GetLeft(ellipse) + 2.0);
                }
            }
        }
    }
}
