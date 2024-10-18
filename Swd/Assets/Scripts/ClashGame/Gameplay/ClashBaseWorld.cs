using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace clash.gameplay
{
    public abstract class ClashBaseWorld : IDisposable
    {
        private int _uuidSeed = 0;

        private Dictionary<int, ClashBaseEntity> _entityMap = null;
        private Dictionary<Type, ClashBaseMetaInfo> _worldComponentMap = null;

        public ClashBaseWorld()
        {
            _entityMap = new Dictionary<int, ClashBaseEntity>();
            _worldComponentMap = new Dictionary<Type, ClashBaseMetaInfo>();
        }


        public abstract void OnStart();
        public abstract void OnUpdate(float deltaTime);
        

        public ClashBaseEntity CreateEntity()
        {
            var entity = new ClashBaseEntity(++_uuidSeed);
            _entityMap.Add(entity.UUID,entity);
            return entity;
        }

        public T CreateWorldMetaInfo<T>() where T : ClashBaseMetaInfo, new()
        {
            T t = new T();
            _worldComponentMap.Add(t.GetType(),t);
            return t;
        }

        public T GetWorldMeta<T>() where T:ClashBaseMetaInfo
        {
            return (T)_worldComponentMap[typeof(T)];
        }

        public void Dispose()
        {
            Debug.Log("ClashBaseWorld::Dispose()");
            DisposeEntities();
            DisposeWorldComps();
        }

        private void DisposeEntities()
        {
            foreach (var entity in _entityMap.Values)
            {
                entity.Dispose();
            }
            _entityMap.Clear();
            _entityMap = null;
        }

        private void DisposeWorldComps()
        {
            foreach (var worldComp in _worldComponentMap.Values)
            {
                worldComp.Dispose();    
            }
            _worldComponentMap.Clear();
            _worldComponentMap = null;
        }
    }
}