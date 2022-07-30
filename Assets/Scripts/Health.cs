using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{

    public int MaxHealth;
    public int currentHealth;
    public int damagedelay;
    
    public Character character;
    public Enemy enemy;

    public Slider HealthSlider;
    public GameObject DeathEffect;

    private void Start()
    {
        if (HealthSlider)
        {
            HealthSlider.maxValue = MaxHealth;
            HealthSlider.value = MaxHealth;

        }

        currentHealth = MaxHealth;
    }

    public void DealDamage(int Damage)
    {
        Debug.Log("Damaging");

        

        currentHealth -= Damage;
        // play damage animation
        
        if (character) character.movementSM.ChangeState(character.damaging);
        else if (enemy) enemy.movementSM.ChangeState(enemy.damaging);

        if (HealthSlider)
        {
            HealthSlider.value -= Damage;

        }

        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        if(currentHealth == 0)
        {
            if(DeathEffect) Instantiate(DeathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}