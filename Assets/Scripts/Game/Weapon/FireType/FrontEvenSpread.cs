using System.Collections;
using System.Collections.Generic;
using Data.Object;
using UnityEngine;

namespace Game.Weapon.FireType
{
    public class FrontEvenSpread
    {
        public static IEnumerator CRun(Character.Character owner, WeaponInformation information)
        {
            for (int j = 0; j < information.continuousCount; j++)
            {
                if (owner.activated == false)
                    break;

                var direction = owner.movement.lookingDirection - information.angleRange * 0.5f;
                var angleDistance = information.angleRange / (information.fireCount + 1);

                for (int i = 0; i < information.fireCount; i++)
                {
                    direction += angleDistance;

                    ObjectPool<Projectile>.instance.GetObject().Initialize(owner, information.projectile, direction);
                }

                yield return Time.WaitForSeconds(0.1f);
            }
        }
    }
}