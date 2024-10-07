using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace clash.ui
{
    public class MenuManager : IDisposable
    {
        private Dictionary<EMenuType, Type> _menuTypeMap = null;
        private Dictionary<EMenuType, ClashBaseMenu> _menuInstanceMap = null;

        private GameObject _root = null;

        public MenuManager(GameObject root)
        {
            _root = root;
            _menuTypeMap = new Dictionary<EMenuType, Type>();
            _menuInstanceMap = new Dictionary<EMenuType, ClashBaseMenu>();
            
            RegisterAllMenu();
        }

        private void RegisterAllMenu()
        {
            _menuTypeMap.Add(EMenuType.GameplayDebug,typeof(MenuGameplayDebug));
        }

        public void ShowMenu(EMenuType menuKey)
        {
            ClashBaseMenu menuInstance = null;
            if (_menuInstanceMap.ContainsKey(menuKey))
            {
                menuInstance = _menuInstanceMap[menuKey];
            }
            else
            {
                Debug.Assert(_menuTypeMap.ContainsKey(menuKey));

                Type menuType = _menuTypeMap[menuKey];
                
                object[] constructorArgs = {this,menuKey,_root};
                menuInstance = (ClashBaseMenu)Activator.CreateInstance(menuType,constructorArgs);
                menuInstance.Init();
                
                Debug.Assert(menuInstance != null);

                _menuInstanceMap.Add(menuKey,menuInstance);
            }
            menuInstance.OnEnter();
        }

        public void CloseMenu(EMenuType menuKey)
        {
            if (_menuInstanceMap.ContainsKey(menuKey))
            {
                ClashBaseMenu menuInstance = _menuInstanceMap[menuKey];
                menuInstance.OnClose();
                menuInstance.Dispose();
                _menuInstanceMap.Remove(menuKey);    
            }
        }
        
        public void Dispose()
        {
            _menuTypeMap.Clear();
            _menuTypeMap = null;
            
            foreach (var menu in _menuInstanceMap.Values) 
            {
                menu.Dispose();
            }
            _menuInstanceMap.Clear();
            _menuInstanceMap = null;
        }
    }
}
