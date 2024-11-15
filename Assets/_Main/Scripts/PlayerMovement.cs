using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private float movement;
    public float jumpForce;

    public float playerHealth;
    private float maxHealth = 100;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private Animator playerAnimator;

    public TextMeshProUGUI healthText;

    bool canJump;

    public float hitTime;
    public float hitForceX;
    public float hitForceY;
    public bool hitFromRight;
    public ParticleSystem Recover_PS;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerHealth = maxHealth;
        healthText.text = $"Health: {playerHealth} / {maxHealth}";

    }

    // Update is called once per frame
    void Update()
    {
        canJump = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        movement = Input.GetAxisRaw("Horizontal");

        

        playerAnimator.SetFloat("Movement", movement);

        if (hitTime <= 0)
        {
            //transform.Translate(Time.deltaTime * (Vector2.right * movement) * speed);
            rb.velocity = new Vector2(movement * speed, rb.velocity.y);
            //rb.AddForce(new Vector2 (movement * speed, 0));
        }
        else
        {
            if (hitFromRight)
            {
                rb.velocity = new Vector2(-hitForceX, hitForceY);
            }
            else if (!hitFromRight)
            {
                rb.velocity = new Vector2(hitForceX, hitForceY);
            }
            hitTime -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    public void TakeDamage(float _damage)
    {
        playerHealth -= _damage;
        healthText.text = $"Health: {playerHealth}/{maxHealth}";


        playerAnimator.SetBool("Hit", true);

        Invoke(nameof(ResetHit), 0.5f);

    }
    private void ResetHit()
    {
        playerAnimator.SetBool("Hit", false);
    }
    public void AddHealth(float health)
     {
         if (playerHealth + health > maxHealth)
         {
             playerHealth = maxHealth;
         }
         else
         {
             playerHealth += health;
         }
         healthText.text = $"Health: {playerHealth}/{maxHealth}";
    }

}
