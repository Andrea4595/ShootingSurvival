using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapon
{
    public class Explosion : ObjectPoolInstance<Explosion>
    {
        [SerializeField]
        Transform _body;
        [SerializeField]
        SpriteRenderer _sprite;

        Character.Character.Force _force;
        Data.Object.WeaponInformation.Projectile _information;

        List<Character.Hitable> _hits = new List<Character.Hitable>();

        private void Awake()
        {
            ObjectPoolInstanceInitialize(this);
        }

        public void Initialize(Character.Character owner, Data.Object.WeaponInformation.Projectile information, Vector3 position)
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
                _sprite.color = new Color(_sprite.color.r, _sprite.color.g, _sprite.color.b, alpha);

                yield return null;
            }

            Destroy();
        }

        void GiveDamage(Character.Hitable target)
        {
            target.Hit(_information.damage);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var target = collision.GetComponent<Character.Hitable>();

            if (target == null)
                return;

            if (target.force == _force)
                return;

            if (!target.canHit)
                return;

            if (_information.hitProjectile == false && target.type == Character.Hitable.Type.Projectile)
                return;

            GiveDamage(target);
            _hits.Add(target);
        }
    }
}