using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    [RequireComponent(typeof(Movement))]
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField]
        Movement _movement;

        private void OnEnable()
        {
            StartCoroutine(CRun());
        }

        IEnumerator CRun()
        {
            while (true)
            {
                yield return null;

                Move();
                LookAtMouse();
            }
        }

        void Move()
        {
            var inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (inputVector.x == 0 && inputVector.y == 0)
                return;

            _movement.Move(inputVector);
        }

        void LookAtMouse()
        {
            if (Time.timeScale == 0)
                return;

            _movement.LookAt(Cursor.position);
        }
    }
}