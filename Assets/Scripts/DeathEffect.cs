using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour
{

    public Animator anim;

    public void EnableDeath()
    {
        anim = GetComponent<Animator>();
        Destroy(anim);
    }
}
