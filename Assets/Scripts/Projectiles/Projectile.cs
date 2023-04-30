using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    private AttackDetails attackDetails;
    private float speed, travelDistance, xStartPosition;

    private Rigidbody2D rb;

    private bool gravitySwitch, grounded;

    [SerializeField]
    private float gravity, damageRadius;

    [SerializeField]
    private LayerMask whatIsGround, whatIsPlayer;

    [SerializeField]
    private Transform damagePosition;
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.gravityScale = 0.0f;
        rb.velocity = transform.right * speed;

        xStartPosition = transform.position.x;

    }

    private void Update()
    {
        if (!grounded) { 

            attackDetails.position = transform.position;

            if (gravitySwitch)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private void FixedUpdate()
    {

        if (!grounded)
        {

            Collider2D damageHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsPlayer);
            Collider2D groundHit = Physics2D.OverlapCircle(damagePosition.position, damageRadius, whatIsGround);

            if (damageHit)
            {
                damageHit.transform.SendMessage("Damage", attackDetails);
                Destroy(gameObject);
            }

            if (groundHit)
            {
                grounded = true;
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;

            }
            if (Mathf.Abs(xStartPosition - transform.position.x) >= travelDistance && !gravitySwitch) // after this distance, turn on the gravity
            {
                gravitySwitch = true;
                rb.gravityScale = gravity;
            }
        }
    }

    public void Shoot(float speed, float travelDistance, float damage)
    {
        this.speed = speed;
        this.travelDistance = travelDistance;
        attackDetails.damageAmount = damage;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
    }

}
