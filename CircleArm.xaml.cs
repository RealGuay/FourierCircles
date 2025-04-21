using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Converters;

namespace FourierCircles
{
    /// <summary>
    /// Interaction logic for CircleArm.xaml
    /// </summary>
    public partial class CircleArm : UserControl
    {
        public Canvas? MainCanvas { get; set; }
        public int ArmLength { get; set; }
        public double RotationSpeed {  get; set; }  

        public CircleArm? NextCircleArm { get; set; }

        public double ArmCenterX
        {
            get { return (double)GetValue(ArmCenterXProperty); }
            set { SetValue(ArmCenterXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArmCenterX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArmCenterXProperty =
            DependencyProperty.Register("ArmCenterX", typeof(double), typeof(CircleArm), new PropertyMetadata(0.0));

        public double ArmCenterY
        {
            get { return (double)GetValue(ArmCenterYProperty); }
            set { SetValue(ArmCenterYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArmCenterY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArmCenterYProperty =
            DependencyProperty.Register("ArmCenterY", typeof(double), typeof(CircleArm), new PropertyMetadata(0.0));

        public double ArmEndX
        {
            get { return (double)GetValue(ArmEndXProperty); }
            set { SetValue(ArmEndXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArmEndX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArmEndXProperty =
            DependencyProperty.Register("ArmEndX", typeof(double), typeof(CircleArm), new PropertyMetadata(0.0));

        public double ArmEndY
        {
            get { return (double)GetValue(ArmEndYProperty); }
            set { SetValue(ArmEndYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArmEndY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArmEndYProperty =
            DependencyProperty.Register("ArmEndY", typeof(double), typeof(CircleArm), new PropertyMetadata(0.0));

        public double RotateAngle
        {
            get { return (double)GetValue(RotateAngleProperty); }
            set { SetValue(RotateAngleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RotateAngle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RotateAngleProperty =
            DependencyProperty.Register("RotateAngle", typeof(double), typeof(CircleArm), new PropertyMetadata(0.0));

        public CircleArm()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void RotateArm(TimeSpan deltaT)
        {
            RotateAngle = deltaT.TotalSeconds * RotationSpeed;

            if (NextCircleArm != null)
            {
                MoveNextArm();
                NextCircleArm.RotateArm(deltaT);
            }
        }

        private void MoveNextArm()
        {
            if (NextCircleArm == null) return;

            try
            {
                Point point = SegmentLine.TransformToAncestor(MainCanvas).Transform(new Point(SegmentLine.X2, SegmentLine.Y2));

                double deltaX = point.X - NextCircleArm.ArmCenterX;
                double deltaY = point.Y - NextCircleArm.ArmCenterY;

                NextCircleArm.ArmCenterX = point.X;
                NextCircleArm.ArmCenterY = point.Y;
                NextCircleArm.ArmEndX += deltaX;
                NextCircleArm.ArmEndY += deltaY;
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
            }
        }

        internal void AddCircleArm(CircleArm newArm)
        {
            if (newArm == null) return;

            if (NextCircleArm == null)
            {
                NextCircleArm = newArm;
                MoveNextArm();
                NextCircleArm.ArmEndX = NextCircleArm.ArmCenterX + NextCircleArm.ArmLength;
                NextCircleArm.ArmEndY = NextCircleArm.ArmCenterY;
            }
            else
            {
                NextCircleArm.AddCircleArm(newArm);
            }
        }
    }
}