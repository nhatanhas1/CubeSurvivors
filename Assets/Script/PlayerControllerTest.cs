using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Rigidbody rb;
    Vector3 moveDir;
    public float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            moveDir.x = Input.GetAxisRaw("Horizontal");
            moveDir.z = Input.GetAxisRaw("Vertical");

            rb.velocity = moveDir * moveSpeed;
        }
        else
        {
            moveDir = Vector3.zero;
            rb.velocity = moveDir;
        }
    }
}
