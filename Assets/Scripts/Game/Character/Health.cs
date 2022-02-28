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

        public event Action onDie;  

        public void Initialize(float maxHp)
        {
            this.maxHp = maxHp;
            hp = maxHp;
        }

        public void SetMaxHp(float newMaxHp)
        {
            var different = newMaxHp - maxHp;

            maxHp = newMaxHp;

            if (different < 0)
                return;

            hp += different;
        }

        public void TakeDamage(float damage)
        {
            hp -= damage;

            if (hp > 0)
                return;

            onDie?.Invoke();
        }

        public void Heal(float heal)
        {
            hp = MathF.Min(hp + heal, maxHp);
        }
    }
}