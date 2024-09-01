using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadWithLabel : MonoBehaviour
{
        
    AsyncOperationHandle<IList<GameObject>> loadHandle;

    void Start()
    {
        IList<string> keys = new List<string>() { "longkui" };
        float x = 0, z = 0;
        loadHandle = Addressables.LoadAssetsAsync<GameObject>(keys, (GameObject prefab) =>
        {
            if (prefab != null)
            {
                Instantiate(prefab, new Vector3(x++ * 2.0f, 0, z++ * 2.0f), Quaternion.identity, transform);
                if (x > 9)
                {
                    x = 0;
                    z++;
                }
            }
        }, Addressables.MergeMode.Union,false);
}

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnDestroy()
    {
        Addressables.Release(loadHandle);
    }

    // Action<TObject> callback
    void OnAssetCallback(GameObject go)
    {
        
    }

}
