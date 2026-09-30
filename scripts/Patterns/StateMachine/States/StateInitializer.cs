using StateCommandDemo.Battle.Units;

namespace StateCommandDemo.Patterns.StateMachine.States
{
    public abstract class StateInitializer
    {
        protected readonly Unit Unit;
        protected StateInitializer(Unit unit) => Unit = unit;
    }
}
