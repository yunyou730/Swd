using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SurfaceGraphDataContainer : ScriptableObject
{
    public List<SurfaceGraphNodeData> NodeData = new List<SurfaceGraphNodeData>();
    public List<SurfaceGraphLinkData> NodeLinks = new List<SurfaceGraphLinkData>();
}
