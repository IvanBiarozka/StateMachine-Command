namespace StateCommandDemo.Patterns.StateMachine
{
    public interface IState { }
    public interface IEnterState : IState { void Enter(); }
    public interface IUpdateState : IState { void Tick(); }
    public interface IExitState : IState { void Exit(); }
}
