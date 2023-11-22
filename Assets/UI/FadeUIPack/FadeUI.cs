using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class FadeUI : Singleton<FadeUI>
{
    protected override bool IsDonDestroyOnLoad => true;

    bool isPlaying;
    Animator anim;

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
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
        yield return new WaitForSeconds(0.2f);
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
}
