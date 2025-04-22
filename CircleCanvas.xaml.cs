using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace FourierCircles
{
    /// <summary>
    /// Interaction logic for CircleCanva.xaml
    /// </summary>
    public partial class CircleCanvas : UserControl
    {
        private CircleArm? rootCircleArm;

        public int RootCircleCenterX
        {
            get { return (int)GetValue(RootCircleCenterXProperty); }
            set { SetValue(RootCircleCenterXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RootCircleCenterX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RootCircleCenterXProperty =
            DependencyProperty.Register("RootCircleCenterX", typeof(int), typeof(CircleCanvas), new PropertyMetadata(0));

        public int RootCircleCenterY
        {
            get { return (int)GetValue(RootCircleCenterYProperty); }
            set { SetValue(RootCircleCenterYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RootCircleCenterY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RootCircleCenterYProperty =
            DependencyProperty.Register("RootCircleCenterY", typeof(int), typeof(CircleCanvas), new PropertyMetadata(0));

        public double LastArmEndX
        {
            get { return (double)GetValue(LastArmEndXProperty); }
            set { SetValue(LastArmEndXProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LastArmEndX.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastArmEndXProperty =
            DependencyProperty.Register("LastArmEndX", typeof(double), typeof(CircleCanvas), new PropertyMetadata(0.0));

        public double LastArmEndY
        {
            get { return (double)GetValue(LastArmEndYProperty); }
            set { SetValue(LastArmEndYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LastArmEndY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LastArmEndYProperty =
            DependencyProperty.Register("LastArmEndY", typeof(double), typeof(CircleCanvas), new PropertyMetadata(0.0));

        public CircleCanvas()
        {
            InitializeComponent();
            DataContext = this;

            Loaded += CircleCanvas_Loaded;
        }

        private void CircleCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            AddCenterTargetLines();
        }

        private void AddCenterTargetLines()
        {
            int crossLineLength = 20;
            CrossHorizontalLine.X1 = RootCircleCenterX - crossLineLength;
            CrossHorizontalLine.X2 = RootCircleCenterX + crossLineLength;
            CrossHorizontalLine.Y1 = RootCircleCenterY;
            CrossHorizontalLine.Y2 = RootCircleCenterY;

            CrossVerticalLine.X1 = RootCircleCenterX;
            CrossVerticalLine.X2 = RootCircleCenterX;
            CrossVerticalLine.Y1 = RootCircleCenterY - crossLineLength;
            CrossVerticalLine.Y2 = RootCircleCenterY + crossLineLength;
        }

        public void AddCircleArm(int lenght, double rotationSpeed)
        {
            CircleArm arm = PartFactory.CreateCircleArm(RootCanvas, lenght, rotationSpeed);

            if (arm == null) return;

            if (rootCircleArm != null)
            {
                rootCircleArm.AddCircleArm(arm);
                arm.LastArmEndPositionUpdated += RootCircleArm_LastArmEndPositionUpdated;
            }
            else
            {
                arm.ArmCenterX = RootCircleCenterX;
                arm.ArmCenterY = RootCircleCenterY;
                arm.ArmEndX = RootCircleCenterX + arm.ArmLength;
                arm.ArmEndY = RootCircleCenterY;
                rootCircleArm = arm;
                LastArmEndX = arm.ArmEndX;
                LastArmEndY = arm.ArmEndY;
                arm.LastArmEndPositionUpdated += RootCircleArm_LastArmEndPositionUpdated;
            }
            RootCanvas.Children.Add(arm);
        }

        private void RootCircleArm_LastArmEndPositionUpdated(object? sender, EndPositionEventArgs e)
        {
            Trace.TraceInformation($" Position : {e.EndPosition.X} , {e.EndPosition.Y}");
            LastArmEndX = e.EndPosition.X;
            LastArmEndY = e.EndPosition.Y;
        }

        internal void RotateArm(TimeSpan timeSpan)
        {
            rootCircleArm?.RotateArm(timeSpan);
        }
    }
}