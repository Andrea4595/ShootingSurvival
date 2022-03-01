using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cheat
{
    public class ClearSave : Cheat
    {
        protected override void Activate()
        {
            Data.GameData.instance.playerData.ClearSave();
            Lobby.PermanentUpgradeGenerator.instance.UpdateInterface();
        }
    }
}