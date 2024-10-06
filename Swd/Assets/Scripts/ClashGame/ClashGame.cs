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
        
        public ClashWorld World { get { return _world; } }
        

        private void Awake()
        {
            
        }

        public void Start()
        {
            _resManager = new ResManager();
            var gameData1 = _resManager.GetAsset<ClashGameData>("Assets/Resources_moved/clashgame/data/ClashGameData_1.asset");
            // var gameData2 = _resManager.GetAsset<ClashGameData>("Assets/Resources_moved/clashgame/data/ClashGameData_2.asset");
            var config = _resManager.GetAsset<ClashConfig>("Assets/Resources_moved/clashgame/data/ClashGameConfig.asset");
            
            TextAsset textAsset = _resManager.GetAsset<TextAsset>("Assets/Resources_moved/clashgame/config/unit/units.json");
            LitJson.JsonData unitsJsonData = JsonMapper.ToObject(textAsset.text);

            var gameData = gameData1;
            Debug.Assert(gameData != null && config != null);
            
            _world = new ClashWorld();
            _world.Start(gameData,config,unitsJsonData,gameObject,_resManager);
            _world.OnStart();
        }
        
        private void Update()
        {
            if (_world != null)
            {
                float dt = Time.deltaTime;
                _world.OnUpdate(dt);
            }
        }

        public void OnDestroy()
        {
            if (_world != null)
            {
                _world.Dispose();
                _world = null;    
            }
            _resManager.ReleaseCache();
        }


        public ResManager GetResManager()
        {
            return _resManager;
        }
        
        // [ConsoleMethod( "cube", "Creates a cube at specified position" )]
        // public static void CreateCubeAt( Vector3 position )
        // {
        //     GameObject.CreatePrimitive( PrimitiveType.Cube ).transform.position = position;
        // }
        
    }
}