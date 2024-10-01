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
        _graphView = new SurfaceGraphView(this)
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
        toolbar.Add(new Button(()=>RequestDataOperation(true)){text = "Save Data"});
        toolbar.Add(new Button(()=>RequestDataOperation(false)){text = "Load Data"});

        // Create node button
        var nodeCreateButton = new Button(() =>
        {
            _graphView.CreateNode("Surface Node",Vector2.zero);
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

    private void RequestDataOperation(bool bSave)
    {
        Debug.Log($"RequestDataOperation: {_fileName}");
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog("Invalid file name!", "Please enter a valid file name", "OK");
            return;
        }

        var saveUtility = SurfaceGraphSaveUtility.GetInstance(_graphView);
        if (bSave)
        {
            saveUtility.SaveGraph(_fileName);    
        }
        else
        {
            saveUtility.LoadGraph(_fileName);
        }
    }
}
