using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JsonEditor
{
    public class ExceptionFilter : MonoBehaviour
    {
        public static float TryFloatParse(string text)
        {
            float value;

            if (text[0] == '.')
                text = $"0{text}";

            if (float.TryParse(text, out value) == false)
            {
                //Todo : Editor Languege ��� �߰�
                Message.Show("���������� �� �Է�", "��Ȯ�� �Ҽ��� ���� �Է����ּ���.");
                return 0;
            }

            return value;
        }

        public static int TryIntParse(string text)
        {
            int value;

            if (text[0] == '.')
                text = $"0{text}";

            if (int.TryParse(text, out value) == false)
            {
                //Todo : Editor Languege ��� �߰�
                Message.Show("���������� �� �Է�", "��Ȯ�� �Ҽ��� ���� �Է����ּ���.");
                return 0;
            }

            return value;
        }
    }
}