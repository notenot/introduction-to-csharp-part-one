namespace Mazes
{
	public static class SnakeMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
		    var ledgeCount = (height - 1) / 4;

		    for (var i = 0; i < ledgeCount; ++i)
		    {
		        MoveAlongLedge(robot, width - 2);

                if (i < ledgeCount - 1)
                    MoveTo(Direction.Down, robot, 2);
            }
		}

	    private static void MoveAlongLedge(Robot robot, int ledgeWidth)
	    {
	        MoveTo(Direction.Right, robot, ledgeWidth - 1);
	        MoveTo(Direction.Down, robot, 2);
            MoveTo(Direction.Left, robot, ledgeWidth - 1);
	    }

	    private static void MoveTo(Direction dir, Robot robot, int stepCount)
	    {
	        for (var i = 0; i < stepCount; ++i)
	            robot.MoveTo(dir);
        }
    }
}