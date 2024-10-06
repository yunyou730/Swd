using UnityEngine;
using System;
using System.Collections.Generic;

namespace clash.gameplay
{
    [Serializable,CreateAssetMenu(fileName = "ClashConfigUnits", menuName = "ClashGame/ScriptableObjects/ClashConfigUnits")]
    public class ClashCfgUnits : ScriptableObject
    {
        public Dictionary<string, string> units;
    }
}