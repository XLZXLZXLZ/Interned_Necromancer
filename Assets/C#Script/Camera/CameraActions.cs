using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraActions : Singleton<CameraActions>
{
    public float leftEdge = -20, rightEdge = 20, topEdge = 20, bottomEdge = -20;
    public float[] savePoints;
    public bool focusing = true;

    public Transform SetFocus;

    Camera cam;
    Transform player_pos;//ÕÊº“Œª÷√

    private void Awake()
    {
        player_pos = GameObject.FindGameObjectWithTag("Player").transform; 
        cam = Camera.main;         
    }
             
    private void Start()        
    {       
        transform.position = player_pos.position;      
        SetFocus = player_pos;
    }

    private void FixedUpdate()
    {
        if (!focusing)
            return;
        Vector3 focus = SetFocus.position;

        focus = EdgeDetective(focus);

        cam.transform.position = Vector3.Lerp(cam.transform.position, focus + new Vector3(0, 0, -10), Time.fixedDeltaTime * 4f);
    }

    private Vector3 EdgeDetective(Vector3 focus)
    {
        float camHeight = 2f * cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;
        float camLeftEdge = cam.transform.position.x - camWidth / 2f;
        float camRightEdge = cam.transform.position.x + camWidth / 2f ;
        float camTopEdge = cam.transform.position.y + camHeight / 2f;
        float camBottomEdge = cam.transform.position.y - camHeight / 2f;

        if (focus.x < cam.transform.position.x && camLeftEdge <= leftEdge)
            focus.x = cam.transform.position.x;
        else if (focus.x > cam.transform.position.x && camRightEdge >= rightEdge)
            focus.x = cam.transform.position.x;

        if (focus.y > cam.transform.position.y && camTopEdge >= topEdge)
            focus.y = cam.transform.position.y;
        else if (focus.y < cam.transform.position.y && camBottomEdge <= bottomEdge)
            focus.y = cam.transform.position.y;

        return focus;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftEdge, bottomEdge), new Vector3(rightEdge, bottomEdge));
        Gizmos.DrawLine(new Vector3(leftEdge,topEdge), new Vector3(rightEdge, topEdge));
        Gizmos.DrawLine(new Vector3(leftEdge,topEdge),new Vector3(leftEdge, bottomEdge));
        Gizmos.DrawLine(new Vector3(rightEdge, topEdge), new Vector3(rightEdge, bottomEdge));
    }
}
