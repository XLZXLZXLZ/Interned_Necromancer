using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPortal : MonoBehaviour
{
    private Transform core;

    private void Update()
    {
        core.position += Vector3.up * Mathf.Sin(Time.time * 2) * Time.deltaTime * 0.2f;
    }

    private void Start()
    {
        core = transform.Find("Core");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            FadeUI.Instance.Fade("ChooseScene");
            Destroy(core.gameObject);
        }
    }
}
