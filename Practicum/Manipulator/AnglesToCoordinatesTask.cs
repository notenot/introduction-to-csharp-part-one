using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        /// По значению углов суставов возвращает массив координат суставов
        /// в порядке new []{elbow, wrist, palmEnd}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            var shoulderPos = new PointF(0, 0);
            var elbowPos = GetNextPoint(
                shoulderPos, Manipulator.UpperArm, shoulder);
            var wristPos = GetNextPoint(
                elbowPos, Manipulator.Forearm, shoulder + Math.PI + elbow); 
            var palmEndPos = GetNextPoint(
                wristPos, Manipulator.Palm, shoulder + elbow + 2 * Math.PI + wrist); 
            return new[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }

        private static PointF GetNextPoint(
            PointF previousPoint, float length, double angle) =>
                new PointF(
                    previousPoint.X + length * (float)Math.Cos(angle), 
                    previousPoint.Y + length * (float)Math.Sin(angle));
    }

    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm, 
            Manipulator.Forearm, Manipulator.UpperArm, 0, Manipulator.UpperArm)]
        [TestCase(0, 0, 0, Manipulator.UpperArm - Manipulator.Forearm + Manipulator.Palm, 0, 
            Manipulator.UpperArm - Manipulator.Forearm, 0, Manipulator.UpperArm, 0)]
        [TestCase(-Math.PI / 2, Math.PI, Math.PI, 0, -Manipulator.UpperArm - Manipulator.Forearm - Manipulator.Palm, 
            0, -Manipulator.UpperArm - Manipulator.Forearm, 0, -Manipulator.UpperArm)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist,
            double palmEndX, double palmEndY,
            double wristEndX, double wristEndY,
            double elbowEndX, double elbowEndY)
        {
            {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);

            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            Assert.AreEqual(wristEndX, joints[1].X, 1e-5, "wrist endX");
            Assert.AreEqual(wristEndY, joints[1].Y, 1e-5, "wrist endY");
            Assert.AreEqual(elbowEndX, joints[0].X, 1e-5, "elbow endX");
            Assert.AreEqual(elbowEndY, joints[0].Y, 1e-5, "elbow endY");

            const int shoulderX = 0;
            const int shoulderY = 0;

            Assert.AreEqual(GetDistance(palmEndX, palmEndY, wristEndX, wristEndY), Manipulator.Palm);
            Assert.AreEqual(GetDistance(elbowEndX, elbowEndY, wristEndX, wristEndY), Manipulator.Forearm);
            Assert.AreEqual(GetDistance(elbowEndX, elbowEndY, shoulderX, shoulderY), Manipulator.UpperArm);
        }

        public static double GetDistance(double x1, double y1, double x2, double y2) =>
            Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
    }
}