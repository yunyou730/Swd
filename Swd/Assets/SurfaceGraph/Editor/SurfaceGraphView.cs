using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class SurfaceGraphView : GraphView
{
    public SurfaceGraphView()
    {
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        
        AddElement(GenerateEntryPointNode());
    }


    SurfaceGraphNode GenerateEntryPointNode()
    {
        var node = new SurfaceGraphNode
        {
            title = "START",
            GUID = Guid.NewGuid().ToString(),
            InfoText = "EntryPoint",
            EntryPoint =  true
        };
        node.SetPosition(new Rect(100,200,100,150));
        return node;
    }
}
