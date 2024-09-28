using System;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using swd.gameplay;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace swd
{
    public class SwdGame : MonoBehaviour
    {
        public static SwdGame Instance = null;
        
        [SerializeField]
        public GameObject _cameraGameObject;
        
        public ResManager _resManager = null;
        public GameplayManager _gameplay = null;

        private void Start()
        {
            Instance = this;
            
            _resManager = new ResManager();

            List<string> toLoadList = new List<string>{"Pal3Res/105/Prefabs/C03.MV3","Pal3Res/105/Prefabs/C02.MV3","Pal3Res/105/Prefabs/C01.MV3"};
            _resManager.PreloadAssetList(toLoadList,DoStart);

            LoadConfig();
            StartGameplay();
        }

        private void DoStart()
        {
            var prefab = _resManager.GetAsset("Pal3Res/105/Prefabs/C03.MV3");
            Instantiate(prefab, transform);
        }
        
        private void OnDestroy()
        {
            _resManager.ReleaseCache();
        }

        void LoadConfig()
        {
            
        }

        void StartGameplay()
        {
            TextAsset textAsset = _resManager.GetAsset<TextAsset>("configs/test1.json");
            if (textAsset != null)
            {
                Debug.Log(textAsset.text);

                // LitJson.JsonData jsonData = LitJson.pars
                LitJson.JsonData jsonData = JsonMapper.ToObject(textAsset.text);

                _gameplay = new GameplayManager(gameObject, _cameraGameObject);
                _gameplay.Prepare(jsonData, () => { _gameplay.Start(); });


                /*
                // jsonData["te"]
                if (jsonData.IsArray)
                {
                    
                }
                else if(jsonData.ContainsKey("test1"))
                {
                    Debug.Log(jsonData["test1"].ToString());
                    Debug.Log(jsonData["characters"][0]["key"].ToString());
                }
                */

            }
        }


        public ResManager GetResManager()
        {
            return _resManager;
        }

    }
}
