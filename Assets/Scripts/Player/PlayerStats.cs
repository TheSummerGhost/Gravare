using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    [SerializeField] private GameObject deathChunkParticle, deathBloodParticle;

    private float currentHealth;

    private LevelManager GM;

    private void Start()
    {
        {
            currentHealth = maxHealth;
            GM = FindObjectOfType<LevelManager>();
        }
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth -= amount; 

        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    public void Die() 
    {
        Instantiate(deathChunkParticle, transform.position, deathChunkParticle.transform.rotation);
        Instantiate(deathBloodParticle, transform.position, deathBloodParticle.transform.rotation);
        GM.Respawn();
        Destroy(gameObject);
    }
}
