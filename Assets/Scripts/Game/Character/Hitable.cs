using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class Hitable : MonoBehaviour
    {
        public enum Type { Character, Projectile }

        [SerializeField]
        Health _health;

        public Character.Force force { get; private set; }
        public Type type { get; private set; }

        public void Initialize(Character.Force force, Type type)
        {
            this.force = force;
            this.type = type;
        }

        public bool canHit => _health.maxHp > 0;

        public void Hit(float damage) => _health.TakeDamage(damage);
    }
}