using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void SwitchAction();
public class Switch : MonoBehaviour
{
    public SwitchAction switchOn;
    public SwitchAction switchOff;
    public bool isOn;

    private int touchCount;
    protected bool isTouch => touchCount > 0;

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isTouch)
            InteractAction();
    }


    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touchCount++;
            OnTouch(collision);
        }

        if(collision.gameObject.tag == "Monster")
        {
            if(collision.TryGetComponent(out Zombie zombie))
            {
                touchCount++;
                OnTouch(collision);
            }
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            touchCount--;
            if (!isTouch)
                OnExit(collision);
        }

        if (collision.gameObject.tag == "Monster")
        {
            if (collision.TryGetComponent(out Zombie zombie))
            {
                touchCount--;

                if(!isTouch)
                    OnExit(collision);
            }
        }
    }

    protected virtual void InteractAction()
    {

    }

    protected virtual void OnTouch(Collider2D collision)
    {

    }

    protected virtual void OnExit(Collider2D collision)
    {

    }
}
