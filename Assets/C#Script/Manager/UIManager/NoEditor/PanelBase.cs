using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelBase : MonoBehaviour
{
    public bool isShow { get; private set; }
    public abstract int panelSortingLayer { get;}

    protected virtual void Start()
    {
        Init();
    }

    /// <summary>
    /// 面板初始化，目前只推荐进行给按钮增加监听的操作
    /// </summary>
    protected virtual void Init()
    {
    
    }

    /// <summary>
    /// 面板显示时执行的逻辑
    /// </summary>
    public virtual void OnShow()
    {
        isShow = true;
    }

    /// <summary>
    /// 面板隐藏时执行的逻辑
    /// </summary>
    public virtual void OnHide()
    {
        isShow = false;
    }

    protected void HideSelf()
    {
        UIManager.Instance.HidePanel(this.GetType());
    }
}
