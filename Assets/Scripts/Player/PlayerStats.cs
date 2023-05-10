using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float maxHealth;

    [SerializeField] private GameObject deathChunkParticle, deathBloodParticle;

    private string identification;

    private float currentHealth;

    private LevelManager GM;

    private void Awake()
    {
        identification = GetComponent<Identifier>().identifier;
        {
            if (!GameManager.instance.loading)
            {
                currentHealth = maxHealth; // default behaviour
            } else
            {
                LoadFloatResult result = SaveLoadManager.LoadFloat(identification + "_currentHealth");
                if (result.success)
                {
                    currentHealth = result.result;
                } else
                {
                    currentHealth = maxHealth;
                }
            }

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

    #region saving

    private void OnEnable()
    {
        GameManager.instance.savePressed.AddListener(savePressed);
    }

    private void savePressed()
    {
        SaveLoadManager.SaveFloat(identification + "_currentHealth", currentHealth);
    }

    private void OnDisable()
    {
        GameManager.instance.savePressed.RemoveListener(savePressed);
    }

    #endregion
}
