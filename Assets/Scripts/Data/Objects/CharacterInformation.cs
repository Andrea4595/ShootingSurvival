using UnityEngine;

namespace Data.Object
{
    [System.Serializable]
    public class CharacterInformation
    {
        public string key = "newCharacter";
        public string sprite = "Fighter";
        public float[] color = { 1, 1, 1};
        public float scale = 1;
        public float maxHp = 10;
        public float moveSpeed = 5;
        public float homming = 1000;
        public string[] weapons = new string[0];

        public Sprite GetSprite() => SpriteInformer.GetSprite(sprite);
        public Color GetColor() => new Color(color[0], color[1], color[2]);

        public CharacterInformation Clone()
        {
            var clone = new CharacterInformation();
            clone.key = key;
            clone.sprite = sprite;
            clone.color = new float[]{ color[0], color[1], color[2] };
            clone.scale = scale;
            clone.maxHp = maxHp;
            clone.moveSpeed = moveSpeed;
            clone.homming = homming;
            clone.weapons = new string[weapons.Length];
            for (var i = 0; i < weapons.Length; i++)
                clone.weapons[i] = (string)weapons[i].Clone();

            return clone;
        }
    }
}