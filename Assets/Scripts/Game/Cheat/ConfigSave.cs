using JsonEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Cheat
{
    public class ConfigSave : Cheat
    {
        protected override void Activate() => SaveJsonData.instance.SaveAll();
    }
}