using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.XR;

namespace swd
{
    public class ResManager
    {
        private Dictionary<string, AsyncOperationHandle<GameObject>> _cache = null;
        private Dictionary<string, AsyncOperationHandle<UnityEngine.Object>> _anyCache = null;


        public ResManager()
        {
            _cache = new Dictionary<string, AsyncOperationHandle<GameObject>>();
        }
        
        public void PreloadAssetList(List<string> assetList,Action callback)
        {
            int finishCnt = 0;
            
            var loadCallback = new Action<AsyncOperationHandle<GameObject>>((AsyncOperationHandle<GameObject> op) =>
            {
                if (op.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogWarning($"load res {op.DebugName} failed");
                }

                finishCnt++;
                if (finishCnt == assetList.Count)
                {
                    callback();
                }
            });
                
            foreach (var key in assetList)
            {
                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(key);
                _cache.Add(key,handle);
                handle.Completed += loadCallback;
            }
        }
        
        public GameObject GetAsset(string key)
        {
            if (_cache.ContainsKey(key))
            {
                return _cache[key].Result;
            }
            else
            {
                Debug.LogWarning($"res {key} not in cache!");
                
                AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(key);
                _cache.Add(key,handle);
                handle.WaitForCompletion();
                return handle.Result;
            }
        }
        
        public T GetAsset<T>(string key)
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(key);
            handle.WaitForCompletion();
            return handle.Result;
        }

        public void ReleaseCache()
        {
            foreach (var handle in _cache.Values)
            {
                Addressables.Release(handle);
            }
            _cache.Clear();
        }
        
    }
}
