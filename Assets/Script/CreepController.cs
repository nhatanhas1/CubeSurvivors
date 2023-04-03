using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] PlayerController playerController;
    [SerializeField] float speed = 10;
    // Start is called before the first frame update
    void Start()
    {
        float speed = 10;
        rb = GetComponent<Rigidbody>();    
        playerController = FindAnyObjectByType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if(playerController != null)
        {
            if ((playerController.transform.position - transform.position).magnitude > 0.5f)
            {
                //Vector3 direction = (playerController.transform.position - transform.position).normalized;
                //rb.velocity = direction * speed;
                Vector3 direction = (playerController.transform.position - transform.position).normalized;
                Vector3 newPosition = transform.position + (direction * Time.deltaTime * speed);
                rb.MovePosition(newPosition);
            }
        }
                
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerController>().Dead();
        }
    }
}
