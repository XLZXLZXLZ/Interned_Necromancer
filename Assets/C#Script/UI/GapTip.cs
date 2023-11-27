using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GapTip : MonoBehaviour
{
    [SerializeField]
    [Multiline(5)]
    private string[] tips;

    [SerializeField]
    private List<string> tipsList = new();

    private TextMeshProUGUI tip;
    private float alpha = 0f;

    private void Awake()
    {
        tip = GetComponent<TextMeshProUGUI>();
    }

    public void ShowTip()
    {
        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            tip.text = "Tips: 玩得开心!";
        }
        else
            tip.text = "Tips:" + GetTip();

        alpha = 1f;
    }

    private string GetTip()
    {
        if (tipsList.Count == 0)
            tipsList = new List<string>(tips); // 将数组转换为队列

        int index = Random.Range(0, tipsList.Count);

        string t = tipsList[index];
        tipsList.Remove(tipsList[index]);

        return t;
    }

    public void FadeTip()
    {
        alpha = 0f;
    }

    void Update()
    {
        (float r, float g, float b,float a) = (tip.color.r,tip.color.g,tip.color.b,tip.color.a);
        tip.color = new Color(r, g, b, Mathf.MoveTowards(a,alpha,2 * Time.deltaTime));

        //tip.alpha = Mathf.MoveTowards(tip.alpha, alpha,Time.deltaTime);
    }
}
