using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Entity : MonoBehaviour
{
    public D_Entity entityData;
    public FiniteStateMachine stateMachine;

    private string identification;
    public int facingDirection { get; private set; }

    public Rigidbody2D rb { get; private set; }

    public Animator anim { get; private set; }

    public GameObject aliveGO { get; private set; }
    public AnimationToStateMachine atsm { get; private set; }

    [SerializeField] private Transform wallCheck;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private Transform playerCheck;
    [SerializeField] Transform groundCheck;

    private float currentHealth;
    private float currentStunResistance; 
    private float lastDamageTime;

    public int lastDamageDirection { get; private set; }

    private Vector2 velocityWorkSpace;

    protected bool isStunned;

    protected bool isDead;

    public virtual void Awake()
    {
        aliveGO = transform.Find("Alive").gameObject;
        EntityStateManager em = FindObjectOfType<EntityStateManager>();
        if (em != null)
        {
            em.entities.Add(this);
        }

        identification = GetComponent<Identifier>().identifier;

        facingDirection = 1;
        if (!GameManager.instance.loading)
        {
            currentHealth = entityData.maxHealth; // not loading -> Default behaviour
        } else
        {
            LoadVectorResult positionResult = SaveLoadManager.LoadVector3(identification + "_Position");
            LoadFloatResult currentHealthResult = SaveLoadManager.LoadFloat(identification + "_CurrentHealth");
            
            if (currentHealthResult.success)
            {
                currentHealth = currentHealthResult.result;
            }
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
            } else
            {
                if (positionResult.success) {
                    aliveGO.transform.localPosition = positionResult.result;
                }
            }
        }

        currentStunResistance = entityData.stunResistance;


        rb = aliveGO.GetComponent<Rigidbody2D>();
        anim = aliveGO.GetComponent<Animator>();
        atsm = aliveGO.GetComponent<AnimationToStateMachine>();
    

        stateMachine = new FiniteStateMachine();
    }

    public virtual void Update()
    {
        {
            stateMachine.currentState.LogicUpdate();

            anim.SetFloat("yVelocity", rb.velocity.y);

            if (Time.time >= lastDamageTime + entityData.stunRecoveryTime)
            {
                ResetStunResistance();
            }
        }
    }

    public virtual void SaveData()
    {
        SaveLoadManager.SaveVector3(identification + "_Position", aliveGO.transform.localPosition);
        SaveLoadManager.SaveFloat(identification + "_CurrentHealth", currentHealth);
    }

    public virtual void FixedUpdate()
    {
        stateMachine.currentState.PhysicsUpdate();
    }

    public virtual void SetVelocity(float velocity)
    {
        velocityWorkSpace.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = velocityWorkSpace;
    }

    public virtual void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        velocityWorkSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        rb.velocity = velocityWorkSpace;
    }

    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, aliveGO.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }

    public virtual bool CheckGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, entityData.groundCheckRadius, entityData.whatIsGround);
    }

    public virtual bool CheckPlayerInMinAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.minAgroDistance, entityData.whatIsplayer);
    }

    public virtual bool CheckPlayerInMaxAgroRange()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.maxAgroDistance, entityData.whatIsplayer);
    }

    public virtual bool CheckPlayerInCloseRangeAction()
    {
        return Physics2D.Raycast(playerCheck.position, aliveGO.transform.right, entityData.closeRangeActionDistance, entityData.whatIsplayer);
    }

    public virtual void ResetStunResistance()
    {
        isStunned = false;
        currentStunResistance = entityData.stunResistance;
    }

    public virtual void Damage(AttackDetails attackDetails)
    {

        lastDamageTime = Time.time;

        currentHealth -= attackDetails.damageAmount;
        currentStunResistance -= attackDetails.stunDamageAmount;

        DamageHop(entityData.damageHopSpeed);

        Instantiate(entityData.hitParticle, aliveGO.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0f,360f)));

        if (attackDetails.position.x > aliveGO.transform.position.x)
        {
            lastDamageDirection = -1;
        }
        else 
        {
            lastDamageDirection = 1;
        }

        if (currentStunResistance <= 0)
        {
            isStunned = true;
        }
        if (currentHealth <= 0)
        {
            isDead = true;
        }
    }

    public virtual void DamageHop(float velocity)
    {
        velocityWorkSpace.Set(rb.velocity.x, velocity);
        rb.velocity = velocityWorkSpace;
    }

    public virtual void Flip()
    {
        facingDirection *= -1;
        aliveGO.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * facingDirection * entityData.wallCheckDistance));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));

        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.closeRangeActionDistance), 0.2f); //look at playercheck position, add close range action distance
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.minAgroDistance), 0.2f);
        Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right * entityData.maxAgroDistance), 0.2f);
    }

}
