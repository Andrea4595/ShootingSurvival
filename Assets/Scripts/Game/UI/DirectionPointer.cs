using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI
{
    public class DirectionPointer : MonoBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(CRun());
        }

        IEnumerator CRun()
        {
            while(true)
            {
                yield return null;

                var position = PlayerSetter.instance.player.movement.position;

                transform.position = position;

                var cursor = (Vector3)Cursor.position;
                var vector = cursor - position;
                var direction = Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;

                transform.eulerAngles = new Vector3(0, 0, direction);

                var distance = Vector2.Distance(position, cursor);
                transform.localScale = new Vector3(distance, 1, 1);
            }
        }
    }
}