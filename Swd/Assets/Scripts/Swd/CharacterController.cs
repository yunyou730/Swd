using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace swd.character
{
    [Serializable]
    public class ActionCfgItem
    {
        public CharacterController.EActionType ActionType;
        // public CharacterActionController ActionCtrl;
        public AssetReference ActionCtrlPrefab;
    }

    public class CharacterController : MonoBehaviour
    {
        [System.Serializable]
        public enum EActionType
        {
            Idle,
            Walk,
            Run,
        }
        
        [SerializeField]
        public List<ActionCfgItem> ActionConfig = null;
        
        // private Dictionary<EActionType, CharacterActionController> _actionMap = new Dictionary<EActionType, CharacterActionController>();


        // public AssetReference ar = null;


        public void Awake()
        {
            InitActionMap();
        }
        
        private void InitActionMap()
        {
            // foreach (ActionCfgItem item in ActionConfig)
            // {
            //     Debug.Assert(!_actionMap.ContainsKey(item.ActionType));
            //     CharacterActionController prefab = item.ActionCtrl;
            //     CharacterActionController go = GameObject.Instantiate(prefab);
            //     go.transform.parent = this.gameObject.transform;
            //     go.enabled = false;
            //     _actionMap.Add(item.ActionType,go);
            // }
        }
        
        public void PlayAction(EActionType actionType)
        {
            // if (_actionMap.ContainsKey(actionType))
            // {
                //CharacterActionController go = _actionMap[actionType];
            // }
        }
    }
}

