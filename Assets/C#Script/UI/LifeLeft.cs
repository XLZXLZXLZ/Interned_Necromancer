using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LifeLeft : Singleton<LifeLeft>
{
    private TextMeshProUGUI TMP;

    protected override void Awake()
    {
        base.Awake();
        TMP = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        TMP.text = "x " + DieAndRevive.Instance.lifeLeft;
        EventManager.Instance.OnPlayerDie += (revive) =>
        {
            TMP.color = revive.lifeLeft <= 0 ? Color.red : Color.white;
            TMP.text = "x " + revive.lifeLeft;
        };
    }
}
