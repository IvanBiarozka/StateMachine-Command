using StateCommandDemo.Battle.Units;
using StateCommandDemo.Patterns.Commands;

namespace StateCommandDemo.Patterns.StateMachine.States
{
    public sealed class DeathState : StateInitializer, IEnterState
    {
        private readonly ICommand _die;

        public DeathState(Unit unit) : base(unit) => _die = new DieCommand(unit);
        public void Enter() => _die.Execute();
    }
}
