using System.Collections.Generic;
using swd.Config;
using UnityEngine;

namespace swd.character
{
    public class CharacterActor
    {
        
        private string _tag;
        private Dictionary<EAction, GameObject> _actionMap = new Dictionary<EAction, GameObject>();
        private EAction _currentAction = EAction.None;

        public CharacterActor(string tag)
        {
            _tag = tag;
        }

        public void PlayAction(EAction action)
        {
            if (_actionMap.ContainsKey(action))
            {
                
            }
            else
            {
                string prefabResKey = string.Format("Pal3Res/" + _tag + "/Prefabs/" + GetActionName(action) + ".MV3");
                
                
                
                
            }
        }

        private void HideCurrentAction()
        {
            
        }

        private string GetActionName(EAction action)
        {
            return ActionConstants.ActionNameMap[action];
        }



        public void PlayIdle()
        {
            // var prefab = _resManager.GetAsset("Pal3Res/105/Prefabs/C03.MV3");
            // Instantiate(prefab, transform);
        }

        public void PlayRun()
        {
            
        }

        public void PlayWalk()
        {
            
        }
    }
}