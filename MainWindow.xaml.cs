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
    }

    private void OnTimer(object? sender, EventArgs e)
    {
        int nextAngle = MainCircleArm.RotateAngle - 1;
        if (nextAngle <= -360)
        {
            nextAngle = 0;
        }
        MainCircleArm.RotateAngle = nextAngle;
    }
}