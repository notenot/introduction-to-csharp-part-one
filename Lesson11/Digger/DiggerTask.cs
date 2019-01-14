using System;
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
            return conflictedObject is Sack || conflictedObject is Monster;
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
            return 2;
        }

        public CreatureCommand Act(int x, int y)
        {
            var command = new CreatureCommand();
            var isFalling = y < Game.MapHeight - 1 &&
                ((Game.Map[x, y + 1] is Player || Game.Map[x, y + 1] is Monster) 
                 && passedCells > 0 || Game.Map[x, y + 1] == null);

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
            return isConflictWithPlayer|| conflictedObject is Monster;
        }
    }

    public class Monster : ICreature
    {
        public string GetImageFileName()
        {
            return "Monster.png";
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public CreatureCommand Act(int x, int y)
        {
            var diggerCoordinates = GetDiggerCoordinates();

            return diggerCoordinates != null ?
                MoveIfPossible(x, y, diggerCoordinates.Item1 - x, diggerCoordinates.Item2 - y) : 
                new CreatureCommand();
        }

        private bool CellIsSuitable(int x, int y) => 
            Game.Map[x, y] == null || Game.Map[x, y] is Player || Game.Map[x, y] is Gold;

        private CreatureCommand MoveIfPossible(int x, int y, int diffX, int diffY)
        {
            // right
            if (diffX > 0 && x < Game.MapWidth - 1 && CellIsSuitable(x + 1, y)) 
                return new CreatureCommand { DeltaX = 1 } ;

            // left
            if (diffX < 0 && x > 0 && CellIsSuitable(x - 1, y)) 
                return new CreatureCommand { DeltaX = -1 };

            // down
            if (diffY > 0 &&  y < Game.MapHeight - 1 && CellIsSuitable(x, y + 1)) 
                return new CreatureCommand { DeltaY = 1 };

            // up
            if (diffY < 0 && y > 0 && CellIsSuitable(x, y - 1)) 
                return new CreatureCommand { DeltaY = -1 };

            return new CreatureCommand();
        } 

        private Tuple<int, int> GetDiggerCoordinates()
        {
            for (var x = 0; x < Game.MapWidth; ++x)
                for (var y = 0; y < Game.MapHeight; ++y)
                    if (Game.Map[x, y] is Player)
                        return new Tuple<int, int>(x, y);

            return null;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject is Sack || conflictedObject is Monster;
        }
    }
}
