using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatDummyController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float maxHealth, knockbackSpeedX, knockbackSpeedY, knockbackDuration, knockbackDeathSpeedX, knockbackDeathSpeedY, deathTorque;

    [SerializeField] bool applyKnockback;

    [SerializeField] private GameObject hitParticle;

    private float currentHealth, knockbackStart;

    private int playerFacingDirection;

    private bool playerOnLeft, knockback;

    private PlayerController pc;

    private GameObject aliveGO, brokenTopGO, brokenBotGO;

    private Rigidbody2D rbAlive, rbBrokenTop, rbBrokenBot;

    private Animator aliveAnim;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        currentHealth = maxHealth;

        pc = GameObject.Find("Player").GetComponent<PlayerController>();

        aliveGO = transform.Find("Alive").gameObject;

        brokenTopGO = transform.Find("BrokenTop").gameObject;

        brokenBotGO = transform.Find("BrokenBottom").gameObject;

        aliveAnim = aliveGO.GetComponent<Animator>();

        rbAlive = aliveGO.GetComponent<Rigidbody2D>();

        rbBrokenTop = brokenTopGO.GetComponent<Rigidbody2D>();

        rbBrokenBot = brokenBotGO.GetComponent<Rigidbody2D>();

        aliveGO.SetActive(true);
        brokenTopGO.SetActive(false);
        brokenTopGO.SetActive(false);
    }

    private void Update()
    {
        CheckKnockback();
    }

    private void Damage(float[] attackDetails)
    {
        currentHealth -= attackDetails[0];
        playerFacingDirection = pc.GetFacingDirection();

        Instantiate(hitParticle, aliveGO.transform.position, Quaternion.Euler(0.0f,0.0f, Random.Range(0.0f,360.0f)));

        if (playerFacingDirection == 1 && attackDetails[1] > aliveAnim.transform.position.x)
        {
            playerOnLeft = true;
        }
        else
        {
            playerOnLeft = false;
        }

        aliveAnim.SetBool("playerOnLeft", playerOnLeft);
        aliveAnim.SetTrigger("damage");

        if (applyKnockback && currentHealth > 0.0f) {
            //Knockback
            Knockback();
        }

        if (currentHealth <= 0.0f)
        {
            //Die
            Die();
        }
    
    }

    private void Knockback()
    {
        knockback = true;
        knockbackStart = Time.time;
        rbAlive.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
    }

    private void CheckKnockback()
    {
        if (Time.time >= knockbackStart + knockbackDuration && knockback)
        {
            knockback = false;
            rbAlive.velocity = new Vector2(0.0f, rbAlive.velocity.y);
        }
    }

    private void Die()
    {
        aliveGO.SetActive(false);
        brokenTopGO.SetActive(true);
        brokenBotGO.SetActive(true);

        brokenTopGO.transform.position = aliveGO.transform.position;
        brokenBotGO.transform.position = aliveGO.transform.position;

        rbBrokenBot.velocity = new Vector2(knockbackSpeedX * playerFacingDirection, knockbackSpeedY);
        rbBrokenTop.velocity = new Vector2(knockbackDeathSpeedX * playerFacingDirection, knockbackDeathSpeedY);
        rbBrokenTop.AddTorque(deathTorque *- playerFacingDirection, ForceMode2D.Impulse);
    }

}
