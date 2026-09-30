using StateCommandDemo.Battle.Units;

namespace StateCommandDemo.Patterns.Commands
{
    public sealed class StopMoveCommand : ICommand
    {
        private readonly Unit _unit;
        public StopMoveCommand(Unit unit) => _unit = unit;
        public void Execute() => _unit.StopMoving();
    }
}
