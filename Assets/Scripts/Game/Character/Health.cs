using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Health : MonoBehaviour
    {
        Character _owner;

        public float maxHp { get; private set; }
        public float hp { get; private set; }

        public void Initialize(Character owner)
        {
            _owner = owner;
            maxHp = owner.information.maxHp;
            hp = owner.information.maxHp;
        }

        public void TakeDamage(float damage)
        {
            hp -= damage;

            if (hp <= 0)
                _owner.Destroy();
        }
    }
}