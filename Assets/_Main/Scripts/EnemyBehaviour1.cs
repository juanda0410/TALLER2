using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour1 : MonoBehaviour
{
     public Transform[] enemyMovementPoints;

     [SerializeField] private Transform actualObjective;
     [SerializeField] private Rigidbody2D rb;
     [SerializeField] private Animator enemyAnimator;

     public float enemySpeed;
     public float detectionRadius = 0.5f;
     public float enemyHitStrengthX;
     public float enemyHitStrengthY;
     public float enemyDamage;
     public float hitTime;

    public ParticleSystem Tree_PS;

    Vector2 movement;

     // Start is called before the first frame update
     void Start()
     {
         rb = GetComponent<Rigidbody2D>();
         enemyAnimator = GetComponent<Animator>();
         actualObjective = enemyMovementPoints[1];
     }

     // Update is called once per frame
     void Update()
     {
         float distanceToObjective = Vector2.Distance(transform.position, actualObjective.position);

         if (distanceToObjective < detectionRadius)
         {
             if (actualObjective == enemyMovementPoints[0])
             {
                 actualObjective = enemyMovementPoints[1];
             }
             else if (actualObjective == enemyMovementPoints[1])
             {
                 actualObjective = enemyMovementPoints[0];
             }
         }
         Vector2 direction = (actualObjective.position - transform.position).normalized;

         int roundedDirection = Mathf.RoundToInt(direction.x);
         movement = new Vector2(roundedDirection, 0);

         if (roundedDirection < 0)
         {
             transform.localScale = new Vector3(1, 1, 1);
         }
         else if (roundedDirection > 0)
         {
             transform.localScale = new Vector3(-1, 1, 1);
         }

         enemyAnimator.SetFloat("Movement", roundedDirection);

         rb.MovePosition(rb.position + movement * enemySpeed * Time.deltaTime);
     }

     private void OnCollisionEnter2D (Collision2D collision)
     {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();

            player.TakeDamage(enemyDamage);
            player.hitTime = player.hitTime = hitTime * 0.1f;
            player.hitForceX = enemyHitStrengthX;
            player.hitForceY = enemyHitStrengthY;
            Tree_PS.Play();

            if (collision.transform.position.x <= transform.position.x)
            {
                player.hitFromRight = true;
            }
            else if (collision.transform.position.x > transform.position.x)
            {
                player.hitFromRight = false;
            }
            enemyAnimator.SetTrigger("Attack");
        }
     }
}
