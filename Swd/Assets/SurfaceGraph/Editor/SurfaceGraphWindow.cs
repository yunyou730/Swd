using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class SurfaceGraphWindow : EditorWindow
{
    private SurfaceGraphView _graphView = null;
    private string _fileName = "my surface graph";
    
    
    [MenuItem("SurfaceGraph/Surface Graph")]
    public static void OpenSurfaceGraphWindow()
    {
        var window = GetWindow<SurfaceGraphWindow>();
        window.titleContent = new GUIContent("Surface Graph Window");
    }


    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
        GenerateMinimap();
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
    
    private void ConstructGraphView()
    {
        _graphView = new SurfaceGraphView
        {
            name = "Surface Graph"
        };
        
        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }
    
    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();
        
        // Graph name and saving graph
        var fileNameTextField = new TextField("File Name:");
        fileNameTextField.SetValueWithoutNotify(_fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback((evt) =>
        {
            _fileName = evt.newValue;
        });
        toolbar.Add(fileNameTextField);
        
        // Save and Load button
        toolbar.Add(new Button(SaveData){text = "Save Data"});
        toolbar.Add(new Button(LoadData){text = "Load Data"});

        // Create node button
        var nodeCreateButton = new Button(() =>
        {
            _graphView.CreateNode("Surface Node");
        });
        nodeCreateButton.text = "Create Node";
        toolbar.Add(nodeCreateButton);
        rootVisualElement.Add(toolbar);
    }
    
    private void GenerateMinimap()
    {
        var minimap = new MiniMap() { anchored = true, };
        minimap.SetPosition(new Rect(10,30,200,140));
        _graphView.Add(minimap);
    }

    private void SaveData()
    {
        Debug.Log($"Save Data: {_fileName}");
        SurfaceGraphSaveUtility.GetInstance(_graphView).SaveGraph(_fileName);
    }
    
    private void LoadData()
    {
        Debug.Log($"Load Data: {_fileName}");
    }
    
}
