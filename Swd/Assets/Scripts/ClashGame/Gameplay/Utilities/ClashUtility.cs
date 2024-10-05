using UnityEngine;

namespace clash.gameplay.Utilities
{
    public class ClashUtility
    {
        public static Vector3 GetPositionAtTile(int tileX,int tileY)
        {
            Vector3 result = new Vector3(tileX,0,tileY);
            return result;
        }
        
        
        
    }
}