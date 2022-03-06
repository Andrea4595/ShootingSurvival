using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JsonEditor
{
    [ExecuteInEditMode]
    public class ContentHeightForceFitter : MonoBehaviour
    {
        [SerializeField]
        RectTransform _target;
        [SerializeField]
        List<RectTransform> _exceptions = new List<RectTransform>();

        float _previousHeight;

        private void Update()
        {
            if (_target == null)
                return;

            var previousHeight = _previousHeight;
            var height = 0f;
            for (var i = 0; i < _target.childCount; i++)
            {
                var child = _target.GetChild(i).GetComponent<RectTransform>();

                if (_exceptions.Contains(child))
                    continue;

                height += child.rect.height;
            }

            if (_previousHeight == height)
                return;

            _previousHeight = height;
            
            _target.sizeDelta = new Vector2(_target.sizeDelta.x, height);

            var layoutGroup = GetComponentInParent<VerticalLayoutGroup>();
            if (layoutGroup == null)
                return;
            layoutGroup.gameObject.SetActive(false);
            layoutGroup.gameObject.SetActive(true);
        }
    }
}