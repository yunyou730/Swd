using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class SurfaceGraphWindow : EditorWindow
{
    private SurfaceGraphView _graphView = null;
    
    
    [MenuItem("SurfaceGraph/Surface Graph")]
    public static void OpenSurfaceGraphWindow()
    {
        var window = GetWindow<SurfaceGraphWindow>();
        window.titleContent = new GUIContent("Surface Graph Window");
    }


    private void OnEnable()
    {
        _graphView = new SurfaceGraphView
        {
            name = "Surface Graph"
        };
        
        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
