using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class AdvancedContent : MonoBehaviour
    {
        [SerializeField]
        UnityEngine.UI.Selectable _field;

        private void Awake()
        {
            AdvancedTool.instance.onEnable += SetEnable;
            AdvancedTool.instance.onDisable += SetDisable;
        }

        private void OnDestroy()
        {
            AdvancedTool.instance.onEnable -= SetEnable;
            AdvancedTool.instance.onDisable -= SetDisable;
        }

        private void OnEnable()
        {
            if (AdvancedTool.instance.enable)
                SetEnable();
            else
                SetDisable();
        }

        void SetEnable()
        {
            _field.interactable = true;
        }

        void SetDisable()
        {
            _field.interactable = false;
        }
    }
}