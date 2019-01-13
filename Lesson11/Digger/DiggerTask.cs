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
                    if (y > 0) 
                        command.DeltaY = -1;
                    break;
                case Keys.Down:
                    if (y < Game.MapHeight - 1) 
                        command.DeltaY = 1;
                    break;
                case Keys.Left:
                    if (x > 0) 
                        command.DeltaX = -1;
                    break;
                case Keys.Right:
                    if (x < Game.MapWidth - 1) 
                        command.DeltaX = 1;
                    break;
            }

            return command;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }
    }
}
