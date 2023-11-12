using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerInfo : Singleton<PlayerInfo>
{
    public float speed = 5;
    public float accelerate => speed * accelerateMult;
    public float accelerateMult = 8;
    public int airJumpChance = 1;
    public bool flyAbility = false;

    public float fallDeathVelocity = 10f;
}
