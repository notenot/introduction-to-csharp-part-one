using System;

namespace Mazes
{
	public static class DiagonalMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
		    var partLength = GetPartLength(width, height);
		    var partCount = GetPartCount(width, height);
            var partDirection = GetPartDirection(width, height);
            var newLineDirection = GetNewLineDirection(width, height);
            
            for (var i = 0; i < partCount; ++i)
		    {
                MoveTo(partDirection, robot, partLength);

                if (i < partCount - 1)
                    MoveTo(newLineDirection, robot, 1);
		    }
		}

        private static void MoveTo(Direction dir, Robot robot, int stepCount)
	    {
	        for (var i = 0; i < stepCount; ++i)
	            robot.MoveTo(dir);
	    }

        private static int GetPartLength(int width, int height) =>
	        (int) Math.Round(Math.Max(
	            (double) width / height, 
	            (double) height / width));

        private static int GetPartCount(int width, int height) =>
            Math.Min(height - 2, width - 2);

        private static Direction GetPartDirection(int width, int height) =>
            height < width ? Direction.Right : Direction.Down;

        private static Direction GetNewLineDirection(int width, int height) =>
            height < width ? Direction.Down : Direction.Right;
    }
}