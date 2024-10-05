using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadWithReference : MonoBehaviour
{
    public AssetReference _reference;
    
    // Start is called before the first frame update
    void Start()
    {
        AsyncOperationHandle<GameObject> handle = _reference.LoadAssetAsync<GameObject>();
        handle.Completed += OnComplete;
    }
    
    private void OnComplete(AsyncOperationHandle<GameObject> op)
    {
        if (op.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(op.Result, transform);
        }
        else
        {
            Debug.LogError($"{_reference.RuntimeKey} load failed.");
        }
    }

    private void OnDestroy()
    {
        _reference.ReleaseAsset();
    }

    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Debug.Log("LoadWithReference release!");
        //     _reference.ReleaseAsset();
        // }
    }
}
