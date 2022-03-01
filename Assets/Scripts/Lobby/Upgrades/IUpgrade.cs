using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lobby.Upgrades
{
    public interface IUpgrade
    {
        public Data.UpgradeInformation.PermanentUpgrades.Upgrade information { get; }

        public string GetName();
        public string GetContent();
    }
}