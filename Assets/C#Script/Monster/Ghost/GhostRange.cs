using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GhostRange : MonoBehaviour
{
    Ghost ghost;

    private void Awake()
    {
        ghost = GetComponentInChildren<Ghost>();
        var ps = GetComponent<ParticleSystem>().shape;
        ps.radius = ghost.boundaryRadius;
    }
    private void Update()
    {
        var ps = GetComponent<ParticleSystem>().main;
        (float r, float g, float b) = (98f/255, 175f/255, 1 );

        ps.startColor = new Color(r, g, b, ghost.controllable ? 15f / 255 : 0f); 
    }
}
