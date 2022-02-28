using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cheat
{
    public class ShowStageUpgrade : Cheat
    {
        protected override void Activate() => UI.StageUpgradeSelector.instance.Show();
    }
}