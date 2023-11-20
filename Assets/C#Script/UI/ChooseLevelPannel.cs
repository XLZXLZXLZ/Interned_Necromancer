using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLevelPannel : MonoBehaviour
{
    public void ChooseLevel(int level)
    {
        FadeUI.Instance.Fade("TestScene" + level);
    }

    public void BackToMenu()
    {
        FadeUI.Instance.Fade("StartScene");
    }
}
