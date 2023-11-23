using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTrigger : MonoBehaviour
{
    public GameObject spell;
    public void SpellTriggerEvent()
    {
        Instantiate(spell,transform.Find("SpellTrigger").position,Quaternion.identity).GetComponent<Spell>().Shoot();
        AudioManager.Instance.PlaySe("CastSpell");
    }
}
