using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class TopMenu : MonoBehaviour
    {
        [SerializeField]
        GameObject[] _menus;

        public void Show(int index)
        {
            foreach (var menu in _menus)
                menu.SetActive(false);

            _menus[index].SetActive(true);
        }
    }
}