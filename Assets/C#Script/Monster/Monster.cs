using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool controllable;
    public GameObject destroyParticle;

    private void OnMouseDown()
    {
        ControllerManager.Instance.CurrentControl = this;
    }

    //右键时摧毁怪物
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ControllerManager.Instance.CurrentControl = null;
            Destroy(gameObject);

            if(destroyParticle)
                Instantiate(destroyParticle,transform.position,Quaternion.identity);
        }
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
