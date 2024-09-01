using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LoadWithLabel : MonoBehaviour
{
    [SerializeField]
    private AssetReference _test;
    [SerializeField]
    private AssetReferenceGameObject _test2;
    [SerializeField]
    private AssetReferenceT<GameObject> _test3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
