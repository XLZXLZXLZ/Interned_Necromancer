using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.SetVolume(0.5f, 0.5f);
    }

    public void StartGame()
    {
        FadeUI.Instance.Fade("ChooseScene");
        AudioManager.Instance.PlaySe("PressButton");
    }

    public void QuitGame()
    {
        FadeUI.Instance.Fade(() => Application.Quit());
        AudioManager.Instance.PlaySe("PressButton");
    }
}
