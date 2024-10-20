using System;
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
        
        private ResManager _resManager = null;
        private MenuManager _menuManager = null;

        public static ClashGame Instance = null;
        public ResManager ResManager { get { return _resManager; } }
        public MenuManager MenuManager { get { return _menuManager; } }


        private gameplay.Gameplay _gameplay = null;
        public gameplay.Gameplay GP { get {return _gameplay;} }
        
        

        

        private void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            _resManager = new ResManager();
            _menuManager = new MenuManager(gameObject);

            _gameplay = new gameplay.Gameplay();
            
            _gameplay.Start();
            _gameplay.SwitchClashWorldToPlayMode();
            
            ShowGameMenu();
        }
        
        private void Update()
        {
            float dt = Time.deltaTime;
            
            _gameplay.OnUpdate(dt);
            _menuManager?.OnUpdate(dt);
        }

        public void OnDestroy()
        {
            _gameplay?.Dispose();
            
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

        // private void StartGame()
        // {
        //     Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //     
        //     var gameData1 = _resManager.GetAsset<ClashGameData>("Assets/Resources_moved/clashgame/data/ClashGameData_1.asset");
        //     // var gameData2 = _resManager.GetAsset<ClashGameData>("Assets/Resources_moved/clashgame/data/ClashGameData_2.asset");
        //     var config = _resManager.GetAsset<ClashConfig>("Assets/Resources_moved/clashgame/data/ClashGameConfig.asset");
        //     
        //     TextAsset textAsset = _resManager.GetAsset<TextAsset>("Assets/Resources_moved/clashgame/config/unit/units.json");
        //     LitJson.JsonData unitsJsonData = JsonMapper.ToObject(textAsset.text);
        //
        //     var gameData = gameData1;
        //     Debug.Assert(gameData != null && config != null);
        //     
        //     _world = new ClashWorld();
        //     _world.Init(gameData,config,unitsJsonData,gameObject,camera,_resManager);
        //     _world.OnStart();
        // }
        
        private void ShowGameMenu()
        {
            _menuManager.ShowMenu(EMenuType.GameplayDebug);
        }

        

    }
}