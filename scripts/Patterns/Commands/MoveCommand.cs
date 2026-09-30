using StateCommandDemo.Battle.Units;

namespace StateCommandDemo.Patterns.Commands
{
    public sealed class MoveCommand : ICommand
    {
        private readonly Unit _unit;
        public MoveCommand(Unit unit) => _unit = unit;

        public void Execute()
        {
            _unit.FindTarget();
            _unit.MoveTowardsTarget();
        }
    }
}
