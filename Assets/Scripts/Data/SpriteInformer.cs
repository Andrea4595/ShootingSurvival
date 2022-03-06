using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public class SpriteInformer : Singleton<SpriteInformer>
    {
        [Serializable]
        public class SpriteKeyPair
        {
            public string key;
            public Sprite sprite;

            public SpriteKeyPair(string key, Sprite sprite)
            {
                this.key = key;
                this.sprite = sprite;
            }
        }

        public List<SpriteKeyPair> sprites = new List<SpriteKeyPair>();

        public static Sprite GetSprite(string key)
        {
            foreach (var sprite in instance.sprites)
                if (sprite.key.CompareTo(key) == 0)
                    return sprite.sprite;

            return null;
        }

        public static int GetSpriteIndex(string key)
        {
            for (var i = 0; i < instance.sprites.Count; i++)
                if (instance.sprites[i].key.CompareTo(key) == 0)
                    return i;

            return -1;
        }
    }
}