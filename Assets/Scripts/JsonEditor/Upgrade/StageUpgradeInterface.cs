using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class StageUpgradeInterface : MonoBehaviour
    {
        [SerializeField]
        StageUpgradeList _stageUpgradeList;
        [SerializeField]
        StageUpgradeLevelList _levelList;

        [SerializeField]
        TMPro.TextMeshProUGUI _title;
        [SerializeField]
        GameObject _singlePower;
        [SerializeField]
        TMPro.TMP_InputField _power;
        [SerializeField]
        TMPro.TMP_InputField _weight;

        enum Type { Upgrade, Weapon }
        Type _type;

        Data.UpgradeInformation.StageUpgrades.Upgrade _upgrade;
        Data.Object.WeaponInformation _weapon;

        private void Awake() => Initialize();

        void Initialize()
        {
            _weight.onEndEdit.AddListener(UpdateWeight);
            _power.onEndEdit.AddListener(UpdatePower);
        }

        public void UpdateInterface(Data.UpgradeInformation.StageUpgrades.Upgrade upgrade, string titleText, bool singleLevel = false)
        {
            gameObject.SetActive(true);

            _type = Type.Upgrade;
            _upgrade = upgrade;
            _title.text = titleText;

            _weight.SetTextWithoutNotify(upgrade.weight.ToString());

            if (singleLevel)
            {
                _singlePower.SetActive(true);
                _power.SetTextWithoutNotify(upgrade.power[0].ToString());
                _levelList.Hide();
            }
            else
            {
                _singlePower.SetActive(false);
                _levelList.UpdateInterface(upgrade);
            }
        }

        public void UpdateInterface(Data.Object.WeaponInformation weapon, string titleText)
        {
            gameObject.SetActive(true);

            _type = Type.Weapon;
            _weapon = weapon;
            _title.text = titleText;

            _weight.SetTextWithoutNotify(weapon.weight.ToString());
            _levelList.Hide();
            _singlePower.gameObject.SetActive(false);
        }

        public void Hide() => gameObject.SetActive(false);

        void UpdateWeight(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);

            switch(_type)
            {
                case Type.Upgrade:
                    _upgrade.weight = value;
                    break;
                case Type.Weapon:
                    _weapon.weight = value;
                    break;
            }

            _stageUpgradeList.UpdateInterface();
        }

        void UpdatePower(string text)
        {
            var value = ExceptionFilter.TryFloatParse(text);
            _upgrade.power[0] = value;
        }
    }
}