using UnityEngine;

public class Hand : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    public Rigidbody2D selectedCardRigidBody;

    [SerializeField] private GameObject cardPrefab;
    
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
                y += 1;
            if (Input.GetKey(KeyCode.S))
                y -= 1;
            if (Input.GetKey(KeyCode.A))
                x -= 1;
            if (Input.GetKey(KeyCode.D))
                x += 1;
            
            if (Input.GetKey(KeyCode.Q))
                rotation += angularSpeed;
            if (Input.GetKey(KeyCode.E))
                rotation -= angularSpeed;

            if (Input.GetKeyDown(KeyCode.C))
                isSelecting = true;
        }
        else if (handIndex == 2)
        {
            if (Input.GetKey(KeyCode.I))
                y += 1;
            if (Input.GetKey(KeyCode.K))
                y -= 1;
            if (Input.GetKey(KeyCode.J))
                x -= 1;
            if (Input.GetKey(KeyCode.L))
                x += 1;
            
            if (Input.GetKey(KeyCode.U))
                rotation += angularSpeed;
            if (Input.GetKey(KeyCode.O))
                rotation -= angularSpeed;
            
            if (Input.GetKeyDown(KeyCode.N))
                isSelecting = true;
        }
        
        rotation *= Time.deltaTime;
        
        transform.Translate(speed * Time.deltaTime * new Vector3(x, y, 0).normalized);
        Debug.DrawRay(transform.position, transform.forward * 2, Color.red, 5);




        if (isSelecting && !selectedCardRigidBody)
        {
            /*
            if (Physics.SphereCast(transform.position, .5f, transform.forward, out RaycastHit hit, 2, LayerMask.GetMask("Card")))
            {
                selectedCardRigidBody = hit.collider.GetComponent<Rigidbody2D>();
            }
            */
            
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, .2f, Vector2.zero);

            if (hit.collider)
            {
                if (hit.collider.CompareTag("CardBox"))
                {
                    GameObject newCard = Instantiate(cardPrefab, transform.position, Quaternion.identity);

                    if (newCard.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                    {
                        selectedCardRigidBody = rb;
                        selectedCardRigidBody.gravityScale = 0;

                        selectedCardRigidBody.linearVelocity = Vector2.zero;
                        selectedCardRigidBody.angularVelocity = 0f;

                        newCard.transform.position = transform.position;
                    }
                }
                else if (hit.collider.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
                {
                    selectedCardRigidBody = rb;
                    selectedCardRigidBody.gravityScale = 0;
                }
            }
        }
        else if (isSelecting && selectedCardRigidBody)
        {
            selectedCardRigidBody.gravityScale = 1;
            selectedCardRigidBody = null;
        }
        
        
        
        
        
        // Sprite
        if (selectedCardRigidBody == null)
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
