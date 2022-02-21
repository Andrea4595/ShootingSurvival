using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ClampPosition : MonoBehaviour
    {
        private void OnEnable()
        {
            StartCoroutine(CRun());
        }

        IEnumerator CRun()
        {
            while (true)
            {
                Clamp();
                yield return null;
            }
        }

        void Clamp()
        {
            var newPosition = transform.position;
            var widthClamp = Camera.main.orthographicSize * Screen.width / Screen.height;
            var heightClamp = Camera.main.orthographicSize;
            newPosition.x = Mathf.Clamp(newPosition.x, -widthClamp, widthClamp);
            newPosition.y = Mathf.Clamp(newPosition.y, -heightClamp, heightClamp);
            transform.position = newPosition;
        }
    }
}