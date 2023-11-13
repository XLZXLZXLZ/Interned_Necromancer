using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class TipsOnScreen : PanelBase
{
    public override int panelSortingLayer => 0;
    public override bool isHideDirectly => false;
    
    [SerializeField] private TextMeshProUGUI tipText;

    protected override void Init()
    {
        base.Init();
    }

    private void OnEnable()
    {
        tipText.color = Consts.transparent;
    }

    public override void OnShow()
    {
        base.OnShow();
        Debug.Log("OnShow");
        tipText.DOColor(Consts.full,Consts.colorTranformTime);
    }

    public override void OnShowingAndCall()
    {
        Debug.Log("OnShowingAndCall");
        tipText.DOColor(Consts.full,Consts.colorTranformTime);
    }

    public override void OnHide()
    {
        base.OnHide();
        Debug.Log("OnHide");
        var callback = tipText.DOColor(Consts.transparent,Consts.colorTranformTime);
        callback.onComplete -= ClearSelfCache;
        callback.onComplete += ClearSelfCache;
    }
}
