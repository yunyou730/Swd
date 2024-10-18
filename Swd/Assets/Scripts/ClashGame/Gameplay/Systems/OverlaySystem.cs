using System.Collections.Generic;
using clash.gameplay.GameObject;
using clash.gameplay.Utilities;
using UnityEngine;

namespace clash.gameplay
{
    public class OverlaySystem:ClashBaseSystem, IStartSystem,IUpdateSystem,ITickSystem
    {
        private ClashWorld _clashWorld = null;
        
        private DebugGrid _debugGrid = null;
        private ClashBaseEntity _tileSelectorEntity = null;
        private MouseCtrlMetaInfo _mouseCtrlMeta = null;

        public OverlaySystem(ClashBaseWorld world) : base(world)
        {
            _clashWorld = GetWorld<ClashWorld>();
            _mouseCtrlMeta = world.GetWorldMeta<MouseCtrlMetaInfo>();
        }

        public void OnStart()
        {
            
            GenerateDebugGrid();
        }

        public void OnUpdate(float deltaTime)
        {
            UpdateTileSelectorPosition();
        }

        public void OnTick(int frameIndex)
        {
            
        }
        
        private void GenerateDebugGrid()
        {
            ClashWorld world = GetWorld<ClashWorld>();
            var gameStartMeta = world.GetWorldMeta<GameStartMeta>();
            var clashConfigMeta = world.GetWorldMeta<ClashConfigMeta>();
            var material = world.ResManager.GetAsset<UnityEngine.Material>("Assets/Resources_moved/clashgame/scenes/grid_debugger/GridDebugger Variant.mat");
            
            _debugGrid = new DebugGrid(world,world.RootGameObject);
            _debugGrid.BuildMesh(material,gameStartMeta,clashConfigMeta);
            _debugGrid.AttachCollider();
        }


        private void UpdateTileSelectorPosition()
        {
            if (_tileSelectorEntity == null)
            {
                List<ClashBaseEntity> entities = _world.GetEntitiesWithComponents(typeof(TileSelectorComponent),typeof(GfxComponent));
                if (entities != null && entities.Count > 0)
                {
                    _tileSelectorEntity = entities[0];
                }
            }

            if (_tileSelectorEntity != null)
            {
                var gfxComp = _tileSelectorEntity.GetComponent<GfxComponent>();
                Vector3 position = ClashUtility.GetPositionAtTile(_clashWorld, _mouseCtrlMeta.TileX, _mouseCtrlMeta.TileY);
                gfxComp.SetPosition(position);
            }
        }

        public override void Dispose()
        {
            _debugGrid?.Dispose();
            _debugGrid = null;
        }
        
    }
}