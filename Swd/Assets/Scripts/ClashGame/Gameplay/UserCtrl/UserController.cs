using System;
using clash.Event;
using clash.gameplay;
using UnityEngine;
using clash.gameplay.Utilities;
using UnityEngine.EventSystems;

namespace clash.Gameplay.UserCtrl
{
    public class UserController : IDisposable
    {
        private Camera _mainCamera = null;
        private ClashWorld _clashWorld = null;
        
        private int _selectTileX = -1;
        private int _selectTileY = -1;
        private ETileTerrainType? _selectTileTerrainType = null;

        private ClashEventManager _eventManager = null;
        
        public UserController(Camera camera,ClashWorld world)
        {
            _mainCamera = camera;
            _clashWorld = world;
            _eventManager = ClashGame.Instance.EventManager;
        }


        public void OnStart()
        {
            _eventManager.EventChangeTileTerrainType += (sender, args) =>
            {
                _selectTileTerrainType = args.TileType;
            };
        }

        public void OnUpdate(float dt)
        {
            RefreshMouseTileCoordinate();
            if (IsTryingToModifyTileTerrainType())
            {
                DoModifyTileTerrainType(_selectTileX,_selectTileY,_selectTileTerrainType.Value);
            }
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


        private bool IsTryingToModifyTileTerrainType()
        {
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && _selectTileTerrainType != null)
            {
                return true;
            }
            return false;
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


        private void DoModifyTileTerrainType(int tileX,int tileY,ETileTerrainType terrainType)
        {
            var cmdMeta = _clashWorld.GetWorldMeta<CmdMeta>();
            cmdMeta.AddCmdChangeTileTerrainType(tileX,tileY,terrainType);
        }

    }

}