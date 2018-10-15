using System;

namespace Billiards
{
    public static class BilliardsTask
    {
        /// <summary>
        /// Расчитывает угол отскока шара от стены без учета физических эффектов. 
        /// </summary>
        /// <param name="directionRadians">Угол направелния движения шара</param>
        /// <param name="wallInclinationRadians">Угол</param>
        /// <returns>Угол отскока</returns>
        public static double BounceWall(double directionRadians, double wallInclinationRadians)
        {
            var wallInclinationDegrees = wallInclinationRadians * 180 / Math.PI;
            var directionDegrees = directionRadians * 180 / Math.PI;
            return (360 - directionDegrees + 2 * wallInclinationDegrees) * Math.PI / 180.0;
        }
    }
}