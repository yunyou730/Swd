using System;
using System.Collections.Generic;
using clash.gameplay;
using UnityEngine;

public delegate void HandleCmdDelegate(CmdBase cmd);

public class CmdSystem : ClashBaseSystem,IStartSystem,ITickSystem
{
    private ClashWorld _clashWorld = null;
    private CmdMeta _cmdMeta = null;

    private TileEditMeta _tileEditMeta = null;

    private Dictionary<Type, HandleCmdDelegate> _handleCmdFuncDict = null;

    public CmdSystem(ClashBaseWorld world) : base(world)
    {
        _clashWorld = GetWorld<ClashWorld>();
        _cmdMeta = _clashWorld.GetWorldMeta<CmdMeta>();
        _tileEditMeta = _clashWorld.GetWorldMeta<TileEditMeta>();
        
        _handleCmdFuncDict = new Dictionary<Type, HandleCmdDelegate>();
        _handleCmdFuncDict.Add(typeof(CmdChangeEditSelectedTerrainType),OnCmdChangeEditSelectedTileTerrainType);
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

    private void OnCmdChangeEditSelectedTileTerrainType(CmdBase cmd)
    {
        var castedCmd = (CmdChangeEditSelectedTerrainType)cmd;
        _tileEditMeta.SelectedTerrainType = castedCmd.TileType;
    }
}