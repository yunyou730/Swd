using System;
using System.Collections.Generic;
using clash.gameplay;
using clash.gameplay.Utilities;
using UnityEngine;

public delegate void HandleCmdDelegate(CmdBase cmd);

public class CmdSystem : ClashBaseSystem,IStartSystem,ITickSystem
{
    private ClashWorld _clashWorld = null;
    private CmdMeta _cmdMeta = null;
    private TileMapMeta _tileMapMeta = null;

    private Dictionary<Type, HandleCmdDelegate> _handleCmdFuncDict = null;

    public CmdSystem(ClashBaseWorld world) : base(world)
    {
        _clashWorld = GetWorld<ClashWorld>();
        _cmdMeta = _clashWorld.GetWorldMeta<CmdMeta>();
        _tileMapMeta = _clashWorld.GetWorldMeta<TileMapMeta>();
        // _tileEditMeta = _clashWorld.GetWorldMeta<TileEditMeta>();
        
        _handleCmdFuncDict = new Dictionary<Type, HandleCmdDelegate>();
        _handleCmdFuncDict.Add(typeof(CmdChangeTileTerrainType),OnCmdChangeTileTerrainType);
        _handleCmdFuncDict.Add(typeof(CmdCreateUnitAtTile),OnCmdCreateUnitAtTile);
    }

    public void OnStart()
    {
        
    }

    public void OnTick(int frameIndex)
    {
        foreach (var cmd in _cmdMeta.CmdList)
        {
            _handleCmdFuncDict[cmd.GetType()](cmd);
        }
        _cmdMeta.CmdList.Clear();
    }
    
    public override void Dispose()
    {
        _handleCmdFuncDict.Clear();
        _handleCmdFuncDict = null;
    }

    private void OnCmdChangeTileTerrainType(CmdBase cmd)
    {
        var castedCmd = (CmdChangeTileTerrainType)cmd;
        _tileMapMeta.SetTileTerrain(castedCmd.TileX,castedCmd.TileY,castedCmd.TileType);
    }
    
    private void OnCmdCreateUnitAtTile(CmdBase cmd)
    {
        var castedCmd = (CmdCreateUnitAtTile)cmd;
        ClashUtility.CreateUnitAtTile(_clashWorld,castedCmd.UnitTag,castedCmd.TileX,castedCmd.TileY);
    }
}