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
    /// Interaction logic for CircleArm.xaml
    /// </summary>
    public partial class CircleArm : UserControl
    {
        public CircleArm()
        {
            InitializeComponent();
            DataContext = this;
        }

        public int ArmLength { get; set; }

        public int RotateAngle
        {
            get { return (int)GetValue(RotateAngleProperty); }
            set { SetRotateAngle(value); }
        }

        private void SetRotateAngle(int value)
        {
            SetValue(RotateAngleProperty, value);
            double x2 = SegmentLine.X2;
            double y2 = SegmentLine.Y2;

            Point point = SegmentLine.TransformToAncestor(CircleCanvas).Transform(new Point(SegmentLine.X2, SegmentLine.Y2));
            Canvas.SetLeft(MyRect, point.X);
            Canvas.SetTop(MyRect, point.Y);
        }

        // Using a DependencyProperty as the backing store for RotateAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RotateAngleProperty =
            DependencyProperty.Register("RotateAngle", typeof(int), typeof(CircleArm), new PropertyMetadata(0));
    }
}