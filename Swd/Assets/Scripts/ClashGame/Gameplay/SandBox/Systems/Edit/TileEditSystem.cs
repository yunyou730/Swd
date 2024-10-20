using clash.gameplay.Utilities;
using UnityEngine;
using UnityEngine.EventSystems;

namespace clash.gameplay
{
    public class TileEditSystem : ClashBaseSystem,IStartSystem,ITickSystem,IUpdateSystem
    {
        private ClashWorld _clashWorld = null;

        private UserCtrlMetaInfo _userCtrlMeta = null;
        private TileEditMeta _tileEditMeta = null;

        public TileEditSystem(ClashBaseWorld world) : base(world)
        {
            _clashWorld = GetWorld<ClashWorld>();
            _userCtrlMeta = _clashWorld.GetWorldMeta<UserCtrlMetaInfo>();
            _tileEditMeta = _clashWorld.GetWorldMeta<TileEditMeta>();
        }
        
        public void OnStart()
        {
            
        }
        
        public void OnUpdate(float deltaTime)
        {
            if (IsMouseClicked() && _tileEditMeta.SelectedTerrainType != null)
            {
                ClashTileEditFunc.ChangeTileTerrainType(_clashWorld,_userCtrlMeta.SelectTileX,_userCtrlMeta.SelectTileY,_tileEditMeta.SelectedTerrainType.Value);
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