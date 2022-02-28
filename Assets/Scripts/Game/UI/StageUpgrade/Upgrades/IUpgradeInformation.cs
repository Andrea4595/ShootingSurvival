using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.StageUpgrade
{
    public interface IUpgradeInformation
    {
        public string GetName();
        public string GetContent();
        public void Upgrade();
    }
}