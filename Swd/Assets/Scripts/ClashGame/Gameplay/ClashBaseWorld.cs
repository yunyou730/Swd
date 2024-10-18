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

        public ClashBaseEntity GetEntity(int uuid)
        {
            if (_entityMap.ContainsKey(uuid))
            {
                return _entityMap[uuid];
            }
            return null;
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
        
        public List<ClashBaseEntity> GetEntitiesWithComponents(params Type[] componentTypes)
        {
            List<ClashBaseEntity> matchingEntities = null;

            foreach (var entity in _entityMap.Values)
            {
                bool hasAllComponents = true;

                foreach (var type in componentTypes)
                {
                    if (!entity.HasComponent(type))
                    {
                        hasAllComponents = false;
                        break;
                    }
                }

                if (hasAllComponents)
                {
                    if (matchingEntities == null)
                        matchingEntities = new List<ClashBaseEntity>();
                    
                    matchingEntities.Add(entity);
                }
            }

            return matchingEntities;
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