using UnityEngine;

public class Hand : MonoBehaviour
{
    public Rigidbody2D selectedCardRigidBody;
    [SerializeField] private float force = 150;
    
    [Header("Hand Settings")]
    [SerializeField] private int handIndex = 0;
    [SerializeField] private float speed = 3;
    [SerializeField] private float angularSpeed = 5000;

    private Vector3 previousPosition;



    void Update()
    {
        float x = 0;
        float y = 0;

        float rotation = 0;
        
        if (handIndex == 1)
        {
            if (Input.GetKey(KeyCode.W))
                y += speed;
            if (Input.GetKey(KeyCode.S))
                y -= speed;
            if (Input.GetKey(KeyCode.A))
                x -= speed;
            if (Input.GetKey(KeyCode.D))
                x += speed;
            if (Input.GetKey(KeyCode.Q))
                rotation += angularSpeed;
            if (Input.GetKey(KeyCode.E))
                rotation -= angularSpeed;
        }
        else if (handIndex == 2)
        {
            if (Input.GetKey(KeyCode.I))
                y += speed;
            if (Input.GetKey(KeyCode.K))
                y -= speed;
            if (Input.GetKey(KeyCode.J))
                x -= speed;
            if (Input.GetKey(KeyCode.L))
                x += speed;
            if (Input.GetKey(KeyCode.U))
                rotation += angularSpeed;
            if (Input.GetKey(KeyCode.O))
                rotation -= angularSpeed;
        }
        
        x *= Time.deltaTime;
        y *= Time.deltaTime;
        rotation *= Time.deltaTime;
        
        transform.Translate(new Vector3(x, y, 0));
        
        if (!selectedCardRigidBody) return;
        
        // Velocity
        Vector3 newPos = transform.position;
        float velocityForce = (newPos - previousPosition).magnitude;
        Vector3 velocityVector = newPos - previousPosition;
        Vector3 newVelocity = force * velocityForce * velocityVector / (selectedCardRigidBody.mass * 10);
        
        float maxSpeed = 15;
        if (newVelocity.magnitude > maxSpeed) { newVelocity *= maxSpeed / newVelocity.magnitude; }
        
        selectedCardRigidBody.linearVelocity = new Vector3(newVelocity.x, newVelocity.y, 0);
        //selectedCardRigidBody.AddForce(transform.position - selectedCardRigidBody.transform.position * force);
        
        previousPosition = selectedCardRigidBody.transform.position;
        
        
        // Angular Velocity
        selectedCardRigidBody.angularVelocity = rotation;
    }
}
