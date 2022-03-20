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

                owner.movement.LookAtDirection(GameMath.Homming(owner.movement.lookingDirection, owner.movement.position, player.movement.position, owner.information.homming));
                owner.movement.Move(owner.movement.lookingDirection);

                yield return null;
            }
        }

        void DestroySelf()
        {
            Destroy(this);
        }
    }
}