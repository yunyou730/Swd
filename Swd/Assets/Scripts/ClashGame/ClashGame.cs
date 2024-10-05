using System;
using System.Collections;
using System.Collections.Generic;
using clash.gameplay;
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


        public void Start()
        {
            _resManager = new ResManager();
            var gameData1 = _resManager.GetAsset<ClashGameData>("Assets/Resources_moved/clashgame/data/ClashGameData_1.asset");
            var gameData2 = _resManager.GetAsset<ClashGameData>("Assets/Resources_moved/clashgame/data/ClashGameData_2.asset");
            
            _world = new ClashWorld();
            _world.Start(gameData1);
            _world.OnStart();
        }
        
        private void Update()
        {
            float dt = Time.deltaTime;
            _world.OnUpdate(dt);
        }

        public void OnDestroy()
        {
            _world.Dispose();
            _world = null;
        }


        public ResManager GetResManager()
        {
            return _resManager;
        }
        
        [ConsoleMethod( "cube", "Creates a cube at specified position" )]
        public static void CreateCubeAt( Vector3 position )
        {
            GameObject.CreatePrimitive( PrimitiveType.Cube ).transform.position = position;
        }
        
    }
}