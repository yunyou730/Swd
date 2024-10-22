using System;
using System.Collections.Generic;
using LitJson;
using swd;
using Unity.VisualScripting;
using UnityEngine;

namespace clash.gameplay
{
    public class ClashWorld : ClashBaseWorld
    {
        private ResManager _resManager = null;
        public ResManager ResManager { get { return _resManager; } }

        // All Systems
        private List<ClashBaseSystem> _systems = null;
        
        private List<IStartSystem> _startableSystems = null;
        private List<IUpdateSystem> _updatableSystems = null;
        private List<ITickSystem> _tickableSystems = null;
        
        // Initialize data & configs
        private ClashGameData _gameData = null;
        private ClashConfigSettings _clashSettings = null;
        private ClashAllUnitsConfig _allUnitsConfig = null;
        
        public ClashConfigSettings ClashSettings { get { return _clashSettings; } }
        public ClashAllUnitsConfig AllUnitsCfg { get { return _allUnitsConfig; } }


        // Root GameObject
        private UnityEngine.GameObject _rootGameObject = null;
        public UnityEngine.GameObject RootGameObject { get { return _rootGameObject; } }
        
        // Camera GameObject
        private UnityEngine.Camera _gameplayMainCamera = null;
        public UnityEngine.Camera GameplayMainCamera { get { return _gameplayMainCamera; } }



        // FPS & Tick Logic
        private float _logicFPS = 16.0f;
        private float _logicDeltaTime = 0.0f;
        private float _deltaTimeCounter = 0.0f;
        private int _frameIndex = 0;

        public void Init(ClashGameData gameData,
                            ClashConfigSettings clashConfig,
                            JsonData unitsJson,
                            UnityEngine.GameObject rootGameObject,
                            UnityEngine.Camera gameplayMainCamera,
                            ResManager resManager)
        {
            _gameData = gameData;
            _clashSettings = clashConfig;
            
            _rootGameObject = rootGameObject;
            _gameplayMainCamera = gameplayMainCamera;

            _resManager = resManager;

            // configs
            _allUnitsConfig = new ClashAllUnitsConfig(this,unitsJson);
            
            InitLogicFPS(clashConfig);
            InitWorldMetaInfo();
            InitSystems();
        }


        private void InitLogicFPS(ClashConfigSettings clashConfig)
        {
            _logicFPS = clashConfig.kLogicFPS;
            _logicDeltaTime = 1.0f / _logicFPS;
        }

        private void InitWorldMetaInfo()
        {
            var gameStartMeta = CreateWorldMetaInfo<GameStartMeta>();
            gameStartMeta.SceneName = _gameData.SceneName;
            gameStartMeta.GridWidth = _gameData.GridWidth;
            gameStartMeta.GridHeight = _gameData.GridHeight;
            
            var clashConfigMeta = CreateWorldMetaInfo<ClashConfigMeta>();
            clashConfigMeta.TileSize = _clashSettings.kTileSize;
            clashConfigMeta.TileBaseX = _clashSettings.kTileBaseX;
            clashConfigMeta.TileBaseZ = _clashSettings.kTileBaseZ;
            
            CreateWorldMetaInfo<UnitFactoryMeta>();
            
            var tileMapMeta = CreateWorldMetaInfo<TileMapMeta>();
            tileMapMeta.Init(_gameData.GridWidth,_gameData.GridHeight);

            CreateWorldMetaInfo<ModeMetaInfo>();
            CreateWorldMetaInfo<ModeSwitchMetaInfo>();
            // CreateWorldMetaInfo<UserCtrlMetaInfo>();
            CreateWorldMetaInfo<CmdMeta>();
        }
        
        private void InitSystems()
        {
            _systems = new List<ClashBaseSystem>();
            _startableSystems = new List<IStartSystem>();
            _updatableSystems = new List<IUpdateSystem>();
            _tickableSystems = new List<ITickSystem>();
            
            RegisterSystem(new CmdSystem(this));

            // Game Mode 
            // RegisterSystem(new ModeSystem(this));
            
            // Scene Object Management
            RegisterSystem(new OverlaySystem(this));
            RegisterSystem(new SceneCreationSystem(this));
            RegisterSystem(new UnitCreationSystem(this));
            // RegisterSystem(new MouseCtrlSystem(this));
            // RegisterSystem(new CameraCtrlSystem(this));
        }

        private void RegisterSystem(ClashBaseSystem system)
        {
            _systems.Add(system);
            if (system is IStartSystem)
            {
                _startableSystems.Add((IStartSystem)system);   
            }
            if (system is IUpdateSystem)
            {
                _updatableSystems.Add((IUpdateSystem)system);
            }

            if (system is ITickSystem)
            {
                _tickableSystems.Add((ITickSystem)system);
            }
        }


        public override void OnStart()
        {
            foreach (var sys in _startableSystems)
            {
                sys.OnStart();
            }
        }

        public override void OnUpdate(float deltaTime)
        {
            // Debug.Log("[clash]OnUpdate");
            foreach(var sys in _updatableSystems)
            {
                sys.OnUpdate(deltaTime);
            }
            
            HandleForLogicTick(deltaTime);
        }

        private void HandleForLogicTick(float deltaTime)
        {
            _deltaTimeCounter += deltaTime;
            if (_deltaTimeCounter >= _logicDeltaTime)
            {
                int times = (int)(_deltaTimeCounter / _logicDeltaTime);
                _deltaTimeCounter -= times * _logicDeltaTime;
                for (var i = 0;i < times;i++)
                {
                    OnTick();
                }
            }
        }


        private void OnTick()
        {
            // Debug.Log("[clash]OnTick");
            foreach (var sys in _tickableSystems)
            {
                sys.OnTick(_frameIndex);
            }
            _frameIndex++;
        }

        public new void Dispose()
        {
            base.Dispose();
            Debug.Log("ClashWorld::Dispose()");
            
            DisposeAllSystems();
            
            // dispose configs
            _gameData = null;
            
            _allUnitsConfig.Dispose();
            _allUnitsConfig = null;
        }

        private void DisposeAllSystems()
        {
            foreach (var sys in _systems)
            {
                sys.Dispose();
            }           
            _systems.Clear();
            _startableSystems.Clear();
            _updatableSystems.Clear();
            _tickableSystems.Clear();
        }
    }
}