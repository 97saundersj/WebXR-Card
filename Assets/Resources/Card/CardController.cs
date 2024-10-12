using UnityEngine;

public class CardController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private bool isOpen = false;

    private float lastClickTime;
    private const float doubleClickTime = 0.3f; // Adjust this value to change the maximum time between clicks

    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateCardState();
    }

    void OnMouseDown()
    {
        float timeSinceLastClick = Time.time - lastClickTime;
        
        if (timeSinceLastClick <= doubleClickTime)
        {
            Debug.Log("Card double-clicked");
            ToggleCard();
        }
        
        lastClickTime = Time.time;
    }

    void OnValidate()
    {
        UpdateCardState();
    }

    void ToggleCard()
    {
        isOpen = !isOpen;
        UpdateCardState();
    }

    void UpdateCardState()
    {
        if (animator != null)
        {
            animator.SetBool("isOpen", isOpen);
            Debug.Log($"Card state updated. isOpen: {isOpen}");
        }
        else
        {
            Debug.LogWarning("Animator is null. Make sure it's attached to the GameObject.");
        }
    }
}