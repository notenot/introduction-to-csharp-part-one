namespace Mazes
{
	public static class EmptyMazeTask
	{
		public static void MoveOut(Robot robot, int width, int height)
		{
            MoveDown(robot, height - 3);
            MoveRight(robot, width - 3);
		}

	    private static void MoveDown(Robot robot, int stepCount)
	    {
            for (var i = 0; i < stepCount; ++i)
                robot.MoveTo(Direction.Down);
	    }

        private static void MoveRight(Robot robot, int stepCount)
        {
            for (var i = 0; i < stepCount; ++i)
                robot.MoveTo(Direction.Right);
        }
    }
}