using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class PlayerSetter : Singleton<PlayerSetter>
    {
        public Character.Character player;

        private void Awake()
        {
            Initialize(this);
            SetPlayer();
        }

        void SetPlayer()
        {
            player.Initialize("player", Character.Character.Force.Player);
        }
    }
}