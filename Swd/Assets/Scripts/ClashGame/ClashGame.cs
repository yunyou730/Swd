﻿using System;
using System.Collections;
using System.Collections.Generic;
using clash.gameplay;
using clash.ui;
using IngameDebugConsole;
using LitJson;
using swd;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace clash
{
    public class ClashGame: MonoBehaviour
    {
        // private ClashGameEnum.EClashGameState _state = ClashGameEnum.EClashGameState.Ready;
        private ClashWorld _world = null;
        private ResManager _resManager = null;
        private MenuManager _menuManager = null;

        public static ClashGame Instance = null;
        public ResManager ResManager { get { return _resManager; } }
        public MenuManager MenuManager { get { return _menuManager; } }
        
        
        public ClashWorld World { get { return _world; } }

        private EClashGameMode _mode = EClashGameMode.Test;
        public EClashGameMode GameMode
        {
            set { _mode = value;}
            get { return _mode; }
        }

        private void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            _resManager = new ResManager();
            _menuManager = new MenuManager(gameObject);
            
            StartGame();
            ShowGameMenu();
            SwitchClashWorldToPlayMode();
        }
        
        private void Update()
        {
            float dt = Time.deltaTime;
            _world?.OnUpdate(dt);
            _menuManager?.OnUpdate(dt);
        }

        public void OnDestroy()
        {
            if (_world != null)
            {
                _world.Dispose();
                _world = null;    
            }
            
            _menuManager.Dispose();
            _menuManager = null;
            
            _resManager.ReleaseCache();
            _resManager = null;
            
            Instance = null;
        }


        public ResManager GetResManager()
        {
            return _resManager;
        }

        private void StartGame()
        {
            Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
            
            var gameData1 = _resManager.GetAsset<ClashGameData>("Assets/Resources_moved/clashgame/data/ClashGameData_1.asset");
            // var gameData2 = _resManager.GetAsset<ClashGameData>("Assets/Resources_moved/clashgame/data/ClashGameData_2.asset");
            var config = _resManager.GetAsset<ClashConfig>("Assets/Resources_moved/clashgame/data/ClashGameConfig.asset");
            
            TextAsset textAsset = _resManager.GetAsset<TextAsset>("Assets/Resources_moved/clashgame/config/unit/units.json");
            LitJson.JsonData unitsJsonData = JsonMapper.ToObject(textAsset.text);

            var gameData = gameData1;
            Debug.Assert(gameData != null && config != null);
            
            _world = new ClashWorld();
            _world.Init(gameData,config,unitsJsonData,gameObject,camera,_resManager);
            _world.OnStart();
        }
        
        private void ShowGameMenu()
        {
            _menuManager.ShowMenu(EMenuType.GameplayDebug);
        }

        private void SwitchClashWorldToPlayMode()
        {
            var modeSwitchMeta = _world.GetWorldMeta<ModeSwitchMetaInfo>();
            modeSwitchMeta.SetNextMode(EClashGameMode.Test);
        }
        
        public void SwitchMode(EClashGameMode nextMode)
        {
            if (GameMode != nextMode)
            {
                GameMode = nextMode;
                var modeSwitchMeta = _world.GetWorldMeta<ModeSwitchMetaInfo>();
                modeSwitchMeta.SetNextMode(nextMode);
            }
        }

    }
}