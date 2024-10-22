using System;
using clash.Event;
using clash.gameplay;
using UnityEngine;
using clash.gameplay.Utilities;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace clash.Gameplay.UserCtrl
{
    /*
     * 控制 Tile 地形刷新
     * 控制 动态创建 Unit
     */
    public class UserController : IDisposable
    {
        private Camera _mainCamera = null;
        private ClashWorld _clashWorld = null;
        
        // selecting tile 
        private int _selectTileX = -1;
        private int _selectTileY = -1;
        private ETileTerrainType? _selectTileTerrainType = null;
        private GameObject _tileSelectorGameObject = null;
        public int SelectTileX { get { return _selectTileX; } }
        public int SelectTileY { get { return _selectTileY; } }
        
        // selecting unit 
        private string _selectUnitTag = null; 
        
        private ClashEventManager _eventManager = null;
        
        public UserController(Camera camera,ClashWorld world)
        {
            _mainCamera = camera;
            _clashWorld = world;
            _eventManager = ClashGame.Instance.EventManager;
        }


        public void OnStart()
        {
            _eventManager.EventChangeTileTerrainType += OnChangeTileTerrainType;
            _eventManager.EventChangeDebugMenuSelectUnitTag += OnChangeSelectUnitTag;
            CreateTileSelector();
        }
        
        public void OnUpdate(float dt)
        {
            bool bSelectAnyNeedTileSelector = HasSelectTileTerrainOrUnitTag();
            UpdateTileSelectorVisibility(bSelectAnyNeedTileSelector);
            if (bSelectAnyNeedTileSelector)
            {
                RefreshMouseTileCoordinate();
                UpdateTileSelectorPosition();
                if (IsTryingToCreateUnitAtTile())
                {
                    DoCreateUnitAtTile(_selectTileX,_selectTileY,_selectUnitTag);
                }
                else if (IsTryingToModifyTileTerrainType())
                {
                    DoModifyTileTerrainType(_selectTileX,_selectTileY,_selectTileTerrainType.Value);
                }   
            }
        }
        
        public void Dispose()
        {
            _eventManager.EventChangeTileTerrainType -= OnChangeTileTerrainType;
            _eventManager.EventChangeDebugMenuSelectUnitTag -= OnChangeSelectUnitTag;
            DestroyTileSelector();
        }


        private void CreateTileSelector()
        {
            var tileSelectorPrefab = _clashWorld.ResManager.GetAsset<UnityEngine.GameObject>("Assets/Resources_moved/clashgame/materials/tile_selector/TileSelector.prefab");
            _tileSelectorGameObject = GameObject.Instantiate(tileSelectorPrefab);
        }

        private void DestroyTileSelector()
        {
            if (_tileSelectorGameObject != null)
            {
                Object.Destroy(_tileSelectorGameObject);
                _tileSelectorGameObject = null;
            }
        }

        private void UpdateTileSelectorPosition()
        {
            Vector3 position = ClashUtility.GetPositionAtTile(_clashWorld, _selectTileX,_selectTileY);
            _tileSelectorGameObject.transform.position = position;
        }

        private void UpdateTileSelectorVisibility(bool bVisible)
        {
            var meshRenderer = _tileSelectorGameObject.GetComponent<MeshRenderer>();
            meshRenderer.enabled = bVisible;
        }

        private void RefreshMouseTileCoordinate()
        {
            int tileX, tileY;
            CheckMouseTileCoordinate(out tileX,out tileY);
            if (tileX != _selectTileX || tileY != _selectTileY)
            {
                _selectTileX = tileX;
                _selectTileY = tileY;
                // _clashWorld.GetWorldMeta<UserCtrlMetaInfo>().SetSelectTile(_selectTileX,_selectTileY);
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

        private bool IsTryingToCreateUnitAtTile()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && _selectUnitTag != null)
            {
                return true;
            }
            return false;
        }
        
        private bool HasSelectTileTerrainOrUnitTag()
        {
            return _selectTileTerrainType != null || _selectUnitTag != null;
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

        private void DoCreateUnitAtTile(int tileX,int tileY,string unitTag)
        {
            var cmdMeta = _clashWorld.GetWorldMeta<CmdMeta>();
            cmdMeta.AddCmdCreateUnitAtTile(tileX,tileY,unitTag);
        }

        private void OnChangeTileTerrainType(object sender, SelectTileTerrainTileEventArgs args)
        {
            _selectTileTerrainType = args.TileType;           
        }

        private void OnChangeSelectUnitTag(object sender,string unitTag)
        {
            _selectUnitTag = unitTag;           
        }

    }

}