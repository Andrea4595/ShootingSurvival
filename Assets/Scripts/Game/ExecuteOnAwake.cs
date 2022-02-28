using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class ExecuteOnAwake : MonoBehaviour
    {
        [SerializeField]
        UnityEvent _event;

        private void Awake() => _event?.Invoke();
    }
}