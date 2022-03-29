using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameMath
    {
        public static float Homming(float original, float target, float homming)
        {
            void TurnLeft(bool clamp = false)
            {
                original += homming * Time.smoothDeltaTime;

                if (!clamp)
                    return;

                original = Mathf.Min(original, target);
            }
            void TurnRight(bool clamp = false)
            {
                original -= homming * Time.smoothDeltaTime;

                if (!clamp)
                    return;

                original = Mathf.Max(original, target);
            }

            while (target > 180)
                target -= 360;
            while (target <= -180)
                target += 360;

            while (original > 180)
                original -= 360;
            while (original <= -180)
                original += 360;

            if (original < target)
            {
                if (original + 180 > target)
                    TurnLeft(true);
                else
                    TurnRight();
            }
            else
            {
                if (original - 180 < target)
                    TurnRight(true);
                else
                    TurnLeft();
            }

            return original;
        }

        public static float Homming(float originalDirection, Vector2 originalVector, Vector2 target, float homming)
        {
            var targetVector = target - originalVector;
            var targetDirection = Mathf.Atan2(targetVector.y, targetVector.x) * Mathf.Rad2Deg;

            return Homming(originalDirection, targetDirection, homming);
        }

        public static Character.Character GetNearest(Character.Character.Force targetForce, Vector3 origin, float maxDistance)
        {
            var targets = StageSpawner.instance.Remains(targetForce);

            if (targets.Length <= 0)
                return null;

            Character.Character nearestTarget = null;
            var nearestDistance = Mathf.Infinity;

            foreach(var target in targets)
            {
                var distance = Vector3.Distance(origin, target.movement.position);
                
                if (distance > maxDistance)
                    continue;

                if (distance > nearestDistance)
                    continue;

                nearestDistance = distance;
                nearestTarget = target;
            }

            return nearestTarget;
        }
    }
}