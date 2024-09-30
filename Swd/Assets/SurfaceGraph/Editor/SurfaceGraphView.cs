using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class SurfaceGraphView : GraphView
{
    private readonly Vector2 kDefaultNodeSize = new Vector2(150,200);
    
    
    public SurfaceGraphView()
    {
        StyleSheet ss = Resources.Load<StyleSheet>("SurfaceGraph");
        if(ss != null)
            styleSheets.Add(ss);
        
        SetupZoom(ContentZoomer.DefaultMinScale,ContentZoomer.DefaultMaxScale);
        
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0,grid);
        grid.StretchToParentSize();

        AddElement(GenerateEntryPointNode());
    }

    public void CreateNode(string nodeName)
    {
        AddElement(CreateSurfaceGraphNode(nodeName));
    }

    public SurfaceGraphNode CreateSurfaceGraphNode(string nodeName)
    {
        var surfaceGraphNode = new SurfaceGraphNode
        {
            title = nodeName,
            InfoText = nodeName,
            GUID = Guid.NewGuid().ToString()
        };

        var inputPort = GeneratePort(surfaceGraphNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        surfaceGraphNode.inputContainer.Add(inputPort);


        var button = new Button(()=>{
            AddChoicePort(surfaceGraphNode);
        });
        button.text = "New Choice";
        surfaceGraphNode.titleContainer.Add(button);
        
        
        surfaceGraphNode.RefreshExpandedState();
        surfaceGraphNode.RefreshPorts();
        surfaceGraphNode.SetPosition(new Rect(Vector2.zero,kDefaultNodeSize));
        
        
        return surfaceGraphNode;
    }
    
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        // var test = base.GetCompatiblePorts(startPort, nodeAdapter); // Count = 0
        var compatiblePorts = new List<Port>();
        ports.ForEach((port) =>
        {
            if (startPort != port && startPort.node != port.node)
            {
                compatiblePorts.Add(port);
            }
        });
        return compatiblePorts;
    }

    private SurfaceGraphNode GenerateEntryPointNode()
    {
        var node = new SurfaceGraphNode
        {
            title = "START",
            GUID = Guid.NewGuid().ToString(),
            InfoText = "EntryPoint",
            EntryPoint = true
        };
        
        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);
        
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }


    private Port GeneratePort(SurfaceGraphNode node, Direction portDirection,Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }


    private void AddChoicePort(SurfaceGraphNode node)
    {
        var generatePort = GeneratePort(node, Direction.Output);

        var outputPortCount = node.outputContainer.Query("connector").ToList().Count;
        generatePort.portName = $"Choise {outputPortCount}";
        node.outputContainer.Add(generatePort);
        node.RefreshExpandedState();
        node.RefreshPorts();
    }

}
