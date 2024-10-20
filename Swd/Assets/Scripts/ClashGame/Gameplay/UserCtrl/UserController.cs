using System;
using clash.gameplay;
using UnityEngine;
using clash.gameplay.Utilities;

namespace clash.Gameplay.UserCtrl
{
    public class UserController : IDisposable
    {
        private Camera _mainCamera = null;
        private ClashWorld _clashWorld = null;
        
        private int _selectTileX = -1;
        private int _selectTileY = -1;
        
        public UserController(Camera camera,ClashWorld world)
        {
            _mainCamera = camera;
            _clashWorld = world;
        }


        public void OnStart()
        {
            
        }

        public void OnUpdate(float dt)
        {
            RefreshMouseTileCoordinate();
        }
        
        public void Dispose()
        {
            
        }

        private void RefreshMouseTileCoordinate()
        {
            int tileX, tileY;
            CheckMouseTileCoordinate(out tileX,out tileY);
            if (tileX != _selectTileX || tileY != _selectTileY)
            {
                _selectTileX = tileX;
                _selectTileY = tileY;
                _clashWorld.GetWorldMeta<UserCtrlMetaInfo>().SetSelectTile(_selectTileX,_selectTileY);
            }
        }

        private bool CheckMouseTileCoordinate(out int tileX,out int tileY)
        {
            tileX = -1;
            tileY = -1;

            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 2f); // 射线长度为 100，持续 2 秒
            if (Physics.Raycast(ray,out hit))
            {
                Vector3 hitPointWorld = hit.point;
                ClashUtility.WorldPositionToTileCoordinate(_clashWorld,hitPointWorld,out tileX,out tileY);
                return true;
            }
            
            return false;
        }
        
    }

}