using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cheat
{
    public class KillAll : Cheat
    {
        protected override void Activate()
        {
            var remains = StageSpawner.instance.Remains(Character.Character.Force.Enemy);

            foreach (var remain in remains)
                remain.Destroy();
        }
    }
}