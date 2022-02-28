using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapon
{
    public class Weapon : ObjectPoolInstance<Weapon>
    {
        Character.Character _owner;

        Data.Object.WeaponInformation _information;

        private void Awake()
        {
            ObjectPoolInstanceInitialize(this);
        }

        public void Initialize(Character.Character owner, Data.Object.WeaponInformation information)
        {
            _owner = owner;
            _information = information;

            StartCoroutine(CRun());
        }

        public void Initialize(Character.Character owner, string key)
        {
            Initialize(owner, Data.GameData.instance.GetWeaponData(key));
        }

        IEnumerator CRun()
        {
            yield return Time.WaitForSeconds(Random.Range(0, _information.interval));

            while (true)
            {
                if (!_owner.activated)
                    break;

                StartCoroutine(_information.FireTypeCoroutine(_owner));

                yield return Time.WaitForSeconds(_information.interval);
            }
        }
    }
}