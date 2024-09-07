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
        }

        public void Prepare(JsonData jsonData,Action callback)
        {
            _gameplayData = new GameplayData(jsonData);

            List<string> loadResList = new List<string>();
            

        }
        
        public void Start()
        {
            CreateTerrain();
            CreateMainCharacter();
        }
        
        private void CreateTerrain()
        {
            
        }

        private void CreateMainCharacter()
        {
            string mcTag = _gameplayData.MainCharacterTag;
            
            GameObject go = new GameObject();
            go.transform.SetParent(_root.transform);
            
            
        }
    }
}