using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 30;
    [SerializeField] private Collider weapon;

    private int _currentHealth;
    private Animator _animator;
    // Start is called before the first frame update
    void Awake()
    {
        _currentHealth = startingHealth;
        _animator = GetComponent<Animator>();
        DisableWeapons();
    }

    public bool isDead()
    { 
    return _currentHealth <= 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PlayerWeapon"))
        {
            TakeDamage(10);
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
        if ((_currentHealth > 0))

            _animator.SetTrigger("Hit");        
        else
        {
            _animator.SetTrigger("Dead");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
