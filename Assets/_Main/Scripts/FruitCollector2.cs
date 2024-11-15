using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FruitCollector2 : MonoBehaviour
{
    [SerializeField] private GameObject efecto;
    private Animator animator;
    public ParticleSystem Recover_PS;
    public int health;
    public float playerHealth;
    private float maxHealth = 100;
    public TextMeshProUGUI healthText;

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            Instantiate(efecto, transform.position, Quaternion.identity);
            player.AddHealth(health);
            Destroy(gameObject);
            Recover_PS.Play();
        }
    }
    public void ButtonCollect()
    {
        PlayerMovement player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        player.AddHealth(health);
        Recover_PS.Play();
    }
}
