using System.Collections.Generic;
using StateCommandDemo.Battle.Units;
using UnityEngine;

namespace StateCommandDemo.Battle.Core
{
    public sealed class BattleManager : MonoBehaviour
    {
        private readonly List<Unit> _allies = new();
        private readonly List<Unit> _enemies = new();
        public static BattleManager Instance { get; private set; }
        public IReadOnlyList<Unit> Allies => _allies;
        public IReadOnlyList<Unit> Enemies => _enemies;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        public void Register(Unit unit)
        {
            List<Unit> team = unit.IsEnemy ? _enemies : _allies;
            if (!team.Contains(unit)) team.Add(unit);
        }

        public void Deregister(Unit unit)
        {
            _allies.Remove(unit);
            _enemies.Remove(unit);
        }

        public IReadOnlyList<Unit> GetOpponents(Unit unit) => unit.IsEnemy ? _allies : _enemies;
    }
}
