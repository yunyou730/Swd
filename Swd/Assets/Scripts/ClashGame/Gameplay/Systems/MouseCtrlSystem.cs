using clash.gameplay.Utilities;
using UnityEngine;

namespace clash.gameplay
{
    public class MouseCtrlSystem : ClashBaseSystem, IUpdateSystem,IStartSystem
    {
        private ClashWorld _clashWorld = null;
        private UnityEngine.Camera _mainCamera = null;
        private MouseCtrlMetaInfo _mouseCtrlMeta = null;
        
        public void OnStart()
        {
            _clashWorld = GetWorld<ClashWorld>();
            _mainCamera = _clashWorld.GameplayMainCamera;
            _mouseCtrlMeta = _clashWorld.GetWorldMeta<MouseCtrlMetaInfo>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            // update mouse at Tile Coordinate
            int tileX, tileY;
            CheckMouseTileCoordinate(out tileX, out tileY);
            _mouseCtrlMeta.TileX = tileX;
            _mouseCtrlMeta.TileY = tileY;
            
            // update mouse button status
            _mouseCtrlMeta.IsLeftButtonDown = Input.GetMouseButton(0);
            _mouseCtrlMeta.IsRightButtonDown = Input.GetMouseButton(1);
            _mouseCtrlMeta.IsMidButtonDown = Input.GetMouseButton(2);
        }

        public MouseCtrlSystem(ClashBaseWorld world) : base(world)
        {
        }

        public override void Dispose()
        {
            
        }


        private bool CheckMouseTileCoordinate(out int tileX,out int tileY)
        {
            tileX = -1;
            tileY = -1;

            // if (Input.GetMouseButton(0))
            // {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f); // 射线长度为 100，持续 2 秒
            if (Physics.Raycast(ray,out hit))
            {
                Vector3 hitPointWorld = hit.point;
                ClashUtility.WorldPositionToTileCoordinate(_clashWorld,hitPointWorld,out tileX,out tileY);
                return true;
            }
            // }
            return false;
        }
    }
}