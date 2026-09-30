using System;

namespace StateCommandDemo.Battle.Data
{
    [Serializable]
    public struct UnitData
    {
        public float damage;
        public float health;
        public float attackRange;
        public float attackInterval;
        public float moveSpeed;
    }
}
