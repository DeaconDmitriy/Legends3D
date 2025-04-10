using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseControl : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float moveSpeed = 10;


    private CharacterController characterController;
    private Vector3 targetPosition;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distToTarget = Vector3.Distance(transform.position, targetPosition);
        if(distToTarget < 1f)
        {
            Vector3 targetDirection = Vector3.Normalize(transform.position - targetPosition);
            characterController.Move(targetDirection * moveSpeed * Time.deltaTime);
            transform.LookAt(targetDirection);
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit, 500, layerMask))
            {
                Debug.Log("hit: " + hit.collider.name);
                targetPosition = hit.point;
            }
        }
    }
}
