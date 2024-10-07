using System.Collections.Generic;
using UnityEngine;

namespace clash.gameplay
{
    public class UnitFactoryMeta : ClashBaseMetaInfo
    {
        public List<UnitGenerateData> Datas = new List<UnitGenerateData>();
        
        public override void Dispose()
        {
            base.Dispose();
            Debug.Log("UnitFactoryWorldComp::Dispose()");
        }
    }

    public struct UnitGenerateData
    {
        public int TileX;
        public int TileY;
        public string UnitTag;

        public UnitGenerateData(string unitTag,int tileX,int tileY)
        {
            UnitTag = unitTag;
            TileX = tileX;
            TileY = tileY;
        }
    }
}