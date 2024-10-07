using System.Collections;
using System.Collections.Generic;
using clash;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace clash.ui
{
    public class MenuGameplayDebug : ClashBaseMenu
    {
        private TextMeshProUGUI _txtMode = null;
        public MenuGameplayDebug(MenuManager menuManager,EMenuType menuType,GameObject root):base(menuManager,menuType,root)
        {
            
        }

        public override void OnEnter()
        {
            Button btnMode = _gameObject.transform.Find("Button_ToggleMode").GetComponent<Button>();
            btnMode.onClick.AddListener(delegate()
            {
                ClashGame.Instance.SwitchMode(NextMode(ClashGame.Instance.GameMode));
                RefreshLabel();
            });
        
            Button btnClose = _gameObject.transform.Find("Button_Close").GetComponent<Button>();
            btnClose.onClick.AddListener(delegate()
            {
                _menuManager.CloseMenu(_menuType);
            });
            
            _txtMode = _gameObject.transform.Find("Text_GameMode").GetComponent<TextMeshProUGUI>();
            RefreshLabel();
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

        private void RefreshLabel()
        {
            switch (ClashGame.Instance.GameMode)
            {
                case EClashGameMode.Test:
                    _txtMode.text = "Test Mode";
                    break;
                case EClashGameMode.MapEdit:
                    _txtMode.text = "Map Edit Mode";
                    break;
                case EClashGameMode.Play:
                    _txtMode.text = "Play Mode";
                    break;
                default:
                    _txtMode.text = "[Mode Error!]";
                    break;
            }
        }

        private EClashGameMode NextMode(EClashGameMode curMode)
        {
            int nextMode = (int)curMode + 1;
            if (nextMode == (int)EClashGameMode.Max)
            {
                nextMode = 0;
            }
            return (EClashGameMode)nextMode;
        }
    }
    
}
