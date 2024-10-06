using System;
using UnityEngine;

namespace clash.gameplay
{
    [Serializable,CreateAssetMenu(fileName = "ClashGameConfig", menuName = "ClashGame/ScriptableObjects/ClashGameConfig")]
    public class ClashConfig : ScriptableObject
    {
        public float kTileSize = 1.0f;
        public float kTileBaseX = 0;
        public float kTileBaseZ = 0;
        
        public int kLogicFPS = 16;
    }
}