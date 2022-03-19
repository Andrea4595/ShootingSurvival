using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Weapon
{
    public class Weapon : ObjectPoolInstance<Weapon>
    {
        Character.Character _owner;

        public Data.Object.WeaponInformation information { get; set; }

        private void Awake()
        {
            ObjectPoolInstanceInitialize(this);
        }

        public void Initialize(Character.Character owner, Data.Object.WeaponInformation information)
        {
            _owner = owner;
            this.information = information;

            StartCoroutine(CRun());
        }

        public void Initialize(Character.Character owner, string key)
        {
            Initialize(owner, Data.GameData.instance.GetWeaponInformation(key));
        }

        IEnumerator CRun()
        {
            yield return Time.WaitForSeconds(Random.Range(0, information.interval));

            while (true)
            {
                if (!_owner.activated)
                    break;

                StartCoroutine(information.FireTypeCoroutine(_owner));

                yield return Time.WaitForSeconds(information.interval);
            }
        }
    }
}