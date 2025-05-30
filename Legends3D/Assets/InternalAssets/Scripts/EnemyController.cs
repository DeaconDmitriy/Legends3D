using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private EnemyHealth health;
    [SerializeField] private float timeBetweenAttacks = 2;

    private NavMeshAgent navMeshAgent;
    private Rigidbody rb;
    private Animator animator;
    private float lastAttackTime = 0;

    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (health.isDead == true)
        {
            navMeshAgent.isStopped = true;
            animator.SetBool("Run", false);
        }
        else
        {

            animator.SetBool("Run", navMeshAgent.velocity.magnitude > 0.5f);
            navMeshAgent.SetDestination(target.position);
            
            if(Vector3.Distance(transform.position, target.position)< 2.2f)
            {
                if(Time.time > lastAttackTime + timeBetweenAttacks && health.isDead == false)
                {
                    animator.SetTrigger("Attack");
                    lastAttackTime = Time.time;
                }

                if(health.isDead == true)
                {
                    animator.SetTrigger("Dead");
                }
            }
        }
        
    }
}
