using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Explosion : ObjectPoolInstance<Explosion>
    {
        [SerializeField]
        Transform _body;
        [SerializeField]
        SpriteRenderer _sprite;

        Character.Force _force;
        Data.Object.Weapon.Projectile _information;

        List<Character> _hits = new List<Character>();

        public void Initialize(Character owner, Data.Object.Weapon.Projectile information, Vector3 position)
        {
            _force = owner.force;
            _information = information;

            _hits.Clear();

            _body.position = position;
            _sprite.color = owner.color;

            StartCoroutine(CRun());
        }

        IEnumerator CRun()
        {
            _body.transform.localScale = new Vector3(_information.range, _information.range, 1);

            var alpha = 1f;

            while(alpha > 0)
            {
                alpha -= Time.smoothDeltaTime;
                _sprite.color = new Color(1, 1, 1, alpha);

                yield return null;
            }

            Destroy();
        }

        void GiveDamage(Character target)
        {
            target.health.TakeDamage(_information.damage);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var target = collision.GetComponent<Character>();

            if (target == null)
                return;

            if (target.force == _force)
                return;
            
            if (_hits.Contains(target) == true)
                return;

            GiveDamage(target);
            _hits.Add(target);
        }
    }
}