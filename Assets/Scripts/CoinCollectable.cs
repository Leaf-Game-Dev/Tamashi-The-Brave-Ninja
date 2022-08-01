using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectable : MonoBehaviour
{
    public GameObject coinEffect;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Character>() != null)
        {
            // show effect;
            var effect = Instantiate(coinEffect,transform.position,Quaternion.identity);
            effect.transform.localScale *= 2;
            Destroy(effect, 2);
            UIManager.instance.addCoinCount();
            SoundManager.PlaySound(SoundManager.Sound.coin, transform.position, 0.2f);

            Destroy(gameObject);
        }
    }
}
