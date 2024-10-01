
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

public class SurfaceGraphSaveUtility
{
    private SurfaceGraphView _targetGraphView = null;
    private SurfaceGraphDataContainer _containerCache = null;

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<SurfaceGraphNode> Nodes => _targetGraphView.nodes.ToList().Cast<SurfaceGraphNode>().ToList();
    
    public static SurfaceGraphSaveUtility GetInstance(SurfaceGraphView targetView)
    {
        return new SurfaceGraphSaveUtility()
        {
            _targetGraphView = targetView
        };
    }
    
    public void SaveGraph(string fileName)
    {
        var dataContainer = ScriptableObject.CreateInstance<SurfaceGraphDataContainer>();
        
        // Save edges
        Edge[] connectedPortEdges = Edges.Where((e) => { return e.input.node != null; }).ToArray();
        for (var i = 0;i < connectedPortEdges.Length;i++)
        {
            var edge = connectedPortEdges[i];
            var outputNode = edge.output.node as SurfaceGraphNode;
            var inputNode = edge.input.node as SurfaceGraphNode;

            dataContainer.NodeLinks.Add(new SurfaceGraphLinkData()
            {
                BaseNodeGuid = outputNode.GUID,
                PortName = edge.output.portName,
                TargetNodeGuid = inputNode.GUID
            });
        }
        
        // Save nodes
        foreach (var surfaceGraphNode in Nodes.Where(node=>!node.EntryPoint))
        {
            dataContainer.NodeData.Add(new SurfaceGraphNodeData()
            {
                Guid = surfaceGraphNode.GUID,
                InfoText = surfaceGraphNode.InfoText,
                Position = surfaceGraphNode.GetPosition().position
            });
        }
            
        
        // Write file
        if (!Directory.Exists("Assets/Resources"))
        {
            Directory.CreateDirectory("Assets/Resources");
        }
        
        AssetDatabase.CreateAsset(dataContainer,$"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    public void LoadGraph(string fileName)
    {
        _containerCache = Resources.Load<SurfaceGraphDataContainer>(fileName);
        if (_containerCache == null)
        {
            EditorUtility.DisplayDialog("File Not Found","Target graph not exists!","OK");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }
    
    // Remove all nodes and edges, except EntryPoint node
    void ClearGraph()
    {
        // Set entry points guid back from the save.Discard exising guid.
        Nodes.Find(x => x.EntryPoint).GUID = _containerCache.NodeLinks[0].BaseNodeGuid;
        
        foreach (var node in Nodes)
        {
            if (node.EntryPoint)
                continue;
            
            // Remove edges that connected to this node
            List<Edge> validEdges = Edges.Where(x => x.input.node == node).ToList();
            validEdges.ForEach(edge=>_targetGraphView.RemoveElement(edge));
            
            // Then remove the node
            _targetGraphView.RemoveElement(node);
        }
    }

    void CreateNodes()
    {
        foreach(var nodeData in _containerCache.NodeData)
        {
            // Create the node
            var tempNode = _targetGraphView.CreateSurfaceGraphNode(nodeData.InfoText,Vector2.zero);
            tempNode.GUID = nodeData.Guid;
            _targetGraphView.AddElement(tempNode);
            
            // Create output ports of the node
            var nodePorts = _containerCache.NodeLinks.Where(
                x => x.BaseNodeGuid == nodeData.Guid
            ).ToList();
            foreach (var nodeLinkData in nodePorts)
            {
                _targetGraphView.AddChoicePort(tempNode,nodeLinkData.PortName);
            }
        }
    }

    void ConnectNodes()
    {
        for (var i = 0;i < Nodes.Count;i++)
        {
            SurfaceGraphNode currentNode = Nodes[i];
            var connections = _containerCache.NodeLinks.Where(
                x=>x.BaseNodeGuid == currentNode.GUID
            ).ToList();

            for (var j = 0;j < connections.Count;j++)
            {
                var conn = connections[j];

                var targetNodeGuid = conn.TargetNodeGuid;
                var portName = conn.PortName;
                
                // Link nodes
                var targetNode = Nodes.First(x=>x.GUID == targetNodeGuid);
                Port output = currentNode.outputContainer[j].Query<Port>();
                Port input = (Port)targetNode.inputContainer[0];
                LinkNodes(output,input);

                // Target Node Position
                SurfaceGraphNodeData targetNodeData = _containerCache.NodeData.First(x => x.Guid == targetNodeGuid); 
                targetNode.SetPosition(new Rect(
                    targetNodeData.Position,
                    _targetGraphView.kDefaultNodeSize
                ));
            }

        }
    }

    void LinkNodes(Port output,Port input)
    {
        // var tempEdge = new Edge()
        // {
        //     output = output,
        //     input = input,
        // };
        
        // tempEdge.input.Connect(tempEdge);
        // tempEdge.output.Connect(tempEdge);

        var tempEdge = new Edge();
        tempEdge.input = input;
        tempEdge.output = output;
        
        input.Connect(tempEdge);
        output.Connect(tempEdge);
        
        _targetGraphView.Add(tempEdge);
    }

}
