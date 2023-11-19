using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster
{
    public float boundaryRadius = 2;
    public float speed = 2;
    private void Update()
    {
        if (!controllable)
            return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 计算移动向量
        Vector3 movement = new Vector2(horizontalInput, verticalInput) * speed * Time.deltaTime;

        // 计算新的位置
        Vector3 newPosition = transform.position + movement;

        // 限制新的位置在半径为boundaryRadius的圆内
        Vector3 offset = newPosition - transform.parent.position;
        if (offset.magnitude > boundaryRadius)
        {
            offset = offset.normalized * boundaryRadius;
            newPosition = transform.parent.position + offset;
        }

        // 更新物体的位置
        transform.position = newPosition;
    }
}
