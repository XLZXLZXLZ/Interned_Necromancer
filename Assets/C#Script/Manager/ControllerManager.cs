using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : Singleton<ControllerManager>
{

    public GameObject player;
    public bool leaveSoul;
    private Monster currentControl;
    public Monster CurrentControl
    {
        get { return currentControl; }
        set
        {
            if (currentControl != null)
            {
                currentControl.OnControlEnd();
                currentControl.controllable = false;
            }
            leaveSoul = value != null;

            currentControl = value;
            CameraActions.Instance.SetFocus = value != null ? value.transform : player.transform;

            if (currentControl != null)
            {
                currentControl.OnControl();
                currentControl.controllable = true;
            }
        }
    }

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentControl = null;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) { CurrentControl = null; };
    }
}
