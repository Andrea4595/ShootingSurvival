using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    [RequireComponent(typeof(Character))]
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField]
        Character _character;
        [SerializeField]
        float _autoAimDistance;

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
                LookAtNeareestTargetOnAim();
            }
        }

        void Move()
        {
            var inputVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (inputVector.x == 0 && inputVector.y == 0)
                return;

            _character.movement.Move(inputVector);
        }

        void LookAtNeareestTargetOnAim()
        {
            if (Time.timeScale == 0)
                return;

            var nearest = GameMath.GetNearest(Character.Force.Enemy, Cursor.position, _autoAimDistance);

            if (nearest != null)
            {
                var newDirection = GameMath.Homming(_character.movement.lookingDirection, _character.movement.position, nearest.movement.position, _character.information.homming);
                _character.movement.LookAtDirection(newDirection);
            }
            else
            {
                var newDirection = GameMath.Homming(_character.movement.lookingDirection, _character.movement.position, Cursor.position, _character.information.homming);
                _character.movement.LookAtDirection(newDirection);
            }
        }
    }
}