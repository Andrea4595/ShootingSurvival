using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class PlayerKeyBlock : MonoBehaviour
    {
        [SerializeField]
        Character _character;
        [SerializeField]
        TMPro.TMP_InputField _inputField;

        private void Awake()
        {
            StartCoroutine(CRun());
        }

        IEnumerator CRun()
        {
            var previous = _character.target;

            while (true)
            {
                yield return null;

                if (previous == _character.target)
                    continue;

                previous = _character.target;

                if (_character.target.key.CompareTo("player") == 0)
                {
                    _inputField.interactable = false;
                }
                else
                {
                    _inputField.interactable = true;
                }
            }
        }
    }
}