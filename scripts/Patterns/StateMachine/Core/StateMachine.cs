using System;
using System.Collections.Generic;

namespace StateCommandDemo.Patterns.StateMachine
{
    public sealed class StateMachine
    {
        private readonly Dictionary<IState, List<Transition>> _transitions = new();
        public IState CurrentState { get; private set; }

        public StateMachine(IState initialState)
        {
            CurrentState = initialState ?? throw new ArgumentNullException(nameof(initialState));
            if (CurrentState is IEnterState enterState) enterState.Enter();
        }

        public void AddTransition(Transition transition)
        {
            if (!_transitions.TryGetValue(transition.From, out List<Transition> transitions))
            {
                transitions = new List<Transition>();
                _transitions.Add(transition.From, transitions);
            }
            transitions.Add(transition);
        }

        public void Tick()
        {
            if (TryGetNextState(out IState nextState)) SwitchTo(nextState);
            if (CurrentState is IUpdateState updateState) updateState.Tick();
        }

        private bool TryGetNextState(out IState nextState)
        {
            nextState = null;
            if (!_transitions.TryGetValue(CurrentState, out List<Transition> transitions)) return false;
            foreach (Transition transition in transitions)
            {
                if (!transition.Condition()) continue;
                nextState = transition.To;
                return true;
            }
            return false;
        }

        private void SwitchTo(IState nextState)
        {
            if (ReferenceEquals(CurrentState, nextState)) return;
            if (CurrentState is IExitState exitState) exitState.Exit();
            CurrentState = nextState;
            if (CurrentState is IEnterState enterState) enterState.Enter();
        }
    }
}
