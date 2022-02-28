using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cheat
{
    public abstract class Cheat : MonoBehaviour
    {
        [SerializeField]
        KeyCode[] _keys;

        private void OnEnable()
        {
            StartCoroutine(CCheckKeyPress());
        }

        IEnumerator CCheckKeyPress()
        {
            while (true)
            {
                yield return null;

                var keyDown = true;

                foreach (var key in _keys)
                    keyDown &= Input.GetKey(key);

                if (!keyDown)
                    continue;

                if (Input.GetKeyDown(_keys[_keys.Length - 1]) == false)
                    continue;
                
                Activate();
            }
        }

        protected abstract void Activate();
    }
}
