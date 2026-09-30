using StateCommandDemo.Battle.Units;

namespace StateCommandDemo.Patterns.Commands
{
    public sealed class StartAttackCommand : ICommand
    {
        private readonly Unit _unit;
        public StartAttackCommand(Unit unit) => _unit = unit;
        public void Execute() => _unit.StartAttacking();
    }
}
