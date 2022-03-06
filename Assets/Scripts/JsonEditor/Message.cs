using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class Message : Singleton<Message>
    {
        [Serializable]
        public class Information
        {
            public string title;
            public string content;
            public enum Type { Ok, OkCancel }
            public Type type;
            public List<Action> onOk = new List<Action>();
            public List<Action> onCancel = new List<Action>();
        }

        static Queue<Information> _messageQueue = new Queue<Information>();

        public static void Show(string title, string content, Information.Type type = Information.Type.Ok, Action onOk = null, Action onCancel = null)
        {
            var information = new Information();
            information.title = title;
            information.content = content;
            information.type = type;

            if (onOk != null)
                information.onOk.Add(onOk);
            if (onCancel != null)
                information.onCancel.Add(onCancel);

            Show(information);
        }

        public static void Show(Information messageQueue)
        {
            _messageQueue.Enqueue(messageQueue);
            CheckQueue();
        }

        [SerializeField]
        TMPro.TextMeshProUGUI _title;
        [SerializeField]
        TMPro.TextMeshProUGUI _content;
        [SerializeField]
        GameObject _ok;
        [SerializeField]
        GameObject _okCancel;

        Information _information;

        event Action _onOk;
        event Action _onCancel;

        static void CheckQueue()
        {
            if (instance.gameObject.activeSelf == true)
                return;

            if (_messageQueue.Count <= 0)
                return;

            instance.ShowMessage(_messageQueue.Dequeue());
        }

        void ShowMessage(Information information)
        {
            gameObject.SetActive(true);

            if (_information != null)
            {
                foreach (var action in _information.onOk)
                    _onOk -= action;
                foreach (var action in _information.onCancel)
                    _onCancel -= action;
            }

            _information = information;

            _title.text = information.title;
            _content.text = information.content;

            _ok.SetActive(information.type == Information.Type.Ok);
            _okCancel.SetActive(information.type == Information.Type.OkCancel);

            foreach (var action in _information.onOk)
                _onOk += action;
            foreach (var action in _information.onCancel)
                _onCancel += action;
        }

        public void PressOk()
        {
            _onOk?.Invoke();

            gameObject.SetActive(false);
            CheckQueue();
        }

        public void PressCancel()
        {
            _onCancel?.Invoke();

            gameObject.SetActive(false);
            CheckQueue();
        }
    }
}