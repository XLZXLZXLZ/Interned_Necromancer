using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipStone : MonoBehaviour
{
    [SerializeField, Multiline(5)] private string tipText;
    [SerializeField] private Vector3 showHeight = new Vector3(0, 5);
    private Vector2 showPos => Camera.main.WorldToScreenPoint(transform.position + showHeight);
    private TipsOnScreen tip;
    private bool enterOrNot;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tip = UIManager.Instance.ShowPanel<TipsOnScreen>();
            tip.SetText(tipText);
            enterOrNot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            tip.onHideCallback -= OnTipPanelHide;
            tip.onHideCallback += OnTipPanelHide;
            UIManager.Instance.HidePanel<TipsOnScreen>();
        }
    }

    private void Update()
    {
        if(enterOrNot)
            tip.transform.position = showPos;
    }

    private void OnTipPanelHide()
    {
        enterOrNot = false;
    }
}
