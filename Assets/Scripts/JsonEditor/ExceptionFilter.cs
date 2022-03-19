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
                //Todo : Editor Languege 기능 추가
                Message.Show("비정상적인 값 입력", "정확한 소숫점 값을 입력해주세요.");
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
                //Todo : Editor Languege 기능 추가
                Message.Show("비정상적인 값 입력", "정확한 소숫점 값을 입력해주세요.");
                return 0;
            }

            return value;
        }
    }
}