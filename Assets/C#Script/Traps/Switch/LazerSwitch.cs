using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerSwitch : Switch
{
    private int input;
    private Transform corePos;
    public Vector2 CorePos => corePos.position;

    private void Awake()
    {
        corePos = transform.Find("Core");
    }

    public void OnActive()
    {
        input++;
        isOn = true;
        switchOn?.Invoke();
        corePos.GetChild(0).gameObject.SetActive(isOn);
    }

    public void OnDisActive()
    {
        input--;
        if (input <= 0)
        {
            isOn = false;
            switchOff?.Invoke();
        }
        corePos.GetChild(0).gameObject.SetActive(isOn);
    }
}
