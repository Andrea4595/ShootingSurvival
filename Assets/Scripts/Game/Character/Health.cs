using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Health : MonoBehaviour
    {
        public float maxHp { get; private set; }
        public float hp { get; private set; }

        public float hpPercent => Mathf.Clamp01(hp / Mathf.Max(maxHp, 1));

        public event Action onUpdate;
        public event Action onDie;

        public void Initialize(float maxHp)
        {
            this.maxHp = maxHp;
            hp = maxHp;
            onUpdate?.Invoke();
        }

        public void SetMaxHp(float newMaxHp)
        {
            var different = newMaxHp - maxHp;

            maxHp = newMaxHp;

            if (different > 0)
                hp += different;

            onUpdate?.Invoke();
        }

        public void TakeDamage(float damage)
        {
            hp -= damage;
            onUpdate?.Invoke();

            if (hp > 0)
                return;

            onDie?.Invoke();
        }

        public void Heal(float heal)
        {
            hp = MathF.Min(hp + heal, maxHp);
            onUpdate?.Invoke();
        }
    }
}