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

        public void Start()
        {
            InitSystems();
        }

        private void InitSystems()
        {
            _systems = new List<ClashBaseSystem>();
            _startableSystems = new List<IStartSystem>();
            _updatableSystems = new List<IUpdateSystem>();
            _tickableSystems = new List<ITickSystem>();
            
            RegisterSystem(new SceneCreationSystem());
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
        
        public void Dispose()
        {
            base.Dispose();
            Debug.Log("ClashWorld::Dispose()");
            
            DisposeAllSystems();
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