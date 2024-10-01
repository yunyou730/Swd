using System.Collections;
using System.Collections.Generic;
using Codice.Client.Common;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class SurfaceGraphNodeSearchWindow : ScriptableObject,ISearchWindowProvider
{
    private SurfaceGraphView _graphView = null;
    private EditorWindow _window = null;

    public void Init(EditorWindow window,SurfaceGraphView graphView)
    {
        _window = window;
        _graphView = graphView;
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var tree = new List<SearchTreeEntry>()
        {
            new SearchTreeGroupEntry(new GUIContent("Create Elements"),0),
            new SearchTreeGroupEntry(new GUIContent("Surface Graph"),1),
            new SearchTreeEntry(new GUIContent("Surface Graph Node"))
            {
                userData = new SurfaceGraphNode(),
                level = 2
            },
            
        };
        return tree;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var worldMousePosition = _window.rootVisualElement.ChangeCoordinatesTo(
            _window.rootVisualElement.parent,
            context.screenMousePosition - _window.position.position);
        var localMousePosition = _graphView.contentViewContainer.WorldToLocal(worldMousePosition);
        
        switch (SearchTreeEntry.userData)
        {
            case SurfaceGraphNode:
                _graphView.CreateNode("Surface Graph Node",localMousePosition);
                Debug.Log("node should create here");
                return true;
            default:
                return false;
        }
    }
}
