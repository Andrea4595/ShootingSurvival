using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapon
{
    public class Projectile : ObjectPoolInstance<Projectile>
    {
        public Data.Object.WeaponInformation.Projectile information { get; private set; }

        [SerializeField]
        Transform _body;
        [SerializeField]
        SpriteRenderer _sprite;
        [SerializeField]
        Character.Hitable _hitable;
        
        public Health health;

        Character.Character _owner;

        float _direction;

        Coroutine _run;

        bool explosion => information.range > 0;

        private void Awake()
        {
            ObjectPoolInstanceInitialize(this);
            health.onDie += () => Destroy();
        }

        public void Initialize(Character.Character owner, Data.Object.WeaponInformation.Projectile information, float direction, Vector2 offset)
        {
            _owner = owner;
            this.information = information;
            _direction = direction;
            _sprite.sprite = Data.SpriteInformer.GetSprite(information.sprite);
            _sprite.color = owner.color;
            _hitable.Initialize(owner.force, Character.Hitable.Type.Projectile);

            health.Initialize(information.maxHp);

            if (_run != null)
                StopCoroutine(_run);

            _run = StartCoroutine(CRun(offset));
        }

        public void Initialize(Character.Character owner, Data.Object.WeaponInformation.Projectile information, float direction) => Initialize(owner, information, direction, Vector2.zero);

        IEnumerator CRun(Vector2 initializeOffset)
        {
            var lifeTime = 0f;
            var position = _owner.movement.position + GetDirectionOffset(_direction, initializeOffset);

            _body.position = position;
            _body.localScale = Vector3.one * information.scale;

            while (lifeTime < information.lifetime)
            {
                yield return null;

                var moveSpeed = information.speed * Time.smoothDeltaTime;
                var newPosition = new Vector3();
                newPosition.x = Mathf.Cos(_direction * Mathf.Deg2Rad) * moveSpeed;
                newPosition.y = Mathf.Sin(_direction * Mathf.Deg2Rad) * moveSpeed;

                _body.position += newPosition;
                _body.eulerAngles = new Vector3(0, 0, _direction);

                if (information.homming > 0)
                    Homming();

                lifeTime += Time.smoothDeltaTime;
            }

            Destroy();
        }

        Vector3 GetDirectionOffset(float direction, Vector2 offset)
        {
            var result = Vector3.zero;

            result.x += Mathf.Cos(direction * Mathf.Deg2Rad) * Mathf.Rad2Deg * offset.x;
            result.y += Mathf.Sin(direction * Mathf.Deg2Rad) * Mathf.Rad2Deg * offset.x;

            result.x += Mathf.Cos((direction + 90) * Mathf.Deg2Rad) * Mathf.Rad2Deg * offset.y;
            result.y += Mathf.Sin((direction + 90) * Mathf.Deg2Rad) * Mathf.Rad2Deg * offset.y;

            return result;
        }

        void Homming()
        {
            Character.Character GetNearestTarget()
            {
                var targets = StageSpawner.instance.TargetRemains(_owner.force);

                var minDistance = Mathf.Infinity;
                Character.Character nearest = null;

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
                _direction += information.homming * Time.smoothDeltaTime;
            }

            void TurnRight()
            {
                _direction -= information.homming * Time.smoothDeltaTime;
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

        void GiveDamage(Character.Hitable target)
        {
            target.Hit(information.damage);
        }

        void SpawnExplosion()
        {
            var explosion = ObjectPool<Explosion>.instance.GetObject();
            explosion.Initialize(_owner, information, _body.position);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var target = collision.GetComponent<Character.Hitable>();

            if (target == null)
                return;

            if (target.force == _owner.force)
                return;

            if (!target.canHit)
                return;

            if (information.hitProjectile == false && target.type == Character.Hitable.Type.Projectile)
                return;

            if (!explosion)
                GiveDamage(target);

            Destroy();
        }

        new void Destroy()
        {
            if (explosion)
                SpawnExplosion();

            base.Destroy();
        }
    }
}