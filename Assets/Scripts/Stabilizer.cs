using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stabilizer : MonoBehaviour
{
    public float rotVectorScalar = 10;
    Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate()
    {
        if (rb.gravityScale == 0) return;
        
        Vector3 axis = Vector3.zero;
		
        if (transform.up != Vector3.up)
            axis = Vector3.Cross(transform.up, Vector3.up);

        Vector3 vector = axis * rotVectorScalar;
        /*
        if (vector.magnitude > 0) {
            transform.rotation = Quaternion.LookRotation(vector) * Quaternion.Euler(90, 0, 0);
        }
        */
        
        rb.AddTorque((vector - Vector3.zero).magnitude);
    }
}
