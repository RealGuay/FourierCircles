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
        private int maxPoints = 1000;

        private List<Ellipse> points = new List<Ellipse>();

        public GraphicOfPoints()
        {
            InitializeComponent();
            Loaded += GraphicOfPoints_Loaded;
        }

        private void GraphicOfPoints_Loaded(object sender, RoutedEventArgs e)
        {
            //GraphicCanvas.Height = GraphicHeight;
            //GraphicCanvas.Width = GraphicWidth;
            //ReferenceLine.Y1 = GraphicHeight / 2;
            //ReferenceLine.Y2 = ReferenceLine.Y1;
            //ReferenceLine.X2 = GraphicWidth;

            CreateAllPoints();
        }

        private void CreateAllPoints()
        {
            GraphicCanvas.Children.Clear();
            for (int i = 0; i < maxPoints; i++)
            {
                Ellipse ellipse = new Ellipse() { Height = 2, Width = 2, Stroke = Brushes.Blue, StrokeThickness = 2 };
                points.Add(ellipse);
                Canvas.SetLeft(ellipse, i);
                Canvas.SetTop(ellipse, i);
                GraphicCanvas.Children.Add(ellipse);
            }
        }

        internal void AddPoint(Point endPosition, double sineGraphicX)
        {
            ScrollPreviousPoints();
            Canvas.SetLeft(points[0], 0);
            Canvas.SetTop(points[0], endPosition.Y);
        }

        private void ScrollPreviousPoints()
        {
            for (int i = points.Count - 1; i > 0; i--)
            {
                double top = Canvas.GetTop(points[i - 1]);
                Canvas.SetTop(points[i], top);
            }
        }
    }
}