using StateCommandDemo.Battle.Units;
using StateCommandDemo.Patterns.Commands;

namespace StateCommandDemo.Patterns.StateMachine.States
{
    public sealed class AttackState : StateInitializer, IEnterState, IExitState
    {
        private readonly ICommand _stopMoving;
        private readonly ICommand _startAttacking;
        private readonly ICommand _stopAttacking;

        public AttackState(Unit unit) : base(unit)
        {
            _stopMoving = new StopMoveCommand(unit);
            _startAttacking = new StartAttackCommand(unit);
            _stopAttacking = new StopAttackCommand(unit);
        }

        public void Enter()
        {
            _stopMoving.Execute();
            _startAttacking.Execute();
        }

        public void Exit() => _stopAttacking.Execute();
    }
}
