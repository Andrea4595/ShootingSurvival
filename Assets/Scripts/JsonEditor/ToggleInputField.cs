using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JsonEditor
{
    public class ToggleInputField : MonoBehaviour
    {
        [SerializeField]
        GameObject _button;
        [SerializeField]
        TMPro.TMP_InputField _inputField;
        [SerializeField]
        float defaultValue = 1;

        public UnityEvent<float> onValueChanged;

        private void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            _inputField.onEndEdit.AddListener(ValueChanged);
        }

        void ValueChanged(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);

            ShowToggle(value <= 0);

            onValueChanged?.Invoke(value);
        }

        public void SetValueWithoutNotify(float value)
        {
            _inputField.text = value.ToString();

            ShowToggle(value <= 0);
        }

        public void ButtonPressed()
        {
            _inputField.text = defaultValue.ToString();

            ShowToggle(false);

            onValueChanged?.Invoke(defaultValue);
        }

        public void ShowToggle(bool toggleActive)
        {
            _button.SetActive(toggleActive);
            _inputField.gameObject.SetActive(!toggleActive);
        }
    }
}