using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        bool _integerHealth;
        [SerializeField]
        float _invulnerableTime;
        float _lastHit;

        public float maxHp { get; private set; }
        public float hp { get; private set; }

        public float hpPercent => Mathf.Clamp01(hp / Mathf.Max(maxHp, 1));

        public event Action onUpdate;
        public event Action onHit;
        public event Action onDie;

        public void Initialize(float maxHp)
        {
            if (_integerHealth)
                maxHp = MathF.Round(maxHp);

            this.maxHp = maxHp;
            hp = maxHp;
            onUpdate?.Invoke();
        }

        public void SetMaxHp(float newMaxHp)
        {
            if (_integerHealth)
                newMaxHp = MathF.Round(newMaxHp);

            var different = newMaxHp - maxHp;

            maxHp = newMaxHp;

            if (different > 0)
                hp += different;

            onUpdate?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            if (_lastHit + _invulnerableTime > Time.time)
                return;

            _lastHit = Time.time;

            if (_integerHealth)
                damage = MathF.Round(damage);

            hp -= damage;
            onHit?.Invoke();
            onUpdate?.Invoke();

            if (hp > 0)
                return;

            onDie?.Invoke();
        }

        public void Heal(float heal)
        {
            if (_integerHealth)
                heal = MathF.Round(heal);

            hp = MathF.Min(hp + heal, maxHp);
            onUpdate?.Invoke();
        }
    }
}