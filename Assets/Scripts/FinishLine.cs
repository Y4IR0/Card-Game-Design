using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private Hand hand1;
    [SerializeField] private Hand hand2;
    [SerializeField] private GameObject winScreen;

    private float startTime;
    private bool timing = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Card")) return;
        if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            if (hand1.selectedCardRigidBody == rb || hand2.selectedCardRigidBody == rb) return;

        float angle = collision.transform.eulerAngles.z;
        if (angle > -8 && angle < 8) return;
        if (angle > 172 && angle < -172) return;

        if (!timing)
        {
            startTime = Time.time;
            timing = true;
        }
        
        if (Time.time >= startTime + .8f && timing)
            WinScreen();
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Card")) return;

        if (timing)
        {
            startTime = Time.time;
            timing = false;
        }
    }

    public void WinScreen()
    {
        winScreen.SetActive(true);
        Debug.Log("You won!");
    }
}
