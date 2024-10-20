using System.Collections.Generic;

namespace clash.gameplay
{
    public class CmdMeta : ClashBaseMetaInfo
    {
        public List<CmdBase> CmdList = new List<CmdBase>();
        
        public void Dispose()
        {
            base.Dispose();
            CmdList.Clear();
            CmdList = null;
        }
        
        public void AddCmdChangeTileTerrainType(int tileX,int tileY,ETileTerrainType terrainType)
        {
            var cmd = new CmdChangeTileTerrainType();
            cmd.TileX = tileX;
            cmd.TileY = tileY;
            cmd.TileType = terrainType;
            CmdList.Add(cmd);
        }

        public void AddCmdCreateUnitAtTile(int tileX,int tileY,string unitTag)
        {
            var cmd = new CmdCreateUnitAtTile();
            cmd.TileX = tileX;
            cmd.TileY = tileY;
            cmd.UnitTag = unitTag;
            CmdList.Add(cmd);
        }
    }
    
    public interface CmdBase
    {
        
    }
    
    public struct CmdChangeTileTerrainType : CmdBase
    {
        public int TileX;
        public int TileY;
        public ETileTerrainType TileType;
    }

    public struct CmdCreateUnitAtTile : CmdBase
    {
        public int TileX;
        public int TileY;
        public string UnitTag;
    }
}