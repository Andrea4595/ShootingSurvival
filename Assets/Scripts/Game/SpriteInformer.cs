using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class SpriteInformer : Singleton<SpriteInformer>
    {
        public Sprite spriteBullet;
        public Sprite spriteMissile;

        public Sprite spriteFighter;
        public Sprite spriteBomber;

        private void Awake()
        {
            Initialize(this);
        }

        public static Sprite GetSprite(string name)
        {
            switch(name)
            {
                case "Bullet":
                    return instance.spriteBullet;
                case "Missile":
                    return instance.spriteMissile;
                case "Fighter":
                    return instance.spriteFighter;
                case "Bomber":
                    return instance.spriteBomber;
                default:
                    Debug.LogError($"no sprite named {name}");
                    return null;
            }
        }
    }
}