using System.Collections;
using System.Collections.Generic;
using Data.Object;
using UnityEngine;

namespace Game.Weapon.FireType
{
    public class Random
    {
        public static IEnumerator CRun(Character.Character owner, WeaponInformation information)
        {
            for (int j = 0; j < information.continuousCount; j++)
            {
                var ownerDirection = owner.movement.lookingDirection;

                if (owner.activated == false)
                    break;

                for (int i = 0; i < information.fireCount; i++)
                {
                    var direction = ownerDirection - information.angleRange * 0.5f + UnityEngine.Random.Range(0f, information.angleRange);

                    ObjectPool<Projectile>.instance.GetObject().Initialize(owner, information.projectile, direction);
                }

                yield return Time.WaitForSeconds(0.1f);
            }
        }
    }
}