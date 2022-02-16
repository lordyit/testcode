using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    [SerializeField] ParticleSystem gunBlast;

    public void GunBlast()
    {
        if (Input.GetButton("Fire1"))
        gunBlast.Emit(50);
    }
}
