using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Weapon : ObjectPoolInstance<Weapon>
    {
        Character _owner;

        Data.Object.Weapon _information;

        public void Initialize(Character owner, Data.Object.Weapon information)
        {
            _owner = owner;
            _information = information;

            StartCoroutine(CRun());
        }

        public void Initialize(Character owner, string key)
        {
            Initialize(owner, Data.GameData.instance.GetWeaponData(key));
        }

        IEnumerator CRun()
        {
            yield return new WaitForSeconds(Random.Range(0f, _information.interval));

            while (true)
            {
                if (!_owner.activated)
                    break;

                StartCoroutine(CFire());

                yield return new WaitForSeconds(_information.interval);
            }
        }

        IEnumerator CFire()
        {
            for (int j = 0; j < _information.continuousCount; j++)
            {
                if (!_owner.activated)
                    break;

                var direction = _owner.movement.lookingDirection - _information.angleRange * 0.5f;
                var directionAdd = _information.angleRange / (_information.fireCount - 1);

                for (int i = 0; i < _information.fireCount; i++)
                {
                    ObjectPool<Projectile>.GetObject().Initialize(_owner, _information.projectile, direction);
                    direction += directionAdd;
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}