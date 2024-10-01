
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor.Experimental;
using UnityEditor.Experimental.GraphView;

public class SurfaceGraphSaveUtility
{
    private SurfaceGraphView _targetGraphView = null;

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
                BaseNodeGuid = inputNode.GUID,
                PortName = edge.output.portName,
                TargetNodeGuid = outputNode.GUID
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
        
    }

    // List<Edge> GetAllEdges()
    // {
    //     return _targetGraphView.edges.ToList();
    // }
}
