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
            AudioManager.Instance.PlaySe("EndLevel");

            string currentSceneName = SceneManager.GetActiveScene().name;
            char lastCharacter = currentSceneName[currentSceneName.Length - 1];
            if (char.IsDigit(lastCharacter))
            {
                int index = int.Parse(lastCharacter.ToString());
                if (index >= 7)
                    FadeUI.Instance.Fade("ChooseScene");
                else
                    FadeUI.Instance.Fade("TestScene" + (index + 1));
            }
            else
            {
                FadeUI.Instance.Fade("ChooseScene");
            }
            Instantiate(particle, core.transform.position, Quaternion.identity);
            Destroy(core.gameObject);
            Destroy(this);
        }
    }
}
