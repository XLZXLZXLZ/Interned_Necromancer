using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool controllable;
    private void OnMouseDown()
    {
        ControllerManager.Instance.CurrentControl = this;
    }

    private float timer = 0;
    private void OnMouseOver()
    {
        timer += Time.deltaTime;
    }

    protected virtual void SkillAction()
    {

    }

    public virtual void OnControl()
    {

    }
    public virtual void OnControlEnd()
    {

    }
}
