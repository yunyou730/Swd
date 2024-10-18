using clash.gameplay.Utilities;
using UnityEngine;

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
            //if (_mouseCtrlMeta.LeftButtonClicked && _tileEditMeta.SelectedTerrainType != null)
            Debug.Log("[ayy-test1]" + Input.GetMouseButton(0));
            if (Input.GetMouseButton(0) && _tileEditMeta.SelectedTerrainType != null)
            {
                Debug.Log("[ayy-test2]");
                ClashTileEditFunc.ChangeTileTerrainType(_clashWorld,_mouseCtrlMeta.TileX,_mouseCtrlMeta.TileY,_tileEditMeta.SelectedTerrainType.Value);
            }
        }        

        public void OnTick(int frameIndex)
        {
            
        }
        
        public override void Dispose()
        {
            
        }

    }
}