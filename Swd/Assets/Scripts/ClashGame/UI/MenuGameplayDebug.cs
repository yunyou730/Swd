using System.Collections;
using System.Collections.Generic;
using clash;
using UnityEngine;
using UnityEngine.UI;

namespace clash.ui
{
    public class MenuGameplayDebug : ClashBaseMenu
    {
        public MenuGameplayDebug(MenuManager menuManager,EMenuType menuType,GameObject root):base(menuManager,menuType,root)
        {
            
        }

        public override void OnEnter()
        {
            // GameObject.Find("", _gameObject);
        
            Button btnClose = _gameObject.transform.Find("Button_Close").GetComponent<Button>();
            btnClose.onClick.AddListener(delegate()
            {
                _menuManager.CloseMenu(_menuType);
            });
        }
    
        protected override GameObject LoadGameObject()
        {
            var prefab = ClashGame.Instance.ResManager.GetAsset<GameObject>("Assets/Resources_moved/clashgame/ui/Canvas_MenueGameplayDebug.prefab");
            var go = GameObject.Instantiate(prefab);
            return go;
        }
    
        public override void OnUpdate(float deltaTime)
        {
        
        }

        // public override void OnClose()
        // {
        //     base.OnClose();
        // }

        public override void Dispose()
        {
        
        }
    }
    
}
