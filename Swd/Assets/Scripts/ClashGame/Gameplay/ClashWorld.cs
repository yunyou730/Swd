﻿using System;
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
        private ClashConfig _clashConfig = null;
        private ClashAllUnitsConfig _allUnitsConfig = null;
        public ClashAllUnitsConfig AllUnitsCfg { get { return _allUnitsConfig; } }
        private UnityEngine.GameObject _rootGameObject = null;

        // Root GameObject
        public UnityEngine.GameObject RootGameObject { get { return _rootGameObject; } }
        
        
        

        // FPS & Tick Logic
        private float _logicFPS = 16.0f;
        private float _logicDeltaTime = 0.0f;
        private float _deltaTimeCounter = 0.0f;
        private int _frameIndex = 0;

        public void Start(ClashGameData gameData,
                            ClashConfig clashConfig,
                            JsonData unitsJson,
                            UnityEngine.GameObject rootGameObject,
                            ResManager resManager)
        {
            _gameData = gameData;
            _clashConfig = clashConfig;
            _rootGameObject = rootGameObject;
            _resManager = resManager;

            // configs
            _allUnitsConfig = new ClashAllUnitsConfig(this,unitsJson);
            
            InitLogicFPS(clashConfig);
            InitWorldComponents();
            InitSystems();
        }


        private void InitLogicFPS(ClashConfig clashConfig)
        {
            _logicFPS = clashConfig.kLogicFPS;
            _logicDeltaTime = 1.0f / _logicFPS;
        }

        private void InitWorldComponents()
        {
            var gameStart = CreateWorldComponent<GameStartMeta>();
            gameStart.SceneName = _gameData.SceneName;
            gameStart.GridWidth = _gameData.GridWidth;
            gameStart.GridHeight = _gameData.GridHeight;
            
            var clashConfig = CreateWorldComponent<ClashConfigMeta>();
            clashConfig.TileSize = _clashConfig.kTileSize;
            clashConfig.TileBaseX = _clashConfig.kTileBaseX;
            clashConfig.TileBaseZ = _clashConfig.kTileBaseZ;
            
            CreateWorldComponent<UnitFactoryMeta>();
        }
        
        private void InitSystems()
        {
            _systems = new List<ClashBaseSystem>();
            _startableSystems = new List<IStartSystem>();
            _updatableSystems = new List<IUpdateSystem>();
            _tickableSystems = new List<ITickSystem>();

            RegisterSystem(new SceneCreationSystem(this));
            RegisterSystem(new UnitCreationSystem(this));
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