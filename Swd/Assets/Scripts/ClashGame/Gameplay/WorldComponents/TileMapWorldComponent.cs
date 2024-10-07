using Unity.VisualScripting;
using UnityEngine;

namespace clash.gameplay
{
    public class TileMapMeta : ClashBaseMetaInfo
    {
        public int Width;
        public int Height;

        // [x,y] 该格是否能走
        private ETileWalkable[,] _tilesWalkable = null;


        public TileMapMeta(int width,int height)
        {
            Width = width;
            Height = height;
            _tilesWalkable = new ETileWalkable[Height,Width];
        }

        public void SetTileWalkable(int tileX,int tileY)
        {
                
        }
        
        public ETileWalkable GetTileWalkable(int tileX,int tileY)
        {
            
            return ETileWalkable.UnWalkable;
        }

        public override void Dispose()
        {
            base.Dispose();
            Debug.Log("TileMapWorldComponent::Dispose()");
        }
    }
}