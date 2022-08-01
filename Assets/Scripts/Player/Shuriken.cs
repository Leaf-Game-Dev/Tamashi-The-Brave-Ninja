using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    public string TargetTag="Enemy";
    public int damageAmount;
    public GameObject Effect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals(TargetTag))
        {
            other.GetComponent<Health>()?.DealDamage(damageAmount);
            var effect = Instantiate(Effect,transform.position,transform.rotation);
            effect.transform.localScale *= 3;
            SoundManager.PlaySound(SoundManager.Sound.Shot, 1f);

            Destroy(effect, 2);
            Destroy(gameObject);
        }
    }

}
