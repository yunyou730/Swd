using System;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

namespace swd.gameplay
{
    public class GameplayManager
    {
        private GameObject _root = null;
        private GameObject _cameraGO = null;
        private GameplayData _gameplayData;
        
        public GameplayManager(GameObject root,GameObject cameraGO)
        {
            this._cameraGO = cameraGO;
            _root = new GameObject("swd_root");
        }

        public void Prepare(JsonData jsonData,Action callback)
        {
            _gameplayData = new GameplayData(jsonData);

            List<string> loadResList = new List<string>();

            if (callback != null)
            {
                callback();
            }


        }
        
        public void Start()
        {
            CreateTerrain();
            CreateMainCharacter();
        }
        
        private void CreateTerrain()
        {
            /*
            GameObject tilePrefab = SwdGame.Instance.GetResManager().GetAsset<GameObject>("Assets/Resources_moved/terrain/TileTest.prefab");
            for (int x = 0;x < 20;x++)
            {
                for (int z = 0;z < 20;z++)
                {
                    Vector3 pos = new Vector3(x, 0, z);
                    GameObject go = GameObject.Instantiate(tilePrefab);
                    go.transform.position = pos;
                    go.transform.SetParent(_root.transform);
                }
            }
            */

        }

        private void CreateMainCharacter()
        {
            string mcTag = _gameplayData.MainCharacterTag;
            
            GameObject go = new GameObject();
            if (_root != null)
            {
                go.transform.SetParent(_root.transform);    
            }
        }
    }
}