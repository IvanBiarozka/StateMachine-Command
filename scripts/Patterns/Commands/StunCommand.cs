using StateCommandDemo.Battle.Units;

namespace StateCommandDemo.Patterns.Commands
{
    public sealed class StunCommand : ICommand
    {
        private readonly Unit _unit;
        private readonly float _duration;

        public StunCommand(Unit unit, float duration)
        {
            _unit = unit;
            _duration = duration;
        }

        public void Execute() => _unit.Stun(_duration);
    }
}
