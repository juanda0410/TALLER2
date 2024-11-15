using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitCollector : MonoBehaviour
{
    [SerializeField] private GameObject efecto;
    private Animator animator;
    public ParticleSystem Damage_PS;
    public Animator playerAnimator;
    public float hitTime;
    public float damage;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            Instantiate(efecto, transform.position, Quaternion.identity);
            Damage_PS.Play();
            player.TakeDamage(damage);
            player.hitTime = hitTime * 0.1f;
            Destroy(gameObject);
        }
    }
}