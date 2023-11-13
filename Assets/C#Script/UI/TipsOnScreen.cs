using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;

public class TipsOnScreen : PanelBase
{
    public override int panelSortingLayer => 0;
    public override bool isHideDirectly => false;
    
    [SerializeField] private TextMeshProUGUI tipText;

    private TweenerCore<Color, Color, ColorOptions> callback;
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
        tipText.DOColor(Consts.full,Consts.colorTranformTime);
        if(callback != null)
            callback.onComplete -= ClearSelfCache;

    }

    public override void OnShowingAndCall()
    {
        base.OnShowingAndCall();
        tipText.DOColor(Consts.full,Consts.colorTranformTime);
        if(callback != null)
            callback.onComplete -= ClearSelfCache;
    }

    public override void OnHide()
    {
        base.OnHide();
        callback = tipText.DOColor(Consts.transparent,Consts.colorTranformTime);
        callback.onComplete += ClearSelfCache;
    }

    public void SetText(string text)
    {
        tipText.text = text;
    }
}
