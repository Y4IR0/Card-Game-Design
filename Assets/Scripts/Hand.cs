using UnityEngine;

public class Hand : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    public Rigidbody2D selectedCardRigidBody;
    
    [Header("Hand Settings")]
    [SerializeField] private int handIndex = 0;
    [SerializeField] private float speed = 3;
    [SerializeField] private float angularSpeed = 5000;
    [SerializeField] private float force = 150;
    
    [Header("Sprites")]
    [SerializeField] private Sprite handSprite1;
    [SerializeField] private Sprite handSprite2;

    private Vector3 previousPosition;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        float x = 0;
        float y = 0;

        float rotation = 0;
        
        bool isSelecting = false;
        
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

            if (Input.GetKey(KeyCode.C))
                isSelecting = true;
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
            
            if (Input.GetKey(KeyCode.Period))
                isSelecting = true;
        }
        
        x *= Time.deltaTime;
        y *= Time.deltaTime;
        rotation *= Time.deltaTime;
        
        transform.Translate(new Vector3(x, y, 0));




        if (isSelecting && !selectedCardRigidBody)
        {
            if (Physics.SphereCast(transform.position, .5f, transform.forward, out RaycastHit hit, 10, LayerMask.GetMask("Card")))
            {
                selectedCardRigidBody = hit.collider.GetComponent<Rigidbody2D>();
            }
        }
        else if (isSelecting && selectedCardRigidBody)
        {
            selectedCardRigidBody = null;
        }
        
        
        
        
        
        // Sprite
        if (selectedCardRigidBody != null)
            spriteRenderer.sprite = handSprite1;
        else
            spriteRenderer.sprite = handSprite2;
        
        
        
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
