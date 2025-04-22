using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FourierCircles;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private DispatcherTimer _circleTimer = new DispatcherTimer(DispatcherPriority.Render);

    private int subArmQty = 4;
    private DateTime _startTime;

    public MainWindow()
    {
        InitializeComponent();
        InitializeTimer();
    }

    private void InitializeTimer()
    {
        _circleTimer.Tick += OnTimer;
        _circleTimer.Interval = TimeSpan.FromMilliseconds(20);
        _circleTimer.Start();
        _startTime = DateTime.Now;
    }

    private void OnTimer(object? sender, EventArgs e)
    {
        TimeSpan timeSpan = DateTime.Now - _startTime;

        RootCircleCanvas.RotateArm(timeSpan);

        int t1 = (int)timeSpan.TotalMilliseconds;
        int t2 = t1 % 123;
        if (t2 == 0)
        {
            AddNewCircleArm();
        }
    }

    private void AddNewCircleArm()
    {
        if (subArmQty > 0)
        {
            int len = 200 * subArmQty / 10;
            double speed = 50 / subArmQty ;
            RootCircleCanvas.AddCircleArm(len, speed);
            subArmQty -= 1;
        }
    }
}