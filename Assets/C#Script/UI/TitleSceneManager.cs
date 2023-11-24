using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
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
