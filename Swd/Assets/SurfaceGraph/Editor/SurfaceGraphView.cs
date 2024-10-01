using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class SurfaceGraphView : GraphView
{
    public readonly Vector2 kDefaultNodeSize = new Vector2(200,200);
    private SurfaceGraphNodeSearchWindow _searchWindow = null;


    public SurfaceGraphView(EditorWindow editorWindow)
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
        AddSearchWindow(editorWindow);
    }

    public void CreateNode(string nodeName,Vector2 position)
    {
        AddElement(CreateSurfaceGraphNode(nodeName,position));
    }

    public SurfaceGraphNode CreateSurfaceGraphNode(string nodeName,Vector2 position)
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
        surfaceGraphNode.SetPosition(new Rect(position,kDefaultNodeSize));
        
        
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


    public void AddChoicePort(SurfaceGraphNode node,string portName = null)
    {
        var generatePort = GeneratePort(node, Direction.Output);

        // var oldLabel = generatePort.contentContainer.Q<Label>("type");
        // generatePort.contentContainer.Remove(oldLabel);
        

        var outputPortCount = node.outputContainer.Query("connector").ToList().Count;
        string choisePortName = string.IsNullOrEmpty(portName) ? $"Choice {outputPortCount}" : portName;
         

        // rename text field
        // var textField = new TextField()
        // {
        //     name = string.Empty,
        //     value = choisePortName,
        // };
        // textField.RegisterValueChangedCallback(evt => generatePort.portName = evt.newValue);
        // generatePort.contentContainer.Add(new Label("   "));
        // generatePort.contentContainer.Add(textField);
        
        
        // delete button
        var deleteButton = new Button(() => { RemovePort(node,generatePort);})
        {
            text = "X"  
        };
        generatePort.contentContainer.Add(deleteButton);
        
        // set port name at last
        generatePort.portName = choisePortName;
        node.outputContainer.Add(generatePort);
        node.RefreshExpandedState();
        node.RefreshPorts();
    }


    void RemovePort(SurfaceGraphNode node,Port port)
    {
        var targetEdge = edges.ToList().Where(x => x.output.portName == port.portName && x.output.node == port.node);
        // var targetEdge = edges.ToList().Where(
        //     x => x.output.portName == port.portName
        // );
        if (targetEdge.Any())
        {
            foreach (var edge in targetEdge)
            {
                edge.input.Disconnect(edge);
                RemoveElement(edge);
            }

            // var edge = targetEdge.First();
            // edge.input.Disconnect(edge);
            // RemoveElement(edge);
            // RemoveElement(targetEdge.First());
        }

        node.outputContainer.Remove(port);
        node.RefreshPorts();
        node.RefreshExpandedState();
    }

    private void AddSearchWindow(EditorWindow window)
    {
        _searchWindow = ScriptableObject.CreateInstance<SurfaceGraphNodeSearchWindow>();
        _searchWindow.Init(window,this);
        nodeCreationRequest = (context) =>
        {
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);
        };
    }

}
