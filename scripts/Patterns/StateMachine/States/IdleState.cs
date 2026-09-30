using StateCommandDemo.Battle.Units;
using StateCommandDemo.Patterns.Commands;

namespace StateCommandDemo.Patterns.StateMachine.States
{
    public sealed class IdleState : StateInitializer, IEnterState
    {
        private readonly ICommand _stopMoving;

        public IdleState(Unit unit) : base(unit) => _stopMoving = new StopMoveCommand(unit);
        public void Enter() => _stopMoving.Execute();
    }
}
