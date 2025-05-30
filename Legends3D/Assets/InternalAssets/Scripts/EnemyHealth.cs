using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 30;
    [SerializeField] private Collider weapon;
    private int _currentHealth;
    private Animator _animator;
    public bool isDead = false;
    public AudioSource dead;
    public int damagecount;

    void Awake()
    {
        _currentHealth = startingHealth;
        _animator = GetComponent<Animator>();
        DisableWeapons();
        damagecount = 5;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PlayerWeapon"))
        {
            if (!isDead)
            {
                TakeDamage(damagecount);
            }
        }
    }

    public void EnableWeapons()
    {
       weapon.enabled = true;
    }

    public void DisableWeapons()
    {
        weapon.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        if ((_currentHealth > 0) && !isDead)
        {
            _animator.SetTrigger("Hit");
        }       
        else
        {
            _animator.SetTrigger("Dead");
            isDead = true;
            dead.Play();
            SpawnEnemies.countEnemies += 1;
        }
    }

}

