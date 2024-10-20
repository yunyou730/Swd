using System;
using clash.gameplay;

namespace clash.Event
{
    public class ClashEventManager
    {
        public event EventHandler<SelectTileTerrainTileEventArgs> EventChangeTileTerrainType;
        
        public void Invoke_EventChangeTileTerrainType(ETileTerrainType? tileTerrainType)
        {
            var arg = new SelectTileTerrainTileEventArgs();
            arg.TileType = tileTerrainType;
            EventChangeTileTerrainType?.Invoke(null,arg);
        }
    }


    public class SelectTileTerrainTileEventArgs : EventArgs
    {
        public ETileTerrainType? TileType = null;
    }
    
    
    
}