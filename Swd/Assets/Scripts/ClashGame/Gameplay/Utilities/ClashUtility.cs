using UnityEngine;

namespace clash.gameplay.Utilities
{
    public class ClashUtility
    {
        public static Vector3 GetPositionAtTile(ClashWorld world,int tileX,int tileY)
        {
            var cfgMeta = world.GetWorldMeta<ClashConfigMeta>();
            
            Vector3 basePos = new Vector3(cfgMeta.TileBaseX, 0, cfgMeta.TileBaseZ);
            float x = (tileX - basePos.x) * cfgMeta.TileSize;
            float z = (tileY - basePos.z) * cfgMeta.TileSize;
            
            // Vector3 result = new Vector3(tileX,0,tileY);
            Vector3 result = new Vector3(x,0,z);
            return result;
        }
        
        public static void WorldPositionToTileCoordinate(ClashWorld world,Vector3 positionInWorld,out int tileX,out int tileY)
        {
            float x = positionInWorld.x;
            float y = positionInWorld.z;
            
            var cfgMeta = world.GetWorldMeta<ClashConfigMeta>();
            Vector3 basePos = new Vector3(cfgMeta.TileBaseX, 0, cfgMeta.TileBaseZ);
            Vector3 originPos = new Vector3(basePos.x - cfgMeta.TileSize * 0.5f,0,basePos.z - cfgMeta.TileSize * 0.5f);

            tileX = (int)((x - originPos.x) / cfgMeta.TileSize);
            tileY = (int)((y - originPos.z) / cfgMeta.TileSize);
        }

    }
}