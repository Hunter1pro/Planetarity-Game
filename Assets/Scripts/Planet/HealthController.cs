using System;
using UnityEngine;

public class HealthController : MonoBehaviour, ITakeDamage, IHPSpawnData
{
    private IHPSlider hpSlider;

    private float maxHealth = 100;
    private float health;

    private void Start()
    {
        this.health = this.maxHealth;
    }

    private void Update()
    {
        
    }

    void ITakeDamage.TakeDamage(float damage)
    {
        this.health = Mathf.Clamp(this.health - damage, 0, this.maxHealth);

        this.hpSlider.SetHPSlider(health);

        if (health <= 0)
            this.KillPlanet();
    }

    private void KillPlanet()
    {
        MonoBehaviour.Destroy(this.gameObject);
    }

    void IHPSpawnData.SetupHPSlider(IHPSlider hpSlider)
    {
        this.hpSlider = hpSlider;
    }
}
