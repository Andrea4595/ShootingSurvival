using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Projectile : ObjectPoolInstance<Projectile>
    {
        [SerializeField]
        Transform _body;
        [SerializeField]
        SpriteRenderer _sprite;

        Character _owner;
        Data.Object.Weapon.Projectile _information;

        float _direction;

        public void Initialize(Character owner, Data.Object.Weapon.Projectile information, float direction)
        {
            _owner = owner;
            _information = information;
            _direction = direction;
            _sprite.sprite = SpriteInformer.GetSprite(information.sprite);
            _sprite.color = owner.color;

            StartCoroutine(CRun());
        }

        IEnumerator CRun()
        {
            var lifeTime = 0f;

            _body.position = _owner.movement.position;
            _body.localScale = Vector3.one * _information.scale;

            while (lifeTime < _information.lifetime)
            {
                yield return null;

                var moveSpeed = _information.speed * Time.smoothDeltaTime;
                var newPosition = new Vector3();
                newPosition.x = Mathf.Cos(_direction * Mathf.Deg2Rad) * moveSpeed;
                newPosition.y = Mathf.Sin(_direction * Mathf.Deg2Rad) * moveSpeed;

                _body.position += newPosition;
                _body.eulerAngles = new Vector3(0, 0, _direction);

                if (_information.homming > 0)
                    Homming();

                lifeTime += Time.smoothDeltaTime;
            }

            Destroy();
        }

        void Homming()
        {
            Character GetNearestTarget()
            {
                var targets = StageSpawner.instance.Remains(_owner.force);

                var minDistance = Mathf.Infinity;
                Character nearest = null;

                foreach (var target in targets)
                {
                    var distance = Vector2.Distance(_body.position, target.movement.position);

                    if (distance > minDistance)
                        continue;

                    minDistance = distance;
                    nearest = target;
                }

                return nearest;
            }

            void TurnLeft()
            {
                _direction += _information.homming * Time.smoothDeltaTime;
            }

            void TurnRight()
            {
                _direction -= _information.homming * Time.smoothDeltaTime;
            }

            var target = GetNearestTarget();

            if (target == null)
                return;

            var targetVector = target.movement.position - _body.position;
            var targetDirection = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;

            if (_direction < targetDirection)
            {
                if (_direction + 180 > targetDirection)
                    TurnLeft();
                else
                    TurnRight();
            }
            else
            {
                if (_direction - 180 < targetDirection)
                    TurnRight();
                else
                    TurnLeft();
            }

            while (_direction > 180)
                _direction -= 360;
            while (_direction <= -180)
                _direction += 360;
        }

        void GiveDamage(Character target)
        {
            target.health.TakeDamage(_information.damage);
        }

        void SpawnExplosion()
        {
            var explosion = ObjectPool<Explosion>.GetObject();
            explosion.Initialize(_owner, _information, _body.position);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var target = collision.GetComponent<Character>();

            if (target == null)
                return;

            if (target.force == _owner.force)
                return;

            if (_information.range == 0)
                GiveDamage(target);
            else
                SpawnExplosion();

            Destroy();
        }
    }
}