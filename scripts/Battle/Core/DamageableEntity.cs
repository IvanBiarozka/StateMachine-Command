using System.Collections;
using UnityEngine;

namespace StateCommandDemo.Battle.Core
{
    public abstract class DamageableEntity : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
        public bool IsAlive { get; private set; }
        public bool IsStunned { get; private set; }

        protected void InitializeHealth(float maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
            IsAlive = true;
            IsStunned = false;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void TakeDamage(float damage)
        {
            if (!IsAlive) return;
            CurrentHealth = Mathf.Clamp(CurrentHealth - damage, 0f, MaxHealth);
            if (!Mathf.Approximately(CurrentHealth, 0f)) return;
            IsAlive = false;
            OnDeath();
        }

        public void Stun(float duration) => StartCoroutine(StunFor(duration));
        public void ApplyKnockback(Vector2 force) => _rigidbody?.AddForce(force, ForceMode2D.Impulse);

        private IEnumerator StunFor(float duration)
        {
            IsStunned = true;
            yield return new WaitForSeconds(duration);
            IsStunned = false;
        }

        protected abstract void OnDeath();
    }
}
