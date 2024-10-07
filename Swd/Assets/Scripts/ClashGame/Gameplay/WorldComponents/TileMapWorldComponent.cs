using Unity.VisualScripting;
using UnityEngine;

namespace clash.gameplay
{
    public class TileMapMeta : ClashBaseMetaInfo
    {
        public int Width;
        public int Height;

        
        private ETileTerrainType[,] _tilesTerrain = null;

        public TileMapMeta()
        {
            
        }

        public void Init(int width,int height)
        {
            Width = width;
            Height = height;
            _tilesTerrain = new ETileTerrainType[Height,Width];
        }

        public void SetTileTerrain(int tileX,int tileY)
        {
            
        }
        
        public ETileTerrainType GetTileTerrain(int tileX,int tileY)
        {
            
            return ETileTerrainType.Ground;
        }

        public override void Dispose()
        {
            base.Dispose();
            Debug.Log("TileMapWorldComponent::Dispose()");
        }
    }
}