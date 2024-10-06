using clash.gameplay.Utilities;
using swd;

namespace clash.gameplay
{
    public class UnitCreationSystem:ClashBaseSystem, IStartSystem,ITickSystem
    {
        UnitFactoryWorldComp _unitFactory = null;
        
        private ClashWorld _clashWorld = null;
        private ResManager _resManager = null;
        
        public UnitCreationSystem(ClashBaseWorld world) : base(world)
        {
            _unitFactory = world.GetWorldComponent<UnitFactoryWorldComp>();
            _clashWorld = (ClashWorld)_world;
        }

        public override void Dispose()
        {
            
        }

        public void OnStart()
        {
            
        }

        public void OnTick(int frameIndex)
        {
            if (_unitFactory.Datas.Count == 0)
            {
                return;
            }

            foreach (var item in _unitFactory.Datas)
            {
                CreateUnit(item);
            }
            _unitFactory.Datas.Clear();
        }
        
        private void CreateUnit(UnitGenerateData data)
        {
            ClashCfgUnitEntry unitConfig = _clashWorld.AllUnitsCfg.GetConfig(data.UnitTag);
            UnityEngine.GameObject prefab = _clashWorld.ResManager.GetAsset<UnityEngine.GameObject>(unitConfig.PrefabPath);
            
            UnityEngine.GameObject go = UnityEngine.GameObject.Instantiate(prefab);
            go.transform.SetParent(_clashWorld.RootGameObject.transform);
            
            UnityEngine.Vector3 pos = ClashUtility.GetPositionAtTile(_clashWorld, data.TileX, data.TileY);
            go.transform.position = pos;
        }
    }
}