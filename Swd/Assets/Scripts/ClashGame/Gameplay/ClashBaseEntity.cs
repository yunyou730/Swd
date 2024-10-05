using System;
using System.Collections.Generic;

namespace clash.gameplay
{
    public class ClashBaseEntity : IDisposable
    {
        private int _uuid = 0;

        private Dictionary<Type, ClashBaseComponent> _components = null;

        public int UUID
        {
            get { return _uuid; }
        }


        public ClashBaseEntity(int uuid)
        {
            _uuid = uuid;
            _components = new Dictionary<Type, ClashBaseComponent>();
        }
        
        public T AttachComponent<T>() where T : ClashBaseComponent,new()
        {
            T comp = new T();
            _components.Add(comp.GetType(),comp);
            return comp;
        }
        
        public void Dispose()
        {
            // Dispose all components
            foreach(var comp in _components.Values)
            {
                comp.Dispose();
            }
            _components.Clear();
            _components = null;
        }
    }
}