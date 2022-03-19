using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace JsonEditor
{
    public class SliderWithInput : MonoBehaviour
    {
        [SerializeField]
        Slider _slider;
        [SerializeField]
        TMPro.TMP_InputField _input;
        [SerializeField]
        float _scaleSliderMultiply = 1;

        public UnityEvent<float> onValueChanged;

        private void Awake()
        {
            Initialize();
        }

        void Initialize()
        {
            _slider.onValueChanged.AddListener(SliderValueChange);
            _input.onValueChanged.AddListener(InputFieldValueChange);
        }

        void SliderValueChange(float value)
        {
            value /= _scaleSliderMultiply;
            var text = value.ToString();
            _input.SetTextWithoutNotify(text);

            onValueChanged?.Invoke(value);
        }

        void InputFieldValueChange(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);
            _slider.SetValueWithoutNotify(value * _scaleSliderMultiply);

            onValueChanged?.Invoke(value);
        }

        public void SetValueWithoutNotify(float value)
        {
            _slider.SetValueWithoutNotify(value * _scaleSliderMultiply);
            _input.SetTextWithoutNotify(value.ToString());
        }
    }
}