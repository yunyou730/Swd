using Unity.VisualScripting;
using UnityEngine;

namespace clash.gameplay
{
    public class TileMapMeta : ClashBaseMetaInfo
    {
        public int Width;
        public int Height;
        private bool _dirtyFlag = false;

        private ETileTerrainType[,] _tilesTerrain = null;
        

        public TileMapMeta()
        {
            
        }

        public void Init(int width,int height)
        {
            Width = width;
            Height = height;
            //_tilesTerrain = new ETileTerrainType[Height,Width];
            _tilesTerrain = new ETileTerrainType[Width,Height];
        }

        public void SetTileTerrain(int tileX,int tileY,ETileTerrainType terrainType)
        {
            if (GetTileTerrain(tileX, tileY) != terrainType)
            {
                _tilesTerrain[tileX, tileY] = terrainType;
                _dirtyFlag = true;
            }
        }
        
        public ETileTerrainType GetTileTerrain(int tileX,int tileY)
        {
            return _tilesTerrain[tileX, tileY];
        }
        
        public bool IsDirty()
        {
            return _dirtyFlag;
        }

        public void ClearDirtyFlag()
        {
            _dirtyFlag = false;
        }

        public override void Dispose()
        {
            base.Dispose();
            _tilesTerrain = null;
            Debug.Log("TileMapWorldComponent::Dispose()");
        }
    }
}