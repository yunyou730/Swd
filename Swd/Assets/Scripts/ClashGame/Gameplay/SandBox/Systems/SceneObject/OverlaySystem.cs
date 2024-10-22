using System.Collections.Generic;
using clash.gameplay.GameObject;
using clash.gameplay.Utilities;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace clash.gameplay
{
    public class OverlaySystem:ClashBaseSystem, IStartSystem,IUpdateSystem,ITickSystem
    {
        private ClashWorld _clashWorld = null;
        
        private DebugGrid _debugGrid = null;
        private ClashBaseEntity _tileSelectorEntity = null;
        
        
        private UserCtrlMetaInfo _userCtrlMeta = null;
        private GameStartMeta _gameStartMeta = null;
        private TileMapMeta _tileMapMeta = null;

        public OverlaySystem(ClashBaseWorld world) : base(world)
        {
            _clashWorld = GetWorld<ClashWorld>();
            _userCtrlMeta = world.GetWorldMeta<UserCtrlMetaInfo>();
            _gameStartMeta = world.GetWorldMeta<GameStartMeta>();
            _tileMapMeta = world.GetWorldMeta<TileMapMeta>();
        }

        public void OnStart()
        {
            GenerateDebugGrid();
        }

        public void OnUpdate(float deltaTime)
        {
            UpdateTileSelectorPosition();
            CheckAndRefreshTerrainGrid();
        }

        public void OnTick(int frameIndex)
        {
            
        }
        
        private void GenerateDebugGrid()
        {
            ClashWorld world = GetWorld<ClashWorld>();
            var gameStartMeta = world.GetWorldMeta<GameStartMeta>();
            var clashConfigMeta = world.GetWorldMeta<ClashConfigMeta>();
            var material = world.ResManager.GetAsset<UnityEngine.Material>("Assets/Resources_moved/clashgame/materials/grid_debugger/GridDebugger Variant.mat");
            
            _debugGrid = new DebugGrid(world,world.RootGameObject);
            _debugGrid.BuildMesh(material,gameStartMeta,clashConfigMeta,_tileMapMeta);
            _debugGrid.AttachCollider();
        }
        
        private void UpdateTileSelectorPosition()
        {
            // get and hold entity
            if (_tileSelectorEntity == null)
            {
                List<ClashBaseEntity> entities = _world.GetEntitiesWithComponents(typeof(TileSelectorComponent),typeof(GfxComponent));
                if (entities != null && entities.Count > 0)
                {
                    _tileSelectorEntity = entities[0];
                }
            }
            
            // do move selector entity
            if (_tileSelectorEntity != null && _userCtrlMeta.SelectTileDirtyFlag)
            {
                var gfxComp = _tileSelectorEntity.GetComponent<GfxComponent>();
                Vector3 position = ClashUtility.GetPositionAtTile(_clashWorld, _userCtrlMeta.SelectTileX, _userCtrlMeta.SelectTileY);
                gfxComp.SetPosition(position);
                _userCtrlMeta.ResetSelectTileDirtyFlag();
            }
        }

        private void CheckAndRefreshTerrainGrid()
        {
            if (_tileMapMeta.IsDirty())
            {
                _debugGrid.RefreshTexture(_gameStartMeta.GridWidth,_gameStartMeta.GridHeight,_tileMapMeta);
                _tileMapMeta.ClearDirtyFlag();
            }
        }

        public override void Dispose()
        {
            _debugGrid?.Dispose();
            _debugGrid = null;
        }
        
    }
}