using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public int MaxHealth;
    public int currentHealth;
    public int damagedelay;
    
    public Character character;
    public Enemy enemy;
    public bool IsPlayer;

    public Slider HealthSlider;
    public GameObject DeathEffect;

    public UnityEvent OnDie;

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

        if(IsPlayer) ScreenCamShake.Instance.ShakeCamera(2, 0.2f);

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
            OnDie?.Invoke();
            if (DeathEffect) Instantiate(DeathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void Heal(int amount){
	currentHealth += amount;
        SoundManager.PlaySound(SoundManager.Sound.Health, 0.2f);

        currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        if (HealthSlider)
        {
            HealthSlider.value += amount;

        }
    }

}
