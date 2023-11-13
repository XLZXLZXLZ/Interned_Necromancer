using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{    
    [SerializeField] private Transform panelRoot;

    [SerializeField] private Image[] panelLayers;

    [SerializeField] private Image uiMask;

    [SerializeField] private PanelContainer panelContainer;
    
    private Dictionary<Type, PanelBase> panelDic = new();
    private Dictionary<Type, PanelBase> panelOnShowing = new();
    private Dictionary<int, bool> isHavePanelShowLayer = new();

    private void Start()
    {
        panelContainer.panels.ForEach(panel => panelDic.Add(panel.GetType(), panel));
        isHavePanelShowLayer = new ()
        {
            {0,false},
            {1,false},
            {2,false},
            {3,false},
            {4,false},
        };
    }
    
    public T ShowPanel<T>() where T : PanelBase
    {
        if (!panelDic.ContainsKey(typeof(T)) 
            || panelOnShowing.ContainsKey(typeof(T))) return null;
        
        T relevantPanel = panelDic[typeof(T)] as T;
        int relevantPanelSortingLayer = relevantPanel.panelSortingLayer;
        
        if (isHavePanelShowLayer[relevantPanel.panelSortingLayer]) return null;
        
        T panel = PoolManager.Instance.GetGameObject(relevantPanel,panelLayers[relevantPanelSortingLayer].transform);
        panel.OnShow();
        
        panelOnShowing.Add(typeof(T),panel);
        isHavePanelShowLayer[relevantPanelSortingLayer] = true;
        panelLayers[relevantPanelSortingLayer].raycastTarget = true;

        return panel;
    }

    public PanelBase ShowPanel(Type panelType)
    {
        if (!panelType.IsAssignableFrom(typeof(PanelBase)) ||
            panelOnShowing.ContainsKey(panelType) || 
            !panelDic.ContainsKey(panelType)) return null;
        
        PanelBase relevantPanel = panelDic[panelType];
        int relevantPanelSortingLayer = relevantPanel.panelSortingLayer;
        if (isHavePanelShowLayer[relevantPanel.panelSortingLayer]) return null;
        
        PanelBase panel = PoolManager.Instance.GetGameObject(relevantPanel,panelLayers[relevantPanelSortingLayer].transform);
        panel.OnShow();
        
        panelOnShowing.Add(panelType,panel);
        isHavePanelShowLayer[relevantPanelSortingLayer] = true;
        panelLayers[relevantPanelSortingLayer].raycastTarget = true;

        return panel;
    }
    
    public void HidePanel<T>() where T : PanelBase
    {
        if (!panelOnShowing.ContainsKey(typeof(T))) return;

        T relevantPanel = panelOnShowing[typeof(T)] as T;
        int relevantPanelSortingLayer = relevantPanel.panelSortingLayer;
        
        relevantPanel.OnHide();
        
        PoolManager.Instance.PushGameObject(relevantPanel.gameObject);
        panelOnShowing.Remove(typeof(T));
        isHavePanelShowLayer[relevantPanel.panelSortingLayer] = false;
        panelLayers[relevantPanelSortingLayer].raycastTarget = false;
    }

    public void HidePanel(Type panelType)
    {
        if (!panelType.IsAssignableFrom(typeof(PanelBase))) return;
        if (!panelOnShowing.ContainsKey(panelType)) return;

        PanelBase relevantPanel = panelOnShowing[panelType];
        int relevantPanelSortingLayer = relevantPanel.panelSortingLayer;
        
        relevantPanel.OnHide();
        
        PoolManager.Instance.PushGameObject(relevantPanel.gameObject);
        panelOnShowing.Remove(panelType);
        isHavePanelShowLayer[relevantPanel.panelSortingLayer] = false;
        panelLayers[relevantPanelSortingLayer].raycastTarget = false;
    }
    
    public T GetPanelOnShowing<T>() where T : PanelBase
    {
        return panelOnShowing.ContainsKey(typeof(T)) ? panelOnShowing[typeof(T)] as T : null;
    }
    
    // public async UniTask LoadSceneAsync(string sceneName,Action onLoadCompeleted = null)
    public void LoadSceneAsync(string sceneName,Action onLoadCompeleted = null)
    {
        SetMask(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            if (operation.progress >= 0.9f)
                break;
        }

        operation.allowSceneActivation = true;
        // await UniTask.DelayFrame(5);
        onLoadCompeleted?.Invoke();

        SetMask(false);
    }

    private void SetMask(bool b)
    {
        uiMask.enabled = b;
    }
}