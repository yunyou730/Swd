using UnityEngine;
using System;

namespace clash.gameplay
{
    [Serializable,CreateAssetMenu(fileName = "ClashTileMapConfig", menuName = "ClashGame/ScriptableObjects/ClashTileMap")]
    public class ClashTileMapConfig : ScriptableObject
    {
        public int GridWidth;
        public int GridHeight;
        public int[,] TileTypeMap = null;
        
        public ClashTileMapConfig()
        {
            GridWidth = 17;
            GridHeight = 30;
            TileTypeMap = new int[GridHeight, GridWidth];
        }
    }
}