using UnityEngine;

namespace clash.gameplay.GameObject
{
    public class ClashUnitMB : MonoBehaviour
    {
        private int _entityUUID = 0;
        private string _unitTag = null;
        public int EntityUUID { get { return _entityUUID; } }
        public string UnitTag { get { return _unitTag; } }
        
        public void Init(int uuid,string tag)
        {
            _entityUUID = uuid;
            _unitTag = tag;
        }
    }
}