﻿using clash.gameplay.GameObject;
using clash.gameplay.Utilities;
using swd;

namespace clash.gameplay
{
    public class UnitCreationSystem:ClashBaseSystem, IStartSystem,ITickSystem
    {
        private UnitFactoryMeta _unitFactory = null;
        private ClashWorld _clashWorld = null;

        public UnitCreationSystem(ClashBaseWorld world) : base(world)
        {
            _unitFactory = world.GetWorldMeta<UnitFactoryMeta>();
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
            ClashBaseEntity entity = _world.CreateEntity();
            var gfxComp = entity.AttachComponent<GfxComponent>();
            var posComp = entity.AttachComponent<PositionComponent>();
            var rotComp = entity.AttachComponent<RotationComponent>();
            
            ClashConfigUnitEntry unitConfig = _clashWorld.AllUnitsCfg.GetConfig(data.UnitTag);
            UnityEngine.GameObject prefab = _clashWorld.ResManager.GetAsset<UnityEngine.GameObject>(unitConfig.PrefabPath);
            
            UnityEngine.GameObject go = UnityEngine.GameObject.Instantiate(prefab);
            go.transform.SetParent(_clashWorld.RootGameObject.transform);
            
            var clashUnitMB = go.AddComponent<ClashUnitMB>(); 
            clashUnitMB.Init(entity.UUID,data.UnitTag);
            
            UnityEngine.Vector3 pos = ClashUtility.GetPositionAtTile(_clashWorld, data.TileX, data.TileY);
            go.transform.position = pos;
            
            gfxComp.GO = go;
            posComp.Pos = pos;
            rotComp.Degree = 0.0f;
        }
    }
}