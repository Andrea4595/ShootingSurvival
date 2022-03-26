using System.Collections;
using System.Collections.Generic;
using Data.Object;
using UnityEngine;

namespace Game.Weapon.FireType
{
    public class FrontArrow
    {
        static bool IsEven(float value) => value % 2 == 0;

        public static IEnumerator CRun(Character.Character owner, WeaponInformation information)
        {
            for (int j = 0; j < information.continuousCount; j++)
            {
                if (owner.activated == false)
                    break;

                var direction = owner.movement.lookingDirection;
                var offsetInterval = information.angleRange * 0.01f;
                var offset = new Vector2(offsetInterval * (information.fireCount - 1) * 0.25f, 0);

                if (IsEven(information.fireCount))
                    offset.y += offsetInterval * 0.5f;

                for (int i = 0; i < information.fireCount; i++)
                {
                    ObjectPool<Projectile>.instance.GetObject().Initialize(owner, information.projectile, direction, offset);

                    if (IsEven(i) != IsEven(information.fireCount))
                    {
                        offset.x -= offsetInterval * 0.5f;
                        offset.y = Mathf.Abs(offset.y) + offsetInterval;
                    }
                    else
                    {
                        offset.y *= -1;
                    }
                }

                yield return Time.WaitForSeconds(0.1f);
            }
        }
    }
}