using UnityEngine;

public class Hand : MonoBehaviour
{
    public Rigidbody2D selectedCardRigidBody;
    [SerializeField] private float force = 3;
    
    [Header("Hand Settings")]
    [SerializeField] private int handIndex = 0;
    [SerializeField] private float speed = 3;
    [SerializeField] private float angularSpeed = 30;



    void Update()
    {
        float x = 0;
        float y = 0;
        
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
        }
        
        x *= Time.deltaTime;
        y *= Time.deltaTime;
        
        transform.Translate(new Vector3(x, y, 0));
        
        if (!selectedCardRigidBody) return;
        selectedCardRigidBody.AddForce((transform.position - selectedCardRigidBody.transform.position) * force);
    }
}
