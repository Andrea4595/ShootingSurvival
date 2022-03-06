using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Cursor : Singleton<Cursor>
    {
        [SerializeField]
        Camera _camera;

        public static Vector2 position
        {
            get
            {
                var camera = instance._camera;

                var mousePosition = Input.mousePosition;
                mousePosition *= 2 * camera.orthographicSize / Screen.height;
                mousePosition += camera.transform.position;

                var ratio = (float)Screen.width / Screen.height;
                mousePosition.x -= ratio * camera.orthographicSize;
                mousePosition.y -= camera.orthographicSize;

                return mousePosition;
            }
        }
    }
}