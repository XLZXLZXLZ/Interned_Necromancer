using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleshEyeBall : MonoBehaviour
{
    public float eyeDistance = 0.20f;
    public Vector2 eyeDir => (eyeBall.position - transform.position).magnitude >= eyeDistance / 2 ? (eyeBall.position - transform.position).normalized : Vector2.zero;

    private Transform eyeBall;


    private void Awake()
    {
        eyeBall = transform.Find("EyeSprite");
    }
    private void Update()
    {
        if (!transform.parent.GetComponent<FleshEye>().controllable)
            return;
        eyeBall.position = transform.position + ((Vector3)Tools.MousePosition - transform.position).normalized * eyeDistance;
    }

}
