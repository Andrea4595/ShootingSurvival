using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class Tip : Singleton<Tip>
    {
        [SerializeField]
        TMPro.TextMeshProUGUI _title;
        [SerializeField]
        TMPro.TextMeshProUGUI _information;

        public static void Show(string title, string information)
        {
            instance.UpdateText(title, information);
        }

        void UpdateText(string title, string information)
        {
            _title.text = title;
            _information.text = information;
        }
    }
}