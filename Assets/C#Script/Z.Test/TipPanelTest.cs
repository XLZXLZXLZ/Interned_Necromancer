using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipPanelTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            UIManager.Instance.ShowPanel<TipsOnScreen>();
        if(Input.GetKeyDown(KeyCode.W))
            UIManager.Instance.HidePanel<TipsOnScreen>();
        if(Input.GetKeyDown(KeyCode.E))
            UIManager.Instance.GetPanelOnShowing<TipsOnScreen>().SetText("鸡老师");
        if(Input.GetKeyDown(KeyCode.R))
            UIManager.Instance.GetPanelOnShowing<TipsOnScreen>().SetText("鸡老师TQL");
    }
}
