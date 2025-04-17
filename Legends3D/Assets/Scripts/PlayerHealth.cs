using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private float timeBetweenHits = 1f;

    private int _currentHealth;
    private float lastHitTime = 0;
    private Animator animator;

    public static bool isAlive = true;

    public int CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            if (value < 0)
                _currentHealth = 0;
            else
                _currentHealth = value;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("EnemyWeapon") && isAlive && Time.time - lastHitTime > timeBetweenHits)
        {
            TakeDamage(5);
        }
    }

    public void TakeDamage(int damage)

    {
        lastHitTime = Time.time;
        _currentHealth -= damage;
        Debug.Log("Current Health: " + _currentHealth);
        if (_currentHealth > 0)
            animator.SetTrigger("HitBack");
        else
        {
            animator.SetTrigger("Death");
            isAlive = false;
        }
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        _currentHealth = startingHealth;
        isAlive = true;
    }

    void Update()
    {
        
    }
}
