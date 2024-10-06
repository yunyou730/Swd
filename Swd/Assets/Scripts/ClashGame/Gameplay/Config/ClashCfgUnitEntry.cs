using UnityEngine;
using System;

namespace clash.gameplay
{
    [Serializable,CreateAssetMenu(fileName = "ClashConfigUnitEntry", menuName = "ClashGame/ScriptableObjects/ClashConfigUnitEntry")]
    public class ClashCfgUnitEntry : ScriptableObject
    {
        public string Tag;
        public string PrefabPath;
        public float MovementSpeed;
    }
}