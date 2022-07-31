using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectable : MonoBehaviour
{
	public int HealAmount = 50;
	public void OnTriggerEnter(Collider col){
		if(col.GetComponent<Character>()!=null){
			col.GetComponent<Health>().Heal(HealAmount );
			Destroy(gameObject);
		}
	}
}
