using System;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {
        /// <summary>
        /// Возвращает массив углов (shoulder, elbow, wrist),
        /// необходимых для приведения эффектора манипулятора в точку x и y 
        /// с углом между последним суставом и горизонталью, равному angle (в радианах)
        /// См. чертеж manipulator.png!
        /// </summary>
        public static double[] MoveManipulatorTo(double x, double y, double angle)
        {
            var wristX = x + Manipulator.Palm * Math.Cos(Math.PI - angle);
            var wristY = y + Manipulator.Palm * Math.Sin(Math.PI - angle);
            var shoulderWristLength = Math.Sqrt(wristX * wristX + wristY * wristY);
            var elbow = TriangleTask.GetABAngle(
                Manipulator.UpperArm, Manipulator.Forearm, shoulderWristLength);

            if (double.IsNaN(elbow))
                return new[] { double.NaN, double.NaN, double.NaN };
            
            var shoulderWrist = Math.Atan2(wristY, wristX);
            var shoulder = TriangleTask.GetABAngle(
                               Manipulator.UpperArm, shoulderWristLength, Manipulator.Forearm) 
                           + shoulderWrist;

            return double.IsNaN(shoulder) 
                ? new[] { double.NaN, double.NaN, double.NaN } 
                : new[] { shoulder, elbow, -angle - shoulder - elbow };
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [Test]
        public void TestMoveManipulatorTo()
        {
            const float armLength = Manipulator.Forearm + Manipulator.Palm + Manipulator.UpperArm;
            var rnd = new Random();
            for (var i = 0; i < 100; ++i)
            {
                var x = rnd.NextDouble() * 2 * armLength - armLength;
                var y = rnd.NextDouble() * 2 * armLength - armLength;
                var angle = rnd.NextDouble() * 2 * Math.PI;
                var solution = ManipulatorTask.MoveManipulatorTo(x, y, angle);

                if (double.IsNaN(solution[0]))
                    continue;

                var palmEnd = AnglesToCoordinatesTask.GetJointPositions(
                    solution[0], solution[1], solution[2])[2];
                Assert.AreEqual(palmEnd.X, x, 1e-4);
                Assert.AreEqual(palmEnd.Y, y, 1e-4);
            }
        }
    }
}