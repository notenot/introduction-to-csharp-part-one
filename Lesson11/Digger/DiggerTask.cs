using System.Windows.Forms;

namespace Digger
{
    public class Terrain : ICreature
    {
        public string GetImageFileName()
        {
            return "Terrain.png";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }
    }

    public class Player : ICreature
    {
        public string GetImageFileName()
        {
            return "Digger.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y)
        {
            var command = new CreatureCommand();
            var key = Game.KeyPressed;

            switch (key)
            {
                case Keys.Up:
                    if (y > 0 && !(Game.Map[x, y - 1] is Sack)) 
                        command.DeltaY = -1;
                    break;
                case Keys.Down:
                    if (y < Game.MapHeight - 1 && !(Game.Map[x, y + 1] is Sack)) 
                        command.DeltaY = 1;
                    break;
                case Keys.Left:
                    if (x > 0 && !(Game.Map[x - 1, y] is Sack)) 
                        command.DeltaX = -1;
                    break;
                case Keys.Right:
                    if (x < Game.MapWidth - 1 && !(Game.Map[x + 1, y] is Sack)) 
                        command.DeltaX = 1;
                    break;
            }

            return command;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack;
        }
    }

    public class Sack : ICreature
    {
        private int passedCells;

        public string GetImageFileName()
        {
            return "Sack.png";
        }

        public int GetDrawingPriority()
        {
            return 3;
        }

        public CreatureCommand Act(int x, int y)
        {
            var command = new CreatureCommand();
            var isFalling = y < Game.MapHeight - 1 &&
                (Game.Map[x, y + 1] is Player && passedCells > 0 || Game.Map[x, y + 1] == null);

            if (isFalling)
            {
                command.DeltaY = 1;
                ++passedCells;
            }
            else
            {
                if (passedCells > 1)
                    command.TransformTo = new Gold();
                passedCells = 0;
            }

            return command;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }

    public class Gold : ICreature
    {
        public string GetImageFileName()
        {
            return "Gold.png";
        }

        public int GetDrawingPriority()
        {
            return 2;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            var isConflictWithPlayer = conflictedObject is Player;
            if (isConflictWithPlayer)
                Game.Scores += 10;
            return isConflictWithPlayer;
        }
    }
}
