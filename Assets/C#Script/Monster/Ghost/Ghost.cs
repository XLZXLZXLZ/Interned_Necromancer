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

        // �����ƶ�����
        Vector3 movement = new Vector2(horizontalInput, verticalInput) * speed * Time.deltaTime;

        // �����µ�λ��
        Vector3 newPosition = transform.position + movement;

        // �����µ�λ���ڰ뾶ΪboundaryRadius��Բ��
        Vector3 offset = newPosition - transform.parent.position;
        if (offset.magnitude > boundaryRadius)
        {
            offset = offset.normalized * boundaryRadius;
            newPosition = transform.parent.position + offset;
        }

        // ���������λ��
        transform.position = newPosition;
    }
}
