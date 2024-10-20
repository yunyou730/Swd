using System;
using clash.gameplay;
using clash.Gameplay.UserCtrl;
using LitJson;
using UnityEngine;

namespace clash.gameplay
{
    public class Gameplay : IDisposable
    {
        private ClashWorld _world = null;
        public ClashWorld World { get { return _world; } }

        
        private UserController _userCtrl = null;
        public UserController UserCtrl { get { return _userCtrl; } }


        private EClashGameMode _mode = EClashGameMode.Test;
        public EClashGameMode GameMode
        {
            set { _mode = value;}
            get { return _mode; }
        }
        
        public Gameplay()
        {
            
        }
        
        public void Start()
        {
            Camera camera = UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera>();
            var resManager = ClashGame.Instance.ResManager;
            var gameObject = ClashGame.Instance.gameObject;
            
            var gameData1 = resManager.GetAsset<ClashGameData>("Assets/Resources_moved/clashgame/data/ClashGameData_1.asset");
            var config = resManager.GetAsset<ClashConfig>("Assets/Resources_moved/clashgame/data/ClashGameConfig.asset");
            TextAsset textAsset = resManager.GetAsset<TextAsset>("Assets/Resources_moved/clashgame/config/unit/units.json");
            LitJson.JsonData unitsJsonData = JsonMapper.ToObject(textAsset.text);

            var gameData = gameData1;
            Debug.Assert(gameData != null && config != null);
            
            // World
            _world = new ClashWorld();
            _world.Init(gameData,config,unitsJsonData,gameObject,camera,resManager);
            

            // UserCtrl
            _userCtrl = new UserController(camera,_world);
            
            // OnStart
            _world.OnStart();
            _userCtrl.OnStart();
        }
        
        
        public void OnUpdate(float dt)
        {
            _world?.OnUpdate(dt);
            _userCtrl?.OnUpdate(dt);
        }
        
        
        public void Dispose()
        {
            _world?.Dispose();
            _userCtrl?.Dispose();
            
            _world = null;
            _userCtrl = null;
        }
        
        public void SwitchClashWorldToPlayMode()
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