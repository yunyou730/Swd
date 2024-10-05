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

        public ClashBaseWorld()
        {
            _entityMap = new Dictionary<int, ClashBaseEntity>();
        }

        public ClashBaseEntity CreateEntity()
        {
            var entity = new ClashBaseEntity(++_uuidSeed);
            return entity;
        }
        
        public void Dispose()
        {
            Debug.Log("ClashBaseWorld::Dispose()");
            DisposeEntities();
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
    }
}