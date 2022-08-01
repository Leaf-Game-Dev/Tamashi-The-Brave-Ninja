using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackListiner : MonoBehaviour
{
    public int detectLayer,DamageAmount;
    public GameObject HitEffect;
    public Vector3 positionOffset, sizeOffset;
    public Vector3 effectOffset;

    [Header("Shuriken Settings")]
    public GameObject ShurikenObject;
    public Transform SpawnPoint;
    public float speed;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (transform.parent.rotation.y > 0)
        {
            positionOffset = new Vector3(Mathf.Abs(positionOffset.x), positionOffset.y, positionOffset.z);
        }
        else if (transform.parent.rotation.y < 0)
        {
            positionOffset = new Vector3(-Mathf.Abs(positionOffset.x), positionOffset.y, positionOffset.z);
        }
    }

    public void HandleAttack(int effectID)
    {
        // use partical system;
        Debug.Log("Attack Start");
        var enemies = GetEnemies();

        foreach (var enemy in enemies)
        {
            if (HitEffect)
            {
                var newEffect = Instantiate(HitEffect, enemy.transform.position+ effectOffset, Quaternion.identity);
                newEffect.transform.localScale *= 3;
                Destroy(newEffect, 2);
            }

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

        star.transform.position = new Vector3(star.transform.position.x,star.transform.position.y, transform.position.z);

        star.GetComponent<Rigidbody>().velocity = transform.forward * speed;

    }

    // sounds
    public void Step()
    {
        var speed = anim.GetFloat("speed");
        if(speed >= 0.5f)  SoundManager.PlaySound(SoundManager.Sound.FootStep, 0.1f);
    }

    public void JumpStep()
    {
        SoundManager.PlaySound(SoundManager.Sound.FootStep, 0.4f);
    }

    public void Dash()
    {
        SoundManager.PlaySound(SoundManager.Sound.Dash, 0.4f);
    }

    public void Shoot()
    {
        SoundManager.PlaySound(SoundManager.Sound.Shoot,transform.position, 0.2f);
    }

    public void Punch()
    {
        var enemies = GetEnemies();
        if(enemies.Length!=0 )
            SoundManager.PlaySound(SoundManager.Sound.Punch, transform.position, 0.4f);
    }


    public void Slash()
    {

            SoundManager.PlaySound(SoundManager.Sound.Slash, transform.position, 0.4f);
    }

}
