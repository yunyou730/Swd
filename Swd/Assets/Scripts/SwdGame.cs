using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace swd
{
    public class SwdGame : MonoBehaviour
    {
        public ResManager _resManager = null;

        private void Start()
        {
            _resManager = new ResManager();

            List<string> toLoadList = new List<string>{"Pal3Res/105/Prefabs/C03.MV3","Pal3Res/105/Prefabs/C02.MV3","Pal3Res/105/Prefabs/C01.MV3"};
            _resManager.PreloadAssetList(toLoadList,DoStart);
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
    }
}
