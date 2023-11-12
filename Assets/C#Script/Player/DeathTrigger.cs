using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    public void OnDeath()
    {
        transform.parent.GetComponent<DieAndRevive>().OnDeath();
    }

    public void OnRevive()
    {
        transform.parent.GetComponent<DieAndRevive>().OnRevive();
    }
}
