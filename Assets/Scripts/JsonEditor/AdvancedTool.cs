using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class AdvancedTool : Singleton<AdvancedTool>
    {
        bool _enable;
        public bool enable
        {
            get => _enable;
            set
            {
                if (_enable == value)
                    return;

                _enable = value;

                if (_enable)
                    onEnable?.Invoke();
                else
                    onDisable?.Invoke();
            }
        }

        public event Action onEnable;
        public event Action onDisable;

        [SerializeField]
        UnityEngine.UI.Toggle _toggle;

        public void SetEnable(bool enable) => this.enable = enable;

        private void Awake() => _toggle.onValueChanged.AddListener(SetEnable);
    }
}