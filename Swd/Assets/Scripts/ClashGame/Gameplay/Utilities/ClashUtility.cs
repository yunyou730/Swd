using UnityEngine;

namespace clash.gameplay.Utilities
{
    public class ClashUtility
    {
        public static Vector3 GetPositionAtTile(ClashWorld world,int tileX,int tileY)
        {
            var cfg = world.GetWorldComponent<ClashConfigWorldComponent>();
            
            Vector3 basePos = new Vector3(cfg.TileBaseX, 0, cfg.TileBaseZ);
            float x = (tileX - basePos.x) * cfg.TileSize;
            float z = (tileY - basePos.z) * cfg.TileSize;
            
            // Vector3 result = new Vector3(tileX,0,tileY);
            Vector3 result = new Vector3(x,0,z);
            return result;
        }
    }
}