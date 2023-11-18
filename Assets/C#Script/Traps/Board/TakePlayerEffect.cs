using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePlayerEffect : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Monster"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Monster"))
        {
            collision.transform.parent = null;
        }
    }
}
