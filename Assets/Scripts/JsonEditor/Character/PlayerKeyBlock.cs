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
        UnityEngine.UI.Selectable _field;


        bool isPlayer => _character.target.key.CompareTo("player") == 0;

        private void Awake()
        {
            _character.onUpdate += CheckPlayer;

            AdvancedTool.instance.onEnable += SetEnable;
            AdvancedTool.instance.onDisable += SetDisable;
        }

        private void OnEnable() => CheckAdvancedEnable();


        void CheckAdvancedEnable()
        {
            if (AdvancedTool.instance.enable)
                SetEnable();
            else
                SetDisable();
        }

        void CheckPlayer()
        {
            if (isPlayer)
            {
                SetDisable();
                return;
            }

            CheckAdvancedEnable();
        }

        void SetEnable()
        {
            if (isPlayer)
            {
                SetDisable();
                return;
            }

            _field.interactable = true;
        }

        void SetDisable()
        {
            _field.interactable = false;
        }
    }
}