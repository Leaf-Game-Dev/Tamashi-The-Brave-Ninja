using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackListiner : MonoBehaviour
{
    public int detectLayer,DamageAmount;

    public Vector3 positionOffset, sizeOffset;

    [Header("Shuriken Settings")]
    public GameObject ShurikenObject;
    public Transform SpawnPoint;
    public float speed;

    private void FixedUpdate()
    {
        if (transform.parent.rotation.y >0) 
            positionOffset = new Vector3( Mathf.Abs(positionOffset.x), positionOffset.y, positionOffset.z);
        else if(transform.parent.rotation.y <0)
            positionOffset = new Vector3( -Mathf.Abs(positionOffset.x), positionOffset.y, positionOffset.z);
    }

    public void HandleAttack(int effectID)
    {
        // use partical system;
        Debug.Log("Attack Start");
        var enemies = GetEnemies();

        foreach (var enemy in enemies)
        {
            enemy.GetComponent<Health>().DealDamage(DamageAmount);
        }

    }

    Collider[] GetEnemies()
    {
        int newdetectLayer = 1 << detectLayer;

        Collider[] hitcollider = Physics.OverlapBox(
    new Vector3(transform.position.x+ positionOffset.x, transform.position.y + positionOffset.y, transform.position.z+ positionOffset.z),
    new Vector3(sizeOffset.x, sizeOffset.y, sizeOffset.z) / 2, Quaternion.identity, newdetectLayer);
        return hitcollider;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x + positionOffset.x, transform.position.y + positionOffset.y, transform.position.z + positionOffset.z), new Vector3(sizeOffset.x, sizeOffset.y, sizeOffset.z));
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z), new Vector3(AttackRange + SizeOffset.x, 1 + SizeOffset.y, 1 + SizeOffset.z));
    }

    public void ThrowShuriken()
    {
        Debug.Log("Attack Start");
        var enemies = GetEnemies();

  
        var star = Instantiate(ShurikenObject, SpawnPoint.position, SpawnPoint.rotation);

        star.GetComponent<Rigidbody>().velocity = transform.forward * speed;

    }


}
