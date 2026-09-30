using StateCommandDemo.Battle.Core;
using StateCommandDemo.Battle.Units;
using UnityEngine;

namespace StateCommandDemo.Patterns.Commands
{
    public sealed class DieCommand : ICommand
    {
        private readonly Unit _unit;
        public DieCommand(Unit unit) => _unit = unit;

        public void Execute()
        {
            BattleManager.Instance?.Deregister(_unit);
            _unit.StopMoving();
            Object.Destroy(_unit.gameObject, 0.3f);
        }
    }
}
