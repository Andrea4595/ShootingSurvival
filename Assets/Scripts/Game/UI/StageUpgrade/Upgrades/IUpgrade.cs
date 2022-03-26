using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.StageUpgrade
{
    public interface IUpgrade
    {
        Data.UpgradeInformation.StageUpgrades.Upgrade upgrade { get; }
        public string GetName();
        public string GetContent();
        public void Upgrade();
    }
}