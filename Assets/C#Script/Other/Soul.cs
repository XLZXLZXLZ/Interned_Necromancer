using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soul : MonoBehaviour
{
    public Vector2 targetPos;
    public GameObject monster;
    public GameObject particle;
    public bool preSet;

    private void Start()
    {
        if(preSet)
            targetPos = transform.position;
    }

    private void Update() //逐渐移向目标位置
    {
        transform.position = Vector2.Lerp(transform.position, targetPos, 3 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision) //法术进入时生成怪物
    {
        if(collision.TryGetComponent<Spell>(out var s))
        {
            Instantiate(particle,transform.position,Quaternion.identity);
            Instantiate(monster,transform.position,Quaternion.identity);

            AudioManager.Instance.PlaySe("SummonMonster");

            Destroy(s.gameObject);
            Destroy(gameObject);
        }
    }
}
