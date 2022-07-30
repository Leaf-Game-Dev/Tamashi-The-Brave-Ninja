using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public string TargetTag="Enemy";
    public int damageAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(TargetTag))
        {
            other.GetComponent<Health>()?.DealDamage(damageAmount);
            Destroy(gameObject);
        }
    }

}
