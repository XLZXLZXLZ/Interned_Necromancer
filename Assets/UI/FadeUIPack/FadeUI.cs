using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FadeUI : Singleton<FadeUI>
{
    protected override bool IsDonDestroyOnLoad => true;

    bool isPlaying;
    Animator anim;
    GapTip tip;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        tip = GetComponentInChildren<GapTip>();
    }

    private IEnumerator LoadEnumerator(UnityAction action)
    {
        if (isPlaying)
            yield break;

        isPlaying = true;
        anim.Play("FadeIn");
        yield return new WaitForSeconds(0.1f);
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= 1f)
        {
            yield return null;
        }
        action?.Invoke();
        yield return new WaitForSeconds(0.6f);
        anim.Play("FadeOut");
        isPlaying = false;
    }

    public void Fade(UnityAction action)
    {
        StartCoroutine(LoadEnumerator(action));
    }

    public void Fade(string targetScene)
    {
        StartCoroutine(LoadEnumerator(()=>SceneManager.LoadScene(targetScene)));
    }

    public void ShowTip()
    {
        tip.ShowTip();
    }

    public void FadeTip() 
    {
        tip.FadeTip();
    }
}
