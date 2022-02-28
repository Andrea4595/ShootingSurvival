using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.UI.StageUpgrade
{
    public class StageUpgradeChoice : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI _name;
        [SerializeField]
        TextMeshProUGUI _content;

        public event Action onSelect;

        public void Initialize(IUpgradeInformation information)
        {
            _name.text = information.GetName();
            _content.text = information.GetContent();
            onSelect += information.Upgrade;
        }

        public void Select()
        {
            onSelect?.Invoke();
            StageUpgradeSelector.instance.Close();
        }
    }
}