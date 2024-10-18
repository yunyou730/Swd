﻿using System.Collections.Generic;

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
        
        public void AddCmdChangeEditSelectedTerrainType(ETileTerrainType? terrainType)
        {
            var cmd = new CmdChangeEditSelectedTerrainType();
            cmd.TileType = terrainType;
            CmdList.Add(cmd);
        }
    }


    public interface CmdBase
    {
        
    }
    
    public struct CmdChangeEditSelectedTerrainType : CmdBase
    {
        public ETileTerrainType? TileType;
    }
}