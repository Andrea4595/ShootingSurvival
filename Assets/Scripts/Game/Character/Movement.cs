using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Character))]
    public class Movement : MonoBehaviour
    {
        [SerializeField]
        Transform _body;

        public float moveSpeed { get; set; }

        public float lookingDirection => _body.eulerAngles.z;
        public Vector3 position => _body.position;

        public void SetPosition(Vector3 position)
        {
            _body.position = position;
        }

        public void SetScale(float scale)
        {
            _body.localScale = new Vector3(scale, scale, 1);
        }

        public void Move(Vector3 direction)
        {
            var vector = Vector2.ClampMagnitude(direction, 1);
            var destination = vector * moveSpeed * Time.smoothDeltaTime;

            _body.position = new Vector3(_body.position.x + destination.x, _body.position.y + destination.y);
        }

        public void Move(float angle)
        {
            var angleRadian = angle * Mathf.Deg2Rad;
            var direction = new Vector2(Mathf.Cos(angleRadian), Mathf.Sin(angleRadian));
            Move(direction);
        }

        public void MoveTo(Vector3 to)
        {
            var vector = Vector2.ClampMagnitude(to - _body.position, 1);
            vector *= moveSpeed * Time.smoothDeltaTime;

            _body.position = new Vector3(_body.position.x + vector.x, _body.position.y + vector.y, 0);
        }

        public void LookAt(Vector3 target)
        {
            var vector = target - _body.position;
            _body.eulerAngles = new Vector3(0, 0, Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg);
        }
    }
}