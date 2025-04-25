using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private GameObject winScreen;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Card"))
        {
            WinScreen();
        }
    }

    public void WinScreen()
    {
        winScreen.SetActive(true);
        Debug.Log("You won!");
    }
}
