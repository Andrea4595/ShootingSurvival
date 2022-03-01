using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Character
{
    public class ChasingPlayer : MonoBehaviour
    {
        public void Initialize(Character owner)
        {
            owner.onDestroy += DestroySelf;

            StartCoroutine(CRun(owner));
        }

        IEnumerator CRun(Character owner)
        {
            var player = PlayerSetter.instance.player;

            while (owner.activated)
            {
                owner.movement.LookAt(player.movement.position);
                owner.movement.MoveTo(player.movement.position);

                yield return null;
            }
        }

        void DestroySelf()
        {
            Destroy(this);
        }
    }
}