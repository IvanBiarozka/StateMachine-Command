using StateCommandDemo.Battle.Units;

namespace StateCommandDemo.Patterns.Commands
{
    public sealed class StopAttackCommand : ICommand
    {
        private readonly Unit _unit;
        public StopAttackCommand(Unit unit) => _unit = unit;
        public void Execute() => _unit.StopAttacking();
    }
}
