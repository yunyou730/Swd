using System;
using System.Collections;
using System.Collections.Generic;
using clash;
using clash.ui;
using UnityEngine;

public abstract class ClashBaseMenu : IDisposable
{
    protected MenuManager _menuManager = null;
    protected EMenuType _menuType;
    protected GameObject _root = null;
    protected GameObject _gameObject = null;

    public ClashBaseMenu(MenuManager menuManager,EMenuType menuType,GameObject root)
    {
        _menuManager = menuManager;
        _root = root;
    }

    public virtual void Init()
    {
        _gameObject = LoadGameObject();
        _gameObject.transform.SetParent(_root.transform);
    }

    public abstract void OnEnter();
    public abstract void OnUpdate(float deltaTime);

    public virtual void OnClose()
    {
        GameObject.Destroy(_gameObject);
        _gameObject = null;
    }

    public abstract void Dispose();

    protected abstract GameObject LoadGameObject();
}
