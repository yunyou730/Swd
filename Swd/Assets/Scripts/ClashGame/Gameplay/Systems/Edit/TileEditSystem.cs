using clash.gameplay.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace clash.gameplay
{
    public class TileEditSystem : ClashBaseSystem,IStartSystem,ITickSystem,IUpdateSystem
    {
        private ClashWorld _clashWorld = null;

        private MouseCtrlMetaInfo _mouseCtrlMeta = null;
        private TileEditMeta _tileEditMeta = null;

        public TileEditSystem(ClashBaseWorld world) : base(world)
        {
            _clashWorld = GetWorld<ClashWorld>();
            _mouseCtrlMeta = _clashWorld.GetWorldMeta<MouseCtrlMetaInfo>();
            _tileEditMeta = _clashWorld.GetWorldMeta<TileEditMeta>();
        }
        
        public void OnStart()
        {
            
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (IsMouseClicked() && _tileEditMeta.SelectedTerrainType != null)
            {
                ClashTileEditFunc.ChangeTileTerrainType(_clashWorld,_mouseCtrlMeta.TileX,_mouseCtrlMeta.TileY,_tileEditMeta.SelectedTerrainType.Value);
            }
        }

        public void OnTick(int frameIndex)
        {
            
        }
        
        public override void Dispose()
        {
            
        }

        private bool IsMouseClicked()
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                // Left button clicked, and not response by GUI 
                return true;
            }
            return false;
        }

    }
}