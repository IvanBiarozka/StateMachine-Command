using StateCommandDemo.Battle.Units;
using StateCommandDemo.Patterns.Commands;

namespace StateCommandDemo.Patterns.StateMachine.States
{
    public sealed class MoveState : StateInitializer, IEnterState, IUpdateState, IExitState
    {
        private readonly ICommand _move;
        private readonly ICommand _stopMoving;

        public MoveState(Unit unit) : base(unit)
        {
            _move = new MoveCommand(unit);
            _stopMoving = new StopMoveCommand(unit);
        }

        public void Enter() { }
        public void Tick() => _move.Execute();
        public void Exit() => _stopMoving.Execute();
    }
}
