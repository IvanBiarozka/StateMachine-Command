using System;

namespace StateCommandDemo.Patterns.StateMachine
{
    public sealed class Transition
    {
        public IState From { get; }
        public IState To { get; }
        public Func<bool> Condition { get; }

        public Transition(IState from, IState to, Func<bool> condition)
        {
            From = from ?? throw new ArgumentNullException(nameof(from));
            To = to ?? throw new ArgumentNullException(nameof(to));
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
        }
    }
}
