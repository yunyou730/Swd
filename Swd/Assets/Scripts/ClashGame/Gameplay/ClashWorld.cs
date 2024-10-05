﻿using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace clash.gameplay
{
    public class ClashWorld : ClashBaseWorld
    {
        private List<ClashBaseSystem> _systems = null;

        private List<IStartSystem> _startableSystems = null;
        private List<IUpdateSystem> _updatableSystems = null;
        private List<ITickSystem> _tickableSystems = null;
        
        private ClashGameData _gameData = null;

        public void Start(ClashGameData gameData)
        {
            _gameData = gameData;
            
            InitWorldComponents();
            InitSystems();
        }
        
        private void InitWorldComponents()
        {
            var gameStart = CreateWorldComponent<GameStartWorldComponent>();
            gameStart.SceneName = _gameData.SceneName;
            gameStart.GridWidth = _gameData.GridWidth;
            gameStart.GridHeight = _gameData.GridHeight;
        }
        
        private void InitSystems()
        {
            _systems = new List<ClashBaseSystem>();
            _startableSystems = new List<IStartSystem>();
            _updatableSystems = new List<IUpdateSystem>();
            _tickableSystems = new List<ITickSystem>();
            
            RegisterSystem(new SceneCreationSystem(this));
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
            foreach(var sys in _updatableSystems)
            {
                sys.OnUpdate(deltaTime);
            }
        }

        public void Dispose()
        {
            base.Dispose();
            Debug.Log("ClashWorld::Dispose()");
            
            DisposeAllSystems();
            _gameData = null;
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