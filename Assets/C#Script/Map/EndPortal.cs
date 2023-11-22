using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPortal : MonoBehaviour
{
    private Transform core;
    [SerializeField]
    private GameObject particle;

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
            Instantiate(particle, core.transform.position, Quaternion.identity);
            Destroy(core.gameObject);
            Destroy(this);
        }
    }
}
