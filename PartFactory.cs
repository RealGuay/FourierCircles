using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FourierCircles
{
    class PartFactory
    {
        public static CircleArm CreateCircleArm(Canvas canvas, int lenght, double rotationSpeed)
        {
            CircleArm arm = new CircleArm {MainCanvas = canvas,  ArmLength = lenght, RotationSpeed = rotationSpeed };
            return arm;
        }
    }
}
