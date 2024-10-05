using System;
using System.Collections;
using System.Collections.Generic;
using clash.gameplay;
using LitJson;
using swd.gameplay;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace clash
{
    public class ClashGame: MonoBehaviour
    {
        // private ClashGameEnum.EClashGameState _state = ClashGameEnum.EClashGameState.Ready;

        private ClashWorld _world = null;
        
        public void Start()
        {
            _world = new ClashWorld();
            _world.Start();
        }   
        
        public void OnDestroy()
        {
            _world.Dispose();
            _world = null;
        }
    }
}