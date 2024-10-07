using UnityEngine;

namespace clash.gameplay.Utilities
{
    public class ClashUtility
    {
        public static Vector3 GetPositionAtTile(ClashWorld world,int tileX,int tileY)
        {
            var cfgMeta = world.GetWorldComponent<ClashConfigMeta>();
            
            Vector3 basePos = new Vector3(cfgMeta.TileBaseX, 0, cfgMeta.TileBaseZ);
            float x = (tileX - basePos.x) * cfgMeta.TileSize;
            float z = (tileY - basePos.z) * cfgMeta.TileSize;
            
            // Vector3 result = new Vector3(tileX,0,tileY);
            Vector3 result = new Vector3(x,0,z);
            return result;
        }
    }
}