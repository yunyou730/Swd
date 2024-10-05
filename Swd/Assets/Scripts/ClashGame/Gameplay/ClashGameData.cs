using System;
using UnityEngine;

namespace clash.gameplay
{
    [Serializable,CreateAssetMenu(fileName = "ClashGameData", menuName = "ClashGame/ScriptableObjects/ClashGameData")]
    public class ClashGameData : ScriptableObject
    {
        public string SceneName;
        public int GridWidth;
        public int GridHeight;
        public bool EnableDebugGrid;
    }
}