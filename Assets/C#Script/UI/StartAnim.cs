using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnim : MonoBehaviour
{
    private static bool firstEnter = true;

    private void Start()
    {
        if (firstEnter)
        {
            GetComponent<Animator>().Play("TitleAnim");
        }
        firstEnter = false;
    }

    public void ShowTitle()
    {
        ShakeScene(0.1f);
        //播放音效
    }

    public void ShakeScene(float time)
    {
        StartCoroutine(ShakeCoroutine(time));
    }

    private IEnumerator ShakeCoroutine(float time)
    {
        var rect = GetComponent<RectTransform>();
        Vector2 startPos = rect.position;
        float timer = 0;
        while(timer < time)
        {
            timer += Time.deltaTime;
            rect.position = startPos + Random.insideUnitCircle * 12f;
            yield return null;
        }
        rect.position = startPos;
    }
}
