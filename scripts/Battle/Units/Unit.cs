using System.Collections;
using System.Linq;
using StateCommandDemo.Battle.Core;
using StateCommandDemo.Battle.Data;
using StateCommandDemo.Patterns.StateMachine;
using StateCommandDemo.Patterns.StateMachine.States;
using UnityEngine;

namespace StateCommandDemo.Battle.Units
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Unit : DamageableEntity
    {
        [SerializeField] private Rigidbody2D body;
        [SerializeField] private UnitData stats;
        [SerializeField, Min(0f)] private float stunDuration = 1f;

        private StateCommandDemo.Patterns.StateMachine.StateMachine _stateMachine;
        private Coroutine _attackRoutine;

        public abstract bool IsEnemy { get; }
        public bool IsAttacking { get; private set; }
        public float StunDuration => stunDuration;
        public Unit CurrentTarget { get; private set; }

        private void Awake()
        {
            if (body == null) body = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            InitializeHealth(stats.health);
            ConfigureStateMachine();
            BattleManager.Instance?.Register(this);
        }

        private void Update() => _stateMachine?.Tick();

        public bool FindTarget()
        {
            CurrentTarget = BattleManager.Instance?.GetOpponents(this)
                .Where(unit => unit != null && unit.IsAlive)
                .OrderBy(unit => Vector2.SqrMagnitude(unit.transform.position - transform.position))
                .FirstOrDefault();
            return CurrentTarget != null;
        }

        public void MoveTowardsTarget()
        {
            if (CurrentTarget == null && !FindTarget()) return;
            Vector2 direction = (CurrentTarget.transform.position - transform.position).normalized;
            body.velocity = direction * stats.moveSpeed;
        }

        public void StopMoving() => body.velocity = Vector2.zero;

        public bool CanAttackTarget()
        {
            return CurrentTarget != null
                && CurrentTarget.IsAlive
                && Vector2.Distance(transform.position, CurrentTarget.transform.position) <= stats.attackRange;
        }

        public void StartAttacking()
        {
            if (IsAttacking) return;
            IsAttacking = true;
            _attackRoutine = StartCoroutine(Attack());
        }

        public void StopAttacking()
        {
            if (_attackRoutine != null) StopCoroutine(_attackRoutine);
            _attackRoutine = null;
            IsAttacking = false;
        }

        private IEnumerator Attack()
        {
            while (IsAlive && CanAttackTarget())
            {
                CurrentTarget.TakeDamage(stats.damage);
                yield return new WaitForSeconds(stats.attackInterval);
            }
            IsAttacking = false;
            _attackRoutine = null;
        }

        private void ConfigureStateMachine()
        {
            var idle = new IdleState(this);
            var move = new MoveState(this);
            var attack = new AttackState(this);
            var stun = new StunState(this);
            var death = new DeathState(this);

            _stateMachine = new StateCommandDemo.Patterns.StateMachine.StateMachine(idle);
            AddTransition(idle, death, () => !IsAlive);
            AddTransition(move, death, () => !IsAlive);
            AddTransition(attack, death, () => !IsAlive);
            AddTransition(stun, death, () => !IsAlive);
            AddTransition(idle, stun, () => IsAlive && IsStunned);
            AddTransition(move, stun, () => IsAlive && IsStunned);
            AddTransition(attack, stun, () => IsAlive && IsStunned);
            AddTransition(stun, move, () => IsAlive && !IsStunned);
            AddTransition(idle, move, () => IsAlive && !IsStunned && FindTarget());
            AddTransition(move, attack, () => IsAlive && !IsStunned && CanAttackTarget());
            AddTransition(attack, move, () => IsAlive && !IsStunned && !CanAttackTarget());
        }

        private void AddTransition(IState from, IState to, System.Func<bool> condition)
        {
            _stateMachine.AddTransition(new Transition(from, to, condition));
        }

        protected override void OnDeath() => StopAttacking();
    }
}
