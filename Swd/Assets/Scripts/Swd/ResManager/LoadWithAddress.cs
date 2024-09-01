using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadWithAddress : MonoBehaviour
{
    public string _address;
    private AsyncOperationHandle<GameObject> _handle;
    
    // Start is called before the first frame update
    void Start()
    {
        _handle = Addressables.LoadAssetAsync<GameObject>(_address);
        _handle.Completed += HandleOnCompleted;
    }


    private void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Debug.Log("LoadWithAddress release!");
        //     Addressables.Release(_handle);
        // }
    }

    private void HandleOnCompleted(AsyncOperationHandle<GameObject> op)
    {
        if (op.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(op.Result, transform);
        }
        else
        {
            Debug.LogError($"Load {_address} failed.");
        }
    }

    private void OnDestroy()
    {
        Addressables.Release(_handle);
    }
}
