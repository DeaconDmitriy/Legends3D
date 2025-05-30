using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public CharacterController characterController;
    public float moveSpeed = 20;
    public Collider[] swordColliders;
    public LayerMask layerMask;
    public float rotationSpeed;
    public AudioSource attack;
    public AudioSource walk;
    public AudioSource spin;

    void Start()
    {
        EndAttack();
    }

    void Update()
    {
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * moveSpeed);

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if ((moveDirection * moveSpeed).magnitude > 0)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            walk.Play();
            animator.SetBool("Running", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.Play("Stab");
            attack.Play();
        }

        if (Input.GetMouseButtonDown(1))
        {
            animator.SetTrigger("Spin");
            spin.Play();
        }
    }
    public void BeginAttack()
    {
        foreach (Collider swordCollider in swordColliders)
        {
            swordCollider.enabled = true;
        }
    }
    public void EndAttack()
    {
        foreach (Collider swordCollider in swordColliders)
        {
            swordCollider.enabled = false;
        }
    }
}
