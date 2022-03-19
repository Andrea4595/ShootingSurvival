using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JsonEditor
{
    public class ToggleInputField : MonoBehaviour
    {
        [SerializeField]
        UnityEngine.UI.Button _button;
        [SerializeField]
        TMPro.TMP_InputField _inputField;
        [SerializeField]
        float defaultValue = 1;

        public UnityEvent<float> onValueChanged;
        public UnityEvent onSelect;

        private void Awake()
        {
            void Initialize()
            {
                void ValueChanged(string text)
                {
                    var value = ExceptionFilter.TryFloatParse(text);

                    ShowToggle(value <= 0);
                    onValueChanged?.Invoke(value);
                }

                void OnSelect(string v) => onSelect?.Invoke();

                _inputField.onEndEdit.AddListener(ValueChanged);
                _inputField.onSelect.AddListener(OnSelect);
            }

            Initialize();
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

            onSelect?.Invoke();
            onValueChanged?.Invoke(defaultValue);
        }

        public void ShowToggle(bool toggleActive)
        {
            _button.gameObject.SetActive(toggleActive);
            _inputField.gameObject.SetActive(!toggleActive);
        }
    }
}